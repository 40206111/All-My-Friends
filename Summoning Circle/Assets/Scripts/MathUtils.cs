using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathUtils
{
    public const float Tau = Mathf.PI * 2f;

    #region vectors

    public static Vector2 Rotate(this Vector2 v, float angle)
    {
        float cos = Mathf.Cos(angle);
        float sin = Mathf.Sin(angle);
        return new Vector2(cos * v.x - sin * v.y, sin * v.x + cos * v.y);
    }

    public static Vector2 MaxLength(this Vector2 v, float l) => v.sqrMagnitude > l * l ? v.normalized * l : v;

    #endregion
}
