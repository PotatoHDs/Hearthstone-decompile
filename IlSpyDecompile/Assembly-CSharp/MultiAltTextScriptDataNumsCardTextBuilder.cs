using System.Reflection;
using Hearthstone;
using UnityEngine;

public class MultiAltTextScriptDataNumsCardTextBuilder : CardTextBuilder
{
	private const char altTextSeparator = '@';

	public MultiAltTextScriptDataNumsCardTextBuilder()
	{
		m_useEntityForTextInPlay = true;
	}

	private string GetAlternateTextSubstring(string baseText, Entity entity)
	{
		if (string.IsNullOrEmpty(baseText) || entity == null)
		{
			return string.Empty;
		}
		string[] array = baseText.Split('@');
		int num = entity.GetTag(GAME_TAG.USE_ALTERNATE_CARD_TEXT);
		if (num < 0 || num >= array.Length)
		{
			string format = MethodBase.GetCurrentMethod().ReflectedType.Name + "." + MethodBase.GetCurrentMethod().Name + "(): " + $"index of alternate text ({num}) on Entity {entity.GetEntityId()} ({entity.GetName()}) " + $"is outside of the valid range [0, {array.Length - 1}]. Value rounded to nearest valid one.";
			Log.Gameplay.PrintWarning(format);
			num = Mathf.Clamp(num, 0, array.Length - 1);
		}
		return array[num];
	}

	private string GetAlternateTextSubstring(string baseText, EntityDef entityDef)
	{
		if (string.IsNullOrEmpty(baseText) || entityDef == null)
		{
			return string.Empty;
		}
		string[] array = baseText.Split('@');
		int num = entityDef.GetTag(GAME_TAG.USE_ALTERNATE_CARD_TEXT);
		if (num < 0 || num >= array.Length)
		{
			string format = MethodBase.GetCurrentMethod().ReflectedType.Name + "." + MethodBase.GetCurrentMethod().Name + "(): " + $"index of alternate text ({num}) on EntityDef ({entityDef.GetName()}) " + $"is outside of the valid range [0, {array.Length - 1}]. Value rounded to nearest valid one.";
			Log.Gameplay.PrintWarning(format);
			num = Mathf.Clamp(num, 0, array.Length - 1);
		}
		return array[num];
	}

	private string SubstituteScriptDataNums(string rawText, Entity entity)
	{
		if (string.IsNullOrEmpty(rawText))
		{
			return string.Empty;
		}
		if (entity == null)
		{
			return rawText;
		}
		string text = rawText;
		if (rawText.Contains("{0}"))
		{
			int tag = entity.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_1);
			if (rawText.Contains("{1}"))
			{
				int tag2 = entity.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_2);
				text = string.Format(text, tag, tag2);
			}
			else
			{
				text = string.Format(text, tag);
			}
		}
		return text;
	}

	private string SubstituteScriptDataNums(string rawText, EntityDef entityDef)
	{
		if (string.IsNullOrEmpty(rawText))
		{
			return string.Empty;
		}
		if (entityDef == null)
		{
			return rawText;
		}
		string text = rawText;
		if (rawText.Contains("{0}"))
		{
			int tag = entityDef.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_1);
			if (rawText.Contains("{1}"))
			{
				int tag2 = entityDef.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_2);
				text = string.Format(text, tag, tag2);
			}
			else
			{
				text = string.Format(text, tag);
			}
		}
		return text;
	}

	private string BuildCardTextForEntity(Entity entity)
	{
		if (entity == null)
		{
			if (HearthstoneApplication.IsPublic())
			{
				return "Error: parameter entity in " + MethodBase.GetCurrentMethod().ReflectedType.Name + "." + MethodBase.GetCurrentMethod().Name + "() is null.";
			}
			return string.Empty;
		}
		string rawCardTextInHand = CardTextBuilder.GetRawCardTextInHand(entity.GetCardId());
		string powersText = SubstituteScriptDataNums(rawCardTextInHand, entity);
		string baseText = TextUtils.TransformCardText(entity.GetDamageBonus(), entity.GetDamageBonusDouble(), entity.GetHealingDouble(), powersText);
		return GetAlternateTextSubstring(baseText, entity);
	}

	public override string BuildCardTextInHand(Entity entity)
	{
		return BuildCardTextForEntity(entity);
	}

	public override string BuildCardTextInHand(EntityDef entityDef)
	{
		string rawCardTextInHand = CardTextBuilder.GetRawCardTextInHand(entityDef.GetCardId());
		string baseText = TextUtils.TransformCardText(SubstituteScriptDataNums(rawCardTextInHand, entityDef));
		return GetAlternateTextSubstring(baseText, entityDef);
	}

	public override string BuildCardTextInHistory(Entity entity)
	{
		return BuildCardTextForEntity(entity);
	}
}
