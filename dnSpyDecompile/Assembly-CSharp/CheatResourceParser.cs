using System;
using Blizzard.T5.Core;

// Token: 0x02000884 RID: 2180
public class CheatResourceParser
{
	// Token: 0x0600761A RID: 30234 RVA: 0x0025E4A0 File Offset: 0x0025C6A0
	public static bool TryParse(string[] args, out CheatResource resource, out string errMsg)
	{
		resource = null;
		errMsg = null;
		if (args.Length == 0)
		{
			errMsg = "Missing valid resource. You must specify one of the following valid resources: cards, gold, dust, tutorial, hero, pack, arenaticket, arena.";
			return false;
		}
		string[] array = args[0].Split(new char[]
		{
			'='
		});
		string text = array[0];
		uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
		if (num <= 1666399712U)
		{
			if (num <= 912332847U)
			{
				if (num != 456875097U)
				{
					if (num == 912332847U)
					{
						if (text == "tutorial")
						{
							int? progress = null;
							if (array.Length > 1)
							{
								int value;
								if (!int.TryParse(array[1], out value))
								{
									errMsg = "Failed to parse progress value. The amount must be a valid number.";
									return false;
								}
								progress = new int?(value);
							}
							resource = new TutorialCheatResource
							{
								Progress = progress
							};
							return true;
						}
					}
				}
				else if (text == "hero")
				{
					string className = null;
					string gametype = null;
					bool? flag = null;
					int? level = null;
					int? wins = null;
					if (args.Length > 1)
					{
						string[] args2 = args.Slice(1);
						MultiAttributeParser multiAttributeParser = new MultiAttributeParser();
						if (!multiAttributeParser.load(args2, out errMsg))
						{
							return false;
						}
						multiAttributeParser.getStringAttribute("class", out className);
						multiAttributeParser.getStringAttribute("gametype", out gametype);
						if (!multiAttributeParser.getIntAttribute("level", out level, out errMsg))
						{
							return false;
						}
						if (!multiAttributeParser.getIntAttribute("wins", out wins, out errMsg))
						{
							return false;
						}
						if (!multiAttributeParser.getBoolAttribute("golden", out flag, out errMsg))
						{
							return false;
						}
					}
					TAG_PREMIUM value2 = (flag != null && flag.Value) ? TAG_PREMIUM.GOLDEN : TAG_PREMIUM.NORMAL;
					resource = new HeroCheatResource
					{
						ClassName = className,
						Level = level,
						Wins = wins,
						Gametype = gametype,
						Premium = new TAG_PREMIUM?(value2)
					};
					return true;
				}
			}
			else if (num != 1379866916U)
			{
				if (num == 1666399712U)
				{
					if (text == "pack")
					{
						int? packCount = null;
						int? typeID = null;
						if (args.Length > 1)
						{
							string[] args3 = args.Slice(1);
							MultiAttributeParser multiAttributeParser2 = new MultiAttributeParser();
							if (!multiAttributeParser2.load(args3, out errMsg))
							{
								return false;
							}
							if (!multiAttributeParser2.getIntAttribute("count", out packCount, out errMsg))
							{
								return false;
							}
							if (!multiAttributeParser2.getIntAttribute("typeID", out typeID, out errMsg))
							{
								return false;
							}
						}
						resource = new PackCheatResource
						{
							PackCount = packCount,
							TypeID = typeID
						};
						return true;
					}
				}
			}
			else if (text == "adventureownership")
			{
				resource = new AllAdventureOwnershipCheatResource();
				return true;
			}
		}
		else if (num <= 1772234876U)
		{
			if (num != 1706796520U)
			{
				if (num == 1772234876U)
				{
					if (text == "arenaticket")
					{
						int? ticketCount = null;
						if (array.Length > 1)
						{
							int value3;
							if (!int.TryParse(array[1], out value3))
							{
								errMsg = "Failed to parse ticket count value. The amount must be a valid number.";
								return false;
							}
							ticketCount = new int?(value3);
						}
						resource = new ArenaTicketCheatResource
						{
							TicketCount = ticketCount
						};
						return true;
					}
				}
			}
			else if (text == "arena")
			{
				int? win = null;
				int? loss = null;
				if (args.Length > 1)
				{
					string[] args4 = args.Slice(1);
					MultiAttributeParser multiAttributeParser3 = new MultiAttributeParser();
					if (!multiAttributeParser3.load(args4, out errMsg))
					{
						return false;
					}
					if (!multiAttributeParser3.getIntAttribute("win", out win, out errMsg))
					{
						return false;
					}
					if (!multiAttributeParser3.getIntAttribute("loss", out loss, out errMsg))
					{
						return false;
					}
				}
				resource = new ArenaCheatResource
				{
					Win = win,
					Loss = loss
				};
				return true;
			}
		}
		else if (num != 2180079684U)
		{
			if (num != 3389733797U)
			{
				if (num == 3966162835U)
				{
					if (text == "gold")
					{
						int? amount = null;
						if (array.Length > 1)
						{
							int value4;
							if (!int.TryParse(array[1], out value4))
							{
								errMsg = "Failed to parse gold amount. The amount must be a valid number.";
								return false;
							}
							amount = new int?(value4);
						}
						resource = new GoldCheatResource
						{
							Amount = amount
						};
						return true;
					}
				}
			}
			else if (text == "dust")
			{
				int? amount2 = null;
				if (array.Length > 1)
				{
					int value5;
					if (!int.TryParse(array[1], out value5))
					{
						errMsg = "Failed to parse dust amount. The amount must be a valid number.";
						return false;
					}
					amount2 = new int?(value5);
				}
				resource = new DustCheatResource
				{
					Amount = amount2
				};
				return true;
			}
		}
		else if (text == "cards")
		{
			resource = new FullCardCollectionCheatResource();
			return true;
		}
		errMsg = "Missing valid resource. You must specify one of the following valid resources: cards, gold, dust, tutorial, hero, pack, arenaticket, arena.";
		return false;
	}

	// Token: 0x04005D59 RID: 23897
	private const string MISSING_VALID_RESOURCE_ERROR_MSG = "Missing valid resource. You must specify one of the following valid resources: cards, gold, dust, tutorial, hero, pack, arenaticket, arena.";

	// Token: 0x04005D5A RID: 23898
	private const string MISSING_VALID_ATTRIBUTE_ERROR_MSG = "Missing valid attribute. You must specify one of the following valid resources: class, level.";
}
