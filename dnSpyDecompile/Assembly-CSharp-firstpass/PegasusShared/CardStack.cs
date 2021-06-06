using System;
using System.IO;

namespace PegasusShared
{
	// Token: 0x02000120 RID: 288
	public class CardStack : IProtoBuf
	{
		// Token: 0x17000390 RID: 912
		// (get) Token: 0x060012EA RID: 4842 RVA: 0x00042176 File Offset: 0x00040376
		// (set) Token: 0x060012EB RID: 4843 RVA: 0x0004217E File Offset: 0x0004037E
		public CardDef CardDef { get; set; }

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x060012EC RID: 4844 RVA: 0x00042187 File Offset: 0x00040387
		// (set) Token: 0x060012ED RID: 4845 RVA: 0x0004218F File Offset: 0x0004038F
		public Date LatestInsertDate { get; set; }

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x060012EE RID: 4846 RVA: 0x00042198 File Offset: 0x00040398
		// (set) Token: 0x060012EF RID: 4847 RVA: 0x000421A0 File Offset: 0x000403A0
		public int Count { get; set; }

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x060012F0 RID: 4848 RVA: 0x000421A9 File Offset: 0x000403A9
		// (set) Token: 0x060012F1 RID: 4849 RVA: 0x000421B1 File Offset: 0x000403B1
		public int NumSeen { get; set; }

		// Token: 0x060012F2 RID: 4850 RVA: 0x000421BC File Offset: 0x000403BC
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.CardDef.GetHashCode() ^ this.LatestInsertDate.GetHashCode() ^ this.Count.GetHashCode() ^ this.NumSeen.GetHashCode();
		}

		// Token: 0x060012F3 RID: 4851 RVA: 0x0004220C File Offset: 0x0004040C
		public override bool Equals(object obj)
		{
			CardStack cardStack = obj as CardStack;
			return cardStack != null && this.CardDef.Equals(cardStack.CardDef) && this.LatestInsertDate.Equals(cardStack.LatestInsertDate) && this.Count.Equals(cardStack.Count) && this.NumSeen.Equals(cardStack.NumSeen);
		}

		// Token: 0x060012F4 RID: 4852 RVA: 0x00042280 File Offset: 0x00040480
		public void Deserialize(Stream stream)
		{
			CardStack.Deserialize(stream, this);
		}

		// Token: 0x060012F5 RID: 4853 RVA: 0x0004228A File Offset: 0x0004048A
		public static CardStack Deserialize(Stream stream, CardStack instance)
		{
			return CardStack.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060012F6 RID: 4854 RVA: 0x00042298 File Offset: 0x00040498
		public static CardStack DeserializeLengthDelimited(Stream stream)
		{
			CardStack cardStack = new CardStack();
			CardStack.DeserializeLengthDelimited(stream, cardStack);
			return cardStack;
		}

		// Token: 0x060012F7 RID: 4855 RVA: 0x000422B4 File Offset: 0x000404B4
		public static CardStack DeserializeLengthDelimited(Stream stream, CardStack instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return CardStack.Deserialize(stream, instance, num);
		}

		// Token: 0x060012F8 RID: 4856 RVA: 0x000422DC File Offset: 0x000404DC
		public static CardStack Deserialize(Stream stream, CardStack instance, long limit)
		{
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
				else
				{
					if (num <= 18)
					{
						if (num != 10)
						{
							if (num == 18)
							{
								if (instance.LatestInsertDate == null)
								{
									instance.LatestInsertDate = Date.DeserializeLengthDelimited(stream);
									continue;
								}
								Date.DeserializeLengthDelimited(stream, instance.LatestInsertDate);
								continue;
							}
						}
						else
						{
							if (instance.CardDef == null)
							{
								instance.CardDef = CardDef.DeserializeLengthDelimited(stream);
								continue;
							}
							CardDef.DeserializeLengthDelimited(stream, instance.CardDef);
							continue;
						}
					}
					else
					{
						if (num == 24)
						{
							instance.Count = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 32)
						{
							instance.NumSeen = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					if (key.Field == 0U)
					{
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					}
					ProtocolParser.SkipKey(stream, key);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060012F9 RID: 4857 RVA: 0x000423E9 File Offset: 0x000405E9
		public void Serialize(Stream stream)
		{
			CardStack.Serialize(stream, this);
		}

		// Token: 0x060012FA RID: 4858 RVA: 0x000423F4 File Offset: 0x000405F4
		public static void Serialize(Stream stream, CardStack instance)
		{
			if (instance.CardDef == null)
			{
				throw new ArgumentNullException("CardDef", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.CardDef.GetSerializedSize());
			CardDef.Serialize(stream, instance.CardDef);
			if (instance.LatestInsertDate == null)
			{
				throw new ArgumentNullException("LatestInsertDate", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteUInt32(stream, instance.LatestInsertDate.GetSerializedSize());
			Date.Serialize(stream, instance.LatestInsertDate);
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Count));
			stream.WriteByte(32);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.NumSeen));
		}

		// Token: 0x060012FB RID: 4859 RVA: 0x000424A8 File Offset: 0x000406A8
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint serializedSize = this.CardDef.GetSerializedSize();
			uint num2 = num + (serializedSize + ProtocolParser.SizeOfUInt32(serializedSize));
			uint serializedSize2 = this.LatestInsertDate.GetSerializedSize();
			return num2 + (serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2)) + ProtocolParser.SizeOfUInt64((ulong)((long)this.Count)) + ProtocolParser.SizeOfUInt64((ulong)((long)this.NumSeen)) + 4U;
		}
	}
}
