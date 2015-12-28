using System;
using System.Threading.Tasks;
using PixelloTools.Logging;
using PixelloTools.XmlObject;

namespace MvvmLight1.Model.UIContent
{
    public class UIContentService :ObjectSerializer<IUIContentItem> ,IUIContentService
    {
        private const string UserSettingFile = "User";
        public UIContentService() : base(ConsoleLogger.GetLoger())  {}

        public async void GetData(Action<IUIContentItem, Exception> callback)
        {
            SaveSingleData(IUIContentItem.GetDefaultValues());
            var item = await GetSingleDataAsync(
                PathBuilder.FilePath(UserSettingFile));
            callback(item, null);
        }
    }
}