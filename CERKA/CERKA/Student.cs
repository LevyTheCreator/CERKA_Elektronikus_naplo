using System;

public class Student
{
    private string Id { get; set; }
    private string Name { get; set; }
    private string Grades { get; set; } 

    public Student(string id, string name, string grades)
    {
        Id = id;
        Name = name;
        Grades = grades;
    }
}