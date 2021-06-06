using System;
using System.Collections.Generic;
using Assets;
using UnityEngine;

public class SoundDucker : MonoBehaviour
{
	public bool m_DuckAllCategories = true;

	public SoundDuckedCategoryDef m_GlobalDuckDef;

	public List<SoundDuckedCategoryDef> m_DuckedCategoryDefs;

	private bool m_ducking;

	private void Awake()
	{
		InitDuckedCategoryDefs();
	}

	private void OnDestroy()
	{
		StopDucking();
	}

	public override string ToString()
	{
		return $"[SoundDucker: {base.name}]";
	}

	public List<SoundDuckedCategoryDef> GetDuckedCategoryDefs()
	{
		return m_DuckedCategoryDefs;
	}

	public void SetDuckedCategoryDefs(List<SoundDuckedCategoryDef> duckedCategoryDef)
	{
		m_DuckedCategoryDefs = duckedCategoryDef;
		InitDuckedCategoryDefs();
	}

	public bool IsDucking()
	{
		return m_ducking;
	}

	public void StartDucking()
	{
		if (SoundManager.Get() != null && !m_ducking)
		{
			InitDuckedCategoryDefs();
			m_ducking = SoundManager.Get().StartDucking(this);
		}
	}

	public void StopDucking()
	{
		if (SoundManager.Get() != null && m_ducking)
		{
			m_ducking = false;
			SoundManager.Get().StopDucking(this);
		}
	}

	private void InitDuckedCategoryDefs()
	{
		if (!m_DuckAllCategories || m_GlobalDuckDef == null)
		{
			return;
		}
		m_DuckedCategoryDefs = new List<SoundDuckedCategoryDef>();
		foreach (Global.SoundCategory value in Enum.GetValues(typeof(Global.SoundCategory)))
		{
			if (value != 0)
			{
				SoundDuckedCategoryDef soundDuckedCategoryDef = new SoundDuckedCategoryDef();
				SoundUtils.CopyDuckedCategoryDef(m_GlobalDuckDef, soundDuckedCategoryDef);
				soundDuckedCategoryDef.m_Category = value;
				m_DuckedCategoryDefs.Add(soundDuckedCategoryDef);
			}
		}
	}
}
