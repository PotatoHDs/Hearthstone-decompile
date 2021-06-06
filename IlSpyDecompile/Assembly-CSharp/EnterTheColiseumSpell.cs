using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CustomEditClass]
public class EnterTheColiseumSpell : Spell
{
	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_SpellStartSoundPrefab;

	public float m_survivorLiftHeight = 2f;

	public float m_LiftTime = 0.5f;

	public float m_LiftOffset = 0.1f;

	public float m_DestroyMinionDelay = 0.5f;

	public float m_LowerDelay = 1.5f;

	public float m_LowerOffset = 0.05f;

	public float m_LowerTime = 0.7f;

	public float m_LightingFadeTime = 0.5f;

	public float m_CameraShakeMagnitude = 0.075f;

	public iTween.EaseType m_liftEaseType = iTween.EaseType.easeInQuart;

	public iTween.EaseType m_lowerEaseType = iTween.EaseType.easeOutCubic;

	public iTween.EaseType m_lightFadeEaseType = iTween.EaseType.easeOutCubic;

	public Spell m_survivorSpellPrefab;

	public Spell m_DustSpellPrefab;

	public bool m_survivorsMeetInMiddle = true;

	public Spell m_ImpactSpellPrefab;

	public string m_RaiseSoundName;

	private List<Card> m_survivorCards;

	private bool m_effectsPlaying;

	private int m_numSurvivorSpellsPlaying;

	protected override void OnAction(SpellStateType prevStateType)
	{
		base.OnAction(prevStateType);
		m_survivorCards = FindSurvivors();
		StartCoroutine(PerformActions());
	}

	private IEnumerator PerformActions()
	{
		m_effectsPlaying = true;
		foreach (Card survivorCard in m_survivorCards)
		{
			if (!(survivorCard == null))
			{
				survivorCard.SetDoNotSort(on: true);
				survivorCard.GetActor().SetUnlit();
				LiftCard(survivorCard);
				yield return new WaitForSeconds(m_LiftOffset);
			}
		}
		FullScreenFXMgr.Get().Vignette(1f, m_LightingFadeTime, m_lightFadeEaseType);
		if (!string.IsNullOrEmpty(m_SpellStartSoundPrefab))
		{
			SoundManager.Get().LoadAndPlay(m_SpellStartSoundPrefab);
		}
		PlayDustCloudSpell();
		yield return new WaitForSeconds(m_LiftTime);
		foreach (Card survivorCard2 in m_survivorCards)
		{
			if (!(survivorCard2 == null))
			{
				PlaySurvivorSpell(survivorCard2);
			}
		}
		yield return new WaitForSeconds(m_DestroyMinionDelay);
		OnSpellFinished();
		CameraShakeMgr.Shake(Camera.main, new Vector3(m_CameraShakeMagnitude, m_CameraShakeMagnitude, m_CameraShakeMagnitude), 0.75f);
		yield return new WaitForSeconds(m_LowerDelay);
		while (m_numSurvivorSpellsPlaying > 0)
		{
			yield return null;
		}
		foreach (Card survivorCard3 in m_survivorCards)
		{
			if (!(survivorCard3 == null))
			{
				Zone zone = survivorCard3.GetZone();
				if (zone is ZonePlay)
				{
					ZonePlay zonePlay = (ZonePlay)zone;
					LowerCard(survivorCard3.gameObject, zonePlay.GetCardPosition(survivorCard3));
					yield return new WaitForSeconds(m_LowerOffset);
				}
			}
		}
		FullScreenFXMgr.Get().StopVignette(m_LightingFadeTime, m_lightFadeEaseType);
		if (m_ImpactSpellPrefab != null)
		{
			foreach (Card survivorCard4 in m_survivorCards)
			{
				if (survivorCard4 == null)
				{
					continue;
				}
				Spell spell2 = UnityEngine.Object.Instantiate(m_ImpactSpellPrefab);
				spell2.transform.parent = survivorCard4.gameObject.transform;
				spell2.transform.localPosition = new Vector3(0f, 0f, 0f);
				spell2.AddStateFinishedCallback(delegate(Spell spell, SpellStateType prevStateType, object userData)
				{
					m_effectsPlaying = false;
					if (spell.GetActiveState() == SpellStateType.NONE)
					{
						UnityEngine.Object.Destroy(spell.gameObject);
					}
				});
				spell2.Activate();
				yield return new WaitForSeconds(m_LowerOffset);
			}
		}
		yield return new WaitForSeconds(m_LowerTime);
		foreach (Card survivorCard5 in m_survivorCards)
		{
			if (!(survivorCard5 == null))
			{
				survivorCard5.SetDoNotSort(on: false);
				survivorCard5.GetActor().SetLit();
			}
		}
		foreach (ZonePlay item in ZoneMgr.Get().FindZonesOfType<ZonePlay>())
		{
			item.UpdateLayout();
		}
		while (m_effectsPlaying)
		{
			yield return null;
		}
		OnStateFinished();
	}

