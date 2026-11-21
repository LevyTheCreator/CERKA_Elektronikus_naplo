using System;

public class SchoolClass
{
    public string ClassName { get; set; }
    public string FormTeacher { get; set; }
    public List<Student> Students { get; set; }

    public SchoolClass(string className, string formTeacher, List<Student> students)
    {
        ClassName = className;
        FormTeacher = formTeacher;
        Students = students;
    }
}
