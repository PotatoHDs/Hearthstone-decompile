using System;
using System.Collections.Generic;
using Assets;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000265 RID: 613
[Serializable]
public class ScenarioDbfRecord : DbfRecord
{
	// Token: 0x17000435 RID: 1077
	// (get) Token: 0x06001FCD RID: 8141 RVA: 0x0009E9B2 File Offset: 0x0009CBB2
	[DbfField("NOTE_DESC")]
	public string NoteDesc
	{
		get
		{
			return this.m_noteDesc;
		}
	}

	// Token: 0x17000436 RID: 1078
	// (get) Token: 0x06001FCE RID: 8142 RVA: 0x0009E9BA File Offset: 0x0009CBBA
	[DbfField("PLAYERS")]
	public int Players
	{
		get
		{
			return this.m_players;
		}
	}

	// Token: 0x17000437 RID: 1079
	// (get) Token: 0x06001FCF RID: 8143 RVA: 0x0009E9C2 File Offset: 0x0009CBC2
	[DbfField("PLAYER1_HERO_CARD_ID")]
	public int Player1HeroCardId
	{
		get
		{
			return this.m_player1HeroCardId;
		}
	}

	// Token: 0x17000438 RID: 1080
	// (get) Token: 0x06001FD0 RID: 8144 RVA: 0x0009E9CA File Offset: 0x0009CBCA
	public CardDbfRecord Player1HeroCardRecord
	{
		get
		{
			return GameDbf.Card.GetRecord(this.m_player1HeroCardId);
		}
	}

	// Token: 0x17000439 RID: 1081
	// (get) Token: 0x06001FD1 RID: 8145 RVA: 0x0009E9DC File Offset: 0x0009CBDC
	[DbfField("PLAYER2_HERO_CARD_ID")]
	public int Player2HeroCardId
	{
		get
		{
			return this.m_player2HeroCardId;
		}
	}

	// Token: 0x1700043A RID: 1082
	// (get) Token: 0x06001FD2 RID: 8146 RVA: 0x0009E9E4 File Offset: 0x0009CBE4
	public CardDbfRecord Player2HeroCardRecord
	{
		get
		{
			return GameDbf.Card.GetRecord(this.m_player2HeroCardId);
		}
	}

	// Token: 0x1700043B RID: 1083
	// (get) Token: 0x06001FD3 RID: 8147 RVA: 0x0009E9F6 File Offset: 0x0009CBF6
	[DbfField("IS_TUTORIAL")]
	public bool IsTutorial
	{
		get
		{
			return this.m_isTutorial;
		}
	}

	// Token: 0x1700043C RID: 1084
	// (get) Token: 0x06001FD4 RID: 8148 RVA: 0x0009E9FE File Offset: 0x0009CBFE
	[DbfField("IS_EXPERT")]
	public bool IsExpert
	{
		get
		{
			return this.m_isExpert;
		}
	}

	// Token: 0x1700043D RID: 1085
	// (get) Token: 0x06001FD5 RID: 8149 RVA: 0x0009EA06 File Offset: 0x0009CC06
	[DbfField("IS_COOP")]
	public bool IsCoop
	{
		get
		{
			return this.m_isCoop;
		}
	}

	// Token: 0x1700043E RID: 1086
	// (get) Token: 0x06001FD6 RID: 8150 RVA: 0x0009EA0E File Offset: 0x0009CC0E
	[DbfField("ONE_SIM_PER_PLAYER")]
	public bool OneSimPerPlayer
	{
		get
		{
			return this.m_oneSimPerPlayer;
		}
	}

	// Token: 0x1700043F RID: 1087
	// (get) Token: 0x06001FD7 RID: 8151 RVA: 0x0009EA16 File Offset: 0x0009CC16
	[DbfField("ADVENTURE_ID")]
	public int AdventureId
	{
		get
		{
			return this.m_adventureId;
		}
	}

	// Token: 0x17000440 RID: 1088
	// (get) Token: 0x06001FD8 RID: 8152 RVA: 0x0009EA1E File Offset: 0x0009CC1E
	public AdventureDbfRecord AdventureRecord
	{
		get
		{
			return GameDbf.Adventure.GetRecord(this.m_adventureId);
		}
	}

	// Token: 0x17000441 RID: 1089
	// (get) Token: 0x06001FD9 RID: 8153 RVA: 0x0009EA30 File Offset: 0x0009CC30
	[DbfField("WING_ID")]
	public int WingId
	{
		get
		{
			return this.m_wingId;
		}
	}

	// Token: 0x17000442 RID: 1090
	// (get) Token: 0x06001FDA RID: 8154 RVA: 0x0009EA38 File Offset: 0x0009CC38
	public WingDbfRecord WingRecord
	{
		get
		{
			return GameDbf.Wing.GetRecord(this.m_wingId);
		}
	}

	// Token: 0x17000443 RID: 1091
	// (get) Token: 0x06001FDB RID: 8155 RVA: 0x0009EA4A File Offset: 0x0009CC4A
	[DbfField("SORT_ORDER")]
	public int SortOrder
	{
		get
		{
			return this.m_sortOrder;
		}
	}

	// Token: 0x17000444 RID: 1092
	// (get) Token: 0x06001FDC RID: 8156 RVA: 0x0009EA52 File Offset: 0x0009CC52
	[DbfField("MODE_ID")]
	public int ModeId
	{
		get
		{
			return this.m_modeId;
		}
	}

	// Token: 0x17000445 RID: 1093
	// (get) Token: 0x06001FDD RID: 8157 RVA: 0x0009EA5A File Offset: 0x0009CC5A
	public AdventureModeDbfRecord ModeRecord
	{
		get
		{
			return GameDbf.AdventureMode.GetRecord(this.m_modeId);
		}
	}

