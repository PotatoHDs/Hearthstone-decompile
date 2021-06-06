using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011A0 RID: 4512
	public class BlizzardCheckoutGeneric : IProtoBuf
	{
		// Token: 0x17000E9C RID: 3740
		// (get) Token: 0x0600C76A RID: 51050 RVA: 0x003BF0C4 File Offset: 0x003BD2C4
		// (set) Token: 0x0600C76B RID: 51051 RVA: 0x003BF0CC File Offset: 0x003BD2CC
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

		// Token: 0x17000E9D RID: 3741
		// (get) Token: 0x0600C76C RID: 51052 RVA: 0x003BF0DF File Offset: 0x003BD2DF
		// (set) Token: 0x0600C76D RID: 51053 RVA: 0x003BF0E7 File Offset: 0x003BD2E7
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

		// Token: 0x17000E9E RID: 3742
		// (get) Token: 0x0600C76E RID: 51054 RVA: 0x003BF0FA File Offset: 0x003BD2FA
		// (set) Token: 0x0600C76F RID: 51055 RVA: 0x003BF102 File Offset: 0x003BD302
		public string MessageKey
		{
			get
			{
				return this._MessageKey;
			}
			set
			{
				this._MessageKey = value;
				this.HasMessageKey = (value != null);
			}
		}

		// Token: 0x17000E9F RID: 3743
		// (get) Token: 0x0600C770 RID: 51056 RVA: 0x003BF115 File Offset: 0x003BD315
		// (set) Token: 0x0600C771 RID: 51057 RVA: 0x003BF11D File Offset: 0x003BD31D
		public string MessageValue
		{
			get
			{
				return this._MessageValue;
			}
			set
			{
				this._MessageValue = value;
				this.HasMessageValue = (value != null);
			}
		}

		// Token: 0x0600C772 RID: 51058 RVA: 0x003BF130 File Offset: 0x003BD330
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
			if (this.HasMessageKey)
			{
				num ^= this.MessageKey.GetHashCode();
			}
			if (this.HasMessageValue)
			{
				num ^= this.MessageValue.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600C773 RID: 51059 RVA: 0x003BF1A4 File Offset: 0x003BD3A4
		public override bool Equals(object obj)
		{
			BlizzardCheckoutGeneric blizzardCheckoutGeneric = obj as BlizzardCheckoutGeneric;
			return blizzardCheckoutGeneric != null && this.HasPlayer == blizzardCheckoutGeneric.HasPlayer && (!this.HasPlayer || this.Player.Equals(blizzardCheckoutGeneric.Player)) && this.HasDeviceInfo == blizzardCheckoutGeneric.HasDeviceInfo && (!this.HasDeviceInfo || this.DeviceInfo.Equals(blizzardCheckoutGeneric.DeviceInfo)) && this.HasMessageKey == blizzardCheckoutGeneric.HasMessageKey && (!this.HasMessageKey || this.MessageKey.Equals(blizzardCheckoutGeneric.MessageKey)) && this.HasMessageValue == blizzardCheckoutGeneric.HasMessageValue && (!this.HasMessageValue || this.MessageValue.Equals(blizzardCheckoutGeneric.MessageValue));
		}

		// Token: 0x0600C774 RID: 51060 RVA: 0x003BF26A File Offset: 0x003BD46A
		public void Deserialize(Stream stream)
		{
			BlizzardCheckoutGeneric.Deserialize(stream, this);
		}

		// Token: 0x0600C775 RID: 51061 RVA: 0x003BF274 File Offset: 0x003BD474
		public static BlizzardCheckoutGeneric Deserialize(Stream stream, BlizzardCheckoutGeneric instance)
		{
			return BlizzardCheckoutGeneric.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600C776 RID: 51062 RVA: 0x003BF280 File Offset: 0x003BD480
		public static BlizzardCheckoutGeneric DeserializeLengthDelimited(Stream stream)
		{
			BlizzardCheckoutGeneric blizzardCheckoutGeneric = new BlizzardCheckoutGeneric();
			BlizzardCheckoutGeneric.DeserializeLengthDelimited(stream, blizzardCheckoutGeneric);
			return blizzardCheckoutGeneric;
		}

		// Token: 0x0600C777 RID: 51063 RVA: 0x003BF29C File Offset: 0x003BD49C
		public static BlizzardCheckoutGeneric DeserializeLengthDelimited(Stream stream, BlizzardCheckoutGeneric instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return BlizzardCheckoutGeneric.Deserialize(stream, instance, num);
		}

		// Token: 0x0600C778 RID: 51064 RVA: 0x003BF2C4 File Offset: 0x003BD4C4
		public static BlizzardCheckoutGeneric Deserialize(Stream stream, BlizzardCheckoutGeneric instance, long limit)
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
							instance.MessageKey = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 34)
						{
							instance.MessageValue = ProtocolParser.ReadString(stream);
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

		// Token: 0x0600C779 RID: 51065 RVA: 0x003BF3CF File Offset: 0x003BD5CF
		public void Serialize(Stream stream)
		{
			BlizzardCheckoutGeneric.Serialize(stream, this);
		}

		// Token: 0x0600C77A RID: 51066 RVA: 0x003BF3D8 File Offset: 0x003BD5D8
		public static void Serialize(Stream stream, BlizzardCheckoutGeneric instance)
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
			if (instance.HasMessageKey)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.MessageKey));
			}
			if (instance.HasMessageValue)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.MessageValue));
			}
		}

		// Token: 0x0600C77B RID: 51067 RVA: 0x003BF48C File Offset: 0x003BD68C
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
			if (this.HasMessageKey)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.MessageKey);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasMessageValue)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.MessageValue);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			return num;
		}

		// Token: 0x04009E84 RID: 40580
		public bool HasPlayer;

		// Token: 0x04009E85 RID: 40581
		private Player _Player;

		// Token: 0x04009E86 RID: 40582
		public bool HasDeviceInfo;

		// Token: 0x04009E87 RID: 40583
		private DeviceInfo _DeviceInfo;

		// Token: 0x04009E88 RID: 40584
		public bool HasMessageKey;

		// Token: 0x04009E89 RID: 40585
		private string _MessageKey;

		// Token: 0x04009E8A RID: 40586
		public bool HasMessageValue;

		// Token: 0x04009E8B RID: 40587
		private string _MessageValue;
	}
}
