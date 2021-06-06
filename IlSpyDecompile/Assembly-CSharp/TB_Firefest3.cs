using System.Collections;
using UnityEngine;

public class TB_Firefest3 : MissionEntity
{
	private Actor horsemanActor;

	private Card horsemanCard;

	private Actor headActor;

	private Card headCard;

	private bool isHorsemanInPlay;

	private static readonly AssetReference VO_Rakanishu_Male_Elemental_FF_Start_02 = new AssetReference("VO_Rakanishu_Male_Elemental_FF_Start_02.prefab:8985db50d3217a349812bd24624db30d");

	public override void PreloadAssets()
	{
		PreloadSound(VO_Rakanishu_Male_Elemental_FF_Start_02);
	}

	private void GetHorsemanHead()
	{
		int tag = GameState.Get().GetGameEntity().GetTag(GAME_TAG.TAG_SCRIPT_DATA_ENT_1);
		if (tag != 0)
		{
			Entity entity = GameState.Get().GetEntity(tag);
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

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		if (missionEvent == 15)
		{
			GetHorsemanHead();
		}
		while (m_enemySpeaking)
		{
			yield return null;
		}
		if (missionEvent != 15)
		{
			GetHorsemanHead();
		}
		if (missionEvent == 10)
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_Rakanishu_Male_Elemental_FF_Start_02, Notification.SpeechBubbleDirection.TopRight, headActor));
			GameState.Get().SetBusy(busy: true);
			yield return new WaitForSeconds(5f);
			GameState.Get().SetBusy(busy: false);
		}
	}
}
