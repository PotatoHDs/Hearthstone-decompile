using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Assets;
using bgs;
using bgs.types;
using Blizzard.T5.Core;
using Blizzard.Telemetry.WTCG.Client;
using UnityEngine;

// Token: 0x020000A3 RID: 163
public class PresenceMgr
{
	// Token: 0x06000A4A RID: 2634 RVA: 0x0003A0B4 File Offset: 0x000382B4
	public static PresenceMgr Get()
	{
		if (PresenceMgr.s_instance == null)
		{
			PresenceMgr.s_instance = new PresenceMgr();
			PresenceMgr.s_instance.Initialize();
		}
		return PresenceMgr.s_instance;
	}

	// Token: 0x06000A4B RID: 2635 RVA: 0x0003A0D6 File Offset: 0x000382D6
	public static bool IsInitialized()
	{
		return PresenceMgr.s_instance != null;
	}

	// Token: 0x06000A4C RID: 2636 RVA: 0x0003A0E0 File Offset: 0x000382E0
	public bool SetStatus(params Enum[] args)
	{
		return this.SetStatusImpl(args);
	}

	// Token: 0x06000A4D RID: 2637 RVA: 0x0003A0EC File Offset: 0x000382EC
	public bool SetStatus_EnteringAdventure(AdventureDbId adventureId, AdventureModeDbId adventureModeId)
	{
		KeyValuePair<AdventureDbId, AdventureModeDbId> key = new KeyValuePair<AdventureDbId, AdventureModeDbId>(adventureId, adventureModeId);
		PresenceMgr.PresenceTargets presenceTargets;
		if (PresenceMgr.s_adventurePresenceMap.TryGetValue(key, out presenceTargets))
		{
			this.SetStatus(new Enum[]
			{
				Global.PresenceStatus.ADVENTURE_SCENARIO_SELECT,
				presenceTargets.EnteringAdventureValue
			});
			return true;
		}
		return false;
	}

	// Token: 0x06000A4E RID: 2638 RVA: 0x0003A139 File Offset: 0x00038339
	public bool SetStatus_PlayingMission(ScenarioDbId missionId)
	{
		return PresenceMgr.s_stringKeyMap.ContainsKey(missionId) && this.SetStatus(new Enum[]
		{
			Global.PresenceStatus.ADVENTURE_SCENARIO_PLAYING_GAME,
			missionId
		});
	}

	// Token: 0x06000A4F RID: 2639 RVA: 0x0003A170 File Offset: 0x00038370
	public bool SetStatus_SpectatingMission(ScenarioDbId missionId)
	{
		AdventureDbId adventureId = GameUtils.GetAdventureId((int)missionId);
		AdventureModeDbId adventureModeId = GameUtils.GetAdventureModeId((int)missionId);
		KeyValuePair<AdventureDbId, AdventureModeDbId> key = new KeyValuePair<AdventureDbId, AdventureModeDbId>(adventureId, adventureModeId);
		PresenceMgr.PresenceTargets presenceTargets;
		return PresenceMgr.s_adventurePresenceMap.TryGetValue(key, out presenceTargets) && this.SetStatus(new Enum[]
		{
			presenceTargets.SpectatingValue
		});
	}

	// Token: 0x06000A50 RID: 2640 RVA: 0x0003A1BF File Offset: 0x000383BF
	public Enum[] GetStatus()
	{
		return this.m_status;
	}

	// Token: 0x17000096 RID: 150
	// (get) Token: 0x06000A51 RID: 2641 RVA: 0x0003A1C7 File Offset: 0x000383C7
	public Global.PresenceStatus CurrentStatus
	{
		get
		{
			if (this.m_status != null && this.m_status.Length != 0)
			{
				return (Global.PresenceStatus)this.m_status[0];
			}
			return Global.PresenceStatus.UNKNOWN;
		}
	}

	// Token: 0x06000A52 RID: 2642 RVA: 0x0003A1E9 File Offset: 0x000383E9
	public Enum[] GetPrevStatus()
	{
		return this.m_prevStatus;
	}

	// Token: 0x06000A53 RID: 2643 RVA: 0x0003A1F1 File Offset: 0x000383F1
	public bool SetPrevStatus()
	{
		return this.SetStatusImpl(this.m_prevStatus);
	}