	// Token: 0x17000446 RID: 1094
	// (get) Token: 0x06001FDE RID: 8158 RVA: 0x0009EA6C File Offset: 0x0009CC6C
	[DbfField("CLIENT_PLAYER2_HERO_CARD_ID")]
	public int ClientPlayer2HeroCardId
	{
		get
		{
			return this.m_clientPlayer2HeroCardId;
		}
	}

	// Token: 0x17000447 RID: 1095
	// (get) Token: 0x06001FDF RID: 8159 RVA: 0x0009EA74 File Offset: 0x0009CC74
	public CardDbfRecord ClientPlayer2HeroCardRecord
	{
		get
		{
			return GameDbf.Card.GetRecord(this.m_clientPlayer2HeroCardId);
		}
	}

	// Token: 0x17000448 RID: 1096
	// (get) Token: 0x06001FE0 RID: 8160 RVA: 0x0009EA86 File Offset: 0x0009CC86
	[DbfField("CLIENT_PLAYER2_HERO_POWER_CARD_ID")]
	public int ClientPlayer2HeroPowerCardId
	{
		get
		{
			return this.m_clientPlayer2HeroPowerCardId;
		}
	}

	// Token: 0x17000449 RID: 1097
	// (get) Token: 0x06001FE1 RID: 8161 RVA: 0x0009EA8E File Offset: 0x0009CC8E
	public CardDbfRecord ClientPlayer2HeroPowerCardRecord
	{
		get
		{
			return GameDbf.Card.GetRecord(this.m_clientPlayer2HeroPowerCardId);
		}
	}

	// Token: 0x1700044A RID: 1098
	// (get) Token: 0x06001FE2 RID: 8162 RVA: 0x0009EAA0 File Offset: 0x0009CCA0
	[DbfField("NAME")]
	public DbfLocValue Name
	{
		get
		{
			return this.m_name;
		}
	}

	// Token: 0x1700044B RID: 1099
	// (get) Token: 0x06001FE3 RID: 8163 RVA: 0x0009EAA8 File Offset: 0x0009CCA8
	[DbfField("SHORT_NAME")]
	public DbfLocValue ShortName
	{
		get
		{
			return this.m_shortName;
		}
	}

	// Token: 0x1700044C RID: 1100
	// (get) Token: 0x06001FE4 RID: 8164 RVA: 0x0009EAB0 File Offset: 0x0009CCB0
	[DbfField("DESCRIPTION")]
	public DbfLocValue Description
	{
		get
		{
			return this.m_description;
		}
	}

	// Token: 0x1700044D RID: 1101
	// (get) Token: 0x06001FE5 RID: 8165 RVA: 0x0009EAB8 File Offset: 0x0009CCB8
	[DbfField("SHORT_DESCRIPTION")]
	public DbfLocValue ShortDescription
	{
		get
		{
			return this.m_shortDescription;
		}
	}

	// Token: 0x1700044E RID: 1102
	// (get) Token: 0x06001FE6 RID: 8166 RVA: 0x0009EAC0 File Offset: 0x0009CCC0
	[DbfField("OPPONENT_NAME")]
	public DbfLocValue OpponentName
	{
		get
		{
			return this.m_opponentName;
		}
	}

	// Token: 0x1700044F RID: 1103
	// (get) Token: 0x06001FE7 RID: 8167 RVA: 0x0009EAC8 File Offset: 0x0009CCC8
	[DbfField("COMPLETED_DESCRIPTION")]
	public DbfLocValue CompletedDescription
	{
		get
		{
			return this.m_completedDescription;
		}
	}

	// Token: 0x17000450 RID: 1104
	// (get) Token: 0x06001FE8 RID: 8168 RVA: 0x0009EAD0 File Offset: 0x0009CCD0
	[DbfField("PLAYER1_DECK_ID")]
	public int Player1DeckId
	{
		get
		{
			return this.m_player1DeckId;
		}
	}

	// Token: 0x17000451 RID: 1105
	// (get) Token: 0x06001FE9 RID: 8169 RVA: 0x0009EAD8 File Offset: 0x0009CCD8
	public DeckDbfRecord Player1DeckRecord
	{
		get
		{
			return GameDbf.Deck.GetRecord(this.m_player1DeckId);
		}
	}

	// Token: 0x17000452 RID: 1106
	// (get) Token: 0x06001FEA RID: 8170 RVA: 0x0009EAEA File Offset: 0x0009CCEA
	[DbfField("DECK_RULESET_ID")]
	public int DeckRulesetId
	{
		get
		{
			return this.m_deckRulesetId;
		}
	}

	// Token: 0x17000453 RID: 1107
	// (get) Token: 0x06001FEB RID: 8171 RVA: 0x0009EAF2 File Offset: 0x0009CCF2
	public DeckRulesetDbfRecord DeckRulesetRecord
	{
		get
		{
			return GameDbf.DeckRuleset.GetRecord(this.m_deckRulesetId);
		}
	}

	// Token: 0x17000454 RID: 1108
	// (get) Token: 0x06001FEC RID: 8172 RVA: 0x0009EB04 File Offset: 0x0009CD04
	[DbfField("RULE_TYPE")]
	public Scenario.RuleType RuleType
	{
		get
		{
			return this.m_ruleType;
		}
	}

	// Token: 0x17000455 RID: 1109
	// (get) Token: 0x06001FED RID: 8173 RVA: 0x0009EB0C File Offset: 0x0009CD0C
	[DbfField("CHOOSE_HERO_TEXT")]
	public DbfLocValue ChooseHeroText
	{
		get
		{
			return this.m_chooseHeroText;
		}
	}

	// Token: 0x17000456 RID: 1110
	// (get) Token: 0x06001FEE RID: 8174 RVA: 0x0009EB14 File Offset: 0x0009CD14
	[DbfField("TB_TEXTURE")]
	public string TbTexture
	{
		get
		{
			return this.m_tbTexture;
		}
	}

