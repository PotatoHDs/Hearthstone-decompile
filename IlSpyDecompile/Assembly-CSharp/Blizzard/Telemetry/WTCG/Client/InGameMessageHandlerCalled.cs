using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	public class InGameMessageHandlerCalled : IProtoBuf
	{
		public bool HasPlayer;

		private Player _Player;

		public bool HasDeviceInfo;

		private DeviceInfo _DeviceInfo;

		public bool HasMessageType;

		private string _MessageType;

		public bool HasTitle;

		private string _Title;

		public bool HasUid;

		private string _Uid;

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

		public string MessageType
		{
			get
			{
				return _MessageType;
			}
			set
			{
				_MessageType = value;
				HasMessageType = value != null;
			}
		}

		public string Title
		{
			get
			{
				return _Title;
			}
			set
			{
				_Title = value;
				HasTitle = value != null;
			}
		}

		public string Uid
		{
			get
			{
				return _Uid;
			}
			set
			{
				_Uid = value;
				HasUid = value != null;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasPlayer)
			{
				num ^= Player.GetHashCode();
			}
			if (HasDeviceInfo)
			{
				num ^= DeviceInfo.GetHashCode();
			}
			if (HasMessageType)
			{
				num ^= MessageType.GetHashCode();
			}
			if (HasTitle)
			{
				num ^= Title.GetHashCode();
			}
			if (HasUid)
			{
				num ^= Uid.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			InGameMessageHandlerCalled inGameMessageHandlerCalled = obj as InGameMessageHandlerCalled;
			if (inGameMessageHandlerCalled == null)
			{
				return false;
			}
			if (HasPlayer != inGameMessageHandlerCalled.HasPlayer || (HasPlayer && !Player.Equals(inGameMessageHandlerCalled.Player)))
			{
				return false;
			}
			if (HasDeviceInfo != inGameMessageHandlerCalled.HasDeviceInfo || (HasDeviceInfo && !DeviceInfo.Equals(inGameMessageHandlerCalled.DeviceInfo)))
			{
				return false;
			}
			if (HasMessageType != inGameMessageHandlerCalled.HasMessageType || (HasMessageType && !MessageType.Equals(inGameMessageHandlerCalled.MessageType)))
			{
				return false;
			}
			if (HasTitle != inGameMessageHandlerCalled.HasTitle || (HasTitle && !Title.Equals(inGameMessageHandlerCalled.Title)))
			{
				return false;
			}
			if (HasUid != inGameMessageHandlerCalled.HasUid || (HasUid && !Uid.Equals(inGameMessageHandlerCalled.Uid)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static InGameMessageHandlerCalled Deserialize(Stream stream, InGameMessageHandlerCalled instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static InGameMessageHandlerCalled DeserializeLengthDelimited(Stream stream)
		{
			InGameMessageHandlerCalled inGameMessageHandlerCalled = new InGameMessageHandlerCalled();
			DeserializeLengthDelimited(stream, inGameMessageHandlerCalled);
			return inGameMessageHandlerCalled;
		}

		public static InGameMessageHandlerCalled DeserializeLengthDelimited(Stream stream, InGameMessageHandlerCalled instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static InGameMessageHandlerCalled Deserialize(Stream stream, InGameMessageHandlerCalled instance, long limit)
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
					if (instance.Player == null)
					{
						instance.Player = Player.DeserializeLengthDelimited(stream);
					}
					else
					{
						Player.DeserializeLengthDelimited(stream, instance.Player);
					}
					continue;
				case 18:
					if (instance.DeviceInfo == null)
					{
						instance.DeviceInfo = DeviceInfo.DeserializeLengthDelimited(stream);
					}
					else
					{
						DeviceInfo.DeserializeLengthDelimited(stream, instance.DeviceInfo);
					}
					continue;
				case 26:
					instance.MessageType = ProtocolParser.ReadString(stream);
					continue;
				case 34:
					instance.Title = ProtocolParser.ReadString(stream);
					continue;
				case 82:
					instance.Uid = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, InGameMessageHandlerCalled instance)
		{
			if (instance.HasPlayer)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Player.GetSerializedSize());
				Player.Serialize(stream, instance.Player);
			}
			if (instance.HasDeviceInfo)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.DeviceInfo.GetSerializedSize());
				DeviceInfo.Serialize(stream, instance.DeviceInfo);
			}
			if (instance.HasMessageType)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.MessageType));
			}
			if (instance.HasTitle)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Title));
			}
			if (instance.HasUid)
			{
				stream.WriteByte(82);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Uid));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasPlayer)
			{
				num++;
				uint serializedSize = Player.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasDeviceInfo)
			{
				num++;
				uint serializedSize2 = DeviceInfo.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (HasMessageType)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(MessageType);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasTitle)
			{
				num++;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(Title);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (HasUid)
			{
				num++;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(Uid);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			return num;
		}
	}
}
