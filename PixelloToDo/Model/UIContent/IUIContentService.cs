using System;

namespace PixelloToDo.Model.UIContent
{
    public interface IUIContentService
    {
        void GetData(Action<IUIContentItem, Exception> callback);
    }
}