using System;
using System.Linq;

// Token: 0x0200092A RID: 2346
public class Tags
{
	// Token: 0x0600819D RID: 33181 RVA: 0x002A2ED4 File Offset: 0x002A10D4
	public static string DebugTag(int tag, int val)
	{
		string arg = tag.ToString();
		GAME_TAG game_TAG;
		try
		{
			game_TAG = (GAME_TAG)tag;
			arg = game_TAG.ToString();
		}
		catch (Exception)
		{
		}
		string arg2 = val.ToString();
		game_TAG = (GAME_TAG)tag;
		if (game_TAG <= GAME_TAG.ZONE)
		{
			if (game_TAG == GAME_TAG.PLAYSTATE)
			{
				goto IL_F9;
			}
			if (game_TAG == GAME_TAG.STEP)
			{
				goto IL_DC;
			}
			if (game_TAG != GAME_TAG.ZONE)
			{
				goto IL_1DA;
			}
		}
		else if (game_TAG <= GAME_TAG.STATE)
		{
			if (game_TAG == GAME_TAG.CARD_SET)
			{
				goto IL_1C3;
			}
			switch (game_TAG)
			{
			case GAME_TAG.NEXT_STEP:
				goto IL_DC;
			case GAME_TAG.CLASS:
				goto IL_150;
			case GAME_TAG.CARDRACE:
				goto IL_17E;
			case GAME_TAG.FACTION:
				goto IL_167;
			case GAME_TAG.CARDTYPE:
				goto IL_116;
			case GAME_TAG.RARITY:
				goto IL_195;
			case GAME_TAG.STATE:
				try
				{
					TAG_STATE tag_STATE = (TAG_STATE)val;
					arg2 = tag_STATE.ToString();
					goto IL_1DA;
				}
				catch (Exception)
				{
					goto IL_1DA;
				}
				break;
			default:
				goto IL_1DA;
			}
		}
		else
		{
			if (game_TAG == GAME_TAG.MULLIGAN_STATE)
			{
				goto IL_133;
			}
			if (game_TAG - GAME_TAG.ENCHANTMENT_BIRTH_VISUAL > 1)
			{
				goto IL_1DA;
			}
			goto IL_1AC;
		}
		try
		{
			TAG_ZONE tag_ZONE = (TAG_ZONE)val;
			arg2 = tag_ZONE.ToString();
			goto IL_1DA;
		}
		catch (Exception)
		{
			goto IL_1DA;
		}
		IL_DC:
		try
		{
			TAG_STEP tag_STEP = (TAG_STEP)val;
			arg2 = tag_STEP.ToString();
			goto IL_1DA;
		}
		catch (Exception)
		{
			goto IL_1DA;
		}
		IL_F9:
		try
		{
			TAG_PLAYSTATE tag_PLAYSTATE = (TAG_PLAYSTATE)val;
			arg2 = tag_PLAYSTATE.ToString();
			goto IL_1DA;
		}
		catch (Exception)
		{
			goto IL_1DA;
		}
		IL_116:
		try
		{
			TAG_CARDTYPE tag_CARDTYPE = (TAG_CARDTYPE)val;
			arg2 = tag_CARDTYPE.ToString();
			goto IL_1DA;
		}
		catch (Exception)
		{
			goto IL_1DA;
		}
		IL_133:
		try
		{
			TAG_MULLIGAN tag_MULLIGAN = (TAG_MULLIGAN)val;
			arg2 = tag_MULLIGAN.ToString();
			goto IL_1DA;
		}
		catch (Exception)
		{
			goto IL_1DA;
		}
		IL_150:
		try
		{
			TAG_CLASS tag_CLASS = (TAG_CLASS)val;
			arg2 = tag_CLASS.ToString();
			goto IL_1DA;
		}
		catch (Exception)
		{
			goto IL_1DA;
		}
		IL_167:
		try
		{
			TAG_FACTION tag_FACTION = (TAG_FACTION)val;
			arg2 = tag_FACTION.ToString();
			goto IL_1DA;
		}
		catch (Exception)
		{
			goto IL_1DA;
		}
		IL_17E:
		try
		{
			TAG_RACE tag_RACE = (TAG_RACE)val;
			arg2 = tag_RACE.ToString();
			goto IL_1DA;
		}
		catch (Exception)
		{
			goto IL_1DA;
		}
		IL_195:
		try
		{
			TAG_RARITY tag_RARITY = (TAG_RARITY)val;
			arg2 = tag_RARITY.ToString();
			goto IL_1DA;
		}
		catch (Exception)
		{
			goto IL_1DA;
		}
		IL_1AC:
		try
		{
			TAG_ENCHANTMENT_VISUAL tag_ENCHANTMENT_VISUAL = (TAG_ENCHANTMENT_VISUAL)val;
			arg2 = tag_ENCHANTMENT_VISUAL.ToString();
			goto IL_1DA;
		}
		catch (Exception)
		{
			goto IL_1DA;
		}
		IL_1C3:
		try
		{
			TAG_CARD_SET tag_CARD_SET = (TAG_CARD_SET)val;
			arg2 = tag_CARD_SET.ToString();
		}
		catch (Exception)
		{
		}
		IL_1DA:
		return string.Format("tag={0} value={1}", arg, arg2);
	}

	// Token: 0x0600819E RID: 33182 RVA: 0x002A3168 File Offset: 0x002A1368
	public static void DebugDump(EntityBase entity, params GAME_TAG[] specificTagsToDump)
	{
		Log.Tag.PrintDebug("Tags.DebugDump: entity={0}", new object[]
		{
			entity
		});
		Map<int, int> map = entity.GetTags().GetMap();
		int[] array;
		if (specificTagsToDump == null || specificTagsToDump.Length == 0)
		{
			array = map.Keys.ToArray<int>();
		}
		else
		{
			array = (from t in specificTagsToDump
			select (int)t).ToArray<int>();
		}
		foreach (int num in array)
		{
			string text;
			if (map.ContainsKey(num))
			{
				int val = map[num];
				text = Tags.DebugTag(num, val);
			}
			else
			{
				string format = "tag={0} value=(NULL)";
				GAME_TAG game_TAG = (GAME_TAG)num;
				text = string.Format(format, game_TAG.ToString());
			}
			Log.Tag.PrintDebug("Tags.DebugDump:           {0}", new object[]
			{
				text
			});
		}
	}
}
