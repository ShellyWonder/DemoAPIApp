using DemoAPIClassLibrary.Models;

namespace DemoAPIClassLibrary.SQLDataAccess
{
    //Abstraction Layer
    public class ToDoData : IToDoData
    {
        private readonly ISqlDataAccess _sql;
        public ToDoData(ISqlDataAccess sql)
        {
            _sql = sql;
        }
        public Task<List<ToDoModel>> GetAllAssigned(int assignedTo)
        {
            return _sql.LoadData<ToDoModel,
                                  dynamic>("dbo.spToDo_GetAllAssigned",
                                new { AssignedTo = assignedTo },
                                "DefaultConnection");

        }
        public async Task<ToDoModel?> GetOneAssigned(int assignedTo,
                                                    int todoId)
        {
            var results = await _sql.LoadData<ToDoModel,
                                  //dynamic needed to pass through an anynomous object to a parameter
                                  dynamic>("dbo.spToDo_GetAllAssigned",
                                //anynonomous object
                                new { AssignedTo = assignedTo, ToDoId = todoId },
                                "DefaultConnection");
            return results.FirstOrDefault();
        }
        public async Task<ToDoModel?> Create(int assignedTo,
                                                    string specificTask)
        {
            var results = await _sql.LoadData<ToDoModel,
                                  //dynamic needed to pass through an anynomous object to a parameter
                                  dynamic>("dbo.spToDo_Create",
                                //anynonomous object
                                new { AssignedTo = assignedTo, SpecificTask = specificTask },
                                "DefaultConnection");
            return results.FirstOrDefault();
        }
        public Task UpdateTask(int assignedTo, int toDoId, string specificTask)
        {
            return _sql.SaveData<dynamic>("dbo.spToDo_UpdateTask",
                                new
                                {
                                    AssignedTo = assignedTo,
                                    ToDoId = toDoId,
                                    SpecificTask = specificTask
                                },
                                "DefaultConnection");

        }
        public Task CompleteTodo(int assignedTo, int toDoId)
        {
            return _sql.SaveData<dynamic>("dbo.spToDo_CompleteTodo",
                                new
                                {
                                    AssignedTo = assignedTo,
                                    ToDoId = toDoId
                                },
                                "DefaultConnection");

        }
        public Task Delete(int assignedTo, int toDoId)
        {
            return _sql.SaveData<dynamic>("dbo.spToDo_Delete",
                                new
                                {
                                    AssignedTo = assignedTo,
                                    ToDoId = toDoId
                                },
                                "DefaultConnection");

        }

    }
}
