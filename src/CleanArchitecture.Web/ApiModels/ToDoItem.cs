namespace CleanArchitecture.Web.ApiModels
{
    public class ToDoItem
    {
        public bool IsDone { get; internal set; }
        public string Description { get; internal set; }
        public int Id { get; internal set; }
        public string Title { get; internal set; }
    }
}