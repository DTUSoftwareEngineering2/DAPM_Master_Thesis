using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace RabbitMQLibrary.Models
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mail { get; set; }
        public Guid Organization { get; set; }
        public string HashPassword { get; set; }
        public int UserRole { get; set; }
        public int accepted { get; set; }
    }
}
