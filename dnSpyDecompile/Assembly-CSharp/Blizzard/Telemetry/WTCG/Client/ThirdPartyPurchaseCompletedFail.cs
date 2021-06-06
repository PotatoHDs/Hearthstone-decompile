using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011FA RID: 4602
	public class ThirdPartyPurchaseCompletedFail : IProtoBuf
	{
		// Token: 0x17000FFA RID: 4090
		// (get) Token: 0x0600CE04 RID: 52740 RVA: 0x003D68DB File Offset: 0x003D4ADB
		// (set) Token: 0x0600CE05 RID: 52741 RVA: 0x003D68E3 File Offset: 0x003D4AE3
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

		// Token: 0x17000FFB RID: 4091
		// (get) Token: 0x0600CE06 RID: 52742 RVA: 0x003D68F6 File Offset: 0x003D4AF6
		// (set) Token: 0x0600CE07 RID: 52743 RVA: 0x003D68FE File Offset: 0x003D4AFE
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

		// Token: 0x17000FFC RID: 4092
		// (get) Token: 0x0600CE08 RID: 52744 RVA: 0x003D6911 File Offset: 0x003D4B11
		// (set) Token: 0x0600CE09 RID: 52745 RVA: 0x003D6919 File Offset: 0x003D4B19
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

		// Token: 0x17000FFD RID: 4093
		// (get) Token: 0x0600CE0A RID: 52746 RVA: 0x003D692C File Offset: 0x003D4B2C
		// (set) Token: 0x0600CE0B RID: 52747 RVA: 0x003D6934 File Offset: 0x003D4B34
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

		// Token: 0x17000FFE RID: 4094
		// (get) Token: 0x0600CE0C RID: 52748 RVA: 0x003D6947 File Offset: 0x003D4B47
		// (set) Token: 0x0600CE0D RID: 52749 RVA: 0x003D694F File Offset: 0x003D4B4F
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

		// Token: 0x17000FFF RID: 4095
		// (get) Token: 0x0600CE0E RID: 52750 RVA: 0x003D6962 File Offset: 0x003D4B62
		// (set) Token: 0x0600CE0F RID: 52751 RVA: 0x003D696A File Offset: 0x003D4B6A
		public string ErrorInfo
		{
			get
			{
				return this._ErrorInfo;
			}
			set
			{
				this._ErrorInfo = value;
				this.HasErrorInfo = (value != null);
			}
		}

		// Token: 0x0600CE10 RID: 52752 RVA: 0x003D6980 File Offset: 0x003D4B80
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
			if (this.HasErrorInfo)
			{
				num ^= this.ErrorInfo.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600CE11 RID: 52753 RVA: 0x003D6A20 File Offset: 0x003D4C20
		public override bool Equals(object obj)
		{
			ThirdPartyPurchaseCompletedFail thirdPartyPurchaseCompletedFail = obj as ThirdPartyPurchaseCompletedFail;
			return thirdPartyPurchaseCompletedFail != null && this.HasPlayer == thirdPartyPurchaseCompletedFail.HasPlayer && (!this.HasPlayer || this.Player.Equals(thirdPartyPurchaseCompletedFail.Player)) && this.HasDeviceInfo == thirdPartyPurchaseCompletedFail.HasDeviceInfo && (!this.HasDeviceInfo || this.DeviceInfo.Equals(thirdPartyPurchaseCompletedFail.DeviceInfo)) && this.HasTransactionId == thirdPartyPurchaseCompletedFail.HasTransactionId && (!this.HasTransactionId || this.TransactionId.Equals(thirdPartyPurchaseCompletedFail.TransactionId)) && this.HasProductId == thirdPartyPurchaseCompletedFail.HasProductId && (!this.HasProductId || this.ProductId.Equals(thirdPartyPurchaseCompletedFail.ProductId)) && this.HasBpayProvider == thirdPartyPurchaseCompletedFail.HasBpayProvider && (!this.HasBpayProvider || this.BpayProvider.Equals(thirdPartyPurchaseCompletedFail.BpayProvider)) && this.HasErrorInfo == thirdPartyPurchaseCompletedFail.HasErrorInfo && (!this.HasErrorInfo || this.ErrorInfo.Equals(thirdPartyPurchaseCompletedFail.ErrorInfo));
		}

		// Token: 0x0600CE12 RID: 52754 RVA: 0x003D6B3C File Offset: 0x003D4D3C
		public void Deserialize(Stream stream)
		{
			ThirdPartyPurchaseCompletedFail.Deserialize(stream, this);
		}

		// Token: 0x0600CE13 RID: 52755 RVA: 0x003D6B46 File Offset: 0x003D4D46
		public static ThirdPartyPurchaseCompletedFail Deserialize(Stream stream, ThirdPartyPurchaseCompletedFail instance)
		{
			return ThirdPartyPurchaseCompletedFail.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600CE14 RID: 52756 RVA: 0x003D6B54 File Offset: 0x003D4D54
		public static ThirdPartyPurchaseCompletedFail DeserializeLengthDelimited(Stream stream)
		{
			ThirdPartyPurchaseCompletedFail thirdPartyPurchaseCompletedFail = new ThirdPartyPurchaseCompletedFail();
			ThirdPartyPurchaseCompletedFail.DeserializeLengthDelimited(stream, thirdPartyPurchaseCompletedFail);
			return thirdPartyPurchaseCompletedFail;
		}

		// Token: 0x0600CE15 RID: 52757 RVA: 0x003D6B70 File Offset: 0x003D4D70
		public static ThirdPartyPurchaseCompletedFail DeserializeLengthDelimited(Stream stream, ThirdPartyPurchaseCompletedFail instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ThirdPartyPurchaseCompletedFail.Deserialize(stream, instance, num);
		}

		// Token: 0x0600CE16 RID: 52758 RVA: 0x003D6B98 File Offset: 0x003D4D98
		public static ThirdPartyPurchaseCompletedFail Deserialize(Stream stream, ThirdPartyPurchaseCompletedFail instance, long limit)
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
					else
					{
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
						if (num == 50)
						{
							instance.ErrorInfo = ProtocolParser.ReadString(stream);
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

		// Token: 0x0600CE17 RID: 52759 RVA: 0x003D6CDB File Offset: 0x003D4EDB
		public void Serialize(Stream stream)
		{
			ThirdPartyPurchaseCompletedFail.Serialize(stream, this);
		}

		// Token: 0x0600CE18 RID: 52760 RVA: 0x003D6CE4 File Offset: 0x003D4EE4
		public static void Serialize(Stream stream, ThirdPartyPurchaseCompletedFail instance)
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
			if (instance.HasErrorInfo)
			{
				stream.WriteByte(50);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ErrorInfo));
			}
		}

		// Token: 0x0600CE19 RID: 52761 RVA: 0x003D6DE4 File Offset: 0x003D4FE4
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
			if (this.HasErrorInfo)
			{
				num += 1U;
				uint byteCount4 = (uint)Encoding.UTF8.GetByteCount(this.ErrorInfo);
				num += ProtocolParser.SizeOfUInt32(byteCount4) + byteCount4;
			}
			return num;
		}

		// Token: 0x0400A13D RID: 41277
		public bool HasPlayer;

		// Token: 0x0400A13E RID: 41278
		private Player _Player;

		// Token: 0x0400A13F RID: 41279
		public bool HasDeviceInfo;

		// Token: 0x0400A140 RID: 41280
		private DeviceInfo _DeviceInfo;

		// Token: 0x0400A141 RID: 41281
		public bool HasTransactionId;

		// Token: 0x0400A142 RID: 41282
		private string _TransactionId;

		// Token: 0x0400A143 RID: 41283
		public bool HasProductId;

		// Token: 0x0400A144 RID: 41284
		private string _ProductId;

		// Token: 0x0400A145 RID: 41285
		public bool HasBpayProvider;

		// Token: 0x0400A146 RID: 41286
		private string _BpayProvider;

		// Token: 0x0400A147 RID: 41287
		public bool HasErrorInfo;

		// Token: 0x0400A148 RID: 41288
		private string _ErrorInfo;
	}
}
