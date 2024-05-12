using JetBrains.Annotations;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Digitalroot.Valheim.Common.Json
{
  [UsedImplicitly]
  [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
  public static class JsonSerializationProvider
  {
    [Obsolete("Use Deserialize<T>()")]
    public static T FromJson<T>(string json) => Deserialize<T>(json);

    public static T Deserialize<T>(string json)
    {
      return SimpleJson.SimpleJson.DeserializeObject<T>(json, new DigitalrootJsonSerializerStrategy());
    }

    [Obsolete("Use Serialize()")]
    public static string ToJson(object obj, bool pretty = false) => Serialize(obj);

    public static string Serialize(object obj)
    {
      return SimpleJson.SimpleJson.SerializeObject(obj, new DigitalrootJsonSerializerStrategy());
    }
  }
}
