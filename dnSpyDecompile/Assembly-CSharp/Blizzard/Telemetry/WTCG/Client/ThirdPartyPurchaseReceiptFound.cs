using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011FF RID: 4607
	public class ThirdPartyPurchaseReceiptFound : IProtoBuf
	{
		// Token: 0x17001013 RID: 4115
		// (get) Token: 0x0600CE6D RID: 52845 RVA: 0x003D831A File Offset: 0x003D651A
		// (set) Token: 0x0600CE6E RID: 52846 RVA: 0x003D8322 File Offset: 0x003D6522
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

		// Token: 0x17001014 RID: 4116
		// (get) Token: 0x0600CE6F RID: 52847 RVA: 0x003D8335 File Offset: 0x003D6535
		// (set) Token: 0x0600CE70 RID: 52848 RVA: 0x003D833D File Offset: 0x003D653D
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

		// Token: 0x17001015 RID: 4117
		// (get) Token: 0x0600CE71 RID: 52849 RVA: 0x003D8350 File Offset: 0x003D6550
		// (set) Token: 0x0600CE72 RID: 52850 RVA: 0x003D8358 File Offset: 0x003D6558
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

		// Token: 0x17001016 RID: 4118
		// (get) Token: 0x0600CE73 RID: 52851 RVA: 0x003D836B File Offset: 0x003D656B
		// (set) Token: 0x0600CE74 RID: 52852 RVA: 0x003D8373 File Offset: 0x003D6573
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

		// Token: 0x0600CE75 RID: 52853 RVA: 0x003D8388 File Offset: 0x003D6588
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

		// Token: 0x0600CE76 RID: 52854 RVA: 0x003D83FC File Offset: 0x003D65FC
		public override bool Equals(object obj)
		{
			ThirdPartyPurchaseReceiptFound thirdPartyPurchaseReceiptFound = obj as ThirdPartyPurchaseReceiptFound;
			return thirdPartyPurchaseReceiptFound != null && this.HasPlayer == thirdPartyPurchaseReceiptFound.HasPlayer && (!this.HasPlayer || this.Player.Equals(thirdPartyPurchaseReceiptFound.Player)) && this.HasDeviceInfo == thirdPartyPurchaseReceiptFound.HasDeviceInfo && (!this.HasDeviceInfo || this.DeviceInfo.Equals(thirdPartyPurchaseReceiptFound.DeviceInfo)) && this.HasTransactionId == thirdPartyPurchaseReceiptFound.HasTransactionId && (!this.HasTransactionId || this.TransactionId.Equals(thirdPartyPurchaseReceiptFound.TransactionId)) && this.HasProductId == thirdPartyPurchaseReceiptFound.HasProductId && (!this.HasProductId || this.ProductId.Equals(thirdPartyPurchaseReceiptFound.ProductId));
		}

		// Token: 0x0600CE77 RID: 52855 RVA: 0x003D84C2 File Offset: 0x003D66C2
		public void Deserialize(Stream stream)
		{
			ThirdPartyPurchaseReceiptFound.Deserialize(stream, this);
		}

		// Token: 0x0600CE78 RID: 52856 RVA: 0x003D84CC File Offset: 0x003D66CC
		public static ThirdPartyPurchaseReceiptFound Deserialize(Stream stream, ThirdPartyPurchaseReceiptFound instance)
		{
			return ThirdPartyPurchaseReceiptFound.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600CE79 RID: 52857 RVA: 0x003D84D8 File Offset: 0x003D66D8
		public static ThirdPartyPurchaseReceiptFound DeserializeLengthDelimited(Stream stream)
		{
			ThirdPartyPurchaseReceiptFound thirdPartyPurchaseReceiptFound = new ThirdPartyPurchaseReceiptFound();
			ThirdPartyPurchaseReceiptFound.DeserializeLengthDelimited(stream, thirdPartyPurchaseReceiptFound);
			return thirdPartyPurchaseReceiptFound;
		}

		// Token: 0x0600CE7A RID: 52858 RVA: 0x003D84F4 File Offset: 0x003D66F4
		public static ThirdPartyPurchaseReceiptFound DeserializeLengthDelimited(Stream stream, ThirdPartyPurchaseReceiptFound instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ThirdPartyPurchaseReceiptFound.Deserialize(stream, instance, num);
		}

		// Token: 0x0600CE7B RID: 52859 RVA: 0x003D851C File Offset: 0x003D671C
		public static ThirdPartyPurchaseReceiptFound Deserialize(Stream stream, ThirdPartyPurchaseReceiptFound instance, long limit)
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

		// Token: 0x0600CE7C RID: 52860 RVA: 0x003D8627 File Offset: 0x003D6827
		public void Serialize(Stream stream)
		{
			ThirdPartyPurchaseReceiptFound.Serialize(stream, this);
		}

		// Token: 0x0600CE7D RID: 52861 RVA: 0x003D8630 File Offset: 0x003D6830
		public static void Serialize(Stream stream, ThirdPartyPurchaseReceiptFound instance)
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

		// Token: 0x0600CE7E RID: 52862 RVA: 0x003D86E4 File Offset: 0x003D68E4
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

		// Token: 0x0400A16F RID: 41327
		public bool HasPlayer;

		// Token: 0x0400A170 RID: 41328
		private Player _Player;

		// Token: 0x0400A171 RID: 41329
		public bool HasDeviceInfo;

		// Token: 0x0400A172 RID: 41330
		private DeviceInfo _DeviceInfo;

		// Token: 0x0400A173 RID: 41331
		public bool HasTransactionId;

		// Token: 0x0400A174 RID: 41332
		private string _TransactionId;

		// Token: 0x0400A175 RID: 41333
		public bool HasProductId;

		// Token: 0x0400A176 RID: 41334
		private string _ProductId;
	}
}
