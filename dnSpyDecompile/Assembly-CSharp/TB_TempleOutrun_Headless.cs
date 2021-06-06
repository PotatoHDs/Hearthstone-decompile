using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200047C RID: 1148
public class TB_TempleOutrun_Headless : ULDA_Dungeon
{
	// Token: 0x06003E1F RID: 15903 RVA: 0x00147D18 File Offset: 0x00145F18
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			TB_TempleOutrun_Headless.VO_HeadlessHorseman_Male_Human_HHIntro_02,
			TB_TempleOutrun_Headless.VO_HeadlessHorseman_Male_Human_HHReaction1_01,
			TB_TempleOutrun_Headless.VO_HeadlessHorseman_Male_Human_HallowsEve_19
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003E20 RID: 15904 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003E21 RID: 15905 RVA: 0x00147DAC File Offset: 0x00145FAC
	public override List<string> GetIdleLines()
	{
		return this.m_IdleLines;
	}

	// Token: 0x06003E22 RID: 15906 RVA: 0x00147DB4 File Offset: 0x00145FB4
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_HeroPowerLines;
	}

	// Token: 0x06003E23 RID: 15907 RVA: 0x00147DBC File Offset: 0x00145FBC
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_standardEmoteResponseLine = TB_TempleOutrun_Headless.VO_HeadlessHorseman_Male_Human_HallowsEve_19;
	}

	// Token: 0x06003E24 RID: 15908 RVA: 0x00147DD4 File Offset: 0x00145FD4
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_standardEmoteResponseLine, Notification.SpeechBubbleDirection.None, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x06003E25 RID: 15909 RVA: 0x00147E2E File Offset: 0x0014602E
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		if (missionEvent != 100)
		{
			if (missionEvent != 101)
			{
				if (missionEvent == 1010)
				{
					Debug.Log("Got Case 1010");
					int tag = GameState.Get().GetFriendlySidePlayer().GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_1);
					string msgString;
					if (tag == 0)
					{
						msgString = GameStrings.Get(TB_TempleOutrun_Headless.popupMsgs[2000].Message);
					}
					else
					{
						string text = "";
						string text2 = "";
						string text3 = "";
						int num = tag / 3600;
						int num2 = tag % 3600 / 60;
						int num3 = tag % 60;
						if (num < 10)
						{
							text = "0";
						}
						if (num2 < 10)
						{
							text2 = "0";
						}
						if (num3 < 10)
						{
							text3 = "0";
						}
						msgString = string.Concat(new object[]
						{
							GameStrings.Get(TB_TempleOutrun_Headless.popupMsgs[missionEvent].Message),
							"\n",
							text,
							num,
							":",
							text2,
							num2,
							":",
							text3,
							num3
						});
						this.popupScale = 1.7f;
					}
					Vector3 popUpPos = default(Vector3);
					if (GameState.Get().GetFriendlySidePlayer() != GameState.Get().GetCurrentPlayer())
					{
						popUpPos.z = (UniversalInputManager.UsePhoneUI ? 27f : 18f);
					}
					else
					{
						popUpPos.z = (UniversalInputManager.UsePhoneUI ? -40f : -40f);
					}
					yield return new WaitForSeconds(4f);
					yield return this.ShowPopup(msgString, TB_TempleOutrun_Headless.popupMsgs[missionEvent].Delay, popUpPos, this.popupScale);
					msgString = null;
					popUpPos = default(Vector3);
				}
			}
			else
			{
				Debug.Log("Got Case 101");
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(TB_TempleOutrun_Headless.VO_HeadlessHorseman_Male_Human_HHReaction1_01, Notification.SpeechBubbleDirection.TopRight, enemyActor, 3f, 1f, true, false, 0f));
			}
		}
		else
		{
			Debug.Log("Got Case 100");
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(TB_TempleOutrun_Headless.VO_HeadlessHorseman_Male_Human_HHIntro_02, Notification.SpeechBubbleDirection.TopRight, enemyActor, 3f, 1f, true, false, 0f));
		}
		yield break;
	}

	// Token: 0x06003E26 RID: 15910 RVA: 0x00147E44 File Offset: 0x00146044
	private IEnumerator ShowPopup(string stringID, float popupDuration, Vector3 popUpPos, float popupScale)
	{
		this.m_popup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, popUpPos, TutorialEntity.GetTextScale() * popupScale, GameStrings.Get(stringID), false, NotificationManager.PopupTextType.BASIC);
		NotificationManager.Get().DestroyNotification(this.m_popup, popupDuration);
		yield return new WaitForSeconds(0f);
		yield break;
	}

	// Token: 0x04002A01 RID: 10753
	private static readonly AssetReference VO_HeadlessHorseman_Male_Human_HHIntro_02 = new AssetReference("VO_HeadlessHorseman_Male_Human_HHIntro_02.prefab:0dc446d089c1c6142819ecd89009e9bf");

	// Token: 0x04002A02 RID: 10754
	private static readonly AssetReference VO_HeadlessHorseman_Male_Human_HHReaction1_01 = new AssetReference("VO_HeadlessHorseman_Male_Human_HHReaction1_01.prefab:8443a7874cc9cbb48a30f57d69e1b431");

	// Token: 0x04002A03 RID: 10755
	private static readonly AssetReference VO_HeadlessHorseman_Male_Human_HallowsEve_19 = new AssetReference("VO_HeadlessHorseman_Male_Human_HallowsEve_19.prefab:74a92ec2af554f94fb8c6e205c561bde");

	// Token: 0x04002A04 RID: 10756
	private List<string> m_HeroPowerLines = new List<string>();

	// Token: 0x04002A05 RID: 10757
	private List<string> m_IdleLines = new List<string>();

	// Token: 0x04002A06 RID: 10758
	private Notification m_popup;

	// Token: 0x04002A07 RID: 10759
	private float popupScale = 1.4f;

	// Token: 0x04002A08 RID: 10760
	private Entity playerEntity;

	// Token: 0x04002A09 RID: 10761
	private static readonly Dictionary<int, TB_TempleOutrun_Headless.PopupMessage> popupMsgs = new Dictionary<int, TB_TempleOutrun_Headless.PopupMessage>
	{
		{
			1000,
			new TB_TempleOutrun_Headless.PopupMessage
			{
				Message = "TB_EVILBRM_CURRENT_BEST_SCORE",
				Delay = 5f
			}
		},
		{
			2000,
			new TB_TempleOutrun_Headless.PopupMessage
			{
				Message = "TB_EVILBRM_NEW_BEST_SCORE",
				Delay = 5f
			}
		}
	};

	// Token: 0x02001A04 RID: 6660
	public struct PopupMessage
	{
		// Token: 0x0400BFFE RID: 49150
		public string Message;

		// Token: 0x0400BFFF RID: 49151
		public float Delay;
	}
}
