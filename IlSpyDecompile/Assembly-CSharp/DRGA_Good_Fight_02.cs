using System.Collections;
using System.Collections.Generic;

public class DRGA_Good_Fight_02 : DRGA_Dungeon
{
	private static readonly AssetReference VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Boss_Death_01 = new AssetReference("VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Boss_Death_01.prefab:da1b64ec5dcd7694eac0d67c240e4880");

	private static readonly AssetReference VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Boss_Death_Alt_01 = new AssetReference("VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Boss_Death_Alt_01.prefab:daa92e634959fd844a8ff90d26cb542d");

	private static readonly AssetReference VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Boss_Flavor_01_01 = new AssetReference("VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Boss_Flavor_01_01.prefab:77b50b00c5814944e9e7d1750879d879");

	private static readonly AssetReference VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Boss_FreezeMinion_01_01 = new AssetReference("VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Boss_FreezeMinion_01_01.prefab:ba2cb30279a95114ba3490f1c0083c41");

	private static readonly AssetReference VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Boss_FreezeMinion_02_01 = new AssetReference("VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Boss_FreezeMinion_02_01.prefab:8b7cfaf8fff53124296c4c761a02233f");

	private static readonly AssetReference VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Boss_HeroPower_01_01 = new AssetReference("VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Boss_HeroPower_01_01.prefab:dedf917f412f47542a9c050b21edd2a9");

	private static readonly AssetReference VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Boss_HeroPower_02_01 = new AssetReference("VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Boss_HeroPower_02_01.prefab:466433cc1aa8ecc43969a24dd1d8c770");

	private static readonly AssetReference VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Boss_HeroPower_03_01 = new AssetReference("VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Boss_HeroPower_03_01.prefab:f62ea41de2f121649a731cc0f546fddf");

	private static readonly AssetReference VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Boss_HeroPower_03_02 = new AssetReference("VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Boss_HeroPower_03_02.prefab:056c5a4c611341e40836d37253810546");

	private static readonly AssetReference VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_BossAttack_01 = new AssetReference("VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_BossAttack_01.prefab:484551f72cc900e478af602332b3a57e");

	private static readonly AssetReference VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_BossStart_01 = new AssetReference("VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_BossStart_01.prefab:92219bb8f6e482f4ebe0ff4577c3ddfc");

	private static readonly AssetReference VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_EmoteResponse_01 = new AssetReference("VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_EmoteResponse_01.prefab:145f4cbc25254034f9a5ad8fd60539ec");

	private static readonly AssetReference VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Idle_01_01 = new AssetReference("VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Idle_01_01.prefab:849043988be8d9846a718f7fd239bc0e");

	private static readonly AssetReference VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Idle_02_01 = new AssetReference("VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Idle_02_01.prefab:d85dc236aeb873d4fa10374d9300d21c");

	private static readonly AssetReference VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Idle_03_01 = new AssetReference("VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Idle_03_01.prefab:6374771862d99fd4994ff3c74b2b9c36");

	private static readonly AssetReference VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Player_Avalanche_01 = new AssetReference("VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Player_Avalanche_01.prefab:084f81aecfe06df449a572536d4eaa59");

	private static readonly AssetReference VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Player_LivingDragonbreath_01 = new AssetReference("VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Player_LivingDragonbreath_01.prefab:1cedd3728cd8ff44ea197eb5647cff93");

	private static readonly AssetReference VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Boss_Flavor_02_01 = new AssetReference("VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Boss_Flavor_02_01.prefab:4f6c3d04538a92d429c9f4d789b5eb07");

	private static readonly AssetReference VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_ArchmageApprentice_01 = new AssetReference("VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_ArchmageApprentice_01.prefab:5ec2e3a6a6006d94dab3ed0d1cf93d1f");

	private static readonly AssetReference VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_ArchmageVargoth_01 = new AssetReference("VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_ArchmageVargoth_01.prefab:b8ffffc14e96748449566ec8c313510e");

	private static readonly AssetReference VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_AstralPortal_01 = new AssetReference("VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_AstralPortal_01.prefab:8d720509ea9e3824e87170ef9e9ab477");

