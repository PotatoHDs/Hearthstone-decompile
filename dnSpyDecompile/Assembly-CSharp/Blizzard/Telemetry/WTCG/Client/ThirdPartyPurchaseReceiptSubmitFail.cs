using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x02001203 RID: 4611
	public class ThirdPartyPurchaseReceiptSubmitFail : IProtoBuf
	{
		// Token: 0x17001023 RID: 4131
		// (get) Token: 0x0600CEB9 RID: 52921 RVA: 0x003D94E9 File Offset: 0x003D76E9
		// (set) Token: 0x0600CEBA RID: 52922 RVA: 0x003D94F1 File Offset: 0x003D76F1
		public Player Player
		{
			get
			{
				return this._Player;
			}
			set
			{
				this._Player = value;
				this.HasPlayer = (value != null);
			}
		}

		// Token: 0x17001024 RID: 4132
		// (get) Token: 0x0600CEBB RID: 52923 RVA: 0x003D9504 File Offset: 0x003D7704
		// (set) Token: 0x0600CEBC RID: 52924 RVA: 0x003D950C File Offset: 0x003D770C
		public DeviceInfo DeviceInfo
		{
			get
			{
				return this._DeviceInfo;
			}
			set
			{
				this._DeviceInfo = value;
				this.HasDeviceInfo = (value != null);
			}
		}

		// Token: 0x17001025 RID: 4133
		// (get) Token: 0x0600CEBD RID: 52925 RVA: 0x003D951F File Offset: 0x003D771F
		// (set) Token: 0x0600CEBE RID: 52926 RVA: 0x003D9527 File Offset: 0x003D7727
		public string TransactionId
		{
			get
			{
				return this._TransactionId;
			}
			set
			{
				this._TransactionId = value;
				this.HasTransactionId = (value != null);
			}
		}

		// Token: 0x17001026 RID: 4134
		// (get) Token: 0x0600CEBF RID: 52927 RVA: 0x003D953A File Offset: 0x003D773A
		// (set) Token: 0x0600CEC0 RID: 52928 RVA: 0x003D9542 File Offset: 0x003D7742
		public string ProductId
		{
			get
			{
				return this._ProductId;
			}
			set
			{
				this._ProductId = value;
				this.HasProductId = (value != null);
			}
		}

		// Token: 0x17001027 RID: 4135
		// (get) Token: 0x0600CEC1 RID: 52929 RVA: 0x003D9555 File Offset: 0x003D7755
		// (set) Token: 0x0600CEC2 RID: 52930 RVA: 0x003D955D File Offset: 0x003D775D
		public string Provider
		{
			get
			{
				return this._Provider;
			}
			set
			{
				this._Provider = value;
				this.HasProvider = (value != null);
			}
		}

		// Token: 0x17001028 RID: 4136
		// (get) Token: 0x0600CEC3 RID: 52931 RVA: 0x003D9570 File Offset: 0x003D7770
		// (set) Token: 0x0600CEC4 RID: 52932 RVA: 0x003D9578 File Offset: 0x003D7778
		public ThirdPartyPurchaseReceiptSubmitFail.FailureReason Reason
		{
			get
			{
				return this._Reason;
			}
			set
			{
				this._Reason = value;
				this.HasReason = true;
			}
		}

		// Token: 0x17001029 RID: 4137
		// (get) Token: 0x0600CEC5 RID: 52933 RVA: 0x003D9588 File Offset: 0x003D7788
		// (set) Token: 0x0600CEC6 RID: 52934 RVA: 0x003D9590 File Offset: 0x003D7790
		public string InvalidData
		{
			get
			{
				return this._InvalidData;
			}
			set
			{
				this._InvalidData = value;
				this.HasInvalidData = (value != null);
			}
		}

		// Token: 0x0600CEC7 RID: 52935 RVA: 0x003D95A4 File Offset: 0x003D77A4
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasPlayer)
			{
				num ^= this.Player.GetHashCode();
			}
			if (this.HasDeviceInfo)
			{
				num ^= this.DeviceInfo.GetHashCode();
			}
			if (this.HasTransactionId)
			{
				num ^= this.TransactionId.GetHashCode();
			}
			if (this.HasProductId)
			{
				num ^= this.ProductId.GetHashCode();
			}
			if (this.HasProvider)
			{
				num ^= this.Provider.GetHashCode();
			}
			if (this.HasReason)
			{
				num ^= this.Reason.GetHashCode();
			}
			if (this.HasInvalidData)
			{
				num ^= this.InvalidData.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600CEC8 RID: 52936 RVA: 0x003D9664 File Offset: 0x003D7864
		public override bool Equals(object obj)
		{
			ThirdPartyPurchaseReceiptSubmitFail thirdPartyPurchaseReceiptSubmitFail = obj as ThirdPartyPurchaseReceiptSubmitFail;
			return thirdPartyPurchaseReceiptSubmitFail != null && this.HasPlayer == thirdPartyPurchaseReceiptSubmitFail.HasPlayer && (!this.HasPlayer || this.Player.Equals(thirdPartyPurchaseReceiptSubmitFail.Player)) && this.HasDeviceInfo == thirdPartyPurchaseReceiptSubmitFail.HasDeviceInfo && (!this.HasDeviceInfo || this.DeviceInfo.Equals(thirdPartyPurchaseReceiptSubmitFail.DeviceInfo)) && this.HasTransactionId == thirdPartyPurchaseReceiptSubmitFail.HasTransactionId && (!this.HasTransactionId || this.TransactionId.Equals(thirdPartyPurchaseReceiptSubmitFail.TransactionId)) && this.HasProductId == thirdPartyPurchaseReceiptSubmitFail.HasProductId && (!this.HasProductId || this.ProductId.Equals(thirdPartyPurchaseReceiptSubmitFail.ProductId)) && this.HasProvider == thirdPartyPurchaseReceiptSubmitFail.HasProvider && (!this.HasProvider || this.Provider.Equals(thirdPartyPurchaseReceiptSubmitFail.Provider)) && this.HasReason == thirdPartyPurchaseReceiptSubmitFail.HasReason && (!this.HasReason || this.Reason.Equals(thirdPartyPurchaseReceiptSubmitFail.Reason)) && this.HasInvalidData == thirdPartyPurchaseReceiptSubmitFail.HasInvalidData && (!this.HasInvalidData || this.InvalidData.Equals(thirdPartyPurchaseReceiptSubmitFail.InvalidData));
		}

		// Token: 0x0600CEC9 RID: 52937 RVA: 0x003D97B9 File Offset: 0x003D79B9
		public void Deserialize(Stream stream)
		{
			ThirdPartyPurchaseReceiptSubmitFail.Deserialize(stream, this);
		}

		// Token: 0x0600CECA RID: 52938 RVA: 0x003D97C3 File Offset: 0x003D79C3
		public static ThirdPartyPurchaseReceiptSubmitFail Deserialize(Stream stream, ThirdPartyPurchaseReceiptSubmitFail instance)
		{
			return ThirdPartyPurchaseReceiptSubmitFail.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600CECB RID: 52939 RVA: 0x003D97D0 File Offset: 0x003D79D0
		public static ThirdPartyPurchaseReceiptSubmitFail DeserializeLengthDelimited(Stream stream)
		{
			ThirdPartyPurchaseReceiptSubmitFail thirdPartyPurchaseReceiptSubmitFail = new ThirdPartyPurchaseReceiptSubmitFail();
			ThirdPartyPurchaseReceiptSubmitFail.DeserializeLengthDelimited(stream, thirdPartyPurchaseReceiptSubmitFail);
			return thirdPartyPurchaseReceiptSubmitFail;
		}

		// Token: 0x0600CECC RID: 52940 RVA: 0x003D97EC File Offset: 0x003D79EC
		public static ThirdPartyPurchaseReceiptSubmitFail DeserializeLengthDelimited(Stream stream, ThirdPartyPurchaseReceiptSubmitFail instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ThirdPartyPurchaseReceiptSubmitFail.Deserialize(stream, instance, num);
		}

		// Token: 0x0600CECD RID: 52941 RVA: 0x003D9814 File Offset: 0x003D7A14
		public static ThirdPartyPurchaseReceiptSubmitFail Deserialize(Stream stream, ThirdPartyPurchaseReceiptSubmitFail instance, long limit)
		{
			instance.Reason = ThirdPartyPurchaseReceiptSubmitFail.FailureReason.INVALID_STATE;
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
					if (num <= 26)
					{
						if (num != 10)
						{
							if (num != 18)
							{
								if (num == 26)
								{
									instance.TransactionId = ProtocolParser.ReadString(stream);
									continue;
								}
							}
							else
							{
								if (instance.DeviceInfo == null)
								{
									instance.DeviceInfo = DeviceInfo.DeserializeLengthDelimited(stream);
									continue;
								}
								DeviceInfo.DeserializeLengthDelimited(stream, instance.DeviceInfo);
								continue;
							}
						}
						else
						{
							if (instance.Player == null)
							{
								instance.Player = Player.DeserializeLengthDelimited(stream);
								continue;
							}
							Player.DeserializeLengthDelimited(stream, instance.Player);
							continue;
						}
					}
					else if (num <= 42)
					{
						if (num == 34)
						{
							instance.ProductId = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 42)
						{
							instance.Provider = ProtocolParser.ReadString(stream);
							continue;
						}
					}
					else
					{
						if (num == 48)
						{
							instance.Reason = (ThirdPartyPurchaseReceiptSubmitFail.FailureReason)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 58)
						{
							instance.InvalidData = ProtocolParser.ReadString(stream);
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

		// Token: 0x0600CECE RID: 52942 RVA: 0x003D9988 File Offset: 0x003D7B88
		public void Serialize(Stream stream)
		{
			ThirdPartyPurchaseReceiptSubmitFail.Serialize(stream, this);
		}

		// Token: 0x0600CECF RID: 52943 RVA: 0x003D9994 File Offset: 0x003D7B94
		public static void Serialize(Stream stream, ThirdPartyPurchaseReceiptSubmitFail instance)
		{
			if (instance.HasPlayer)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Player.GetSerializedSize());
				Player.Serialize(stream, instance.Player);
			}
			if (instance.HasDeviceInfo)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.DeviceInfo.GetSerializedSize());
				DeviceInfo.Serialize(stream, instance.DeviceInfo);
			}
			if (instance.HasTransactionId)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.TransactionId));
			}
			if (instance.HasProductId)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ProductId));
			}
			if (instance.HasProvider)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Provider));
			}
			if (instance.HasReason)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Reason));
			}
			if (instance.HasInvalidData)
			{
				stream.WriteByte(58);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.InvalidData));
			}
		}

		// Token: 0x0600CED0 RID: 52944 RVA: 0x003D9AB0 File Offset: 0x003D7CB0
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasPlayer)
			{
				num += 1U;
				uint serializedSize = this.Player.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasDeviceInfo)
			{
				num += 1U;
				uint serializedSize2 = this.DeviceInfo.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasTransactionId)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.TransactionId);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasProductId)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.ProductId);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (this.HasProvider)
			{
				num += 1U;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(this.Provider);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			if (this.HasReason)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Reason));
			}
			if (this.HasInvalidData)
			{
				num += 1U;
				uint byteCount4 = (uint)Encoding.UTF8.GetByteCount(this.InvalidData);
				num += ProtocolParser.SizeOfUInt32(byteCount4) + byteCount4;
			}
			return num;
		}

		// Token: 0x0400A18F RID: 41359
		public bool HasPlayer;

		// Token: 0x0400A190 RID: 41360
		private Player _Player;

		// Token: 0x0400A191 RID: 41361
		public bool HasDeviceInfo;

		// Token: 0x0400A192 RID: 41362
		private DeviceInfo _DeviceInfo;

		// Token: 0x0400A193 RID: 41363
		public bool HasTransactionId;

		// Token: 0x0400A194 RID: 41364
		private string _TransactionId;

		// Token: 0x0400A195 RID: 41365
		public bool HasProductId;

		// Token: 0x0400A196 RID: 41366
		private string _ProductId;

		// Token: 0x0400A197 RID: 41367
		public bool HasProvider;

		// Token: 0x0400A198 RID: 41368
		private string _Provider;

		// Token: 0x0400A199 RID: 41369
		public bool HasReason;

		// Token: 0x0400A19A RID: 41370
		private ThirdPartyPurchaseReceiptSubmitFail.FailureReason _Reason;

		// Token: 0x0400A19B RID: 41371
		public bool HasInvalidData;

		// Token: 0x0400A19C RID: 41372
		private string _InvalidData;

		// Token: 0x0200294D RID: 10573
		public enum FailureReason
		{
			// Token: 0x0400FC8E RID: 64654
			INVALID_STATE = 1,
			// Token: 0x0400FC8F RID: 64655
			INVALID_PROVIDER,
			// Token: 0x0400FC90 RID: 64656
			NO_ACTIVE_TRANSACTION,
			// Token: 0x0400FC91 RID: 64657
			NO_THIRD_PARTY_USER_ID
		}
	}
}
