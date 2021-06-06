using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x02000104 RID: 260
	public class SetFavoriteCoin : IProtoBuf
	{
		// Token: 0x1700033F RID: 831
		// (get) Token: 0x0600112A RID: 4394 RVA: 0x0003C66F File Offset: 0x0003A86F
		// (set) Token: 0x0600112B RID: 4395 RVA: 0x0003C677 File Offset: 0x0003A877
		public int Coin
		{
			get
			{
				return this._Coin;
			}
			set
			{
				this._Coin = value;
				this.HasCoin = true;
			}
		}

		// Token: 0x0600112C RID: 4396 RVA: 0x0003C688 File Offset: 0x0003A888
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasCoin)
			{
				num ^= this.Coin.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600112D RID: 4397 RVA: 0x0003C6BC File Offset: 0x0003A8BC
		public override bool Equals(object obj)
		{
			SetFavoriteCoin setFavoriteCoin = obj as SetFavoriteCoin;
			return setFavoriteCoin != null && this.HasCoin == setFavoriteCoin.HasCoin && (!this.HasCoin || this.Coin.Equals(setFavoriteCoin.Coin));
		}

		// Token: 0x0600112E RID: 4398 RVA: 0x0003C704 File Offset: 0x0003A904
		public void Deserialize(Stream stream)
		{
			SetFavoriteCoin.Deserialize(stream, this);
		}

		// Token: 0x0600112F RID: 4399 RVA: 0x0003C70E File Offset: 0x0003A90E
		public static SetFavoriteCoin Deserialize(Stream stream, SetFavoriteCoin instance)
		{
			return SetFavoriteCoin.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001130 RID: 4400 RVA: 0x0003C71C File Offset: 0x0003A91C
		public static SetFavoriteCoin DeserializeLengthDelimited(Stream stream)
		{
			SetFavoriteCoin setFavoriteCoin = new SetFavoriteCoin();
			SetFavoriteCoin.DeserializeLengthDelimited(stream, setFavoriteCoin);
			return setFavoriteCoin;
		}

		// Token: 0x06001131 RID: 4401 RVA: 0x0003C738 File Offset: 0x0003A938
		public static SetFavoriteCoin DeserializeLengthDelimited(Stream stream, SetFavoriteCoin instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SetFavoriteCoin.Deserialize(stream, instance, num);
		}

		// Token: 0x06001132 RID: 4402 RVA: 0x0003C760 File Offset: 0x0003A960
		public static SetFavoriteCoin Deserialize(Stream stream, SetFavoriteCoin instance, long limit)
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
				else if (num == 8)
				{
					instance.Coin = (int)ProtocolParser.ReadUInt64(stream);
				}
				else
				{
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

		// Token: 0x06001133 RID: 4403 RVA: 0x0003C7E0 File Offset: 0x0003A9E0
		public void Serialize(Stream stream)
		{
			SetFavoriteCoin.Serialize(stream, this);
		}

		// Token: 0x06001134 RID: 4404 RVA: 0x0003C7E9 File Offset: 0x0003A9E9
		public static void Serialize(Stream stream, SetFavoriteCoin instance)
		{
			if (instance.HasCoin)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Coin));
			}
		}

		// Token: 0x06001135 RID: 4405 RVA: 0x0003C808 File Offset: 0x0003AA08
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasCoin)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Coin));
			}
			return num;
		}

		// Token: 0x04000544 RID: 1348
		public bool HasCoin;

		// Token: 0x04000545 RID: 1349
		private int _Coin;

		// Token: 0x02000606 RID: 1542
		public enum PacketID
		{
			// Token: 0x0400204F RID: 8271
			ID = 609,
			// Token: 0x04002050 RID: 8272
			System = 0
		}
	}
}
