using System;

// Token: 0x02000946 RID: 2374
[Serializable]
public enum MusicPlaylistType
{
	// Token: 0x04006DAC RID: 28076
	Invalid,
	// Token: 0x04006DAD RID: 28077
	UI_MainTitle = 100,
	// Token: 0x04006DAE RID: 28078
	UI_Tournament,
	// Token: 0x04006DAF RID: 28079
	UI_Arena,
	// Token: 0x04006DB0 RID: 28080
	UI_Friendly,
	// Token: 0x04006DB1 RID: 28081
	UI_CollectionManager,
	// Token: 0x04006DB2 RID: 28082
	UI_PackOpening,
	// Token: 0x04006DB3 RID: 28083
	UI_Credits,
	// Token: 0x04006DB4 RID: 28084
	UI_EndGameScreen,
	// Token: 0x04006DB5 RID: 28085
	UI_TavernBrawl,
	// Token: 0x04006DB6 RID: 28086
	UI_CMHeroSkinPreview,
	// Token: 0x04006DB7 RID: 28087
	UI_HeroicBrawl,
	// Token: 0x04006DB8 RID: 28088
	UI_NullSilence,
	// Token: 0x04006DB9 RID: 28089
	UI_Battlegrounds,
	// Token: 0x04006DBA RID: 28090
	UI_Store,
	// Token: 0x04006DBB RID: 28091
	UI_Duels,
	// Token: 0x04006DBC RID: 28092
	UI_Journal,
	// Token: 0x04006DBD RID: 28093
	InGame_Mulligan = 200,
	// Token: 0x04006DBE RID: 28094
	InGame_MulliganSoft,
	// Token: 0x04006DBF RID: 28095
	InGame_Default,
	// Token: 0x04006DC0 RID: 28096
	InGame_GvGBoard,
	// Token: 0x04006DC1 RID: 28097
	InGame_NaxxramasAdventure,
	// Token: 0x04006DC2 RID: 28098
	InGame_BRMAdventure,
	// Token: 0x04006DC3 RID: 28099
	InGame_LOE1Adventure,
	// Token: 0x04006DC4 RID: 28100
	InGame_LOE2Adventure,
	// Token: 0x04006DC5 RID: 28101
	InGame_LOE_Minecart,
	// Token: 0x04006DC6 RID: 28102
	InGame_LOE_Wing3,
	// Token: 0x04006DC7 RID: 28103
	InGame_LOE_Wing4Mission4,
	// Token: 0x04006DC8 RID: 28104
	InGame_Karazhan,
	// Token: 0x04006DC9 RID: 28105
	InGame_KarazhanPrologue,
	// Token: 0x04006DCA RID: 28106
	InGame_KarazhanFreeMedivh,
	// Token: 0x04006DCB RID: 28107
	InGame_ICC,
	// Token: 0x04006DCC RID: 28108
	InGame_ICCLichKing,
	// Token: 0x04006DCD RID: 28109
	InGame_ICCMulligan,
	// Token: 0x04006DCE RID: 28110
	InGame_LOOT,
	// Token: 0x04006DCF RID: 28111
	InGame_LOOTFinalBoss,
	// Token: 0x04006DD0 RID: 28112
	InGame_LOOTMulligan,
	// Token: 0x04006DD1 RID: 28113
	InGame_GIL,
	// Token: 0x04006DD2 RID: 28114
	InGame_GILFinalBoss,
	// Token: 0x04006DD3 RID: 28115
	InGame_GILMulligan,
	// Token: 0x04006DD4 RID: 28116
	InGame_BOT,
	// Token: 0x04006DD5 RID: 28117
	InGame_BOTFinalBoss,
	// Token: 0x04006DD6 RID: 28118
	InGame_BOTMulligan,
	// Token: 0x04006DD7 RID: 28119
	InGame_TRL,
	// Token: 0x04006DD8 RID: 28120
	InGame_TRLFinalBoss,
	// Token: 0x04006DD9 RID: 28121
	InGame_TRLMulligan,
	// Token: 0x04006DDA RID: 28122
	InGame_TRLAdventure,
	// Token: 0x04006DDB RID: 28123
	InGame_DAL,
	// Token: 0x04006DDC RID: 28124
	InGame_DALFinalBoss,
	// Token: 0x04006DDD RID: 28125
	InGame_DALMulligan,
	// Token: 0x04006DDE RID: 28126
	InGame_ULD,
	// Token: 0x04006DDF RID: 28127
	InGame_ULDFinalBoss,
	// Token: 0x04006DE0 RID: 28128
	InGame_ULDMulligan,
	// Token: 0x04006DE1 RID: 28129
	InGame_BGSShop,
	// Token: 0x04006DE2 RID: 28130
	InGame_BGSCombat,
	// Token: 0x04006DE3 RID: 28131
	InGame_DRG,
	// Token: 0x04006DE4 RID: 28132
	InGame_DRGMulligan,
	// Token: 0x04006DE5 RID: 28133
	InGame_DRGLOEBoss,
	// Token: 0x04006DE6 RID: 28134
	InGame_DRGEVILBoss,
	// Token: 0x04006DE7 RID: 28135
	InGame_BT,
	// Token: 0x04006DE8 RID: 28136
	InGame_DHPrologue,
	// Token: 0x04006DE9 RID: 28137
	InGame_DHMulligan,
	// Token: 0x04006DEA RID: 28138
	InGame_DHPrologueBoss,
	// Token: 0x04006DEB RID: 28139
	InGame_BT_FinalBoss,
	// Token: 0x04006DEC RID: 28140
	InGame_SCH,
	// Token: 0x04006DED RID: 28141
	InGame_SCH_FinalLevels,
	// Token: 0x04006DEE RID: 28142
	InGame_SCH_Mulligan,
	// Token: 0x04006DEF RID: 28143
	InGame_DMF,
	// Token: 0x04006DF0 RID: 28144
	InGame_DMF_FinalLevels,
	// Token: 0x04006DF1 RID: 28145
	InGame_DMF_Mulligan,
	// Token: 0x04006DF2 RID: 28146
	InGame_BAR,
	// Token: 0x04006DF3 RID: 28147
	UISolo_Practice = 300,
	// Token: 0x04006DF4 RID: 28148
	UISolo_Naxxramas,
	// Token: 0x04006DF5 RID: 28149
	UISolo_BRM,
	// Token: 0x04006DF6 RID: 28150
	UISolo_LOE_Select,
	// Token: 0x04006DF7 RID: 28151
	UISolo_LOE_Mission,
	// Token: 0x04006DF8 RID: 28152
	UISolo_Karazhan,
	// Token: 0x04006DF9 RID: 28153
	UISolo_ICC,
	// Token: 0x04006DFA RID: 28154
	UISolo_LOOT,
	// Token: 0x04006DFB RID: 28155
	UISolo_LOOT_Select,
	// Token: 0x04006DFC RID: 28156
	UISolo_GIL,
	// Token: 0x04006DFD RID: 28157
	UISolo_GIL_Select,
	// Token: 0x04006DFE RID: 28158
	UISolo_BOT,
	// Token: 0x04006DFF RID: 28159
	UISolo_BOT_Select,
	// Token: 0x04006E00 RID: 28160
	UISolo_TRL,
	// Token: 0x04006E01 RID: 28161
	UISolo_TRL_Select,
	// Token: 0x04006E02 RID: 28162
	UISolo_DAL_Select,
	// Token: 0x04006E03 RID: 28163
	UISolo_DAL,
	// Token: 0x04006E04 RID: 28164
	UISolo_ULD_Select,
	// Token: 0x04006E05 RID: 28165
	UISolo_ULD,
	// Token: 0x04006E06 RID: 28166
	UISolo_DRG_Select,
	// Token: 0x04006E07 RID: 28167
	UISolo_DRG,
	// Token: 0x04006E08 RID: 28168
	UISolo_DHPrologue,
	// Token: 0x04006E09 RID: 28169
	UISolo_DHPrologue_Select,
	// Token: 0x04006E0A RID: 28170
	UISolo_RPE,
	// Token: 0x04006E0B RID: 28171
	UISolo_BT,
	// Token: 0x04006E0C RID: 28172
	UISolo_BOH,
	// Token: 0x04006E0D RID: 28173
	UISolo_BOHJaina,
	// Token: 0x04006E0E RID: 28174
	UISolo_BOHRexxar,
	// Token: 0x04006E0F RID: 28175
	UISolo_BOHGarrosh,
	// Token: 0x04006E10 RID: 28176
	UISolo_BOHUther,
	// Token: 0x04006E11 RID: 28177
	UISolo_BOHAnduin,
	// Token: 0x04006E12 RID: 28178
	UISolo_BOHValeera,
	// Token: 0x04006E13 RID: 28179
	UISolo_BOHThrall,
	// Token: 0x04006E14 RID: 28180
	UISolo_BOMRokara,
	// Token: 0x04006E15 RID: 28181
	UISolo_BOM,
	// Token: 0x04006E16 RID: 28182
	UISolo_BOMXyrella,
	// Token: 0x04006E17 RID: 28183
	UISolo_BOMGuff,
	// Token: 0x04006E18 RID: 28184
	UISolo_BOHMalfurion,
	// Token: 0x04006E19 RID: 28185
	Store_PacksClassic = 400,
	// Token: 0x04006E1A RID: 28186
	Store_PacksGvG,
	// Token: 0x04006E1B RID: 28187
	Store_PacksTGT,
	// Token: 0x04006E1C RID: 28188
	Store_PacksOG,
	// Token: 0x04006E1D RID: 28189
	Store_PacksMSG,
	// Token: 0x04006E1E RID: 28190
	Store_PacksUNG,
	// Token: 0x04006E1F RID: 28191
	Store_PacksICC,
	// Token: 0x04006E20 RID: 28192
	Store_PacksLOOT,
	// Token: 0x04006E21 RID: 28193
	Store_PacksGIL,
	// Token: 0x04006E22 RID: 28194
	Store_PacksBOT,
	// Token: 0x04006E23 RID: 28195
	Store_PacksTRL,
	// Token: 0x04006E24 RID: 28196
	Store_PacksDAL,
	// Token: 0x04006E25 RID: 28197
	Store_PacksULD,
	// Token: 0x04006E26 RID: 28198
	Store_PacksDRG,
	// Token: 0x04006E27 RID: 28199
	Store_PacksBT,
	// Token: 0x04006E28 RID: 28200
	Store_PacksSCH,
	// Token: 0x04006E29 RID: 28201
	Store_packsDMF,
	// Token: 0x04006E2A RID: 28202
	Store_PacksDMFMiniSet,
	// Token: 0x04006E2B RID: 28203
	Store_PacksBAR,
	// Token: 0x04006E2C RID: 28204
	Store_AdvNaxxramas = 450,
	// Token: 0x04006E2D RID: 28205
	Store_AdvBRM,
	// Token: 0x04006E2E RID: 28206
	Store_AdvLOE,
	// Token: 0x04006E2F RID: 28207
	Store_AdvKarazhan,
	// Token: 0x04006E30 RID: 28208
	Misc_Tutorial01 = 501,
	// Token: 0x04006E31 RID: 28209
	Misc_Tutorial01PackOpen,
	// Token: 0x04006E32 RID: 28210
	Hero_Magni = 900,
	// Token: 0x04006E33 RID: 28211
	Hero_Alleria,
	// Token: 0x04006E34 RID: 28212
	Hero_Medvih,
	// Token: 0x04006E35 RID: 28213
	Hero_Liadrin,
	// Token: 0x04006E36 RID: 28214
	Hero_Khadgar,
	// Token: 0x04006E37 RID: 28215
	Hero_Morgl,
	// Token: 0x04006E38 RID: 28216
	Hero_Tyrande,
	// Token: 0x04006E39 RID: 28217
	Hero_Maiev,
	// Token: 0x04006E3A RID: 28218
	Hero_Arthas,
	// Token: 0x04006E3B RID: 28219
	Hero_Nemsy,
	// Token: 0x04006E3C RID: 28220
	Hero_Lunara,
	// Token: 0x04006E3D RID: 28221
	Hero_MechaJ = 912,
	// Token: 0x04006E3E RID: 28222
	Hero_SirAnnoyo,
	// Token: 0x04006E3F RID: 28223
	Hero_Rastakhan,
	// Token: 0x04006E40 RID: 28224
	Hero_Lazul,
	// Token: 0x04006E41 RID: 28225
	Hero_ThunderKing,
	// Token: 0x04006E42 RID: 28226
	Hero_Elise,
	// Token: 0x04006E43 RID: 28227
	Hero_Deathwing,
	// Token: 0x04006E44 RID: 28228
	Hero_Hazelbark,
	// Token: 0x04006E45 RID: 28229
	Hero_Sylvanas,
	// Token: 0x04006E46 RID: 28230
	Hero_LadyVashj,
	// Token: 0x04006E47 RID: 28231
	Hero_Aranna,
	// Token: 0x04006E48 RID: 28232
	Hero_KelThuzad,
	// Token: 0x04006E49 RID: 28233
	Hero_NZoth,
	// Token: 0x04006E4A RID: 28234
	Hero_Annhylde,
	// Token: 0x04006E4B RID: 28235
	Hero_Hamuul
}
