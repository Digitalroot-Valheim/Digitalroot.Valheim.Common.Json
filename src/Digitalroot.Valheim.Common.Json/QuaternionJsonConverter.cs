using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using UnityEngine;

namespace Digitalroot.Valheim.Common.Json;

/// <inheritdoc />
public class QuaternionJsonConverter : JsonConverter<Quaternion>
{
  #region Overrides of JsonConverter<Quaternion>

  /// <inheritdoc />
  public override Quaternion Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    var json = reader.GetString();
    var a = JsonSerializationProvider.FromJson<float[]>(json);
    return new Quaternion(a[0], a[1], a[2], a[3]);
  }

  /// <inheritdoc />
  public override void Write(Utf8JsonWriter writer, Quaternion value, JsonSerializerOptions options)
  {
    var a = new[] { value.x, value.y, value.z, value.w };
    writer.WriteStringValue(JsonSerializationProvider.ToJson(a));
  }

  #endregion
}
