using System.IO;
using System.Text;

namespace PegasusUtil
{
	public class GoldCostBooster : IProtoBuf
	{
		public bool HasBuyWithGoldEventName;

		private string _BuyWithGoldEventName;

		public long Cost { get; set; }

		public int PackType { get; set; }

		public string BuyWithGoldEventName
		{
			get
			{
				return _BuyWithGoldEventName;
			}
			set
			{
				_BuyWithGoldEventName = value;
				HasBuyWithGoldEventName = value != null;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= Cost.GetHashCode();
			hashCode ^= PackType.GetHashCode();
			if (HasBuyWithGoldEventName)
			{
				hashCode ^= BuyWithGoldEventName.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			GoldCostBooster goldCostBooster = obj as GoldCostBooster;
			if (goldCostBooster == null)
			{
				return false;
			}
			if (!Cost.Equals(goldCostBooster.Cost))
			{
				return false;
			}
			if (!PackType.Equals(goldCostBooster.PackType))
			{
				return false;
			}
			if (HasBuyWithGoldEventName != goldCostBooster.HasBuyWithGoldEventName || (HasBuyWithGoldEventName && !BuyWithGoldEventName.Equals(goldCostBooster.BuyWithGoldEventName)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GoldCostBooster Deserialize(Stream stream, GoldCostBooster instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GoldCostBooster DeserializeLengthDelimited(Stream stream)
		{
			GoldCostBooster goldCostBooster = new GoldCostBooster();
			DeserializeLengthDelimited(stream, goldCostBooster);
			return goldCostBooster;
		}

		public static GoldCostBooster DeserializeLengthDelimited(Stream stream, GoldCostBooster instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GoldCostBooster Deserialize(Stream stream, GoldCostBooster instance, long limit)
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
					instance.Cost = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.PackType = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 26:
					instance.BuyWithGoldEventName = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, GoldCostBooster instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Cost);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.PackType);
			if (instance.HasBuyWithGoldEventName)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.BuyWithGoldEventName));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)Cost);
			num += ProtocolParser.SizeOfUInt64((ulong)PackType);
			if (HasBuyWithGoldEventName)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(BuyWithGoldEventName);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num + 2;
		}
	}
}
