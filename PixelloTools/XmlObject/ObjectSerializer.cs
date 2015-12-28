using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using PixelloTools.Logging;

namespace PixelloTools.XmlObject
{
    public class ObjectSerializer<T> : IObjectSerializer<T>
    {
        private int _countOfRetiring;
        public IPathBuilder PathBuilder;

        private readonly Ilogger _ilogger;

        public ObjectSerializer(Ilogger ilogger,IPathBuilder pathBuilder = null, int readRetryIfErrorRetiringCount = 5) //Default Value
        {
            _countOfRetiring = readRetryIfErrorRetiringCount;
            PathBuilder = pathBuilder ?? new PathBuilder("SerializedData","Template",".xml");
            _ilogger = ilogger;
        }

        public T GetSingleData(string fileName = null)
        {
            return File.Exists(PathBuilder.FilePath(fileName)) ? Deserialize(PathBuilder.FilePath(fileName)) : Deserialize(PathBuilder.FilePath());
        }

        public async Task<T> GetSingleDataAsync(string filename = null)
        {
            Func<T> action = () => GetSingleData(filename);
            return await action.ToAsync();
        }

        public void DeleteData(string fileName)
        {
            if(File.Exists(PathBuilder.FilePath(fileName)))                
            {                                                                                              
                File.Delete(PathBuilder.FilePath(fileName));                
            }
        }

        protected T Deserialize(string rawPath)
        {

            using (StreamReader reader = new StreamReader(rawPath))
            {
                try
                {
                    _ilogger.LogIt("Reading: " + rawPath);
                    XmlSerializer serializer = new XmlSerializer(typeof (T));
                    return (T) serializer.Deserialize(reader);

                }
                catch (Exception ex)
                {
                    _ilogger.LogIt("Failed to load: " + rawPath + ex.Message + ex.Data);
                    throw;
                }
            }
        }

        public IEnumerable<T> GetEnumerableData()
        {
            try
            {
                if (Directory.Exists(PathBuilder.Folder))
                {
                    var files = Directory.GetFiles(PathBuilder.Folder);
                    return files.Select(x=>Deserialize(x));
                }
            }
            catch (Exception ex)
            {
                _ilogger.LogIt(Environment.NewLine + ex.Message + ex.Data + "Reading Failed");
            }
            return new List<T>();
        }

        public async Task SaveSingleDataAsync(T objectOfType, string FileName = null)
        {
            Action action = () => SaveSingleData(objectOfType, FileName);
            await action.ToAsync();
        }

        public void SaveSingleData(T objectOfType,string FileName = null)
        {
            if (!Directory.Exists(PathBuilder.Folder)) Directory.CreateDirectory(PathBuilder.Folder);
            {
                SaveToPath(
                    PathBuilder.FilePath(FileName), objectOfType);
            }
        }

        protected void SaveToPath(string path, T objectOfType)
        {
            using (StreamWriter writer = new StreamWriter(path))
            {
                XmlSerializer document = new XmlSerializer(typeof(T));
                document.Serialize(writer, objectOfType);
                document.Serialize(_ilogger.GetLoggerTextWriter(), objectOfType);
            }
        }
    }
}