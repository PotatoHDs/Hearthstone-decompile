using System.IO;

namespace Blizzard.Telemetry.WTCG.Client
{
	public class PresenceChanged : IProtoBuf
	{
		public bool HasPlayer;

		private Player _Player;

		public bool HasDeviceInfo;

		private DeviceInfo _DeviceInfo;

		public bool HasNewPresenceStatus;

		private PresenceStatus _NewPresenceStatus;

		public bool HasPrevPresenceStatus;

		private PresenceStatus _PrevPresenceStatus;

		public bool HasMillisecondsSincePrev;

		private long _MillisecondsSincePrev;

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

		public PresenceStatus NewPresenceStatus
		{
			get
			{
				return _NewPresenceStatus;
			}
			set
			{
				_NewPresenceStatus = value;
				HasNewPresenceStatus = value != null;
			}
		}

		public PresenceStatus PrevPresenceStatus
		{
			get
			{
				return _PrevPresenceStatus;
			}
			set
			{
				_PrevPresenceStatus = value;
				HasPrevPresenceStatus = value != null;
			}
		}

		public long MillisecondsSincePrev
		{
			get
			{
				return _MillisecondsSincePrev;
			}
			set
			{
				_MillisecondsSincePrev = value;
				HasMillisecondsSincePrev = true;
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
			if (HasNewPresenceStatus)
			{
				num ^= NewPresenceStatus.GetHashCode();
			}
			if (HasPrevPresenceStatus)
			{
				num ^= PrevPresenceStatus.GetHashCode();
			}
			if (HasMillisecondsSincePrev)
			{
				num ^= MillisecondsSincePrev.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			PresenceChanged presenceChanged = obj as PresenceChanged;
			if (presenceChanged == null)
			{
				return false;
			}
			if (HasPlayer != presenceChanged.HasPlayer || (HasPlayer && !Player.Equals(presenceChanged.Player)))
			{
				return false;
			}
			if (HasDeviceInfo != presenceChanged.HasDeviceInfo || (HasDeviceInfo && !DeviceInfo.Equals(presenceChanged.DeviceInfo)))
			{
				return false;
			}
			if (HasNewPresenceStatus != presenceChanged.HasNewPresenceStatus || (HasNewPresenceStatus && !NewPresenceStatus.Equals(presenceChanged.NewPresenceStatus)))
			{
				return false;
			}
			if (HasPrevPresenceStatus != presenceChanged.HasPrevPresenceStatus || (HasPrevPresenceStatus && !PrevPresenceStatus.Equals(presenceChanged.PrevPresenceStatus)))
			{
				return false;
			}
			if (HasMillisecondsSincePrev != presenceChanged.HasMillisecondsSincePrev || (HasMillisecondsSincePrev && !MillisecondsSincePrev.Equals(presenceChanged.MillisecondsSincePrev)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static PresenceChanged Deserialize(Stream stream, PresenceChanged instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static PresenceChanged DeserializeLengthDelimited(Stream stream)
		{
			PresenceChanged presenceChanged = new PresenceChanged();
			DeserializeLengthDelimited(stream, presenceChanged);
			return presenceChanged;
		}

		public static PresenceChanged DeserializeLengthDelimited(Stream stream, PresenceChanged instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static PresenceChanged Deserialize(Stream stream, PresenceChanged instance, long limit)
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
					if (instance.NewPresenceStatus == null)
					{
						instance.NewPresenceStatus = PresenceStatus.DeserializeLengthDelimited(stream);
					}
					else
					{
						PresenceStatus.DeserializeLengthDelimited(stream, instance.NewPresenceStatus);
					}
					continue;
				case 34:
					if (instance.PrevPresenceStatus == null)
					{
						instance.PrevPresenceStatus = PresenceStatus.DeserializeLengthDelimited(stream);
					}
					else
					{
						PresenceStatus.DeserializeLengthDelimited(stream, instance.PrevPresenceStatus);
					}
					continue;
				case 40:
					instance.MillisecondsSincePrev = (long)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, PresenceChanged instance)
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
			if (instance.HasNewPresenceStatus)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.NewPresenceStatus.GetSerializedSize());
				PresenceStatus.Serialize(stream, instance.NewPresenceStatus);
			}
			if (instance.HasPrevPresenceStatus)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.PrevPresenceStatus.GetSerializedSize());
				PresenceStatus.Serialize(stream, instance.PrevPresenceStatus);
			}
			if (instance.HasMillisecondsSincePrev)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.MillisecondsSincePrev);
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
			if (HasNewPresenceStatus)
			{
				num++;
				uint serializedSize3 = NewPresenceStatus.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			if (HasPrevPresenceStatus)
			{
				num++;
				uint serializedSize4 = PrevPresenceStatus.GetSerializedSize();
				num += serializedSize4 + ProtocolParser.SizeOfUInt32(serializedSize4);
			}
			if (HasMillisecondsSincePrev)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)MillisecondsSincePrev);
			}
			return num;
		}
	}
}
