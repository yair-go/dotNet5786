using System;

namespace Stage0;

internal class Program
{
    static void Main(string[] args)
    {
        Student student = new Student
        {
            Name = "Yair",
            BirthDate = new DateTime(1989, 5, 23)
        };
        student.Name = "Yair Doron";
       // student.BirthDate = 1989;

        Student anotherStudent = new Student
        {
            Name = "Alice",
            BirthDate = new DateTime(1989, 5, 23)
        };

        List<Student> students = new List<Student> { student, anotherStudent };

        List<Student> students1 =
        [
            new Student
            {
                Name = "Bob",
                BirthDate = new DateTime(1990, 1, 15),
                Grade = 85
            },
            new Student
            {
                Name = "Charlie",
                BirthDate = new DateTime(1991, 2, 20),
                Grade = 92
            },
            new Student
            {
                Name = "Diana",
                BirthDate = new DateTime(1992, 3, 25),
                Grade = 78
            },
            new Student
            {
                Name = "Ethan",
                BirthDate = new DateTime(1993, 4, 30),
                Grade = 88
            },
            new Student
            {
                Name = "Fiona",
                BirthDate = new DateTime(1994, 5, 5),
                Grade = 95
            },
            new Student
            {
                Name = "George",
                BirthDate = new DateTime(1995, 6, 10),
                Grade = 80
            }

        ];

        students1.Sort();

        var students2 = students1.Where(student => student.Grade >= 30);

        foreach (var stud in students1)
        {
            Console.WriteLine(stud);
        }

        Console.WriteLine(student);
        Console.WriteLine(DateTime.Now.Add(new TimeSpan(30,0,0)));
        //anotherStudent[0];
        Greeting(student.Name);
        Greeting("Doron");

        var lecturer = new { ID = 29, Name = "Dani" };


        Console.ReadLine();
    }

    /// <summary>
    /// Displays a greeting message to the specified user.
    /// </summary>
    /// <remarks>This method writes the greeting message to the console. Ensure that the <paramref
    /// name="name"/> parameter  is not null or empty to avoid unexpected behavior.</remarks>
    /// <param name="name">The name of the user to include in the greeting. Cannot be null or empty.</param>
    private static void Greeting(string name)
    {
        Console.WriteLine($"Hello {name}");
    }
}