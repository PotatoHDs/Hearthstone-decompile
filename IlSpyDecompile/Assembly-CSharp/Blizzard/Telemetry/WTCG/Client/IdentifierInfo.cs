using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	public class IdentifierInfo : IProtoBuf
	{
		public bool HasAppInstallId;

		private string _AppInstallId;

		public bool HasDeviceId;

		private string _DeviceId;

		public bool HasAdvertisingId;

		private string _AdvertisingId;

		public string AppInstallId
		{
			get
			{
				return _AppInstallId;
			}
			set
			{
				_AppInstallId = value;
				HasAppInstallId = value != null;
			}
		}

		public string DeviceId
		{
			get
			{
				return _DeviceId;
			}
			set
			{
				_DeviceId = value;
				HasDeviceId = value != null;
			}
		}

		public string AdvertisingId
		{
			get
			{
				return _AdvertisingId;
			}
			set
			{
				_AdvertisingId = value;
				HasAdvertisingId = value != null;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasAppInstallId)
			{
				num ^= AppInstallId.GetHashCode();
			}
			if (HasDeviceId)
			{
				num ^= DeviceId.GetHashCode();
			}
			if (HasAdvertisingId)
			{
				num ^= AdvertisingId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			IdentifierInfo identifierInfo = obj as IdentifierInfo;
			if (identifierInfo == null)
			{
				return false;
			}
			if (HasAppInstallId != identifierInfo.HasAppInstallId || (HasAppInstallId && !AppInstallId.Equals(identifierInfo.AppInstallId)))
			{
				return false;
			}
			if (HasDeviceId != identifierInfo.HasDeviceId || (HasDeviceId && !DeviceId.Equals(identifierInfo.DeviceId)))
			{
				return false;
			}
			if (HasAdvertisingId != identifierInfo.HasAdvertisingId || (HasAdvertisingId && !AdvertisingId.Equals(identifierInfo.AdvertisingId)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static IdentifierInfo Deserialize(Stream stream, IdentifierInfo instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static IdentifierInfo DeserializeLengthDelimited(Stream stream)
		{
			IdentifierInfo identifierInfo = new IdentifierInfo();
			DeserializeLengthDelimited(stream, identifierInfo);
			return identifierInfo;
		}

		public static IdentifierInfo DeserializeLengthDelimited(Stream stream, IdentifierInfo instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static IdentifierInfo Deserialize(Stream stream, IdentifierInfo instance, long limit)
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
				if (num == -1)
				{
					if (limit < 0)
					{
						break;
					}
					throw new EndOfStreamException();
				}
				Key key = ProtocolParser.ReadKey((byte)num, stream);
				switch (key.Field)
				{
				case 0u:
					throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
				case 101u:
					if (key.WireType == Wire.LengthDelimited)
					{
						instance.AppInstallId = ProtocolParser.ReadString(stream);
					}
					break;
				case 102u:
					if (key.WireType == Wire.LengthDelimited)
					{
						instance.DeviceId = ProtocolParser.ReadString(stream);
					}
					break;
				case 103u:
					if (key.WireType == Wire.LengthDelimited)
					{
						instance.AdvertisingId = ProtocolParser.ReadString(stream);
					}
					break;
				default:
					ProtocolParser.SkipKey(stream, key);
					break;
				}
			}
			return instance;
		}

		public void Serialize(Stream stream)
		{
			Serialize(stream, this);
		}

		public static void Serialize(Stream stream, IdentifierInfo instance)
		{
			if (instance.HasAppInstallId)
			{
				stream.WriteByte(170);
				stream.WriteByte(6);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.AppInstallId));
			}
			if (instance.HasDeviceId)
			{
				stream.WriteByte(178);
				stream.WriteByte(6);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.DeviceId));
			}
			if (instance.HasAdvertisingId)
			{
				stream.WriteByte(186);
				stream.WriteByte(6);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.AdvertisingId));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasAppInstallId)
			{
				num += 2;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(AppInstallId);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasDeviceId)
			{
				num += 2;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(DeviceId);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (HasAdvertisingId)
			{
				num += 2;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(AdvertisingId);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			return num;
		}
	}
}
