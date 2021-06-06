using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TB_TempleOutrun_Headless : ULDA_Dungeon
{
	public struct PopupMessage
	{
		public string Message;

		public float Delay;
	}

	private static readonly AssetReference VO_HeadlessHorseman_Male_Human_HHIntro_02 = new AssetReference("VO_HeadlessHorseman_Male_Human_HHIntro_02.prefab:0dc446d089c1c6142819ecd89009e9bf");

	private static readonly AssetReference VO_HeadlessHorseman_Male_Human_HHReaction1_01 = new AssetReference("VO_HeadlessHorseman_Male_Human_HHReaction1_01.prefab:8443a7874cc9cbb48a30f57d69e1b431");

	private static readonly AssetReference VO_HeadlessHorseman_Male_Human_HallowsEve_19 = new AssetReference("VO_HeadlessHorseman_Male_Human_HallowsEve_19.prefab:74a92ec2af554f94fb8c6e205c561bde");

	private List<string> m_HeroPowerLines = new List<string>();

	private List<string> m_IdleLines = new List<string>();

	private Notification m_popup;

	private float popupScale = 1.4f;

	private Entity playerEntity;

	private static readonly Dictionary<int, PopupMessage> popupMsgs;

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string> { VO_HeadlessHorseman_Male_Human_HHIntro_02, VO_HeadlessHorseman_Male_Human_HHReaction1_01, VO_HeadlessHorseman_Male_Human_HallowsEve_19 };
		SetBossVOLines(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
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
		m_standardEmoteResponseLine = VO_HeadlessHorseman_Male_Human_HallowsEve_19;
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(m_standardEmoteResponseLine, Notification.SpeechBubbleDirection.None, actor));
		}
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		while (m_enemySpeaking)
		{
			yield return null;
		}
		switch (missionEvent)
		{
		case 1010:
		{
			Debug.Log("Got Case 1010");
			int tag = GameState.Get().GetFriendlySidePlayer().GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_1);
			string msgString;
			if (tag == 0)
			{
				msgString = GameStrings.Get(popupMsgs[2000].Message);
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
				msgString = GameStrings.Get(popupMsgs[missionEvent].Message) + "\n" + text + num + ":" + text2 + num2 + ":" + text3 + num3;
				popupScale = 1.7f;
			}
			Vector3 popUpPos = default(Vector3);
			if (GameState.Get().GetFriendlySidePlayer() != GameState.Get().GetCurrentPlayer())
			{
				popUpPos.z = (UniversalInputManager.UsePhoneUI ? 27f : 18f);
			}
			else
			{
				popUpPos.z = (UniversalInputManager.UsePhoneUI ? (-40f) : (-40f));
			}
			yield return new WaitForSeconds(4f);
			yield return ShowPopup(msgString, popupMsgs[missionEvent].Delay, popUpPos, popupScale);
			popUpPos = default(Vector3);
			break;
		}
		case 100:
			Debug.Log("Got Case 100");
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_HeadlessHorseman_Male_Human_HHIntro_02, Notification.SpeechBubbleDirection.TopRight, enemyActor));
			break;
		case 101:
			Debug.Log("Got Case 101");
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_HeadlessHorseman_Male_Human_HHReaction1_01, Notification.SpeechBubbleDirection.TopRight, enemyActor));
			break;
		}
	}

	private IEnumerator ShowPopup(string stringID, float popupDuration, Vector3 popUpPos, float popupScale)
	{
		m_popup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, popUpPos, TutorialEntity.GetTextScale() * popupScale, GameStrings.Get(stringID), convertLegacyPosition: false);
		NotificationManager.Get().DestroyNotification(m_popup, popupDuration);
		yield return new WaitForSeconds(0f);
	}

	static TB_TempleOutrun_Headless()
	{
		Dictionary<int, PopupMessage> dictionary = new Dictionary<int, PopupMessage>();
		PopupMessage value = new PopupMessage
		{
			Message = "TB_EVILBRM_CURRENT_BEST_SCORE",
			Delay = 5f
		};
		dictionary.Add(1000, value);
		value = new PopupMessage
		{
			Message = "TB_EVILBRM_NEW_BEST_SCORE",
			Delay = 5f
		};
		dictionary.Add(2000, value);
		popupMsgs = dictionary;
	}
}
