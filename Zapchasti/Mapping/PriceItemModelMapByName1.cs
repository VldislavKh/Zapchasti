using CsvHelper.Configuration;
using Domain;

namespace Presentation
{
    public class PriceItemModelMapByName1 : ClassMap<PriceItemModel>
    {
        public PriceItemModelMapByName1() 
        {
            Map(m => m.Vendor).Name("Бренд");
            Map(m => m.Number).Name("Каталожный номер");
            Map(m => m.Description).Name("Описание");
            Map(m => m.Price).Name("Цена, руб.");
            Map(m => m.Count).Name("Наличие");
        }
    }
}
