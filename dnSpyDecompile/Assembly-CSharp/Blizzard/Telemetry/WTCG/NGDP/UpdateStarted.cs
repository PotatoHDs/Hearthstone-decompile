using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.NGDP
{
	// Token: 0x02001186 RID: 4486
	public class UpdateStarted : IProtoBuf
	{
		// Token: 0x17000E24 RID: 3620
		// (get) Token: 0x0600C55C RID: 50524 RVA: 0x003B737C File Offset: 0x003B557C
		// (set) Token: 0x0600C55D RID: 50525 RVA: 0x003B7384 File Offset: 0x003B5584
		public string InstalledVersion
		{
			get
			{
				return this._InstalledVersion;
			}
			set
			{
				this._InstalledVersion = value;
				this.HasInstalledVersion = (value != null);
			}
		}

		// Token: 0x17000E25 RID: 3621
		// (get) Token: 0x0600C55E RID: 50526 RVA: 0x003B7397 File Offset: 0x003B5597
		// (set) Token: 0x0600C55F RID: 50527 RVA: 0x003B739F File Offset: 0x003B559F
		public string TextureFormat
		{
			get
			{
				return this._TextureFormat;
			}
			set
			{
				this._TextureFormat = value;
				this.HasTextureFormat = (value != null);
			}
		}

		// Token: 0x17000E26 RID: 3622
		// (get) Token: 0x0600C560 RID: 50528 RVA: 0x003B73B2 File Offset: 0x003B55B2
		// (set) Token: 0x0600C561 RID: 50529 RVA: 0x003B73BA File Offset: 0x003B55BA
		public string DataPath
		{
			get
			{
				return this._DataPath;
			}
			set
			{
				this._DataPath = value;
				this.HasDataPath = (value != null);
			}
		}

		// Token: 0x17000E27 RID: 3623
		// (get) Token: 0x0600C562 RID: 50530 RVA: 0x003B73CD File Offset: 0x003B55CD
		// (set) Token: 0x0600C563 RID: 50531 RVA: 0x003B73D5 File Offset: 0x003B55D5
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

		// Token: 0x0600C564 RID: 50532 RVA: 0x003B73E8 File Offset: 0x003B55E8
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasInstalledVersion)
			{
				num ^= this.InstalledVersion.GetHashCode();
			}
			if (this.HasTextureFormat)
			{
				num ^= this.TextureFormat.GetHashCode();
			}
			if (this.HasDataPath)
			{
				num ^= this.DataPath.GetHashCode();
			}
			if (this.HasAvailableSpaceMB)
			{
				num ^= this.AvailableSpaceMB.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600C565 RID: 50533 RVA: 0x003B7460 File Offset: 0x003B5660
		public override bool Equals(object obj)
		{
			UpdateStarted updateStarted = obj as UpdateStarted;
			return updateStarted != null && this.HasInstalledVersion == updateStarted.HasInstalledVersion && (!this.HasInstalledVersion || this.InstalledVersion.Equals(updateStarted.InstalledVersion)) && this.HasTextureFormat == updateStarted.HasTextureFormat && (!this.HasTextureFormat || this.TextureFormat.Equals(updateStarted.TextureFormat)) && this.HasDataPath == updateStarted.HasDataPath && (!this.HasDataPath || this.DataPath.Equals(updateStarted.DataPath)) && this.HasAvailableSpaceMB == updateStarted.HasAvailableSpaceMB && (!this.HasAvailableSpaceMB || this.AvailableSpaceMB.Equals(updateStarted.AvailableSpaceMB));
		}

		// Token: 0x0600C566 RID: 50534 RVA: 0x003B7529 File Offset: 0x003B5729
		public void Deserialize(Stream stream)
		{
			UpdateStarted.Deserialize(stream, this);
		}

		// Token: 0x0600C567 RID: 50535 RVA: 0x003B7533 File Offset: 0x003B5733
		public static UpdateStarted Deserialize(Stream stream, UpdateStarted instance)
		{
			return UpdateStarted.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600C568 RID: 50536 RVA: 0x003B7540 File Offset: 0x003B5740
		public static UpdateStarted DeserializeLengthDelimited(Stream stream)
		{
			UpdateStarted updateStarted = new UpdateStarted();
			UpdateStarted.DeserializeLengthDelimited(stream, updateStarted);
			return updateStarted;
		}

		// Token: 0x0600C569 RID: 50537 RVA: 0x003B755C File Offset: 0x003B575C
		public static UpdateStarted DeserializeLengthDelimited(Stream stream, UpdateStarted instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return UpdateStarted.Deserialize(stream, instance, num);
		}

		// Token: 0x0600C56A RID: 50538 RVA: 0x003B7584 File Offset: 0x003B5784
		public static UpdateStarted Deserialize(Stream stream, UpdateStarted instance, long limit)
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
				else
				{
					if (num <= 18)
					{
						if (num == 10)
						{
							instance.InstalledVersion = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 18)
						{
							instance.TextureFormat = ProtocolParser.ReadString(stream);
							continue;
						}
					}
					else
					{
						if (num == 26)
						{
							instance.DataPath = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 37)
						{
							instance.AvailableSpaceMB = binaryReader.ReadSingle();
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

		// Token: 0x0600C56B RID: 50539 RVA: 0x003B765C File Offset: 0x003B585C
		public void Serialize(Stream stream)
		{
			UpdateStarted.Serialize(stream, this);
		}

		// Token: 0x0600C56C RID: 50540 RVA: 0x003B7668 File Offset: 0x003B5868
		public static void Serialize(Stream stream, UpdateStarted instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasInstalledVersion)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.InstalledVersion));
			}
			if (instance.HasTextureFormat)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.TextureFormat));
			}
			if (instance.HasDataPath)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.DataPath));
			}
			if (instance.HasAvailableSpaceMB)
			{
				stream.WriteByte(37);
				binaryWriter.Write(instance.AvailableSpaceMB);
			}
		}

		// Token: 0x0600C56D RID: 50541 RVA: 0x003B770C File Offset: 0x003B590C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasInstalledVersion)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.InstalledVersion);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasTextureFormat)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.TextureFormat);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (this.HasDataPath)
			{
				num += 1U;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(this.DataPath);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			if (this.HasAvailableSpaceMB)
			{
				num += 1U;
				num += 4U;
			}
			return num;
		}

		// Token: 0x04009D95 RID: 40341
		public bool HasInstalledVersion;

		// Token: 0x04009D96 RID: 40342
		private string _InstalledVersion;

		// Token: 0x04009D97 RID: 40343
		public bool HasTextureFormat;

		// Token: 0x04009D98 RID: 40344
		private string _TextureFormat;

		// Token: 0x04009D99 RID: 40345
		public bool HasDataPath;

		// Token: 0x04009D9A RID: 40346
		private string _DataPath;

		// Token: 0x04009D9B RID: 40347
		public bool HasAvailableSpaceMB;

		// Token: 0x04009D9C RID: 40348
		private float _AvailableSpaceMB;
	}
}
