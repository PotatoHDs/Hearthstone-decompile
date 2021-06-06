using System;
using System.Collections;
using System.Collections.Generic;
using Blizzard.T5.Core;
using PegasusGame;
using UnityEngine;

[CustomEditClass]
public class JoustSpellController : SpellController
{
	private class Jouster
	{
		public Player m_player;

		public Card m_card;

		public int m_deckIndex;

		public Actor m_initialActor;

		public Actor m_revealedActor;

		public int m_effectsPendingFinish;
	}

	public Spell m_WinnerSpellPrefab;

	public Spell m_LoserSpellPrefab;

	public Spell m_NoJousterSpellPrefab;

	public float m_RandomSecMin = 0.1f;

	public float m_RandomSecMax = 0.25f;

	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_ShowSoundPrefab;

	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_DrawStingerPrefab;

	public float m_ShowTime = 1.2f;

	public float m_DriftCycleTime = 10f;

	public float m_RevealTime = 0.5f;

	public iTween.EaseType m_RevealEaseType = iTween.EaseType.easeOutBack;

	public float m_HoldTime = 1.2f;

	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_HideSoundPrefab;

	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_HideStingerPrefab;

	public float m_HideTime = 0.8f;

	public string m_FriendlyBoneName = "FriendlyJoust";

	public string m_OpponentBoneName = "OpponentJoust";

	private int m_joustTaskIndex;

	private const int ONE_SIDED_JOUST = 1;

	private const int TWO_SIDED_JOUST = 2;

	private int m_joustType;

	private Jouster m_friendlyJouster;

	private Jouster m_opponentJouster;

	private Jouster m_winningJouster;

	private Jouster m_sourceJouster;

	protected override bool AddPowerSourceAndTargets(PowerTaskList taskList)
	{
		if (!HasSourceCard(taskList))
		{
			return false;
		}
		m_joustTaskIndex = -1;
		List<PowerTask> taskList2 = taskList.GetTaskList();
		for (int i = 0; i < taskList2.Count; i++)
		{
			Network.HistMetaData histMetaData = taskList2[i].GetPower() as Network.HistMetaData;
			if (histMetaData == null || histMetaData.MetaType != HistoryMeta.Type.JOUST)
			{
				continue;
			}
			m_joustTaskIndex = i;
			if (histMetaData.AdditionalData != null && histMetaData.AdditionalData.Count > 0)
			{
				int num = histMetaData.AdditionalData[0];
				if ((uint)(num - 1) <= 1u)
				{
					m_joustType = num;
				}
				else
				{
					m_joustType = 2;
				}
			}
			else
			{
				m_joustType = 2;
			}
		}
		if (m_joustTaskIndex < 0)
		{
			return false;
		}
		Card card = taskList.GetSourceEntity().GetCard();
		SetSource(card);
		return true;
	}

	protected override void OnProcessTaskList()
	{
		StartCoroutine(DoEffectWithTiming());
	}

	private IEnumerator DoEffectWithTiming()
	{
		yield return StartCoroutine(WaitForShowEntities());
		CreateJousters();
		yield return StartCoroutine(ShowJousters());
		yield return StartCoroutine(Joust());
		yield return StartCoroutine(HideJousters());
		DestroyJousters();
		base.OnProcessTaskList();
	}

	private IEnumerator WaitForShowEntities()
	{
		bool complete = false;
		PowerTaskList.CompleteCallback callback = delegate
		{
			complete = true;
		};
		m_taskList.DoTasks(0, m_joustTaskIndex, callback);
		while (!complete)
		{
			yield return null;
		}
	}

	private void CreateJousters()
	{
		Network.HistMetaData metaData = (Network.HistMetaData)m_taskList.GetTaskList()[m_joustTaskIndex].GetPower();
		Player friendlySidePlayer = GameState.Get().GetFriendlySidePlayer();
		Player opposingSidePlayer = GameState.Get().GetOpposingSidePlayer();
		m_friendlyJouster = CreateJouster(friendlySidePlayer, metaData);
		m_opponentJouster = CreateJouster(opposingSidePlayer, metaData);
		DetermineWinner(metaData);
		DetermineSourceJouster();
	}

