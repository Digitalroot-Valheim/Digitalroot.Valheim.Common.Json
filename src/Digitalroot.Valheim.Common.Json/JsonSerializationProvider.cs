using JetBrains.Annotations;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Digitalroot.Valheim.Common.Json
{
  [UsedImplicitly]
  public static class JsonSerializationProvider
  {
    private static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
      AllowTrailingCommas = false
      , DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
      , PropertyNamingPolicy = JsonNamingPolicy.CamelCase
      , IncludeFields = true
      , ReferenceHandler = ReferenceHandler.Preserve
      , NumberHandling = JsonNumberHandling.AllowReadingFromString
      , Converters =
      {
        new JsonStringEnumConverter()
        , new Vector3JsonConverter()
        , new QuaternionJsonConverter()
      }
    };

    private static readonly JsonSerializerOptions JsonSerializerOptionsPretty = new(JsonSerializerOptions)
    {
      WriteIndented = true
    };
 
    public static T FromJson<T>(string json)
    {
      return JsonSerializer.Deserialize<T>(json, JsonSerializerOptions); // System.Text.Json
    }

    public static string ToJson(object obj, bool pretty = false)
    {
      if (pretty)
      {
        return JsonSerializer.Serialize(obj, JsonSerializerOptionsPretty); // System.Text.Json
      }

      return JsonSerializer.Serialize(obj, JsonSerializerOptions); // System.Text.Json
    }
  }
}
