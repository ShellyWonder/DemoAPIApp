namespace DemoAPIClassLibrary.Models
{
    //Data Access Model
    public class ToDoModel

    {
        public int Id { get; set; }
        public string? SpecificTask { get; set; }
      
        public bool IsComplete { get; set; }
        //foreign key
        //id of the user
        public int AssignedTo { get; set; }
    }
}
