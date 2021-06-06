using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000977 RID: 2423
public class SpellState : MonoBehaviour
{
	// Token: 0x0600857C RID: 34172 RVA: 0x002B1994 File Offset: 0x002AFB94
	private void Start()
	{
		this.m_spell = SceneUtils.FindComponentInParents<Spell>(base.gameObject);
		for (int i = 0; i < this.m_ExternalAnimatedObjects.Count; i++)
		{
			this.m_ExternalAnimatedObjects[i].Init();
		}
		for (int j = 0; j < this.m_AudioSources.Count; j++)
		{
			this.m_AudioSources[j].Init();
		}
		this.m_initialized = true;
		if (this.m_shown && this.m_playing)
		{
			this.PlayImpl();
			return;
		}
		this.StopImpl(null);
	}

	// Token: 0x0600857D RID: 34173 RVA: 0x002B1A25 File Offset: 0x002AFC25
	public void Play()
	{
		if (this.m_playing)
		{
			return;
		}
		if (!this.m_shown)
		{
			return;
		}
		this.m_playing = true;
		if (!this.m_initialized)
		{
			return;
		}
		this.PlayImpl();
	}

	// Token: 0x0600857E RID: 34174 RVA: 0x002B1A4F File Offset: 0x002AFC4F
	public void Stop(List<SpellState> nextStateList)
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
		this.StopImpl(nextStateList);
	}

	// Token: 0x0600857F RID: 34175 RVA: 0x002B1A71 File Offset: 0x002AFC71
	public void ShowState()
	{
		if (this.m_shown)
		{
			return;
		}
		this.m_shown = true;
		if (!this.m_initialized)
		{
			return;
		}
		if (!this.m_playing)
		{
			return;
		}
		this.PlayImpl();
	}

	// Token: 0x06008580 RID: 34176 RVA: 0x002B1A9B File Offset: 0x002AFC9B
	public void HideState()
	{
		if (!this.m_shown)
		{
			return;
		}
		this.m_shown = false;
		if (!this.m_initialized)
		{
			return;
		}
		if (!this.m_playing)
		{
			return;
		}
		this.StopImpl(null);
	}

	// Token: 0x06008581 RID: 34177 RVA: 0x002B1AC8 File Offset: 0x002AFCC8
	public void OnLoad()
	{
		base.gameObject.SetActive(true);
		foreach (SpellStateAnimObject spellStateAnimObject in this.m_ExternalAnimatedObjects)
		{
			spellStateAnimObject.OnLoad(this);
		}
	}

	// Token: 0x06008582 RID: 34178 RVA: 0x002B1B28 File Offset: 0x002AFD28
	private void OnStateFinished()
	{
		this.m_spell.OnStateFinished();
	}

	// Token: 0x06008583 RID: 34179 RVA: 0x002B1B35 File Offset: 0x002AFD35
	private void OnSpellFinished()
	{
		this.m_spell.OnSpellFinished();
	}

	// Token: 0x06008584 RID: 34180 RVA: 0x002B1B42 File Offset: 0x002AFD42
	private void OnChangeState(SpellStateType stateType)
	{
		this.m_spell.ChangeState(stateType);
	}

	// Token: 0x06008585 RID: 34181 RVA: 0x002B1B50 File Offset: 0x002AFD50
	private IEnumerator DelayedPlay()
	{
		yield return new WaitForSeconds(this.m_StartDelaySec);
		this.PlayNow();
		yield break;
	}

	// Token: 0x06008586 RID: 34182 RVA: 0x002B1B5F File Offset: 0x002AFD5F
	private void PlayImpl()
	{
		base.gameObject.SetActive(true);
		if (Mathf.Approximately(this.m_StartDelaySec, 0f))
		{
			this.PlayNow();
			return;
		}
		base.StartCoroutine(this.DelayedPlay());
	}

	// Token: 0x06008587 RID: 34183 RVA: 0x002B1B94 File Offset: 0x002AFD94
	private void StopImpl(List<SpellState> nextStateList)
	{
		if (nextStateList == null)
		{
			using (List<SpellStateAnimObject>.Enumerator enumerator = this.m_ExternalAnimatedObjects.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					SpellStateAnimObject spellStateAnimObject = enumerator.Current;
					spellStateAnimObject.Stop();
				}
				goto IL_6A;
			}
		}
		foreach (SpellStateAnimObject spellStateAnimObject2 in this.m_ExternalAnimatedObjects)
		{
			spellStateAnimObject2.Stop(nextStateList);
		}
		IL_6A:
		foreach (SpellStateAudioSource spellStateAudioSource in this.m_AudioSources)
		{
			spellStateAudioSource.Stop();
		}
		base.gameObject.SetActive(false);
	}

	// Token: 0x06008588 RID: 34184 RVA: 0x002B1C74 File Offset: 0x002AFE74
	private void PlayNow()
	{
		foreach (SpellStateAnimObject spellStateAnimObject in this.m_ExternalAnimatedObjects)
		{
			spellStateAnimObject.Play();
		}
		foreach (SpellStateAudioSource spellStateAudioSource in this.m_AudioSources)
		{
			spellStateAudioSource.Play(this);
		}
	}

	// Token: 0x04006FE7 RID: 28647
	public SpellStateType m_StateType;

	// Token: 0x04006FE8 RID: 28648
	public float m_StartDelaySec;

	// Token: 0x04006FE9 RID: 28649
	public List<SpellStateAnimObject> m_ExternalAnimatedObjects;

	// Token: 0x04006FEA RID: 28650
	public List<SpellStateAudioSource> m_AudioSources;

	// Token: 0x04006FEB RID: 28651
	private Spell m_spell;

	// Token: 0x04006FEC RID: 28652
	private bool m_playing;

	// Token: 0x04006FED RID: 28653
	private bool m_initialized;

	// Token: 0x04006FEE RID: 28654
	private bool m_shown = true;
}
