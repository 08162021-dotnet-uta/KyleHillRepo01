using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Project0.StoreApplication.Domain;
using Xunit;
using Project0.StoreApplication.Storage.Adapters;

namespace Project0.StoreApplication.Testing
{
  public class FileAdapterTests
  {
    [Fact]
    public void readFromNonexistentFile()
    {
      FileAdapter fileAdapter = new FileAdapter();
      Assert.Null(fileAdapter.ReadFromFile<object>("NotThere.xml"));
    }

    [Fact]
    public void ReadWrite()
    {
        string path = "testfile.xml";
        FileAdapter fileAdapter = new FileAdapter();
        List<object> objects = new List<object>() { new object(), new object() };
        fileAdapter.WriteToFile<object>(path, objects);
        Assert.True(fileAdapter.ReadFromFile<object>(path).Count == 2);
        File.Delete(path);

    }
}
}