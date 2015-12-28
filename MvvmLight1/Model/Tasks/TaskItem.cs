namespace MvvmLight1.Model.Tasks
{
    public class TaskItem
    {
        public string AccessName { get; set; }
        public string Name { get; set; }
        public string Tags { get; set; }
        public TaskPirority TaskPirority { get; set; }
        public bool IsCompleted { get; set; }
    }
}