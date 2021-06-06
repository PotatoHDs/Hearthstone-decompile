using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000B1 RID: 177
[CustomEditClass]
public class TGTFood : MonoBehaviour
{
	// Token: 0x06000B0B RID: 2827 RVA: 0x00041A58 File Offset: 0x0003FC58
	private void Start()
	{
		this.m_CurrentFoodItem = this.m_Food[this.m_StartingFoodIndex];
		this.m_lastFoodIdx = this.m_StartingFoodIndex;
		this.m_CurrentFoodItem.m_FSM.gameObject.SetActive(true);
		this.m_CurrentFoodItem.m_FSM.SendEvent("Birth");
		this.m_Drink.m_FSM.gameObject.SetActive(true);
		this.m_Drink.m_FSM.SendEvent("Birth");
		if (this.m_Phone)
		{
			this.m_Triangle.SetActive(false);
		}
	}

	// Token: 0x06000B0C RID: 2828 RVA: 0x00041AF4 File Offset: 0x0003FCF4
	private void Update()
	{
		this.HandleHits();
		if (!this.m_Phone || this.m_Triangle.activeSelf)
		{
			return;
		}
		if (Time.timeSinceLevelLoad < this.m_phoneNextCheckTime)
		{
			return;
		}
		this.m_phoneNextCheckTime = Time.timeSinceLevelLoad + 0.25f;
		bool value = this.m_CurrentFoodItem.m_FSM.FsmVariables.FindFsmBool("isEmpty").Value;
		bool value2 = this.m_Drink.m_FSM.FsmVariables.FindFsmBool("isEmpty").Value;
		if (value && value2 && !this.m_isAnimating)
		{
			this.m_Triangle.SetActive(true);
			return;
		}
		if (this.m_Triangle.activeSelf)
		{
			this.m_Triangle.SetActive(false);
		}
	}

	// Token: 0x06000B0D RID: 2829 RVA: 0x00041BAD File Offset: 0x0003FDAD
	private void HandleHits()
	{
		if (UniversalInputManager.Get().GetMouseButtonUp(0) && this.IsOver(this.m_Triangle) && !this.m_isAnimating)
		{
			base.StartCoroutine(this.RingTheBell());
		}
	}

	// Token: 0x06000B0E RID: 2830 RVA: 0x00041BDF File Offset: 0x0003FDDF
	private IEnumerator RingTheBell()
	{
		if (this.m_Phone)
		{
			this.m_Triangle.SetActive(false);
		}
		this.m_isAnimating = true;
		bool foodEmpty = this.m_CurrentFoodItem.m_FSM.FsmVariables.FindFsmBool("isEmpty").Value;
		bool drinkEmpty = this.m_Drink.m_FSM.FsmVariables.FindFsmBool("isEmpty").Value;
		this.BellAnimation();
		if (foodEmpty)
		{
			this.m_CurrentFoodItem.m_FSM.SendEvent("Death");
		}
		if (drinkEmpty)
		{
			this.m_Drink.m_FSM.SendEvent("Death");
		}
		yield return new WaitForSeconds(this.m_NewFoodDelay);
		if (this.m_Phone)
		{
			this.m_Triangle.SetActive(false);
		}
		if (foodEmpty)
		{
			int num = UnityEngine.Random.Range(0, this.m_Food.Count);
			if (num == this.m_lastFoodIdx)
			{
				num = UnityEngine.Random.Range(0, this.m_Food.Count);
				if (num == this.m_lastFoodIdx)
				{
					num = this.m_lastFoodIdx - 1;
					if (num < 0)
					{
						num = 0;
					}
				}
			}
			this.m_lastFoodIdx = num;
			this.m_CurrentFoodItem = this.m_Food[num];
			this.m_CurrentFoodItem.m_FSM.gameObject.SetActive(true);
			this.m_CurrentFoodItem.m_FSM.SendEvent("Birth");
		}
		if (drinkEmpty)
		{
			this.m_Drink.m_FSM.SendEvent("Birth");
		}
		yield return new WaitForSeconds(this.m_NewFoodDelay);
		this.m_isAnimating = false;
		yield break;
	}

	// Token: 0x06000B0F RID: 2831 RVA: 0x00041BF0 File Offset: 0x0003FDF0
	private void BellAnimation()
	{
		if (!this.m_Phone)
		{
			this.m_TriangleAnimator.SetTrigger("Clicked");
		}
		if (!string.IsNullOrEmpty(this.m_TriangleSoundPrefab))
		{
			string triangleSoundPrefab = this.m_TriangleSoundPrefab;
			if (!string.IsNullOrEmpty(triangleSoundPrefab))
			{
				SoundManager.Get().LoadAndPlay(triangleSoundPrefab, this.m_Triangle);
			}
		}
	}

	// Token: 0x06000B10 RID: 2832 RVA: 0x000402EE File Offset: 0x0003E4EE
	private bool IsOver(GameObject go)
	{
		return go && InputUtil.IsPlayMakerMouseInputAllowed(go) && UniversalInputManager.Get().InputIsOver(go);
	}

	// Token: 0x0400073A RID: 1850
	public bool m_Phone;

	// Token: 0x0400073B RID: 1851
	public GameObject m_Triangle;

	// Token: 0x0400073C RID: 1852
	public Animator m_TriangleAnimator;

	// Token: 0x0400073D RID: 1853
	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_TriangleSoundPrefab;

	// Token: 0x0400073E RID: 1854
	public int m_StartingFoodIndex;

	// Token: 0x0400073F RID: 1855
	public float m_NewFoodDelay = 1f;

	// Token: 0x04000740 RID: 1856
	public List<TGTFood.FoodItem> m_Food;

	// Token: 0x04000741 RID: 1857
	public TGTFood.FoodItem m_Drink;

	// Token: 0x04000742 RID: 1858
	private bool m_isAnimating;

	// Token: 0x04000743 RID: 1859
	private int m_lastFoodIdx;

	// Token: 0x04000744 RID: 1860
	private TGTFood.FoodItem m_CurrentFoodItem;

	// Token: 0x04000745 RID: 1861
	private float m_phoneNextCheckTime;

	// Token: 0x020013B8 RID: 5048
	[Serializable]
	public class FoodItem
	{
		// Token: 0x0400A79B RID: 42907
		public PlayMakerFSM m_FSM;
	}
}
