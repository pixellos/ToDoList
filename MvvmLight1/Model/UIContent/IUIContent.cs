using System.Collections.Generic;

namespace MvvmLight1.Model.UIContent
{
    public interface IUIContent
    {
        string ListNameDescription { get; set; }
        string ListTagsDescription { get; set; }
        string ListTaskPirorityDescription { get; set; }
        string ListIsCompletedDescription { get; set; }
        
        string TaskAddButtonName { get; set; }
        string TaskNameDescription { get; set; }
        string TaskTagsDescription { get; set; }
        List<string> TaskPirorityList { get; set; } 
    }
}