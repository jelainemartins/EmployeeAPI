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
        public string Job { get; private set; }
        public string? Photo { get; private set; }

        public Employee(int id, string firstName, string lastName, DateTime birthdate, char sex, string job, string photo)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Birthdate = birthdate;
            Sex = sex;
            Job = job;
            Photo = photo;
        }

        public Employee(string firstName, string lastName, DateTime birthdate, char sex, string job, string photo)
        {
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
            Birthdate = birthdate;
            Sex = sex;
            Job = job ?? throw new ArgumentNullException(nameof(job));
            Photo = photo;
        }
    }
}

