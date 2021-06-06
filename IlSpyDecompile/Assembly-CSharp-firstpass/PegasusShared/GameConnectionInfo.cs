using System.IO;
using System.Text;

namespace PegasusShared
{
	public class GameConnectionInfo : IProtoBuf
	{
		public bool HasAddress;

		private string _Address;

		public bool HasGameHandle;

		private int _GameHandle;

		public bool HasClientHandle;

		private long _ClientHandle;

		public bool HasPort;

		private int _Port;

		public bool HasVersion;

		private string _Version;

		public bool HasAuroraPassword;

		private string _AuroraPassword;

		public bool HasScenario;

		private int _Scenario;

		public bool HasGameType;

		private GameType _GameType;

		public bool HasFormatType;

		private FormatType _FormatType;

		public bool HasBrawlLibraryItemId;

		private int _BrawlLibraryItemId;

		public bool HasLoadGameState;

		private bool _LoadGameState;

		public string Address
		{
			get
			{
				return _Address;
			}
			set
			{
				_Address = value;
				HasAddress = value != null;
			}
		}

		public int GameHandle
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

		public int Port
		{
			get
			{
				return _Port;
			}
			set
			{
				_Port = value;
				HasPort = true;
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

		public string AuroraPassword
		{
			get
			{
				return _AuroraPassword;
			}
			set
			{
				_AuroraPassword = value;
				HasAuroraPassword = value != null;
			}
		}

		public int Scenario
		{
			get
			{
				return _Scenario;
			}
			set
			{
				_Scenario = value;
				HasScenario = true;
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

		public bool LoadGameState
		{
			get
			{
				return _LoadGameState;
			}
			set
			{
				_LoadGameState = value;
				HasLoadGameState = true;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasAddress)
			{
				num ^= Address.GetHashCode();
			}
			if (HasGameHandle)
			{
				num ^= GameHandle.GetHashCode();
			}
			if (HasClientHandle)
			{
				num ^= ClientHandle.GetHashCode();
			}
			if (HasPort)
			{
				num ^= Port.GetHashCode();
			}
			if (HasVersion)
			{
				num ^= Version.GetHashCode();
			}
			if (HasAuroraPassword)
			{
				num ^= AuroraPassword.GetHashCode();
			}
			if (HasScenario)
			{
				num ^= Scenario.GetHashCode();
			}
			if (HasGameType)
			{
				num ^= GameType.GetHashCode();
			}
			if (HasFormatType)
			{
				num ^= FormatType.GetHashCode();
			}
			if (HasBrawlLibraryItemId)
			{
				num ^= BrawlLibraryItemId.GetHashCode();
			}
			if (HasLoadGameState)
			{
				num ^= LoadGameState.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GameConnectionInfo gameConnectionInfo = obj as GameConnectionInfo;
			if (gameConnectionInfo == null)
			{
				return false;
			}
			if (HasAddress != gameConnectionInfo.HasAddress || (HasAddress && !Address.Equals(gameConnectionInfo.Address)))
			{
				return false;
			}
			if (HasGameHandle != gameConnectionInfo.HasGameHandle || (HasGameHandle && !GameHandle.Equals(gameConnectionInfo.GameHandle)))
			{
				return false;
			}
			if (HasClientHandle != gameConnectionInfo.HasClientHandle || (HasClientHandle && !ClientHandle.Equals(gameConnectionInfo.ClientHandle)))
			{
				return false;
			}
			if (HasPort != gameConnectionInfo.HasPort || (HasPort && !Port.Equals(gameConnectionInfo.Port)))
			{
				return false;
			}
			if (HasVersion != gameConnectionInfo.HasVersion || (HasVersion && !Version.Equals(gameConnectionInfo.Version)))
			{
				return false;
			}
			if (HasAuroraPassword != gameConnectionInfo.HasAuroraPassword || (HasAuroraPassword && !AuroraPassword.Equals(gameConnectionInfo.AuroraPassword)))
			{
				return false;
			}
			if (HasScenario != gameConnectionInfo.HasScenario || (HasScenario && !Scenario.Equals(gameConnectionInfo.Scenario)))
			{
				return false;
			}
			if (HasGameType != gameConnectionInfo.HasGameType || (HasGameType && !GameType.Equals(gameConnectionInfo.GameType)))
			{
				return false;
			}
			if (HasFormatType != gameConnectionInfo.HasFormatType || (HasFormatType && !FormatType.Equals(gameConnectionInfo.FormatType)))
			{
				return false;
			}
			if (HasBrawlLibraryItemId != gameConnectionInfo.HasBrawlLibraryItemId || (HasBrawlLibraryItemId && !BrawlLibraryItemId.Equals(gameConnectionInfo.BrawlLibraryItemId)))
			{
				return false;
			}
			if (HasLoadGameState != gameConnectionInfo.HasLoadGameState || (HasLoadGameState && !LoadGameState.Equals(gameConnectionInfo.LoadGameState)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GameConnectionInfo Deserialize(Stream stream, GameConnectionInfo instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GameConnectionInfo DeserializeLengthDelimited(Stream stream)
		{
			GameConnectionInfo gameConnectionInfo = new GameConnectionInfo();
			DeserializeLengthDelimited(stream, gameConnectionInfo);
			return gameConnectionInfo;
		}

		public static GameConnectionInfo DeserializeLengthDelimited(Stream stream, GameConnectionInfo instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GameConnectionInfo Deserialize(Stream stream, GameConnectionInfo instance, long limit)
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
					instance.Address = ProtocolParser.ReadString(stream);
					continue;
				case 16:
					instance.GameHandle = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.ClientHandle = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				case 32:
					instance.Port = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 42:
					instance.Version = ProtocolParser.ReadString(stream);
					continue;
				case 50:
					instance.AuroraPassword = ProtocolParser.ReadString(stream);
					continue;
				case 56:
					instance.Scenario = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 64:
					instance.GameType = (GameType)ProtocolParser.ReadUInt64(stream);
					continue;
				case 72:
					instance.FormatType = (FormatType)ProtocolParser.ReadUInt64(stream);
					continue;
				case 80:
					instance.BrawlLibraryItemId = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 88:
					instance.LoadGameState = ProtocolParser.ReadBool(stream);
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

		public static void Serialize(Stream stream, GameConnectionInfo instance)
		{
			if (instance.HasAddress)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Address));
			}
			if (instance.HasGameHandle)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.GameHandle);
			}
			if (instance.HasClientHandle)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.ClientHandle);
			}
			if (instance.HasPort)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.Port);
			}
			if (instance.HasVersion)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Version));
			}
			if (instance.HasAuroraPassword)
			{
				stream.WriteByte(50);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.AuroraPassword));
			}
			if (instance.HasScenario)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.Scenario);
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
			if (instance.HasBrawlLibraryItemId)
			{
				stream.WriteByte(80);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.BrawlLibraryItemId);
			}
			if (instance.HasLoadGameState)
			{
				stream.WriteByte(88);
				ProtocolParser.WriteBool(stream, instance.LoadGameState);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasAddress)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(Address);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasGameHandle)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)GameHandle);
			}
			if (HasClientHandle)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)ClientHandle);
			}
			if (HasPort)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)Port);
			}
			if (HasVersion)
			{
				num++;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(Version);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (HasAuroraPassword)
			{
				num++;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(AuroraPassword);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			if (HasScenario)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)Scenario);
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
			if (HasBrawlLibraryItemId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)BrawlLibraryItemId);
			}
			if (HasLoadGameState)
			{
				num++;
				num++;
			}
			return num;
		}
	}
}
