using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CustomEditClass]
public class TGTArcheryTarget : MonoBehaviour
{
	public int m_BullseyePercent = 5;

	public int m_TargetDummyPercent = 1;

	public float m_MaxRandomOffset = 0.3f;

	public int m_Levelup = 50;

	public GameObject m_Collider01;

	public GameObject m_TargetPhysics;

	public GameObject m_TargetRoot;

	public GameObject m_Arrow;

	public GameObject m_SplitArrow;

	public float m_HitIntensity;

	public int m_MaxArrows;

	public List<TGTArrow> m_TargetDummyArrows;

	public GameObject m_ArrowBone01;

	public GameObject m_ArrowBone02;

	public BoxCollider m_BoxCollider01;

	public BoxCollider m_BoxCollider02;

	public BoxCollider m_BoxColliderBullseye;

	public Transform m_CenterBone;

	public Transform m_OuterRadiusBone;

	public Transform m_BullseyeCenterBone;

	public Transform m_BullseyeRadiusBone;

	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_HitTargetSoundPrefab;

	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_HitBullseyeSoundPrefab;

	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_HitTargetDummySoundPrefab;

	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_SplitArrowSoundPrefab;

	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_RemoveArrowSoundPrefab;

	private GameObject[] m_arrows;

	private int m_lastArrow = 1;

	private float m_targetRadius;

	private float m_bullseyeRadius;

	private int m_ArrowCount;

	private List<int> m_AvailableTargetDummyArrows;

	private GameObject m_lastBullseyeArrow;

	private bool m_lastArrowWasBullseye;

	private bool m_clearingArrows;

	private float m_lastClickTime;

	private void Start()
	{
		m_arrows = new GameObject[m_MaxArrows];
		for (int i = 0; i < m_MaxArrows; i++)
		{
			m_arrows[i] = Object.Instantiate(m_Arrow);
			m_arrows[i].transform.position = new Vector3(-15f, -15f, -15f);
			m_arrows[i].transform.parent = m_TargetRoot.transform;
			m_arrows[i].SetActive(value: false);
		}
		m_arrows[0].SetActive(value: true);
		m_arrows[0].transform.position = m_ArrowBone01.transform.position;
		m_arrows[0].transform.rotation = m_ArrowBone01.transform.rotation;
		m_arrows[1].SetActive(value: true);
		m_arrows[1].transform.position = m_ArrowBone02.transform.position;
		m_arrows[1].transform.rotation = m_ArrowBone02.transform.rotation;
		m_lastArrow = 2;
		m_targetRadius = Vector3.Distance(m_CenterBone.position, m_OuterRadiusBone.position);
		m_bullseyeRadius = Vector3.Distance(m_BullseyeCenterBone.position, m_BullseyeRadiusBone.position);
		m_AvailableTargetDummyArrows = new List<int>();
		for (int j = 0; j < m_TargetDummyArrows.Count; j++)
		{
			m_AvailableTargetDummyArrows.Add(j);
		}
		m_SplitArrow.SetActive(value: false);
	}

	private void Update()
	{
		HandleHits();
	}

	private void HandleHits()
	{
		if (UniversalInputManager.Get().GetMouseButtonDown(0) && IsOver(m_Collider01))
		{
			HnadleFireArrow();
		}
	}

	private void HnadleFireArrow()
	{
		if (m_clearingArrows)
		{
			return;
		}
		m_ArrowCount++;
		if (m_ArrowCount > m_Levelup)
		{
			m_ArrowCount = 0;
			m_MaxRandomOffset *= 0.95f;
			m_BullseyePercent += 4;
		}
		if (Random.Range(0, 100) < m_TargetDummyPercent && m_AvailableTargetDummyArrows.Count > 0)
		{
			HitTargetDummy();
			return;
		}
		Ray ray = Camera.main.ScreenPointToRay(InputCollection.GetMousePosition());
		bool bullseye = false;
		if (m_BoxColliderBullseye.Raycast(ray, out var hitInfo, 100f))
		{
			bullseye = true;
		}
		if (m_BoxCollider02.Raycast(ray, out hitInfo, 100f))
		{
			m_lastArrow++;
			if (m_lastArrow >= m_MaxArrows)
			{
				m_lastArrow = 0;
				StartCoroutine(ClearArrows());
				return;
			}
			GameObject obj = m_arrows[m_lastArrow];
			TGTArrow component = obj.GetComponent<TGTArrow>();
			FireArrow(component, hitInfo.point, bullseye);
			obj.transform.eulerAngles = hitInfo.normal;
			ImpactTarget();
		}
	}

	private IEnumerator ClearArrows()
	{
		m_clearingArrows = true;
		GameObject[] arrows = m_arrows;
		foreach (GameObject gameObject in arrows)
		{
			if (gameObject.activeSelf)
			{
				gameObject.SetActive(value: false);
				m_TargetPhysics.GetComponent<Rigidbody>().angularVelocity = new Vector3(Random.Range(m_HitIntensity * -0.25f, m_HitIntensity * -0.5f), 0f, 0f);
				PlaySound(m_RemoveArrowSoundPrefab);
				yield return new WaitForSeconds(0.2f);
			}
		}
		yield return new WaitForSeconds(0.2f);
		if (m_SplitArrow.activeSelf)
		{
			m_SplitArrow.SetActive(value: false);
			m_TargetPhysics.GetComponent<Rigidbody>().angularVelocity = new Vector3(Random.Range(m_HitIntensity * -0.25f, m_HitIntensity * -0.5f), 0f, 0f);
			PlaySound(m_RemoveArrowSoundPrefab);
		}
		m_lastArrowWasBullseye = false;
		m_lastBullseyeArrow = null;
		m_clearingArrows = false;
	}

