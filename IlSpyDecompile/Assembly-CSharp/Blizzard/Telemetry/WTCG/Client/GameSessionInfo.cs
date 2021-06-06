using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	public class GameSessionInfo : IProtoBuf
	{
		public bool HasGameServerIpAddress;

		private string _GameServerIpAddress;

		public bool HasGameServerPort;

		private uint _GameServerPort;

		public bool HasVersion;

		private string _Version;

		public bool HasGameHandle;

		private uint _GameHandle;

		public bool HasScenarioId;

		private int _ScenarioId;

		public bool HasBrawlLibraryItemId;

		private int _BrawlLibraryItemId;

		public bool HasSeasonId;

		private int _SeasonId;

		public bool HasGameType;

		private GameType _GameType;

		public bool HasFormatType;

		private FormatType _FormatType;

		public bool HasIsReconnect;

		private bool _IsReconnect;

		public bool HasIsSpectating;

		private bool _IsSpectating;

		public bool HasClientHandle;

		private long _ClientHandle;

		public bool HasClientDeckId;

		private long _ClientDeckId;

		public bool HasAiDeckId;

		private long _AiDeckId;

		public bool HasClientHeroCardId;

		private long _ClientHeroCardId;

		public string GameServerIpAddress
		{
			get
			{
				return _GameServerIpAddress;
			}
			set
			{
				_GameServerIpAddress = value;
				HasGameServerIpAddress = value != null;
			}
		}

		public uint GameServerPort
		{
			get
			{
				return _GameServerPort;
			}
			set
			{
				_GameServerPort = value;
				HasGameServerPort = true;
			}
		}

		public string Version
		{
			get
			{
				return _Version;
			}
			set
			{
				_Version = value;
				HasVersion = value != null;
			}
		}

		public uint GameHandle
		{
			get
			{
				return _GameHandle;
			}
			set
			{
				_GameHandle = value;
				HasGameHandle = true;
			}
		}

		public int ScenarioId
		{
			get
			{
				return _ScenarioId;
			}
			set
			{
				_ScenarioId = value;
				HasScenarioId = true;
			}
		}

		public int BrawlLibraryItemId
		{
			get
			{
				return _BrawlLibraryItemId;
			}
			set
			{
				_BrawlLibraryItemId = value;
				HasBrawlLibraryItemId = true;
			}
		}

		public int SeasonId
		{
			get
			{
				return _SeasonId;
			}
			set
			{
				_SeasonId = value;
				HasSeasonId = true;
			}
		}

		public GameType GameType
		{
			get
			{
				return _GameType;
			}
			set
			{
				_GameType = value;
				HasGameType = true;
			}
		}

		public FormatType FormatType
		{
			get
			{
				return _FormatType;
			}
			set
			{
				_FormatType = value;
				HasFormatType = true;
			}
		}

		public bool IsReconnect
		{
			get
			{
				return _IsReconnect;
			}
			set
			{
				_IsReconnect = value;
				HasIsReconnect = true;
			}
		}

		public bool IsSpectating
		{
			get
			{
				return _IsSpectating;
			}
			set
			{
				_IsSpectating = value;
				HasIsSpectating = true;
			}
		}

		public long ClientHandle
		{
			get
			{
				return _ClientHandle;
			}
			set
			{
				_ClientHandle = value;
				HasClientHandle = true;
			}
		}

		public long ClientDeckId
		{
			get
			{
				return _ClientDeckId;
			}
			set
			{
				_ClientDeckId = value;
				HasClientDeckId = true;
			}
		}

		public long AiDeckId
		{
			get
			{
				return _AiDeckId;
			}
			set
			{
				_AiDeckId = value;
				HasAiDeckId = true;
			}
		}

		public long ClientHeroCardId
		{
			get
			{
				return _ClientHeroCardId;
			}
			set
			{
				_ClientHeroCardId = value;
				HasClientHeroCardId = true;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasGameServerIpAddress)
			{
				num ^= GameServerIpAddress.GetHashCode();
			}
			if (HasGameServerPort)
			{
				num ^= GameServerPort.GetHashCode();
			}
			if (HasVersion)
			{
				num ^= Version.GetHashCode();
			}
			if (HasGameHandle)
			{
				num ^= GameHandle.GetHashCode();
			}
			if (HasScenarioId)
			{
				num ^= ScenarioId.GetHashCode();
			}
			if (HasBrawlLibraryItemId)
			{
				num ^= BrawlLibraryItemId.GetHashCode();
			}
			if (HasSeasonId)
			{
				num ^= SeasonId.GetHashCode();
			}
			if (HasGameType)
			{
				num ^= GameType.GetHashCode();
			}
			if (HasFormatType)
			{
				num ^= FormatType.GetHashCode();
			}
			if (HasIsReconnect)
			{
				num ^= IsReconnect.GetHashCode();
			}
			if (HasIsSpectating)
			{
				num ^= IsSpectating.GetHashCode();
			}
			if (HasClientHandle)
			{
				num ^= ClientHandle.GetHashCode();
			}
			if (HasClientDeckId)
			{
				num ^= ClientDeckId.GetHashCode();
			}
			if (HasAiDeckId)
			{
				num ^= AiDeckId.GetHashCode();
			}
			if (HasClientHeroCardId)
			{
				num ^= ClientHeroCardId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GameSessionInfo gameSessionInfo = obj as GameSessionInfo;
			if (gameSessionInfo == null)
			{
				return false;
			}
			if (HasGameServerIpAddress != gameSessionInfo.HasGameServerIpAddress || (HasGameServerIpAddress && !GameServerIpAddress.Equals(gameSessionInfo.GameServerIpAddress)))
			{
				return false;
			}
			if (HasGameServerPort != gameSessionInfo.HasGameServerPort || (HasGameServerPort && !GameServerPort.Equals(gameSessionInfo.GameServerPort)))
			{
				return false;
			}
			if (HasVersion != gameSessionInfo.HasVersion || (HasVersion && !Version.Equals(gameSessionInfo.Version)))
			{
				return false;
			}
			if (HasGameHandle != gameSessionInfo.HasGameHandle || (HasGameHandle && !GameHandle.Equals(gameSessionInfo.GameHandle)))
			{
				return false;
			}
			if (HasScenarioId != gameSessionInfo.HasScenarioId || (HasScenarioId && !ScenarioId.Equals(gameSessionInfo.ScenarioId)))
			{
				return false;
			}
			if (HasBrawlLibraryItemId != gameSessionInfo.HasBrawlLibraryItemId || (HasBrawlLibraryItemId && !BrawlLibraryItemId.Equals(gameSessionInfo.BrawlLibraryItemId)))
			{
				return false;
			}
			if (HasSeasonId != gameSessionInfo.HasSeasonId || (HasSeasonId && !SeasonId.Equals(gameSessionInfo.SeasonId)))
			{
				return false;
			}
			if (HasGameType != gameSessionInfo.HasGameType || (HasGameType && !GameType.Equals(gameSessionInfo.GameType)))
			{
				return false;
			}
			if (HasFormatType != gameSessionInfo.HasFormatType || (HasFormatType && !FormatType.Equals(gameSessionInfo.FormatType)))
			{
				return false;
			}
			if (HasIsReconnect != gameSessionInfo.HasIsReconnect || (HasIsReconnect && !IsReconnect.Equals(gameSessionInfo.IsReconnect)))
			{
				return false;
			}
			if (HasIsSpectating != gameSessionInfo.HasIsSpectating || (HasIsSpectating && !IsSpectating.Equals(gameSessionInfo.IsSpectating)))
			{
				return false;
			}
			if (HasClientHandle != gameSessionInfo.HasClientHandle || (HasClientHandle && !ClientHandle.Equals(gameSessionInfo.ClientHandle)))
			{
				return false;
			}
			if (HasClientDeckId != gameSessionInfo.HasClientDeckId || (HasClientDeckId && !ClientDeckId.Equals(gameSessionInfo.ClientDeckId)))
			{
				return false;
			}
			if (HasAiDeckId != gameSessionInfo.HasAiDeckId || (HasAiDeckId && !AiDeckId.Equals(gameSessionInfo.AiDeckId)))
			{
				return false;
			}
			if (HasClientHeroCardId != gameSessionInfo.HasClientHeroCardId || (HasClientHeroCardId && !ClientHeroCardId.Equals(gameSessionInfo.ClientHeroCardId)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GameSessionInfo Deserialize(Stream stream, GameSessionInfo instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GameSessionInfo DeserializeLengthDelimited(Stream stream)
		{
			GameSessionInfo gameSessionInfo = new GameSessionInfo();
			DeserializeLengthDelimited(stream, gameSessionInfo);
			return gameSessionInfo;
		}

		public static GameSessionInfo DeserializeLengthDelimited(Stream stream, GameSessionInfo instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GameSessionInfo Deserialize(Stream stream, GameSessionInfo instance, long limit)
		{
			instance.GameType = GameType.GT_UNKNOWN;
			instance.FormatType = FormatType.FT_UNKNOWN;
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
					instance.GameServerIpAddress = ProtocolParser.ReadString(stream);
					continue;
				case 16:
					instance.GameServerPort = ProtocolParser.ReadUInt32(stream);
					continue;
				case 26:
					instance.Version = ProtocolParser.ReadString(stream);
					continue;
				case 32:
					instance.GameHandle = ProtocolParser.ReadUInt32(stream);
					continue;
				case 40:
					instance.ScenarioId = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 48:
					instance.BrawlLibraryItemId = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 56:
					instance.SeasonId = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 64:
					instance.GameType = (GameType)ProtocolParser.ReadUInt64(stream);
					continue;
				case 72:
					instance.FormatType = (FormatType)ProtocolParser.ReadUInt64(stream);
					continue;
				case 80:
					instance.IsReconnect = ProtocolParser.ReadBool(stream);
					continue;
				case 88:
					instance.IsSpectating = ProtocolParser.ReadBool(stream);
					continue;
				case 96:
					instance.ClientHandle = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				case 104:
					instance.ClientDeckId = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				case 112:
					instance.AiDeckId = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				case 120:
					instance.ClientHeroCardId = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				default:
				{
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					if (key.Field == 0)
					{
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					}
					ProtocolParser.SkipKey(stream, key);
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

		public static void Serialize(Stream stream, GameSessionInfo instance)
		{
			if (instance.HasGameServerIpAddress)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.GameServerIpAddress));
			}
			if (instance.HasGameServerPort)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.GameServerPort);
			}
			if (instance.HasVersion)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Version));
			}
			if (instance.HasGameHandle)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt32(stream, instance.GameHandle);
			}
			if (instance.HasScenarioId)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.ScenarioId);
			}
			if (instance.HasBrawlLibraryItemId)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.BrawlLibraryItemId);
			}
			if (instance.HasSeasonId)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.SeasonId);
			}
			if (instance.HasGameType)
			{
				stream.WriteByte(64);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.GameType);
			}
			if (instance.HasFormatType)
			{
				stream.WriteByte(72);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.FormatType);
			}
			if (instance.HasIsReconnect)
			{
				stream.WriteByte(80);
				ProtocolParser.WriteBool(stream, instance.IsReconnect);
			}
			if (instance.HasIsSpectating)
			{
				stream.WriteByte(88);
				ProtocolParser.WriteBool(stream, instance.IsSpectating);
			}
			if (instance.HasClientHandle)
			{
				stream.WriteByte(96);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.ClientHandle);
			}
			if (instance.HasClientDeckId)
			{
				stream.WriteByte(104);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.ClientDeckId);
			}
			if (instance.HasAiDeckId)
			{
				stream.WriteByte(112);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.AiDeckId);
			}
			if (instance.HasClientHeroCardId)
			{
				stream.WriteByte(120);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.ClientHeroCardId);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasGameServerIpAddress)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(GameServerIpAddress);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasGameServerPort)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(GameServerPort);
			}
			if (HasVersion)
			{
				num++;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(Version);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (HasGameHandle)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(GameHandle);
			}
			if (HasScenarioId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)ScenarioId);
			}
			if (HasBrawlLibraryItemId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)BrawlLibraryItemId);
			}
			if (HasSeasonId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)SeasonId);
			}
			if (HasGameType)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)GameType);
			}
			if (HasFormatType)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)FormatType);
			}
			if (HasIsReconnect)
			{
				num++;
				num++;
			}
			if (HasIsSpectating)
			{
				num++;
				num++;
			}
			if (HasClientHandle)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)ClientHandle);
			}
			if (HasClientDeckId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)ClientDeckId);
			}
			if (HasAiDeckId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)AiDeckId);
			}
			if (HasClientHeroCardId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)ClientHeroCardId);
			}
			return num;
		}
	}
}
