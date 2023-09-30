
using Ecommerce.Core.Helper;

namespace Ecommerce.Core.DTOs
{
    public class RegisterRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PositionId { get; set; }
        public string Address { get; set; }
        public string Status { get; set; } = Constants.Status.Active;
        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}
