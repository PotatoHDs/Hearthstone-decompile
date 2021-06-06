using System.Collections;
using UnityEngine;

public class TB_HeadlessHorseman : MissionEntity
{
	private Actor horsemanActor;

	private Card horsemanCard;

	private Actor headActor;

	private Card headCard;

	private bool isHeadInPlay;

	private bool isHorsemanInPlay;

	private bool hasSpoken;

	private static readonly AssetReference VO_CS2_222_Attack_02 = new AssetReference("VO_CS2_222_Attack_02.prefab:c3191e3764f78654899b70a311936b93");

	private static readonly AssetReference VO_HeadlessHorseman_Male_Human_HallowsEve_01 = new AssetReference("VO_HeadlessHorseman_Male_Human_HallowsEve_01.prefab:95ede70d25607fd47923f829d4e5de42");

	private static readonly AssetReference VO_HeadlessHorseman_Male_Human_HallowsEve_02 = new AssetReference("VO_HeadlessHorseman_Male_Human_HallowsEve_02.prefab:35efdbdae6db14745bb99a6bf351634a");

	private static readonly AssetReference VO_HeadlessHorseman_Male_Human_HallowsEve_04 = new AssetReference("VO_HeadlessHorseman_Male_Human_HallowsEve_04.prefab:6324e43e11bdb2448ac1de3c8d07d048");

	private static readonly AssetReference VO_HeadlessHorseman_Male_Human_HallowsEve_06 = new AssetReference("VO_HeadlessHorseman_Male_Human_HallowsEve_06.prefab:36e1b30c8aceaf04992bc6cf0959d9c6");

	private static readonly AssetReference VO_HeadlessHorseman_Male_Human_HallowsEve_07 = new AssetReference("VO_HeadlessHorseman_Male_Human_HallowsEve_07.prefab:b88173866204fce4da7cbb3f7dddc915");

	private static readonly AssetReference VO_HeadlessHorseman_Male_Human_HallowsEve_08 = new AssetReference("VO_HeadlessHorseman_Male_Human_HallowsEve_08.prefab:dc0854d9ad190c84dbbea8f4b0f99dfe");

	private static readonly AssetReference VO_HeadlessHorseman_Male_Human_HallowsEve_10 = new AssetReference("VO_HeadlessHorseman_Male_Human_HallowsEve_10.prefab:92570580735ed754589290f4df5058bb");

	private static readonly AssetReference VO_HeadlessHorseman_Male_Human_HallowsEve_12 = new AssetReference("VO_HeadlessHorseman_Male_Human_HallowsEve_12.prefab:5b588aa7f8994a04eaf048ca87d73807");

	private static readonly AssetReference VO_HeadlessHorseman_Male_Human_HallowsEve_13 = new AssetReference("VO_HeadlessHorseman_Male_Human_HallowsEve_13.prefab:a015bfc61fca6a0489f276e3e2fbb0a3");

	private static readonly AssetReference VO_HeadlessHorseman_Male_Human_HallowsEve_17 = new AssetReference("VO_HeadlessHorseman_Male_Human_HallowsEve_17.prefab:84d6d0029d5f72542926291be8e40b39");

	private static readonly AssetReference VO_HeadlessHorseman_Male_Human_HallowsEve_19 = new AssetReference("VO_HeadlessHorseman_Male_Human_HallowsEve_19.prefab:74a92ec2af554f94fb8c6e205c561bde");

	private Vector3 popUpPos;

	private Notification StartPopup;

	private int _announcerLinesPlayed;

	private int _fireballDialogDelay;

	private bool _hasPlayed10;

	private bool _hasPlayed11;

	private bool _hasPlayed12;

	private bool _hasPlayed13;

	private bool _hasPlayed14;

	private bool _hasPlayed15;

	private bool _hasPlayed16;

	private bool _hasPlayed17;

	private bool _hasPlayed18;

	private bool _hasPlayed19;

	private bool _hasPlayed20;

	private bool _hasPlayed21;

