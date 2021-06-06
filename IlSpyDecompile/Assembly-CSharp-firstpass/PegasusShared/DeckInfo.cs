using System;
using System.IO;
using System.Text;

namespace PegasusShared
{
	public class DeckInfo : IProtoBuf
	{
		public enum ValidityFlags
		{
			UNLOCKED_HERO_CLASS = 1,
			OWNS_CARDS = 2,
			HAS_30_CARDS = 4,
			OBEYS_MAXES = 8,
			CLASS_MATCHES = 0x10,
			OWNS_CARD_BACK = 0x20,
			OWNS_HERO = 0x40,
			TAGGED_STANDARD = 0x80,
			NEEDS_VALIDATION = 0x100,
			NEEDS_NAME = 0x200,
			LOCKED_DECK = 0x400
		}

		public bool HasLastModified;

		private long _LastModified;

		public bool HasSeasonId;

		private int _SeasonId;

		public bool HasSortOrder;

		private long _SortOrder;

		public bool HasCreateDate;

		private long _CreateDate;

		public bool HasSourceType;

		private DeckSourceType _SourceType;

		public bool HasPastedDeckHash;

		private string _PastedDeckHash;

		public bool HasBrawlLibraryItemId;

		private int _BrawlLibraryItemId;

		public bool HasUiHeroOverride;

		private int _UiHeroOverride;

		public bool HasUiHeroOverridePremium;

		private int _UiHeroOverridePremium;

		public bool HasFormatType;

		private FormatType _FormatType;

		public long Id { get; set; }

		public string Name { get; set; }

		public int CardBack { get; set; }

		public int Hero { get; set; }

		public DeckType DeckType { get; set; }

		public ulong Validity { get; set; }

		public int HeroPremium { get; set; }

		public bool CardBackOverride { get; set; }

		public bool HeroOverride { get; set; }

		public long LastModified
		{
			get
			{
				return _LastModified;
			}
			set
			{
				_LastModified = value;
				HasLastModified = true;
			}
		}

