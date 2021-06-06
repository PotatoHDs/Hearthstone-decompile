using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using PegasusShared;
using PegasusUtil;

public class ShareableDeck
{
	public const int VERSION_NUMBER_ZERO = 0;

	public const int VERSION_NUMBER_ONE = 1;

	public const int VERSION_NUMBER_CURRENT = 1;

	public const string CommentLinePrefix = "# ";

	public const string DeckNameLinePrefix = "###";

	private int VersionNumber { get; set; }

	public string DeckName { get; set; }

	public int HeroCardDbId { get; set; }

	public DeckContents DeckContents { get; set; }

	public FormatType FormatType { get; set; }

	public bool IsArenaDeck { get; set; }

	private ShareableDeck()
	{
		VersionNumber = 1;
		DeckName = string.Empty;
		HeroCardDbId = 0;
		DeckContents = new DeckContents();
		FormatType = FormatType.FT_UNKNOWN;
		IsArenaDeck = false;
	}

	public ShareableDeck(string deckName, int heroCardDbId, DeckContents deckContents, FormatType formatType, bool isArenaDeck)
	{
		DeckName = deckName;
		HeroCardDbId = heroCardDbId;
		DeckContents = deckContents;
		FormatType = formatType;
		IsArenaDeck = isArenaDeck;
		VersionNumber = 1;
	}

	public static ShareableDeck DeserializeFromClipboard()
	{
		return Deserialize(ClipboardUtils.PastedStringFromClipboard);
	}

	public static ShareableDeck Deserialize(string pastedString)
	{
		if (string.IsNullOrEmpty(pastedString))
		{
			return null;
		}
		bool deckHasWildCards = false;
		ShareableDeck shareableDeck = new ShareableDeck();
		try
		{
			string deckName;
			string text = ParseDataFromDeckString(pastedString, out deckName);
			if (string.IsNullOrEmpty(text))
			{
				return null;
			}
			shareableDeck.DeckName = deckName;
			using MemoryStream stream = new MemoryStream(Convert.FromBase64String(text));
			if (!IsValidEncodedDeckHeader(stream))
			{
				return null;
			}
			if (!DeserializeFromVersion((int)ProtocolParser.ReadUInt64(stream), shareableDeck, stream, ref deckHasWildCards))
			{
				return null;
			}
		}
		catch (Exception)
		{
			return null;
		}
		if (deckHasWildCards)
		{
			shareableDeck.FormatType = FormatType.FT_WILD;
		}
		else if (!CollectionManager.Get().ShouldAccountSeeStandardWild())
		{
			shareableDeck.FormatType = FormatType.FT_STANDARD;
		}
		return shareableDeck;
	}

	private static bool DeserializeFromVersion(int versionNumber, ShareableDeck shareableDeck, MemoryStream stream, ref bool deckHasWildCards)
	{
		return versionNumber switch
		{
			0 => DeserializeFromVersion_0(shareableDeck, stream, ref deckHasWildCards), 
			_ => DeserializeFromVersion_1(shareableDeck, stream, ref deckHasWildCards), 
		};
	}

