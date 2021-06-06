using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020007EE RID: 2030
[CustomEditClass]
public class EnterTheColiseumSpell : Spell
{
	// Token: 0x06006EB6 RID: 28342 RVA: 0x0023B0EF File Offset: 0x002392EF
	protected override void OnAction(SpellStateType prevStateType)
	{
		base.OnAction(prevStateType);
		this.m_survivorCards = this.FindSurvivors();
		base.StartCoroutine(this.PerformActions());
	}

	// Token: 0x06006EB7 RID: 28343 RVA: 0x0023B111 File Offset: 0x00239311
	private IEnumerator PerformActions()
	{
		this.m_effectsPlaying = true;
		foreach (Card card in this.m_survivorCards)
		{
			if (!(card == null))
			{
				card.SetDoNotSort(true);
				card.GetActor().SetUnlit();
				this.LiftCard(card);
				yield return new WaitForSeconds(this.m_LiftOffset);
			}
		}
		List<Card>.Enumerator enumerator = default(List<Card>.Enumerator);
		FullScreenFXMgr.Get().Vignette(1f, this.m_LightingFadeTime, this.m_lightFadeEaseType, null, null);
		if (!string.IsNullOrEmpty(this.m_SpellStartSoundPrefab))
		{
			SoundManager.Get().LoadAndPlay(this.m_SpellStartSoundPrefab);
		}
		this.PlayDustCloudSpell();
		yield return new WaitForSeconds(this.m_LiftTime);
		foreach (Card card2 in this.m_survivorCards)
		{
			if (!(card2 == null))
			{
				this.PlaySurvivorSpell(card2);
			}
		}
		yield return new WaitForSeconds(this.m_DestroyMinionDelay);
		this.OnSpellFinished();
		CameraShakeMgr.Shake(Camera.main, new Vector3(this.m_CameraShakeMagnitude, this.m_CameraShakeMagnitude, this.m_CameraShakeMagnitude), 0.75f);
		yield return new WaitForSeconds(this.m_LowerDelay);
		while (this.m_numSurvivorSpellsPlaying > 0)
		{
			yield return null;
		}
		foreach (Card card3 in this.m_survivorCards)
		{
			if (!(card3 == null))
			{
				Zone zone = card3.GetZone();
				if (zone is ZonePlay)
				{
					ZonePlay zonePlay = (ZonePlay)zone;
					this.LowerCard(card3.gameObject, zonePlay.GetCardPosition(card3));
					yield return new WaitForSeconds(this.m_LowerOffset);
				}
			}
		}
		enumerator = default(List<Card>.Enumerator);
		FullScreenFXMgr.Get().StopVignette(this.m_LightingFadeTime, this.m_lightFadeEaseType, null, null);
		if (this.m_ImpactSpellPrefab != null)
		{
			foreach (Card card4 in this.m_survivorCards)
			{
				if (!(card4 == null))
				{
					Spell spell2 = UnityEngine.Object.Instantiate<Spell>(this.m_ImpactSpellPrefab);
					spell2.transform.parent = card4.gameObject.transform;
					spell2.transform.localPosition = new Vector3(0f, 0f, 0f);
					spell2.AddStateFinishedCallback(delegate(Spell spell, SpellStateType prevStateType, object userData)
					{
						this.m_effectsPlaying = false;
						if (spell.GetActiveState() == SpellStateType.NONE)
						{
							UnityEngine.Object.Destroy(spell.gameObject);
						}
					});
					spell2.Activate();
					yield return new WaitForSeconds(this.m_LowerOffset);
				}
			}
			enumerator = default(List<Card>.Enumerator);
		}
		yield return new WaitForSeconds(this.m_LowerTime);
		foreach (Card card5 in this.m_survivorCards)
		{
			if (!(card5 == null))
			{
				card5.SetDoNotSort(false);
				card5.GetActor().SetLit();
			}
		}
		using (List<ZonePlay>.Enumerator enumerator3 = ZoneMgr.Get().FindZonesOfType<ZonePlay>().GetEnumerator())
		{
			while (enumerator3.MoveNext())
			{
				ZonePlay zonePlay2 = enumerator3.Current;
				zonePlay2.UpdateLayout();
			}
			goto IL_49C;
		}
		IL_482:
		yield return null;
		IL_49C:
		if (!this.m_effectsPlaying)
		{
			this.OnStateFinished();
			yield break;
		}
		goto IL_482;
		yield break;
	}

	// Token: 0x06006EB8 RID: 28344 RVA: 0x0023B120 File Offset: 0x00239320
	private void LiftCard(Card card)
	{
		GameObject gameObject = card.gameObject;
		Vector3 position = gameObject.transform.position;
		Vector3 position2 = card.GetZone().gameObject.transform.position;
		Hashtable args = iTween.Hash(new object[]
		{
			"time",
			this.m_LiftTime,
			"position",
			new Vector3(this.m_survivorsMeetInMiddle ? position2.x : position.x, position.y + this.m_survivorLiftHeight, position.z),
			"onstart",
			new Action<object>(delegate(object newVal)
			{
				SoundManager.Get().LoadAndPlay(this.m_RaiseSoundName);
			}),
			"easetype",
			this.m_liftEaseType
		});
		iTween.MoveTo(gameObject, args);
	}

