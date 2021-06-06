using Assets;

public class CardTextBuilder
{
	protected bool m_useEntityForTextInPlay;

	private static CardTextBuilder m_fallbackCardTextBuilder;

	public CardTextBuilder()
	{
		m_useEntityForTextInPlay = false;
	}

	public static string GetRawCardTextInHand(string cardId)
	{
		CardDbfRecord cardRecord = GameDbf.GetIndex().GetCardRecord(cardId);
		if (cardRecord != null && cardRecord.TextInHand != null)
		{
			return cardRecord.TextInHand;
		}
		return string.Empty;
	}

	public static string GetRawCardName(EntityDef entityDef)
	{
		CardDbfRecord cardRecord = GameDbf.GetIndex().GetCardRecord(entityDef.GetCardId());
		if (cardRecord != null && cardRecord.Name != null)
		{
			return cardRecord.Name.GetString() ?? entityDef.GetDebugName();
		}
		return entityDef.GetDebugName();
	}

	public static string GetDefaultCardTextInHand(EntityDef entityDef)
	{
		return TextUtils.TransformCardText(GetRawCardTextInHand(entityDef.GetCardId()));
	}

	public static CardTextBuilder GetFallbackCardTextBuilder()
	{
		if (m_fallbackCardTextBuilder == null)
		{
			m_fallbackCardTextBuilder = CardTextBuilderFactory.Create(Assets.Card.CardTextBuilderType.DEFAULT);
		}
		return m_fallbackCardTextBuilder;
	}

	public static string GetDefaultCardName(EntityDef entityDef)
	{
		return TextUtils.TransformCardText(GetRawCardName(entityDef));
	}

	public virtual string BuildCardName(Entity entity)
	{
		return GetDefaultCardName(entity.GetEntityDef());
	}

	public virtual string BuildCardName(EntityDef entityDef)
	{
		return GetDefaultCardName(entityDef);
	}

	public virtual string BuildCardTextInHand(Entity entity)
	{
		return TextUtils.TransformCardText(entity, GetRawCardTextInHand(entity.GetCardId()));
	}

	public virtual string BuildCardTextInHand(EntityDef entityDef)
	{
		return GetDefaultCardTextInHand(entityDef);
	}

	public virtual bool ContainsBonusDamageToken(Entity entity)
	{
		return TextUtils.HasBonusDamage(GetRawCardTextInHand(entity.GetCardId()));
	}

	public virtual bool ContainsBonusHealingToken(Entity entity)
	{
		return TextUtils.HasBonusHealing(GetRawCardTextInHand(entity.GetCardId()));
	}

	public virtual string BuildCardTextInHistory(Entity entity)
	{
		CardTextHistoryData cardTextHistoryData = entity.GetCardTextHistoryData();
		if (cardTextHistoryData == null)
		{
			Log.All.Print("CardTextBuilder.BuildCardTextInHistory: entity {0} does not have a CardTextHistoryData object.", entity.GetEntityId());
			return "";
		}
		EntityDef entityDef = entity.GetEntityDef();
		string rawCardTextInHand = GetRawCardTextInHand(entity.GetCardId() ?? entityDef.GetCardId());
		return TextUtils.TransformCardText(cardTextHistoryData.m_damageBonus, cardTextHistoryData.m_damageBonusDouble, cardTextHistoryData.m_healingDouble, rawCardTextInHand);
	}

	public virtual CardTextHistoryData CreateCardTextHistoryData()
	{
		return new CardTextHistoryData();
	}

	public virtual string GetTargetingArrowText(Entity entity)
	{
		CardDbfRecord cardRecord = GameDbf.GetIndex().GetCardRecord(entity.GetCardId());
		if (cardRecord == null || cardRecord.TargetArrowText == null)
		{
			return string.Empty;
		}
		return TextUtils.TransformCardText(entity, cardRecord.TargetArrowText.GetString());
	}

	public virtual void OnTagChange(Card card, TagDelta tagChange)
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

	public bool ShouldUseEntityForTextInPlay()
	{
		return m_useEntityForTextInPlay;
	}
}