	// Token: 0x06000A54 RID: 2644 RVA: 0x0003A200 File Offset: 0x00038400
	public bool IsRichPresence(params Enum[] status)
	{
		if (status == null)
		{
			return false;
		}
		if (status.Length == 0)
		{
			return false;
		}
		foreach (Enum @enum in status)
		{
			if (@enum == null)
			{
				return false;
			}
			if (PresenceMgr.s_richPresenceMap.ContainsKey(@enum))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06000A55 RID: 2645 RVA: 0x0003A240 File Offset: 0x00038440
	public string GetStatusText(BnetPlayer player)
	{
		List<string> list = new List<string>();
		string text = null;
		if (this.GetStatus_Internal(player, ref text, list, null) != Global.PresenceStatus.UNKNOWN && BnetPresenceMgr.Get().IsSubscribedToPlayer(player.GetHearthstoneGameAccountId()))
		{
			string[] array = list.ToArray();
			string result;
			try
			{
				string key = text;
				object[] args = array;
				result = GameStrings.Format(key, args);
			}
			catch (FormatException)
			{
				global::Log.Presence.PrintWarning("PresenceMgr.GetStatusText: Arguments were expected for presence string, but none were provided.", Array.Empty<object>());
				result = GameStrings.Get("GLOBAL_PROGRAMNAME_HEARTHSTONE");
			}
			return result;
		}
		BnetGameAccount bestGameAccount = player.GetBestGameAccount();
		if (!(bestGameAccount == null))
		{
			return bestGameAccount.GetRichPresence();
		}
		return null;
	}

	// Token: 0x06000A56 RID: 2646 RVA: 0x0003A2DC File Offset: 0x000384DC
	public string GetStatusText(byte[] presenceFieldBlobValue)
	{
		List<string> list = new List<string>();
		string text = null;
		this.GetStatus_Internal(presenceFieldBlobValue, ref text, list, null);
		string[] array = list.ToArray();
		string result;
		try
		{
			string key = text;
			object[] args = array;
			result = GameStrings.Format(key, args);
		}
		catch (FormatException)
		{
			global::Log.Presence.PrintWarning("PresenceMgr.GetStatusText: Arguments were expected for presence string, but none were provided.", Array.Empty<object>());
			result = GameStrings.Get("GLOBAL_PROGRAMNAME_HEARTHSTONE");
		}
		return result;
	}

	// Token: 0x06000A57 RID: 2647 RVA: 0x0003A348 File Offset: 0x00038548
	public Global.PresenceStatus GetStatus(BnetPlayer player)
	{
		string text = null;
		return this.GetStatus_Internal(player, ref text, null, null);
	}

	// Token: 0x06000A58 RID: 2648 RVA: 0x0003A364 File Offset: 0x00038564
	public Enum[] GetStatusEnums(BnetPlayer player)
	{
		string text = null;
		List<Enum> list = new List<Enum>();
		this.GetStatus_Internal(player, ref text, null, list);
		return list.ToArray();
	}

	// Token: 0x06000A59 RID: 2649 RVA: 0x0003A38B File Offset: 0x0003858B
	public void OnShutdown()
	{
		this.ReportPresenceToTelemetry(new Enum[]
		{
			Global.PresenceStatus.UNKNOWN
		});
	}

	// Token: 0x06000A5A RID: 2650 RVA: 0x0003A3A2 File Offset: 0x000385A2
	public void ResetTelemetry()
	{
		this.ReportPresenceToTelemetry(new Enum[]
		{
			Global.PresenceStatus.UNKNOWN
		});
		this.m_currentStatus = null;
		this.m_timeStartStatusMs = (long)(Time.realtimeSinceStartup * 1000f);
	}

	// Token: 0x06000A5B RID: 2651 RVA: 0x0003A3D4 File Offset: 0x000385D4
	private Global.PresenceStatus GetStatus_Internal(BnetPlayer player, ref string statusKey, List<string> stringArgs = null, List<Enum> enumVals = null)
	{
		Global.PresenceStatus result = Global.PresenceStatus.UNKNOWN;
		if (player == null)
		{
			return result;
		}
		if (player.GetBestGameAccount() == null)
		{
			return result;
		}
		BnetGameAccount hearthstoneGameAccount = player.GetHearthstoneGameAccount();
		if (hearthstoneGameAccount == null)
		{
			return result;
		}
		byte[] bytes;
		if (!hearthstoneGameAccount.TryGetGameFieldBytes(17U, out bytes))
		{
			return result;
		}
		return this.GetStatus_Internal(bytes, ref statusKey, stringArgs, enumVals);
	}

	// Token: 0x06000A5C RID: 2652 RVA: 0x0003A428 File Offset: 0x00038628
	private Global.PresenceStatus GetStatus_Internal(byte[] bytes, ref string statusKey, List<string> stringArgs = null, List<Enum> enumVals = null)
	{
		Global.PresenceStatus presenceStatus = Global.PresenceStatus.UNKNOWN;
		if (bytes == null)
		{
			return presenceStatus;
		}
		Enum @enum = null;
		using (MemoryStream memoryStream = new MemoryStream(bytes))
		{
			using (BinaryReader binaryReader = new BinaryReader(memoryStream))
			{
				if (!this.DecodeStatusVal(binaryReader, ref @enum, ref statusKey))
				{
					return presenceStatus;
				}
				presenceStatus = (Global.PresenceStatus)((int)@enum);
				if (enumVals != null)
				{
					enumVals.Add(presenceStatus);
				}
				if (stringArgs == null)
				{
					if (enumVals == null)
					{
						goto IL_8A;
					}
				}
				while (memoryStream.Position < (long)bytes.Length)
				{
					string key = null;
					if (!this.DecodeStatusVal(binaryReader, ref @enum, ref key))
					{
						return presenceStatus;
					}
					if (enumVals != null)
					{
						enumVals.Add(@enum);
					}
					if (stringArgs != null)
					{
						string item = GameStrings.Get(key);
						stringArgs.Add(item);
					}
				}
				IL_8A:;
			}
		}
		return presenceStatus;
	}

	// Token: 0x06000A5D RID: 2653 RVA: 0x0003A4F8 File Offset: 0x000386F8
	public static global::Map<Enum, string> GetEnumStringMap()
	{
		return PresenceMgr.s_stringKeyMap;
	}

	// Token: 0x06000A5E RID: 2654 RVA: 0x0003A500 File Offset: 0x00038700
	private void Initialize()
	{
		for (int i = 0; i < PresenceMgr.s_enumIdList.Length; i++)
		{
			Type type = PresenceMgr.s_enumIdList[i];
			if (Enum.GetUnderlyingType(type) != typeof(int))
			{
				throw new Exception(string.Format("Underlying type of enum {0} (underlying={1}) must {2} be to used by Presence system.", type.FullName, Enum.GetUnderlyingType(type).FullName, typeof(int).Name));
			}
			byte b = (byte)(i + 1);
			this.m_enumToIdMap.Add(type, b);
			this.m_idToEnumMap.Add(b, type);
		}
	}

	// Token: 0x06000A5F RID: 2655 RVA: 0x0003A590 File Offset: 0x00038790
	private bool SetStatusImpl(Enum[] status)
	{
		if (!Network.ShouldBeConnectedToAurora())
		{
			return false;
		}
		if (!Network.IsLoggedIn())
		{
			return true;
		}
		if (status == null || status.Length == 0)
		{
			global::Error.AddDevFatal("PresenceMgr.SetStatusImpl() - Received status of length 0. Setting empty status is not supported.", Array.Empty<object>());
			return false;
		}
		if (GeneralUtils.AreArraysEqual<Enum>(this.m_status, status))
		{
			return true;
		}
		if (!this.SetRichPresence(status))
		{
			return false;
		}
		if (!this.SetGamePresence(status))
		{
			return false;
		}
		NetCache.NetCacheFeatures netObject = NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>();
		if (netObject != null && netObject.SendTelemetryPresence)
		{
			this.ReportPresenceToTelemetry(status);
		}
		this.m_prevStatus = this.m_status;
		bool flag = this.m_prevStatus == null || this.m_prevStatus.Length == 0;
		this.m_status = new Enum[status.Length];
		Array.Copy(status, this.m_status, status.Length);
		if (flag || !PresenceMgr.IsStatusPlayingGame((Global.PresenceStatus)status[0]))
		{
			SpectatorManager.Get().UpdateMySpectatorInfo();
		}
		return true;
	}

	// Token: 0x06000A60 RID: 2656 RVA: 0x0003A664 File Offset: 0x00038864
	private void ReportPresenceToTelemetry(Enum[] status)
	{
		if (status.Length == 0)
		{
			return;
		}
		long num = (long)(Time.realtimeSinceStartup * 1000f);
		long millisecondsSincePrev = num - this.m_timeStartStatusMs;
		this.m_timeStartStatusMs = num;
		PresenceStatus presenceStatus = new PresenceStatus
		{
			PresenceId = (long)((Global.PresenceStatus)status[0])
		};
		if (status.Length > 1)
		{
			presenceStatus.PresenceSubId = Convert.ToInt64(status.Skip(1).FirstOrDefault<Enum>());
		}
		TelemetryManager.Client().SendPresenceChanged(presenceStatus, this.m_currentStatus, millisecondsSincePrev);
		this.m_timeStartStatusMs = num;
		this.m_currentStatus = presenceStatus;
	}

	// Token: 0x06000A61 RID: 2657 RVA: 0x0003A6E8 File Offset: 0x000388E8
	private bool SetRichPresence(Enum[] status)
	{
		Enum[] array = new Enum[status.Length];
		for (int i = 0; i < status.Length; i++)
		{
			Enum @enum = status[i];
			Enum enum2;
			if (PresenceMgr.s_richPresenceMap.TryGetValue(@enum, out enum2))
			{
				if (enum2 == null)
				{
					return false;
				}
			}
			else
			{
				enum2 = @enum;
			}
			array[i] = enum2;
		}
		if (array.Any((Enum e) => !RichPresence.s_streamIds.ContainsKey(e.GetType())))
		{
			array = new Enum[]
			{
				array[0]
			};
		}
		if (GeneralUtils.AreArraysEqual<Enum>(this.m_richPresence, array))
		{
			return true;
		}
		this.m_richPresence = array;
		if (!Network.ShouldBeConnectedToAurora())
		{
			string format = "Caller should check for Battle.net connection before calling SetRichPresence {0}";
			object arg;
			if (array != null)
			{
				arg = string.Join(", ", (from x in array
				select x.ToString()).ToArray<string>());
			}
			else
			{
				arg = "";
			}
			global::Error.AddDevFatal(string.Format(format, arg), Array.Empty<object>());
			return false;
		}
		if (array == null)
		{
			return false;
		}
		if (array.Length == 0)
		{
			return false;
		}
		RichPresenceUpdate[] array2 = new RichPresenceUpdate[array.Length];
		for (int j = 0; j < array.Length; j++)
		{
			Enum enum3 = array[j];
			Type type = enum3.GetType();
			global::FourCC fourCC = RichPresence.s_streamIds[type];
			array2[j] = new RichPresenceUpdate
			{
				presenceFieldIndex = (ulong)((j == 0) ? 0 : (458752 + j)),
				programId = BnetProgramId.HEARTHSTONE.GetValue(),
				streamId = fourCC.GetValue(),
				index = Convert.ToUInt32(enum3)
			};
		}
		BattleNet.SetRichPresence(array2);
		return true;
	}

	// Token: 0x06000A62 RID: 2658 RVA: 0x0003A878 File Offset: 0x00038A78
	private bool SetGamePresence(Enum[] status)
	{
		bool result;
		using (MemoryStream memoryStream = new MemoryStream())
		{
			using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
			{
				for (int i = 0; i < status.Length; i++)
				{
					byte value;
					int value2;
					if (!this.EncodeStatusVal(status, i, out value, out value2))
					{
						return false;
					}
					binaryWriter.Write(value);
					binaryWriter.Write(value2);
				}
				Array buffer = memoryStream.GetBuffer();
				byte[] array = new byte[memoryStream.Position];
				Array.Copy(buffer, array, array.Length);
				result = BnetPresenceMgr.Get().SetGameField(17U, array);
			}
		}
		return result;
	}

	// Token: 0x06000A63 RID: 2659 RVA: 0x0003A924 File Offset: 0x00038B24
	private bool EncodeStatusVal(Enum[] status, int index, out byte id, out int intVal)
	{
		Enum @enum = status[index];
		Type type = @enum.GetType();
		intVal = Convert.ToInt32(@enum);
		if (!this.m_enumToIdMap.TryGetValue(type, out id))
		{
			global::Error.AddDevFatal("PresenceMgr.EncodeStatusVal() - {0} at index {1} belongs to type {2}, which has no id", new object[]
			{
				@enum,
				index,
				type
			});
			return false;
		}
		return true;
	}

	// Token: 0x06000A64 RID: 2660 RVA: 0x0003A978 File Offset: 0x00038B78
	private bool DecodeStatusVal(BinaryReader reader, ref Enum enumVal, ref string key)
	{
		key = null;
		byte b = 0;
		int num = 0;
		int num2 = (int)reader.BaseStream.Position;
		int num3 = num2 + 1;
		try
		{
			b = reader.ReadByte();
			num3 = (int)reader.BaseStream.Position;
		}
		catch (Exception ex)
		{
			global::Log.Presence.Print("PresenceMgr.DecodeStatusVal - unable to decode enum id {0} at index {1} : {2} {3}", new object[]
			{
				b,
				num2,
				ex.GetType().FullName,
				ex.Message
			});
			return false;
		}
		Type type;
		if (!this.m_idToEnumMap.TryGetValue(b, out type))
		{
			global::Log.Presence.Print("PresenceMgr.DecodeStatusVal - id {0} at index {1}, has no enum type", new object[]
			{
				b,
				num2
			});
			return false;
		}
		try
		{
			num = reader.ReadInt32();
		}
		catch (Exception ex2)
		{
			global::Log.Presence.Print("PresenceMgr.DecodeStatusVal - unable to decode enum value {0} at index {1} : {2} {3}", new object[]
			{
				b,
				num3,
				ex2.GetType().FullName,
				ex2.Message
			});
			return false;
		}
		if (type == typeof(Global.PresenceStatus))
		{
			Global.PresenceStatus presenceStatus = (Global.PresenceStatus)num;
			enumVal = presenceStatus;
			if (!PresenceMgr.s_stringKeyMap.TryGetValue(presenceStatus, out key))
			{
				global::Log.Presence.Print("PresenceMgr.DecodeStatusVal - value {0}.{1} at index {2}, has no string", new object[]
				{
					type,
					presenceStatus,
					num3
				});
				return false;
			}
		}
		else if (type == typeof(PresenceTutorial))
		{
			PresenceTutorial presenceTutorial = (PresenceTutorial)num;
			enumVal = presenceTutorial;
			if (!PresenceMgr.s_stringKeyMap.TryGetValue(presenceTutorial, out key))
			{
				global::Log.Presence.Print("PresenceMgr.DecodeStatusVal - value {0}.{1} at index {2}, has no string", new object[]
				{
					type,
					presenceTutorial,
					num3
				});
				return false;
			}
		}
		else if (type == typeof(PresenceAdventureMode))
		{
			PresenceAdventureMode presenceAdventureMode = (PresenceAdventureMode)num;
			enumVal = presenceAdventureMode;
			if (!PresenceMgr.s_stringKeyMap.TryGetValue(presenceAdventureMode, out key))
			{
				global::Log.Presence.Print("PresenceMgr.DecodeStatusVal - value {0}.{1} at index {2}, has no string", new object[]
				{
					type,
					presenceAdventureMode,
					num3
				});
				return false;
			}
		}
		else if (type == typeof(ScenarioDbId))
		{
			ScenarioDbId scenarioDbId = (ScenarioDbId)num;
			enumVal = scenarioDbId;
			if (!PresenceMgr.s_stringKeyMap.TryGetValue(scenarioDbId, out key))
			{
				global::Log.Presence.Print("PresenceMgr.DecodeStatusVal - value {0}.{1} at index {2}, has no string", new object[]
				{
					type,
					scenarioDbId,
					num3
				});
				return false;
			}
		}
		return true;
	}

	// Token: 0x06000A65 RID: 2661 RVA: 0x0003AC3C File Offset: 0x00038E3C
	public static bool IsStatusPlayingGame(Global.PresenceStatus status)
	{
		if (status <= Global.PresenceStatus.ADVENTURE_SCENARIO_PLAYING_GAME)
		{
			if (status <= Global.PresenceStatus.PRACTICE_GAME)
			{
				if (status != Global.PresenceStatus.TUTORIAL_GAME && status != Global.PresenceStatus.PLAY_GAME && status != Global.PresenceStatus.PRACTICE_GAME)
				{
					return false;
				}
			}
			else if (status != Global.PresenceStatus.ARENA_GAME && status != Global.PresenceStatus.FRIENDLY_GAME && status != Global.PresenceStatus.ADVENTURE_SCENARIO_PLAYING_GAME)
			{
				return false;
			}
		}
		else if (status <= Global.PresenceStatus.WAIT_FOR_OPPONENT_RECONNECT)
		{
			if (status != Global.PresenceStatus.TAVERN_BRAWL_GAME && status != Global.PresenceStatus.TAVERN_BRAWL_FRIENDLY_GAME && status != Global.PresenceStatus.WAIT_FOR_OPPONENT_RECONNECT)
			{
				return false;
			}
		}
		else if (status != Global.PresenceStatus.BATTLEGROUNDS_GAME && status != Global.PresenceStatus.DUELS_GAME && status - Global.PresenceStatus.PLAY_RANKED_STANDARD > 5)
		{
			return false;
		}
		return true;
	}

	// Token: 0x04000697 RID: 1687
	private static readonly global::Map<Enum, Enum> s_richPresenceMap = new global::Map<Enum, Enum>
	{
		{
			Global.PresenceStatus.LOGIN,
			null
		},
		{
			Global.PresenceStatus.WELCOMEQUESTS,
			Global.PresenceStatus.HUB
		},
		{
			Global.PresenceStatus.STORE,
			Global.PresenceStatus.HUB
		},
		{
			Global.PresenceStatus.QUESTLOG,
			Global.PresenceStatus.HUB
		},
		{
			Global.PresenceStatus.PACKOPENING,
			Global.PresenceStatus.HUB
		},
		{
			Global.PresenceStatus.COLLECTION,
			Global.PresenceStatus.HUB
		},
		{
			Global.PresenceStatus.DECKEDITOR,
			Global.PresenceStatus.HUB
		},
		{
			Global.PresenceStatus.CRAFTING,
			Global.PresenceStatus.HUB
		},
		{
			Global.PresenceStatus.PLAY_DECKPICKER,
			Global.PresenceStatus.HUB
		},
		{
			Global.PresenceStatus.PLAY_QUEUE,
			Global.PresenceStatus.HUB
		},
		{
			Global.PresenceStatus.PRACTICE_DECKPICKER,
			Global.PresenceStatus.HUB
		},
		{
			Global.PresenceStatus.ARENA_PURCHASE,
			Global.PresenceStatus.HUB
		},
		{
			Global.PresenceStatus.ARENA_FORGE,
			Global.PresenceStatus.HUB
		},
		{
			Global.PresenceStatus.ARENA_IDLE,
			Global.PresenceStatus.HUB
		},
		{
			Global.PresenceStatus.ARENA_QUEUE,
			Global.PresenceStatus.HUB
		},
		{
			Global.PresenceStatus.ARENA_REWARD,
			Global.PresenceStatus.HUB
		},
		{
			Global.PresenceStatus.FRIENDLY_DECKPICKER,
			Global.PresenceStatus.HUB
		},
		{
			Global.PresenceStatus.ADVENTURE_CHOOSING_MODE,
			Global.PresenceStatus.HUB
		},
		{
			Global.PresenceStatus.ADVENTURE_SCENARIO_SELECT,
			Global.PresenceStatus.HUB
		},
		{
			Global.PresenceStatus.TAVERN_BRAWL_SCREEN,
			Global.PresenceStatus.HUB
		},
		{
			Global.PresenceStatus.TAVERN_BRAWL_DECKEDITOR,
			Global.PresenceStatus.HUB
		},
		{
			Global.PresenceStatus.TAVERN_BRAWL_QUEUE,
			Global.PresenceStatus.HUB
		},
		{
			Global.PresenceStatus.TAVERN_BRAWL_FRIENDLY_WAITING,
			Global.PresenceStatus.HUB
		},
		{
			Global.PresenceStatus.FIRESIDE_BRAWL_SCREEN,
			Global.PresenceStatus.HUB
		},
		{
			Global.PresenceStatus.BATTLEGROUNDS_QUEUE,
			Global.PresenceStatus.HUB
		},
		{
			Global.PresenceStatus.DUELS_QUEUE,
			Global.PresenceStatus.HUB
		}
	};

	// Token: 0x04000698 RID: 1688
	private static readonly global::Map<Enum, string> s_stringKeyMap = new global::Map<Enum, string>
	{
		{
			Global.PresenceStatus.LOGIN,
			"PRESENCE_STATUS_LOGIN"
		},
		{
			Global.PresenceStatus.TUTORIAL_PREGAME,
			"PRESENCE_STATUS_TUTORIAL_PREGAME"
		},
		{
			Global.PresenceStatus.TUTORIAL_GAME,
			"PRESENCE_STATUS_TUTORIAL_GAME"
		},
		{
			Global.PresenceStatus.WELCOMEQUESTS,
			"PRESENCE_STATUS_WELCOMEQUESTS"
		},
		{
			Global.PresenceStatus.HUB,
			"PRESENCE_STATUS_HUB"
		},
		{
			Global.PresenceStatus.STORE,
			"PRESENCE_STATUS_STORE"
		},
		{
			Global.PresenceStatus.QUESTLOG,
			"PRESENCE_STATUS_QUESTLOG"
		},
		{
			Global.PresenceStatus.PACKOPENING,
			"PRESENCE_STATUS_PACKOPENING"
		},
		{
			Global.PresenceStatus.COLLECTION,
			"PRESENCE_STATUS_COLLECTION"
		},
		{
			Global.PresenceStatus.DECKEDITOR,
			"PRESENCE_STATUS_DECKEDITOR"
		},
		{
			Global.PresenceStatus.CRAFTING,
			"PRESENCE_STATUS_CRAFTING"
		},
		{
			Global.PresenceStatus.PLAY_DECKPICKER,
			"PRESENCE_STATUS_PLAY_DECKPICKER"
		},
		{
			Global.PresenceStatus.PLAY_QUEUE,
			"PRESENCE_STATUS_PLAY_QUEUE"
		},
		{
			Global.PresenceStatus.PLAY_GAME,
			"PRESENCE_STATUS_PLAY_GAME"
		},
		{
			Global.PresenceStatus.WAIT_FOR_OPPONENT_RECONNECT,
			"PRESENCE_STATUS_WAIT_FOR_OPPONENT_RECONNECT"
		},
		{
			Global.PresenceStatus.PRACTICE_DECKPICKER,
			"PRESENCE_STATUS_PRACTICE_DECKPICKER"
		},
		{
			Global.PresenceStatus.PRACTICE_GAME,
			"PRESENCE_STATUS_PRACTICE_GAME"
		},
		{
			Global.PresenceStatus.ARENA_PURCHASE,
			"PRESENCE_STATUS_ARENA_PURCHASE"
		},
		{
			Global.PresenceStatus.ARENA_FORGE,
			"PRESENCE_STATUS_ARENA_FORGE"
		},
		{
			Global.PresenceStatus.ARENA_IDLE,
			"PRESENCE_STATUS_ARENA_IDLE"
		},
		{
			Global.PresenceStatus.ARENA_QUEUE,
			"PRESENCE_STATUS_ARENA_QUEUE"
		},
		{
			Global.PresenceStatus.ARENA_GAME,
			"PRESENCE_STATUS_ARENA_GAME"
		},
		{
			Global.PresenceStatus.ARENA_REWARD,
			"PRESENCE_STATUS_ARENA_REWARD"
		},
		{
			Global.PresenceStatus.FRIENDLY_DECKPICKER,
			"PRESENCE_STATUS_FRIENDLY_DECKPICKER"
		},
		{
			Global.PresenceStatus.FRIENDLY_GAME,
			"PRESENCE_STATUS_FRIENDLY_GAME"
		},
		{
			Global.PresenceStatus.ADVENTURE_CHOOSING_MODE,
			"PRESENCE_STATUS_ADVENTURE_CHOOSING_MODE"
		},
		{
			Global.PresenceStatus.ADVENTURE_SCENARIO_SELECT,
			"PRESENCE_STATUS_ADVENTURE_SCENARIO_SELECT"
		},
		{
			Global.PresenceStatus.ADVENTURE_SCENARIO_PLAYING_GAME,
			"PRESENCE_STATUS_ADVENTURE_SCENARIO_PLAYING_GAME"
		},
		{
			Global.PresenceStatus.TAVERN_BRAWL_SCREEN,
			"PRESENCE_STATUS_TAVERN_BRAWL_SCREEN"
		},
		{
			Global.PresenceStatus.TAVERN_BRAWL_DECKEDITOR,
			"PRESENCE_STATUS_TAVERN_BRAWL_DECKEDITOR"
		},
		{
			Global.PresenceStatus.TAVERN_BRAWL_QUEUE,
			"PRESENCE_STATUS_TAVERN_BRAWL_QUEUE"
		},
		{
			Global.PresenceStatus.TAVERN_BRAWL_GAME,
			"PRESENCE_STATUS_TAVERN_BRAWL_GAME"
		},
		{
			Global.PresenceStatus.TAVERN_BRAWL_FRIENDLY_WAITING,
			"PRESENCE_STATUS_TAVERN_BRAWL_FRIENDLY_WAITING"
		},
		{
			Global.PresenceStatus.TAVERN_BRAWL_FRIENDLY_GAME,
			"PRESENCE_STATUS_TAVERN_BRAWL_FRIENDLY_GAME"
		},
		{
			Global.PresenceStatus.SPECTATING_GAME_TUTORIAL,
			"PRESENCE_STATUS_SPECTATING_GAME_TUTORIAL"
		},
		{
			Global.PresenceStatus.SPECTATING_GAME_PRACTICE,
			"PRESENCE_STATUS_SPECTATING_GAME_PRACTICE"
		},
		{
			Global.PresenceStatus.SPECTATING_GAME_PLAY,
			"PRESENCE_STATUS_SPECTATING_GAME_PLAY"
		},
		{
			Global.PresenceStatus.SPECTATING_GAME_ARENA,
			"PRESENCE_STATUS_SPECTATING_GAME_ARENA"
		},
		{
			Global.PresenceStatus.SPECTATING_GAME_FRIENDLY,
			"PRESENCE_STATUS_SPECTATING_GAME_FRIENDLY"
		},
		{
			Global.PresenceStatus.SPECTATING_GAME_TAVERN_BRAWL,
			"PRESENCE_STATUS_SPECTATING_GAME_TAVERN_BRAWL"
		},
		{
			Global.PresenceStatus.SPECTATING_GAME_RETURNING_PLAYER_CHALLENGE,
			"PRESENCE_STATUS_SPECTATING_GAME_RETURNING_PLAYER_CHALLENGE"
		},
		{
			Global.PresenceStatus.FIRESIDE_BRAWL_SCREEN,
			"PRESENCE_STATUS_FIRESIDE_BRAWL_SCREEN"
		},
		{
			Global.PresenceStatus.PLAY_RANKED_STANDARD,
			"PRESENCE_STATUS_PLAY_RANKED_STANDARD"
		},
		{
			Global.PresenceStatus.PLAY_RANKED_WILD,
			"PRESENCE_STATUS_PLAY_RANKED_WILD"
		},
		{
			Global.PresenceStatus.PLAY_RANKED_CLASSIC,
			"PRESENCE_STATUS_PLAY_RANKED_CLASSIC"
		},
		{
			Global.PresenceStatus.PLAY_CASUAL_STANDARD,
			"PRESENCE_STATUS_PLAY_CASUAL_STANDARD"
		},
		{
			Global.PresenceStatus.PLAY_CASUAL_WILD,
			"PRESENCE_STATUS_PLAY_CASUAL_WILD"
		},
		{
			Global.PresenceStatus.PLAY_CASUAL_CLASSIC,
			"PRESENCE_STATUS_PLAY_CASUAL_CLASSIC"
		},
		{
			PresenceTutorial.HOGGER,
			"PRESENCE_TUTORIAL_HOGGER"
		},
		{
			PresenceTutorial.MILLHOUSE,
			"PRESENCE_TUTORIAL_MILLHOUSE"
		},
		{
			PresenceTutorial.MUKLA,
			"PRESENCE_TUTORIAL_MUKLA"
		},
		{
			PresenceTutorial.HEMET,
			"PRESENCE_TUTORIAL_HEMET"
		},
		{
			PresenceTutorial.ILLIDAN,
			"PRESENCE_TUTORIAL_ILLIDAN"
		},
		{
			PresenceTutorial.CHO,
			"PRESENCE_TUTORIAL_CHO"
		},
		{
			PresenceAdventureMode.RETURNING_PLAYER_CHALLENGE,
			"PRESENCE_ADVENTURE_MODE_RETURNING_PLAYER_CHALLENGE"
		},
		{
			PresenceAdventureMode.NAXX_NORMAL,
			"PRESENCE_ADVENTURE_MODE_NAXX_NORMAL"
		},
		{
			PresenceAdventureMode.NAXX_HEROIC,
			"PRESENCE_ADVENTURE_MODE_NAXX_HEROIC"
		},
		{
			PresenceAdventureMode.NAXX_CLASS_CHALLENGE,
			"PRESENCE_ADVENTURE_MODE_NAXX_CLASS_CHALLENGE"
		},
		{
			Global.PresenceStatus.SPECTATING_GAME_ADVENTURE_NAXX_NORMAL,
			"PRESENCE_STATUS_SPECTATING_GAME_ADVENTURE_NAXX_NORMAL"
		},
		{
			Global.PresenceStatus.SPECTATING_GAME_ADVENTURE_NAXX_HEROIC,
			"PRESENCE_STATUS_SPECTATING_GAME_ADVENTURE_NAXX_HEROIC"
		},
		{
			Global.PresenceStatus.SPECTATING_GAME_ADVENTURE_NAXX_CLASS_CHALLENGE,
			"PRESENCE_STATUS_SPECTATING_GAME_ADVENTURE_NAXX_CLASS_CHALLENGE"
		},
		{
			PresenceAdventureMode.BRM_NORMAL,
			"PRESENCE_ADVENTURE_MODE_BRM_NORMAL"
		},
		{
			PresenceAdventureMode.BRM_HEROIC,
			"PRESENCE_ADVENTURE_MODE_BRM_HEROIC"
		},
		{
			PresenceAdventureMode.BRM_CLASS_CHALLENGE,
			"PRESENCE_ADVENTURE_MODE_BRM_CLASS_CHALLENGE"
		},
		{
			Global.PresenceStatus.SPECTATING_GAME_ADVENTURE_BRM_NORMAL,
			"PRESENCE_STATUS_SPECTATING_GAME_ADVENTURE_BRM_NORMAL"
		},
		{
			Global.PresenceStatus.SPECTATING_GAME_ADVENTURE_BRM_HEROIC,
			"PRESENCE_STATUS_SPECTATING_GAME_ADVENTURE_BRM_HEROIC"
		},
		{
			Global.PresenceStatus.SPECTATING_GAME_ADVENTURE_BRM_CLASS_CHALLENGE,
			"PRESENCE_STATUS_SPECTATING_GAME_ADVENTURE_BRM_CLASS_CHALLENGE"
		},
		{
			PresenceAdventureMode.LOE_NORMAL,
			"PRESENCE_ADVENTURE_MODE_LOE_NORMAL"
		},
		{
			PresenceAdventureMode.LOE_HEROIC,
			"PRESENCE_ADVENTURE_MODE_LOE_HEROIC"
		},
		{
			PresenceAdventureMode.LOE_CLASS_CHALLENGE,
			"PRESENCE_ADVENTURE_MODE_LOE_CLASS_CHALLENGE"
		},
		{
			Global.PresenceStatus.SPECTATING_GAME_ADVENTURE_LOE_NORMAL,
			"PRESENCE_STATUS_SPECTATING_GAME_ADVENTURE_LOE_NORMAL"
		},
		{
			Global.PresenceStatus.SPECTATING_GAME_ADVENTURE_LOE_HEROIC,
			"PRESENCE_STATUS_SPECTATING_GAME_ADVENTURE_LOE_HEROIC"
		},
		{
			Global.PresenceStatus.SPECTATING_GAME_ADVENTURE_LOE_CLASS_CHALLENGE,
			"PRESENCE_STATUS_SPECTATING_GAME_ADVENTURE_LOE_CLASS_CHALLENGE"
		},
		{
			PresenceAdventureMode.KAR_NORMAL,
			"PRESENCE_ADVENTURE_MODE_KAR_NORMAL"
		},
		{
			PresenceAdventureMode.KAR_HEROIC,
			"PRESENCE_ADVENTURE_MODE_KAR_HEROIC"
		},
		{
			PresenceAdventureMode.KAR_CLASS_CHALLENGE,
			"PRESENCE_ADVENTURE_MODE_KAR_CLASS_CHALLENGE"
		},
		{
			Global.PresenceStatus.SPECTATING_GAME_ADVENTURE_KAR_NORMAL,
			"PRESENCE_STATUS_SPECTATING_GAME_ADVENTURE_KAR_NORMAL"
		},
		{
			Global.PresenceStatus.SPECTATING_GAME_ADVENTURE_KAR_HEROIC,
			"PRESENCE_STATUS_SPECTATING_GAME_ADVENTURE_KAR_HEROIC"
		},
		{
			Global.PresenceStatus.SPECTATING_GAME_ADVENTURE_KAR_CLASS_CHALLENGE,
			"PRESENCE_STATUS_SPECTATING_GAME_ADVENTURE_KAR_CLASS_CHALLENGE"
		},
		{
			PresenceAdventureMode.ICC_NORMAL,
			"PRESENCE_ADVENTURE_MODE_ICC_NORMAL"
		},
		{
			Global.PresenceStatus.SPECTATING_GAME_ADVENTURE_ICC_NORMAL,
			"PRESENCE_STATUS_SPECTATING_GAME_ADVENTURE_ICC_NORMAL"
		},
		{
			PresenceAdventureMode.LOOT,
			"PRESENCE_ADVENTURE_MODE_LOOT"
		},
		{
			Global.PresenceStatus.SPECTATING_GAME_ADVENTURE_LOOT,
			"PRESENCE_STATUS_SPECTATING_GAME_LOOT"
		},
		{
			PresenceAdventureMode.GIL,
			"PRESENCE_ADVENTURE_MODE_GIL"
		},
		{
			Global.PresenceStatus.SPECTATING_GAME_ADVENTURE_GIL,
			"PRESENCE_STATUS_SPECTATING_GAME_GIL"
		},
		{
			Global.PresenceStatus.SPECTATING_GAME_ADVENTURE_GIL_BONUS_CHALLENGE,
			"PRESENCE_STATUS_SPECTATING_GAME_GIL_BONUS_CHALLENGE"
		},
		{
			PresenceAdventureMode.BOT,
			"PRESENCE_ADVENTURE_MODE_BOT"
		},
		{
			Global.PresenceStatus.SPECTATING_GAME_ADVENTURE_BOT,
			"PRESENCE_STATUS_SPECTATING_GAME_BOT"
		},
		{
			PresenceAdventureMode.TRL,
			"PRESENCE_ADVENTURE_MODE_TRL"
		},
		{
			Global.PresenceStatus.SPECTATING_GAME_ADVENTURE_TRL,
			"PRESENCE_STATUS_SPECTATING_GAME_TRL"
		},
		{
			PresenceAdventureMode.DAL,
			"PRESENCE_ADVENTURE_MODE_DAL"
		},
		{
			Global.PresenceStatus.SPECTATING_GAME_ADVENTURE_DAL,
			"PRESENCE_STATUS_SPECTATING_GAME_DAL"
		},
		{
			Global.PresenceStatus.SPECTATING_GAME_ADVENTURE_DAL_HEROIC,
			"PRESENCE_STATUS_SPECTATING_GAME_DAL_HEROIC"
		},
		{
			PresenceAdventureMode.ULD,
			"PRESENCE_ADVENTURE_MODE_ULD"
		},
		{
			Global.PresenceStatus.SPECTATING_GAME_ADVENTURE_ULD,
			"PRESENCE_STATUS_SPECTATING_GAME_ULD"
		},
		{
			Global.PresenceStatus.SPECTATING_GAME_ADVENTURE_ULD_HEROIC,
			"PRESENCE_STATUS_SPECTATING_GAME_ULD_HEROIC"
		},
		{
			PresenceAdventureMode.DRG,
			"PRESENCE_ADVENTURE_MODE_DRG"
		},
		{
			Global.PresenceStatus.SPECTATING_GAME_ADVENTURE_DRG,
			"PRESENCE_STATUS_SPECTATING_GAME_DRG"
		},
		{
			Global.PresenceStatus.SPECTATING_GAME_ADVENTURE_DRG_HEROIC,
			"PRESENCE_STATUS_SPECTATING_GAME_DRG_HEROIC"
		},
		{
			PresenceAdventureMode.BTP,
			"PRESENCE_ADVENTURE_MODE_BTP"
		},
		{
			Global.PresenceStatus.SPECTATING_GAME_ADVENTURE_BTP,
			"PRESENCE_STATUS_SPECTATING_GAME_BTP"
		},
		{
			PresenceAdventureMode.BTA,
			"PRESENCE_ADVENTURE_MODE_BTA"
		},
		{
			Global.PresenceStatus.SPECTATING_GAME_ADVENTURE_BTA,
			"PRESENCE_STATUS_SPECTATING_GAME_BTA"
		},
		{
			PresenceAdventureMode.BTA_HEROIC,
			"PRESENCE_ADVENTURE_MODE_BTA_HEROIC"
		},
		{
			Global.PresenceStatus.SPECTATING_GAME_ADVENTURE_BTA_HEROIC,
			"PRESENCE_STATUS_SPECTATING_GAME_BTA_HEROIC"
		},
		{
			PresenceAdventureMode.BOH,
			"PRESENCE_ADVENTURE_MODE_BOH"
		},
		{
			Global.PresenceStatus.SPECTATING_GAME_ADVENTURE_BOH,
			"PRESENCE_STATUS_SPECTATING_GAME_BOH"
		},
		{
			PresenceAdventureMode.BOM,
			"PRESENCE_ADVENTURE_MODE_BOM"
		},
		{
			Global.PresenceStatus.SPECTATING_GAME_ADVENTURE_BOM,
			"PRESENCE_STATUS_SPECTATING_GAME_BOM"
		},
		{
			ScenarioDbId.NAXX_ANUBREKHAN,
			"PRESENCE_SCENARIO_NAXX_NORMAL_SCENARIO_ANUBREKHAN"
		},
		{
			ScenarioDbId.NAXX_FAERLINA,
			"PRESENCE_SCENARIO_NAXX_NORMAL_SCENARIO_FAERLINA"
		},
		{
			ScenarioDbId.NAXX_MAEXXNA,
			"PRESENCE_SCENARIO_NAXX_NORMAL_SCENARIO_MAEXXNA"
		},
		{
			ScenarioDbId.NAXX_NOTH,
			"PRESENCE_SCENARIO_NAXX_NORMAL_SCENARIO_NOTH"
		},
		{
			ScenarioDbId.NAXX_HEIGAN,
			"PRESENCE_SCENARIO_NAXX_NORMAL_SCENARIO_HEIGAN"
		},
		{
			ScenarioDbId.NAXX_LOATHEB,
			"PRESENCE_SCENARIO_NAXX_NORMAL_SCENARIO_LOATHEB"
		},
		{
			ScenarioDbId.NAXX_RAZUVIOUS,
			"PRESENCE_SCENARIO_NAXX_NORMAL_SCENARIO_RAZUVIOUS"
		},
		{
			ScenarioDbId.NAXX_GOTHIK,
			"PRESENCE_SCENARIO_NAXX_NORMAL_SCENARIO_GOTHIK"
		},
		{
			ScenarioDbId.NAXX_HORSEMEN,
			"PRESENCE_SCENARIO_NAXX_NORMAL_SCENARIO_HORSEMEN"
		},
		{
			ScenarioDbId.NAXX_PATCHWERK,
			"PRESENCE_SCENARIO_NAXX_NORMAL_SCENARIO_PATCHWERK"
		},
		{
			ScenarioDbId.NAXX_GROBBULUS,
			"PRESENCE_SCENARIO_NAXX_NORMAL_SCENARIO_GROBBULUS"
		},
		{
			ScenarioDbId.NAXX_GLUTH,
			"PRESENCE_SCENARIO_NAXX_NORMAL_SCENARIO_GLUTH"
		},
		{
			ScenarioDbId.NAXX_THADDIUS,
			"PRESENCE_SCENARIO_NAXX_NORMAL_SCENARIO_THADDIUS"
		},
		{
			ScenarioDbId.NAXX_SAPPHIRON,
			"PRESENCE_SCENARIO_NAXX_NORMAL_SCENARIO_SAPPHIRON"
		},
		{
			ScenarioDbId.NAXX_KELTHUZAD,
			"PRESENCE_SCENARIO_NAXX_NORMAL_SCENARIO_KELTHUZAD"
		},
		{
			ScenarioDbId.NAXX_HEROIC_ANUBREKHAN,
			"PRESENCE_SCENARIO_NAXX_HEROIC_SCENARIO_ANUBREKHAN"
		},
		{
			ScenarioDbId.NAXX_HEROIC_FAERLINA,
			"PRESENCE_SCENARIO_NAXX_HEROIC_SCENARIO_FAERLINA"
		},
		{
			ScenarioDbId.NAXX_HEROIC_MAEXXNA,
			"PRESENCE_SCENARIO_NAXX_HEROIC_SCENARIO_MAEXXNA"
		},
		{
			ScenarioDbId.NAXX_HEROIC_NOTH,
			"PRESENCE_SCENARIO_NAXX_HEROIC_SCENARIO_NOTH"
		},
		{
			ScenarioDbId.NAXX_HEROIC_HEIGAN,
			"PRESENCE_SCENARIO_NAXX_HEROIC_SCENARIO_HEIGAN"
		},
		{
			ScenarioDbId.NAXX_HEROIC_LOATHEB,
			"PRESENCE_SCENARIO_NAXX_HEROIC_SCENARIO_LOATHEB"
		},
		{
			ScenarioDbId.NAXX_HEROIC_RAZUVIOUS,
			"PRESENCE_SCENARIO_NAXX_HEROIC_SCENARIO_RAZUVIOUS"
		},
		{
			ScenarioDbId.NAXX_HEROIC_GOTHIK,
			"PRESENCE_SCENARIO_NAXX_HEROIC_SCENARIO_GOTHIK"
		},
		{
			ScenarioDbId.NAXX_HEROIC_HORSEMEN,
			"PRESENCE_SCENARIO_NAXX_HEROIC_SCENARIO_HORSEMEN"
		},
		{
			ScenarioDbId.NAXX_HEROIC_PATCHWERK,
			"PRESENCE_SCENARIO_NAXX_HEROIC_SCENARIO_PATCHWERK"
		},
		{
			ScenarioDbId.NAXX_HEROIC_GROBBULUS,
			"PRESENCE_SCENARIO_NAXX_HEROIC_SCENARIO_GROBBULUS"
		},
		{
			ScenarioDbId.NAXX_HEROIC_GLUTH,
			"PRESENCE_SCENARIO_NAXX_HEROIC_SCENARIO_GLUTH"
		},
		{
			ScenarioDbId.NAXX_HEROIC_THADDIUS,
			"PRESENCE_SCENARIO_NAXX_HEROIC_SCENARIO_THADDIUS"
		},
		{
			ScenarioDbId.NAXX_HEROIC_SAPPHIRON,
			"PRESENCE_SCENARIO_NAXX_HEROIC_SCENARIO_SAPPHIRON"
		},
		{
			ScenarioDbId.NAXX_HEROIC_KELTHUZAD,
			"PRESENCE_SCENARIO_NAXX_HEROIC_SCENARIO_KELTHUZAD"
		},
		{
			ScenarioDbId.NAXX_CHALLENGE_HUNTER_V_LOATHEB,
			"PRESENCE_SCENARIO_NAXX_CLASS_CHALLENGE_HUNTER"
		},
		{
			ScenarioDbId.NAXX_CHALLENGE_WARRIOR_V_GROBBULUS,
			"PRESENCE_SCENARIO_NAXX_CLASS_CHALLENGE_WARRIOR"
		},
		{
			ScenarioDbId.NAXX_CHALLENGE_ROGUE_V_MAEXXNA,
			"PRESENCE_SCENARIO_NAXX_CLASS_CHALLENGE_ROGUE"
		},
		{
			ScenarioDbId.NAXX_CHALLENGE_DRUID_V_FAERLINA,
			"PRESENCE_SCENARIO_NAXX_CLASS_CHALLENGE_DRUID"
		},
		{
			ScenarioDbId.NAXX_CHALLENGE_PRIEST_V_THADDIUS,
			"PRESENCE_SCENARIO_NAXX_CLASS_CHALLENGE_PRIEST"
		},
		{
			ScenarioDbId.NAXX_CHALLENGE_SHAMAN_V_GOTHIK,
			"PRESENCE_SCENARIO_NAXX_CLASS_CHALLENGE_SHAMAN"
		},
		{
			ScenarioDbId.NAXX_CHALLENGE_MAGE_V_HEIGAN,
			"PRESENCE_SCENARIO_NAXX_CLASS_CHALLENGE_MAGE"
		},
		{
			ScenarioDbId.NAXX_CHALLENGE_PALADIN_V_KELTHUZAD,
			"PRESENCE_SCENARIO_NAXX_CLASS_CHALLENGE_PALADIN"
		},
		{
			ScenarioDbId.NAXX_CHALLENGE_WARLOCK_V_HORSEMEN,
			"PRESENCE_SCENARIO_NAXX_CLASS_CHALLENGE_WARLOCK"
		},
		{
			ScenarioDbId.BRM_GRIM_GUZZLER,
			"PRESENCE_SCENARIO_BRM_NORMAL_SCENARIO_GRIM_GUZZLER"
		},
		{
			ScenarioDbId.BRM_DARK_IRON_ARENA,
			"PRESENCE_SCENARIO_BRM_NORMAL_SCENARIO_DARK_IRON_ARENA"
		},
		{
			ScenarioDbId.BRM_THAURISSAN,
			"PRESENCE_SCENARIO_BRM_NORMAL_SCENARIO_THAURISSAN"
		},
		{
			ScenarioDbId.BRM_GARR,
			"PRESENCE_SCENARIO_BRM_NORMAL_SCENARIO_GARR"
		},
		{
			ScenarioDbId.BRM_MAJORDOMO,
			"PRESENCE_SCENARIO_BRM_NORMAL_SCENARIO_MAJORDOMO"
		},
		{
			ScenarioDbId.BRM_BARON_GEDDON,
			"PRESENCE_SCENARIO_BRM_NORMAL_SCENARIO_BARON_GEDDON"
		},
		{
			ScenarioDbId.BRM_OMOKK,
			"PRESENCE_SCENARIO_BRM_NORMAL_SCENARIO_OMOKK"
		},
		{
			ScenarioDbId.BRM_DRAKKISATH,
			"PRESENCE_SCENARIO_BRM_NORMAL_SCENARIO_DRAKKISATH"
		},
		{
			ScenarioDbId.BRM_REND_BLACKHAND,
			"PRESENCE_SCENARIO_BRM_NORMAL_SCENARIO_REND_BLACKHAND"
		},
		{
			ScenarioDbId.BRM_RAZORGORE,
			"PRESENCE_SCENARIO_BRM_NORMAL_SCENARIO_RAZORGORE"
		},
		{
			ScenarioDbId.BRM_VAELASTRASZ,
			"PRESENCE_SCENARIO_BRM_NORMAL_SCENARIO_VAELASTRASZ"
		},
		{
			ScenarioDbId.BRM_CHROMAGGUS,
			"PRESENCE_SCENARIO_BRM_NORMAL_SCENARIO_CHROMAGGUS"
		},
		{
			ScenarioDbId.BRM_NEFARIAN,
			"PRESENCE_SCENARIO_BRM_NORMAL_SCENARIO_NEFARIAN"
		},
		{
			ScenarioDbId.BRM_OMNOTRON,
			"PRESENCE_SCENARIO_BRM_NORMAL_SCENARIO_OMNOTRON"
		},
		{
			ScenarioDbId.BRM_MALORIAK,
			"PRESENCE_SCENARIO_BRM_NORMAL_SCENARIO_MALORIAK"
		},
		{
			ScenarioDbId.BRM_ATRAMEDES,
			"PRESENCE_SCENARIO_BRM_NORMAL_SCENARIO_ATRAMEDES"
		},
		{
			ScenarioDbId.BRM_ZOMBIE_NEF,
			"PRESENCE_SCENARIO_BRM_NORMAL_SCENARIO_ZOMBIE_NEF"
		},
		{
			ScenarioDbId.BRM_HEROIC_GRIM_GUZZLER,
			"PRESENCE_SCENARIO_BRM_HEROIC_SCENARIO_GRIM_GUZZLER"
		},
		{
			ScenarioDbId.BRM_HEROIC_DARK_IRON_ARENA,
			"PRESENCE_SCENARIO_BRM_HEROIC_SCENARIO_DARK_IRON_ARENA"
		},
		{
			ScenarioDbId.BRM_HEROIC_THAURISSAN,
			"PRESENCE_SCENARIO_BRM_HEROIC_SCENARIO_THAURISSAN"
		},
		{
			ScenarioDbId.BRM_HEROIC_GARR,
			"PRESENCE_SCENARIO_BRM_HEROIC_SCENARIO_GARR"
		},
		{
			ScenarioDbId.BRM_HEROIC_MAJORDOMO,
			"PRESENCE_SCENARIO_BRM_HEROIC_SCENARIO_MAJORDOMO"
		},
		{
			ScenarioDbId.BRM_HEROIC_BARON_GEDDON,
			"PRESENCE_SCENARIO_BRM_HEROIC_SCENARIO_BARON_GEDDON"
		},
		{
			ScenarioDbId.BRM_HEROIC_OMOKK,
			"PRESENCE_SCENARIO_BRM_HEROIC_SCENARIO_OMOKK"
		},
		{
			ScenarioDbId.BRM_HEROIC_DRAKKISATH,
			"PRESENCE_SCENARIO_BRM_HEROIC_SCENARIO_DRAKKISATH"
		},
		{
			ScenarioDbId.BRM_HEROIC_REND_BLACKHAND,
			"PRESENCE_SCENARIO_BRM_HEROIC_SCENARIO_REND_BLACKHAND"
		},
		{
			ScenarioDbId.BRM_HEROIC_RAZORGORE,
			"PRESENCE_SCENARIO_BRM_HEROIC_SCENARIO_RAZORGORE"
		},
		{
			ScenarioDbId.BRM_HEROIC_VAELASTRASZ,
			"PRESENCE_SCENARIO_BRM_HEROIC_SCENARIO_VAELASTRASZ"
		},
		{
			ScenarioDbId.BRM_HEROIC_CHROMAGGUS,
			"PRESENCE_SCENARIO_BRM_HEROIC_SCENARIO_CHROMAGGUS"
		},
		{
			ScenarioDbId.BRM_HEROIC_NEFARIAN,
			"PRESENCE_SCENARIO_BRM_HEROIC_SCENARIO_NEFARIAN"
		},
		{
			ScenarioDbId.BRM_HEROIC_OMNOTRON,
			"PRESENCE_SCENARIO_BRM_HEROIC_SCENARIO_OMNOTRON"
		},
		{
			ScenarioDbId.BRM_HEROIC_MALORIAK,
			"PRESENCE_SCENARIO_BRM_HEROIC_SCENARIO_MALORIAK"
		},
		{
			ScenarioDbId.BRM_HEROIC_ATRAMEDES,
			"PRESENCE_SCENARIO_BRM_HEROIC_SCENARIO_ATRAMEDES"
		},
		{
			ScenarioDbId.BRM_HEROIC_ZOMBIE_NEF,
			"PRESENCE_SCENARIO_BRM_HEROIC_SCENARIO_ZOMBIE_NEF"
		},
		{
			ScenarioDbId.BRM_CHALLENGE_HUNTER_V_GUZZLER,
			"PRESENCE_SCENARIO_BRM_CLASS_CHALLENGE_HUNTER"
		},
		{
			ScenarioDbId.BRM_CHALLENGE_WARRIOR_V_GARR,
			"PRESENCE_SCENARIO_BRM_CLASS_CHALLENGE_WARRIOR"
		},
		{
			ScenarioDbId.BRM_CHALLENGE_ROGUE_V_VAELASTRASZ,
			"PRESENCE_SCENARIO_BRM_CLASS_CHALLENGE_ROGUE"
		},
		{
			ScenarioDbId.BRM_CHALLENGE_DRUID_V_BLACKHAND,
			"PRESENCE_SCENARIO_BRM_CLASS_CHALLENGE_DRUID"
		},
		{
			ScenarioDbId.BRM_CHALLENGE_PRIEST_V_DRAKKISATH,
			"PRESENCE_SCENARIO_BRM_CLASS_CHALLENGE_PRIEST"
		},
		{
			ScenarioDbId.BRM_CHALLENGE_SHAMAN_V_GEDDON,
			"PRESENCE_SCENARIO_BRM_CLASS_CHALLENGE_SHAMAN"
		},
		{
			ScenarioDbId.BRM_CHALLENGE_MAGE_V_DARK_IRON_ARENA,
			"PRESENCE_SCENARIO_BRM_CLASS_CHALLENGE_MAGE"
		},
		{
			ScenarioDbId.BRM_CHALLENGE_PALADIN_V_OMNOTRON,
			"PRESENCE_SCENARIO_BRM_CLASS_CHALLENGE_PALADIN"
		},
		{
			ScenarioDbId.BRM_CHALLENGE_WARLOCK_V_RAZORGORE,
			"PRESENCE_SCENARIO_BRM_CLASS_CHALLENGE_WARLOCK"
		},
		{
			ScenarioDbId.LOE_ZINAAR,
			"PRESENCE_SCENARIO_LOE_NORMAL_ZINAAR"
		},
		{
			ScenarioDbId.LOE_SUN_RAIDER_PHAERIX,
			"PRESENCE_SCENARIO_LOE_NORMAL_SUN_RAIDER_PHAERIX"
		},
		{
			ScenarioDbId.LOE_TEMPLE_ESCAPE,
			"PRESENCE_SCENARIO_LOE_NORMAL_TEMPLE_ESCAPE"
		},
		{
			ScenarioDbId.LOE_SCARVASH,
			"PRESENCE_SCENARIO_LOE_NORMAL_SCARVASH"
		},
		{
			ScenarioDbId.LOE_MINE_CART,
			"PRESENCE_SCENARIO_LOE_NORMAL_MINE_CART"
		},
		{
			ScenarioDbId.LOE_ARCHAEDAS,
			"PRESENCE_SCENARIO_LOE_NORMAL_ARCHAEDAS"
		},
		{
			ScenarioDbId.LOE_SLITHERSPEAR,
			"PRESENCE_SCENARIO_LOE_NORMAL_SLITHERSPEAR"
		},
		{
			ScenarioDbId.LOE_GIANTFIN,
			"PRESENCE_SCENARIO_LOE_NORMAL_GIANTFIN"
		},
		{
			ScenarioDbId.LOE_LADY_NAZJAR,
			"PRESENCE_SCENARIO_LOE_NORMAL_LADY_NAZJAR"
		},
		{
			ScenarioDbId.LOE_SKELESAURUS,
			"PRESENCE_SCENARIO_LOE_NORMAL_SKELESAURUS"
		},
		{
			ScenarioDbId.LOE_STEEL_SENTINEL,
			"PRESENCE_SCENARIO_LOE_NORMAL_STEEL_SENTINEL"
		},
		{
			ScenarioDbId.LOE_RAFAAM_1,
			"PRESENCE_SCENARIO_LOE_NORMAL_RAFAAM_1"
		},
		{
			ScenarioDbId.LOE_RAFAAM_2,
			"PRESENCE_SCENARIO_LOE_NORMAL_RAFAAM_2"
		},
		{
			ScenarioDbId.LOE_HEROIC_ZINAAR,
			"PRESENCE_SCENARIO_LOE_HEROIC_ZINAAR"
		},
		{
			ScenarioDbId.LOE_HEROIC_SUN_RAIDER_PHAERIX,
			"PRESENCE_SCENARIO_LOE_HEROIC_SUN_RAIDER_PHAERIX"
		},
		{
			ScenarioDbId.LOE_HEROIC_TEMPLE_ESCAPE,
			"PRESENCE_SCENARIO_LOE_HEROIC_TEMPLE_ESCAPE"
		},
		{
			ScenarioDbId.LOE_HEROIC_SCARVASH,
			"PRESENCE_SCENARIO_LOE_HEROIC_SCARVASH"
		},
		{
			ScenarioDbId.LOE_HEROIC_MINE_CART,
			"PRESENCE_SCENARIO_LOE_HEROIC_MINE_CART"
		},
		{
			ScenarioDbId.LOE_HEROIC_ARCHAEDAS,
			"PRESENCE_SCENARIO_LOE_HEROIC_ARCHAEDAS"
		},
		{
			ScenarioDbId.LOE_HEROIC_SLITHERSPEAR,
			"PRESENCE_SCENARIO_LOE_HEROIC_SLITHERSPEAR"
		},
		{
			ScenarioDbId.LOE_HEROIC_GIANTFIN,
			"PRESENCE_SCENARIO_LOE_HEROIC_GIANTFIN"
		},
		{
			ScenarioDbId.LOE_HEROIC_LADY_NAZJAR,
			"PRESENCE_SCENARIO_LOE_HEROIC_LADY_NAZJAR"
		},
		{
			ScenarioDbId.LOE_HEROIC_SKELESAURUS,
			"PRESENCE_SCENARIO_LOE_HEROIC_SKELESAURUS"
		},
		{
			ScenarioDbId.LOE_HEROIC_STEEL_SENTINEL,
			"PRESENCE_SCENARIO_LOE_HEROIC_STEEL_SENTINEL"
		},
		{
			ScenarioDbId.LOE_HEROIC_RAFAAM_1,
			"PRESENCE_SCENARIO_LOE_HEROIC_RAFAAM_1"
		},
		{
			ScenarioDbId.LOE_HEROIC_RAFAAM_2,
			"PRESENCE_SCENARIO_LOE_HEROIC_RAFAAM_2"
		},
		{
			ScenarioDbId.LOE_CHALLENGE_WARRIOR_V_ZINAAR,
			"PRESENCE_SCENARIO_LOE_CLASS_CHALLENGE_WARRIOR"
		},
		{
			ScenarioDbId.LOE_CHALLENGE_WARLOCK_V_SUN_RAIDER,
			"PRESENCE_SCENARIO_LOE_CLASS_CHALLENGE_WARLOCK"
		},
		{
			ScenarioDbId.LOE_CHALLENGE_DRUID_V_SCARVASH,
			"PRESENCE_SCENARIO_LOE_CLASS_CHALLENGE_DRUID"
		},
		{
			ScenarioDbId.LOE_CHALLENGE_PALADIN_V_ARCHAEDUS,
			"PRESENCE_SCENARIO_LOE_CLASS_CHALLENGE_PALADIN"
		},
		{
			ScenarioDbId.LOE_CHALLENGE_HUNTER_V_SLITHERSPEAR,
			"PRESENCE_SCENARIO_LOE_CLASS_CHALLENGE_HUNTER"
		},
		{
			ScenarioDbId.LOE_CHALLENGE_SHAMAN_V_GIANTFIN,
			"PRESENCE_SCENARIO_LOE_CLASS_CHALLENGE_SHAMAN"
		},
		{
			ScenarioDbId.LOE_CHALLENGE_PRIEST_V_NAZJAR,
			"PRESENCE_SCENARIO_LOE_CLASS_CHALLENGE_PRIEST"
		},
		{
			ScenarioDbId.LOE_CHALLENGE_ROGUE_V_SKELESAURUS,
			"PRESENCE_SCENARIO_LOE_CLASS_CHALLENGE_ROGUE"
		},
		{
			ScenarioDbId.LOE_CHALLENGE_MAGE_V_SENTINEL,
			"PRESENCE_SCENARIO_LOE_CLASS_CHALLENGE_MAGE"
		},
		{
			ScenarioDbId.KAR_PROLOGUE,
			"PRESENCE_SCENARIO_KAR_NORMAL_PROLOGUE"
		},
		{
			ScenarioDbId.KAR_PANTRY,
			"PRESENCE_SCENARIO_KAR_NORMAL_PANTRY"
		},
		{
			ScenarioDbId.KAR_MIRROR,
			"PRESENCE_SCENARIO_KAR_NORMAL_MIRROR"
		},
		{
			ScenarioDbId.KAR_CHESS,
			"PRESENCE_SCENARIO_KAR_NORMAL_CHESS"
		},
		{
			ScenarioDbId.KAR_JULIANNE,
			"PRESENCE_SCENARIO_KAR_NORMAL_JULIANNE"
		},
		{
			ScenarioDbId.KAR_WOLF,
			"PRESENCE_SCENARIO_KAR_NORMAL_WOLF"
		},
		{
			ScenarioDbId.KAR_CRONE,
			"PRESENCE_SCENARIO_KAR_NORMAL_CRONE"
		},
		{
			ScenarioDbId.KAR_CURATOR,
			"PRESENCE_SCENARIO_KAR_NORMAL_CURATOR"
		},
		{
			ScenarioDbId.KAR_NIGHTBANE,
			"PRESENCE_SCENARIO_KAR_NORMAL_NIGHTBANE"
		},
		{
			ScenarioDbId.KAR_ILLHOOF,
			"PRESENCE_SCENARIO_KAR_NORMAL_ILLHOOF"
		},
		{
			ScenarioDbId.KAR_ARAN,
			"PRESENCE_SCENARIO_KAR_NORMAL_ARAN"
		},
		{
			ScenarioDbId.KAR_NETHERSPITE,
			"PRESENCE_SCENARIO_KAR_NORMAL_NETHERSPITE"
		},
		{
			ScenarioDbId.KAR_PORTALS,
			"PRESENCE_SCENARIO_KAR_NORMAL_PORTALS"
		},
		{
			ScenarioDbId.KAR_HEROIC_PROLOGUE,
			"PRESENCE_SCENARIO_KAR_HEROIC_PROLOGUE"
		},
		{
			ScenarioDbId.KAR_HEROIC_PANTRY,
			"PRESENCE_SCENARIO_KAR_HEROIC_PANTRY"
		},
		{
			ScenarioDbId.KAR_HEROIC_MIRROR,
			"PRESENCE_SCENARIO_KAR_HEROIC_MIRROR"
		},
		{
			ScenarioDbId.KAR_HEROIC_CHESS,
			"PRESENCE_SCENARIO_KAR_HEROIC_CHESS"
		},
		{
			ScenarioDbId.KAR_HEROIC_JULIANNE,
			"PRESENCE_SCENARIO_KAR_HEROIC_JULIANNE"
		},
		{
			ScenarioDbId.KAR_HEROIC_WOLF,
			"PRESENCE_SCENARIO_KAR_HEROIC_WOLF"
		},
		{
			ScenarioDbId.KAR_HEROIC_CRONE,
			"PRESENCE_SCENARIO_KAR_HEROIC_CRONE"
		},
		{
			ScenarioDbId.KAR_HEROIC_CURATOR,
			"PRESENCE_SCENARIO_KAR_HEROIC_CURATOR"
		},
		{
			ScenarioDbId.KAR_HEROIC_NIGHTBANE,
			"PRESENCE_SCENARIO_KAR_HEROIC_NIGHTBANE"
		},
		{
			ScenarioDbId.KAR_HEROIC_ILLHOOF,
			"PRESENCE_SCENARIO_KAR_HEROIC_ILLHOOF"
		},
		{
			ScenarioDbId.KAR_HEROIC_ARAN,
			"PRESENCE_SCENARIO_KAR_HEROIC_ARAN"
		},
		{
			ScenarioDbId.KAR_HEROIC_NETHERSPITE,
			"PRESENCE_SCENARIO_KAR_HEROIC_NETHERSPITE"
		},
		{
			ScenarioDbId.KAR_HEROIC_PORTALS,
			"PRESENCE_SCENARIO_KAR_HEROIC_PORTALS"
		},
		{
			ScenarioDbId.KAR_CHALLENGE_SHAMAN_V_MIRROR,
			"PRESENCE_SCENARIO_KAR_CLASS_CHALLENGE_SHAMAN"
		},
		{
			ScenarioDbId.KAR_CHALLENGE_PRIEST_V_PANTRY,
			"PRESENCE_SCENARIO_KAR_CLASS_CHALLENGE_PRIEST"
		},
		{
			ScenarioDbId.KAR_CHALLENGE_PALADIN_V_WOLF,
			"PRESENCE_SCENARIO_KAR_CLASS_CHALLENGE_PALADIN"
		},
		{
			ScenarioDbId.KAR_CHALLENGE_WARLOCK_V_JULIANNE,
			"PRESENCE_SCENARIO_KAR_CLASS_CHALLENGE_WARLOCK"
		},
		{
			ScenarioDbId.KAR_CHALLENGE_HUNTER_V_CURATOR,
			"PRESENCE_SCENARIO_KAR_CLASS_CHALLENGE_HUNTER"
		},
		{
			ScenarioDbId.KAR_CHALLENGE_WARRIOR_V_ILLHOOF,
			"PRESENCE_SCENARIO_KAR_CLASS_CHALLENGE_WARRIOR"
		},
		{
			ScenarioDbId.KAR_CHALLENGE_MAGE_V_NIGHTBANE,
			"PRESENCE_SCENARIO_KAR_CLASS_CHALLENGE_MAGE"
		},
		{
			ScenarioDbId.KAR_CHALLENGE_ROGUE_V_ARAN,
			"PRESENCE_SCENARIO_KAR_CLASS_CHALLENGE_ROGUE"
		},
		{
			ScenarioDbId.KAR_CHALLENGE_DRUID_V_NETHERSPITE,
			"PRESENCE_SCENARIO_KAR_CLASS_CHALLENGE_DRUID"
		},
		{
			ScenarioDbId.RETURNING_PLAYER_CHALLENGE_1,
			"PRESENCE_SCENARIO_RETURNING_PLAYER_CHALLENGE_1"
		},
		{
			ScenarioDbId.RETURNING_PLAYER_CHALLENGE_2,
			"PRESENCE_SCENARIO_RETURNING_PLAYER_CHALLENGE_2"
		},
		{
			ScenarioDbId.RETURNING_PLAYER_CHALLENGE_3,
			"PRESENCE_SCENARIO_RETURNING_PLAYER_CHALLENGE_3"
		},
		{
			ScenarioDbId.ICC_01_LICHKING,
			"PRESENCE_SCENARIO_ICC_NORMAL_LICHKING"
		},
		{
			ScenarioDbId.ICC_04_SINDRAGOSA,
			"PRESENCE_SCENARIO_ICC_NORMAL_SINDRAGOSA"
		},
		{
			ScenarioDbId.ICC_06_MARROWGAR,
			"PRESENCE_SCENARIO_ICC_NORMAL_MARROWGAR"
		},
		{
			ScenarioDbId.ICC_05_LANATHEL,
			"PRESENCE_SCENARIO_ICC_NORMAL_LANATHEL"
		},
		{
			ScenarioDbId.ICC_07_PUTRICIDE,
			"PRESENCE_SCENARIO_ICC_NORMAL_PUTRICIDE"
		},
		{
			ScenarioDbId.ICC_08_FINALE,
			"PRESENCE_SCENARIO_ICC_NORMAL_FINALE"
		},
		{
			ScenarioDbId.ICC_09_SAURFANG,
			"PRESENCE_SCENARIO_ICC_NORMAL_SAURFANG"
		},
		{
			ScenarioDbId.ICC_10_DEATHWHISPER,
			"PRESENCE_SCENARIO_ICC_NORMAL_DEATHWHISPER"
		},
		{
			ScenarioDbId.LOOT_DUNGEON,
			"PRESENCE_SCENARIO_LOOT_DUNGEON"
		},
		{
			ScenarioDbId.GIL_DUNGEON,
			"PRESENCE_SCENARIO_GIL_DUNGEON"
		},
		{
			ScenarioDbId.GIL_BONUS_CHALLENGE,
			"PRESENCE_SCENARIO_GIL_BONUS_CHALLENGE"
		},
		{
			ScenarioDbId.BOTA_MIRROR_PUZZLE_1,
			"PRESENCE_SCENARIO_BOTA_MIRROR_PUZZLE_1"
		},
		{
			ScenarioDbId.BOTA_MIRROR_PUZZLE_2,
			"PRESENCE_SCENARIO_BOTA_MIRROR_PUZZLE_2"
		},
		{
			ScenarioDbId.BOTA_MIRROR_PUZZLE_3,
			"PRESENCE_SCENARIO_BOTA_MIRROR_PUZZLE_3"
		},
		{
			ScenarioDbId.BOTA_MIRROR_PUZZLE_4,
			"PRESENCE_SCENARIO_BOTA_MIRROR_PUZZLE_4"
		},
		{
			ScenarioDbId.BOTA_MIRROR_BOOM,
			"PRESENCE_SCENARIO_BOTA_MIRROR_BOOM"
		},
		{
			ScenarioDbId.BOTA_LETHAL_PUZZLE_1,
			"PRESENCE_SCENARIO_BOTA_LETHAL_PUZZLE_1"
		},
		{
			ScenarioDbId.BOTA_LETHAL_PUZZLE_2,
			"PRESENCE_SCENARIO_BOTA_LETHAL_PUZZLE_2"
		},
		{
			ScenarioDbId.BOTA_LETHAL_PUZZLE_3,
			"PRESENCE_SCENARIO_BOTA_LETHAL_PUZZLE_3"
		},
		{
			ScenarioDbId.BOTA_LETHAL_PUZZLE_4,
			"PRESENCE_SCENARIO_BOTA_LETHAL_PUZZLE_4"
		},
		{
			ScenarioDbId.BOTA_LETHAL_BOOM,
			"PRESENCE_SCENARIO_BOTA_LETHAL_BOOM"
		},
		{
			ScenarioDbId.BOTA_CLEAR_PUZZLE_1,
			"PRESENCE_SCENARIO_BOTA_CLEAR_PUZZLE_1"
		},
		{
			ScenarioDbId.BOTA_CLEAR_PUZZLE_2,
			"PRESENCE_SCENARIO_BOTA_CLEAR_PUZZLE_2"
		},
		{
			ScenarioDbId.BOTA_CLEAR_PUZZLE_3,
			"PRESENCE_SCENARIO_BOTA_CLEAR_PUZZLE_3"
		},
		{
			ScenarioDbId.BOTA_CLEAR_PUZZLE_4,
			"PRESENCE_SCENARIO_BOTA_CLEAR_PUZZLE_4"
		},
		{
			ScenarioDbId.BOTA_CLEAR_BOOM,
			"PRESENCE_SCENARIO_BOTA_CLEAR_BOOM"
		},
		{
			ScenarioDbId.BOTA_SURVIVAL_PUZZLE_1,
			"PRESENCE_SCENARIO_BOTA_SURVIVAL_PUZZLE_1"
		},
		{
			ScenarioDbId.BOTA_SURVIVAL_PUZZLE_2,
			"PRESENCE_SCENARIO_BOTA_SURVIVAL_PUZZLE_2"
		},
		{
			ScenarioDbId.BOTA_SURVIVAL_PUZZLE_3,
			"PRESENCE_SCENARIO_BOTA_SURVIVAL_PUZZLE_3"
		},
		{
			ScenarioDbId.BOTA_SURVIVAL_PUZZLE_4,
			"PRESENCE_SCENARIO_BOTA_SURVIVAL_PUZZLE_4"
		},
		{
			ScenarioDbId.BOTA_SURVIVAL_BOOM,
			"PRESENCE_SCENARIO_BOTA_SURVIVAL_BOOM"
		},
		{
			ScenarioDbId.TRL_DUNGEON,
			"PRESENCE_SCENARIO_TRL_DUNGEON"
		},
		{
			ScenarioDbId.DALA_01_BANK,
			"PRESENCE_SCENARIO_DALA_01_BANK"
		},
		{
			ScenarioDbId.DALA_01_BANK_HEROIC,
			"PRESENCE_SCENARIO_DALA_01_BANK_HEROIC"
		},
		{
			ScenarioDbId.DALA_02_VIOLET_HOLD,
			"PRESENCE_SCENARIO_DALA_02_VIOLET_HOLD"
		},
		{
			ScenarioDbId.DALA_02_VIOLET_HOLD_HEROIC,
			"PRESENCE_SCENARIO_DALA_02_VIOLET_HOLD_HEROIC"
		},
		{
			ScenarioDbId.DALA_03_STREETS,
			"PRESENCE_SCENARIO_DALA_03_STREETS"
		},
		{
			ScenarioDbId.DALA_03_STREETS_HEROIC,
			"PRESENCE_SCENARIO_DALA_03_STREETS_HEROIC"
		},
		{
			ScenarioDbId.DALA_04_UNDERBELLY,
			"PRESENCE_SCENARIO_DALA_04_UNDERBELLY"
		},
		{
			ScenarioDbId.DALA_04_UNDERBELLY_HEROIC,
			"PRESENCE_SCENARIO_DALA_04_UNDERBELLY_HEROIC"
		},
		{
			ScenarioDbId.DALA_05_CITADEL,
			"PRESENCE_SCENARIO_DALA_05_CITADEL"
		},
		{
			ScenarioDbId.DALA_05_CITADEL_HEROIC,
			"PRESENCE_SCENARIO_DALA_05_CITADEL_HEROIC"
		},
		{
			ScenarioDbId.DALA_TAVERN,
			"PRESENCE_SCENARIO_DALA_TAVERN"
		},
		{
			ScenarioDbId.DALA_TAVERN_HEROIC,
			"PRESENCE_SCENARIO_DALA_TAVERN_HEROIC"
		},
		{
			ScenarioDbId.ULDA_CITY,
			"PRESENCE_SCENARIO_ULD_01_CITY"
		},
		{
			ScenarioDbId.ULDA_CITY_HEROIC,
			"PRESENCE_SCENARIO_ULD_01_CITY_HEROIC"
		},
		{
			ScenarioDbId.ULDA_DESERT,
			"PRESENCE_SCENARIO_ULD_02_DESERT"
		},
		{
			ScenarioDbId.ULDA_DESERT_HEROIC,
			"PRESENCE_SCENARIO_ULD_02_DESERT_HEROIC"
		},
		{
			ScenarioDbId.ULDA_TOMB,
			"PRESENCE_SCENARIO_ULD_03_TOMB"
		},
		{
			ScenarioDbId.ULDA_TOMB_HEROIC,
			"PRESENCE_SCENARIO_ULD_03_TOMB_HEROIC"
		},
		{
			ScenarioDbId.ULDA_HALLS,
			"PRESENCE_SCENARIO_ULD_04_HALLS"
		},
		{
			ScenarioDbId.ULDA_HALLS_HEROIC,
			"PRESENCE_SCENARIO_ULD_04_HALLS_HEROIC"
		},
		{
			ScenarioDbId.ULDA_SANCTUM,
			"PRESENCE_SCENARIO_ULD_05_SANCTUM"
		},
		{
			ScenarioDbId.ULDA_SANCTUM_HEROIC,
			"PRESENCE_SCENARIO_ULD_05_SANCTUM_HEROIC"
		},
		{
			ScenarioDbId.ULDA_TAVERN,
			"PRESENCE_SCENARIO_ULD_TAVERN"
		},
		{
			ScenarioDbId.ULDA_TAVERN_HEROIC,
			"PRESENCE_SCENARIO_ULD_TAVERN_HEROIC"
		},
		{
			Global.PresenceStatus.BATTLEGROUNDS_QUEUE,
			"PRESENCE_STATUS_BATTLEGROUNDS_QUEUE"
		},
		{
			Global.PresenceStatus.BATTLEGROUNDS_GAME,
			"PRESENCE_STATUS_BATTLEGROUNDS_GAME"
		},
		{
			Global.PresenceStatus.SPECTATING_GAME_BATTLEGROUNDS,
			"PRESENCE_STATUS_SPECTATING_GAME_BATTLEGROUNDS"
		},
		{
			Global.PresenceStatus.BATTLEGROUNDS_SCREEN,
			"PRESENCE_STATUS_BATTLEGROUNDS_SCREEN"
		},
		{
			Global.PresenceStatus.DUELS_QUEUE,
			"PRESENCE_STATUS_DUELS_QUEUE"
		},
		{
			Global.PresenceStatus.DUELS_GAME,
			"PRESENCE_STATUS_DUELS_GAME"
		},
		{
			Global.PresenceStatus.SPECTATING_GAME_DUELS,
			"PRESENCE_STATUS_SPECTATING_GAME_DUELS"
		},
		{
			Global.PresenceStatus.DUELS_IDLE,
			"PRESENCE_STATUS_DUELS_IDLE"
		},
		{
			Global.PresenceStatus.DUELS_BUILDING_DECK,
			"PRESENCE_STATUS_DUELS_FORGE"
		},
		{
			Global.PresenceStatus.DUELS_PURCHASE,
			"PRESENCE_STATUS_DUELS_PURCHASE"
		},
		{
			Global.PresenceStatus.DUELS_REWARD,
			"PRESENCE_STATUS_DUELS_REWARD"
		},
		{
			Global.PresenceStatus.VIEWING_JOURNAL,
			"PRESENCE_STATUS_VIEWING_JOURNAL"
		},
		{
			ScenarioDbId.DRGA_Evil_01,
			"PRESENCE_SCENARIO_DRGA_EVIL_CHAPTER_01_COIN_01"
		},
		{
			ScenarioDbId.DRGA_Evil_01_Heroic,
			"PRESENCE_SCENARIO_DRGA_EVIL_CHAPTER_01_COIN_01_HEROIC"
		},
		{
			ScenarioDbId.DRGA_Evil_02,
			"PRESENCE_SCENARIO_DRGA_EVIL_CHAPTER_01_COIN_02"
		},
		{
			ScenarioDbId.DRGA_Evil_02_Heroic,
			"PRESENCE_SCENARIO_DRGA_EVIL_CHAPTER_01_COIN_02_HEROIC"
		},
		{
			ScenarioDbId.DRGA_Evil_03,
			"PRESENCE_SCENARIO_DRGA_EVIL_CHAPTER_01_COIN_03"
		},
		{
			ScenarioDbId.DRGA_Evil_03_Heroic,
			"PRESENCE_SCENARIO_DRGA_EVIL_CHAPTER_01_COIN_03_HEROIC"
		},
		{
			ScenarioDbId.DRGA_Evil_04,
			"PRESENCE_SCENARIO_DRGA_EVIL_CHAPTER_02_COIN_01"
		},
		{
			ScenarioDbId.DRGA_Evil_04_Heroic,
			"PRESENCE_SCENARIO_DRGA_EVIL_CHAPTER_02_COIN_01_HEROIC"
		},
		{
			ScenarioDbId.DRGA_Evil_05,
			"PRESENCE_SCENARIO_DRGA_EVIL_CHAPTER_02_COIN_02"
		},
		{
			ScenarioDbId.DRGA_Evil_05_Heroic,
			"PRESENCE_SCENARIO_DRGA_EVIL_CHAPTER_02_COIN_02_HEROIC"
		},
		{
			ScenarioDbId.DRGA_Evil_06,
			"PRESENCE_SCENARIO_DRGA_EVIL_CHAPTER_02_COIN_03"
		},
		{
			ScenarioDbId.DRGA_Evil_06_Heroic,
			"PRESENCE_SCENARIO_DRGA_EVIL_CHAPTER_02_COIN_03_HEROIC"
		},
		{
			ScenarioDbId.DRGA_Evil_07,
			"PRESENCE_SCENARIO_DRGA_EVIL_CHAPTER_03_COIN_01"
		},
		{
			ScenarioDbId.DRGA_Evil_07_Heroic,
			"PRESENCE_SCENARIO_DRGA_EVIL_CHAPTER_03_COIN_01_HEROIC"
		},
		{
			ScenarioDbId.DRGA_Evil_08,
			"PRESENCE_SCENARIO_DRGA_EVIL_CHAPTER_03_COIN_02"
		},
		{
			ScenarioDbId.DRGA_Evil_08_Heroic,
			"PRESENCE_SCENARIO_DRGA_EVIL_CHAPTER_03_COIN_02_HEROIC"
		},
		{
			ScenarioDbId.DRGA_Evil_09,
			"PRESENCE_SCENARIO_DRGA_EVIL_CHAPTER_03_COIN_03"
		},
		{
			ScenarioDbId.DRGA_Evil_09_Heroic,
			"PRESENCE_SCENARIO_DRGA_EVIL_CHAPTER_03_COIN_03_HEROIC"
		},
		{
			ScenarioDbId.DRGA_Evil_10,
			"PRESENCE_SCENARIO_DRGA_EVIL_CHAPTER_04_COIN_01"
		},
		{
			ScenarioDbId.DRGA_Evil_10_Heroic,
			"PRESENCE_SCENARIO_DRGA_EVIL_CHAPTER_04_COIN_01_HEROIC"
		},
		{
			ScenarioDbId.DRGA_Evil_11,
			"PRESENCE_SCENARIO_DRGA_EVIL_CHAPTER_04_COIN_02"
		},
		{
			ScenarioDbId.DRGA_Evil_11_Heroic,
			"PRESENCE_SCENARIO_DRGA_EVIL_CHAPTER_04_COIN_02_HEROIC"
		},
		{
			ScenarioDbId.DRGA_Evil_12,
			"PRESENCE_SCENARIO_DRGA_EVIL_CHAPTER_04_COIN_03"
		},
		{
			ScenarioDbId.DRGA_Evil_12_Heroic,
			"PRESENCE_SCENARIO_DRGA_EVIL_CHAPTER_04_COIN_03_HEROIC"
		},
		{
			ScenarioDbId.DRGA_Good_01,
			"PRESENCE_SCENARIO_DRGA_GOOD_CHAPTER_01_COIN_01"
		},
		{
			ScenarioDbId.DRGA_Good_01_Heroic,
			"PRESENCE_SCENARIO_DRGA_GOOD_CHAPTER_01_COIN_01_HEROIC"
		},
		{
			ScenarioDbId.DRGA_Good_02,
			"PRESENCE_SCENARIO_DRGA_GOOD_CHAPTER_01_COIN_02"
		},
		{
			ScenarioDbId.DRGA_Good_02_Heroic,
			"PRESENCE_SCENARIO_DRGA_GOOD_CHAPTER_01_COIN_02_HEROIC"
		},
		{
			ScenarioDbId.DRGA_Good_03,
			"PRESENCE_SCENARIO_DRGA_GOOD_CHAPTER_01_COIN_03"
		},
		{
			ScenarioDbId.DRGA_Good_03_Heroic,
			"PRESENCE_SCENARIO_DRGA_GOOD_CHAPTER_01_COIN_03_HEROIC"
		},
		{
			ScenarioDbId.DRGA_Good_04,
			"PRESENCE_SCENARIO_DRGA_GOOD_CHAPTER_02_COIN_01"
		},
		{
			ScenarioDbId.DRGA_Good_04_Heroic,
			"PRESENCE_SCENARIO_DRGA_GOOD_CHAPTER_02_COIN_01_HEROIC"
		},
		{
			ScenarioDbId.DRGA_Good_05,
			"PRESENCE_SCENARIO_DRGA_GOOD_CHAPTER_02_COIN_02"
		},
		{
			ScenarioDbId.DRGA_Good_05_Heroic,
			"PRESENCE_SCENARIO_DRGA_GOOD_CHAPTER_02_COIN_02_HEROIC"
		},
		{
			ScenarioDbId.DRGA_Good_06,
			"PRESENCE_SCENARIO_DRGA_GOOD_CHAPTER_02_COIN_03"
		},
		{
			ScenarioDbId.DRGA_Good_06_Heroic,
			"PRESENCE_SCENARIO_DRGA_GOOD_CHAPTER_02_COIN_03_HEROIC"
		},
		{
			ScenarioDbId.DRGA_Good_07,
			"PRESENCE_SCENARIO_DRGA_GOOD_CHAPTER_03_COIN_01"
		},
		{
			ScenarioDbId.DRGA_Good_07_Heroic,
			"PRESENCE_SCENARIO_DRGA_GOOD_CHAPTER_03_COIN_01_HEROIC"
		},
		{
			ScenarioDbId.DRGA_Good_08,
			"PRESENCE_SCENARIO_DRGA_GOOD_CHAPTER_03_COIN_02"
		},
		{
			ScenarioDbId.DRGA_Good_08_Heroic,
			"PRESENCE_SCENARIO_DRGA_GOOD_CHAPTER_03_COIN_02_HEROIC"
		},
		{
			ScenarioDbId.DRGA_Good_09,
			"PRESENCE_SCENARIO_DRGA_GOOD_CHAPTER_03_COIN_03"
		},
		{
			ScenarioDbId.DRGA_Good_09_Heroic,
			"PRESENCE_SCENARIO_DRGA_GOOD_CHAPTER_03_COIN_03_HEROIC"
		},
		{
			ScenarioDbId.DRGA_Good_10,
			"PRESENCE_SCENARIO_DRGA_GOOD_CHAPTER_04_COIN_01"
		},
		{
			ScenarioDbId.DRGA_Good_10_Heroic,
			"PRESENCE_SCENARIO_DRGA_GOOD_CHAPTER_04_COIN_01_HEROIC"
		},
		{
			ScenarioDbId.DRGA_Good_11,
			"PRESENCE_SCENARIO_DRGA_GOOD_CHAPTER_04_COIN_02"
		},
		{
			ScenarioDbId.DRGA_Good_11_Heroic,
			"PRESENCE_SCENARIO_DRGA_GOOD_CHAPTER_04_COIN_02_HEROIC"
		},
		{
			ScenarioDbId.DRGA_Good_12,
			"PRESENCE_SCENARIO_DRGA_GOOD_CHAPTER_04_COIN_03"
		},
		{
			ScenarioDbId.DRGA_Good_12_Heroic,
			"PRESENCE_SCENARIO_DRGA_GOOD_CHAPTER_04_COIN_03_HEROIC"
		},
		{
			ScenarioDbId.BTP_01_AZZINOTH,
			"PRESENCE_SCENARIO_BTP_COIN_01_AZZINOTH"
		},
		{
			ScenarioDbId.BTP_02_XAVIUS,
			"PRESENCE_SCENARIO_BTP_COIN_02_XAVIUS"
		},
		{
			ScenarioDbId.BTP_03_MANNOROTH,
			"PRESENCE_SCENARIO_BTP_COIN_03_MANNOROTH"
		},
		{
			ScenarioDbId.BTP_04_CENARIUS,
			"PRESENCE_SCENARIO_BTP_COIN_04_CENARIUS"
		},
		{
			ScenarioDbId.BTA_01_INQUISITOR_DAKREL,
			"PRESENCE_SCENARIO_BTA_COIN_01_INQUISITOR_DAKREL"
		},
		{
			ScenarioDbId.BTA_02_XUR_GOTH,
			"PRESENCE_SCENARIO_BTA_COIN_02_XUR_GOTH"
		},
		{
			ScenarioDbId.BTA_03_ZIXOR,
			"PRESENCE_SCENARIO_BTA_COIN_03_ZIXOR"
		},
		{
			ScenarioDbId.BTA_04_BALTHARAK,
			"PRESENCE_SCENARIO_BTA_COIN_04_BALTHARAK"
		},
		{
			ScenarioDbId.BTA_05_KANRETHAD_PRIME,
			"PRESENCE_SCENARIO_BTA_COIN_05_KANRETHAD_PRIME"
		},
		{
			ScenarioDbId.BTA_06_BURGRAK_CRUELCHAIN,
			"PRESENCE_SCENARIO_BTA_COIN_06_BURGRAK_CRUELCHAIN"
		},
		{
			ScenarioDbId.BTA_07_FELSTORM_RUN,
			"PRESENCE_SCENARIO_BTA_COIN_07_FELSTORM_RUN"
		},
		{
			ScenarioDbId.BTA_08_MOTHER_SHAHRAZ,
			"PRESENCE_SCENARIO_BTA_COIN_08_MOTHER_SHAHRAZ"
		},
		{
			ScenarioDbId.BTA_09_SHAL_JA_OUTCAST,
			"PRESENCE_SCENARIO_BTA_COIN_09_SHAL_JA_OUTCAST"
		},
		{
			ScenarioDbId.BTA_10_KARNUK_OUTCAST,
			"PRESENCE_SCENARIO_BTA_COIN_10_KARNUK_OUTCAST"
		},
		{
			ScenarioDbId.BTA_11_JEK_HAZ,
			"PRESENCE_SCENARIO_BTA_COIN_11_JEK_HAZ"
		},
		{
			ScenarioDbId.BTA_12_MAGTHERIDON_PRIME,
			"PRESENCE_SCENARIO_BTA_COIN_12_MAGTHERIDON_PRIME"
		},
		{
			ScenarioDbId.BTA_13_GOK_AMOK,
			"PRESENCE_SCENARIO_BTA_COIN_13_GOK_AMOK"
		},
		{
			ScenarioDbId.BTA_14_FLIKK,
			"PRESENCE_SCENARIO_BTA_COIN_14_FLIKK"
		},
		{
			ScenarioDbId.BTA_15_BADUU_CORRUPTED,
			"PRESENCE_SCENARIO_BTA_COIN_15_BADUU_CORRUPTED"
		},
		{
			ScenarioDbId.BTA_16_MECHA_JARAXXUS,
			"PRESENCE_SCENARIO_BTA_COIN_16_MECHA_JARAXXUS"
		},
		{
			ScenarioDbId.BTA_17_ILLIDAN_STORMRAGE,
			"PRESENCE_SCENARIO_BTA_COIN_17_ILLIDAN_STORMRAGE"
		},
		{
			ScenarioDbId.BTA_Heroic_KAZZAK,
			"PRESENCE_SCENARIO_BTA_HEROIC_COIN_01_KAZZAK"
		},
		{
			ScenarioDbId.BTA_Heroic_GRUUL,
			"PRESENCE_SCENARIO_BTA_HEROIC_COIN_02_GRUUL"
		},
		{
			ScenarioDbId.BTA_Heroic_MAGTHERIDON,
			"PRESENCE_SCENARIO_BTA_HEROIC_COIN_03_MAGTHERIDON"
		},
		{
			ScenarioDbId.BTA_Heroic_SUPREMUS,
			"PRESENCE_SCENARIO_BTA_HEROIC_COIN_04_SUPREMUS"
		},
		{
			ScenarioDbId.BTA_Heroic_TERON_GOREFIEND,
			"PRESENCE_SCENARIO_BTA_HEROIC_COIN_05_TERON_GOREFIEND"
		},
		{
			ScenarioDbId.BTA_Heroic_MOTHER_SHARAZ,
			"PRESENCE_SCENARIO_BTA_HEROIC_COIN_06_MOTHER_SHARAZ"
		},
		{
			ScenarioDbId.BTA_Heroic_LADY_VASHJ,
			"PRESENCE_SCENARIO_BTA_HEROIC_COIN_07_LADY_VASHJ"
		},
		{
			ScenarioDbId.BTA_Heroic_KAELTHAS,
			"PRESENCE_SCENARIO_BTA_HEROIC_COIN_08_KAELTHAS"
		},
		{
			ScenarioDbId.BTA_Heroic_ILLIDAN,
			"PRESENCE_SCENARIO_BTA_HEROIC_COIN_09_ILLIDAN"
		},
		{
			ScenarioDbId.BOH_JAINA_01,
			"PRESENCE_SCENARIO_BOH_FIGHT_01"
		},
		{
			ScenarioDbId.BOH_JAINA_02,
			"PRESENCE_SCENARIO_BOH_FIGHT_02"
		},
		{
			ScenarioDbId.BOH_JAINA_03,
			"PRESENCE_SCENARIO_BOH_FIGHT_03"
		},
		{
			ScenarioDbId.BOH_JAINA_04,
			"PRESENCE_SCENARIO_BOH_FIGHT_04"
		},
		{
			ScenarioDbId.BOH_JAINA_05,
			"PRESENCE_SCENARIO_BOH_FIGHT_05"
		},
		{
			ScenarioDbId.BOH_JAINA_06,
			"PRESENCE_SCENARIO_BOH_FIGHT_06"
		},
		{
			ScenarioDbId.BOH_JAINA_07,
			"PRESENCE_SCENARIO_BOH_FIGHT_07"
		},
		{
			ScenarioDbId.BOH_JAINA_08,
			"PRESENCE_SCENARIO_BOH_FIGHT_08"
		},
		{
			ScenarioDbId.BOH_REXXAR_01,
			"PRESENCE_SCENARIO_BOH2_FIGHT_01"
		},
		{
			ScenarioDbId.BOH_REXXAR_02,
			"PRESENCE_SCENARIO_BOH2_FIGHT_02"
		},
		{
			ScenarioDbId.BOH_REXXAR_03,
			"PRESENCE_SCENARIO_BOH2_FIGHT_03"
		},
		{
			ScenarioDbId.BOH_REXXAR_04,
			"PRESENCE_SCENARIO_BOH2_FIGHT_04"
		},
		{
			ScenarioDbId.BOH_REXXAR_05,
			"PRESENCE_SCENARIO_BOH2_FIGHT_05"
		},
		{
			ScenarioDbId.BOH_REXXAR_06,
			"PRESENCE_SCENARIO_BOH2_FIGHT_06"
		},
		{
			ScenarioDbId.BOH_REXXAR_07,
			"PRESENCE_SCENARIO_BOH2_FIGHT_07"
		},
		{
			ScenarioDbId.BOH_REXXAR_08,
			"PRESENCE_SCENARIO_BOH2_FIGHT_08"
		},
		{
			ScenarioDbId.BOH_GARROSH_01,
			"PRESENCE_SCENARIO_BOH3_FIGHT_01"
		},
		{
			ScenarioDbId.BOH_GARROSH_02,
			"PRESENCE_SCENARIO_BOH3_FIGHT_02"
		},
		{
			ScenarioDbId.BOH_GARROSH_03,
			"PRESENCE_SCENARIO_BOH3_FIGHT_03"
		},
		{
			ScenarioDbId.BOH_GARROSH_04,
			"PRESENCE_SCENARIO_BOH3_FIGHT_04"
		},
		{
			ScenarioDbId.BOH_GARROSH_05,
			"PRESENCE_SCENARIO_BOH3_FIGHT_05"
		},
		{
			ScenarioDbId.BOH_GARROSH_06,
			"PRESENCE_SCENARIO_BOH3_FIGHT_06"
		},
		{
			ScenarioDbId.BOH_GARROSH_07,
			"PRESENCE_SCENARIO_BOH3_FIGHT_07"
		},
		{
			ScenarioDbId.BOH_GARROSH_08,
			"PRESENCE_SCENARIO_BOH3_FIGHT_08"
		},
		{
			ScenarioDbId.BOH_UTHER_01,
			"PRESENCE_SCENARIO_BOH4_FIGHT_01"
		},
		{
			ScenarioDbId.BOH_UTHER_02,
			"PRESENCE_SCENARIO_BOH4_FIGHT_02"
		},
		{
			ScenarioDbId.BOH_UTHER_03,
			"PRESENCE_SCENARIO_BOH4_FIGHT_03"
		},
		{
			ScenarioDbId.BOH_UTHER_04,
			"PRESENCE_SCENARIO_BOH4_FIGHT_04"
		},
		{
			ScenarioDbId.BOH_UTHER_05,
			"PRESENCE_SCENARIO_BOH4_FIGHT_05"
		},
		{
			ScenarioDbId.BOH_UTHER_06,
			"PRESENCE_SCENARIO_BOH4_FIGHT_06"
		},
		{
			ScenarioDbId.BOH_UTHER_07,
			"PRESENCE_SCENARIO_BOH4_FIGHT_07"
		},
		{
			ScenarioDbId.BOH_UTHER_08,
			"PRESENCE_SCENARIO_BOH4_FIGHT_08"
		},
		{
			ScenarioDbId.BOH_ANDUIN_01,
			"PRESENCE_SCENARIO_BOH5_FIGHT_01"
		},
		{
			ScenarioDbId.BOH_ANDUIN_02,
			"PRESENCE_SCENARIO_BOH5_FIGHT_02"
		},
		{
			ScenarioDbId.BOH_ANDUIN_03,
			"PRESENCE_SCENARIO_BOH5_FIGHT_03"
		},
		{
			ScenarioDbId.BOH_ANDUIN_04,
			"PRESENCE_SCENARIO_BOH5_FIGHT_04"
		},
		{
			ScenarioDbId.BOH_ANDUIN_05,
			"PRESENCE_SCENARIO_BOH5_FIGHT_05"
		},
		{
			ScenarioDbId.BOH_ANDUIN_06,
			"PRESENCE_SCENARIO_BOH5_FIGHT_06"
		},
		{
			ScenarioDbId.BOH_ANDUIN_07,
			"PRESENCE_SCENARIO_BOH5_FIGHT_07"
		},
		{
			ScenarioDbId.BOH_ANDUIN_08,
			"PRESENCE_SCENARIO_BOH5_FIGHT_08"
		},
		{
			ScenarioDbId.BOH_VALEERA_01,
			"PRESENCE_SCENARIO_BOH6_FIGHT_01"
		},
		{
			ScenarioDbId.BOH_VALEERA_02,
			"PRESENCE_SCENARIO_BOH6_FIGHT_02"
		},
		{
			ScenarioDbId.BOH_VALEERA_03,
			"PRESENCE_SCENARIO_BOH6_FIGHT_03"
		},
		{
			ScenarioDbId.BOH_VALEERA_04,
			"PRESENCE_SCENARIO_BOH6_FIGHT_04"
		},
		{
			ScenarioDbId.BOH_VALEERA_05,
			"PRESENCE_SCENARIO_BOH6_FIGHT_05"
		},
		{
			ScenarioDbId.BOH_VALEERA_06,
			"PRESENCE_SCENARIO_BOH6_FIGHT_06"
		},
		{
			ScenarioDbId.BOH_VALEERA_07,
			"PRESENCE_SCENARIO_BOH6_FIGHT_07"
		},
		{
			ScenarioDbId.BOH_VALEERA_08,
			"PRESENCE_SCENARIO_BOH6_FIGHT_08"
		},
		{
			ScenarioDbId.BOH_THRALL_01,
			"PRESENCE_SCENARIO_BOH7_FIGHT_01"
		},
		{
			ScenarioDbId.BOH_THRALL_02,
			"PRESENCE_SCENARIO_BOH7_FIGHT_02"
		},
		{
			ScenarioDbId.BOH_THRALL_03,
			"PRESENCE_SCENARIO_BOH7_FIGHT_03"
		},
		{
			ScenarioDbId.BOH_THRALL_04,
			"PRESENCE_SCENARIO_BOH7_FIGHT_04"
		},
		{
			ScenarioDbId.BOH_THRALL_05,
			"PRESENCE_SCENARIO_BOH7_FIGHT_05"
		},
		{
			ScenarioDbId.BOH_THRALL_06,
			"PRESENCE_SCENARIO_BOH7_FIGHT_06"
		},
		{
			ScenarioDbId.BOH_THRALL_07,
			"PRESENCE_SCENARIO_BOH7_FIGHT_07"
		},
		{
			ScenarioDbId.BOH_THRALL_08,
			"PRESENCE_SCENARIO_BOH7_FIGHT_08"
		},
		{
			ScenarioDbId.BOH_MALFURION_01,
			"PRESENCE_SCENARIO_BOH8_FIGHT_01"
		},
		{
			ScenarioDbId.BOH_MALFURION_02,
			"PRESENCE_SCENARIO_BOH8_FIGHT_02"
		},
		{
			ScenarioDbId.BOH_MALFURION_03,
			"PRESENCE_SCENARIO_BOH8_FIGHT_03"
		},
		{
			ScenarioDbId.BOH_MALFURION_04,
			"PRESENCE_SCENARIO_BOH8_FIGHT_04"
		},
		{
			ScenarioDbId.BOH_MALFURION_05,
			"PRESENCE_SCENARIO_BOH8_FIGHT_05"
		},
		{
			ScenarioDbId.BOH_MALFURION_06,
			"PRESENCE_SCENARIO_BOH8_FIGHT_06"
		},
		{
			ScenarioDbId.BOH_MALFURION_07,
			"PRESENCE_SCENARIO_BOH8_FIGHT_07"
		},
		{
			ScenarioDbId.BOH_MALFURION_08,
			"PRESENCE_SCENARIO_BOH8_FIGHT_08"
		},
		{
			ScenarioDbId.BOM_01_Rokara_01,
			"PRESENCE_SCENARIO_BOM_FIGHT_01"
		},
		{
			ScenarioDbId.BOM_01_Rokara_02,
			"PRESENCE_SCENARIO_BOM_FIGHT_02"
		},
		{
			ScenarioDbId.BOM_01_Rokara_03,
			"PRESENCE_SCENARIO_BOM_FIGHT_03"
		},
		{
			ScenarioDbId.BOM_01_Rokara_04,
			"PRESENCE_SCENARIO_BOM_FIGHT_04"
		},
		{
			ScenarioDbId.BOM_01_Rokara_05,
			"PRESENCE_SCENARIO_BOM_FIGHT_05"
		},
		{
			ScenarioDbId.BOM_01_Rokara_06,
			"PRESENCE_SCENARIO_BOM_FIGHT_06"
		},
		{
			ScenarioDbId.BOM_01_Rokara_07,
			"PRESENCE_SCENARIO_BOM_FIGHT_07"
		},
		{
			ScenarioDbId.BOM_01_Rokara_08,
			"PRESENCE_SCENARIO_BOM_FIGHT_08"
		},
		{
			ScenarioDbId.BOM_02_Xyrella_01,
			"PRESENCE_SCENARIO_BOM2_FIGHT_01"
		},
		{
			ScenarioDbId.BOM_02_Xyrella_02,
			"PRESENCE_SCENARIO_BOM2_FIGHT_02"
		},
		{
			ScenarioDbId.BOM_02_Xyrella_03,
			"PRESENCE_SCENARIO_BOM2_FIGHT_03"
		},
		{
			ScenarioDbId.BOM_02_Xyrella_04,
			"PRESENCE_SCENARIO_BOM2_FIGHT_04"
		},
		{
			ScenarioDbId.BOM_02_Xyrella_05,
			"PRESENCE_SCENARIO_BOM2_FIGHT_05"
		},
		{
			ScenarioDbId.BOM_02_Xyrella_06,
			"PRESENCE_SCENARIO_BOM2_FIGHT_06"
		},
		{
			ScenarioDbId.BOM_02_Xyrella_07,
			"PRESENCE_SCENARIO_BOM2_FIGHT_07"
		},
		{
			ScenarioDbId.BOM_02_Xyrella_08,
			"PRESENCE_SCENARIO_BOM2_FIGHT_08"
		},
		{
			ScenarioDbId.BOM_03_Guff_01,
			"PRESENCE_SCENARIO_BOM3_FIGHT_01"
		},
		{
			ScenarioDbId.BOM_03_Guff_02,
			"PRESENCE_SCENARIO_BOM3_FIGHT_02"
		},
		{
			ScenarioDbId.BOM_03_Guff_03,
			"PRESENCE_SCENARIO_BOM3_FIGHT_03"
		},
		{
			ScenarioDbId.BOM_03_Guff_04,
			"PRESENCE_SCENARIO_BOM3_FIGHT_04"
		},
		{
			ScenarioDbId.BOM_03_Guff_05,
			"PRESENCE_SCENARIO_BOM3_FIGHT_05"
		},
		{
			ScenarioDbId.BOM_03_Guff_06,
			"PRESENCE_SCENARIO_BOM3_FIGHT_06"
		},
		{
			ScenarioDbId.BOM_03_Guff_07,
			"PRESENCE_SCENARIO_BOM3_FIGHT_07"
		},
		{
			ScenarioDbId.BOM_03_Guff_08,
			"PRESENCE_SCENARIO_BOM3_FIGHT_08"
		}
	};

	// Token: 0x04000699 RID: 1689
	private static readonly global::Map<KeyValuePair<AdventureDbId, AdventureModeDbId>, PresenceMgr.PresenceTargets> s_adventurePresenceMap = new global::Map<KeyValuePair<AdventureDbId, AdventureModeDbId>, PresenceMgr.PresenceTargets>
	{
		{
			new KeyValuePair<AdventureDbId, AdventureModeDbId>(AdventureDbId.NAXXRAMAS, AdventureModeDbId.LINEAR),
			new PresenceMgr.PresenceTargets(PresenceAdventureMode.NAXX_NORMAL, Global.PresenceStatus.SPECTATING_GAME_ADVENTURE_NAXX_NORMAL)
		},
		{
			new KeyValuePair<AdventureDbId, AdventureModeDbId>(AdventureDbId.NAXXRAMAS, AdventureModeDbId.LINEAR_HEROIC),
			new PresenceMgr.PresenceTargets(PresenceAdventureMode.NAXX_HEROIC, Global.PresenceStatus.SPECTATING_GAME_ADVENTURE_NAXX_HEROIC)
		},
		{
			new KeyValuePair<AdventureDbId, AdventureModeDbId>(AdventureDbId.NAXXRAMAS, AdventureModeDbId.CLASS_CHALLENGE),
			new PresenceMgr.PresenceTargets(PresenceAdventureMode.NAXX_CLASS_CHALLENGE, Global.PresenceStatus.SPECTATING_GAME_ADVENTURE_NAXX_CLASS_CHALLENGE)
		},
		{
			new KeyValuePair<AdventureDbId, AdventureModeDbId>(AdventureDbId.BRM, AdventureModeDbId.LINEAR),
			new PresenceMgr.PresenceTargets(PresenceAdventureMode.BRM_NORMAL, Global.PresenceStatus.SPECTATING_GAME_ADVENTURE_BRM_NORMAL)
		},
		{
			new KeyValuePair<AdventureDbId, AdventureModeDbId>(AdventureDbId.BRM, AdventureModeDbId.LINEAR_HEROIC),
			new PresenceMgr.PresenceTargets(PresenceAdventureMode.BRM_HEROIC, Global.PresenceStatus.SPECTATING_GAME_ADVENTURE_BRM_HEROIC)
		},
		{
			new KeyValuePair<AdventureDbId, AdventureModeDbId>(AdventureDbId.BRM, AdventureModeDbId.CLASS_CHALLENGE),
			new PresenceMgr.PresenceTargets(PresenceAdventureMode.BRM_CLASS_CHALLENGE, Global.PresenceStatus.SPECTATING_GAME_ADVENTURE_BRM_CLASS_CHALLENGE)
		},
		{
			new KeyValuePair<AdventureDbId, AdventureModeDbId>(AdventureDbId.LOE, AdventureModeDbId.LINEAR),
			new PresenceMgr.PresenceTargets(PresenceAdventureMode.LOE_NORMAL, Global.PresenceStatus.SPECTATING_GAME_ADVENTURE_LOE_NORMAL)
		},
		{
			new KeyValuePair<AdventureDbId, AdventureModeDbId>(AdventureDbId.LOE, AdventureModeDbId.LINEAR_HEROIC),
			new PresenceMgr.PresenceTargets(PresenceAdventureMode.LOE_HEROIC, Global.PresenceStatus.SPECTATING_GAME_ADVENTURE_LOE_HEROIC)
		},
		{
			new KeyValuePair<AdventureDbId, AdventureModeDbId>(AdventureDbId.LOE, AdventureModeDbId.CLASS_CHALLENGE),
			new PresenceMgr.PresenceTargets(PresenceAdventureMode.LOE_CLASS_CHALLENGE, Global.PresenceStatus.SPECTATING_GAME_ADVENTURE_LOE_CLASS_CHALLENGE)
		},
		{
			new KeyValuePair<AdventureDbId, AdventureModeDbId>(AdventureDbId.KARA, AdventureModeDbId.LINEAR),
			new PresenceMgr.PresenceTargets(PresenceAdventureMode.KAR_NORMAL, Global.PresenceStatus.SPECTATING_GAME_ADVENTURE_KAR_NORMAL)
		},
		{
			new KeyValuePair<AdventureDbId, AdventureModeDbId>(AdventureDbId.KARA, AdventureModeDbId.LINEAR_HEROIC),
			new PresenceMgr.PresenceTargets(PresenceAdventureMode.KAR_HEROIC, Global.PresenceStatus.SPECTATING_GAME_ADVENTURE_KAR_HEROIC)
		},
		{
			new KeyValuePair<AdventureDbId, AdventureModeDbId>(AdventureDbId.KARA, AdventureModeDbId.CLASS_CHALLENGE),
			new PresenceMgr.PresenceTargets(PresenceAdventureMode.KAR_CLASS_CHALLENGE, Global.PresenceStatus.SPECTATING_GAME_ADVENTURE_KAR_CLASS_CHALLENGE)
		},
		{
			new KeyValuePair<AdventureDbId, AdventureModeDbId>(AdventureDbId.ICC, AdventureModeDbId.LINEAR),
			new PresenceMgr.PresenceTargets(PresenceAdventureMode.ICC_NORMAL, Global.PresenceStatus.SPECTATING_GAME_ADVENTURE_ICC_NORMAL)
		},
		{
			new KeyValuePair<AdventureDbId, AdventureModeDbId>(AdventureDbId.LOOT, AdventureModeDbId.DUNGEON_CRAWL),
			new PresenceMgr.PresenceTargets(PresenceAdventureMode.LOOT, Global.PresenceStatus.SPECTATING_GAME_ADVENTURE_LOOT)
		},
		{
			new KeyValuePair<AdventureDbId, AdventureModeDbId>(AdventureDbId.GIL, AdventureModeDbId.DUNGEON_CRAWL),
			new PresenceMgr.PresenceTargets(PresenceAdventureMode.GIL, Global.PresenceStatus.SPECTATING_GAME_ADVENTURE_GIL)
		},
		{
			new KeyValuePair<AdventureDbId, AdventureModeDbId>(AdventureDbId.GIL, AdventureModeDbId.BONUS_CHALLENGE),
			new PresenceMgr.PresenceTargets(PresenceAdventureMode.GIL, Global.PresenceStatus.SPECTATING_GAME_ADVENTURE_GIL_BONUS_CHALLENGE)
		},
		{
			new KeyValuePair<AdventureDbId, AdventureModeDbId>(AdventureDbId.BOT, AdventureModeDbId.LINEAR),
			new PresenceMgr.PresenceTargets(PresenceAdventureMode.BOT, Global.PresenceStatus.SPECTATING_GAME_ADVENTURE_BOT)
		},
		{
			new KeyValuePair<AdventureDbId, AdventureModeDbId>(AdventureDbId.TRL, AdventureModeDbId.DUNGEON_CRAWL),
			new PresenceMgr.PresenceTargets(PresenceAdventureMode.TRL, Global.PresenceStatus.SPECTATING_GAME_ADVENTURE_TRL)
		},
		{
			new KeyValuePair<AdventureDbId, AdventureModeDbId>(AdventureDbId.DALARAN, AdventureModeDbId.DUNGEON_CRAWL),
			new PresenceMgr.PresenceTargets(PresenceAdventureMode.DAL, Global.PresenceStatus.SPECTATING_GAME_ADVENTURE_DAL)
		},
		{
			new KeyValuePair<AdventureDbId, AdventureModeDbId>(AdventureDbId.DALARAN, AdventureModeDbId.DUNGEON_CRAWL_HEROIC),
			new PresenceMgr.PresenceTargets(PresenceAdventureMode.DAL, Global.PresenceStatus.SPECTATING_GAME_ADVENTURE_DAL_HEROIC)
		},
		{
			new KeyValuePair<AdventureDbId, AdventureModeDbId>(AdventureDbId.ULDUM, AdventureModeDbId.DUNGEON_CRAWL),
			new PresenceMgr.PresenceTargets(PresenceAdventureMode.ULD, Global.PresenceStatus.SPECTATING_GAME_ADVENTURE_ULD)
		},
		{
			new KeyValuePair<AdventureDbId, AdventureModeDbId>(AdventureDbId.ULDUM, AdventureModeDbId.DUNGEON_CRAWL_HEROIC),
			new PresenceMgr.PresenceTargets(PresenceAdventureMode.ULD, Global.PresenceStatus.SPECTATING_GAME_ADVENTURE_ULD_HEROIC)
		},
		{
			new KeyValuePair<AdventureDbId, AdventureModeDbId>(AdventureDbId.DRAGONS, AdventureModeDbId.LINEAR),
			new PresenceMgr.PresenceTargets(PresenceAdventureMode.DRG, Global.PresenceStatus.SPECTATING_GAME_ADVENTURE_DRG)
		},
		{
			new KeyValuePair<AdventureDbId, AdventureModeDbId>(AdventureDbId.DRAGONS, AdventureModeDbId.LINEAR_HEROIC),
			new PresenceMgr.PresenceTargets(PresenceAdventureMode.DRG, Global.PresenceStatus.SPECTATING_GAME_ADVENTURE_DRG_HEROIC)
		},
		{
			new KeyValuePair<AdventureDbId, AdventureModeDbId>(AdventureDbId.BTP, AdventureModeDbId.LINEAR),
			new PresenceMgr.PresenceTargets(PresenceAdventureMode.BTP, Global.PresenceStatus.SPECTATING_GAME_ADVENTURE_BTP)
		},
		{
			new KeyValuePair<AdventureDbId, AdventureModeDbId>(AdventureDbId.BTA, AdventureModeDbId.LINEAR),
			new PresenceMgr.PresenceTargets(PresenceAdventureMode.BTA, Global.PresenceStatus.SPECTATING_GAME_ADVENTURE_BTA)
		},
		{
			new KeyValuePair<AdventureDbId, AdventureModeDbId>(AdventureDbId.BTA_HEROIC, AdventureModeDbId.LINEAR_HEROIC),
			new PresenceMgr.PresenceTargets(PresenceAdventureMode.BTA_HEROIC, Global.PresenceStatus.SPECTATING_GAME_ADVENTURE_BTA_HEROIC)
		},
		{
			new KeyValuePair<AdventureDbId, AdventureModeDbId>(AdventureDbId.BOH, AdventureModeDbId.DUNGEON_CRAWL),
			new PresenceMgr.PresenceTargets(PresenceAdventureMode.BOH, Global.PresenceStatus.SPECTATING_GAME_ADVENTURE_BOH)
		},
		{
			new KeyValuePair<AdventureDbId, AdventureModeDbId>(AdventureDbId.BOM, AdventureModeDbId.DUNGEON_CRAWL),
			new PresenceMgr.PresenceTargets(PresenceAdventureMode.BOM, Global.PresenceStatus.SPECTATING_GAME_ADVENTURE_BOM)
		}
	};

	// Token: 0x0400069A RID: 1690
	private static readonly Type[] s_enumIdList = new Type[]
	{
		typeof(Global.PresenceStatus),
		typeof(PresenceTutorial),
		typeof(PresenceAdventureMode),
		typeof(ScenarioDbId)
	};

	// Token: 0x0400069B RID: 1691
	private static PresenceMgr s_instance;

	// Token: 0x0400069C RID: 1692
	private global::Map<Type, byte> m_enumToIdMap = new global::Map<Type, byte>();

	// Token: 0x0400069D RID: 1693
	private global::Map<byte, Type> m_idToEnumMap = new global::Map<byte, Type>();

	// Token: 0x0400069E RID: 1694
	private Enum[] m_prevStatus;

	// Token: 0x0400069F RID: 1695
	private Enum[] m_status;

	// Token: 0x040006A0 RID: 1696
	private long m_timeStartStatusMs;

	// Token: 0x040006A1 RID: 1697
	private PresenceStatus m_currentStatus;

	// Token: 0x040006A2 RID: 1698
	private Enum[] m_richPresence;

	// Token: 0x020013AD RID: 5037
	private struct PresenceTargets
	{
		// Token: 0x0600D82D RID: 55341 RVA: 0x003EDB05 File Offset: 0x003EBD05
		public PresenceTargets(PresenceAdventureMode enteringAdventureValue, Global.PresenceStatus spectatingValue)
		{
			this.EnteringAdventureValue = enteringAdventureValue;
			this.SpectatingValue = spectatingValue;
		}

		// Token: 0x0400A771 RID: 42865
		public PresenceAdventureMode EnteringAdventureValue;

		// Token: 0x0400A772 RID: 42866
		public Global.PresenceStatus SpectatingValue;
	}
}
