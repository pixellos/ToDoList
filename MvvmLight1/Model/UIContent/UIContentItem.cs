using System;
using System.Collections.Generic;

namespace MvvmLight1.Model.UIContent
{
    [Serializable]
    public class IUIContentItem : IUIContent
    {
        public static IUIContentItem GetDefaultValues()
        {
            return new IUIContentItem()
            { 
                ListNameDescription = "Task content",
                ListIsCompletedDescription = "Is completed",
                ListTagsDescription = "Tags",
                ListTaskPirorityDescription = "Priority",
                TaskAddButtonName = "Add",
                TaskNameDescription = "Type your task here",
                TaskTagsDescription = "Type your tags here, separate it with ,",
                TaskPirorityList = new List<string>()
                {
                    TaskPirority.Highest.ToString(),
                    TaskPirority.High.ToString(),
                    TaskPirority.Average.ToString(),
                    TaskPirority.Low.ToString(),
                }
            };
        }

        public string ListNameDescription { get; set; }
        public string ListTagsDescription { get; set; }
        public string ListTaskPirorityDescription { get; set; }
        public string ListIsCompletedDescription { get; set; }
        public string TaskAddButtonName { get; set; }
        public string TaskNameDescription { get; set; }
        public string TaskTagsDescription { get; set; }
        public List<string> TaskPirorityList { get; set; } 


    }
}