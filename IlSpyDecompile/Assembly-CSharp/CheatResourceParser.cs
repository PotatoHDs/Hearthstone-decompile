using Blizzard.T5.Core;

public class CheatResourceParser
{
	private const string MISSING_VALID_RESOURCE_ERROR_MSG = "Missing valid resource. You must specify one of the following valid resources: cards, gold, dust, tutorial, hero, pack, arenaticket, arena.";

	private const string MISSING_VALID_ATTRIBUTE_ERROR_MSG = "Missing valid attribute. You must specify one of the following valid resources: class, level.";

	public static bool TryParse(string[] args, out CheatResource resource, out string errMsg)
	{
		resource = null;
		errMsg = null;
		if (args.Length == 0)
		{
			errMsg = "Missing valid resource. You must specify one of the following valid resources: cards, gold, dust, tutorial, hero, pack, arenaticket, arena.";
			return false;
		}
		string[] array = args[0].Split('=');
		switch (array[0])
		{
		case "cards":
			resource = new FullCardCollectionCheatResource();
			return true;
		case "gold":
		{
			int? amount = null;
			if (array.Length > 1)
			{
				if (!int.TryParse(array[1], out var result3))
				{
					errMsg = "Failed to parse gold amount. The amount must be a valid number.";
					return false;
				}
				amount = result3;
			}
			resource = new GoldCheatResource
			{
				Amount = amount
			};
			return true;
		}
		case "dust":
		{
			int? amount2 = null;
			if (array.Length > 1)
			{
				if (!int.TryParse(array[1], out var result4))
				{
					errMsg = "Failed to parse dust amount. The amount must be a valid number.";
					return false;
				}
				amount2 = result4;
			}
			resource = new DustCheatResource
			{
				Amount = amount2
			};
			return true;
		}
		case "tutorial":
		{
			int? progress = null;
			if (array.Length > 1)
			{
				if (!int.TryParse(array[1], out var result))
				{
					errMsg = "Failed to parse progress value. The amount must be a valid number.";
					return false;
				}
				progress = result;
			}
			resource = new TutorialCheatResource
			{
				Progress = progress
			};
			return true;
		}
		case "arenaticket":
		{
			int? ticketCount = null;
			if (array.Length > 1)
			{
				if (!int.TryParse(array[1], out var result2))
				{
					errMsg = "Failed to parse ticket count value. The amount must be a valid number.";
					return false;
				}
				ticketCount = result2;
			}
			resource = new ArenaTicketCheatResource
			{
				TicketCount = ticketCount
			};
			return true;
		}
		case "arena":
		{
			int? value9 = null;
			int? value10 = null;
			if (args.Length > 1)
			{
				string[] args4 = args.Slice(1);
				MultiAttributeParser multiAttributeParser3 = new MultiAttributeParser();
				if (!multiAttributeParser3.load(args4, out errMsg))
				{
					return false;
				}
				if (!multiAttributeParser3.getIntAttribute("win", out value9, out errMsg))
				{
					return false;
				}
				if (!multiAttributeParser3.getIntAttribute("loss", out value10, out errMsg))
				{
					return false;
				}
			}
			resource = new ArenaCheatResource
			{
				Win = value9,
				Loss = value10
			};
			return true;
		}
		case "pack":
		{
			int? value7 = null;
			int? value8 = null;
			if (args.Length > 1)
			{
				string[] args3 = args.Slice(1);
				MultiAttributeParser multiAttributeParser2 = new MultiAttributeParser();
				if (!multiAttributeParser2.load(args3, out errMsg))
				{
					return false;
				}
				if (!multiAttributeParser2.getIntAttribute("count", out value7, out errMsg))
				{
					return false;
				}
				if (!multiAttributeParser2.getIntAttribute("typeID", out value8, out errMsg))
				{
					return false;
				}
			}
			resource = new PackCheatResource
			{
				PackCount = value7,
				TypeID = value8
			};
			return true;
		}
		case "hero":
		{
			string value = null;
			string value2 = null;
			bool? value3 = null;
			int? value4 = null;
			int? value5 = null;
			if (args.Length > 1)
			{
				string[] args2 = args.Slice(1);
				MultiAttributeParser multiAttributeParser = new MultiAttributeParser();
				if (!multiAttributeParser.load(args2, out errMsg))
				{
					return false;
				}
				multiAttributeParser.getStringAttribute("class", out value);
				multiAttributeParser.getStringAttribute("gametype", out value2);
				if (!multiAttributeParser.getIntAttribute("level", out value4, out errMsg))
				{
					return false;
				}
				if (!multiAttributeParser.getIntAttribute("wins", out value5, out errMsg))
				{
					return false;
				}
				if (!multiAttributeParser.getBoolAttribute("golden", out value3, out errMsg))
				{
					return false;
				}
			}
			TAG_PREMIUM value6 = ((value3.HasValue && value3.Value) ? TAG_PREMIUM.GOLDEN : TAG_PREMIUM.NORMAL);
			resource = new HeroCheatResource
			{
				ClassName = value,
				Level = value4,
				Wins = value5,
				Gametype = value2,
				Premium = value6
			};
			return true;
		}
		case "adventureownership":
			resource = new AllAdventureOwnershipCheatResource();
			return true;
		default:
			errMsg = "Missing valid resource. You must specify one of the following valid resources: cards, gold, dust, tutorial, hero, pack, arenaticket, arena.";
			return false;
		}
	}
}
