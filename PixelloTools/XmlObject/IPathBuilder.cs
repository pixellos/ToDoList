namespace PixelloTools.XmlObject
{
    public interface IPathBuilder
    {
        string FileExtension { get; }
        string FileName { get; }
        string Folder { get; }
        string FilePath(string fileName = null);
    }
}