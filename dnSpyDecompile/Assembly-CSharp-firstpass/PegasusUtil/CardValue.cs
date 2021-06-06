using System;
using System.IO;
using System.Text;
using PegasusShared;

namespace PegasusUtil
{
	// Token: 0x02000037 RID: 55
	public class CardValue : IProtoBuf
	{
		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060002E3 RID: 739 RVA: 0x0000C0C2 File Offset: 0x0000A2C2
		// (set) Token: 0x060002E4 RID: 740 RVA: 0x0000C0CA File Offset: 0x0000A2CA
		public CardDef Card { get; set; }

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060002E5 RID: 741 RVA: 0x0000C0D3 File Offset: 0x0000A2D3
		// (set) Token: 0x060002E6 RID: 742 RVA: 0x0000C0DB File Offset: 0x0000A2DB
		public int Buy { get; set; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060002E7 RID: 743 RVA: 0x0000C0E4 File Offset: 0x0000A2E4
		// (set) Token: 0x060002E8 RID: 744 RVA: 0x0000C0EC File Offset: 0x0000A2EC
		public int Sell { get; set; }

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x0000C0F5 File Offset: 0x0000A2F5
		// (set) Token: 0x060002EA RID: 746 RVA: 0x0000C0FD File Offset: 0x0000A2FD
		public bool Nerfed { get; set; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060002EB RID: 747 RVA: 0x0000C106 File Offset: 0x0000A306
		// (set) Token: 0x060002EC RID: 748 RVA: 0x0000C10E File Offset: 0x0000A30E
		public bool DeprecatedNerfed
		{
			get
			{
				return this._DeprecatedNerfed;
			}
			set
			{
				this._DeprecatedNerfed = value;
				this.HasDeprecatedNerfed = true;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060002ED RID: 749 RVA: 0x0000C11E File Offset: 0x0000A31E
		// (set) Token: 0x060002EE RID: 750 RVA: 0x0000C126 File Offset: 0x0000A326
		public int BuyValueOverride
		{
			get
			{
				return this._BuyValueOverride;
			}
			set
			{
				this._BuyValueOverride = value;
				this.HasBuyValueOverride = true;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060002EF RID: 751 RVA: 0x0000C136 File Offset: 0x0000A336
		// (set) Token: 0x060002F0 RID: 752 RVA: 0x0000C13E File Offset: 0x0000A33E
		public int SellValueOverride
		{
			get
			{
				return this._SellValueOverride;
			}
			set
			{
				this._SellValueOverride = value;
				this.HasSellValueOverride = true;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x0000C14E File Offset: 0x0000A34E
		// (set) Token: 0x060002F2 RID: 754 RVA: 0x0000C156 File Offset: 0x0000A356
		public string OverrideEventName
		{
			get
			{
				return this._OverrideEventName;
			}
			set
			{
				this._OverrideEventName = value;
				this.HasOverrideEventName = (value != null);
			}
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0000C16C File Offset: 0x0000A36C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Card.GetHashCode();
			num ^= this.Buy.GetHashCode();
			num ^= this.Sell.GetHashCode();
			num ^= this.Nerfed.GetHashCode();
			if (this.HasDeprecatedNerfed)
			{
				num ^= this.DeprecatedNerfed.GetHashCode();
			}
			if (this.HasBuyValueOverride)
			{
				num ^= this.BuyValueOverride.GetHashCode();
			}
			if (this.HasSellValueOverride)
			{
				num ^= this.SellValueOverride.GetHashCode();
			}
			if (this.HasOverrideEventName)
			{
				num ^= this.OverrideEventName.GetHashCode();
			}
			return num;
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0000C228 File Offset: 0x0000A428
		public override bool Equals(object obj)
		{
			CardValue cardValue = obj as CardValue;
			return cardValue != null && this.Card.Equals(cardValue.Card) && this.Buy.Equals(cardValue.Buy) && this.Sell.Equals(cardValue.Sell) && this.Nerfed.Equals(cardValue.Nerfed) && this.HasDeprecatedNerfed == cardValue.HasDeprecatedNerfed && (!this.HasDeprecatedNerfed || this.DeprecatedNerfed.Equals(cardValue.DeprecatedNerfed)) && this.HasBuyValueOverride == cardValue.HasBuyValueOverride && (!this.HasBuyValueOverride || this.BuyValueOverride.Equals(cardValue.BuyValueOverride)) && this.HasSellValueOverride == cardValue.HasSellValueOverride && (!this.HasSellValueOverride || this.SellValueOverride.Equals(cardValue.SellValueOverride)) && this.HasOverrideEventName == cardValue.HasOverrideEventName && (!this.HasOverrideEventName || this.OverrideEventName.Equals(cardValue.OverrideEventName));
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0000C354 File Offset: 0x0000A554
		public void Deserialize(Stream stream)
		{
			CardValue.Deserialize(stream, this);
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0000C35E File Offset: 0x0000A55E
		public static CardValue Deserialize(Stream stream, CardValue instance)
		{
			return CardValue.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0000C36C File Offset: 0x0000A56C
		public static CardValue DeserializeLengthDelimited(Stream stream)
		{
			CardValue cardValue = new CardValue();
			CardValue.DeserializeLengthDelimited(stream, cardValue);
			return cardValue;
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0000C388 File Offset: 0x0000A588
		public static CardValue DeserializeLengthDelimited(Stream stream, CardValue instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return CardValue.Deserialize(stream, instance, num);
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x0000C3B0 File Offset: 0x0000A5B0
		public static CardValue Deserialize(Stream stream, CardValue instance, long limit)
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
					if (num <= 32)
					{
						if (num <= 16)
						{
							if (num != 10)
							{
								if (num == 16)
								{
									instance.Buy = (int)ProtocolParser.ReadUInt64(stream);
									continue;
								}
							}
							else
							{
								if (instance.Card == null)
								{
									instance.Card = CardDef.DeserializeLengthDelimited(stream);
									continue;
								}
								CardDef.DeserializeLengthDelimited(stream, instance.Card);
								continue;
							}
						}
						else
						{
							if (num == 24)
							{
								instance.Sell = (int)ProtocolParser.ReadUInt64(stream);
								continue;
							}
							if (num == 32)
							{
								instance.Nerfed = ProtocolParser.ReadBool(stream);
								continue;
							}
						}
					}
					else if (num <= 48)
					{
						if (num == 40)
						{
							instance.DeprecatedNerfed = ProtocolParser.ReadBool(stream);
							continue;
						}
						if (num == 48)
						{
							instance.BuyValueOverride = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					else
					{
						if (num == 56)
						{
							instance.SellValueOverride = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 66)
						{
							instance.OverrideEventName = ProtocolParser.ReadString(stream);
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

		// Token: 0x060002FA RID: 762 RVA: 0x0000C526 File Offset: 0x0000A726
		public void Serialize(Stream stream)
		{
			CardValue.Serialize(stream, this);
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0000C530 File Offset: 0x0000A730
		public static void Serialize(Stream stream, CardValue instance)
		{
			if (instance.Card == null)
			{
				throw new ArgumentNullException("Card", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.Card.GetSerializedSize());
			CardDef.Serialize(stream, instance.Card);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Buy));
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Sell));
			stream.WriteByte(32);
			ProtocolParser.WriteBool(stream, instance.Nerfed);
			if (instance.HasDeprecatedNerfed)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteBool(stream, instance.DeprecatedNerfed);
			}
			if (instance.HasBuyValueOverride)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.BuyValueOverride));
			}
			if (instance.HasSellValueOverride)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.SellValueOverride));
			}
			if (instance.HasOverrideEventName)
			{
				stream.WriteByte(66);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.OverrideEventName));
			}
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0000C634 File Offset: 0x0000A834
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint serializedSize = this.Card.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Buy));
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Sell));
			num += 1U;
			if (this.HasDeprecatedNerfed)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasBuyValueOverride)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.BuyValueOverride));
			}
			if (this.HasSellValueOverride)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.SellValueOverride));
			}
			if (this.HasOverrideEventName)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.OverrideEventName);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num + 4U;
		}

		// Token: 0x040000ED RID: 237
		public bool HasDeprecatedNerfed;

		// Token: 0x040000EE RID: 238
		private bool _DeprecatedNerfed;

		// Token: 0x040000EF RID: 239
		public bool HasBuyValueOverride;

		// Token: 0x040000F0 RID: 240
		private int _BuyValueOverride;

		// Token: 0x040000F1 RID: 241
		public bool HasSellValueOverride;

		// Token: 0x040000F2 RID: 242
		private int _SellValueOverride;

		// Token: 0x040000F3 RID: 243
		public bool HasOverrideEventName;

		// Token: 0x040000F4 RID: 244
		private string _OverrideEventName;
	}
}
