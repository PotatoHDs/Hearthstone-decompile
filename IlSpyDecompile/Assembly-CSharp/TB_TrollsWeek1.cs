using System.Collections;
using UnityEngine;

public class TB_TrollsWeek1 : MissionEntity
{
	private enum VICTOR
	{
		PLAYERLOST,
		PLAYERWIN,
		ERROR
	}

	private VICTOR matchResult;

	private Notification StartPopup;

	private int shouldShowVictory;

	private int shouldShowIntro;

	private static readonly AssetReference VO_HERO_02b_Male_Troll_Event_11 = new AssetReference("VO_HERO_02b_Male_Troll_Event_11:e360856574d463247960068d89134791");

	private static readonly AssetReference VO_HERO_02b_Male_Troll_Event_03 = new AssetReference("VO_HERO_02b_Male_Troll_Event_03:7a6078498a8e2dc4284fc70f8d37faf4");

	private static readonly AssetReference VO_HERO_02b_Male_Troll_Event_09 = new AssetReference("VO_HERO_02b_Male_Troll_Event_09:2b061472d8f0e4549801ff0c25d8d686");

	private static readonly Vector3 LEFT_OF_FRIENDLY_HERO = new Vector3(-1f, 0f, 1f);

	private Player friendlySidePlayer;

	public override void PreloadAssets()
	{
		PreloadSound(VO_HERO_02b_Male_Troll_Event_11);
		PreloadSound(VO_HERO_02b_Male_Troll_Event_03);
		PreloadSound(VO_HERO_02b_Male_Troll_Event_09);
	}

	private void Start()
	{
		friendlySidePlayer = GameState.Get().GetFriendlySidePlayer();
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		switch (missionEvent)
		{
		case 10:
			friendlySidePlayer = GameState.Get().GetFriendlySidePlayer();
			shouldShowIntro = friendlySidePlayer.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_1);
			if (shouldShowIntro == 1)
			{
				yield return PlayBossLine(VO_HERO_02b_Male_Troll_Event_11);
				yield return new WaitForSeconds(4f);
			}
			break;
		case 11:
			yield return PlayBossLine(VO_HERO_02b_Male_Troll_Event_03);
			yield return new WaitForSeconds(4f);
			break;
		case 12:
			yield return PlayBossLine(VO_HERO_02b_Male_Troll_Event_09);
			yield return new WaitForSeconds(4f);
			break;
		}
	}

	private IEnumerator PlayBossLine(string line, bool persistCharacter = false)
	{
		Notification.SpeechBubbleDirection direction = Notification.SpeechBubbleDirection.BottomLeft;
		yield return PlayMissionFlavorLine("Rastakhan_BrassRing_Quote:179bfad1464576448aeabfe5c3eff601", line, LEFT_OF_FRIENDLY_HERO, direction, 2.5f, persistCharacter);
	}

	public override void NotifyOfGameOver(TAG_PLAYSTATE gameResult)
	{
		switch (gameResult)
		{
		case TAG_PLAYSTATE.WON:
			matchResult = VICTOR.PLAYERWIN;
			break;
		case TAG_PLAYSTATE.LOST:
			Debug.Log("Made it to Playstate:Lost");
			matchResult = VICTOR.PLAYERLOST;
			break;
		case TAG_PLAYSTATE.TIED:
			matchResult = VICTOR.ERROR;
			break;
		}
		base.NotifyOfGameOver(gameResult);
	}

	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		friendlySidePlayer = GameState.Get().GetFriendlySidePlayer();
		shouldShowVictory = friendlySidePlayer.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_2);
		yield return new WaitForSeconds(2f);
		switch (matchResult)
		{
		case VICTOR.PLAYERLOST:
			GameState.Get().SetBusy(busy: true);
			GameState.Get().SetBusy(busy: false);
			break;
		case VICTOR.PLAYERWIN:
			if (shouldShowVictory == 1)
			{
				yield return new WaitForSeconds(3f);
				GameState.Get().SetBusy(busy: true);
				yield return PlayBossLine(VO_HERO_02b_Male_Troll_Event_03);
				yield return PlayBossLine(VO_HERO_02b_Male_Troll_Event_09);
				GameState.Get().SetBusy(busy: false);
			}
			break;
		}
	}
}
