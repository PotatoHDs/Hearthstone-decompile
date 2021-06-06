using System;
using System.Collections;
using System.Collections.Generic;
using Blizzard.T5.Core;
using UnityEngine;

[CustomEditClass]
public class PlaySpellCardFromDeckSepll : Spell
{
	private class RevealedCard
	{
		public Player m_player;

		public Card m_card;

		public int m_deckIndex;

		public Actor m_initialActor;

		public Actor m_revealedActor;

		public int m_effectsPendingFinish;
	}

	public float m_ShowTime = 1.2f;

	public float m_RandomSecMin = 0.1f;

	public float m_RandomSecMax = 0.25f;

	public float m_DriftCycleTime = 10f;

	public float m_RevealTime = 0.5f;

	public float m_HoldTime = 1.2f;

	public iTween.EaseType m_RevealEaseType = iTween.EaseType.easeOutBack;

	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_DrawStingerPrefab;

	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_ShowSoundPrefab;

	private RevealedCard m_revealedCard;

	public override bool AddPowerTargets()
	{
		foreach (PowerTask task in m_taskList.GetTaskList())
		{
			Network.PowerHistory power = task.GetPower();
			if (power.Type == Network.PowerType.FULL_ENTITY)
			{
				Network.HistFullEntity histFullEntity = power as Network.HistFullEntity;
				Entity entity = GameState.Get().GetEntity(histFullEntity.Entity.ID);
				AddTarget(entity.GetCard().gameObject);
				return true;
			}
		}
		return false;
	}

	protected override void OnAction(SpellStateType prevStateType)
	{
		StartCoroutine(DoEffectWithTiming());
		base.OnAction(prevStateType);
	}

	private IEnumerator DoEffectWithTiming()
	{
		yield return StartCoroutine(CompleteTasksUntilShowTargetEntity());
		CreateRevealedCardActors();
		if (m_revealedCard != null)
		{
			yield return StartCoroutine(PullRevealedCardFromDeck());
			yield return StartCoroutine(FlipRevealedCard());
			yield return StartCoroutine(PlayRevealedCard());
			DestroyRevealedCard();
			OnSpellFinished();
		}
		else
		{
			Log.Gameplay.PrintError("{0}.DoEffectWithTiming() - Failed to find revealed card", this);
		}
	}

	private int FindTaskCountToRun()
	{
		Entity entity = GetTargetCard().GetEntity();
		int num = 0;
		foreach (PowerTask task in m_taskList.GetTaskList())
		{
			num++;
			Network.PowerHistory power = task.GetPower();
			if (power.Type == Network.PowerType.SHOW_ENTITY && ((Network.HistShowEntity)power).Entity.ID == entity.GetEntityId())
			{
				return num;
			}
		}
		num = 0;
		foreach (PowerTask task2 in m_taskList.GetTaskList())
		{
			num++;
			Network.PowerHistory power2 = task2.GetPower();
			if (power2.Type == Network.PowerType.TAG_CHANGE)
			{
				Network.HistTagChange histTagChange = (Network.HistTagChange)power2;
				if (histTagChange.Entity == entity.GetEntityId() && histTagChange.Tag == 49)
				{
					return --num;
				}
			}
		}
		Log.Gameplay.PrintError("{0}.FindTaskCountToRun() - Failed to find tasks to run.", this);
		return 0;
	}

	private IEnumerator CompleteTasksUntilShowTargetEntity()
	{
		int num = FindTaskCountToRun();
		if (num > 0)
		{
			bool complete = false;
			m_taskList.DoTasks(0, num, delegate
			{
				complete = true;
			});
			while (!complete)
			{
				yield return null;
			}
		}
	}

