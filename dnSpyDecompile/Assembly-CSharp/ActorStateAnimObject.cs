using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000964 RID: 2404
[Serializable]
public class ActorStateAnimObject
{
	// Token: 0x0600843D RID: 33853 RVA: 0x002ACA78 File Offset: 0x002AAC78
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

	// Token: 0x0600843E RID: 33854 RVA: 0x002ACB14 File Offset: 0x002AAD14
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
			string name = this.m_AnimClip.name;
			this.m_GameObject.GetComponent<Animation>()[name].enabled = true;
			if (Mathf.Approximately(this.m_CrossFadeSec, 0f))
			{
				if (!this.m_GameObject.GetComponent<Animation>().Play(name))
				{
					Debug.LogWarning(string.Format("ActorStateAnimObject.PlayNow() - FAILED to play clip {0} on {1}", name, this.m_GameObject));
					return;
				}
			}
			else
			{
				this.m_GameObject.GetComponent<Animation>().CrossFade(name, this.m_CrossFadeSec);
			}
		}
	}

	// Token: 0x0600843F RID: 33855 RVA: 0x002ACBC0 File Offset: 0x002AADC0
	public void Stop()
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
			this.m_GameObject.GetComponent<Animation>()[this.m_AnimClip.name].time = 0f;
			this.m_GameObject.GetComponent<Animation>().Sample();
			this.m_GameObject.GetComponent<Animation>()[this.m_AnimClip.name].enabled = false;
		}
	}

	// Token: 0x06008440 RID: 33856 RVA: 0x002ACC4C File Offset: 0x002AAE4C
	public void Stop(List<ActorState> nextStateList)
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
			bool flag = false;
			int num = 0;
			while (!flag && num < nextStateList.Count)
			{
				ActorState actorState = nextStateList[num];
				for (int i = 0; i < actorState.m_ExternalAnimatedObjects.Count; i++)
				{
					ActorStateAnimObject actorStateAnimObject = actorState.m_ExternalAnimatedObjects[i];
					if (this.m_GameObject == actorStateAnimObject.m_GameObject && this.m_AnimLayer == actorStateAnimObject.m_AnimLayer)
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
	}

	// Token: 0x04006F56 RID: 28502
	public bool m_Enabled = true;

	// Token: 0x04006F57 RID: 28503
	public GameObject m_GameObject;

	// Token: 0x04006F58 RID: 28504
	public AnimationClip m_AnimClip;

	// Token: 0x04006F59 RID: 28505
	public int m_AnimLayer;

	// Token: 0x04006F5A RID: 28506
	public float m_CrossFadeSec;

	// Token: 0x04006F5B RID: 28507
	public bool m_EmitParticles;

	// Token: 0x04006F5C RID: 28508
	public string m_Comment;

	// Token: 0x04006F5D RID: 28509
	private bool m_prevParticleEmitValue;
}
