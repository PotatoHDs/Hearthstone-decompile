using System;
using System.Collections;
using System.Collections.Generic;
using Blizzard.T5.Core;
using PegasusGame;
using UnityEngine;

// Token: 0x020006C7 RID: 1735
[CustomEditClass]
public class JoustSpellController : SpellController
{
	// Token: 0x06006136 RID: 24886 RVA: 0x001FB5EC File Offset: 0x001F97EC
	protected override bool AddPowerSourceAndTargets(PowerTaskList taskList)
	{
		if (!this.HasSourceCard(taskList))
		{
			return false;
		}
		this.m_joustTaskIndex = -1;
		List<PowerTask> taskList2 = taskList.GetTaskList();
		for (int i = 0; i < taskList2.Count; i++)
		{
			Network.HistMetaData histMetaData = taskList2[i].GetPower() as Network.HistMetaData;
			if (histMetaData != null && histMetaData.MetaType == HistoryMeta.Type.JOUST)
			{
				this.m_joustTaskIndex = i;
				if (histMetaData.AdditionalData != null && histMetaData.AdditionalData.Count > 0)
				{
					int num = histMetaData.AdditionalData[0];
					if (num - 1 <= 1)
					{
						this.m_joustType = num;
					}
					else
					{
						this.m_joustType = 2;
					}
				}
				else
				{
					this.m_joustType = 2;
				}
			}
		}
		if (this.m_joustTaskIndex < 0)
		{
			return false;
		}
		Card card = taskList.GetSourceEntity(true).GetCard();
		base.SetSource(card);
		return true;
	}

	// Token: 0x06006137 RID: 24887 RVA: 0x001FB6AD File Offset: 0x001F98AD
	protected override void OnProcessTaskList()
	{
		base.StartCoroutine(this.DoEffectWithTiming());
	}

	// Token: 0x06006138 RID: 24888 RVA: 0x001FB6BC File Offset: 0x001F98BC
	private IEnumerator DoEffectWithTiming()
	{
		yield return base.StartCoroutine(this.WaitForShowEntities());
		this.CreateJousters();
		yield return base.StartCoroutine(this.ShowJousters());
		yield return base.StartCoroutine(this.Joust());
		yield return base.StartCoroutine(this.HideJousters());
		this.DestroyJousters();
		base.OnProcessTaskList();
		yield break;
	}

	// Token: 0x06006139 RID: 24889 RVA: 0x001FB6CB File Offset: 0x001F98CB
	private IEnumerator WaitForShowEntities()
	{
		bool complete = false;
		PowerTaskList.CompleteCallback callback = delegate(PowerTaskList taskList, int startIndex, int count, object userData)
		{
			complete = true;
		};
		this.m_taskList.DoTasks(0, this.m_joustTaskIndex, callback);
		while (!complete)
		{
			yield return null;
		}
		yield break;
	}

	// Token: 0x0600613A RID: 24890 RVA: 0x001FB6DC File Offset: 0x001F98DC
	private void CreateJousters()
	{
		Network.HistMetaData metaData = (Network.HistMetaData)this.m_taskList.GetTaskList()[this.m_joustTaskIndex].GetPower();
		global::Player friendlySidePlayer = GameState.Get().GetFriendlySidePlayer();
		global::Player opposingSidePlayer = GameState.Get().GetOpposingSidePlayer();
		this.m_friendlyJouster = this.CreateJouster(friendlySidePlayer, metaData);
		this.m_opponentJouster = this.CreateJouster(opposingSidePlayer, metaData);
		this.DetermineWinner(metaData);
		this.DetermineSourceJouster();
	}

