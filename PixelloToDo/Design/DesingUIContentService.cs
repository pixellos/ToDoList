using System;
using PixelloToDo.Model.UIContent;
using PixelloTools.Logging;
using PixelloTools.XmlObject;

namespace PixelloToDo.Design
{
    public class DesingUIContentService :ObjectSerializer<IUIContentItem> ,IUIContentService
    {
        public DesingUIContentService() : base(ConsoleLogger.GetLoger())  {}

        public void GetData(Action<IUIContentItem, Exception> callback)
        {
            var item = IUIContentItem.GetDefaultValues();
            callback(item, null);
        }
    }
}