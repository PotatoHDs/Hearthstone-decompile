using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	public class Balance : IProtoBuf
	{
		public bool HasName;

		private string _Name;

		public bool HasAmount;

		private double _Amount;

		public string Name
		{
			get
			{
				return _Name;
			}
			set
			{
				_Name = value;
				HasName = value != null;
			}
		}

		public double Amount
		{
			get
			{
				return _Amount;
			}
			set
			{
				_Amount = value;
				HasAmount = true;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasName)
			{
				num ^= Name.GetHashCode();
			}
			if (HasAmount)
			{
				num ^= Amount.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			Balance balance = obj as Balance;
			if (balance == null)
			{
				return false;
			}
			if (HasName != balance.HasName || (HasName && !Name.Equals(balance.Name)))
			{
				return false;
			}
			if (HasAmount != balance.HasAmount || (HasAmount && !Amount.Equals(balance.Amount)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static Balance Deserialize(Stream stream, Balance instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static Balance DeserializeLengthDelimited(Stream stream)
		{
			Balance balance = new Balance();
			DeserializeLengthDelimited(stream, balance);
			return balance;
		}

		public static Balance DeserializeLengthDelimited(Stream stream, Balance instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static Balance Deserialize(Stream stream, Balance instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
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
					instance.Name = ProtocolParser.ReadString(stream);
					continue;
				case 17:
					instance.Amount = binaryReader.ReadDouble();
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

		public static void Serialize(Stream stream, Balance instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasName)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Name));
			}
			if (instance.HasAmount)
			{
				stream.WriteByte(17);
				binaryWriter.Write(instance.Amount);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasName)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(Name);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasAmount)
			{
				num++;
				num += 8;
			}
			return num;
		}
	}
}