	public override void PreloadAssets()
	{
		PreloadSound(VO_HeadlessHorseman_Male_Human_HallowsEve_01);
		PreloadSound(VO_HeadlessHorseman_Male_Human_HallowsEve_02);
		PreloadSound(VO_HeadlessHorseman_Male_Human_HallowsEve_04);
		PreloadSound(VO_HeadlessHorseman_Male_Human_HallowsEve_06);
		PreloadSound(VO_HeadlessHorseman_Male_Human_HallowsEve_07);
		PreloadSound(VO_HeadlessHorseman_Male_Human_HallowsEve_08);
		PreloadSound(VO_HeadlessHorseman_Male_Human_HallowsEve_10);
		PreloadSound(VO_HeadlessHorseman_Male_Human_HallowsEve_12);
		PreloadSound(VO_HeadlessHorseman_Male_Human_HallowsEve_13);
		PreloadSound(VO_HeadlessHorseman_Male_Human_HallowsEve_17);
		PreloadSound(VO_HeadlessHorseman_Male_Human_HallowsEve_19);
		PreloadSound(VO_CS2_222_Attack_02);
	}

	private void GetHorsemanHead()
	{
		isHeadInPlay = false;
		int tag = GameState.Get().GetGameEntity().GetTag(GAME_TAG.TAG_SCRIPT_DATA_ENT_1);
		if (tag != 0)
		{
			Entity entity = GameState.Get().GetEntity(tag);
			if (entity != null)
			{
				isHeadInPlay = entity.GetZone() == TAG_ZONE.PLAY;
			}
			if (entity != null)
			{
				headCard = entity.GetCard();
			}
			if (headCard != null)
			{
				headActor = headCard.GetActor();
			}
		}
	}

	private void GetHorseman()
	{
		int tag = GameState.Get().GetGameEntity().GetTag(GAME_TAG.TAG_SCRIPT_DATA_ENT_2);
		Entity entity = GameState.Get().GetEntity(tag);
		if (tag != 0)
		{
			if (entity != null)
			{
				horsemanCard = entity.GetCard();
				isHorsemanInPlay = entity.GetZone() == TAG_ZONE.PLAY;
			}
			if (horsemanCard != null)
			{
				horsemanActor = horsemanCard.GetActor();
			}
		}
	}