	private void CreateRevealedCardActors()
	{
		Card card = GetTargetCard();
		Entity entity = card.GetEntity();
		card.SetInputEnabled(enabled: false);
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab("Card_Hidden.prefab:1a94649d257bc284ca6e2962f634a8b9", AssetLoadingOptions.IgnorePrefabPosition);
		if (gameObject == null)
		{
			Log.Gameplay.PrintError("{0}.CreateRevealedCardActors() - Failed to load HIDDEN actor.", this);
			return;
		}
		string empty = string.Empty;
		empty = ((!entity.IsControlledByOpposingSidePlayer() || !entity.IsSecret()) ? ActorNames.GetHandActor(entity) : ActorNames.GetHistorySecretActor(entity));
		GameObject gameObject2 = AssetLoader.Get().InstantiatePrefab(empty, AssetLoadingOptions.IgnorePrefabPosition);
		if (gameObject2 == null)
		{
			Log.Gameplay.PrintError("{0}.CreateRevealedCardActors() - Failed to load HAND actor.", this);
			return;
		}
		m_revealedCard = new RevealedCard();
		m_revealedCard.m_player = entity.GetController();
		m_revealedCard.m_card = card;
		m_revealedCard.m_initialActor = gameObject.GetComponent<Actor>();
		m_revealedCard.m_revealedActor = gameObject2.GetComponent<Actor>();
		Action<Actor> obj = delegate(Actor actor)
		{
			actor.SetEntity(entity);
			actor.SetCard(card);
			actor.SetCardDefFromCard(card);
			actor.UpdateAllComponents();
			actor.Hide();
		};
		obj(m_revealedCard.m_initialActor);
		obj(m_revealedCard.m_revealedActor);
	}

	private IEnumerator PullRevealedCardFromDeck()
	{
		if (!string.IsNullOrEmpty(m_DrawStingerPrefab))
		{
			SoundManager.Get().LoadAndPlay(m_DrawStingerPrefab);
		}
		string bigCardBoneName = HistoryManager.Get().GetBigCardBoneName();
		Transform obj = Board.Get().FindBone(bigCardBoneName);
		Vector3 localScale = obj.localScale;
		Vector3 position = obj.position;
		Quaternion rotation = obj.rotation;
		float randomSec = GetRandomSec();
		float showSec = m_ShowTime + GetRandomSec();
		PullRevealedCardFromDeckAnim(localScale, rotation, position, randomSec, showSec);
		while (IsRevealedCardBusy())
		{
			yield return null;
		}
	}

	private void PullRevealedCardFromDeckAnim(Vector3 localScale, Quaternion rotation, Vector3 position, float delaySec, float showSec)
	{
		m_revealedCard.m_effectsPendingFinish++;
		Card card = m_revealedCard.m_card;
		ZoneDeck deckZone = m_revealedCard.m_player.GetDeckZone();
		Actor thicknessForLayout = deckZone.GetThicknessForLayout();
		m_revealedCard.m_deckIndex = deckZone.RemoveCard(card);
		deckZone.SetSuppressEmotes(suppress: true);
		deckZone.UpdateLayout();
		float num = 0.5f * showSec;
		Vector3 vector = thicknessForLayout.GetMeshRenderer().bounds.center + Card.IN_DECK_OFFSET;
		Vector3 vector2 = vector + Card.ABOVE_DECK_OFFSET;
		Vector3 eulerAngles = rotation.eulerAngles;
		Vector3[] array = new Vector3[3] { vector, vector2, position };
		card.ShowCard();
		m_revealedCard.m_initialActor.Show();
		card.transform.position = vector;
		card.transform.rotation = Card.IN_DECK_HIDDEN_ROTATION;
		card.transform.localScale = Card.IN_DECK_SCALE;
		iTween.MoveTo(card.gameObject, iTween.Hash("path", array, "delay", delaySec, "time", showSec, "easetype", iTween.EaseType.easeInOutQuart));
		iTween.RotateTo(card.gameObject, iTween.Hash("rotation", eulerAngles, "delay", delaySec + num, "time", num, "easetype", iTween.EaseType.easeInOutCubic));
		iTween.ScaleTo(card.gameObject, iTween.Hash("scale", localScale, "delay", delaySec + num, "time", num, "easetype", iTween.EaseType.easeInOutQuint));
		if (!string.IsNullOrEmpty(m_ShowSoundPrefab))
		{
			SoundManager.Get().LoadAndPlay(m_ShowSoundPrefab);
		}
		Action<object> action = delegate
		{
			m_revealedCard.m_effectsPendingFinish--;
			DriftRevealedCard();
		};
		iTween.Timer(card.gameObject, iTween.Hash("delay", delaySec, "time", showSec, "oncomplete", action));
	}

