using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PegasusShared
{
	public class DeckDbRecord : IProtoBuf
	{
		public bool HasTopCardId;

		private int _TopCardId;

		private List<DeckCardDbRecord> _DeckCard = new List<DeckCardDbRecord>();

		private List<LocalizedString> _Strings = new List<LocalizedString>();

		public int Id { get; set; }

		public string NoteName { get; set; }

		public int TopCardId
		{
			get
			{
				return _TopCardId;
			}
			set
			{
				_TopCardId = value;
				HasTopCardId = true;
			}
		}

		public List<DeckCardDbRecord> DeckCard
		{
			get
			{
				return _DeckCard;
			}
			set
			{
				_DeckCard = value;
			}
		}

		public List<LocalizedString> Strings
		{
			get
			{
				return _Strings;
			}
			set
			{
				_Strings = value;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= Id.GetHashCode();
			hashCode ^= NoteName.GetHashCode();
			if (HasTopCardId)
			{
				hashCode ^= TopCardId.GetHashCode();
			}
			foreach (DeckCardDbRecord item in DeckCard)
			{
				hashCode ^= item.GetHashCode();
			}
			foreach (LocalizedString @string in Strings)
			{
				hashCode ^= @string.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			DeckDbRecord deckDbRecord = obj as DeckDbRecord;
			if (deckDbRecord == null)
			{
				return false;
			}
			if (!Id.Equals(deckDbRecord.Id))
			{
				return false;
			}
			if (!NoteName.Equals(deckDbRecord.NoteName))
			{
				return false;
			}
			if (HasTopCardId != deckDbRecord.HasTopCardId || (HasTopCardId && !TopCardId.Equals(deckDbRecord.TopCardId)))
			{
				return false;
			}
			if (DeckCard.Count != deckDbRecord.DeckCard.Count)
			{
				return false;
			}
			for (int i = 0; i < DeckCard.Count; i++)
			{
				if (!DeckCard[i].Equals(deckDbRecord.DeckCard[i]))
				{
					return false;
				}
			}
			if (Strings.Count != deckDbRecord.Strings.Count)
			{
				return false;
			}
			for (int j = 0; j < Strings.Count; j++)
			{
				if (!Strings[j].Equals(deckDbRecord.Strings[j]))
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

		public static DeckDbRecord Deserialize(Stream stream, DeckDbRecord instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static DeckDbRecord DeserializeLengthDelimited(Stream stream)
		{
			DeckDbRecord deckDbRecord = new DeckDbRecord();
			DeserializeLengthDelimited(stream, deckDbRecord);
			return deckDbRecord;
		}

		public static DeckDbRecord DeserializeLengthDelimited(Stream stream, DeckDbRecord instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static DeckDbRecord Deserialize(Stream stream, DeckDbRecord instance, long limit)
		{
			if (instance.DeckCard == null)
			{
				instance.DeckCard = new List<DeckCardDbRecord>();
			}
			if (instance.Strings == null)
			{
				instance.Strings = new List<LocalizedString>();
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
					instance.Id = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 18:
					instance.NoteName = ProtocolParser.ReadString(stream);
					continue;
				case 24:
					instance.TopCardId = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 34:
					instance.DeckCard.Add(DeckCardDbRecord.DeserializeLengthDelimited(stream));
					continue;
				case 42:
					instance.Strings.Add(LocalizedString.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, DeckDbRecord instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Id);
			if (instance.NoteName == null)
			{
				throw new ArgumentNullException("NoteName", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.NoteName));
			if (instance.HasTopCardId)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.TopCardId);
			}
			if (instance.DeckCard.Count > 0)
			{
				foreach (DeckCardDbRecord item in instance.DeckCard)
				{
					stream.WriteByte(34);
					ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
					DeckCardDbRecord.Serialize(stream, item);
				}
			}
			if (instance.Strings.Count <= 0)
			{
				return;
			}
			foreach (LocalizedString @string in instance.Strings)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteUInt32(stream, @string.GetSerializedSize());
				LocalizedString.Serialize(stream, @string);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)Id);
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(NoteName);
			num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			if (HasTopCardId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)TopCardId);
			}
			if (DeckCard.Count > 0)
			{
				foreach (DeckCardDbRecord item in DeckCard)
				{
					num++;
					uint serializedSize = item.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (Strings.Count > 0)
			{
				foreach (LocalizedString @string in Strings)
				{
					num++;
					uint serializedSize2 = @string.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			return num + 2;
		}
	}
}