	// Token: 0x17000457 RID: 1111
	// (get) Token: 0x06001FEF RID: 8175 RVA: 0x0009EB1C File Offset: 0x0009CD1C
	[DbfField("TB_TEXTURE_PHONE")]
	public string TbTexturePhone
	{
		get
		{
			return this.m_tbTexturePhone;
		}
	}

	// Token: 0x17000458 RID: 1112
	// (get) Token: 0x06001FF0 RID: 8176 RVA: 0x0009EB24 File Offset: 0x0009CD24
	[DbfField("TB_TEXTURE_PHONE_OFFSET_Y")]
	public double TbTexturePhoneOffsetY
	{
		get
		{
			return this.m_tbTexturePhoneOffsetY;
		}
	}

	// Token: 0x17000459 RID: 1113
	// (get) Token: 0x06001FF1 RID: 8177 RVA: 0x0009EB2C File Offset: 0x0009CD2C
	[DbfField("GAME_SAVE_DATA_PROGRESS_SUBKEY")]
	public int GameSaveDataProgressSubkey
	{
		get
		{
			return this.m_gameSaveDataProgressSubkeyId;
		}
	}

	// Token: 0x1700045A RID: 1114
	// (get) Token: 0x06001FF2 RID: 8178 RVA: 0x0009EB34 File Offset: 0x0009CD34
	public GameSaveSubkeyDbfRecord GameSaveDataProgressSubkeyRecord
	{
		get
		{
			return GameDbf.GameSaveSubkey.GetRecord(this.m_gameSaveDataProgressSubkeyId);
		}
	}

	// Token: 0x1700045B RID: 1115
	// (get) Token: 0x06001FF3 RID: 8179 RVA: 0x0009EB46 File Offset: 0x0009CD46
	[DbfField("GAME_SAVE_DATA_PROGRESS_MAX")]
	public int GameSaveDataProgressMax
	{
		get
		{
			return this.m_gameSaveDataProgressMax;
		}
	}

	// Token: 0x1700045C RID: 1116
	// (get) Token: 0x06001FF4 RID: 8180 RVA: 0x0009EB4E File Offset: 0x0009CD4E
	[DbfField("HIDE_BOSS_HERO_POWER_IN_UI")]
	public bool HideBossHeroPowerInUi
	{
		get
		{
			return this.m_hideBossHeroPowerInUi;
		}
	}

	// Token: 0x1700045D RID: 1117
	// (get) Token: 0x06001FF5 RID: 8181 RVA: 0x0009EB56 File Offset: 0x0009CD56
	[DbfField("SCRIPT_OBJECT")]
	public string ScriptObject
	{
		get
		{
			return this.m_scriptObject;
		}
	}

	// Token: 0x1700045E RID: 1118
	// (get) Token: 0x06001FF6 RID: 8182 RVA: 0x0009EB5E File Offset: 0x0009CD5E
	public List<ClassExclusionsDbfRecord> ClassExclusions
	{
		get
		{
			return GameDbf.ClassExclusions.GetRecords((ClassExclusionsDbfRecord r) => r.ScenarioId == base.ID, -1);
		}
	}

	// Token: 0x1700045F RID: 1119
	// (get) Token: 0x06001FF7 RID: 8183 RVA: 0x0009EB77 File Offset: 0x0009CD77
	public List<ScenarioGuestHeroesDbfRecord> GuestHeroes
	{
		get
		{
			return GameDbf.ScenarioGuestHeroes.GetRecords((ScenarioGuestHeroesDbfRecord r) => r.ScenarioId == base.ID, -1);
		}
	}

	// Token: 0x06001FF8 RID: 8184 RVA: 0x0009EB90 File Offset: 0x0009CD90
	public void SetNoteDesc(string v)
	{
		this.m_noteDesc = v;
	}

	// Token: 0x06001FF9 RID: 8185 RVA: 0x0009EB99 File Offset: 0x0009CD99
	public void SetPlayers(int v)
	{
		this.m_players = v;
	}

	// Token: 0x06001FFA RID: 8186 RVA: 0x0009EBA2 File Offset: 0x0009CDA2
	public void SetPlayer1HeroCardId(int v)
	{
		this.m_player1HeroCardId = v;
	}

	// Token: 0x06001FFB RID: 8187 RVA: 0x0009EBAB File Offset: 0x0009CDAB
	public void SetPlayer2HeroCardId(int v)
	{
		this.m_player2HeroCardId = v;
	}

	// Token: 0x06001FFC RID: 8188 RVA: 0x0009EBB4 File Offset: 0x0009CDB4
	public void SetIsTutorial(bool v)
	{
		this.m_isTutorial = v;
	}

	// Token: 0x06001FFD RID: 8189 RVA: 0x0009EBBD File Offset: 0x0009CDBD
	public void SetIsExpert(bool v)
	{
		this.m_isExpert = v;
	}

	// Token: 0x06001FFE RID: 8190 RVA: 0x0009EBC6 File Offset: 0x0009CDC6
	public void SetIsCoop(bool v)
	{
		this.m_isCoop = v;
	}

	// Token: 0x06001FFF RID: 8191 RVA: 0x0009EBCF File Offset: 0x0009CDCF
	public void SetOneSimPerPlayer(bool v)
	{
		this.m_oneSimPerPlayer = v;
	}

	// Token: 0x06002000 RID: 8192 RVA: 0x0009EBD8 File Offset: 0x0009CDD8
	public void SetAdventureId(int v)
	{
		this.m_adventureId = v;
	}

	// Token: 0x06002001 RID: 8193 RVA: 0x0009EBE1 File Offset: 0x0009CDE1
	public void SetWingId(int v)
	{
		this.m_wingId = v;
	}