		public int SeasonId
		{
			get
			{
				return _SeasonId;
			}
			set
			{
				_SeasonId = value;
				HasSeasonId = true;
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

		public long CreateDate
		{
			get
			{
				return _CreateDate;
			}
			set
			{
				_CreateDate = value;
				HasCreateDate = true;
			}
		}

		public DeckSourceType SourceType
		{
			get
			{
				return _SourceType;
			}
			set
			{
				_SourceType = value;
				HasSourceType = true;
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

		public int BrawlLibraryItemId
		{
			get
			{
				return _BrawlLibraryItemId;
			}
			set
			{
				_BrawlLibraryItemId = value;
				HasBrawlLibraryItemId = true;
			}
		}

		public int UiHeroOverride
		{
			get
			{
				return _UiHeroOverride;
			}
			set
			{
				_UiHeroOverride = value;
				HasUiHeroOverride = true;
			}
		}

		public int UiHeroOverridePremium
		{
			get
			{
				return _UiHeroOverridePremium;
			}
			set
			{
				_UiHeroOverridePremium = value;
				HasUiHeroOverridePremium = true;
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

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= Id.GetHashCode();
			hashCode ^= Name.GetHashCode();
			hashCode ^= CardBack.GetHashCode();
			hashCode ^= Hero.GetHashCode();
			hashCode ^= DeckType.GetHashCode();
			hashCode ^= Validity.GetHashCode();
			hashCode ^= HeroPremium.GetHashCode();
			hashCode ^= CardBackOverride.GetHashCode();
			hashCode ^= HeroOverride.GetHashCode();
			if (HasLastModified)
			{
				hashCode ^= LastModified.GetHashCode();
			}
			if (HasSeasonId)
			{
				hashCode ^= SeasonId.GetHashCode();
			}
			if (HasSortOrder)
			{
				hashCode ^= SortOrder.GetHashCode();
			}
			if (HasCreateDate)
			{
				hashCode ^= CreateDate.GetHashCode();
			}
			if (HasSourceType)
			{
				hashCode ^= SourceType.GetHashCode();
			}
			if (HasPastedDeckHash)
			{
				hashCode ^= PastedDeckHash.GetHashCode();
			}
			if (HasBrawlLibraryItemId)
			{
				hashCode ^= BrawlLibraryItemId.GetHashCode();
			}
			if (HasUiHeroOverride)
			{
				hashCode ^= UiHeroOverride.GetHashCode();
			}
			if (HasUiHeroOverridePremium)
			{
				hashCode ^= UiHeroOverridePremium.GetHashCode();
			}
			if (HasFormatType)
			{
				hashCode ^= FormatType.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			DeckInfo deckInfo = obj as DeckInfo;
			if (deckInfo == null)
			{
				return false;
			}
			if (!Id.Equals(deckInfo.Id))
			{
				return false;
			}
			if (!Name.Equals(deckInfo.Name))
			{
				return false;
			}
			if (!CardBack.Equals(deckInfo.CardBack))
			{
				return false;
			}
			if (!Hero.Equals(deckInfo.Hero))
			{
				return false;
			}
			if (!DeckType.Equals(deckInfo.DeckType))
			{
				return false;
			}
			if (!Validity.Equals(deckInfo.Validity))
			{
				return false;
			}
			if (!HeroPremium.Equals(deckInfo.HeroPremium))
			{
				return false;
			}
			if (!CardBackOverride.Equals(deckInfo.CardBackOverride))
			{
				return false;
			}
			if (!HeroOverride.Equals(deckInfo.HeroOverride))
			{
				return false;
			}
			if (HasLastModified != deckInfo.HasLastModified || (HasLastModified && !LastModified.Equals(deckInfo.LastModified)))
			{
				return false;
			}
			if (HasSeasonId != deckInfo.HasSeasonId || (HasSeasonId && !SeasonId.Equals(deckInfo.SeasonId)))
			{
				return false;
			}
			if (HasSortOrder != deckInfo.HasSortOrder || (HasSortOrder && !SortOrder.Equals(deckInfo.SortOrder)))
			{
				return false;
			}
			if (HasCreateDate != deckInfo.HasCreateDate || (HasCreateDate && !CreateDate.Equals(deckInfo.CreateDate)))
			{
				return false;
			}
			if (HasSourceType != deckInfo.HasSourceType || (HasSourceType && !SourceType.Equals(deckInfo.SourceType)))
			{
				return false;
			}
			if (HasPastedDeckHash != deckInfo.HasPastedDeckHash || (HasPastedDeckHash && !PastedDeckHash.Equals(deckInfo.PastedDeckHash)))
			{
				return false;
			}
			if (HasBrawlLibraryItemId != deckInfo.HasBrawlLibraryItemId || (HasBrawlLibraryItemId && !BrawlLibraryItemId.Equals(deckInfo.BrawlLibraryItemId)))
			{
				return false;
			}
			if (HasUiHeroOverride != deckInfo.HasUiHeroOverride || (HasUiHeroOverride && !UiHeroOverride.Equals(deckInfo.UiHeroOverride)))
			{
				return false;
			}
			if (HasUiHeroOverridePremium != deckInfo.HasUiHeroOverridePremium || (HasUiHeroOverridePremium && !UiHeroOverridePremium.Equals(deckInfo.UiHeroOverridePremium)))
			{
				return false;
			}
			if (HasFormatType != deckInfo.HasFormatType || (HasFormatType && !FormatType.Equals(deckInfo.FormatType)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static DeckInfo Deserialize(Stream stream, DeckInfo instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static DeckInfo DeserializeLengthDelimited(Stream stream)
		{
			DeckInfo deckInfo = new DeckInfo();
			DeserializeLengthDelimited(stream, deckInfo);
			return deckInfo;
		}

		public static DeckInfo DeserializeLengthDelimited(Stream stream, DeckInfo instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static DeckInfo Deserialize(Stream stream, DeckInfo instance, long limit)
		{
			instance.SourceType = DeckSourceType.DECK_SOURCE_TYPE_UNKNOWN;
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
					instance.Id = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				case 18:
					instance.Name = ProtocolParser.ReadString(stream);
					continue;
				case 24:
					instance.CardBack = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 32:
					instance.Hero = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 40:
					instance.DeckType = (DeckType)ProtocolParser.ReadUInt64(stream);
					continue;
				case 48:
					instance.Validity = ProtocolParser.ReadUInt64(stream);
					continue;
				case 56:
					instance.HeroPremium = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 64:
					instance.CardBackOverride = ProtocolParser.ReadBool(stream);
					continue;
				case 72:
					instance.HeroOverride = ProtocolParser.ReadBool(stream);
					continue;
				case 80:
					instance.LastModified = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				case 88:
					instance.SeasonId = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 96:
					instance.SortOrder = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				case 104:
					instance.CreateDate = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				case 112:
					instance.SourceType = (DeckSourceType)ProtocolParser.ReadUInt64(stream);
					continue;
				case 122:
					instance.PastedDeckHash = ProtocolParser.ReadString(stream);
					continue;
				default:
				{
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					switch (key.Field)
					{
					case 0u:
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					case 16u:
						if (key.WireType == Wire.Varint)
						{
							instance.BrawlLibraryItemId = (int)ProtocolParser.ReadUInt64(stream);
						}
						break;
					case 17u:
						if (key.WireType == Wire.Varint)
						{
							instance.UiHeroOverride = (int)ProtocolParser.ReadUInt64(stream);
						}
						break;
					case 18u:
						if (key.WireType == Wire.Varint)
						{
							instance.UiHeroOverridePremium = (int)ProtocolParser.ReadUInt64(stream);
						}
						break;
					case 19u:
						if (key.WireType == Wire.Varint)
						{
							instance.FormatType = (FormatType)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, DeckInfo instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Id);
			if (instance.Name == null)
			{
				throw new ArgumentNullException("Name", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Name));
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.CardBack);
			stream.WriteByte(32);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Hero);
			stream.WriteByte(40);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.DeckType);
			stream.WriteByte(48);
			ProtocolParser.WriteUInt64(stream, instance.Validity);
			stream.WriteByte(56);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.HeroPremium);
			stream.WriteByte(64);
			ProtocolParser.WriteBool(stream, instance.CardBackOverride);
			stream.WriteByte(72);
			ProtocolParser.WriteBool(stream, instance.HeroOverride);
			if (instance.HasLastModified)
			{
				stream.WriteByte(80);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.LastModified);
			}
			if (instance.HasSeasonId)
			{
				stream.WriteByte(88);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.SeasonId);
			}
			if (instance.HasSortOrder)
			{
				stream.WriteByte(96);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.SortOrder);
			}
			if (instance.HasCreateDate)
			{
				stream.WriteByte(104);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.CreateDate);
			}
			if (instance.HasSourceType)
			{
				stream.WriteByte(112);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.SourceType);
			}
			if (instance.HasPastedDeckHash)
			{
				stream.WriteByte(122);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.PastedDeckHash));
			}
			if (instance.HasBrawlLibraryItemId)
			{
				stream.WriteByte(128);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.BrawlLibraryItemId);
			}
			if (instance.HasUiHeroOverride)
			{
				stream.WriteByte(136);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.UiHeroOverride);
			}
			if (instance.HasUiHeroOverridePremium)
			{
				stream.WriteByte(144);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.UiHeroOverridePremium);
			}
			if (instance.HasFormatType)
			{
				stream.WriteByte(152);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.FormatType);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)Id);
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(Name);
			num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			num += ProtocolParser.SizeOfUInt64((ulong)CardBack);
			num += ProtocolParser.SizeOfUInt64((ulong)Hero);
			num += ProtocolParser.SizeOfUInt64((ulong)DeckType);
			num += ProtocolParser.SizeOfUInt64(Validity);
			num += ProtocolParser.SizeOfUInt64((ulong)HeroPremium);
			num++;
			num++;
			if (HasLastModified)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)LastModified);
			}
			if (HasSeasonId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)SeasonId);
			}
			if (HasSortOrder)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)SortOrder);
			}
			if (HasCreateDate)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)CreateDate);
			}
			if (HasSourceType)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)SourceType);
			}
			if (HasPastedDeckHash)
			{
				num++;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(PastedDeckHash);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (HasBrawlLibraryItemId)
			{
				num += 2;
				num += ProtocolParser.SizeOfUInt64((ulong)BrawlLibraryItemId);
			}
			if (HasUiHeroOverride)
			{
				num += 2;
				num += ProtocolParser.SizeOfUInt64((ulong)UiHeroOverride);
			}
			if (HasUiHeroOverridePremium)
			{
				num += 2;
				num += ProtocolParser.SizeOfUInt64((ulong)UiHeroOverridePremium);
			}
			if (HasFormatType)
			{
				num += 2;
				num += ProtocolParser.SizeOfUInt64((ulong)FormatType);
			}
			return num + 9;
		}
	}
}
