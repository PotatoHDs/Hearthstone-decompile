using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011FB RID: 4603
	public class ThirdPartyPurchaseCompletedSuccess : IProtoBuf
	{
		// Token: 0x17001000 RID: 4096
		// (get) Token: 0x0600CE1B RID: 52763 RVA: 0x003D6EE3 File Offset: 0x003D50E3
		// (set) Token: 0x0600CE1C RID: 52764 RVA: 0x003D6EEB File Offset: 0x003D50EB
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

		// Token: 0x17001001 RID: 4097
		// (get) Token: 0x0600CE1D RID: 52765 RVA: 0x003D6EFE File Offset: 0x003D50FE
		// (set) Token: 0x0600CE1E RID: 52766 RVA: 0x003D6F06 File Offset: 0x003D5106
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

		// Token: 0x17001002 RID: 4098
		// (get) Token: 0x0600CE1F RID: 52767 RVA: 0x003D6F19 File Offset: 0x003D5119
		// (set) Token: 0x0600CE20 RID: 52768 RVA: 0x003D6F21 File Offset: 0x003D5121
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

		// Token: 0x17001003 RID: 4099
		// (get) Token: 0x0600CE21 RID: 52769 RVA: 0x003D6F34 File Offset: 0x003D5134
		// (set) Token: 0x0600CE22 RID: 52770 RVA: 0x003D6F3C File Offset: 0x003D513C
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

		// Token: 0x17001004 RID: 4100
		// (get) Token: 0x0600CE23 RID: 52771 RVA: 0x003D6F4F File Offset: 0x003D514F
		// (set) Token: 0x0600CE24 RID: 52772 RVA: 0x003D6F57 File Offset: 0x003D5157
		public string BpayProvider
		{
			get
			{
				return this._BpayProvider;
			}
			set
			{
				this._BpayProvider = value;
				this.HasBpayProvider = (value != null);
			}
		}

		// Token: 0x0600CE25 RID: 52773 RVA: 0x003D6F6C File Offset: 0x003D516C
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
			if (this.HasBpayProvider)
			{
				num ^= this.BpayProvider.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600CE26 RID: 52774 RVA: 0x003D6FF4 File Offset: 0x003D51F4
		public override bool Equals(object obj)
		{
			ThirdPartyPurchaseCompletedSuccess thirdPartyPurchaseCompletedSuccess = obj as ThirdPartyPurchaseCompletedSuccess;
			return thirdPartyPurchaseCompletedSuccess != null && this.HasPlayer == thirdPartyPurchaseCompletedSuccess.HasPlayer && (!this.HasPlayer || this.Player.Equals(thirdPartyPurchaseCompletedSuccess.Player)) && this.HasDeviceInfo == thirdPartyPurchaseCompletedSuccess.HasDeviceInfo && (!this.HasDeviceInfo || this.DeviceInfo.Equals(thirdPartyPurchaseCompletedSuccess.DeviceInfo)) && this.HasTransactionId == thirdPartyPurchaseCompletedSuccess.HasTransactionId && (!this.HasTransactionId || this.TransactionId.Equals(thirdPartyPurchaseCompletedSuccess.TransactionId)) && this.HasProductId == thirdPartyPurchaseCompletedSuccess.HasProductId && (!this.HasProductId || this.ProductId.Equals(thirdPartyPurchaseCompletedSuccess.ProductId)) && this.HasBpayProvider == thirdPartyPurchaseCompletedSuccess.HasBpayProvider && (!this.HasBpayProvider || this.BpayProvider.Equals(thirdPartyPurchaseCompletedSuccess.BpayProvider));
		}

		// Token: 0x0600CE27 RID: 52775 RVA: 0x003D70E5 File Offset: 0x003D52E5
		public void Deserialize(Stream stream)
		{
			ThirdPartyPurchaseCompletedSuccess.Deserialize(stream, this);
		}

		// Token: 0x0600CE28 RID: 52776 RVA: 0x003D70EF File Offset: 0x003D52EF
		public static ThirdPartyPurchaseCompletedSuccess Deserialize(Stream stream, ThirdPartyPurchaseCompletedSuccess instance)
		{
			return ThirdPartyPurchaseCompletedSuccess.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600CE29 RID: 52777 RVA: 0x003D70FC File Offset: 0x003D52FC
		public static ThirdPartyPurchaseCompletedSuccess DeserializeLengthDelimited(Stream stream)
		{
			ThirdPartyPurchaseCompletedSuccess thirdPartyPurchaseCompletedSuccess = new ThirdPartyPurchaseCompletedSuccess();
			ThirdPartyPurchaseCompletedSuccess.DeserializeLengthDelimited(stream, thirdPartyPurchaseCompletedSuccess);
			return thirdPartyPurchaseCompletedSuccess;
		}

		// Token: 0x0600CE2A RID: 52778 RVA: 0x003D7118 File Offset: 0x003D5318
		public static ThirdPartyPurchaseCompletedSuccess DeserializeLengthDelimited(Stream stream, ThirdPartyPurchaseCompletedSuccess instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ThirdPartyPurchaseCompletedSuccess.Deserialize(stream, instance, num);
		}

		// Token: 0x0600CE2B RID: 52779 RVA: 0x003D7140 File Offset: 0x003D5340
		public static ThirdPartyPurchaseCompletedSuccess Deserialize(Stream stream, ThirdPartyPurchaseCompletedSuccess instance, long limit)
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
				else
				{
					if (num <= 18)
					{
						if (num != 10)
						{
							if (num == 18)
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
					else
					{
						if (num == 26)
						{
							instance.TransactionId = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 34)
						{
							instance.ProductId = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 42)
						{
							instance.BpayProvider = ProtocolParser.ReadString(stream);
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

		// Token: 0x0600CE2C RID: 52780 RVA: 0x003D7264 File Offset: 0x003D5464
		public void Serialize(Stream stream)
		{
			ThirdPartyPurchaseCompletedSuccess.Serialize(stream, this);
		}

		// Token: 0x0600CE2D RID: 52781 RVA: 0x003D7270 File Offset: 0x003D5470
		public static void Serialize(Stream stream, ThirdPartyPurchaseCompletedSuccess instance)
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
			if (instance.HasBpayProvider)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.BpayProvider));
			}
		}

		// Token: 0x0600CE2E RID: 52782 RVA: 0x003D734C File Offset: 0x003D554C
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
			if (this.HasBpayProvider)
			{
				num += 1U;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(this.BpayProvider);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			return num;
		}

		// Token: 0x0400A149 RID: 41289
		public bool HasPlayer;

		// Token: 0x0400A14A RID: 41290
		private Player _Player;

		// Token: 0x0400A14B RID: 41291
		public bool HasDeviceInfo;

		// Token: 0x0400A14C RID: 41292
		private DeviceInfo _DeviceInfo;

		// Token: 0x0400A14D RID: 41293
		public bool HasTransactionId;

		// Token: 0x0400A14E RID: 41294
		private string _TransactionId;

		// Token: 0x0400A14F RID: 41295
		public bool HasProductId;

		// Token: 0x0400A150 RID: 41296
		private string _ProductId;

		// Token: 0x0400A151 RID: 41297
		public bool HasBpayProvider;

		// Token: 0x0400A152 RID: 41298
		private string _BpayProvider;
	}
}