	private static readonly AssetReference VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_Ending_01_01 = new AssetReference("VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_Ending_01_01.prefab:6b325da46c713b346ad9543e8080278e");

	private static readonly AssetReference VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_Ending_01_02 = new AssetReference("VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_Ending_01_02.prefab:21651a92447ed7446a7f9d94fe265409");

	private static readonly AssetReference VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_FloatingHead_01_01 = new AssetReference("VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_FloatingHead_01_01.prefab:6e44668e738fee74a8e4bb3a3259847f");

	private static readonly AssetReference VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_FloatingHead_02_01 = new AssetReference("VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_FloatingHead_02_01.prefab:0ba035d391033da47aaa12d9adcf4948");

	private static readonly AssetReference VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_FloatingHead_03_01 = new AssetReference("VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_FloatingHead_03_01.prefab:739430c7762668c49952cd3e2064e848");

	private static readonly AssetReference VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_Kalecgos_01 = new AssetReference("VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_Kalecgos_01.prefab:69e3d5df75cfe194c873300fbf4659ee");

	private static readonly AssetReference VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_Misc_01_01 = new AssetReference("VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_Misc_01_01.prefab:ffdfb1dc1b8245449aec6c313217859f");

	private static readonly AssetReference VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_Misc_02_01 = new AssetReference("VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_Misc_02_01.prefab:5d6ae6e4476436047bce3df63871e018");

	private static readonly AssetReference VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_Misc_04_01 = new AssetReference("VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_Misc_04_01.prefab:291af12de6ca34a4498f88f43cf2a53e");

	private static readonly AssetReference VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_PlayerStart_01 = new AssetReference("VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_PlayerStart_01.prefab:045e2a1330885054cbff9c52418d5c5e");

	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Copycard_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Copycard_01.prefab:bbb71f2dfac1c474da3209a15215f4ea");

	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_CopyCards_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_CopyCards_01.prefab:ad01bc4d23eab3e4f86c994d722cf247");

	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_CostRandomizer_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_CostRandomizer_01.prefab:688849bb5a4d2fd48a817159d1a224fa");

	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_DrawExtra_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_DrawExtra_01.prefab:001c634fac4ab874eb6e16d050554b1f");

	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Emptyhand_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Emptyhand_01.prefab:a5d093ea8ec90d64fb53834b44395720");

	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_ExtraMana_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_ExtraMana_01.prefab:97a9c24cb50609347964387b02a62b3c");

	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_ExtraTurn_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_ExtraTurn_01.prefab:feb7534214a13214bac9e2b8726a8cc8");

	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Fatigue_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Fatigue_01.prefab:3fd9e692fe70e924fa0fb5bfabcf17bd");

	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FatigueReset_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FatigueReset_01.prefab:701ed0ac22ab9a84daade7ed23403317");

	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FreeMinions_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FreeMinions_01.prefab:f2eeac3ed0b6f554dbfbc9deea60739f");

	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FreezeMinions_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FreezeMinions_01.prefab:f9054e8df8774e44d869a0f96ac07efa");

	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_HealPlayer_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_HealPlayer_01.prefab:d0a3f9b5c01e04d458178ca8c5069d66");

	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Minionwipe_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Minionwipe_01.prefab:ab486ac19b475c74f84999cc9a80b7a6");

	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_MirrorImage_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_MirrorImage_01.prefab:8789714bb9a92d143bb2024188b8ddd0");

	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_RandomCast_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_RandomCast_01.prefab:7fd61e2a38015f240bef703ee9f66e5c");

	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_RandomLegendary_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_RandomLegendary_01.prefab:9273a8457f705514f9755153f0c7abf6");

	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SaveLife_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SaveLife_01.prefab:cad9725371b04e943b07f43ecac56b32");

	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SelfDamage_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SelfDamage_01.prefab:ce7a5a15de006d041ad515427fc6f72f");

	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SpellCast_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SpellCast_01.prefab:92c7854b16b6919499ff3fe7e1e2a422");

	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Start_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Start_01.prefab:3edd1b61bd705b6439dd75542dd6b442");

	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Treasure_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Treasure_01.prefab:b9fae030ab3026a4bb17f592028c276d");

