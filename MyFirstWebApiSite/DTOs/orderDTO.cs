using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DTOs
{
    public class orderDTO
    {
        public int OrderId { get; set; }

        public DateTime? OrderDate { get; set; }

        public double OrderSum { get; set; }

        public int? UserId { get; set; }
        //public string? UserName { get; set; }

        public virtual ICollection<orderItemDTO> OrderItems { get; set; } = new List<orderItemDTO>();

    }
}
