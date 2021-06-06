using System.Collections.Generic;
using System.IO;
using System.Text;
using PegasusShared;

namespace PegasusUtil
{
	public class DeckSetData : IProtoBuf
	{
		public enum PacketID
		{
			ID = 222,
			System = 0
		}

		private List<DeckCardData> _Cards = new List<DeckCardData>();

		public bool HasHero;

		private CardDef _Hero;

		public bool HasCardBack;

		private int _CardBack;

		public bool HasTaggedStandard;

		private bool _TaggedStandard;

		public bool HasSortOrder;

		private long _SortOrder;

		public bool HasPastedDeckHash;

		private string _PastedDeckHash;

		public bool HasUiHeroOverride;

		private CardDef _UiHeroOverride;

		public bool HasFormatType;

		private FormatType _FormatType;

		public bool HasFsgId;

		private long _FsgId;

		public bool HasFsgSharedSecretKey;

		private byte[] _FsgSharedSecretKey;

		public long Deck { get; set; }

		public List<DeckCardData> Cards
		{
			get
			{
				return _Cards;
			}
			set
			{
				_Cards = value;
			}
		}

		public CardDef Hero
		{
			get
			{
				return _Hero;
			}
			set
			{
				_Hero = value;
				HasHero = value != null;
			}
		}

		public int CardBack
		{
			get
			{
				return _CardBack;
			}
			set
			{
				_CardBack = value;
				HasCardBack = true;
			}
		}

		public bool TaggedStandard
		{
			get
			{
				return _TaggedStandard;
			}
			set
			{
				_TaggedStandard = value;
				HasTaggedStandard = true;
			}
		}

		public long SortOrder
		{
			get
			{
				return _SortOrder;
			}
			set
			{
				_SortOrder = value;
				HasSortOrder = true;
			}
		}

		public string PastedDeckHash
		{
			get
			{
				return _PastedDeckHash;
			}
			set
			{
				_PastedDeckHash = value;
				HasPastedDeckHash = value != null;
			}
		}

