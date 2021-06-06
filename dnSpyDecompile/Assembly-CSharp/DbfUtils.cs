using System;
using System.Collections.Generic;
using Assets;
using PegasusShared;

// Token: 0x020007B8 RID: 1976
public static class DbfUtils
{
	// Token: 0x06006D79 RID: 28025 RVA: 0x00234844 File Offset: 0x00232A44
	public static ScenarioDbfRecord ConvertFromProtobuf(ScenarioDbRecord protoScenario, out List<ScenarioGuestHeroesDbfRecord> outScenarioGuestHeroRecords, out List<ClassExclusionsDbfRecord> outClassExclusionsRecords)
	{
		outScenarioGuestHeroRecords = new List<ScenarioGuestHeroesDbfRecord>();
		outClassExclusionsRecords = new List<ClassExclusionsDbfRecord>();
		if (protoScenario == null)
		{
			return null;
		}
		ScenarioDbfRecord scenarioDbfRecord = new ScenarioDbfRecord();
		scenarioDbfRecord.SetID(protoScenario.Id);
		scenarioDbfRecord.SetNoteDesc(protoScenario.NoteDesc);
		scenarioDbfRecord.SetPlayers(protoScenario.NumHumanPlayers);
		scenarioDbfRecord.SetPlayer1HeroCardId((int)protoScenario.Player1HeroCardId);
		scenarioDbfRecord.SetPlayer2HeroCardId((int)protoScenario.Player2HeroCardId);
		scenarioDbfRecord.SetIsExpert(protoScenario.IsExpert);
		scenarioDbfRecord.SetIsCoop(protoScenario.HasIsCoop && protoScenario.IsCoop);
		scenarioDbfRecord.SetAdventureId(protoScenario.AdventureId);
		if (protoScenario.HasAdventureModeId)
		{
			scenarioDbfRecord.SetModeId(protoScenario.AdventureModeId);
		}
		scenarioDbfRecord.SetWingId(protoScenario.WingId);
		scenarioDbfRecord.SetSortOrder(protoScenario.SortOrder);
		if (protoScenario.HasClientPlayer2HeroCardId)
		{
			scenarioDbfRecord.SetClientPlayer2HeroCardId((int)protoScenario.ClientPlayer2HeroCardId);
		}
		scenarioDbfRecord.SetTbTexture(protoScenario.TavernBrawlTexture);
		scenarioDbfRecord.SetTbTexturePhone(protoScenario.TavernBrawlTexturePhone);
		if (protoScenario.HasTavernBrawlTexturePhoneOffset)
		{
			scenarioDbfRecord.SetTbTexturePhoneOffsetY((double)protoScenario.TavernBrawlTexturePhoneOffset.Y);
		}
		foreach (ScenarioGuestHeroDbRecord scenarioGuestHeroDbRecord in protoScenario.GuestHeroes)
		{
			ScenarioGuestHeroesDbfRecord scenarioGuestHeroesDbfRecord = new ScenarioGuestHeroesDbfRecord();
			scenarioGuestHeroesDbfRecord.SetScenarioId(scenarioGuestHeroDbRecord.ScenarioId);
			scenarioGuestHeroesDbfRecord.SetGuestHeroId(scenarioGuestHeroDbRecord.GuestHeroId);
			scenarioGuestHeroesDbfRecord.SetSortOrder(scenarioGuestHeroDbRecord.SortOrder);
			outScenarioGuestHeroRecords.Add(scenarioGuestHeroesDbfRecord);
		}
		foreach (ClassExclusionDbRecord classExclusionDbRecord in protoScenario.ClassExclusions)
		{
			ClassExclusionsDbfRecord classExclusionsDbfRecord = new ClassExclusionsDbfRecord();
			classExclusionsDbfRecord.SetScenarioId(classExclusionDbRecord.ScenarioId);
			classExclusionsDbfRecord.SetClassId(classExclusionDbRecord.ClassId);
			outClassExclusionsRecords.Add(classExclusionsDbfRecord);
		}
		scenarioDbfRecord.SetScriptObject(protoScenario.ScriptObject);
		DbfUtils.AddLocStrings(scenarioDbfRecord, protoScenario.Strings);
		if (protoScenario.HasDeckRulesetId)
		{
			scenarioDbfRecord.SetDeckRulesetId(protoScenario.DeckRulesetId);
		}
		if (protoScenario.HasRuleType)
		{
			int ruleType = (int)protoScenario.RuleType;
			scenarioDbfRecord.SetRuleType((Scenario.RuleType)ruleType);
		}
		return scenarioDbfRecord;
	}