	// Token: 0x06002002 RID: 8194 RVA: 0x0009EBEA File Offset: 0x0009CDEA
	public void SetSortOrder(int v)
	{
		this.m_sortOrder = v;
	}

	// Token: 0x06002003 RID: 8195 RVA: 0x0009EBF3 File Offset: 0x0009CDF3
	public void SetModeId(int v)
	{
		this.m_modeId = v;
	}

	// Token: 0x06002004 RID: 8196 RVA: 0x0009EBFC File Offset: 0x0009CDFC
	public void SetClientPlayer2HeroCardId(int v)
	{
		this.m_clientPlayer2HeroCardId = v;
	}

	// Token: 0x06002005 RID: 8197 RVA: 0x0009EC05 File Offset: 0x0009CE05
	public void SetClientPlayer2HeroPowerCardId(int v)
	{
		this.m_clientPlayer2HeroPowerCardId = v;
	}

	// Token: 0x06002006 RID: 8198 RVA: 0x0009EC0E File Offset: 0x0009CE0E
	public void SetName(DbfLocValue v)
	{
		this.m_name = v;
		v.SetDebugInfo(base.ID, "NAME");
	}

	// Token: 0x06002007 RID: 8199 RVA: 0x0009EC28 File Offset: 0x0009CE28
	public void SetShortName(DbfLocValue v)
	{
		this.m_shortName = v;
		v.SetDebugInfo(base.ID, "SHORT_NAME");
	}

	// Token: 0x06002008 RID: 8200 RVA: 0x0009EC42 File Offset: 0x0009CE42
	public void SetDescription(DbfLocValue v)
	{
		this.m_description = v;
		v.SetDebugInfo(base.ID, "DESCRIPTION");
	}

	// Token: 0x06002009 RID: 8201 RVA: 0x0009EC5C File Offset: 0x0009CE5C
	public void SetShortDescription(DbfLocValue v)
	{
		this.m_shortDescription = v;
		v.SetDebugInfo(base.ID, "SHORT_DESCRIPTION");
	}

	// Token: 0x0600200A RID: 8202 RVA: 0x0009EC76 File Offset: 0x0009CE76
	public void SetOpponentName(DbfLocValue v)
	{
		this.m_opponentName = v;
		v.SetDebugInfo(base.ID, "OPPONENT_NAME");
	}

	// Token: 0x0600200B RID: 8203 RVA: 0x0009EC90 File Offset: 0x0009CE90
	public void SetCompletedDescription(DbfLocValue v)
	{
		this.m_completedDescription = v;
		v.SetDebugInfo(base.ID, "COMPLETED_DESCRIPTION");
	}

	// Token: 0x0600200C RID: 8204 RVA: 0x0009ECAA File Offset: 0x0009CEAA
	public void SetPlayer1DeckId(int v)
	{
		this.m_player1DeckId = v;
	}

	// Token: 0x0600200D RID: 8205 RVA: 0x0009ECB3 File Offset: 0x0009CEB3
	public void SetDeckRulesetId(int v)
	{
		this.m_deckRulesetId = v;
	}

	// Token: 0x0600200E RID: 8206 RVA: 0x0009ECBC File Offset: 0x0009CEBC
	public void SetRuleType(Scenario.RuleType v)
	{
		this.m_ruleType = v;
	}

	// Token: 0x0600200F RID: 8207 RVA: 0x0009ECC5 File Offset: 0x0009CEC5
	public void SetChooseHeroText(DbfLocValue v)
	{
		this.m_chooseHeroText = v;
		v.SetDebugInfo(base.ID, "CHOOSE_HERO_TEXT");
	}

	// Token: 0x06002010 RID: 8208 RVA: 0x0009ECDF File Offset: 0x0009CEDF
	public void SetTbTexture(string v)
	{
		this.m_tbTexture = v;
	}

	// Token: 0x06002011 RID: 8209 RVA: 0x0009ECE8 File Offset: 0x0009CEE8
	public void SetTbTexturePhone(string v)
	{
		this.m_tbTexturePhone = v;
	}

	// Token: 0x06002012 RID: 8210 RVA: 0x0009ECF1 File Offset: 0x0009CEF1
	public void SetTbTexturePhoneOffsetY(double v)
	{
		this.m_tbTexturePhoneOffsetY = v;
	}

	// Token: 0x06002013 RID: 8211 RVA: 0x0009ECFA File Offset: 0x0009CEFA
	public void SetGameSaveDataProgressSubkey(int v)
	{
		this.m_gameSaveDataProgressSubkeyId = v;
	}

	// Token: 0x06002014 RID: 8212 RVA: 0x0009ED03 File Offset: 0x0009CF03
	public void SetGameSaveDataProgressMax(int v)
	{
		this.m_gameSaveDataProgressMax = v;
	}

	// Token: 0x06002015 RID: 8213 RVA: 0x0009ED0C File Offset: 0x0009CF0C
	public void SetHideBossHeroPowerInUi(bool v)
	{
		this.m_hideBossHeroPowerInUi = v;
	}

	// Token: 0x06002016 RID: 8214 RVA: 0x0009ED15 File Offset: 0x0009CF15
	public void SetScriptObject(string v)
	{
		this.m_scriptObject = v;
	}

