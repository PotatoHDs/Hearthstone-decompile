using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	public class InGameMessageAction : IProtoBuf
	{
		public enum ActionType
		{
			CLOSE = 1,
			MORE_LINK_CLICK,
			SCROLL_TO_NEXT,
			OPENED_SHOP
		}

		public bool HasPlayer;

		private Player _Player;

		public bool HasDeviceInfo;

		private DeviceInfo _DeviceInfo;

		public bool HasMessageType;

		private string _MessageType;

		public bool HasTitle;

		private string _Title;

		public bool HasAction;

		private ActionType _Action;

		public bool HasViewCounts;

		private int _ViewCounts;

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

		public ActionType Action
		{
			get
			{
				return _Action;
			}
			set
			{
				_Action = value;
				HasAction = true;
			}
		}

		public int ViewCounts
		{
			get
			{
				return _ViewCounts;
			}
			set
			{
				_ViewCounts = value;
				HasViewCounts = true;
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
			if (HasAction)
			{
				num ^= Action.GetHashCode();
			}
			if (HasViewCounts)
			{
				num ^= ViewCounts.GetHashCode();
			}
			if (HasUid)
			{
				num ^= Uid.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			InGameMessageAction inGameMessageAction = obj as InGameMessageAction;
			if (inGameMessageAction == null)
			{
				return false;
			}
			if (HasPlayer != inGameMessageAction.HasPlayer || (HasPlayer && !Player.Equals(inGameMessageAction.Player)))
			{
				return false;
			}
			if (HasDeviceInfo != inGameMessageAction.HasDeviceInfo || (HasDeviceInfo && !DeviceInfo.Equals(inGameMessageAction.DeviceInfo)))
			{
				return false;
			}
			if (HasMessageType != inGameMessageAction.HasMessageType || (HasMessageType && !MessageType.Equals(inGameMessageAction.MessageType)))
			{
				return false;
			}
			if (HasTitle != inGameMessageAction.HasTitle || (HasTitle && !Title.Equals(inGameMessageAction.Title)))
			{
				return false;
			}
			if (HasAction != inGameMessageAction.HasAction || (HasAction && !Action.Equals(inGameMessageAction.Action)))
			{
				return false;
			}
			if (HasViewCounts != inGameMessageAction.HasViewCounts || (HasViewCounts && !ViewCounts.Equals(inGameMessageAction.ViewCounts)))
			{
				return false;
			}
			if (HasUid != inGameMessageAction.HasUid || (HasUid && !Uid.Equals(inGameMessageAction.Uid)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static InGameMessageAction Deserialize(Stream stream, InGameMessageAction instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static InGameMessageAction DeserializeLengthDelimited(Stream stream)
		{
			InGameMessageAction inGameMessageAction = new InGameMessageAction();
			DeserializeLengthDelimited(stream, inGameMessageAction);
			return inGameMessageAction;
		}

		public static InGameMessageAction DeserializeLengthDelimited(Stream stream, InGameMessageAction instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static InGameMessageAction Deserialize(Stream stream, InGameMessageAction instance, long limit)
		{
			instance.Action = ActionType.CLOSE;
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
				case 40:
					instance.Action = (ActionType)ProtocolParser.ReadUInt64(stream);
					continue;
				case 48:
					instance.ViewCounts = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, InGameMessageAction instance)
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
			if (instance.HasAction)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.Action);
			}
			if (instance.HasViewCounts)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.ViewCounts);
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
			if (HasAction)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)Action);
			}
			if (HasViewCounts)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)ViewCounts);
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
