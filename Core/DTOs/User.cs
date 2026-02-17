
using Core.Common;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Core.DTOs
{
    public class CreateUserDto
    {
        [Required] 
        public string Username { get; set; } = string.Empty;
        [Required]
        public string FullName { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string MobileNumber { get; set; } = string.Empty;
        [Required] 
        public string Password { get; set; } = string.Empty;
        [Required]
        [EnumDataType(typeof(UserRole))]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public UserRole Role { get; set; } 
    }

     public class UpdateUserDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Username { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string MobileNumber { get; set; } = string.Empty;

        [Required]
        [EnumDataType(typeof(UserRole))]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public UserRole Role { get; set; }
    }

}