	private void LiftCard(Card card)
	{
		GameObject gameObject = card.gameObject;
		Vector3 position = gameObject.transform.position;
		Vector3 position2 = card.GetZone().gameObject.transform.position;
		Hashtable args = iTween.Hash("time", m_LiftTime, "position", new Vector3(m_survivorsMeetInMiddle ? position2.x : position.x, position.y + m_survivorLiftHeight, position.z), "onstart", (Action<object>)delegate
		{
			SoundManager.Get().LoadAndPlay(m_RaiseSoundName);
		}, "easetype", m_liftEaseType);
		iTween.MoveTo(gameObject, args);
	}

	private void LowerCard(GameObject target, Vector3 finalPosition)
	{
		Hashtable args = iTween.Hash("time", m_LowerTime, "position", finalPosition, "easetype", m_lowerEaseType);
		iTween.MoveTo(target, args);
	}

	private List<Card> FindSurvivors()
	{
		List<Card> list = new List<Card>();
		foreach (GameObject target in m_targets)
		{
			Card component = target.GetComponent<Card>();
			bool flag = true;
			foreach (PowerTask task in m_taskList.GetTaskList())
			{
				Network.PowerHistory power = task.GetPower();
				if (power.Type != Network.PowerType.TAG_CHANGE)
				{
					continue;
				}
				Network.HistTagChange histTagChange = power as Network.HistTagChange;
				if (histTagChange.Tag == 360 && histTagChange.Value == 1)
				{
					Entity entity = GameState.Get().GetEntity(histTagChange.Entity);
					if (entity == null)
					{
						string format = $"{this}.FindSurvivors() - WARNING trying to get entity with id {histTagChange.Entity} but there is no entity with that id";
						Log.Power.PrintWarning(format);
					}
					else if (component == entity.GetCard())
					{
						flag = false;
						break;
					}
				}
			}
			if (flag)
			{
				list.Add(component);
			}
		}
		return list;
	}

	private void PlaySurvivorSpell(Card card)
	{
		if (m_survivorSpellPrefab == null)
		{
			return;
		}
		m_numSurvivorSpellsPlaying++;
		Spell spell2 = UnityEngine.Object.Instantiate(m_survivorSpellPrefab);
		spell2.transform.parent = card.GetActor().transform;
		spell2.AddFinishedCallback(delegate
		{
			m_numSurvivorSpellsPlaying--;
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
	}

	private void PlayDustCloudSpell()
	{
		if (m_DustSpellPrefab == null)
		{
			return;
		}
		Spell spell2 = UnityEngine.Object.Instantiate(m_DustSpellPrefab);
		spell2.AddStateFinishedCallback(delegate(Spell spell, SpellStateType prevStateType, object userData)
		{
			if (spell.GetActiveState() == SpellStateType.NONE)
			{
				UnityEngine.Object.Destroy(spell.gameObject);
			}
		});
		spell2.Activate();
	}
}
