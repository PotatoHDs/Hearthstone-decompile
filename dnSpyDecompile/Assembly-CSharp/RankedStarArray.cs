using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Blizzard.T5.Core;
using HutongGames.PlayMaker;
using UnityEngine;

// Token: 0x0200064E RID: 1614
[CustomEditClass]
public class RankedStarArray : MonoBehaviour
{
	// Token: 0x17000553 RID: 1363
	// (get) Token: 0x06005AFC RID: 23292 RVA: 0x001DAABF File Offset: 0x001D8CBF
	// (set) Token: 0x06005AFD RID: 23293 RVA: 0x001DAAC7 File Offset: 0x001D8CC7
	private bool IsShowing { get; set; }

	// Token: 0x06005AFE RID: 23294 RVA: 0x001DAAD0 File Offset: 0x001D8CD0
	private void Awake()
	{
		this.LoadStars();
	}

	// Token: 0x06005AFF RID: 23295 RVA: 0x001DAAD8 File Offset: 0x001D8CD8
	public void Show()
	{
		if (this.m_showCoroutine != null)
		{
			base.StopCoroutine(this.m_showCoroutine);
		}
		this.m_showCoroutine = base.StartCoroutine(this.ShowWhenReady());
	}

	// Token: 0x06005B00 RID: 23296 RVA: 0x001DAB00 File Offset: 0x001D8D00
	public void Hide()
	{
		if (this.IsShowing)
		{
			foreach (RankChangeStar rankChangeStar in this.m_stars)
			{
				rankChangeStar.gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x06005B01 RID: 23297 RVA: 0x001DAB60 File Offset: 0x001D8D60
	private IEnumerator ShowWhenReady()
	{
		while (this.IsLoading())
		{
			yield return null;
		}
		foreach (RankChangeStar rankChangeStar in this.m_stars)
		{
			rankChangeStar.gameObject.SetActive(true);
		}
		this.IsShowing = true;
		yield break;
	}

	// Token: 0x06005B02 RID: 23298 RVA: 0x001DAB6F File Offset: 0x001D8D6F
	public void Init(int starCount, int starCountDarkened)
	{
		this.m_starCount = starCount;
		this.m_starCountDarkened = starCountDarkened;
		this.Reset();
	}

	// Token: 0x06005B03 RID: 23299 RVA: 0x001DAB88 File Offset: 0x001D8D88
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
			count = this.m_stars.Count;
		}
		fsmArray.objectReferences = (from star in this.m_stars.Skip(startIndex).Take(count)
		select star.gameObject).ToArray<UnityEngine.Object>();
		return true;
	}

	// Token: 0x06005B04 RID: 23300 RVA: 0x001DAC11 File Offset: 0x001D8E11
	public bool IsLoading()
	{
		return this.m_stars.Count < this.m_starCount;
	}

	// Token: 0x06005B05 RID: 23301 RVA: 0x001DAC26 File Offset: 0x001D8E26
	private void LoadStars()
	{
		if (this.m_loadCoroutine != null)
		{
			base.StopCoroutine(this.m_loadCoroutine);
		}
		this.m_loadCoroutine = base.StartCoroutine(this.LoadStarsWhenReady());
	}

	// Token: 0x06005B06 RID: 23302 RVA: 0x001DAC4E File Offset: 0x001D8E4E
	private IEnumerator LoadStarsWhenReady()
	{
		if (this.m_starCount > 0)
		{
			while (!HearthstoneServices.IsAvailable<IAssetLoader>())
			{
				yield return null;
			}
			for (int i = 0; i < this.m_starCount; i++)
			{
				AssetLoader.Get().InstantiatePrefab(RankedStarArray.s_starPrefab, new PrefabCallback<GameObject>(this.OnStarLoaded), null, AssetLoadingOptions.IgnorePrefabPosition);
			}
		}
		yield break;
	}

	// Token: 0x06005B07 RID: 23303 RVA: 0x001DAC60 File Offset: 0x001D8E60
	private void OnStarLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		go.transform.localScale = Vector3.one;
		GameUtils.SetParent(go, base.gameObject, false);
		go.SetActive(false);
		RankChangeStar component = go.GetComponent<RankChangeStar>();
		this.m_stars.Add(component);
		if (this.m_stars.Count == this.m_starCount)
		{
			int num = this.m_starCountDarkened;
			int num2 = this.m_stars.Count - 1;
			while (num2 >= 0 && num > 0)
			{
				this.m_stars[num2].BlackOut();
				num2--;
				num--;
			}
			if (this.m_layout == RankedStarArray.LayoutStyle.Arc)
			{
				this.LayoutStarsArc();
				return;
			}
			this.LayoutStarsLinear();
		}
	}

