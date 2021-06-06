using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000965 RID: 2405
public class ActorState : MonoBehaviour
{
	// Token: 0x06008442 RID: 33858 RVA: 0x002ACD18 File Offset: 0x002AAF18
	private void Start()
	{
		this.m_stateMgr = SceneUtils.FindComponentInParents<ActorStateMgr>(base.gameObject);
		foreach (ActorStateAnimObject actorStateAnimObject in this.m_ExternalAnimatedObjects)
		{
			actorStateAnimObject.Init();
		}
		this.m_initialized = true;
		if (this.m_playing)
		{
			base.gameObject.SetActive(true);
			this.PlayNow();
		}
	}

	// Token: 0x06008443 RID: 33859 RVA: 0x002ACD9C File Offset: 0x002AAF9C
	public void Play()
	{
		if (this.m_playing)
		{
			return;
		}
		this.m_playing = true;
		if (!this.m_initialized)
		{
			return;
		}
		base.gameObject.SetActive(true);
		this.PlayNow();
	}

	// Token: 0x06008444 RID: 33860 RVA: 0x002ACDCC File Offset: 0x002AAFCC
	public void Stop(List<ActorState> nextStateList)
	{
		if (!this.m_playing)
		{
			return;
		}
		this.m_playing = false;
		if (!this.m_initialized)
		{
			return;
		}
		if (base.GetComponent<Animation>() != null)
		{
			base.GetComponent<Animation>().Stop();
		}
		if (nextStateList == null)
		{
			using (List<ActorStateAnimObject>.Enumerator enumerator = this.m_ExternalAnimatedObjects.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					ActorStateAnimObject actorStateAnimObject = enumerator.Current;
					actorStateAnimObject.Stop();
				}
				goto IL_9C;
			}
		}
		foreach (ActorStateAnimObject actorStateAnimObject2 in this.m_ExternalAnimatedObjects)
		{
			actorStateAnimObject2.Stop(nextStateList);
		}
		IL_9C:
		base.gameObject.SetActive(false);
	}

	// Token: 0x06008445 RID: 33861 RVA: 0x002ACEA0 File Offset: 0x002AB0A0
	public float GetAnimationDuration()
	{
		float num = 0f;
		for (int i = 0; i < this.m_ExternalAnimatedObjects.Count; i++)
		{
			if (this.m_ExternalAnimatedObjects[i].m_GameObject != null)
			{
				num = Mathf.Max(this.m_ExternalAnimatedObjects[i].m_AnimClip.length, num);
			}
		}
		return num;
	}

	// Token: 0x06008446 RID: 33862 RVA: 0x002ACF00 File Offset: 0x002AB100
	public void ShowState()
	{
		base.gameObject.SetActive(true);
		this.Play();
	}

	// Token: 0x06008447 RID: 33863 RVA: 0x002ACF14 File Offset: 0x002AB114
	public void HideState()
	{
		this.Stop(null);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06008448 RID: 33864 RVA: 0x002ACF29 File Offset: 0x002AB129
	private void OnChangeState(ActorStateType stateType)
	{
		this.m_stateMgr.ChangeState(stateType);
	}

	// Token: 0x06008449 RID: 33865 RVA: 0x002ACF38 File Offset: 0x002AB138
	private void PlayNow()
	{
		if (base.GetComponent<Animation>() != null)
		{
			base.GetComponent<Animation>().Play();
		}
		foreach (ActorStateAnimObject actorStateAnimObject in this.m_ExternalAnimatedObjects)
		{
			actorStateAnimObject.Play();
		}
	}

	// Token: 0x04006F5E RID: 28510
	public ActorStateType m_StateType;

	// Token: 0x04006F5F RID: 28511
	public List<ActorStateAnimObject> m_ExternalAnimatedObjects;

	// Token: 0x04006F60 RID: 28512
	private ActorStateMgr m_stateMgr;

	// Token: 0x04006F61 RID: 28513
	private bool m_playing;

	// Token: 0x04006F62 RID: 28514
	private bool m_initialized;
}
