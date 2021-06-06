using System.Collections.Generic;

public class ScriptDataNum1CardTextBuilder : CardTextBuilder
{
	public ScriptDataNum1CardTextBuilder()
	{
		m_useEntityForTextInPlay = true;
	}

	protected static List<int> GetDelimiterIndexList(string text)
	{
		List<int> list = new List<int>();
		for (int num = text.IndexOf('@'); num >= 0; num = text.IndexOf('@', num + 1))
		{
			list.Add(num);
		}
		return list;
	}

	protected string BuildCardTextInternal(Entity entity)
	{
		string rawCardTextInHand = CardTextBuilder.GetRawCardTextInHand(entity.GetCardId());
		string newValue = entity.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_1).ToString();
		string text = "";
		List<int> delimiterIndexList = GetDelimiterIndexList(rawCardTextInHand);
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

	public override string BuildCardTextInHand(Entity entity)
	{
		return BuildCardTextInternal(entity);
	}

	public override string BuildCardTextInHistory(Entity entity)
	{
		return BuildCardTextInternal(entity);
	}

	public override string BuildCardTextInHand(EntityDef entityDef)
	{
		string rawCardTextInHand = CardTextBuilder.GetRawCardTextInHand(entityDef.GetCardId());
		List<int> delimiterIndexList = GetDelimiterIndexList(rawCardTextInHand);
		if (delimiterIndexList.Count == 2 && entityDef.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_1) == 0)
		{
			rawCardTextInHand = rawCardTextInHand.Substring(0, delimiterIndexList[0]);
			return TextUtils.TransformCardText(rawCardTextInHand);
		}
		string newValue = entityDef.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_1).ToString();
		return TextUtils.TransformCardText(rawCardTextInHand.Replace("@", newValue));
	}

	public override void OnTagChange(Card card, TagDelta tagChange)
	{
		if (tagChange.tag == 2 && card != null && card.GetActor() != null)
		{
			card.GetActor().UpdateTextComponents();
		}
	}
}
