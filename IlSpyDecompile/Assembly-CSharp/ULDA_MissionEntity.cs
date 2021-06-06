using System.Collections;
using Hearthstone.DungeonCrawl;
using UnityEngine;

public abstract class ULDA_MissionEntity : GenericDungeonMissionEntity
{
	public sealed override AdventureDbId GetAdventureID()
	{
		return AdventureDbId.ULDUM;
	}

	protected sealed override bool CanPlayVOLines(Entity speakerEntity, VOSpeaker speaker)
	{
		if (speaker == VOSpeaker.FRIENDLY_HERO)
		{
			return speakerEntity.GetCardId().Contains("ULDA_");
		}
		return base.CanPlayVOLines(speakerEntity, speaker);
	}

	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.LOST)
		{
			yield return new WaitForSeconds(5f);
		}
	}

	public override bool ShouldShowHeroClassDuringMulligan(Player.Side playerSide)
	{
		return false;
	}

	public override void OnPlayThinkEmote()
	{
		if (m_enemySpeaking)
		{
			return;
		}
		Player currentPlayer = GameState.Get().GetCurrentPlayer();
		if (currentPlayer.IsFriendlySide() && !currentPlayer.GetHeroCard().HasActiveEmoteSound())
		{
			EmoteType emoteType = EmoteType.THINK1;
			switch (Random.Range(1, 4))
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
			GameState.Get().GetCurrentPlayer().GetHeroCard()
				.PlayEmote(emoteType);
		}
	}

	public override void StartMulliganSoundtracks(bool soft)
	{
		if (!soft)
		{
			MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_ULDMulligan);
		}
	}

	public int GetDefeatedBossCountForFinalBoss()
	{
		int missionId = GameMgr.Get().GetMissionId();
		if (missionId == 3432 || missionId == 3437)
		{
			return 0;
		}
		return 7;
	}

	protected virtual float ChanceToPlayPlotTwistVOLine()
	{
		return 1f;
	}

	public override void StartGameplaySoundtracks()
	{
		if (GameUtils.GetDefeatedBossCount() == GetDefeatedBossCountForFinalBoss())
		{
			MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_ULDFinalBoss);
		}
		else
		{
			base.StartGameplaySoundtracks();
		}
	}

	public static bool GetIsFirstBoss()
	{
		int @int = Options.Get().GetInt(Option.SELECTED_ADVENTURE);
		AdventureModeDbId int2 = (AdventureModeDbId)Options.Get().GetInt(Option.SELECTED_ADVENTURE_MODE);
		AdventureDataDbfRecord adventureDataRecord = GetAdventureDataRecord(@int, (int)int2);
		if (adventureDataRecord == null)
		{
			return true;
		}
		if (!DungeonCrawlUtil.IsDungeonRunActive((GameSaveKeyId)adventureDataRecord.GameSaveDataServerKey))
		{
			return true;
		}
		return false;
	}

	public static AdventureDataDbfRecord GetAdventureDataRecord(int adventureId, int modeId)
	{
		foreach (AdventureDataDbfRecord record in GameDbf.AdventureData.GetRecords())
		{
			if (record.AdventureId == adventureId && record.ModeId == modeId)
			{
				return record;
			}
		}
		return null;
	}
}
