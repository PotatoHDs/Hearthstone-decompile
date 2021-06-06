using System;
using System.IO;
using System.Text;

namespace bnet.protocol.authentication.v1
{
	// Token: 0x020004F4 RID: 1268
	public class VersionInfo : IProtoBuf
	{
		// Token: 0x17001106 RID: 4358
		// (get) Token: 0x06005A15 RID: 23061 RVA: 0x001134FE File Offset: 0x001116FE
		// (set) Token: 0x06005A16 RID: 23062 RVA: 0x00113506 File Offset: 0x00111706
		public uint Number
		{
			get
			{
				return this._Number;
			}
			set
			{
				this._Number = value;
				this.HasNumber = true;
			}
		}

		// Token: 0x06005A17 RID: 23063 RVA: 0x00113516 File Offset: 0x00111716
		public void SetNumber(uint val)
		{
			this.Number = val;
		}

		// Token: 0x17001107 RID: 4359
		// (get) Token: 0x06005A18 RID: 23064 RVA: 0x0011351F File Offset: 0x0011171F
		// (set) Token: 0x06005A19 RID: 23065 RVA: 0x00113527 File Offset: 0x00111727
		public string Patch
		{
			get
			{
				return this._Patch;
			}
			set
			{
				this._Patch = value;
				this.HasPatch = (value != null);
			}
		}

		// Token: 0x06005A1A RID: 23066 RVA: 0x0011353A File Offset: 0x0011173A
		public void SetPatch(string val)
		{
			this.Patch = val;
		}

		// Token: 0x17001108 RID: 4360
		// (get) Token: 0x06005A1B RID: 23067 RVA: 0x00113543 File Offset: 0x00111743
		// (set) Token: 0x06005A1C RID: 23068 RVA: 0x0011354B File Offset: 0x0011174B
		public bool IsOptional
		{
			get
			{
				return this._IsOptional;
			}
			set
			{
				this._IsOptional = value;
				this.HasIsOptional = true;
			}
		}

		// Token: 0x06005A1D RID: 23069 RVA: 0x0011355B File Offset: 0x0011175B
		public void SetIsOptional(bool val)
		{
			this.IsOptional = val;
		}

		// Token: 0x17001109 RID: 4361
		// (get) Token: 0x06005A1E RID: 23070 RVA: 0x00113564 File Offset: 0x00111764
		// (set) Token: 0x06005A1F RID: 23071 RVA: 0x0011356C File Offset: 0x0011176C
		public ulong KickTime
		{
			get
			{
				return this._KickTime;
			}
			set
			{
				this._KickTime = value;
				this.HasKickTime = true;
			}
		}

		// Token: 0x06005A20 RID: 23072 RVA: 0x0011357C File Offset: 0x0011177C
		public void SetKickTime(ulong val)
		{
			this.KickTime = val;
		}

		// Token: 0x06005A21 RID: 23073 RVA: 0x00113588 File Offset: 0x00111788
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasNumber)
			{
				num ^= this.Number.GetHashCode();
			}
			if (this.HasPatch)
			{
				num ^= this.Patch.GetHashCode();
			}
			if (this.HasIsOptional)
			{
				num ^= this.IsOptional.GetHashCode();
			}
			if (this.HasKickTime)
			{
				num ^= this.KickTime.GetHashCode();
			}
			return num;
		}

		// Token: 0x06005A22 RID: 23074 RVA: 0x00113604 File Offset: 0x00111804
		public override bool Equals(object obj)
		{
			VersionInfo versionInfo = obj as VersionInfo;
			return versionInfo != null && this.HasNumber == versionInfo.HasNumber && (!this.HasNumber || this.Number.Equals(versionInfo.Number)) && this.HasPatch == versionInfo.HasPatch && (!this.HasPatch || this.Patch.Equals(versionInfo.Patch)) && this.HasIsOptional == versionInfo.HasIsOptional && (!this.HasIsOptional || this.IsOptional.Equals(versionInfo.IsOptional)) && this.HasKickTime == versionInfo.HasKickTime && (!this.HasKickTime || this.KickTime.Equals(versionInfo.KickTime));
		}

		// Token: 0x1700110A RID: 4362
		// (get) Token: 0x06005A23 RID: 23075 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005A24 RID: 23076 RVA: 0x001136D3 File Offset: 0x001118D3
		public static VersionInfo ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<VersionInfo>(bs, 0, -1);
		}

		// Token: 0x06005A25 RID: 23077 RVA: 0x001136DD File Offset: 0x001118DD
		public void Deserialize(Stream stream)
		{
			VersionInfo.Deserialize(stream, this);
		}

		// Token: 0x06005A26 RID: 23078 RVA: 0x001136E7 File Offset: 0x001118E7
		public static VersionInfo Deserialize(Stream stream, VersionInfo instance)
		{
			return VersionInfo.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005A27 RID: 23079 RVA: 0x001136F4 File Offset: 0x001118F4
		public static VersionInfo DeserializeLengthDelimited(Stream stream)
		{
			VersionInfo versionInfo = new VersionInfo();
			VersionInfo.DeserializeLengthDelimited(stream, versionInfo);
			return versionInfo;
		}

		// Token: 0x06005A28 RID: 23080 RVA: 0x00113710 File Offset: 0x00111910
		public static VersionInfo DeserializeLengthDelimited(Stream stream, VersionInfo instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return VersionInfo.Deserialize(stream, instance, num);
		}

		// Token: 0x06005A29 RID: 23081 RVA: 0x00113738 File Offset: 0x00111938
		public static VersionInfo Deserialize(Stream stream, VersionInfo instance, long limit)
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
						if (num == 8)
						{
							instance.Number = ProtocolParser.ReadUInt32(stream);
							continue;
						}
						if (num == 18)
						{
							instance.Patch = ProtocolParser.ReadString(stream);
							continue;
						}
					}
					else
					{
						if (num == 24)
						{
							instance.IsOptional = ProtocolParser.ReadBool(stream);
							continue;
						}
						if (num == 32)
						{
							instance.KickTime = ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x06005A2A RID: 23082 RVA: 0x00113808 File Offset: 0x00111A08
		public void Serialize(Stream stream)
		{
			VersionInfo.Serialize(stream, this);
		}

		// Token: 0x06005A2B RID: 23083 RVA: 0x00113814 File Offset: 0x00111A14
		public static void Serialize(Stream stream, VersionInfo instance)
		{
			if (instance.HasNumber)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt32(stream, instance.Number);
			}
			if (instance.HasPatch)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Patch));
			}
			if (instance.HasIsOptional)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteBool(stream, instance.IsOptional);
			}
			if (instance.HasKickTime)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, instance.KickTime);
			}
		}

		// Token: 0x06005A2C RID: 23084 RVA: 0x0011389C File Offset: 0x00111A9C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasNumber)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.Number);
			}
			if (this.HasPatch)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Patch);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasIsOptional)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasKickTime)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.KickTime);
			}
			return num;
		}

		// Token: 0x04001C15 RID: 7189
		public bool HasNumber;

		// Token: 0x04001C16 RID: 7190
		private uint _Number;

		// Token: 0x04001C17 RID: 7191
		public bool HasPatch;

		// Token: 0x04001C18 RID: 7192
		private string _Patch;

		// Token: 0x04001C19 RID: 7193
		public bool HasIsOptional;

		// Token: 0x04001C1A RID: 7194
		private bool _IsOptional;

		// Token: 0x04001C1B RID: 7195
		public bool HasKickTime;

		// Token: 0x04001C1C RID: 7196
		private ulong _KickTime;
	}
}
