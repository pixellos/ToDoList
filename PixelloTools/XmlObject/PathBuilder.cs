namespace PixelloTools.XmlObject
{
    public class PathBuilder : IPathBuilder
    {
        public string Folder { get; private set; }
        public string FileName { get; private set; }
        public string FileExtension { get; private set; }
        private string separator = "\\";

        public PathBuilder(string folder = "Default",string fileName = "FileName",string fileExtension = ".dat")
        {
            Folder = folder;
            FileExtension = fileExtension;
            FileName = fileName;
        }
        public string FilePath(string fileName = null) => 
            Folder + separator + (fileName ?? FileName) + FileExtension;
    }
}