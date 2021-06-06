using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TagVoData
{
	public AudioSource m_AudioSource;

	public string m_GameStringKeyOverride;

	public List<TagVoRequirement> m_TagRequirements = new List<TagVoRequirement>();
}
