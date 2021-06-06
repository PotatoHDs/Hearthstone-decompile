using System;
using System.Collections;
using System.Collections.Generic;
using Blizzard.T5.Core;
using UnityEngine;

// Token: 0x02000815 RID: 2069
[CustomEditClass]
public class PlaySpellCardFromDeckSepll : Spell
{
	// Token: 0x06006FA0 RID: 28576 RVA: 0x0023FF84 File Offset: 0x0023E184
	public override bool AddPowerTargets()
	{
		foreach (PowerTask powerTask in this.m_taskList.GetTaskList())
		{
			Network.PowerHistory power = powerTask.GetPower();
			if (power.Type == Network.PowerType.FULL_ENTITY)
			{
				Network.HistFullEntity histFullEntity = power as Network.HistFullEntity;
				Entity entity = GameState.Get().GetEntity(histFullEntity.Entity.ID);
				this.AddTarget(entity.GetCard().gameObject);
				return true;
			}
		}
		return false;
	}

	// Token: 0x06006FA1 RID: 28577 RVA: 0x0024001C File Offset: 0x0023E21C
	protected override void OnAction(SpellStateType prevStateType)
	{
		base.StartCoroutine(this.DoEffectWithTiming());
		base.OnAction(prevStateType);
	}

	// Token: 0x06006FA2 RID: 28578 RVA: 0x00240032 File Offset: 0x0023E232
	private IEnumerator DoEffectWithTiming()
	{
		yield return base.StartCoroutine(this.CompleteTasksUntilShowTargetEntity());
		this.CreateRevealedCardActors();
		if (this.m_revealedCard != null)
		{
			yield return base.StartCoroutine(this.PullRevealedCardFromDeck());
			yield return base.StartCoroutine(this.FlipRevealedCard());
			yield return base.StartCoroutine(this.PlayRevealedCard());
			this.DestroyRevealedCard();
			this.OnSpellFinished();
		}
		else
		{
			Log.Gameplay.PrintError("{0}.DoEffectWithTiming() - Failed to find revealed card", new object[]
			{
				this
			});
		}
		yield break;
	}

	// Token: 0x06006FA3 RID: 28579 RVA: 0x00240044 File Offset: 0x0023E244
	private int FindTaskCountToRun()
	{
		Entity entity = base.GetTargetCard().GetEntity();
		int num = 0;
		foreach (PowerTask powerTask in this.m_taskList.GetTaskList())
		{
			num++;
			Network.PowerHistory power = powerTask.GetPower();
			if (power.Type == Network.PowerType.SHOW_ENTITY && ((Network.HistShowEntity)power).Entity.ID == entity.GetEntityId())
			{
				return num;
			}
		}
		num = 0;
		foreach (PowerTask powerTask2 in this.m_taskList.GetTaskList())
		{
			num++;
			Network.PowerHistory power2 = powerTask2.GetPower();
			if (power2.Type == Network.PowerType.TAG_CHANGE)
			{
				Network.HistTagChange histTagChange = (Network.HistTagChange)power2;
				if (histTagChange.Entity == entity.GetEntityId() && histTagChange.Tag == 49)
				{
					return num - 1;
				}
			}
		}
		Log.Gameplay.PrintError("{0}.FindTaskCountToRun() - Failed to find tasks to run.", new object[]
		{
			this
		});
		return 0;
	}

	// Token: 0x06006FA4 RID: 28580 RVA: 0x00240174 File Offset: 0x0023E374
	private IEnumerator CompleteTasksUntilShowTargetEntity()
	{
		int num = this.FindTaskCountToRun();
		if (num <= 0)
		{
			yield break;
		}
		bool complete = false;
		this.m_taskList.DoTasks(0, num, delegate(PowerTaskList taskList, int startIndex, int count, object userData)
		{
			complete = true;
		});
		while (!complete)
		{
			yield return null;
		}
		yield break;
	}

