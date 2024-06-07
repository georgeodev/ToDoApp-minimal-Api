using System;
namespace MyTodoApp
{
	public class TodoItem
	{
		public int Id { get; set; }
		public string? TaskName { get; set; }
		public string? Comment { get; set; }
		public bool IsComplete { get; set; }

	}
}

