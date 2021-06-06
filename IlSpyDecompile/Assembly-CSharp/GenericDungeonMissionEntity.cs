using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericDungeonMissionEntity : MissionEntity
{
	public enum VOSpeaker
	{
		INVALID,
		FRIENDLY_HERO,
		OPPONENT_HERO
	}

	protected class VOPool
	{
		public List<string> m_soundFiles;

		public float m_chanceToPlay = 0.2f;

		public ShouldPlayValue m_shouldPlay = ShouldPlayValue.Once;

		public VOSpeaker m_speaker;

		public string m_quotePrefabPath;

		public GameSaveKeySubkeyId m_oncePerAccountGameSaveSubkey = GameSaveKeySubkeyId.INVALID;

		public long m_timesOncePerAccountVOSeen;

		public VOPool(List<string> soundFiles, float chanceToPlay, ShouldPlayValue shouldPlay, VOSpeaker speaker, string quotePrefabPath = "", GameSaveKeySubkeyId oncePerAccountGameSaveSubkey = GameSaveKeySubkeyId.INVALID)
		{
			m_soundFiles = soundFiles;
			m_chanceToPlay = chanceToPlay;
			m_shouldPlay = shouldPlay;
			m_speaker = speaker;
			m_quotePrefabPath = quotePrefabPath;
			m_oncePerAccountGameSaveSubkey = oncePerAccountGameSaveSubkey;
		}
	}

	protected Dictionary<int, VOPool> m_VOPools = new Dictionary<int, VOPool>();

	private GameSaveKeyId m_gameSaveDataClientKey = GameSaveKeyId.INVALID;

	public virtual AdventureDbId GetAdventureID()
	{
		return AdventureDbId.INVALID;
	}

	public override void PreloadAssets()
	{
		AdventureDataDbfRecord record = GameDbf.AdventureData.GetRecord((AdventureDataDbfRecord r) => r.AdventureId == (int)GetAdventureID());
		if (record != null)
		{
			m_gameSaveDataClientKey = (GameSaveKeyId)record.GameSaveDataClientKey;
		}
		foreach (KeyValuePair<int, VOPool> vOPool in m_VOPools)
		{
			foreach (string soundFile in vOPool.Value.m_soundFiles)
			{
				PreloadSound(soundFile);
			}
			if (m_gameSaveDataClientKey != GameSaveKeyId.INVALID && vOPool.Value.m_oncePerAccountGameSaveSubkey != GameSaveKeySubkeyId.INVALID)
			{
				GameSaveDataManager.Get().GetSubkeyValue(m_gameSaveDataClientKey, vOPool.Value.m_oncePerAccountGameSaveSubkey, out vOPool.Value.m_timesOncePerAccountVOSeen);
			}
		}
	}

	protected virtual bool CanPlayVOLines(Entity heroEntity, VOSpeaker speaker)
	{
		return true;
	}

	protected Card ResolveSpeakerCard(VOSpeaker speaker)
	{
		return speaker switch
		{
			VOSpeaker.FRIENDLY_HERO => GameState.Get().GetFriendlySidePlayer()?.GetHeroCard(), 
			VOSpeaker.OPPONENT_HERO => GameState.Get().GetOpposingSidePlayer()?.GetHeroCard(), 
			_ => null, 
		};
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		if (!m_VOPools.ContainsKey(missionEvent))
		{
			yield break;
		}
		while (m_enemySpeaking)
		{
			yield return null;
		}
		VOPool vOPool = m_VOPools[missionEvent];
		if (vOPool == null || (vOPool.m_oncePerAccountGameSaveSubkey != GameSaveKeySubkeyId.INVALID && vOPool.m_timesOncePerAccountVOSeen > 0))
		{
			yield break;
		}
		Actor actor = null;
		if (string.IsNullOrEmpty(vOPool.m_quotePrefabPath))
		{
			Card card = ResolveSpeakerCard(vOPool.m_speaker);
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
			if (actor == null || !CanPlayVOLines(entity, vOPool.m_speaker))
			{
				yield break;
			}
		}
		List<string> list = new List<string>(vOPool.m_soundFiles);
		if (list == null || list.Count == 0)
		{
			yield break;
		}
		float chanceToPlay = vOPool.m_chanceToPlay;
		float num = Random.Range(0f, 1f);
		if (chanceToPlay < num)
		{
			yield break;
		}
		string text;
		while (true)
		{
			text = list[Random.Range(0, list.Count)];
			if (!NotificationManager.Get().HasSoundPlayedThisSession(text))
			{
				break;
			}
			list.Remove(text);
			if (list.Count != 0)
			{
				continue;
			}
			if (vOPool.m_shouldPlay == ShouldPlayValue.Always)
			{
				for (int i = 0; i < vOPool.m_soundFiles.Count; i++)
				{
					NotificationManager.Get().ForceRemoveSoundFromPlayedList(vOPool.m_soundFiles[i]);
				}
				text = vOPool.m_soundFiles[Random.Range(0, vOPool.m_soundFiles.Count)];
				break;
			}
			yield break;
		}
		if (!string.IsNullOrEmpty(text))
		{
			if (vOPool.m_oncePerAccountGameSaveSubkey != GameSaveKeySubkeyId.INVALID)
			{
				vOPool.m_timesOncePerAccountVOSeen++;
				GameSaveDataManager.Get().SaveSubkey(new GameSaveDataManager.SubkeySaveRequest(m_gameSaveDataClientKey, vOPool.m_oncePerAccountGameSaveSubkey, 1L));
			}
			if (string.IsNullOrEmpty(vOPool.m_quotePrefabPath))
			{
				yield return PlayCriticalLine(actor, text);
				yield break;
			}
			m_enemySpeaking = true;
			yield return PlayBossLine(vOPool.m_quotePrefabPath, text);
			m_enemySpeaking = false;
		}
	}

	protected IEnumerator WaitForEntitySoundToFinish(Entity entity)
	{
		List<CardSoundSpell> playSoundSpells = entity.GetCard().GetPlaySoundSpells(0, loadIfNeeded: false);
		if (playSoundSpells == null || playSoundSpells.Count <= 0)
		{
			yield break;
		}
		CardSoundSpell firstSoundSpell = playSoundSpells[0];
		if (!(firstSoundSpell == null))
		{
			while (firstSoundSpell.GetActiveAudioSource() != null && firstSoundSpell.GetActiveAudioSource().isPlaying)
			{
				yield return null;
			}
		}
	}

	public override string GetNameBannerSubtextOverride(Player.Side playerSide)
	{
		return string.Empty;
	}

	public override bool ShouldShowHeroClassDuringMulligan(Player.Side playerSide)
	{
		return playerSide == Player.Side.FRIENDLY;
	}

	protected static string GetOpposingHeroCardID(List<Network.PowerHistory> powerList, Network.HistCreateGame createGame)
	{
		int num = 0;
		foreach (Network.HistCreateGame.PlayerData player in createGame.Players)
		{
			if (player.GameAccountId.IsEmpty())
			{
				num = player.Player.Tags.Find((Network.Entity.Tag x) => x.Name == 27).Value;
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

	protected virtual float ChanceToPlayRandomVOLine()
	{
		return 0.5f;
	}

	protected string PopRandomLineWithChance(List<string> lines)
	{
		if (lines.Count == 0 || lines == null)
		{
			return null;
		}
		float num = ChanceToPlayRandomVOLine();
		float num2 = Random.Range(0f, 1f);
		if (num < num2)
		{
			return null;
		}
		string text = lines[Random.Range(0, lines.Count)];
		lines.Remove(text);
		return text;
	}
}
