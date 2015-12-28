using Microsoft.VisualStudio.TestTools.UnitTesting;
using PixelloTools.XmlObject;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PixelloTools.Logging;

namespace PixelloTools.XmlObject.Tests
{
    [TestClass()]
    public class ObjectSerializerTests
    {
        private ObjectSerializer<Guid> GetSerializer(IPathBuilder pathBuilder) => new ObjectSerializer<Guid>(ConsoleLogger.GetLoger(),pathBuilder);

        [TestMethod()]
        public void SaveAndReadingUniqueDataTest()
        {
            ObjectSerializer<Guid> serializer = GetSerializer(
                new PathBuilder("SaveAndReadTests", Guid.NewGuid().ToString(), ".xml") );

            var Str = Guid.NewGuid();
            serializer.SaveSingleData(Str);
            Assert.AreEqual(Str,serializer.GetSingleData());
        }

        [TestMethod()]
        public void DeleteDataTest()
        {
            var file = Guid.NewGuid().ToString();
            ObjectSerializer<Guid> serializer = GetSerializer(
                new PathBuilder("DeleteDataTests", file, ".xml"));
           

            var Str = Guid.NewGuid();
            serializer.SaveSingleData(Str);

            Assert.IsTrue(
                File.Exists(
                    serializer.PathBuilder.FilePath(file)
                    ));

            serializer.DeleteData(file);
            Assert.IsFalse(
                 File.Exists(
                     serializer.PathBuilder.FilePath(file)
                     ));

        }

        [TestMethod()]
        public void GetEnumerableDataTest()
        {
            var file = Guid.NewGuid().ToString();
            ObjectSerializer<Guid> serializer = GetSerializer(
                new PathBuilder("EnumerableTests", file, ".xml"));
            List<Guid> guids = new List<Guid>()
            {
                Guid.NewGuid(),Guid.NewGuid(),Guid.NewGuid(),Guid.NewGuid()
            };

            foreach (var VARIABLE in guids)
            {
                serializer.SaveSingleData(VARIABLE,VARIABLE.ToString());
            }

           List<Guid> list = serializer.GetEnumerableData().ToList();

            foreach (var VARIABLE in list)
            {
                Console.WriteLine(VARIABLE.ToString());
            }

            Assert.IsTrue(
                guids.All(x=> list.Contains(x)));


        }

        [TestMethod()]
        public async Task SaveSingleDataAsyncTest()
        {

            var file = Guid.NewGuid().ToString();
            ObjectSerializer<Guid> serializer = GetSerializer(
                new PathBuilder("SaveAsync", file, ".xml"));
            Directory.Delete(serializer.PathBuilder.Folder,true);
            var content = Guid.NewGuid();
            await serializer.SaveSingleDataAsync(content);
            var data = await serializer.GetSingleDataAsync();
            Assert.AreEqual(
               data,content
                );

        }

        [TestMethod()]
        public void SaveSingleDataTest()
        {
            var file = Guid.NewGuid().ToString();
            ObjectSerializer<Guid> serializer = GetSerializer(
                new PathBuilder("SaveTests", file, ".xml"));

            serializer.SaveSingleData(Guid.NewGuid());

            Assert.IsTrue(
                File.Exists(
                    serializer.PathBuilder.FilePath(
                        file)));
        }
    }
}