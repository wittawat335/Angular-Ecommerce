using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.DTOs
{
    public class PositionRequest
    {
        public string PositionName { get; set; }
        public string Status { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}
