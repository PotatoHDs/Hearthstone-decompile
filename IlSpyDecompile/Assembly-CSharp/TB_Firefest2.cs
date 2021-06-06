using System.Collections;
using UnityEngine;

public class TB_Firefest2 : MissionEntity
{
	private enum VICTOR
	{
		ELEMENTALSWIN,
		PLAYERWIN,
		ERROR
	}

	private enum BOSS
	{
		AHUNE,
		RAGNAROS
	}

	private Actor ragnarosActor;

	private Card ragnarosCard;

	private bool isHeadInPlay;

	private bool hasSpoken;

	private Vector3 popUpPos;

	private VICTOR matchResult;

	private Notification StartPopup;

	private static readonly AssetReference VO_AHUNE_Male_Elemental_HSFireFestival_02 = new AssetReference("VO_AHUNE_Male_Elemental_HSFireFestival_02:8dc670a0c96a23d44bc6b87957b223fe");

	private static readonly AssetReference VO_AHUNE_Male_Elemental_HSFireFestival_04 = new AssetReference("VO_AHUNE_Male_Elemental_HSFireFestival_04:63a6f5920298e39418087cbfb837e9af");

	private static readonly AssetReference VO_AHUNE_Male_Elemental_HSFireFestival_05 = new AssetReference("VO_AHUNE_Male_Elemental_HSFireFestival_05:4a15da86b328e8c4ea5a805f9080f8c5");

	private static readonly AssetReference VO_RAGNAROS_Male_Elemental_AhuneResponses_01 = new AssetReference("VO_RAGNAROS_Male_Elemental_AhuneResponses_01:0de84fe30f9c4c04dbc1f996bd2694b3");

	private static readonly AssetReference VO_RAGNAROS_Male_Elemental_AhuneResponses_02 = new AssetReference("VO_RAGNAROS_Male_Elemental_AhuneResponses_02:58a0b7d69171f57409999d3b984c54d9");

	private static readonly AssetReference VO_RAGNAROS_Male_Elemental_AhuneResponses_05 = new AssetReference("VO_RAGNAROS_Male_Elemental_AhuneResponses_05:31f75d15dc1a7f34bafcfbdde1c9f2a1");

	private static readonly AssetReference VO_Ragnaros_Male_Elemental_Brawl_01 = new AssetReference("VO_Ragnaros_Male_Elemental_Brawl_01:da09dbd1ad9ba434fbb549c8bbd2c9ce");

	private static readonly AssetReference VO_Ragnaros_Male_Elemental_Brawl_02 = new AssetReference("VO_Ragnaros_Male_Elemental_Brawl_02:b5630abf5a135384695d1f58fa025fe5");

	private static readonly AssetReference VO_Ragnaros_Male_Elemental_Brawl_05 = new AssetReference("VO_Ragnaros_Male_Elemental_Brawl_05:4455f90db8e99eb45bc158677acb672e");

	private static readonly AssetReference VO_Ragnaros_Male_Elemental_Brawl_07 = new AssetReference("VO_Ragnaros_Male_Elemental_Brawl_07:0934d1efe1db28041be7f03b4295ffdd");

	private static readonly AssetReference VO_Ahune_Male_Elemental_Brawl_18 = new AssetReference("VO_Ahune_Male_Elemental_Brawl_18:49d4e9ef35728e84cb171df7cc56a32b");

	private static readonly AssetReference VO_Ahune_Male_Elemental_Brawl_20 = new AssetReference("VO_Ahune_Male_Elemental_Brawl_20:5fb1142388aecbd4588f2ca08d8f391a");

	private static readonly AssetReference VO_Ahune_Male_Elemental_Brawl_25 = new AssetReference("VO_Ahune_Male_Elemental_Brawl_25:c280663239e28fe419072cc64df39098");

	private static readonly Vector3 LEFT_OF_ENEMY_HERO = new Vector3(-1f, 0f, -2.8f);

	private static readonly Vector3 RIGHT_OF_ENEMY_HERO = new Vector3(-6f, 0f, -2.8f);

