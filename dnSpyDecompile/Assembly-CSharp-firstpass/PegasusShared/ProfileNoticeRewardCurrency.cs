using System;
using System.IO;

namespace PegasusShared
{
	// Token: 0x0200012F RID: 303
	public class ProfileNoticeRewardCurrency : IProtoBuf
	{
		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x060013F3 RID: 5107 RVA: 0x000451C5 File Offset: 0x000433C5
		// (set) Token: 0x060013F4 RID: 5108 RVA: 0x000451CD File Offset: 0x000433CD
		public int Amount { get; set; }

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x060013F5 RID: 5109 RVA: 0x000451D6 File Offset: 0x000433D6
		// (set) Token: 0x060013F6 RID: 5110 RVA: 0x000451DE File Offset: 0x000433DE
		public CurrencyType CurrencyType
		{
			get
			{
				return this._CurrencyType;
			}
			set
			{
				this._CurrencyType = value;
				this.HasCurrencyType = true;
			}
		}

		// Token: 0x060013F7 RID: 5111 RVA: 0x000451F0 File Offset: 0x000433F0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Amount.GetHashCode();
			if (this.HasCurrencyType)
			{
				num ^= this.CurrencyType.GetHashCode();
			}
			return num;
		}

		// Token: 0x060013F8 RID: 5112 RVA: 0x0004523C File Offset: 0x0004343C
		public override bool Equals(object obj)
		{
			ProfileNoticeRewardCurrency profileNoticeRewardCurrency = obj as ProfileNoticeRewardCurrency;
			return profileNoticeRewardCurrency != null && this.Amount.Equals(profileNoticeRewardCurrency.Amount) && this.HasCurrencyType == profileNoticeRewardCurrency.HasCurrencyType && (!this.HasCurrencyType || this.CurrencyType.Equals(profileNoticeRewardCurrency.CurrencyType));
		}

		// Token: 0x060013F9 RID: 5113 RVA: 0x000452A7 File Offset: 0x000434A7
		public void Deserialize(Stream stream)
		{
			ProfileNoticeRewardCurrency.Deserialize(stream, this);
		}

		// Token: 0x060013FA RID: 5114 RVA: 0x000452B1 File Offset: 0x000434B1
		public static ProfileNoticeRewardCurrency Deserialize(Stream stream, ProfileNoticeRewardCurrency instance)
		{
			return ProfileNoticeRewardCurrency.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060013FB RID: 5115 RVA: 0x000452BC File Offset: 0x000434BC
		public static ProfileNoticeRewardCurrency DeserializeLengthDelimited(Stream stream)
		{
			ProfileNoticeRewardCurrency profileNoticeRewardCurrency = new ProfileNoticeRewardCurrency();
			ProfileNoticeRewardCurrency.DeserializeLengthDelimited(stream, profileNoticeRewardCurrency);
			return profileNoticeRewardCurrency;
		}

		// Token: 0x060013FC RID: 5116 RVA: 0x000452D8 File Offset: 0x000434D8
		public static ProfileNoticeRewardCurrency DeserializeLengthDelimited(Stream stream, ProfileNoticeRewardCurrency instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ProfileNoticeRewardCurrency.Deserialize(stream, instance, num);
		}

		// Token: 0x060013FD RID: 5117 RVA: 0x00045300 File Offset: 0x00043500
		public static ProfileNoticeRewardCurrency Deserialize(Stream stream, ProfileNoticeRewardCurrency instance, long limit)
		{
			instance.CurrencyType = CurrencyType.CURRENCY_TYPE_UNKNOWN;
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
						instance.CurrencyType = (CurrencyType)ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.Amount = (int)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060013FE RID: 5118 RVA: 0x000453A0 File Offset: 0x000435A0
		public void Serialize(Stream stream)
		{
			ProfileNoticeRewardCurrency.Serialize(stream, this);
		}

		// Token: 0x060013FF RID: 5119 RVA: 0x000453A9 File Offset: 0x000435A9
		public static void Serialize(Stream stream, ProfileNoticeRewardCurrency instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Amount));
			if (instance.HasCurrencyType)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.CurrencyType));
			}
		}

		// Token: 0x06001400 RID: 5120 RVA: 0x000453DC File Offset: 0x000435DC
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Amount));
			if (this.HasCurrencyType)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.CurrencyType));
			}
			return num + 1U;
		}

		// Token: 0x04000628 RID: 1576
		public bool HasCurrencyType;

		// Token: 0x04000629 RID: 1577
		private CurrencyType _CurrencyType;

		// Token: 0x02000623 RID: 1571
		public enum NoticeID
		{
			// Token: 0x040020A0 RID: 8352
			ID = 9
		}
	}
}
