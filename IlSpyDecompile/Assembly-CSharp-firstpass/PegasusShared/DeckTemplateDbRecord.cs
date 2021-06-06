using System.IO;
using System.Text;

namespace PegasusShared
{
	public class DeckTemplateDbRecord : IProtoBuf
	{
		public bool HasEvent;

		private string _Event;

		public bool HasDisplayTexture;

		private string _DisplayTexture;

		public bool HasDeckRecord;

		private DeckDbRecord _DeckRecord;

		public bool HasIsFreeReward;

		private bool _IsFreeReward;

		public bool HasIsStarterDeck;

		private bool _IsStarterDeck;

		public bool HasFormatType;

		private FormatType _FormatType;

		public bool HasDisplayCardId;

		private int _DisplayCardId;

		public int Id { get; set; }

		public int ClassId { get; set; }

		public string Event
		{
			get
			{
				return _Event;
			}
			set
			{
				_Event = value;
				HasEvent = value != null;
			}
		}

		public int SortOrder { get; set; }

		public int DeckId { get; set; }

		public string DisplayTexture
		{
			get
			{
				return _DisplayTexture;
			}
			set
			{
				_DisplayTexture = value;
				HasDisplayTexture = value != null;
			}
		}

		public DeckDbRecord DeckRecord
		{
			get
			{
				return _DeckRecord;
			}
			set
			{
				_DeckRecord = value;
				HasDeckRecord = value != null;
			}
		}

		public bool IsFreeReward
		{
			get
			{
				return _IsFreeReward;
			}
			set
			{
				_IsFreeReward = value;
				HasIsFreeReward = true;
			}
		}

		public bool IsStarterDeck
		{
			get
			{
				return _IsStarterDeck;
			}
			set
			{
				_IsStarterDeck = value;
				HasIsStarterDeck = true;
			}
		}

		public FormatType FormatType
		{
			get
			{
				return _FormatType;
			}
			set
			{
				_FormatType = value;
				HasFormatType = true;
			}
		}

