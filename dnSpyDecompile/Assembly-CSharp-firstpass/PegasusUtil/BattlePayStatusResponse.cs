using System;
using System.IO;
using System.Text;
using PegasusShared;

namespace PegasusUtil
{
	// Token: 0x020000AE RID: 174
	public class BattlePayStatusResponse : IProtoBuf
	{
		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000C0C RID: 3084 RVA: 0x0002D908 File Offset: 0x0002BB08
		// (set) Token: 0x06000C0D RID: 3085 RVA: 0x0002D910 File Offset: 0x0002BB10
		public BattlePayStatusResponse.PurchaseState Status { get; set; }

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000C0E RID: 3086 RVA: 0x0002D919 File Offset: 0x0002BB19
		// (set) Token: 0x06000C0F RID: 3087 RVA: 0x0002D921 File Offset: 0x0002BB21
		public long PmtProductId
		{
			get
			{
				return this._PmtProductId;
			}
			set
			{
				this._PmtProductId = value;
				this.HasPmtProductId = true;
			}
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000C10 RID: 3088 RVA: 0x0002D931 File Offset: 0x0002BB31
		// (set) Token: 0x06000C11 RID: 3089 RVA: 0x0002D939 File Offset: 0x0002BB39
		public PurchaseError PurchaseError
		{
			get
			{
				return this._PurchaseError;
			}
			set
			{
				this._PurchaseError = value;
				this.HasPurchaseError = (value != null);
			}
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000C12 RID: 3090 RVA: 0x0002D94C File Offset: 0x0002BB4C
		// (set) Token: 0x06000C13 RID: 3091 RVA: 0x0002D954 File Offset: 0x0002BB54
		public bool BattlePayAvailable { get; set; }

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000C14 RID: 3092 RVA: 0x0002D95D File Offset: 0x0002BB5D
		// (set) Token: 0x06000C15 RID: 3093 RVA: 0x0002D965 File Offset: 0x0002BB65
		public long TransactionId
		{
			get
			{
				return this._TransactionId;
			}
			set
			{
				this._TransactionId = value;
				this.HasTransactionId = true;
			}
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000C16 RID: 3094 RVA: 0x0002D975 File Offset: 0x0002BB75
		// (set) Token: 0x06000C17 RID: 3095 RVA: 0x0002D97D File Offset: 0x0002BB7D
		public string ThirdPartyId
		{
			get
			{
				return this._ThirdPartyId;
			}
			set
			{
				this._ThirdPartyId = value;
				this.HasThirdPartyId = (value != null);
			}
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000C18 RID: 3096 RVA: 0x0002D990 File Offset: 0x0002BB90
		// (set) Token: 0x06000C19 RID: 3097 RVA: 0x0002D998 File Offset: 0x0002BB98
		public int CurrencyDeprecated
		{
			get
			{
				return this._CurrencyDeprecated;
			}
			set
			{
				this._CurrencyDeprecated = value;
				this.HasCurrencyDeprecated = true;
			}
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000C1A RID: 3098 RVA: 0x0002D9A8 File Offset: 0x0002BBA8
		// (set) Token: 0x06000C1B RID: 3099 RVA: 0x0002D9B0 File Offset: 0x0002BBB0
		public BattlePayProvider Provider
		{
			get
			{
				return this._Provider;
			}
			set
			{
				this._Provider = value;
				this.HasProvider = true;
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000C1C RID: 3100 RVA: 0x0002D9C0 File Offset: 0x0002BBC0
		// (set) Token: 0x06000C1D RID: 3101 RVA: 0x0002D9C8 File Offset: 0x0002BBC8
		public string CurrencyCode
		{
			get
			{
				return this._CurrencyCode;
			}
			set
			{
				this._CurrencyCode = value;
				this.HasCurrencyCode = (value != null);
			}
		}

		// Token: 0x06000C1E RID: 3102 RVA: 0x0002D9DC File Offset: 0x0002BBDC
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Status.GetHashCode();
			if (this.HasPmtProductId)
			{
				num ^= this.PmtProductId.GetHashCode();
			}
			if (this.HasPurchaseError)
			{
				num ^= this.PurchaseError.GetHashCode();
			}
			num ^= this.BattlePayAvailable.GetHashCode();
			if (this.HasTransactionId)
			{
				num ^= this.TransactionId.GetHashCode();
			}
			if (this.HasThirdPartyId)
			{
				num ^= this.ThirdPartyId.GetHashCode();
			}
			if (this.HasCurrencyDeprecated)
			{
				num ^= this.CurrencyDeprecated.GetHashCode();
			}
			if (this.HasProvider)
			{
				num ^= this.Provider.GetHashCode();
			}
			if (this.HasCurrencyCode)
			{
				num ^= this.CurrencyCode.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000C1F RID: 3103 RVA: 0x0002DACC File Offset: 0x0002BCCC
		public override bool Equals(object obj)
		{
			BattlePayStatusResponse battlePayStatusResponse = obj as BattlePayStatusResponse;
			return battlePayStatusResponse != null && this.Status.Equals(battlePayStatusResponse.Status) && this.HasPmtProductId == battlePayStatusResponse.HasPmtProductId && (!this.HasPmtProductId || this.PmtProductId.Equals(battlePayStatusResponse.PmtProductId)) && this.HasPurchaseError == battlePayStatusResponse.HasPurchaseError && (!this.HasPurchaseError || this.PurchaseError.Equals(battlePayStatusResponse.PurchaseError)) && this.BattlePayAvailable.Equals(battlePayStatusResponse.BattlePayAvailable) && this.HasTransactionId == battlePayStatusResponse.HasTransactionId && (!this.HasTransactionId || this.TransactionId.Equals(battlePayStatusResponse.TransactionId)) && this.HasThirdPartyId == battlePayStatusResponse.HasThirdPartyId && (!this.HasThirdPartyId || this.ThirdPartyId.Equals(battlePayStatusResponse.ThirdPartyId)) && this.HasCurrencyDeprecated == battlePayStatusResponse.HasCurrencyDeprecated && (!this.HasCurrencyDeprecated || this.CurrencyDeprecated.Equals(battlePayStatusResponse.CurrencyDeprecated)) && this.HasProvider == battlePayStatusResponse.HasProvider && (!this.HasProvider || this.Provider.Equals(battlePayStatusResponse.Provider)) && this.HasCurrencyCode == battlePayStatusResponse.HasCurrencyCode && (!this.HasCurrencyCode || this.CurrencyCode.Equals(battlePayStatusResponse.CurrencyCode));
		}

		// Token: 0x06000C20 RID: 3104 RVA: 0x0002DC67 File Offset: 0x0002BE67
		public void Deserialize(Stream stream)
		{
			BattlePayStatusResponse.Deserialize(stream, this);
		}

		// Token: 0x06000C21 RID: 3105 RVA: 0x0002DC71 File Offset: 0x0002BE71
		public static BattlePayStatusResponse Deserialize(Stream stream, BattlePayStatusResponse instance)
		{
			return BattlePayStatusResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000C22 RID: 3106 RVA: 0x0002DC7C File Offset: 0x0002BE7C
		public static BattlePayStatusResponse DeserializeLengthDelimited(Stream stream)
		{
			BattlePayStatusResponse battlePayStatusResponse = new BattlePayStatusResponse();
			BattlePayStatusResponse.DeserializeLengthDelimited(stream, battlePayStatusResponse);
			return battlePayStatusResponse;
		}

		// Token: 0x06000C23 RID: 3107 RVA: 0x0002DC98 File Offset: 0x0002BE98
		public static BattlePayStatusResponse DeserializeLengthDelimited(Stream stream, BattlePayStatusResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return BattlePayStatusResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06000C24 RID: 3108 RVA: 0x0002DCC0 File Offset: 0x0002BEC0
		public static BattlePayStatusResponse Deserialize(Stream stream, BattlePayStatusResponse instance, long limit)
		{
			instance.Provider = BattlePayProvider.BP_PROVIDER_BLIZZARD;
			instance.CurrencyCode = "";
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
							if (num == 8)
							{
								instance.Status = (BattlePayStatusResponse.PurchaseState)ProtocolParser.ReadUInt64(stream);
								continue;
							}
							if (num == 16)
							{
								instance.PmtProductId = (long)ProtocolParser.ReadUInt64(stream);
								continue;
							}
						}
						else if (num != 26)
						{
							if (num == 32)
							{
								instance.BattlePayAvailable = ProtocolParser.ReadBool(stream);
								continue;
							}
						}
						else
						{
							if (instance.PurchaseError == null)
							{
								instance.PurchaseError = PurchaseError.DeserializeLengthDelimited(stream);
								continue;
							}
							PurchaseError.DeserializeLengthDelimited(stream, instance.PurchaseError);
							continue;
						}
					}
					else if (num <= 50)
					{
						if (num == 40)
						{
							instance.TransactionId = (long)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 50)
						{
							instance.ThirdPartyId = ProtocolParser.ReadString(stream);
							continue;
						}
					}
					else
					{
						if (num == 56)
						{
							instance.CurrencyDeprecated = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 64)
						{
							instance.Provider = (BattlePayProvider)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 74)
						{
							instance.CurrencyCode = ProtocolParser.ReadString(stream);
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

		// Token: 0x06000C25 RID: 3109 RVA: 0x0002DE5F File Offset: 0x0002C05F
		public void Serialize(Stream stream)
		{
			BattlePayStatusResponse.Serialize(stream, this);
		}

		// Token: 0x06000C26 RID: 3110 RVA: 0x0002DE68 File Offset: 0x0002C068
		public static void Serialize(Stream stream, BattlePayStatusResponse instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Status));
			if (instance.HasPmtProductId)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.PmtProductId);
			}
			if (instance.HasPurchaseError)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.PurchaseError.GetSerializedSize());
				PurchaseError.Serialize(stream, instance.PurchaseError);
			}
			stream.WriteByte(32);
			ProtocolParser.WriteBool(stream, instance.BattlePayAvailable);
			if (instance.HasTransactionId)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.TransactionId);
			}
			if (instance.HasThirdPartyId)
			{
				stream.WriteByte(50);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ThirdPartyId));
			}
			if (instance.HasCurrencyDeprecated)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.CurrencyDeprecated));
			}
			if (instance.HasProvider)
			{
				stream.WriteByte(64);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Provider));
			}
			if (instance.HasCurrencyCode)
			{
				stream.WriteByte(74);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.CurrencyCode));
			}
		}

		// Token: 0x06000C27 RID: 3111 RVA: 0x0002DF88 File Offset: 0x0002C188
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Status));
			if (this.HasPmtProductId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.PmtProductId);
			}
			if (this.HasPurchaseError)
			{
				num += 1U;
				uint serializedSize = this.PurchaseError.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			num += 1U;
			if (this.HasTransactionId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.TransactionId);
			}
			if (this.HasThirdPartyId)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.ThirdPartyId);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasCurrencyDeprecated)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.CurrencyDeprecated));
			}
			if (this.HasProvider)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Provider));
			}
			if (this.HasCurrencyCode)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.CurrencyCode);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			return num + 2U;
		}

		// Token: 0x0400043F RID: 1087
		public bool HasPmtProductId;

		// Token: 0x04000440 RID: 1088
		private long _PmtProductId;

		// Token: 0x04000441 RID: 1089
		public bool HasPurchaseError;

		// Token: 0x04000442 RID: 1090
		private PurchaseError _PurchaseError;

		// Token: 0x04000444 RID: 1092
		public bool HasTransactionId;

		// Token: 0x04000445 RID: 1093
		private long _TransactionId;

		// Token: 0x04000446 RID: 1094
		public bool HasThirdPartyId;

		// Token: 0x04000447 RID: 1095
		private string _ThirdPartyId;

		// Token: 0x04000448 RID: 1096
		public bool HasCurrencyDeprecated;

		// Token: 0x04000449 RID: 1097
		private int _CurrencyDeprecated;

		// Token: 0x0400044A RID: 1098
		public bool HasProvider;

		// Token: 0x0400044B RID: 1099
		private BattlePayProvider _Provider;

		// Token: 0x0400044C RID: 1100
		public bool HasCurrencyCode;

		// Token: 0x0400044D RID: 1101
		private string _CurrencyCode;

		// Token: 0x020005B6 RID: 1462
		public enum PacketID
		{
			// Token: 0x04001F72 RID: 8050
			ID = 265
		}

		// Token: 0x020005B7 RID: 1463
		public enum PurchaseState
		{
			// Token: 0x04001F74 RID: 8052
			PS_READY,
			// Token: 0x04001F75 RID: 8053
			PS_CHECK_RESULTS,
			// Token: 0x04001F76 RID: 8054
			PS_ERROR
		}
	}
}
