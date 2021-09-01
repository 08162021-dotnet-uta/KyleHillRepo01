using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Project0.StoreApplication.Storage.Adapters
{
  /// <summary>
  /// 
  /// </summary>
  public class FileAdapter
  {
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public List<T> ReadFromFile<T>(string path) where T : class, new()
    {
      if (!File.Exists(path))
      {
        return null;
      }
            using (var file = new StreamReader(path))
            {
                var xml = new XmlSerializer(typeof(List<T>));
                return xml.Deserialize(file) as List<T>;
            }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public void WriteToFile<T>(string path, List<T> data) where T : class, new()
    {
            using (var file = new StreamWriter(path))
            {
                var xml = new XmlSerializer(typeof(List<T>));
                xml.Serialize(file, data);
            }
    }
  }
}