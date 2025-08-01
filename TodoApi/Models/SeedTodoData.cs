using Microsoft.EntityFrameworkCore;
using TodoApi.Data;

namespace TodoApi.Models;

public static class SeedTodoData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (
            var context = new TodoDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<TodoDbContext>>()
            )
        )
        {
            if (context.Todos.Any())
                return;

            context.Todos.AddRange(
                new Todo { Name = "Be amazing with C#", IsComplete = true },
                new Todo { Name = "Earn black belt in TSCA", IsComplete = false },
                new Todo { Name = "Master dependency injection", IsComplete = false },
                new Todo { Name = "Practice doing more Microsoft tutorials", IsComplete = false },
                new Todo { Name = "Update portfolio website", IsComplete = true },
                new Todo { Name = "Start applying for C# jobs", IsComplete = true }
            );

            context.SaveChanges();
        }
    }
}