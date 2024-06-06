
namespace EmployeeAPI.ViewModel
{
    public class EmployeeViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? Birthdate { get; set; }
        public string Job { get; set; }
        public char? Sex { get; set; }
        public IFormFile Photo { get; set; }
    }
}
