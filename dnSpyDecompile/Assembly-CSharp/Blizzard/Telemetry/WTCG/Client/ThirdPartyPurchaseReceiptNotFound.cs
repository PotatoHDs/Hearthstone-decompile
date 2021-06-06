using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x02001200 RID: 4608
	public class ThirdPartyPurchaseReceiptNotFound : IProtoBuf
	{
		// Token: 0x17001017 RID: 4119
		// (get) Token: 0x0600CE80 RID: 52864 RVA: 0x003D878D File Offset: 0x003D698D
		// (set) Token: 0x0600CE81 RID: 52865 RVA: 0x003D8795 File Offset: 0x003D6995
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

		// Token: 0x17001018 RID: 4120
		// (get) Token: 0x0600CE82 RID: 52866 RVA: 0x003D87A8 File Offset: 0x003D69A8
		// (set) Token: 0x0600CE83 RID: 52867 RVA: 0x003D87B0 File Offset: 0x003D69B0
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

		// Token: 0x17001019 RID: 4121
		// (get) Token: 0x0600CE84 RID: 52868 RVA: 0x003D87C3 File Offset: 0x003D69C3
		// (set) Token: 0x0600CE85 RID: 52869 RVA: 0x003D87CB File Offset: 0x003D69CB
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

		// Token: 0x1700101A RID: 4122
		// (get) Token: 0x0600CE86 RID: 52870 RVA: 0x003D87DE File Offset: 0x003D69DE
		// (set) Token: 0x0600CE87 RID: 52871 RVA: 0x003D87E6 File Offset: 0x003D69E6
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

		// Token: 0x0600CE88 RID: 52872 RVA: 0x003D87FC File Offset: 0x003D69FC
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

		// Token: 0x0600CE89 RID: 52873 RVA: 0x003D8870 File Offset: 0x003D6A70
		public override bool Equals(object obj)
		{
			ThirdPartyPurchaseReceiptNotFound thirdPartyPurchaseReceiptNotFound = obj as ThirdPartyPurchaseReceiptNotFound;
			return thirdPartyPurchaseReceiptNotFound != null && this.HasPlayer == thirdPartyPurchaseReceiptNotFound.HasPlayer && (!this.HasPlayer || this.Player.Equals(thirdPartyPurchaseReceiptNotFound.Player)) && this.HasDeviceInfo == thirdPartyPurchaseReceiptNotFound.HasDeviceInfo && (!this.HasDeviceInfo || this.DeviceInfo.Equals(thirdPartyPurchaseReceiptNotFound.DeviceInfo)) && this.HasTransactionId == thirdPartyPurchaseReceiptNotFound.HasTransactionId && (!this.HasTransactionId || this.TransactionId.Equals(thirdPartyPurchaseReceiptNotFound.TransactionId)) && this.HasProductId == thirdPartyPurchaseReceiptNotFound.HasProductId && (!this.HasProductId || this.ProductId.Equals(thirdPartyPurchaseReceiptNotFound.ProductId));
		}

		// Token: 0x0600CE8A RID: 52874 RVA: 0x003D8936 File Offset: 0x003D6B36
		public void Deserialize(Stream stream)
		{
			ThirdPartyPurchaseReceiptNotFound.Deserialize(stream, this);
		}

		// Token: 0x0600CE8B RID: 52875 RVA: 0x003D8940 File Offset: 0x003D6B40
		public static ThirdPartyPurchaseReceiptNotFound Deserialize(Stream stream, ThirdPartyPurchaseReceiptNotFound instance)
		{
			return ThirdPartyPurchaseReceiptNotFound.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600CE8C RID: 52876 RVA: 0x003D894C File Offset: 0x003D6B4C
		public static ThirdPartyPurchaseReceiptNotFound DeserializeLengthDelimited(Stream stream)
		{
			ThirdPartyPurchaseReceiptNotFound thirdPartyPurchaseReceiptNotFound = new ThirdPartyPurchaseReceiptNotFound();
			ThirdPartyPurchaseReceiptNotFound.DeserializeLengthDelimited(stream, thirdPartyPurchaseReceiptNotFound);
			return thirdPartyPurchaseReceiptNotFound;
		}

		// Token: 0x0600CE8D RID: 52877 RVA: 0x003D8968 File Offset: 0x003D6B68
		public static ThirdPartyPurchaseReceiptNotFound DeserializeLengthDelimited(Stream stream, ThirdPartyPurchaseReceiptNotFound instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ThirdPartyPurchaseReceiptNotFound.Deserialize(stream, instance, num);
		}

		// Token: 0x0600CE8E RID: 52878 RVA: 0x003D8990 File Offset: 0x003D6B90
		public static ThirdPartyPurchaseReceiptNotFound Deserialize(Stream stream, ThirdPartyPurchaseReceiptNotFound instance, long limit)
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

		// Token: 0x0600CE8F RID: 52879 RVA: 0x003D8A9B File Offset: 0x003D6C9B
		public void Serialize(Stream stream)
		{
			ThirdPartyPurchaseReceiptNotFound.Serialize(stream, this);
		}

		// Token: 0x0600CE90 RID: 52880 RVA: 0x003D8AA4 File Offset: 0x003D6CA4
		public static void Serialize(Stream stream, ThirdPartyPurchaseReceiptNotFound instance)
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

		// Token: 0x0600CE91 RID: 52881 RVA: 0x003D8B58 File Offset: 0x003D6D58
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

		// Token: 0x0400A177 RID: 41335
		public bool HasPlayer;

		// Token: 0x0400A178 RID: 41336
		private Player _Player;

		// Token: 0x0400A179 RID: 41337
		public bool HasDeviceInfo;

		// Token: 0x0400A17A RID: 41338
		private DeviceInfo _DeviceInfo;

		// Token: 0x0400A17B RID: 41339
		public bool HasTransactionId;

		// Token: 0x0400A17C RID: 41340
		private string _TransactionId;

		// Token: 0x0400A17D RID: 41341
		public bool HasProductId;

		// Token: 0x0400A17E RID: 41342
		private string _ProductId;
	}
}
