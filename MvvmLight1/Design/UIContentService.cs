using System;
using System.Threading.Tasks;
using PixelloTools.Logging;
using PixelloTools.XmlObject;

namespace MvvmLight1.Model.UIContent
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