	public override AudioSource GetAnnouncerLine(Card heroCard, Card.AnnouncerLineType type)
	{
		_announcerLinesPlayed++;
		return _announcerLinesPlayed switch
		{
			1 => GetPreloadedSound(VO_CS2_222_Attack_02), 
			2 => GetPreloadedSound(VO_HeadlessHorseman_Male_Human_HallowsEve_19), 
			_ => base.GetAnnouncerLine(heroCard, type), 
		};
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		if (missionEvent == 15)
		{
			GetHorsemanHead();
			GetHorseman();
		}
		while (m_enemySpeaking)
		{
			yield return null;
		}
		if (hasSpoken && missionEvent != 99)
		{
			yield break;
		}
		if (missionEvent != 15)
		{
			GetHorsemanHead();
			GetHorseman();
		}
		switch (missionEvent)
		{
		case 10:
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_HeadlessHorseman_Male_Human_HallowsEve_04, Notification.SpeechBubbleDirection.TopRight, horsemanActor));
			yield return new WaitForSeconds(2f);
			GameState.Get().SetBusy(busy: false);
			hasSpoken = true;
			break;
		case 11:
			if (isHeadInPlay)
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_HeadlessHorseman_Male_Human_HallowsEve_08, Notification.SpeechBubbleDirection.TopRight, headActor));
			}
			else if (isHorsemanInPlay)
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_HeadlessHorseman_Male_Human_HallowsEve_08, Notification.SpeechBubbleDirection.TopRight, horsemanActor));
			}
			GameState.Get().SetBusy(busy: true);
			yield return new WaitForSeconds(5f);
			GameState.Get().SetBusy(busy: false);
			hasSpoken = true;
			break;
		case 12:
			_fireballDialogDelay++;
			if (_fireballDialogDelay > 1 && _fireballDialogDelay <= 2)
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_HeadlessHorseman_Male_Human_HallowsEve_02, Notification.SpeechBubbleDirection.TopRight, headActor));
				GameState.Get().SetBusy(busy: true);
				yield return new WaitForSeconds(2.5f);
				GameState.Get().SetBusy(busy: false);
				hasSpoken = true;
			}
			break;
		case 13:
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_HeadlessHorseman_Male_Human_HallowsEve_10, Notification.SpeechBubbleDirection.TopRight, horsemanActor));
			GameState.Get().SetBusy(busy: true);
			yield return new WaitForSeconds(4f);
			GameState.Get().SetBusy(busy: false);
			hasSpoken = true;
			break;
		case 14:
			if (!_hasPlayed14)
			{
				_hasPlayed14 = true;
				if (isHeadInPlay)
				{
					Debug.LogWarning(isHeadInPlay);
					Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_HeadlessHorseman_Male_Human_HallowsEve_13, Notification.SpeechBubbleDirection.TopRight, headActor));
				}
				else if (isHorsemanInPlay)
				{
					Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_HeadlessHorseman_Male_Human_HallowsEve_13, Notification.SpeechBubbleDirection.TopRight, horsemanActor));
				}
				GameState.Get().SetBusy(busy: true);
				yield return new WaitForSeconds(4f);
				GameState.Get().SetBusy(busy: false);
				hasSpoken = true;
			}
			break;
		case 15:
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_HeadlessHorseman_Male_Human_HallowsEve_17, Notification.SpeechBubbleDirection.TopRight, headActor));
			GameState.Get().SetBusy(busy: true);
			yield return new WaitForSeconds(3f);
			GameState.Get().SetBusy(busy: false);
			hasSpoken = true;
			break;
		case 16:
			if (isHeadInPlay && isHorsemanInPlay && !_hasPlayed16)
			{
				_hasPlayed16 = true;
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_HeadlessHorseman_Male_Human_HallowsEve_01, Notification.SpeechBubbleDirection.TopRight, headActor));
				GameState.Get().SetBusy(busy: true);
				yield return new WaitForSeconds(5f);
				GameState.Get().SetBusy(busy: false);
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_HeadlessHorseman_Male_Human_HallowsEve_10, Notification.SpeechBubbleDirection.TopRight, horsemanActor));
				GameState.Get().SetBusy(busy: true);
				yield return new WaitForSeconds(2f);
				GameState.Get().SetBusy(busy: false);
				hasSpoken = true;
			}
			break;
		case 17:
			if (!_hasPlayed17)
			{
				_hasPlayed17 = true;
				if (isHeadInPlay)
				{
					Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_HeadlessHorseman_Male_Human_HallowsEve_12, Notification.SpeechBubbleDirection.TopRight, headActor));
				}
				else if (isHorsemanInPlay)
				{
					Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_HeadlessHorseman_Male_Human_HallowsEve_12, Notification.SpeechBubbleDirection.TopRight, horsemanActor));
				}
				GameState.Get().SetBusy(busy: true);
				yield return new WaitForSeconds(3f);
				GameState.Get().SetBusy(busy: false);
				hasSpoken = true;
			}
			break;
		case 18:
			if (!_hasPlayed18)
			{
				_hasPlayed18 = true;
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_HeadlessHorseman_Male_Human_HallowsEve_07, Notification.SpeechBubbleDirection.TopRight, headActor));
				GameState.Get().SetBusy(busy: true);
				yield return new WaitForSeconds(3f);
				GameState.Get().SetBusy(busy: false);
				hasSpoken = true;
			}
			break;
		case 20:
			popUpPos = new Vector3(0f, 0f, 0f);
			StartPopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, popUpPos, TutorialEntity.GetTextScale(), GameStrings.Get("TB_HEADLESS_HORSEMAN_POISON"), convertLegacyPosition: false);
			NotificationManager.Get().DestroyNotification(StartPopup, 5f);
			GameState.Get().SetBusy(busy: true);
			yield return new WaitForSeconds(2f);
			GameState.Get().SetBusy(busy: false);
			hasSpoken = true;
			break;
		case 99:
			hasSpoken = false;
			break;
		}
	}
}
