using System;
using System.IO;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011E9 RID: 4585
	public class PurchasePayNowClicked : IProtoBuf
	{
		// Token: 0x17000FB7 RID: 4023
		// (get) Token: 0x0600CCC3 RID: 52419 RVA: 0x003D2074 File Offset: 0x003D0274
		// (set) Token: 0x0600CCC4 RID: 52420 RVA: 0x003D207C File Offset: 0x003D027C
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

		// Token: 0x17000FB8 RID: 4024
		// (get) Token: 0x0600CCC5 RID: 52421 RVA: 0x003D208F File Offset: 0x003D028F
		// (set) Token: 0x0600CCC6 RID: 52422 RVA: 0x003D2097 File Offset: 0x003D0297
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

		// Token: 0x17000FB9 RID: 4025
		// (get) Token: 0x0600CCC7 RID: 52423 RVA: 0x003D20AA File Offset: 0x003D02AA
		// (set) Token: 0x0600CCC8 RID: 52424 RVA: 0x003D20B2 File Offset: 0x003D02B2
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

		// Token: 0x0600CCC9 RID: 52425 RVA: 0x003D20C4 File Offset: 0x003D02C4
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

		// Token: 0x0600CCCA RID: 52426 RVA: 0x003D2124 File Offset: 0x003D0324
		public override bool Equals(object obj)
		{
			PurchasePayNowClicked purchasePayNowClicked = obj as PurchasePayNowClicked;
			return purchasePayNowClicked != null && this.HasPlayer == purchasePayNowClicked.HasPlayer && (!this.HasPlayer || this.Player.Equals(purchasePayNowClicked.Player)) && this.HasDeviceInfo == purchasePayNowClicked.HasDeviceInfo && (!this.HasDeviceInfo || this.DeviceInfo.Equals(purchasePayNowClicked.DeviceInfo)) && this.HasPmtProductId == purchasePayNowClicked.HasPmtProductId && (!this.HasPmtProductId || this.PmtProductId.Equals(purchasePayNowClicked.PmtProductId));
		}

		// Token: 0x0600CCCB RID: 52427 RVA: 0x003D21C2 File Offset: 0x003D03C2
		public void Deserialize(Stream stream)
		{
			PurchasePayNowClicked.Deserialize(stream, this);
		}

		// Token: 0x0600CCCC RID: 52428 RVA: 0x003D21CC File Offset: 0x003D03CC
		public static PurchasePayNowClicked Deserialize(Stream stream, PurchasePayNowClicked instance)
		{
			return PurchasePayNowClicked.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600CCCD RID: 52429 RVA: 0x003D21D8 File Offset: 0x003D03D8
		public static PurchasePayNowClicked DeserializeLengthDelimited(Stream stream)
		{
			PurchasePayNowClicked purchasePayNowClicked = new PurchasePayNowClicked();
			PurchasePayNowClicked.DeserializeLengthDelimited(stream, purchasePayNowClicked);
			return purchasePayNowClicked;
		}

		// Token: 0x0600CCCE RID: 52430 RVA: 0x003D21F4 File Offset: 0x003D03F4
		public static PurchasePayNowClicked DeserializeLengthDelimited(Stream stream, PurchasePayNowClicked instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return PurchasePayNowClicked.Deserialize(stream, instance, num);
		}

		// Token: 0x0600CCCF RID: 52431 RVA: 0x003D221C File Offset: 0x003D041C
		public static PurchasePayNowClicked Deserialize(Stream stream, PurchasePayNowClicked instance, long limit)
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

		// Token: 0x0600CCD0 RID: 52432 RVA: 0x003D2304 File Offset: 0x003D0504
		public void Serialize(Stream stream)
		{
			PurchasePayNowClicked.Serialize(stream, this);
		}

		// Token: 0x0600CCD1 RID: 52433 RVA: 0x003D2310 File Offset: 0x003D0510
		public static void Serialize(Stream stream, PurchasePayNowClicked instance)
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

		// Token: 0x0600CCD2 RID: 52434 RVA: 0x003D2394 File Offset: 0x003D0594
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

		// Token: 0x0400A0B9 RID: 41145
		public bool HasPlayer;

		// Token: 0x0400A0BA RID: 41146
		private Player _Player;

		// Token: 0x0400A0BB RID: 41147
		public bool HasDeviceInfo;

		// Token: 0x0400A0BC RID: 41148
		private DeviceInfo _DeviceInfo;

		// Token: 0x0400A0BD RID: 41149
		public bool HasPmtProductId;

		// Token: 0x0400A0BE RID: 41150
		private long _PmtProductId;
	}
}
