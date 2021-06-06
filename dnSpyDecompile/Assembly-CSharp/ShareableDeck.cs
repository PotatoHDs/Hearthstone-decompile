using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using PegasusShared;
using PegasusUtil;

// Token: 0x0200013D RID: 317
public class ShareableDeck
{
	// Token: 0x170000F7 RID: 247
	// (get) Token: 0x060014BE RID: 5310 RVA: 0x000768B9 File Offset: 0x00074AB9
	// (set) Token: 0x060014BF RID: 5311 RVA: 0x000768C1 File Offset: 0x00074AC1
	private int VersionNumber { get; set; }

	// Token: 0x170000F8 RID: 248
	// (get) Token: 0x060014C0 RID: 5312 RVA: 0x000768CA File Offset: 0x00074ACA
	// (set) Token: 0x060014C1 RID: 5313 RVA: 0x000768D2 File Offset: 0x00074AD2
	public string DeckName { get; set; }

	// Token: 0x170000F9 RID: 249
	// (get) Token: 0x060014C2 RID: 5314 RVA: 0x000768DB File Offset: 0x00074ADB
	// (set) Token: 0x060014C3 RID: 5315 RVA: 0x000768E3 File Offset: 0x00074AE3
	public int HeroCardDbId { get; set; }

	// Token: 0x170000FA RID: 250
	// (get) Token: 0x060014C4 RID: 5316 RVA: 0x000768EC File Offset: 0x00074AEC
	// (set) Token: 0x060014C5 RID: 5317 RVA: 0x000768F4 File Offset: 0x00074AF4
	public DeckContents DeckContents { get; set; }

	// Token: 0x170000FB RID: 251
	// (get) Token: 0x060014C6 RID: 5318 RVA: 0x000768FD File Offset: 0x00074AFD
	// (set) Token: 0x060014C7 RID: 5319 RVA: 0x00076905 File Offset: 0x00074B05
	public FormatType FormatType { get; set; }

	// Token: 0x170000FC RID: 252
	// (get) Token: 0x060014C8 RID: 5320 RVA: 0x0007690E File Offset: 0x00074B0E
	// (set) Token: 0x060014C9 RID: 5321 RVA: 0x00076916 File Offset: 0x00074B16
	public bool IsArenaDeck { get; set; }

	// Token: 0x060014CA RID: 5322 RVA: 0x0007691F File Offset: 0x00074B1F
	private ShareableDeck()
	{
		this.VersionNumber = 1;
		this.DeckName = string.Empty;
		this.HeroCardDbId = 0;
		this.DeckContents = new DeckContents();
		this.FormatType = FormatType.FT_UNKNOWN;
		this.IsArenaDeck = false;
	}

	// Token: 0x060014CB RID: 5323 RVA: 0x00076959 File Offset: 0x00074B59
	public ShareableDeck(string deckName, int heroCardDbId, DeckContents deckContents, FormatType formatType, bool isArenaDeck)
	{
		this.DeckName = deckName;
		this.HeroCardDbId = heroCardDbId;
		this.DeckContents = deckContents;
		this.FormatType = formatType;
		this.IsArenaDeck = isArenaDeck;
		this.VersionNumber = 1;
	}

	// Token: 0x060014CC RID: 5324 RVA: 0x0007698D File Offset: 0x00074B8D
	public static ShareableDeck DeserializeFromClipboard()
	{
		return ShareableDeck.Deserialize(ClipboardUtils.PastedStringFromClipboard);
	}

