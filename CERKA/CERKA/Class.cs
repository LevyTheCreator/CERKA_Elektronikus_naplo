using System;

public class SchoolClass
{
   private string ClassName { get; set; }
   private string FormTeacher { get; set; }
   private List<Student> Students { get; set; }
    
   public SchoolClass(string className, string formTeacher, List<Student> students)
   {
        ClassName = className;
        FormTeacher = formTeacher;
        Students = students;
   }
}