	private Jouster CreateJouster(Player player, Network.HistMetaData metaData)
	{
		Entity entity = null;
		foreach (int item in metaData.Info)
		{
			Entity entity2 = GameState.Get().GetEntity(item);
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
		card.SetInputEnabled(enabled: false);
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab("Card_Hidden.prefab:1a94649d257bc284ca6e2962f634a8b9", AssetLoadingOptions.IgnorePrefabPosition);
		GameObject gameObject2 = AssetLoader.Get().InstantiatePrefab(ActorNames.GetHandActor(entity), AssetLoadingOptions.IgnorePrefabPosition);
		Jouster jouster = new Jouster();
		jouster.m_player = player;
		jouster.m_card = card;
		jouster.m_initialActor = gameObject.GetComponent<Actor>();
		jouster.m_revealedActor = gameObject2.GetComponent<Actor>();
		Action<Actor> obj = delegate(Actor actor)
		{
			actor.SetEntity(entity);
			actor.SetCard(card);
			actor.SetCardDefFromCard(card);
			actor.UpdateAllComponents();
			actor.Hide();
		};
		obj(jouster.m_initialActor);
		obj(jouster.m_revealedActor);
		return jouster;
	}

	private void DetermineWinner(Network.HistMetaData metaData)
	{
		Card joustWinner = GameUtils.GetJoustWinner(metaData);
		if ((bool)joustWinner)
		{
			if (joustWinner.GetController().IsFriendlySide())
			{
				m_winningJouster = m_friendlyJouster;
			}
			else
			{
				m_winningJouster = m_opponentJouster;
			}
		}
	}

	private void DetermineSourceJouster()
	{
		Player controller = GetSource().GetController();
		if (m_friendlyJouster != null && m_friendlyJouster.m_card.GetController() == controller)
		{
			m_sourceJouster = m_friendlyJouster;
		}
		else if (m_opponentJouster != null && m_opponentJouster.m_card.GetController() == controller)
		{
			m_sourceJouster = m_opponentJouster;
		}
	}

	private IEnumerator ShowJousters()
	{
		if (!string.IsNullOrEmpty(m_DrawStingerPrefab))
		{
			SoundManager.Get().LoadAndPlay(m_DrawStingerPrefab);
		}
		string text = m_FriendlyBoneName;
		string text2 = m_OpponentBoneName;
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			text += "_phone";
			text2 += "_phone";
		}
		Transform transform = Board.Get().FindBone(text);
		Transform transform2 = Board.Get().FindBone(text2);
		Quaternion rotation = Quaternion.LookRotation(transform2.position - transform.position);
		if (m_friendlyJouster != null)
		{
			Vector3 localScale = transform.localScale;
			Vector3 position = transform.position;
			float randomSec = GetRandomSec();
			float showSec = m_ShowTime + GetRandomSec();
			ShowJouster(m_friendlyJouster, localScale, rotation, position, randomSec, showSec);
		}
		else if (m_joustType == 2)
		{
			PlayNoJousterSpell(GameState.Get().GetFriendlySidePlayer());
		}
		if (m_opponentJouster != null)
		{
			Vector3 localScale2 = transform2.localScale;
			Vector3 position2 = transform2.position;
			float randomSec2 = GetRandomSec();
			float showSec2 = m_ShowTime + GetRandomSec();
			ShowJouster(m_opponentJouster, localScale2, rotation, position2, randomSec2, showSec2);
		}
		else if (m_joustType == 2)
		{
			PlayNoJousterSpell(GameState.Get().GetOpposingSidePlayer());
		}
		while (IsJousterBusy(m_friendlyJouster) || IsJousterBusy(m_opponentJouster))
		{
			yield return null;
		}
	}

