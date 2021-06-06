using System.Collections.Generic;
using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	public class InitialClientState : IProtoBuf
	{
		public enum PacketID
		{
			ID = 207,
			System = 0
		}

		public bool HasCollection;

		private Collection _Collection;

		public bool HasNotices;

		private ProfileNotices _Notices;

		public bool HasAchievements;

		private Achieves _Achievements;

		public bool HasGameCurrencyStates;

		private GameCurrencyStates _GameCurrencyStates;

		public bool HasClientOptions;

		private ClientOptions _ClientOptions;

		public bool HasGuardianVars;

		private GuardianVars _GuardianVars;

		private List<SpecialEventTiming> _SpecialEventTiming = new List<SpecialEventTiming>();

		private List<TavernBrawlInfo> _TavernBrawlsList = new List<TavernBrawlInfo>();

		public bool HasBoosters;

		private Boosters _Boosters;

		public bool HasDisconnectedGame;

		private GameConnectionInfo _DisconnectedGame;

		public bool HasArenaSession;

		private ArenaSessionResponse _ArenaSession;

		public bool HasDisplayBanner;

		private int _DisplayBanner;

		private List<DeckInfo> _Decks = new List<DeckInfo>();

		public bool HasMedalInfo;

		private MedalInfo _MedalInfo;

		public bool HasDevTimeOffsetSeconds;

		private long _DevTimeOffsetSeconds;

		private List<GameSaveDataUpdate> _GameSaveData = new List<GameSaveDataUpdate>();

		private List<DeckContents> _DeckContents = new List<DeckContents>();

		private List<long> _ValidCachedDeckIds = new List<long>();

		public bool HasPlayerId;

		private long _PlayerId;

		public bool HasPlayerDraftTickets;

		private PlayerDraftTickets _PlayerDraftTickets;

		public Collection Collection
		{
			get
			{
				return _Collection;
			}
			set
			{
				_Collection = value;
				HasCollection = value != null;
			}
		}

		public ProfileNotices Notices
		{
			get
			{
				return _Notices;
			}
			set
			{
				_Notices = value;
				HasNotices = value != null;
			}
		}

		public Achieves Achievements
		{
			get
			{
				return _Achievements;
			}
			set
			{
				_Achievements = value;
				HasAchievements = value != null;
			}
		}

		public GameCurrencyStates GameCurrencyStates
		{
			get
			{
				return _GameCurrencyStates;
			}
			set
			{
				_GameCurrencyStates = value;
				HasGameCurrencyStates = value != null;
			}
		}

		public ClientOptions ClientOptions
		{
			get
			{
				return _ClientOptions;
			}
			set
			{
				_ClientOptions = value;
				HasClientOptions = value != null;
			}
		}

		public GuardianVars GuardianVars
		{
			get
			{
				return _GuardianVars;
			}
			set
			{
				_GuardianVars = value;
				HasGuardianVars = value != null;
			}
		}

		public List<SpecialEventTiming> SpecialEventTiming
		{
			get
			{
				return _SpecialEventTiming;
			}
			set
			{
				_SpecialEventTiming = value;
			}
		}

		public List<TavernBrawlInfo> TavernBrawlsList
		{
			get
			{
				return _TavernBrawlsList;
			}
			set
			{
				_TavernBrawlsList = value;
			}
		}

		public Boosters Boosters
		{
			get
			{
				return _Boosters;
			}
			set
			{
				_Boosters = value;
				HasBoosters = value != null;
			}
		}

		public GameConnectionInfo DisconnectedGame
		{
			get
			{
				return _DisconnectedGame;
			}
			set
			{
				_DisconnectedGame = value;
				HasDisconnectedGame = value != null;
			}
		}

		public ArenaSessionResponse ArenaSession
		{
			get
			{
				return _ArenaSession;
			}
			set
			{
				_ArenaSession = value;
				HasArenaSession = value != null;
			}
		}

		public int DisplayBanner
		{
			get
			{
				return _DisplayBanner;
			}
			set
			{
				_DisplayBanner = value;
				HasDisplayBanner = true;
			}
		}

		public List<DeckInfo> Decks
		{
			get
			{
				return _Decks;
			}
			set
			{
				_Decks = value;
			}
		}

		public MedalInfo MedalInfo
		{
			get
			{
				return _MedalInfo;
			}
			set
			{
				_MedalInfo = value;
				HasMedalInfo = value != null;
			}
		}

		public long DevTimeOffsetSeconds
		{
			get
			{
				return _DevTimeOffsetSeconds;
			}
			set
			{
				_DevTimeOffsetSeconds = value;
				HasDevTimeOffsetSeconds = true;
			}
		}

		public List<GameSaveDataUpdate> GameSaveData
		{
			get
			{
				return _GameSaveData;
			}
			set
			{
				_GameSaveData = value;
			}
		}

		public List<DeckContents> DeckContents
		{
			get
			{
				return _DeckContents;
			}
			set
			{
				_DeckContents = value;
			}
		}

		public List<long> ValidCachedDeckIds
		{
			get
			{
				return _ValidCachedDeckIds;
			}
			set
			{
				_ValidCachedDeckIds = value;
			}
		}

		public long PlayerId
		{
			get
			{
				return _PlayerId;
			}
			set
			{
				_PlayerId = value;
				HasPlayerId = true;
			}
		}

		public PlayerDraftTickets PlayerDraftTickets
		{
			get
			{
				return _PlayerDraftTickets;
			}
			set
			{
				_PlayerDraftTickets = value;
				HasPlayerDraftTickets = value != null;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasCollection)
			{
				num ^= Collection.GetHashCode();
			}
			if (HasNotices)
			{
				num ^= Notices.GetHashCode();
			}
			if (HasAchievements)
			{
				num ^= Achievements.GetHashCode();
			}
			if (HasGameCurrencyStates)
			{
				num ^= GameCurrencyStates.GetHashCode();
			}
			if (HasClientOptions)
			{
				num ^= ClientOptions.GetHashCode();
			}
			if (HasGuardianVars)
			{
				num ^= GuardianVars.GetHashCode();
			}
			foreach (SpecialEventTiming item in SpecialEventTiming)
			{
				num ^= item.GetHashCode();
			}
			foreach (TavernBrawlInfo tavernBrawls in TavernBrawlsList)
			{
				num ^= tavernBrawls.GetHashCode();
			}
			if (HasBoosters)
			{
				num ^= Boosters.GetHashCode();
			}
			if (HasDisconnectedGame)
			{
				num ^= DisconnectedGame.GetHashCode();
			}
			if (HasArenaSession)
			{
				num ^= ArenaSession.GetHashCode();
			}
			if (HasDisplayBanner)
			{
				num ^= DisplayBanner.GetHashCode();
			}
			foreach (DeckInfo deck in Decks)
			{
				num ^= deck.GetHashCode();
			}
			if (HasMedalInfo)
			{
				num ^= MedalInfo.GetHashCode();
			}
			if (HasDevTimeOffsetSeconds)
			{
				num ^= DevTimeOffsetSeconds.GetHashCode();
			}
			foreach (GameSaveDataUpdate gameSaveDatum in GameSaveData)
			{
				num ^= gameSaveDatum.GetHashCode();
			}
			foreach (DeckContents deckContent in DeckContents)
			{
				num ^= deckContent.GetHashCode();
			}
			foreach (long validCachedDeckId in ValidCachedDeckIds)
			{
				num ^= validCachedDeckId.GetHashCode();
			}
			if (HasPlayerId)
			{
				num ^= PlayerId.GetHashCode();
			}
			if (HasPlayerDraftTickets)
			{
				num ^= PlayerDraftTickets.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			InitialClientState initialClientState = obj as InitialClientState;
			if (initialClientState == null)
			{
				return false;
			}
			if (HasCollection != initialClientState.HasCollection || (HasCollection && !Collection.Equals(initialClientState.Collection)))
			{
				return false;
			}
			if (HasNotices != initialClientState.HasNotices || (HasNotices && !Notices.Equals(initialClientState.Notices)))
			{
				return false;
			}
			if (HasAchievements != initialClientState.HasAchievements || (HasAchievements && !Achievements.Equals(initialClientState.Achievements)))
			{
				return false;
			}
			if (HasGameCurrencyStates != initialClientState.HasGameCurrencyStates || (HasGameCurrencyStates && !GameCurrencyStates.Equals(initialClientState.GameCurrencyStates)))
			{
				return false;
			}
			if (HasClientOptions != initialClientState.HasClientOptions || (HasClientOptions && !ClientOptions.Equals(initialClientState.ClientOptions)))
			{
				return false;
			}
			if (HasGuardianVars != initialClientState.HasGuardianVars || (HasGuardianVars && !GuardianVars.Equals(initialClientState.GuardianVars)))
			{
				return false;
			}
			if (SpecialEventTiming.Count != initialClientState.SpecialEventTiming.Count)
			{
				return false;
			}
			for (int i = 0; i < SpecialEventTiming.Count; i++)
			{
				if (!SpecialEventTiming[i].Equals(initialClientState.SpecialEventTiming[i]))
				{
					return false;
				}
			}
			if (TavernBrawlsList.Count != initialClientState.TavernBrawlsList.Count)
			{
				return false;
			}
			for (int j = 0; j < TavernBrawlsList.Count; j++)
			{
				if (!TavernBrawlsList[j].Equals(initialClientState.TavernBrawlsList[j]))
				{
					return false;
				}
			}
			if (HasBoosters != initialClientState.HasBoosters || (HasBoosters && !Boosters.Equals(initialClientState.Boosters)))
			{
				return false;
			}
			if (HasDisconnectedGame != initialClientState.HasDisconnectedGame || (HasDisconnectedGame && !DisconnectedGame.Equals(initialClientState.DisconnectedGame)))
			{
				return false;
			}
			if (HasArenaSession != initialClientState.HasArenaSession || (HasArenaSession && !ArenaSession.Equals(initialClientState.ArenaSession)))
			{
				return false;
			}
			if (HasDisplayBanner != initialClientState.HasDisplayBanner || (HasDisplayBanner && !DisplayBanner.Equals(initialClientState.DisplayBanner)))
			{
				return false;
			}
			if (Decks.Count != initialClientState.Decks.Count)
			{
				return false;
			}
			for (int k = 0; k < Decks.Count; k++)
			{
				if (!Decks[k].Equals(initialClientState.Decks[k]))
				{
					return false;
				}
			}
			if (HasMedalInfo != initialClientState.HasMedalInfo || (HasMedalInfo && !MedalInfo.Equals(initialClientState.MedalInfo)))
			{
				return false;
			}
			if (HasDevTimeOffsetSeconds != initialClientState.HasDevTimeOffsetSeconds || (HasDevTimeOffsetSeconds && !DevTimeOffsetSeconds.Equals(initialClientState.DevTimeOffsetSeconds)))
			{
				return false;
			}
			if (GameSaveData.Count != initialClientState.GameSaveData.Count)
			{
				return false;
			}
			for (int l = 0; l < GameSaveData.Count; l++)
			{
				if (!GameSaveData[l].Equals(initialClientState.GameSaveData[l]))
				{
					return false;
				}
			}
			if (DeckContents.Count != initialClientState.DeckContents.Count)
			{
				return false;
			}
			for (int m = 0; m < DeckContents.Count; m++)
			{
				if (!DeckContents[m].Equals(initialClientState.DeckContents[m]))
				{
					return false;
				}
			}
			if (ValidCachedDeckIds.Count != initialClientState.ValidCachedDeckIds.Count)
			{
				return false;
			}
			for (int n = 0; n < ValidCachedDeckIds.Count; n++)
			{
				if (!ValidCachedDeckIds[n].Equals(initialClientState.ValidCachedDeckIds[n]))
				{
					return false;
				}
			}
			if (HasPlayerId != initialClientState.HasPlayerId || (HasPlayerId && !PlayerId.Equals(initialClientState.PlayerId)))
			{
				return false;
			}
			if (HasPlayerDraftTickets != initialClientState.HasPlayerDraftTickets || (HasPlayerDraftTickets && !PlayerDraftTickets.Equals(initialClientState.PlayerDraftTickets)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static InitialClientState Deserialize(Stream stream, InitialClientState instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static InitialClientState DeserializeLengthDelimited(Stream stream)
		{
			InitialClientState initialClientState = new InitialClientState();
			DeserializeLengthDelimited(stream, initialClientState);
			return initialClientState;
		}

		public static InitialClientState DeserializeLengthDelimited(Stream stream, InitialClientState instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static InitialClientState Deserialize(Stream stream, InitialClientState instance, long limit)
		{
			if (instance.SpecialEventTiming == null)
			{
				instance.SpecialEventTiming = new List<SpecialEventTiming>();
			}
			if (instance.TavernBrawlsList == null)
			{
				instance.TavernBrawlsList = new List<TavernBrawlInfo>();
			}
			instance.DisplayBanner = 0;
			if (instance.Decks == null)
			{
				instance.Decks = new List<DeckInfo>();
			}
			if (instance.GameSaveData == null)
			{
				instance.GameSaveData = new List<GameSaveDataUpdate>();
			}
			if (instance.DeckContents == null)
			{
				instance.DeckContents = new List<DeckContents>();
			}
			if (instance.ValidCachedDeckIds == null)
			{
				instance.ValidCachedDeckIds = new List<long>();
			}
			while (true)
			{
				if (limit >= 0 && stream.Position >= limit)
				{
					if (stream.Position == limit)
					{
						break;
					}
					throw new ProtocolBufferException("Read past max limit");
				}
				int num = stream.ReadByte();
				switch (num)
				{
				case -1:
					break;
				case 10:
					if (instance.Collection == null)
					{
						instance.Collection = Collection.DeserializeLengthDelimited(stream);
					}
					else
					{
						Collection.DeserializeLengthDelimited(stream, instance.Collection);
					}
					continue;
				case 18:
					if (instance.Notices == null)
					{
						instance.Notices = ProfileNotices.DeserializeLengthDelimited(stream);
					}
					else
					{
						ProfileNotices.DeserializeLengthDelimited(stream, instance.Notices);
					}
					continue;
				case 26:
					if (instance.Achievements == null)
					{
						instance.Achievements = Achieves.DeserializeLengthDelimited(stream);
					}
					else
					{
						Achieves.DeserializeLengthDelimited(stream, instance.Achievements);
					}
					continue;
				case 34:
					if (instance.GameCurrencyStates == null)
					{
						instance.GameCurrencyStates = GameCurrencyStates.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameCurrencyStates.DeserializeLengthDelimited(stream, instance.GameCurrencyStates);
					}
					continue;
				case 42:
					if (instance.ClientOptions == null)
					{
						instance.ClientOptions = ClientOptions.DeserializeLengthDelimited(stream);
					}
					else
					{
						ClientOptions.DeserializeLengthDelimited(stream, instance.ClientOptions);
					}
					continue;
				case 50:
					if (instance.GuardianVars == null)
					{
						instance.GuardianVars = GuardianVars.DeserializeLengthDelimited(stream);
					}
					else
					{
						GuardianVars.DeserializeLengthDelimited(stream, instance.GuardianVars);
					}
					continue;
				case 58:
					instance.SpecialEventTiming.Add(PegasusUtil.SpecialEventTiming.DeserializeLengthDelimited(stream));
					continue;
				case 66:
					instance.TavernBrawlsList.Add(TavernBrawlInfo.DeserializeLengthDelimited(stream));
					continue;
				case 74:
					if (instance.Boosters == null)
					{
						instance.Boosters = Boosters.DeserializeLengthDelimited(stream);
					}
					else
					{
						Boosters.DeserializeLengthDelimited(stream, instance.Boosters);
					}
					continue;
				case 82:
					if (instance.DisconnectedGame == null)
					{
						instance.DisconnectedGame = GameConnectionInfo.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameConnectionInfo.DeserializeLengthDelimited(stream, instance.DisconnectedGame);
					}
					continue;
				case 90:
					if (instance.ArenaSession == null)
					{
						instance.ArenaSession = ArenaSessionResponse.DeserializeLengthDelimited(stream);
					}
					else
					{
						ArenaSessionResponse.DeserializeLengthDelimited(stream, instance.ArenaSession);
					}
					continue;
				case 96:
					instance.DisplayBanner = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 106:
					instance.Decks.Add(DeckInfo.DeserializeLengthDelimited(stream));
					continue;
				case 114:
					if (instance.MedalInfo == null)
					{
						instance.MedalInfo = MedalInfo.DeserializeLengthDelimited(stream);
					}
					else
					{
						MedalInfo.DeserializeLengthDelimited(stream, instance.MedalInfo);
					}
					continue;
				case 120:
					instance.DevTimeOffsetSeconds = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				default:
				{
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					switch (key.Field)
					{
					case 0u:
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					case 16u:
						if (key.WireType == Wire.LengthDelimited)
						{
							instance.GameSaveData.Add(GameSaveDataUpdate.DeserializeLengthDelimited(stream));
						}
						break;
					case 17u:
						if (key.WireType == Wire.LengthDelimited)
						{
							instance.DeckContents.Add(PegasusUtil.DeckContents.DeserializeLengthDelimited(stream));
						}
						break;
					case 18u:
						if (key.WireType == Wire.Varint)
						{
							instance.ValidCachedDeckIds.Add((long)ProtocolParser.ReadUInt64(stream));
						}
						break;
					case 19u:
						if (key.WireType == Wire.Varint)
						{
							instance.PlayerId = (long)ProtocolParser.ReadUInt64(stream);
						}
						break;
					case 20u:
						if (key.WireType == Wire.LengthDelimited)
						{
							if (instance.PlayerDraftTickets == null)
							{
								instance.PlayerDraftTickets = PlayerDraftTickets.DeserializeLengthDelimited(stream);
							}
							else
							{
								PlayerDraftTickets.DeserializeLengthDelimited(stream, instance.PlayerDraftTickets);
							}
						}
						break;
					default:
						ProtocolParser.SkipKey(stream, key);
						break;
					}
					continue;
				}
				}
				if (limit < 0)
				{
					break;
				}
				throw new EndOfStreamException();
			}
			return instance;
		}

		public void Serialize(Stream stream)
		{
			Serialize(stream, this);
		}

		public static void Serialize(Stream stream, InitialClientState instance)
		{
			if (instance.HasCollection)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Collection.GetSerializedSize());
				Collection.Serialize(stream, instance.Collection);
			}
			if (instance.HasNotices)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.Notices.GetSerializedSize());
				ProfileNotices.Serialize(stream, instance.Notices);
			}
			if (instance.HasAchievements)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.Achievements.GetSerializedSize());
				Achieves.Serialize(stream, instance.Achievements);
			}
			if (instance.HasGameCurrencyStates)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.GameCurrencyStates.GetSerializedSize());
				GameCurrencyStates.Serialize(stream, instance.GameCurrencyStates);
			}
			if (instance.HasClientOptions)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteUInt32(stream, instance.ClientOptions.GetSerializedSize());
				ClientOptions.Serialize(stream, instance.ClientOptions);
			}
			if (instance.HasGuardianVars)
			{
				stream.WriteByte(50);
				ProtocolParser.WriteUInt32(stream, instance.GuardianVars.GetSerializedSize());
				GuardianVars.Serialize(stream, instance.GuardianVars);
			}
			if (instance.SpecialEventTiming.Count > 0)
			{
				foreach (SpecialEventTiming item in instance.SpecialEventTiming)
				{
					stream.WriteByte(58);
					ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
					PegasusUtil.SpecialEventTiming.Serialize(stream, item);
				}
			}
			if (instance.TavernBrawlsList.Count > 0)
			{
				foreach (TavernBrawlInfo tavernBrawls in instance.TavernBrawlsList)
				{
					stream.WriteByte(66);
					ProtocolParser.WriteUInt32(stream, tavernBrawls.GetSerializedSize());
					TavernBrawlInfo.Serialize(stream, tavernBrawls);
				}
			}
			if (instance.HasBoosters)
			{
				stream.WriteByte(74);
				ProtocolParser.WriteUInt32(stream, instance.Boosters.GetSerializedSize());
				Boosters.Serialize(stream, instance.Boosters);
			}
			if (instance.HasDisconnectedGame)
			{
				stream.WriteByte(82);
				ProtocolParser.WriteUInt32(stream, instance.DisconnectedGame.GetSerializedSize());
				GameConnectionInfo.Serialize(stream, instance.DisconnectedGame);
			}
			if (instance.HasArenaSession)
			{
				stream.WriteByte(90);
				ProtocolParser.WriteUInt32(stream, instance.ArenaSession.GetSerializedSize());
				ArenaSessionResponse.Serialize(stream, instance.ArenaSession);
			}
			if (instance.HasDisplayBanner)
			{
				stream.WriteByte(96);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.DisplayBanner);
			}
			if (instance.Decks.Count > 0)
			{
				foreach (DeckInfo deck in instance.Decks)
				{
					stream.WriteByte(106);
					ProtocolParser.WriteUInt32(stream, deck.GetSerializedSize());
					DeckInfo.Serialize(stream, deck);
				}
			}
			if (instance.HasMedalInfo)
			{
				stream.WriteByte(114);
				ProtocolParser.WriteUInt32(stream, instance.MedalInfo.GetSerializedSize());
				MedalInfo.Serialize(stream, instance.MedalInfo);
			}
			if (instance.HasDevTimeOffsetSeconds)
			{
				stream.WriteByte(120);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.DevTimeOffsetSeconds);
			}
			if (instance.GameSaveData.Count > 0)
			{
				foreach (GameSaveDataUpdate gameSaveDatum in instance.GameSaveData)
				{
					stream.WriteByte(130);
					stream.WriteByte(1);
					ProtocolParser.WriteUInt32(stream, gameSaveDatum.GetSerializedSize());
					GameSaveDataUpdate.Serialize(stream, gameSaveDatum);
				}
			}
			if (instance.DeckContents.Count > 0)
			{
				foreach (DeckContents deckContent in instance.DeckContents)
				{
					stream.WriteByte(138);
					stream.WriteByte(1);
					ProtocolParser.WriteUInt32(stream, deckContent.GetSerializedSize());
					PegasusUtil.DeckContents.Serialize(stream, deckContent);
				}
			}
			if (instance.ValidCachedDeckIds.Count > 0)
			{
				foreach (long validCachedDeckId in instance.ValidCachedDeckIds)
				{
					stream.WriteByte(144);
					stream.WriteByte(1);
					ProtocolParser.WriteUInt64(stream, (ulong)validCachedDeckId);
				}
			}
			if (instance.HasPlayerId)
			{
				stream.WriteByte(152);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.PlayerId);
			}
			if (instance.HasPlayerDraftTickets)
			{
				stream.WriteByte(162);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt32(stream, instance.PlayerDraftTickets.GetSerializedSize());
				PlayerDraftTickets.Serialize(stream, instance.PlayerDraftTickets);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasCollection)
			{
				num++;
				uint serializedSize = Collection.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasNotices)
			{
				num++;
				uint serializedSize2 = Notices.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (HasAchievements)
			{
				num++;
				uint serializedSize3 = Achievements.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			if (HasGameCurrencyStates)
			{
				num++;
				uint serializedSize4 = GameCurrencyStates.GetSerializedSize();
				num += serializedSize4 + ProtocolParser.SizeOfUInt32(serializedSize4);
			}
			if (HasClientOptions)
			{
				num++;
				uint serializedSize5 = ClientOptions.GetSerializedSize();
				num += serializedSize5 + ProtocolParser.SizeOfUInt32(serializedSize5);
			}
			if (HasGuardianVars)
			{
				num++;
				uint serializedSize6 = GuardianVars.GetSerializedSize();
				num += serializedSize6 + ProtocolParser.SizeOfUInt32(serializedSize6);
			}
			if (SpecialEventTiming.Count > 0)
			{
				foreach (SpecialEventTiming item in SpecialEventTiming)
				{
					num++;
					uint serializedSize7 = item.GetSerializedSize();
					num += serializedSize7 + ProtocolParser.SizeOfUInt32(serializedSize7);
				}
			}
			if (TavernBrawlsList.Count > 0)
			{
				foreach (TavernBrawlInfo tavernBrawls in TavernBrawlsList)
				{
					num++;
					uint serializedSize8 = tavernBrawls.GetSerializedSize();
					num += serializedSize8 + ProtocolParser.SizeOfUInt32(serializedSize8);
				}
			}
			if (HasBoosters)
			{
				num++;
				uint serializedSize9 = Boosters.GetSerializedSize();
				num += serializedSize9 + ProtocolParser.SizeOfUInt32(serializedSize9);
			}
			if (HasDisconnectedGame)
			{
				num++;
				uint serializedSize10 = DisconnectedGame.GetSerializedSize();
				num += serializedSize10 + ProtocolParser.SizeOfUInt32(serializedSize10);
			}
			if (HasArenaSession)
			{
				num++;
				uint serializedSize11 = ArenaSession.GetSerializedSize();
				num += serializedSize11 + ProtocolParser.SizeOfUInt32(serializedSize11);
			}
			if (HasDisplayBanner)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)DisplayBanner);
			}
			if (Decks.Count > 0)
			{
				foreach (DeckInfo deck in Decks)
				{
					num++;
					uint serializedSize12 = deck.GetSerializedSize();
					num += serializedSize12 + ProtocolParser.SizeOfUInt32(serializedSize12);
				}
			}
			if (HasMedalInfo)
			{
				num++;
				uint serializedSize13 = MedalInfo.GetSerializedSize();
				num += serializedSize13 + ProtocolParser.SizeOfUInt32(serializedSize13);
			}
			if (HasDevTimeOffsetSeconds)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)DevTimeOffsetSeconds);
			}
			if (GameSaveData.Count > 0)
			{
				foreach (GameSaveDataUpdate gameSaveDatum in GameSaveData)
				{
					num += 2;
					uint serializedSize14 = gameSaveDatum.GetSerializedSize();
					num += serializedSize14 + ProtocolParser.SizeOfUInt32(serializedSize14);
				}
			}
			if (DeckContents.Count > 0)
			{
				foreach (DeckContents deckContent in DeckContents)
				{
					num += 2;
					uint serializedSize15 = deckContent.GetSerializedSize();
					num += serializedSize15 + ProtocolParser.SizeOfUInt32(serializedSize15);
				}
			}
			if (ValidCachedDeckIds.Count > 0)
			{
				foreach (long validCachedDeckId in ValidCachedDeckIds)
				{
					num += 2;
					num += ProtocolParser.SizeOfUInt64((ulong)validCachedDeckId);
				}
			}
			if (HasPlayerId)
			{
				num += 2;
				num += ProtocolParser.SizeOfUInt64((ulong)PlayerId);
			}
			if (HasPlayerDraftTickets)
			{
				num += 2;
				uint serializedSize16 = PlayerDraftTickets.GetSerializedSize();
				num += serializedSize16 + ProtocolParser.SizeOfUInt32(serializedSize16);
			}
			return num;
		}
	}
}