	// Token: 0x06006FA5 RID: 28581 RVA: 0x00240184 File Offset: 0x0023E384
	private void CreateRevealedCardActors()
	{
		Card card = base.GetTargetCard();
		Entity entity = card.GetEntity();
		card.SetInputEnabled(false);
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab("Card_Hidden.prefab:1a94649d257bc284ca6e2962f634a8b9", AssetLoadingOptions.IgnorePrefabPosition);
		if (gameObject == null)
		{
			Log.Gameplay.PrintError("{0}.CreateRevealedCardActors() - Failed to load HIDDEN actor.", new object[]
			{
				this
			});
			return;
		}
		string input = string.Empty;
		if (entity.IsControlledByOpposingSidePlayer() && entity.IsSecret())
		{
			input = ActorNames.GetHistorySecretActor(entity);
		}
		else
		{
			input = ActorNames.GetHandActor(entity);
		}
		GameObject gameObject2 = AssetLoader.Get().InstantiatePrefab(input, AssetLoadingOptions.IgnorePrefabPosition);
		if (gameObject2 == null)
		{
			Log.Gameplay.PrintError("{0}.CreateRevealedCardActors() - Failed to load HAND actor.", new object[]
			{
				this
			});
			return;
		}
		this.m_revealedCard = new PlaySpellCardFromDeckSepll.RevealedCard();
		this.m_revealedCard.m_player = entity.GetController();
		this.m_revealedCard.m_card = card;
		this.m_revealedCard.m_initialActor = gameObject.GetComponent<Actor>();
		this.m_revealedCard.m_revealedActor = gameObject2.GetComponent<Actor>();
		Action<Actor> action = delegate(Actor actor)
		{
			actor.SetEntity(entity);
			actor.SetCard(card);
			actor.SetCardDefFromCard(card);
			actor.UpdateAllComponents();
			actor.Hide();
		};
		action(this.m_revealedCard.m_initialActor);
		action(this.m_revealedCard.m_revealedActor);
	}

	// Token: 0x06006FA6 RID: 28582 RVA: 0x002402E9 File Offset: 0x0023E4E9
	private IEnumerator PullRevealedCardFromDeck()
	{
		if (!string.IsNullOrEmpty(this.m_DrawStingerPrefab))
		{
			SoundManager.Get().LoadAndPlay(this.m_DrawStingerPrefab);
		}
		string bigCardBoneName = HistoryManager.Get().GetBigCardBoneName();
		Transform transform = Board.Get().FindBone(bigCardBoneName);
		Vector3 localScale = transform.localScale;
		Vector3 position = transform.position;
		Quaternion rotation = transform.rotation;
		float randomSec = this.GetRandomSec();
		float showSec = this.m_ShowTime + this.GetRandomSec();
		this.PullRevealedCardFromDeckAnim(localScale, rotation, position, randomSec, showSec);
		while (this.IsRevealedCardBusy())
		{
			yield return null;
		}
		yield break;
	}

	// Token: 0x06006FA7 RID: 28583 RVA: 0x002402F8 File Offset: 0x0023E4F8
	private void PullRevealedCardFromDeckAnim(Vector3 localScale, Quaternion rotation, Vector3 position, float delaySec, float showSec)
	{
		this.m_revealedCard.m_effectsPendingFinish++;
		Card card = this.m_revealedCard.m_card;
		ZoneDeck deckZone = this.m_revealedCard.m_player.GetDeckZone();
		Actor thicknessForLayout = deckZone.GetThicknessForLayout();
		this.m_revealedCard.m_deckIndex = deckZone.RemoveCard(card);
		deckZone.SetSuppressEmotes(true);
		deckZone.UpdateLayout();
		float num = 0.5f * showSec;
		Vector3 vector = thicknessForLayout.GetMeshRenderer(false).bounds.center + Card.IN_DECK_OFFSET;
		Vector3 vector2 = vector + Card.ABOVE_DECK_OFFSET;
		Vector3 eulerAngles = rotation.eulerAngles;
		Vector3[] array = new Vector3[]
		{
			vector,
			vector2,
			position
		};
		card.ShowCard();
		this.m_revealedCard.m_initialActor.Show();
		card.transform.position = vector;
		card.transform.rotation = Card.IN_DECK_HIDDEN_ROTATION;
		card.transform.localScale = Card.IN_DECK_SCALE;
		iTween.MoveTo(card.gameObject, iTween.Hash(new object[]
		{
			"path",
			array,
			"delay",
			delaySec,
			"time",
			showSec,
			"easetype",
			iTween.EaseType.easeInOutQuart
		}));
		iTween.RotateTo(card.gameObject, iTween.Hash(new object[]
		{
			"rotation",
			eulerAngles,
			"delay",
			delaySec + num,
			"time",
			num,
			"easetype",
			iTween.EaseType.easeInOutCubic
		}));
		iTween.ScaleTo(card.gameObject, iTween.Hash(new object[]
		{
			"scale",
			localScale,
			"delay",
			delaySec + num,
			"time",
			num,
			"easetype",
			iTween.EaseType.easeInOutQuint
		}));
		if (!string.IsNullOrEmpty(this.m_ShowSoundPrefab))
		{
			SoundManager.Get().LoadAndPlay(this.m_ShowSoundPrefab);
		}
		Action<object> action = delegate(object tweenUserData)
		{
			this.m_revealedCard.m_effectsPendingFinish--;
			this.DriftRevealedCard();
		};
		iTween.Timer(card.gameObject, iTween.Hash(new object[]
		{
			"delay",
			delaySec,
			"time",
			showSec,
			"oncomplete",
			action
		}));
	}

