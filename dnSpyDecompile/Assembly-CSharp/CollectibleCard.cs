using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

// Token: 0x020000F3 RID: 243
public class CollectibleCard
{
	// Token: 0x170000A2 RID: 162
	// (get) Token: 0x06000E03 RID: 3587 RVA: 0x0004F1D7 File Offset: 0x0004D3D7
	public int CardDbId
	{
		get
		{
			return this.m_CardDbId;
		}
	}

	// Token: 0x170000A3 RID: 163
	// (get) Token: 0x06000E04 RID: 3588 RVA: 0x0004F1DF File Offset: 0x0004D3DF
	public string CardId
	{
		get
		{
			return this.m_EntityDef.GetCardId();
		}
	}

	// Token: 0x170000A4 RID: 164
	// (get) Token: 0x06000E05 RID: 3589 RVA: 0x0004F1EC File Offset: 0x0004D3EC
	public string Name
	{
		get
		{
			return CardTextBuilder.GetDefaultCardName(this.m_EntityDef);
		}
	}

	// Token: 0x170000A5 RID: 165
	// (get) Token: 0x06000E06 RID: 3590 RVA: 0x0004F1FC File Offset: 0x0004D3FC
	public string CardInHandText
	{
		get
		{
			CardTextBuilder cardTextBuilder = this.m_EntityDef.GetCardTextBuilder();
			if (cardTextBuilder != null)
			{
				return cardTextBuilder.BuildCardTextInHand(this.m_EntityDef);
			}
			return CardTextBuilder.GetDefaultCardTextInHand(this.m_EntityDef);
		}
	}

	// Token: 0x170000A6 RID: 166
	// (get) Token: 0x06000E07 RID: 3591 RVA: 0x0004F230 File Offset: 0x0004D430
	public string ArtistName
	{
		get
		{
			return this.m_EntityDef.GetArtistName();
		}
	}

	// Token: 0x170000A7 RID: 167
	// (get) Token: 0x06000E08 RID: 3592 RVA: 0x0004F23D File Offset: 0x0004D43D
	public int ManaCost
	{
		get
		{
			return this.m_EntityDef.GetCost();
		}
	}

	// Token: 0x170000A8 RID: 168
	// (get) Token: 0x06000E09 RID: 3593 RVA: 0x0004F24A File Offset: 0x0004D44A
	public int Attack
	{
		get
		{
			return this.m_EntityDef.GetATK();
		}
	}

	// Token: 0x170000A9 RID: 169
	// (get) Token: 0x06000E0A RID: 3594 RVA: 0x0004F257 File Offset: 0x0004D457
	public int Health
	{
		get
		{
			return this.m_EntityDef.GetHealth();
		}
	}

	// Token: 0x170000AA RID: 170
	// (get) Token: 0x06000E0B RID: 3595 RVA: 0x0004F264 File Offset: 0x0004D464
	public TAG_CARD_SET Set
	{
		get
		{
			return this.m_EntityDef.GetCardSet();
		}
	}

	// Token: 0x170000AB RID: 171
	// (get) Token: 0x06000E0C RID: 3596 RVA: 0x0004F271 File Offset: 0x0004D471
	public TAG_CLASS Class
	{
		get
		{
			return this.m_EntityDef.GetClass();
		}
	}

	// Token: 0x170000AC RID: 172
	// (get) Token: 0x06000E0D RID: 3597 RVA: 0x0004F27E File Offset: 0x0004D47E
	public IEnumerable<TAG_CLASS> Classes
	{
		get
		{
			return this.m_EntityDef.GetClasses(null);
		}
	}

	// Token: 0x170000AD RID: 173
	// (get) Token: 0x06000E0E RID: 3598 RVA: 0x0004F28C File Offset: 0x0004D48C
	public TAG_MULTI_CLASS_GROUP MultiClassGroup
	{
		get
		{
			return this.m_EntityDef.GetMultiClassGroup();
		}
	}

	// Token: 0x170000AE RID: 174
	// (get) Token: 0x06000E0F RID: 3599 RVA: 0x0004F299 File Offset: 0x0004D499
	public TAG_RARITY Rarity
	{
		get
		{
			return this.m_EntityDef.GetRarity();
		}
	}

