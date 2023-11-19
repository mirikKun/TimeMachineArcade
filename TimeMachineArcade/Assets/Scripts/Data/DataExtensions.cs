using System;
using UnityEngine;

namespace Data
{
  public static class DataExtensions
  {
    public static float SqrMagnitudeTo(this Vector3 from, Vector3 to)
    {
      return Vector3.SqrMagnitude(to - from);
    }

    public static string ToJson(this object obj) => 
      JsonUtility.ToJson(obj);
    public static Vector3 ColorToVector3(this Color color) => 
      new (color.r,color.g,color.b);    
    public static Color Vector3ToColor(this Vector3 vector3) => 
      new (vector3.x,vector3.y,vector3.z);

    public static T ToDeserialized<T>(this string json) =>
      JsonUtility.FromJson<T>(json);
    public static T ToEnum<T>(this string value) => (T) Enum.Parse(typeof(T), value, true);

  }
}