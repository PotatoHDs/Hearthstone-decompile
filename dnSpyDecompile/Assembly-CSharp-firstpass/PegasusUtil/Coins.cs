using System;
using System.Collections.Generic;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x02000103 RID: 259
	public class Coins : IProtoBuf
	{
		// Token: 0x1700033D RID: 829
		// (get) Token: 0x0600111B RID: 4379 RVA: 0x0003C2F2 File Offset: 0x0003A4F2
		// (set) Token: 0x0600111C RID: 4380 RVA: 0x0003C2FA File Offset: 0x0003A4FA
		public int FavoriteCoin
		{
			get
			{
				return this._FavoriteCoin;
			}
			set
			{
				this._FavoriteCoin = value;
				this.HasFavoriteCoin = true;
			}
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x0600111D RID: 4381 RVA: 0x0003C30A File Offset: 0x0003A50A
		// (set) Token: 0x0600111E RID: 4382 RVA: 0x0003C312 File Offset: 0x0003A512
		public List<int> Coins_
		{
			get
			{
				return this._Coins_;
			}
			set
			{
				this._Coins_ = value;
			}
		}

		// Token: 0x0600111F RID: 4383 RVA: 0x0003C31C File Offset: 0x0003A51C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasFavoriteCoin)
			{
				num ^= this.FavoriteCoin.GetHashCode();
			}
			foreach (int num2 in this.Coins_)
			{
				num ^= num2.GetHashCode();
			}
			return num;
		}

		// Token: 0x06001120 RID: 4384 RVA: 0x0003C398 File Offset: 0x0003A598
		public override bool Equals(object obj)
		{
			Coins coins = obj as Coins;
			if (coins == null)
			{
				return false;
			}
			if (this.HasFavoriteCoin != coins.HasFavoriteCoin || (this.HasFavoriteCoin && !this.FavoriteCoin.Equals(coins.FavoriteCoin)))
			{
				return false;
			}
			if (this.Coins_.Count != coins.Coins_.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Coins_.Count; i++)
			{
				if (!this.Coins_[i].Equals(coins.Coins_[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001121 RID: 4385 RVA: 0x0003C434 File Offset: 0x0003A634
		public void Deserialize(Stream stream)
		{
			Coins.Deserialize(stream, this);
		}

		// Token: 0x06001122 RID: 4386 RVA: 0x0003C43E File Offset: 0x0003A63E
		public static Coins Deserialize(Stream stream, Coins instance)
		{
			return Coins.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001123 RID: 4387 RVA: 0x0003C44C File Offset: 0x0003A64C
		public static Coins DeserializeLengthDelimited(Stream stream)
		{
			Coins coins = new Coins();
			Coins.DeserializeLengthDelimited(stream, coins);
			return coins;
		}

		// Token: 0x06001124 RID: 4388 RVA: 0x0003C468 File Offset: 0x0003A668
		public static Coins DeserializeLengthDelimited(Stream stream, Coins instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return Coins.Deserialize(stream, instance, num);
		}

		// Token: 0x06001125 RID: 4389 RVA: 0x0003C490 File Offset: 0x0003A690
		public static Coins Deserialize(Stream stream, Coins instance, long limit)
		{
			if (instance.Coins_ == null)
			{
				instance.Coins_ = new List<int>();
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
						instance.Coins_.Add((int)ProtocolParser.ReadUInt64(stream));
					}
				}
				else
				{
					instance.FavoriteCoin = (int)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06001126 RID: 4390 RVA: 0x0003C541 File Offset: 0x0003A741
		public void Serialize(Stream stream)
		{
			Coins.Serialize(stream, this);
		}

		// Token: 0x06001127 RID: 4391 RVA: 0x0003C54C File Offset: 0x0003A74C
		public static void Serialize(Stream stream, Coins instance)
		{
			if (instance.HasFavoriteCoin)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.FavoriteCoin));
			}
			if (instance.Coins_.Count > 0)
			{
				foreach (int num in instance.Coins_)
				{
					stream.WriteByte(16);
					ProtocolParser.WriteUInt64(stream, (ulong)((long)num));
				}
			}
		}

		// Token: 0x06001128 RID: 4392 RVA: 0x0003C5D4 File Offset: 0x0003A7D4
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasFavoriteCoin)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.FavoriteCoin));
			}
			if (this.Coins_.Count > 0)
			{
				foreach (int num2 in this.Coins_)
				{
					num += 1U;
					num += ProtocolParser.SizeOfUInt64((ulong)((long)num2));
				}
			}
			return num;
		}

		// Token: 0x04000541 RID: 1345
		public bool HasFavoriteCoin;

		// Token: 0x04000542 RID: 1346
		private int _FavoriteCoin;

		// Token: 0x04000543 RID: 1347
		private List<int> _Coins_ = new List<int>();

		// Token: 0x02000605 RID: 1541
		public enum PacketID
		{
			// Token: 0x0400204C RID: 8268
			ID = 608,
			// Token: 0x0400204D RID: 8269
			System = 0
		}
	}
}