	// Token: 0x170000AF RID: 175
	// (get) Token: 0x06000E10 RID: 3600 RVA: 0x0004F2A6 File Offset: 0x0004D4A6
	public TAG_RACE Race
	{
		get
		{
			return this.m_EntityDef.GetRace();
		}
	}

	// Token: 0x170000B0 RID: 176
	// (get) Token: 0x06000E11 RID: 3601 RVA: 0x0004F2B3 File Offset: 0x0004D4B3
	public TAG_CARDTYPE CardType
	{
		get
		{
			return this.m_EntityDef.GetCardType();
		}
	}

	// Token: 0x170000B1 RID: 177
	// (get) Token: 0x06000E12 RID: 3602 RVA: 0x0004F2C0 File Offset: 0x0004D4C0
	public bool IsHeroSkin
	{
		get
		{
			return this.m_EntityDef.IsHeroSkin();
		}
	}

	// Token: 0x170000B2 RID: 178
	// (get) Token: 0x06000E13 RID: 3603 RVA: 0x0004F2CD File Offset: 0x0004D4CD
	public TAG_PREMIUM PremiumType
	{
		get
		{
			return this.m_PremiumType;
		}
	}

	// Token: 0x170000B3 RID: 179
	// (get) Token: 0x06000E14 RID: 3604 RVA: 0x0004F2D5 File Offset: 0x0004D4D5
	// (set) Token: 0x06000E15 RID: 3605 RVA: 0x0004F2DD File Offset: 0x0004D4DD
	public int SeenCount { get; set; }

	// Token: 0x170000B4 RID: 180
	// (get) Token: 0x06000E16 RID: 3606 RVA: 0x0004F2E6 File Offset: 0x0004D4E6
	// (set) Token: 0x06000E17 RID: 3607 RVA: 0x0004F2EE File Offset: 0x0004D4EE
	public int OwnedCount { get; set; }

	// Token: 0x170000B5 RID: 181
	// (get) Token: 0x06000E18 RID: 3608 RVA: 0x0004F2F7 File Offset: 0x0004D4F7
	public int DisenchantCount
	{
		get
		{
			if (!this.IsCraftable)
			{
				return 0;
			}
			return Mathf.Max(this.OwnedCount - this.DefaultMaxCopiesPerDeck, 0);
		}
	}

	// Token: 0x170000B6 RID: 182
	// (get) Token: 0x06000E19 RID: 3609 RVA: 0x0004F316 File Offset: 0x0004D516
	public TAG_SPELL_SCHOOL SpellSchool
	{
		get
		{
			return this.m_EntityDef.GetSpellSchool();
		}
	}

	// Token: 0x170000B7 RID: 183
	// (get) Token: 0x06000E1A RID: 3610 RVA: 0x0004F323 File Offset: 0x0004D523
	public int DefaultMaxCopiesPerDeck
	{
		get
		{
			if (!this.m_EntityDef.IsElite())
			{
				return 2;
			}
			return 1;
		}
	}

	// Token: 0x170000B8 RID: 184
	// (get) Token: 0x06000E1B RID: 3611 RVA: 0x0004F335 File Offset: 0x0004D535
	public int CraftBuyCost
	{
		get
		{
			return CraftingManager.Get().GetCardValue(this.CardId, this.PremiumType).GetBuyValue();
		}
	}

	// Token: 0x170000B9 RID: 185
	// (get) Token: 0x06000E1C RID: 3612 RVA: 0x0004F354 File Offset: 0x0004D554
	public bool IsCraftable
	{
		get
		{
			bool flag;
			return CraftingManager.Get().GetCardValue(this.CardId, this.PremiumType) != null && !this.IsHeroSkin && FixedRewardsMgr.Get().CanCraftCard(this.CardId, this.PremiumType) && CraftingUI.IsCraftingEventForCardActive(this.CardId, this.PremiumType, out flag);
		}
	}

