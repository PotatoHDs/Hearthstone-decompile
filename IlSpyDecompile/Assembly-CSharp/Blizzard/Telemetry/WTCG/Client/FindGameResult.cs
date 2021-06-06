using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	public class FindGameResult : IProtoBuf
	{
		public bool HasDeviceInfo;

		private DeviceInfo _DeviceInfo;

		public bool HasPlayer;

		private Player _Player;

		public bool HasResultCode;

		private uint _ResultCode;

		public bool HasResultCodeString;

		private string _ResultCodeString;

		public bool HasTimeSpentMilliseconds;

		private long _TimeSpentMilliseconds;

		public bool HasGameSessionInfo;

		private GameSessionInfo _GameSessionInfo;

		public DeviceInfo DeviceInfo
		{
			get
			{
				return _DeviceInfo;
			}
			set
			{
				_DeviceInfo = value;
				HasDeviceInfo = value != null;
			}
		}

		public Player Player
		{
			get
			{
				return _Player;
			}
			set
			{
				_Player = value;
				HasPlayer = value != null;
			}
		}

		public uint ResultCode
		{
			get
			{
				return _ResultCode;
			}
			set
			{
				_ResultCode = value;
				HasResultCode = true;
			}
		}

		public string ResultCodeString
		{
			get
			{
				return _ResultCodeString;
			}
			set
			{
				_ResultCodeString = value;
				HasResultCodeString = value != null;
			}
		}

		public long TimeSpentMilliseconds
		{
			get
			{
				return _TimeSpentMilliseconds;
			}
			set
			{
				_TimeSpentMilliseconds = value;
				HasTimeSpentMilliseconds = true;
			}
		}

		public GameSessionInfo GameSessionInfo
		{
			get
			{
				return _GameSessionInfo;
			}
			set
			{
				_GameSessionInfo = value;
				HasGameSessionInfo = value != null;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasDeviceInfo)
			{
				num ^= DeviceInfo.GetHashCode();
			}
			if (HasPlayer)
			{
				num ^= Player.GetHashCode();
			}
			if (HasResultCode)
			{
				num ^= ResultCode.GetHashCode();
			}
			if (HasResultCodeString)
			{
				num ^= ResultCodeString.GetHashCode();
			}
			if (HasTimeSpentMilliseconds)
			{
				num ^= TimeSpentMilliseconds.GetHashCode();
			}
			if (HasGameSessionInfo)
			{
				num ^= GameSessionInfo.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			FindGameResult findGameResult = obj as FindGameResult;
			if (findGameResult == null)
			{
				return false;
			}
			if (HasDeviceInfo != findGameResult.HasDeviceInfo || (HasDeviceInfo && !DeviceInfo.Equals(findGameResult.DeviceInfo)))
			{
				return false;
			}
			if (HasPlayer != findGameResult.HasPlayer || (HasPlayer && !Player.Equals(findGameResult.Player)))
			{
				return false;
			}
			if (HasResultCode != findGameResult.HasResultCode || (HasResultCode && !ResultCode.Equals(findGameResult.ResultCode)))
			{
				return false;
			}
			if (HasResultCodeString != findGameResult.HasResultCodeString || (HasResultCodeString && !ResultCodeString.Equals(findGameResult.ResultCodeString)))
			{
				return false;
			}
			if (HasTimeSpentMilliseconds != findGameResult.HasTimeSpentMilliseconds || (HasTimeSpentMilliseconds && !TimeSpentMilliseconds.Equals(findGameResult.TimeSpentMilliseconds)))
			{
				return false;
			}
			if (HasGameSessionInfo != findGameResult.HasGameSessionInfo || (HasGameSessionInfo && !GameSessionInfo.Equals(findGameResult.GameSessionInfo)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static FindGameResult Deserialize(Stream stream, FindGameResult instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static FindGameResult DeserializeLengthDelimited(Stream stream)
		{
			FindGameResult findGameResult = new FindGameResult();
			DeserializeLengthDelimited(stream, findGameResult);
			return findGameResult;
		}

		public static FindGameResult DeserializeLengthDelimited(Stream stream, FindGameResult instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static FindGameResult Deserialize(Stream stream, FindGameResult instance, long limit)
		{
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
					if (instance.DeviceInfo == null)
					{
						instance.DeviceInfo = DeviceInfo.DeserializeLengthDelimited(stream);
					}
					else
					{
						DeviceInfo.DeserializeLengthDelimited(stream, instance.DeviceInfo);
					}
					continue;
				case 18:
					if (instance.Player == null)
					{
						instance.Player = Player.DeserializeLengthDelimited(stream);
					}
					else
					{
						Player.DeserializeLengthDelimited(stream, instance.Player);
					}
					continue;
				case 24:
					instance.ResultCode = ProtocolParser.ReadUInt32(stream);
					continue;
				case 34:
					instance.ResultCodeString = ProtocolParser.ReadString(stream);
					continue;
				case 40:
					instance.TimeSpentMilliseconds = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				case 50:
					if (instance.GameSessionInfo == null)
					{
						instance.GameSessionInfo = GameSessionInfo.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameSessionInfo.DeserializeLengthDelimited(stream, instance.GameSessionInfo);
					}
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

		public static void Serialize(Stream stream, FindGameResult instance)
		{
			if (instance.HasDeviceInfo)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.DeviceInfo.GetSerializedSize());
				DeviceInfo.Serialize(stream, instance.DeviceInfo);
			}
			if (instance.HasPlayer)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.Player.GetSerializedSize());
				Player.Serialize(stream, instance.Player);
			}
			if (instance.HasResultCode)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt32(stream, instance.ResultCode);
			}
			if (instance.HasResultCodeString)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ResultCodeString));
			}
			if (instance.HasTimeSpentMilliseconds)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.TimeSpentMilliseconds);
			}
			if (instance.HasGameSessionInfo)
			{
				stream.WriteByte(50);
				ProtocolParser.WriteUInt32(stream, instance.GameSessionInfo.GetSerializedSize());
				GameSessionInfo.Serialize(stream, instance.GameSessionInfo);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasDeviceInfo)
			{
				num++;
				uint serializedSize = DeviceInfo.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasPlayer)
			{
				num++;
				uint serializedSize2 = Player.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (HasResultCode)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(ResultCode);
			}
			if (HasResultCodeString)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(ResultCodeString);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasTimeSpentMilliseconds)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)TimeSpentMilliseconds);
			}
			if (HasGameSessionInfo)
			{
				num++;
				uint serializedSize3 = GameSessionInfo.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			return num;
		}
	}
}
