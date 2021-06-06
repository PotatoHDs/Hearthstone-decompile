using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class TransformOverride : MonoBehaviour
{
	public List<ScreenCategory> m_screenCategory = new List<ScreenCategory>();

	public List<Vector3> m_localPosition = new List<Vector3>();

	public List<Vector3> m_localScale = new List<Vector3>();

	public List<Quaternion> m_localRotation = new List<Quaternion>();

	public float testVal;

	public void Awake()
	{
		if (Application.IsPlaying(this))
		{
			UpdateObject();
		}
	}

	public void AddCategory(ScreenCategory screen)
	{
		if (!Application.IsPlaying(this))
		{
			m_screenCategory.Add(screen);
			m_localPosition.Add(base.transform.localPosition);
			m_localScale.Add(base.transform.localScale);
			m_localRotation.Add(base.transform.localRotation);
		}
	}

	public void AddCategory()
	{
		AddCategory(PlatformSettings.Screen);
	}

	public void UpdateObject()
	{
		int bestScreenMatch = PlatformSettings.GetBestScreenMatch(m_screenCategory);
		base.transform.localPosition = m_localPosition[bestScreenMatch];
		base.transform.localScale = m_localScale[bestScreenMatch];
		base.transform.localRotation = m_localRotation[bestScreenMatch];
	}
}
