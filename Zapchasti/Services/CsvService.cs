using CsvHelper;
using CsvHelper.Configuration;
using Domain;
using Presentation.Services.Interfaces;
using System.Text.RegularExpressions;


namespace Presentation.Services
{
    public class CsvService : IPutPriceItemsToContext
    {
        private readonly ApplicationContext _context;
        private readonly IRecieveLastCsvEmail _recieveLastCsvEmail;

        public CsvService(ApplicationContext context, IRecieveLastCsvEmail recieveLastCsvEmail)
        {
            _context = context;
            _recieveLastCsvEmail = recieveLastCsvEmail;
        }

        public async Task<string> PutPriceItemsToContext(string email, string password, int providerId, string path)
        {

            _recieveLastCsvEmail.RecieveLastCsvEmail(email, password, providerId, path);

            var isRecordBad = false;
            string badRecord = string.Empty;
            var config = new CsvConfiguration(System.Globalization.CultureInfo.InvariantCulture);
            config.MissingFieldFound = null;
            config.Delimiter = ";";
            config.LineBreakInQuotedFieldIsBadData = true;
            config.Mode = CsvMode.NoEscape;
            config.BadDataFound = context =>
            {
                isRecordBad = true;
                badRecord = context.RawRecord;
            };
            var items = new List<PriceItem>();

            using (var reader = new StreamReader(@path))
            using (var csv = new CsvReader(reader, config))
            {
                await csv.ReadAsync();
                csv.ReadHeader();

                switch(providerId)
                {
                    case 1: 
                        csv.Context.RegisterClassMap<PriceItemModelMapByName1>();
                        break;
                    default: return "Нет конфигурации для данного поставщика!";
                }
                
                List<string> badrecords = new List<string>();

                while (await csv.ReadAsync())
                {
                    var record = csv.GetRecord<PriceItemModel>();

                    if (isRecordBad)
                    {
                        badrecords.Add(badRecord);
                        isRecordBad = false;
                    }

                    var priceItemModel = new PriceItemModel()
                    {
                        Vendor = record.Vendor,
                        Number = record.Number,
                        Description = record.Description,
                        Price = record.Price,
                        Count = record.Count,
                    };

                    if (!isRecordBad)
                    {
                        var priceItem = PriceItemMaker(priceItemModel);
                        items.Add(priceItem);
                    }
                }
            }

            var chunks = items.Chunk(2000);
            foreach (var chunk in chunks)
            {
                _context.AddRange(chunk);
                await _context.SaveChangesAsync();
            }

            return "OK";
        }

        private static decimal PriceToDecimal(string price)
        {
            if (price.Contains(','))
                price.Replace(',', '.');
            try
            {
                return Convert.ToDecimal(price);
            }
            catch { return 0; }
        }

        private static int CountToInt(string count, PriceItemModel item)
        {
            try
            {
                if (string.IsNullOrEmpty(count))
                    return 0;
                if (count.Contains(">"))
                    return Convert.ToInt32(count.Trim('>'));
                else if (count.Contains("<"))
                    return Convert.ToInt32(count.Trim('<'));
                else if (count.Contains("-"))
                    return Convert.ToInt32(count.Substring(0, count.IndexOf('-')));
                else
                    return Convert.ToInt32(count);
            }
            catch
            {
                var a = item;
                throw new InvalidOperationException($". Count: {count}. Number: {item.Number}, {item.Price}, {item.Vendor}");
            }
        }

        private static string DescriptionCutter(string description)
        {
            if (description.Length > 512)
                description = description.Substring(0, 512);
            return description;
        }

        private static string StringToSearchFormat(string str)
        {
            return Regex.Replace(str, "[^a-zA-Z0-9]", String.Empty).ToUpper();
        }

        private static PriceItem PriceItemMaker(PriceItemModel priceItemModel)
        {
            var priceItem = new PriceItem();
            priceItem.Vendor = priceItemModel.Vendor;
            priceItem.Number = priceItemModel.Number;

            priceItem.SearchVendor = StringToSearchFormat(priceItemModel.Vendor);
            priceItem.SearchNumber = StringToSearchFormat(priceItemModel.Number);

            priceItem.Price = PriceToDecimal(priceItemModel.Price);

            priceItem.Description = DescriptionCutter(priceItemModel.Description);

            priceItem.Count = CountToInt(priceItemModel.Count, priceItemModel);
            return priceItem;
        }
    }
}
