using System.IO;

namespace PegasusUtil
{
	public class AssetsVersionResponse : IProtoBuf
	{
		public enum PacketID
		{
			ID = 304
		}

		public bool HasReturningPlayerInfo;

		private ReturningPlayerInfo _ReturningPlayerInfo;

		public int Version { get; set; }

		public ReturningPlayerInfo ReturningPlayerInfo
		{
			get
			{
				return _ReturningPlayerInfo;
			}
			set
			{
				_ReturningPlayerInfo = value;
				HasReturningPlayerInfo = value != null;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= Version.GetHashCode();
			if (HasReturningPlayerInfo)
			{
				hashCode ^= ReturningPlayerInfo.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			AssetsVersionResponse assetsVersionResponse = obj as AssetsVersionResponse;
			if (assetsVersionResponse == null)
			{
				return false;
			}
			if (!Version.Equals(assetsVersionResponse.Version))
			{
				return false;
			}
			if (HasReturningPlayerInfo != assetsVersionResponse.HasReturningPlayerInfo || (HasReturningPlayerInfo && !ReturningPlayerInfo.Equals(assetsVersionResponse.ReturningPlayerInfo)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static AssetsVersionResponse Deserialize(Stream stream, AssetsVersionResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static AssetsVersionResponse DeserializeLengthDelimited(Stream stream)
		{
			AssetsVersionResponse assetsVersionResponse = new AssetsVersionResponse();
			DeserializeLengthDelimited(stream, assetsVersionResponse);
			return assetsVersionResponse;
		}

		public static AssetsVersionResponse DeserializeLengthDelimited(Stream stream, AssetsVersionResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static AssetsVersionResponse Deserialize(Stream stream, AssetsVersionResponse instance, long limit)
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
				case 8:
					instance.Version = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 18:
					if (instance.ReturningPlayerInfo == null)
					{
						instance.ReturningPlayerInfo = ReturningPlayerInfo.DeserializeLengthDelimited(stream);
					}
					else
					{
						ReturningPlayerInfo.DeserializeLengthDelimited(stream, instance.ReturningPlayerInfo);
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

		public static void Serialize(Stream stream, AssetsVersionResponse instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Version);
			if (instance.HasReturningPlayerInfo)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.ReturningPlayerInfo.GetSerializedSize());
				ReturningPlayerInfo.Serialize(stream, instance.ReturningPlayerInfo);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)Version);
			if (HasReturningPlayerInfo)
			{
				num++;
				uint serializedSize = ReturningPlayerInfo.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num + 1;
		}
	}
}
