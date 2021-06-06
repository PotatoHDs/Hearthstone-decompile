using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000903 RID: 2307
[RequireComponent(typeof(Spell))]
[RequireComponent(typeof(Actor))]
public class QuestController : MonoBehaviour
{
	// Token: 0x06008061 RID: 32865 RVA: 0x0029BAE4 File Offset: 0x00299CE4
	private void Awake()
	{
		this.m_currentQuestProgress = 0;
		this.m_targetQuestProgress = 0;
		this.m_isScalingDown = false;
		this.m_spell = base.GetComponent<Spell>();
		if (this.m_spell == null)
		{
			Log.Gameplay.PrintError("QuestController.Awake(): GameObject {0} does not have a Spell Component!", new object[]
			{
				base.gameObject.name
			});
		}
		this.m_spell.AddSpellEventCallback(new Spell.SpellEventCallback(this.OnSpellEvent));
		this.m_actor = base.GetComponent<Actor>();
		if (this.m_actor == null)
		{
			Log.Gameplay.PrintError("QuestController.Awake(): GameObject {0} does not have an Actor Component!", new object[]
			{
				base.gameObject.name
			});
		}
	}

	// Token: 0x06008062 RID: 32866 RVA: 0x0029BB98 File Offset: 0x00299D98
	private void Start()
	{
		this.m_questProgressUI = this.m_QuestProgressUIContainer.PrefabGameObject(true).GetComponent<QuestProgressUI>();
		this.m_questProgressUI.SetOriginalQuestActor(this.m_actor);
		this.m_questProgressUI.Hide();
		Transform parent = Board.Get().FindBone(this.m_QuestUIBoneName);
		this.m_questProgressUI.transform.parent = parent;
		TransformUtil.Identity(this.m_questProgressUI);
	}

	// Token: 0x06008063 RID: 32867 RVA: 0x0029BC08 File Offset: 0x00299E08
	public static string GetRewardCardIDFromQuestCardID(Entity ent)
	{
		int dbId = 53649;
		if (ent != null && ent.HasTag(GAME_TAG.QUEST_REWARD_DATABASE_ID))
		{
			dbId = ent.GetTag(GAME_TAG.QUEST_REWARD_DATABASE_ID);
		}
		return GameUtils.TranslateDbIdToCardId(dbId, false);
	}

	// Token: 0x06008064 RID: 32868 RVA: 0x0029BC3E File Offset: 0x00299E3E
	public void NotifyMousedOver()
	{
		if (!this.m_questCompleted)
		{
			base.StopCoroutine("WaitThenShowQuestUI");
			base.StartCoroutine("WaitThenShowQuestUI");
		}
	}

	// Token: 0x06008065 RID: 32869 RVA: 0x0029BC5F File Offset: 0x00299E5F
	public void NotifyMousedOut()
	{
		base.StopCoroutine("WaitThenShowQuestUI");
		this.m_questProgressUI.Hide();
	}

	// Token: 0x06008066 RID: 32870 RVA: 0x0029BC77 File Offset: 0x00299E77
	private IEnumerator WaitThenShowQuestUI()
	{
		yield return new WaitForSeconds(InputManager.Get().m_MouseOverDelay);
		if (this.GetEntity() == null)
		{
			yield break;
		}
		this.m_questProgressUI.UpdateText(this.m_currentQuestProgress, this.m_questProgressTotal);
		this.m_questProgressUI.Show();
		yield break;
	}

	// Token: 0x06008067 RID: 32871 RVA: 0x0029BC86 File Offset: 0x00299E86
	public void UpdateQuestUI()
	{
		base.StartCoroutine(this.UpdateQuestUIImpl());
	}

	// Token: 0x06008068 RID: 32872 RVA: 0x0029BC95 File Offset: 0x00299E95
	private IEnumerator UpdateQuestUIImpl()
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
		this.m_targetQuestProgress = num;
		GameState.Get().SetBusy(true);
		while (this.m_isScalingDown)
		{
			yield return null;
		}
		if (this.m_targetQuestProgress < this.m_questProgressTotal)
		{
			GameState.Get().SetBusy(false);
		}
		if (!this.m_spell.IsActive())
		{
			this.UpdateProgressText(this.m_targetQuestProgress);
			this.m_spell.ActivateState(SpellStateType.ACTION);
		}
		yield break;
	}

	// Token: 0x06008069 RID: 32873 RVA: 0x0029BCA4 File Offset: 0x00299EA4
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
			this.m_spell.ActivateState(SpellStateType.NONE);
		}
	}

	// Token: 0x0600806A RID: 32874 RVA: 0x0029BD01 File Offset: 0x00299F01
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

	// Token: 0x0600806B RID: 32875 RVA: 0x0029BD10 File Offset: 0x00299F10
	private void UpdateProgressText(int currentProgress)
	{
		this.m_currentQuestProgress = currentProgress;
		this.m_ProgressUpdateParticles.Stop();
		this.m_ProgressUpdateParticles.Play();
		this.m_ProgressText.Text = string.Format("{0}/{1}", this.m_currentQuestProgress, this.m_questProgressTotal);
		this.m_questProgressUI.UpdateText(this.m_currentQuestProgress, this.m_questProgressTotal);
	}

	// Token: 0x0600806C RID: 32876 RVA: 0x0029BD7C File Offset: 0x00299F7C
	private void CompleteQuest()
	{
		GameState.Get().SetBusy(false);
		this.m_questCompleted = true;
		this.m_questProgressUI.Hide();
		this.m_spell.ActivateState(SpellStateType.DEATH);
	}

	// Token: 0x0600806D RID: 32877 RVA: 0x0029BDA8 File Offset: 0x00299FA8
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

	// Token: 0x04006952 RID: 26962
	public UberText m_ProgressText;

	// Token: 0x04006953 RID: 26963
	public NestedPrefab m_QuestProgressUIContainer;

	// Token: 0x04006954 RID: 26964
	public string m_QuestUIBoneName = "QuestUI";

	// Token: 0x04006955 RID: 26965
	public float m_ProgressUpdateDelay = 1f;

	// Token: 0x04006956 RID: 26966
	public ParticleSystem m_ProgressUpdateParticles;

	// Token: 0x04006957 RID: 26967
	private Spell m_spell;

	// Token: 0x04006958 RID: 26968
	private Actor m_actor;

	// Token: 0x04006959 RID: 26969
	private Entity m_entity;

	// Token: 0x0400695A RID: 26970
	private QuestProgressUI m_questProgressUI;

	// Token: 0x0400695B RID: 26971
	private bool m_questCompleted;

	// Token: 0x0400695C RID: 26972
	private int m_currentQuestProgress;

	// Token: 0x0400695D RID: 26973
	private int m_questProgressTotal;

	// Token: 0x0400695E RID: 26974
	private int m_targetQuestProgress;

	// Token: 0x0400695F RID: 26975
	private bool m_isScalingDown;
}
