using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x02001206 RID: 4614
	public class ThirdPartyReceiptConsumed : IProtoBuf
	{
		// Token: 0x17001033 RID: 4147
		// (get) Token: 0x0600CEFA RID: 52986 RVA: 0x003DA55E File Offset: 0x003D875E
		// (set) Token: 0x0600CEFB RID: 52987 RVA: 0x003DA566 File Offset: 0x003D8766
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

		// Token: 0x17001034 RID: 4148
		// (get) Token: 0x0600CEFC RID: 52988 RVA: 0x003DA579 File Offset: 0x003D8779
		// (set) Token: 0x0600CEFD RID: 52989 RVA: 0x003DA581 File Offset: 0x003D8781
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

		// Token: 0x17001035 RID: 4149
		// (get) Token: 0x0600CEFE RID: 52990 RVA: 0x003DA594 File Offset: 0x003D8794
		// (set) Token: 0x0600CEFF RID: 52991 RVA: 0x003DA59C File Offset: 0x003D879C
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

		// Token: 0x0600CF00 RID: 52992 RVA: 0x003DA5B0 File Offset: 0x003D87B0
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

		// Token: 0x0600CF01 RID: 52993 RVA: 0x003DA60C File Offset: 0x003D880C
		public override bool Equals(object obj)
		{
			ThirdPartyReceiptConsumed thirdPartyReceiptConsumed = obj as ThirdPartyReceiptConsumed;
			return thirdPartyReceiptConsumed != null && this.HasPlayer == thirdPartyReceiptConsumed.HasPlayer && (!this.HasPlayer || this.Player.Equals(thirdPartyReceiptConsumed.Player)) && this.HasDeviceInfo == thirdPartyReceiptConsumed.HasDeviceInfo && (!this.HasDeviceInfo || this.DeviceInfo.Equals(thirdPartyReceiptConsumed.DeviceInfo)) && this.HasTransactionId == thirdPartyReceiptConsumed.HasTransactionId && (!this.HasTransactionId || this.TransactionId.Equals(thirdPartyReceiptConsumed.TransactionId));
		}

		// Token: 0x0600CF02 RID: 52994 RVA: 0x003DA6A7 File Offset: 0x003D88A7
		public void Deserialize(Stream stream)
		{
			ThirdPartyReceiptConsumed.Deserialize(stream, this);
		}

		// Token: 0x0600CF03 RID: 52995 RVA: 0x003DA6B1 File Offset: 0x003D88B1
		public static ThirdPartyReceiptConsumed Deserialize(Stream stream, ThirdPartyReceiptConsumed instance)
		{
			return ThirdPartyReceiptConsumed.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600CF04 RID: 52996 RVA: 0x003DA6BC File Offset: 0x003D88BC
		public static ThirdPartyReceiptConsumed DeserializeLengthDelimited(Stream stream)
		{
			ThirdPartyReceiptConsumed thirdPartyReceiptConsumed = new ThirdPartyReceiptConsumed();
			ThirdPartyReceiptConsumed.DeserializeLengthDelimited(stream, thirdPartyReceiptConsumed);
			return thirdPartyReceiptConsumed;
		}

		// Token: 0x0600CF05 RID: 52997 RVA: 0x003DA6D8 File Offset: 0x003D88D8
		public static ThirdPartyReceiptConsumed DeserializeLengthDelimited(Stream stream, ThirdPartyReceiptConsumed instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ThirdPartyReceiptConsumed.Deserialize(stream, instance, num);
		}

		// Token: 0x0600CF06 RID: 52998 RVA: 0x003DA700 File Offset: 0x003D8900
		public static ThirdPartyReceiptConsumed Deserialize(Stream stream, ThirdPartyReceiptConsumed instance, long limit)
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

		// Token: 0x0600CF07 RID: 52999 RVA: 0x003DA7E8 File Offset: 0x003D89E8
		public void Serialize(Stream stream)
		{
			ThirdPartyReceiptConsumed.Serialize(stream, this);
		}

		// Token: 0x0600CF08 RID: 53000 RVA: 0x003DA7F4 File Offset: 0x003D89F4
		public static void Serialize(Stream stream, ThirdPartyReceiptConsumed instance)
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

		// Token: 0x0600CF09 RID: 53001 RVA: 0x003DA884 File Offset: 0x003D8A84
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

		// Token: 0x0400A1AF RID: 41391
		public bool HasPlayer;

		// Token: 0x0400A1B0 RID: 41392
		private Player _Player;

		// Token: 0x0400A1B1 RID: 41393
		public bool HasDeviceInfo;

		// Token: 0x0400A1B2 RID: 41394
		private DeviceInfo _DeviceInfo;

		// Token: 0x0400A1B3 RID: 41395
		public bool HasTransactionId;

		// Token: 0x0400A1B4 RID: 41396
		private string _TransactionId;
	}
}