	// Token: 0x06002017 RID: 8215 RVA: 0x0009ED20 File Offset: 0x0009CF20
	public override object GetVar(string name)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 2167425274U)
		{
			if (num <= 1103584457U)
			{
				if (num <= 286783624U)
				{
					if (num <= 190718801U)
					{
						if (num != 179436941U)
						{
							if (num == 190718801U)
							{
								if (name == "ADVENTURE_ID")
								{
									return this.m_adventureId;
								}
							}
						}
						else if (name == "GAME_SAVE_DATA_PROGRESS_MAX")
						{
							return this.m_gameSaveDataProgressMax;
						}
					}
					else if (num != 191153791U)
					{
						if (num == 286783624U)
						{
							if (name == "IS_TUTORIAL")
							{
								return this.m_isTutorial;
							}
						}
					}
					else if (name == "IS_COOP")
					{
						return this.m_isCoop;
					}
				}
				else if (num <= 912310022U)
				{
					if (num != 806552859U)
					{
						if (num == 912310022U)
						{
							if (name == "TB_TEXTURE_PHONE_OFFSET_Y")
							{
								return this.m_tbTexturePhoneOffsetY;
							}
						}
					}
					else if (name == "PLAYER1_DECK_ID")
					{
						return this.m_player1DeckId;
					}
				}
				else if (num != 931009045U)
				{
					if (num == 1103584457U)
					{
						if (name == "DESCRIPTION")
						{
							return this.m_description;
						}
					}
				}
				else if (name == "ONE_SIM_PER_PLAYER")
				{
					return this.m_oneSimPerPlayer;
				}
			}
			else if (num <= 1460468812U)
			{
				if (num <= 1388406934U)
				{
					if (num != 1387956774U)
					{
						if (num == 1388406934U)
						{
							if (name == "PLAYER2_HERO_CARD_ID")
							{
								return this.m_player2HeroCardId;
							}
						}
					}
					else if (name == "NAME")
					{
						return this.m_name;
					}
				}
				else if (num != 1458105184U)
				{
					if (num == 1460468812U)
					{
						if (name == "SCRIPT_OBJECT")
						{
							return this.m_scriptObject;
						}
					}
				}
				else if (name == "ID")
				{
					return base.ID;
				}
			}
			else if (num <= 1832013819U)
			{
				if (num != 1559555090U)
				{
					if (num == 1832013819U)
					{
						if (name == "COMPLETED_DESCRIPTION")
						{
							return this.m_completedDescription;
						}
					}
				}
				else if (name == "WING_ID")
				{
					return this.m_wingId;
				}
			}
			else if (num != 1960700408U)
			{
				if (num == 2167425274U)
				{
					if (name == "IS_EXPERT")
					{
						return this.m_isExpert;
					}
				}
			}
			else if (name == "CLIENT_PLAYER2_HERO_CARD_ID")
			{
				return this.m_clientPlayer2HeroCardId;
			}
		}
		else if (num <= 3226467965U)
		{
			if (num <= 2626514961U)
			{
				if (num <= 2418820992U)
				{
					if (num != 2392988442U)
					{
						if (num == 2418820992U)
						{
							if (name == "SHORT_DESCRIPTION")
							{
								return this.m_shortDescription;
							}
						}
					}
					else if (name == "OPPONENT_NAME")
					{
						return this.m_opponentName;
					}
				}
				else if (num != 2575670047U)
				{
					if (num == 2626514961U)
					{
						if (name == "CHOOSE_HERO_TEXT")
						{
							return this.m_chooseHeroText;
						}
					}
				}
				else if (name == "TB_TEXTURE")
				{
					return this.m_tbTexture;
				}
			}
			else if (num <= 3022554311U)
			{
				if (num != 2822633211U)
				{
					if (num == 3022554311U)
					{
						if (name == "NOTE_DESC")
						{
							return this.m_noteDesc;
						}
					}
				}
				else if (name == "HIDE_BOSS_HERO_POWER_IN_UI")
				{
					return this.m_hideBossHeroPowerInUi;
				}
			}
			else if (num != 3183294954U)
			{
				if (num == 3226467965U)
				{
					if (name == "SHORT_NAME")
					{
						return this.m_shortName;
					}
				}
			}
			else if (name == "CLIENT_PLAYER2_HERO_POWER_CARD_ID")
			{
				return this.m_clientPlayer2HeroPowerCardId;
			}
		}
		else if (num <= 3439379241U)
		{
			if (num <= 3306344503U)
			{
				if (num != 3270521116U)
				{
					if (num == 3306344503U)
					{
						if (name == "DECK_RULESET_ID")
						{
							return this.m_deckRulesetId;
						}
					}
				}
				else if (name == "TB_TEXTURE_PHONE")
				{
					return this.m_tbTexturePhone;
				}
			}
			else if (num != 3321980672U)
			{
				if (num == 3439379241U)
				{
					if (name == "PLAYERS")
					{
						return this.m_players;
					}
				}
			}
			else if (name == "GAME_SAVE_DATA_PROGRESS_SUBKEY")
			{
				return this.m_gameSaveDataProgressSubkeyId;
			}
		}
		else if (num <= 3917898540U)
		{
			if (num != 3741890713U)
			{
				if (num == 3917898540U)
				{
					if (name == "RULE_TYPE")
					{
						return this.m_ruleType;
					}
				}
			}
			else if (name == "PLAYER1_HERO_CARD_ID")
			{
				return this.m_player1HeroCardId;
			}
		}
		else if (num != 3959141178U)
		{
			if (num == 4214602626U)
			{
				if (name == "SORT_ORDER")
				{
					return this.m_sortOrder;
				}
			}
		}
		else if (name == "MODE_ID")
		{
			return this.m_modeId;
		}
		return null;
	}

	// Token: 0x06002018 RID: 8216 RVA: 0x0009F34C File Offset: 0x0009D54C
	public override void SetVar(string name, object val)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 2167425274U)
		{
			if (num <= 1103584457U)
			{
				if (num <= 286783624U)
				{
					if (num <= 190718801U)
					{
						if (num != 179436941U)
						{
							if (num != 190718801U)
							{
								return;
							}
							if (!(name == "ADVENTURE_ID"))
							{
								return;
							}
							this.m_adventureId = (int)val;
							return;
						}
						else
						{
							if (!(name == "GAME_SAVE_DATA_PROGRESS_MAX"))
							{
								return;
							}
							this.m_gameSaveDataProgressMax = (int)val;
							return;
						}
					}
					else if (num != 191153791U)
					{
						if (num != 286783624U)
						{
							return;
						}
						if (!(name == "IS_TUTORIAL"))
						{
							return;
						}
						this.m_isTutorial = (bool)val;
						return;
					}
					else
					{
						if (!(name == "IS_COOP"))
						{
							return;
						}
						this.m_isCoop = (bool)val;
						return;
					}
				}
				else if (num <= 912310022U)
				{
					if (num != 806552859U)
					{
						if (num != 912310022U)
						{
							return;
						}
						if (!(name == "TB_TEXTURE_PHONE_OFFSET_Y"))
						{
							return;
						}
						this.m_tbTexturePhoneOffsetY = (double)val;
						return;
					}
					else
					{
						if (!(name == "PLAYER1_DECK_ID"))
						{
							return;
						}
						this.m_player1DeckId = (int)val;
						return;
					}
				}
				else if (num != 931009045U)
				{
					if (num != 1103584457U)
					{
						return;
					}
					if (!(name == "DESCRIPTION"))
					{
						return;
					}
					this.m_description = (DbfLocValue)val;
					return;
				}
				else
				{
					if (!(name == "ONE_SIM_PER_PLAYER"))
					{
						return;
					}
					this.m_oneSimPerPlayer = (bool)val;
					return;
				}
			}
			else if (num <= 1460468812U)
			{
				if (num <= 1388406934U)
				{
					if (num != 1387956774U)
					{
						if (num != 1388406934U)
						{
							return;
						}
						if (!(name == "PLAYER2_HERO_CARD_ID"))
						{
							return;
						}
						this.m_player2HeroCardId = (int)val;
						return;
					}
					else
					{
						if (!(name == "NAME"))
						{
							return;
						}
						this.m_name = (DbfLocValue)val;
						return;
					}
				}
				else if (num != 1458105184U)
				{
					if (num != 1460468812U)
					{
						return;
					}
					if (!(name == "SCRIPT_OBJECT"))
					{
						return;
					}
					this.m_scriptObject = (string)val;
				}
				else
				{
					if (!(name == "ID"))
					{
						return;
					}
					base.SetID((int)val);
					return;
				}
			}
			else if (num <= 1832013819U)
			{
				if (num != 1559555090U)
				{
					if (num != 1832013819U)
					{
						return;
					}
					if (!(name == "COMPLETED_DESCRIPTION"))
					{
						return;
					}
					this.m_completedDescription = (DbfLocValue)val;
					return;
				}
				else
				{
					if (!(name == "WING_ID"))
					{
						return;
					}
					this.m_wingId = (int)val;
					return;
				}
			}
			else if (num != 1960700408U)
			{
				if (num != 2167425274U)
				{
					return;
				}
				if (!(name == "IS_EXPERT"))
				{
					return;
				}
				this.m_isExpert = (bool)val;
				return;
			}
			else
			{
				if (!(name == "CLIENT_PLAYER2_HERO_CARD_ID"))
				{
					return;
				}
				this.m_clientPlayer2HeroCardId = (int)val;
				return;
			}
		}
		else if (num <= 3226467965U)
		{
			if (num <= 2626514961U)
			{
				if (num <= 2418820992U)
				{
					if (num != 2392988442U)
					{
						if (num != 2418820992U)
						{
							return;
						}
						if (!(name == "SHORT_DESCRIPTION"))
						{
							return;
						}
						this.m_shortDescription = (DbfLocValue)val;
						return;
					}
					else
					{
						if (!(name == "OPPONENT_NAME"))
						{
							return;
						}
						this.m_opponentName = (DbfLocValue)val;
						return;
					}
				}
				else if (num != 2575670047U)
				{
					if (num != 2626514961U)
					{
						return;
					}
					if (!(name == "CHOOSE_HERO_TEXT"))
					{
						return;
					}
					this.m_chooseHeroText = (DbfLocValue)val;
					return;
				}
				else
				{
					if (!(name == "TB_TEXTURE"))
					{
						return;
					}
					this.m_tbTexture = (string)val;
					return;
				}
			}
			else if (num <= 3022554311U)
			{
				if (num != 2822633211U)
				{
					if (num != 3022554311U)
					{
						return;
					}
					if (!(name == "NOTE_DESC"))
					{
						return;
					}
					this.m_noteDesc = (string)val;
					return;
				}
				else
				{
					if (!(name == "HIDE_BOSS_HERO_POWER_IN_UI"))
					{
						return;
					}
					this.m_hideBossHeroPowerInUi = (bool)val;
					return;
				}
			}
			else if (num != 3183294954U)
			{
				if (num != 3226467965U)
				{
					return;
				}
				if (!(name == "SHORT_NAME"))
				{
					return;
				}
				this.m_shortName = (DbfLocValue)val;
				return;
			}
			else
			{
				if (!(name == "CLIENT_PLAYER2_HERO_POWER_CARD_ID"))
				{
					return;
				}
				this.m_clientPlayer2HeroPowerCardId = (int)val;
				return;
			}
		}
		else if (num <= 3439379241U)
		{
			if (num <= 3306344503U)
			{
				if (num != 3270521116U)
				{
					if (num != 3306344503U)
					{
						return;
					}
					if (!(name == "DECK_RULESET_ID"))
					{
						return;
					}
					this.m_deckRulesetId = (int)val;
					return;
				}
				else
				{
					if (!(name == "TB_TEXTURE_PHONE"))
					{
						return;
					}
					this.m_tbTexturePhone = (string)val;
					return;
				}
			}
			else if (num != 3321980672U)
			{
				if (num != 3439379241U)
				{
					return;
				}
				if (!(name == "PLAYERS"))
				{
					return;
				}
				this.m_players = (int)val;
				return;
			}
			else
			{
				if (!(name == "GAME_SAVE_DATA_PROGRESS_SUBKEY"))
				{
					return;
				}
				this.m_gameSaveDataProgressSubkeyId = (int)val;
				return;
			}
		}
		else if (num <= 3917898540U)
		{
			if (num != 3741890713U)
			{
				if (num != 3917898540U)
				{
					return;
				}
				if (!(name == "RULE_TYPE"))
				{
					return;
				}
				if (val == null)
				{
					this.m_ruleType = Scenario.RuleType.NONE;
					return;
				}
				if (val is Scenario.RuleType || val is int)
				{
					this.m_ruleType = (Scenario.RuleType)val;
					return;
				}
				if (val is string)
				{
					this.m_ruleType = Scenario.ParseRuleTypeValue((string)val);
					return;
				}
			}
			else
			{
				if (!(name == "PLAYER1_HERO_CARD_ID"))
				{
					return;
				}
				this.m_player1HeroCardId = (int)val;
				return;
			}
		}
		else if (num != 3959141178U)
		{
			if (num != 4214602626U)
			{
				return;
			}
			if (!(name == "SORT_ORDER"))
			{
				return;
			}
			this.m_sortOrder = (int)val;
			return;
		}
		else
		{
			if (!(name == "MODE_ID"))
			{
				return;
			}
			this.m_modeId = (int)val;
			return;
		}
	}

	// Token: 0x06002019 RID: 8217 RVA: 0x0009F93C File Offset: 0x0009DB3C
	public override Type GetVarType(string name)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 2167425274U)
		{
			if (num <= 1103584457U)
			{
				if (num <= 286783624U)
				{
					if (num <= 190718801U)
					{
						if (num != 179436941U)
						{
							if (num == 190718801U)
							{
								if (name == "ADVENTURE_ID")
								{
									return typeof(int);
								}
							}
						}
						else if (name == "GAME_SAVE_DATA_PROGRESS_MAX")
						{
							return typeof(int);
						}
					}
					else if (num != 191153791U)
					{
						if (num == 286783624U)
						{
							if (name == "IS_TUTORIAL")
							{
								return typeof(bool);
							}
						}
					}
					else if (name == "IS_COOP")
					{
						return typeof(bool);
					}
				}
				else if (num <= 912310022U)
				{
					if (num != 806552859U)
					{
						if (num == 912310022U)
						{
							if (name == "TB_TEXTURE_PHONE_OFFSET_Y")
							{
								return typeof(double);
							}
						}
					}
					else if (name == "PLAYER1_DECK_ID")
					{
						return typeof(int);
					}
				}
				else if (num != 931009045U)
				{
					if (num == 1103584457U)
					{
						if (name == "DESCRIPTION")
						{
							return typeof(DbfLocValue);
						}
					}
				}
				else if (name == "ONE_SIM_PER_PLAYER")
				{
					return typeof(bool);
				}
			}
			else if (num <= 1460468812U)
			{
				if (num <= 1388406934U)
				{
					if (num != 1387956774U)
					{
						if (num == 1388406934U)
						{
							if (name == "PLAYER2_HERO_CARD_ID")
							{
								return typeof(int);
							}
						}
					}
					else if (name == "NAME")
					{
						return typeof(DbfLocValue);
					}
				}
				else if (num != 1458105184U)
				{
					if (num == 1460468812U)
					{
						if (name == "SCRIPT_OBJECT")
						{
							return typeof(string);
						}
					}
				}
				else if (name == "ID")
				{
					return typeof(int);
				}
			}
			else if (num <= 1832013819U)
			{
				if (num != 1559555090U)
				{
					if (num == 1832013819U)
					{
						if (name == "COMPLETED_DESCRIPTION")
						{
							return typeof(DbfLocValue);
						}
					}
				}
				else if (name == "WING_ID")
				{
					return typeof(int);
				}
			}
			else if (num != 1960700408U)
			{
				if (num == 2167425274U)
				{
					if (name == "IS_EXPERT")
					{
						return typeof(bool);
					}
				}
			}
			else if (name == "CLIENT_PLAYER2_HERO_CARD_ID")
			{
				return typeof(int);
			}
		}
		else if (num <= 3226467965U)
		{
			if (num <= 2626514961U)
			{
				if (num <= 2418820992U)
				{
					if (num != 2392988442U)
					{
						if (num == 2418820992U)
						{
							if (name == "SHORT_DESCRIPTION")
							{
								return typeof(DbfLocValue);
							}
						}
					}
					else if (name == "OPPONENT_NAME")
					{
						return typeof(DbfLocValue);
					}
				}
				else if (num != 2575670047U)
				{
					if (num == 2626514961U)
					{
						if (name == "CHOOSE_HERO_TEXT")
						{
							return typeof(DbfLocValue);
						}
					}
				}
				else if (name == "TB_TEXTURE")
				{
					return typeof(string);
				}
			}
			else if (num <= 3022554311U)
			{
				if (num != 2822633211U)
				{
					if (num == 3022554311U)
					{
						if (name == "NOTE_DESC")
						{
							return typeof(string);
						}
					}
				}
				else if (name == "HIDE_BOSS_HERO_POWER_IN_UI")
				{
					return typeof(bool);
				}
			}
			else if (num != 3183294954U)
			{
				if (num == 3226467965U)
				{
					if (name == "SHORT_NAME")
					{
						return typeof(DbfLocValue);
					}
				}
			}
			else if (name == "CLIENT_PLAYER2_HERO_POWER_CARD_ID")
			{
				return typeof(int);
			}
		}
		else if (num <= 3439379241U)
		{
			if (num <= 3306344503U)
			{
				if (num != 3270521116U)
				{
					if (num == 3306344503U)
					{
						if (name == "DECK_RULESET_ID")
						{
							return typeof(int);
						}
					}
				}
				else if (name == "TB_TEXTURE_PHONE")
				{
					return typeof(string);
				}
			}
			else if (num != 3321980672U)
			{
				if (num == 3439379241U)
				{
					if (name == "PLAYERS")
					{
						return typeof(int);
					}
				}
			}
			else if (name == "GAME_SAVE_DATA_PROGRESS_SUBKEY")
			{
				return typeof(int);
			}
		}
		else if (num <= 3917898540U)
		{
			if (num != 3741890713U)
			{
				if (num == 3917898540U)
				{
					if (name == "RULE_TYPE")
					{
						return typeof(Scenario.RuleType);
					}
				}
			}
			else if (name == "PLAYER1_HERO_CARD_ID")
			{
				return typeof(int);
			}
		}
		else if (num != 3959141178U)
		{
			if (num == 4214602626U)
			{
				if (name == "SORT_ORDER")
				{
					return typeof(int);
				}
			}
		}
		else if (name == "MODE_ID")
		{
			return typeof(int);
		}
		return null;
	}

	// Token: 0x0600201A RID: 8218 RVA: 0x0009FF7C File Offset: 0x0009E17C
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadScenarioDbfRecords loadRecords = new LoadScenarioDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x0600201B RID: 8219 RVA: 0x0009FF94 File Offset: 0x0009E194
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		ScenarioDbfAsset scenarioDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(ScenarioDbfAsset)) as ScenarioDbfAsset;
		if (scenarioDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("ScenarioDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < scenarioDbfAsset.Records.Count; i++)
		{
			scenarioDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (scenarioDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x0600201C RID: 8220 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x0600201D RID: 8221 RVA: 0x000A0014 File Offset: 0x0009E214
	public override void StripUnusedLocales()
	{
		this.m_name.StripUnusedLocales();
		this.m_shortName.StripUnusedLocales();
		this.m_description.StripUnusedLocales();
		this.m_shortDescription.StripUnusedLocales();
		this.m_opponentName.StripUnusedLocales();
		this.m_completedDescription.StripUnusedLocales();
		this.m_chooseHeroText.StripUnusedLocales();
	}

	// Token: 0x04001207 RID: 4615
	[SerializeField]
	private string m_noteDesc;

	// Token: 0x04001208 RID: 4616
	[SerializeField]
	private int m_players;

	// Token: 0x04001209 RID: 4617
	[SerializeField]
	private int m_player1HeroCardId;

	// Token: 0x0400120A RID: 4618
	[SerializeField]
	private int m_player2HeroCardId;

	// Token: 0x0400120B RID: 4619
	[SerializeField]
	private bool m_isTutorial;

	// Token: 0x0400120C RID: 4620
	[SerializeField]
	private bool m_isExpert = true;

	// Token: 0x0400120D RID: 4621
	[SerializeField]
	private bool m_isCoop;

	// Token: 0x0400120E RID: 4622
	[SerializeField]
	private bool m_oneSimPerPlayer;

	// Token: 0x0400120F RID: 4623
	[SerializeField]
	private int m_adventureId;

	// Token: 0x04001210 RID: 4624
	[SerializeField]
	private int m_wingId;

	// Token: 0x04001211 RID: 4625
	[SerializeField]
	private int m_sortOrder;

	// Token: 0x04001212 RID: 4626
	[SerializeField]
	private int m_modeId;

	// Token: 0x04001213 RID: 4627
	[SerializeField]
	private int m_clientPlayer2HeroCardId;

	// Token: 0x04001214 RID: 4628
	[SerializeField]
	private int m_clientPlayer2HeroPowerCardId;

	// Token: 0x04001215 RID: 4629
	[SerializeField]
	private DbfLocValue m_name;

	// Token: 0x04001216 RID: 4630
	[SerializeField]
	private DbfLocValue m_shortName;

	// Token: 0x04001217 RID: 4631
	[SerializeField]
	private DbfLocValue m_description;

	// Token: 0x04001218 RID: 4632
	[SerializeField]
	private DbfLocValue m_shortDescription;

	// Token: 0x04001219 RID: 4633
	[SerializeField]
	private DbfLocValue m_opponentName;

	// Token: 0x0400121A RID: 4634
	[SerializeField]
	private DbfLocValue m_completedDescription;

	// Token: 0x0400121B RID: 4635
	[SerializeField]
	private int m_player1DeckId;

	// Token: 0x0400121C RID: 4636
	[SerializeField]
	private int m_deckRulesetId;

	// Token: 0x0400121D RID: 4637
	[SerializeField]
	private Scenario.RuleType m_ruleType;

	// Token: 0x0400121E RID: 4638
	[SerializeField]
	private DbfLocValue m_chooseHeroText;

	// Token: 0x0400121F RID: 4639
	[SerializeField]
	private string m_tbTexture;

	// Token: 0x04001220 RID: 4640
	[SerializeField]
	private string m_tbTexturePhone;

	// Token: 0x04001221 RID: 4641
	[SerializeField]
	private double m_tbTexturePhoneOffsetY;

	// Token: 0x04001222 RID: 4642
	[SerializeField]
	private int m_gameSaveDataProgressSubkeyId;

	// Token: 0x04001223 RID: 4643
	[SerializeField]
	private int m_gameSaveDataProgressMax;

	// Token: 0x04001224 RID: 4644
	[SerializeField]
	private bool m_hideBossHeroPowerInUi;

	// Token: 0x04001225 RID: 4645
	[SerializeField]
	private string m_scriptObject;
}