	private void ShowJouster(Jouster jouster, Vector3 localScale, Quaternion rotation, Vector3 position, float delaySec, float showSec)
	{
		jouster.m_effectsPendingFinish++;
		Card card = jouster.m_card;
		ZoneDeck deckZone = jouster.m_player.GetDeckZone();
		Actor thicknessForLayout = deckZone.GetThicknessForLayout();
		jouster.m_deckIndex = deckZone.RemoveCard(card);
		Card firstCard = deckZone.GetFirstCard();
		deckZone.RemoveCard(firstCard);
		deckZone.SetSuppressEmotes(suppress: true);
		deckZone.UpdateLayout();
		if (firstCard != null)
		{
			deckZone.InsertCard(0, firstCard);
		}
		float num = 0.5f * showSec;
		Vector3 vector = thicknessForLayout.GetMeshRenderer().bounds.center + Card.IN_DECK_OFFSET;
		Vector3 vector2 = vector + Card.ABOVE_DECK_OFFSET;
		Vector3 eulerAngles = rotation.eulerAngles;
		Vector3[] array = new Vector3[3] { vector, vector2, position };
		card.ShowCard();
		jouster.m_initialActor.Show();
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
			jouster.m_effectsPendingFinish--;
			DriftJouster(jouster);
		};
		iTween.Timer(card.gameObject, iTween.Hash("delay", delaySec, "time", showSec, "oncomplete", action));
	}

	private void PlayNoJousterSpell(Player player)
	{
		ZoneDeck deckZone = player.GetDeckZone();
		Spell spell2 = UnityEngine.Object.Instantiate(m_NoJousterSpellPrefab);
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

	private void DriftJouster(Jouster jouster)
	{
		Card card = jouster.m_card;
		Vector3 position = card.transform.position;
		float z = jouster.m_initialActor.GetMeshRenderer().bounds.size.z;
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

	private IEnumerator Joust()
	{
		if (m_friendlyJouster != null)
		{
			float revealSec = m_RevealTime + GetRandomSec();
			RevealJouster(m_friendlyJouster, revealSec);
		}
		if (m_opponentJouster != null)
		{
			float revealSec2 = m_RevealTime + GetRandomSec();
			RevealJouster(m_opponentJouster, revealSec2);
		}
		if (m_sourceJouster != null)
		{
			while (IsJousterBusy(m_friendlyJouster) || IsJousterBusy(m_opponentJouster))
			{
				yield return null;
			}
			PlaySpellOnActor(spellPrefab: (m_joustType != 1) ? ((m_sourceJouster == m_winningJouster) ? m_WinnerSpellPrefab : m_LoserSpellPrefab) : ((!m_sourceJouster.m_player.IsFriendlySide()) ? m_LoserSpellPrefab : ((m_sourceJouster == m_winningJouster) ? m_WinnerSpellPrefab : m_LoserSpellPrefab)), jouster: m_sourceJouster, actor: m_sourceJouster.m_revealedActor);
		}
		if (m_friendlyJouster != null || m_opponentJouster != null)
		{
			iTween.Timer(base.gameObject, iTween.Hash("time", m_HoldTime));
		}
		while (IsJousterBusy(m_friendlyJouster) || IsJousterBusy(m_opponentJouster) || iTween.HasTween(base.gameObject))
		{
			yield return null;
		}
	}

	private void RevealJouster(Jouster jouster, float revealSec)
	{
		if (m_joustType == 1 && !m_sourceJouster.m_player.IsFriendlySide())
		{
			return;
		}
		jouster.m_effectsPendingFinish++;
		Card card = jouster.m_card;
		Actor hiddenActor = jouster.m_initialActor;
		Actor revealedActor = jouster.m_revealedActor;
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
			jouster.m_effectsPendingFinish--;
		};
		iTween.Timer(card.gameObject, iTween.Hash("time", revealSec, "onupdate", action, "oncomplete", action2));
	}

	private IEnumerator HideJousters()
	{
		if (!string.IsNullOrEmpty(m_HideStingerPrefab))
		{
			SoundManager.Get().LoadAndPlay(m_HideStingerPrefab);
		}
		if (m_friendlyJouster != null)
		{
			float randomSec = GetRandomSec();
			float hideSec = m_HideTime + GetRandomSec();
			HideJouster(m_friendlyJouster, randomSec, hideSec);
		}
		if (m_opponentJouster != null)
		{
			float randomSec2 = GetRandomSec();
			float hideSec2 = m_HideTime + GetRandomSec();
			HideJouster(m_opponentJouster, randomSec2, hideSec2);
		}
		while (IsJousterBusy(m_friendlyJouster) || IsJousterBusy(m_opponentJouster))
		{
			yield return null;
		}
	}

	private void HideJouster(Jouster jouster, float delaySec, float hideSec)
	{
		jouster.m_effectsPendingFinish++;
		Card card = jouster.m_card;
		ZoneDeck deck = jouster.m_player.GetDeckZone();
		Vector3 center = deck.GetThicknessForLayout().GetMeshRenderer().bounds.center;
		float num = 0.5f * hideSec;
		Vector3 position = card.transform.position;
		Vector3 vector = center + Card.ABOVE_DECK_OFFSET;
		Vector3 vector2 = center + Card.IN_DECK_OFFSET;
		Vector3 iN_DECK_ANGLES = Card.IN_DECK_ANGLES;
		if (m_joustType == 1 && !m_sourceJouster.m_player.IsFriendlySide())
		{
			iN_DECK_ANGLES.x *= -1f;
		}
		Vector3 iN_DECK_SCALE = Card.IN_DECK_SCALE;
		Vector3[] array = new Vector3[3] { position, vector, vector2 };
		iTween.MoveTo(card.gameObject, iTween.Hash("path", array, "delay", delaySec, "time", hideSec, "easetype", iTween.EaseType.easeInOutQuad));
		iTween.RotateTo(card.gameObject, iTween.Hash("rotation", iN_DECK_ANGLES, "delay", delaySec, "time", num, "easetype", iTween.EaseType.easeInOutCubic));
		iTween.ScaleTo(card.gameObject, iTween.Hash("scale", iN_DECK_SCALE, "delay", delaySec + num, "time", num, "easetype", iTween.EaseType.easeInOutQuint));
		if (!string.IsNullOrEmpty(m_HideSoundPrefab))
		{
			SoundManager.Get().LoadAndPlay(m_HideSoundPrefab);
		}
		Action<object> action = delegate
		{
			jouster.m_effectsPendingFinish--;
			jouster.m_initialActor.GetCard().HideCard();
			deck.InsertCard(jouster.m_deckIndex, card);
			deck.UpdateLayout();
			deck.SetSuppressEmotes(suppress: false);
		};
		iTween.Timer(card.gameObject, iTween.Hash("delay", delaySec, "time", hideSec, "oncomplete", action));
	}

	private void DestroyJousters()
	{
		if (m_friendlyJouster != null)
		{
			DestroyJouster(m_friendlyJouster);
			m_friendlyJouster = null;
		}
		if (m_opponentJouster != null)
		{
			DestroyJouster(m_opponentJouster);
			m_opponentJouster = null;
		}
	}

	private void DestroyJouster(Jouster jouster)
	{
		if (jouster != null)
		{
			jouster.m_card.SetInputEnabled(enabled: true);
			jouster.m_initialActor.Destroy();
			jouster.m_revealedActor.Destroy();
		}
	}

	private float GetRandomSec()
	{
		return UnityEngine.Random.Range(m_RandomSecMin, m_RandomSecMax);
	}

	private bool PlaySpellOnActor(Jouster jouster, Actor actor, Spell spellPrefab)
	{
		if (!spellPrefab)
		{
			return false;
		}
		jouster.m_effectsPendingFinish++;
		Card card = actor.GetCard();
		Spell spell2 = UnityEngine.Object.Instantiate(spellPrefab);
		spell2.transform.parent = actor.transform;
		spell2.AddFinishedCallback(delegate
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

	private bool IsJousterBusy(Jouster jouster)
	{
		if (jouster == null)
		{
			return false;
		}
		return jouster.m_effectsPendingFinish > 0;
	}
}
