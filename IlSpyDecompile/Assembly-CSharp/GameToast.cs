using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameToast : MonoBehaviour
{
	public List<Material> m_intensityMaterials = new List<Material>();

	private void Start()
	{
		UpdateIntensity(16f);
		Hashtable args = iTween.Hash("time", 0.5f, "from", 16f, "to", 1f, "delay", 0.25f, "easetype", iTween.EaseType.easeOutCubic, "onupdate", "UpdateIntensity");
		iTween.ValueTo(base.gameObject, args);
	}

	private void UpdateIntensity(float intensity)
	{
		foreach (Material intensityMaterial in m_intensityMaterials)
		{
			intensityMaterial.SetFloat("_Intensity", intensity);
		}
	}
}
