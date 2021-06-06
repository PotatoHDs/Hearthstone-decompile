using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonicProjectPlaySpell : Spell
{
	private class ActorLoadData
	{
		public EntityDef entityDef;

		public Player.Side playerSide;

		public TAG_PREMIUM premium;
	}

	[SerializeField]
	private string m_FriendlyBoneName = "FriendlyJoust";

	[SerializeField]
	private string m_OpponentBoneName = "OpponentJoust";

	[SerializeField]
	private float m_MoveOldCardTime = 1f;

	[SerializeField]
	private float m_ShowNewCardTime = 1f;

	[SerializeField]
	private Spell m_TransformSpell;

	private List<int> m_newEntityIDs = new List<int>();

	private Actor[] m_newActors = new Actor[2];

	private int m_numNewActorsInLoading;

	private int m_numOldActorsInMoving;

	private List<Spell> m_activeSpells = new List<Spell>();

	protected override void OnAction(SpellStateType prevStateType)
	{
		InputManager.Get().DisableInput();
		StartCoroutine(DoEffectWithTiming());
		base.OnAction(prevStateType);
	}

	public override void OnSpellFinished()
	{
		InputManager.Get().EnableInput();
		base.OnSpellFinished();
	}

	private IEnumerator DoEffectWithTiming()
	{
		AddNewEntities();
		yield return StartCoroutine(CompleteTasksBeforeSetAside());
		yield return StartCoroutine(LoadAssets());
		yield return StartCoroutine(MoveOldCards());
		yield return StartCoroutine(PlayTransformFX());
		yield return StartCoroutine(ShowNewCards());
		yield return StartCoroutine(SwitchToRealCards());
		yield return StartCoroutine(WaitAndDeactivate());
	}

	private void AddNewEntities()
	{
		List<PowerTask> taskList = m_taskList.GetTaskList();
		for (int i = 0; i < taskList.Count; i++)
		{
			Network.PowerHistory power = taskList[i].GetPower();
			if (power.Type == Network.PowerType.FULL_ENTITY)
			{
				Network.HistFullEntity histFullEntity = (Network.HistFullEntity)power;
				Network.Entity.Tag tag = histFullEntity.Entity.Tags.Find((Network.Entity.Tag item) => item.Name == 49);
				if (tag != null && tag.Value == 6)
				{
					m_newEntityIDs.Add(histFullEntity.Entity.ID);
				}
			}
		}
	}

	private int FindLastFullEntityTaskIndex()
	{
		List<PowerTask> taskList = m_taskList.GetTaskList();
		for (int num = taskList.Count - 1; num >= 0; num--)
		{
			if (taskList[num].GetPower().Type == Network.PowerType.FULL_ENTITY)
			{
				return num;
			}
		}
		return -1;
	}

	private IEnumerator CompleteTasksBeforeSetAside()
	{
		int num = FindLastFullEntityTaskIndex();
		if (num == -1)
		{
			OnSpellFinished();
			yield break;
		}
		int total = num + 1;
		m_taskList.DoTasks(0, total);
		List<PowerTask> powerTaskList = m_taskList.GetTaskList();
		int i = 0;
		while (i < total)
		{
			PowerTask task = powerTaskList[i];
			while (!task.IsCompleted())
			{
				yield return null;
			}
			int num2 = i + 1;
			i = num2;
		}
	}

	private IEnumerator LoadAssets()
	{
		Entity entity = GetSourceCard().GetEntity();
		LoadActor(GAME_TAG.TAG_SCRIPT_DATA_ENT_1, GAME_TAG.TAG_SCRIPT_DATA_NUM_1, entity.IsControlledByFriendlySidePlayer());
		LoadActor(GAME_TAG.TAG_SCRIPT_DATA_ENT_2, GAME_TAG.TAG_SCRIPT_DATA_NUM_2, !entity.IsControlledByFriendlySidePlayer());
		if (m_numNewActorsInLoading == 0)
		{
			OnSpellFinished();
			yield break;
		}
		while (m_numNewActorsInLoading > 0)
		{
			yield return null;
		}
	}

	private void LoadActor(GAME_TAG tagDataEntity, GAME_TAG tagDataNum, bool friendly)
	{
		Entity entity = GetSourceCard().GetEntity();
		if (entity.HasTag(tagDataNum))
		{
			m_numNewActorsInLoading++;
			Entity entity2 = GameState.Get().GetEntity(entity.GetTag(tagDataEntity));
			int dbId = entity.GetTag(tagDataNum);
			TAG_PREMIUM tAG_PREMIUM = ((entity2.HasTag(GAME_TAG.PREMIUM) || entity.HasTag(GAME_TAG.PREMIUM)) ? TAG_PREMIUM.GOLDEN : TAG_PREMIUM.NORMAL);
			Player.Side playerSide = (friendly ? Player.Side.FRIENDLY : Player.Side.OPPOSING);
			EntityDef entityDef = DefLoader.Get().GetEntityDef(dbId);
			string handActor = ActorNames.GetHandActor(entityDef, tAG_PREMIUM);
			ActorLoadData callbackData = new ActorLoadData
			{
				entityDef = entityDef,
				playerSide = playerSide,
				premium = tAG_PREMIUM
			};
			AssetLoader.Get().InstantiatePrefab(handActor, OnActorLoaded, callbackData, AssetLoadingOptions.IgnorePrefabPosition);
		}
	}

	private void OnActorLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		m_numNewActorsInLoading--;
		Actor component = go.GetComponent<Actor>();
		ActorLoadData actorLoadData = (ActorLoadData)callbackData;
		component.SetEntityDef(actorLoadData.entityDef);
		component.SetPremium(actorLoadData.premium);
		component.SetCardBackSideOverride(actorLoadData.playerSide);
		component.UpdateAllComponents();
		component.Hide();
		m_newActors[(int)(actorLoadData.playerSide - 1)] = component;
	}

	private IEnumerator MoveOldCards()
	{
		MoveOldCard(GAME_TAG.TAG_SCRIPT_DATA_ENT_1);
		MoveOldCard(GAME_TAG.TAG_SCRIPT_DATA_ENT_2);
		if (m_numOldActorsInMoving == 0)
		{
			OnSpellFinished();
			yield break;
		}
		while (m_numOldActorsInMoving > 0)
		{
			yield return null;
		}
	}

	private void MoveOldCard(GAME_TAG tag)
	{
		Entity entity = GetSourceCard().GetEntity();
		if (entity.HasTag(tag))
		{
			m_numOldActorsInMoving++;
			Entity entity2 = GameState.Get().GetEntity(entity.GetTag(tag));
			Card card = entity2.GetCard();
			if (entity2.IsControlledByOpposingSidePlayer())
			{
				string handActor = ActorNames.GetHandActor(entity2);
				card.UpdateActor(forceIfNullZone: false, handActor);
			}
			string text = ((tag != GAME_TAG.TAG_SCRIPT_DATA_ENT_1) ? (entity.IsControlledByFriendlySidePlayer() ? m_OpponentBoneName : m_FriendlyBoneName) : (entity.IsControlledByFriendlySidePlayer() ? m_FriendlyBoneName : m_OpponentBoneName));
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				text += "_phone";
			}
			Transform obj = Board.Get().FindBone(text);
			Vector3 localScale = obj.localScale;
			Vector3 position = obj.position;
			Quaternion rotation = obj.rotation;
			Action<object> action = delegate
			{
				m_numOldActorsInMoving--;
			};
			iTween.MoveTo(card.gameObject, iTween.Hash("position", position, "time", m_MoveOldCardTime, "easetype", iTween.EaseType.easeInOutQuart, "oncomplete", action));
			iTween.RotateTo(card.gameObject, iTween.Hash("rotation", rotation.eulerAngles, "time", m_MoveOldCardTime, "easetype", iTween.EaseType.easeInOutCubic));
			iTween.ScaleTo(card.gameObject, iTween.Hash("scale", localScale, "time", m_MoveOldCardTime, "easetype", iTween.EaseType.easeInOutQuint));
		}
	}

	private IEnumerator PlayTransformFX()
	{
		ActivateTransformSpell(GAME_TAG.TAG_SCRIPT_DATA_ENT_1);
		ActivateTransformSpell(GAME_TAG.TAG_SCRIPT_DATA_ENT_2);
		foreach (Spell spell in m_activeSpells)
		{
			while (!spell.IsFinished())
			{
				yield return null;
			}
		}
	}

	private void ActivateTransformSpell(GAME_TAG tag)
	{
		Entity entity = GetSourceCard().GetEntity();
		if (entity.HasTag(tag))
		{
			Spell spell = UnityEngine.Object.Instantiate(m_TransformSpell);
			Entity entity2 = GameState.Get().GetEntity(entity.GetTag(tag));
			spell.SetSource(entity2.GetCard().gameObject);
			spell.AddStateFinishedCallback(OnSpellStateFinished);
			spell.ActivateState(SpellStateType.ACTION);
			m_activeSpells.Add(spell);
		}
	}

	private void OnSpellStateFinished(Spell spell, SpellStateType prevStateType, object userData)
	{
		if (spell.GetActiveState() == SpellStateType.NONE)
		{
			m_activeSpells.Remove(spell);
			UnityEngine.Object.Destroy(spell);
		}
	}

	private IEnumerator ShowNewCards()
	{
		ShowNewCard(Player.Side.FRIENDLY);
		ShowNewCard(Player.Side.OPPOSING);
		yield return new WaitForSeconds(m_ShowNewCardTime);
	}

	private void ShowNewCard(Player.Side side)
	{
		Actor actor = m_newActors[(int)(side - 1)];
		if (!(actor == null))
		{
			Entity entity = GetSourceCard().GetEntity();
			GAME_TAG enumTag = ((!entity.IsControlledByFriendlySidePlayer()) ? ((side == Player.Side.FRIENDLY) ? GAME_TAG.TAG_SCRIPT_DATA_ENT_2 : GAME_TAG.TAG_SCRIPT_DATA_ENT_1) : ((side == Player.Side.FRIENDLY) ? GAME_TAG.TAG_SCRIPT_DATA_ENT_1 : GAME_TAG.TAG_SCRIPT_DATA_ENT_2));
			Entity entity2 = GameState.Get().GetEntity(entity.GetTag(enumTag));
			TransformUtil.CopyWorld(actor.gameObject, entity2.GetCard().gameObject);
			entity2.GetCard().TransitionToZone(null);
			actor.Show();
		}
	}

	private IEnumerator SwitchToRealCards()
	{
		foreach (int newEntityID in m_newEntityIDs)
		{
			Entity entity = GameState.Get().GetEntity(newEntityID);
			Card card = entity.GetCard();
			card.SetDoNotSort(on: true);
			card.SetDoNotWarpToNewZone(on: true);
			card.TransitionToZone(entity.GetController().GetHandZone());
			int num = ((!entity.IsControlledByFriendlySidePlayer()) ? 1 : 0);
			Actor actor = m_newActors[num];
			while (card.IsActorLoading())
			{
				yield return null;
			}
			actor.Hide();
			TransformUtil.CopyWorld(card.gameObject, actor.gameObject);
			card.SetDoNotSort(on: false);
			card.SetDoNotWarpToNewZone(on: false);
		}
		OnSpellFinished();
	}

	private IEnumerator WaitAndDeactivate()
	{
		while (m_activeSpells.Count > 0)
		{
			yield return null;
		}
		Actor[] newActors = m_newActors;
		for (int i = 0; i < newActors.Length; i++)
		{
			UnityEngine.Object.Destroy(newActors[i]);
		}
		Deactivate();
	}
}
