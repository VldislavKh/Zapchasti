using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class PriceItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Column("Бренд")]
        public string Vendor {  get; set; }

        [Column("Номер запчасти")]
        public string Number { get; set; }

        [Column("Производитель для поиска")]
        public string SearchVendor { get; set; }

        [Column("Номер для поиска")]
        public string SearchNumber { get; set; }

        [Column("Наименование")]
        public string Description { get; set; }

        [Column("Цена")]
        public decimal Price { get; set; }

        [Column("Количество")]
        public int Count { get; set; }
    }
}