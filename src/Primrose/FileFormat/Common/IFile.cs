namespace Primrose.FileFormats.Common
{
  /// <summary>Defines an interface for reading and writing to a file</summary>
  public interface IFile
  {
    /// <summary>Opens and reads information from a source file</summary>
    /// <param name="filePath">The file to read from</param>
    void ReadFromFile(string filePath);

    /// <summary>Writes information to a destination file</summary>
    /// <param name="destinationPath">The file to write to</param>
    void WriteToFile(string destinationPath);
  }
}