	private static readonly AssetReference Wisdomball_Pop_up_BrassRing_Quote = new AssetReference("Wisdomball_Pop-up_BrassRing_Quote.prefab:896ee20514caff74db639aa7055838f6");

	private List<string> m_missionEventTrigger101Lines = new List<string> { VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_Ending_01_01, VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_Ending_01_02 };

	private List<string> m_missionKhadgarFloatingHeadResponses = new List<string> { VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_FloatingHead_01_01, VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_FloatingHead_02_01, VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_FloatingHead_03_01 };

	private List<string> m_missionEventTrigger107Lines = new List<string> { VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Boss_FreezeMinion_01_01, VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Boss_FreezeMinion_02_01 };

	private List<string> m_VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Boss_HeroPowerLines = new List<string> { VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Boss_HeroPower_01_01, VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Boss_HeroPower_02_01, VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Boss_HeroPower_03_01, VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Boss_HeroPower_03_02 };

	private List<string> m_VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_IdleLines = new List<string> { VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Idle_01_01, VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Idle_02_01, VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Idle_03_01 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Boss_Death_01, VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Boss_Death_Alt_01, VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Boss_Flavor_01_01, VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Boss_FreezeMinion_01_01, VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Boss_FreezeMinion_02_01, VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Boss_HeroPower_01_01, VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Boss_HeroPower_02_01, VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Boss_HeroPower_03_01, VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Boss_HeroPower_03_02, VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_BossAttack_01,
			VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_BossStart_01, VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_EmoteResponse_01, VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Idle_01_01, VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Idle_02_01, VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Idle_03_01, VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Player_Avalanche_01, VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Player_LivingDragonbreath_01, VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Boss_Flavor_02_01, VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_ArchmageApprentice_01, VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_ArchmageVargoth_01,
			VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_AstralPortal_01, VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_Ending_01_01, VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_Ending_01_02, VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_FloatingHead_01_01, VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_FloatingHead_02_01, VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_FloatingHead_03_01, VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_Kalecgos_01, VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_Misc_01_01, VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_Misc_02_01, VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_Misc_04_01,
			VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_PlayerStart_01
		};
		SetBossVOLines(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	public override List<string> GetIdleLines()
	{
		return m_VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_IdleLines;
	}

	public override List<string> GetBossHeroPowerRandomLines()
	{
		return m_VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Boss_HeroPowerLines;
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_deathLine = VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Boss_Death_01;
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_BossStart_01, Notification.SpeechBubbleDirection.TopRight, actor));
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_EmoteResponse_01, Notification.SpeechBubbleDirection.TopRight, actor));
		}
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (missionEvent)
		{
		case 101:
			if (!m_Heroic)
			{
				GameState.Get().SetBusy(busy: true);
				yield return PlayRandomLineAlways(friendlyActor, m_missionEventTrigger101Lines);
				GameState.Get().SetBusy(busy: false);
			}
			break;
		case 102:
			if (!m_Heroic)
			{
				yield return PlayRandomLineAlways(friendlyActor, m_missionKhadgarFloatingHeadResponses);
			}
			break;
		case 103:
			if (!m_Heroic)
			{
				yield return PlayLineAlways(friendlyActor, VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_Misc_01_01);
			}
			break;
		case 104:
			if (!m_Heroic)
			{
				yield return PlayLineAlways(friendlyActor, VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_Misc_02_01);
			}
			break;
		case 107:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_missionEventTrigger107Lines);
			break;
		case 108:
			yield return PlayLineOnlyOnce(actor, VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_BossAttack_01);
			break;
		case 109:
			if (!m_Heroic)
			{
				yield return PlayLineAlways(actor, VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Boss_Flavor_01_01);
				yield return PlayLineAlways(friendlyActor, VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Boss_Flavor_02_01);
			}
			break;
		case 505:
			yield return PlayLineAlways(Wisdomball_Pop_up_BrassRing_Quote, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Copycard_01);
			if (!m_Heroic)
			{
				yield return PlayAndRemoveRandomLineOnlyOnce(friendlyActor, m_missionKhadgarFloatingHeadResponses);
			}
			break;
		case 506:
			yield return PlayLineAlways(Wisdomball_Pop_up_BrassRing_Quote, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_CopyCards_01);
			if (!m_Heroic)
			{
				yield return PlayAndRemoveRandomLineOnlyOnce(friendlyActor, m_missionKhadgarFloatingHeadResponses);
			}
			break;
		case 507:
			yield return PlayLineAlways(Wisdomball_Pop_up_BrassRing_Quote, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_CostRandomizer_01);
			if (!m_Heroic)
			{
				yield return PlayAndRemoveRandomLineOnlyOnce(friendlyActor, m_missionKhadgarFloatingHeadResponses);
			}
			break;
		case 508:
			yield return PlayLineAlways(Wisdomball_Pop_up_BrassRing_Quote, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_DrawExtra_01);
			if (!m_Heroic)
			{
				yield return PlayAndRemoveRandomLineOnlyOnce(friendlyActor, m_missionKhadgarFloatingHeadResponses);
			}
			break;
		case 509:
			yield return PlayLineAlways(Wisdomball_Pop_up_BrassRing_Quote, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Emptyhand_01);
			if (!m_Heroic)
			{
				yield return PlayAndRemoveRandomLineOnlyOnce(friendlyActor, m_missionKhadgarFloatingHeadResponses);
			}
			break;
		case 510:
			yield return PlayLineAlways(Wisdomball_Pop_up_BrassRing_Quote, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_ExtraMana_01);
			if (!m_Heroic)
			{
				yield return PlayAndRemoveRandomLineOnlyOnce(friendlyActor, m_missionKhadgarFloatingHeadResponses);
			}
			break;
		case 511:
			yield return PlayLineAlways(Wisdomball_Pop_up_BrassRing_Quote, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_ExtraTurn_01);
			if (!m_Heroic)
			{
				yield return PlayAndRemoveRandomLineOnlyOnce(friendlyActor, m_missionKhadgarFloatingHeadResponses);
			}
			break;
		case 512:
			yield return PlayLineAlways(Wisdomball_Pop_up_BrassRing_Quote, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Fatigue_01);
			if (!m_Heroic)
			{
				yield return PlayAndRemoveRandomLineOnlyOnce(friendlyActor, m_missionKhadgarFloatingHeadResponses);
			}
			break;
		case 513:
			yield return PlayLineAlways(Wisdomball_Pop_up_BrassRing_Quote, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FatigueReset_01);
			if (!m_Heroic)
			{
				yield return PlayAndRemoveRandomLineOnlyOnce(friendlyActor, m_missionKhadgarFloatingHeadResponses);
			}
			break;
		case 514:
			yield return PlayLineAlways(Wisdomball_Pop_up_BrassRing_Quote, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FreeMinions_01);
			if (!m_Heroic)
			{
				yield return PlayAndRemoveRandomLineOnlyOnce(friendlyActor, m_missionKhadgarFloatingHeadResponses);
			}
			break;
		case 515:
			yield return PlayLineAlways(Wisdomball_Pop_up_BrassRing_Quote, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FreezeMinions_01);
			if (!m_Heroic)
			{
				yield return PlayAndRemoveRandomLineOnlyOnce(friendlyActor, m_missionKhadgarFloatingHeadResponses);
			}
			break;
		case 516:
			yield return PlayLineAlways(Wisdomball_Pop_up_BrassRing_Quote, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_HealPlayer_01);
			if (!m_Heroic)
			{
				yield return PlayAndRemoveRandomLineOnlyOnce(friendlyActor, m_missionKhadgarFloatingHeadResponses);
			}
			break;
		case 517:
			yield return PlayLineAlways(Wisdomball_Pop_up_BrassRing_Quote, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Minionwipe_01);
			if (!m_Heroic)
			{
				yield return PlayAndRemoveRandomLineOnlyOnce(friendlyActor, m_missionKhadgarFloatingHeadResponses);
			}
			break;
		case 518:
			yield return PlayLineAlways(Wisdomball_Pop_up_BrassRing_Quote, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_MirrorImage_01);
			if (!m_Heroic)
			{
				yield return PlayAndRemoveRandomLineOnlyOnce(friendlyActor, m_missionKhadgarFloatingHeadResponses);
			}
			break;
		case 519:
			yield return PlayLineAlways(Wisdomball_Pop_up_BrassRing_Quote, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_RandomCast_01);
			if (!m_Heroic)
			{
				yield return PlayAndRemoveRandomLineOnlyOnce(friendlyActor, m_missionKhadgarFloatingHeadResponses);
			}
			break;
		case 520:
			yield return PlayLineAlways(Wisdomball_Pop_up_BrassRing_Quote, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_RandomLegendary_01);
			if (!m_Heroic)
			{
				yield return PlayAndRemoveRandomLineOnlyOnce(friendlyActor, m_missionKhadgarFloatingHeadResponses);
			}
			break;
		case 521:
			yield return PlayLineAlways(Wisdomball_Pop_up_BrassRing_Quote, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SaveLife_01);
			if (!m_Heroic)
			{
				yield return PlayAndRemoveRandomLineOnlyOnce(friendlyActor, m_missionKhadgarFloatingHeadResponses);
			}
			break;
		case 522:
			yield return PlayLineAlways(Wisdomball_Pop_up_BrassRing_Quote, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SelfDamage_01);
			if (!m_Heroic)
			{
				yield return PlayAndRemoveRandomLineOnlyOnce(friendlyActor, m_missionKhadgarFloatingHeadResponses);
			}
			break;
		case 523:
			yield return PlayLineAlways(Wisdomball_Pop_up_BrassRing_Quote, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SpellCast_01);
			if (!m_Heroic)
			{
				yield return PlayAndRemoveRandomLineOnlyOnce(friendlyActor, m_missionKhadgarFloatingHeadResponses);
			}
			break;
		case 524:
			yield return PlayLineAlways(Wisdomball_Pop_up_BrassRing_Quote, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Start_01);
			if (!m_Heroic)
			{
				yield return PlayAndRemoveRandomLineOnlyOnce(friendlyActor, m_missionKhadgarFloatingHeadResponses);
			}
			break;
		case 525:
			yield return PlayLineAlways(Wisdomball_Pop_up_BrassRing_Quote, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Treasure_01);
			if (!m_Heroic)
			{
				yield return PlayAndRemoveRandomLineOnlyOnce(friendlyActor, m_missionKhadgarFloatingHeadResponses);
			}
			break;
		default:
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		}
	}

	protected override IEnumerator RespondToFriendlyPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToFriendlyPlayedCardWithTiming(entity);
		while (m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		if (m_playedLines.Contains(entity.GetCardId()) && entity.GetCardType() != TAG_CARDTYPE.HERO_POWER)
		{
			yield break;
		}
		yield return WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		m_playedLines.Add(cardId);
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		Actor actor2 = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (cardId)
		{
		case "ICC_078":
			yield return PlayLineOnlyOnce(actor, VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Player_Avalanche_01);
			break;
		case "DRG_068":
			yield return PlayLineOnlyOnce(actor, VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Player_LivingDragonbreath_01);
			break;
		case "KARA_00_08":
			if (!m_Heroic)
			{
				yield return PlayLineOnlyOnce(actor2, VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_ArchmageApprentice_01);
			}
			break;
		case "DAL_558":
			if (!m_Heroic)
			{
				yield return PlayLineOnlyOnce(actor2, VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_ArchmageVargoth_01);
			}
			break;
		case "KARA_00_07":
			if (!m_Heroic)
			{
				yield return PlayLineOnlyOnce(actor2, VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_AstralPortal_01);
			}
			break;
		case "DAL_609":
			if (!m_Heroic)
			{
				yield return PlayLineOnlyOnce(actor2, VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_Kalecgos_01);
			}
			break;
		case "KARA_00_05":
			if (!m_Heroic)
			{
				yield return PlayLineAlways(actor2, VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_Misc_04_01);
			}
			break;
		}
	}

	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		if (!m_playedLines.Contains(entity.GetCardId()) || entity.GetCardType() == TAG_CARDTYPE.HERO_POWER)
		{
			yield return base.RespondToPlayedCardWithTiming(entity);
			yield return WaitForEntitySoundToFinish(entity);
			string cardId = entity.GetCardId();
			m_playedLines.Add(cardId);
		}
	}
}
