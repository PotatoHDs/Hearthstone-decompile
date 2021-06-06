using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	public class FatalError : IProtoBuf
	{
		public bool HasDeviceInfo;

		private DeviceInfo _DeviceInfo;

		public bool HasReason;

		private string _Reason;

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

		public string Reason
		{
			get
			{
				return _Reason;
			}
			set
			{
				_Reason = value;
				HasReason = value != null;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasDeviceInfo)
			{
				num ^= DeviceInfo.GetHashCode();
			}
			if (HasReason)
			{
				num ^= Reason.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			FatalError fatalError = obj as FatalError;
			if (fatalError == null)
			{
				return false;
			}
			if (HasDeviceInfo != fatalError.HasDeviceInfo || (HasDeviceInfo && !DeviceInfo.Equals(fatalError.DeviceInfo)))
			{
				return false;
			}
			if (HasReason != fatalError.HasReason || (HasReason && !Reason.Equals(fatalError.Reason)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static FatalError Deserialize(Stream stream, FatalError instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static FatalError DeserializeLengthDelimited(Stream stream)
		{
			FatalError fatalError = new FatalError();
			DeserializeLengthDelimited(stream, fatalError);
			return fatalError;
		}

		public static FatalError DeserializeLengthDelimited(Stream stream, FatalError instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static FatalError Deserialize(Stream stream, FatalError instance, long limit)
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
					instance.Reason = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, FatalError instance)
		{
			if (instance.HasDeviceInfo)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.DeviceInfo.GetSerializedSize());
				DeviceInfo.Serialize(stream, instance.DeviceInfo);
			}
			if (instance.HasReason)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Reason));
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
			if (HasReason)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(Reason);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num;
		}
	}
}
