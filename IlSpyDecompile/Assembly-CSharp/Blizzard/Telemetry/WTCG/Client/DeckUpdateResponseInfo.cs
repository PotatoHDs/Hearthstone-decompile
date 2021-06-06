using System.IO;

namespace Blizzard.Telemetry.WTCG.Client
{
	public class DeckUpdateResponseInfo : IProtoBuf
	{
		public bool HasDeviceInfo;

		private DeviceInfo _DeviceInfo;

		public bool HasDuration;

		private float _Duration;

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

		public float Duration
		{
			get
			{
				return _Duration;
			}
			set
			{
				_Duration = value;
				HasDuration = true;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasDeviceInfo)
			{
				num ^= DeviceInfo.GetHashCode();
			}
			if (HasDuration)
			{
				num ^= Duration.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			DeckUpdateResponseInfo deckUpdateResponseInfo = obj as DeckUpdateResponseInfo;
			if (deckUpdateResponseInfo == null)
			{
				return false;
			}
			if (HasDeviceInfo != deckUpdateResponseInfo.HasDeviceInfo || (HasDeviceInfo && !DeviceInfo.Equals(deckUpdateResponseInfo.DeviceInfo)))
			{
				return false;
			}
			if (HasDuration != deckUpdateResponseInfo.HasDuration || (HasDuration && !Duration.Equals(deckUpdateResponseInfo.Duration)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static DeckUpdateResponseInfo Deserialize(Stream stream, DeckUpdateResponseInfo instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static DeckUpdateResponseInfo DeserializeLengthDelimited(Stream stream)
		{
			DeckUpdateResponseInfo deckUpdateResponseInfo = new DeckUpdateResponseInfo();
			DeserializeLengthDelimited(stream, deckUpdateResponseInfo);
			return deckUpdateResponseInfo;
		}

		public static DeckUpdateResponseInfo DeserializeLengthDelimited(Stream stream, DeckUpdateResponseInfo instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static DeckUpdateResponseInfo Deserialize(Stream stream, DeckUpdateResponseInfo instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
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
				case 21:
					instance.Duration = binaryReader.ReadSingle();
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

		public static void Serialize(Stream stream, DeckUpdateResponseInfo instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasDeviceInfo)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.DeviceInfo.GetSerializedSize());
				DeviceInfo.Serialize(stream, instance.DeviceInfo);
			}
			if (instance.HasDuration)
			{
				stream.WriteByte(21);
				binaryWriter.Write(instance.Duration);
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
			if (HasDuration)
			{
				num++;
				num += 4;
			}
			return num;
		}
	}
}
