using Digitalroot.Valheim.Common.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace UnitTests
{
  public class JsonTests
  {
    [SetUp]
    public void Setup() { }

    private readonly string jsonRef = "{\"$id\":\"1\",\"string\":\"String\",\"int\":9999,\"stringList\":{\"$id\":\"2\",\"$values\":[\"A\",\"B\",\"C\",\"D\"]},\"myEnum\":\"ONE\"}";

    [Test(Author = "Digitalroot", Description = "Tests Object to JSON.", TestOf = typeof(JsonSerializationProvider)), Timeout(5000)]
    public void ToJsonTest()
    {
      var obj = new UnitTestJsonClass();
      var json = JsonSerializationProvider.ToJson(obj);
      Assert.That(json, Is.Not.Empty);
      Assert.That(json, Is.EqualTo(jsonRef));
    }

    [Test(Author = "Digitalroot", Description = "Tests JSON to object.", TestOf = typeof(JsonSerializationProvider)), Timeout(5000)]
    public void FromJsonTest()
    {
      var obj = JsonSerializationProvider.FromJson<UnitTestJsonClass>(jsonRef);
      Assert.That(obj, Is.Not.Null);
      Assert.That(obj.Int, Is.EqualTo(9999));
      Assert.That(obj.String, Is.EqualTo(nameof(String)));
      Assert.That(obj.StringList, Is.Not.Null.Or.Not.Empty);
      Assert.That(obj.StringList, Has.Count.EqualTo(4));
      Assert.That(obj.StringList, Has.One.Items.EqualTo("A"));
      Assert.That(obj.StringList, Has.One.Items.EqualTo("B"));
      Assert.That(obj.StringList, Has.One.Items.EqualTo("C"));
      Assert.That(obj.StringList, Has.One.Items.EqualTo("D"));
      Assert.That(obj.MyEnum, Is.EqualTo(MyEnum.ONE));
    }

    public class UnitTestJsonClass
    {
      [JsonInclude]
      public string String = nameof(String);

      [JsonInclude]
      public int Int = 9999;

      [JsonInclude]
      public List<string> StringList = new List<string>();

      [JsonInclude]
      public MyEnum MyEnum;

      public UnitTestJsonClass()
      {
        MyEnum = MyEnum.ONE;
        StringList.Add("A");
        StringList.Add("B");
        StringList.Add("C");
        StringList.Add("D");
      }
    }

    public enum MyEnum
    {
      ONE
      , TWO
      , THREE
    }
  }
}