	// Token: 0x170000BA RID: 186
	// (get) Token: 0x06000E1D RID: 3613 RVA: 0x0004F3BA File Offset: 0x0004D5BA
	public bool IsNewCard
	{
		get
		{
			return this.OwnedCount > 0 && this.SeenCount < this.OwnedCount && this.SeenCount < this.DefaultMaxCopiesPerDeck;
		}
	}

	// Token: 0x170000BB RID: 187
	// (get) Token: 0x06000E1E RID: 3614 RVA: 0x0004F3E3 File Offset: 0x0004D5E3
	public int SuggestWeight
	{
		get
		{
			return this.m_CardRecord.SuggestionWeight;
		}
	}

	// Token: 0x170000BC RID: 188
	// (get) Token: 0x06000E1F RID: 3615 RVA: 0x0004F3F0 File Offset: 0x0004D5F0
	public int ChangeVersion
	{
		get
		{
			return this.m_CardRecord.ChangeVersion;
		}
	}

	// Token: 0x170000BD RID: 189
	// (get) Token: 0x06000E20 RID: 3616 RVA: 0x0004F3FD File Offset: 0x0004D5FD
	// (set) Token: 0x06000E21 RID: 3617 RVA: 0x0004F405 File Offset: 0x0004D605
	public DateTime LatestInsertDate
	{
		get
		{
			return this.m_LatestInsertDate;
		}
		set
		{
			if (value > this.m_LatestInsertDate)
			{
				this.m_LatestInsertDate = value;
			}
		}
	}

	// Token: 0x06000E22 RID: 3618 RVA: 0x0004F41C File Offset: 0x0004D61C
	public CollectibleCard(CardDbfRecord cardRecord, EntityDef refEntityDef, TAG_PREMIUM premiumType)
	{
		this.m_CardDbId = cardRecord.ID;
		this.m_EntityDef = refEntityDef;
		this.m_PremiumType = premiumType;
		this.m_CardRecord = cardRecord;
	}

