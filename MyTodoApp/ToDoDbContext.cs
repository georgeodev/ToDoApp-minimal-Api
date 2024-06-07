using System;
using Microsoft.EntityFrameworkCore;
namespace MyTodoApp
{
	public class ToDoDbContext : DbContext
	{
		public DbSet<TodoItem> ToDoItems => Set<TodoItem>();
		public ToDoDbContext(DbContextOptions<ToDoDbContext> options) : base(options)
		{
		}
	}
}

