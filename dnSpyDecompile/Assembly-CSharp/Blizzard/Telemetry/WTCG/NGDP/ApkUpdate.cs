using System;
using System.IO;

namespace Blizzard.Telemetry.WTCG.NGDP
{
	// Token: 0x0200117E RID: 4478
	public class ApkUpdate : IProtoBuf
	{
		// Token: 0x17000E09 RID: 3593
		// (get) Token: 0x0600C4CE RID: 50382 RVA: 0x003B5748 File Offset: 0x003B3948
		// (set) Token: 0x0600C4CF RID: 50383 RVA: 0x003B5750 File Offset: 0x003B3950
		public int InstalledVersion
		{
			get
			{
				return this._InstalledVersion;
			}
			set
			{
				this._InstalledVersion = value;
				this.HasInstalledVersion = true;
			}
		}

		// Token: 0x17000E0A RID: 3594
		// (get) Token: 0x0600C4D0 RID: 50384 RVA: 0x003B5760 File Offset: 0x003B3960
		// (set) Token: 0x0600C4D1 RID: 50385 RVA: 0x003B5768 File Offset: 0x003B3968
		public int AssetVersion
		{
			get
			{
				return this._AssetVersion;
			}
			set
			{
				this._AssetVersion = value;
				this.HasAssetVersion = true;
			}
		}

		// Token: 0x17000E0B RID: 3595
		// (get) Token: 0x0600C4D2 RID: 50386 RVA: 0x003B5778 File Offset: 0x003B3978
		// (set) Token: 0x0600C4D3 RID: 50387 RVA: 0x003B5780 File Offset: 0x003B3980
		public int AgentVersion
		{
			get
			{
				return this._AgentVersion;
			}
			set
			{
				this._AgentVersion = value;
				this.HasAgentVersion = true;
			}
		}

		// Token: 0x0600C4D4 RID: 50388 RVA: 0x003B5790 File Offset: 0x003B3990
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasInstalledVersion)
			{
				num ^= this.InstalledVersion.GetHashCode();
			}
			if (this.HasAssetVersion)
			{
				num ^= this.AssetVersion.GetHashCode();
			}
			if (this.HasAgentVersion)
			{
				num ^= this.AgentVersion.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600C4D5 RID: 50389 RVA: 0x003B57F8 File Offset: 0x003B39F8
		public override bool Equals(object obj)
		{
			ApkUpdate apkUpdate = obj as ApkUpdate;
			return apkUpdate != null && this.HasInstalledVersion == apkUpdate.HasInstalledVersion && (!this.HasInstalledVersion || this.InstalledVersion.Equals(apkUpdate.InstalledVersion)) && this.HasAssetVersion == apkUpdate.HasAssetVersion && (!this.HasAssetVersion || this.AssetVersion.Equals(apkUpdate.AssetVersion)) && this.HasAgentVersion == apkUpdate.HasAgentVersion && (!this.HasAgentVersion || this.AgentVersion.Equals(apkUpdate.AgentVersion));
		}

		// Token: 0x0600C4D6 RID: 50390 RVA: 0x003B589C File Offset: 0x003B3A9C
		public void Deserialize(Stream stream)
		{
			ApkUpdate.Deserialize(stream, this);
		}

		// Token: 0x0600C4D7 RID: 50391 RVA: 0x003B58A6 File Offset: 0x003B3AA6
		public static ApkUpdate Deserialize(Stream stream, ApkUpdate instance)
		{
			return ApkUpdate.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600C4D8 RID: 50392 RVA: 0x003B58B4 File Offset: 0x003B3AB4
		public static ApkUpdate DeserializeLengthDelimited(Stream stream)
		{
			ApkUpdate apkUpdate = new ApkUpdate();
			ApkUpdate.DeserializeLengthDelimited(stream, apkUpdate);
			return apkUpdate;
		}

		// Token: 0x0600C4D9 RID: 50393 RVA: 0x003B58D0 File Offset: 0x003B3AD0
		public static ApkUpdate DeserializeLengthDelimited(Stream stream, ApkUpdate instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ApkUpdate.Deserialize(stream, instance, num);
		}

		// Token: 0x0600C4DA RID: 50394 RVA: 0x003B58F8 File Offset: 0x003B3AF8
		public static ApkUpdate Deserialize(Stream stream, ApkUpdate instance, long limit)
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
				else if (num != 8)
				{
					if (num != 16)
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
							instance.AgentVersion = ProtocolParser.ReadZInt32(stream);
						}
					}
					else
					{
						instance.AssetVersion = ProtocolParser.ReadZInt32(stream);
					}
				}
				else
				{
					instance.InstalledVersion = ProtocolParser.ReadZInt32(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600C4DB RID: 50395 RVA: 0x003B59A5 File Offset: 0x003B3BA5
		public void Serialize(Stream stream)
		{
			ApkUpdate.Serialize(stream, this);
		}

		// Token: 0x0600C4DC RID: 50396 RVA: 0x003B59B0 File Offset: 0x003B3BB0
		public static void Serialize(Stream stream, ApkUpdate instance)
		{
			if (instance.HasInstalledVersion)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteZInt32(stream, instance.InstalledVersion);
			}
			if (instance.HasAssetVersion)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteZInt32(stream, instance.AssetVersion);
			}
			if (instance.HasAgentVersion)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteZInt32(stream, instance.AgentVersion);
			}
		}

		// Token: 0x0600C4DD RID: 50397 RVA: 0x003B5A10 File Offset: 0x003B3C10
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasInstalledVersion)
			{
				num += 1U;
				num += ProtocolParser.SizeOfZInt32(this.InstalledVersion);
			}
			if (this.HasAssetVersion)
			{
				num += 1U;
				num += ProtocolParser.SizeOfZInt32(this.AssetVersion);
			}
			if (this.HasAgentVersion)
			{
				num += 1U;
				num += ProtocolParser.SizeOfZInt32(this.AgentVersion);
			}
			return num;
		}

		// Token: 0x04009D5F RID: 40287
		public bool HasInstalledVersion;

		// Token: 0x04009D60 RID: 40288
		private int _InstalledVersion;

		// Token: 0x04009D61 RID: 40289
		public bool HasAssetVersion;

		// Token: 0x04009D62 RID: 40290
		private int _AssetVersion;

		// Token: 0x04009D63 RID: 40291
		public bool HasAgentVersion;

		// Token: 0x04009D64 RID: 40292
		private int _AgentVersion;
	}
}
