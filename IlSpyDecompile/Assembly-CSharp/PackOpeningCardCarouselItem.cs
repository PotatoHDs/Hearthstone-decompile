using UnityEngine;

public class PackOpeningCardCarouselItem : CarouselItem
{
	private PackOpeningCard m_card;

	public PackOpeningCardCarouselItem(PackOpeningCard card)
	{
		m_card = card;
	}

	public override void Show(Carousel card)
	{
	}

	public override void Hide()
	{
	}

	public override void Clear()
	{
		m_card = null;
	}

	public override GameObject GetGameObject()
	{
		return m_card.gameObject;
	}

	public override bool IsLoaded()
	{
		return true;
	}
}
