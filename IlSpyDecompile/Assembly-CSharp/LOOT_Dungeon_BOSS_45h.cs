using System.Collections;
using System.Collections.Generic;

public class LOOT_Dungeon_BOSS_45h : LOOT_Dungeon
{
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string item in new List<string> { "VO_LOOTA_BOSS_45h_Male_Gnome_Intro_01.prefab:bfdb9f8e2c3f0494083ce5aa230c7f56", "VO_LOOTA_BOSS_45h_Male_Gnome_EmoteResponse_01.prefab:77e7d3d50d4efce4b87b1cad34b67048", "VO_LOOTA_BOSS_45h_Male_Gnome_HeroPower1_01.prefab:ddfb713f6b7cd284daf2a96f21f731d8", "VO_LOOTA_BOSS_45h_Male_Gnome_HeroPower2_01.prefab:005a46770cc21444cbc8c05853a88481", "VO_LOOTA_BOSS_45h_Male_Gnome_HeroPower3_01.prefab:35be5eca8eae03342a5018b40c7741ba", "VO_LOOTA_BOSS_45h_Male_Gnome_HeroPower4_01.prefab:43f0b3d950d0bce4281f30f83c11cae2", "VO_LOOTA_BOSS_45h_Male_Gnome_Death_01.prefab:84f3bb48aefd92846aadc833a1928aeb", "VO_LOOTA_BOSS_45h_Male_Gnome_DefeatPlayer_01.prefab:bad2261cb6735c145a71f6fe50cf427a" })
		{
			PreloadSound(item);
		}
	}

	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
	}

	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string> { "VO_LOOTA_BOSS_45h_Male_Gnome_HeroPower1_01.prefab:ddfb713f6b7cd284daf2a96f21f731d8", "VO_LOOTA_BOSS_45h_Male_Gnome_HeroPower2_01.prefab:005a46770cc21444cbc8c05853a88481", "VO_LOOTA_BOSS_45h_Male_Gnome_HeroPower3_01.prefab:35be5eca8eae03342a5018b40c7741ba", "VO_LOOTA_BOSS_45h_Male_Gnome_HeroPower4_01.prefab:43f0b3d950d0bce4281f30f83c11cae2" };
	}

	protected override string GetBossDeathLine()
	{
		return "VO_LOOTA_BOSS_45h_Male_Gnome_Death_01.prefab:84f3bb48aefd92846aadc833a1928aeb";
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
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_45h_Male_Gnome_Intro_01.prefab:bfdb9f8e2c3f0494083ce5aa230c7f56", Notification.SpeechBubbleDirection.TopRight, actor));
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_45h_Male_Gnome_EmoteResponse_01.prefab:77e7d3d50d4efce4b87b1cad34b67048", Notification.SpeechBubbleDirection.TopRight, actor));
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
