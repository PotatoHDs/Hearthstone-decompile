using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SpellStateAnimObject
{
	public enum Target
	{
		AS_SPECIFIED,
		ACTOR,
		ROOT_OBJECT
	}

	public GameObject m_GameObject;

	public Target m_Target;

	public AnimationClip m_AnimClip;

	public int m_AnimLayer;

	public float m_AnimSpeed = 1f;

	public float m_CrossFadeSec;

	public bool m_ControlParticles;

	public bool m_EmitParticles;

	public string m_Comment;

	public bool m_Enabled = true;

	private bool m_prevParticleEmitValue;

	public void Init()
	{
		if (!(m_GameObject == null) && !(m_AnimClip == null))
		{
			SetupAnimation();
		}
	}

	private void SetupAnimation()
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

	public void OnLoad(SpellState state)
	{
		if (m_Target == Target.AS_SPECIFIED)
		{
			if (m_GameObject == null)
			{
				Debug.LogError("Error: spell state anim target has a null game object after load");
			}
		}
		else if (m_Target == Target.ACTOR)
		{
			Actor actor = SceneUtils.FindComponentInParents<Actor>(state.transform);
			if (actor == null || actor.gameObject == null)
			{
				Debug.LogError("Error: spell state anim target has a null game object after load");
				return;
			}
			m_GameObject = actor.gameObject;
			SetupAnimation();
		}
		else if (m_Target == Target.ROOT_OBJECT)
		{
			Actor actor2 = SceneUtils.FindComponentInParents<Actor>(state.transform);
			if (actor2 == null || actor2.gameObject == null)
			{
				Debug.LogError("Error: spell state anim target has a null game object after load");
				return;
			}
			m_GameObject = actor2.GetRootObject();
			SetupAnimation();
		}
		else
		{
			Debug.LogWarning("Error: unimplemented spell anim target");
		}
	}

	public void Play()
	{
		if (!m_Enabled || m_GameObject == null)
		{
			return;
		}
		if (m_AnimClip != null)
		{
			Animation component = m_GameObject.GetComponent<Animation>();
			string name = m_AnimClip.name;
			AnimationState animationState = component[name];
			animationState.enabled = true;
			animationState.speed = m_AnimSpeed;
			if (Mathf.Approximately(m_CrossFadeSec, 0f))
			{
				if (!component.Play(name))
				{
					Debug.LogWarning($"SpellStateAnimObject.PlayNow() - FAILED to play clip {name} on {m_GameObject}");
				}
			}
			else
			{
				component.CrossFade(name, m_CrossFadeSec);
			}
		}
		if (m_ControlParticles)
		{
			ParticleSystem component2 = m_GameObject.GetComponent<ParticleSystem>();
			if (component2 != null)
			{
				component2.Play();
			}
		}
	}

	public void Stop()
	{
		if (m_GameObject == null)
		{
			return;
		}
		if (m_AnimClip != null)
		{
			m_GameObject.GetComponent<Animation>().Stop(m_AnimClip.name);
		}
		if (m_ControlParticles)
		{
			ParticleSystem component = m_GameObject.GetComponent<ParticleSystem>();
			if (component != null)
			{
				component.Stop();
			}
		}
	}

	public void Stop(List<SpellState> nextStateList)
	{
		if (m_GameObject == null)
		{
			return;
		}
		if (m_AnimClip != null)
		{
			bool flag = false;
			int num = 0;
			while (!flag && num < nextStateList.Count)
			{
				SpellState spellState = nextStateList[num];
				for (int i = 0; i < spellState.m_ExternalAnimatedObjects.Count; i++)
				{
					SpellStateAnimObject spellStateAnimObject = spellState.m_ExternalAnimatedObjects[i];
					if (spellStateAnimObject.m_Enabled && m_GameObject == spellStateAnimObject.m_GameObject && m_AnimLayer == spellStateAnimObject.m_AnimLayer)
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
		if (m_ControlParticles)
		{
			ParticleSystem component = m_GameObject.GetComponent<ParticleSystem>();
			if (component != null)
			{
				component.Stop();
			}
		}
	}

	public void Show()
	{
		if (!(m_GameObject == null))
		{
			m_GameObject.SetActive(value: true);
		}
	}

	public void Hide()
	{
		if (!(m_GameObject == null))
		{
			m_GameObject.SetActive(value: false);
		}
	}
}