	private Player friendlySidePlayer;

	private Entity playerEntity;

	public override void PreloadAssets()
	{
		PreloadSound(VO_AHUNE_Male_Elemental_HSFireFestival_02);
		PreloadSound(VO_AHUNE_Male_Elemental_HSFireFestival_04);
		PreloadSound(VO_AHUNE_Male_Elemental_HSFireFestival_05);
		PreloadSound(VO_RAGNAROS_Male_Elemental_AhuneResponses_01);
		PreloadSound(VO_RAGNAROS_Male_Elemental_AhuneResponses_02);
		PreloadSound(VO_RAGNAROS_Male_Elemental_AhuneResponses_05);
		PreloadSound(VO_Ragnaros_Male_Elemental_Brawl_01);
		PreloadSound(VO_Ragnaros_Male_Elemental_Brawl_02);
		PreloadSound(VO_Ragnaros_Male_Elemental_Brawl_05);
		PreloadSound(VO_Ragnaros_Male_Elemental_Brawl_07);
		PreloadSound(VO_Ahune_Male_Elemental_Brawl_18);
		PreloadSound(VO_Ahune_Male_Elemental_Brawl_20);
		PreloadSound(VO_Ahune_Male_Elemental_Brawl_25);
	}

	private void Start()
	{
		friendlySidePlayer = GameState.Get().GetFriendlySidePlayer();
	}

	private void GetRagnaros()
	{
		int tag = GameState.Get().GetGameEntity().GetTag(GAME_TAG.TAG_SCRIPT_DATA_ENT_2);
		Entity entity = GameState.Get().GetEntity(tag);
		if (entity != null)
		{
			ragnarosCard = entity.GetCard();
		}
		if (ragnarosCard != null)
		{
			ragnarosActor = ragnarosCard.GetActor();
		}
	}

