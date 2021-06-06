using System.IO;

namespace Blizzard.Telemetry.WTCG.Client
{
	public class Cinematic : IProtoBuf
	{
		public bool HasDeviceInfo;

		private DeviceInfo _DeviceInfo;

		public bool HasBegin;

		private bool _Begin;

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

		public bool Begin
		{
			get
			{
				return _Begin;
			}
			set
			{
				_Begin = value;
				HasBegin = true;
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
			if (HasBegin)
			{
				num ^= Begin.GetHashCode();
			}
			if (HasDuration)
			{
				num ^= Duration.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			Cinematic cinematic = obj as Cinematic;
			if (cinematic == null)
			{
				return false;
			}
			if (HasDeviceInfo != cinematic.HasDeviceInfo || (HasDeviceInfo && !DeviceInfo.Equals(cinematic.DeviceInfo)))
			{
				return false;
			}
			if (HasBegin != cinematic.HasBegin || (HasBegin && !Begin.Equals(cinematic.Begin)))
			{
				return false;
			}
			if (HasDuration != cinematic.HasDuration || (HasDuration && !Duration.Equals(cinematic.Duration)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static Cinematic Deserialize(Stream stream, Cinematic instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static Cinematic DeserializeLengthDelimited(Stream stream)
		{
			Cinematic cinematic = new Cinematic();
			DeserializeLengthDelimited(stream, cinematic);
			return cinematic;
		}

		public static Cinematic DeserializeLengthDelimited(Stream stream, Cinematic instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static Cinematic Deserialize(Stream stream, Cinematic instance, long limit)
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
				case 16:
					instance.Begin = ProtocolParser.ReadBool(stream);
					continue;
				case 29:
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

		public static void Serialize(Stream stream, Cinematic instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasDeviceInfo)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.DeviceInfo.GetSerializedSize());
				DeviceInfo.Serialize(stream, instance.DeviceInfo);
			}
			if (instance.HasBegin)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteBool(stream, instance.Begin);
			}
			if (instance.HasDuration)
			{
				stream.WriteByte(29);
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
			if (HasBegin)
			{
				num++;
				num++;
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