	// Token: 0x060014CD RID: 5325 RVA: 0x0007699C File Offset: 0x00074B9C
	public static ShareableDeck Deserialize(string pastedString)
	{
		if (string.IsNullOrEmpty(pastedString))
		{
			return null;
		}
		bool flag = false;
		ShareableDeck shareableDeck = new ShareableDeck();
		try
		{
			string deckName;
			string text = ShareableDeck.ParseDataFromDeckString(pastedString, out deckName);
			if (string.IsNullOrEmpty(text))
			{
				return null;
			}
			shareableDeck.DeckName = deckName;
			using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(text)))
			{
				if (!ShareableDeck.IsValidEncodedDeckHeader(memoryStream))
				{
					return null;
				}
				if (!ShareableDeck.DeserializeFromVersion((int)ProtocolParser.ReadUInt64(memoryStream), shareableDeck, memoryStream, ref flag))
				{
					return null;
				}
			}
		}
		catch (Exception)
		{
			return null;
		}
		if (flag)
		{
			shareableDeck.FormatType = FormatType.FT_WILD;
		}
		else if (!CollectionManager.Get().ShouldAccountSeeStandardWild())
		{
			shareableDeck.FormatType = FormatType.FT_STANDARD;
		}
		return shareableDeck;
	}

	// Token: 0x060014CE RID: 5326 RVA: 0x00076A64 File Offset: 0x00074C64
	private static bool DeserializeFromVersion(int versionNumber, ShareableDeck shareableDeck, MemoryStream stream, ref bool deckHasWildCards)
	{
		if (versionNumber != 0)
		{
			if (versionNumber != 1)
			{
			}
			return ShareableDeck.DeserializeFromVersion_1(shareableDeck, stream, ref deckHasWildCards);
		}
		return ShareableDeck.DeserializeFromVersion_0(shareableDeck, stream, ref deckHasWildCards);
	}

	// Token: 0x060014CF RID: 5327 RVA: 0x00076A80 File Offset: 0x00074C80
	private static bool DeserializeFromVersion_0(ShareableDeck shareableDeck, MemoryStream stream, ref bool deckHasWildCards)
	{
		ulong num = ProtocolParser.ReadUInt64(stream);
		for (ulong num2 = 0UL; num2 < num; num2 += 1UL)
		{
			shareableDeck.HeroCardDbId = (int)ProtocolParser.ReadUInt64(stream);
		}
		if (!GameDbf.Card.HasRecord(shareableDeck.HeroCardDbId))
		{
			return false;
		}
		string cardId = GameUtils.TranslateDbIdToCardId(shareableDeck.HeroCardDbId, false);
		if (!DefLoader.Get().GetEntityDef(cardId).IsHeroSkin())
		{
			return false;
		}
		shareableDeck.FormatType = (FormatType)ProtocolParser.ReadUInt64(stream);
		if (shareableDeck.FormatType != FormatType.FT_WILD && shareableDeck.FormatType != FormatType.FT_STANDARD)
		{
			return false;
		}
		if (!ShareableDeck.Deserialize_ReadArrayOfCards(1, TAG_PREMIUM.NORMAL, shareableDeck, stream, ref deckHasWildCards))
		{
			return false;
		}
		if (!ShareableDeck.Deserialize_ReadArrayOfCards(1, TAG_PREMIUM.GOLDEN, shareableDeck, stream, ref deckHasWildCards))
		{
			return false;
		}
		ulong num3 = ProtocolParser.ReadUInt64(stream);
		uint num4 = 0U;
		while ((ulong)num4 < num3)
		{
			int num5 = (int)ProtocolParser.ReadUInt64(stream);
			ulong num6 = ProtocolParser.ReadUInt64(stream);
			if (!GameDbf.Card.HasRecord(num5) || !GameUtils.IsCardCollectible(GameUtils.TranslateDbIdToCardId(num5, false)))
			{
				return false;
			}
			if (GameUtils.IsWildCard(GameUtils.TranslateDbIdToCardId(num5, false)))
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
			num4 += 1U;
		}
		ulong num7 = ProtocolParser.ReadUInt64(stream);
		for (ulong num8 = 0UL; num8 < num7; num8 += 1UL)
		{
			int num9 = (int)ProtocolParser.ReadUInt64(stream);
			ulong num10 = ProtocolParser.ReadUInt64(stream);
			if (!GameDbf.Card.HasRecord(num9) || !GameUtils.IsCardCollectible(GameUtils.TranslateDbIdToCardId(num9, false)))
			{
				return false;
			}
			if (GameUtils.IsWildCard(GameUtils.TranslateDbIdToCardId(num9, false)))
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

	// Token: 0x060014D0 RID: 5328 RVA: 0x00076C60 File Offset: 0x00074E60
	private static bool DeserializeFromVersion_1(ShareableDeck shareableDeck, MemoryStream stream, ref bool deckHasWildCards)
	{
		ulong num = ProtocolParser.ReadUInt64(stream);
		if (num <= 0UL)
		{
			return false;
		}
		bool flag = false;
		using (IEnumerator enumerator = Enum.GetValues(typeof(FormatType)).GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if ((long)((FormatType)enumerator.Current) == (long)num)
				{
					flag = true;
					break;
				}
			}
		}
		if (!flag)
		{
			return false;
		}
		shareableDeck.FormatType = (FormatType)num;
		ulong num2 = ProtocolParser.ReadUInt64(stream);
		for (ulong num3 = 0UL; num3 < num2; num3 += 1UL)
		{
			shareableDeck.HeroCardDbId = (int)ProtocolParser.ReadUInt64(stream);
		}
		if (!GameDbf.Card.HasRecord(shareableDeck.HeroCardDbId))
		{
			return false;
		}
		string cardId = GameUtils.TranslateDbIdToCardId(shareableDeck.HeroCardDbId, false);
		EntityDef entityDef = DefLoader.Get().GetEntityDef(cardId);
		if (!entityDef.IsHeroSkin())
		{
			return false;
		}
		if (shareableDeck.FormatType == FormatType.FT_CLASSIC && !GameUtils.CLASSIC_ORDERED_HERO_CLASSES.Contains(entityDef.GetClass()))
		{
			return false;
		}
		if (!ShareableDeck.Deserialize_ReadArrayOfCards(1, TAG_PREMIUM.NORMAL, shareableDeck, stream, ref deckHasWildCards))
		{
			return false;
		}
		if (!ShareableDeck.Deserialize_ReadArrayOfCards(2, TAG_PREMIUM.NORMAL, shareableDeck, stream, ref deckHasWildCards))
		{
			return false;
		}
		ulong num4 = ProtocolParser.ReadUInt64(stream);
		uint num5 = 0U;
		while ((ulong)num5 < num4)
		{
			int cardDbId = (int)ProtocolParser.ReadUInt64(stream);
			ulong num6 = ProtocolParser.ReadUInt64(stream);
			if (!GameDbf.Card.HasRecord(cardDbId))
			{
				return false;
			}
			if (GameUtils.IsWildCard(GameUtils.TranslateDbIdToCardId(cardDbId, false)))
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
			num5 += 1U;
		}
		return true;
	}

	// Token: 0x060014D1 RID: 5329 RVA: 0x00076E60 File Offset: 0x00075060
	private static bool Deserialize_ReadArrayOfCards(int quantityPerCard, TAG_PREMIUM premium, ShareableDeck shareableDeck, MemoryStream stream, ref bool deckHasWildCards)
	{
		ulong num = ProtocolParser.ReadUInt64(stream);
		for (ulong num2 = 0UL; num2 < num; num2 += 1UL)
		{
			int cardDbId = (int)ProtocolParser.ReadUInt64(stream);
			if (!GameDbf.Card.HasRecord(cardDbId))
			{
				return false;
			}
			if (GameUtils.IsWildCard(GameUtils.TranslateDbIdToCardId(cardDbId, false)))
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

	// Token: 0x060014D2 RID: 5330 RVA: 0x00076F34 File Offset: 0x00075134
	public string Serialize(bool includeComments = true)
	{
		string text = this.SerializeToVersion(1);
		if (includeComments)
		{
			return this.GetDeckStringWithComments(text);
		}
		return text;
	}

	// Token: 0x060014D3 RID: 5331 RVA: 0x00076F55 File Offset: 0x00075155
	private string SerializeToVersion(int versionNumber)
	{
		if (versionNumber != 0)
		{
			if (versionNumber != 1)
			{
			}
			return this.SerializeToVersion_1();
		}
		return this.SerializeToVersion_0();
	}

	// Token: 0x060014D4 RID: 5332 RVA: 0x00076F70 File Offset: 0x00075170
	private string SerializeToVersion_0()
	{
		if (this.DeckContents == null)
		{
			return null;
		}
		byte[] inArray = null;
		using (MemoryStream memoryStream = new MemoryStream())
		{
			memoryStream.WriteByte(0);
			ProtocolParser.WriteUInt64(memoryStream, 0UL);
			ProtocolParser.WriteUInt64(memoryStream, 1UL);
			ProtocolParser.WriteUInt64(memoryStream, (ulong)((long)this.HeroCardDbId));
			ProtocolParser.WriteUInt64(memoryStream, (ulong)Convert.ToUInt32(this.FormatType));
			int[] cardDbIds = (from d in this.DeckContents.Cards
			where d.Def.Premium == 0 && d.Qty == 1
			select d.Def.Asset).ToArray<int>();
			int[] cardDbIds2 = (from d in this.DeckContents.Cards
			where d.Def.Premium == 1 && d.Qty == 1
			select d.Def.Asset).ToArray<int>();
			DeckCardData[] array = (from d in this.DeckContents.Cards
			where d.Def.Premium == 0 && d.Qty != 1
			select d).ToArray<DeckCardData>();
			DeckCardData[] array2 = (from d in this.DeckContents.Cards
			where d.Def.Premium == 1 && d.Qty != 1
			select d).ToArray<DeckCardData>();
			this.Serialize_WriteArrayOfCards(cardDbIds, memoryStream);
			this.Serialize_WriteArrayOfCards(cardDbIds2, memoryStream);
			ProtocolParser.WriteUInt64(memoryStream, (ulong)((long)array.Length));
			foreach (DeckCardData deckCardData in array)
			{
				ProtocolParser.WriteUInt64(memoryStream, (ulong)((long)deckCardData.Def.Asset));
				ProtocolParser.WriteUInt64(memoryStream, (ulong)((long)Math.Max(0, deckCardData.Qty)));
			}
			ProtocolParser.WriteUInt64(memoryStream, (ulong)((long)array2.Count<DeckCardData>()));
			foreach (DeckCardData deckCardData2 in array2)
			{
				ProtocolParser.WriteUInt64(memoryStream, (ulong)((long)deckCardData2.Def.Asset));
				ProtocolParser.WriteUInt64(memoryStream, (ulong)((long)Math.Max(0, deckCardData2.Qty)));
			}
			inArray = memoryStream.ToArray();
		}
		return Convert.ToBase64String(inArray);
	}

	// Token: 0x060014D5 RID: 5333 RVA: 0x000771D0 File Offset: 0x000753D0
	private string SerializeToVersion_1()
	{
		if (this.DeckContents == null)
		{
			return null;
		}
		byte[] inArray = null;
		using (MemoryStream memoryStream = new MemoryStream())
		{
			memoryStream.WriteByte(0);
			ProtocolParser.WriteUInt64(memoryStream, 1UL);
			ProtocolParser.WriteUInt64(memoryStream, (ulong)Convert.ToUInt32(this.FormatType));
			ProtocolParser.WriteUInt64(memoryStream, 1UL);
			ProtocolParser.WriteUInt64(memoryStream, (ulong)((long)this.HeroCardDbId));
			int[] cardDbIds = (from d in this.DeckContents.Cards
			where d.Qty == 1
			select d.Def.Asset into d
			orderby d
			select d).ToArray<int>();
			int[] cardDbIds2 = (from d in this.DeckContents.Cards
			where d.Qty == 2
			select d.Def.Asset into d
			orderby d
			select d).ToArray<int>();
			DeckCardData[] array = (from d in this.DeckContents.Cards
			where d.Qty > 2
			orderby d.Def.Asset
			select d).ToArray<DeckCardData>();
			this.Serialize_WriteArrayOfCards(cardDbIds, memoryStream);
			this.Serialize_WriteArrayOfCards(cardDbIds2, memoryStream);
			ProtocolParser.WriteUInt64(memoryStream, (ulong)((long)array.Length));
			foreach (DeckCardData deckCardData in array)
			{
				ProtocolParser.WriteUInt64(memoryStream, (ulong)((long)deckCardData.Def.Asset));
				ProtocolParser.WriteUInt64(memoryStream, (ulong)((long)Math.Max(0, deckCardData.Qty)));
			}
			inArray = memoryStream.ToArray();
		}
		return Convert.ToBase64String(inArray);
	}

	// Token: 0x060014D6 RID: 5334 RVA: 0x00077414 File Offset: 0x00075614
	public void Serialize_WriteArrayOfCards(int[] cardDbIds, MemoryStream stream)
	{
		ProtocolParser.WriteUInt64(stream, (ulong)((long)cardDbIds.Length));
		foreach (int num in cardDbIds)
		{
			ProtocolParser.WriteUInt64(stream, (ulong)((long)num));
		}
	}

	// Token: 0x060014D7 RID: 5335 RVA: 0x00077448 File Offset: 0x00075648
	public int GetCardCountInDeck()
	{
		if (this.DeckContents == null)
		{
			return 0;
		}
		int num = 0;
		for (int i = 0; i < this.DeckContents.Cards.Count; i++)
		{
			num += this.DeckContents.Cards[i].Qty;
		}
		return num;
	}

	// Token: 0x060014D8 RID: 5336 RVA: 0x00077498 File Offset: 0x00075698
	public override bool Equals(object obj)
	{
		ShareableDeck shareableDeck = (ShareableDeck)obj;
		if (shareableDeck == null)
		{
			return false;
		}
		if (this.FormatType != shareableDeck.FormatType)
		{
			return false;
		}
		if (this.DeckContents == null && shareableDeck.DeckContents != null)
		{
			return false;
		}
		if (this.DeckContents != null && shareableDeck.DeckContents == null)
		{
			return false;
		}
		if (this.DeckContents == null && shareableDeck.DeckContents == null)
		{
			return true;
		}
		Dictionary<int, int> dictionary = new Dictionary<int, int>();
		Dictionary<int, int> dictionary2 = new Dictionary<int, int>();
		for (int i = 0; i < this.DeckContents.Cards.Count; i++)
		{
			if (dictionary.ContainsKey(this.DeckContents.Cards[i].Def.Asset))
			{
				Dictionary<int, int> dictionary3 = dictionary;
				int asset = this.DeckContents.Cards[i].Def.Asset;
				dictionary3[asset] += this.DeckContents.Cards[i].Qty;
			}
			else
			{
				dictionary[this.DeckContents.Cards[i].Def.Asset] = this.DeckContents.Cards[i].Qty;
			}
		}
		for (int j = 0; j < shareableDeck.DeckContents.Cards.Count; j++)
		{
			if (dictionary2.ContainsKey(shareableDeck.DeckContents.Cards[j].Def.Asset))
			{
				Dictionary<int, int> dictionary3 = dictionary2;
				int asset = shareableDeck.DeckContents.Cards[j].Def.Asset;
				dictionary3[asset] += shareableDeck.DeckContents.Cards[j].Qty;
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
		foreach (KeyValuePair<int, int> keyValuePair in dictionary)
		{
			if (!dictionary2.ContainsKey(keyValuePair.Key) || dictionary[keyValuePair.Key] != dictionary2[keyValuePair.Key])
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x060014D9 RID: 5337 RVA: 0x00077718 File Offset: 0x00075918
	public override int GetHashCode()
	{
		if (this.DeckContents != null)
		{
			return this.DeckContents.GetHashCode() ^ this.HeroCardDbId.GetHashCode();
		}
		return 0;
	}

	// Token: 0x060014DA RID: 5338 RVA: 0x0007774C File Offset: 0x0007594C
	private string GetDeckStringWithComments(string encodedDeck)
	{
		StringBuilder stringBuilder = new StringBuilder();
		string cardId = GameUtils.TranslateDbIdToCardId(this.HeroCardDbId, false);
		string className = GameStrings.GetClassName(DefLoader.Get().GetEntityDef(cardId).GetClass());
		string formatName = GameStrings.GetFormatName(this.FormatType);
		if (!this.IsArenaDeck)
		{
			stringBuilder.AppendFormat("{0} {1}\n", "###", this.DeckName);
		}
		else
		{
			stringBuilder.AppendFormat("{0} {1}\n", "###", GameStrings.Get("GLUE_COLLECTION_DECK_COPY_COMMENT_HEADER_DECK_ARENA"));
			stringBuilder.AppendFormat("{0}{1}\n", "# ", GameStrings.Get("GLUE_COLLECTION_DECK_PASTE_COMMENT_ARENA_WARNING"));
		}
		stringBuilder.Append("# ").AppendFormat(GameStrings.Get("GLUE_COLLECTION_DECK_COPY_COMMENT_HEADER_CLASS"), className).Append("\n");
		stringBuilder.Append("# ").AppendFormat(GameStrings.Get("GLUE_COLLECTION_DECK_COPY_COMMENT_HEADER_FORMAT"), formatName).Append("\n");
		if (this.FormatType == FormatType.FT_STANDARD)
		{
			string value = SpecialEventManager.Get().IsEventActive(SpecialEventType.SPECIAL_EVENT_POST_SET_ROTATION_2021, false) ? GameStrings.Get("GLUE_SET_ROTATION_NEXT_YEAR") : GameStrings.Get("GLUE_SET_ROTATION_CURRENT_YEAR");
			stringBuilder.Append("# ").Append(value).Append("\n");
		}
		stringBuilder.Append("#\n");
		if (this.DeckContents != null)
		{
			foreach (DeckCardData deckCardData in this.DeckContents.Cards)
			{
				EntityDef entityDef = DefLoader.Get().GetEntityDef(deckCardData.Def.Asset, true);
				stringBuilder.AppendFormat("# {0}x ({1}) {2}\n", deckCardData.Qty, entityDef.GetCost(), entityDef.GetName());
			}
		}
		stringBuilder.Append("# \n");
		stringBuilder.Append(encodedDeck + "\n");
		stringBuilder.Append("# \n");
		stringBuilder.Append("# " + GameStrings.Get("GLUE_COLLECTION_DECK_PASTE_COMMENT_INSTRUCTIONS") + "\n");
		return stringBuilder.ToString();
	}

	// Token: 0x060014DB RID: 5339 RVA: 0x0007796C File Offset: 0x00075B6C
	private static bool IsValidEncodedDeckHeader(Stream stream)
	{
		byte[] array = new byte[1];
		if (stream.Read(array, 0, array.Length) < array.Length)
		{
			return false;
		}
		int num = 0;
		return array[num++] == 0;
	}

	// Token: 0x060014DC RID: 5340 RVA: 0x000779A0 File Offset: 0x00075BA0
	private static string ParseDataFromDeckString(string deckString, out string deckName)
	{
		string[] source = deckString.Split(new string[]
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
		if (text != null)
		{
			return text.Trim();
		}
		return null;
	}

	// Token: 0x04000DE5 RID: 3557
	public const int VERSION_NUMBER_ZERO = 0;

	// Token: 0x04000DE6 RID: 3558
	public const int VERSION_NUMBER_ONE = 1;

	// Token: 0x04000DE7 RID: 3559
	public const int VERSION_NUMBER_CURRENT = 1;

	// Token: 0x04000DEE RID: 3566
	public const string CommentLinePrefix = "# ";

	// Token: 0x04000DEF RID: 3567
	public const string DeckNameLinePrefix = "###";
}
