using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.NGDP
{
	// Token: 0x02001187 RID: 4487
	public class UsingCellularData : IProtoBuf
	{
		// Token: 0x17000E28 RID: 3624
		// (get) Token: 0x0600C56F RID: 50543 RVA: 0x003B77A4 File Offset: 0x003B59A4
		// (set) Token: 0x0600C570 RID: 50544 RVA: 0x003B77AC File Offset: 0x003B59AC
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

		// Token: 0x17000E29 RID: 3625
		// (get) Token: 0x0600C571 RID: 50545 RVA: 0x003B77BF File Offset: 0x003B59BF
		// (set) Token: 0x0600C572 RID: 50546 RVA: 0x003B77C7 File Offset: 0x003B59C7
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

		// Token: 0x17000E2A RID: 3626
		// (get) Token: 0x0600C573 RID: 50547 RVA: 0x003B77D7 File Offset: 0x003B59D7
		// (set) Token: 0x0600C574 RID: 50548 RVA: 0x003B77DF File Offset: 0x003B59DF
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

		// Token: 0x0600C575 RID: 50549 RVA: 0x003B77F0 File Offset: 0x003B59F0
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

		// Token: 0x0600C576 RID: 50550 RVA: 0x003B7854 File Offset: 0x003B5A54
		public override bool Equals(object obj)
		{
			UsingCellularData usingCellularData = obj as UsingCellularData;
			return usingCellularData != null && this.HasUpdatedVersion == usingCellularData.HasUpdatedVersion && (!this.HasUpdatedVersion || this.UpdatedVersion.Equals(usingCellularData.UpdatedVersion)) && this.HasAvailableSpaceMB == usingCellularData.HasAvailableSpaceMB && (!this.HasAvailableSpaceMB || this.AvailableSpaceMB.Equals(usingCellularData.AvailableSpaceMB)) && this.HasElapsedSeconds == usingCellularData.HasElapsedSeconds && (!this.HasElapsedSeconds || this.ElapsedSeconds.Equals(usingCellularData.ElapsedSeconds));
		}

		// Token: 0x0600C577 RID: 50551 RVA: 0x003B78F5 File Offset: 0x003B5AF5
		public void Deserialize(Stream stream)
		{
			UsingCellularData.Deserialize(stream, this);
		}

		// Token: 0x0600C578 RID: 50552 RVA: 0x003B78FF File Offset: 0x003B5AFF
		public static UsingCellularData Deserialize(Stream stream, UsingCellularData instance)
		{
			return UsingCellularData.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600C579 RID: 50553 RVA: 0x003B790C File Offset: 0x003B5B0C
		public static UsingCellularData DeserializeLengthDelimited(Stream stream)
		{
			UsingCellularData usingCellularData = new UsingCellularData();
			UsingCellularData.DeserializeLengthDelimited(stream, usingCellularData);
			return usingCellularData;
		}

		// Token: 0x0600C57A RID: 50554 RVA: 0x003B7928 File Offset: 0x003B5B28
		public static UsingCellularData DeserializeLengthDelimited(Stream stream, UsingCellularData instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return UsingCellularData.Deserialize(stream, instance, num);
		}

		// Token: 0x0600C57B RID: 50555 RVA: 0x003B7950 File Offset: 0x003B5B50
		public static UsingCellularData Deserialize(Stream stream, UsingCellularData instance, long limit)
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

		// Token: 0x0600C57C RID: 50556 RVA: 0x003B7A05 File Offset: 0x003B5C05
		public void Serialize(Stream stream)
		{
			UsingCellularData.Serialize(stream, this);
		}

		// Token: 0x0600C57D RID: 50557 RVA: 0x003B7A10 File Offset: 0x003B5C10
		public static void Serialize(Stream stream, UsingCellularData instance)
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

		// Token: 0x0600C57E RID: 50558 RVA: 0x003B7A84 File Offset: 0x003B5C84
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

		// Token: 0x04009D9D RID: 40349
		public bool HasUpdatedVersion;

		// Token: 0x04009D9E RID: 40350
		private string _UpdatedVersion;

		// Token: 0x04009D9F RID: 40351
		public bool HasAvailableSpaceMB;

		// Token: 0x04009DA0 RID: 40352
		private float _AvailableSpaceMB;

		// Token: 0x04009DA1 RID: 40353
		public bool HasElapsedSeconds;

		// Token: 0x04009DA2 RID: 40354
		private float _ElapsedSeconds;
	}
}
