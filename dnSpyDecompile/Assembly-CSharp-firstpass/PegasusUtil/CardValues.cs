using System;
using System.Collections.Generic;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x02000089 RID: 137
	public class CardValues : IProtoBuf
	{
		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000919 RID: 2329 RVA: 0x00021850 File Offset: 0x0001FA50
		// (set) Token: 0x0600091A RID: 2330 RVA: 0x00021858 File Offset: 0x0001FA58
		public List<CardValue> Cards
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

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x0600091B RID: 2331 RVA: 0x00021861 File Offset: 0x0001FA61
		// (set) Token: 0x0600091C RID: 2332 RVA: 0x00021869 File Offset: 0x0001FA69
		public int CardNerfIndex { get; set; }

		// Token: 0x0600091D RID: 2333 RVA: 0x00021874 File Offset: 0x0001FA74
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (CardValue cardValue in this.Cards)
			{
				num ^= cardValue.GetHashCode();
			}
			num ^= this.CardNerfIndex.GetHashCode();
			return num;
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x000218E8 File Offset: 0x0001FAE8
		public override bool Equals(object obj)
		{
			CardValues cardValues = obj as CardValues;
			if (cardValues == null)
			{
				return false;
			}
			if (this.Cards.Count != cardValues.Cards.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Cards.Count; i++)
			{
				if (!this.Cards[i].Equals(cardValues.Cards[i]))
				{
					return false;
				}
			}
			return this.CardNerfIndex.Equals(cardValues.CardNerfIndex);
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x0002196B File Offset: 0x0001FB6B
		public void Deserialize(Stream stream)
		{
			CardValues.Deserialize(stream, this);
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x00021975 File Offset: 0x0001FB75
		public static CardValues Deserialize(Stream stream, CardValues instance)
		{
			return CardValues.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x00021980 File Offset: 0x0001FB80
		public static CardValues DeserializeLengthDelimited(Stream stream)
		{
			CardValues cardValues = new CardValues();
			CardValues.DeserializeLengthDelimited(stream, cardValues);
			return cardValues;
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x0002199C File Offset: 0x0001FB9C
		public static CardValues DeserializeLengthDelimited(Stream stream, CardValues instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return CardValues.Deserialize(stream, instance, num);
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x000219C4 File Offset: 0x0001FBC4
		public static CardValues Deserialize(Stream stream, CardValues instance, long limit)
		{
			if (instance.Cards == null)
			{
				instance.Cards = new List<CardValue>();
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
					if (num != 16)
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
						instance.CardNerfIndex = (int)ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.Cards.Add(CardValue.DeserializeLengthDelimited(stream));
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x00021A75 File Offset: 0x0001FC75
		public void Serialize(Stream stream)
		{
			CardValues.Serialize(stream, this);
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x00021A80 File Offset: 0x0001FC80
		public static void Serialize(Stream stream, CardValues instance)
		{
			if (instance.Cards.Count > 0)
			{
				foreach (CardValue cardValue in instance.Cards)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, cardValue.GetSerializedSize());
					CardValue.Serialize(stream, cardValue);
				}
			}
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.CardNerfIndex));
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x00021B0C File Offset: 0x0001FD0C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.Cards.Count > 0)
			{
				foreach (CardValue cardValue in this.Cards)
				{
					num += 1U;
					uint serializedSize = cardValue.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.CardNerfIndex));
			num += 1U;
			return num;
		}

		// Token: 0x0400032A RID: 810
		private List<CardValue> _Cards = new List<CardValue>();

		// Token: 0x0200059B RID: 1435
		public enum PacketID
		{
			// Token: 0x04001F2A RID: 7978
			ID = 260
		}
	}
}
