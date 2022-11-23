using Digitalroot.Valheim.Common.Json;
using JetBrains.Annotations;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;

namespace UnitTests
{
  public class JsonTests : TestFixture
  {
    private readonly string jsonRef = "{\"$types\":{\"UnitTests.JsonTests+UnitTestJsonClass, UnitTests, Version=" 
                                      + typeof(UnitTestJsonClass).Assembly.GetName().Version 
                                      + ", Culture=neutral, PublicKeyToken=null\":\"1\"},\"$type\":\"1\",\"String\":\"String\",\"Int\":9999}";

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
      Assert.That(obj.String, Is.EqualTo("String"));
    }

    private class UnitTestJsonClass
    {
      public string String;
      public int Int;

      public UnitTestJsonClass()
      {
        String = nameof(String);
        Int = 9999;
      }
    }

    /// <inheritdoc />
    public JsonTests([NotNull] ITypeInfo fixtureType, [CanBeNull] [ItemCanBeNull] object[] arguments = null)
      : base(fixtureType, arguments) { }
  }
}
