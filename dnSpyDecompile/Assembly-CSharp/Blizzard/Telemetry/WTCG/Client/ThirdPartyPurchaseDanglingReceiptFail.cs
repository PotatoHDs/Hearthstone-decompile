using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011FC RID: 4604
	public class ThirdPartyPurchaseDanglingReceiptFail : IProtoBuf
	{
		// Token: 0x17001005 RID: 4101
		// (get) Token: 0x0600CE30 RID: 52784 RVA: 0x003D7420 File Offset: 0x003D5620
		// (set) Token: 0x0600CE31 RID: 52785 RVA: 0x003D7428 File Offset: 0x003D5628
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

		// Token: 0x17001006 RID: 4102
		// (get) Token: 0x0600CE32 RID: 52786 RVA: 0x003D743B File Offset: 0x003D563B
		// (set) Token: 0x0600CE33 RID: 52787 RVA: 0x003D7443 File Offset: 0x003D5643
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

		// Token: 0x17001007 RID: 4103
		// (get) Token: 0x0600CE34 RID: 52788 RVA: 0x003D7456 File Offset: 0x003D5656
		// (set) Token: 0x0600CE35 RID: 52789 RVA: 0x003D745E File Offset: 0x003D565E
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

		// Token: 0x17001008 RID: 4104
		// (get) Token: 0x0600CE36 RID: 52790 RVA: 0x003D7471 File Offset: 0x003D5671
		// (set) Token: 0x0600CE37 RID: 52791 RVA: 0x003D7479 File Offset: 0x003D5679
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

		// Token: 0x17001009 RID: 4105
		// (get) Token: 0x0600CE38 RID: 52792 RVA: 0x003D748C File Offset: 0x003D568C
		// (set) Token: 0x0600CE39 RID: 52793 RVA: 0x003D7494 File Offset: 0x003D5694
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

		// Token: 0x1700100A RID: 4106
		// (get) Token: 0x0600CE3A RID: 52794 RVA: 0x003D74A7 File Offset: 0x003D56A7
		// (set) Token: 0x0600CE3B RID: 52795 RVA: 0x003D74AF File Offset: 0x003D56AF
		public ThirdPartyPurchaseDanglingReceiptFail.FailureReason Reason
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

		// Token: 0x1700100B RID: 4107
		// (get) Token: 0x0600CE3C RID: 52796 RVA: 0x003D74BF File Offset: 0x003D56BF
		// (set) Token: 0x0600CE3D RID: 52797 RVA: 0x003D74C7 File Offset: 0x003D56C7
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

		// Token: 0x0600CE3E RID: 52798 RVA: 0x003D74DC File Offset: 0x003D56DC
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

		// Token: 0x0600CE3F RID: 52799 RVA: 0x003D759C File Offset: 0x003D579C
		public override bool Equals(object obj)
		{
			ThirdPartyPurchaseDanglingReceiptFail thirdPartyPurchaseDanglingReceiptFail = obj as ThirdPartyPurchaseDanglingReceiptFail;
			return thirdPartyPurchaseDanglingReceiptFail != null && this.HasPlayer == thirdPartyPurchaseDanglingReceiptFail.HasPlayer && (!this.HasPlayer || this.Player.Equals(thirdPartyPurchaseDanglingReceiptFail.Player)) && this.HasDeviceInfo == thirdPartyPurchaseDanglingReceiptFail.HasDeviceInfo && (!this.HasDeviceInfo || this.DeviceInfo.Equals(thirdPartyPurchaseDanglingReceiptFail.DeviceInfo)) && this.HasTransactionId == thirdPartyPurchaseDanglingReceiptFail.HasTransactionId && (!this.HasTransactionId || this.TransactionId.Equals(thirdPartyPurchaseDanglingReceiptFail.TransactionId)) && this.HasProductId == thirdPartyPurchaseDanglingReceiptFail.HasProductId && (!this.HasProductId || this.ProductId.Equals(thirdPartyPurchaseDanglingReceiptFail.ProductId)) && this.HasProvider == thirdPartyPurchaseDanglingReceiptFail.HasProvider && (!this.HasProvider || this.Provider.Equals(thirdPartyPurchaseDanglingReceiptFail.Provider)) && this.HasReason == thirdPartyPurchaseDanglingReceiptFail.HasReason && (!this.HasReason || this.Reason.Equals(thirdPartyPurchaseDanglingReceiptFail.Reason)) && this.HasInvalidData == thirdPartyPurchaseDanglingReceiptFail.HasInvalidData && (!this.HasInvalidData || this.InvalidData.Equals(thirdPartyPurchaseDanglingReceiptFail.InvalidData));
		}

		// Token: 0x0600CE40 RID: 52800 RVA: 0x003D76F1 File Offset: 0x003D58F1
		public void Deserialize(Stream stream)
		{
			ThirdPartyPurchaseDanglingReceiptFail.Deserialize(stream, this);
		}

		// Token: 0x0600CE41 RID: 52801 RVA: 0x003D76FB File Offset: 0x003D58FB
		public static ThirdPartyPurchaseDanglingReceiptFail Deserialize(Stream stream, ThirdPartyPurchaseDanglingReceiptFail instance)
		{
			return ThirdPartyPurchaseDanglingReceiptFail.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600CE42 RID: 52802 RVA: 0x003D7708 File Offset: 0x003D5908
		public static ThirdPartyPurchaseDanglingReceiptFail DeserializeLengthDelimited(Stream stream)
		{
			ThirdPartyPurchaseDanglingReceiptFail thirdPartyPurchaseDanglingReceiptFail = new ThirdPartyPurchaseDanglingReceiptFail();
			ThirdPartyPurchaseDanglingReceiptFail.DeserializeLengthDelimited(stream, thirdPartyPurchaseDanglingReceiptFail);
			return thirdPartyPurchaseDanglingReceiptFail;
		}

		// Token: 0x0600CE43 RID: 52803 RVA: 0x003D7724 File Offset: 0x003D5924
		public static ThirdPartyPurchaseDanglingReceiptFail DeserializeLengthDelimited(Stream stream, ThirdPartyPurchaseDanglingReceiptFail instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ThirdPartyPurchaseDanglingReceiptFail.Deserialize(stream, instance, num);
		}

		// Token: 0x0600CE44 RID: 52804 RVA: 0x003D774C File Offset: 0x003D594C
		public static ThirdPartyPurchaseDanglingReceiptFail Deserialize(Stream stream, ThirdPartyPurchaseDanglingReceiptFail instance, long limit)
		{
			instance.Reason = ThirdPartyPurchaseDanglingReceiptFail.FailureReason.INVALID_STATE;
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
							instance.Reason = (ThirdPartyPurchaseDanglingReceiptFail.FailureReason)ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x0600CE45 RID: 52805 RVA: 0x003D78C0 File Offset: 0x003D5AC0
		public void Serialize(Stream stream)
		{
			ThirdPartyPurchaseDanglingReceiptFail.Serialize(stream, this);
		}

		// Token: 0x0600CE46 RID: 52806 RVA: 0x003D78CC File Offset: 0x003D5ACC
		public static void Serialize(Stream stream, ThirdPartyPurchaseDanglingReceiptFail instance)
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

		// Token: 0x0600CE47 RID: 52807 RVA: 0x003D79E8 File Offset: 0x003D5BE8
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

		// Token: 0x0400A153 RID: 41299
		public bool HasPlayer;

		// Token: 0x0400A154 RID: 41300
		private Player _Player;

		// Token: 0x0400A155 RID: 41301
		public bool HasDeviceInfo;

		// Token: 0x0400A156 RID: 41302
		private DeviceInfo _DeviceInfo;

		// Token: 0x0400A157 RID: 41303
		public bool HasTransactionId;

		// Token: 0x0400A158 RID: 41304
		private string _TransactionId;

		// Token: 0x0400A159 RID: 41305
		public bool HasProductId;

		// Token: 0x0400A15A RID: 41306
		private string _ProductId;

		// Token: 0x0400A15B RID: 41307
		public bool HasProvider;

		// Token: 0x0400A15C RID: 41308
		private string _Provider;

		// Token: 0x0400A15D RID: 41309
		public bool HasReason;

		// Token: 0x0400A15E RID: 41310
		private ThirdPartyPurchaseDanglingReceiptFail.FailureReason _Reason;

		// Token: 0x0400A15F RID: 41311
		public bool HasInvalidData;

		// Token: 0x0400A160 RID: 41312
		private string _InvalidData;

		// Token: 0x0200294C RID: 10572
		public enum FailureReason
		{
			// Token: 0x0400FC8B RID: 64651
			INVALID_STATE = 1,
			// Token: 0x0400FC8C RID: 64652
			NO_THIRD_PARTY_USER_ID
		}
	}
}
