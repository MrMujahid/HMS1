namespace HMS.Models.EntityModels
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Doctor>? Doctors { get; set; }
    
    }
}
