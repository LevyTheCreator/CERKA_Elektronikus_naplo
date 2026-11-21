using System;

public class Student
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Math { get; set; }
    public string Litrature { get; set; }
    public string History { get; set; }
    public string English { get; set; }
    public string Grammer { get; set; }

    public Student(
        string id,
        string name,
        string math,
        string litrature,
        string history,
        string english,
        string grammer
    )
    {
        Id = id;
        Name = name;
        Math = math;
        Litrature = litrature;
        History = history;
        English = english;
        Grammer = grammer;
    }
}
