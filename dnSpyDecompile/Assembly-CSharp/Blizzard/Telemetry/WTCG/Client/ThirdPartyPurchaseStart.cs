using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x02001204 RID: 4612
	public class ThirdPartyPurchaseStart : IProtoBuf
	{
		// Token: 0x1700102A RID: 4138
		// (get) Token: 0x0600CED2 RID: 52946 RVA: 0x003D9BCA File Offset: 0x003D7DCA
		// (set) Token: 0x0600CED3 RID: 52947 RVA: 0x003D9BD2 File Offset: 0x003D7DD2
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

		// Token: 0x1700102B RID: 4139
		// (get) Token: 0x0600CED4 RID: 52948 RVA: 0x003D9BE5 File Offset: 0x003D7DE5
		// (set) Token: 0x0600CED5 RID: 52949 RVA: 0x003D9BED File Offset: 0x003D7DED
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

		// Token: 0x1700102C RID: 4140
		// (get) Token: 0x0600CED6 RID: 52950 RVA: 0x003D9C00 File Offset: 0x003D7E00
		// (set) Token: 0x0600CED7 RID: 52951 RVA: 0x003D9C08 File Offset: 0x003D7E08
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

		// Token: 0x1700102D RID: 4141
		// (get) Token: 0x0600CED8 RID: 52952 RVA: 0x003D9C1B File Offset: 0x003D7E1B
		// (set) Token: 0x0600CED9 RID: 52953 RVA: 0x003D9C23 File Offset: 0x003D7E23
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

		// Token: 0x1700102E RID: 4142
		// (get) Token: 0x0600CEDA RID: 52954 RVA: 0x003D9C36 File Offset: 0x003D7E36
		// (set) Token: 0x0600CEDB RID: 52955 RVA: 0x003D9C3E File Offset: 0x003D7E3E
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

		// Token: 0x0600CEDC RID: 52956 RVA: 0x003D9C54 File Offset: 0x003D7E54
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

		// Token: 0x0600CEDD RID: 52957 RVA: 0x003D9CDC File Offset: 0x003D7EDC
		public override bool Equals(object obj)
		{
			ThirdPartyPurchaseStart thirdPartyPurchaseStart = obj as ThirdPartyPurchaseStart;
			return thirdPartyPurchaseStart != null && this.HasPlayer == thirdPartyPurchaseStart.HasPlayer && (!this.HasPlayer || this.Player.Equals(thirdPartyPurchaseStart.Player)) && this.HasDeviceInfo == thirdPartyPurchaseStart.HasDeviceInfo && (!this.HasDeviceInfo || this.DeviceInfo.Equals(thirdPartyPurchaseStart.DeviceInfo)) && this.HasTransactionId == thirdPartyPurchaseStart.HasTransactionId && (!this.HasTransactionId || this.TransactionId.Equals(thirdPartyPurchaseStart.TransactionId)) && this.HasProductId == thirdPartyPurchaseStart.HasProductId && (!this.HasProductId || this.ProductId.Equals(thirdPartyPurchaseStart.ProductId)) && this.HasBpayProvider == thirdPartyPurchaseStart.HasBpayProvider && (!this.HasBpayProvider || this.BpayProvider.Equals(thirdPartyPurchaseStart.BpayProvider));
		}

		// Token: 0x0600CEDE RID: 52958 RVA: 0x003D9DCD File Offset: 0x003D7FCD
		public void Deserialize(Stream stream)
		{
			ThirdPartyPurchaseStart.Deserialize(stream, this);
		}

		// Token: 0x0600CEDF RID: 52959 RVA: 0x003D9DD7 File Offset: 0x003D7FD7
		public static ThirdPartyPurchaseStart Deserialize(Stream stream, ThirdPartyPurchaseStart instance)
		{
			return ThirdPartyPurchaseStart.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600CEE0 RID: 52960 RVA: 0x003D9DE4 File Offset: 0x003D7FE4
		public static ThirdPartyPurchaseStart DeserializeLengthDelimited(Stream stream)
		{
			ThirdPartyPurchaseStart thirdPartyPurchaseStart = new ThirdPartyPurchaseStart();
			ThirdPartyPurchaseStart.DeserializeLengthDelimited(stream, thirdPartyPurchaseStart);
			return thirdPartyPurchaseStart;
		}

		// Token: 0x0600CEE1 RID: 52961 RVA: 0x003D9E00 File Offset: 0x003D8000
		public static ThirdPartyPurchaseStart DeserializeLengthDelimited(Stream stream, ThirdPartyPurchaseStart instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ThirdPartyPurchaseStart.Deserialize(stream, instance, num);
		}

		// Token: 0x0600CEE2 RID: 52962 RVA: 0x003D9E28 File Offset: 0x003D8028
		public static ThirdPartyPurchaseStart Deserialize(Stream stream, ThirdPartyPurchaseStart instance, long limit)
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

		// Token: 0x0600CEE3 RID: 52963 RVA: 0x003D9F4C File Offset: 0x003D814C
		public void Serialize(Stream stream)
		{
			ThirdPartyPurchaseStart.Serialize(stream, this);
		}

		// Token: 0x0600CEE4 RID: 52964 RVA: 0x003D9F58 File Offset: 0x003D8158
		public static void Serialize(Stream stream, ThirdPartyPurchaseStart instance)
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

		// Token: 0x0600CEE5 RID: 52965 RVA: 0x003DA034 File Offset: 0x003D8234
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

		// Token: 0x0400A19D RID: 41373
		public bool HasPlayer;

		// Token: 0x0400A19E RID: 41374
		private Player _Player;

		// Token: 0x0400A19F RID: 41375
		public bool HasDeviceInfo;

		// Token: 0x0400A1A0 RID: 41376
		private DeviceInfo _DeviceInfo;

		// Token: 0x0400A1A1 RID: 41377
		public bool HasTransactionId;

		// Token: 0x0400A1A2 RID: 41378
		private string _TransactionId;

		// Token: 0x0400A1A3 RID: 41379
		public bool HasProductId;

		// Token: 0x0400A1A4 RID: 41380
		private string _ProductId;

		// Token: 0x0400A1A5 RID: 41381
		public bool HasBpayProvider;

		// Token: 0x0400A1A6 RID: 41382
		private string _BpayProvider;
	}
}