	private static bool DeserializeFromVersion_0(ShareableDeck shareableDeck, MemoryStream stream, ref bool deckHasWildCards)
	{
		ulong num = ProtocolParser.ReadUInt64(stream);
		for (ulong num2 = 0uL; num2 < num; num2++)
		{
			shareableDeck.HeroCardDbId = (int)ProtocolParser.ReadUInt64(stream);
		}
		if (!GameDbf.Card.HasRecord(shareableDeck.HeroCardDbId))
		{
			return false;
		}
		string cardId = GameUtils.TranslateDbIdToCardId(shareableDeck.HeroCardDbId);
		if (!DefLoader.Get().GetEntityDef(cardId).IsHeroSkin())
		{
			return false;
		}
		shareableDeck.FormatType = (FormatType)ProtocolParser.ReadUInt64(stream);
		if (shareableDeck.FormatType != FormatType.FT_WILD && shareableDeck.FormatType != FormatType.FT_STANDARD)
		{
			return false;
		}
		if (!Deserialize_ReadArrayOfCards(1, TAG_PREMIUM.NORMAL, shareableDeck, stream, ref deckHasWildCards))
		{
			return false;
		}
		if (!Deserialize_ReadArrayOfCards(1, TAG_PREMIUM.GOLDEN, shareableDeck, stream, ref deckHasWildCards))
		{
			return false;
		}
		ulong num3 = ProtocolParser.ReadUInt64(stream);
		for (uint num4 = 0u; num4 < num3; num4++)
		{
			int num5 = (int)ProtocolParser.ReadUInt64(stream);
			ulong num6 = ProtocolParser.ReadUInt64(stream);
			if (!GameDbf.Card.HasRecord(num5) || !GameUtils.IsCardCollectible(GameUtils.TranslateDbIdToCardId(num5)))
			{
				return false;
			}
			if (GameUtils.IsWildCard(GameUtils.TranslateDbIdToCardId(num5)))
			{
				deckHasWildCards = true;
			}
			DeckCardData item = new DeckCardData
			{
				Def = new PegasusShared.CardDef
				{
					Premium = 0,
					Asset = num5
				},
				Qty = (int)num6
			};
			shareableDeck.DeckContents.Cards.Add(item);
		}
		ulong num7 = ProtocolParser.ReadUInt64(stream);
		for (ulong num8 = 0uL; num8 < num7; num8++)
		{
			int num9 = (int)ProtocolParser.ReadUInt64(stream);
			ulong num10 = ProtocolParser.ReadUInt64(stream);
			if (!GameDbf.Card.HasRecord(num9) || !GameUtils.IsCardCollectible(GameUtils.TranslateDbIdToCardId(num9)))
			{
				return false;
			}
			if (GameUtils.IsWildCard(GameUtils.TranslateDbIdToCardId(num9)))
			{
				deckHasWildCards = true;
			}
			DeckCardData item2 = new DeckCardData
			{
				Def = new PegasusShared.CardDef
				{
					Premium = 1,
					Asset = num9
				},
				Qty = (int)num10
			};
			shareableDeck.DeckContents.Cards.Add(item2);
		}
		return true;
	}

	private static bool DeserializeFromVersion_1(ShareableDeck shareableDeck, MemoryStream stream, ref bool deckHasWildCards)
	{
		ulong num = ProtocolParser.ReadUInt64(stream);
		if (num == 0)
		{
			return false;
		}
		bool flag = false;
		foreach (FormatType value in Enum.GetValues(typeof(FormatType)))
		{
			if ((ulong)value == num)
			{
				flag = true;
				break;
			}
		}
		if (!flag)
		{
			return false;
		}
		shareableDeck.FormatType = (FormatType)num;
		ulong num2 = ProtocolParser.ReadUInt64(stream);
		for (ulong num3 = 0uL; num3 < num2; num3++)
		{
			shareableDeck.HeroCardDbId = (int)ProtocolParser.ReadUInt64(stream);
		}
		if (!GameDbf.Card.HasRecord(shareableDeck.HeroCardDbId))
		{
			return false;
		}
		string cardId = GameUtils.TranslateDbIdToCardId(shareableDeck.HeroCardDbId);
		EntityDef entityDef = DefLoader.Get().GetEntityDef(cardId);
		if (!entityDef.IsHeroSkin())
		{
			return false;
		}
		if (shareableDeck.FormatType == FormatType.FT_CLASSIC && !GameUtils.CLASSIC_ORDERED_HERO_CLASSES.Contains(entityDef.GetClass()))
		{
			return false;
		}
		if (!Deserialize_ReadArrayOfCards(1, TAG_PREMIUM.NORMAL, shareableDeck, stream, ref deckHasWildCards))
		{
			return false;
		}
		if (!Deserialize_ReadArrayOfCards(2, TAG_PREMIUM.NORMAL, shareableDeck, stream, ref deckHasWildCards))
		{
			return false;
		}
		ulong num4 = ProtocolParser.ReadUInt64(stream);
		for (uint num5 = 0u; num5 < num4; num5++)
		{
			int cardDbId = (int)ProtocolParser.ReadUInt64(stream);
			ulong num6 = ProtocolParser.ReadUInt64(stream);
			if (!GameDbf.Card.HasRecord(cardDbId))
			{
				return false;
			}
			if (GameUtils.IsWildCard(GameUtils.TranslateDbIdToCardId(cardDbId)))
			{
				deckHasWildCards = true;
			}
			DeckCardData deckCardData2 = shareableDeck.DeckContents.Cards.FirstOrDefault((DeckCardData deckCardData) => deckCardData != null && deckCardData.Def != null && deckCardData.Def.Asset == cardDbId && deckCardData.Def.Premium == 0);
			if (deckCardData2 == null)
			{
				deckCardData2 = new DeckCardData
				{
					Def = new PegasusShared.CardDef
					{
						Premium = 0,
						Asset = cardDbId
					},
					Qty = (int)num6
				};
			}
			else
			{
				deckCardData2.Qty += (int)num6;
			}
			shareableDeck.DeckContents.Cards.Add(deckCardData2);
		}
		return true;
	}

