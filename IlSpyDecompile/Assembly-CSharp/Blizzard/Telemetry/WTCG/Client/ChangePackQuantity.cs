using System.IO;

namespace Blizzard.Telemetry.WTCG.Client
{
	public class ChangePackQuantity : IProtoBuf
	{
		public bool HasBoosterId;

		private int _BoosterId;

		public int BoosterId
		{
			get
			{
				return _BoosterId;
			}
			set
			{
				_BoosterId = value;
				HasBoosterId = true;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasBoosterId)
			{
				num ^= BoosterId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ChangePackQuantity changePackQuantity = obj as ChangePackQuantity;
			if (changePackQuantity == null)
			{
				return false;
			}
			if (HasBoosterId != changePackQuantity.HasBoosterId || (HasBoosterId && !BoosterId.Equals(changePackQuantity.BoosterId)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ChangePackQuantity Deserialize(Stream stream, ChangePackQuantity instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ChangePackQuantity DeserializeLengthDelimited(Stream stream)
		{
			ChangePackQuantity changePackQuantity = new ChangePackQuantity();
			DeserializeLengthDelimited(stream, changePackQuantity);
			return changePackQuantity;
		}

		public static ChangePackQuantity DeserializeLengthDelimited(Stream stream, ChangePackQuantity instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ChangePackQuantity Deserialize(Stream stream, ChangePackQuantity instance, long limit)
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
					instance.BoosterId = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, ChangePackQuantity instance)
		{
			if (instance.HasBoosterId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.BoosterId);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasBoosterId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)BoosterId);
			}
			return num;
		}
	}
}
