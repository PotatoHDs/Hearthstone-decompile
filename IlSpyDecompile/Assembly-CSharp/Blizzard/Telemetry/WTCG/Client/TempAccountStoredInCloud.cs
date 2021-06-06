using System.IO;

namespace Blizzard.Telemetry.WTCG.Client
{
	public class TempAccountStoredInCloud : IProtoBuf
	{
		public bool HasDeviceInfo;

		private DeviceInfo _DeviceInfo;

		public bool HasStored;

		private bool _Stored;

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

		public bool Stored
		{
			get
			{
				return _Stored;
			}
			set
			{
				_Stored = value;
				HasStored = true;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasDeviceInfo)
			{
				num ^= DeviceInfo.GetHashCode();
			}
			if (HasStored)
			{
				num ^= Stored.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			TempAccountStoredInCloud tempAccountStoredInCloud = obj as TempAccountStoredInCloud;
			if (tempAccountStoredInCloud == null)
			{
				return false;
			}
			if (HasDeviceInfo != tempAccountStoredInCloud.HasDeviceInfo || (HasDeviceInfo && !DeviceInfo.Equals(tempAccountStoredInCloud.DeviceInfo)))
			{
				return false;
			}
			if (HasStored != tempAccountStoredInCloud.HasStored || (HasStored && !Stored.Equals(tempAccountStoredInCloud.Stored)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static TempAccountStoredInCloud Deserialize(Stream stream, TempAccountStoredInCloud instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static TempAccountStoredInCloud DeserializeLengthDelimited(Stream stream)
		{
			TempAccountStoredInCloud tempAccountStoredInCloud = new TempAccountStoredInCloud();
			DeserializeLengthDelimited(stream, tempAccountStoredInCloud);
			return tempAccountStoredInCloud;
		}

		public static TempAccountStoredInCloud DeserializeLengthDelimited(Stream stream, TempAccountStoredInCloud instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static TempAccountStoredInCloud Deserialize(Stream stream, TempAccountStoredInCloud instance, long limit)
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
					instance.Stored = ProtocolParser.ReadBool(stream);
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

		public static void Serialize(Stream stream, TempAccountStoredInCloud instance)
		{
			if (instance.HasDeviceInfo)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.DeviceInfo.GetSerializedSize());
				DeviceInfo.Serialize(stream, instance.DeviceInfo);
			}
			if (instance.HasStored)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteBool(stream, instance.Stored);
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
			if (HasStored)
			{
				num++;
				num++;
			}
			return num;
		}
	}
}