	// Token: 0x0600613B RID: 24891 RVA: 0x001FB74C File Offset: 0x001F994C
	private JoustSpellController.Jouster CreateJouster(global::Player player, Network.HistMetaData metaData)
	{
		global::Entity entity = null;
		foreach (int id in metaData.Info)
		{
			global::Entity entity2 = GameState.Get().GetEntity(id);
			if (entity2 != null && entity2.GetController() == player)
			{
				entity = entity2;
				break;
			}
		}
		if (entity == null)
		{
			return null;
		}
		Card card = entity.GetCard();
		card.SetInputEnabled(false);
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab("Card_Hidden.prefab:1a94649d257bc284ca6e2962f634a8b9", AssetLoadingOptions.IgnorePrefabPosition);
		GameObject gameObject2 = AssetLoader.Get().InstantiatePrefab(ActorNames.GetHandActor(entity), AssetLoadingOptions.IgnorePrefabPosition);
		JoustSpellController.Jouster jouster = new JoustSpellController.Jouster();
		jouster.m_player = player;
		jouster.m_card = card;
		jouster.m_initialActor = gameObject.GetComponent<Actor>();
		jouster.m_revealedActor = gameObject2.GetComponent<Actor>();
		Action<Actor> action = delegate(Actor actor)
		{
			actor.SetEntity(entity);
			actor.SetCard(card);
			actor.SetCardDefFromCard(card);
			actor.UpdateAllComponents();
			actor.Hide();
		};
		action(jouster.m_initialActor);
		action(jouster.m_revealedActor);
		return jouster;
	}

	// Token: 0x0600613C RID: 24892 RVA: 0x001FB87C File Offset: 0x001F9A7C
	private void DetermineWinner(Network.HistMetaData metaData)
	{
		Card joustWinner = GameUtils.GetJoustWinner(metaData);
		if (!joustWinner)
		{
			return;
		}
		if (joustWinner.GetController().IsFriendlySide())
		{
			this.m_winningJouster = this.m_friendlyJouster;
			return;
		}
		this.m_winningJouster = this.m_opponentJouster;
	}

	// Token: 0x0600613D RID: 24893 RVA: 0x001FB8C0 File Offset: 0x001F9AC0
	private void DetermineSourceJouster()
	{
		global::Player controller = base.GetSource().GetController();
		if (this.m_friendlyJouster != null && this.m_friendlyJouster.m_card.GetController() == controller)
		{
			this.m_sourceJouster = this.m_friendlyJouster;
			return;
		}
		if (this.m_opponentJouster != null && this.m_opponentJouster.m_card.GetController() == controller)
		{
			this.m_sourceJouster = this.m_opponentJouster;
		}
	}

	// Token: 0x0600613E RID: 24894 RVA: 0x001FB928 File Offset: 0x001F9B28
	private IEnumerator ShowJousters()
	{
		if (!string.IsNullOrEmpty(this.m_DrawStingerPrefab))
		{
			SoundManager.Get().LoadAndPlay(this.m_DrawStingerPrefab);
		}
		string text = this.m_FriendlyBoneName;
		string text2 = this.m_OpponentBoneName;
		if (UniversalInputManager.UsePhoneUI)
		{
			text += "_phone";
			text2 += "_phone";
		}
		Transform transform = Board.Get().FindBone(text);
		Transform transform2 = Board.Get().FindBone(text2);
		Quaternion rotation = Quaternion.LookRotation(transform2.position - transform.position);
		if (this.m_friendlyJouster != null)
		{
			Vector3 localScale = transform.localScale;
			Vector3 position = transform.position;
			float randomSec = this.GetRandomSec();
			float showSec = this.m_ShowTime + this.GetRandomSec();
			this.ShowJouster(this.m_friendlyJouster, localScale, rotation, position, randomSec, showSec);
		}
		else if (this.m_joustType == 2)
		{
			this.PlayNoJousterSpell(GameState.Get().GetFriendlySidePlayer());
		}
		if (this.m_opponentJouster != null)
		{
			Vector3 localScale2 = transform2.localScale;
			Vector3 position2 = transform2.position;
			float randomSec2 = this.GetRandomSec();
			float showSec2 = this.m_ShowTime + this.GetRandomSec();
			this.ShowJouster(this.m_opponentJouster, localScale2, rotation, position2, randomSec2, showSec2);
		}
		else if (this.m_joustType == 2)
		{
			this.PlayNoJousterSpell(GameState.Get().GetOpposingSidePlayer());
		}
		while (this.IsJousterBusy(this.m_friendlyJouster) || this.IsJousterBusy(this.m_opponentJouster))
		{
			yield return null;
		}
		yield break;
	}

