using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000AF RID: 175
[CustomEditClass]
public class TGTArcheryTarget : MonoBehaviour
{
	// Token: 0x06000AFA RID: 2810 RVA: 0x00041108 File Offset: 0x0003F308
	private void Start()
	{
		this.m_arrows = new GameObject[this.m_MaxArrows];
		for (int i = 0; i < this.m_MaxArrows; i++)
		{
			this.m_arrows[i] = UnityEngine.Object.Instantiate<GameObject>(this.m_Arrow);
			this.m_arrows[i].transform.position = new Vector3(-15f, -15f, -15f);
			this.m_arrows[i].transform.parent = this.m_TargetRoot.transform;
			this.m_arrows[i].SetActive(false);
		}
		this.m_arrows[0].SetActive(true);
		this.m_arrows[0].transform.position = this.m_ArrowBone01.transform.position;
		this.m_arrows[0].transform.rotation = this.m_ArrowBone01.transform.rotation;
		this.m_arrows[1].SetActive(true);
		this.m_arrows[1].transform.position = this.m_ArrowBone02.transform.position;
		this.m_arrows[1].transform.rotation = this.m_ArrowBone02.transform.rotation;
		this.m_lastArrow = 2;
		this.m_targetRadius = Vector3.Distance(this.m_CenterBone.position, this.m_OuterRadiusBone.position);
		this.m_bullseyeRadius = Vector3.Distance(this.m_BullseyeCenterBone.position, this.m_BullseyeRadiusBone.position);
		this.m_AvailableTargetDummyArrows = new List<int>();
		for (int j = 0; j < this.m_TargetDummyArrows.Count; j++)
		{
			this.m_AvailableTargetDummyArrows.Add(j);
		}
		this.m_SplitArrow.SetActive(false);
	}

	// Token: 0x06000AFB RID: 2811 RVA: 0x000412C1 File Offset: 0x0003F4C1
	private void Update()
	{
		this.HandleHits();
	}

	// Token: 0x06000AFC RID: 2812 RVA: 0x000412C9 File Offset: 0x0003F4C9
	private void HandleHits()
	{
		if (UniversalInputManager.Get().GetMouseButtonDown(0) && this.IsOver(this.m_Collider01))
		{
			this.HnadleFireArrow();
		}
	}

	// Token: 0x06000AFD RID: 2813 RVA: 0x000412EC File Offset: 0x0003F4EC
	private void HnadleFireArrow()
	{
		if (this.m_clearingArrows)
		{
			return;
		}
		this.m_ArrowCount++;
		if (this.m_ArrowCount > this.m_Levelup)
		{
			this.m_ArrowCount = 0;
			this.m_MaxRandomOffset *= 0.95f;
			this.m_BullseyePercent += 4;
		}
		if (UnityEngine.Random.Range(0, 100) < this.m_TargetDummyPercent && this.m_AvailableTargetDummyArrows.Count > 0)
		{
			this.HitTargetDummy();
			return;
		}
		Ray ray = Camera.main.ScreenPointToRay(InputCollection.GetMousePosition());
		bool bullseye = false;
		RaycastHit raycastHit;
		if (this.m_BoxColliderBullseye.Raycast(ray, out raycastHit, 100f))
		{
			bullseye = true;
		}
		if (!this.m_BoxCollider02.Raycast(ray, out raycastHit, 100f))
		{
			return;
		}
		this.m_lastArrow++;
		if (this.m_lastArrow >= this.m_MaxArrows)
		{
			this.m_lastArrow = 0;
			base.StartCoroutine(this.ClearArrows());
			return;
		}
		GameObject gameObject = this.m_arrows[this.m_lastArrow];
		TGTArrow component = gameObject.GetComponent<TGTArrow>();
		this.FireArrow(component, raycastHit.point, bullseye);
		gameObject.transform.eulerAngles = raycastHit.normal;
		this.ImpactTarget();
	}

	// Token: 0x06000AFE RID: 2814 RVA: 0x00041414 File Offset: 0x0003F614
	private IEnumerator ClearArrows()
	{
		this.m_clearingArrows = true;
		foreach (GameObject gameObject in this.m_arrows)
		{
			if (gameObject.activeSelf)
			{
				gameObject.SetActive(false);
				this.m_TargetPhysics.GetComponent<Rigidbody>().angularVelocity = new Vector3(UnityEngine.Random.Range(this.m_HitIntensity * -0.25f, this.m_HitIntensity * -0.5f), 0f, 0f);
				this.PlaySound(this.m_RemoveArrowSoundPrefab);
				yield return new WaitForSeconds(0.2f);
			}
		}
		GameObject[] array = null;
		yield return new WaitForSeconds(0.2f);
		if (this.m_SplitArrow.activeSelf)
		{
			this.m_SplitArrow.SetActive(false);
			this.m_TargetPhysics.GetComponent<Rigidbody>().angularVelocity = new Vector3(UnityEngine.Random.Range(this.m_HitIntensity * -0.25f, this.m_HitIntensity * -0.5f), 0f, 0f);
			this.PlaySound(this.m_RemoveArrowSoundPrefab);
		}
		this.m_lastArrowWasBullseye = false;
		this.m_lastBullseyeArrow = null;
		this.m_clearingArrows = false;
		yield break;
	}

