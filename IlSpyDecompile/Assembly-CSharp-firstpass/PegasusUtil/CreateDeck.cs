using System;
using System.IO;
using System.Text;
using PegasusShared;

namespace PegasusUtil
{
	public class CreateDeck : IProtoBuf
	{
		public enum PacketID
		{
			ID = 209,
			System = 0
		}

		public bool HasTaggedStandard;

		private bool _TaggedStandard;

		public bool HasSourceType;

		private DeckSourceType _SourceType;

		public bool HasPastedDeckHash;

		private string _PastedDeckHash;

		public bool HasBrawlLibraryItemId;

		private int _BrawlLibraryItemId;

		public bool HasRequestId;

		private int _RequestId;

		public bool HasFormatType;

		private FormatType _FormatType;

		public bool HasFsgId;

		private long _FsgId;

		public bool HasFsgSharedSecretKey;

		private byte[] _FsgSharedSecretKey;

		public string Name { get; set; }

		public int Hero { get; set; }

		public int HeroPremium { get; set; }

		public DeckType DeckType { get; set; }

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

		public long SortOrder { get; set; }

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

		public int RequestId
		{
			get
			{
				return _RequestId;
			}
			set
			{
				_RequestId = value;
				HasRequestId = true;
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
			hashCode ^= Name.GetHashCode();
			hashCode ^= Hero.GetHashCode();
			hashCode ^= HeroPremium.GetHashCode();
			hashCode ^= DeckType.GetHashCode();
			if (HasTaggedStandard)
			{
				hashCode ^= TaggedStandard.GetHashCode();
			}
			hashCode ^= SortOrder.GetHashCode();
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
			if (HasRequestId)
			{
				hashCode ^= RequestId.GetHashCode();
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
			CreateDeck createDeck = obj as CreateDeck;
			if (createDeck == null)
			{
				return false;
			}
			if (!Name.Equals(createDeck.Name))
			{
				return false;
			}
			if (!Hero.Equals(createDeck.Hero))
			{
				return false;
			}
			if (!HeroPremium.Equals(createDeck.HeroPremium))
			{
				return false;
			}
			if (!DeckType.Equals(createDeck.DeckType))
			{
				return false;
			}
			if (HasTaggedStandard != createDeck.HasTaggedStandard || (HasTaggedStandard && !TaggedStandard.Equals(createDeck.TaggedStandard)))
			{
				return false;
			}
			if (!SortOrder.Equals(createDeck.SortOrder))
			{
				return false;
			}
			if (HasSourceType != createDeck.HasSourceType || (HasSourceType && !SourceType.Equals(createDeck.SourceType)))
			{
				return false;
			}
			if (HasPastedDeckHash != createDeck.HasPastedDeckHash || (HasPastedDeckHash && !PastedDeckHash.Equals(createDeck.PastedDeckHash)))
			{
				return false;
			}
			if (HasBrawlLibraryItemId != createDeck.HasBrawlLibraryItemId || (HasBrawlLibraryItemId && !BrawlLibraryItemId.Equals(createDeck.BrawlLibraryItemId)))
			{
				return false;
			}
			if (HasRequestId != createDeck.HasRequestId || (HasRequestId && !RequestId.Equals(createDeck.RequestId)))
			{
				return false;
			}
			if (HasFormatType != createDeck.HasFormatType || (HasFormatType && !FormatType.Equals(createDeck.FormatType)))
			{
				return false;
			}
			if (HasFsgId != createDeck.HasFsgId || (HasFsgId && !FsgId.Equals(createDeck.FsgId)))
			{
				return false;
			}
			if (HasFsgSharedSecretKey != createDeck.HasFsgSharedSecretKey || (HasFsgSharedSecretKey && !FsgSharedSecretKey.Equals(createDeck.FsgSharedSecretKey)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static CreateDeck Deserialize(Stream stream, CreateDeck instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static CreateDeck DeserializeLengthDelimited(Stream stream)
		{
			CreateDeck createDeck = new CreateDeck();
			DeserializeLengthDelimited(stream, createDeck);
			return createDeck;
		}

		public static CreateDeck DeserializeLengthDelimited(Stream stream, CreateDeck instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static CreateDeck Deserialize(Stream stream, CreateDeck instance, long limit)
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
				case 10:
					instance.Name = ProtocolParser.ReadString(stream);
					continue;
				case 16:
					instance.Hero = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.HeroPremium = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 32:
					instance.DeckType = (DeckType)ProtocolParser.ReadUInt64(stream);
					continue;
				case 40:
					instance.TaggedStandard = ProtocolParser.ReadBool(stream);
					continue;
				case 48:
					instance.SortOrder = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				case 56:
					instance.SourceType = (DeckSourceType)ProtocolParser.ReadUInt64(stream);
					continue;
				case 66:
					instance.PastedDeckHash = ProtocolParser.ReadString(stream);
					continue;
				case 72:
					instance.BrawlLibraryItemId = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 80:
					instance.RequestId = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 88:
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

		public static void Serialize(Stream stream, CreateDeck instance)
		{
			if (instance.Name == null)
			{
				throw new ArgumentNullException("Name", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Name));
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Hero);
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.HeroPremium);
			stream.WriteByte(32);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.DeckType);
			if (instance.HasTaggedStandard)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteBool(stream, instance.TaggedStandard);
			}
			stream.WriteByte(48);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.SortOrder);
			if (instance.HasSourceType)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.SourceType);
			}
			if (instance.HasPastedDeckHash)
			{
				stream.WriteByte(66);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.PastedDeckHash));
			}
			if (instance.HasBrawlLibraryItemId)
			{
				stream.WriteByte(72);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.BrawlLibraryItemId);
			}
			if (instance.HasRequestId)
			{
				stream.WriteByte(80);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.RequestId);
			}
			if (instance.HasFormatType)
			{
				stream.WriteByte(88);
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
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(Name);
			num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			num += ProtocolParser.SizeOfUInt64((ulong)Hero);
			num += ProtocolParser.SizeOfUInt64((ulong)HeroPremium);
			num += ProtocolParser.SizeOfUInt64((ulong)DeckType);
			if (HasTaggedStandard)
			{
				num++;
				num++;
			}
			num += ProtocolParser.SizeOfUInt64((ulong)SortOrder);
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
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)BrawlLibraryItemId);
			}
			if (HasRequestId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)RequestId);
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
			return num + 5;
		}
	}
}
