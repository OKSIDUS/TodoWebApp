namespace TodoListApp.Services;
internal interface ITodoListService
{
    public IQueryable<TodoList> TodoLists { get; }
}
