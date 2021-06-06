using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011BD RID: 4541
	public class FatalBattleNetError : IProtoBuf
	{
		// Token: 0x17000F05 RID: 3845
		// (get) Token: 0x0600C97B RID: 51579 RVA: 0x003C6450 File Offset: 0x003C4650
		// (set) Token: 0x0600C97C RID: 51580 RVA: 0x003C6458 File Offset: 0x003C4658
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

		// Token: 0x17000F06 RID: 3846
		// (get) Token: 0x0600C97D RID: 51581 RVA: 0x003C646B File Offset: 0x003C466B
		// (set) Token: 0x0600C97E RID: 51582 RVA: 0x003C6473 File Offset: 0x003C4673
		public int ErrorCode
		{
			get
			{
				return this._ErrorCode;
			}
			set
			{
				this._ErrorCode = value;
				this.HasErrorCode = true;
			}
		}

		// Token: 0x17000F07 RID: 3847
		// (get) Token: 0x0600C97F RID: 51583 RVA: 0x003C6483 File Offset: 0x003C4683
		// (set) Token: 0x0600C980 RID: 51584 RVA: 0x003C648B File Offset: 0x003C468B
		public string Description
		{
			get
			{
				return this._Description;
			}
			set
			{
				this._Description = value;
				this.HasDescription = (value != null);
			}
		}

		// Token: 0x0600C981 RID: 51585 RVA: 0x003C64A0 File Offset: 0x003C46A0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasDeviceInfo)
			{
				num ^= this.DeviceInfo.GetHashCode();
			}
			if (this.HasErrorCode)
			{
				num ^= this.ErrorCode.GetHashCode();
			}
			if (this.HasDescription)
			{
				num ^= this.Description.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600C982 RID: 51586 RVA: 0x003C6500 File Offset: 0x003C4700
		public override bool Equals(object obj)
		{
			FatalBattleNetError fatalBattleNetError = obj as FatalBattleNetError;
			return fatalBattleNetError != null && this.HasDeviceInfo == fatalBattleNetError.HasDeviceInfo && (!this.HasDeviceInfo || this.DeviceInfo.Equals(fatalBattleNetError.DeviceInfo)) && this.HasErrorCode == fatalBattleNetError.HasErrorCode && (!this.HasErrorCode || this.ErrorCode.Equals(fatalBattleNetError.ErrorCode)) && this.HasDescription == fatalBattleNetError.HasDescription && (!this.HasDescription || this.Description.Equals(fatalBattleNetError.Description));
		}

		// Token: 0x0600C983 RID: 51587 RVA: 0x003C659E File Offset: 0x003C479E
		public void Deserialize(Stream stream)
		{
			FatalBattleNetError.Deserialize(stream, this);
		}

		// Token: 0x0600C984 RID: 51588 RVA: 0x003C65A8 File Offset: 0x003C47A8
		public static FatalBattleNetError Deserialize(Stream stream, FatalBattleNetError instance)
		{
			return FatalBattleNetError.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600C985 RID: 51589 RVA: 0x003C65B4 File Offset: 0x003C47B4
		public static FatalBattleNetError DeserializeLengthDelimited(Stream stream)
		{
			FatalBattleNetError fatalBattleNetError = new FatalBattleNetError();
			FatalBattleNetError.DeserializeLengthDelimited(stream, fatalBattleNetError);
			return fatalBattleNetError;
		}

		// Token: 0x0600C986 RID: 51590 RVA: 0x003C65D0 File Offset: 0x003C47D0
		public static FatalBattleNetError DeserializeLengthDelimited(Stream stream, FatalBattleNetError instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return FatalBattleNetError.Deserialize(stream, instance, num);
		}

		// Token: 0x0600C987 RID: 51591 RVA: 0x003C65F8 File Offset: 0x003C47F8
		public static FatalBattleNetError Deserialize(Stream stream, FatalBattleNetError instance, long limit)
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
					if (num != 16)
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
							instance.Description = ProtocolParser.ReadString(stream);
						}
					}
					else
					{
						instance.ErrorCode = (int)ProtocolParser.ReadUInt64(stream);
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
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600C988 RID: 51592 RVA: 0x003C66C7 File Offset: 0x003C48C7
		public void Serialize(Stream stream)
		{
			FatalBattleNetError.Serialize(stream, this);
		}

		// Token: 0x0600C989 RID: 51593 RVA: 0x003C66D0 File Offset: 0x003C48D0
		public static void Serialize(Stream stream, FatalBattleNetError instance)
		{
			if (instance.HasDeviceInfo)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.DeviceInfo.GetSerializedSize());
				DeviceInfo.Serialize(stream, instance.DeviceInfo);
			}
			if (instance.HasErrorCode)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.ErrorCode));
			}
			if (instance.HasDescription)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Description));
			}
		}

		// Token: 0x0600C98A RID: 51594 RVA: 0x003C6750 File Offset: 0x003C4950
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasDeviceInfo)
			{
				num += 1U;
				uint serializedSize = this.DeviceInfo.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasErrorCode)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.ErrorCode));
			}
			if (this.HasDescription)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Description);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num;
		}

		// Token: 0x04009F55 RID: 40789
		public bool HasDeviceInfo;

		// Token: 0x04009F56 RID: 40790
		private DeviceInfo _DeviceInfo;

		// Token: 0x04009F57 RID: 40791
		public bool HasErrorCode;

		// Token: 0x04009F58 RID: 40792
		private int _ErrorCode;

		// Token: 0x04009F59 RID: 40793
		public bool HasDescription;

		// Token: 0x04009F5A RID: 40794
		private string _Description;
	}
}