	private void DriftRevealedCard()
	{
		Card card = m_revealedCard.m_card;
		Vector3 position = card.transform.position;
		float z = m_revealedCard.m_initialActor.GetMeshRenderer().bounds.size.z;
		float num = 0.02f * z;
		Vector3 vector = GeneralUtils.RandomSign() * num * card.transform.up;
		Vector3 vector2 = -vector;
		Vector3 vector3 = GeneralUtils.RandomSign() * num * card.transform.right;
		Vector3 vector4 = -vector3;
		List<Vector3> list = new List<Vector3>();
		list.Add(position + vector + vector3);
		list.Add(position + vector2 + vector3);
		list.Add(position);
		list.Add(position + vector + vector4);
		list.Add(position + vector2 + vector4);
		list.Add(position);
		float num2 = m_DriftCycleTime + GetRandomSec();
		Hashtable args = iTween.Hash("path", list.ToArray(), "time", num2, "easetype", iTween.EaseType.linear, "looptype", iTween.LoopType.loop);
		iTween.MoveTo(card.gameObject, args);
	}

	private IEnumerator FlipRevealedCard()
	{
		float revealSec = m_RevealTime + GetRandomSec();
		FlipRevealedCardAnim(revealSec);
		while (IsRevealedCardBusy())
		{
			yield return null;
		}
		iTween.Timer(base.gameObject, iTween.Hash("time", m_HoldTime));
		while (iTween.HasTween(base.gameObject))
		{
			yield return null;
		}
	}

	private void FlipRevealedCardAnim(float revealSec)
	{
		m_revealedCard.m_effectsPendingFinish++;
		Card card = m_revealedCard.m_card;
		Actor hiddenActor = m_revealedCard.m_initialActor;
		Actor revealedActor = m_revealedCard.m_revealedActor;
		TransformUtil.SetEulerAngleZ(revealedActor.gameObject, -180f);
		iTween.RotateAdd(hiddenActor.gameObject, iTween.Hash("z", 180f, "time", revealSec, "easetype", m_RevealEaseType));
		iTween.RotateAdd(revealedActor.gameObject, iTween.Hash("z", 180f, "time", revealSec, "easetype", m_RevealEaseType));
		float startAngleZ = revealedActor.transform.rotation.eulerAngles.z;
		Action<object> action = delegate
		{
			float z = revealedActor.transform.rotation.eulerAngles.z;
			if (Mathf.DeltaAngle(startAngleZ, z) >= 90f)
			{
				revealedActor.Show();
				hiddenActor.Hide();
			}
		};
		Action<object> action2 = delegate
		{
			revealedActor.Show();
			hiddenActor.Hide();
			m_revealedCard.m_effectsPendingFinish--;
		};
		iTween.Timer(card.gameObject, iTween.Hash("time", revealSec, "onupdate", action, "oncomplete", action2));
	}

	private IEnumerator PlayRevealedCard()
	{
		Spell powerUpSpell = m_revealedCard.m_revealedActor.GetSpell(SpellType.POWER_UP);
		if (powerUpSpell == null)
		{
			yield break;
		}
		powerUpSpell.AddStateFinishedCallback(delegate(Spell spell, SpellStateType prevStateType, object userData)
		{
			if (prevStateType == SpellStateType.BIRTH)
			{
				m_revealedCard.m_effectsPendingFinish--;
			}
		});
		m_revealedCard.m_effectsPendingFinish++;
		powerUpSpell.ActivateState(SpellStateType.BIRTH);
		while (IsRevealedCardBusy())
		{
			yield return null;
		}
		powerUpSpell.Deactivate();
	}

	private void DestroyRevealedCard()
	{
		m_revealedCard.m_card.SetInputEnabled(enabled: true);
		m_revealedCard.m_initialActor.Destroy();
		m_revealedCard.m_revealedActor.Destroy();
		m_revealedCard = null;
	}

	private float GetRandomSec()
	{
		return UnityEngine.Random.Range(m_RandomSecMin, m_RandomSecMax);
	}

	private bool IsRevealedCardBusy()
	{
		if (m_revealedCard == null)
		{
			return false;
		}
		return m_revealedCard.m_effectsPendingFinish > 0;
	}
}
