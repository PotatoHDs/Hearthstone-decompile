using System;
using System.Collections.Generic;
using UnityEngine;

public class FiresideGatheringSign : MonoBehaviour
{
	public delegate void OnDestroyCallback();

	public GameObject m_fxMotes;

	public Transform m_shieldContainer;

	private FiresideGatheringSignShield m_shield;

	private List<Action> m_signSocketAnimationCompleteListeners = new List<Action>();

	public event OnDestroyCallback OnDestroyEvent;

	public void SetSignShield(FiresideGatheringSignShield shield)
	{
		m_shield = shield;
	}

	public void SetSignShadowEnabled(bool enabled)
	{
		m_shield.m_ShieldShadow.SetActive(enabled);
	}

	public MeshRenderer GetShieldMeshRenderer()
	{
		return m_shield.m_ShieldMeshRenderer;
	}

	private void OnDestroy()
	{
		if (this.OnDestroyEvent != null)
		{
			this.OnDestroyEvent();
		}
	}

	public void RegisterSignSocketAnimationCompleteListener(Action listener)
	{
		if (!m_signSocketAnimationCompleteListeners.Contains(listener))
		{
			m_signSocketAnimationCompleteListeners.Add(listener);
		}
	}

	public void UnregisterSignSocketAnimationCompleteListener(Action listener)
	{
		if (m_signSocketAnimationCompleteListeners.Contains(listener))
		{
			m_signSocketAnimationCompleteListeners.Remove(listener);
		}
	}

	public void FireSignSocketAnimationCompleteListener()
	{
		foreach (Action signSocketAnimationCompleteListener in m_signSocketAnimationCompleteListeners)
		{
			signSocketAnimationCompleteListener();
		}
	}
}
