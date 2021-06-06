using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	public class ConnectToGameServer : IProtoBuf
	{
		public bool HasDeviceInfo;

		private DeviceInfo _DeviceInfo;

		public bool HasPlayer;

		private Player _Player;

		public bool HasResultBnetCode;

		private uint _ResultBnetCode;

		public bool HasResultBnetCodeString;

		private string _ResultBnetCodeString;

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

		public uint ResultBnetCode
		{
			get
			{
				return _ResultBnetCode;
			}
			set
			{
				_ResultBnetCode = value;
				HasResultBnetCode = true;
			}
		}

		public string ResultBnetCodeString
		{
			get
			{
				return _ResultBnetCodeString;
			}
			set
			{
				_ResultBnetCodeString = value;
				HasResultBnetCodeString = value != null;
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
			if (HasResultBnetCode)
			{
				num ^= ResultBnetCode.GetHashCode();
			}
			if (HasResultBnetCodeString)
			{
				num ^= ResultBnetCodeString.GetHashCode();
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
			ConnectToGameServer connectToGameServer = obj as ConnectToGameServer;
			if (connectToGameServer == null)
			{
				return false;
			}
			if (HasDeviceInfo != connectToGameServer.HasDeviceInfo || (HasDeviceInfo && !DeviceInfo.Equals(connectToGameServer.DeviceInfo)))
			{
				return false;
			}
			if (HasPlayer != connectToGameServer.HasPlayer || (HasPlayer && !Player.Equals(connectToGameServer.Player)))
			{
				return false;
			}
			if (HasResultBnetCode != connectToGameServer.HasResultBnetCode || (HasResultBnetCode && !ResultBnetCode.Equals(connectToGameServer.ResultBnetCode)))
			{
				return false;
			}
			if (HasResultBnetCodeString != connectToGameServer.HasResultBnetCodeString || (HasResultBnetCodeString && !ResultBnetCodeString.Equals(connectToGameServer.ResultBnetCodeString)))
			{
				return false;
			}
			if (HasTimeSpentMilliseconds != connectToGameServer.HasTimeSpentMilliseconds || (HasTimeSpentMilliseconds && !TimeSpentMilliseconds.Equals(connectToGameServer.TimeSpentMilliseconds)))
			{
				return false;
			}
			if (HasGameSessionInfo != connectToGameServer.HasGameSessionInfo || (HasGameSessionInfo && !GameSessionInfo.Equals(connectToGameServer.GameSessionInfo)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ConnectToGameServer Deserialize(Stream stream, ConnectToGameServer instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ConnectToGameServer DeserializeLengthDelimited(Stream stream)
		{
			ConnectToGameServer connectToGameServer = new ConnectToGameServer();
			DeserializeLengthDelimited(stream, connectToGameServer);
			return connectToGameServer;
		}

		public static ConnectToGameServer DeserializeLengthDelimited(Stream stream, ConnectToGameServer instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ConnectToGameServer Deserialize(Stream stream, ConnectToGameServer instance, long limit)
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
					instance.ResultBnetCode = ProtocolParser.ReadUInt32(stream);
					continue;
				case 34:
					instance.ResultBnetCodeString = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, ConnectToGameServer instance)
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
			if (instance.HasResultBnetCode)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt32(stream, instance.ResultBnetCode);
			}
			if (instance.HasResultBnetCodeString)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ResultBnetCodeString));
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
			if (HasResultBnetCode)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(ResultBnetCode);
			}
			if (HasResultBnetCodeString)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(ResultBnetCodeString);
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
