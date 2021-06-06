using System.Collections;
using System.Collections.Generic;

public class LOOT_Dungeon_BOSS_48h : LOOT_Dungeon
{
	private static readonly AssetReference TrappedRoom_LOOTA_BOSS_48h_Death = new AssetReference("TrappedRoom_LOOTA_BOSS_48h_Death.prefab:a6c6e15236bcc405aafc279d56f13a3d");

	private static readonly AssetReference TrappedRoom_LOOTA_BOSS_48h_EmoteResponse = new AssetReference("TrappedRoom_LOOTA_BOSS_48h_EmoteResponse.prefab:36afbe32da4e24850860d944519508bc");

	private static readonly AssetReference TrappedRoom_LOOTA_BOSS_48h_Intro = new AssetReference("TrappedRoom_LOOTA_BOSS_48h_Intro.prefab:4d478c1f12dc2411d89e3f3b85fbcd85");

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string item in new List<string> { TrappedRoom_LOOTA_BOSS_48h_Death, TrappedRoom_LOOTA_BOSS_48h_EmoteResponse, TrappedRoom_LOOTA_BOSS_48h_Intro })
		{
			PreloadSound(item);
		}
	}

	protected override string GetBossDeathLine()
	{
		return TrappedRoom_LOOTA_BOSS_48h_Death;
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
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(TrappedRoom_LOOTA_BOSS_48h_Intro, Notification.SpeechBubbleDirection.None, actor));
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(TrappedRoom_LOOTA_BOSS_48h_EmoteResponse, Notification.SpeechBubbleDirection.TopRight, actor));
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