	// Token: 0x06006D7A RID: 28026 RVA: 0x00234A6C File Offset: 0x00232C6C
	public static DeckTemplateDbfRecord ConvertFromProtobuf(DeckTemplateDbRecord proto, out DeckDbfRecord deckDbf, out List<DeckCardDbfRecord> deckCardDbfs)
	{
		if (proto == null)
		{
			deckDbf = null;
			deckCardDbfs = null;
			return null;
		}
		DeckTemplateDbfRecord deckTemplateDbfRecord = new DeckTemplateDbfRecord();
		deckTemplateDbfRecord.SetID(proto.Id);
		deckTemplateDbfRecord.SetDeckId(proto.DeckId);
		deckTemplateDbfRecord.SetClassId(proto.ClassId);
		deckTemplateDbfRecord.SetSortOrder(proto.SortOrder);
		deckTemplateDbfRecord.SetIsFreeReward(proto.IsFreeReward);
		deckTemplateDbfRecord.SetIsStarterDeck(proto.IsStarterDeck);
		if (proto.HasEvent)
		{
			deckTemplateDbfRecord.SetEvent(proto.Event);
		}
		if (proto.HasDisplayTexture)
		{
			deckTemplateDbfRecord.SetDisplayTexture(proto.DisplayTexture);
		}
		if (proto.HasFormatType)
		{
			deckTemplateDbfRecord.SetFormatType((DeckTemplate.FormatType)proto.FormatType);
		}
		if (proto.HasDisplayCardId)
		{
			deckTemplateDbfRecord.SetDisplayCardId(proto.DisplayCardId);
		}
		DeckDbRecord deckRecord = proto.DeckRecord;
		deckDbf = new DeckDbfRecord();
		deckDbf.SetID(deckRecord.Id);
		deckDbf.SetNoteName(deckRecord.NoteName);
		deckDbf.SetTopCardId(deckRecord.TopCardId);
		DbfUtils.AddLocStrings(deckDbf, deckRecord.Strings);
		deckCardDbfs = new List<DeckCardDbfRecord>();
		for (int i = 0; i < deckRecord.DeckCard.Count; i++)
		{
			DeckCardDbRecord deckCardDbRecord = deckRecord.DeckCard[i];
			DeckCardDbfRecord deckCardDbfRecord = new DeckCardDbfRecord();
			deckCardDbfRecord.SetID(deckCardDbRecord.Id);
			deckCardDbfRecord.SetCardId(deckCardDbRecord.CardId);
			deckCardDbfRecord.SetDeckId(deckRecord.Id);
			int num = i + 1;
			if (num < deckRecord.DeckCard.Count)
			{
				deckCardDbfRecord.SetNextCard(deckRecord.DeckCard[num].Id);
			}
			deckCardDbfs.Add(deckCardDbfRecord);
		}
		return deckTemplateDbfRecord;
	}

	// Token: 0x06006D7B RID: 28027 RVA: 0x00234BF6 File Offset: 0x00232DF6
	public static DeckRulesetDbfRecord ConvertFromProtobuf(DeckRulesetDbRecord proto)
	{
		if (proto == null)
		{
			return null;
		}
		DeckRulesetDbfRecord deckRulesetDbfRecord = new DeckRulesetDbfRecord();
		deckRulesetDbfRecord.SetID(proto.Id);
		return deckRulesetDbfRecord;
	}

