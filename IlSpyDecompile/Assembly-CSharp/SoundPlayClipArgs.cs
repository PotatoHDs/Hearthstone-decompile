using Assets;
using UnityEngine;

public class SoundPlayClipArgs
{
	public AudioSource m_templateSource;

	public SoundDef m_def;

	public AudioClip m_forcedAudioClip;

	public float? m_volume;

	public float? m_pitch;

	public float? m_spatialBlend;

	public Global.SoundCategory? m_category;

	public GameObject m_parentObject;
}
