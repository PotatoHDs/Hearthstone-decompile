using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020007E7 RID: 2023
public class DemonicProjectPlaySpell : Spell
{
	// Token: 0x06006E83 RID: 28291 RVA: 0x0023A368 File Offset: 0x00238568
	protected override void OnAction(SpellStateType prevStateType)
	{
		InputManager.Get().DisableInput();
		base.StartCoroutine(this.DoEffectWithTiming());
		base.OnAction(prevStateType);
	}

	// Token: 0x06006E84 RID: 28292 RVA: 0x0023A388 File Offset: 0x00238588
	public override void OnSpellFinished()
	{
		InputManager.Get().EnableInput();
		base.OnSpellFinished();
	}

	// Token: 0x06006E85 RID: 28293 RVA: 0x0023A39A File Offset: 0x0023859A
	private IEnumerator DoEffectWithTiming()
	{
		this.AddNewEntities();
		yield return base.StartCoroutine(this.CompleteTasksBeforeSetAside());
		yield return base.StartCoroutine(this.LoadAssets());
		yield return base.StartCoroutine(this.MoveOldCards());
		yield return base.StartCoroutine(this.PlayTransformFX());
		yield return base.StartCoroutine(this.ShowNewCards());
		yield return base.StartCoroutine(this.SwitchToRealCards());
		yield return base.StartCoroutine(this.WaitAndDeactivate());
		yield break;
	}

	// Token: 0x06006E86 RID: 28294 RVA: 0x0023A3AC File Offset: 0x002385AC
	private void AddNewEntities()
	{
		List<PowerTask> taskList = this.m_taskList.GetTaskList();
		for (int i = 0; i < taskList.Count; i++)
		{
			Network.PowerHistory power = taskList[i].GetPower();
			if (power.Type == Network.PowerType.FULL_ENTITY)
			{
				Network.HistFullEntity histFullEntity = (Network.HistFullEntity)power;
				Network.Entity.Tag tag = histFullEntity.Entity.Tags.Find((Network.Entity.Tag item) => item.Name == 49);
				if (tag != null && tag.Value == 6)
				{
					this.m_newEntityIDs.Add(histFullEntity.Entity.ID);
				}
			}
		}
	}

	// Token: 0x06006E87 RID: 28295 RVA: 0x0023A448 File Offset: 0x00238648
	private int FindLastFullEntityTaskIndex()
	{
		List<PowerTask> taskList = this.m_taskList.GetTaskList();
		for (int i = taskList.Count - 1; i >= 0; i--)
		{
			if (taskList[i].GetPower().Type == Network.PowerType.FULL_ENTITY)
			{
				return i;
			}
		}
		return -1;
	}

	// Token: 0x06006E88 RID: 28296 RVA: 0x0023A48B File Offset: 0x0023868B
	private IEnumerator CompleteTasksBeforeSetAside()
	{
		int num = this.FindLastFullEntityTaskIndex();
		if (num == -1)
		{
			this.OnSpellFinished();
			yield break;
		}
		int total = num + 1;
		this.m_taskList.DoTasks(0, total);
		List<PowerTask> powerTaskList = this.m_taskList.GetTaskList();
		int num2;
		for (int i = 0; i < total; i = num2)
		{
			PowerTask task = powerTaskList[i];
			while (!task.IsCompleted())
			{
				yield return null;
			}
			task = null;
			num2 = i + 1;
		}
		yield break;
	}

	// Token: 0x06006E89 RID: 28297 RVA: 0x0023A49A File Offset: 0x0023869A
	private IEnumerator LoadAssets()
	{
		Entity entity = base.GetSourceCard().GetEntity();
		this.LoadActor(GAME_TAG.TAG_SCRIPT_DATA_ENT_1, GAME_TAG.TAG_SCRIPT_DATA_NUM_1, entity.IsControlledByFriendlySidePlayer());
		this.LoadActor(GAME_TAG.TAG_SCRIPT_DATA_ENT_2, GAME_TAG.TAG_SCRIPT_DATA_NUM_2, !entity.IsControlledByFriendlySidePlayer());
		if (this.m_numNewActorsInLoading == 0)
		{
			this.OnSpellFinished();
			yield break;
		}
		while (this.m_numNewActorsInLoading > 0)
		{
			yield return null;
		}
		yield break;
	}