	// Token: 0x06006D7C RID: 28028 RVA: 0x00234C10 File Offset: 0x00232E10
	public static DeckRulesetRuleDbfRecord ConvertFromProtobuf(DeckRulesetRuleDbRecord proto, out List<int> outTargetSubsetIds)
	{
		outTargetSubsetIds = null;
		if (proto == null)
		{
			return null;
		}
		DeckRulesetRuleDbfRecord deckRulesetRuleDbfRecord = new DeckRulesetRuleDbfRecord();
		deckRulesetRuleDbfRecord.SetID(proto.Id);
		deckRulesetRuleDbfRecord.SetDeckRulesetId(proto.DeckRulesetId);
		if (proto.HasAppliesToSubsetId)
		{
			deckRulesetRuleDbfRecord.SetAppliesToSubsetId(proto.AppliesToSubsetId);
		}
		if (proto.HasAppliesToIsNot)
		{
			deckRulesetRuleDbfRecord.SetAppliesToIsNot(proto.AppliesToIsNot);
		}
		DeckRulesetRule.RuleType ruleType = (DeckRulesetRule.RuleType)Enum.Parse(typeof(DeckRulesetRule.RuleType), proto.RuleType, true);
		deckRulesetRuleDbfRecord.SetRuleType(ruleType);
		deckRulesetRuleDbfRecord.SetRuleIsNot(proto.RuleIsNot);
		if (proto.HasMinValue)
		{
			deckRulesetRuleDbfRecord.SetMinValue(proto.MinValue);
		}
		if (proto.HasMaxValue)
		{
			deckRulesetRuleDbfRecord.SetMaxValue(proto.MaxValue);
		}
		if (proto.HasTag)
		{
			deckRulesetRuleDbfRecord.SetTag(proto.Tag);
		}
		if (proto.HasTagMinValue)
		{
			deckRulesetRuleDbfRecord.SetTagMinValue(proto.TagMinValue);
		}
		if (proto.HasTagMaxValue)
		{
			deckRulesetRuleDbfRecord.SetTagMaxValue(proto.TagMaxValue);
		}
		if (proto.HasStringValue)
		{
			deckRulesetRuleDbfRecord.SetStringValue(proto.StringValue);
		}
		deckRulesetRuleDbfRecord.SetShowInvalidCards(proto.ShowInvalidCards);
		outTargetSubsetIds = proto.TargetSubsetIds;
		DbfUtils.AddLocStrings(deckRulesetRuleDbfRecord, proto.Strings);
		return deckRulesetRuleDbfRecord;
	}

	// Token: 0x06006D7D RID: 28029 RVA: 0x00234D35 File Offset: 0x00232F35
	public static RewardChestDbfRecord ConvertFromProtobuf(RewardChestDbRecord proto)
	{
		if (proto == null)
		{
			return null;
		}
		RewardChestDbfRecord rewardChestDbfRecord = new RewardChestDbfRecord();
		rewardChestDbfRecord.SetID(proto.Id);
		rewardChestDbfRecord.SetShowToReturningPlayer(proto.HasShowToReturningPlayer && proto.ShowToReturningPlayer);
		DbfUtils.AddLocStrings(rewardChestDbfRecord, proto.Strings);
		return rewardChestDbfRecord;
	}

	// Token: 0x06006D7E RID: 28030 RVA: 0x00234D70 File Offset: 0x00232F70
	public static GuestHeroDbfRecord ConvertFromProtobuf(GuestHeroDbRecord proto)
	{
		if (proto == null)
		{
			return null;
		}
		GuestHeroDbfRecord guestHeroDbfRecord = new GuestHeroDbfRecord();
		guestHeroDbfRecord.SetID(proto.Id);
		guestHeroDbfRecord.SetCardId(proto.CardId);
		guestHeroDbfRecord.SetUnlockEvent(proto.UnlockEvent);
		DbfUtils.AddLocStrings(guestHeroDbfRecord, proto.Strings);
		return guestHeroDbfRecord;
	}

	// Token: 0x06006D7F RID: 28031 RVA: 0x00234DAC File Offset: 0x00232FAC
	public static DbfLocValue ConvertFromProtobuf(LocalizedString protoLocString)
	{
		DbfLocValue dbfLocValue = new DbfLocValue();
		dbfLocValue.SetCapacity(protoLocString.Values.Count);
		foreach (LocalizedStringValue localizedStringValue in protoLocString.Values)
		{
			dbfLocValue.SetString((Locale)localizedStringValue.Locale, TextUtils.DecodeWhitespaces(localizedStringValue.Value));
		}
		return dbfLocValue;
	}

	// Token: 0x06006D80 RID: 28032 RVA: 0x00234E28 File Offset: 0x00233028
	private static void AddLocStrings(DbfRecord record, List<LocalizedString> protoStrings)
	{
		foreach (LocalizedString localizedString in protoStrings)
		{
			record.SetVar(localizedString.Key, DbfUtils.ConvertFromProtobuf(localizedString));
		}
	}
}
