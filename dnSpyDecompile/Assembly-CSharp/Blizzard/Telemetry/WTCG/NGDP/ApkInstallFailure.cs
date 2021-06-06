using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.NGDP
{
	// Token: 0x0200117C RID: 4476
	public class ApkInstallFailure : IProtoBuf
	{
		// Token: 0x17000E04 RID: 3588
		// (get) Token: 0x0600C4AE RID: 50350 RVA: 0x003B5165 File Offset: 0x003B3365
		// (set) Token: 0x0600C4AF RID: 50351 RVA: 0x003B516D File Offset: 0x003B336D
		public string UpdatedVersion
		{
			get
			{
				return this._UpdatedVersion;
			}
			set
			{
				this._UpdatedVersion = value;
				this.HasUpdatedVersion = (value != null);
			}
		}

		// Token: 0x17000E05 RID: 3589
		// (get) Token: 0x0600C4B0 RID: 50352 RVA: 0x003B5180 File Offset: 0x003B3380
		// (set) Token: 0x0600C4B1 RID: 50353 RVA: 0x003B5188 File Offset: 0x003B3388
		public string Reason
		{
			get
			{
				return this._Reason;
			}
			set
			{
				this._Reason = value;
				this.HasReason = (value != null);
			}
		}

		// Token: 0x0600C4B2 RID: 50354 RVA: 0x003B519C File Offset: 0x003B339C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasUpdatedVersion)
			{
				num ^= this.UpdatedVersion.GetHashCode();
			}
			if (this.HasReason)
			{
				num ^= this.Reason.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600C4B3 RID: 50355 RVA: 0x003B51E4 File Offset: 0x003B33E4
		public override bool Equals(object obj)
		{
			ApkInstallFailure apkInstallFailure = obj as ApkInstallFailure;
			return apkInstallFailure != null && this.HasUpdatedVersion == apkInstallFailure.HasUpdatedVersion && (!this.HasUpdatedVersion || this.UpdatedVersion.Equals(apkInstallFailure.UpdatedVersion)) && this.HasReason == apkInstallFailure.HasReason && (!this.HasReason || this.Reason.Equals(apkInstallFailure.Reason));
		}

		// Token: 0x0600C4B4 RID: 50356 RVA: 0x003B5254 File Offset: 0x003B3454
		public void Deserialize(Stream stream)
		{
			ApkInstallFailure.Deserialize(stream, this);
		}

		// Token: 0x0600C4B5 RID: 50357 RVA: 0x003B525E File Offset: 0x003B345E
		public static ApkInstallFailure Deserialize(Stream stream, ApkInstallFailure instance)
		{
			return ApkInstallFailure.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600C4B6 RID: 50358 RVA: 0x003B526C File Offset: 0x003B346C
		public static ApkInstallFailure DeserializeLengthDelimited(Stream stream)
		{
			ApkInstallFailure apkInstallFailure = new ApkInstallFailure();
			ApkInstallFailure.DeserializeLengthDelimited(stream, apkInstallFailure);
			return apkInstallFailure;
		}

		// Token: 0x0600C4B7 RID: 50359 RVA: 0x003B5288 File Offset: 0x003B3488
		public static ApkInstallFailure DeserializeLengthDelimited(Stream stream, ApkInstallFailure instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ApkInstallFailure.Deserialize(stream, instance, num);
		}

		// Token: 0x0600C4B8 RID: 50360 RVA: 0x003B52B0 File Offset: 0x003B34B0
		public static ApkInstallFailure Deserialize(Stream stream, ApkInstallFailure instance, long limit)
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
				else if (num != 18)
				{
					if (num != 42)
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
						instance.Reason = ProtocolParser.ReadString(stream);
					}
				}
				else
				{
					instance.UpdatedVersion = ProtocolParser.ReadString(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600C4B9 RID: 50361 RVA: 0x003B5348 File Offset: 0x003B3548
		public void Serialize(Stream stream)
		{
			ApkInstallFailure.Serialize(stream, this);
		}

		// Token: 0x0600C4BA RID: 50362 RVA: 0x003B5354 File Offset: 0x003B3554
		public static void Serialize(Stream stream, ApkInstallFailure instance)
		{
			if (instance.HasUpdatedVersion)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.UpdatedVersion));
			}
			if (instance.HasReason)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Reason));
			}
		}

		// Token: 0x0600C4BB RID: 50363 RVA: 0x003B53B0 File Offset: 0x003B35B0
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasUpdatedVersion)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.UpdatedVersion);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasReason)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.Reason);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			return num;
		}

		// Token: 0x04009D55 RID: 40277
		public bool HasUpdatedVersion;

		// Token: 0x04009D56 RID: 40278
		private string _UpdatedVersion;

		// Token: 0x04009D57 RID: 40279
		public bool HasReason;

		// Token: 0x04009D58 RID: 40280
		private string _Reason;
	}
}
