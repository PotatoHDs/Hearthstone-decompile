using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DRGA_Evil_Fight_05 : DRGA_Dungeon
{
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_05_Turn_03_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_05_Turn_03_01.prefab:c8d6161eb39d39443b4e04f409f63ace");

	private static readonly AssetReference VO_DRGA_BOSS_27ha_Male_Dwarf_Evil_Fight_05_Boss_Death_01_01 = new AssetReference("VO_DRGA_BOSS_27ha_Male_Dwarf_Evil_Fight_05_Boss_Death_01_01.prefab:d3edee70b27ef54408e2f583dfd1caf9");

	private static readonly AssetReference VO_DRGA_BOSS_27ha_Male_Dwarf_Evil_Fight_05_Boss_HeroPower_01_01 = new AssetReference("VO_DRGA_BOSS_27ha_Male_Dwarf_Evil_Fight_05_Boss_HeroPower_01_01.prefab:b4821b4dee594e8439b7ddaf796e9ffe");

	private static readonly AssetReference VO_DRGA_BOSS_27ha_Male_Dwarf_Evil_Fight_05_BossAttack_01_01 = new AssetReference("VO_DRGA_BOSS_27ha_Male_Dwarf_Evil_Fight_05_BossAttack_01_01.prefab:7c349a914815fda498636e03968aa936");

	private static readonly AssetReference VO_DRGA_BOSS_27ha_Male_Dwarf_Evil_Fight_05_BossStart_01_01 = new AssetReference("VO_DRGA_BOSS_27ha_Male_Dwarf_Evil_Fight_05_BossStart_01_01.prefab:6d05e6848f8a64f4ab32ec9827fb5f8c");

	private static readonly AssetReference VO_DRGA_BOSS_27ha_Male_Dwarf_Evil_Fight_05_BossStartHeroic_01_01 = new AssetReference("VO_DRGA_BOSS_27ha_Male_Dwarf_Evil_Fight_05_BossStartHeroic_01_01.prefab:3438f2c7ebbea2348a158926ec35b0c5");

	private static readonly AssetReference VO_DRGA_BOSS_27ha_Male_Dwarf_Evil_Fight_05_EmoteResponse_01_01 = new AssetReference("VO_DRGA_BOSS_27ha_Male_Dwarf_Evil_Fight_05_EmoteResponse_01_01.prefab:755db052f14bf6f4c99eb4b068c05fc9");

	private static readonly AssetReference VO_DRGA_BOSS_27ha_Male_Dwarf_Evil_Fight_05_Idle_01_01 = new AssetReference("VO_DRGA_BOSS_27ha_Male_Dwarf_Evil_Fight_05_Idle_01_01.prefab:35b229bd11396ab4aa206ea3b4f50d96");

	private static readonly AssetReference VO_DRGA_BOSS_27ha_Male_Dwarf_Evil_Fight_05_Misc_01_01 = new AssetReference("VO_DRGA_BOSS_27ha_Male_Dwarf_Evil_Fight_05_Misc_01_01.prefab:21f98641b434c4842a86867c5569073d");

	private static readonly AssetReference VO_DRGA_BOSS_27hb_Female_NightElf_Evil_Fight_05_Boss_Death_02_01 = new AssetReference("VO_DRGA_BOSS_27hb_Female_NightElf_Evil_Fight_05_Boss_Death_02_01.prefab:ecc5728c307b8914b853f93921ce9ac6");

	private static readonly AssetReference VO_DRGA_BOSS_27hb_Female_NightElf_Evil_Fight_05_Boss_HeroPower_02_01 = new AssetReference("VO_DRGA_BOSS_27hb_Female_NightElf_Evil_Fight_05_Boss_HeroPower_02_01.prefab:39d4fef637c1f404a8d708341d77d398");

	private static readonly AssetReference VO_DRGA_BOSS_27hb_Female_NightElf_Evil_Fight_05_BossAttack_02_01 = new AssetReference("VO_DRGA_BOSS_27hb_Female_NightElf_Evil_Fight_05_BossAttack_02_01.prefab:ae4ab22150cd7cc47ab20b41756c354a");

	private static readonly AssetReference VO_DRGA_BOSS_27hb_Female_NightElf_Evil_Fight_05_BossStart_02_01 = new AssetReference("VO_DRGA_BOSS_27hb_Female_NightElf_Evil_Fight_05_BossStart_02_01.prefab:13679765a17e3da478c10bd6c877ab94");

	private static readonly AssetReference VO_DRGA_BOSS_27hb_Female_NightElf_Evil_Fight_05_BossStartHeroic_02_01 = new AssetReference("VO_DRGA_BOSS_27hb_Female_NightElf_Evil_Fight_05_BossStartHeroic_02_01.prefab:9e024b3bb535b8341a04ba5babcda4d7");

	private static readonly AssetReference VO_DRGA_BOSS_27hb_Female_NightElf_Evil_Fight_05_EmoteResponse_02_01 = new AssetReference("VO_DRGA_BOSS_27hb_Female_NightElf_Evil_Fight_05_EmoteResponse_02_01.prefab:7241af47027c0e44599d6c8a05ed80c1");

	private static readonly AssetReference VO_DRGA_BOSS_27hb_Female_NightElf_Evil_Fight_05_Idle_02_01 = new AssetReference("VO_DRGA_BOSS_27hb_Female_NightElf_Evil_Fight_05_Idle_02_01.prefab:6828b7cee0c8c49489b8309f8ecdeea0");

	private static readonly AssetReference VO_DRGA_BOSS_27hb_Female_NightElf_Evil_Fight_05_Misc_02_01 = new AssetReference("VO_DRGA_BOSS_27hb_Female_NightElf_Evil_Fight_05_Misc_02_01.prefab:52e409e0cba2bb749b92a06a39106dea");

	private static readonly AssetReference VO_DRGA_BOSS_27hc_Female_Human_Evil_Fight_05_Boss_Death_03_01 = new AssetReference("VO_DRGA_BOSS_27hc_Female_Human_Evil_Fight_05_Boss_Death_03_01.prefab:9155b3651c495f845a826d0814ae6857");

	private static readonly AssetReference VO_DRGA_BOSS_27hc_Female_Human_Evil_Fight_05_Boss_HeroPower_03_01 = new AssetReference("VO_DRGA_BOSS_27hc_Female_Human_Evil_Fight_05_Boss_HeroPower_03_01.prefab:56890e0c54f4f9647a11d2b381ac015d");

	private static readonly AssetReference VO_DRGA_BOSS_27hc_Female_Human_Evil_Fight_05_BossAttack_03_01 = new AssetReference("VO_DRGA_BOSS_27hc_Female_Human_Evil_Fight_05_BossAttack_03_01.prefab:c1621584e1977b244ac3bae1769d4bbb");

	private static readonly AssetReference VO_DRGA_BOSS_27hc_Female_Human_Evil_Fight_05_EmoteResponse_03_01 = new AssetReference("VO_DRGA_BOSS_27hc_Female_Human_Evil_Fight_05_EmoteResponse_03_01.prefab:0bc7f0b937072c846b072bdecde39a96");

	private static readonly AssetReference VO_DRGA_BOSS_27hc_Female_Human_Evil_Fight_05_Idle_03_01 = new AssetReference("VO_DRGA_BOSS_27hc_Female_Human_Evil_Fight_05_Idle_03_01.prefab:4d8e09af6f8403d418764ad44c8bb1e0");

	private static readonly AssetReference VO_DRGA_BOSS_27hc_Female_Human_Evil_Fight_05_Misc_03_01 = new AssetReference("VO_DRGA_BOSS_27hc_Female_Human_Evil_Fight_05_Misc_03_01.prefab:6e753897c88077242992c60441126620");

	private static readonly AssetReference VO_DRGA_BOSS_27hc_Female_Human_Evil_Fight_05_Misc_05_01 = new AssetReference("VO_DRGA_BOSS_27hc_Female_Human_Evil_Fight_05_Misc_05_01.prefab:0928fae526e8a214fa129f0866ab3022");

	private static readonly AssetReference VO_DRGA_BOSS_27hd_Male_Gnome_Evil_Fight_05_Boss_Death_04_01 = new AssetReference("VO_DRGA_BOSS_27hd_Male_Gnome_Evil_Fight_05_Boss_Death_04_01.prefab:35d6fd187c2613c409be58a5db67c0f0");

	private static readonly AssetReference VO_DRGA_BOSS_27hd_Male_Gnome_Evil_Fight_05_Boss_HeroPower_04_01 = new AssetReference("VO_DRGA_BOSS_27hd_Male_Gnome_Evil_Fight_05_Boss_HeroPower_04_01.prefab:8a2227b10408fb74fa8d8b9213a97ddc");

	private static readonly AssetReference VO_DRGA_BOSS_27hd_Male_Gnome_Evil_Fight_05_BossAttack_04_01 = new AssetReference("VO_DRGA_BOSS_27hd_Male_Gnome_Evil_Fight_05_BossAttack_04_01.prefab:5f1caf82ee6af944d91aafd8e99be0a4");

	private static readonly AssetReference VO_DRGA_BOSS_27hd_Male_Gnome_Evil_Fight_05_EmoteResponse_04_01 = new AssetReference("VO_DRGA_BOSS_27hd_Male_Gnome_Evil_Fight_05_EmoteResponse_04_01.prefab:59b4e16350a790d47ae92cf59cf12bcd");

	private static readonly AssetReference VO_DRGA_BOSS_27hd_Male_Gnome_Evil_Fight_05_Idle_04_01 = new AssetReference("VO_DRGA_BOSS_27hd_Male_Gnome_Evil_Fight_05_Idle_04_01.prefab:f81e9686a73ec8f41a1170061b8608d5");

	private static readonly AssetReference VO_DRGA_BOSS_27hd_Male_Gnome_Evil_Fight_05_Misc_04_01 = new AssetReference("VO_DRGA_BOSS_27hd_Male_Gnome_Evil_Fight_05_Misc_04_01.prefab:90c4ad759d620ab4ba76d91cbddcc304");

	private static readonly AssetReference VO_DRGA_BOSS_27hd_Male_Gnome_Evil_Fight_05_Misc_06_01 = new AssetReference("VO_DRGA_BOSS_27hd_Male_Gnome_Evil_Fight_05_Misc_06_01.prefab:ec474c4bdd8b9b244983c4afdd665ed0");

	private static readonly AssetReference VO_DRGA_BOSS_28h_Male_Tauren_Evil_Fight_05_Misc_01_01 = new AssetReference("VO_DRGA_BOSS_28h_Male_Tauren_Evil_Fight_05_Misc_01_01.prefab:6b9e049b82a6edd409bf01c4ff21fc70");

	private static readonly AssetReference VO_DRGA_BOSS_28h_Male_Tauren_Evil_Fight_05_Misc_02_01 = new AssetReference("VO_DRGA_BOSS_28h_Male_Tauren_Evil_Fight_05_Misc_02_01.prefab:c9c164090d0e08a4e894069fdd225339");

	private static readonly AssetReference VO_DRGA_BOSS_28h_Male_Tauren_Evil_Fight_05_Misc_03_01 = new AssetReference("VO_DRGA_BOSS_28h_Male_Tauren_Evil_Fight_05_Misc_03_01.prefab:31bec908ca45f7741ad178c2e7e39a6f");

	private static readonly AssetReference VO_DRGA_BOSS_28h_Male_Tauren_Evil_Fight_05_Misc_04_01 = new AssetReference("VO_DRGA_BOSS_28h_Male_Tauren_Evil_Fight_05_Misc_04_01.prefab:4b983edb3a288db4b9cca2aad5566e5e");

	private static readonly AssetReference VO_DRGA_BOSS_28h_Male_Tauren_Evil_Fight_05_Misc_05_01 = new AssetReference("VO_DRGA_BOSS_28h_Male_Tauren_Evil_Fight_05_Misc_05_01.prefab:8e2b0e2f04fe6604f97f40733974d945");

	private static readonly AssetReference VO_DRGA_BOSS_28h_Male_Tauren_Evil_Fight_05_Misc_06_01 = new AssetReference("VO_DRGA_BOSS_28h_Male_Tauren_Evil_Fight_05_Misc_06_01.prefab:0a952a233bfcbe640afe9cd7a3908152");

	private static readonly AssetReference VO_DRGA_BOSS_28h_Male_Tauren_Evil_Fight_05_PlayerStart_01 = new AssetReference("VO_DRGA_BOSS_28h_Male_Tauren_Evil_Fight_05_PlayerStart_01.prefab:48f3e06e887eeee46843b3b6a198a165");

	private List<string> m_VO_DRGA_BOSS_Combined_HeroPowerLines = new List<string> { VO_DRGA_BOSS_27ha_Male_Dwarf_Evil_Fight_05_Boss_HeroPower_01_01, VO_DRGA_BOSS_27hb_Female_NightElf_Evil_Fight_05_Boss_HeroPower_02_01, VO_DRGA_BOSS_27hc_Female_Human_Evil_Fight_05_Boss_HeroPower_03_01, VO_DRGA_BOSS_27hd_Male_Gnome_Evil_Fight_05_Boss_HeroPower_04_01 };

	private List<string> m_VO_DRGA_BOSS_Combined_IdleLines = new List<string> { VO_DRGA_BOSS_27ha_Male_Dwarf_Evil_Fight_05_Idle_01_01, VO_DRGA_BOSS_27hb_Female_NightElf_Evil_Fight_05_Idle_02_01, VO_DRGA_BOSS_27hc_Female_Human_Evil_Fight_05_Idle_03_01, VO_DRGA_BOSS_27hd_Male_Gnome_Evil_Fight_05_Idle_04_01 };

	private List<string> m_VO_DRGA_BOSS_Combined_Attack = new List<string> { VO_DRGA_BOSS_27ha_Male_Dwarf_Evil_Fight_05_BossAttack_01_01, VO_DRGA_BOSS_27hb_Female_NightElf_Evil_Fight_05_BossAttack_02_01, VO_DRGA_BOSS_27hc_Female_Human_Evil_Fight_05_BossAttack_03_01, VO_DRGA_BOSS_27hd_Male_Gnome_Evil_Fight_05_BossAttack_04_01 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_05_Turn_03_01, VO_DRGA_BOSS_27ha_Male_Dwarf_Evil_Fight_05_Boss_Death_01_01, VO_DRGA_BOSS_27ha_Male_Dwarf_Evil_Fight_05_Boss_HeroPower_01_01, VO_DRGA_BOSS_27ha_Male_Dwarf_Evil_Fight_05_BossAttack_01_01, VO_DRGA_BOSS_27ha_Male_Dwarf_Evil_Fight_05_BossStart_01_01, VO_DRGA_BOSS_27ha_Male_Dwarf_Evil_Fight_05_BossStartHeroic_01_01, VO_DRGA_BOSS_27ha_Male_Dwarf_Evil_Fight_05_EmoteResponse_01_01, VO_DRGA_BOSS_27ha_Male_Dwarf_Evil_Fight_05_Idle_01_01, VO_DRGA_BOSS_27ha_Male_Dwarf_Evil_Fight_05_Misc_01_01, VO_DRGA_BOSS_27hb_Female_NightElf_Evil_Fight_05_Boss_Death_02_01,
			VO_DRGA_BOSS_27hb_Female_NightElf_Evil_Fight_05_Boss_HeroPower_02_01, VO_DRGA_BOSS_27hb_Female_NightElf_Evil_Fight_05_BossAttack_02_01, VO_DRGA_BOSS_27hb_Female_NightElf_Evil_Fight_05_BossStart_02_01, VO_DRGA_BOSS_27hb_Female_NightElf_Evil_Fight_05_BossStartHeroic_02_01, VO_DRGA_BOSS_27hb_Female_NightElf_Evil_Fight_05_EmoteResponse_02_01, VO_DRGA_BOSS_27hb_Female_NightElf_Evil_Fight_05_Idle_02_01, VO_DRGA_BOSS_27hb_Female_NightElf_Evil_Fight_05_Misc_02_01, VO_DRGA_BOSS_27hc_Female_Human_Evil_Fight_05_Boss_Death_03_01, VO_DRGA_BOSS_27hc_Female_Human_Evil_Fight_05_Boss_HeroPower_03_01, VO_DRGA_BOSS_27hc_Female_Human_Evil_Fight_05_BossAttack_03_01,
			VO_DRGA_BOSS_27hc_Female_Human_Evil_Fight_05_EmoteResponse_03_01, VO_DRGA_BOSS_27hc_Female_Human_Evil_Fight_05_Idle_03_01, VO_DRGA_BOSS_27hc_Female_Human_Evil_Fight_05_Misc_03_01, VO_DRGA_BOSS_27hc_Female_Human_Evil_Fight_05_Misc_05_01, VO_DRGA_BOSS_27hd_Male_Gnome_Evil_Fight_05_Boss_Death_04_01, VO_DRGA_BOSS_27hd_Male_Gnome_Evil_Fight_05_Boss_HeroPower_04_01, VO_DRGA_BOSS_27hd_Male_Gnome_Evil_Fight_05_BossAttack_04_01, VO_DRGA_BOSS_27hd_Male_Gnome_Evil_Fight_05_EmoteResponse_04_01, VO_DRGA_BOSS_27hd_Male_Gnome_Evil_Fight_05_Idle_04_01, VO_DRGA_BOSS_27hd_Male_Gnome_Evil_Fight_05_Misc_04_01,
			VO_DRGA_BOSS_27hd_Male_Gnome_Evil_Fight_05_Misc_06_01, VO_DRGA_BOSS_28h_Male_Tauren_Evil_Fight_05_Misc_01_01, VO_DRGA_BOSS_28h_Male_Tauren_Evil_Fight_05_Misc_02_01, VO_DRGA_BOSS_28h_Male_Tauren_Evil_Fight_05_Misc_03_01, VO_DRGA_BOSS_28h_Male_Tauren_Evil_Fight_05_Misc_04_01, VO_DRGA_BOSS_28h_Male_Tauren_Evil_Fight_05_Misc_05_01, VO_DRGA_BOSS_28h_Male_Tauren_Evil_Fight_05_Misc_06_01, VO_DRGA_BOSS_28h_Male_Tauren_Evil_Fight_05_PlayerStart_01
		};
		SetBossVOLines(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	public override List<string> GetIdleLines()
	{
		return m_VO_DRGA_BOSS_Combined_IdleLines;
	}

	public override List<string> GetBossHeroPowerRandomLines()
	{
		return m_VO_DRGA_BOSS_Combined_HeroPowerLines;
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		int num = Random.Range(1, 5);
		if (num == 1)
		{
			m_deathLine = VO_DRGA_BOSS_27ha_Male_Dwarf_Evil_Fight_05_Boss_Death_01_01;
		}
		if (num == 2)
		{
			m_deathLine = VO_DRGA_BOSS_27hb_Female_NightElf_Evil_Fight_05_Boss_Death_02_01;
		}
		if (num == 3)
		{
			m_deathLine = VO_DRGA_BOSS_27hc_Female_Human_Evil_Fight_05_Boss_Death_03_01;
		}
		if (num == 4)
		{
			m_deathLine = VO_DRGA_BOSS_27hd_Male_Gnome_Evil_Fight_05_Boss_Death_04_01;
		}
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		int num = Random.Range(1, 5);
		if (emoteType == EmoteType.START)
		{
			if (num < 3)
			{
				if (!m_Heroic)
				{
					Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DRGA_BOSS_27ha_Male_Dwarf_Evil_Fight_05_BossStart_01_01, Notification.SpeechBubbleDirection.TopRight, actor));
				}
				else if (m_Heroic)
				{
					Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DRGA_BOSS_27ha_Male_Dwarf_Evil_Fight_05_BossStartHeroic_01_01, Notification.SpeechBubbleDirection.TopRight, actor));
				}
			}
			else if (!m_Heroic)
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DRGA_BOSS_27hb_Female_NightElf_Evil_Fight_05_BossStart_02_01, Notification.SpeechBubbleDirection.TopRight, actor));
			}
			else if (m_Heroic)
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DRGA_BOSS_27hb_Female_NightElf_Evil_Fight_05_BossStartHeroic_02_01, Notification.SpeechBubbleDirection.TopRight, actor));
			}
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			switch (num)
			{
			case 1:
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DRGA_BOSS_27ha_Male_Dwarf_Evil_Fight_05_EmoteResponse_01_01, Notification.SpeechBubbleDirection.TopRight, actor));
				break;
			case 2:
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DRGA_BOSS_27hb_Female_NightElf_Evil_Fight_05_EmoteResponse_02_01, Notification.SpeechBubbleDirection.TopRight, actor));
				break;
			case 3:
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DRGA_BOSS_27hc_Female_Human_Evil_Fight_05_EmoteResponse_03_01, Notification.SpeechBubbleDirection.TopRight, actor));
				break;
			case 4:
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DRGA_BOSS_27hd_Male_Gnome_Evil_Fight_05_EmoteResponse_04_01, Notification.SpeechBubbleDirection.TopRight, actor));
				break;
			}
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
		Actor actor2 = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (missionEvent)
		{
		case 100:
			yield return PlayLineAlways(actor, VO_DRGA_BOSS_27ha_Male_Dwarf_Evil_Fight_05_Misc_01_01);
			break;
		case 101:
			yield return PlayLineAlways(actor, VO_DRGA_BOSS_27hb_Female_NightElf_Evil_Fight_05_Misc_02_01);
			break;
		case 102:
			yield return PlayLineAlways(actor, VO_DRGA_BOSS_27hc_Female_Human_Evil_Fight_05_Misc_03_01);
			break;
		case 103:
			yield return PlayLineAlways(actor, VO_DRGA_BOSS_27hc_Female_Human_Evil_Fight_05_Misc_05_01);
			break;
		case 104:
			yield return PlayLineAlways(actor, VO_DRGA_BOSS_27hd_Male_Gnome_Evil_Fight_05_Misc_04_01);
			break;
		case 105:
			yield return PlayLineAlways(actor, VO_DRGA_BOSS_27hd_Male_Gnome_Evil_Fight_05_Misc_06_01);
			break;
		case 107:
			if (!m_Heroic)
			{
				yield return PlayLineAlways(DRGA_Dungeon.RafaamBrassRing, VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_05_Turn_03_01);
			}
			break;
		case 108:
			if (!m_Heroic)
			{
				yield return PlayLineAlways(actor2, VO_DRGA_BOSS_28h_Male_Tauren_Evil_Fight_05_Misc_01_01);
			}
			break;
		case 109:
			if (!m_Heroic)
			{
				yield return PlayLineAlways(actor2, VO_DRGA_BOSS_28h_Male_Tauren_Evil_Fight_05_Misc_02_01);
			}
			break;
		case 110:
			if (!m_Heroic)
			{
				yield return PlayLineAlways(actor2, VO_DRGA_BOSS_28h_Male_Tauren_Evil_Fight_05_Misc_03_01);
			}
			break;
		case 111:
			if (!m_Heroic)
			{
				yield return PlayLineAlways(actor2, VO_DRGA_BOSS_28h_Male_Tauren_Evil_Fight_05_Misc_04_01);
			}
			break;
		case 112:
			if (!m_Heroic)
			{
				yield return PlayLineAlways(actor2, VO_DRGA_BOSS_28h_Male_Tauren_Evil_Fight_05_Misc_05_01);
			}
			break;
		case 113:
			if (!m_Heroic)
			{
				yield return PlayLineAlways(actor2, VO_DRGA_BOSS_28h_Male_Tauren_Evil_Fight_05_Misc_06_01);
			}
			break;
		case 114:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_VO_DRGA_BOSS_Combined_Attack);
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
		if (!m_playedLines.Contains(entity.GetCardId()) || entity.GetCardType() == TAG_CARDTYPE.HERO_POWER)
		{
			yield return WaitForEntitySoundToFinish(entity);
			string cardId = entity.GetCardId();
			m_playedLines.Add(cardId);
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
			Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			if (cardId == "DRGA_BOSS_27t")
			{
				yield return PlayLineOnlyOnce(enemyActor, VO_DRGA_BOSS_27hc_Female_Human_Evil_Fight_05_Misc_05_01);
				yield return PlayLineOnlyOnce(enemyActor, VO_DRGA_BOSS_27hd_Male_Gnome_Evil_Fight_05_Misc_06_01);
			}
		}
	}
}
