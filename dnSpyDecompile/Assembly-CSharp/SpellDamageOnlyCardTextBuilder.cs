using System;

// Token: 0x020007A6 RID: 1958
public class SpellDamageOnlyCardTextBuilder : CardTextBuilder
{
	// Token: 0x06006CCD RID: 27853 RVA: 0x002309C3 File Offset: 0x0022EBC3
	public SpellDamageOnlyCardTextBuilder()
	{
		this.m_useEntityForTextInPlay = true;
	}

	// Token: 0x06006CCE RID: 27854 RVA: 0x002329E4 File Offset: 0x00230BE4
	public override string BuildCardTextInHand(Entity entity)
	{
		return TextUtils.TransformCardText(entity.GetDamageBonus(), 0, 0, CardTextBuilder.GetRawCardTextInHand(entity.GetCardId()));
	}

	// Token: 0x06006CCF RID: 27855 RVA: 0x00232A00 File Offset: 0x00230C00
	public override string BuildCardTextInHistory(Entity entity)
	{
		CardTextHistoryData cardTextHistoryData = entity.GetCardTextHistoryData();
		if (cardTextHistoryData == null)
		{
			Log.All.Print("SpellDamageOnlyCardTextBuilder.BuildCardTextInHistory: entity {0} does not have a CardTextHistoryData object.", new object[]
			{
				entity.GetEntityId()
			});
			return string.Empty;
		}
		return TextUtils.TransformCardText(cardTextHistoryData.m_damageBonus, 0, 0, CardTextBuilder.GetRawCardTextInHand(entity.GetCardId()));
	}
}
