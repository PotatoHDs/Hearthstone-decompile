using System;
using UnityEngine;

// Token: 0x02000871 RID: 2161
public class CardDefHandle
{
	// Token: 0x060075E3 RID: 30179 RVA: 0x0025D74A File Offset: 0x0025B94A
	public void SetCardId(string cardId)
	{
		this.m_cardId = cardId;
	}

	// Token: 0x060075E4 RID: 30180 RVA: 0x0025D753 File Offset: 0x0025B953
	public void Set(CardDefHandle other)
	{
		this.m_cardId = ((other != null) ? other.m_cardId : null);
		this.SetCardDef((other != null) ? other.m_cardDef : null);
	}

	// Token: 0x060075E5 RID: 30181 RVA: 0x0025D77C File Offset: 0x0025B97C
	public bool SetCardDef(DefLoader.DisposableCardDef def)
	{
		UnityEngine.Object x = (def != null) ? def.CardDef : null;
		DefLoader.DisposableCardDef cardDef = this.m_cardDef;
		if (x != ((cardDef != null) ? cardDef.CardDef : null))
		{
			this.ReleaseCardDef();
			this.m_cardDef = ((def != null) ? def.Share() : null);
			return true;
		}
		return false;
	}

	// Token: 0x060075E6 RID: 30182 RVA: 0x0025D7C9 File Offset: 0x0025B9C9
	public DefLoader.DisposableCardDef Share()
	{
		if (this.m_cardDef == null)
		{
			DefLoader defLoader = DefLoader.Get();
			this.m_cardDef = ((defLoader != null) ? defLoader.GetCardDef(this.m_cardId, null) : null);
		}
		DefLoader.DisposableCardDef cardDef = this.m_cardDef;
		if (cardDef == null)
		{
			return null;
		}
		return cardDef.Share();
	}

	// Token: 0x060075E7 RID: 30183 RVA: 0x0025D802 File Offset: 0x0025BA02
	public CardDef Get()
	{
		if (this.m_cardDef == null)
		{
			DefLoader defLoader = DefLoader.Get();
			this.m_cardDef = ((defLoader != null) ? defLoader.GetCardDef(this.m_cardId, null) : null);
		}
		DefLoader.DisposableCardDef cardDef = this.m_cardDef;
		if (cardDef == null)
		{
			return null;
		}
		return cardDef.CardDef;
	}

	// Token: 0x060075E8 RID: 30184 RVA: 0x0025D83B File Offset: 0x0025BA3B
	public void ReleaseCardDef()
	{
		DefLoader.DisposableCardDef cardDef = this.m_cardDef;
		if (cardDef != null)
		{
			cardDef.Dispose();
		}
		this.m_cardDef = null;
	}

	// Token: 0x04005D20 RID: 23840
	private string m_cardId;

	// Token: 0x04005D21 RID: 23841
	private DefLoader.DisposableCardDef m_cardDef;
}
