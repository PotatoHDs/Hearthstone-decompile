using System.IO;

namespace bnet.protocol.authentication.v1
{
	public class VersionInfoNotification : IProtoBuf
	{
		public bool HasVersionInfo;

		private VersionInfo _VersionInfo;

		public VersionInfo VersionInfo
		{
			get
			{
				return _VersionInfo;
			}
			set
			{
				_VersionInfo = value;
				HasVersionInfo = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetVersionInfo(VersionInfo val)
		{
			VersionInfo = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasVersionInfo)
			{
				num ^= VersionInfo.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			VersionInfoNotification versionInfoNotification = obj as VersionInfoNotification;
			if (versionInfoNotification == null)
			{
				return false;
			}
			if (HasVersionInfo != versionInfoNotification.HasVersionInfo || (HasVersionInfo && !VersionInfo.Equals(versionInfoNotification.VersionInfo)))
			{
				return false;
			}
			return true;
		}

		public static VersionInfoNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<VersionInfoNotification>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static VersionInfoNotification Deserialize(Stream stream, VersionInfoNotification instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static VersionInfoNotification DeserializeLengthDelimited(Stream stream)
		{
			VersionInfoNotification versionInfoNotification = new VersionInfoNotification();
			DeserializeLengthDelimited(stream, versionInfoNotification);
			return versionInfoNotification;
		}

		public static VersionInfoNotification DeserializeLengthDelimited(Stream stream, VersionInfoNotification instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static VersionInfoNotification Deserialize(Stream stream, VersionInfoNotification instance, long limit)
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
					if (instance.VersionInfo == null)
					{
						instance.VersionInfo = VersionInfo.DeserializeLengthDelimited(stream);
					}
					else
					{
						VersionInfo.DeserializeLengthDelimited(stream, instance.VersionInfo);
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

		public static void Serialize(Stream stream, VersionInfoNotification instance)
		{
			if (instance.HasVersionInfo)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.VersionInfo.GetSerializedSize());
				VersionInfo.Serialize(stream, instance.VersionInfo);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasVersionInfo)
			{
				num++;
				uint serializedSize = VersionInfo.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}
	}
}