	// Token: 0x06005B08 RID: 23304 RVA: 0x001DAD08 File Offset: 0x001D8F08
	private void LayoutStarsArc()
	{
		float num = this.m_centerStarsAtDegrees * 0.017453292f;
		float num2 = this.m_arcDegreesPerStar * 0.017453292f;
		float num3 = num2 * (float)(this.m_stars.Count - 1);
		float num4 = num + num3 / 2f;
		Vector3 position = base.transform.position;
		position.x += this.m_arcRadius * Mathf.Cos(num);
		position.z += this.m_arcRadius * Mathf.Sin(num);
		foreach (Component component in this.m_stars)
		{
			Vector3 vector = base.transform.position;
			vector.x += this.m_arcRadius * Mathf.Cos(num4);
			vector.z += this.m_arcRadius * Mathf.Sin(num4);
			if (this.m_arcAlignEdge)
			{
				Vector3 b = base.transform.position - position;
				vector += b;
			}
			component.transform.position = vector;
			num4 -= num2;
		}
	}

	// Token: 0x06005B09 RID: 23305 RVA: 0x001DAE40 File Offset: 0x001D9040
	private void LayoutStarsLinear()
	{
		int i = this.m_stars.Count / 2 - 1;
		int num = i + 1;
		float num2 = (this.m_layout == RankedStarArray.LayoutStyle.Vertical) ? 1f : -1f;
		float num3 = (this.m_layout == RankedStarArray.LayoutStyle.Vertical) ? -1f : 1f;
		float num4 = 0f;
		float num5 = 0f;
		if (GeneralUtils.IsOdd(this.m_stars.Count))
		{
			if (this.m_stars.Count < 3)
			{
				return;
			}
			num++;
		}
		else
		{
			if (this.m_stars.Count < 2)
			{
				return;
			}
			if (this.m_layout == RankedStarArray.LayoutStyle.Vertical)
			{
				num5 += this.m_zOffsetPerStar / 2f;
				TransformUtil.SetLocalPosZ(this.m_stars[i], num5 * -1f);
				TransformUtil.SetLocalPosZ(this.m_stars[num], num5);
			}
			else
			{
				num4 += this.m_xOffsetPerStar / 2f;
				TransformUtil.SetLocalPosX(this.m_stars[i], num4 * -1f);
				TransformUtil.SetLocalPosX(this.m_stars[num], num4);
			}
			i--;
			num++;
		}
		while (i >= 0)
		{
			num4 += this.m_xOffsetPerStar;
			num5 += this.m_zOffsetPerStar;
			TransformUtil.SetLocalPosX(this.m_stars[i], num4 * num2);
			TransformUtil.SetLocalPosZ(this.m_stars[i], num5 * num3);
			i--;
			TransformUtil.SetLocalPosX(this.m_stars[num], num4);
			TransformUtil.SetLocalPosZ(this.m_stars[num], num5);
			num++;
		}
	}

	// Token: 0x06005B0A RID: 23306 RVA: 0x001DAFD5 File Offset: 0x001D91D5
	[ContextMenu("Show")]
	private void ResetAndShow()
	{
		this.Reset();
		this.Show();
	}

	// Token: 0x06005B0B RID: 23307 RVA: 0x001DAFE4 File Offset: 0x001D91E4
	[ContextMenu("Reset")]
	private void Reset()
	{
		foreach (RankChangeStar rankChangeStar in this.m_stars)
		{
			UnityEngine.Object.Destroy(rankChangeStar.gameObject);
		}
		this.m_stars.Clear();
		this.IsShowing = false;
		this.LoadStars();
	}

	// Token: 0x04004DBD RID: 19901
	[CustomEditField(Sections = "General")]
	public int m_starCount;

	// Token: 0x04004DBE RID: 19902
	[CustomEditField(Sections = "General")]
	public int m_starCountDarkened;

	// Token: 0x04004DBF RID: 19903
	[CustomEditField(Sections = "General")]
	public RankedStarArray.LayoutStyle m_layout;

	// Token: 0x04004DC0 RID: 19904
	[CustomEditField(Sections = "Linear Layout")]
	public float m_xOffsetPerStar;

	// Token: 0x04004DC1 RID: 19905
	[CustomEditField(Sections = "Linear Layout")]
	public float m_zOffsetPerStar;

	// Token: 0x04004DC2 RID: 19906
	[CustomEditField(Sections = "Arc Layout")]
	public float m_arcRadius;

	// Token: 0x04004DC3 RID: 19907
	[CustomEditField(Sections = "Arc Layout")]
	public float m_arcDegreesPerStar;

	// Token: 0x04004DC4 RID: 19908
	[CustomEditField(Sections = "Arc Layout")]
	public float m_centerStarsAtDegrees;

	// Token: 0x04004DC5 RID: 19909
	[CustomEditField(Sections = "Arc Layout")]
	public bool m_arcAlignEdge;

	// Token: 0x04004DC6 RID: 19910
	private static readonly string s_starPrefab = "Star_Ranked.prefab:48d5a18072eff2445a3de1c9f7348bea";

	// Token: 0x04004DC7 RID: 19911
	private List<RankChangeStar> m_stars = new List<RankChangeStar>();

	// Token: 0x04004DC9 RID: 19913
	private Coroutine m_showCoroutine;

	// Token: 0x04004DCA RID: 19914
	private Coroutine m_loadCoroutine;

	// Token: 0x02002161 RID: 8545
	public enum LayoutStyle
	{
		// Token: 0x0400E013 RID: 57363
		Horizontal,
		// Token: 0x0400E014 RID: 57364
		Vertical,
		// Token: 0x0400E015 RID: 57365
		Arc
	}
}
