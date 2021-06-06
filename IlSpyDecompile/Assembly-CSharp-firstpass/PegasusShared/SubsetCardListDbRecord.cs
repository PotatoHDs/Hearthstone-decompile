using System.Collections.Generic;
using System.IO;

namespace PegasusShared
{
	public class SubsetCardListDbRecord : IProtoBuf
	{
		private List<int> _CardIds = new List<int>();

		public int SubsetId { get; set; }

		public List<int> CardIds
		{
			get
			{
				return _CardIds;
			}
			set
			{
				_CardIds = value;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= SubsetId.GetHashCode();
			foreach (int cardId in CardIds)
			{
				hashCode ^= cardId.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			SubsetCardListDbRecord subsetCardListDbRecord = obj as SubsetCardListDbRecord;
			if (subsetCardListDbRecord == null)
			{
				return false;
			}
			if (!SubsetId.Equals(subsetCardListDbRecord.SubsetId))
			{
				return false;
			}
			if (CardIds.Count != subsetCardListDbRecord.CardIds.Count)
			{
				return false;
			}
			for (int i = 0; i < CardIds.Count; i++)
			{
				if (!CardIds[i].Equals(subsetCardListDbRecord.CardIds[i]))
				{
					return false;
				}
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static SubsetCardListDbRecord Deserialize(Stream stream, SubsetCardListDbRecord instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static SubsetCardListDbRecord DeserializeLengthDelimited(Stream stream)
		{
			SubsetCardListDbRecord subsetCardListDbRecord = new SubsetCardListDbRecord();
			DeserializeLengthDelimited(stream, subsetCardListDbRecord);
			return subsetCardListDbRecord;
		}

		public static SubsetCardListDbRecord DeserializeLengthDelimited(Stream stream, SubsetCardListDbRecord instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static SubsetCardListDbRecord Deserialize(Stream stream, SubsetCardListDbRecord instance, long limit)
		{
			if (instance.CardIds == null)
			{
				instance.CardIds = new List<int>();
			}
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
					instance.SubsetId = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.CardIds.Add((int)ProtocolParser.ReadUInt64(stream));
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

		public static void Serialize(Stream stream, SubsetCardListDbRecord instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.SubsetId);
			if (instance.CardIds.Count <= 0)
			{
				return;
			}
			foreach (int cardId in instance.CardIds)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)cardId);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)SubsetId);
			if (CardIds.Count > 0)
			{
				foreach (int cardId in CardIds)
				{
					num++;
					num += ProtocolParser.SizeOfUInt64((ulong)cardId);
				}
			}
			return num + 1;
		}
	}
}
