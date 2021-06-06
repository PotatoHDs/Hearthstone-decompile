using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x02000105 RID: 261
	public class SetFavoriteCoinResponse : IProtoBuf
	{
		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06001137 RID: 4407 RVA: 0x0003C833 File Offset: 0x0003AA33
		// (set) Token: 0x06001138 RID: 4408 RVA: 0x0003C83B File Offset: 0x0003AA3B
		public bool Success
		{
			get
			{
				return this._Success;
			}
			set
			{
				this._Success = value;
				this.HasSuccess = true;
			}
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06001139 RID: 4409 RVA: 0x0003C84B File Offset: 0x0003AA4B
		// (set) Token: 0x0600113A RID: 4410 RVA: 0x0003C853 File Offset: 0x0003AA53
		public int CoinId
		{
			get
			{
				return this._CoinId;
			}
			set
			{
				this._CoinId = value;
				this.HasCoinId = true;
			}
		}

		// Token: 0x0600113B RID: 4411 RVA: 0x0003C864 File Offset: 0x0003AA64
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasSuccess)
			{
				num ^= this.Success.GetHashCode();
			}
			if (this.HasCoinId)
			{
				num ^= this.CoinId.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600113C RID: 4412 RVA: 0x0003C8B0 File Offset: 0x0003AAB0
		public override bool Equals(object obj)
		{
			SetFavoriteCoinResponse setFavoriteCoinResponse = obj as SetFavoriteCoinResponse;
			return setFavoriteCoinResponse != null && this.HasSuccess == setFavoriteCoinResponse.HasSuccess && (!this.HasSuccess || this.Success.Equals(setFavoriteCoinResponse.Success)) && this.HasCoinId == setFavoriteCoinResponse.HasCoinId && (!this.HasCoinId || this.CoinId.Equals(setFavoriteCoinResponse.CoinId));
		}

		// Token: 0x0600113D RID: 4413 RVA: 0x0003C926 File Offset: 0x0003AB26
		public void Deserialize(Stream stream)
		{
			SetFavoriteCoinResponse.Deserialize(stream, this);
		}

		// Token: 0x0600113E RID: 4414 RVA: 0x0003C930 File Offset: 0x0003AB30
		public static SetFavoriteCoinResponse Deserialize(Stream stream, SetFavoriteCoinResponse instance)
		{
			return SetFavoriteCoinResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600113F RID: 4415 RVA: 0x0003C93C File Offset: 0x0003AB3C
		public static SetFavoriteCoinResponse DeserializeLengthDelimited(Stream stream)
		{
			SetFavoriteCoinResponse setFavoriteCoinResponse = new SetFavoriteCoinResponse();
			SetFavoriteCoinResponse.DeserializeLengthDelimited(stream, setFavoriteCoinResponse);
			return setFavoriteCoinResponse;
		}

		// Token: 0x06001140 RID: 4416 RVA: 0x0003C958 File Offset: 0x0003AB58
		public static SetFavoriteCoinResponse DeserializeLengthDelimited(Stream stream, SetFavoriteCoinResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SetFavoriteCoinResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06001141 RID: 4417 RVA: 0x0003C980 File Offset: 0x0003AB80
		public static SetFavoriteCoinResponse Deserialize(Stream stream, SetFavoriteCoinResponse instance, long limit)
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
						instance.CoinId = (int)ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.Success = ProtocolParser.ReadBool(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06001142 RID: 4418 RVA: 0x0003CA18 File Offset: 0x0003AC18
		public void Serialize(Stream stream)
		{
			SetFavoriteCoinResponse.Serialize(stream, this);
		}

		// Token: 0x06001143 RID: 4419 RVA: 0x0003CA21 File Offset: 0x0003AC21
		public static void Serialize(Stream stream, SetFavoriteCoinResponse instance)
		{
			if (instance.HasSuccess)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteBool(stream, instance.Success);
			}
			if (instance.HasCoinId)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.CoinId));
			}
		}

		// Token: 0x06001144 RID: 4420 RVA: 0x0003CA5C File Offset: 0x0003AC5C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasSuccess)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasCoinId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.CoinId));
			}
			return num;
		}

		// Token: 0x04000546 RID: 1350
		public bool HasSuccess;

		// Token: 0x04000547 RID: 1351
		private bool _Success;

		// Token: 0x04000548 RID: 1352
		public bool HasCoinId;

		// Token: 0x04000549 RID: 1353
		private int _CoinId;

		// Token: 0x02000607 RID: 1543
		public enum PacketID
		{
			// Token: 0x04002052 RID: 8274
			ID = 610
		}
	}
}
