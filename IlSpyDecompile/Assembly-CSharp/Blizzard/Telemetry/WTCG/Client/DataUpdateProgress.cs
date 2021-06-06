using System.IO;

namespace Blizzard.Telemetry.WTCG.Client
{
	public class DataUpdateProgress : IProtoBuf
	{
		public bool HasDeviceInfo;

		private DeviceInfo _DeviceInfo;

		public bool HasDuration;

		private float _Duration;

		public bool HasRealDownloadBytes;

		private long _RealDownloadBytes;

		public bool HasExpectedDownloadBytes;

		private long _ExpectedDownloadBytes;

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

		public long RealDownloadBytes
		{
			get
			{
				return _RealDownloadBytes;
			}
			set
			{
				_RealDownloadBytes = value;
				HasRealDownloadBytes = true;
			}
		}

		public long ExpectedDownloadBytes
		{
			get
			{
				return _ExpectedDownloadBytes;
			}
			set
			{
				_ExpectedDownloadBytes = value;
				HasExpectedDownloadBytes = true;
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
			if (HasRealDownloadBytes)
			{
				num ^= RealDownloadBytes.GetHashCode();
			}
			if (HasExpectedDownloadBytes)
			{
				num ^= ExpectedDownloadBytes.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			DataUpdateProgress dataUpdateProgress = obj as DataUpdateProgress;
			if (dataUpdateProgress == null)
			{
				return false;
			}
			if (HasDeviceInfo != dataUpdateProgress.HasDeviceInfo || (HasDeviceInfo && !DeviceInfo.Equals(dataUpdateProgress.DeviceInfo)))
			{
				return false;
			}
			if (HasDuration != dataUpdateProgress.HasDuration || (HasDuration && !Duration.Equals(dataUpdateProgress.Duration)))
			{
				return false;
			}
			if (HasRealDownloadBytes != dataUpdateProgress.HasRealDownloadBytes || (HasRealDownloadBytes && !RealDownloadBytes.Equals(dataUpdateProgress.RealDownloadBytes)))
			{
				return false;
			}
			if (HasExpectedDownloadBytes != dataUpdateProgress.HasExpectedDownloadBytes || (HasExpectedDownloadBytes && !ExpectedDownloadBytes.Equals(dataUpdateProgress.ExpectedDownloadBytes)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static DataUpdateProgress Deserialize(Stream stream, DataUpdateProgress instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static DataUpdateProgress DeserializeLengthDelimited(Stream stream)
		{
			DataUpdateProgress dataUpdateProgress = new DataUpdateProgress();
			DeserializeLengthDelimited(stream, dataUpdateProgress);
			return dataUpdateProgress;
		}

		public static DataUpdateProgress DeserializeLengthDelimited(Stream stream, DataUpdateProgress instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static DataUpdateProgress Deserialize(Stream stream, DataUpdateProgress instance, long limit)
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
				case 24:
					instance.RealDownloadBytes = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				case 32:
					instance.ExpectedDownloadBytes = (long)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, DataUpdateProgress instance)
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
			if (instance.HasRealDownloadBytes)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.RealDownloadBytes);
			}
			if (instance.HasExpectedDownloadBytes)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.ExpectedDownloadBytes);
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
			if (HasRealDownloadBytes)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)RealDownloadBytes);
			}
			if (HasExpectedDownloadBytes)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)ExpectedDownloadBytes);
			}
			return num;
		}
	}
}
