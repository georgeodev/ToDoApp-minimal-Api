using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using MyTodoApp;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); //for API documentation
//setting up the database to use inmemory database gotten from the entity framework core
builder.Services.AddDbContext<ToDoDbContext>(opt => opt.UseInMemoryDatabase("TodoApp"));

var app = builder.Build(); //this line of code is what builds the app

//creating endpoint for Get
app.MapGet("/todolist", async (ToDoDbContext db) =>
await db.ToDoItems.ToListAsync());

app.MapGet("/todolist/{id}", async (int id, ToDoDbContext db) =>
{
    var toDoItem = await db.ToDoItems.FindAsync(id);
    return toDoItem != null ? Results.Ok(toDoItem) : Results.NotFound();
 
});
//creating endpoint for Post
app.MapPost("/todolist", async (TodoItem toDoItem, ToDoDbContext db) =>
{
    db.ToDoItems.Add(toDoItem);
    await db.SaveChangesAsync();
    return Results.Created($"/ShoppingList/{toDoItem.Id}", toDoItem);

});
//creating endpoint for Put
app.MapPut("/todolist/{id}", async (int id, TodoItem toDoItem, ToDoDbContext db) =>
{
    var toDoItemToBeUpdated = await db.ToDoItems.FindAsync(id);
    if (toDoItemToBeUpdated != null)
    {
        
        //we can do this to update individual properties
        toDoItemToBeUpdated.TaskName = toDoItem.TaskName;
        toDoItemToBeUpdated.Comment = toDoItemToBeUpdated.Comment;
        toDoItemToBeUpdated.IsComplete = toDoItem.IsComplete;
        await db.SaveChangesAsync();
        return Results.Ok(toDoItemToBeUpdated);
    }

    return Results.NotFound();
});

//creating endpoint for Delete
app.MapDelete("/todolist/{id}", async (int id, ToDoDbContext db) =>
{
    var toDoItem = await db.ToDoItems.FindAsync(id);
    if (toDoItem != null)
    {
        db.ToDoItems.Remove(toDoItem);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }

    return Results.NotFound();
});


app.UseHttpsRedirection();
app.Run();