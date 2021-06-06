using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x02001201 RID: 4609
	public class ThirdPartyPurchaseReceiptReceived : IProtoBuf
	{
		// Token: 0x1700101B RID: 4123
		// (get) Token: 0x0600CE93 RID: 52883 RVA: 0x003D8C01 File Offset: 0x003D6E01
		// (set) Token: 0x0600CE94 RID: 52884 RVA: 0x003D8C09 File Offset: 0x003D6E09
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

		// Token: 0x1700101C RID: 4124
		// (get) Token: 0x0600CE95 RID: 52885 RVA: 0x003D8C1C File Offset: 0x003D6E1C
		// (set) Token: 0x0600CE96 RID: 52886 RVA: 0x003D8C24 File Offset: 0x003D6E24
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

		// Token: 0x1700101D RID: 4125
		// (get) Token: 0x0600CE97 RID: 52887 RVA: 0x003D8C37 File Offset: 0x003D6E37
		// (set) Token: 0x0600CE98 RID: 52888 RVA: 0x003D8C3F File Offset: 0x003D6E3F
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

		// Token: 0x1700101E RID: 4126
		// (get) Token: 0x0600CE99 RID: 52889 RVA: 0x003D8C52 File Offset: 0x003D6E52
		// (set) Token: 0x0600CE9A RID: 52890 RVA: 0x003D8C5A File Offset: 0x003D6E5A
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

		// Token: 0x0600CE9B RID: 52891 RVA: 0x003D8C70 File Offset: 0x003D6E70
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

		// Token: 0x0600CE9C RID: 52892 RVA: 0x003D8CE4 File Offset: 0x003D6EE4
		public override bool Equals(object obj)
		{
			ThirdPartyPurchaseReceiptReceived thirdPartyPurchaseReceiptReceived = obj as ThirdPartyPurchaseReceiptReceived;
			return thirdPartyPurchaseReceiptReceived != null && this.HasPlayer == thirdPartyPurchaseReceiptReceived.HasPlayer && (!this.HasPlayer || this.Player.Equals(thirdPartyPurchaseReceiptReceived.Player)) && this.HasDeviceInfo == thirdPartyPurchaseReceiptReceived.HasDeviceInfo && (!this.HasDeviceInfo || this.DeviceInfo.Equals(thirdPartyPurchaseReceiptReceived.DeviceInfo)) && this.HasTransactionId == thirdPartyPurchaseReceiptReceived.HasTransactionId && (!this.HasTransactionId || this.TransactionId.Equals(thirdPartyPurchaseReceiptReceived.TransactionId)) && this.HasProductId == thirdPartyPurchaseReceiptReceived.HasProductId && (!this.HasProductId || this.ProductId.Equals(thirdPartyPurchaseReceiptReceived.ProductId));
		}

		// Token: 0x0600CE9D RID: 52893 RVA: 0x003D8DAA File Offset: 0x003D6FAA
		public void Deserialize(Stream stream)
		{
			ThirdPartyPurchaseReceiptReceived.Deserialize(stream, this);
		}

		// Token: 0x0600CE9E RID: 52894 RVA: 0x003D8DB4 File Offset: 0x003D6FB4
		public static ThirdPartyPurchaseReceiptReceived Deserialize(Stream stream, ThirdPartyPurchaseReceiptReceived instance)
		{
			return ThirdPartyPurchaseReceiptReceived.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600CE9F RID: 52895 RVA: 0x003D8DC0 File Offset: 0x003D6FC0
		public static ThirdPartyPurchaseReceiptReceived DeserializeLengthDelimited(Stream stream)
		{
			ThirdPartyPurchaseReceiptReceived thirdPartyPurchaseReceiptReceived = new ThirdPartyPurchaseReceiptReceived();
			ThirdPartyPurchaseReceiptReceived.DeserializeLengthDelimited(stream, thirdPartyPurchaseReceiptReceived);
			return thirdPartyPurchaseReceiptReceived;
		}

		// Token: 0x0600CEA0 RID: 52896 RVA: 0x003D8DDC File Offset: 0x003D6FDC
		public static ThirdPartyPurchaseReceiptReceived DeserializeLengthDelimited(Stream stream, ThirdPartyPurchaseReceiptReceived instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ThirdPartyPurchaseReceiptReceived.Deserialize(stream, instance, num);
		}

		// Token: 0x0600CEA1 RID: 52897 RVA: 0x003D8E04 File Offset: 0x003D7004
		public static ThirdPartyPurchaseReceiptReceived Deserialize(Stream stream, ThirdPartyPurchaseReceiptReceived instance, long limit)
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

		// Token: 0x0600CEA2 RID: 52898 RVA: 0x003D8F0F File Offset: 0x003D710F
		public void Serialize(Stream stream)
		{
			ThirdPartyPurchaseReceiptReceived.Serialize(stream, this);
		}

		// Token: 0x0600CEA3 RID: 52899 RVA: 0x003D8F18 File Offset: 0x003D7118
		public static void Serialize(Stream stream, ThirdPartyPurchaseReceiptReceived instance)
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

		// Token: 0x0600CEA4 RID: 52900 RVA: 0x003D8FCC File Offset: 0x003D71CC
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

		// Token: 0x0400A17F RID: 41343
		public bool HasPlayer;

		// Token: 0x0400A180 RID: 41344
		private Player _Player;

		// Token: 0x0400A181 RID: 41345
		public bool HasDeviceInfo;

		// Token: 0x0400A182 RID: 41346
		private DeviceInfo _DeviceInfo;

		// Token: 0x0400A183 RID: 41347
		public bool HasTransactionId;

		// Token: 0x0400A184 RID: 41348
		private string _TransactionId;

		// Token: 0x0400A185 RID: 41349
		public bool HasProductId;

		// Token: 0x0400A186 RID: 41350
		private string _ProductId;
	}
}
