using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.NGDP
{
	// Token: 0x02001185 RID: 4485
	public class UpdateFinished : IProtoBuf
	{
		// Token: 0x17000E21 RID: 3617
		// (get) Token: 0x0600C54B RID: 50507 RVA: 0x003B7042 File Offset: 0x003B5242
		// (set) Token: 0x0600C54C RID: 50508 RVA: 0x003B704A File Offset: 0x003B524A
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

		// Token: 0x17000E22 RID: 3618
		// (get) Token: 0x0600C54D RID: 50509 RVA: 0x003B705D File Offset: 0x003B525D
		// (set) Token: 0x0600C54E RID: 50510 RVA: 0x003B7065 File Offset: 0x003B5265
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

		// Token: 0x17000E23 RID: 3619
		// (get) Token: 0x0600C54F RID: 50511 RVA: 0x003B7075 File Offset: 0x003B5275
		// (set) Token: 0x0600C550 RID: 50512 RVA: 0x003B707D File Offset: 0x003B527D
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

		// Token: 0x0600C551 RID: 50513 RVA: 0x003B7090 File Offset: 0x003B5290
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

		// Token: 0x0600C552 RID: 50514 RVA: 0x003B70F4 File Offset: 0x003B52F4
		public override bool Equals(object obj)
		{
			UpdateFinished updateFinished = obj as UpdateFinished;
			return updateFinished != null && this.HasUpdatedVersion == updateFinished.HasUpdatedVersion && (!this.HasUpdatedVersion || this.UpdatedVersion.Equals(updateFinished.UpdatedVersion)) && this.HasAvailableSpaceMB == updateFinished.HasAvailableSpaceMB && (!this.HasAvailableSpaceMB || this.AvailableSpaceMB.Equals(updateFinished.AvailableSpaceMB)) && this.HasElapsedSeconds == updateFinished.HasElapsedSeconds && (!this.HasElapsedSeconds || this.ElapsedSeconds.Equals(updateFinished.ElapsedSeconds));
		}

		// Token: 0x0600C553 RID: 50515 RVA: 0x003B7195 File Offset: 0x003B5395
		public void Deserialize(Stream stream)
		{
			UpdateFinished.Deserialize(stream, this);
		}

		// Token: 0x0600C554 RID: 50516 RVA: 0x003B719F File Offset: 0x003B539F
		public static UpdateFinished Deserialize(Stream stream, UpdateFinished instance)
		{
			return UpdateFinished.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600C555 RID: 50517 RVA: 0x003B71AC File Offset: 0x003B53AC
		public static UpdateFinished DeserializeLengthDelimited(Stream stream)
		{
			UpdateFinished updateFinished = new UpdateFinished();
			UpdateFinished.DeserializeLengthDelimited(stream, updateFinished);
			return updateFinished;
		}

		// Token: 0x0600C556 RID: 50518 RVA: 0x003B71C8 File Offset: 0x003B53C8
		public static UpdateFinished DeserializeLengthDelimited(Stream stream, UpdateFinished instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return UpdateFinished.Deserialize(stream, instance, num);
		}

		// Token: 0x0600C557 RID: 50519 RVA: 0x003B71F0 File Offset: 0x003B53F0
		public static UpdateFinished Deserialize(Stream stream, UpdateFinished instance, long limit)
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

		// Token: 0x0600C558 RID: 50520 RVA: 0x003B72A5 File Offset: 0x003B54A5
		public void Serialize(Stream stream)
		{
			UpdateFinished.Serialize(stream, this);
		}

		// Token: 0x0600C559 RID: 50521 RVA: 0x003B72B0 File Offset: 0x003B54B0
		public static void Serialize(Stream stream, UpdateFinished instance)
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

		// Token: 0x0600C55A RID: 50522 RVA: 0x003B7324 File Offset: 0x003B5524
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

		// Token: 0x04009D8F RID: 40335
		public bool HasUpdatedVersion;

		// Token: 0x04009D90 RID: 40336
		private string _UpdatedVersion;

		// Token: 0x04009D91 RID: 40337
		public bool HasAvailableSpaceMB;

		// Token: 0x04009D92 RID: 40338
		private float _AvailableSpaceMB;

		// Token: 0x04009D93 RID: 40339
		public bool HasElapsedSeconds;

		// Token: 0x04009D94 RID: 40340
		private float _ElapsedSeconds;
	}
}
