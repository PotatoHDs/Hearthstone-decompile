using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x02001202 RID: 4610
	public class ThirdPartyPurchaseReceiptRequest : IProtoBuf
	{
		// Token: 0x1700101F RID: 4127
		// (get) Token: 0x0600CEA6 RID: 52902 RVA: 0x003D9075 File Offset: 0x003D7275
		// (set) Token: 0x0600CEA7 RID: 52903 RVA: 0x003D907D File Offset: 0x003D727D
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

		// Token: 0x17001020 RID: 4128
		// (get) Token: 0x0600CEA8 RID: 52904 RVA: 0x003D9090 File Offset: 0x003D7290
		// (set) Token: 0x0600CEA9 RID: 52905 RVA: 0x003D9098 File Offset: 0x003D7298
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

		// Token: 0x17001021 RID: 4129
		// (get) Token: 0x0600CEAA RID: 52906 RVA: 0x003D90AB File Offset: 0x003D72AB
		// (set) Token: 0x0600CEAB RID: 52907 RVA: 0x003D90B3 File Offset: 0x003D72B3
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

		// Token: 0x17001022 RID: 4130
		// (get) Token: 0x0600CEAC RID: 52908 RVA: 0x003D90C6 File Offset: 0x003D72C6
		// (set) Token: 0x0600CEAD RID: 52909 RVA: 0x003D90CE File Offset: 0x003D72CE
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

		// Token: 0x0600CEAE RID: 52910 RVA: 0x003D90E4 File Offset: 0x003D72E4
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
			return num;
		}

		// Token: 0x0600CEAF RID: 52911 RVA: 0x003D9158 File Offset: 0x003D7358
		public override bool Equals(object obj)
		{
			ThirdPartyPurchaseReceiptRequest thirdPartyPurchaseReceiptRequest = obj as ThirdPartyPurchaseReceiptRequest;
			return thirdPartyPurchaseReceiptRequest != null && this.HasPlayer == thirdPartyPurchaseReceiptRequest.HasPlayer && (!this.HasPlayer || this.Player.Equals(thirdPartyPurchaseReceiptRequest.Player)) && this.HasDeviceInfo == thirdPartyPurchaseReceiptRequest.HasDeviceInfo && (!this.HasDeviceInfo || this.DeviceInfo.Equals(thirdPartyPurchaseReceiptRequest.DeviceInfo)) && this.HasTransactionId == thirdPartyPurchaseReceiptRequest.HasTransactionId && (!this.HasTransactionId || this.TransactionId.Equals(thirdPartyPurchaseReceiptRequest.TransactionId)) && this.HasProductId == thirdPartyPurchaseReceiptRequest.HasProductId && (!this.HasProductId || this.ProductId.Equals(thirdPartyPurchaseReceiptRequest.ProductId));
		}

		// Token: 0x0600CEB0 RID: 52912 RVA: 0x003D921E File Offset: 0x003D741E
		public void Deserialize(Stream stream)
		{
			ThirdPartyPurchaseReceiptRequest.Deserialize(stream, this);
		}

		// Token: 0x0600CEB1 RID: 52913 RVA: 0x003D9228 File Offset: 0x003D7428
		public static ThirdPartyPurchaseReceiptRequest Deserialize(Stream stream, ThirdPartyPurchaseReceiptRequest instance)
		{
			return ThirdPartyPurchaseReceiptRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600CEB2 RID: 52914 RVA: 0x003D9234 File Offset: 0x003D7434
		public static ThirdPartyPurchaseReceiptRequest DeserializeLengthDelimited(Stream stream)
		{
			ThirdPartyPurchaseReceiptRequest thirdPartyPurchaseReceiptRequest = new ThirdPartyPurchaseReceiptRequest();
			ThirdPartyPurchaseReceiptRequest.DeserializeLengthDelimited(stream, thirdPartyPurchaseReceiptRequest);
			return thirdPartyPurchaseReceiptRequest;
		}

		// Token: 0x0600CEB3 RID: 52915 RVA: 0x003D9250 File Offset: 0x003D7450
		public static ThirdPartyPurchaseReceiptRequest DeserializeLengthDelimited(Stream stream, ThirdPartyPurchaseReceiptRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ThirdPartyPurchaseReceiptRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x0600CEB4 RID: 52916 RVA: 0x003D9278 File Offset: 0x003D7478
		public static ThirdPartyPurchaseReceiptRequest Deserialize(Stream stream, ThirdPartyPurchaseReceiptRequest instance, long limit)
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

		// Token: 0x0600CEB5 RID: 52917 RVA: 0x003D9383 File Offset: 0x003D7583
		public void Serialize(Stream stream)
		{
			ThirdPartyPurchaseReceiptRequest.Serialize(stream, this);
		}

		// Token: 0x0600CEB6 RID: 52918 RVA: 0x003D938C File Offset: 0x003D758C
		public static void Serialize(Stream stream, ThirdPartyPurchaseReceiptRequest instance)
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
		}

		// Token: 0x0600CEB7 RID: 52919 RVA: 0x003D9440 File Offset: 0x003D7640
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
			return num;
		}

		// Token: 0x0400A187 RID: 41351
		public bool HasPlayer;

		// Token: 0x0400A188 RID: 41352
		private Player _Player;

		// Token: 0x0400A189 RID: 41353
		public bool HasDeviceInfo;

		// Token: 0x0400A18A RID: 41354
		private DeviceInfo _DeviceInfo;

		// Token: 0x0400A18B RID: 41355
		public bool HasTransactionId;

		// Token: 0x0400A18C RID: 41356
		private string _TransactionId;

		// Token: 0x0400A18D RID: 41357
		public bool HasProductId;

		// Token: 0x0400A18E RID: 41358
		private string _ProductId;
	}
}