		public int DisplayCardId
		{
			get
			{
				return _DisplayCardId;
			}
			set
			{
				_DisplayCardId = value;
				HasDisplayCardId = true;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= Id.GetHashCode();
			hashCode ^= ClassId.GetHashCode();
			if (HasEvent)
			{
				hashCode ^= Event.GetHashCode();
			}
			hashCode ^= SortOrder.GetHashCode();
			hashCode ^= DeckId.GetHashCode();
			if (HasDisplayTexture)
			{
				hashCode ^= DisplayTexture.GetHashCode();
			}
			if (HasDeckRecord)
			{
				hashCode ^= DeckRecord.GetHashCode();
			}
			if (HasIsFreeReward)
			{
				hashCode ^= IsFreeReward.GetHashCode();
			}
			if (HasIsStarterDeck)
			{
				hashCode ^= IsStarterDeck.GetHashCode();
			}
			if (HasFormatType)
			{
				hashCode ^= FormatType.GetHashCode();
			}
			if (HasDisplayCardId)
			{
				hashCode ^= DisplayCardId.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			DeckTemplateDbRecord deckTemplateDbRecord = obj as DeckTemplateDbRecord;
			if (deckTemplateDbRecord == null)
			{
				return false;
			}
			if (!Id.Equals(deckTemplateDbRecord.Id))
			{
				return false;
			}
			if (!ClassId.Equals(deckTemplateDbRecord.ClassId))
			{
				return false;
			}
			if (HasEvent != deckTemplateDbRecord.HasEvent || (HasEvent && !Event.Equals(deckTemplateDbRecord.Event)))
			{
				return false;
			}
			if (!SortOrder.Equals(deckTemplateDbRecord.SortOrder))
			{
				return false;
			}
			if (!DeckId.Equals(deckTemplateDbRecord.DeckId))
			{
				return false;
			}
			if (HasDisplayTexture != deckTemplateDbRecord.HasDisplayTexture || (HasDisplayTexture && !DisplayTexture.Equals(deckTemplateDbRecord.DisplayTexture)))
			{
				return false;
			}
			if (HasDeckRecord != deckTemplateDbRecord.HasDeckRecord || (HasDeckRecord && !DeckRecord.Equals(deckTemplateDbRecord.DeckRecord)))
			{
				return false;
			}
			if (HasIsFreeReward != deckTemplateDbRecord.HasIsFreeReward || (HasIsFreeReward && !IsFreeReward.Equals(deckTemplateDbRecord.IsFreeReward)))
			{
				return false;
			}
			if (HasIsStarterDeck != deckTemplateDbRecord.HasIsStarterDeck || (HasIsStarterDeck && !IsStarterDeck.Equals(deckTemplateDbRecord.IsStarterDeck)))
			{
				return false;
			}
			if (HasFormatType != deckTemplateDbRecord.HasFormatType || (HasFormatType && !FormatType.Equals(deckTemplateDbRecord.FormatType)))
			{
				return false;
			}
			if (HasDisplayCardId != deckTemplateDbRecord.HasDisplayCardId || (HasDisplayCardId && !DisplayCardId.Equals(deckTemplateDbRecord.DisplayCardId)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static DeckTemplateDbRecord Deserialize(Stream stream, DeckTemplateDbRecord instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static DeckTemplateDbRecord DeserializeLengthDelimited(Stream stream)
		{
			DeckTemplateDbRecord deckTemplateDbRecord = new DeckTemplateDbRecord();
			DeserializeLengthDelimited(stream, deckTemplateDbRecord);
			return deckTemplateDbRecord;
		}

		public static DeckTemplateDbRecord DeserializeLengthDelimited(Stream stream, DeckTemplateDbRecord instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static DeckTemplateDbRecord Deserialize(Stream stream, DeckTemplateDbRecord instance, long limit)
		{
			instance.FormatType = FormatType.FT_UNKNOWN;
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
				case 16:
					instance.ClassId = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 26:
					instance.Event = ProtocolParser.ReadString(stream);
					continue;
				case 32:
					instance.SortOrder = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 40:
					instance.DeckId = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 50:
					instance.DisplayTexture = ProtocolParser.ReadString(stream);
					continue;
				case 58:
					if (instance.DeckRecord == null)
					{
						instance.DeckRecord = DeckDbRecord.DeserializeLengthDelimited(stream);
					}
					else
					{
						DeckDbRecord.DeserializeLengthDelimited(stream, instance.DeckRecord);
					}
					continue;
				case 64:
					instance.IsFreeReward = ProtocolParser.ReadBool(stream);
					continue;
				case 72:
					instance.IsStarterDeck = ProtocolParser.ReadBool(stream);
					continue;
				case 80:
					instance.FormatType = (FormatType)ProtocolParser.ReadUInt64(stream);
					continue;
				case 88:
					instance.DisplayCardId = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, DeckTemplateDbRecord instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Id);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.ClassId);
			if (instance.HasEvent)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Event));
			}
			stream.WriteByte(32);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.SortOrder);
			stream.WriteByte(40);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.DeckId);
			if (instance.HasDisplayTexture)
			{
				stream.WriteByte(50);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.DisplayTexture));
			}
			if (instance.HasDeckRecord)
			{
				stream.WriteByte(58);
				ProtocolParser.WriteUInt32(stream, instance.DeckRecord.GetSerializedSize());
				DeckDbRecord.Serialize(stream, instance.DeckRecord);
			}
			if (instance.HasIsFreeReward)
			{
				stream.WriteByte(64);
				ProtocolParser.WriteBool(stream, instance.IsFreeReward);
			}
			if (instance.HasIsStarterDeck)
			{
				stream.WriteByte(72);
				ProtocolParser.WriteBool(stream, instance.IsStarterDeck);
			}
			if (instance.HasFormatType)
			{
				stream.WriteByte(80);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.FormatType);
			}
			if (instance.HasDisplayCardId)
			{
				stream.WriteByte(88);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.DisplayCardId);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)Id);
			num += ProtocolParser.SizeOfUInt64((ulong)ClassId);
			if (HasEvent)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(Event);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			num += ProtocolParser.SizeOfUInt64((ulong)SortOrder);
			num += ProtocolParser.SizeOfUInt64((ulong)DeckId);
			if (HasDisplayTexture)
			{
				num++;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(DisplayTexture);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (HasDeckRecord)
			{
				num++;
				uint serializedSize = DeckRecord.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasIsFreeReward)
			{
				num++;
				num++;
			}
			if (HasIsStarterDeck)
			{
				num++;
				num++;
			}
			if (HasFormatType)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)FormatType);
			}
			if (HasDisplayCardId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)DisplayCardId);
			}
			return num + 4;
		}
	}
}
