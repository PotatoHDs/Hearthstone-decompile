using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000580 RID: 1408
public class GenericDungeonMissionEntity : MissionEntity
{
	// Token: 0x06004E60 RID: 20064 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public virtual AdventureDbId GetAdventureID()
	{
		return AdventureDbId.INVALID;
	}

	// Token: 0x06004E61 RID: 20065 RVA: 0x0019DC94 File Offset: 0x0019BE94
	public override void PreloadAssets()
	{
		AdventureDataDbfRecord record = GameDbf.AdventureData.GetRecord((AdventureDataDbfRecord r) => r.AdventureId == (int)this.GetAdventureID());
		if (record != null)
		{
			this.m_gameSaveDataClientKey = (GameSaveKeyId)record.GameSaveDataClientKey;
		}
		foreach (KeyValuePair<int, GenericDungeonMissionEntity.VOPool> keyValuePair in this.m_VOPools)
		{
			foreach (string soundPath in keyValuePair.Value.m_soundFiles)
			{
				base.PreloadSound(soundPath);
			}
			if (this.m_gameSaveDataClientKey != GameSaveKeyId.INVALID && keyValuePair.Value.m_oncePerAccountGameSaveSubkey != GameSaveKeySubkeyId.INVALID)
			{
				GameSaveDataManager.Get().GetSubkeyValue(this.m_gameSaveDataClientKey, keyValuePair.Value.m_oncePerAccountGameSaveSubkey, out keyValuePair.Value.m_timesOncePerAccountVOSeen);
			}
		}
	}

	// Token: 0x06004E62 RID: 20066 RVA: 0x000052EC File Offset: 0x000034EC
	protected virtual bool CanPlayVOLines(Entity heroEntity, GenericDungeonMissionEntity.VOSpeaker speaker)
	{
		return true;
	}

	// Token: 0x06004E63 RID: 20067 RVA: 0x0019DD98 File Offset: 0x0019BF98
	protected Card ResolveSpeakerCard(GenericDungeonMissionEntity.VOSpeaker speaker)
	{
		if (speaker != GenericDungeonMissionEntity.VOSpeaker.FRIENDLY_HERO)
		{
			if (speaker != GenericDungeonMissionEntity.VOSpeaker.OPPONENT_HERO)
			{
				return null;
			}
			Player opposingSidePlayer = GameState.Get().GetOpposingSidePlayer();
			if (opposingSidePlayer == null)
			{
				return null;
			}
			return opposingSidePlayer.GetHeroCard();
		}
		else
		{
			Player friendlySidePlayer = GameState.Get().GetFriendlySidePlayer();
			if (friendlySidePlayer == null)
			{
				return null;
			}
			return friendlySidePlayer.GetHeroCard();
		}
	}

