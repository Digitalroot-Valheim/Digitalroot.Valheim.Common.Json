using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using UnityEngine;

namespace Digitalroot.Valheim.Common.Json;

/// <inheritdoc />
public class Vector3JsonConverter : JsonConverter<Vector3>
{
  #region Overrides of JsonConverter<Vector3>

  /// <inheritdoc />
  public override Vector3 Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    var json = reader.GetString();
    var a = JsonSerializationProvider.FromJson<float[]>(json);
    return new Vector3(a[0], a[1], a[2]);
  }

  /// <inheritdoc />
  public override void Write(Utf8JsonWriter writer, Vector3 value, JsonSerializerOptions options)
  {
    var a = new[] { value.x, value.y, value.z };
    writer.WriteStringValue(JsonSerializationProvider.ToJson(a));
  }

  #endregion
}