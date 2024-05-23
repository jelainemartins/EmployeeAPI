using System.ComponentModel.DataAnnotations;

namespace EmployeeAPI.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public DateTime Birthdate { get; private set; }
        public char Sex { get; private set; }
        public string? Photo { get; private set; }

        public Employee(string firstName, string lastName, DateTime birthdate, char sex, string? photo)
        {
            FirstName = firstName ?? throw new ArgumentNullException(nameof(FirstName));
            LastName = lastName;
            Birthdate = birthdate;
            Sex = sex;
            Photo = photo;
        }
    }
}

