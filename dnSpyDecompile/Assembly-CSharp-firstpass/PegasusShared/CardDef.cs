using System;
using System.IO;

namespace PegasusShared
{
	// Token: 0x0200011B RID: 283
	public class CardDef : IProtoBuf
	{
		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06001293 RID: 4755 RVA: 0x000413AB File Offset: 0x0003F5AB
		// (set) Token: 0x06001294 RID: 4756 RVA: 0x000413B3 File Offset: 0x0003F5B3
		public int Asset { get; set; }

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06001295 RID: 4757 RVA: 0x000413BC File Offset: 0x0003F5BC
		// (set) Token: 0x06001296 RID: 4758 RVA: 0x000413C4 File Offset: 0x0003F5C4
		public int Premium
		{
			get
			{
				return this._Premium;
			}
			set
			{
				this._Premium = value;
				this.HasPremium = true;
			}
		}

		// Token: 0x06001297 RID: 4759 RVA: 0x000413D4 File Offset: 0x0003F5D4
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Asset.GetHashCode();
			if (this.HasPremium)
			{
				num ^= this.Premium.GetHashCode();
			}
			return num;
		}

		// Token: 0x06001298 RID: 4760 RVA: 0x00041418 File Offset: 0x0003F618
		public override bool Equals(object obj)
		{
			CardDef cardDef = obj as CardDef;
			return cardDef != null && this.Asset.Equals(cardDef.Asset) && this.HasPremium == cardDef.HasPremium && (!this.HasPremium || this.Premium.Equals(cardDef.Premium));
		}

		// Token: 0x06001299 RID: 4761 RVA: 0x00041478 File Offset: 0x0003F678
		public void Deserialize(Stream stream)
		{
			CardDef.Deserialize(stream, this);
		}

		// Token: 0x0600129A RID: 4762 RVA: 0x00041482 File Offset: 0x0003F682
		public static CardDef Deserialize(Stream stream, CardDef instance)
		{
			return CardDef.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600129B RID: 4763 RVA: 0x00041490 File Offset: 0x0003F690
		public static CardDef DeserializeLengthDelimited(Stream stream)
		{
			CardDef cardDef = new CardDef();
			CardDef.DeserializeLengthDelimited(stream, cardDef);
			return cardDef;
		}

		// Token: 0x0600129C RID: 4764 RVA: 0x000414AC File Offset: 0x0003F6AC
		public static CardDef DeserializeLengthDelimited(Stream stream, CardDef instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return CardDef.Deserialize(stream, instance, num);
		}

		// Token: 0x0600129D RID: 4765 RVA: 0x000414D4 File Offset: 0x0003F6D4
		public static CardDef Deserialize(Stream stream, CardDef instance, long limit)
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
				else if (num != 8)
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
						instance.Premium = (int)ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.Asset = (int)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600129E RID: 4766 RVA: 0x0004156D File Offset: 0x0003F76D
		public void Serialize(Stream stream)
		{
			CardDef.Serialize(stream, this);
		}

		// Token: 0x0600129F RID: 4767 RVA: 0x00041576 File Offset: 0x0003F776
		public static void Serialize(Stream stream, CardDef instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Asset));
			if (instance.HasPremium)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Premium));
			}
		}

		// Token: 0x060012A0 RID: 4768 RVA: 0x000415AC File Offset: 0x0003F7AC
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Asset));
			if (this.HasPremium)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Premium));
			}
			return num + 1U;
		}

		// Token: 0x040005CE RID: 1486
		public bool HasPremium;

		// Token: 0x040005CF RID: 1487
		private int _Premium;
	}
}