	private void SetPopupPosition()
	{
		if (friendlySidePlayer.IsCurrentPlayer())
		{
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				popUpPos.z = -66f;
			}
			else
			{
				popUpPos.z = -44f;
			}
		}
		else if ((bool)UniversalInputManager.UsePhoneUI)
		{
			popUpPos.z = 66f;
		}
		else
		{
			popUpPos.z = 44f;
		}
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		GetRagnaros();
		NameBanner banner = Gameplay.Get().GetNameBannerForSide(Player.Side.OPPOSING);
		switch (missionEvent)
		{
		case 10:
			yield return PlayBossLine(BOSS.AHUNE, VO_AHUNE_Male_Elemental_HSFireFestival_02);
			yield return PlayBossLineGameOver(BOSS.RAGNAROS, VO_RAGNAROS_Male_Elemental_AhuneResponses_01);
			yield return new WaitForSeconds(4f);
			banner.SetName(GameStrings.Get("TB_FIREFEST2_02"));
			break;
		case 11:
			GetRagnaros();
			Debug.Log("Case11");
			yield return PlayBossLine(BOSS.AHUNE, VO_AHUNE_Male_Elemental_HSFireFestival_04);
			yield return PlayBossLineGameOver(BOSS.RAGNAROS, VO_RAGNAROS_Male_Elemental_AhuneResponses_02);
			yield return PlayBossLine(BOSS.AHUNE, VO_AHUNE_Male_Elemental_HSFireFestival_05);
			yield return PlayBossLineGameOver(BOSS.RAGNAROS, VO_RAGNAROS_Male_Elemental_AhuneResponses_05);
			banner.SetName(GameStrings.Get("TB_FIREFEST2_02"));
			break;
		case 14:
			yield return PlayBossLineGameOver(BOSS.RAGNAROS, VO_Ragnaros_Male_Elemental_Brawl_01);
			banner.SetName(GameStrings.Get("TB_FIREFEST2_01"));
			break;
		case 13:
			yield return PlayBossLineGameOver(BOSS.RAGNAROS, VO_Ragnaros_Male_Elemental_Brawl_02);
			banner.SetName(GameStrings.Get("TB_FIREFEST2_01"));
			break;
		case 16:
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_Ahune_Male_Elemental_Brawl_18, Notification.SpeechBubbleDirection.TopLeft, ragnarosActor));
			break;
		}
	}

	private IEnumerator ShowPopup(string displayString)
	{
		StartPopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, popUpPos, TutorialEntity.GetTextScale(), GameStrings.Get(displayString), convertLegacyPosition: false);
		NotificationManager.Get().DestroyNotification(StartPopup, 7f);
		GameState.Get().SetBusy(busy: true);
		yield return new WaitForSeconds(2f);
		GameState.Get().SetBusy(busy: false);
	}

	private IEnumerator PlayBossLineGameOver(BOSS boss, string line, bool persistCharacter = false)
	{
		Notification.SpeechBubbleDirection direction = Notification.SpeechBubbleDirection.TopRight;
		switch (boss)
		{
		case BOSS.AHUNE:
			yield return PlayMissionFlavorLine("Ahune_BigQuote.prefab:00dd8f83adda33345ac291cc76241482", line, RIGHT_OF_ENEMY_HERO, direction, 2.5f, persistCharacter);
			break;
		case BOSS.RAGNAROS:
			yield return PlayMissionFlavorLine("Ragnaros_BigQuote.prefab:843c4fab946192943a909b026f755505", line, RIGHT_OF_ENEMY_HERO, direction, 2.5f, persistCharacter);
			break;
		}
	}

	private IEnumerator PlayBossLine(BOSS boss, string line, bool persistCharacter = false)
	{
		Notification.SpeechBubbleDirection direction = Notification.SpeechBubbleDirection.TopLeft;
		switch (boss)
		{
		case BOSS.AHUNE:
			yield return PlayMissionFlavorLine("Ahune_BigQuote.prefab:00dd8f83adda33345ac291cc76241482", line, LEFT_OF_ENEMY_HERO, direction, 2.5f, persistCharacter);
			break;
		case BOSS.RAGNAROS:
			yield return PlayMissionFlavorLine("Ragnaros_BigQuote.prefab:843c4fab946192943a909b026f755505", line, LEFT_OF_ENEMY_HERO, direction, 2.5f, persistCharacter);
			break;
		}
	}

	public override void NotifyOfGameOver(TAG_PLAYSTATE gameResult)
	{
		Debug.Log("gameresult is " + gameResult);
		switch (gameResult)
		{
		case TAG_PLAYSTATE.WON:
			matchResult = VICTOR.PLAYERWIN;
			break;
		case TAG_PLAYSTATE.LOST:
			Debug.Log("Made it to Playstate:Lost");
			matchResult = VICTOR.ELEMENTALSWIN;
			break;
		case TAG_PLAYSTATE.TIED:
			matchResult = VICTOR.ERROR;
			break;
		}
		base.NotifyOfGameOver(gameResult);
	}

	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		yield return new WaitForSeconds(2f);
		switch (matchResult)
		{
		case VICTOR.ELEMENTALSWIN:
			Debug.Log("elementals won");
			GameState.Get().SetBusy(busy: true);
			yield return new WaitForSeconds(3f);
			yield return PlayBossLine(BOSS.AHUNE, VO_Ahune_Male_Elemental_Brawl_25);
			yield return PlayBossLineGameOver(BOSS.RAGNAROS, VO_Ragnaros_Male_Elemental_Brawl_07);
			GameState.Get().SetBusy(busy: false);
			break;
		case VICTOR.PLAYERWIN:
			yield return new WaitForSeconds(3f);
			GameState.Get().SetBusy(busy: true);
			yield return PlayBossLine(BOSS.AHUNE, VO_Ahune_Male_Elemental_Brawl_20);
			yield return PlayBossLineGameOver(BOSS.RAGNAROS, VO_Ragnaros_Male_Elemental_Brawl_05);
			GameState.Get().SetBusy(busy: false);
			break;
		}
	}
}
