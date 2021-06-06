using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011A6 RID: 4518
	public class BlizzardCheckoutPurchaseStart : IProtoBuf
	{
		// Token: 0x17000EB6 RID: 3766
		// (get) Token: 0x0600C7E0 RID: 51168 RVA: 0x003C0DB4 File Offset: 0x003BEFB4
		// (set) Token: 0x0600C7E1 RID: 51169 RVA: 0x003C0DBC File Offset: 0x003BEFBC
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

		// Token: 0x17000EB7 RID: 3767
		// (get) Token: 0x0600C7E2 RID: 51170 RVA: 0x003C0DCF File Offset: 0x003BEFCF
		// (set) Token: 0x0600C7E3 RID: 51171 RVA: 0x003C0DD7 File Offset: 0x003BEFD7
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

		// Token: 0x17000EB8 RID: 3768
		// (get) Token: 0x0600C7E4 RID: 51172 RVA: 0x003C0DEA File Offset: 0x003BEFEA
		// (set) Token: 0x0600C7E5 RID: 51173 RVA: 0x003C0DF2 File Offset: 0x003BEFF2
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

		// Token: 0x17000EB9 RID: 3769
		// (get) Token: 0x0600C7E6 RID: 51174 RVA: 0x003C0E05 File Offset: 0x003BF005
		// (set) Token: 0x0600C7E7 RID: 51175 RVA: 0x003C0E0D File Offset: 0x003BF00D
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

		// Token: 0x17000EBA RID: 3770
		// (get) Token: 0x0600C7E8 RID: 51176 RVA: 0x003C0E20 File Offset: 0x003BF020
		// (set) Token: 0x0600C7E9 RID: 51177 RVA: 0x003C0E28 File Offset: 0x003BF028
		public string Currency
		{
			get
			{
				return this._Currency;
			}
			set
			{
				this._Currency = value;
				this.HasCurrency = (value != null);
			}
		}

		// Token: 0x0600C7EA RID: 51178 RVA: 0x003C0E3C File Offset: 0x003BF03C
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
			if (this.HasCurrency)
			{
				num ^= this.Currency.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600C7EB RID: 51179 RVA: 0x003C0EC4 File Offset: 0x003BF0C4
		public override bool Equals(object obj)
		{
			BlizzardCheckoutPurchaseStart blizzardCheckoutPurchaseStart = obj as BlizzardCheckoutPurchaseStart;
			return blizzardCheckoutPurchaseStart != null && this.HasPlayer == blizzardCheckoutPurchaseStart.HasPlayer && (!this.HasPlayer || this.Player.Equals(blizzardCheckoutPurchaseStart.Player)) && this.HasDeviceInfo == blizzardCheckoutPurchaseStart.HasDeviceInfo && (!this.HasDeviceInfo || this.DeviceInfo.Equals(blizzardCheckoutPurchaseStart.DeviceInfo)) && this.HasTransactionId == blizzardCheckoutPurchaseStart.HasTransactionId && (!this.HasTransactionId || this.TransactionId.Equals(blizzardCheckoutPurchaseStart.TransactionId)) && this.HasProductId == blizzardCheckoutPurchaseStart.HasProductId && (!this.HasProductId || this.ProductId.Equals(blizzardCheckoutPurchaseStart.ProductId)) && this.HasCurrency == blizzardCheckoutPurchaseStart.HasCurrency && (!this.HasCurrency || this.Currency.Equals(blizzardCheckoutPurchaseStart.Currency));
		}

		// Token: 0x0600C7EC RID: 51180 RVA: 0x003C0FB5 File Offset: 0x003BF1B5
		public void Deserialize(Stream stream)
		{
			BlizzardCheckoutPurchaseStart.Deserialize(stream, this);
		}

		// Token: 0x0600C7ED RID: 51181 RVA: 0x003C0FBF File Offset: 0x003BF1BF
		public static BlizzardCheckoutPurchaseStart Deserialize(Stream stream, BlizzardCheckoutPurchaseStart instance)
		{
			return BlizzardCheckoutPurchaseStart.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600C7EE RID: 51182 RVA: 0x003C0FCC File Offset: 0x003BF1CC
		public static BlizzardCheckoutPurchaseStart DeserializeLengthDelimited(Stream stream)
		{
			BlizzardCheckoutPurchaseStart blizzardCheckoutPurchaseStart = new BlizzardCheckoutPurchaseStart();
			BlizzardCheckoutPurchaseStart.DeserializeLengthDelimited(stream, blizzardCheckoutPurchaseStart);
			return blizzardCheckoutPurchaseStart;
		}

		// Token: 0x0600C7EF RID: 51183 RVA: 0x003C0FE8 File Offset: 0x003BF1E8
		public static BlizzardCheckoutPurchaseStart DeserializeLengthDelimited(Stream stream, BlizzardCheckoutPurchaseStart instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return BlizzardCheckoutPurchaseStart.Deserialize(stream, instance, num);
		}

		// Token: 0x0600C7F0 RID: 51184 RVA: 0x003C1010 File Offset: 0x003BF210
		public static BlizzardCheckoutPurchaseStart Deserialize(Stream stream, BlizzardCheckoutPurchaseStart instance, long limit)
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
							instance.Currency = ProtocolParser.ReadString(stream);
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

		// Token: 0x0600C7F1 RID: 51185 RVA: 0x003C1134 File Offset: 0x003BF334
		public void Serialize(Stream stream)
		{
			BlizzardCheckoutPurchaseStart.Serialize(stream, this);
		}

		// Token: 0x0600C7F2 RID: 51186 RVA: 0x003C1140 File Offset: 0x003BF340
		public static void Serialize(Stream stream, BlizzardCheckoutPurchaseStart instance)
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
			if (instance.HasCurrency)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Currency));
			}
		}

		// Token: 0x0600C7F3 RID: 51187 RVA: 0x003C121C File Offset: 0x003BF41C
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
			if (this.HasCurrency)
			{
				num += 1U;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(this.Currency);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			return num;
		}

		// Token: 0x04009EB7 RID: 40631
		public bool HasPlayer;

		// Token: 0x04009EB8 RID: 40632
		private Player _Player;

		// Token: 0x04009EB9 RID: 40633
		public bool HasDeviceInfo;

		// Token: 0x04009EBA RID: 40634
		private DeviceInfo _DeviceInfo;

		// Token: 0x04009EBB RID: 40635
		public bool HasTransactionId;

		// Token: 0x04009EBC RID: 40636
		private string _TransactionId;

		// Token: 0x04009EBD RID: 40637
		public bool HasProductId;

		// Token: 0x04009EBE RID: 40638
		private string _ProductId;

		// Token: 0x04009EBF RID: 40639
		public bool HasCurrency;

		// Token: 0x04009EC0 RID: 40640
		private string _Currency;
	}
}
