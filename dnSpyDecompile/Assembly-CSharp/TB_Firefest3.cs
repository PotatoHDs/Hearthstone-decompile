using System;
using System.Collections;
using UnityEngine;

// Token: 0x020005AD RID: 1453
public class TB_Firefest3 : MissionEntity
{
	// Token: 0x060050A4 RID: 20644 RVA: 0x001A7F26 File Offset: 0x001A6126
	public override void PreloadAssets()
	{
		base.PreloadSound(TB_Firefest3.VO_Rakanishu_Male_Elemental_FF_Start_02);
	}

	// Token: 0x060050A5 RID: 20645 RVA: 0x001A7F38 File Offset: 0x001A6138
	private void GetHorsemanHead()
	{
		int tag = GameState.Get().GetGameEntity().GetTag(GAME_TAG.TAG_SCRIPT_DATA_ENT_1);
		if (tag == 0)
		{
			return;
		}
		Entity entity = GameState.Get().GetEntity(tag);
		if (entity != null)
		{
			this.headCard = entity.GetCard();
		}
		if (this.headCard != null)
		{
			this.headActor = this.headCard.GetActor();
		}
	}

	// Token: 0x060050A6 RID: 20646 RVA: 0x001A7F94 File Offset: 0x001A6194
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		if (missionEvent == 15)
		{
			this.GetHorsemanHead();
		}
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		if (missionEvent != 15)
		{
			this.GetHorsemanHead();
		}
		if (missionEvent == 10)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(TB_Firefest3.VO_Rakanishu_Male_Elemental_FF_Start_02, Notification.SpeechBubbleDirection.TopRight, this.headActor, 3f, 1f, true, false, 0f));
			GameState.Get().SetBusy(true);
			yield return new WaitForSeconds(5f);
			GameState.Get().SetBusy(false);
		}
		yield break;
	}

	// Token: 0x040046F7 RID: 18167
	private Actor horsemanActor;

	// Token: 0x040046F8 RID: 18168
	private Card horsemanCard;

	// Token: 0x040046F9 RID: 18169
	private Actor headActor;

	// Token: 0x040046FA RID: 18170
	private Card headCard;

	// Token: 0x040046FB RID: 18171
	private bool isHorsemanInPlay;

	// Token: 0x040046FC RID: 18172
	private static readonly AssetReference VO_Rakanishu_Male_Elemental_FF_Start_02 = new AssetReference("VO_Rakanishu_Male_Elemental_FF_Start_02.prefab:8985db50d3217a349812bd24624db30d");
}
