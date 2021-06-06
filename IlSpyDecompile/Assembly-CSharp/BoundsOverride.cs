using System.Collections.Generic;
using UnityEngine;

public class BoundsOverride : MonoBehaviour
{
	public List<ScreenCategory> m_screenCategory = new List<ScreenCategory>();

	public List<Bounds> m_bounds = new List<Bounds>();

	public Bounds bounds
	{
		get
		{
			int bestScreenMatch = PlatformSettings.GetBestScreenMatch(m_screenCategory);
			return m_bounds[bestScreenMatch];
		}
	}

	public void AddCategory()
	{
		AddCategory(PlatformSettings.Screen);
	}

	public void AddCategory(ScreenCategory screen)
	{
		if (!Application.IsPlaying(this))
		{
			m_screenCategory.Add(screen);
			m_bounds.Add(default(Bounds));
		}
	}
}
