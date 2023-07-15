using CsvHelper.Configuration.Attributes;

namespace Domain
{
    public class PriceItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Name("Бренд")]
        public string Vendor {  get; set; }

        [Name("Каталожный номер")]
        public string Number { get; set; }

        [Name("Бренд для поиска")]
        public string SearchVendor { get; set; }

        [Name("Номер для поиска")]
        public string SearchNumber { get; set; }

        [Name("Описание")]
        public string Description { get; set; }

        [Name("Цена")]
        public decimal Price { get; set; }

        [Name("Наличие")]
        public int Count { get; set; }
    }
}