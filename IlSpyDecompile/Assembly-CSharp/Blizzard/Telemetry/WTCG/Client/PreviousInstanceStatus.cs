using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	public class PreviousInstanceStatus : IProtoBuf
	{
		public bool HasDeviceInfo;

		private DeviceInfo _DeviceInfo;

		public bool HasTotalCrashCount;

		private int _TotalCrashCount;

		public bool HasTotalExceptionCount;

		private int _TotalExceptionCount;

		public bool HasLowMemoryWarningCount;

		private int _LowMemoryWarningCount;

		public bool HasCrashInARowCount;

		private int _CrashInARowCount;

		public bool HasSameExceptionCount;

		private int _SameExceptionCount;

		public bool HasCrashed;

		private bool _Crashed;

		public bool HasExceptionHash;

		private string _ExceptionHash;

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

		public int TotalCrashCount
		{
			get
			{
				return _TotalCrashCount;
			}
			set
			{
				_TotalCrashCount = value;
				HasTotalCrashCount = true;
			}
		}

		public int TotalExceptionCount
		{
			get
			{
				return _TotalExceptionCount;
			}
			set
			{
				_TotalExceptionCount = value;
				HasTotalExceptionCount = true;
			}
		}

		public int LowMemoryWarningCount
		{
			get
			{
				return _LowMemoryWarningCount;
			}
			set
			{
				_LowMemoryWarningCount = value;
				HasLowMemoryWarningCount = true;
			}
		}

		public int CrashInARowCount
		{
			get
			{
				return _CrashInARowCount;
			}
			set
			{
				_CrashInARowCount = value;
				HasCrashInARowCount = true;
			}
		}

		public int SameExceptionCount
		{
			get
			{
				return _SameExceptionCount;
			}
			set
			{
				_SameExceptionCount = value;
				HasSameExceptionCount = true;
			}
		}

		public bool Crashed
		{
			get
			{
				return _Crashed;
			}
			set
			{
				_Crashed = value;
				HasCrashed = true;
			}
		}

		public string ExceptionHash
		{
			get
			{
				return _ExceptionHash;
			}
			set
			{
				_ExceptionHash = value;
				HasExceptionHash = value != null;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasDeviceInfo)
			{
				num ^= DeviceInfo.GetHashCode();
			}
			if (HasTotalCrashCount)
			{
				num ^= TotalCrashCount.GetHashCode();
			}
			if (HasTotalExceptionCount)
			{
				num ^= TotalExceptionCount.GetHashCode();
			}
			if (HasLowMemoryWarningCount)
			{
				num ^= LowMemoryWarningCount.GetHashCode();
			}
			if (HasCrashInARowCount)
			{
				num ^= CrashInARowCount.GetHashCode();
			}
			if (HasSameExceptionCount)
			{
				num ^= SameExceptionCount.GetHashCode();
			}
			if (HasCrashed)
			{
				num ^= Crashed.GetHashCode();
			}
			if (HasExceptionHash)
			{
				num ^= ExceptionHash.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			PreviousInstanceStatus previousInstanceStatus = obj as PreviousInstanceStatus;
			if (previousInstanceStatus == null)
			{
				return false;
			}
			if (HasDeviceInfo != previousInstanceStatus.HasDeviceInfo || (HasDeviceInfo && !DeviceInfo.Equals(previousInstanceStatus.DeviceInfo)))
			{
				return false;
			}
			if (HasTotalCrashCount != previousInstanceStatus.HasTotalCrashCount || (HasTotalCrashCount && !TotalCrashCount.Equals(previousInstanceStatus.TotalCrashCount)))
			{
				return false;
			}
			if (HasTotalExceptionCount != previousInstanceStatus.HasTotalExceptionCount || (HasTotalExceptionCount && !TotalExceptionCount.Equals(previousInstanceStatus.TotalExceptionCount)))
			{
				return false;
			}
			if (HasLowMemoryWarningCount != previousInstanceStatus.HasLowMemoryWarningCount || (HasLowMemoryWarningCount && !LowMemoryWarningCount.Equals(previousInstanceStatus.LowMemoryWarningCount)))
			{
				return false;
			}
			if (HasCrashInARowCount != previousInstanceStatus.HasCrashInARowCount || (HasCrashInARowCount && !CrashInARowCount.Equals(previousInstanceStatus.CrashInARowCount)))
			{
				return false;
			}
			if (HasSameExceptionCount != previousInstanceStatus.HasSameExceptionCount || (HasSameExceptionCount && !SameExceptionCount.Equals(previousInstanceStatus.SameExceptionCount)))
			{
				return false;
			}
			if (HasCrashed != previousInstanceStatus.HasCrashed || (HasCrashed && !Crashed.Equals(previousInstanceStatus.Crashed)))
			{
				return false;
			}
			if (HasExceptionHash != previousInstanceStatus.HasExceptionHash || (HasExceptionHash && !ExceptionHash.Equals(previousInstanceStatus.ExceptionHash)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static PreviousInstanceStatus Deserialize(Stream stream, PreviousInstanceStatus instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static PreviousInstanceStatus DeserializeLengthDelimited(Stream stream)
		{
			PreviousInstanceStatus previousInstanceStatus = new PreviousInstanceStatus();
			DeserializeLengthDelimited(stream, previousInstanceStatus);
			return previousInstanceStatus;
		}

		public static PreviousInstanceStatus DeserializeLengthDelimited(Stream stream, PreviousInstanceStatus instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static PreviousInstanceStatus Deserialize(Stream stream, PreviousInstanceStatus instance, long limit)
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
					instance.TotalCrashCount = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.TotalExceptionCount = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 32:
					instance.LowMemoryWarningCount = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 40:
					instance.CrashInARowCount = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 48:
					instance.SameExceptionCount = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 56:
					instance.Crashed = ProtocolParser.ReadBool(stream);
					continue;
				case 66:
					instance.ExceptionHash = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, PreviousInstanceStatus instance)
		{
			if (instance.HasDeviceInfo)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.DeviceInfo.GetSerializedSize());
				DeviceInfo.Serialize(stream, instance.DeviceInfo);
			}
			if (instance.HasTotalCrashCount)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.TotalCrashCount);
			}
			if (instance.HasTotalExceptionCount)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.TotalExceptionCount);
			}
			if (instance.HasLowMemoryWarningCount)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.LowMemoryWarningCount);
			}
			if (instance.HasCrashInARowCount)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.CrashInARowCount);
			}
			if (instance.HasSameExceptionCount)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.SameExceptionCount);
			}
			if (instance.HasCrashed)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteBool(stream, instance.Crashed);
			}
			if (instance.HasExceptionHash)
			{
				stream.WriteByte(66);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ExceptionHash));
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
			if (HasTotalCrashCount)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)TotalCrashCount);
			}
			if (HasTotalExceptionCount)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)TotalExceptionCount);
			}
			if (HasLowMemoryWarningCount)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)LowMemoryWarningCount);
			}
			if (HasCrashInARowCount)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)CrashInARowCount);
			}
			if (HasSameExceptionCount)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)SameExceptionCount);
			}
			if (HasCrashed)
			{
				num++;
				num++;
			}
			if (HasExceptionHash)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(ExceptionHash);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num;
		}
	}
}
