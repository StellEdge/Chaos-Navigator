using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Variables/Float Variable", order = 2)]
public class FloatVariableSO : ScriptableObject
{
#if UNITY_EDITOR
    [Multiline]
    public string DeveloperDescription = "";
#endif
    public float Value;
    public float DefaultValue;

    public void SetValue(float value)
    {
        Value = value;
    }

    public void SetValue(FloatVariableSO value)
    {
        Value = value.Value;
    }

    public void ApplyChange(float amount)
    {
        Value += amount;
    }

    public void ApplyChange(FloatVariableSO amount)
    {
        Value += amount.Value;
    }

    public void SetDefaultValue()
    {
        Value = DefaultValue;
    }
}