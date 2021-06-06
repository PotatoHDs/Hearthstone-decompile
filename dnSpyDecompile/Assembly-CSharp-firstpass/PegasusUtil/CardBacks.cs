using System;
using System.Collections.Generic;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x0200005D RID: 93
	public class CardBacks : IProtoBuf
	{
		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060005E7 RID: 1511 RVA: 0x000176D3 File Offset: 0x000158D3
		// (set) Token: 0x060005E8 RID: 1512 RVA: 0x000176DB File Offset: 0x000158DB
		public int FavoriteCardBack { get; set; }

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060005E9 RID: 1513 RVA: 0x000176E4 File Offset: 0x000158E4
		// (set) Token: 0x060005EA RID: 1514 RVA: 0x000176EC File Offset: 0x000158EC
		public List<int> CardBacks_
		{
			get
			{
				return this._CardBacks_;
			}
			set
			{
				this._CardBacks_ = value;
			}
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x000176F8 File Offset: 0x000158F8
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.FavoriteCardBack.GetHashCode();
			foreach (int num2 in this.CardBacks_)
			{
				num ^= num2.GetHashCode();
			}
			return num;
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x0001776C File Offset: 0x0001596C
		public override bool Equals(object obj)
		{
			CardBacks cardBacks = obj as CardBacks;
			if (cardBacks == null)
			{
				return false;
			}
			if (!this.FavoriteCardBack.Equals(cardBacks.FavoriteCardBack))
			{
				return false;
			}
			if (this.CardBacks_.Count != cardBacks.CardBacks_.Count)
			{
				return false;
			}
			for (int i = 0; i < this.CardBacks_.Count; i++)
			{
				if (!this.CardBacks_[i].Equals(cardBacks.CardBacks_[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x000177F2 File Offset: 0x000159F2
		public void Deserialize(Stream stream)
		{
			CardBacks.Deserialize(stream, this);
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x000177FC File Offset: 0x000159FC
		public static CardBacks Deserialize(Stream stream, CardBacks instance)
		{
			return CardBacks.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x00017808 File Offset: 0x00015A08
		public static CardBacks DeserializeLengthDelimited(Stream stream)
		{
			CardBacks cardBacks = new CardBacks();
			CardBacks.DeserializeLengthDelimited(stream, cardBacks);
			return cardBacks;
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x00017824 File Offset: 0x00015A24
		public static CardBacks DeserializeLengthDelimited(Stream stream, CardBacks instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return CardBacks.Deserialize(stream, instance, num);
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x0001784C File Offset: 0x00015A4C
		public static CardBacks Deserialize(Stream stream, CardBacks instance, long limit)
		{
			if (instance.CardBacks_ == null)
			{
				instance.CardBacks_ = new List<int>();
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
						instance.CardBacks_.Add((int)ProtocolParser.ReadUInt64(stream));
					}
				}
				else
				{
					instance.FavoriteCardBack = (int)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x000178FD File Offset: 0x00015AFD
		public void Serialize(Stream stream)
		{
			CardBacks.Serialize(stream, this);
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x00017908 File Offset: 0x00015B08
		public static void Serialize(Stream stream, CardBacks instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.FavoriteCardBack));
			if (instance.CardBacks_.Count > 0)
			{
				foreach (int num in instance.CardBacks_)
				{
					stream.WriteByte(16);
					ProtocolParser.WriteUInt64(stream, (ulong)((long)num));
				}
			}
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x00017988 File Offset: 0x00015B88
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.FavoriteCardBack));
			if (this.CardBacks_.Count > 0)
			{
				foreach (int num2 in this.CardBacks_)
				{
					num += 1U;
					num += ProtocolParser.SizeOfUInt64((ulong)((long)num2));
				}
			}
			num += 1U;
			return num;
		}

		// Token: 0x04000211 RID: 529
		private List<int> _CardBacks_ = new List<int>();

		// Token: 0x0200056F RID: 1391
		public enum PacketID
		{
			// Token: 0x04001EA3 RID: 7843
			ID = 236,
			// Token: 0x04001EA4 RID: 7844
			System = 0
		}
	}
}