	// Token: 0x06006FA8 RID: 28584 RVA: 0x00240594 File Offset: 0x0023E794
	private void DriftRevealedCard()
	{
		Card card = this.m_revealedCard.m_card;
		Vector3 position = card.transform.position;
		float z = this.m_revealedCard.m_initialActor.GetMeshRenderer(false).bounds.size.z;
		float num = 0.02f * z;
		Vector3 vector = GeneralUtils.RandomSign() * num * card.transform.up;
		Vector3 b = -vector;
		Vector3 vector2 = GeneralUtils.RandomSign() * num * card.transform.right;
		Vector3 b2 = -vector2;
		List<Vector3> list = new List<Vector3>();
		list.Add(position + vector + vector2);
		list.Add(position + b + vector2);
		list.Add(position);
		list.Add(position + vector + b2);
		list.Add(position + b + b2);
		list.Add(position);
		float num2 = this.m_DriftCycleTime + this.GetRandomSec();
		Hashtable args = iTween.Hash(new object[]
		{
			"path",
			list.ToArray(),
			"time",
			num2,
			"easetype",
			iTween.EaseType.linear,
			"looptype",
			iTween.LoopType.loop
		});
		iTween.MoveTo(card.gameObject, args);
	}

	// Token: 0x06006FA9 RID: 28585 RVA: 0x0024070B File Offset: 0x0023E90B
	private IEnumerator FlipRevealedCard()
	{
		float revealSec = this.m_RevealTime + this.GetRandomSec();
		this.FlipRevealedCardAnim(revealSec);
		while (this.IsRevealedCardBusy())
		{
			yield return null;
		}
		iTween.Timer(base.gameObject, iTween.Hash(new object[]
		{
			"time",
			this.m_HoldTime
		}));
		while (iTween.HasTween(base.gameObject))
		{
			yield return null;
		}
		yield break;
	}

	// Token: 0x06006FAA RID: 28586 RVA: 0x0024071C File Offset: 0x0023E91C
	private void FlipRevealedCardAnim(float revealSec)
	{
		this.m_revealedCard.m_effectsPendingFinish++;
		Card card = this.m_revealedCard.m_card;
		Actor hiddenActor = this.m_revealedCard.m_initialActor;
		Actor revealedActor = this.m_revealedCard.m_revealedActor;
		TransformUtil.SetEulerAngleZ(revealedActor.gameObject, -180f);
		iTween.RotateAdd(hiddenActor.gameObject, iTween.Hash(new object[]
		{
			"z",
			180f,
			"time",
			revealSec,
			"easetype",
			this.m_RevealEaseType
		}));
		iTween.RotateAdd(revealedActor.gameObject, iTween.Hash(new object[]
		{
			"z",
			180f,
			"time",
			revealSec,
			"easetype",
			this.m_RevealEaseType
		}));
		float startAngleZ = revealedActor.transform.rotation.eulerAngles.z;
		Action<object> action = delegate(object tweenUserData)
		{
			float z = revealedActor.transform.rotation.eulerAngles.z;
			if (Mathf.DeltaAngle(startAngleZ, z) >= 90f)
			{
				revealedActor.Show();
				hiddenActor.Hide();
			}
		};
		Action<object> action2 = delegate(object tweenUserData)
		{
			revealedActor.Show();
			hiddenActor.Hide();
			this.m_revealedCard.m_effectsPendingFinish--;
		};
		iTween.Timer(card.gameObject, iTween.Hash(new object[]
		{
			"time",
			revealSec,
			"onupdate",
			action,
			"oncomplete",
			action2
		}));
	}

