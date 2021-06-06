using System;
using System.Collections;
using System.Collections.Generic;
using Blizzard.T5.Core;
using UnityEngine;

// Token: 0x020006CB RID: 1739
[CustomEditClass]
public class RevealCardSpellController : SpellController
{
	// Token: 0x0600616C RID: 24940 RVA: 0x001FCD3C File Offset: 0x001FAF3C
	protected override bool AddPowerSourceAndTargets(PowerTaskList taskList)
	{
		if (!this.HasSourceCard(taskList))
		{
			return false;
		}
		Card card = taskList.GetSourceEntity(true).GetCard();
		base.SetSource(card);
		this.m_hideEntityTask = -1;
		if (taskList.HasTargetEntity())
		{
			int entityId = taskList.GetTargetEntity(true).GetEntityId();
			List<PowerTask> taskList2 = taskList.GetTaskList();
			for (int i = 0; i < taskList2.Count; i++)
			{
				Network.HistHideEntity histHideEntity = taskList2[i].GetPower() as Network.HistHideEntity;
				if (histHideEntity != null && histHideEntity.Entity == entityId)
				{
					this.m_hideEntityTask = i;
				}
			}
			if (this.m_hideEntityTask < 0)
			{
				return false;
			}
		}
		else if (!taskList.IsStartOfBlock())
		{
			return false;
		}
		return true;
	}

	// Token: 0x0600616D RID: 24941 RVA: 0x001FCDDA File Offset: 0x001FAFDA
	protected override void OnProcessTaskList()
	{
		base.StartCoroutine(this.DoEffectWithTiming());
	}

	// Token: 0x0600616E RID: 24942 RVA: 0x001FCDE9 File Offset: 0x001FAFE9
	private IEnumerator DoEffectWithTiming()
	{
		yield return base.StartCoroutine(this.CompleteTasksBeforeHideEntity());
		this.CreateRevealedCardActors();
		if (this.m_revealedCard != null)
		{
			yield return base.StartCoroutine(this.PullRevealedCardFromDeck());
			yield return base.StartCoroutine(this.FlipRevealedCard());
			yield return base.StartCoroutine(this.HideRevealedCardAnim());
			this.DestroyRevealedCard();
		}
		else
		{
			yield return base.StartCoroutine(this.PlayNoRevealedCardSpell());
		}
		base.OnProcessTaskList();
		yield break;
	}

	// Token: 0x0600616F RID: 24943 RVA: 0x001FCDF8 File Offset: 0x001FAFF8
	private IEnumerator CompleteTasksBeforeHideEntity()
	{
		if (this.m_hideEntityTask > 0)
		{
			RevealCardSpellController.<>c__DisplayClass21_0 CS$<>8__locals1 = new RevealCardSpellController.<>c__DisplayClass21_0();
			CS$<>8__locals1.complete = false;
			PowerTaskList.CompleteCallback callback = delegate(PowerTaskList taskList, int startIndex, int count, object userData)
			{
				CS$<>8__locals1.complete = true;
			};
			this.m_taskList.DoTasks(0, this.m_hideEntityTask, callback);
			while (!CS$<>8__locals1.complete)
			{
				yield return null;
			}
			CS$<>8__locals1 = null;
		}
		yield break;
	}

