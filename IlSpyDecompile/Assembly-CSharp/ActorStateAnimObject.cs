using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ActorStateAnimObject
{
	public bool m_Enabled = true;

	public GameObject m_GameObject;

	public AnimationClip m_AnimClip;

	public int m_AnimLayer;

	public float m_CrossFadeSec;

	public bool m_EmitParticles;

	public string m_Comment;

	private bool m_prevParticleEmitValue;

	public void Init()
	{
		if (!(m_GameObject == null) && !(m_AnimClip == null))
		{
			string name = m_AnimClip.name;
			Animation animation = ((!(m_GameObject.GetComponent<Animation>() == null)) ? m_GameObject.GetComponent<Animation>() : m_GameObject.AddComponent<Animation>());
			animation.playAutomatically = false;
			if (animation[name] == null)
			{
				animation.AddClip(m_AnimClip, name);
			}
			animation[name].layer = m_AnimLayer;
		}
	}

	public void Play()
	{
		if (!m_Enabled || m_GameObject == null || !(m_AnimClip != null))
		{
			return;
		}
		string name = m_AnimClip.name;
		m_GameObject.GetComponent<Animation>()[name].enabled = true;
		if (Mathf.Approximately(m_CrossFadeSec, 0f))
		{
			if (!m_GameObject.GetComponent<Animation>().Play(name))
			{
				Debug.LogWarning($"ActorStateAnimObject.PlayNow() - FAILED to play clip {name} on {m_GameObject}");
			}
		}
		else
		{
			m_GameObject.GetComponent<Animation>().CrossFade(name, m_CrossFadeSec);
		}
	}

	public void Stop()
	{
		if (m_Enabled && !(m_GameObject == null) && m_AnimClip != null)
		{
			m_GameObject.GetComponent<Animation>()[m_AnimClip.name].time = 0f;
			m_GameObject.GetComponent<Animation>().Sample();
			m_GameObject.GetComponent<Animation>()[m_AnimClip.name].enabled = false;
		}
	}

	public void Stop(List<ActorState> nextStateList)
	{
		if (!m_Enabled || m_GameObject == null || !(m_AnimClip != null))
		{
			return;
		}
		bool flag = false;
		int num = 0;
		while (!flag && num < nextStateList.Count)
		{
			ActorState actorState = nextStateList[num];
			for (int i = 0; i < actorState.m_ExternalAnimatedObjects.Count; i++)
			{
				ActorStateAnimObject actorStateAnimObject = actorState.m_ExternalAnimatedObjects[i];
				if (m_GameObject == actorStateAnimObject.m_GameObject && m_AnimLayer == actorStateAnimObject.m_AnimLayer)
				{
					flag = true;
					break;
				}
			}
			num++;
		}
		if (!flag)
		{
			m_GameObject.GetComponent<Animation>().Stop(m_AnimClip.name);
		}
	}
}