	// Token: 0x06000AFF RID: 2815 RVA: 0x00041424 File Offset: 0x0003F624
	private void FireArrow(TGTArrow arrow, Vector3 hitPosition, bool bullseye)
	{
		arrow.transform.position = hitPosition;
		bool flag = false;
		if (Time.timeSinceLevelLoad > this.m_lastClickTime + 0.8f)
		{
			flag = true;
		}
		this.m_lastClickTime = Time.timeSinceLevelLoad;
		int num = this.m_BullseyePercent;
		if (flag)
		{
			num *= 2;
		}
		if (num > 80)
		{
			num = 80;
		}
		if (bullseye && UnityEngine.Random.Range(0, 100) < num)
		{
			int num2 = 2;
			if (flag)
			{
				num2 = 8;
			}
			if (this.m_lastArrowWasBullseye && !this.m_SplitArrow.activeSelf && bullseye && UnityEngine.Random.Range(0, 100) < num2)
			{
				this.m_SplitArrow.transform.position = this.m_lastBullseyeArrow.transform.position;
				this.m_SplitArrow.transform.rotation = this.m_lastBullseyeArrow.transform.rotation;
				TGTArrow component = this.m_SplitArrow.GetComponent<TGTArrow>();
				TGTArrow component2 = this.m_lastBullseyeArrow.GetComponent<TGTArrow>();
				this.m_SplitArrow.SetActive(true);
				component.FireArrow(false);
				component.Bullseye();
				this.PlaySound(this.m_SplitArrowSoundPrefab);
				component.m_ArrowRoot.transform.position = component2.m_ArrowRoot.transform.position;
				component.m_ArrowRoot.transform.rotation = component2.m_ArrowRoot.transform.rotation;
				this.m_lastBullseyeArrow.SetActive(false);
				this.m_lastArrowWasBullseye = false;
				this.m_lastBullseyeArrow = null;
				return;
			}
			arrow.gameObject.SetActive(true);
			arrow.Bullseye();
			this.PlaySound(this.m_HitBullseyeSoundPrefab);
			arrow.m_ArrowRoot.transform.localPosition = Vector3.zero;
			this.m_lastBullseyeArrow = arrow.gameObject;
			this.m_lastArrowWasBullseye = true;
			return;
		}
		else
		{
			this.m_lastArrowWasBullseye = false;
			this.m_lastBullseyeArrow = null;
			arrow.gameObject.SetActive(true);
			if (bullseye)
			{
				Vector2 vector = UnityEngine.Random.insideUnitCircle.normalized * this.m_bullseyeRadius * 2f;
				arrow.m_ArrowRoot.transform.localPosition = new Vector3(vector.x, vector.y, 0f);
				arrow.FireArrow(true);
				this.PlaySound(this.m_HitTargetSoundPrefab);
				return;
			}
			Vector2 vector2 = UnityEngine.Random.insideUnitCircle * UnityEngine.Random.Range(0f, this.m_MaxRandomOffset);
			arrow.m_ArrowRoot.transform.localPosition = new Vector3(vector2.x, vector2.y, 0f);
			if (Vector3.Distance(arrow.m_ArrowRoot.transform.position, this.m_CenterBone.position) > this.m_targetRadius)
			{
				arrow.m_ArrowRoot.transform.localPosition = Vector3.zero;
			}
			if (Vector3.Distance(arrow.m_ArrowRoot.transform.position, this.m_BullseyeCenterBone.position) < this.m_bullseyeRadius)
			{
				arrow.m_ArrowRoot.transform.localPosition = Vector3.zero;
			}
			arrow.FireArrow(true);
			this.PlaySound(this.m_HitTargetSoundPrefab);
			return;
		}
	}

	// Token: 0x06000B00 RID: 2816 RVA: 0x0004172C File Offset: 0x0003F92C
	private void HitTargetDummy()
	{
		int index = 0;
		if (this.m_AvailableTargetDummyArrows.Count > 1)
		{
			index = UnityEngine.Random.Range(0, this.m_AvailableTargetDummyArrows.Count);
		}
		TGTArrow tgtarrow = this.m_TargetDummyArrows[this.m_AvailableTargetDummyArrows[index]];
		tgtarrow.gameObject.SetActive(true);
		tgtarrow.FireArrow(false);
		TGTTargetDummy.Get().ArrowHit();
		this.PlaySound(this.m_HitTargetDummySoundPrefab);
		if (this.m_AvailableTargetDummyArrows.Count > 1)
		{
			this.m_AvailableTargetDummyArrows.RemoveAt(index);
			return;
		}
		this.m_AvailableTargetDummyArrows.Clear();
	}