	// Token: 0x06006170 RID: 24944 RVA: 0x001FCE08 File Offset: 0x001FB008
	private void CreateRevealedCardActors()
	{
		if (!this.m_taskList.HasTargetEntity())
		{
			return;
		}
		Entity entity = this.m_taskList.GetTargetEntity(true);
		Card card = entity.GetCard();
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
		GameObject gameObject2 = AssetLoader.Get().InstantiatePrefab(ActorNames.GetHandActor(entity), AssetLoadingOptions.IgnorePrefabPosition);
		if (gameObject2 == null)
		{
			Log.Gameplay.PrintError("{0}.CreateRevealedCardActors() - Failed to load HAND actor.", new object[]
			{
				this
			});
			return;
		}
		this.m_revealedCard = new RevealCardSpellController.RevealedCard();
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

	// Token: 0x06006171 RID: 24945 RVA: 0x001FCF51 File Offset: 0x001FB151
	private IEnumerator PullRevealedCardFromDeck()
	{
		if (!string.IsNullOrEmpty(this.m_DrawStingerPrefab))
		{
			SoundManager.Get().LoadAndPlay(this.m_DrawStingerPrefab);
		}
		string text = (this.m_revealedCard.m_player.GetSide() == Player.Side.FRIENDLY) ? this.m_FriendlyBoneName : this.m_OpponentBoneName;
		if (UniversalInputManager.UsePhoneUI)
		{
			text += "_phone";
		}
		Transform transform = Board.Get().FindBone(text);
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

	// Token: 0x06006172 RID: 24946 RVA: 0x001FCF60 File Offset: 0x001FB160
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

	// Token: 0x06006173 RID: 24947 RVA: 0x001FD1FC File Offset: 0x001FB3FC
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

	// Token: 0x06006174 RID: 24948 RVA: 0x001FD373 File Offset: 0x001FB573
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

	// Token: 0x06006175 RID: 24949 RVA: 0x001FD384 File Offset: 0x001FB584
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

	// Token: 0x06006176 RID: 24950 RVA: 0x001FD51F File Offset: 0x001FB71F
	private IEnumerator HideRevealedCardAnim()
	{
		if (!string.IsNullOrEmpty(this.m_HideStingerPrefab))
		{
			SoundManager.Get().LoadAndPlay(this.m_HideStingerPrefab);
		}
		float randomSec = this.GetRandomSec();
		float hideSec = this.m_HideTime + this.GetRandomSec();
		this.HideRevealedCardAnim(randomSec, hideSec);
		while (this.IsRevealedCardBusy())
		{
			yield return null;
		}
		yield break;
	}

	// Token: 0x06006177 RID: 24951 RVA: 0x001FD530 File Offset: 0x001FB730
	private void HideRevealedCardAnim(float delaySec, float hideSec)
	{
		this.m_revealedCard.m_effectsPendingFinish++;
		Card card = this.m_revealedCard.m_card;
		ZoneDeck deck = this.m_revealedCard.m_player.GetDeckZone();
		Vector3 center = deck.GetThicknessForLayout().GetMeshRenderer(false).bounds.center;
		float num = 0.5f * hideSec;
		Vector3 position = card.transform.position;
		Vector3 vector = center + Card.ABOVE_DECK_OFFSET;
		Vector3 vector2 = center + Card.IN_DECK_OFFSET;
		Vector3 in_DECK_ANGLES = Card.IN_DECK_ANGLES;
		Vector3 in_DECK_SCALE = Card.IN_DECK_SCALE;
		Vector3[] array = new Vector3[]
		{
			position,
			vector,
			vector2
		};
		iTween.MoveTo(card.gameObject, iTween.Hash(new object[]
		{
			"path",
			array,
			"delay",
			delaySec,
			"time",
			hideSec,
			"easetype",
			iTween.EaseType.easeInOutQuad
		}));
		iTween.RotateTo(card.gameObject, iTween.Hash(new object[]
		{
			"rotation",
			in_DECK_ANGLES,
			"delay",
			delaySec,
			"time",
			num,
			"easetype",
			iTween.EaseType.easeInOutCubic
		}));
		iTween.ScaleTo(card.gameObject, iTween.Hash(new object[]
		{
			"scale",
			in_DECK_SCALE,
			"delay",
			delaySec + num,
			"time",
			num,
			"easetype",
			iTween.EaseType.easeInOutQuint
		}));
		if (!string.IsNullOrEmpty(this.m_HideSoundPrefab))
		{
			SoundManager.Get().LoadAndPlay(this.m_HideSoundPrefab);
		}
		Action<object> action = delegate(object userData)
		{
			this.m_revealedCard.m_effectsPendingFinish--;
			this.m_revealedCard.m_initialActor.GetCard().HideCard();
			deck.InsertCard(this.m_revealedCard.m_deckIndex, card);
			deck.UpdateLayout();
			deck.SetSuppressEmotes(false);
		};
		iTween.Timer(card.gameObject, iTween.Hash(new object[]
		{
			"delay",
			delaySec,
			"time",
			hideSec,
			"oncomplete",
			action
		}));
	}

	// Token: 0x06006178 RID: 24952 RVA: 0x001FD79E File Offset: 0x001FB99E
	private void DestroyRevealedCard()
	{
		this.m_revealedCard.m_card.SetInputEnabled(true);
		this.m_revealedCard.m_initialActor.Destroy();
		this.m_revealedCard.m_revealedActor.Destroy();
		this.m_revealedCard = null;
	}

	// Token: 0x06006179 RID: 24953 RVA: 0x001FD7D8 File Offset: 0x001FB9D8
	private IEnumerator PlayNoRevealedCardSpell()
	{
		ZoneDeck deckZone = base.GetSource().GetController().GetDeckZone();
		if (deckZone == null)
		{
			yield break;
		}
		Spell noCardSpellInstance = UnityEngine.Object.Instantiate<Spell>(this.m_NoRevealedCardSpellPrefab);
		if (noCardSpellInstance == null)
		{
			yield break;
		}
		noCardSpellInstance.SetPosition(deckZone.transform.position);
		noCardSpellInstance.AddStateFinishedCallback(delegate(Spell spell, SpellStateType prevStateType, object userData)
		{
			if (spell.GetActiveState() == SpellStateType.NONE)
			{
				UnityEngine.Object.Destroy(spell.gameObject);
			}
		});
		noCardSpellInstance.Activate();
		while (!noCardSpellInstance.IsFinished())
		{
			yield return null;
		}
		yield break;
	}

	// Token: 0x0600617A RID: 24954 RVA: 0x001FD7E7 File Offset: 0x001FB9E7
	private float GetRandomSec()
	{
		return UnityEngine.Random.Range(this.m_RandomSecMin, this.m_RandomSecMax);
	}

	// Token: 0x0600617B RID: 24955 RVA: 0x001FD7FA File Offset: 0x001FB9FA
	private bool IsRevealedCardBusy()
	{
		return this.m_revealedCard != null && this.m_revealedCard.m_effectsPendingFinish > 0;
	}

	// Token: 0x0400513B RID: 20795
	public Spell m_NoRevealedCardSpellPrefab;

	// Token: 0x0400513C RID: 20796
	public float m_RandomSecMin = 0.1f;

	// Token: 0x0400513D RID: 20797
	public float m_RandomSecMax = 0.25f;

	// Token: 0x0400513E RID: 20798
	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_ShowSoundPrefab;

	// Token: 0x0400513F RID: 20799
	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_DrawStingerPrefab;

	// Token: 0x04005140 RID: 20800
	public float m_ShowTime = 1.2f;

	// Token: 0x04005141 RID: 20801
	public float m_DriftCycleTime = 10f;

	// Token: 0x04005142 RID: 20802
	public float m_RevealTime = 0.5f;

	// Token: 0x04005143 RID: 20803
	public iTween.EaseType m_RevealEaseType = iTween.EaseType.easeOutBack;

	// Token: 0x04005144 RID: 20804
	public float m_HoldTime = 1.2f;

	// Token: 0x04005145 RID: 20805
	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_HideSoundPrefab;

	// Token: 0x04005146 RID: 20806
	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_HideStingerPrefab;

	// Token: 0x04005147 RID: 20807
	public float m_HideTime = 0.8f;

	// Token: 0x04005148 RID: 20808
	public string m_FriendlyBoneName = "FriendlyJoust";

	// Token: 0x04005149 RID: 20809
	public string m_OpponentBoneName = "OpponentJoust";

	// Token: 0x0400514A RID: 20810
	private int m_hideEntityTask;

	// Token: 0x0400514B RID: 20811
	private RevealCardSpellController.RevealedCard m_revealedCard;

	// Token: 0x02002225 RID: 8741
	private class RevealedCard
	{
		// Token: 0x0400E28B RID: 57995
		public Player m_player;

		// Token: 0x0400E28C RID: 57996
		public Card m_card;

		// Token: 0x0400E28D RID: 57997
		public int m_deckIndex;

		// Token: 0x0400E28E RID: 57998
		public Actor m_initialActor;

		// Token: 0x0400E28F RID: 57999
		public Actor m_revealedActor;

		// Token: 0x0400E290 RID: 58000
		public int m_effectsPendingFinish;
	}
}
