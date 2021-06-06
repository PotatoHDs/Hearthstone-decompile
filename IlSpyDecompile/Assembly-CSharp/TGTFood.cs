using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CustomEditClass]
public class TGTFood : MonoBehaviour
{
	[Serializable]
	public class FoodItem
	{
		public PlayMakerFSM m_FSM;
	}

	public bool m_Phone;

	public GameObject m_Triangle;

	public Animator m_TriangleAnimator;

	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_TriangleSoundPrefab;

	public int m_StartingFoodIndex;

	public float m_NewFoodDelay = 1f;

	public List<FoodItem> m_Food;

	public FoodItem m_Drink;

	private bool m_isAnimating;

	private int m_lastFoodIdx;

	private FoodItem m_CurrentFoodItem;

	private float m_phoneNextCheckTime;

	private void Start()
	{
		m_CurrentFoodItem = m_Food[m_StartingFoodIndex];
		m_lastFoodIdx = m_StartingFoodIndex;
		m_CurrentFoodItem.m_FSM.gameObject.SetActive(value: true);
		m_CurrentFoodItem.m_FSM.SendEvent("Birth");
		m_Drink.m_FSM.gameObject.SetActive(value: true);
		m_Drink.m_FSM.SendEvent("Birth");
		if (m_Phone)
		{
			m_Triangle.SetActive(value: false);
		}
	}

	private void Update()
	{
		HandleHits();
		if (m_Phone && !m_Triangle.activeSelf && !(Time.timeSinceLevelLoad < m_phoneNextCheckTime))
		{
			m_phoneNextCheckTime = Time.timeSinceLevelLoad + 0.25f;
			bool value = m_CurrentFoodItem.m_FSM.FsmVariables.FindFsmBool("isEmpty").Value;
			bool value2 = m_Drink.m_FSM.FsmVariables.FindFsmBool("isEmpty").Value;
			if (value && value2 && !m_isAnimating)
			{
				m_Triangle.SetActive(value: true);
			}
			else if (m_Triangle.activeSelf)
			{
				m_Triangle.SetActive(value: false);
			}
		}
	}

	private void HandleHits()
	{
		if (UniversalInputManager.Get().GetMouseButtonUp(0) && IsOver(m_Triangle) && !m_isAnimating)
		{
			StartCoroutine(RingTheBell());
		}
	}

	private IEnumerator RingTheBell()
	{
		if (m_Phone)
		{
			m_Triangle.SetActive(value: false);
		}
		m_isAnimating = true;
		bool foodEmpty = m_CurrentFoodItem.m_FSM.FsmVariables.FindFsmBool("isEmpty").Value;
		bool drinkEmpty = m_Drink.m_FSM.FsmVariables.FindFsmBool("isEmpty").Value;
		BellAnimation();
		if (foodEmpty)
		{
			m_CurrentFoodItem.m_FSM.SendEvent("Death");
		}
		if (drinkEmpty)
		{
			m_Drink.m_FSM.SendEvent("Death");
		}
		yield return new WaitForSeconds(m_NewFoodDelay);
		if (m_Phone)
		{
			m_Triangle.SetActive(value: false);
		}
		if (foodEmpty)
		{
			int num = UnityEngine.Random.Range(0, m_Food.Count);
			if (num == m_lastFoodIdx)
			{
				num = UnityEngine.Random.Range(0, m_Food.Count);
				if (num == m_lastFoodIdx)
				{
					num = m_lastFoodIdx - 1;
					if (num < 0)
					{
						num = 0;
					}
				}
			}
			m_lastFoodIdx = num;
			m_CurrentFoodItem = m_Food[num];
			m_CurrentFoodItem.m_FSM.gameObject.SetActive(value: true);
			m_CurrentFoodItem.m_FSM.SendEvent("Birth");
		}
		if (drinkEmpty)
		{
			m_Drink.m_FSM.SendEvent("Birth");
		}
		yield return new WaitForSeconds(m_NewFoodDelay);
		m_isAnimating = false;
	}

	private void BellAnimation()
	{
		if (!m_Phone)
		{
			m_TriangleAnimator.SetTrigger("Clicked");
		}
		if (!string.IsNullOrEmpty(m_TriangleSoundPrefab))
		{
			string triangleSoundPrefab = m_TriangleSoundPrefab;
			if (!string.IsNullOrEmpty(triangleSoundPrefab))
			{
				SoundManager.Get().LoadAndPlay(triangleSoundPrefab, m_Triangle);
			}
		}
	}

	private bool IsOver(GameObject go)
	{
		if (!go)
		{
			return false;
		}
		if (!InputUtil.IsPlayMakerMouseInputAllowed(go))
		{
			return false;
		}
		if (!UniversalInputManager.Get().InputIsOver(go))
		{
			return false;
		}
		return true;
	}
}
