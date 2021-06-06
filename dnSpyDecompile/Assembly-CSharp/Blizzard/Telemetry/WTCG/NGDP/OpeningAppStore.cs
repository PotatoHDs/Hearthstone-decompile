using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.NGDP
{
	// Token: 0x02001182 RID: 4482
	public class OpeningAppStore : IProtoBuf
	{
		// Token: 0x17000E19 RID: 3609
		// (get) Token: 0x0600C51A RID: 50458 RVA: 0x003B673C File Offset: 0x003B493C
		// (set) Token: 0x0600C51B RID: 50459 RVA: 0x003B6744 File Offset: 0x003B4944
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

		// Token: 0x17000E1A RID: 3610
		// (get) Token: 0x0600C51C RID: 50460 RVA: 0x003B6757 File Offset: 0x003B4957
		// (set) Token: 0x0600C51D RID: 50461 RVA: 0x003B675F File Offset: 0x003B495F
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

		// Token: 0x17000E1B RID: 3611
		// (get) Token: 0x0600C51E RID: 50462 RVA: 0x003B676F File Offset: 0x003B496F
		// (set) Token: 0x0600C51F RID: 50463 RVA: 0x003B6777 File Offset: 0x003B4977
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

		// Token: 0x0600C520 RID: 50464 RVA: 0x003B6788 File Offset: 0x003B4988
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

		// Token: 0x0600C521 RID: 50465 RVA: 0x003B67EC File Offset: 0x003B49EC
		public override bool Equals(object obj)
		{
			OpeningAppStore openingAppStore = obj as OpeningAppStore;
			return openingAppStore != null && this.HasUpdatedVersion == openingAppStore.HasUpdatedVersion && (!this.HasUpdatedVersion || this.UpdatedVersion.Equals(openingAppStore.UpdatedVersion)) && this.HasAvailableSpaceMB == openingAppStore.HasAvailableSpaceMB && (!this.HasAvailableSpaceMB || this.AvailableSpaceMB.Equals(openingAppStore.AvailableSpaceMB)) && this.HasElapsedSeconds == openingAppStore.HasElapsedSeconds && (!this.HasElapsedSeconds || this.ElapsedSeconds.Equals(openingAppStore.ElapsedSeconds));
		}

		// Token: 0x0600C522 RID: 50466 RVA: 0x003B688D File Offset: 0x003B4A8D
		public void Deserialize(Stream stream)
		{
			OpeningAppStore.Deserialize(stream, this);
		}

		// Token: 0x0600C523 RID: 50467 RVA: 0x003B6897 File Offset: 0x003B4A97
		public static OpeningAppStore Deserialize(Stream stream, OpeningAppStore instance)
		{
			return OpeningAppStore.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600C524 RID: 50468 RVA: 0x003B68A4 File Offset: 0x003B4AA4
		public static OpeningAppStore DeserializeLengthDelimited(Stream stream)
		{
			OpeningAppStore openingAppStore = new OpeningAppStore();
			OpeningAppStore.DeserializeLengthDelimited(stream, openingAppStore);
			return openingAppStore;
		}

		// Token: 0x0600C525 RID: 50469 RVA: 0x003B68C0 File Offset: 0x003B4AC0
		public static OpeningAppStore DeserializeLengthDelimited(Stream stream, OpeningAppStore instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return OpeningAppStore.Deserialize(stream, instance, num);
		}

		// Token: 0x0600C526 RID: 50470 RVA: 0x003B68E8 File Offset: 0x003B4AE8
		public static OpeningAppStore Deserialize(Stream stream, OpeningAppStore instance, long limit)
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

		// Token: 0x0600C527 RID: 50471 RVA: 0x003B699D File Offset: 0x003B4B9D
		public void Serialize(Stream stream)
		{
			OpeningAppStore.Serialize(stream, this);
		}

		// Token: 0x0600C528 RID: 50472 RVA: 0x003B69A8 File Offset: 0x003B4BA8
		public static void Serialize(Stream stream, OpeningAppStore instance)
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

		// Token: 0x0600C529 RID: 50473 RVA: 0x003B6A1C File Offset: 0x003B4C1C
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

		// Token: 0x04009D7F RID: 40319
		public bool HasUpdatedVersion;

		// Token: 0x04009D80 RID: 40320
		private string _UpdatedVersion;

		// Token: 0x04009D81 RID: 40321
		public bool HasAvailableSpaceMB;

		// Token: 0x04009D82 RID: 40322
		private float _AvailableSpaceMB;

		// Token: 0x04009D83 RID: 40323
		public bool HasElapsedSeconds;

		// Token: 0x04009D84 RID: 40324
		private float _ElapsedSeconds;
	}
}
