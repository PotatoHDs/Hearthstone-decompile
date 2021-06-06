using System;
using System.IO;
using System.Text;

namespace PegasusUtil
{
	public class ThirdPartyReceiptData : IProtoBuf
	{
		public bool HasThirdPartyUserId;

		private string _ThirdPartyUserId;

		public string ThirdPartyId { get; set; }

		public string Receipt { get; set; }

		public string ThirdPartyUserId
		{
			get
			{
				return _ThirdPartyUserId;
			}
			set
			{
				_ThirdPartyUserId = value;
				HasThirdPartyUserId = value != null;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= ThirdPartyId.GetHashCode();
			hashCode ^= Receipt.GetHashCode();
			if (HasThirdPartyUserId)
			{
				hashCode ^= ThirdPartyUserId.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			ThirdPartyReceiptData thirdPartyReceiptData = obj as ThirdPartyReceiptData;
			if (thirdPartyReceiptData == null)
			{
				return false;
			}
			if (!ThirdPartyId.Equals(thirdPartyReceiptData.ThirdPartyId))
			{
				return false;
			}
			if (!Receipt.Equals(thirdPartyReceiptData.Receipt))
			{
				return false;
			}
			if (HasThirdPartyUserId != thirdPartyReceiptData.HasThirdPartyUserId || (HasThirdPartyUserId && !ThirdPartyUserId.Equals(thirdPartyReceiptData.ThirdPartyUserId)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ThirdPartyReceiptData Deserialize(Stream stream, ThirdPartyReceiptData instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ThirdPartyReceiptData DeserializeLengthDelimited(Stream stream)
		{
			ThirdPartyReceiptData thirdPartyReceiptData = new ThirdPartyReceiptData();
			DeserializeLengthDelimited(stream, thirdPartyReceiptData);
			return thirdPartyReceiptData;
		}

		public static ThirdPartyReceiptData DeserializeLengthDelimited(Stream stream, ThirdPartyReceiptData instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ThirdPartyReceiptData Deserialize(Stream stream, ThirdPartyReceiptData instance, long limit)
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
				case 18:
					instance.Receipt = ProtocolParser.ReadString(stream);
					continue;
				case 26:
					instance.ThirdPartyUserId = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, ThirdPartyReceiptData instance)
		{
			if (instance.ThirdPartyId == null)
			{
				throw new ArgumentNullException("ThirdPartyId", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ThirdPartyId));
			if (instance.Receipt == null)
			{
				throw new ArgumentNullException("Receipt", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Receipt));
			if (instance.HasThirdPartyUserId)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ThirdPartyUserId));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(ThirdPartyId);
			num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(Receipt);
			num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			if (HasThirdPartyUserId)
			{
				num++;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(ThirdPartyUserId);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			return num + 2;
		}
	}
}
