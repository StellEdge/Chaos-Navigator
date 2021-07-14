using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObjects/Variables/String Variable", order = 3)]
public class StringVariableSO : ScriptableObject
{
    [Multiline]
    [SerializeField]
    public string Value;

}
