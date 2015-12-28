using System;
using System.Threading.Tasks;

namespace PixelloTools.XmlObject
{
    public static class FakeAsyncHelper
    {
        public static async Task<T> ToAsync<T>(this Func<T> func )
        {
            return await Task<T>.Factory.StartNew(func);
        }

        public static async Task ToAsync(this Action action)
        {
            await Task.Factory.StartNew(action);
        } 
    }
}