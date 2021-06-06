using System.Collections;
using System.Collections.Generic;

public class ULDA_Dungeon_Boss_33h : ULDA_Dungeon
{
	private static readonly AssetReference VO_ULDA_BOSS_33h_Male_Gnome_BossFirstMech_01 = new AssetReference("VO_ULDA_BOSS_33h_Male_Gnome_BossFirstMech_01.prefab:d80348395d0e80b43aa5f903a5799da3");

	private static readonly AssetReference VO_ULDA_BOSS_33h_Male_Gnome_BossViciousScraphound_01 = new AssetReference("VO_ULDA_BOSS_33h_Male_Gnome_BossViciousScraphound_01.prefab:8d8609718015d494381e46298983b3a6");

	private static readonly AssetReference VO_ULDA_BOSS_33h_Male_Gnome_DeathALT_01 = new AssetReference("VO_ULDA_BOSS_33h_Male_Gnome_DeathALT_01.prefab:d0b713b2a683e20478e95c4ee16bdefa");

	private static readonly AssetReference VO_ULDA_BOSS_33h_Male_Gnome_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_33h_Male_Gnome_DefeatPlayer_01.prefab:0a87f9bad19ce11498b1448d09728d66");

	private static readonly AssetReference VO_ULDA_BOSS_33h_Male_Gnome_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_33h_Male_Gnome_EmoteResponse_01.prefab:83b97b0f553b2674db5eeb9ed31c362e");

	private static readonly AssetReference VO_ULDA_BOSS_33h_Male_Gnome_HeroPower_01 = new AssetReference("VO_ULDA_BOSS_33h_Male_Gnome_HeroPower_01.prefab:2a82517c083674a4598cb74993fae711");

	private static readonly AssetReference VO_ULDA_BOSS_33h_Male_Gnome_HeroPower_02 = new AssetReference("VO_ULDA_BOSS_33h_Male_Gnome_HeroPower_02.prefab:8a94adad8acbb2946b89854f0c3371e6");

	private static readonly AssetReference VO_ULDA_BOSS_33h_Male_Gnome_HeroPower_04 = new AssetReference("VO_ULDA_BOSS_33h_Male_Gnome_HeroPower_04.prefab:1672fa110802b5147b99b79eff7138b0");

	private static readonly AssetReference VO_ULDA_BOSS_33h_Male_Gnome_HeroPower_05 = new AssetReference("VO_ULDA_BOSS_33h_Male_Gnome_HeroPower_05.prefab:b0a730614d4fb4547953b170506da852");

	private static readonly AssetReference VO_ULDA_BOSS_33h_Male_Gnome_HeroPowerBossMinionOnly_01 = new AssetReference("VO_ULDA_BOSS_33h_Male_Gnome_HeroPowerBossMinionOnly_01.prefab:93ffda6280c8417469cd4bb7e8c5bb6b");

	private static readonly AssetReference VO_ULDA_BOSS_33h_Male_Gnome_HeroPowerMinionDestroy_01 = new AssetReference("VO_ULDA_BOSS_33h_Male_Gnome_HeroPowerMinionDestroy_01.prefab:451f2bfc23c1c6c4fbf238c6831b8e9b");

	private static readonly AssetReference VO_ULDA_BOSS_33h_Male_Gnome_HeroPowerNoMinions_01 = new AssetReference("VO_ULDA_BOSS_33h_Male_Gnome_HeroPowerNoMinions_01.prefab:8219b43b0018d1541bc4c1f17cd13edd");

	private static readonly AssetReference VO_ULDA_BOSS_33h_Male_Gnome_HeroPowerPlayerMinionOnly_01 = new AssetReference("VO_ULDA_BOSS_33h_Male_Gnome_HeroPowerPlayerMinionOnly_01.prefab:49dad37437303474894d12d650afbab7");

	private static readonly AssetReference VO_ULDA_BOSS_33h_Male_Gnome_Idle_01 = new AssetReference("VO_ULDA_BOSS_33h_Male_Gnome_Idle_01.prefab:e27b9ccdd2c730b4b9d5403b7c0354ef");

	private static readonly AssetReference VO_ULDA_BOSS_33h_Male_Gnome_Idle_02 = new AssetReference("VO_ULDA_BOSS_33h_Male_Gnome_Idle_02.prefab:860ed7510ef3e0842bcc36ceaf510c14");

	private static readonly AssetReference VO_ULDA_BOSS_33h_Male_Gnome_Idle_03 = new AssetReference("VO_ULDA_BOSS_33h_Male_Gnome_Idle_03.prefab:c47782b1377a5374dadd5320fe1692f8");

	private static readonly AssetReference VO_ULDA_BOSS_33h_Male_Gnome_Intro_01 = new AssetReference("VO_ULDA_BOSS_33h_Male_Gnome_Intro_01.prefab:f54369690a0d210459bc50e46add0ef8");

	private static readonly AssetReference VO_ULDA_BOSS_33h_Male_Gnome_PlayerEMPOperative_01 = new AssetReference("VO_ULDA_BOSS_33h_Male_Gnome_PlayerEMPOperative_01.prefab:cd33f4afb106db7449f8ad336f2853ee");

