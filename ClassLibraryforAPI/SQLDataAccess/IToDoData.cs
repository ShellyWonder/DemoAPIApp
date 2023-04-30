using DemoAPIClassLibrary.Models;

namespace DemoAPIClassLibrary.SQLDataAccess
{
    public interface IToDoData
    {
        Task CompleteTodo(int assignedTo, int toDoId);
        Task<ToDoModel?> Create(int assignedTo, string specificTask);
        Task Delete(int assignedTo, int toDoId);
        Task<List<ToDoModel>> GetAllAssigned(int assignedTo);
        Task<ToDoModel?> GetOneAssigned(int assignedTo, int todoId);
        Task UpdateTask(int assignedTo, int toDoId, string specificTask);
    }
}