using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	public class RequestTavernBrawlPlayerRecord : IProtoBuf
	{
		public enum PacketID
		{
			ID = 353,
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
			RequestTavernBrawlPlayerRecord requestTavernBrawlPlayerRecord = obj as RequestTavernBrawlPlayerRecord;
			if (requestTavernBrawlPlayerRecord == null)
			{
				return false;
			}
			if (!BrawlType.Equals(requestTavernBrawlPlayerRecord.BrawlType))
			{
				return false;
			}
			if (HasFsgId != requestTavernBrawlPlayerRecord.HasFsgId || (HasFsgId && !FsgId.Equals(requestTavernBrawlPlayerRecord.FsgId)))
			{
				return false;
			}
			if (HasFsgSharedSecretKey != requestTavernBrawlPlayerRecord.HasFsgSharedSecretKey || (HasFsgSharedSecretKey && !FsgSharedSecretKey.Equals(requestTavernBrawlPlayerRecord.FsgSharedSecretKey)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static RequestTavernBrawlPlayerRecord Deserialize(Stream stream, RequestTavernBrawlPlayerRecord instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static RequestTavernBrawlPlayerRecord DeserializeLengthDelimited(Stream stream)
		{
			RequestTavernBrawlPlayerRecord requestTavernBrawlPlayerRecord = new RequestTavernBrawlPlayerRecord();
			DeserializeLengthDelimited(stream, requestTavernBrawlPlayerRecord);
			return requestTavernBrawlPlayerRecord;
		}

		public static RequestTavernBrawlPlayerRecord DeserializeLengthDelimited(Stream stream, RequestTavernBrawlPlayerRecord instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static RequestTavernBrawlPlayerRecord Deserialize(Stream stream, RequestTavernBrawlPlayerRecord instance, long limit)
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

		public static void Serialize(Stream stream, RequestTavernBrawlPlayerRecord instance)
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
