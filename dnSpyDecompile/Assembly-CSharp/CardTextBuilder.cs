using System;
using Assets;

// Token: 0x0200078C RID: 1932
public class CardTextBuilder
{
	// Token: 0x06006C4A RID: 27722 RVA: 0x00230B00 File Offset: 0x0022ED00
	public CardTextBuilder()
	{
		this.m_useEntityForTextInPlay = false;
	}

	// Token: 0x06006C4B RID: 27723 RVA: 0x00230B10 File Offset: 0x0022ED10
	public static string GetRawCardTextInHand(string cardId)
	{
		CardDbfRecord cardRecord = GameDbf.GetIndex().GetCardRecord(cardId);
		if (cardRecord != null && cardRecord.TextInHand != null)
		{
			return cardRecord.TextInHand;
		}
		return string.Empty;
	}

	// Token: 0x06006C4C RID: 27724 RVA: 0x00230B48 File Offset: 0x0022ED48
	public static string GetRawCardName(EntityDef entityDef)
	{
		CardDbfRecord cardRecord = GameDbf.GetIndex().GetCardRecord(entityDef.GetCardId());
		if (cardRecord != null && cardRecord.Name != null)
		{
			return cardRecord.Name.GetString(true) ?? entityDef.GetDebugName();
		}
		return entityDef.GetDebugName();
	}

	// Token: 0x06006C4D RID: 27725 RVA: 0x00230B8E File Offset: 0x0022ED8E
	public static string GetDefaultCardTextInHand(EntityDef entityDef)
	{
		return TextUtils.TransformCardText(CardTextBuilder.GetRawCardTextInHand(entityDef.GetCardId()));
	}

	// Token: 0x06006C4E RID: 27726 RVA: 0x00230BA0 File Offset: 0x0022EDA0
	public static CardTextBuilder GetFallbackCardTextBuilder()
	{
		if (CardTextBuilder.m_fallbackCardTextBuilder == null)
		{
			CardTextBuilder.m_fallbackCardTextBuilder = CardTextBuilderFactory.Create(Assets.Card.CardTextBuilderType.DEFAULT);
		}
		return CardTextBuilder.m_fallbackCardTextBuilder;
	}

	// Token: 0x06006C4F RID: 27727 RVA: 0x00230BB9 File Offset: 0x0022EDB9
	public static string GetDefaultCardName(EntityDef entityDef)
	{
		return TextUtils.TransformCardText(CardTextBuilder.GetRawCardName(entityDef));
	}

	// Token: 0x06006C50 RID: 27728 RVA: 0x00230BC6 File Offset: 0x0022EDC6
	public virtual string BuildCardName(Entity entity)
	{
		return CardTextBuilder.GetDefaultCardName(entity.GetEntityDef());
	}

	// Token: 0x06006C51 RID: 27729 RVA: 0x00230BD3 File Offset: 0x0022EDD3
	public virtual string BuildCardName(EntityDef entityDef)
	{
		return CardTextBuilder.GetDefaultCardName(entityDef);
	}

	// Token: 0x06006C52 RID: 27730 RVA: 0x00230BDB File Offset: 0x0022EDDB
	public virtual string BuildCardTextInHand(Entity entity)
	{
		return TextUtils.TransformCardText(entity, CardTextBuilder.GetRawCardTextInHand(entity.GetCardId()));
	}

	// Token: 0x06006C53 RID: 27731 RVA: 0x00230BEE File Offset: 0x0022EDEE
	public virtual string BuildCardTextInHand(EntityDef entityDef)
	{
		return CardTextBuilder.GetDefaultCardTextInHand(entityDef);
	}

	// Token: 0x06006C54 RID: 27732 RVA: 0x00230BF6 File Offset: 0x0022EDF6
	public virtual bool ContainsBonusDamageToken(Entity entity)
	{
		return TextUtils.HasBonusDamage(CardTextBuilder.GetRawCardTextInHand(entity.GetCardId()));
	}

	// Token: 0x06006C55 RID: 27733 RVA: 0x00230C08 File Offset: 0x0022EE08
	public virtual bool ContainsBonusHealingToken(Entity entity)
	{
		return TextUtils.HasBonusHealing(CardTextBuilder.GetRawCardTextInHand(entity.GetCardId()));
	}

	// Token: 0x06006C56 RID: 27734 RVA: 0x00230C1C File Offset: 0x0022EE1C
	public virtual string BuildCardTextInHistory(Entity entity)
	{
		CardTextHistoryData cardTextHistoryData = entity.GetCardTextHistoryData();
		if (cardTextHistoryData == null)
		{
			Log.All.Print("CardTextBuilder.BuildCardTextInHistory: entity {0} does not have a CardTextHistoryData object.", new object[]
			{
				entity.GetEntityId()
			});
			return "";
		}
		EntityDef entityDef = entity.GetEntityDef();
		string rawCardTextInHand = CardTextBuilder.GetRawCardTextInHand(entity.GetCardId() ?? entityDef.GetCardId());
		return TextUtils.TransformCardText(cardTextHistoryData.m_damageBonus, cardTextHistoryData.m_damageBonusDouble, cardTextHistoryData.m_healingDouble, rawCardTextInHand);
	}

	// Token: 0x06006C57 RID: 27735 RVA: 0x00230C91 File Offset: 0x0022EE91
	public virtual CardTextHistoryData CreateCardTextHistoryData()
	{
		return new CardTextHistoryData();
	}

	// Token: 0x06006C58 RID: 27736 RVA: 0x00230C98 File Offset: 0x0022EE98
	public virtual string GetTargetingArrowText(Entity entity)
	{
		CardDbfRecord cardRecord = GameDbf.GetIndex().GetCardRecord(entity.GetCardId());
		if (cardRecord == null || cardRecord.TargetArrowText == null)
		{
			return string.Empty;
		}
		return TextUtils.TransformCardText(entity, cardRecord.TargetArrowText.GetString(true));
	}

	// Token: 0x06006C59 RID: 27737 RVA: 0x00230CDC File Offset: 0x0022EEDC
	public virtual void OnTagChange(global::Card card, TagDelta tagChange)
	{
		GAME_TAG tag = (GAME_TAG)tagChange.tag;
		if (tag == GAME_TAG.OVERRIDECARDTEXTBUILDER && card != null && card.GetActor() != null)
		{
			Actor actor = card.GetActor();
			if (actor.GetEntity() != null && actor.GetEntity().GetEntityDef() != null)
			{
				actor.GetEntity().GetEntityDef().ClearCardTextBuilder();
			}
			actor.UpdatePowersText();
		}
	}

	// Token: 0x06006C5A RID: 27738 RVA: 0x00230D41 File Offset: 0x0022EF41
	public bool ShouldUseEntityForTextInPlay()
	{
		return this.m_useEntityForTextInPlay;
	}

	// Token: 0x040057A9 RID: 22441
	protected bool m_useEntityForTextInPlay;

	// Token: 0x040057AA RID: 22442
	private static CardTextBuilder m_fallbackCardTextBuilder;
}
