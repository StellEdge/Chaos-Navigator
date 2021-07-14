using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(AudioClipSO), true)]
public class AudioEventEditor : Editor
{
	[SerializeField] 
	private AudioSource previewer;

	public void OnEnable()
	{
		previewer = EditorUtility.CreateGameObjectWithHideFlags("Audio preview", HideFlags.HideAndDontSave, typeof(AudioSource)).GetComponent<AudioSource>();
	}

	public void OnDisable()
	{
		DestroyImmediate(previewer.gameObject);
	}

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		EditorGUI.BeginDisabledGroup(serializedObject.isEditingMultipleObjects);
		if (GUILayout.Button("Preview"))
		{
			((AudioClipSO) target).Play(previewer);
		}
		EditorGUI.EndDisabledGroup();
	}
}
