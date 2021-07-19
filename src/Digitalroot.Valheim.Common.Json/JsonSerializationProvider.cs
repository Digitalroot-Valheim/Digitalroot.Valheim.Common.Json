using fastJSON;
using JetBrains.Annotations;
using UnityEngine;

namespace Digitalroot.Valheim.Common
{
  [UsedImplicitly]
  public static class JsonSerializationProvider
  {
    private static readonly JSONParameters JsonParameters = new JSONParameters {SerializeNullValues = false, SerializeToLowerCaseNames = false, BadListTypeChecking = true, ShowReadOnlyProperties = true, KVStyleStringDictionary = false, UseEscapedUnicode = true, InlineCircularReferences = true};
    private static bool _customTypeLoaded;

    private static void Init()
    {
      if (_customTypeLoaded) return;

      JSON.RegisterCustomType(typeof(Vector3),
        x =>
        {
          var v3 = (Vector3) x;
          var a = new[] { v3.x, v3.y, v3.z };
          return JSON.ToJSON(a);
        },
        x =>
        {
          var a = JSON.ToObject<float[]>(x);
          return new Vector3(a[0], a[1], a[2]);
        });

      JSON.RegisterCustomType(typeof(Quaternion),
        x =>
        {
          var q = (Quaternion)x;
          var a = new[] { q.x, q.y, q.z, q.w };
          return JSON.ToJSON(a);
        },
        x =>
        {
          var a = JSON.ToObject<float[]>(x);
          return new Quaternion(a[0], a[1], a[2], a[3]);
        });
      
      _customTypeLoaded = true;

    }

    public static T FromJson<T>(string json)
    {
      Init();
      return JSON.ToObject<T>(json, JsonParameters); // fastJson
    }

    public static string ToJson(object obj, bool pretty = false)
    {
      Init();
      if (pretty)
      {
        return JSON.ToNiceJSON(obj, JsonParameters);
      }
      return JSON.ToJSON(obj, JsonParameters);
    }
  }
}
