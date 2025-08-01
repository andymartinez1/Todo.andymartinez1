using System.ComponentModel.DataAnnotations;
using TodoApi.Models;

namespace TodoApi.DTO;

public class TodoItemDTO
{
    public TodoItemDTO() { }

    public TodoItemDTO(Todo todoItem)
    {
        (Id, Name, IsComplete) = (todoItem.Id, todoItem.Name, todoItem.IsComplete);
    }

    [Key]
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public bool IsComplete { get; set; }
}
