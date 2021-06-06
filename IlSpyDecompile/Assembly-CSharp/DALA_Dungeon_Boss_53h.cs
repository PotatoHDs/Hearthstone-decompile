using System.Collections;
using System.Collections.Generic;

public class DALA_Dungeon_Boss_53h : DALA_Dungeon
{
	private static readonly AssetReference VO_DALA_BOSS_53h_Male_Murloc_Death_01 = new AssetReference("VO_DALA_BOSS_53h_Male_Murloc_Death_01.prefab:e3bf95c5ab6eec54d808f1250ab22e1e");

	private static readonly AssetReference VO_DALA_BOSS_53h_Male_Murloc_DefeatPlayer_02 = new AssetReference("VO_DALA_BOSS_53h_Male_Murloc_DefeatPlayer_02.prefab:4dc3e58ff8eaa3543bb49056fbc71529");

	private static readonly AssetReference VO_DALA_BOSS_53h_Male_Murloc_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_53h_Male_Murloc_EmoteResponse_01.prefab:71e7c09ba24fca74db76a97b7a6e1667");

	private static readonly AssetReference VO_DALA_BOSS_53h_Male_Murloc_HeroPower_01 = new AssetReference("VO_DALA_BOSS_53h_Male_Murloc_HeroPower_01.prefab:1039d921dad26eb41951d60dd05c96b6");

	private static readonly AssetReference VO_DALA_BOSS_53h_Male_Murloc_HeroPower_02 = new AssetReference("VO_DALA_BOSS_53h_Male_Murloc_HeroPower_02.prefab:099a69e4686ebe348a585e20be363c58");

	private static readonly AssetReference VO_DALA_BOSS_53h_Male_Murloc_HeroPower_03 = new AssetReference("VO_DALA_BOSS_53h_Male_Murloc_HeroPower_03.prefab:4d6b1895e7904e54c8ad30d435328e67");

	private static readonly AssetReference VO_DALA_BOSS_53h_Male_Murloc_HeroPower_04 = new AssetReference("VO_DALA_BOSS_53h_Male_Murloc_HeroPower_04.prefab:1cb1390b529f3ec46bec334b065851c8");

	private static readonly AssetReference VO_DALA_BOSS_53h_Male_Murloc_HeroPower_05 = new AssetReference("VO_DALA_BOSS_53h_Male_Murloc_HeroPower_05.prefab:def450c772088e24387d584759cb21d8");

	private static readonly AssetReference VO_DALA_BOSS_53h_Male_Murloc_HeroPower_06 = new AssetReference("VO_DALA_BOSS_53h_Male_Murloc_HeroPower_06.prefab:e50af7eac2fb44648acbc231c30eb8ed");

	private static readonly AssetReference VO_DALA_BOSS_53h_Male_Murloc_HeroPower_07 = new AssetReference("VO_DALA_BOSS_53h_Male_Murloc_HeroPower_07.prefab:3b7e42fcbca56df43a5d23ea684b51b9");

	private static readonly AssetReference VO_DALA_BOSS_53h_Male_Murloc_HeroPower_08 = new AssetReference("VO_DALA_BOSS_53h_Male_Murloc_HeroPower_08.prefab:5fff01046a5fbda40bf2edf2b921918a");

	private static readonly AssetReference VO_DALA_BOSS_53h_Male_Murloc_HeroPower_09 = new AssetReference("VO_DALA_BOSS_53h_Male_Murloc_HeroPower_09.prefab:f0137bd3e6e1ebe4a8aa54cace26c1a6");

	private static readonly AssetReference VO_DALA_BOSS_53h_Male_Murloc_HeroPower_11 = new AssetReference("VO_DALA_BOSS_53h_Male_Murloc_HeroPower_11.prefab:1092e920fa881654aa59ddd397f0698f");

	private static readonly AssetReference VO_DALA_BOSS_53h_Male_Murloc_Intro_01 = new AssetReference("VO_DALA_BOSS_53h_Male_Murloc_Intro_01.prefab:23e54644913977444af93befdfc3ec27");

	private static readonly AssetReference VO_DALA_BOSS_53h_Male_Murloc_PlayerMurloc_01 = new AssetReference("VO_DALA_BOSS_53h_Male_Murloc_PlayerMurloc_01.prefab:13ed21ba65aa81841a3def332d19940c");

	private static readonly AssetReference VO_DALA_BOSS_53h_Male_Murloc_PlayerMurloc_02 = new AssetReference("VO_DALA_BOSS_53h_Male_Murloc_PlayerMurloc_02.prefab:9df0a564df5893a48a648367cf634804");

	private static readonly AssetReference VO_DALA_BOSS_53h_Male_Murloc_PlayerMurloc_03 = new AssetReference("VO_DALA_BOSS_53h_Male_Murloc_PlayerMurloc_03.prefab:90a45315c0871e1458be5af01ea73121");

