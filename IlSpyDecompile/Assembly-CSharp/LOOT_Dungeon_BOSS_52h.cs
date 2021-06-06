using System.Collections;
using System.Collections.Generic;

public class LOOT_Dungeon_BOSS_52h : LOOT_Dungeon
{
	private static readonly AssetReference LOOTA_BOSS_52h_TreasureVault_Death = new AssetReference("LOOTA_BOSS_52h_TreasureVault_Death.prefab:b6852fd41796e6649b95bbfca14a45e4");

	private static readonly AssetReference LOOTA_BOSS_52h_TreasureVault_Emote = new AssetReference("LOOTA_BOSS_52h_TreasureVault_Emote.prefab:0248c411691a18a4f88409445f837035");

	private static readonly AssetReference LOOTA_BOSS_52h_TreasureVault_Intro = new AssetReference("LOOTA_BOSS_52h_TreasureVault_Intro.prefab:dd6522622d543b742a2766c78d14f3e3");

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string item in new List<string> { LOOTA_BOSS_52h_TreasureVault_Death, LOOTA_BOSS_52h_TreasureVault_Emote, LOOTA_BOSS_52h_TreasureVault_Intro })
		{
			PreloadSound(item);
		}
	}

	protected override string GetBossDeathLine()
	{
		return LOOTA_BOSS_52h_TreasureVault_Death;
	}

	protected override bool GetShouldSupressDeathTextBubble()
	{
		return true;
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(LOOTA_BOSS_52h_TreasureVault_Intro, Notification.SpeechBubbleDirection.None, actor));
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(LOOTA_BOSS_52h_TreasureVault_Emote, Notification.SpeechBubbleDirection.TopRight, actor));
		}
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		yield return PlayLoyalSideKickBetrayal(missionEvent);
	}
}
