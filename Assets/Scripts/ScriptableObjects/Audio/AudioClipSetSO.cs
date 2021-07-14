using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObjects/Audio/Audio Clip Set", order = 2)]
public class AudioClipSetSO : ScriptableObject
{
    public List<AudioClipSO> Items = new List<AudioClipSO>();

    public void Add(AudioClipSO value)
    {
        if (!Items.Contains(value))
            Items.Add(value);
    }

    public void Remove(AudioClipSO value)
    {
        if (Items.Contains(value))
            Items.Remove(value);
    }

    public AudioClipSO FindClipByName(string s)
    {
        foreach(AudioClipSO clip in Items)
        {
            if(clip.clipName == s)
                return clip;
        }
        return null;
    }
}
