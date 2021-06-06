using System;
using System.IO;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011D3 RID: 4563
	public class LargeThirdPartyReceiptFound : IProtoBuf
	{
		// Token: 0x17000F6A RID: 3946
		// (get) Token: 0x0600CB37 RID: 52023 RVA: 0x003CCB9F File Offset: 0x003CAD9F
		// (set) Token: 0x0600CB38 RID: 52024 RVA: 0x003CCBA7 File Offset: 0x003CADA7
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

		// Token: 0x17000F6B RID: 3947
		// (get) Token: 0x0600CB39 RID: 52025 RVA: 0x003CCBBA File Offset: 0x003CADBA
		// (set) Token: 0x0600CB3A RID: 52026 RVA: 0x003CCBC2 File Offset: 0x003CADC2
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

		// Token: 0x17000F6C RID: 3948
		// (get) Token: 0x0600CB3B RID: 52027 RVA: 0x003CCBD5 File Offset: 0x003CADD5
		// (set) Token: 0x0600CB3C RID: 52028 RVA: 0x003CCBDD File Offset: 0x003CADDD
		public long ReceiptSize
		{
			get
			{
				return this._ReceiptSize;
			}
			set
			{
				this._ReceiptSize = value;
				this.HasReceiptSize = true;
			}
		}

		// Token: 0x0600CB3D RID: 52029 RVA: 0x003CCBF0 File Offset: 0x003CADF0
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
			if (this.HasReceiptSize)
			{
				num ^= this.ReceiptSize.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600CB3E RID: 52030 RVA: 0x003CCC50 File Offset: 0x003CAE50
		public override bool Equals(object obj)
		{
			LargeThirdPartyReceiptFound largeThirdPartyReceiptFound = obj as LargeThirdPartyReceiptFound;
			return largeThirdPartyReceiptFound != null && this.HasPlayer == largeThirdPartyReceiptFound.HasPlayer && (!this.HasPlayer || this.Player.Equals(largeThirdPartyReceiptFound.Player)) && this.HasDeviceInfo == largeThirdPartyReceiptFound.HasDeviceInfo && (!this.HasDeviceInfo || this.DeviceInfo.Equals(largeThirdPartyReceiptFound.DeviceInfo)) && this.HasReceiptSize == largeThirdPartyReceiptFound.HasReceiptSize && (!this.HasReceiptSize || this.ReceiptSize.Equals(largeThirdPartyReceiptFound.ReceiptSize));
		}

		// Token: 0x0600CB3F RID: 52031 RVA: 0x003CCCEE File Offset: 0x003CAEEE
		public void Deserialize(Stream stream)
		{
			LargeThirdPartyReceiptFound.Deserialize(stream, this);
		}

		// Token: 0x0600CB40 RID: 52032 RVA: 0x003CCCF8 File Offset: 0x003CAEF8
		public static LargeThirdPartyReceiptFound Deserialize(Stream stream, LargeThirdPartyReceiptFound instance)
		{
			return LargeThirdPartyReceiptFound.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600CB41 RID: 52033 RVA: 0x003CCD04 File Offset: 0x003CAF04
		public static LargeThirdPartyReceiptFound DeserializeLengthDelimited(Stream stream)
		{
			LargeThirdPartyReceiptFound largeThirdPartyReceiptFound = new LargeThirdPartyReceiptFound();
			LargeThirdPartyReceiptFound.DeserializeLengthDelimited(stream, largeThirdPartyReceiptFound);
			return largeThirdPartyReceiptFound;
		}

		// Token: 0x0600CB42 RID: 52034 RVA: 0x003CCD20 File Offset: 0x003CAF20
		public static LargeThirdPartyReceiptFound DeserializeLengthDelimited(Stream stream, LargeThirdPartyReceiptFound instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return LargeThirdPartyReceiptFound.Deserialize(stream, instance, num);
		}

		// Token: 0x0600CB43 RID: 52035 RVA: 0x003CCD48 File Offset: 0x003CAF48
		public static LargeThirdPartyReceiptFound Deserialize(Stream stream, LargeThirdPartyReceiptFound instance, long limit)
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
						if (num != 24)
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
							instance.ReceiptSize = (long)ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x0600CB44 RID: 52036 RVA: 0x003CCE30 File Offset: 0x003CB030
		public void Serialize(Stream stream)
		{
			LargeThirdPartyReceiptFound.Serialize(stream, this);
		}

		// Token: 0x0600CB45 RID: 52037 RVA: 0x003CCE3C File Offset: 0x003CB03C
		public static void Serialize(Stream stream, LargeThirdPartyReceiptFound instance)
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
			if (instance.HasReceiptSize)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.ReceiptSize);
			}
		}

		// Token: 0x0600CB46 RID: 52038 RVA: 0x003CCEC0 File Offset: 0x003CB0C0
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
			if (this.HasReceiptSize)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.ReceiptSize);
			}
			return num;
		}

		// Token: 0x0400A01F RID: 40991
		public bool HasPlayer;

		// Token: 0x0400A020 RID: 40992
		private Player _Player;

		// Token: 0x0400A021 RID: 40993
		public bool HasDeviceInfo;

		// Token: 0x0400A022 RID: 40994
		private DeviceInfo _DeviceInfo;

		// Token: 0x0400A023 RID: 40995
		public bool HasReceiptSize;

		// Token: 0x0400A024 RID: 40996
		private long _ReceiptSize;
	}
}
