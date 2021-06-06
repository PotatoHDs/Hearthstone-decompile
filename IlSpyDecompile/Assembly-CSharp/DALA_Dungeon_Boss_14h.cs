using System.Collections;
using System.Collections.Generic;

public class DALA_Dungeon_Boss_14h : DALA_Dungeon
{
	private static readonly AssetReference VO_DALA_BOSS_14h_Female_KulTiran_BossBrawl_01 = new AssetReference("VO_DALA_BOSS_14h_Female_KulTiran_BossBrawl_01.prefab:0387c38aaa4828946bf1afe52d5b57d7");

	private static readonly AssetReference VO_DALA_BOSS_14h_Female_KulTiran_BossBrawl_02 = new AssetReference("VO_DALA_BOSS_14h_Female_KulTiran_BossBrawl_02.prefab:1055c46b195ef0b429b98c7f148ca354");

	private static readonly AssetReference VO_DALA_BOSS_14h_Female_KulTiran_BossMinions_01 = new AssetReference("VO_DALA_BOSS_14h_Female_KulTiran_BossMinions_01.prefab:3529c6612f600d945ba2a63b20cfac30");

	private static readonly AssetReference VO_DALA_BOSS_14h_Female_KulTiran_BossMinions_02 = new AssetReference("VO_DALA_BOSS_14h_Female_KulTiran_BossMinions_02.prefab:c559926c12ee89a468ff1f560e6c7357");

	private static readonly AssetReference VO_DALA_BOSS_14h_Female_KulTiran_BossMinions_03 = new AssetReference("VO_DALA_BOSS_14h_Female_KulTiran_BossMinions_03.prefab:2130ce377fd22474aa87e5f7bd86034b");

	private static readonly AssetReference VO_DALA_BOSS_14h_Female_KulTiran_BossMinions_04 = new AssetReference("VO_DALA_BOSS_14h_Female_KulTiran_BossMinions_04.prefab:e20958b1ba708404fb103bbc82d682e4");

	private static readonly AssetReference VO_DALA_BOSS_14h_Female_KulTiran_Death_01 = new AssetReference("VO_DALA_BOSS_14h_Female_KulTiran_Death_01.prefab:7e25486f6ed6630468c87ca07757b98c");

	private static readonly AssetReference VO_DALA_BOSS_14h_Female_KulTiran_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_14h_Female_KulTiran_DefeatPlayer_01.prefab:ba15377f29b7aed44922214b13a53cb0");

	private static readonly AssetReference VO_DALA_BOSS_14h_Female_KulTiran_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_14h_Female_KulTiran_EmoteResponse_01.prefab:0c5757ee092f237499e93532dabb4862");

	private static readonly AssetReference VO_DALA_BOSS_14h_Female_KulTiran_HeroPower_01 = new AssetReference("VO_DALA_BOSS_14h_Female_KulTiran_HeroPower_01.prefab:9616a94095992594ab4e26cb130a84bb");

	private static readonly AssetReference VO_DALA_BOSS_14h_Female_KulTiran_HeroPower_02 = new AssetReference("VO_DALA_BOSS_14h_Female_KulTiran_HeroPower_02.prefab:33f12ace376cdd4439784dbf89c15372");

	private static readonly AssetReference VO_DALA_BOSS_14h_Female_KulTiran_HeroPower_03 = new AssetReference("VO_DALA_BOSS_14h_Female_KulTiran_HeroPower_03.prefab:37a0706354e36fe49966a1efc4f06034");

	private static readonly AssetReference VO_DALA_BOSS_14h_Female_KulTiran_HeroPower_04 = new AssetReference("VO_DALA_BOSS_14h_Female_KulTiran_HeroPower_04.prefab:085da2b55d007914aa390c165073bcd9");

	private static readonly AssetReference VO_DALA_BOSS_14h_Female_KulTiran_Idle_01 = new AssetReference("VO_DALA_BOSS_14h_Female_KulTiran_Idle_01.prefab:7741e3f93c779f04fa7f6b0492330c37");

	private static readonly AssetReference VO_DALA_BOSS_14h_Female_KulTiran_Idle_02 = new AssetReference("VO_DALA_BOSS_14h_Female_KulTiran_Idle_02.prefab:dad14c9ae939c1648970bfb583a481cc");

