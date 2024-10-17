namespace Rvn.Ch02;

public class ToDoTask
{
    public string Id { get; set; }
    public string Task { get; set; }
    public bool Completed { get; set; }
    public DateTime DueDate { get; set; }
    
    public string AssignedTo { get; set; }
    public string CreatedBy { get; set; }
}