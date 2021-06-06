using System;
using System.IO;
using System.Text;

namespace PegasusUtil
{
	public class ThirdPartyPurchaseStatusResponse : IProtoBuf
	{
		public enum PacketID
		{
			ID = 295
		}

		public enum Status
		{
			NOT_FOUND = 1,
			SUCCEEDED,
			FAILED,
			IN_PROGRESS
		}

		public string ThirdPartyId { get; set; }

		public Status Status_ { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ ThirdPartyId.GetHashCode() ^ Status_.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			ThirdPartyPurchaseStatusResponse thirdPartyPurchaseStatusResponse = obj as ThirdPartyPurchaseStatusResponse;
			if (thirdPartyPurchaseStatusResponse == null)
			{
				return false;
			}
			if (!ThirdPartyId.Equals(thirdPartyPurchaseStatusResponse.ThirdPartyId))
			{
				return false;
			}
			if (!Status_.Equals(thirdPartyPurchaseStatusResponse.Status_))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ThirdPartyPurchaseStatusResponse Deserialize(Stream stream, ThirdPartyPurchaseStatusResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ThirdPartyPurchaseStatusResponse DeserializeLengthDelimited(Stream stream)
		{
			ThirdPartyPurchaseStatusResponse thirdPartyPurchaseStatusResponse = new ThirdPartyPurchaseStatusResponse();
			DeserializeLengthDelimited(stream, thirdPartyPurchaseStatusResponse);
			return thirdPartyPurchaseStatusResponse;
		}

		public static ThirdPartyPurchaseStatusResponse DeserializeLengthDelimited(Stream stream, ThirdPartyPurchaseStatusResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ThirdPartyPurchaseStatusResponse Deserialize(Stream stream, ThirdPartyPurchaseStatusResponse instance, long limit)
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
				case 16:
					instance.Status_ = (Status)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, ThirdPartyPurchaseStatusResponse instance)
		{
			if (instance.ThirdPartyId == null)
			{
				throw new ArgumentNullException("ThirdPartyId", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ThirdPartyId));
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Status_);
		}

		public uint GetSerializedSize()
		{
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(ThirdPartyId);
			return 0 + (ProtocolParser.SizeOfUInt32(byteCount) + byteCount) + ProtocolParser.SizeOfUInt64((ulong)Status_) + 2;
		}
	}
}
