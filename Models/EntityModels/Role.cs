using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace HMS.Models.EntityModels
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<User> Users { get; set; } = new List<User>();

    }
}
