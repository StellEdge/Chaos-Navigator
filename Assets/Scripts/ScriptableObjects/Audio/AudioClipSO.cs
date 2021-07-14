using UnityEngine;
using Random = UnityEngine.Random;

/*
 * hold audio clip 
 * can modify volume in runtime and store it 
 */
[CreateAssetMenu(menuName= "ScriptableObjects/Audio/Single Audio Clip", order = 1)]
public class AudioClipSO : ScriptableObject
{
	public AudioClip clip;

    public string clipName;

	[Range(0, 4)]
	public float volume;

	[Range(0, 4)]
	public float pitch;

	public void Play(AudioSource source)
	{
		if (clip == null) return;

		source.clip = clip;
		source.volume = volume;
		source.pitch = pitch;
		source.Play();
	}
}