		public CardDef UiHeroOverride
		{
			get
			{
				return _UiHeroOverride;
			}
			set
			{
				_UiHeroOverride = value;
				HasUiHeroOverride = value != null;
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

		public long FsgId
		{
			get
			{
				return _FsgId;
			}
			set
			{
				_FsgId = value;
				HasFsgId = true;
			}
		}

		public byte[] FsgSharedSecretKey
		{
			get
			{
				return _FsgSharedSecretKey;
			}
			set
			{
				_FsgSharedSecretKey = value;
				HasFsgSharedSecretKey = value != null;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= Deck.GetHashCode();
			foreach (DeckCardData card in Cards)
			{
				hashCode ^= card.GetHashCode();
			}
			if (HasHero)
			{
				hashCode ^= Hero.GetHashCode();
			}
			if (HasCardBack)
			{
				hashCode ^= CardBack.GetHashCode();
			}
			if (HasTaggedStandard)
			{
				hashCode ^= TaggedStandard.GetHashCode();
			}
			if (HasSortOrder)
			{
				hashCode ^= SortOrder.GetHashCode();
			}
			if (HasPastedDeckHash)
			{
				hashCode ^= PastedDeckHash.GetHashCode();
			}
			if (HasUiHeroOverride)
			{
				hashCode ^= UiHeroOverride.GetHashCode();
			}
			if (HasFormatType)
			{
				hashCode ^= FormatType.GetHashCode();
			}
			if (HasFsgId)
			{
				hashCode ^= FsgId.GetHashCode();
			}
			if (HasFsgSharedSecretKey)
			{
				hashCode ^= FsgSharedSecretKey.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			DeckSetData deckSetData = obj as DeckSetData;
			if (deckSetData == null)
			{
				return false;
			}
			if (!Deck.Equals(deckSetData.Deck))
			{
				return false;
			}
			if (Cards.Count != deckSetData.Cards.Count)
			{
				return false;
			}
			for (int i = 0; i < Cards.Count; i++)
			{
				if (!Cards[i].Equals(deckSetData.Cards[i]))
				{
					return false;
				}
			}
			if (HasHero != deckSetData.HasHero || (HasHero && !Hero.Equals(deckSetData.Hero)))
			{
				return false;
			}
			if (HasCardBack != deckSetData.HasCardBack || (HasCardBack && !CardBack.Equals(deckSetData.CardBack)))
			{
				return false;
			}
			if (HasTaggedStandard != deckSetData.HasTaggedStandard || (HasTaggedStandard && !TaggedStandard.Equals(deckSetData.TaggedStandard)))
			{
				return false;
			}
			if (HasSortOrder != deckSetData.HasSortOrder || (HasSortOrder && !SortOrder.Equals(deckSetData.SortOrder)))
			{
				return false;
			}
			if (HasPastedDeckHash != deckSetData.HasPastedDeckHash || (HasPastedDeckHash && !PastedDeckHash.Equals(deckSetData.PastedDeckHash)))
			{
				return false;
			}
			if (HasUiHeroOverride != deckSetData.HasUiHeroOverride || (HasUiHeroOverride && !UiHeroOverride.Equals(deckSetData.UiHeroOverride)))
			{
				return false;
			}
			if (HasFormatType != deckSetData.HasFormatType || (HasFormatType && !FormatType.Equals(deckSetData.FormatType)))
			{
				return false;
			}
			if (HasFsgId != deckSetData.HasFsgId || (HasFsgId && !FsgId.Equals(deckSetData.FsgId)))
			{
				return false;
			}
			if (HasFsgSharedSecretKey != deckSetData.HasFsgSharedSecretKey || (HasFsgSharedSecretKey && !FsgSharedSecretKey.Equals(deckSetData.FsgSharedSecretKey)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static DeckSetData Deserialize(Stream stream, DeckSetData instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static DeckSetData DeserializeLengthDelimited(Stream stream)
		{
			DeckSetData deckSetData = new DeckSetData();
			DeserializeLengthDelimited(stream, deckSetData);
			return deckSetData;
		}

		public static DeckSetData DeserializeLengthDelimited(Stream stream, DeckSetData instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static DeckSetData Deserialize(Stream stream, DeckSetData instance, long limit)
		{
			if (instance.Cards == null)
			{
				instance.Cards = new List<DeckCardData>();
			}
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
					instance.Deck = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				case 18:
					instance.Cards.Add(DeckCardData.DeserializeLengthDelimited(stream));
					continue;
				case 26:
					if (instance.Hero == null)
					{
						instance.Hero = CardDef.DeserializeLengthDelimited(stream);
					}
					else
					{
						CardDef.DeserializeLengthDelimited(stream, instance.Hero);
					}
					continue;
				case 32:
					instance.CardBack = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 40:
					instance.TaggedStandard = ProtocolParser.ReadBool(stream);
					continue;
				case 48:
					instance.SortOrder = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				case 58:
					instance.PastedDeckHash = ProtocolParser.ReadString(stream);
					continue;
				case 66:
					if (instance.UiHeroOverride == null)
					{
						instance.UiHeroOverride = CardDef.DeserializeLengthDelimited(stream);
					}
					else
					{
						CardDef.DeserializeLengthDelimited(stream, instance.UiHeroOverride);
					}
					continue;
				case 72:
					instance.FormatType = (FormatType)ProtocolParser.ReadUInt64(stream);
					continue;
				default:
				{
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					switch (key.Field)
					{
					case 0u:
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					case 100u:
						if (key.WireType == Wire.Varint)
						{
							instance.FsgId = (long)ProtocolParser.ReadUInt64(stream);
						}
						break;
					case 101u:
						if (key.WireType == Wire.LengthDelimited)
						{
							instance.FsgSharedSecretKey = ProtocolParser.ReadBytes(stream);
						}
						break;
					default:
						ProtocolParser.SkipKey(stream, key);
						break;
					}
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

		public static void Serialize(Stream stream, DeckSetData instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Deck);
			if (instance.Cards.Count > 0)
			{
				foreach (DeckCardData card in instance.Cards)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, card.GetSerializedSize());
					DeckCardData.Serialize(stream, card);
				}
			}
			if (instance.HasHero)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.Hero.GetSerializedSize());
				CardDef.Serialize(stream, instance.Hero);
			}
			if (instance.HasCardBack)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.CardBack);
			}
			if (instance.HasTaggedStandard)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteBool(stream, instance.TaggedStandard);
			}
			if (instance.HasSortOrder)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.SortOrder);
			}
			if (instance.HasPastedDeckHash)
			{
				stream.WriteByte(58);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.PastedDeckHash));
			}
			if (instance.HasUiHeroOverride)
			{
				stream.WriteByte(66);
				ProtocolParser.WriteUInt32(stream, instance.UiHeroOverride.GetSerializedSize());
				CardDef.Serialize(stream, instance.UiHeroOverride);
			}
			if (instance.HasFormatType)
			{
				stream.WriteByte(72);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.FormatType);
			}
			if (instance.HasFsgId)
			{
				stream.WriteByte(160);
				stream.WriteByte(6);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.FsgId);
			}
			if (instance.HasFsgSharedSecretKey)
			{
				stream.WriteByte(170);
				stream.WriteByte(6);
				ProtocolParser.WriteBytes(stream, instance.FsgSharedSecretKey);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)Deck);
			if (Cards.Count > 0)
			{
				foreach (DeckCardData card in Cards)
				{
					num++;
					uint serializedSize = card.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (HasHero)
			{
				num++;
				uint serializedSize2 = Hero.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (HasCardBack)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)CardBack);
			}
			if (HasTaggedStandard)
			{
				num++;
				num++;
			}
			if (HasSortOrder)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)SortOrder);
			}
			if (HasPastedDeckHash)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(PastedDeckHash);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasUiHeroOverride)
			{
				num++;
				uint serializedSize3 = UiHeroOverride.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			if (HasFormatType)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)FormatType);
			}
			if (HasFsgId)
			{
				num += 2;
				num += ProtocolParser.SizeOfUInt64((ulong)FsgId);
			}
			if (HasFsgSharedSecretKey)
			{
				num += 2;
				num += (uint)((int)ProtocolParser.SizeOfUInt32(FsgSharedSecretKey.Length) + FsgSharedSecretKey.Length);
			}
			return num + 1;
		}
	}
}
