using System.IO;

namespace Blizzard.Telemetry.WTCG.Client
{
	public class RuntimeUpdate : IProtoBuf
	{
		public enum Intention
		{
			UNINITIALIZED = -1,
			HIGH_RES = 0,
			DONE = 100
		}

		public bool HasDeviceInfo;

		private DeviceInfo _DeviceInfo;

		public bool HasDuration;

		private float _Duration;

		public bool HasIntention_;

		private Intention _Intention_;

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

		public Intention Intention_
		{
			get
			{
				return _Intention_;
			}
			set
			{
				_Intention_ = value;
				HasIntention_ = true;
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
			if (HasIntention_)
			{
				num ^= Intention_.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			RuntimeUpdate runtimeUpdate = obj as RuntimeUpdate;
			if (runtimeUpdate == null)
			{
				return false;
			}
			if (HasDeviceInfo != runtimeUpdate.HasDeviceInfo || (HasDeviceInfo && !DeviceInfo.Equals(runtimeUpdate.DeviceInfo)))
			{
				return false;
			}
			if (HasDuration != runtimeUpdate.HasDuration || (HasDuration && !Duration.Equals(runtimeUpdate.Duration)))
			{
				return false;
			}
			if (HasIntention_ != runtimeUpdate.HasIntention_ || (HasIntention_ && !Intention_.Equals(runtimeUpdate.Intention_)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static RuntimeUpdate Deserialize(Stream stream, RuntimeUpdate instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static RuntimeUpdate DeserializeLengthDelimited(Stream stream)
		{
			RuntimeUpdate runtimeUpdate = new RuntimeUpdate();
			DeserializeLengthDelimited(stream, runtimeUpdate);
			return runtimeUpdate;
		}

		public static RuntimeUpdate DeserializeLengthDelimited(Stream stream, RuntimeUpdate instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static RuntimeUpdate Deserialize(Stream stream, RuntimeUpdate instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			instance.Intention_ = Intention.UNINITIALIZED;
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
				case 24:
					instance.Intention_ = (Intention)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, RuntimeUpdate instance)
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
			if (instance.HasIntention_)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.Intention_);
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
			if (HasIntention_)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)Intention_);
			}
			return num;
		}
	}
}
