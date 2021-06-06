using System;
using System.Reflection;
using Hearthstone;
using UnityEngine;

// Token: 0x0200079C RID: 1948
public class MultiAltTextScriptDataNumsCardTextBuilder : CardTextBuilder
{
	// Token: 0x06006C9E RID: 27806 RVA: 0x002309C3 File Offset: 0x0022EBC3
	public MultiAltTextScriptDataNumsCardTextBuilder()
	{
		this.m_useEntityForTextInPlay = true;
	}

	// Token: 0x06006C9F RID: 27807 RVA: 0x00231F14 File Offset: 0x00230114
	private string GetAlternateTextSubstring(string baseText, Entity entity)
	{
		if (string.IsNullOrEmpty(baseText) || entity == null)
		{
			return string.Empty;
		}
		string[] array = baseText.Split(new char[]
		{
			'@'
		});
		int num = entity.GetTag(GAME_TAG.USE_ALTERNATE_CARD_TEXT);
		if (num < 0 || num >= array.Length)
		{
			string format = string.Concat(new string[]
			{
				MethodBase.GetCurrentMethod().ReflectedType.Name,
				".",
				MethodBase.GetCurrentMethod().Name,
				"(): ",
				string.Format("index of alternate text ({0}) on Entity {1} ({2}) ", num, entity.GetEntityId(), entity.GetName()),
				string.Format("is outside of the valid range [0, {0}]. Value rounded to nearest valid one.", array.Length - 1)
			});
			Log.Gameplay.PrintWarning(format, Array.Empty<object>());
			num = Mathf.Clamp(num, 0, array.Length - 1);
		}
		return array[num];
	}

	// Token: 0x06006CA0 RID: 27808 RVA: 0x00231FF4 File Offset: 0x002301F4
	private string GetAlternateTextSubstring(string baseText, EntityDef entityDef)
	{
		if (string.IsNullOrEmpty(baseText) || entityDef == null)
		{
			return string.Empty;
		}
		string[] array = baseText.Split(new char[]
		{
			'@'
		});
		int num = entityDef.GetTag(GAME_TAG.USE_ALTERNATE_CARD_TEXT);
		if (num < 0 || num >= array.Length)
		{
			string format = string.Concat(new string[]
			{
				MethodBase.GetCurrentMethod().ReflectedType.Name,
				".",
				MethodBase.GetCurrentMethod().Name,
				"(): ",
				string.Format("index of alternate text ({0}) on EntityDef ({1}) ", num, entityDef.GetName()),
				string.Format("is outside of the valid range [0, {0}]. Value rounded to nearest valid one.", array.Length - 1)
			});
			Log.Gameplay.PrintWarning(format, Array.Empty<object>());
			num = Mathf.Clamp(num, 0, array.Length - 1);
		}
		return array[num];
	}

	// Token: 0x06006CA1 RID: 27809 RVA: 0x002320C8 File Offset: 0x002302C8
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

	// Token: 0x06006CA2 RID: 27810 RVA: 0x00232138 File Offset: 0x00230338
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

	// Token: 0x06006CA3 RID: 27811 RVA: 0x002321A8 File Offset: 0x002303A8
	private string BuildCardTextForEntity(Entity entity)
	{
		if (entity != null)
		{
			string rawCardTextInHand = CardTextBuilder.GetRawCardTextInHand(entity.GetCardId());
			string powersText = this.SubstituteScriptDataNums(rawCardTextInHand, entity);
			string baseText = TextUtils.TransformCardText(entity.GetDamageBonus(), entity.GetDamageBonusDouble(), entity.GetHealingDouble(), powersText);
			return this.GetAlternateTextSubstring(baseText, entity);
		}
		if (HearthstoneApplication.IsPublic())
		{
			return string.Concat(new string[]
			{
				"Error: parameter entity in ",
				MethodBase.GetCurrentMethod().ReflectedType.Name,
				".",
				MethodBase.GetCurrentMethod().Name,
				"() is null."
			});
		}
		return string.Empty;
	}

	// Token: 0x06006CA4 RID: 27812 RVA: 0x0023223E File Offset: 0x0023043E
	public override string BuildCardTextInHand(Entity entity)
	{
		return this.BuildCardTextForEntity(entity);
	}

	// Token: 0x06006CA5 RID: 27813 RVA: 0x00232248 File Offset: 0x00230448
	public override string BuildCardTextInHand(EntityDef entityDef)
	{
		string rawCardTextInHand = CardTextBuilder.GetRawCardTextInHand(entityDef.GetCardId());
		string baseText = TextUtils.TransformCardText(this.SubstituteScriptDataNums(rawCardTextInHand, entityDef));
		return this.GetAlternateTextSubstring(baseText, entityDef);
	}

	// Token: 0x06006CA6 RID: 27814 RVA: 0x0023223E File Offset: 0x0023043E
	public override string BuildCardTextInHistory(Entity entity)
	{
		return this.BuildCardTextForEntity(entity);
	}

	// Token: 0x040057C1 RID: 22465
	private const char altTextSeparator = '@';
}
