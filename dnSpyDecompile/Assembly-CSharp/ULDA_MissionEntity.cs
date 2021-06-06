using System;
using System.Collections;
using Hearthstone.DungeonCrawl;
using UnityEngine;

// Token: 0x020004CC RID: 1228
public abstract class ULDA_MissionEntity : GenericDungeonMissionEntity
{
	// Token: 0x060041B4 RID: 16820 RVA: 0x0015F612 File Offset: 0x0015D812
	public sealed override AdventureDbId GetAdventureID()
	{
		return AdventureDbId.ULDUM;
	}

	// Token: 0x060041B5 RID: 16821 RVA: 0x0015F619 File Offset: 0x0015D819
	protected sealed override bool CanPlayVOLines(Entity speakerEntity, GenericDungeonMissionEntity.VOSpeaker speaker)
	{
		if (speaker == GenericDungeonMissionEntity.VOSpeaker.FRIENDLY_HERO)
		{
			return speakerEntity.GetCardId().Contains("ULDA_");
		}
		return base.CanPlayVOLines(speakerEntity, speaker);
	}

	// Token: 0x060041B6 RID: 16822 RVA: 0x0015F638 File Offset: 0x0015D838
	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.LOST)
		{
			yield return new WaitForSeconds(5f);
			yield break;
		}
		yield break;
	}

	// Token: 0x060041B7 RID: 16823 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool ShouldShowHeroClassDuringMulligan(Player.Side playerSide)
	{
		return false;
	}

	// Token: 0x060041B8 RID: 16824 RVA: 0x0015F648 File Offset: 0x0015D848
	public override void OnPlayThinkEmote()
	{
		if (this.m_enemySpeaking)
		{
			return;
		}
		Player currentPlayer = GameState.Get().GetCurrentPlayer();
		if (!currentPlayer.IsFriendlySide())
		{
			return;
		}
		if (currentPlayer.GetHeroCard().HasActiveEmoteSound())
		{
			return;
		}
		EmoteType emoteType = EmoteType.THINK1;
		switch (UnityEngine.Random.Range(1, 4))
		{
		case 1:
			emoteType = EmoteType.THINK1;
			break;
		case 2:
			emoteType = EmoteType.THINK2;
			break;
		case 3:
			emoteType = EmoteType.THINK3;
			break;
		}
		GameState.Get().GetCurrentPlayer().GetHeroCard().PlayEmote(emoteType);
	}

	// Token: 0x060041B9 RID: 16825 RVA: 0x0015F6C4 File Offset: 0x0015D8C4
	public override void StartMulliganSoundtracks(bool soft)
	{
		if (!soft)
		{
			MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_ULDMulligan);
		}
	}

	// Token: 0x060041BA RID: 16826 RVA: 0x0015F6DC File Offset: 0x0015D8DC
	public int GetDefeatedBossCountForFinalBoss()
	{
		int missionId = GameMgr.Get().GetMissionId();
		if (missionId == 3432 || missionId == 3437)
		{
			return 0;
		}
		return 7;
	}

	// Token: 0x060041BB RID: 16827 RVA: 0x000C3B6A File Offset: 0x000C1D6A
	protected virtual float ChanceToPlayPlotTwistVOLine()
	{
		return 1f;
	}

	// Token: 0x060041BC RID: 16828 RVA: 0x0015F707 File Offset: 0x0015D907
	public override void StartGameplaySoundtracks()
	{
		if (GameUtils.GetDefeatedBossCount() == this.GetDefeatedBossCountForFinalBoss())
		{
			MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_ULDFinalBoss);
			return;
		}
		base.StartGameplaySoundtracks();
	}

	// Token: 0x060041BD RID: 16829 RVA: 0x0015F730 File Offset: 0x0015D930
	public static bool GetIsFirstBoss()
	{
		int @int = Options.Get().GetInt(Option.SELECTED_ADVENTURE);
		AdventureModeDbId int2 = (AdventureModeDbId)Options.Get().GetInt(Option.SELECTED_ADVENTURE_MODE);
		AdventureDataDbfRecord adventureDataRecord = ULDA_MissionEntity.GetAdventureDataRecord(@int, (int)int2);
		return adventureDataRecord == null || !DungeonCrawlUtil.IsDungeonRunActive((GameSaveKeyId)adventureDataRecord.GameSaveDataServerKey);
	}

	// Token: 0x060041BE RID: 16830 RVA: 0x0015F774 File Offset: 0x0015D974
	public static AdventureDataDbfRecord GetAdventureDataRecord(int adventureId, int modeId)
	{
		foreach (AdventureDataDbfRecord adventureDataDbfRecord in GameDbf.AdventureData.GetRecords())
		{
			if (adventureDataDbfRecord.AdventureId == adventureId && adventureDataDbfRecord.ModeId == modeId)
			{
				return adventureDataDbfRecord;
			}
		}
		return null;
	}
}