	// Token: 0x06004E64 RID: 20068 RVA: 0x0019DDDE File Offset: 0x0019BFDE
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		if (!this.m_VOPools.ContainsKey(missionEvent))
		{
			yield break;
		}
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		GenericDungeonMissionEntity.VOPool vopool = this.m_VOPools[missionEvent];
		if (vopool == null)
		{
			yield break;
		}
		if (vopool.m_oncePerAccountGameSaveSubkey != GameSaveKeySubkeyId.INVALID && vopool.m_timesOncePerAccountVOSeen > 0L)
		{
			yield break;
		}
		Actor actor = null;
		if (string.IsNullOrEmpty(vopool.m_quotePrefabPath))
		{
			Card card = this.ResolveSpeakerCard(vopool.m_speaker);
			if (card == null)
			{
				yield break;
			}
			Entity entity = card.GetEntity();
			if (entity == null)
			{
				yield break;
			}
			actor = card.GetActor();
			if (actor == null)
			{
				yield break;
			}
			if (!this.CanPlayVOLines(entity, vopool.m_speaker))
			{
				yield break;
			}
		}
		List<string> list = new List<string>(vopool.m_soundFiles);
		if (list == null || list.Count == 0)
		{
			yield break;
		}
		float chanceToPlay = vopool.m_chanceToPlay;
		float num = UnityEngine.Random.Range(0f, 1f);
		if (chanceToPlay < num)
		{
			yield break;
		}
		string text;
		do
		{
			text = list[UnityEngine.Random.Range(0, list.Count)];
			if (!NotificationManager.Get().HasSoundPlayedThisSession(text))
			{
				goto IL_1BE;
			}
			list.Remove(text);
		}
		while (list.Count != 0);
		if (vopool.m_shouldPlay != MissionEntity.ShouldPlayValue.Always)
		{
			yield break;
		}
		for (int i = 0; i < vopool.m_soundFiles.Count; i++)
		{
			NotificationManager.Get().ForceRemoveSoundFromPlayedList(vopool.m_soundFiles[i]);
		}
		text = vopool.m_soundFiles[UnityEngine.Random.Range(0, vopool.m_soundFiles.Count)];
		IL_1BE:
		if (string.IsNullOrEmpty(text))
		{
			yield break;
		}
		if (vopool.m_oncePerAccountGameSaveSubkey != GameSaveKeySubkeyId.INVALID)
		{
			vopool.m_timesOncePerAccountVOSeen += 1L;
			GameSaveDataManager.Get().SaveSubkey(new GameSaveDataManager.SubkeySaveRequest(this.m_gameSaveDataClientKey, vopool.m_oncePerAccountGameSaveSubkey, new long[]
			{
				1L
			}), null);
		}
		if (string.IsNullOrEmpty(vopool.m_quotePrefabPath))
		{
			yield return base.PlayCriticalLine(actor, text, 2.5f);
		}
		else
		{
			this.m_enemySpeaking = true;
			yield return base.PlayBossLine(vopool.m_quotePrefabPath, text, 2.5f);
			this.m_enemySpeaking = false;
		}
		yield break;
	}

	// Token: 0x06004E65 RID: 20069 RVA: 0x0019DDF4 File Offset: 0x0019BFF4
	protected IEnumerator WaitForEntitySoundToFinish(Entity entity)
	{
		List<CardSoundSpell> playSoundSpells = entity.GetCard().GetPlaySoundSpells(0, false);
		if (playSoundSpells != null && playSoundSpells.Count > 0)
		{
			CardSoundSpell firstSoundSpell = playSoundSpells[0];
			if (firstSoundSpell == null)
			{
				yield break;
			}
			while (firstSoundSpell.GetActiveAudioSource() != null && firstSoundSpell.GetActiveAudioSource().isPlaying)
			{
				yield return null;
			}
			firstSoundSpell = null;
		}
		yield break;
	}

	// Token: 0x06004E66 RID: 20070 RVA: 0x0019DE03 File Offset: 0x0019C003
	public override string GetNameBannerSubtextOverride(Player.Side playerSide)
	{
		return string.Empty;
	}

	// Token: 0x06004E67 RID: 20071 RVA: 0x0019DE0A File Offset: 0x0019C00A
	public override bool ShouldShowHeroClassDuringMulligan(Player.Side playerSide)
	{
		return playerSide == Player.Side.FRIENDLY;
	}

	// Token: 0x06004E68 RID: 20072 RVA: 0x0019DE10 File Offset: 0x0019C010
	protected static string GetOpposingHeroCardID(List<Network.PowerHistory> powerList, Network.HistCreateGame createGame)
	{
		int num = 0;
		foreach (Network.HistCreateGame.PlayerData playerData in createGame.Players)
		{
			if (playerData.GameAccountId.IsEmpty())
			{
				num = playerData.Player.Tags.Find((Network.Entity.Tag x) => x.Name == 27).Value;
				break;
			}
		}
		for (int i = 0; i < powerList.Count; i++)
		{
			Network.PowerHistory powerHistory = powerList[i];
			if (powerHistory.Type == Network.PowerType.FULL_ENTITY)
			{
				Network.Entity entity = ((Network.HistFullEntity)powerHistory).Entity;
				if (entity.ID == num)
				{
					return entity.CardID;
				}
			}
		}
		return "";
	}

	// Token: 0x06004E69 RID: 20073 RVA: 0x00112BA9 File Offset: 0x00110DA9
	protected virtual float ChanceToPlayRandomVOLine()
	{
		return 0.5f;
	}

	// Token: 0x06004E6A RID: 20074 RVA: 0x0019DEEC File Offset: 0x0019C0EC
	protected string PopRandomLineWithChance(List<string> lines)
	{
		if (lines.Count == 0 || lines == null)
		{
			return null;
		}
		float num = this.ChanceToPlayRandomVOLine();
		float num2 = UnityEngine.Random.Range(0f, 1f);
		if (num < num2)
		{
			return null;
		}
		string text = lines[UnityEngine.Random.Range(0, lines.Count)];
		lines.Remove(text);
		return text;
	}

	// Token: 0x04004526 RID: 17702
	protected Dictionary<int, GenericDungeonMissionEntity.VOPool> m_VOPools = new Dictionary<int, GenericDungeonMissionEntity.VOPool>();

	// Token: 0x04004527 RID: 17703
	private GameSaveKeyId m_gameSaveDataClientKey = GameSaveKeyId.INVALID;

	// Token: 0x02001EE4 RID: 7908
	public enum VOSpeaker
	{
		// Token: 0x0400D659 RID: 54873
		INVALID,
		// Token: 0x0400D65A RID: 54874
		FRIENDLY_HERO,
		// Token: 0x0400D65B RID: 54875
		OPPONENT_HERO
	}

	// Token: 0x02001EE5 RID: 7909
	protected class VOPool
	{
		// Token: 0x06011556 RID: 70998 RVA: 0x004D6EF0 File Offset: 0x004D50F0
		public VOPool(List<string> soundFiles, float chanceToPlay, MissionEntity.ShouldPlayValue shouldPlay, GenericDungeonMissionEntity.VOSpeaker speaker, string quotePrefabPath = "", GameSaveKeySubkeyId oncePerAccountGameSaveSubkey = GameSaveKeySubkeyId.INVALID)
		{
			this.m_soundFiles = soundFiles;
			this.m_chanceToPlay = chanceToPlay;
			this.m_shouldPlay = shouldPlay;
			this.m_speaker = speaker;
			this.m_quotePrefabPath = quotePrefabPath;
			this.m_oncePerAccountGameSaveSubkey = oncePerAccountGameSaveSubkey;
		}

		// Token: 0x0400D65C RID: 54876
		public List<string> m_soundFiles;

		// Token: 0x0400D65D RID: 54877
		public float m_chanceToPlay = 0.2f;

		// Token: 0x0400D65E RID: 54878
		public MissionEntity.ShouldPlayValue m_shouldPlay = MissionEntity.ShouldPlayValue.Once;

		// Token: 0x0400D65F RID: 54879
		public GenericDungeonMissionEntity.VOSpeaker m_speaker;

		// Token: 0x0400D660 RID: 54880
		public string m_quotePrefabPath;

		// Token: 0x0400D661 RID: 54881
		public GameSaveKeySubkeyId m_oncePerAccountGameSaveSubkey = GameSaveKeySubkeyId.INVALID;

		// Token: 0x0400D662 RID: 54882
		public long m_timesOncePerAccountVOSeen;
	}
}
