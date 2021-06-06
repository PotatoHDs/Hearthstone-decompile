using System;
using System.Collections.Generic;

// Token: 0x020007A4 RID: 1956
public class ScriptDataNum1CardTextBuilder : CardTextBuilder
{
	// Token: 0x06006CC1 RID: 27841 RVA: 0x002309C3 File Offset: 0x0022EBC3
	public ScriptDataNum1CardTextBuilder()
	{
		this.m_useEntityForTextInPlay = true;
	}

	// Token: 0x06006CC2 RID: 27842 RVA: 0x0023279C File Offset: 0x0023099C
	protected static List<int> GetDelimiterIndexList(string text)
	{
		List<int> list = new List<int>();
		for (int i = text.IndexOf('@'); i >= 0; i = text.IndexOf('@', i + 1))
		{
			list.Add(i);
		}
		return list;
	}

	// Token: 0x06006CC3 RID: 27843 RVA: 0x002327D4 File Offset: 0x002309D4
	protected string BuildCardTextInternal(Entity entity)
	{
		string rawCardTextInHand = CardTextBuilder.GetRawCardTextInHand(entity.GetCardId());
		string newValue = entity.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_1).ToString();
		List<int> delimiterIndexList = ScriptDataNum1CardTextBuilder.GetDelimiterIndexList(rawCardTextInHand);
		string text;
		if (delimiterIndexList.Count == 2 && entity.GetEntityDef().GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_1) == 0)
		{
			text = rawCardTextInHand.Substring(0, delimiterIndexList[0]);
			text += rawCardTextInHand.Substring(delimiterIndexList[0] + 1).Replace("@", newValue);
		}
		else
		{
			text = rawCardTextInHand.Replace("@", newValue);
		}
		return TextUtils.TransformCardText(entity.GetDamageBonus(), entity.GetDamageBonusDouble(), entity.GetHealingDouble(), text);
	}

	// Token: 0x06006CC4 RID: 27844 RVA: 0x00232879 File Offset: 0x00230A79
	public override string BuildCardTextInHand(Entity entity)
	{
		return this.BuildCardTextInternal(entity);
	}

	// Token: 0x06006CC5 RID: 27845 RVA: 0x00232879 File Offset: 0x00230A79
	public override string BuildCardTextInHistory(Entity entity)
	{
		return this.BuildCardTextInternal(entity);
	}

	// Token: 0x06006CC6 RID: 27846 RVA: 0x00232884 File Offset: 0x00230A84
	public override string BuildCardTextInHand(EntityDef entityDef)
	{
		string text = CardTextBuilder.GetRawCardTextInHand(entityDef.GetCardId());
		List<int> delimiterIndexList = ScriptDataNum1CardTextBuilder.GetDelimiterIndexList(text);
		if (delimiterIndexList.Count == 2 && entityDef.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_1) == 0)
		{
			text = text.Substring(0, delimiterIndexList[0]);
			return TextUtils.TransformCardText(text);
		}
		string newValue = entityDef.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_1).ToString();
		return TextUtils.TransformCardText(text.Replace("@", newValue));
	}

	// Token: 0x06006CC7 RID: 27847 RVA: 0x00231219 File Offset: 0x0022F419
	public override void OnTagChange(Card card, TagDelta tagChange)
	{
		if (tagChange.tag == 2 && card != null && card.GetActor() != null)
		{
			card.GetActor().UpdateTextComponents();
		}
	}
}