	private void FireArrow(TGTArrow arrow, Vector3 hitPosition, bool bullseye)
	{
		arrow.transform.position = hitPosition;
		bool flag = false;
		if (Time.timeSinceLevelLoad > m_lastClickTime + 0.8f)
		{
			flag = true;
		}
		m_lastClickTime = Time.timeSinceLevelLoad;
		int num = m_BullseyePercent;
		if (flag)
		{
			num *= 2;
		}
		if (num > 80)
		{
			num = 80;
		}
		if (bullseye && Random.Range(0, 100) < num)
		{
			int num2 = 2;
			if (flag)
			{
				num2 = 8;
			}
			if (m_lastArrowWasBullseye && !m_SplitArrow.activeSelf && bullseye && Random.Range(0, 100) < num2)
			{
				m_SplitArrow.transform.position = m_lastBullseyeArrow.transform.position;
				m_SplitArrow.transform.rotation = m_lastBullseyeArrow.transform.rotation;
				TGTArrow component = m_SplitArrow.GetComponent<TGTArrow>();
				TGTArrow component2 = m_lastBullseyeArrow.GetComponent<TGTArrow>();
				m_SplitArrow.SetActive(value: true);
				component.FireArrow(randomRotation: false);
				component.Bullseye();
				PlaySound(m_SplitArrowSoundPrefab);
				component.m_ArrowRoot.transform.position = component2.m_ArrowRoot.transform.position;
				component.m_ArrowRoot.transform.rotation = component2.m_ArrowRoot.transform.rotation;
				m_lastBullseyeArrow.SetActive(value: false);
				m_lastArrowWasBullseye = false;
				m_lastBullseyeArrow = null;
			}
			else
			{
				arrow.gameObject.SetActive(value: true);
				arrow.Bullseye();
				PlaySound(m_HitBullseyeSoundPrefab);
				arrow.m_ArrowRoot.transform.localPosition = Vector3.zero;
				m_lastBullseyeArrow = arrow.gameObject;
				m_lastArrowWasBullseye = true;
			}
			return;
		}
		m_lastArrowWasBullseye = false;
		m_lastBullseyeArrow = null;
		arrow.gameObject.SetActive(value: true);
		if (bullseye)
		{
			Vector2 vector = Random.insideUnitCircle.normalized * m_bullseyeRadius * 2f;
			arrow.m_ArrowRoot.transform.localPosition = new Vector3(vector.x, vector.y, 0f);
			arrow.FireArrow(randomRotation: true);
			PlaySound(m_HitTargetSoundPrefab);
			return;
		}
		Vector2 vector2 = Random.insideUnitCircle * Random.Range(0f, m_MaxRandomOffset);
		arrow.m_ArrowRoot.transform.localPosition = new Vector3(vector2.x, vector2.y, 0f);
		if (Vector3.Distance(arrow.m_ArrowRoot.transform.position, m_CenterBone.position) > m_targetRadius)
		{
			arrow.m_ArrowRoot.transform.localPosition = Vector3.zero;
		}
		if (Vector3.Distance(arrow.m_ArrowRoot.transform.position, m_BullseyeCenterBone.position) < m_bullseyeRadius)
		{
			arrow.m_ArrowRoot.transform.localPosition = Vector3.zero;
		}
		arrow.FireArrow(randomRotation: true);
		PlaySound(m_HitTargetSoundPrefab);
	}

	private void HitTargetDummy()
	{
		int index = 0;
		if (m_AvailableTargetDummyArrows.Count > 1)
		{
			index = Random.Range(0, m_AvailableTargetDummyArrows.Count);
		}
		TGTArrow tGTArrow = m_TargetDummyArrows[m_AvailableTargetDummyArrows[index]];
		tGTArrow.gameObject.SetActive(value: true);
		tGTArrow.FireArrow(randomRotation: false);
		TGTTargetDummy.Get().ArrowHit();
		PlaySound(m_HitTargetDummySoundPrefab);
		if (m_AvailableTargetDummyArrows.Count > 1)
		{
			m_AvailableTargetDummyArrows.RemoveAt(index);
		}
		else
		{
			m_AvailableTargetDummyArrows.Clear();
		}
	}

	private void ImpactTarget()
	{
		m_TargetPhysics.GetComponent<Rigidbody>().angularVelocity = new Vector3(Random.Range(m_HitIntensity * 0.25f, m_HitIntensity), 0f, 0f);
	}

	private void PlaySound(string soundPrefab)
	{
		if (!string.IsNullOrEmpty(soundPrefab) && !string.IsNullOrEmpty(soundPrefab))
		{
			SoundManager.Get().LoadAndPlay(soundPrefab, base.gameObject);
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
