using System;
using System.ComponentModel;

// Token: 0x0200095F RID: 2399
public enum SpecialEventType
{
	// Token: 0x04006EE2 RID: 28386
	UNKNOWN = -1,
	// Token: 0x04006EE3 RID: 28387
	[Description("none")]
	IGNORE,
	// Token: 0x04006EE4 RID: 28388
	[Description("fireside_gatherings_cardback")]
	FIRESIDE_GATHERINGS_CARDBACK,
	// Token: 0x04006EE5 RID: 28389
	[Description("gvg_promote")]
	GVG_PROMOTION = 7,
	// Token: 0x04006EE6 RID: 28390
	[Description("lunar_new_year")]
	LUNAR_NEW_YEAR = 11,
	// Token: 0x04006EE7 RID: 28391
	[Description("tb_pre_event")]
	SPECIAL_EVENT_PRE_TAVERN_BRAWL = 19,
	// Token: 0x04006EE8 RID: 28392
	[Description("feast_of_winter_veil")]
	FEAST_OF_WINTER_VEIL = 85,
	// Token: 0x04006EE9 RID: 28393
	[Description("post_set_rotation_2017")]
	SPECIAL_EVENT_POST_SET_ROTATION_2017 = 87,
	// Token: 0x04006EEA RID: 28394
	[Description("post_set_rotation_2018")]
	SPECIAL_EVENT_POST_SET_ROTATION_2018,
	// Token: 0x04006EEB RID: 28395
	[Description("post_set_rotation_2019")]
	SPECIAL_EVENT_POST_SET_ROTATION_2019,
	// Token: 0x04006EEC RID: 28396
	[Description("post_set_rotation_2020")]
	SPECIAL_EVENT_POST_SET_ROTATION_2020,
	// Token: 0x04006EED RID: 28397
	[Description("post_set_rotation_2021")]
	SPECIAL_EVENT_POST_SET_ROTATION_2021,
	// Token: 0x04006EEE RID: 28398
	[Description("never")]
	SPECIAL_EVENT_NEVER = 164,
	// Token: 0x04006EEF RID: 28399
	[Description("friend_week")]
	FRIEND_WEEK = 166,
	// Token: 0x04006EF0 RID: 28400
	[Description("pre_set_rotation_2017")]
	SPECIAL_EVENT_PRE_SET_ROTATION_2017 = 183,
	// Token: 0x04006EF1 RID: 28401
	[Description("pre_set_rotation_2018")]
	SPECIAL_EVENT_PRE_SET_ROTATION_2018,
	// Token: 0x04006EF2 RID: 28402
	[Description("pre_set_rotation_2019")]
	SPECIAL_EVENT_PRE_SET_ROTATION_2019,
	// Token: 0x04006EF3 RID: 28403
	[Description("pre_set_rotation_2020")]
	SPECIAL_EVENT_PRE_SET_ROTATION_2020,
	// Token: 0x04006EF4 RID: 28404
	[Description("pre_set_rotation_2021")]
	SPECIAL_EVENT_PRE_SET_ROTATION_2021,
	// Token: 0x04006EF5 RID: 28405
	[Description("always")]
	SPECIAL_EVENT_ALWAYS = 203,
	// Token: 0x04006EF6 RID: 28406
	[Description("event_happy_new_year")]
	SPECIAL_EVENT_HAPPY_NEW_YEAR = 219,
	// Token: 0x04006EF7 RID: 28407
	[Description("fire_festival")]
	SPECIAL_EVENT_FIRE_FESTIVAL = 287,
	// Token: 0x04006EF8 RID: 28408
	[Description("gold_doubled")]
	SPECIAL_EVENT_GOLD_DOUBLED = 289,
	// Token: 0x04006EF9 RID: 28409
	[Description("frost_festival")]
	SPECIAL_EVENT_FROST_FESTIVAL = 292,
	// Token: 0x04006EFA RID: 28410
	[Description("icc_normal_sale")]
	SPECIAL_EVENT_ICC_NORMAL_SALE = 307,
	// Token: 0x04006EFB RID: 28411
	[Description("frost_fest_free_arena_win")]
	SPECIAL_EVENT_FROST_FESTIVAL_FREE_ARENA_WIN = 315,
	// Token: 0x04006EFC RID: 28412
	[Description("pirate_day")]
	SPECIAL_EVENT_PIRATE_DAY,
	// Token: 0x04006EFD RID: 28413
	[Description("icc_launch_freepacks")]
	SPECIAL_EVENT_ICC_LAUNCH_FREEPACKS = 320,
	// Token: 0x04006EFE RID: 28414
	[Description("hallows_end")]
	SPECIAL_EVENT_HALLOWS_END,
	// Token: 0x04006EFF RID: 28415
	[Description("hearthstone_world_championship")]
	SPECIAL_EVENT_HEARTHSTONE_WORLD_CHAMPIONSHIP = 408,
	// Token: 0x04006F00 RID: 28416
	[Description("wild_week_2018")]
	SPECIAL_EVENT_WILD_WEEK_2018 = 410,
	// Token: 0x04006F01 RID: 28417
	[Description("road_to_raven")]
	SPECIAL_EVENT_ROAD_TO_RAVEN = 414,
	// Token: 0x04006F02 RID: 28418
	[Description("noblegarden_event")]
	SPECIAL_EVENT_NOBLEGARDEN = 473,
	// Token: 0x04006F03 RID: 28419
	[Description("taverns_of_time")]
	SPECIAL_EVENT_TAVERNS_OF_TIME = 490,
	// Token: 0x04006F04 RID: 28420
	[Description("fire_festival_v2")]
	SPECIAL_EVENT_FIRE_FESTIVAL_V2 = 499,
	// Token: 0x04006F05 RID: 28421
	[Description("days_of_the_frozen_throne")]
	SPECIAL_EVENT_DAYS_OF_THE_FROZEN_THRONE = 525,
	// Token: 0x04006F06 RID: 28422
	[Description("blizzcon_2018_flare")]
	SPECIAL_EVENT_BLIZZCON_2018_FLARE = 528,
	// Token: 0x04006F07 RID: 28423
	[Description("celebrate_the_players")]
	SPECIAL_EVENT_CELEBRATE_THE_PLAYERS = 541,
	// Token: 0x04006F08 RID: 28424
	[Description("feast_of_winter_veil_2018")]
	SPECIAL_EVENT_FEAST_OF_WINTER_VEIL_2018 = 567,
	// Token: 0x04006F09 RID: 28425
	[Description("rastakhan_season_week_1")]
	SPECIAL_EVENT_SEASON_OF_RASTAKHAN_WK1 = 580,
	// Token: 0x04006F0A RID: 28426
	[Description("rastakhan_season_week_2")]
	SPECIAL_EVENT_SEASON_OF_RASTAKHAN_WK2,
	// Token: 0x04006F0B RID: 28427
	[Description("rastakhan_season_week_3")]
	SPECIAL_EVENT_SEASON_OF_RASTAKHAN_WK3,
	// Token: 0x04006F0C RID: 28428
	[Description("henchmania_tb_quest")]
	SPECIAL_EVENT_HENCHMANIA_TB_SEASON,
	// Token: 0x04006F0D RID: 28429
	[Description("fire_festival_v3")]
	SPECIAL_EVENT_FIRE_FESTIVAL_V3,
	// Token: 0x04006F0E RID: 28430
	[Description("tb_season_221")]
	SPECIAL_EVENT_TB_SEASON_221,
	// Token: 0x04006F0F RID: 28431
	[Description("tb_season_222")]
	SPECIAL_EVENT_TB_SEASON_222,
	// Token: 0x04006F10 RID: 28432
	[Description("uldum_launch_quest")]
	SPECIAL_EVENT_ULDUM_LAUNCH_QUEST,
	// Token: 0x04006F11 RID: 28433
	[Description("post_hall_of_fame_2020")]
	SPECIAL_EVENT_POST_HALL_OF_FAME_2020,
	// Token: 0x04006F12 RID: 28434
	[Description("pre_hall_of_fame_2020")]
	SPECIAL_EVENT_PRE_HALL_OF_FAME_2020,
	// Token: 0x04006F13 RID: 28435
	[Description("fire_festival_emote_ever_green")]
	SPECIAL_EVENT_FIRE_FESTIVAL_EMOTES_EVERGREEN,
	// Token: 0x04006F14 RID: 28436
	[Description("fire_festival_box_dressing_ever_green")]
	SPECIAL_EVENT_FIRE_FESTIVAL_BOX_DRESSING_EVERGREEN,
	// Token: 0x04006F15 RID: 28437
	BASE_SPECIAL_EVENT_DATA_ID = 10000000
}
