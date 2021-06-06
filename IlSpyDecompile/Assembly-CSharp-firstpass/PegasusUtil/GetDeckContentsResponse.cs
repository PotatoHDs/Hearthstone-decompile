using System.Collections.Generic;
using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	public class GetDeckContentsResponse : IProtoBuf
	{
		public enum PacketID
		{
			ID = 215
		}

		public bool HasDeprecatedDeckId;

		private long _DeprecatedDeckId;

		private List<DeckCardData> _DeprecatedCards = new List<DeckCardData>();

		private List<DeckContents> _Decks = new List<DeckContents>();

		public long DeprecatedDeckId
		{
			get
			{
				return _DeprecatedDeckId;
			}
			set
			{
				_DeprecatedDeckId = value;
				HasDeprecatedDeckId = true;
			}
		}

		public List<DeckCardData> DeprecatedCards
		{
			get
			{
				return _DeprecatedCards;
			}
			set
			{
				_DeprecatedCards = value;
			}
		}

		public List<DeckContents> Decks
		{
			get
			{
				return _Decks;
			}
			set
			{
				_Decks = value;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasDeprecatedDeckId)
			{
				num ^= DeprecatedDeckId.GetHashCode();
			}
			foreach (DeckCardData deprecatedCard in DeprecatedCards)
			{
				num ^= deprecatedCard.GetHashCode();
			}
			foreach (DeckContents deck in Decks)
			{
				num ^= deck.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GetDeckContentsResponse getDeckContentsResponse = obj as GetDeckContentsResponse;
			if (getDeckContentsResponse == null)
			{
				return false;
			}
			if (HasDeprecatedDeckId != getDeckContentsResponse.HasDeprecatedDeckId || (HasDeprecatedDeckId && !DeprecatedDeckId.Equals(getDeckContentsResponse.DeprecatedDeckId)))
			{
				return false;
			}
			if (DeprecatedCards.Count != getDeckContentsResponse.DeprecatedCards.Count)
			{
				return false;
			}
			for (int i = 0; i < DeprecatedCards.Count; i++)
			{
				if (!DeprecatedCards[i].Equals(getDeckContentsResponse.DeprecatedCards[i]))
				{
					return false;
				}
			}
			if (Decks.Count != getDeckContentsResponse.Decks.Count)
			{
				return false;
			}
			for (int j = 0; j < Decks.Count; j++)
			{
				if (!Decks[j].Equals(getDeckContentsResponse.Decks[j]))
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

		public static GetDeckContentsResponse Deserialize(Stream stream, GetDeckContentsResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GetDeckContentsResponse DeserializeLengthDelimited(Stream stream)
		{
			GetDeckContentsResponse getDeckContentsResponse = new GetDeckContentsResponse();
			DeserializeLengthDelimited(stream, getDeckContentsResponse);
			return getDeckContentsResponse;
		}

		public static GetDeckContentsResponse DeserializeLengthDelimited(Stream stream, GetDeckContentsResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GetDeckContentsResponse Deserialize(Stream stream, GetDeckContentsResponse instance, long limit)
		{
			if (instance.DeprecatedCards == null)
			{
				instance.DeprecatedCards = new List<DeckCardData>();
			}
			if (instance.Decks == null)
			{
				instance.Decks = new List<DeckContents>();
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
					instance.DeprecatedDeckId = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				case 18:
					instance.DeprecatedCards.Add(DeckCardData.DeserializeLengthDelimited(stream));
					continue;
				case 26:
					instance.Decks.Add(DeckContents.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, GetDeckContentsResponse instance)
		{
			if (instance.HasDeprecatedDeckId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.DeprecatedDeckId);
			}
			if (instance.DeprecatedCards.Count > 0)
			{
				foreach (DeckCardData deprecatedCard in instance.DeprecatedCards)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, deprecatedCard.GetSerializedSize());
					DeckCardData.Serialize(stream, deprecatedCard);
				}
			}
			if (instance.Decks.Count <= 0)
			{
				return;
			}
			foreach (DeckContents deck in instance.Decks)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, deck.GetSerializedSize());
				DeckContents.Serialize(stream, deck);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasDeprecatedDeckId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)DeprecatedDeckId);
			}
			if (DeprecatedCards.Count > 0)
			{
				foreach (DeckCardData deprecatedCard in DeprecatedCards)
				{
					num++;
					uint serializedSize = deprecatedCard.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (Decks.Count > 0)
			{
				foreach (DeckContents deck in Decks)
				{
					num++;
					uint serializedSize2 = deck.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
				return num;
			}
			return num;
		}
	}
}
