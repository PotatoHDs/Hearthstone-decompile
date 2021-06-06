using System;
using System.IO;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011A3 RID: 4515
	public class BlizzardCheckoutPurchaseCancel : IProtoBuf
	{
		// Token: 0x17000EA9 RID: 3753
		// (get) Token: 0x0600C7A5 RID: 51109 RVA: 0x003BFE92 File Offset: 0x003BE092
		// (set) Token: 0x0600C7A6 RID: 51110 RVA: 0x003BFE9A File Offset: 0x003BE09A
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

		// Token: 0x17000EAA RID: 3754
		// (get) Token: 0x0600C7A7 RID: 51111 RVA: 0x003BFEAD File Offset: 0x003BE0AD
		// (set) Token: 0x0600C7A8 RID: 51112 RVA: 0x003BFEB5 File Offset: 0x003BE0B5
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

		// Token: 0x0600C7A9 RID: 51113 RVA: 0x003BFEC8 File Offset: 0x003BE0C8
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
			return num;
		}

		// Token: 0x0600C7AA RID: 51114 RVA: 0x003BFF10 File Offset: 0x003BE110
		public override bool Equals(object obj)
		{
			BlizzardCheckoutPurchaseCancel blizzardCheckoutPurchaseCancel = obj as BlizzardCheckoutPurchaseCancel;
			return blizzardCheckoutPurchaseCancel != null && this.HasPlayer == blizzardCheckoutPurchaseCancel.HasPlayer && (!this.HasPlayer || this.Player.Equals(blizzardCheckoutPurchaseCancel.Player)) && this.HasDeviceInfo == blizzardCheckoutPurchaseCancel.HasDeviceInfo && (!this.HasDeviceInfo || this.DeviceInfo.Equals(blizzardCheckoutPurchaseCancel.DeviceInfo));
		}

		// Token: 0x0600C7AB RID: 51115 RVA: 0x003BFF80 File Offset: 0x003BE180
		public void Deserialize(Stream stream)
		{
			BlizzardCheckoutPurchaseCancel.Deserialize(stream, this);
		}

		// Token: 0x0600C7AC RID: 51116 RVA: 0x003BFF8A File Offset: 0x003BE18A
		public static BlizzardCheckoutPurchaseCancel Deserialize(Stream stream, BlizzardCheckoutPurchaseCancel instance)
		{
			return BlizzardCheckoutPurchaseCancel.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600C7AD RID: 51117 RVA: 0x003BFF98 File Offset: 0x003BE198
		public static BlizzardCheckoutPurchaseCancel DeserializeLengthDelimited(Stream stream)
		{
			BlizzardCheckoutPurchaseCancel blizzardCheckoutPurchaseCancel = new BlizzardCheckoutPurchaseCancel();
			BlizzardCheckoutPurchaseCancel.DeserializeLengthDelimited(stream, blizzardCheckoutPurchaseCancel);
			return blizzardCheckoutPurchaseCancel;
		}

		// Token: 0x0600C7AE RID: 51118 RVA: 0x003BFFB4 File Offset: 0x003BE1B4
		public static BlizzardCheckoutPurchaseCancel DeserializeLengthDelimited(Stream stream, BlizzardCheckoutPurchaseCancel instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return BlizzardCheckoutPurchaseCancel.Deserialize(stream, instance, num);
		}

		// Token: 0x0600C7AF RID: 51119 RVA: 0x003BFFDC File Offset: 0x003BE1DC
		public static BlizzardCheckoutPurchaseCancel Deserialize(Stream stream, BlizzardCheckoutPurchaseCancel instance, long limit)
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
						Key key = ProtocolParser.ReadKey((byte)num, stream);
						if (key.Field == 0U)
						{
							throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
						}
						ProtocolParser.SkipKey(stream, key);
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

		// Token: 0x0600C7B0 RID: 51120 RVA: 0x003C00AE File Offset: 0x003BE2AE
		public void Serialize(Stream stream)
		{
			BlizzardCheckoutPurchaseCancel.Serialize(stream, this);
		}

		// Token: 0x0600C7B1 RID: 51121 RVA: 0x003C00B8 File Offset: 0x003BE2B8
		public static void Serialize(Stream stream, BlizzardCheckoutPurchaseCancel instance)
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
		}

		// Token: 0x0600C7B2 RID: 51122 RVA: 0x003C0120 File Offset: 0x003BE320
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
			return num;
		}

		// Token: 0x04009E9E RID: 40606
		public bool HasPlayer;

		// Token: 0x04009E9F RID: 40607
		private Player _Player;

		// Token: 0x04009EA0 RID: 40608
		public bool HasDeviceInfo;

		// Token: 0x04009EA1 RID: 40609
		private DeviceInfo _DeviceInfo;
	}
}
