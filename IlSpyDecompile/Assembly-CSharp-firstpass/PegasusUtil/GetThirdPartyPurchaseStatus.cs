using System;
using System.IO;
using System.Text;

namespace PegasusUtil
{
	public class GetThirdPartyPurchaseStatus : IProtoBuf
	{
		public enum PacketID
		{
			ID = 294,
			System = 1
		}

		public string ThirdPartyId { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ ThirdPartyId.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			GetThirdPartyPurchaseStatus getThirdPartyPurchaseStatus = obj as GetThirdPartyPurchaseStatus;
			if (getThirdPartyPurchaseStatus == null)
			{
				return false;
			}
			if (!ThirdPartyId.Equals(getThirdPartyPurchaseStatus.ThirdPartyId))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GetThirdPartyPurchaseStatus Deserialize(Stream stream, GetThirdPartyPurchaseStatus instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GetThirdPartyPurchaseStatus DeserializeLengthDelimited(Stream stream)
		{
			GetThirdPartyPurchaseStatus getThirdPartyPurchaseStatus = new GetThirdPartyPurchaseStatus();
			DeserializeLengthDelimited(stream, getThirdPartyPurchaseStatus);
			return getThirdPartyPurchaseStatus;
		}

		public static GetThirdPartyPurchaseStatus DeserializeLengthDelimited(Stream stream, GetThirdPartyPurchaseStatus instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GetThirdPartyPurchaseStatus Deserialize(Stream stream, GetThirdPartyPurchaseStatus instance, long limit)
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
					instance.ThirdPartyId = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, GetThirdPartyPurchaseStatus instance)
		{
			if (instance.ThirdPartyId == null)
			{
				throw new ArgumentNullException("ThirdPartyId", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ThirdPartyId));
		}

		public uint GetSerializedSize()
		{
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(ThirdPartyId);
			return 0 + (ProtocolParser.SizeOfUInt32(byteCount) + byteCount) + 1;
		}
	}
}