	private static readonly AssetReference VO_DALA_BOSS_14h_Female_KulTiran_Idle_03 = new AssetReference("VO_DALA_BOSS_14h_Female_KulTiran_Idle_03.prefab:a10f0db716b36914e80189de9837b05c");

	private static readonly AssetReference VO_DALA_BOSS_14h_Female_KulTiran_Intro_01 = new AssetReference("VO_DALA_BOSS_14h_Female_KulTiran_Intro_01.prefab:e4c01fcf044ade142af03ede6bdc6aa5");

	private static readonly AssetReference VO_DALA_BOSS_14h_Female_KulTiran_IntroChu_01 = new AssetReference("VO_DALA_BOSS_14h_Female_KulTiran_IntroChu_01.prefab:761a30e5ac47dce4ebdb58bc89d5aa97");

	private static readonly AssetReference VO_DALA_BOSS_14h_Female_KulTiran_IntroGeorge_01 = new AssetReference("VO_DALA_BOSS_14h_Female_KulTiran_IntroGeorge_01.prefab:a9a436580dab9f1428fc79e0cd301149");

	private static readonly AssetReference VO_DALA_BOSS_14h_Female_KulTiran_IntroOlBarkeye_01 = new AssetReference("VO_DALA_BOSS_14h_Female_KulTiran_IntroOlBarkeye_01.prefab:cbcaeacaba94312429e33d6daa659829");

	private static readonly AssetReference VO_DALA_BOSS_14h_Female_KulTiran_PlayerBarista_01 = new AssetReference("VO_DALA_BOSS_14h_Female_KulTiran_PlayerBarista_01.prefab:6d2e50276607aa34db9c2a5a7a2641fa");

	private static readonly AssetReference VO_DALA_BOSS_14h_Female_KulTiran_PlayerBelligerentGnome_01 = new AssetReference("VO_DALA_BOSS_14h_Female_KulTiran_PlayerBelligerentGnome_01.prefab:296063de5dab691408cf8659b9fac8b7");

	private static readonly AssetReference VO_DALA_BOSS_14h_Female_KulTiran_PlayerBrawl_01 = new AssetReference("VO_DALA_BOSS_14h_Female_KulTiran_PlayerBrawl_01.prefab:aba174b9c03c2b542859aa3902ffb5c0");

	private static readonly AssetReference VO_DALA_BOSS_14h_Female_KulTiran_PlayerBrawl_02 = new AssetReference("VO_DALA_BOSS_14h_Female_KulTiran_PlayerBrawl_02.prefab:37273bbda399c7e4696b3eade4e5df6d");

	private static readonly AssetReference VO_DALA_BOSS_14h_Female_KulTiran_PlayerFriendlyBartender_01 = new AssetReference("VO_DALA_BOSS_14h_Female_KulTiran_PlayerFriendlyBartender_01.prefab:baaa7281105b85f499284e3513b3e834");

	private static readonly AssetReference VO_DALA_BOSS_14h_Female_KulTiran_PlayerInnkeeper_01 = new AssetReference("VO_DALA_BOSS_14h_Female_KulTiran_PlayerInnkeeper_01.prefab:9d19a40eedb9dac47ae55a62077a3e73");