	private static bool Deserialize_ReadArrayOfCards(int quantityPerCard, TAG_PREMIUM premium, ShareableDeck shareableDeck, MemoryStream stream, ref bool deckHasWildCards)
	{
		ulong num = ProtocolParser.ReadUInt64(stream);
		for (ulong num2 = 0uL; num2 < num; num2++)
		{
			int cardDbId = (int)ProtocolParser.ReadUInt64(stream);
			if (!GameDbf.Card.HasRecord(cardDbId))
			{
				return false;
			}
			if (GameUtils.IsWildCard(GameUtils.TranslateDbIdToCardId(cardDbId)))
			{
				deckHasWildCards = true;
			}
			DeckCardData deckCardData2 = shareableDeck.DeckContents.Cards.FirstOrDefault((DeckCardData deckCardData) => deckCardData != null && deckCardData.Def != null && deckCardData.Def.Asset == cardDbId && deckCardData.Def.Premium == 0);
			if (deckCardData2 == null)
			{
				deckCardData2 = new DeckCardData
				{
					Def = new PegasusShared.CardDef
					{
						Premium = (int)premium,
						Asset = cardDbId
					},
					Qty = quantityPerCard
				};
			}
			else
			{
				deckCardData2.Qty += quantityPerCard;
			}
			shareableDeck.DeckContents.Cards.Add(deckCardData2);
		}
		return true;
	}

	public string Serialize(bool includeComments = true)
	{
		string text = SerializeToVersion(1);
		if (includeComments)
		{
			return GetDeckStringWithComments(text);
		}
		return text;
	}

	private string SerializeToVersion(int versionNumber)
	{
		return versionNumber switch
		{
			0 => SerializeToVersion_0(), 
			_ => SerializeToVersion_1(), 
		};
	}

	private string SerializeToVersion_0()
	{
		if (DeckContents == null)
		{
			return null;
		}
		byte[] inArray = null;
		using (MemoryStream memoryStream = new MemoryStream())
		{
			memoryStream.WriteByte(0);
			ProtocolParser.WriteUInt64(memoryStream, 0uL);
			ProtocolParser.WriteUInt64(memoryStream, 1uL);
			ProtocolParser.WriteUInt64(memoryStream, (ulong)HeroCardDbId);
			ProtocolParser.WriteUInt64(memoryStream, Convert.ToUInt32(FormatType));
			int[] cardDbIds = (from d in DeckContents.Cards
				where d.Def.Premium == 0 && d.Qty == 1
				select d.Def.Asset).ToArray();
			int[] cardDbIds2 = (from d in DeckContents.Cards
				where d.Def.Premium == 1 && d.Qty == 1
				select d.Def.Asset).ToArray();
			DeckCardData[] array = DeckContents.Cards.Where((DeckCardData d) => d.Def.Premium == 0 && d.Qty != 1).ToArray();
			DeckCardData[] array2 = DeckContents.Cards.Where((DeckCardData d) => d.Def.Premium == 1 && d.Qty != 1).ToArray();
			Serialize_WriteArrayOfCards(cardDbIds, memoryStream);
			Serialize_WriteArrayOfCards(cardDbIds2, memoryStream);
			ProtocolParser.WriteUInt64(memoryStream, (ulong)array.Length);
			DeckCardData[] array3 = array;
			foreach (DeckCardData deckCardData in array3)
			{
				ProtocolParser.WriteUInt64(memoryStream, (ulong)deckCardData.Def.Asset);
				ProtocolParser.WriteUInt64(memoryStream, (ulong)Math.Max(0, deckCardData.Qty));
			}
			ProtocolParser.WriteUInt64(memoryStream, (ulong)array2.Count());
			array3 = array2;
			foreach (DeckCardData deckCardData2 in array3)
			{
				ProtocolParser.WriteUInt64(memoryStream, (ulong)deckCardData2.Def.Asset);
				ProtocolParser.WriteUInt64(memoryStream, (ulong)Math.Max(0, deckCardData2.Qty));
			}
			inArray = memoryStream.ToArray();
		}
		return Convert.ToBase64String(inArray);
	}

