using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stage0;

internal class Student : IComparable<Student>
{
    private int id;

    public string Name { get; set; }

    public int Grade { get; set; }
    public DateTime BirthDate { get; init; }

    public int Age { get => DateTime.Now.Year - BirthDate.Year; }

    public Student()
    {
        id = new Random().Next(1, 1000);
    }

    public override string ToString() =>
         $"Student Id: {id}, Name: {Name}, BirthDate: {BirthDate.ToShortDateString()}, Age : {Age}, Grade {Grade}";

    public int CompareTo(Student? other)
    {
        return this.Grade.CompareTo(other?.Grade);
    }
}
