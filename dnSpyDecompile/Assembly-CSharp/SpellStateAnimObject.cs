using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000975 RID: 2421
[Serializable]
public class SpellStateAnimObject
{
	// Token: 0x0600856D RID: 34157 RVA: 0x002B1457 File Offset: 0x002AF657
	public void Init()
	{
		if (this.m_GameObject == null)
		{
			return;
		}
		if (this.m_AnimClip == null)
		{
			return;
		}
		this.SetupAnimation();
	}

	// Token: 0x0600856E RID: 34158 RVA: 0x002B1480 File Offset: 0x002AF680
	private void SetupAnimation()
	{
		string name = this.m_AnimClip.name;
		Animation animation;
		if (this.m_GameObject.GetComponent<Animation>() == null)
		{
			animation = this.m_GameObject.AddComponent<Animation>();
		}
		else
		{
			animation = this.m_GameObject.GetComponent<Animation>();
		}
		animation.playAutomatically = false;
		if (animation[name] == null)
		{
			animation.AddClip(this.m_AnimClip, name);
		}
		animation[name].layer = this.m_AnimLayer;
	}

	// Token: 0x0600856F RID: 34159 RVA: 0x002B14FC File Offset: 0x002AF6FC
	public void OnLoad(SpellState state)
	{
		if (this.m_Target == SpellStateAnimObject.Target.AS_SPECIFIED)
		{
			if (this.m_GameObject == null)
			{
				Debug.LogError("Error: spell state anim target has a null game object after load");
			}
			return;
		}
		if (this.m_Target == SpellStateAnimObject.Target.ACTOR)
		{
			Actor actor = SceneUtils.FindComponentInParents<Actor>(state.transform);
			if (actor == null || actor.gameObject == null)
			{
				Debug.LogError("Error: spell state anim target has a null game object after load");
				return;
			}
			this.m_GameObject = actor.gameObject;
			this.SetupAnimation();
			return;
		}
		else
		{
			if (this.m_Target != SpellStateAnimObject.Target.ROOT_OBJECT)
			{
				Debug.LogWarning("Error: unimplemented spell anim target");
				return;
			}
			Actor actor2 = SceneUtils.FindComponentInParents<Actor>(state.transform);
			if (actor2 == null || actor2.gameObject == null)
			{
				Debug.LogError("Error: spell state anim target has a null game object after load");
				return;
			}
			this.m_GameObject = actor2.GetRootObject();
			this.SetupAnimation();
			return;
		}
	}

	// Token: 0x06008570 RID: 34160 RVA: 0x002B15C8 File Offset: 0x002AF7C8
	public void Play()
	{
		if (!this.m_Enabled)
		{
			return;
		}
		if (this.m_GameObject == null)
		{
			return;
		}
		if (this.m_AnimClip != null)
		{
			Animation component = this.m_GameObject.GetComponent<Animation>();
			string name = this.m_AnimClip.name;
			AnimationState animationState = component[name];
			animationState.enabled = true;
			animationState.speed = this.m_AnimSpeed;
			if (Mathf.Approximately(this.m_CrossFadeSec, 0f))
			{
				if (!component.Play(name))
				{
					Debug.LogWarning(string.Format("SpellStateAnimObject.PlayNow() - FAILED to play clip {0} on {1}", name, this.m_GameObject));
				}
			}
			else
			{
				component.CrossFade(name, this.m_CrossFadeSec);
			}
		}
		if (this.m_ControlParticles)
		{
			ParticleSystem component2 = this.m_GameObject.GetComponent<ParticleSystem>();
			if (component2 != null)
			{
				component2.Play();
			}
		}
	}

	// Token: 0x06008571 RID: 34161 RVA: 0x002B1690 File Offset: 0x002AF890
	public void Stop()
	{
		if (this.m_GameObject == null)
		{
			return;
		}
		if (this.m_AnimClip != null)
		{
			this.m_GameObject.GetComponent<Animation>().Stop(this.m_AnimClip.name);
		}
		if (this.m_ControlParticles)
		{
			ParticleSystem component = this.m_GameObject.GetComponent<ParticleSystem>();
			if (component != null)
			{
				component.Stop();
			}
		}
	}

	// Token: 0x06008572 RID: 34162 RVA: 0x002B16F8 File Offset: 0x002AF8F8
	public void Stop(List<SpellState> nextStateList)
	{
		if (this.m_GameObject == null)
		{
			return;
		}
		if (this.m_AnimClip != null)
		{
			bool flag = false;
			int num = 0;
			while (!flag && num < nextStateList.Count)
			{
				SpellState spellState = nextStateList[num];
				for (int i = 0; i < spellState.m_ExternalAnimatedObjects.Count; i++)
				{
					SpellStateAnimObject spellStateAnimObject = spellState.m_ExternalAnimatedObjects[i];
					if (spellStateAnimObject.m_Enabled && this.m_GameObject == spellStateAnimObject.m_GameObject && this.m_AnimLayer == spellStateAnimObject.m_AnimLayer)
					{
						flag = true;
						break;
					}
				}
				num++;
			}
			if (!flag)
			{
				this.m_GameObject.GetComponent<Animation>().Stop(this.m_AnimClip.name);
			}
		}
		if (this.m_ControlParticles)
		{
			ParticleSystem component = this.m_GameObject.GetComponent<ParticleSystem>();
			if (component != null)
			{
				component.Stop();
			}
		}
	}

	// Token: 0x06008573 RID: 34163 RVA: 0x002B17DB File Offset: 0x002AF9DB
	public void Show()
	{
		if (this.m_GameObject == null)
		{
			return;
		}
		this.m_GameObject.SetActive(true);
	}

	// Token: 0x06008574 RID: 34164 RVA: 0x002B17F8 File Offset: 0x002AF9F8
	public void Hide()
	{
		if (this.m_GameObject == null)
		{
			return;
		}
		this.m_GameObject.SetActive(false);
	}

	// Token: 0x04006FD6 RID: 28630
	public GameObject m_GameObject;

	// Token: 0x04006FD7 RID: 28631
	public SpellStateAnimObject.Target m_Target;

	// Token: 0x04006FD8 RID: 28632
	public AnimationClip m_AnimClip;

	// Token: 0x04006FD9 RID: 28633
	public int m_AnimLayer;

	// Token: 0x04006FDA RID: 28634
	public float m_AnimSpeed = 1f;

	// Token: 0x04006FDB RID: 28635
	public float m_CrossFadeSec;

	// Token: 0x04006FDC RID: 28636
	public bool m_ControlParticles;

	// Token: 0x04006FDD RID: 28637
	public bool m_EmitParticles;

	// Token: 0x04006FDE RID: 28638
	public string m_Comment;

	// Token: 0x04006FDF RID: 28639
	public bool m_Enabled = true;

	// Token: 0x04006FE0 RID: 28640
	private bool m_prevParticleEmitValue;

	// Token: 0x02002641 RID: 9793
	public enum Target
	{
		// Token: 0x0400F00A RID: 61450
		AS_SPECIFIED,
		// Token: 0x0400F00B RID: 61451
		ACTOR,
		// Token: 0x0400F00C RID: 61452
		ROOT_OBJECT
	}
}
