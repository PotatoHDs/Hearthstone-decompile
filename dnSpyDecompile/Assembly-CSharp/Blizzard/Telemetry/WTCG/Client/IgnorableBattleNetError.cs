using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011C7 RID: 4551
	public class IgnorableBattleNetError : IProtoBuf
	{
		// Token: 0x17000F35 RID: 3893
		// (get) Token: 0x0600CA49 RID: 51785 RVA: 0x003C9504 File Offset: 0x003C7704
		// (set) Token: 0x0600CA4A RID: 51786 RVA: 0x003C950C File Offset: 0x003C770C
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

		// Token: 0x17000F36 RID: 3894
		// (get) Token: 0x0600CA4B RID: 51787 RVA: 0x003C951F File Offset: 0x003C771F
		// (set) Token: 0x0600CA4C RID: 51788 RVA: 0x003C9527 File Offset: 0x003C7727
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

		// Token: 0x17000F37 RID: 3895
		// (get) Token: 0x0600CA4D RID: 51789 RVA: 0x003C9537 File Offset: 0x003C7737
		// (set) Token: 0x0600CA4E RID: 51790 RVA: 0x003C953F File Offset: 0x003C773F
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

		// Token: 0x0600CA4F RID: 51791 RVA: 0x003C9554 File Offset: 0x003C7754
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

		// Token: 0x0600CA50 RID: 51792 RVA: 0x003C95B4 File Offset: 0x003C77B4
		public override bool Equals(object obj)
		{
			IgnorableBattleNetError ignorableBattleNetError = obj as IgnorableBattleNetError;
			return ignorableBattleNetError != null && this.HasDeviceInfo == ignorableBattleNetError.HasDeviceInfo && (!this.HasDeviceInfo || this.DeviceInfo.Equals(ignorableBattleNetError.DeviceInfo)) && this.HasErrorCode == ignorableBattleNetError.HasErrorCode && (!this.HasErrorCode || this.ErrorCode.Equals(ignorableBattleNetError.ErrorCode)) && this.HasDescription == ignorableBattleNetError.HasDescription && (!this.HasDescription || this.Description.Equals(ignorableBattleNetError.Description));
		}

		// Token: 0x0600CA51 RID: 51793 RVA: 0x003C9652 File Offset: 0x003C7852
		public void Deserialize(Stream stream)
		{
			IgnorableBattleNetError.Deserialize(stream, this);
		}

		// Token: 0x0600CA52 RID: 51794 RVA: 0x003C965C File Offset: 0x003C785C
		public static IgnorableBattleNetError Deserialize(Stream stream, IgnorableBattleNetError instance)
		{
			return IgnorableBattleNetError.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600CA53 RID: 51795 RVA: 0x003C9668 File Offset: 0x003C7868
		public static IgnorableBattleNetError DeserializeLengthDelimited(Stream stream)
		{
			IgnorableBattleNetError ignorableBattleNetError = new IgnorableBattleNetError();
			IgnorableBattleNetError.DeserializeLengthDelimited(stream, ignorableBattleNetError);
			return ignorableBattleNetError;
		}

		// Token: 0x0600CA54 RID: 51796 RVA: 0x003C9684 File Offset: 0x003C7884
		public static IgnorableBattleNetError DeserializeLengthDelimited(Stream stream, IgnorableBattleNetError instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return IgnorableBattleNetError.Deserialize(stream, instance, num);
		}

		// Token: 0x0600CA55 RID: 51797 RVA: 0x003C96AC File Offset: 0x003C78AC
		public static IgnorableBattleNetError Deserialize(Stream stream, IgnorableBattleNetError instance, long limit)
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

		// Token: 0x0600CA56 RID: 51798 RVA: 0x003C977B File Offset: 0x003C797B
		public void Serialize(Stream stream)
		{
			IgnorableBattleNetError.Serialize(stream, this);
		}

		// Token: 0x0600CA57 RID: 51799 RVA: 0x003C9784 File Offset: 0x003C7984
		public static void Serialize(Stream stream, IgnorableBattleNetError instance)
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

		// Token: 0x0600CA58 RID: 51800 RVA: 0x003C9804 File Offset: 0x003C7A04
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

		// Token: 0x04009FB5 RID: 40885
		public bool HasDeviceInfo;

		// Token: 0x04009FB6 RID: 40886
		private DeviceInfo _DeviceInfo;

		// Token: 0x04009FB7 RID: 40887
		public bool HasErrorCode;

		// Token: 0x04009FB8 RID: 40888
		private int _ErrorCode;

		// Token: 0x04009FB9 RID: 40889
		public bool HasDescription;

		// Token: 0x04009FBA RID: 40890
		private string _Description;
	}
}
