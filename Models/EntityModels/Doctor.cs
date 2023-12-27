namespace HMS.Models.EntityModels
{
    public class Doctor
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int DepartmentId { get; set; }
        public string? ContactNo { get; set; }
        public string? Designation {  get; set; }
        public string? Remark { get; set; }
        public Department? Department { get; set; }



    }
}