using System.IO;
using System.Text;
using PegasusShared;

namespace SpectatorProto
{
	public class PartyServerInfo : IProtoBuf
	{
		public bool HasServerIpAddress;

		private string _ServerIpAddress;

		public bool HasServerPort;

		private uint _ServerPort;

		public bool HasGameHandle;

		private int _GameHandle;

		public bool HasSecretKey;

		private string _SecretKey;

		public bool HasGameType;

		private GameType _GameType;

		public bool HasFormatType;

		private FormatType _FormatType;

		public bool HasMissionId;

		private int _MissionId;

		public string ServerIpAddress
		{
			get
			{
				return _ServerIpAddress;
			}
			set
			{
				_ServerIpAddress = value;
				HasServerIpAddress = value != null;
			}
		}

		public uint ServerPort
		{
			get
			{
				return _ServerPort;
			}
			set
			{
				_ServerPort = value;
				HasServerPort = true;
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

		public string SecretKey
		{
			get
			{
				return _SecretKey;
			}
			set
			{
				_SecretKey = value;
				HasSecretKey = value != null;
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

		public int MissionId
		{
			get
			{
				return _MissionId;
			}
			set
			{
				_MissionId = value;
				HasMissionId = true;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasServerIpAddress)
			{
				num ^= ServerIpAddress.GetHashCode();
			}
			if (HasServerPort)
			{
				num ^= ServerPort.GetHashCode();
			}
			if (HasGameHandle)
			{
				num ^= GameHandle.GetHashCode();
			}
			if (HasSecretKey)
			{
				num ^= SecretKey.GetHashCode();
			}
			if (HasGameType)
			{
				num ^= GameType.GetHashCode();
			}
			if (HasFormatType)
			{
				num ^= FormatType.GetHashCode();
			}
			if (HasMissionId)
			{
				num ^= MissionId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			PartyServerInfo partyServerInfo = obj as PartyServerInfo;
			if (partyServerInfo == null)
			{
				return false;
			}
			if (HasServerIpAddress != partyServerInfo.HasServerIpAddress || (HasServerIpAddress && !ServerIpAddress.Equals(partyServerInfo.ServerIpAddress)))
			{
				return false;
			}
			if (HasServerPort != partyServerInfo.HasServerPort || (HasServerPort && !ServerPort.Equals(partyServerInfo.ServerPort)))
			{
				return false;
			}
			if (HasGameHandle != partyServerInfo.HasGameHandle || (HasGameHandle && !GameHandle.Equals(partyServerInfo.GameHandle)))
			{
				return false;
			}
			if (HasSecretKey != partyServerInfo.HasSecretKey || (HasSecretKey && !SecretKey.Equals(partyServerInfo.SecretKey)))
			{
				return false;
			}
			if (HasGameType != partyServerInfo.HasGameType || (HasGameType && !GameType.Equals(partyServerInfo.GameType)))
			{
				return false;
			}
			if (HasFormatType != partyServerInfo.HasFormatType || (HasFormatType && !FormatType.Equals(partyServerInfo.FormatType)))
			{
				return false;
			}
			if (HasMissionId != partyServerInfo.HasMissionId || (HasMissionId && !MissionId.Equals(partyServerInfo.MissionId)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static PartyServerInfo Deserialize(Stream stream, PartyServerInfo instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static PartyServerInfo DeserializeLengthDelimited(Stream stream)
		{
			PartyServerInfo partyServerInfo = new PartyServerInfo();
			DeserializeLengthDelimited(stream, partyServerInfo);
			return partyServerInfo;
		}

		public static PartyServerInfo DeserializeLengthDelimited(Stream stream, PartyServerInfo instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static PartyServerInfo Deserialize(Stream stream, PartyServerInfo instance, long limit)
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
					instance.ServerIpAddress = ProtocolParser.ReadString(stream);
					continue;
				case 16:
					instance.ServerPort = ProtocolParser.ReadUInt32(stream);
					continue;
				case 24:
					instance.GameHandle = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 34:
					instance.SecretKey = ProtocolParser.ReadString(stream);
					continue;
				case 40:
					instance.GameType = (GameType)ProtocolParser.ReadUInt64(stream);
					continue;
				case 56:
					instance.FormatType = (FormatType)ProtocolParser.ReadUInt64(stream);
					continue;
				case 48:
					instance.MissionId = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, PartyServerInfo instance)
		{
			if (instance.HasServerIpAddress)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ServerIpAddress));
			}
			if (instance.HasServerPort)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.ServerPort);
			}
			if (instance.HasGameHandle)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.GameHandle);
			}
			if (instance.HasSecretKey)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.SecretKey));
			}
			if (instance.HasGameType)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.GameType);
			}
			if (instance.HasFormatType)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.FormatType);
			}
			if (instance.HasMissionId)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.MissionId);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasServerIpAddress)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(ServerIpAddress);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasServerPort)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(ServerPort);
			}
			if (HasGameHandle)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)GameHandle);
			}
			if (HasSecretKey)
			{
				num++;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(SecretKey);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
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
			if (HasMissionId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)MissionId);
			}
			return num;
		}
	}
}
