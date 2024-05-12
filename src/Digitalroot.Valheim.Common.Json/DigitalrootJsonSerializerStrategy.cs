using System;
using UnityEngine;

namespace Digitalroot.Valheim.Common.Json;

/// <inheritdoc />
public class DigitalrootJsonSerializerStrategy : SimpleJson.PocoJsonSerializerStrategy
{
  #region Implementation of IJsonSerializerStrategy

  /// <inheritdoc />
  public override bool TrySerializeNonPrimitiveObject(object input, out object output)
  {
    switch (input)
    {
      case Vector3 vector3:
        output = new[] { vector3.x, vector3.y, vector3.z };
        return true;

      case Quaternion quaternion:
        output = new[] { quaternion.x, quaternion.y, quaternion.z, quaternion.w };
        return true;

      default:
        return base.TrySerializeNonPrimitiveObject(input, out output);
    }
  }

  /// <inheritdoc />
  public override object DeserializeObject(object value, Type type)
  {
    if (type == null) throw new ArgumentNullException(nameof(type));
    if (value == null) throw new ArgumentNullException(nameof(value));

    if (value is string str)
    {
      if (string.IsNullOrWhiteSpace(str))
      {
        throw new ArgumentNullException(nameof(value));
      }

      if (type == typeof(Vector3))
      {
        if (DeserializeObject(value, typeof(float[])) is not float[] floats || floats is { Length: not 3 })
        {
          throw new ArgumentException($"The value '{value}' can be converted to a {nameof(Vector3)}.", nameof(value));
        }

        return new Vector3(floats[0], floats[1], floats[2]);
      }

      if (type == typeof(Quaternion))
      {
        if (DeserializeObject(value, typeof(float[])) is not float[] floats || floats is { Length: not 4 })
        {
          throw new ArgumentException($"The value '{value}' can be converted to a {nameof(Quaternion)}.", nameof(value));
        }

        return new Quaternion(floats[0], floats[1], floats[2], floats[3]);
      }

      return base.DeserializeObject(value, type);
    }

    throw new ArgumentException($"The value '{value}' can be converted to a {type.Name}.", nameof(value));
  }

  #endregion
}
