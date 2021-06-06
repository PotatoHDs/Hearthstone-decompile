using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011FD RID: 4605
	public class ThirdPartyPurchaseDeferred : IProtoBuf
	{
		// Token: 0x1700100C RID: 4108
		// (get) Token: 0x0600CE49 RID: 52809 RVA: 0x003D7B02 File Offset: 0x003D5D02
		// (set) Token: 0x0600CE4A RID: 52810 RVA: 0x003D7B0A File Offset: 0x003D5D0A
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

		// Token: 0x1700100D RID: 4109
		// (get) Token: 0x0600CE4B RID: 52811 RVA: 0x003D7B1D File Offset: 0x003D5D1D
		// (set) Token: 0x0600CE4C RID: 52812 RVA: 0x003D7B25 File Offset: 0x003D5D25
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

		// Token: 0x1700100E RID: 4110
		// (get) Token: 0x0600CE4D RID: 52813 RVA: 0x003D7B38 File Offset: 0x003D5D38
		// (set) Token: 0x0600CE4E RID: 52814 RVA: 0x003D7B40 File Offset: 0x003D5D40
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

		// Token: 0x1700100F RID: 4111
		// (get) Token: 0x0600CE4F RID: 52815 RVA: 0x003D7B53 File Offset: 0x003D5D53
		// (set) Token: 0x0600CE50 RID: 52816 RVA: 0x003D7B5B File Offset: 0x003D5D5B
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

		// Token: 0x0600CE51 RID: 52817 RVA: 0x003D7B70 File Offset: 0x003D5D70
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

		// Token: 0x0600CE52 RID: 52818 RVA: 0x003D7BE4 File Offset: 0x003D5DE4
		public override bool Equals(object obj)
		{
			ThirdPartyPurchaseDeferred thirdPartyPurchaseDeferred = obj as ThirdPartyPurchaseDeferred;
			return thirdPartyPurchaseDeferred != null && this.HasPlayer == thirdPartyPurchaseDeferred.HasPlayer && (!this.HasPlayer || this.Player.Equals(thirdPartyPurchaseDeferred.Player)) && this.HasDeviceInfo == thirdPartyPurchaseDeferred.HasDeviceInfo && (!this.HasDeviceInfo || this.DeviceInfo.Equals(thirdPartyPurchaseDeferred.DeviceInfo)) && this.HasTransactionId == thirdPartyPurchaseDeferred.HasTransactionId && (!this.HasTransactionId || this.TransactionId.Equals(thirdPartyPurchaseDeferred.TransactionId)) && this.HasProductId == thirdPartyPurchaseDeferred.HasProductId && (!this.HasProductId || this.ProductId.Equals(thirdPartyPurchaseDeferred.ProductId));
		}

		// Token: 0x0600CE53 RID: 52819 RVA: 0x003D7CAA File Offset: 0x003D5EAA
		public void Deserialize(Stream stream)
		{
			ThirdPartyPurchaseDeferred.Deserialize(stream, this);
		}

		// Token: 0x0600CE54 RID: 52820 RVA: 0x003D7CB4 File Offset: 0x003D5EB4
		public static ThirdPartyPurchaseDeferred Deserialize(Stream stream, ThirdPartyPurchaseDeferred instance)
		{
			return ThirdPartyPurchaseDeferred.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600CE55 RID: 52821 RVA: 0x003D7CC0 File Offset: 0x003D5EC0
		public static ThirdPartyPurchaseDeferred DeserializeLengthDelimited(Stream stream)
		{
			ThirdPartyPurchaseDeferred thirdPartyPurchaseDeferred = new ThirdPartyPurchaseDeferred();
			ThirdPartyPurchaseDeferred.DeserializeLengthDelimited(stream, thirdPartyPurchaseDeferred);
			return thirdPartyPurchaseDeferred;
		}

		// Token: 0x0600CE56 RID: 52822 RVA: 0x003D7CDC File Offset: 0x003D5EDC
		public static ThirdPartyPurchaseDeferred DeserializeLengthDelimited(Stream stream, ThirdPartyPurchaseDeferred instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ThirdPartyPurchaseDeferred.Deserialize(stream, instance, num);
		}

		// Token: 0x0600CE57 RID: 52823 RVA: 0x003D7D04 File Offset: 0x003D5F04
		public static ThirdPartyPurchaseDeferred Deserialize(Stream stream, ThirdPartyPurchaseDeferred instance, long limit)
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

		// Token: 0x0600CE58 RID: 52824 RVA: 0x003D7E0F File Offset: 0x003D600F
		public void Serialize(Stream stream)
		{
			ThirdPartyPurchaseDeferred.Serialize(stream, this);
		}

		// Token: 0x0600CE59 RID: 52825 RVA: 0x003D7E18 File Offset: 0x003D6018
		public static void Serialize(Stream stream, ThirdPartyPurchaseDeferred instance)
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

		// Token: 0x0600CE5A RID: 52826 RVA: 0x003D7ECC File Offset: 0x003D60CC
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

		// Token: 0x0400A161 RID: 41313
		public bool HasPlayer;

		// Token: 0x0400A162 RID: 41314
		private Player _Player;

		// Token: 0x0400A163 RID: 41315
		public bool HasDeviceInfo;

		// Token: 0x0400A164 RID: 41316
		private DeviceInfo _DeviceInfo;

		// Token: 0x0400A165 RID: 41317
		public bool HasTransactionId;

		// Token: 0x0400A166 RID: 41318
		private string _TransactionId;

		// Token: 0x0400A167 RID: 41319
		public bool HasProductId;

		// Token: 0x0400A168 RID: 41320
		private string _ProductId;
	}
}