	// Token: 0x0600613F RID: 24895 RVA: 0x001FB938 File Offset: 0x001F9B38
	private void ShowJouster(JoustSpellController.Jouster jouster, Vector3 localScale, Quaternion rotation, Vector3 position, float delaySec, float showSec)
	{
		jouster.m_effectsPendingFinish++;
		Card card = jouster.m_card;
		ZoneDeck deckZone = jouster.m_player.GetDeckZone();
		Actor thicknessForLayout = deckZone.GetThicknessForLayout();
		jouster.m_deckIndex = deckZone.RemoveCard(card);
		Card firstCard = deckZone.GetFirstCard();
		deckZone.RemoveCard(firstCard);
		deckZone.SetSuppressEmotes(true);
		deckZone.UpdateLayout();
		if (firstCard != null)
		{
			deckZone.InsertCard(0, firstCard);
		}
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
		jouster.m_initialActor.Show();
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
			jouster.m_effectsPendingFinish--;
			this.DriftJouster(jouster);
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

	// Token: 0x06006140 RID: 24896 RVA: 0x001FBC18 File Offset: 0x001F9E18
	private void PlayNoJousterSpell(global::Player player)
	{
		ZoneDeck deckZone = player.GetDeckZone();
		Spell spell2 = UnityEngine.Object.Instantiate<Spell>(this.m_NoJousterSpellPrefab);
		spell2.SetPosition(deckZone.transform.position);
		spell2.AddStateFinishedCallback(delegate(Spell spell, SpellStateType prevStateType, object userData)
		{
			if (spell.GetActiveState() == SpellStateType.NONE)
			{
				UnityEngine.Object.Destroy(spell.gameObject);
			}
		});
		spell2.Activate();
	}

	// Token: 0x06006141 RID: 24897 RVA: 0x001FBC74 File Offset: 0x001F9E74
	private void DriftJouster(JoustSpellController.Jouster jouster)
	{
		Card card = jouster.m_card;
		Vector3 position = card.transform.position;
		float z = jouster.m_initialActor.GetMeshRenderer(false).bounds.size.z;
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

	// Token: 0x06006142 RID: 24898 RVA: 0x001FBDE1 File Offset: 0x001F9FE1
	private IEnumerator Joust()
	{
		if (this.m_friendlyJouster != null)
		{
			float revealSec = this.m_RevealTime + this.GetRandomSec();
			this.RevealJouster(this.m_friendlyJouster, revealSec);
		}
		if (this.m_opponentJouster != null)
		{
			float revealSec2 = this.m_RevealTime + this.GetRandomSec();
			this.RevealJouster(this.m_opponentJouster, revealSec2);
		}
		if (this.m_sourceJouster != null)
		{
			while (this.IsJousterBusy(this.m_friendlyJouster) || this.IsJousterBusy(this.m_opponentJouster))
			{
				yield return null;
			}
			Spell spellPrefab;
			if (this.m_joustType == 1)
			{
				if (this.m_sourceJouster.m_player.IsFriendlySide())
				{
					spellPrefab = ((this.m_sourceJouster == this.m_winningJouster) ? this.m_WinnerSpellPrefab : this.m_LoserSpellPrefab);
				}
				else
				{
					spellPrefab = this.m_LoserSpellPrefab;
				}
			}
			else
			{
				spellPrefab = ((this.m_sourceJouster == this.m_winningJouster) ? this.m_WinnerSpellPrefab : this.m_LoserSpellPrefab);
			}
			this.PlaySpellOnActor(this.m_sourceJouster, this.m_sourceJouster.m_revealedActor, spellPrefab);
		}
		if (this.m_friendlyJouster != null || this.m_opponentJouster != null)
		{
			iTween.Timer(base.gameObject, iTween.Hash(new object[]
			{
				"time",
				this.m_HoldTime
			}));
		}
		while (this.IsJousterBusy(this.m_friendlyJouster) || this.IsJousterBusy(this.m_opponentJouster) || iTween.HasTween(base.gameObject))
		{
			yield return null;
		}
		yield break;
	}

	// Token: 0x06006143 RID: 24899 RVA: 0x001FBDF0 File Offset: 0x001F9FF0
	private void RevealJouster(JoustSpellController.Jouster jouster, float revealSec)
	{
		if (this.m_joustType == 1 && !this.m_sourceJouster.m_player.IsFriendlySide())
		{
			return;
		}
		jouster.m_effectsPendingFinish++;
		Card card = jouster.m_card;
		Actor hiddenActor = jouster.m_initialActor;
		Actor revealedActor = jouster.m_revealedActor;
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
			jouster.m_effectsPendingFinish--;
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

	// Token: 0x06006144 RID: 24900 RVA: 0x001FBFA7 File Offset: 0x001FA1A7
	private IEnumerator HideJousters()
	{
		if (!string.IsNullOrEmpty(this.m_HideStingerPrefab))
		{
			SoundManager.Get().LoadAndPlay(this.m_HideStingerPrefab);
		}
		if (this.m_friendlyJouster != null)
		{
			float randomSec = this.GetRandomSec();
			float hideSec = this.m_HideTime + this.GetRandomSec();
			this.HideJouster(this.m_friendlyJouster, randomSec, hideSec);
		}
		if (this.m_opponentJouster != null)
		{
			float randomSec2 = this.GetRandomSec();
			float hideSec2 = this.m_HideTime + this.GetRandomSec();
			this.HideJouster(this.m_opponentJouster, randomSec2, hideSec2);
		}
		while (this.IsJousterBusy(this.m_friendlyJouster) || this.IsJousterBusy(this.m_opponentJouster))
		{
			yield return null;
		}
		yield break;
	}

	// Token: 0x06006145 RID: 24901 RVA: 0x001FBFB8 File Offset: 0x001FA1B8
	private void HideJouster(JoustSpellController.Jouster jouster, float delaySec, float hideSec)
	{
		jouster.m_effectsPendingFinish++;
		Card card = jouster.m_card;
		ZoneDeck deck = jouster.m_player.GetDeckZone();
		Vector3 center = deck.GetThicknessForLayout().GetMeshRenderer(false).bounds.center;
		float num = 0.5f * hideSec;
		Vector3 position = card.transform.position;
		Vector3 vector = center + Card.ABOVE_DECK_OFFSET;
		Vector3 vector2 = center + Card.IN_DECK_OFFSET;
		Vector3 in_DECK_ANGLES = Card.IN_DECK_ANGLES;
		if (this.m_joustType == 1 && !this.m_sourceJouster.m_player.IsFriendlySide())
		{
			in_DECK_ANGLES.x *= -1f;
		}
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
			jouster.m_effectsPendingFinish--;
			jouster.m_initialActor.GetCard().HideCard();
			deck.InsertCard(jouster.m_deckIndex, card);
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

	// Token: 0x06006146 RID: 24902 RVA: 0x001FC251 File Offset: 0x001FA451
	private void DestroyJousters()
	{
		if (this.m_friendlyJouster != null)
		{
			this.DestroyJouster(this.m_friendlyJouster);
			this.m_friendlyJouster = null;
		}
		if (this.m_opponentJouster != null)
		{
			this.DestroyJouster(this.m_opponentJouster);
			this.m_opponentJouster = null;
		}
	}

	// Token: 0x06006147 RID: 24903 RVA: 0x001FC289 File Offset: 0x001FA489
	private void DestroyJouster(JoustSpellController.Jouster jouster)
	{
		if (jouster == null)
		{
			return;
		}
		jouster.m_card.SetInputEnabled(true);
		jouster.m_initialActor.Destroy();
		jouster.m_revealedActor.Destroy();
	}

	// Token: 0x06006148 RID: 24904 RVA: 0x001FC2B1 File Offset: 0x001FA4B1
	private float GetRandomSec()
	{
		return UnityEngine.Random.Range(this.m_RandomSecMin, this.m_RandomSecMax);
	}

	// Token: 0x06006149 RID: 24905 RVA: 0x001FC2C4 File Offset: 0x001FA4C4
	private bool PlaySpellOnActor(JoustSpellController.Jouster jouster, Actor actor, Spell spellPrefab)
	{
		if (!spellPrefab)
		{
			return false;
		}
		jouster.m_effectsPendingFinish++;
		Card card = actor.GetCard();
		Spell spell2 = UnityEngine.Object.Instantiate<Spell>(spellPrefab);
		spell2.transform.parent = actor.transform;
		spell2.AddFinishedCallback(delegate(Spell spell, object spellUserData)
		{
			jouster.m_effectsPendingFinish--;
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
		return true;
	}

	// Token: 0x0600614A RID: 24906 RVA: 0x001FC362 File Offset: 0x001FA562
	private bool IsJousterBusy(JoustSpellController.Jouster jouster)
	{
		return jouster != null && jouster.m_effectsPendingFinish > 0;
	}

	// Token: 0x04005119 RID: 20761
	public Spell m_WinnerSpellPrefab;

	// Token: 0x0400511A RID: 20762
	public Spell m_LoserSpellPrefab;

	// Token: 0x0400511B RID: 20763
	public Spell m_NoJousterSpellPrefab;

	// Token: 0x0400511C RID: 20764
	public float m_RandomSecMin = 0.1f;

	// Token: 0x0400511D RID: 20765
	public float m_RandomSecMax = 0.25f;

	// Token: 0x0400511E RID: 20766
	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_ShowSoundPrefab;

	// Token: 0x0400511F RID: 20767
	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_DrawStingerPrefab;

	// Token: 0x04005120 RID: 20768
	public float m_ShowTime = 1.2f;

	// Token: 0x04005121 RID: 20769
	public float m_DriftCycleTime = 10f;

	// Token: 0x04005122 RID: 20770
	public float m_RevealTime = 0.5f;

	// Token: 0x04005123 RID: 20771
	public iTween.EaseType m_RevealEaseType = iTween.EaseType.easeOutBack;

	// Token: 0x04005124 RID: 20772
	public float m_HoldTime = 1.2f;

	// Token: 0x04005125 RID: 20773
	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_HideSoundPrefab;

	// Token: 0x04005126 RID: 20774
	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_HideStingerPrefab;

	// Token: 0x04005127 RID: 20775
	public float m_HideTime = 0.8f;

	// Token: 0x04005128 RID: 20776
	public string m_FriendlyBoneName = "FriendlyJoust";

	// Token: 0x04005129 RID: 20777
	public string m_OpponentBoneName = "OpponentJoust";

	// Token: 0x0400512A RID: 20778
	private int m_joustTaskIndex;

	// Token: 0x0400512B RID: 20779
	private const int ONE_SIDED_JOUST = 1;

	// Token: 0x0400512C RID: 20780
	private const int TWO_SIDED_JOUST = 2;

	// Token: 0x0400512D RID: 20781
	private int m_joustType;

	// Token: 0x0400512E RID: 20782
	private JoustSpellController.Jouster m_friendlyJouster;

	// Token: 0x0400512F RID: 20783
	private JoustSpellController.Jouster m_opponentJouster;

	// Token: 0x04005130 RID: 20784
	private JoustSpellController.Jouster m_winningJouster;

	// Token: 0x04005131 RID: 20785
	private JoustSpellController.Jouster m_sourceJouster;

	// Token: 0x02002215 RID: 8725
	private class Jouster
	{
		// Token: 0x0400E259 RID: 57945
		public global::Player m_player;

		// Token: 0x0400E25A RID: 57946
		public Card m_card;

		// Token: 0x0400E25B RID: 57947
		public int m_deckIndex;

		// Token: 0x0400E25C RID: 57948
		public Actor m_initialActor;

		// Token: 0x0400E25D RID: 57949
		public Actor m_revealedActor;

		// Token: 0x0400E25E RID: 57950
		public int m_effectsPendingFinish;
	}
}