	private string SerializeToVersion_1()
	{
		if (DeckContents == null)
		{
			return null;
		}
		byte[] inArray = null;
		using (MemoryStream memoryStream = new MemoryStream())
		{
			memoryStream.WriteByte(0);
			ProtocolParser.WriteUInt64(memoryStream, 1uL);
			ProtocolParser.WriteUInt64(memoryStream, Convert.ToUInt32(FormatType));
			ProtocolParser.WriteUInt64(memoryStream, 1uL);
			ProtocolParser.WriteUInt64(memoryStream, (ulong)HeroCardDbId);
			int[] cardDbIds = (from d in DeckContents.Cards
				where d.Qty == 1
				select d.Def.Asset into d
				orderby d
				select d).ToArray();
			int[] cardDbIds2 = (from d in DeckContents.Cards
				where d.Qty == 2
				select d.Def.Asset into d
				orderby d
				select d).ToArray();
			DeckCardData[] array = (from d in DeckContents.Cards
				where d.Qty > 2
				orderby d.Def.Asset
				select d).ToArray();
			Serialize_WriteArrayOfCards(cardDbIds, memoryStream);
			Serialize_WriteArrayOfCards(cardDbIds2, memoryStream);
			ProtocolParser.WriteUInt64(memoryStream, (ulong)array.Length);
			DeckCardData[] array2 = array;
			foreach (DeckCardData deckCardData in array2)
			{
				ProtocolParser.WriteUInt64(memoryStream, (ulong)deckCardData.Def.Asset);
				ProtocolParser.WriteUInt64(memoryStream, (ulong)Math.Max(0, deckCardData.Qty));
			}
			inArray = memoryStream.ToArray();
		}
		return Convert.ToBase64String(inArray);
	}

	public void Serialize_WriteArrayOfCards(int[] cardDbIds, MemoryStream stream)
	{
		ProtocolParser.WriteUInt64(stream, (ulong)cardDbIds.Length);
		foreach (int num in cardDbIds)
		{
			ProtocolParser.WriteUInt64(stream, (ulong)num);
		}
	}

	public int GetCardCountInDeck()
	{
		if (DeckContents == null)
		{
			return 0;
		}
		int num = 0;
		for (int i = 0; i < DeckContents.Cards.Count; i++)
		{
			num += DeckContents.Cards[i].Qty;
		}
		return num;
	}

	public override bool Equals(object obj)
	{
		ShareableDeck shareableDeck = (ShareableDeck)obj;
		if (shareableDeck == null)
		{
			return false;
		}
		if (FormatType != shareableDeck.FormatType)
		{
			return false;
		}
		if (DeckContents == null && shareableDeck.DeckContents != null)
		{
			return false;
		}
		if (DeckContents != null && shareableDeck.DeckContents == null)
		{
			return false;
		}
		if (DeckContents == null && shareableDeck.DeckContents == null)
		{
			return true;
		}
		Dictionary<int, int> dictionary = new Dictionary<int, int>();
		Dictionary<int, int> dictionary2 = new Dictionary<int, int>();
		for (int i = 0; i < DeckContents.Cards.Count; i++)
		{
			if (dictionary.ContainsKey(DeckContents.Cards[i].Def.Asset))
			{
				dictionary[DeckContents.Cards[i].Def.Asset] += DeckContents.Cards[i].Qty;
			}
			else
			{
				dictionary[DeckContents.Cards[i].Def.Asset] = DeckContents.Cards[i].Qty;
			}
		}
		for (int j = 0; j < shareableDeck.DeckContents.Cards.Count; j++)
		{
			if (dictionary2.ContainsKey(shareableDeck.DeckContents.Cards[j].Def.Asset))
			{
				dictionary2[shareableDeck.DeckContents.Cards[j].Def.Asset] += shareableDeck.DeckContents.Cards[j].Qty;
			}
			else
			{
				dictionary2[shareableDeck.DeckContents.Cards[j].Def.Asset] = shareableDeck.DeckContents.Cards[j].Qty;
			}
		}
		if (dictionary.Count != dictionary2.Count)
		{
			return false;
		}
		foreach (KeyValuePair<int, int> item in dictionary)
		{
			if (!dictionary2.ContainsKey(item.Key) || dictionary[item.Key] != dictionary2[item.Key])
			{
				return false;
			}
		}
		return true;
	}

