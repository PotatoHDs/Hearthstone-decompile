using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrawlSpell : Spell
{
	public float m_MinJumpHeight = 1.5f;

	public float m_MaxJumpHeight = 2.5f;

	public float m_MinJumpInDelay = 0.1f;

	public float m_MaxJumpInDelay = 0.2f;

	public float m_JumpInDuration = 1.5f;

	public iTween.EaseType m_JumpInEaseType = iTween.EaseType.linear;

	public float m_HoldTime = 0.1f;

	public float m_MinJumpOutDelay = 0.1f;

	public float m_MaxJumpOutDelay = 0.2f;

	public float m_JumpOutDuration = 1.5f;

	public iTween.EaseType m_JumpOutEaseType = iTween.EaseType.easeOutBounce;

	public float m_SurvivorHoldDuration = 0.5f;

	public List<GameObject> m_LeftJumpOutBones;

	public List<GameObject> m_RightJumpOutBones;

	public AudioSource m_JumpInSoundPrefab;

	public float m_JumpInSoundDelay;

	public AudioSource m_JumpOutSoundPrefab;

	public float m_JumpOutSoundDelay;

	public AudioSource m_LandSoundPrefab;

	public float m_LandSoundDelay;

	private int m_jumpsPending;

	private Card m_survivorCard;

	protected override void OnAction(SpellStateType prevStateType)
	{
		if (m_targets.Count > 0)
		{
			m_survivorCard = FindSurvivor();
			StartJumpIns();
		}
		else
		{
			OnSpellFinished();
			OnStateFinished();
		}
	}

	private Card FindSurvivor()
	{
		foreach (GameObject target in m_targets)
		{
			bool flag = true;
			Card component = target.GetComponent<Card>();
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
						Debug.LogWarning($"{this}.FindSurvivor() - WARNING trying to get entity with id {histTagChange.Entity} but there is no entity with that id");
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
				return component;
			}
		}
		return null;
	}

	private void StartJumpIns()
	{
		m_jumpsPending = m_targets.Count;
		List<Card> list = new List<Card>(m_jumpsPending);
		foreach (GameObject target in m_targets)
		{
			Card component = target.GetComponent<Card>();
			list.Add(component);
		}
		float startSec = 0f;
		while (list.Count > 0)
		{
			int index = Random.Range(0, list.Count);
			Card card = list[index];
			list.RemoveAt(index);
			StartJumpIn(card, ref startSec);
		}
	}

	private void StartJumpIn(Card card, ref float startSec)
	{
		float num = Random.Range(m_MinJumpInDelay, m_MaxJumpInDelay);
		StartCoroutine(JumpIn(card, startSec + num));
		startSec += num;
	}

	private IEnumerator JumpIn(Card card, float delaySec)
	{
		yield return new WaitForSeconds(delaySec);
		Vector3[] array = new Vector3[3];
		array[0] = card.transform.position;
		array[2] = base.transform.position;
		array[1] = 0.5f * (array[0] + array[2]);
		float num = Random.Range(m_MinJumpHeight, m_MaxJumpHeight);
		array[1].y += num;
		Hashtable args = iTween.Hash("path", array, "orienttopath", true, "time", m_JumpInDuration, "easetype", m_JumpInEaseType, "oncomplete", "OnJumpInComplete", "oncompletetarget", base.gameObject, "oncompleteparams", card);
		iTween.MoveTo(card.gameObject, args);
		if (m_JumpInSoundPrefab != null)
		{
			StartCoroutine(LoadAndPlaySound(m_JumpInSoundPrefab, m_JumpInSoundDelay));
		}
	}

	private void OnJumpInComplete(Card targetCard)
	{
		targetCard.HideCard();
		m_jumpsPending--;
		if (m_jumpsPending <= 0)
		{
			StartCoroutine(Hold());
		}
	}

	private IEnumerator Hold()
	{
		yield return new WaitForSeconds(m_HoldTime);
		StartJumpOuts();
	}

	private void StartJumpOuts()
	{
		m_jumpsPending = m_targets.Count - 1;
		List<int> list = new List<int>();
		List<int> list2 = new List<int>();
		float num = 0f;
		bool flag = true;
		for (int i = 0; i < m_targets.Count; i++)
		{
			Card component = m_targets[i].GetComponent<Card>();
			if (component == m_survivorCard)
			{
				continue;
			}
			GameObject gameObject = null;
			if (flag)
			{
				gameObject = GetFreeBone(m_LeftJumpOutBones, list);
				if (gameObject == null)
				{
					list.Clear();
					gameObject = GetFreeBone(m_LeftJumpOutBones, list);
				}
			}
			else
			{
				gameObject = GetFreeBone(m_RightJumpOutBones, list2);
				if (gameObject == null)
				{
					list2.Clear();
					gameObject = GetFreeBone(m_RightJumpOutBones, list2);
				}
			}
			float num2 = Random.Range(m_MinJumpOutDelay, m_MaxJumpOutDelay);
			StartCoroutine(JumpOut(component, num + num2, gameObject.transform.position));
			num += num2;
			flag = !flag;
		}
	}

	private GameObject GetFreeBone(List<GameObject> boneList, List<int> usedBoneIndexes)
	{
		List<int> list = new List<int>();
		for (int i = 0; i < boneList.Count; i++)
		{
			if (!usedBoneIndexes.Contains(i))
			{
				list.Add(i);
			}
		}
		if (list.Count == 0)
		{
			return null;
		}
		int index = Random.Range(0, list.Count - 1);
		int num = list[index];
		usedBoneIndexes.Add(num);
		return boneList[num];
	}

	private IEnumerator JumpOut(Card card, float delaySec, Vector3 destPos)
	{
		yield return new WaitForSeconds(delaySec);
		card.transform.rotation = Quaternion.identity;
		card.ShowCard();
		Vector3[] array = new Vector3[3];
		array[0] = card.transform.position;
		array[2] = destPos;
		array[1] = 0.5f * (array[0] + array[2]);
		float num = Random.Range(m_MinJumpHeight, m_MaxJumpHeight);
		array[1].y += num;
		Hashtable args = iTween.Hash("path", array, "time", m_JumpOutDuration, "easetype", m_JumpOutEaseType, "oncomplete", "OnJumpOutComplete", "oncompletetarget", base.gameObject, "oncompleteparams", card);
		iTween.MoveTo(card.gameObject, args);
		if (m_JumpOutSoundPrefab != null)
		{
			StartCoroutine(LoadAndPlaySound(m_JumpOutSoundPrefab, m_JumpOutSoundDelay));
		}
		if (m_LandSoundPrefab != null)
		{
			StartCoroutine(LoadAndPlaySound(m_LandSoundPrefab, m_LandSoundDelay));
		}
	}

	private void OnJumpOutComplete(Card targetCard)
	{
		m_jumpsPending--;
		if (m_jumpsPending <= 0)
		{
			ActivateState(SpellStateType.DEATH);
			StartCoroutine(SurvivorHold());
		}
	}

	private IEnumerator SurvivorHold()
	{
		m_survivorCard.transform.rotation = Quaternion.identity;
		m_survivorCard.ShowCard();
		yield return new WaitForSeconds(m_SurvivorHoldDuration);
		if (IsSurvivorAlone())
		{
			m_survivorCard.GetZone().UpdateLayout();
		}
		OnSpellFinished();
		OnStateFinished();
	}

	private bool IsSurvivorAlone()
	{
		Zone zone = m_survivorCard.GetZone();
		foreach (GameObject target in m_targets)
		{
			Card component = target.GetComponent<Card>();
			if (!(component == m_survivorCard) && component.GetZone() == zone)
			{
				return false;
			}
		}
		return true;
	}

	private IEnumerator LoadAndPlaySound(AudioSource prefab, float delaySec)
	{
		AudioSource source = Object.Instantiate(prefab);
		source.transform.parent = base.transform;
		TransformUtil.Identity(source);
		yield return new WaitForSeconds(delaySec);
		SoundManager.Get().PlayPreloaded(source);
	}
}
