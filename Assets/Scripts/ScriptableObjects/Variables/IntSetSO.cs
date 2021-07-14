using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Sets/Int Set", order = 2)]
public class IntSetSO : ScriptableObject
{
    public List<IntVariableSO> Items = new List<IntVariableSO>();

    public void Add(IntVariableSO value)
    {
        if (!Items.Contains(value))
            Items.Add(value);
    }

    public void Remove(IntVariableSO value)
    {
        if (Items.Contains(value))
            Items.Remove(value);
    }
}
