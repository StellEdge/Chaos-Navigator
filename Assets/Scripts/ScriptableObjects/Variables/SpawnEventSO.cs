using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObjects/Spawn Event", order = 1)]
public class SpawnEventSO : ScriptableObject
{
    public IntReference shipType;
    public IntReference shipNum;
    public IntReference offset;
    public FloatReference speed;
    public Vec3Reference[] initPos;
    public Vec3Reference[] initDirections;
}
