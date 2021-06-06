using System;
using System.IO;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011E8 RID: 4584
	public class PurchaseCancelClicked : IProtoBuf
	{
		// Token: 0x17000FB4 RID: 4020
		// (get) Token: 0x0600CCB2 RID: 52402 RVA: 0x003D1CE5 File Offset: 0x003CFEE5
		// (set) Token: 0x0600CCB3 RID: 52403 RVA: 0x003D1CED File Offset: 0x003CFEED
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

		// Token: 0x17000FB5 RID: 4021
		// (get) Token: 0x0600CCB4 RID: 52404 RVA: 0x003D1D00 File Offset: 0x003CFF00
		// (set) Token: 0x0600CCB5 RID: 52405 RVA: 0x003D1D08 File Offset: 0x003CFF08
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

		// Token: 0x17000FB6 RID: 4022
		// (get) Token: 0x0600CCB6 RID: 52406 RVA: 0x003D1D1B File Offset: 0x003CFF1B
		// (set) Token: 0x0600CCB7 RID: 52407 RVA: 0x003D1D23 File Offset: 0x003CFF23
		public long PmtProductId
		{
			get
			{
				return this._PmtProductId;
			}
			set
			{
				this._PmtProductId = value;
				this.HasPmtProductId = true;
			}
		}

		// Token: 0x0600CCB8 RID: 52408 RVA: 0x003D1D34 File Offset: 0x003CFF34
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
			if (this.HasPmtProductId)
			{
				num ^= this.PmtProductId.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600CCB9 RID: 52409 RVA: 0x003D1D94 File Offset: 0x003CFF94
		public override bool Equals(object obj)
		{
			PurchaseCancelClicked purchaseCancelClicked = obj as PurchaseCancelClicked;
			return purchaseCancelClicked != null && this.HasPlayer == purchaseCancelClicked.HasPlayer && (!this.HasPlayer || this.Player.Equals(purchaseCancelClicked.Player)) && this.HasDeviceInfo == purchaseCancelClicked.HasDeviceInfo && (!this.HasDeviceInfo || this.DeviceInfo.Equals(purchaseCancelClicked.DeviceInfo)) && this.HasPmtProductId == purchaseCancelClicked.HasPmtProductId && (!this.HasPmtProductId || this.PmtProductId.Equals(purchaseCancelClicked.PmtProductId));
		}

		// Token: 0x0600CCBA RID: 52410 RVA: 0x003D1E32 File Offset: 0x003D0032
		public void Deserialize(Stream stream)
		{
			PurchaseCancelClicked.Deserialize(stream, this);
		}

		// Token: 0x0600CCBB RID: 52411 RVA: 0x003D1E3C File Offset: 0x003D003C
		public static PurchaseCancelClicked Deserialize(Stream stream, PurchaseCancelClicked instance)
		{
			return PurchaseCancelClicked.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600CCBC RID: 52412 RVA: 0x003D1E48 File Offset: 0x003D0048
		public static PurchaseCancelClicked DeserializeLengthDelimited(Stream stream)
		{
			PurchaseCancelClicked purchaseCancelClicked = new PurchaseCancelClicked();
			PurchaseCancelClicked.DeserializeLengthDelimited(stream, purchaseCancelClicked);
			return purchaseCancelClicked;
		}

		// Token: 0x0600CCBD RID: 52413 RVA: 0x003D1E64 File Offset: 0x003D0064
		public static PurchaseCancelClicked DeserializeLengthDelimited(Stream stream, PurchaseCancelClicked instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return PurchaseCancelClicked.Deserialize(stream, instance, num);
		}

		// Token: 0x0600CCBE RID: 52414 RVA: 0x003D1E8C File Offset: 0x003D008C
		public static PurchaseCancelClicked Deserialize(Stream stream, PurchaseCancelClicked instance, long limit)
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
						if (num != 32)
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
							instance.PmtProductId = (long)ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x0600CCBF RID: 52415 RVA: 0x003D1F74 File Offset: 0x003D0174
		public void Serialize(Stream stream)
		{
			PurchaseCancelClicked.Serialize(stream, this);
		}

		// Token: 0x0600CCC0 RID: 52416 RVA: 0x003D1F80 File Offset: 0x003D0180
		public static void Serialize(Stream stream, PurchaseCancelClicked instance)
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
			if (instance.HasPmtProductId)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.PmtProductId);
			}
		}

		// Token: 0x0600CCC1 RID: 52417 RVA: 0x003D2004 File Offset: 0x003D0204
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
			if (this.HasPmtProductId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.PmtProductId);
			}
			return num;
		}

		// Token: 0x0400A0B3 RID: 41139
		public bool HasPlayer;

		// Token: 0x0400A0B4 RID: 41140
		private Player _Player;

		// Token: 0x0400A0B5 RID: 41141
		public bool HasDeviceInfo;

		// Token: 0x0400A0B6 RID: 41142
		private DeviceInfo _DeviceInfo;

		// Token: 0x0400A0B7 RID: 41143
		public bool HasPmtProductId;

		// Token: 0x0400A0B8 RID: 41144
		private long _PmtProductId;
	}
}
