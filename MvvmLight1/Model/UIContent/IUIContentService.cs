using System;
using System.Threading.Tasks;

namespace MvvmLight1.Model.UIContent
{
    public interface IUIContentService
    {
        void GetData(Action<IUIContentItem, Exception> callback);
    }
}