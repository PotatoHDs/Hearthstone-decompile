using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Blizzard.T5.Core;
using HutongGames.PlayMaker;
using UnityEngine;

[CustomEditClass]
public class RankedStarArray : MonoBehaviour
{
	public enum LayoutStyle
	{
		Horizontal,
		Vertical,
		Arc
	}

	[CustomEditField(Sections = "General")]
	public int m_starCount;

	[CustomEditField(Sections = "General")]
	public int m_starCountDarkened;

	[CustomEditField(Sections = "General")]
	public LayoutStyle m_layout;

	[CustomEditField(Sections = "Linear Layout")]
	public float m_xOffsetPerStar;

	[CustomEditField(Sections = "Linear Layout")]
	public float m_zOffsetPerStar;

	[CustomEditField(Sections = "Arc Layout")]
	public float m_arcRadius;

	[CustomEditField(Sections = "Arc Layout")]
	public float m_arcDegreesPerStar;

	[CustomEditField(Sections = "Arc Layout")]
	public float m_centerStarsAtDegrees;

	[CustomEditField(Sections = "Arc Layout")]
	public bool m_arcAlignEdge;

	private static readonly string s_starPrefab = "Star_Ranked.prefab:48d5a18072eff2445a3de1c9f7348bea";

	private List<RankChangeStar> m_stars = new List<RankChangeStar>();

	private Coroutine m_showCoroutine;

	private Coroutine m_loadCoroutine;

	private bool IsShowing { get; set; }

	private void Awake()
	{
		LoadStars();
	}

	public void Show()
	{
		if (m_showCoroutine != null)
		{
			StopCoroutine(m_showCoroutine);
		}
		m_showCoroutine = StartCoroutine(ShowWhenReady());
	}

	public void Hide()
	{
		if (!IsShowing)
		{
			return;
		}
		foreach (RankChangeStar star in m_stars)
		{
			star.gameObject.SetActive(value: false);
		}
	}

	private IEnumerator ShowWhenReady()
	{
		while (IsLoading())
		{
			yield return null;
		}
		foreach (RankChangeStar star in m_stars)
		{
			star.gameObject.SetActive(value: true);
		}
		IsShowing = true;
	}

	public void Init(int starCount, int starCountDarkened)
	{
		m_starCount = starCount;
		m_starCountDarkened = starCountDarkened;
		Reset();
	}

	public bool PopulateFsmArrayWithStars(PlayMakerFSM fsm, string varName, int startIndex = 0, int count = 0)
	{
		if (fsm == null)
		{
			return false;
		}
		if (string.IsNullOrEmpty(varName))
		{
			return false;
		}
		FsmArray fsmArray = fsm.FsmVariables.GetFsmArray(varName);
		if (fsmArray == null)
		{
			return false;
		}
		if (count <= 0)
		{
			count = m_stars.Count;
		}
		fsmArray.objectReferences = m_stars.Skip(startIndex).Take(count).Select((Func<RankChangeStar, UnityEngine.Object>)((RankChangeStar star) => star.gameObject))
			.ToArray();
		return true;
	}

	public bool IsLoading()
	{
		return m_stars.Count < m_starCount;
	}

	private void LoadStars()
	{
		if (m_loadCoroutine != null)
		{
			StopCoroutine(m_loadCoroutine);
		}
		m_loadCoroutine = StartCoroutine(LoadStarsWhenReady());
	}

	private IEnumerator LoadStarsWhenReady()
	{
		if (m_starCount > 0)
		{
			while (!HearthstoneServices.IsAvailable<IAssetLoader>())
			{
				yield return null;
			}
			for (int i = 0; i < m_starCount; i++)
			{
				AssetLoader.Get().InstantiatePrefab(s_starPrefab, OnStarLoaded, null, AssetLoadingOptions.IgnorePrefabPosition);
			}
		}
	}

	private void OnStarLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		go.transform.localScale = Vector3.one;
		GameUtils.SetParent(go, base.gameObject);
		go.SetActive(value: false);
		RankChangeStar component = go.GetComponent<RankChangeStar>();
		m_stars.Add(component);
		if (m_stars.Count == m_starCount)
		{
			int num = m_starCountDarkened;
			int num2 = m_stars.Count - 1;
			while (num2 >= 0 && num > 0)
			{
				m_stars[num2].BlackOut();
				num2--;
				num--;
			}
			if (m_layout == LayoutStyle.Arc)
			{
				LayoutStarsArc();
			}
			else
			{
				LayoutStarsLinear();
			}
		}
	}

	private void LayoutStarsArc()
	{
		float num = m_centerStarsAtDegrees * ((float)Math.PI / 180f);
		float num2 = m_arcDegreesPerStar * ((float)Math.PI / 180f);
		float num3 = num2 * (float)(m_stars.Count - 1);
		float num4 = num + num3 / 2f;
		Vector3 position = base.transform.position;
		position.x += m_arcRadius * Mathf.Cos(num);
		position.z += m_arcRadius * Mathf.Sin(num);
		foreach (RankChangeStar star in m_stars)
		{
			Vector3 position2 = base.transform.position;
			position2.x += m_arcRadius * Mathf.Cos(num4);
			position2.z += m_arcRadius * Mathf.Sin(num4);
			if (m_arcAlignEdge)
			{
				Vector3 vector = base.transform.position - position;
				position2 += vector;
			}
			star.transform.position = position2;
			num4 -= num2;
		}
	}

	private void LayoutStarsLinear()
	{
		int num = m_stars.Count / 2 - 1;
		int num2 = num + 1;
		float num3 = ((m_layout == LayoutStyle.Vertical) ? 1f : (-1f));
		float num4 = ((m_layout == LayoutStyle.Vertical) ? (-1f) : 1f);
		float num5 = 0f;
		float num6 = 0f;
		if (GeneralUtils.IsOdd(m_stars.Count))
		{
			if (m_stars.Count < 3)
			{
				return;
			}
			num2++;
		}
		else
		{
			if (m_stars.Count < 2)
			{
				return;
			}
			if (m_layout == LayoutStyle.Vertical)
			{
				num6 += m_zOffsetPerStar / 2f;
				TransformUtil.SetLocalPosZ(m_stars[num], num6 * -1f);
				TransformUtil.SetLocalPosZ(m_stars[num2], num6);
			}
			else
			{
				num5 += m_xOffsetPerStar / 2f;
				TransformUtil.SetLocalPosX(m_stars[num], num5 * -1f);
				TransformUtil.SetLocalPosX(m_stars[num2], num5);
			}
			num--;
			num2++;
		}
		while (num >= 0)
		{
			num5 += m_xOffsetPerStar;
			num6 += m_zOffsetPerStar;
			TransformUtil.SetLocalPosX(m_stars[num], num5 * num3);
			TransformUtil.SetLocalPosZ(m_stars[num], num6 * num4);
			num--;
			TransformUtil.SetLocalPosX(m_stars[num2], num5);
			TransformUtil.SetLocalPosZ(m_stars[num2], num6);
			num2++;
		}
	}

	[ContextMenu("Show")]
	private void ResetAndShow()
	{
		Reset();
		Show();
	}

	[ContextMenu("Reset")]
	private void Reset()
	{
		foreach (RankChangeStar star in m_stars)
		{
			UnityEngine.Object.Destroy(star.gameObject);
		}
		m_stars.Clear();
		IsShowing = false;
		LoadStars();
	}
}