	public override int GetHashCode()
	{
		if (DeckContents != null)
		{
			return DeckContents.GetHashCode() ^ HeroCardDbId.GetHashCode();
		}
		return 0;
	}

	private string GetDeckStringWithComments(string encodedDeck)
	{
		StringBuilder stringBuilder = new StringBuilder();
		string cardId = GameUtils.TranslateDbIdToCardId(HeroCardDbId);
		string className = GameStrings.GetClassName(DefLoader.Get().GetEntityDef(cardId).GetClass());
		string formatName = GameStrings.GetFormatName(FormatType);
		if (!IsArenaDeck)
		{
			stringBuilder.AppendFormat("{0} {1}\n", "###", DeckName);
		}
		else
		{
			stringBuilder.AppendFormat("{0} {1}\n", "###", GameStrings.Get("GLUE_COLLECTION_DECK_COPY_COMMENT_HEADER_DECK_ARENA"));
			stringBuilder.AppendFormat("{0}{1}\n", "# ", GameStrings.Get("GLUE_COLLECTION_DECK_PASTE_COMMENT_ARENA_WARNING"));
		}
		stringBuilder.Append("# ").AppendFormat(GameStrings.Get("GLUE_COLLECTION_DECK_COPY_COMMENT_HEADER_CLASS"), className).Append("\n");
		stringBuilder.Append("# ").AppendFormat(GameStrings.Get("GLUE_COLLECTION_DECK_COPY_COMMENT_HEADER_FORMAT"), formatName).Append("\n");
		if (FormatType == FormatType.FT_STANDARD)
		{
			string value = (SpecialEventManager.Get().IsEventActive(SpecialEventType.SPECIAL_EVENT_POST_SET_ROTATION_2021, activeIfDoesNotExist: false) ? GameStrings.Get("GLUE_SET_ROTATION_NEXT_YEAR") : GameStrings.Get("GLUE_SET_ROTATION_CURRENT_YEAR"));
			stringBuilder.Append("# ").Append(value).Append("\n");
		}
		stringBuilder.Append("#\n");
		if (DeckContents != null)
		{
			foreach (DeckCardData card in DeckContents.Cards)
			{
				EntityDef entityDef = DefLoader.Get().GetEntityDef(card.Def.Asset);
				stringBuilder.AppendFormat("# {0}x ({1}) {2}\n", card.Qty, entityDef.GetCost(), entityDef.GetName());
			}
		}
		stringBuilder.Append("# \n");
		stringBuilder.Append(encodedDeck + "\n");
		stringBuilder.Append("# \n");
		stringBuilder.Append("# " + GameStrings.Get("GLUE_COLLECTION_DECK_PASTE_COMMENT_INSTRUCTIONS") + "\n");
		return stringBuilder.ToString();
	}

	private static bool IsValidEncodedDeckHeader(Stream stream)
	{
		byte[] array = new byte[1];
		if (stream.Read(array, 0, array.Length) < array.Length)
		{
			return false;
		}
		int num = 0;
		if (array[num++] != 0)
		{
			return false;
		}
		return true;
	}

	private static string ParseDataFromDeckString(string deckString, out string deckName)
	{
		string[] source = deckString.Split(new string[3]
		{
			Environment.NewLine,
			"\r",
			"\n"
		}, StringSplitOptions.RemoveEmptyEntries);
		string text = source.FirstOrDefault((string s) => !s.Trim().StartsWith("#"));
		string text2 = source.FirstOrDefault((string s) => s.Trim().StartsWith("###"));
		deckName = string.Empty;
		if (!string.IsNullOrEmpty(text2))
		{
			deckName = text2.Replace("###", string.Empty);
			deckName = deckName.Trim();
		}
		int defaultMaxDeckNameCharacters = CollectionDeck.DefaultMaxDeckNameCharacters;
		if (deckName.Length > defaultMaxDeckNameCharacters)
		{
			deckName = deckName.Substring(0, defaultMaxDeckNameCharacters);
		}
		return text?.Trim();
	}
}
