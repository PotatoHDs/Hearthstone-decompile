using System.IO;

namespace PegasusUtil
{
	public class PurchaseWithGoldResponse : IProtoBuf
	{
		public enum PacketID
		{
			ID = 280
		}

		public enum PurchaseResult
		{
			PR_SUCCESS = 1,
			PR_INSUFFICIENT_FUNDS,
			PR_PRODUCT_NA,
			PR_FEATURE_NA,
			PR_INVALID_QUANTITY,
			PR_PRODUCT_EVENT_HAS_ENDED
		}

		public bool HasGoldUsed;

		private long _GoldUsed;

		public PurchaseResult Result { get; set; }

		public long GoldUsed
		{
			get
			{
				return _GoldUsed;
			}
			set
			{
				_GoldUsed = value;
				HasGoldUsed = true;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= Result.GetHashCode();
			if (HasGoldUsed)
			{
				hashCode ^= GoldUsed.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			PurchaseWithGoldResponse purchaseWithGoldResponse = obj as PurchaseWithGoldResponse;
			if (purchaseWithGoldResponse == null)
			{
				return false;
			}
			if (!Result.Equals(purchaseWithGoldResponse.Result))
			{
				return false;
			}
			if (HasGoldUsed != purchaseWithGoldResponse.HasGoldUsed || (HasGoldUsed && !GoldUsed.Equals(purchaseWithGoldResponse.GoldUsed)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static PurchaseWithGoldResponse Deserialize(Stream stream, PurchaseWithGoldResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static PurchaseWithGoldResponse DeserializeLengthDelimited(Stream stream)
		{
			PurchaseWithGoldResponse purchaseWithGoldResponse = new PurchaseWithGoldResponse();
			DeserializeLengthDelimited(stream, purchaseWithGoldResponse);
			return purchaseWithGoldResponse;
		}

		public static PurchaseWithGoldResponse DeserializeLengthDelimited(Stream stream, PurchaseWithGoldResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static PurchaseWithGoldResponse Deserialize(Stream stream, PurchaseWithGoldResponse instance, long limit)
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
					instance.Result = (PurchaseResult)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.GoldUsed = (long)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, PurchaseWithGoldResponse instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Result);
			if (instance.HasGoldUsed)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.GoldUsed);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)Result);
			if (HasGoldUsed)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)GoldUsed);
			}
			return num + 1;
		}
	}
}
