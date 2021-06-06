using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.NGDP
{
	// Token: 0x02001181 RID: 4481
	public class NoWifi : IProtoBuf
	{
		// Token: 0x17000E16 RID: 3606
		// (get) Token: 0x0600C509 RID: 50441 RVA: 0x003B6404 File Offset: 0x003B4604
		// (set) Token: 0x0600C50A RID: 50442 RVA: 0x003B640C File Offset: 0x003B460C
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

		// Token: 0x17000E17 RID: 3607
		// (get) Token: 0x0600C50B RID: 50443 RVA: 0x003B641F File Offset: 0x003B461F
		// (set) Token: 0x0600C50C RID: 50444 RVA: 0x003B6427 File Offset: 0x003B4627
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

		// Token: 0x17000E18 RID: 3608
		// (get) Token: 0x0600C50D RID: 50445 RVA: 0x003B6437 File Offset: 0x003B4637
		// (set) Token: 0x0600C50E RID: 50446 RVA: 0x003B643F File Offset: 0x003B463F
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

		// Token: 0x0600C50F RID: 50447 RVA: 0x003B6450 File Offset: 0x003B4650
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

		// Token: 0x0600C510 RID: 50448 RVA: 0x003B64B4 File Offset: 0x003B46B4
		public override bool Equals(object obj)
		{
			NoWifi noWifi = obj as NoWifi;
			return noWifi != null && this.HasUpdatedVersion == noWifi.HasUpdatedVersion && (!this.HasUpdatedVersion || this.UpdatedVersion.Equals(noWifi.UpdatedVersion)) && this.HasAvailableSpaceMB == noWifi.HasAvailableSpaceMB && (!this.HasAvailableSpaceMB || this.AvailableSpaceMB.Equals(noWifi.AvailableSpaceMB)) && this.HasElapsedSeconds == noWifi.HasElapsedSeconds && (!this.HasElapsedSeconds || this.ElapsedSeconds.Equals(noWifi.ElapsedSeconds));
		}

		// Token: 0x0600C511 RID: 50449 RVA: 0x003B6555 File Offset: 0x003B4755
		public void Deserialize(Stream stream)
		{
			NoWifi.Deserialize(stream, this);
		}

		// Token: 0x0600C512 RID: 50450 RVA: 0x003B655F File Offset: 0x003B475F
		public static NoWifi Deserialize(Stream stream, NoWifi instance)
		{
			return NoWifi.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600C513 RID: 50451 RVA: 0x003B656C File Offset: 0x003B476C
		public static NoWifi DeserializeLengthDelimited(Stream stream)
		{
			NoWifi noWifi = new NoWifi();
			NoWifi.DeserializeLengthDelimited(stream, noWifi);
			return noWifi;
		}

		// Token: 0x0600C514 RID: 50452 RVA: 0x003B6588 File Offset: 0x003B4788
		public static NoWifi DeserializeLengthDelimited(Stream stream, NoWifi instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return NoWifi.Deserialize(stream, instance, num);
		}

		// Token: 0x0600C515 RID: 50453 RVA: 0x003B65B0 File Offset: 0x003B47B0
		public static NoWifi Deserialize(Stream stream, NoWifi instance, long limit)
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

		// Token: 0x0600C516 RID: 50454 RVA: 0x003B6665 File Offset: 0x003B4865
		public void Serialize(Stream stream)
		{
			NoWifi.Serialize(stream, this);
		}

		// Token: 0x0600C517 RID: 50455 RVA: 0x003B6670 File Offset: 0x003B4870
		public static void Serialize(Stream stream, NoWifi instance)
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

		// Token: 0x0600C518 RID: 50456 RVA: 0x003B66E4 File Offset: 0x003B48E4
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

		// Token: 0x04009D79 RID: 40313
		public bool HasUpdatedVersion;

		// Token: 0x04009D7A RID: 40314
		private string _UpdatedVersion;

		// Token: 0x04009D7B RID: 40315
		public bool HasAvailableSpaceMB;

		// Token: 0x04009D7C RID: 40316
		private float _AvailableSpaceMB;

		// Token: 0x04009D7D RID: 40317
		public bool HasElapsedSeconds;

		// Token: 0x04009D7E RID: 40318
		private float _ElapsedSeconds;
	}
}
