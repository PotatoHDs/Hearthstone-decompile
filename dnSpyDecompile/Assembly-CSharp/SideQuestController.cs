using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200090F RID: 2319
[RequireComponent(typeof(Spell))]
[RequireComponent(typeof(Actor))]
public class SideQuestController : MonoBehaviour
{
	// Token: 0x060080ED RID: 33005 RVA: 0x0029DDA4 File Offset: 0x0029BFA4
	private void Awake()
	{
		this.m_currentQuestProgress = 0;
		this.m_targetQuestProgress = 0;
		this.m_isScalingDown = false;
		this.m_spell = base.GetComponent<Spell>();
		if (this.m_spell == null)
		{
			Log.Gameplay.PrintError("SideQuestController.Awake(): GameObject {0} does not have a Spell Component!", new object[]
			{
				base.gameObject.name
			});
		}
		this.m_spell.AddSpellEventCallback(new Spell.SpellEventCallback(this.OnSpellEvent));
		this.m_actor = base.GetComponent<Actor>();
		if (this.m_actor == null)
		{
			Log.Gameplay.PrintError("SideQuestController.Awake(): GameObject {0} does not have an Actor Component!", new object[]
			{
				base.gameObject.name
			});
		}
	}

	// Token: 0x060080EE RID: 33006 RVA: 0x0029DE57 File Offset: 0x0029C057
	public void UpdateQuestUI(bool allowQuestComplete)
	{
		base.StartCoroutine(this.UpdateQuestUIImpl(allowQuestComplete));
	}

	// Token: 0x060080EF RID: 33007 RVA: 0x0029DE67 File Offset: 0x0029C067
	private IEnumerator UpdateQuestUIImpl(bool allowQuestComplete)
	{
		Entity entity = this.GetEntity();
		if (entity == null)
		{
			yield break;
		}
		int num = entity.GetTag(GAME_TAG.QUEST_PROGRESS);
		num = Mathf.Min(num, this.m_questProgressTotal);
		if (num == this.m_targetQuestProgress)
		{
			yield break;
		}
		if (!allowQuestComplete && num >= this.m_questProgressTotal)
		{
			yield break;
		}
		this.m_targetQuestProgress = num;
		GameState.Get().SetBusy(true);
		while (this.m_isScalingDown)
		{
			yield return null;
		}
		if (!this.m_spell.IsActive())
		{
			this.UpdateProgressText(this.m_targetQuestProgress);
			this.m_spell.ActivateState(SpellStateType.ACTION);
		}
		yield break;
	}

	// Token: 0x060080F0 RID: 33008 RVA: 0x0029DE80 File Offset: 0x0029C080
	private void OnSpellEvent(string eventName, object eventData, object userData)
	{
		if (eventName == "ScaledUp")
		{
			base.StartCoroutine(this.UpdateQuestProgress());
			return;
		}
		if (eventName == "ScaledDown")
		{
			this.m_isScalingDown = false;
			if (this.m_currentQuestProgress >= this.m_questProgressTotal)
			{
				this.CompleteQuest();
				return;
			}
			GameState.Get().SetBusy(false);
			this.m_spell.ActivateState(SpellStateType.NONE);
		}
	}

	// Token: 0x060080F1 RID: 33009 RVA: 0x0029DEE8 File Offset: 0x0029C0E8
	private IEnumerator UpdateQuestProgress()
	{
		bool done = false;
		while (!done)
		{
			yield return new WaitForSeconds(this.m_ProgressUpdateDelay);
			if (this.m_currentQuestProgress < this.m_targetQuestProgress)
			{
				this.UpdateProgressText(this.m_currentQuestProgress + 1);
			}
			else
			{
				done = true;
			}
		}
		this.m_isScalingDown = true;
		this.m_spell.GetComponent<PlayMakerFSM>().SendEvent("ScaleDown");
		yield break;
	}

	// Token: 0x060080F2 RID: 33010 RVA: 0x0029DEF8 File Offset: 0x0029C0F8
	private void UpdateProgressText(int currentProgress)
	{
		this.m_currentQuestProgress = currentProgress;
		this.m_ProgressUpdateParticles.Stop();
		this.m_ProgressUpdateParticles.Play();
		this.m_ProgressText.Text = string.Format("{0}/{1}", this.m_currentQuestProgress, this.m_questProgressTotal);
	}

	// Token: 0x060080F3 RID: 33011 RVA: 0x0029DF50 File Offset: 0x0029C150
	private void CompleteQuest()
	{
		GameState.Get().SetBusy(false);
		Card card = this.m_entity.GetCard();
		if (UniversalInputManager.UsePhoneUI)
		{
			ZoneSecret zoneSecret = card.GetZone() as ZoneSecret;
			if (zoneSecret != null && zoneSecret.GetSideQuestCount() == 1)
			{
				card.HideCard();
			}
		}
		else
		{
			card.HideCard();
		}
		this.m_spell.ActivateState(SpellStateType.DEATH);
	}

	// Token: 0x060080F4 RID: 33012 RVA: 0x0029DFB8 File Offset: 0x0029C1B8
	private Entity GetEntity()
	{
		if (this.m_entity == null)
		{
			this.m_entity = this.m_actor.GetEntity();
			if (this.m_entity != null)
			{
				this.m_currentQuestProgress = this.m_entity.GetTag(GAME_TAG.QUEST_PROGRESS);
				this.m_questProgressTotal = this.m_entity.GetTag(GAME_TAG.QUEST_PROGRESS_TOTAL);
			}
		}
		return this.m_entity;
	}

	// Token: 0x0400699C RID: 27036
	public UberText m_ProgressText;

	// Token: 0x0400699D RID: 27037
	public float m_ProgressUpdateDelay = 1f;

	// Token: 0x0400699E RID: 27038
	public ParticleSystem m_ProgressUpdateParticles;

	// Token: 0x0400699F RID: 27039
	private Spell m_spell;

	// Token: 0x040069A0 RID: 27040
	private Actor m_actor;

	// Token: 0x040069A1 RID: 27041
	private Entity m_entity;

	// Token: 0x040069A2 RID: 27042
	private int m_currentQuestProgress;

	// Token: 0x040069A3 RID: 27043
	private int m_questProgressTotal;

	// Token: 0x040069A4 RID: 27044
	private int m_targetQuestProgress;

	// Token: 0x040069A5 RID: 27045
	private bool m_isScalingDown;
}
