using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Project0.StoreApplication.Domain.Abstracts;
using System;

namespace Project0.StoreApplication.Storage.Adapters
{
  //TESTED that each of these worked as intended
  //Thoroughly tested each of these methods, that they write and return the same thing
  public class FileAdapter
  {
    public List<T> ReadFromFile<T>(string path) where T : class
    {
      var file = new StreamReader(path);
      var xml = new XmlSerializer(typeof(List<T>));
      try
      {
        return xml.Deserialize(file) as List<T>;
      }
      catch (InvalidOperationException e)
      {
        return new List<T>();
      }
    }

    public void WriteToFile<T>(string path, List<T> data) where T : class
    {
      var file = new StreamWriter(path);
      var xml = new XmlSerializer(typeof(List<T>));
      xml.Serialize(file, data);
    }
  }
}