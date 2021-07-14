using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObjects/Variables/Vec3 Variable", order = 4)]
public class Vec3VariableSO : ScriptableObject
{
#if UNITY_EDITOR
    [Multiline]
    public string DeveloperDescription = "";
#endif

    public Vector3 Value;
    public Vector3 DefaultValue;

    public void SetValue(Vector3 value)
    {
        Value = value;
    }

    public void SetValue(Vec3VariableSO value)
    {
        Value = value.Value;
    }

    public void ApplyChange(Vector3 amount)
    {
        Value += amount;
    }

    public void ApplyChange(Vec3VariableSO amount)
    {
        Value += amount.Value;
    }

    public void SetDefaultValue()
    {
        Value = DefaultValue;
    }
}
