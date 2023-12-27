using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace HMS.Models.EntityModels
{
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? FullName { get; set; }
        
        public bool Inactive { get; set; }
        public string? Password { get; set; }
        public string? ContactNo { get; set; }
        public string? Email { get;  set; }
        public string? Photo { get; set; }


        public Role Role { get; set; }
        public int? RoleId { get; set; }


    }
}