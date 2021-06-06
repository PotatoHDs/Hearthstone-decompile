using System.IO;
using System.Text;

namespace PegasusUtil
{
	public class VirtualCurrencyCost : IProtoBuf
	{
		public bool HasCost;

		private long _Cost;

		public bool HasCurrencyCode;

		private string _CurrencyCode;

		public long Cost
		{
			get
			{
				return _Cost;
			}
			set
			{
				_Cost = value;
				HasCost = true;
			}
		}

		public string CurrencyCode
		{
			get
			{
				return _CurrencyCode;
			}
			set
			{
				_CurrencyCode = value;
				HasCurrencyCode = value != null;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasCost)
			{
				num ^= Cost.GetHashCode();
			}
			if (HasCurrencyCode)
			{
				num ^= CurrencyCode.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			VirtualCurrencyCost virtualCurrencyCost = obj as VirtualCurrencyCost;
			if (virtualCurrencyCost == null)
			{
				return false;
			}
			if (HasCost != virtualCurrencyCost.HasCost || (HasCost && !Cost.Equals(virtualCurrencyCost.Cost)))
			{
				return false;
			}
			if (HasCurrencyCode != virtualCurrencyCost.HasCurrencyCode || (HasCurrencyCode && !CurrencyCode.Equals(virtualCurrencyCost.CurrencyCode)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static VirtualCurrencyCost Deserialize(Stream stream, VirtualCurrencyCost instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static VirtualCurrencyCost DeserializeLengthDelimited(Stream stream)
		{
			VirtualCurrencyCost virtualCurrencyCost = new VirtualCurrencyCost();
			DeserializeLengthDelimited(stream, virtualCurrencyCost);
			return virtualCurrencyCost;
		}

		public static VirtualCurrencyCost DeserializeLengthDelimited(Stream stream, VirtualCurrencyCost instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static VirtualCurrencyCost Deserialize(Stream stream, VirtualCurrencyCost instance, long limit)
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
				case 18:
					instance.CurrencyCode = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, VirtualCurrencyCost instance)
		{
			if (instance.HasCost)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.Cost);
			}
			if (instance.HasCurrencyCode)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.CurrencyCode));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasCost)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)Cost);
			}
			if (HasCurrencyCode)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(CurrencyCode);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num;
		}
	}
}