	private static readonly AssetReference VO_ULDA_BOSS_33h_Male_Gnome_PlayerMaskedContender_01 = new AssetReference("VO_ULDA_BOSS_33h_Male_Gnome_PlayerMaskedContender_01.prefab:9a9495d4097787a4aadb02901e0d2180");

	private static readonly AssetReference VO_ULDA_BOSS_33h_Male_Gnome_PlayerWrenchcalibur_01 = new AssetReference("VO_ULDA_BOSS_33h_Male_Gnome_PlayerWrenchcalibur_01.prefab:56abaf82459f4214f925f6542ffd78b9");

	private static readonly AssetReference VO_ULDA_BOSS_33h_Male_Gnome_Take20Damage_01 = new AssetReference("VO_ULDA_BOSS_33h_Male_Gnome_Take20Damage_01.prefab:89d75002cc1c305438390f205c279647");

	private static readonly AssetReference VO_ULDA_BOSS_33h_Male_Gnome_TurnOne_01 = new AssetReference("VO_ULDA_BOSS_33h_Male_Gnome_TurnOne_01.prefab:6d40927df7682d541826f6bec4944ba9");

	private List<string> m_HeroPowerLines = new List<string> { VO_ULDA_BOSS_33h_Male_Gnome_HeroPower_01, VO_ULDA_BOSS_33h_Male_Gnome_HeroPower_02, VO_ULDA_BOSS_33h_Male_Gnome_HeroPower_04, VO_ULDA_BOSS_33h_Male_Gnome_HeroPower_05 };

	private List<string> m_IdleLines = new List<string> { VO_ULDA_BOSS_33h_Male_Gnome_Idle_01, VO_ULDA_BOSS_33h_Male_Gnome_Idle_02, VO_ULDA_BOSS_33h_Male_Gnome_Idle_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_ULDA_BOSS_33h_Male_Gnome_BossFirstMech_01, VO_ULDA_BOSS_33h_Male_Gnome_BossViciousScraphound_01, VO_ULDA_BOSS_33h_Male_Gnome_DeathALT_01, VO_ULDA_BOSS_33h_Male_Gnome_DefeatPlayer_01, VO_ULDA_BOSS_33h_Male_Gnome_EmoteResponse_01, VO_ULDA_BOSS_33h_Male_Gnome_HeroPower_01, VO_ULDA_BOSS_33h_Male_Gnome_HeroPower_02, VO_ULDA_BOSS_33h_Male_Gnome_HeroPower_04, VO_ULDA_BOSS_33h_Male_Gnome_HeroPower_05, VO_ULDA_BOSS_33h_Male_Gnome_HeroPowerBossMinionOnly_01,
			VO_ULDA_BOSS_33h_Male_Gnome_HeroPowerMinionDestroy_01, VO_ULDA_BOSS_33h_Male_Gnome_HeroPowerNoMinions_01, VO_ULDA_BOSS_33h_Male_Gnome_HeroPowerPlayerMinionOnly_01, VO_ULDA_BOSS_33h_Male_Gnome_Idle_01, VO_ULDA_BOSS_33h_Male_Gnome_Idle_02, VO_ULDA_BOSS_33h_Male_Gnome_Idle_03, VO_ULDA_BOSS_33h_Male_Gnome_Intro_01, VO_ULDA_BOSS_33h_Male_Gnome_PlayerEMPOperative_01, VO_ULDA_BOSS_33h_Male_Gnome_PlayerMaskedContender_01, VO_ULDA_BOSS_33h_Male_Gnome_PlayerWrenchcalibur_01,
			VO_ULDA_BOSS_33h_Male_Gnome_Take20Damage_01, VO_ULDA_BOSS_33h_Male_Gnome_TurnOne_01
		};
		SetBossVOLines(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	public override List<string> GetIdleLines()
	{
		return m_IdleLines;
	}

	public override List<string> GetBossHeroPowerRandomLines()
	{
		return m_HeroPowerLines;
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_introLine = VO_ULDA_BOSS_33h_Male_Gnome_Intro_01;
		m_deathLine = VO_ULDA_BOSS_33h_Male_Gnome_DeathALT_01;
		m_standardEmoteResponseLine = VO_ULDA_BOSS_33h_Male_Gnome_EmoteResponse_01;
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(m_introLine, Notification.SpeechBubbleDirection.TopRight, actor));
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
		case 107:
			yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_33h_Male_Gnome_BossFirstMech_01);
			break;
		case 104:
			yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_33h_Male_Gnome_HeroPowerBossMinionOnly_01);
			break;
		case 106:
			yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_33h_Male_Gnome_HeroPowerMinionDestroy_01);
			break;
		case 103:
			yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_33h_Male_Gnome_HeroPowerNoMinions_01);
			break;
		case 102:
			yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_33h_Male_Gnome_Take20Damage_01);
			break;
		case 101:
			yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_33h_Male_Gnome_TurnOne_01);
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
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			switch (cardId)
			{
			case "DAL_759":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_33h_Male_Gnome_BossViciousScraphound_01);
				break;
			case "BOT_540":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_33h_Male_Gnome_PlayerEMPOperative_01);
				break;
			case "TRL_530":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_33h_Male_Gnome_PlayerMaskedContender_01);
				break;
			case "DAL_063":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_33h_Male_Gnome_PlayerWrenchcalibur_01);
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
		}
	}
}
