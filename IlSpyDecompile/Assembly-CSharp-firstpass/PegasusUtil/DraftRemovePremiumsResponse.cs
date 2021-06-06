using System.Collections.Generic;
using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	public class DraftRemovePremiumsResponse : IProtoBuf
	{
		public enum PacketID
		{
			ID = 355
		}

		private List<DeckCardData> _Cards = new List<DeckCardData>();

		private List<CardDef> _ChoiceList = new List<CardDef>();

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

		public List<CardDef> ChoiceList
		{
			get
			{
				return _ChoiceList;
			}
			set
			{
				_ChoiceList = value;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (DeckCardData card in Cards)
			{
				num ^= card.GetHashCode();
			}
			foreach (CardDef choice in ChoiceList)
			{
				num ^= choice.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			DraftRemovePremiumsResponse draftRemovePremiumsResponse = obj as DraftRemovePremiumsResponse;
			if (draftRemovePremiumsResponse == null)
			{
				return false;
			}
			if (Cards.Count != draftRemovePremiumsResponse.Cards.Count)
			{
				return false;
			}
			for (int i = 0; i < Cards.Count; i++)
			{
				if (!Cards[i].Equals(draftRemovePremiumsResponse.Cards[i]))
				{
					return false;
				}
			}
			if (ChoiceList.Count != draftRemovePremiumsResponse.ChoiceList.Count)
			{
				return false;
			}
			for (int j = 0; j < ChoiceList.Count; j++)
			{
				if (!ChoiceList[j].Equals(draftRemovePremiumsResponse.ChoiceList[j]))
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

		public static DraftRemovePremiumsResponse Deserialize(Stream stream, DraftRemovePremiumsResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static DraftRemovePremiumsResponse DeserializeLengthDelimited(Stream stream)
		{
			DraftRemovePremiumsResponse draftRemovePremiumsResponse = new DraftRemovePremiumsResponse();
			DeserializeLengthDelimited(stream, draftRemovePremiumsResponse);
			return draftRemovePremiumsResponse;
		}

		public static DraftRemovePremiumsResponse DeserializeLengthDelimited(Stream stream, DraftRemovePremiumsResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static DraftRemovePremiumsResponse Deserialize(Stream stream, DraftRemovePremiumsResponse instance, long limit)
		{
			if (instance.Cards == null)
			{
				instance.Cards = new List<DeckCardData>();
			}
			if (instance.ChoiceList == null)
			{
				instance.ChoiceList = new List<CardDef>();
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
					instance.Cards.Add(DeckCardData.DeserializeLengthDelimited(stream));
					continue;
				case 18:
					instance.ChoiceList.Add(CardDef.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, DraftRemovePremiumsResponse instance)
		{
			if (instance.Cards.Count > 0)
			{
				foreach (DeckCardData card in instance.Cards)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, card.GetSerializedSize());
					DeckCardData.Serialize(stream, card);
				}
			}
			if (instance.ChoiceList.Count <= 0)
			{
				return;
			}
			foreach (CardDef choice in instance.ChoiceList)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, choice.GetSerializedSize());
				CardDef.Serialize(stream, choice);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (Cards.Count > 0)
			{
				foreach (DeckCardData card in Cards)
				{
					num++;
					uint serializedSize = card.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (ChoiceList.Count > 0)
			{
				foreach (CardDef choice in ChoiceList)
				{
					num++;
					uint serializedSize2 = choice.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
				return num;
			}
			return num;
		}
	}
}
