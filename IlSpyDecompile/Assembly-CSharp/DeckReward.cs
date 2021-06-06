using Assets;
using UnityEngine;

public class DeckReward : Reward
{
	protected int DeckId;

	protected int ClassId;

	private DefLoader.DisposableCardDef m_heroCardDef;

	public UberText deckNameWild;

	public UberText deckNameStandard;

	public GameObject deckFrameWild;

	public GameObject deckFrameStandard;

	public MeshRenderer deckMeshWild;

	public MeshRenderer deckMeshStandard;

	protected override void InitData()
	{
		SetData(new DeckRewardData(0, 0), updateVisuals: false);
	}

	protected override void ShowReward(bool updateCacheValues)
	{
		if (!(base.Data is DeckRewardData))
		{
			Debug.LogWarning($"SimpleReward.ShowReward() - Data {base.Data} is not SimpleRewardData");
			return;
		}
		Vector3 localScale = m_root.transform.localScale;
		m_root.SetActive(value: true);
		m_root.transform.localScale = Vector3.zero;
		iTween.ScaleTo(m_root, iTween.Hash("scale", localScale, "time", 0.5f, "easetype", iTween.EaseType.easeOutElastic));
	}

	protected override void HideReward()
	{
		base.HideReward();
		m_root.SetActive(value: false);
	}

	protected override void OnDataSet(bool updateVisuals)
	{
		if (!updateVisuals)
		{
			return;
		}
		DeckRewardData deckRewardData = base.Data as DeckRewardData;
		if (deckRewardData != null)
		{
			DeckId = deckRewardData.DeckId;
			ClassId = deckRewardData.ClassId;
			if (GameUtils.DeckIncludesRotatedCards(DeckId))
			{
				deckFrameStandard.SetActive(value: false);
				deckMeshWild.SetMaterial(GetClassMaterial());
			}
			else
			{
				deckFrameWild.SetActive(value: false);
				deckMeshStandard.SetMaterial(GetClassMaterial());
			}
			string text2 = (deckNameWild.Text = (deckNameStandard.Text = GetDeckName()));
		}
	}

	private Material GetClassMaterial()
	{
		ReleaseCardDef();
		string heroCardId = CollectionManager.GetHeroCardId((TAG_CLASS)ClassId, CardHero.HeroType.VANILLA);
		m_heroCardDef = DefLoader.Get().GetCardDef(heroCardId);
		return m_heroCardDef.CardDef.GetCustomDeckPortrait();
	}

	private string GetDeckName()
	{
		return GameDbf.Deck.GetRecord(DeckId).Name;
	}

	protected override void OnDestroy()
	{
		ReleaseCardDef();
		base.OnDestroy();
	}

	private void ReleaseCardDef()
	{
		m_heroCardDef?.Dispose();
		m_heroCardDef = null;
	}
}
