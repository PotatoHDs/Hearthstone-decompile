using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.NGDP
{
	// Token: 0x02001189 RID: 4489
	public class VersionFinished : IProtoBuf
	{
		// Token: 0x17000E31 RID: 3633
		// (get) Token: 0x0600C597 RID: 50583 RVA: 0x003B8063 File Offset: 0x003B6263
		// (set) Token: 0x0600C598 RID: 50584 RVA: 0x003B806B File Offset: 0x003B626B
		public string CurrentVersion
		{
			get
			{
				return this._CurrentVersion;
			}
			set
			{
				this._CurrentVersion = value;
				this.HasCurrentVersion = (value != null);
			}
		}

		// Token: 0x17000E32 RID: 3634
		// (get) Token: 0x0600C599 RID: 50585 RVA: 0x003B807E File Offset: 0x003B627E
		// (set) Token: 0x0600C59A RID: 50586 RVA: 0x003B8086 File Offset: 0x003B6286
		public string LiveVersion
		{
			get
			{
				return this._LiveVersion;
			}
			set
			{
				this._LiveVersion = value;
				this.HasLiveVersion = (value != null);
			}
		}

		// Token: 0x0600C59B RID: 50587 RVA: 0x003B809C File Offset: 0x003B629C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasCurrentVersion)
			{
				num ^= this.CurrentVersion.GetHashCode();
			}
			if (this.HasLiveVersion)
			{
				num ^= this.LiveVersion.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600C59C RID: 50588 RVA: 0x003B80E4 File Offset: 0x003B62E4
		public override bool Equals(object obj)
		{
			VersionFinished versionFinished = obj as VersionFinished;
			return versionFinished != null && this.HasCurrentVersion == versionFinished.HasCurrentVersion && (!this.HasCurrentVersion || this.CurrentVersion.Equals(versionFinished.CurrentVersion)) && this.HasLiveVersion == versionFinished.HasLiveVersion && (!this.HasLiveVersion || this.LiveVersion.Equals(versionFinished.LiveVersion));
		}

		// Token: 0x0600C59D RID: 50589 RVA: 0x003B8154 File Offset: 0x003B6354
		public void Deserialize(Stream stream)
		{
			VersionFinished.Deserialize(stream, this);
		}

		// Token: 0x0600C59E RID: 50590 RVA: 0x003B815E File Offset: 0x003B635E
		public static VersionFinished Deserialize(Stream stream, VersionFinished instance)
		{
			return VersionFinished.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600C59F RID: 50591 RVA: 0x003B816C File Offset: 0x003B636C
		public static VersionFinished DeserializeLengthDelimited(Stream stream)
		{
			VersionFinished versionFinished = new VersionFinished();
			VersionFinished.DeserializeLengthDelimited(stream, versionFinished);
			return versionFinished;
		}

		// Token: 0x0600C5A0 RID: 50592 RVA: 0x003B8188 File Offset: 0x003B6388
		public static VersionFinished DeserializeLengthDelimited(Stream stream, VersionFinished instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return VersionFinished.Deserialize(stream, instance, num);
		}

		// Token: 0x0600C5A1 RID: 50593 RVA: 0x003B81B0 File Offset: 0x003B63B0
		public static VersionFinished Deserialize(Stream stream, VersionFinished instance, long limit)
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
					else
					{
						instance.LiveVersion = ProtocolParser.ReadString(stream);
					}
				}
				else
				{
					instance.CurrentVersion = ProtocolParser.ReadString(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600C5A2 RID: 50594 RVA: 0x003B8248 File Offset: 0x003B6448
		public void Serialize(Stream stream)
		{
			VersionFinished.Serialize(stream, this);
		}

		// Token: 0x0600C5A3 RID: 50595 RVA: 0x003B8254 File Offset: 0x003B6454
		public static void Serialize(Stream stream, VersionFinished instance)
		{
			if (instance.HasCurrentVersion)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.CurrentVersion));
			}
			if (instance.HasLiveVersion)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.LiveVersion));
			}
		}

		// Token: 0x0600C5A4 RID: 50596 RVA: 0x003B82B0 File Offset: 0x003B64B0
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasCurrentVersion)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.CurrentVersion);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasLiveVersion)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.LiveVersion);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			return num;
		}

		// Token: 0x04009DAF RID: 40367
		public bool HasCurrentVersion;

		// Token: 0x04009DB0 RID: 40368
		private string _CurrentVersion;

		// Token: 0x04009DB1 RID: 40369
		public bool HasLiveVersion;

		// Token: 0x04009DB2 RID: 40370
		private string _LiveVersion;
	}
}
