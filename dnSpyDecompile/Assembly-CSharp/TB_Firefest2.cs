using System;
using System.Collections;
using UnityEngine;

// Token: 0x020005AC RID: 1452
public class TB_Firefest2 : MissionEntity
{
	// Token: 0x06005098 RID: 20632 RVA: 0x001A7B7C File Offset: 0x001A5D7C
	public override void PreloadAssets()
	{
		base.PreloadSound(TB_Firefest2.VO_AHUNE_Male_Elemental_HSFireFestival_02);
		base.PreloadSound(TB_Firefest2.VO_AHUNE_Male_Elemental_HSFireFestival_04);
		base.PreloadSound(TB_Firefest2.VO_AHUNE_Male_Elemental_HSFireFestival_05);
		base.PreloadSound(TB_Firefest2.VO_RAGNAROS_Male_Elemental_AhuneResponses_01);
		base.PreloadSound(TB_Firefest2.VO_RAGNAROS_Male_Elemental_AhuneResponses_02);
		base.PreloadSound(TB_Firefest2.VO_RAGNAROS_Male_Elemental_AhuneResponses_05);
		base.PreloadSound(TB_Firefest2.VO_Ragnaros_Male_Elemental_Brawl_01);
		base.PreloadSound(TB_Firefest2.VO_Ragnaros_Male_Elemental_Brawl_02);
		base.PreloadSound(TB_Firefest2.VO_Ragnaros_Male_Elemental_Brawl_05);
		base.PreloadSound(TB_Firefest2.VO_Ragnaros_Male_Elemental_Brawl_07);
		base.PreloadSound(TB_Firefest2.VO_Ahune_Male_Elemental_Brawl_18);
		base.PreloadSound(TB_Firefest2.VO_Ahune_Male_Elemental_Brawl_20);
		base.PreloadSound(TB_Firefest2.VO_Ahune_Male_Elemental_Brawl_25);
	}

	// Token: 0x06005099 RID: 20633 RVA: 0x001A7C59 File Offset: 0x001A5E59
	private void Start()
	{
		this.friendlySidePlayer = GameState.Get().GetFriendlySidePlayer();
	}

	// Token: 0x0600509A RID: 20634 RVA: 0x001A7C6C File Offset: 0x001A5E6C
	private void GetRagnaros()
	{
		int tag = GameState.Get().GetGameEntity().GetTag(GAME_TAG.TAG_SCRIPT_DATA_ENT_2);
		Entity entity = GameState.Get().GetEntity(tag);
		if (entity != null)
		{
			this.ragnarosCard = entity.GetCard();
		}
		if (this.ragnarosCard != null)
		{
			this.ragnarosActor = this.ragnarosCard.GetActor();
		}
	}

	// Token: 0x0600509B RID: 20635 RVA: 0x001A7CC4 File Offset: 0x001A5EC4
	private void SetPopupPosition()
	{
		if (this.friendlySidePlayer.IsCurrentPlayer())
		{
			if (UniversalInputManager.UsePhoneUI)
			{
				this.popUpPos.z = -66f;
				return;
			}
			this.popUpPos.z = -44f;
			return;
		}
		else
		{
			if (UniversalInputManager.UsePhoneUI)
			{
				this.popUpPos.z = 66f;
				return;
			}
			this.popUpPos.z = 44f;
			return;
		}
	}

