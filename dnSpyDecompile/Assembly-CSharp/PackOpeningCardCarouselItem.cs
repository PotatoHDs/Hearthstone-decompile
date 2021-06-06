using System;
using UnityEngine;

// Token: 0x02000B00 RID: 2816
public class PackOpeningCardCarouselItem : CarouselItem
{
	// Token: 0x060095E2 RID: 38370 RVA: 0x003088D0 File Offset: 0x00306AD0
	public PackOpeningCardCarouselItem(PackOpeningCard card)
	{
		this.m_card = card;
	}

	// Token: 0x060095E3 RID: 38371 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void Show(Carousel card)
	{
	}

	// Token: 0x060095E4 RID: 38372 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void Hide()
	{
	}

	// Token: 0x060095E5 RID: 38373 RVA: 0x003088DF File Offset: 0x00306ADF
	public override void Clear()
	{
		this.m_card = null;
	}

	// Token: 0x060095E6 RID: 38374 RVA: 0x003088E8 File Offset: 0x00306AE8
	public override GameObject GetGameObject()
	{
		return this.m_card.gameObject;
	}

	// Token: 0x060095E7 RID: 38375 RVA: 0x000052EC File Offset: 0x000034EC
	public override bool IsLoaded()
	{
		return true;
	}

	// Token: 0x04007D9E RID: 32158
	private PackOpeningCard m_card;
}