	// Token: 0x06000E23 RID: 3619 RVA: 0x0004F45C File Offset: 0x0004D65C
	public HashSet<string> GetSearchableTokens()
	{
		if (this.m_SearchableTokens == null)
		{
			this.m_SearchableTokens = new HashSet<string>();
			if (GameUtils.IsLegacySet(this.Set))
			{
				CollectibleCardFilter.AddSearchableTokensToSet<TAG_CARD_SET>(TAG_CARD_SET.LEGACY, new Func<TAG_CARD_SET, bool>(GameStrings.HasCardSetName), new Func<TAG_CARD_SET, string>(GameStrings.GetCardSetName), this.m_SearchableTokens);
				CollectibleCardFilter.AddSearchableTokensToSet<TAG_CARD_SET>(TAG_CARD_SET.LEGACY, new Func<TAG_CARD_SET, bool>(GameStrings.HasCardSetNameShortened), new Func<TAG_CARD_SET, string>(GameStrings.GetCardSetNameShortened), this.m_SearchableTokens);
				CollectibleCardFilter.AddSearchableTokensToSet<TAG_CARD_SET>(TAG_CARD_SET.LEGACY, new Func<TAG_CARD_SET, bool>(GameStrings.HasCardSetNameInitials), new Func<TAG_CARD_SET, string>(GameStrings.GetCardSetNameInitials), this.m_SearchableTokens);
			}
			else
			{
				CollectibleCardFilter.AddSearchableTokensToSet<TAG_CARD_SET>(this.Set, new Func<TAG_CARD_SET, bool>(GameStrings.HasCardSetName), new Func<TAG_CARD_SET, string>(GameStrings.GetCardSetName), this.m_SearchableTokens);
				CollectibleCardFilter.AddSearchableTokensToSet<TAG_CARD_SET>(this.Set, new Func<TAG_CARD_SET, bool>(GameStrings.HasCardSetNameShortened), new Func<TAG_CARD_SET, string>(GameStrings.GetCardSetNameShortened), this.m_SearchableTokens);
				CollectibleCardFilter.AddSearchableTokensToSet<TAG_CARD_SET>(this.Set, new Func<TAG_CARD_SET, bool>(GameStrings.HasCardSetNameInitials), new Func<TAG_CARD_SET, string>(GameStrings.GetCardSetNameInitials), this.m_SearchableTokens);
			}
			CollectibleCardFilter.AddSearchableTokensToSet<TAG_RARITY>(this.Rarity, new Func<TAG_RARITY, bool>(GameStrings.HasRarityText), new Func<TAG_RARITY, string>(GameStrings.GetRarityText), this.m_SearchableTokens);
			CollectibleCardFilter.AddSearchableTokensToSet<TAG_RACE>(this.Race, new Func<TAG_RACE, bool>(GameStrings.HasRaceName), new Func<TAG_RACE, string>(GameStrings.GetRaceName), this.m_SearchableTokens);
			CollectibleCardFilter.AddSearchableTokensToSet<TAG_CARDTYPE>(this.CardType, new Func<TAG_CARDTYPE, bool>(GameStrings.HasCardTypeName), new Func<TAG_CARDTYPE, string>(GameStrings.GetCardTypeName), this.m_SearchableTokens);
			CollectibleCardFilter.AddSearchableTokensToSet<TAG_MULTI_CLASS_GROUP>(this.m_EntityDef.GetMultiClassGroup(), new Func<TAG_MULTI_CLASS_GROUP, bool>(GameStrings.HasMultiClassGroupName), new Func<TAG_MULTI_CLASS_GROUP, string>(GameStrings.GetMultiClassGroupName), this.m_SearchableTokens);
			CollectibleCardFilter.AddSearchableTokensToSet<TAG_SPELL_SCHOOL>(this.SpellSchool, new Func<TAG_SPELL_SCHOOL, bool>(GameStrings.HasSpellSchoolName), new Func<TAG_SPELL_SCHOOL, string>(GameStrings.GetSpellSchoolName), this.m_SearchableTokens);
			if (this.m_EntityDef.HasTag(GAME_TAG.MINI_SET))
			{
				CollectibleCardFilter.AddSearchableTokensToSet<TAG_CARD_SET>(this.Set, new Func<TAG_CARD_SET, bool>(GameStrings.HasMiniSetName), new Func<TAG_CARD_SET, string>(GameStrings.GetMiniSetName), this.m_SearchableTokens);
			}
			if (this.m_EntityDef.IsMultiClass())
			{
				foreach (TAG_CLASS tag in this.m_EntityDef.GetClasses(null))
				{
					if (GameStrings.HasClassName(tag))
					{
						CollectibleCardFilter.AddSingleSearchableTokenToSet(GameStrings.GetClassName(tag), this.m_SearchableTokens);
					}
				}
			}
			if (this.m_EntityDef.HasCharge() && GameStrings.HasKeywordName(GAME_TAG.CHARGE))
			{
				CollectibleCardFilter.AddSingleSearchableTokenToSet(GameStrings.GetKeywordName(GAME_TAG.CHARGE), this.m_SearchableTokens);
			}
			if (this.Race == TAG_RACE.ALL)
			{
				foreach (object obj in Enum.GetValues(typeof(TAG_RACE)))
				{
					CollectibleCardFilter.AddSearchableTokensToSet<TAG_RACE>((TAG_RACE)obj, new Func<TAG_RACE, bool>(GameStrings.HasRaceName), new Func<TAG_RACE, string>(GameStrings.GetRaceName), this.m_SearchableTokens);
				}
			}
		}
		return this.m_SearchableTokens;
	}

