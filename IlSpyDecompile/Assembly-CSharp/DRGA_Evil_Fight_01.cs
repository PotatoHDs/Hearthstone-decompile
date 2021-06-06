using System.Collections;
using System.Collections.Generic;

public class DRGA_Evil_Fight_01 : DRGA_Dungeon
{
	private static readonly AssetReference VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Boss_Death_01 = new AssetReference("VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Boss_Death_01.prefab:c0b6294a7f3565e46a0bcf6ba465690d");

	private static readonly AssetReference VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Boss_HeroPower_01_01 = new AssetReference("VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Boss_HeroPower_01_01.prefab:3f451d515cd3d7141bd33a354a3a21bb");

	private static readonly AssetReference VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Boss_HeroPower_02_01 = new AssetReference("VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Boss_HeroPower_02_01.prefab:3f57520d4e98fe64cadbcbee11cc78f7");

	private static readonly AssetReference VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Boss_HeroPower_03_01 = new AssetReference("VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Boss_HeroPower_03_01.prefab:2dfaf6f49d934a943ae1ceda9a6edb95");

	private static readonly AssetReference VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_BossAttack_01 = new AssetReference("VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_BossAttack_01.prefab:c939a890e0ca0b841afb7e621d18ea16");

	private static readonly AssetReference VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_BossStart_01 = new AssetReference("VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_BossStart_01.prefab:e0fb30ba5b7c8f143b73db19ae80d81b");

	private static readonly AssetReference VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_BossStartHeroic_01 = new AssetReference("VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_BossStartHeroic_01.prefab:656e8f68d1e94a9439305c82f54f0fb4");

	private static readonly AssetReference VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_EmoteResponse_01 = new AssetReference("VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_EmoteResponse_01.prefab:caf7ced67c43a26438ceacf5b1dc51b2");

	private static readonly AssetReference VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_EmoteResponseHeroic_01 = new AssetReference("VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_EmoteResponseHeroic_01.prefab:72fad61b9efeab74d9eea748d0386474");

	private static readonly AssetReference VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Finely_Bubblehearth_01_01 = new AssetReference("VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Finely_Bubblehearth_01_01.prefab:601f126cecc8c004389d25f7499e3247");

	private static readonly AssetReference VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Finely_Bubblehearth_02_01 = new AssetReference("VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Finely_Bubblehearth_02_01.prefab:47163ff23d2c5414389b6ba9c704d91a");

	private static readonly AssetReference VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Idle_01_01 = new AssetReference("VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Idle_01_01.prefab:9c51fdf75b4ca1e418b859479a7bbf3f");

	private static readonly AssetReference VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Idle_02_01 = new AssetReference("VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Idle_02_01.prefab:2bb7f600b5e3a694d88dfa5703366bfe");

	private static readonly AssetReference VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Idle_03_01 = new AssetReference("VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Idle_03_01.prefab:b0a5d0ccc1642f743b35ee475eb9cc48");

	private static readonly AssetReference VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Misc_01a_01 = new AssetReference("VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Misc_01a_01.prefab:c0f6dc96e459df14d905836244f6184f");

	private static readonly AssetReference VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Misc_02_01 = new AssetReference("VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Misc_02_01.prefab:9623c241e058e764fb882ff123d04d96");

	private static readonly AssetReference VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Misc_03_01 = new AssetReference("VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Misc_03_01.prefab:397f10ad117297f46961de5b10413eb6");

	private static readonly AssetReference VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Misc_04_01 = new AssetReference("VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Misc_04_01.prefab:50dc292c739a2d14e96c8a90a50c59df");

	private static readonly AssetReference VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Misc_05_01 = new AssetReference("VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Misc_05_01.prefab:d94140d723c2f2a48999c7b268ec21c8");

	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_01_Turn_02_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_01_Turn_02_01.prefab:b1c4c16dc36290a49be1d0a399b2c0df");

	private static readonly AssetReference VO_DRGA_BOSS_13h_Female_Arakkoa_Evil_Fight_01_Misc_01b_01 = new AssetReference("VO_DRGA_BOSS_13h_Female_Arakkoa_Evil_Fight_01_Misc_01b_01.prefab:c2596741bfce6a647bb8a77529798d0b");

	private List<string> m_VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Boss_HeroPowerLines = new List<string> { VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Boss_HeroPower_01_01, VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Boss_HeroPower_02_01, VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Boss_HeroPower_03_01 };

	private List<string> m_VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_IdleLines = new List<string> { VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Idle_01_01, VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Idle_02_01, VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Idle_03_01 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Boss_Death_01, VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Boss_HeroPower_01_01, VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Boss_HeroPower_02_01, VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Boss_HeroPower_03_01, VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_BossAttack_01, VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_BossStart_01, VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_BossStartHeroic_01, VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_EmoteResponse_01, VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_EmoteResponseHeroic_01, VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Finely_Bubblehearth_01_01,
			VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Finely_Bubblehearth_02_01, VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Idle_01_01, VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Idle_02_01, VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Idle_03_01, VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Misc_01a_01, VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Misc_02_01, VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Misc_03_01, VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Misc_04_01, VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Misc_05_01, VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_01_Turn_02_01,
			VO_DRGA_BOSS_13h_Female_Arakkoa_Evil_Fight_01_Misc_01b_01
		};
		SetBossVOLines(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	public override List<string> GetIdleLines()
	{
		return m_VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_IdleLines;
	}

	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	public override List<string> GetBossHeroPowerRandomLines()
	{
		return m_VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Boss_HeroPowerLines;
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		if (emoteType == EmoteType.START)
		{
			if (!m_Heroic)
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_BossStart_01, Notification.SpeechBubbleDirection.TopRight, actor));
			}
			else if (m_Heroic)
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_BossStartHeroic_01, Notification.SpeechBubbleDirection.TopRight, actor));
			}
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			if (!m_Heroic)
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_EmoteResponse_01, Notification.SpeechBubbleDirection.TopRight, actor));
			}
			else if (m_Heroic)
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_EmoteResponseHeroic_01, Notification.SpeechBubbleDirection.TopRight, actor));
			}
		}
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero()
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
				yield return PlayLineAlways(DRGA_Dungeon.RafaamBrassRing, VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_01_Turn_02_01);
			}
			break;
		case 102:
			yield return PlayLineOnlyOnce(enemyActor, VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_BossAttack_01);
			break;
		case 103:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(enemyActor, VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Finely_Bubblehearth_01_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 105:
			if (!m_Heroic)
			{
				yield return PlayLineAlways(enemyActor, VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Misc_01a_01);
				yield return PlayLineAlways(friendlyActor, VO_DRGA_BOSS_13h_Female_Arakkoa_Evil_Fight_01_Misc_01b_01);
				yield return PlayLineAlways(enemyActor, VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Misc_02_01);
			}
			break;
		case 107:
			yield return PlayLineAlways(enemyActor, VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Misc_03_01);
			break;
		case 108:
			if (!m_Heroic)
			{
				yield return PlayLineAlways(enemyActor, VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Misc_04_01);
			}
			break;
		case 109:
			yield return PlayLineAlways(enemyActor, VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Misc_05_01);
			break;
		case 110:
			yield return PlayLineAlways(enemyActor, VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Boss_Death_01);
			break;
		case 111:
			yield return PlayLineAlways(enemyActor, VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Finely_Bubblehearth_02_01);
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
		}
	}
}
