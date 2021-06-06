using System;
using System.Linq;

public class Tags
{
	public static string DebugTag(int tag, int val)
	{
		string arg = tag.ToString();
		try
		{
			GAME_TAG gAME_TAG = (GAME_TAG)tag;
			arg = gAME_TAG.ToString();
		}
		catch (Exception)
		{
		}
		string arg2 = val.ToString();
		switch (tag)
		{
		case 204:
			try
			{
				TAG_STATE tAG_STATE = (TAG_STATE)val;
				arg2 = tAG_STATE.ToString();
			}
			catch (Exception)
			{
			}
			break;
		case 49:
			try
			{
				TAG_ZONE tAG_ZONE = (TAG_ZONE)val;
				arg2 = tAG_ZONE.ToString();
			}
			catch (Exception)
			{
			}
			break;
		case 19:
		case 198:
			try
			{
				TAG_STEP tAG_STEP = (TAG_STEP)val;
				arg2 = tAG_STEP.ToString();
			}
			catch (Exception)
			{
			}
			break;
		case 17:
			try
			{
				TAG_PLAYSTATE tAG_PLAYSTATE = (TAG_PLAYSTATE)val;
				arg2 = tAG_PLAYSTATE.ToString();
			}
			catch (Exception)
			{
			}
			break;
		case 202:
			try
			{
				TAG_CARDTYPE tAG_CARDTYPE = (TAG_CARDTYPE)val;
				arg2 = tAG_CARDTYPE.ToString();
			}
			catch (Exception)
			{
			}
			break;
		case 305:
			try
			{
				TAG_MULLIGAN tAG_MULLIGAN = (TAG_MULLIGAN)val;
				arg2 = tAG_MULLIGAN.ToString();
			}
			catch (Exception)
			{
			}
			break;
		case 199:
			try
			{
				TAG_CLASS tAG_CLASS = (TAG_CLASS)val;
				arg2 = tAG_CLASS.ToString();
			}
			catch (Exception)
			{
			}
			break;
		case 201:
			try
			{
				TAG_FACTION tAG_FACTION = (TAG_FACTION)val;
				arg2 = tAG_FACTION.ToString();
			}
			catch (Exception)
			{
			}
			break;
		case 200:
			try
			{
				TAG_RACE tAG_RACE = (TAG_RACE)val;
				arg2 = tAG_RACE.ToString();
			}
			catch (Exception)
			{
			}
			break;
		case 203:
			try
			{
				TAG_RARITY tAG_RARITY = (TAG_RARITY)val;
				arg2 = tAG_RARITY.ToString();
			}
			catch (Exception)
			{
			}
			break;
		case 330:
		case 331:
			try
			{
				TAG_ENCHANTMENT_VISUAL tAG_ENCHANTMENT_VISUAL = (TAG_ENCHANTMENT_VISUAL)val;
				arg2 = tAG_ENCHANTMENT_VISUAL.ToString();
			}
			catch (Exception)
			{
			}
			break;
		case 183:
			try
			{
				TAG_CARD_SET tAG_CARD_SET = (TAG_CARD_SET)val;
				arg2 = tAG_CARD_SET.ToString();
			}
			catch (Exception)
			{
			}
			break;
		}
		return $"tag={arg} value={arg2}";
	}

	public static void DebugDump(EntityBase entity, params GAME_TAG[] specificTagsToDump)
	{
		Log.Tag.PrintDebug("Tags.DebugDump: entity={0}", entity);
		Map<int, int> map = entity.GetTags().GetMap();
		int[] array = ((specificTagsToDump != null && specificTagsToDump.Length != 0) ? specificTagsToDump.Select((GAME_TAG t) => (int)t).ToArray() : map.Keys.ToArray());
		foreach (int num in array)
		{
			string text = "";
			if (map.ContainsKey(num))
			{
				int val = map[num];
				text = DebugTag(num, val);
			}
			else
			{
				GAME_TAG gAME_TAG = (GAME_TAG)num;
				text = $"tag={gAME_TAG.ToString()} value=(NULL)";
			}
			Log.Tag.PrintDebug("Tags.DebugDump:           {0}", text);
		}
	}
}
