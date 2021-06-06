using System;
using System.Collections.Generic;
using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	// Token: 0x020000EC RID: 236
	public class DraftRemovePremiumsResponse : IProtoBuf
	{
		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06000FDA RID: 4058 RVA: 0x00038B62 File Offset: 0x00036D62
		// (set) Token: 0x06000FDB RID: 4059 RVA: 0x00038B6A File Offset: 0x00036D6A
		public List<DeckCardData> Cards
		{
			get
			{
				return this._Cards;
			}
			set
			{
				this._Cards = value;
			}
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000FDC RID: 4060 RVA: 0x00038B73 File Offset: 0x00036D73
		// (set) Token: 0x06000FDD RID: 4061 RVA: 0x00038B7B File Offset: 0x00036D7B
		public List<CardDef> ChoiceList
		{
			get
			{
				return this._ChoiceList;
			}
			set
			{
				this._ChoiceList = value;
			}
		}

		// Token: 0x06000FDE RID: 4062 RVA: 0x00038B84 File Offset: 0x00036D84
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (DeckCardData deckCardData in this.Cards)
			{
				num ^= deckCardData.GetHashCode();
			}
			foreach (CardDef cardDef in this.ChoiceList)
			{
				num ^= cardDef.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000FDF RID: 4063 RVA: 0x00038C2C File Offset: 0x00036E2C
		public override bool Equals(object obj)
		{
			DraftRemovePremiumsResponse draftRemovePremiumsResponse = obj as DraftRemovePremiumsResponse;
			if (draftRemovePremiumsResponse == null)
			{
				return false;
			}
			if (this.Cards.Count != draftRemovePremiumsResponse.Cards.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Cards.Count; i++)
			{
				if (!this.Cards[i].Equals(draftRemovePremiumsResponse.Cards[i]))
				{
					return false;
				}
			}
			if (this.ChoiceList.Count != draftRemovePremiumsResponse.ChoiceList.Count)
			{
				return false;
			}
			for (int j = 0; j < this.ChoiceList.Count; j++)
			{
				if (!this.ChoiceList[j].Equals(draftRemovePremiumsResponse.ChoiceList[j]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000FE0 RID: 4064 RVA: 0x00038CE8 File Offset: 0x00036EE8
		public void Deserialize(Stream stream)
		{
			DraftRemovePremiumsResponse.Deserialize(stream, this);
		}

		// Token: 0x06000FE1 RID: 4065 RVA: 0x00038CF2 File Offset: 0x00036EF2
		public static DraftRemovePremiumsResponse Deserialize(Stream stream, DraftRemovePremiumsResponse instance)
		{
			return DraftRemovePremiumsResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000FE2 RID: 4066 RVA: 0x00038D00 File Offset: 0x00036F00
		public static DraftRemovePremiumsResponse DeserializeLengthDelimited(Stream stream)
		{
			DraftRemovePremiumsResponse draftRemovePremiumsResponse = new DraftRemovePremiumsResponse();
			DraftRemovePremiumsResponse.DeserializeLengthDelimited(stream, draftRemovePremiumsResponse);
			return draftRemovePremiumsResponse;
		}

		// Token: 0x06000FE3 RID: 4067 RVA: 0x00038D1C File Offset: 0x00036F1C
		public static DraftRemovePremiumsResponse DeserializeLengthDelimited(Stream stream, DraftRemovePremiumsResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return DraftRemovePremiumsResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06000FE4 RID: 4068 RVA: 0x00038D44 File Offset: 0x00036F44
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
			while (limit < 0L || stream.Position < limit)
			{
				int num = stream.ReadByte();
				if (num == -1)
				{
					if (limit >= 0L)
					{
						throw new EndOfStreamException();
					}
					return instance;
				}
				else if (num != 10)
				{
					if (num != 18)
					{
						Key key = ProtocolParser.ReadKey((byte)num, stream);
						if (key.Field == 0U)
						{
							throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
						}
						ProtocolParser.SkipKey(stream, key);
					}
					else
					{
						instance.ChoiceList.Add(CardDef.DeserializeLengthDelimited(stream));
					}
				}
				else
				{
					instance.Cards.Add(DeckCardData.DeserializeLengthDelimited(stream));
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06000FE5 RID: 4069 RVA: 0x00038E0C File Offset: 0x0003700C
		public void Serialize(Stream stream)
		{
			DraftRemovePremiumsResponse.Serialize(stream, this);
		}

		// Token: 0x06000FE6 RID: 4070 RVA: 0x00038E18 File Offset: 0x00037018
		public static void Serialize(Stream stream, DraftRemovePremiumsResponse instance)
		{
			if (instance.Cards.Count > 0)
			{
				foreach (DeckCardData deckCardData in instance.Cards)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, deckCardData.GetSerializedSize());
					DeckCardData.Serialize(stream, deckCardData);
				}
			}
			if (instance.ChoiceList.Count > 0)
			{
				foreach (CardDef cardDef in instance.ChoiceList)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, cardDef.GetSerializedSize());
					CardDef.Serialize(stream, cardDef);
				}
			}
		}

		// Token: 0x06000FE7 RID: 4071 RVA: 0x00038EF4 File Offset: 0x000370F4
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.Cards.Count > 0)
			{
				foreach (DeckCardData deckCardData in this.Cards)
				{
					num += 1U;
					uint serializedSize = deckCardData.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (this.ChoiceList.Count > 0)
			{
				foreach (CardDef cardDef in this.ChoiceList)
				{
					num += 1U;
					uint serializedSize2 = cardDef.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			return num;
		}

		// Token: 0x04000511 RID: 1297
		private List<DeckCardData> _Cards = new List<DeckCardData>();

		// Token: 0x04000512 RID: 1298
		private List<CardDef> _ChoiceList = new List<CardDef>();

		// Token: 0x020005F0 RID: 1520
		public enum PacketID
		{
			// Token: 0x04002013 RID: 8211
			ID = 355
		}
	}
}
