using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CapstoneBackEnd.Models {
    public class POLine {

        public int Id { get; set; }
        public int ProductId { get; set; }
        public int ProductOrderId { get; set; }
        public int TotalQuantity { get; set; }
        public decimal LineTotal { get; set; }


        public virtual ProductOrder ProductOrder { get; set; }
        [JsonIgnore]
        public virtual Product Product { get; set; }

        public POLine() { }
    }
}
