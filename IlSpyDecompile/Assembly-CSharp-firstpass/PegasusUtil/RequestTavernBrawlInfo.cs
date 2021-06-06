using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	public class RequestTavernBrawlInfo : IProtoBuf
	{
		public enum PacketID
		{
			ID = 352,
			System = 0
		}

		public bool HasFsgId;

		private long _FsgId;

		public bool HasFsgSharedSecretKey;

		private byte[] _FsgSharedSecretKey;

		public BrawlType BrawlType { get; set; }

		public long FsgId
		{
			get
			{
				return _FsgId;
			}
			set
			{
				_FsgId = value;
				HasFsgId = true;
			}
		}

		public byte[] FsgSharedSecretKey
		{
			get
			{
				return _FsgSharedSecretKey;
			}
			set
			{
				_FsgSharedSecretKey = value;
				HasFsgSharedSecretKey = value != null;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= BrawlType.GetHashCode();
			if (HasFsgId)
			{
				hashCode ^= FsgId.GetHashCode();
			}
			if (HasFsgSharedSecretKey)
			{
				hashCode ^= FsgSharedSecretKey.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			RequestTavernBrawlInfo requestTavernBrawlInfo = obj as RequestTavernBrawlInfo;
			if (requestTavernBrawlInfo == null)
			{
				return false;
			}
			if (!BrawlType.Equals(requestTavernBrawlInfo.BrawlType))
			{
				return false;
			}
			if (HasFsgId != requestTavernBrawlInfo.HasFsgId || (HasFsgId && !FsgId.Equals(requestTavernBrawlInfo.FsgId)))
			{
				return false;
			}
			if (HasFsgSharedSecretKey != requestTavernBrawlInfo.HasFsgSharedSecretKey || (HasFsgSharedSecretKey && !FsgSharedSecretKey.Equals(requestTavernBrawlInfo.FsgSharedSecretKey)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static RequestTavernBrawlInfo Deserialize(Stream stream, RequestTavernBrawlInfo instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static RequestTavernBrawlInfo DeserializeLengthDelimited(Stream stream)
		{
			RequestTavernBrawlInfo requestTavernBrawlInfo = new RequestTavernBrawlInfo();
			DeserializeLengthDelimited(stream, requestTavernBrawlInfo);
			return requestTavernBrawlInfo;
		}

		public static RequestTavernBrawlInfo DeserializeLengthDelimited(Stream stream, RequestTavernBrawlInfo instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static RequestTavernBrawlInfo Deserialize(Stream stream, RequestTavernBrawlInfo instance, long limit)
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
				case 16:
					instance.BrawlType = (BrawlType)ProtocolParser.ReadUInt64(stream);
					continue;
				default:
				{
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					switch (key.Field)
					{
					case 0u:
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					case 100u:
						if (key.WireType == Wire.Varint)
						{
							instance.FsgId = (long)ProtocolParser.ReadUInt64(stream);
						}
						break;
					case 101u:
						if (key.WireType == Wire.LengthDelimited)
						{
							instance.FsgSharedSecretKey = ProtocolParser.ReadBytes(stream);
						}
						break;
					default:
						ProtocolParser.SkipKey(stream, key);
						break;
					}
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

		public static void Serialize(Stream stream, RequestTavernBrawlInfo instance)
		{
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.BrawlType);
			if (instance.HasFsgId)
			{
				stream.WriteByte(160);
				stream.WriteByte(6);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.FsgId);
			}
			if (instance.HasFsgSharedSecretKey)
			{
				stream.WriteByte(170);
				stream.WriteByte(6);
				ProtocolParser.WriteBytes(stream, instance.FsgSharedSecretKey);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)BrawlType);
			if (HasFsgId)
			{
				num += 2;
				num += ProtocolParser.SizeOfUInt64((ulong)FsgId);
			}
			if (HasFsgSharedSecretKey)
			{
				num += 2;
				num += (uint)((int)ProtocolParser.SizeOfUInt32(FsgSharedSecretKey.Length) + FsgSharedSecretKey.Length);
			}
			return num + 1;
		}
	}
}
