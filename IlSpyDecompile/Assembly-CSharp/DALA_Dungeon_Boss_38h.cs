using System.Collections;
using System.Collections.Generic;

public class DALA_Dungeon_Boss_38h : DALA_Dungeon
{
	private static readonly AssetReference VO_DALA_BOSS_38h_Female_Gnome_BossPortal_01 = new AssetReference("VO_DALA_BOSS_38h_Female_Gnome_BossPortal_01.prefab:5b826b60e67152b4885635c113d05b4c");

	private static readonly AssetReference VO_DALA_BOSS_38h_Female_Gnome_BossPortal_02 = new AssetReference("VO_DALA_BOSS_38h_Female_Gnome_BossPortal_02.prefab:e39f74d9a0036024e9d01860b69a79c5");

	private static readonly AssetReference VO_DALA_BOSS_38h_Female_Gnome_BossPortal_03 = new AssetReference("VO_DALA_BOSS_38h_Female_Gnome_BossPortal_03.prefab:4d566e2c93f4c9a45862f379911165d6");

	private static readonly AssetReference VO_DALA_BOSS_38h_Female_Gnome_BossPortal_04 = new AssetReference("VO_DALA_BOSS_38h_Female_Gnome_BossPortal_04.prefab:d0444d1589adbdb41b7da76fa85fcc26");

	private static readonly AssetReference VO_DALA_BOSS_38h_Female_Gnome_BossPortal_05 = new AssetReference("VO_DALA_BOSS_38h_Female_Gnome_BossPortal_05.prefab:9e86f21294ea754469acb5e3c043fb08");

	private static readonly AssetReference VO_DALA_BOSS_38h_Female_Gnome_BossPortal_06 = new AssetReference("VO_DALA_BOSS_38h_Female_Gnome_BossPortal_06.prefab:c940ade1ed7b524449c124a1e87e99bb");

	private static readonly AssetReference VO_DALA_BOSS_38h_Female_Gnome_Death_01 = new AssetReference("VO_DALA_BOSS_38h_Female_Gnome_Death_01.prefab:e8650f9cf9569ef4eaf429f7a60bc578");

	private static readonly AssetReference VO_DALA_BOSS_38h_Female_Gnome_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_38h_Female_Gnome_DefeatPlayer_01.prefab:933653975b69d2b438891b976067f5d4");

	private static readonly AssetReference VO_DALA_BOSS_38h_Female_Gnome_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_38h_Female_Gnome_EmoteResponse_01.prefab:e6297f146486096408c1e2381b634a5e");

	private static readonly AssetReference VO_DALA_BOSS_38h_Female_Gnome_HeroPower_02 = new AssetReference("VO_DALA_BOSS_38h_Female_Gnome_HeroPower_02.prefab:c58df3a37ad4e154cac7d1562c12caca");

	private static readonly AssetReference VO_DALA_BOSS_38h_Female_Gnome_HeroPower_03 = new AssetReference("VO_DALA_BOSS_38h_Female_Gnome_HeroPower_03.prefab:bf66eea2f3e114b4784b0a435b9f966a");

	private static readonly AssetReference VO_DALA_BOSS_38h_Female_Gnome_HeroPower_04 = new AssetReference("VO_DALA_BOSS_38h_Female_Gnome_HeroPower_04.prefab:c883ef4410295824e9925ae8a6b2cc8e");

	private static readonly AssetReference VO_DALA_BOSS_38h_Female_Gnome_HeroPower_05 = new AssetReference("VO_DALA_BOSS_38h_Female_Gnome_HeroPower_05.prefab:140641b94f9b2b84f947ad6b216f29ed");

	private static readonly AssetReference VO_DALA_BOSS_38h_Female_Gnome_HeroPower_06 = new AssetReference("VO_DALA_BOSS_38h_Female_Gnome_HeroPower_06.prefab:b69d3d582b850974ab91915d390de1b1");

	private static readonly AssetReference VO_DALA_BOSS_38h_Female_Gnome_HeroPowerPlayFirelandsPortal_01 = new AssetReference("VO_DALA_BOSS_38h_Female_Gnome_HeroPowerPlayFirelandsPortal_01.prefab:8d130e514087e0d499d469db1392bf91");

	private static readonly AssetReference VO_DALA_BOSS_38h_Female_Gnome_Idle_01 = new AssetReference("VO_DALA_BOSS_38h_Female_Gnome_Idle_01.prefab:c3e13e097b1b6d948ba2bf3d20f594dd");

	private static readonly AssetReference VO_DALA_BOSS_38h_Female_Gnome_Idle_02 = new AssetReference("VO_DALA_BOSS_38h_Female_Gnome_Idle_02.prefab:b569d23be124c0f4586891fd62f4a7bc");

	private static readonly AssetReference VO_DALA_BOSS_38h_Female_Gnome_Idle_03 = new AssetReference("VO_DALA_BOSS_38h_Female_Gnome_Idle_03.prefab:56a6d2785a3acab44ac6ae7e58a2a5ba");

	private static readonly AssetReference VO_DALA_BOSS_38h_Female_Gnome_Intro_01 = new AssetReference("VO_DALA_BOSS_38h_Female_Gnome_Intro_01.prefab:7edcdc4307049924a955327b72093264");

	private static readonly AssetReference VO_DALA_BOSS_38h_Female_Gnome_PlayerGatherYourParty_01 = new AssetReference("VO_DALA_BOSS_38h_Female_Gnome_PlayerGatherYourParty_01.prefab:e106e003d3d797e42a83a25e9f900f91");