	// Token: 0x06000B01 RID: 2817 RVA: 0x000417C0 File Offset: 0x0003F9C0
	private void ImpactTarget()
	{
		this.m_TargetPhysics.GetComponent<Rigidbody>().angularVelocity = new Vector3(UnityEngine.Random.Range(this.m_HitIntensity * 0.25f, this.m_HitIntensity), 0f, 0f);
	}

	// Token: 0x06000B02 RID: 2818 RVA: 0x000417F8 File Offset: 0x0003F9F8
	private void PlaySound(string soundPrefab)
	{
		if (!string.IsNullOrEmpty(soundPrefab) && !string.IsNullOrEmpty(soundPrefab))
		{
			SoundManager.Get().LoadAndPlay(soundPrefab, base.gameObject);
		}
	}

	// Token: 0x06000B03 RID: 2819 RVA: 0x000402EE File Offset: 0x0003E4EE
	private bool IsOver(GameObject go)
	{
		return go && InputUtil.IsPlayMakerMouseInputAllowed(go) && UniversalInputManager.Get().InputIsOver(go);
	}

	// Token: 0x04000712 RID: 1810
	public int m_BullseyePercent = 5;

	// Token: 0x04000713 RID: 1811
	public int m_TargetDummyPercent = 1;

	// Token: 0x04000714 RID: 1812
	public float m_MaxRandomOffset = 0.3f;

	// Token: 0x04000715 RID: 1813
	public int m_Levelup = 50;

	// Token: 0x04000716 RID: 1814
	public GameObject m_Collider01;

	// Token: 0x04000717 RID: 1815
	public GameObject m_TargetPhysics;

	// Token: 0x04000718 RID: 1816
	public GameObject m_TargetRoot;

	// Token: 0x04000719 RID: 1817
	public GameObject m_Arrow;

	// Token: 0x0400071A RID: 1818
	public GameObject m_SplitArrow;

	// Token: 0x0400071B RID: 1819
	public float m_HitIntensity;

	// Token: 0x0400071C RID: 1820
	public int m_MaxArrows;

	// Token: 0x0400071D RID: 1821
	public List<TGTArrow> m_TargetDummyArrows;

	// Token: 0x0400071E RID: 1822
	public GameObject m_ArrowBone01;

	// Token: 0x0400071F RID: 1823
	public GameObject m_ArrowBone02;

	// Token: 0x04000720 RID: 1824
	public BoxCollider m_BoxCollider01;

	// Token: 0x04000721 RID: 1825
	public BoxCollider m_BoxCollider02;

	// Token: 0x04000722 RID: 1826
	public BoxCollider m_BoxColliderBullseye;

	// Token: 0x04000723 RID: 1827
	public Transform m_CenterBone;

	// Token: 0x04000724 RID: 1828
	public Transform m_OuterRadiusBone;

	// Token: 0x04000725 RID: 1829
	public Transform m_BullseyeCenterBone;

	// Token: 0x04000726 RID: 1830
	public Transform m_BullseyeRadiusBone;

	// Token: 0x04000727 RID: 1831
	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_HitTargetSoundPrefab;

	// Token: 0x04000728 RID: 1832
	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_HitBullseyeSoundPrefab;

	// Token: 0x04000729 RID: 1833
	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_HitTargetDummySoundPrefab;

	// Token: 0x0400072A RID: 1834
	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_SplitArrowSoundPrefab;

	// Token: 0x0400072B RID: 1835
	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_RemoveArrowSoundPrefab;

	// Token: 0x0400072C RID: 1836
	private GameObject[] m_arrows;

	// Token: 0x0400072D RID: 1837
	private int m_lastArrow = 1;

	// Token: 0x0400072E RID: 1838
	private float m_targetRadius;

	// Token: 0x0400072F RID: 1839
	private float m_bullseyeRadius;

	// Token: 0x04000730 RID: 1840
	private int m_ArrowCount;

	// Token: 0x04000731 RID: 1841
	private List<int> m_AvailableTargetDummyArrows;

	// Token: 0x04000732 RID: 1842
	private GameObject m_lastBullseyeArrow;

	// Token: 0x04000733 RID: 1843
	private bool m_lastArrowWasBullseye;

	// Token: 0x04000734 RID: 1844
	private bool m_clearingArrows;

	// Token: 0x04000735 RID: 1845
	private float m_lastClickTime;
}