	// Token: 0x0600509C RID: 20636 RVA: 0x001A7D39 File Offset: 0x001A5F39
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		this.GetRagnaros();
		NameBanner banner = Gameplay.Get().GetNameBannerForSide(Player.Side.OPPOSING);
		switch (missionEvent)
		{
		case 10:
			yield return this.PlayBossLine(TB_Firefest2.BOSS.AHUNE, TB_Firefest2.VO_AHUNE_Male_Elemental_HSFireFestival_02, false);
			yield return this.PlayBossLineGameOver(TB_Firefest2.BOSS.RAGNAROS, TB_Firefest2.VO_RAGNAROS_Male_Elemental_AhuneResponses_01, false);
			yield return new WaitForSeconds(4f);
			banner.SetName(GameStrings.Get("TB_FIREFEST2_02"));
			break;
		case 11:
			this.GetRagnaros();
			Debug.Log("Case11");
			yield return this.PlayBossLine(TB_Firefest2.BOSS.AHUNE, TB_Firefest2.VO_AHUNE_Male_Elemental_HSFireFestival_04, false);
			yield return this.PlayBossLineGameOver(TB_Firefest2.BOSS.RAGNAROS, TB_Firefest2.VO_RAGNAROS_Male_Elemental_AhuneResponses_02, false);
			yield return this.PlayBossLine(TB_Firefest2.BOSS.AHUNE, TB_Firefest2.VO_AHUNE_Male_Elemental_HSFireFestival_05, false);
			yield return this.PlayBossLineGameOver(TB_Firefest2.BOSS.RAGNAROS, TB_Firefest2.VO_RAGNAROS_Male_Elemental_AhuneResponses_05, false);
			banner.SetName(GameStrings.Get("TB_FIREFEST2_02"));
			break;
		case 13:
			yield return this.PlayBossLineGameOver(TB_Firefest2.BOSS.RAGNAROS, TB_Firefest2.VO_Ragnaros_Male_Elemental_Brawl_02, false);
			banner.SetName(GameStrings.Get("TB_FIREFEST2_01"));
			break;
		case 14:
			yield return this.PlayBossLineGameOver(TB_Firefest2.BOSS.RAGNAROS, TB_Firefest2.VO_Ragnaros_Male_Elemental_Brawl_01, false);
			banner.SetName(GameStrings.Get("TB_FIREFEST2_01"));
			break;
		case 16:
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(TB_Firefest2.VO_Ahune_Male_Elemental_Brawl_18, Notification.SpeechBubbleDirection.TopLeft, this.ragnarosActor, 3f, 1f, true, false, 0f));
			break;
		}
		yield break;
	}

	// Token: 0x0600509D RID: 20637 RVA: 0x001A7D4F File Offset: 0x001A5F4F
	private IEnumerator ShowPopup(string displayString)
	{
		this.StartPopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, this.popUpPos, TutorialEntity.GetTextScale(), GameStrings.Get(displayString), false, NotificationManager.PopupTextType.BASIC);
		NotificationManager.Get().DestroyNotification(this.StartPopup, 7f);
		GameState.Get().SetBusy(true);
		yield return new WaitForSeconds(2f);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x0600509E RID: 20638 RVA: 0x001A7D65 File Offset: 0x001A5F65
	private IEnumerator PlayBossLineGameOver(TB_Firefest2.BOSS boss, string line, bool persistCharacter = false)
	{
		Notification.SpeechBubbleDirection direction = Notification.SpeechBubbleDirection.TopRight;
		if (boss != TB_Firefest2.BOSS.AHUNE)
		{
			if (boss == TB_Firefest2.BOSS.RAGNAROS)
			{
				yield return base.PlayMissionFlavorLine("Ragnaros_BigQuote.prefab:843c4fab946192943a909b026f755505", line, TB_Firefest2.RIGHT_OF_ENEMY_HERO, direction, 2.5f, persistCharacter);
			}
		}
		else
		{
			yield return base.PlayMissionFlavorLine("Ahune_BigQuote.prefab:00dd8f83adda33345ac291cc76241482", line, TB_Firefest2.RIGHT_OF_ENEMY_HERO, direction, 2.5f, persistCharacter);
		}
		yield break;
	}

	// Token: 0x0600509F RID: 20639 RVA: 0x001A7D89 File Offset: 0x001A5F89
	private IEnumerator PlayBossLine(TB_Firefest2.BOSS boss, string line, bool persistCharacter = false)
	{
		Notification.SpeechBubbleDirection direction = Notification.SpeechBubbleDirection.TopLeft;
		if (boss != TB_Firefest2.BOSS.AHUNE)
		{
			if (boss == TB_Firefest2.BOSS.RAGNAROS)
			{
				yield return base.PlayMissionFlavorLine("Ragnaros_BigQuote.prefab:843c4fab946192943a909b026f755505", line, TB_Firefest2.LEFT_OF_ENEMY_HERO, direction, 2.5f, persistCharacter);
			}
		}
		else
		{
			yield return base.PlayMissionFlavorLine("Ahune_BigQuote.prefab:00dd8f83adda33345ac291cc76241482", line, TB_Firefest2.LEFT_OF_ENEMY_HERO, direction, 2.5f, persistCharacter);
		}
		yield break;
	}

	// Token: 0x060050A0 RID: 20640 RVA: 0x001A7DB0 File Offset: 0x001A5FB0
	public override void NotifyOfGameOver(TAG_PLAYSTATE gameResult)
	{
		Debug.Log("gameresult is " + gameResult);
		switch (gameResult)
		{
		case TAG_PLAYSTATE.WON:
			this.matchResult = TB_Firefest2.VICTOR.PLAYERWIN;
			break;
		case TAG_PLAYSTATE.LOST:
			Debug.Log("Made it to Playstate:Lost");
			this.matchResult = TB_Firefest2.VICTOR.ELEMENTALSWIN;
			break;
		case TAG_PLAYSTATE.TIED:
			this.matchResult = TB_Firefest2.VICTOR.ERROR;
			break;
		}
		base.NotifyOfGameOver(gameResult);
	}

	// Token: 0x060050A1 RID: 20641 RVA: 0x001A7E12 File Offset: 0x001A6012
	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		yield return new WaitForSeconds(2f);
		switch (this.matchResult)
		{
		case TB_Firefest2.VICTOR.ELEMENTALSWIN:
			Debug.Log("elementals won");
			GameState.Get().SetBusy(true);
			yield return new WaitForSeconds(3f);
			yield return this.PlayBossLine(TB_Firefest2.BOSS.AHUNE, TB_Firefest2.VO_Ahune_Male_Elemental_Brawl_25, false);
			yield return this.PlayBossLineGameOver(TB_Firefest2.BOSS.RAGNAROS, TB_Firefest2.VO_Ragnaros_Male_Elemental_Brawl_07, false);
			GameState.Get().SetBusy(false);
			break;
		case TB_Firefest2.VICTOR.PLAYERWIN:
			yield return new WaitForSeconds(3f);
			GameState.Get().SetBusy(true);
			yield return this.PlayBossLine(TB_Firefest2.BOSS.AHUNE, TB_Firefest2.VO_Ahune_Male_Elemental_Brawl_20, false);
			yield return this.PlayBossLineGameOver(TB_Firefest2.BOSS.RAGNAROS, TB_Firefest2.VO_Ragnaros_Male_Elemental_Brawl_05, false);
			GameState.Get().SetBusy(false);
			break;
		}
		yield break;
	}

	// Token: 0x040046DF RID: 18143
	private Actor ragnarosActor;

	// Token: 0x040046E0 RID: 18144
	private Card ragnarosCard;

	// Token: 0x040046E1 RID: 18145
	private bool isHeadInPlay;

	// Token: 0x040046E2 RID: 18146
	private bool hasSpoken;

	// Token: 0x040046E3 RID: 18147
	private Vector3 popUpPos;

	// Token: 0x040046E4 RID: 18148
	private TB_Firefest2.VICTOR matchResult;

	// Token: 0x040046E5 RID: 18149
	private Notification StartPopup;

	// Token: 0x040046E6 RID: 18150
	private static readonly AssetReference VO_AHUNE_Male_Elemental_HSFireFestival_02 = new AssetReference("VO_AHUNE_Male_Elemental_HSFireFestival_02:8dc670a0c96a23d44bc6b87957b223fe");

	// Token: 0x040046E7 RID: 18151
	private static readonly AssetReference VO_AHUNE_Male_Elemental_HSFireFestival_04 = new AssetReference("VO_AHUNE_Male_Elemental_HSFireFestival_04:63a6f5920298e39418087cbfb837e9af");

	// Token: 0x040046E8 RID: 18152
	private static readonly AssetReference VO_AHUNE_Male_Elemental_HSFireFestival_05 = new AssetReference("VO_AHUNE_Male_Elemental_HSFireFestival_05:4a15da86b328e8c4ea5a805f9080f8c5");

	// Token: 0x040046E9 RID: 18153
	private static readonly AssetReference VO_RAGNAROS_Male_Elemental_AhuneResponses_01 = new AssetReference("VO_RAGNAROS_Male_Elemental_AhuneResponses_01:0de84fe30f9c4c04dbc1f996bd2694b3");

	// Token: 0x040046EA RID: 18154
	private static readonly AssetReference VO_RAGNAROS_Male_Elemental_AhuneResponses_02 = new AssetReference("VO_RAGNAROS_Male_Elemental_AhuneResponses_02:58a0b7d69171f57409999d3b984c54d9");

	// Token: 0x040046EB RID: 18155
	private static readonly AssetReference VO_RAGNAROS_Male_Elemental_AhuneResponses_05 = new AssetReference("VO_RAGNAROS_Male_Elemental_AhuneResponses_05:31f75d15dc1a7f34bafcfbdde1c9f2a1");

	// Token: 0x040046EC RID: 18156
	private static readonly AssetReference VO_Ragnaros_Male_Elemental_Brawl_01 = new AssetReference("VO_Ragnaros_Male_Elemental_Brawl_01:da09dbd1ad9ba434fbb549c8bbd2c9ce");

	// Token: 0x040046ED RID: 18157
	private static readonly AssetReference VO_Ragnaros_Male_Elemental_Brawl_02 = new AssetReference("VO_Ragnaros_Male_Elemental_Brawl_02:b5630abf5a135384695d1f58fa025fe5");

	// Token: 0x040046EE RID: 18158
	private static readonly AssetReference VO_Ragnaros_Male_Elemental_Brawl_05 = new AssetReference("VO_Ragnaros_Male_Elemental_Brawl_05:4455f90db8e99eb45bc158677acb672e");

	// Token: 0x040046EF RID: 18159
	private static readonly AssetReference VO_Ragnaros_Male_Elemental_Brawl_07 = new AssetReference("VO_Ragnaros_Male_Elemental_Brawl_07:0934d1efe1db28041be7f03b4295ffdd");

	// Token: 0x040046F0 RID: 18160
	private static readonly AssetReference VO_Ahune_Male_Elemental_Brawl_18 = new AssetReference("VO_Ahune_Male_Elemental_Brawl_18:49d4e9ef35728e84cb171df7cc56a32b");

	// Token: 0x040046F1 RID: 18161
	private static readonly AssetReference VO_Ahune_Male_Elemental_Brawl_20 = new AssetReference("VO_Ahune_Male_Elemental_Brawl_20:5fb1142388aecbd4588f2ca08d8f391a");

	// Token: 0x040046F2 RID: 18162
	private static readonly AssetReference VO_Ahune_Male_Elemental_Brawl_25 = new AssetReference("VO_Ahune_Male_Elemental_Brawl_25:c280663239e28fe419072cc64df39098");

	// Token: 0x040046F3 RID: 18163
	private static readonly Vector3 LEFT_OF_ENEMY_HERO = new Vector3(-1f, 0f, -2.8f);

	// Token: 0x040046F4 RID: 18164
	private static readonly Vector3 RIGHT_OF_ENEMY_HERO = new Vector3(-6f, 0f, -2.8f);

	// Token: 0x040046F5 RID: 18165
	private Player friendlySidePlayer;

	// Token: 0x040046F6 RID: 18166
	private Entity playerEntity;

	// Token: 0x02001FA8 RID: 8104
	private enum VICTOR
	{
		// Token: 0x0400DA11 RID: 55825
		ELEMENTALSWIN,
		// Token: 0x0400DA12 RID: 55826
		PLAYERWIN,
		// Token: 0x0400DA13 RID: 55827
		ERROR
	}

	// Token: 0x02001FA9 RID: 8105
	private enum BOSS
	{
		// Token: 0x0400DA15 RID: 55829
		AHUNE,
		// Token: 0x0400DA16 RID: 55830
		RAGNAROS
	}
}
