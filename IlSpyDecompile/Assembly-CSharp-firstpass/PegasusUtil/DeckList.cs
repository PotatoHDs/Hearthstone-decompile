using System.Collections.Generic;
using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	public class DeckList : IProtoBuf
	{
		public enum PacketID
		{
			ID = 202
		}

		private List<DeckInfo> _Decks = new List<DeckInfo>();

		public List<DeckInfo> Decks
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
			foreach (DeckInfo deck in Decks)
			{
				num ^= deck.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			DeckList deckList = obj as DeckList;
			if (deckList == null)
			{
				return false;
			}
			if (Decks.Count != deckList.Decks.Count)
			{
				return false;
			}
			for (int i = 0; i < Decks.Count; i++)
			{
				if (!Decks[i].Equals(deckList.Decks[i]))
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

		public static DeckList Deserialize(Stream stream, DeckList instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static DeckList DeserializeLengthDelimited(Stream stream)
		{
			DeckList deckList = new DeckList();
			DeserializeLengthDelimited(stream, deckList);
			return deckList;
		}

		public static DeckList DeserializeLengthDelimited(Stream stream, DeckList instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static DeckList Deserialize(Stream stream, DeckList instance, long limit)
		{
			if (instance.Decks == null)
			{
				instance.Decks = new List<DeckInfo>();
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
				case 10:
					instance.Decks.Add(DeckInfo.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, DeckList instance)
		{
			if (instance.Decks.Count <= 0)
			{
				return;
			}
			foreach (DeckInfo deck in instance.Decks)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, deck.GetSerializedSize());
				DeckInfo.Serialize(stream, deck);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (Decks.Count > 0)
			{
				foreach (DeckInfo deck in Decks)
				{
					num++;
					uint serializedSize = deck.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
				return num;
			}
			return num;
		}
	}
}
