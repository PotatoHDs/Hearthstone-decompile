using System;
using System.Collections.Generic;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x02000106 RID: 262
	public class CoinUpdate : IProtoBuf
	{
		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06001146 RID: 4422 RVA: 0x0003CA97 File Offset: 0x0003AC97
		// (set) Token: 0x06001147 RID: 4423 RVA: 0x0003CA9F File Offset: 0x0003AC9F
		public int FavoriteCoinId
		{
			get
			{
				return this._FavoriteCoinId;
			}
			set
			{
				this._FavoriteCoinId = value;
				this.HasFavoriteCoinId = true;
			}
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06001148 RID: 4424 RVA: 0x0003CAAF File Offset: 0x0003ACAF
		// (set) Token: 0x06001149 RID: 4425 RVA: 0x0003CAB7 File Offset: 0x0003ACB7
		public List<int> AddCoinId
		{
			get
			{
				return this._AddCoinId;
			}
			set
			{
				this._AddCoinId = value;
			}
		}

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x0600114A RID: 4426 RVA: 0x0003CAC0 File Offset: 0x0003ACC0
		// (set) Token: 0x0600114B RID: 4427 RVA: 0x0003CAC8 File Offset: 0x0003ACC8
		public List<int> RemoveCoinId
		{
			get
			{
				return this._RemoveCoinId;
			}
			set
			{
				this._RemoveCoinId = value;
			}
		}

		// Token: 0x0600114C RID: 4428 RVA: 0x0003CAD4 File Offset: 0x0003ACD4
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasFavoriteCoinId)
			{
				num ^= this.FavoriteCoinId.GetHashCode();
			}
			foreach (int num2 in this.AddCoinId)
			{
				num ^= num2.GetHashCode();
			}
			foreach (int num3 in this.RemoveCoinId)
			{
				num ^= num3.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600114D RID: 4429 RVA: 0x0003CB98 File Offset: 0x0003AD98
		public override bool Equals(object obj)
		{
			CoinUpdate coinUpdate = obj as CoinUpdate;
			if (coinUpdate == null)
			{
				return false;
			}
			if (this.HasFavoriteCoinId != coinUpdate.HasFavoriteCoinId || (this.HasFavoriteCoinId && !this.FavoriteCoinId.Equals(coinUpdate.FavoriteCoinId)))
			{
				return false;
			}
			if (this.AddCoinId.Count != coinUpdate.AddCoinId.Count)
			{
				return false;
			}
			for (int i = 0; i < this.AddCoinId.Count; i++)
			{
				if (!this.AddCoinId[i].Equals(coinUpdate.AddCoinId[i]))
				{
					return false;
				}
			}
			if (this.RemoveCoinId.Count != coinUpdate.RemoveCoinId.Count)
			{
				return false;
			}
			for (int j = 0; j < this.RemoveCoinId.Count; j++)
			{
				if (!this.RemoveCoinId[j].Equals(coinUpdate.RemoveCoinId[j]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600114E RID: 4430 RVA: 0x0003CC88 File Offset: 0x0003AE88
		public void Deserialize(Stream stream)
		{
			CoinUpdate.Deserialize(stream, this);
		}

		// Token: 0x0600114F RID: 4431 RVA: 0x0003CC92 File Offset: 0x0003AE92
		public static CoinUpdate Deserialize(Stream stream, CoinUpdate instance)
		{
			return CoinUpdate.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001150 RID: 4432 RVA: 0x0003CCA0 File Offset: 0x0003AEA0
		public static CoinUpdate DeserializeLengthDelimited(Stream stream)
		{
			CoinUpdate coinUpdate = new CoinUpdate();
			CoinUpdate.DeserializeLengthDelimited(stream, coinUpdate);
			return coinUpdate;
		}

		// Token: 0x06001151 RID: 4433 RVA: 0x0003CCBC File Offset: 0x0003AEBC
		public static CoinUpdate DeserializeLengthDelimited(Stream stream, CoinUpdate instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return CoinUpdate.Deserialize(stream, instance, num);
		}

		// Token: 0x06001152 RID: 4434 RVA: 0x0003CCE4 File Offset: 0x0003AEE4
		public static CoinUpdate Deserialize(Stream stream, CoinUpdate instance, long limit)
		{
			if (instance.AddCoinId == null)
			{
				instance.AddCoinId = new List<int>();
			}
			if (instance.RemoveCoinId == null)
			{
				instance.RemoveCoinId = new List<int>();
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
						if (num != 24)
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
							instance.RemoveCoinId.Add((int)ProtocolParser.ReadUInt64(stream));
						}
					}
					else
					{
						instance.AddCoinId.Add((int)ProtocolParser.ReadUInt64(stream));
					}
				}
				else
				{
					instance.FavoriteCoinId = (int)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06001153 RID: 4435 RVA: 0x0003CDC7 File Offset: 0x0003AFC7
		public void Serialize(Stream stream)
		{
			CoinUpdate.Serialize(stream, this);
		}

		// Token: 0x06001154 RID: 4436 RVA: 0x0003CDD0 File Offset: 0x0003AFD0
		public static void Serialize(Stream stream, CoinUpdate instance)
		{
			if (instance.HasFavoriteCoinId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.FavoriteCoinId));
			}
			if (instance.AddCoinId.Count > 0)
			{
				foreach (int num in instance.AddCoinId)
				{
					stream.WriteByte(16);
					ProtocolParser.WriteUInt64(stream, (ulong)((long)num));
				}
			}
			if (instance.RemoveCoinId.Count > 0)
			{
				foreach (int num2 in instance.RemoveCoinId)
				{
					stream.WriteByte(24);
					ProtocolParser.WriteUInt64(stream, (ulong)((long)num2));
				}
			}
		}

		// Token: 0x06001155 RID: 4437 RVA: 0x0003CEB0 File Offset: 0x0003B0B0
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasFavoriteCoinId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.FavoriteCoinId));
			}
			if (this.AddCoinId.Count > 0)
			{
				foreach (int num2 in this.AddCoinId)
				{
					num += 1U;
					num += ProtocolParser.SizeOfUInt64((ulong)((long)num2));
				}
			}
			if (this.RemoveCoinId.Count > 0)
			{
				foreach (int num3 in this.RemoveCoinId)
				{
					num += 1U;
					num += ProtocolParser.SizeOfUInt64((ulong)((long)num3));
				}
			}
			return num;
		}

		// Token: 0x0400054A RID: 1354
		public bool HasFavoriteCoinId;

		// Token: 0x0400054B RID: 1355
		private int _FavoriteCoinId;

		// Token: 0x0400054C RID: 1356
		private List<int> _AddCoinId = new List<int>();

		// Token: 0x0400054D RID: 1357
		private List<int> _RemoveCoinId = new List<int>();

		// Token: 0x02000608 RID: 1544
		public enum PacketID
		{
			// Token: 0x04002054 RID: 8276
			ID = 611,
			// Token: 0x04002055 RID: 8277
			System = 0
		}
	}
}