	// Token: 0x06006EB9 RID: 28345 RVA: 0x0023B1EC File Offset: 0x002393EC
	private void LowerCard(GameObject target, Vector3 finalPosition)
	{
		Hashtable args = iTween.Hash(new object[]
		{
			"time",
			this.m_LowerTime,
			"position",
			finalPosition,
			"easetype",
			this.m_lowerEaseType
		});
		iTween.MoveTo(target, args);
	}

	// Token: 0x06006EBA RID: 28346 RVA: 0x0023B24C File Offset: 0x0023944C
	private List<Card> FindSurvivors()
	{
		List<Card> list = new List<Card>();
		foreach (GameObject gameObject in this.m_targets)
		{
			Card component = gameObject.GetComponent<Card>();
			bool flag = true;
			foreach (PowerTask powerTask in this.m_taskList.GetTaskList())
			{
				Network.PowerHistory power = powerTask.GetPower();
				if (power.Type == Network.PowerType.TAG_CHANGE)
				{
					Network.HistTagChange histTagChange = power as Network.HistTagChange;
					if (histTagChange.Tag == 360 && histTagChange.Value == 1)
					{
						Entity entity = GameState.Get().GetEntity(histTagChange.Entity);
						if (entity == null)
						{
							string format = string.Format("{0}.FindSurvivors() - WARNING trying to get entity with id {1} but there is no entity with that id", this, histTagChange.Entity);
							Log.Power.PrintWarning(format, Array.Empty<object>());
						}
						else if (component == entity.GetCard())
						{
							flag = false;
							break;
						}
					}
				}
			}
			if (flag)
			{
				list.Add(component);
			}
		}
		return list;
	}

	// Token: 0x06006EBB RID: 28347 RVA: 0x0023B384 File Offset: 0x00239584
	private void PlaySurvivorSpell(Card card)
	{
		if (this.m_survivorSpellPrefab == null)
		{
			return;
		}
		this.m_numSurvivorSpellsPlaying++;
		Spell spell2 = UnityEngine.Object.Instantiate<Spell>(this.m_survivorSpellPrefab);
		spell2.transform.parent = card.GetActor().transform;
		spell2.AddFinishedCallback(delegate(Spell spell, object spellUserData)
		{
			this.m_numSurvivorSpellsPlaying--;
		});
		spell2.AddStateFinishedCallback(delegate(Spell spell, SpellStateType prevStateType, object userData)
		{
			if (spell.GetActiveState() == SpellStateType.NONE)
			{
				UnityEngine.Object.Destroy(spell.gameObject);
			}
		});
		spell2.SetSource(card.gameObject);
		spell2.Activate();
	}

	// Token: 0x06006EBC RID: 28348 RVA: 0x0023B418 File Offset: 0x00239618
	private void PlayDustCloudSpell()
	{
		if (this.m_DustSpellPrefab == null)
		{
			return;
		}
		Spell spell2 = UnityEngine.Object.Instantiate<Spell>(this.m_DustSpellPrefab);
		spell2.AddStateFinishedCallback(delegate(Spell spell, SpellStateType prevStateType, object userData)
		{
			if (spell.GetActiveState() == SpellStateType.NONE)
			{
				UnityEngine.Object.Destroy(spell.gameObject);
			}
		});
		spell2.Activate();
	}

	// Token: 0x040058CC RID: 22732
	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_SpellStartSoundPrefab;

	// Token: 0x040058CD RID: 22733
	public float m_survivorLiftHeight = 2f;

	// Token: 0x040058CE RID: 22734
	public float m_LiftTime = 0.5f;

	// Token: 0x040058CF RID: 22735
	public float m_LiftOffset = 0.1f;

	// Token: 0x040058D0 RID: 22736
	public float m_DestroyMinionDelay = 0.5f;

	// Token: 0x040058D1 RID: 22737
	public float m_LowerDelay = 1.5f;

	// Token: 0x040058D2 RID: 22738
	public float m_LowerOffset = 0.05f;

	// Token: 0x040058D3 RID: 22739
	public float m_LowerTime = 0.7f;

	// Token: 0x040058D4 RID: 22740
	public float m_LightingFadeTime = 0.5f;

	// Token: 0x040058D5 RID: 22741
	public float m_CameraShakeMagnitude = 0.075f;

	// Token: 0x040058D6 RID: 22742
	public iTween.EaseType m_liftEaseType = iTween.EaseType.easeInQuart;

	// Token: 0x040058D7 RID: 22743
	public iTween.EaseType m_lowerEaseType = iTween.EaseType.easeOutCubic;

	// Token: 0x040058D8 RID: 22744
	public iTween.EaseType m_lightFadeEaseType = iTween.EaseType.easeOutCubic;

	// Token: 0x040058D9 RID: 22745
	public Spell m_survivorSpellPrefab;

	// Token: 0x040058DA RID: 22746
	public Spell m_DustSpellPrefab;

	// Token: 0x040058DB RID: 22747
	public bool m_survivorsMeetInMiddle = true;

	// Token: 0x040058DC RID: 22748
	public Spell m_ImpactSpellPrefab;

	// Token: 0x040058DD RID: 22749
	public string m_RaiseSoundName;

	// Token: 0x040058DE RID: 22750
	private List<Card> m_survivorCards;

	// Token: 0x040058DF RID: 22751
	private bool m_effectsPlaying;

	// Token: 0x040058E0 RID: 22752
	private int m_numSurvivorSpellsPlaying;
}