	private static readonly AssetReference VO_DALA_BOSS_53h_Male_Murloc_PlayerMurloc_04 = new AssetReference("VO_DALA_BOSS_53h_Male_Murloc_PlayerMurloc_04.prefab:ed859e85cf96b0847b95f469a1f22eca");

	private static readonly AssetReference VO_DALA_BOSS_53h_Male_Murloc_PlayerMurlocKnight_01 = new AssetReference("VO_DALA_BOSS_53h_Male_Murloc_PlayerMurlocKnight_01.prefab:4b788431304575149a556655330c896a");

	private HashSet<string> m_playedLines = new HashSet<string>();

	private static List<string> m_BossHeroPower = new List<string> { VO_DALA_BOSS_53h_Male_Murloc_HeroPower_01, VO_DALA_BOSS_53h_Male_Murloc_HeroPower_02, VO_DALA_BOSS_53h_Male_Murloc_HeroPower_03, VO_DALA_BOSS_53h_Male_Murloc_HeroPower_04, VO_DALA_BOSS_53h_Male_Murloc_HeroPower_05, VO_DALA_BOSS_53h_Male_Murloc_HeroPower_06, VO_DALA_BOSS_53h_Male_Murloc_HeroPower_07, VO_DALA_BOSS_53h_Male_Murloc_HeroPower_08, VO_DALA_BOSS_53h_Male_Murloc_HeroPower_09, VO_DALA_BOSS_53h_Male_Murloc_HeroPower_11 };

	private static List<string> m_PlayerMurloc = new List<string> { VO_DALA_BOSS_53h_Male_Murloc_PlayerMurloc_01, VO_DALA_BOSS_53h_Male_Murloc_PlayerMurloc_02, VO_DALA_BOSS_53h_Male_Murloc_PlayerMurloc_03, VO_DALA_BOSS_53h_Male_Murloc_PlayerMurloc_04 };

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_DALA_BOSS_53h_Male_Murloc_Death_01, VO_DALA_BOSS_53h_Male_Murloc_DefeatPlayer_02, VO_DALA_BOSS_53h_Male_Murloc_EmoteResponse_01, VO_DALA_BOSS_53h_Male_Murloc_HeroPower_01, VO_DALA_BOSS_53h_Male_Murloc_HeroPower_02, VO_DALA_BOSS_53h_Male_Murloc_HeroPower_03, VO_DALA_BOSS_53h_Male_Murloc_HeroPower_04, VO_DALA_BOSS_53h_Male_Murloc_HeroPower_05, VO_DALA_BOSS_53h_Male_Murloc_HeroPower_06, VO_DALA_BOSS_53h_Male_Murloc_HeroPower_07,
			VO_DALA_BOSS_53h_Male_Murloc_HeroPower_08, VO_DALA_BOSS_53h_Male_Murloc_HeroPower_09, VO_DALA_BOSS_53h_Male_Murloc_HeroPower_11, VO_DALA_BOSS_53h_Male_Murloc_Intro_01, VO_DALA_BOSS_53h_Male_Murloc_PlayerMurloc_01, VO_DALA_BOSS_53h_Male_Murloc_PlayerMurloc_02, VO_DALA_BOSS_53h_Male_Murloc_PlayerMurloc_03, VO_DALA_BOSS_53h_Male_Murloc_PlayerMurloc_04, VO_DALA_BOSS_53h_Male_Murloc_PlayerMurlocKnight_01
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
		m_introLine = VO_DALA_BOSS_53h_Male_Murloc_Intro_01;
		m_deathLine = VO_DALA_BOSS_53h_Male_Murloc_Death_01;
		m_standardEmoteResponseLine = VO_DALA_BOSS_53h_Male_Murloc_EmoteResponse_01;
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
			if (cardId != "DALA_George" && cardId != "DALA_Barkeye" && cardId != "DALA_Rakanishu" && cardId != "DALA_Vessina")
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(m_introLine, Notification.SpeechBubbleDirection.TopRight, actor));
			}
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(m_standardEmoteResponseLine, Notification.SpeechBubbleDirection.TopRight, actor));
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
		switch (missionEvent)
		{
		case 101:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_PlayerMurloc);
			break;
		case 102:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_BossHeroPower);
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
			Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			yield return WaitForEntitySoundToFinish(entity);
			string cardId = entity.GetCardId();
			m_playedLines.Add(cardId);
			if (cardId == "AT_076")
			{
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_53h_Male_Murloc_PlayerMurlocKnight_01);
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
			yield return WaitForEntitySoundToFinish(entity);
			string cardId = entity.GetCardId();
			m_playedLines.Add(cardId);
		}
	}
}
