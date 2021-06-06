using System.Collections.Generic;
using Assets;
using UnityEngine;

[CustomEditClass]
public class SoundDef : MonoBehaviour
{
	[CustomEditField(T = EditType.AUDIO_CLIP)]
	public string m_AudioClip;

	public Global.SoundCategory m_Category = Global.SoundCategory.FX;

	public List<RandomAudioClip> m_RandomClips;

	public float m_RandomPitchMin = 1f;

	public float m_RandomPitchMax = 1f;

	public float m_RandomVolumeMin = 1f;

	public float m_RandomVolumeMax = 1f;

	public bool m_IgnoreDucking;

	public bool m_persistPastGameEnd;

	public void Awake()
	{
	}
}
