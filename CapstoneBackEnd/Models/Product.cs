using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CapstoneBackEnd.Models {
    public class Product {

        public int Id { get; set; }
        public string PartNbr { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Unit { get; set; }
        public string PhotoPath { get; set; }
        public int VendorId { get; set; }

        //[JsonIgnore]
        public virtual Vendor Vendor { get; set; }

        public Product() { }

    }
}
