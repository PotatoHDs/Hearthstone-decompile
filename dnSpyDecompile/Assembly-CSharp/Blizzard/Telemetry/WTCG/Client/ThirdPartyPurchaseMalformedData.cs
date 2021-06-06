using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011FE RID: 4606
	public class ThirdPartyPurchaseMalformedData : IProtoBuf
	{
		// Token: 0x17001010 RID: 4112
		// (get) Token: 0x0600CE5C RID: 52828 RVA: 0x003D7F75 File Offset: 0x003D6175
		// (set) Token: 0x0600CE5D RID: 52829 RVA: 0x003D7F7D File Offset: 0x003D617D
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

		// Token: 0x17001011 RID: 4113
		// (get) Token: 0x0600CE5E RID: 52830 RVA: 0x003D7F90 File Offset: 0x003D6190
		// (set) Token: 0x0600CE5F RID: 52831 RVA: 0x003D7F98 File Offset: 0x003D6198
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

		// Token: 0x17001012 RID: 4114
		// (get) Token: 0x0600CE60 RID: 52832 RVA: 0x003D7FAB File Offset: 0x003D61AB
		// (set) Token: 0x0600CE61 RID: 52833 RVA: 0x003D7FB3 File Offset: 0x003D61B3
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

		// Token: 0x0600CE62 RID: 52834 RVA: 0x003D7FC8 File Offset: 0x003D61C8
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
			return num;
		}

		// Token: 0x0600CE63 RID: 52835 RVA: 0x003D8024 File Offset: 0x003D6224
		public override bool Equals(object obj)
		{
			ThirdPartyPurchaseMalformedData thirdPartyPurchaseMalformedData = obj as ThirdPartyPurchaseMalformedData;
			return thirdPartyPurchaseMalformedData != null && this.HasPlayer == thirdPartyPurchaseMalformedData.HasPlayer && (!this.HasPlayer || this.Player.Equals(thirdPartyPurchaseMalformedData.Player)) && this.HasDeviceInfo == thirdPartyPurchaseMalformedData.HasDeviceInfo && (!this.HasDeviceInfo || this.DeviceInfo.Equals(thirdPartyPurchaseMalformedData.DeviceInfo)) && this.HasTransactionId == thirdPartyPurchaseMalformedData.HasTransactionId && (!this.HasTransactionId || this.TransactionId.Equals(thirdPartyPurchaseMalformedData.TransactionId));
		}

		// Token: 0x0600CE64 RID: 52836 RVA: 0x003D80BF File Offset: 0x003D62BF
		public void Deserialize(Stream stream)
		{
			ThirdPartyPurchaseMalformedData.Deserialize(stream, this);
		}

		// Token: 0x0600CE65 RID: 52837 RVA: 0x003D80C9 File Offset: 0x003D62C9
		public static ThirdPartyPurchaseMalformedData Deserialize(Stream stream, ThirdPartyPurchaseMalformedData instance)
		{
			return ThirdPartyPurchaseMalformedData.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600CE66 RID: 52838 RVA: 0x003D80D4 File Offset: 0x003D62D4
		public static ThirdPartyPurchaseMalformedData DeserializeLengthDelimited(Stream stream)
		{
			ThirdPartyPurchaseMalformedData thirdPartyPurchaseMalformedData = new ThirdPartyPurchaseMalformedData();
			ThirdPartyPurchaseMalformedData.DeserializeLengthDelimited(stream, thirdPartyPurchaseMalformedData);
			return thirdPartyPurchaseMalformedData;
		}

		// Token: 0x0600CE67 RID: 52839 RVA: 0x003D80F0 File Offset: 0x003D62F0
		public static ThirdPartyPurchaseMalformedData DeserializeLengthDelimited(Stream stream, ThirdPartyPurchaseMalformedData instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ThirdPartyPurchaseMalformedData.Deserialize(stream, instance, num);
		}

		// Token: 0x0600CE68 RID: 52840 RVA: 0x003D8118 File Offset: 0x003D6318
		public static ThirdPartyPurchaseMalformedData Deserialize(Stream stream, ThirdPartyPurchaseMalformedData instance, long limit)
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
				else if (num != 10)
				{
					if (num != 18)
					{
						if (num != 26)
						{
							Key key = ProtocolParser.ReadKey((byte)num, stream);
							if (key.Field == 0U)
							{
								throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
							}
							ProtocolParser.SkipKey(stream, key);
						}
						else
						{
							instance.TransactionId = ProtocolParser.ReadString(stream);
						}
					}
					else if (instance.DeviceInfo == null)
					{
						instance.DeviceInfo = DeviceInfo.DeserializeLengthDelimited(stream);
					}
					else
					{
						DeviceInfo.DeserializeLengthDelimited(stream, instance.DeviceInfo);
					}
				}
				else if (instance.Player == null)
				{
					instance.Player = Player.DeserializeLengthDelimited(stream);
				}
				else
				{
					Player.DeserializeLengthDelimited(stream, instance.Player);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600CE69 RID: 52841 RVA: 0x003D8200 File Offset: 0x003D6400
		public void Serialize(Stream stream)
		{
			ThirdPartyPurchaseMalformedData.Serialize(stream, this);
		}

		// Token: 0x0600CE6A RID: 52842 RVA: 0x003D820C File Offset: 0x003D640C
		public static void Serialize(Stream stream, ThirdPartyPurchaseMalformedData instance)
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
		}

		// Token: 0x0600CE6B RID: 52843 RVA: 0x003D829C File Offset: 0x003D649C
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
			return num;
		}

		// Token: 0x0400A169 RID: 41321
		public bool HasPlayer;

		// Token: 0x0400A16A RID: 41322
		private Player _Player;

		// Token: 0x0400A16B RID: 41323
		public bool HasDeviceInfo;

		// Token: 0x0400A16C RID: 41324
		private DeviceInfo _DeviceInfo;

		// Token: 0x0400A16D RID: 41325
		public bool HasTransactionId;

		// Token: 0x0400A16E RID: 41326
		private string _TransactionId;
	}
}