	// Token: 0x06006E8A RID: 28298 RVA: 0x0023A4AC File Offset: 0x002386AC
	private void LoadActor(GAME_TAG tagDataEntity, GAME_TAG tagDataNum, bool friendly)
	{
		Entity entity = base.GetSourceCard().GetEntity();
		if (!entity.HasTag(tagDataNum))
		{
			return;
		}
		this.m_numNewActorsInLoading++;
		EntityBase entity2 = GameState.Get().GetEntity(entity.GetTag(tagDataEntity));
		int tag = entity.GetTag(tagDataNum);
		TAG_PREMIUM tag_PREMIUM = (entity2.HasTag(GAME_TAG.PREMIUM) || entity.HasTag(GAME_TAG.PREMIUM)) ? TAG_PREMIUM.GOLDEN : TAG_PREMIUM.NORMAL;
		Player.Side playerSide = friendly ? Player.Side.FRIENDLY : Player.Side.OPPOSING;
		EntityDef entityDef = DefLoader.Get().GetEntityDef(tag, true);
		string handActor = ActorNames.GetHandActor(entityDef, tag_PREMIUM);
		DemonicProjectPlaySpell.ActorLoadData callbackData = new DemonicProjectPlaySpell.ActorLoadData
		{
			entityDef = entityDef,
			playerSide = playerSide,
			premium = tag_PREMIUM
		};
		AssetLoader.Get().InstantiatePrefab(handActor, new PrefabCallback<GameObject>(this.OnActorLoaded), callbackData, AssetLoadingOptions.IgnorePrefabPosition);
	}

