using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020006D8 RID: 1752
public class BrawlSpell : Spell
{
	// Token: 0x060061FC RID: 25084 RVA: 0x001FFC71 File Offset: 0x001FDE71
	protected override void OnAction(SpellStateType prevStateType)
	{
		if (this.m_targets.Count > 0)
		{
			this.m_survivorCard = this.FindSurvivor();
			this.StartJumpIns();
			return;
		}
		this.OnSpellFinished();
		this.OnStateFinished();
	}

	// Token: 0x060061FD RID: 25085 RVA: 0x001FFCA0 File Offset: 0x001FDEA0
	private Card FindSurvivor()
	{
		foreach (GameObject gameObject in this.m_targets)
		{
			bool flag = true;
			Card component = gameObject.GetComponent<Card>();
			foreach (PowerTask powerTask in this.m_taskList.GetTaskList())
			{
				Network.PowerHistory power = powerTask.GetPower();
				if (power.Type == Network.PowerType.TAG_CHANGE)
				{
					Network.HistTagChange histTagChange = power as Network.HistTagChange;
					if (histTagChange.Tag == 360 && histTagChange.Value == 1)
					{
						Entity entity = GameState.Get().GetEntity(histTagChange.Entity);
						if (entity == null)
						{
							Debug.LogWarning(string.Format("{0}.FindSurvivor() - WARNING trying to get entity with id {1} but there is no entity with that id", this, histTagChange.Entity));
						}
						else if (component == entity.GetCard())
						{
							flag = false;
							break;
						}
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

	// Token: 0x060061FE RID: 25086 RVA: 0x001FFDC4 File Offset: 0x001FDFC4
	private void StartJumpIns()
	{
		this.m_jumpsPending = this.m_targets.Count;
		List<Card> list = new List<Card>(this.m_jumpsPending);
		foreach (GameObject gameObject in this.m_targets)
		{
			Card component = gameObject.GetComponent<Card>();
			list.Add(component);
		}
		float num = 0f;
		while (list.Count > 0)
		{
			int index = UnityEngine.Random.Range(0, list.Count);
			Card card = list[index];
			list.RemoveAt(index);
			this.StartJumpIn(card, ref num);
		}
	}

	// Token: 0x060061FF RID: 25087 RVA: 0x001FFE74 File Offset: 0x001FE074
	private void StartJumpIn(Card card, ref float startSec)
	{
		float num = UnityEngine.Random.Range(this.m_MinJumpInDelay, this.m_MaxJumpInDelay);
		base.StartCoroutine(this.JumpIn(card, startSec + num));
		startSec += num;
	}

	// Token: 0x06006200 RID: 25088 RVA: 0x001FFEAB File Offset: 0x001FE0AB
	private IEnumerator JumpIn(Card card, float delaySec)
	{
		yield return new WaitForSeconds(delaySec);
		Vector3[] array = new Vector3[]
		{
			card.transform.position,
			default(Vector3),
			base.transform.position
		};
		array[1] = 0.5f * (array[0] + array[2]);
		float num = UnityEngine.Random.Range(this.m_MinJumpHeight, this.m_MaxJumpHeight);
		Vector3[] array2 = array;
		int num2 = 1;
		array2[num2].y = array2[num2].y + num;
		Hashtable args = iTween.Hash(new object[]
		{
			"path",
			array,
			"orienttopath",
			true,
			"time",
			this.m_JumpInDuration,
			"easetype",
			this.m_JumpInEaseType,
			"oncomplete",
			"OnJumpInComplete",
			"oncompletetarget",
			base.gameObject,
			"oncompleteparams",
			card
		});
		iTween.MoveTo(card.gameObject, args);
		if (this.m_JumpInSoundPrefab != null)
		{
			base.StartCoroutine(this.LoadAndPlaySound(this.m_JumpInSoundPrefab, this.m_JumpInSoundDelay));
		}
		yield break;
	}

	// Token: 0x06006201 RID: 25089 RVA: 0x001FFEC8 File Offset: 0x001FE0C8
	private void OnJumpInComplete(Card targetCard)
	{
		targetCard.HideCard();
		this.m_jumpsPending--;
		if (this.m_jumpsPending > 0)
		{
			return;
		}
		base.StartCoroutine(this.Hold());
	}

	// Token: 0x06006202 RID: 25090 RVA: 0x001FFEF5 File Offset: 0x001FE0F5
	private IEnumerator Hold()
	{
		yield return new WaitForSeconds(this.m_HoldTime);
		this.StartJumpOuts();
		yield break;
	}

	// Token: 0x06006203 RID: 25091 RVA: 0x001FFF04 File Offset: 0x001FE104
	private void StartJumpOuts()
	{
		this.m_jumpsPending = this.m_targets.Count - 1;
		List<int> list = new List<int>();
		List<int> list2 = new List<int>();
		float num = 0f;
		bool flag = true;
		for (int i = 0; i < this.m_targets.Count; i++)
		{
			Card component = this.m_targets[i].GetComponent<Card>();
			if (!(component == this.m_survivorCard))
			{
				GameObject freeBone;
				if (flag)
				{
					freeBone = this.GetFreeBone(this.m_LeftJumpOutBones, list);
					if (freeBone == null)
					{
						list.Clear();
						freeBone = this.GetFreeBone(this.m_LeftJumpOutBones, list);
					}
				}
				else
				{
					freeBone = this.GetFreeBone(this.m_RightJumpOutBones, list2);
					if (freeBone == null)
					{
						list2.Clear();
						freeBone = this.GetFreeBone(this.m_RightJumpOutBones, list2);
					}
				}
				float num2 = UnityEngine.Random.Range(this.m_MinJumpOutDelay, this.m_MaxJumpOutDelay);
				base.StartCoroutine(this.JumpOut(component, num + num2, freeBone.transform.position));
				num += num2;
				flag = !flag;
			}
		}
	}

	// Token: 0x06006204 RID: 25092 RVA: 0x00200020 File Offset: 0x001FE220
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
		int index = UnityEngine.Random.Range(0, list.Count - 1);
		int num = list[index];
		usedBoneIndexes.Add(num);
		return boneList[num];
	}

	// Token: 0x06006205 RID: 25093 RVA: 0x00200083 File Offset: 0x001FE283
	private IEnumerator JumpOut(Card card, float delaySec, Vector3 destPos)
	{
		yield return new WaitForSeconds(delaySec);
		card.transform.rotation = Quaternion.identity;
		card.ShowCard();
		Vector3[] array = new Vector3[]
		{
			card.transform.position,
			default(Vector3),
			destPos
		};
		array[1] = 0.5f * (array[0] + array[2]);
		float num = UnityEngine.Random.Range(this.m_MinJumpHeight, this.m_MaxJumpHeight);
		Vector3[] array2 = array;
		int num2 = 1;
		array2[num2].y = array2[num2].y + num;
		Hashtable args = iTween.Hash(new object[]
		{
			"path",
			array,
			"time",
			this.m_JumpOutDuration,
			"easetype",
			this.m_JumpOutEaseType,
			"oncomplete",
			"OnJumpOutComplete",
			"oncompletetarget",
			base.gameObject,
			"oncompleteparams",
			card
		});
		iTween.MoveTo(card.gameObject, args);
		if (this.m_JumpOutSoundPrefab != null)
		{
			base.StartCoroutine(this.LoadAndPlaySound(this.m_JumpOutSoundPrefab, this.m_JumpOutSoundDelay));
		}
		if (this.m_LandSoundPrefab != null)
		{
			base.StartCoroutine(this.LoadAndPlaySound(this.m_LandSoundPrefab, this.m_LandSoundDelay));
		}
		yield break;
	}

	// Token: 0x06006206 RID: 25094 RVA: 0x002000A7 File Offset: 0x001FE2A7
	private void OnJumpOutComplete(Card targetCard)
	{
		this.m_jumpsPending--;
		if (this.m_jumpsPending > 0)
		{
			return;
		}
		base.ActivateState(SpellStateType.DEATH);
		base.StartCoroutine(this.SurvivorHold());
	}

	// Token: 0x06006207 RID: 25095 RVA: 0x002000D5 File Offset: 0x001FE2D5
	private IEnumerator SurvivorHold()
	{
		this.m_survivorCard.transform.rotation = Quaternion.identity;
		this.m_survivorCard.ShowCard();
		yield return new WaitForSeconds(this.m_SurvivorHoldDuration);
		if (this.IsSurvivorAlone())
		{
			this.m_survivorCard.GetZone().UpdateLayout();
		}
		this.OnSpellFinished();
		this.OnStateFinished();
		yield break;
	}

	// Token: 0x06006208 RID: 25096 RVA: 0x002000E4 File Offset: 0x001FE2E4
	private bool IsSurvivorAlone()
	{
		Zone zone = this.m_survivorCard.GetZone();
		foreach (GameObject gameObject in this.m_targets)
		{
			Card component = gameObject.GetComponent<Card>();
			if (!(component == this.m_survivorCard) && component.GetZone() == zone)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06006209 RID: 25097 RVA: 0x00200164 File Offset: 0x001FE364
	private IEnumerator LoadAndPlaySound(AudioSource prefab, float delaySec)
	{
		AudioSource source = UnityEngine.Object.Instantiate<AudioSource>(prefab);
		source.transform.parent = base.transform;
		TransformUtil.Identity(source);
		yield return new WaitForSeconds(delaySec);
		SoundManager.Get().PlayPreloaded(source);
		yield break;
	}

	// Token: 0x04005182 RID: 20866
	public float m_MinJumpHeight = 1.5f;

	// Token: 0x04005183 RID: 20867
	public float m_MaxJumpHeight = 2.5f;

	// Token: 0x04005184 RID: 20868
	public float m_MinJumpInDelay = 0.1f;

	// Token: 0x04005185 RID: 20869
	public float m_MaxJumpInDelay = 0.2f;

	// Token: 0x04005186 RID: 20870
	public float m_JumpInDuration = 1.5f;

	// Token: 0x04005187 RID: 20871
	public iTween.EaseType m_JumpInEaseType = iTween.EaseType.linear;

	// Token: 0x04005188 RID: 20872
	public float m_HoldTime = 0.1f;

	// Token: 0x04005189 RID: 20873
	public float m_MinJumpOutDelay = 0.1f;

	// Token: 0x0400518A RID: 20874
	public float m_MaxJumpOutDelay = 0.2f;

	// Token: 0x0400518B RID: 20875
	public float m_JumpOutDuration = 1.5f;

	// Token: 0x0400518C RID: 20876
	public iTween.EaseType m_JumpOutEaseType = iTween.EaseType.easeOutBounce;

	// Token: 0x0400518D RID: 20877
	public float m_SurvivorHoldDuration = 0.5f;

	// Token: 0x0400518E RID: 20878
	public List<GameObject> m_LeftJumpOutBones;

	// Token: 0x0400518F RID: 20879
	public List<GameObject> m_RightJumpOutBones;

	// Token: 0x04005190 RID: 20880
	public AudioSource m_JumpInSoundPrefab;

	// Token: 0x04005191 RID: 20881
	public float m_JumpInSoundDelay;

	// Token: 0x04005192 RID: 20882
	public AudioSource m_JumpOutSoundPrefab;

	// Token: 0x04005193 RID: 20883
	public float m_JumpOutSoundDelay;

	// Token: 0x04005194 RID: 20884
	public AudioSource m_LandSoundPrefab;

	// Token: 0x04005195 RID: 20885
	public float m_LandSoundDelay;

	// Token: 0x04005196 RID: 20886
	private int m_jumpsPending;

	// Token: 0x04005197 RID: 20887
	private Card m_survivorCard;
}
