using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020006C6 RID: 1734
public class FatigueSpellController : SpellController
{
	// Token: 0x0600612D RID: 24877 RVA: 0x001FB104 File Offset: 0x001F9304
	protected override bool AddPowerSourceAndTargets(PowerTaskList taskList)
	{
		if (!this.HasSourceCard(taskList))
		{
			return false;
		}
		this.m_fatigueTagChange = null;
		List<PowerTask> taskList2 = this.m_taskList.GetTaskList();
		for (int i = 0; i < taskList2.Count; i++)
		{
			Network.PowerHistory power = taskList2[i].GetPower();
			if (power.Type == Network.PowerType.TAG_CHANGE)
			{
				Network.HistTagChange histTagChange = (Network.HistTagChange)power;
				if (histTagChange.Tag == 22)
				{
					this.m_fatigueTagChange = histTagChange;
				}
			}
		}
		if (this.m_fatigueTagChange == null)
		{
			return false;
		}
		Card card = taskList.GetSourceEntity(true).GetCard();
		base.SetSource(card);
		return true;
	}

	// Token: 0x0600612E RID: 24878 RVA: 0x001FB190 File Offset: 0x001F9390
	protected override void OnProcessTaskList()
	{
		AssetLoader.Get().InstantiatePrefab("Card_Hand_Fatigue.prefab:ae394ca0bb29a964eb4c7eeb555f2fae", new PrefabCallback<GameObject>(this.OnFatigueActorLoaded), null, AssetLoadingOptions.IgnorePrefabPosition);
	}