	// Token: 0x06006E8B RID: 28299 RVA: 0x0023A56C File Offset: 0x0023876C
	private void OnActorLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		this.m_numNewActorsInLoading--;
		Actor component = go.GetComponent<Actor>();
		DemonicProjectPlaySpell.ActorLoadData actorLoadData = (DemonicProjectPlaySpell.ActorLoadData)callbackData;
		component.SetEntityDef(actorLoadData.entityDef);
		component.SetPremium(actorLoadData.premium);
		component.SetCardBackSideOverride(new Player.Side?(actorLoadData.playerSide));
		component.UpdateAllComponents();
		component.Hide();
		this.m_newActors[actorLoadData.playerSide - Player.Side.FRIENDLY] = component;
	}

	// Token: 0x06006E8C RID: 28300 RVA: 0x0023A5DA File Offset: 0x002387DA
	private IEnumerator MoveOldCards()
	{
		this.MoveOldCard(GAME_TAG.TAG_SCRIPT_DATA_ENT_1);
		this.MoveOldCard(GAME_TAG.TAG_SCRIPT_DATA_ENT_2);
		if (this.m_numOldActorsInMoving == 0)
		{
			this.OnSpellFinished();
			yield break;
		}
		while (this.m_numOldActorsInMoving > 0)
		{
			yield return null;
		}
		yield break;
	}

	// Token: 0x06006E8D RID: 28301 RVA: 0x0023A5EC File Offset: 0x002387EC
	private void MoveOldCard(GAME_TAG tag)
	{
		Entity entity = base.GetSourceCard().GetEntity();
		if (!entity.HasTag(tag))
		{
			return;
		}
		this.m_numOldActorsInMoving++;
		Entity entity2 = GameState.Get().GetEntity(entity.GetTag(tag));
		Card card = entity2.GetCard();
		if (entity2.IsControlledByOpposingSidePlayer())
		{
			string handActor = ActorNames.GetHandActor(entity2);
			card.UpdateActor(false, handActor);
		}
		string text;
		if (tag == GAME_TAG.TAG_SCRIPT_DATA_ENT_1)
		{
			text = (entity.IsControlledByFriendlySidePlayer() ? this.m_FriendlyBoneName : this.m_OpponentBoneName);
		}
		else
		{
			text = (entity.IsControlledByFriendlySidePlayer() ? this.m_OpponentBoneName : this.m_FriendlyBoneName);
		}
		if (UniversalInputManager.UsePhoneUI)
		{
			text += "_phone";
		}
		Transform transform = Board.Get().FindBone(text);
		Vector3 localScale = transform.localScale;
		Vector3 position = transform.position;
		Quaternion rotation = transform.rotation;
		Action<object> action = delegate(object tweenUserData)
		{
			this.m_numOldActorsInMoving--;
		};
		iTween.MoveTo(card.gameObject, iTween.Hash(new object[]
		{
			"position",
			position,
			"time",
			this.m_MoveOldCardTime,
			"easetype",
			iTween.EaseType.easeInOutQuart,
			"oncomplete",
			action
		}));
		iTween.RotateTo(card.gameObject, iTween.Hash(new object[]
		{
			"rotation",
			rotation.eulerAngles,
			"time",
			this.m_MoveOldCardTime,
			"easetype",
			iTween.EaseType.easeInOutCubic
		}));
		iTween.ScaleTo(card.gameObject, iTween.Hash(new object[]
		{
			"scale",
			localScale,
			"time",
			this.m_MoveOldCardTime,
			"easetype",
			iTween.EaseType.easeInOutQuint
		}));
	}

	// Token: 0x06006E8E RID: 28302 RVA: 0x0023A7CB File Offset: 0x002389CB
	private IEnumerator PlayTransformFX()
	{
		this.ActivateTransformSpell(GAME_TAG.TAG_SCRIPT_DATA_ENT_1);
		this.ActivateTransformSpell(GAME_TAG.TAG_SCRIPT_DATA_ENT_2);
		foreach (Spell spell in this.m_activeSpells)
		{
			while (!spell.IsFinished())
			{
				yield return null;
			}
			spell = null;
		}
		List<Spell>.Enumerator enumerator = default(List<Spell>.Enumerator);
		yield break;
		yield break;
	}

	// Token: 0x06006E8F RID: 28303 RVA: 0x0023A7DC File Offset: 0x002389DC
	private void ActivateTransformSpell(GAME_TAG tag)
	{
		Entity entity = base.GetSourceCard().GetEntity();
		if (entity.HasTag(tag))
		{
			Spell spell = UnityEngine.Object.Instantiate<Spell>(this.m_TransformSpell);
			Entity entity2 = GameState.Get().GetEntity(entity.GetTag(tag));
			spell.SetSource(entity2.GetCard().gameObject);
			spell.AddStateFinishedCallback(new Spell.StateFinishedCallback(this.OnSpellStateFinished));
			spell.ActivateState(SpellStateType.ACTION);
			this.m_activeSpells.Add(spell);
		}
	}

	// Token: 0x06006E90 RID: 28304 RVA: 0x0023A852 File Offset: 0x00238A52
	private void OnSpellStateFinished(Spell spell, SpellStateType prevStateType, object userData)
	{
		if (spell.GetActiveState() == SpellStateType.NONE)
		{
			this.m_activeSpells.Remove(spell);
			UnityEngine.Object.Destroy(spell);
		}
	}

	// Token: 0x06006E91 RID: 28305 RVA: 0x0023A86F File Offset: 0x00238A6F
	private IEnumerator ShowNewCards()
	{
		this.ShowNewCard(Player.Side.FRIENDLY);
		this.ShowNewCard(Player.Side.OPPOSING);
		yield return new WaitForSeconds(this.m_ShowNewCardTime);
		yield break;
	}

	// Token: 0x06006E92 RID: 28306 RVA: 0x0023A880 File Offset: 0x00238A80
	private void ShowNewCard(Player.Side side)
	{
		Actor actor = this.m_newActors[side - Player.Side.FRIENDLY];
		if (actor == null)
		{
			return;
		}
		Entity entity = base.GetSourceCard().GetEntity();
		GAME_TAG enumTag;
		if (entity.IsControlledByFriendlySidePlayer())
		{
			enumTag = ((side == Player.Side.FRIENDLY) ? GAME_TAG.TAG_SCRIPT_DATA_ENT_1 : GAME_TAG.TAG_SCRIPT_DATA_ENT_2);
		}
		else
		{
			enumTag = ((side == Player.Side.FRIENDLY) ? GAME_TAG.TAG_SCRIPT_DATA_ENT_2 : GAME_TAG.TAG_SCRIPT_DATA_ENT_1);
		}
		Entity entity2 = GameState.Get().GetEntity(entity.GetTag(enumTag));
		TransformUtil.CopyWorld(actor.gameObject, entity2.GetCard().gameObject);
		entity2.GetCard().TransitionToZone(null, null);
		actor.Show();
	}

	// Token: 0x06006E93 RID: 28307 RVA: 0x0023A905 File Offset: 0x00238B05
	private IEnumerator SwitchToRealCards()
	{
		foreach (int id in this.m_newEntityIDs)
		{
			Entity entity = GameState.Get().GetEntity(id);
			Card card = entity.GetCard();
			card.SetDoNotSort(true);
			card.SetDoNotWarpToNewZone(true);
			card.TransitionToZone(entity.GetController().GetHandZone(), null);
			int num = entity.IsControlledByFriendlySidePlayer() ? 0 : 1;
			Actor actor = this.m_newActors[num];
			while (card.IsActorLoading())
			{
				yield return null;
			}
			actor.Hide();
			TransformUtil.CopyWorld(card.gameObject, actor.gameObject);
			card.SetDoNotSort(false);
			card.SetDoNotWarpToNewZone(false);
			card = null;
			actor = null;
		}
		List<int>.Enumerator enumerator = default(List<int>.Enumerator);
		this.OnSpellFinished();
		yield break;
		yield break;
	}

	// Token: 0x06006E94 RID: 28308 RVA: 0x0023A914 File Offset: 0x00238B14
	private IEnumerator WaitAndDeactivate()
	{
		while (this.m_activeSpells.Count > 0)
		{
			yield return null;
		}
		Actor[] newActors = this.m_newActors;
		for (int i = 0; i < newActors.Length; i++)
		{
			UnityEngine.Object.Destroy(newActors[i]);
		}
		base.Deactivate();
		yield break;
	}

	// Token: 0x040058B4 RID: 22708
	[SerializeField]
	private string m_FriendlyBoneName = "FriendlyJoust";

	// Token: 0x040058B5 RID: 22709
	[SerializeField]
	private string m_OpponentBoneName = "OpponentJoust";

	// Token: 0x040058B6 RID: 22710
	[SerializeField]
	private float m_MoveOldCardTime = 1f;

	// Token: 0x040058B7 RID: 22711
	[SerializeField]
	private float m_ShowNewCardTime = 1f;

	// Token: 0x040058B8 RID: 22712
	[SerializeField]
	private Spell m_TransformSpell;

	// Token: 0x040058B9 RID: 22713
	private List<int> m_newEntityIDs = new List<int>();

	// Token: 0x040058BA RID: 22714
	private Actor[] m_newActors = new Actor[2];

	// Token: 0x040058BB RID: 22715
	private int m_numNewActorsInLoading;

	// Token: 0x040058BC RID: 22716
	private int m_numOldActorsInMoving;

	// Token: 0x040058BD RID: 22717
	private List<Spell> m_activeSpells = new List<Spell>();

	// Token: 0x02002389 RID: 9097
	private class ActorLoadData
	{
		// Token: 0x0400E719 RID: 59161
		public EntityDef entityDef;

		// Token: 0x0400E71A RID: 59162
		public Player.Side playerSide;

		// Token: 0x0400E71B RID: 59163
		public TAG_PREMIUM premium;
	}
}
