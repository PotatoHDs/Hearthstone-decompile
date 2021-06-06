using System.IO;

namespace Blizzard.Telemetry.WTCG.Client
{
	public class ManaFilterToggleOff : IProtoBuf
	{
		public bool HasDeviceInfo;

		private DeviceInfo _DeviceInfo;

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

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasDeviceInfo)
			{
				num ^= DeviceInfo.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ManaFilterToggleOff manaFilterToggleOff = obj as ManaFilterToggleOff;
			if (manaFilterToggleOff == null)
			{
				return false;
			}
			if (HasDeviceInfo != manaFilterToggleOff.HasDeviceInfo || (HasDeviceInfo && !DeviceInfo.Equals(manaFilterToggleOff.DeviceInfo)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ManaFilterToggleOff Deserialize(Stream stream, ManaFilterToggleOff instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ManaFilterToggleOff DeserializeLengthDelimited(Stream stream)
		{
			ManaFilterToggleOff manaFilterToggleOff = new ManaFilterToggleOff();
			DeserializeLengthDelimited(stream, manaFilterToggleOff);
			return manaFilterToggleOff;
		}

		public static ManaFilterToggleOff DeserializeLengthDelimited(Stream stream, ManaFilterToggleOff instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ManaFilterToggleOff Deserialize(Stream stream, ManaFilterToggleOff instance, long limit)
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

		public static void Serialize(Stream stream, ManaFilterToggleOff instance)
		{
			if (instance.HasDeviceInfo)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.DeviceInfo.GetSerializedSize());
				DeviceInfo.Serialize(stream, instance.DeviceInfo);
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
			return num;
		}
	}
}
