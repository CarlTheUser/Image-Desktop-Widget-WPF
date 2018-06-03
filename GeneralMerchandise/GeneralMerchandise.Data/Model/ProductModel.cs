using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralMerchandise.Data.Model
{
    internal class ProductModel : PersistibleModel<long>
    {
        public static readonly long DEFAULT_IDENTITY = 0;

        public static ProductModel New(string imageFilename, string name, string brand, string description, double unitPrice, int quantity = 0)
        {
            return new ProductModel
            {
                Identity = DEFAULT_IDENTITY,
                ImageFilename = imageFilename,
                Name = name,
                Brand = brand,
                Description = description,
                UnitPrice = unitPrice,
                Quantity = quantity
            };
        }

        private ProductModel() { }

        /// <summary>
        /// The Serial Number on a product barcode
        /// </summary>
        public override long Identity { get; set; }

        public string ImageFilename { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public double UnitPrice { get; set; }
        public int Quantity { get; set; }

    }
}