	private static List<string> m_IdleLines = new List<string> { VO_DALA_BOSS_14h_Female_KulTiran_Idle_01, VO_DALA_BOSS_14h_Female_KulTiran_Idle_02, VO_DALA_BOSS_14h_Female_KulTiran_Idle_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	private static List<string> m_PlayerBrawl = new List<string> { VO_DALA_BOSS_14h_Female_KulTiran_Idle_01, VO_DALA_BOSS_14h_Female_KulTiran_Idle_02, VO_DALA_BOSS_14h_Female_KulTiran_Idle_03 };

	private static List<string> m_BossMinions = new List<string> { VO_DALA_BOSS_14h_Female_KulTiran_BossMinions_01, VO_DALA_BOSS_14h_Female_KulTiran_BossMinions_02, VO_DALA_BOSS_14h_Female_KulTiran_BossMinions_03, VO_DALA_BOSS_14h_Female_KulTiran_BossMinions_04 };

	private static List<string> m_BossBrawl = new List<string> { VO_DALA_BOSS_14h_Female_KulTiran_BossBrawl_01, VO_DALA_BOSS_14h_Female_KulTiran_BossBrawl_02 };

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_DALA_BOSS_14h_Female_KulTiran_BossBrawl_01, VO_DALA_BOSS_14h_Female_KulTiran_BossBrawl_02, VO_DALA_BOSS_14h_Female_KulTiran_BossMinions_01, VO_DALA_BOSS_14h_Female_KulTiran_BossMinions_02, VO_DALA_BOSS_14h_Female_KulTiran_BossMinions_03, VO_DALA_BOSS_14h_Female_KulTiran_BossMinions_04, VO_DALA_BOSS_14h_Female_KulTiran_Death_01, VO_DALA_BOSS_14h_Female_KulTiran_DefeatPlayer_01, VO_DALA_BOSS_14h_Female_KulTiran_EmoteResponse_01, VO_DALA_BOSS_14h_Female_KulTiran_HeroPower_01,
			VO_DALA_BOSS_14h_Female_KulTiran_HeroPower_02, VO_DALA_BOSS_14h_Female_KulTiran_HeroPower_03, VO_DALA_BOSS_14h_Female_KulTiran_HeroPower_04, VO_DALA_BOSS_14h_Female_KulTiran_Idle_01, VO_DALA_BOSS_14h_Female_KulTiran_Idle_02, VO_DALA_BOSS_14h_Female_KulTiran_Idle_03, VO_DALA_BOSS_14h_Female_KulTiran_Intro_01, VO_DALA_BOSS_14h_Female_KulTiran_IntroChu_01, VO_DALA_BOSS_14h_Female_KulTiran_IntroGeorge_01, VO_DALA_BOSS_14h_Female_KulTiran_IntroOlBarkeye_01,
			VO_DALA_BOSS_14h_Female_KulTiran_PlayerBarista_01, VO_DALA_BOSS_14h_Female_KulTiran_PlayerBelligerentGnome_01, VO_DALA_BOSS_14h_Female_KulTiran_PlayerBrawl_01, VO_DALA_BOSS_14h_Female_KulTiran_PlayerBrawl_02, VO_DALA_BOSS_14h_Female_KulTiran_PlayerFriendlyBartender_01, VO_DALA_BOSS_14h_Female_KulTiran_PlayerInnkeeper_01
		};
		SetBossVOLines(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string> { VO_DALA_BOSS_14h_Female_KulTiran_HeroPower_01, VO_DALA_BOSS_14h_Female_KulTiran_HeroPower_02, VO_DALA_BOSS_14h_Female_KulTiran_HeroPower_03, VO_DALA_BOSS_14h_Female_KulTiran_HeroPower_04 };
	}

	public override List<string> GetIdleLines()
	{
		return m_IdleLines;
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_introLine = VO_DALA_BOSS_14h_Female_KulTiran_Intro_01;
		m_deathLine = VO_DALA_BOSS_14h_Female_KulTiran_Death_01;
		m_standardEmoteResponseLine = VO_DALA_BOSS_14h_Female_KulTiran_EmoteResponse_01;
	}

	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "DALA_Barkeye")
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DALA_BOSS_14h_Female_KulTiran_IntroOlBarkeye_01, Notification.SpeechBubbleDirection.TopRight, actor));
			}
			else if (cardId == "DALA_George")
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DALA_BOSS_14h_Female_KulTiran_IntroGeorge_01, Notification.SpeechBubbleDirection.TopRight, actor));
			}
			else if (cardId != "DALA_Chu")
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(m_introLine, Notification.SpeechBubbleDirection.TopRight, actor));
			}
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(m_standardEmoteResponseLine, Notification.SpeechBubbleDirection.TopRight, actor));
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
			Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			yield return WaitForEntitySoundToFinish(entity);
			string cardId = entity.GetCardId();
			m_playedLines.Add(cardId);
			switch (cardId)
			{
			case "EX1_407":
				yield return PlayAndRemoveRandomLineOnlyOnce(enemyActor, m_PlayerBrawl);
				break;
			case "CFM_654":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_14h_Female_KulTiran_PlayerFriendlyBartender_01);
				break;
			case "DAL_546":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_14h_Female_KulTiran_PlayerBarista_01);
				break;
			case "DAL_560":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_14h_Female_KulTiran_PlayerInnkeeper_01);
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
			case "EX1_407":
				yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_BossBrawl);
				break;
			case "UNG_929":
			case "LOOT_367":
			case "EX1_604":
			case "BRM_019":
				yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_BossMinions);
				break;
			}
		}
	}
}
