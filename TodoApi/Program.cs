using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.DTO;
using TodoApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<TodoDbContext>(opt =>
    opt.UseSqlServer(
        builder.Configuration.GetConnectionString("TodoListDatabase")
        ?? throw new InvalidOperationException(
            "Connection string 'TodoListDatabase' not found."
        )
    )
);

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Initialize the database with seed data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    SeedTodoData.Initialize(services);
}

app.UseDefaultFiles();
app.UseStaticFiles();

var todoItems = app.MapGroup("/api/todoitems");

todoItems.MapGet("/", GetAllTodos);
todoItems.MapGet("/complete", GetCompleteTodos);
todoItems.MapGet("/{id}", GetTodo);
todoItems.MapPost("/", CreateTodo);
todoItems.MapPut("/{id}", UpdateTodo);
todoItems.MapDelete("/{id}", DeleteTodo);

static async Task<IResult> GetAllTodos(TodoDbContext db)
{
    return TypedResults.Ok(await db.Todos.Select(x => new TodoItemDTO(x)).ToArrayAsync());
}

static async Task<IResult> GetCompleteTodos(TodoDbContext db)
{
    return TypedResults.Ok(
        await db.Todos.Where(t => t.IsComplete).Select(x => new TodoItemDTO(x)).ToListAsync()
    );
}

static async Task<IResult> GetTodo(int id, TodoDbContext db)
{
    return await db.Todos.FindAsync(id) is Todo todo
        ? TypedResults.Ok(new TodoItemDTO(todo))
        : TypedResults.NotFound();
}

static async Task<IResult> CreateTodo(TodoItemDTO todoItemDTO, TodoDbContext db)
{
    var todoItem = new Todo { IsComplete = todoItemDTO.IsComplete, Name = todoItemDTO.Name };

    db.Todos.Add(todoItem);
    await db.SaveChangesAsync();

    todoItemDTO = new TodoItemDTO(todoItem);

    return TypedResults.Created($"/todoitems/{todoItem.Id}", todoItemDTO);
}

static async Task<IResult> UpdateTodo(int id, TodoItemDTO todoItemDTO, TodoDbContext db)
{
    var todo = await db.Todos.FindAsync(id);

    if (todo is null)
        return TypedResults.NotFound();

    todo.Name = todoItemDTO.Name;
    todo.IsComplete = todoItemDTO.IsComplete;

    await db.SaveChangesAsync();

    return TypedResults.NoContent();
}

static async Task<IResult> DeleteTodo(int id, TodoDbContext db)
{
    if (await db.Todos.FindAsync(id) is Todo todo)
    {
        db.Todos.Remove(todo);
        await db.SaveChangesAsync();
        return TypedResults.NoContent();
    }

    return TypedResults.NotFound();
}

app.Run();