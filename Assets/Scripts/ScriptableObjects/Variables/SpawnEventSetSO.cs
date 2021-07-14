using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Sets/Spawn Event Set", order = 1)]
public class SpawnEventSetSO : ScriptableObject
{
    public List<SpawnEventSO> Items = new List<SpawnEventSO>();

    public void Add(SpawnEventSO spawn)
    {
        if (!Items.Contains(spawn))
            Items.Add(spawn);
    }

    public void Remove(SpawnEventSO spawn)
    {
        if (Items.Contains(spawn))
            Items.Remove(spawn);
    }
}