	// Token: 0x0600612F RID: 24879 RVA: 0x001FB1B8 File Offset: 0x001F93B8
	private void OnFatigueActorLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogWarning(string.Format("FatigueSpellController.OnFatigueActorLoaded() - FAILED to load actor \"{0}\"", assetRef));
			this.DoFinishFatigue();
			return;
		}
		Actor component = go.GetComponent<Actor>();
		if (component == null)
		{
			Debug.LogWarning(string.Format("FatigueSpellController.OnFatigueActorLoaded() - ERROR actor \"{0}\" has no Actor component", assetRef));
			this.DoFinishFatigue();
			return;
		}
		Player controller = base.GetSource().GetController();
		Player.Side controllerSide = controller.GetControllerSide();
		bool flag = controllerSide == Player.Side.FRIENDLY;
		this.m_fatigueActor = component;
		UberText nameText = this.m_fatigueActor.GetNameText();
		if (nameText != null)
		{
			nameText.Text = GameStrings.Get("GAMEPLAY_FATIGUE_TITLE");
		}
		int num = this.m_fatigueTagChange.Value;
		if (controller.HasTag(GAME_TAG.DOUBLE_FATIGUE_DAMAGE))
		{
			num *= (int)Mathf.Pow(2f, (float)controller.GetTag(GAME_TAG.DOUBLE_FATIGUE_DAMAGE));
		}
		UberText powersText = this.m_fatigueActor.GetPowersText();
		if (powersText != null)
		{
			powersText.Text = GameStrings.Format("GAMEPLAY_FATIGUE_TEXT", new object[]
			{
				num
			});
		}
		component.SetCardBackSideOverride(new Player.Side?(controllerSide));
		component.UpdateCardBack();
		ZoneDeck zoneDeck = flag ? GameState.Get().GetFriendlySidePlayer().GetDeckZone() : GameState.Get().GetOpposingSidePlayer().GetDeckZone();
		zoneDeck.DoFatigueGlow();
		this.m_fatigueActor.transform.localEulerAngles = FatigueSpellController.FATIGUE_ACTOR_INITIAL_LOCAL_ROTATION;
		this.m_fatigueActor.transform.localScale = FatigueSpellController.FATIGUE_ACTOR_START_SCALE;
		this.m_fatigueActor.transform.position = zoneDeck.transform.position;
		Vector3[] array = new Vector3[]
		{
			this.m_fatigueActor.transform.position,
			new Vector3(this.m_fatigueActor.transform.position.x, this.m_fatigueActor.transform.position.y + 3.6f, this.m_fatigueActor.transform.position.z),
			Board.Get().FindBone("FatigueCardBone").position
		};
		iTween.MoveTo(this.m_fatigueActor.gameObject, iTween.Hash(new object[]
		{
			"path",
			array,
			"time",
			1.2f,
			"easetype",
			iTween.EaseType.easeInSineOutExpo
		}));
		iTween.RotateTo(this.m_fatigueActor.gameObject, iTween.Hash(new object[]
		{
			"rotation",
			FatigueSpellController.FATIGUE_ACTOR_FINAL_LOCAL_ROTATION,
			"time",
			1.2f,
			"delay",
			0.15f
		}));
		iTween.ScaleTo(this.m_fatigueActor.gameObject, FatigueSpellController.FATIGUE_ACTOR_FINAL_SCALE, 1f);
		base.StartCoroutine(this.WaitThenFinishFatigue(0.8f));
	}

	// Token: 0x06006130 RID: 24880 RVA: 0x001FB4A8 File Offset: 0x001F96A8
	private IEnumerator WaitThenFinishFatigue(float seconds)
	{
		yield return new WaitForSeconds(seconds);
		this.DoFinishFatigue();
		yield break;
	}

	// Token: 0x06006131 RID: 24881 RVA: 0x001FB4BE File Offset: 0x001F96BE
	private void DoFinishFatigue()
	{
		Spell spell = base.GetSource().GetActor().GetSpell(SpellType.FATIGUE_DEATH);
		spell.AddFinishedCallback(new Spell.FinishedCallback(this.OnFatigueDamageFinished));
		spell.ActivateState(SpellStateType.BIRTH);
	}

	// Token: 0x06006132 RID: 24882 RVA: 0x001FB4EC File Offset: 0x001F96EC
	private void OnFatigueDamageFinished(Spell spell, object userData)
	{
		spell.RemoveFinishedCallback(new Spell.FinishedCallback(this.OnFatigueDamageFinished));
		if (this.m_fatigueActor == null)
		{
			this.OnFinishedTaskList();
			return;
		}
		Spell spell2 = this.m_fatigueActor.GetSpell(SpellType.DEATH);
		if (spell2 == null)
		{
			this.OnFinishedTaskList();
			return;
		}
		Actor fatigueActor = this.m_fatigueActor;
		this.m_fatigueActor = null;
		spell2.AddStateFinishedCallback(new Spell.StateFinishedCallback(this.OnFatigueDeathSpellFinished), fatigueActor);
		spell2.Activate();
		this.OnFinishedTaskList();
	}

	// Token: 0x06006133 RID: 24883 RVA: 0x001FB56C File Offset: 0x001F976C
	private void OnFatigueDeathSpellFinished(Spell spell, SpellStateType prevStateType, object userData)
	{
		Actor actor = (Actor)userData;
		if (actor != null)
		{
			actor.Destroy();
		}
		this.OnFinished();
	}

	// Token: 0x04005110 RID: 20752
	private const float FATIGUE_DRAW_ANIM_TIME = 1.2f;

	// Token: 0x04005111 RID: 20753
	private const float FATIGUE_DRAW_SCALE_TIME = 1f;

	// Token: 0x04005112 RID: 20754
	private static readonly Vector3 FATIGUE_ACTOR_START_SCALE = new Vector3(0.88f, 0.88f, 0.88f);

	// Token: 0x04005113 RID: 20755
	private static readonly Vector3 FATIGUE_ACTOR_FINAL_SCALE = Vector3.one;

	// Token: 0x04005114 RID: 20756
	private static readonly Vector3 FATIGUE_ACTOR_INITIAL_LOCAL_ROTATION = new Vector3(270f, 270f, 0f);

	// Token: 0x04005115 RID: 20757
	private static readonly Vector3 FATIGUE_ACTOR_FINAL_LOCAL_ROTATION = Vector3.zero;

	// Token: 0x04005116 RID: 20758
	private const float FATIGUE_HOLD_TIME = 0.8f;

	// Token: 0x04005117 RID: 20759
	private Network.HistTagChange m_fatigueTagChange;

	// Token: 0x04005118 RID: 20760
	private Actor m_fatigueActor;
}
