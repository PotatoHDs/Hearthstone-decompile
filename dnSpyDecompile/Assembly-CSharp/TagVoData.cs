using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000788 RID: 1928
[Serializable]
public class TagVoData
{
	// Token: 0x040057A2 RID: 22434
	public AudioSource m_AudioSource;

	// Token: 0x040057A3 RID: 22435
	public string m_GameStringKeyOverride;

	// Token: 0x040057A4 RID: 22436
	public List<TagVoRequirement> m_TagRequirements = new List<TagVoRequirement>();
}