	private static readonly AssetReference VO_DALA_BOSS_38h_Female_Gnome_PlayerPortal_02 = new AssetReference("VO_DALA_BOSS_38h_Female_Gnome_PlayerPortal_02.prefab:6712c015107aa9d4c93f0b7db0f7cff7");

	private static readonly AssetReference VO_DALA_BOSS_38h_Female_Gnome_PlayerSanguineReveler_01 = new AssetReference("VO_DALA_BOSS_38h_Female_Gnome_PlayerSanguineReveler_01.prefab:7c03cf81502beab40b9072c7d891e6a9");

	private static readonly AssetReference VO_DALA_BOSS_38h_Female_Gnome_PlayerTwistingNether_01 = new AssetReference("VO_DALA_BOSS_38h_Female_Gnome_PlayerTwistingNether_01.prefab:3358cc7d1df59444ab2ef4f1e62a820e");

	private HashSet<string> m_playedLines = new HashSet<string>();

	private static List<string> m_BossPortal = new List<string> { VO_DALA_BOSS_38h_Female_Gnome_BossPortal_01, VO_DALA_BOSS_38h_Female_Gnome_BossPortal_02, VO_DALA_BOSS_38h_Female_Gnome_BossPortal_03, VO_DALA_BOSS_38h_Female_Gnome_BossPortal_04, VO_DALA_BOSS_38h_Female_Gnome_BossPortal_05, VO_DALA_BOSS_38h_Female_Gnome_BossPortal_06 };

	private static List<string> m_IdleLines = new List<string> { VO_DALA_BOSS_38h_Female_Gnome_Idle_01, VO_DALA_BOSS_38h_Female_Gnome_Idle_02, VO_DALA_BOSS_38h_Female_Gnome_Idle_03 };

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_DALA_BOSS_38h_Female_Gnome_BossPortal_01, VO_DALA_BOSS_38h_Female_Gnome_BossPortal_02, VO_DALA_BOSS_38h_Female_Gnome_BossPortal_03, VO_DALA_BOSS_38h_Female_Gnome_BossPortal_04, VO_DALA_BOSS_38h_Female_Gnome_BossPortal_05, VO_DALA_BOSS_38h_Female_Gnome_BossPortal_06, VO_DALA_BOSS_38h_Female_Gnome_Death_01, VO_DALA_BOSS_38h_Female_Gnome_DefeatPlayer_01, VO_DALA_BOSS_38h_Female_Gnome_EmoteResponse_01, VO_DALA_BOSS_38h_Female_Gnome_HeroPower_02,
			VO_DALA_BOSS_38h_Female_Gnome_HeroPower_03, VO_DALA_BOSS_38h_Female_Gnome_HeroPower_04, VO_DALA_BOSS_38h_Female_Gnome_HeroPower_05, VO_DALA_BOSS_38h_Female_Gnome_HeroPower_06, VO_DALA_BOSS_38h_Female_Gnome_HeroPowerPlayFirelandsPortal_01, VO_DALA_BOSS_38h_Female_Gnome_Idle_01, VO_DALA_BOSS_38h_Female_Gnome_Idle_02, VO_DALA_BOSS_38h_Female_Gnome_Idle_03, VO_DALA_BOSS_38h_Female_Gnome_Intro_01, VO_DALA_BOSS_38h_Female_Gnome_PlayerGatherYourParty_01,
			VO_DALA_BOSS_38h_Female_Gnome_PlayerPortal_02, VO_DALA_BOSS_38h_Female_Gnome_PlayerSanguineReveler_01, VO_DALA_BOSS_38h_Female_Gnome_PlayerTwistingNether_01
		};
		SetBossVOLines(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_introLine = VO_DALA_BOSS_38h_Female_Gnome_Intro_01;
		m_deathLine = VO_DALA_BOSS_38h_Female_Gnome_Death_01;
		m_standardEmoteResponseLine = VO_DALA_BOSS_38h_Female_Gnome_EmoteResponse_01;
	}

	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string> { VO_DALA_BOSS_38h_Female_Gnome_HeroPower_02, VO_DALA_BOSS_38h_Female_Gnome_HeroPower_03, VO_DALA_BOSS_38h_Female_Gnome_HeroPower_04, VO_DALA_BOSS_38h_Female_Gnome_HeroPower_05, VO_DALA_BOSS_38h_Female_Gnome_HeroPower_06 };
	}

	public override List<string> GetIdleLines()
	{
		return m_IdleLines;
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		_ = missionEvent;
		yield return base.HandleMissionEventWithTiming(missionEvent);
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
			Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			yield return WaitForEntitySoundToFinish(entity);
			string cardId = entity.GetCardId();
			m_playedLines.Add(cardId);
			switch (cardId)
			{
			case "LOOT_370":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_38h_Female_Gnome_PlayerGatherYourParty_01);
				break;
			case "ICC_903":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_38h_Female_Gnome_PlayerSanguineReveler_01);
				break;
			case "EX1_312":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_38h_Female_Gnome_PlayerTwistingNether_01);
				break;
			case "GVG_003":
			case "KAR_077":
			case "DALA_BOSS_38t":
			case "KAR_075":
			case "KAR_073":
			case "KAR_091":
			case "KAR_076":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_38h_Female_Gnome_PlayerPortal_02);
				break;
			}
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
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			switch (cardId)
			{
			case "GVG_003":
			case "KAR_077":
			case "DALA_BOSS_38t":
			case "KAR_075":
			case "KAR_073":
			case "KAR_091":
				yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_BossPortal);
				break;
			case "KAR_076":
				yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_38h_Female_Gnome_HeroPowerPlayFirelandsPortal_01);
				break;
			}
		}
	}
}