	// Token: 0x06006FAB RID: 28587 RVA: 0x002408B7 File Offset: 0x0023EAB7
	private IEnumerator PlayRevealedCard()
	{
		Spell powerUpSpell = this.m_revealedCard.m_revealedActor.GetSpell(SpellType.POWER_UP);
		if (powerUpSpell == null)
		{
			yield break;
		}
		powerUpSpell.AddStateFinishedCallback(delegate(Spell spell, SpellStateType prevStateType, object userData)
		{
			if (prevStateType == SpellStateType.BIRTH)
			{
				this.m_revealedCard.m_effectsPendingFinish--;
			}
		});
		this.m_revealedCard.m_effectsPendingFinish++;
		powerUpSpell.ActivateState(SpellStateType.BIRTH);
		while (this.IsRevealedCardBusy())
		{
			yield return null;
		}
		powerUpSpell.Deactivate();
		yield break;
	}

	// Token: 0x06006FAC RID: 28588 RVA: 0x002408C6 File Offset: 0x0023EAC6
	private void DestroyRevealedCard()
	{
		this.m_revealedCard.m_card.SetInputEnabled(true);
		this.m_revealedCard.m_initialActor.Destroy();
		this.m_revealedCard.m_revealedActor.Destroy();
		this.m_revealedCard = null;
	}

	// Token: 0x06006FAD RID: 28589 RVA: 0x00240900 File Offset: 0x0023EB00
	private float GetRandomSec()
	{
		return UnityEngine.Random.Range(this.m_RandomSecMin, this.m_RandomSecMax);
	}

	// Token: 0x06006FAE RID: 28590 RVA: 0x00240913 File Offset: 0x0023EB13
	private bool IsRevealedCardBusy()
	{
		return this.m_revealedCard != null && this.m_revealedCard.m_effectsPendingFinish > 0;
	}

	// Token: 0x04005983 RID: 22915
	public float m_ShowTime = 1.2f;

	// Token: 0x04005984 RID: 22916
	public float m_RandomSecMin = 0.1f;

	// Token: 0x04005985 RID: 22917
	public float m_RandomSecMax = 0.25f;

	// Token: 0x04005986 RID: 22918
	public float m_DriftCycleTime = 10f;

	// Token: 0x04005987 RID: 22919
	public float m_RevealTime = 0.5f;

	// Token: 0x04005988 RID: 22920
	public float m_HoldTime = 1.2f;

	// Token: 0x04005989 RID: 22921
	public iTween.EaseType m_RevealEaseType = iTween.EaseType.easeOutBack;

	// Token: 0x0400598A RID: 22922
	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_DrawStingerPrefab;

	// Token: 0x0400598B RID: 22923
	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_ShowSoundPrefab;

	// Token: 0x0400598C RID: 22924
	private PlaySpellCardFromDeckSepll.RevealedCard m_revealedCard;

	// Token: 0x020023CB RID: 9163
	private class RevealedCard
	{
		// Token: 0x0400E811 RID: 59409
		public Player m_player;

		// Token: 0x0400E812 RID: 59410
		public Card m_card;

		// Token: 0x0400E813 RID: 59411
		public int m_deckIndex;

		// Token: 0x0400E814 RID: 59412
		public Actor m_initialActor;

		// Token: 0x0400E815 RID: 59413
		public Actor m_revealedActor;

		// Token: 0x0400E816 RID: 59414
		public int m_effectsPendingFinish;
	}
}
