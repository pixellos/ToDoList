using System.Threading.Tasks;

namespace PixelloTools.XmlObject
{
    public interface IObjectSerializer<T>
    {
        T GetSingleData(string customPath = null);
        Task<T> GetSingleDataAsync(string customPath = null);
        void SaveSingleData(T objectOfType, string FileName = null);
        Task SaveSingleDataAsync(T objectOfType, string FileName = null);
    }
}