	// Token: 0x06000E24 RID: 3620 RVA: 0x0004F7A0 File Offset: 0x0004D9A0
	public bool FindTextInCard(string searchStr)
	{
		searchStr = searchStr.Trim();
		if (this.GetSearchableTokens().Contains(searchStr))
		{
			return true;
		}
		if (this.m_LongSearchableName == null)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.Name);
			stringBuilder.Append(" ");
			stringBuilder.Append(this.CardInHandText);
			foreach (CardAdditonalSearchTermsDbfRecord cardAdditonalSearchTermsDbfRecord in this.m_CardRecord.SearchTerms)
			{
				stringBuilder.Append(" ");
				stringBuilder.Append(cardAdditonalSearchTermsDbfRecord.SearchTerm.GetString(true));
			}
			this.m_LongSearchableName = UberText.RemoveMarkupAndCollapseWhitespaces(stringBuilder.ToString());
			this.m_LongSearchableName = this.m_LongSearchableName.Trim().ToLower();
			this.m_LongSearchableNameNonEuropean = CollectibleCardFilter.ConvertEuropeanCharacters(this.m_LongSearchableName);
			this.m_LongSearchableNameNoDiacritics = CollectibleCardFilter.RemoveDiacritics(this.m_LongSearchableName);
		}
		return this.m_LongSearchableName.Contains(searchStr, StringComparison.OrdinalIgnoreCase) || this.m_LongSearchableNameNonEuropean.Contains(searchStr, StringComparison.OrdinalIgnoreCase) || this.m_LongSearchableNameNoDiacritics.Contains(searchStr, StringComparison.OrdinalIgnoreCase);
	}

	// Token: 0x06000E25 RID: 3621 RVA: 0x0004F8D8 File Offset: 0x0004DAD8
	public static bool FindTextInternational(string searchStr, string stringToSearch)
	{
		string str = CollectibleCardFilter.ConvertEuropeanCharacters(stringToSearch);
		string str2 = CollectibleCardFilter.RemoveDiacritics(stringToSearch);
		return stringToSearch.Contains(searchStr, StringComparison.OrdinalIgnoreCase) || str.Contains(searchStr, StringComparison.OrdinalIgnoreCase) || str2.Contains(searchStr, StringComparison.OrdinalIgnoreCase);
	}

	// Token: 0x06000E26 RID: 3622 RVA: 0x0004F911 File Offset: 0x0004DB11
	public void AddCounts(int addOwnedCount, int addSeenCount, DateTime latestInsertDate)
	{
		this.OwnedCount += addOwnedCount;
		this.SeenCount += addSeenCount;
		this.LatestInsertDate = latestInsertDate;
	}

	// Token: 0x06000E27 RID: 3623 RVA: 0x0004F936 File Offset: 0x0004DB36
	public void RemoveCounts(int removeOwnedCount)
	{
		this.OwnedCount = Mathf.Max(this.OwnedCount - removeOwnedCount, 0);
	}

	// Token: 0x06000E28 RID: 3624 RVA: 0x0004F94C File Offset: 0x0004DB4C
	public void ClearCounts()
	{
		this.OwnedCount = 0;
		this.SeenCount = 0;
	}

	// Token: 0x06000E29 RID: 3625 RVA: 0x0004F95C File Offset: 0x0004DB5C
	public EntityDef GetEntityDef()
	{
		return this.m_EntityDef;
	}

	// Token: 0x06000E2A RID: 3626 RVA: 0x0004F964 File Offset: 0x0004DB64
	public override bool Equals(object obj)
	{
		return obj != null && (this == obj || (this.CardDbId == ((CollectibleCard)obj).CardDbId && this.PremiumType == ((CollectibleCard)obj).PremiumType));
	}

	// Token: 0x06000E2B RID: 3627 RVA: 0x0004F999 File Offset: 0x0004DB99
	public override int GetHashCode()
	{
		return (int)(this.CardId.GetHashCode() + this.PremiumType);
	}

	// Token: 0x040009BD RID: 2493
	private int m_CardDbId = -1;

	// Token: 0x040009BE RID: 2494
	private DateTime m_LatestInsertDate = new DateTime(0L);

	// Token: 0x040009BF RID: 2495
	private HashSet<string> m_SearchableTokens;

	// Token: 0x040009C0 RID: 2496
	private string m_LongSearchableName;

	// Token: 0x040009C1 RID: 2497
	private string m_LongSearchableNameNonEuropean;

	// Token: 0x040009C2 RID: 2498
	private string m_LongSearchableNameNoDiacritics;

	// Token: 0x040009C3 RID: 2499
	private EntityDef m_EntityDef;

	// Token: 0x040009C4 RID: 2500
	private TAG_PREMIUM m_PremiumType;

	// Token: 0x040009C5 RID: 2501
	private CardDbfRecord m_CardRecord;
}
