using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020008CC RID: 2252
public class GameToast : MonoBehaviour
{
	// Token: 0x06007CA6 RID: 31910 RVA: 0x00288D40 File Offset: 0x00286F40
	private void Start()
	{
		this.UpdateIntensity(16f);
		Hashtable args = iTween.Hash(new object[]
		{
			"time",
			0.5f,
			"from",
			16f,
			"to",
			1f,
			"delay",
			0.25f,
			"easetype",
			iTween.EaseType.easeOutCubic,
			"onupdate",
			"UpdateIntensity"
		});
		iTween.ValueTo(base.gameObject, args);
	}

	// Token: 0x06007CA7 RID: 31911 RVA: 0x00288DEC File Offset: 0x00286FEC
	private void UpdateIntensity(float intensity)
	{
		foreach (Material material in this.m_intensityMaterials)
		{
			material.SetFloat("_Intensity", intensity);
		}
	}

	// Token: 0x04006565 RID: 25957
	public List<Material> m_intensityMaterials = new List<Material>();
}
