using System.Collections;
using UnityEngine;

public class TB_MammothParty : MissionEntity
{
	private Notification StartPopup;

	private bool hasCrasherBeenDiscarded;

	private string textID10 = "TB_MP_COOP_TEXT_START";

	private string textID11 = "TB_MP_COOP_FIRST_CRASHER";

	private string textID12 = "TB_MP_COOP_PINATA";

	private string textID13 = "TB_MP_COOP_CRASHER_DISCARD";

	private string textID14 = "TB_MP_COOP_ENDING";

	private string textID15 = "TB_MP_COOP_1STSPELL";

	private string textID16 = "TB_MP_COOP_2NDSPELL";

	private Vector3 popUpPos;

	private void Start()
	{
	}

	private void Update()
	{
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		popUpPos = new Vector3(0f, 0f, 0f);
		switch (missionEvent)
		{
		case 10:
			StartPopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, popUpPos, TutorialEntity.GetTextScale() * 1.5f, GameStrings.Get(textID10), convertLegacyPosition: false);
			NotificationManager.Get().DestroyNotification(StartPopup, 7f);
			GameState.Get().SetBusy(busy: true);
			yield return new WaitForSeconds(2f);
			GameState.Get().SetBusy(busy: false);
			break;
		case 11:
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				popUpPos.z = -66f;
			}
			else
			{
				popUpPos.z = -44f;
			}
			StartPopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, popUpPos, TutorialEntity.GetTextScale() * 1.5f, GameStrings.Get(textID11), convertLegacyPosition: false);
			NotificationManager.Get().DestroyNotification(StartPopup, 7f);
			GameState.Get().SetBusy(busy: true);
			yield return new WaitForSeconds(2f);
			GameState.Get().SetBusy(busy: false);
			break;
		case 12:
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				popUpPos.z = -66f;
			}
			else
			{
				popUpPos.z = -44f;
			}
			StartPopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, popUpPos, TutorialEntity.GetTextScale() * 2f, GameStrings.Get(textID12), convertLegacyPosition: false);
			NotificationManager.Get().DestroyNotification(StartPopup, 5f);
			GameState.Get().SetBusy(busy: true);
			yield return new WaitForSeconds(6.5f);
			GameState.Get().SetBusy(busy: false);
			break;
		case 13:
			if (!hasCrasherBeenDiscarded)
			{
				hasCrasherBeenDiscarded = true;
				if ((bool)UniversalInputManager.UsePhoneUI)
				{
					popUpPos.z = -66f;
				}
				else
				{
					popUpPos.z = -44f;
				}
				StartPopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, popUpPos, TutorialEntity.GetTextScale() * 1.5f, GameStrings.Get(textID13), convertLegacyPosition: false);
				NotificationManager.Get().DestroyNotification(StartPopup, 7f);
				GameState.Get().SetBusy(busy: true);
				yield return new WaitForSeconds(2f);
				GameState.Get().SetBusy(busy: false);
			}
			break;
		case 14:
			StartPopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, popUpPos, TutorialEntity.GetTextScale() * 2f, GameStrings.Get(textID14), convertLegacyPosition: false);
			NotificationManager.Get().DestroyNotification(StartPopup, 7f);
			GameState.Get().SetBusy(busy: true);
			yield return new WaitForSeconds(7f);
			GameState.Get().SetBusy(busy: false);
			break;
		case 15:
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				popUpPos.z = -66f;
			}
			else
			{
				popUpPos.z = -44f;
			}
			StartPopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, popUpPos, TutorialEntity.GetTextScale() * 1.5f, GameStrings.Get(textID15), convertLegacyPosition: false);
			NotificationManager.Get().DestroyNotification(StartPopup, 7f);
			GameState.Get().SetBusy(busy: true);
			yield return new WaitForSeconds(2f);
			GameState.Get().SetBusy(busy: false);
			break;
		case 16:
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				popUpPos.z = -66f;
			}
			else
			{
				popUpPos.z = -44f;
			}
			StartPopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, popUpPos, TutorialEntity.GetTextScale() * 1.5f, GameStrings.Get(textID16), convertLegacyPosition: false);
			NotificationManager.Get().DestroyNotification(StartPopup, 7f);
			GameState.Get().SetBusy(busy: true);
			yield return new WaitForSeconds(2f);
			GameState.Get().SetBusy(busy: false);
			break;
		}
	}

	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}
}
