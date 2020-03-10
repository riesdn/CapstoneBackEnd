using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CapstoneBackEnd.Models {
    public class ProductOrder {

        public int Id { get; set; }
        public int VendorId { get; set; }
        public decimal OrderTotal { get; set; }

        [JsonIgnore]
        public virtual Vendor Vendor { get; set; }
        [JsonIgnore]
        public virtual IEnumerable<POLine> POLines { get; set; }

        public ProductOrder() { }

    }
}
