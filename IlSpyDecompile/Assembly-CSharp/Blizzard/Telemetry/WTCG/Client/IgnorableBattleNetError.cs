using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	public class IgnorableBattleNetError : IProtoBuf
	{
		public bool HasDeviceInfo;

		private DeviceInfo _DeviceInfo;

		public bool HasErrorCode;

		private int _ErrorCode;

		public bool HasDescription;

		private string _Description;

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

		public int ErrorCode
		{
			get
			{
				return _ErrorCode;
			}
			set
			{
				_ErrorCode = value;
				HasErrorCode = true;
			}
		}

		public string Description
		{
			get
			{
				return _Description;
			}
			set
			{
				_Description = value;
				HasDescription = value != null;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasDeviceInfo)
			{
				num ^= DeviceInfo.GetHashCode();
			}
			if (HasErrorCode)
			{
				num ^= ErrorCode.GetHashCode();
			}
			if (HasDescription)
			{
				num ^= Description.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			IgnorableBattleNetError ignorableBattleNetError = obj as IgnorableBattleNetError;
			if (ignorableBattleNetError == null)
			{
				return false;
			}
			if (HasDeviceInfo != ignorableBattleNetError.HasDeviceInfo || (HasDeviceInfo && !DeviceInfo.Equals(ignorableBattleNetError.DeviceInfo)))
			{
				return false;
			}
			if (HasErrorCode != ignorableBattleNetError.HasErrorCode || (HasErrorCode && !ErrorCode.Equals(ignorableBattleNetError.ErrorCode)))
			{
				return false;
			}
			if (HasDescription != ignorableBattleNetError.HasDescription || (HasDescription && !Description.Equals(ignorableBattleNetError.Description)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static IgnorableBattleNetError Deserialize(Stream stream, IgnorableBattleNetError instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static IgnorableBattleNetError DeserializeLengthDelimited(Stream stream)
		{
			IgnorableBattleNetError ignorableBattleNetError = new IgnorableBattleNetError();
			DeserializeLengthDelimited(stream, ignorableBattleNetError);
			return ignorableBattleNetError;
		}

		public static IgnorableBattleNetError DeserializeLengthDelimited(Stream stream, IgnorableBattleNetError instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static IgnorableBattleNetError Deserialize(Stream stream, IgnorableBattleNetError instance, long limit)
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
				case 16:
					instance.ErrorCode = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 26:
					instance.Description = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, IgnorableBattleNetError instance)
		{
			if (instance.HasDeviceInfo)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.DeviceInfo.GetSerializedSize());
				DeviceInfo.Serialize(stream, instance.DeviceInfo);
			}
			if (instance.HasErrorCode)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.ErrorCode);
			}
			if (instance.HasDescription)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Description));
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
			if (HasErrorCode)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)ErrorCode);
			}
			if (HasDescription)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(Description);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num;
		}
	}
}
