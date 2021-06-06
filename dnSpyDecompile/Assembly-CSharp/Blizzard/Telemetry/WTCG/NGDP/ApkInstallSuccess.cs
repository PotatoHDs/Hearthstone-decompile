using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.NGDP
{
	// Token: 0x0200117D RID: 4477
	public class ApkInstallSuccess : IProtoBuf
	{
		// Token: 0x17000E06 RID: 3590
		// (get) Token: 0x0600C4BD RID: 50365 RVA: 0x003B5410 File Offset: 0x003B3610
		// (set) Token: 0x0600C4BE RID: 50366 RVA: 0x003B5418 File Offset: 0x003B3618
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

		// Token: 0x17000E07 RID: 3591
		// (get) Token: 0x0600C4BF RID: 50367 RVA: 0x003B542B File Offset: 0x003B362B
		// (set) Token: 0x0600C4C0 RID: 50368 RVA: 0x003B5433 File Offset: 0x003B3633
		public float AvailableSpaceMB
		{
			get
			{
				return this._AvailableSpaceMB;
			}
			set
			{
				this._AvailableSpaceMB = value;
				this.HasAvailableSpaceMB = true;
			}
		}

		// Token: 0x17000E08 RID: 3592
		// (get) Token: 0x0600C4C1 RID: 50369 RVA: 0x003B5443 File Offset: 0x003B3643
		// (set) Token: 0x0600C4C2 RID: 50370 RVA: 0x003B544B File Offset: 0x003B364B
		public float ElapsedSeconds
		{
			get
			{
				return this._ElapsedSeconds;
			}
			set
			{
				this._ElapsedSeconds = value;
				this.HasElapsedSeconds = true;
			}
		}

		// Token: 0x0600C4C3 RID: 50371 RVA: 0x003B545C File Offset: 0x003B365C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasUpdatedVersion)
			{
				num ^= this.UpdatedVersion.GetHashCode();
			}
			if (this.HasAvailableSpaceMB)
			{
				num ^= this.AvailableSpaceMB.GetHashCode();
			}
			if (this.HasElapsedSeconds)
			{
				num ^= this.ElapsedSeconds.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600C4C4 RID: 50372 RVA: 0x003B54C0 File Offset: 0x003B36C0
		public override bool Equals(object obj)
		{
			ApkInstallSuccess apkInstallSuccess = obj as ApkInstallSuccess;
			return apkInstallSuccess != null && this.HasUpdatedVersion == apkInstallSuccess.HasUpdatedVersion && (!this.HasUpdatedVersion || this.UpdatedVersion.Equals(apkInstallSuccess.UpdatedVersion)) && this.HasAvailableSpaceMB == apkInstallSuccess.HasAvailableSpaceMB && (!this.HasAvailableSpaceMB || this.AvailableSpaceMB.Equals(apkInstallSuccess.AvailableSpaceMB)) && this.HasElapsedSeconds == apkInstallSuccess.HasElapsedSeconds && (!this.HasElapsedSeconds || this.ElapsedSeconds.Equals(apkInstallSuccess.ElapsedSeconds));
		}

		// Token: 0x0600C4C5 RID: 50373 RVA: 0x003B5561 File Offset: 0x003B3761
		public void Deserialize(Stream stream)
		{
			ApkInstallSuccess.Deserialize(stream, this);
		}

		// Token: 0x0600C4C6 RID: 50374 RVA: 0x003B556B File Offset: 0x003B376B
		public static ApkInstallSuccess Deserialize(Stream stream, ApkInstallSuccess instance)
		{
			return ApkInstallSuccess.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600C4C7 RID: 50375 RVA: 0x003B5578 File Offset: 0x003B3778
		public static ApkInstallSuccess DeserializeLengthDelimited(Stream stream)
		{
			ApkInstallSuccess apkInstallSuccess = new ApkInstallSuccess();
			ApkInstallSuccess.DeserializeLengthDelimited(stream, apkInstallSuccess);
			return apkInstallSuccess;
		}

		// Token: 0x0600C4C8 RID: 50376 RVA: 0x003B5594 File Offset: 0x003B3794
		public static ApkInstallSuccess DeserializeLengthDelimited(Stream stream, ApkInstallSuccess instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ApkInstallSuccess.Deserialize(stream, instance, num);
		}

		// Token: 0x0600C4C9 RID: 50377 RVA: 0x003B55BC File Offset: 0x003B37BC
		public static ApkInstallSuccess Deserialize(Stream stream, ApkInstallSuccess instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
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
					if (num != 29)
					{
						if (num != 37)
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
							instance.ElapsedSeconds = binaryReader.ReadSingle();
						}
					}
					else
					{
						instance.AvailableSpaceMB = binaryReader.ReadSingle();
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

		// Token: 0x0600C4CA RID: 50378 RVA: 0x003B5671 File Offset: 0x003B3871
		public void Serialize(Stream stream)
		{
			ApkInstallSuccess.Serialize(stream, this);
		}

		// Token: 0x0600C4CB RID: 50379 RVA: 0x003B567C File Offset: 0x003B387C
		public static void Serialize(Stream stream, ApkInstallSuccess instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasUpdatedVersion)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.UpdatedVersion));
			}
			if (instance.HasAvailableSpaceMB)
			{
				stream.WriteByte(29);
				binaryWriter.Write(instance.AvailableSpaceMB);
			}
			if (instance.HasElapsedSeconds)
			{
				stream.WriteByte(37);
				binaryWriter.Write(instance.ElapsedSeconds);
			}
		}

		// Token: 0x0600C4CC RID: 50380 RVA: 0x003B56F0 File Offset: 0x003B38F0
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasUpdatedVersion)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.UpdatedVersion);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasAvailableSpaceMB)
			{
				num += 1U;
				num += 4U;
			}
			if (this.HasElapsedSeconds)
			{
				num += 1U;
				num += 4U;
			}
			return num;
		}

		// Token: 0x04009D59 RID: 40281
		public bool HasUpdatedVersion;

		// Token: 0x04009D5A RID: 40282
		private string _UpdatedVersion;

		// Token: 0x04009D5B RID: 40283
		public bool HasAvailableSpaceMB;

		// Token: 0x04009D5C RID: 40284
		private float _AvailableSpaceMB;

		// Token: 0x04009D5D RID: 40285
		public bool HasElapsedSeconds;

		// Token: 0x04009D5E RID: 40286
		private float _ElapsedSeconds;
	}
}
