using AutoMapper;
using CsvHelper;
using CsvHelper.Configuration;
using Domain;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using Microsoft.OpenApi.Any;
using MimeKit;
using Presentation.Services.Interfaces;
using System;
using System.Globalization;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Text.Unicode;

namespace Presentation.Services
{
    public class EmailService : IRecieveLastCsvEmail
    {
        private readonly ApplicationContext _context;

        public EmailService(ApplicationContext context)
        {
            _context = context;
        }

        public string RecieveLastCsvEmail(string email, string password)
        {
            //var download = new DownLoadFile();
            //download.DownLoad(email, password);

            try
            {
                var isRecordBad = false;
                string badRecord = string.Empty;
                var config = new CsvConfiguration(System.Globalization.CultureInfo.InvariantCulture);
                config.MissingFieldFound = null;
                config.Delimiter = ";";
                config.BadDataFound = context =>
                {
                    isRecordBad = true;
                    badRecord = context.RawRecord;
                    //Console.WriteLine(context.RawRecord);
                };

                using (var reader = new StreamReader(@"D:\РАБОТА\Практика\Тестовое\Zapchasti\Price\price.csv"))
                using (var csv = new CsvReader(reader, config))
                {
                    csv.Read();
                    csv.ReadHeader();
                    csv.Context.RegisterClassMap<PriceItemModelMapByName>();
                    List<string> badrecords = new List<string>();

                    while (csv.Read())
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

                        //var config = new MapperConfiguration(cfg => cfg.CreateMap<PriceItemModel, PriceItem>()
                        //    .ForMember("Vendor", opt => opt.MapFrom(c => c.Vendor))
                        //    .ForMember("Number", opt => opt.MapFrom(c => c.Number))
                        //    .ForMember("SearchVendor", opt => opt.MapFrom(c => c.Vendor.ToUpper()))
                        //    .ForMember("SearchNumber", opt => opt.MapFrom(c => c.Number.ToUpper()))
                        //    .ForMember("Description", opt => opt.MapFrom(c => c.Description))
                        //    .ForMember("Price", opt => opt.MapFrom(c => c.Price))
                        //    .ForMember("Count", opt => opt.MapFrom(c => c.Count)));

                        var priceItem = PriceItemMaker(priceItemModel);

                        _context.Add(priceItem);
                        _context.SaveChanges();
                        if (badrecords.Count > 0)
                            return $"there {badrecords.Count} bad records";
                        
                    }
                }
            }
            catch (BadDataException ex) 
            {
                return "Not OK";
            }

            return "OK";

        }

        private static decimal PriceToDecimal(string price)
        {
            if (price.Contains(','))
                price.Replace(',', '.');
            return Convert.ToDecimal(price);
        }

        private static int CountToInt(string count)
        {
            try
            {
                if (count.Contains(">"))
                    return Convert.ToInt32(count.Trim('>'));
                else if (count.Contains("<"))
                    return Convert.ToInt32(count.Trim('<'));
                else if (count.Contains("-"))
                    return Convert.ToInt32(count.Substring(0, count.IndexOf('-')));
                else
                    return Convert.ToInt32(count);
            }
            catch(Exception ex) { return 0; }
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

            priceItem.Description = priceItemModel.Description;

            priceItem.Count = CountToInt(priceItemModel.Count);
            return priceItem;
        }
    }
}
