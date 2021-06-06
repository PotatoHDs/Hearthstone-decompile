using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.NGDP
{
	// Token: 0x02001180 RID: 4480
	public class NotEnoughSpaceError : IProtoBuf
	{
		// Token: 0x17000E13 RID: 3603
		// (get) Token: 0x0600C4F8 RID: 50424 RVA: 0x003B60C8 File Offset: 0x003B42C8
		// (set) Token: 0x0600C4F9 RID: 50425 RVA: 0x003B60D0 File Offset: 0x003B42D0
		public ulong AvailableSpace
		{
			get
			{
				return this._AvailableSpace;
			}
			set
			{
				this._AvailableSpace = value;
				this.HasAvailableSpace = true;
			}
		}

		// Token: 0x17000E14 RID: 3604
		// (get) Token: 0x0600C4FA RID: 50426 RVA: 0x003B60E0 File Offset: 0x003B42E0
		// (set) Token: 0x0600C4FB RID: 50427 RVA: 0x003B60E8 File Offset: 0x003B42E8
		public ulong ExpectedOrgBytes
		{
			get
			{
				return this._ExpectedOrgBytes;
			}
			set
			{
				this._ExpectedOrgBytes = value;
				this.HasExpectedOrgBytes = true;
			}
		}

		// Token: 0x17000E15 RID: 3605
		// (get) Token: 0x0600C4FC RID: 50428 RVA: 0x003B60F8 File Offset: 0x003B42F8
		// (set) Token: 0x0600C4FD RID: 50429 RVA: 0x003B6100 File Offset: 0x003B4300
		public string FilesDir
		{
			get
			{
				return this._FilesDir;
			}
			set
			{
				this._FilesDir = value;
				this.HasFilesDir = (value != null);
			}
		}

		// Token: 0x0600C4FE RID: 50430 RVA: 0x003B6114 File Offset: 0x003B4314
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAvailableSpace)
			{
				num ^= this.AvailableSpace.GetHashCode();
			}
			if (this.HasExpectedOrgBytes)
			{
				num ^= this.ExpectedOrgBytes.GetHashCode();
			}
			if (this.HasFilesDir)
			{
				num ^= this.FilesDir.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600C4FF RID: 50431 RVA: 0x003B6178 File Offset: 0x003B4378
		public override bool Equals(object obj)
		{
			NotEnoughSpaceError notEnoughSpaceError = obj as NotEnoughSpaceError;
			return notEnoughSpaceError != null && this.HasAvailableSpace == notEnoughSpaceError.HasAvailableSpace && (!this.HasAvailableSpace || this.AvailableSpace.Equals(notEnoughSpaceError.AvailableSpace)) && this.HasExpectedOrgBytes == notEnoughSpaceError.HasExpectedOrgBytes && (!this.HasExpectedOrgBytes || this.ExpectedOrgBytes.Equals(notEnoughSpaceError.ExpectedOrgBytes)) && this.HasFilesDir == notEnoughSpaceError.HasFilesDir && (!this.HasFilesDir || this.FilesDir.Equals(notEnoughSpaceError.FilesDir));
		}

		// Token: 0x0600C500 RID: 50432 RVA: 0x003B6219 File Offset: 0x003B4419
		public void Deserialize(Stream stream)
		{
			NotEnoughSpaceError.Deserialize(stream, this);
		}

		// Token: 0x0600C501 RID: 50433 RVA: 0x003B6223 File Offset: 0x003B4423
		public static NotEnoughSpaceError Deserialize(Stream stream, NotEnoughSpaceError instance)
		{
			return NotEnoughSpaceError.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600C502 RID: 50434 RVA: 0x003B6230 File Offset: 0x003B4430
		public static NotEnoughSpaceError DeserializeLengthDelimited(Stream stream)
		{
			NotEnoughSpaceError notEnoughSpaceError = new NotEnoughSpaceError();
			NotEnoughSpaceError.DeserializeLengthDelimited(stream, notEnoughSpaceError);
			return notEnoughSpaceError;
		}

		// Token: 0x0600C503 RID: 50435 RVA: 0x003B624C File Offset: 0x003B444C
		public static NotEnoughSpaceError DeserializeLengthDelimited(Stream stream, NotEnoughSpaceError instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return NotEnoughSpaceError.Deserialize(stream, instance, num);
		}

		// Token: 0x0600C504 RID: 50436 RVA: 0x003B6274 File Offset: 0x003B4474
		public static NotEnoughSpaceError Deserialize(Stream stream, NotEnoughSpaceError instance, long limit)
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
					if (num != 32)
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
							instance.FilesDir = ProtocolParser.ReadString(stream);
						}
					}
					else
					{
						instance.ExpectedOrgBytes = ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.AvailableSpace = ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600C505 RID: 50437 RVA: 0x003B6321 File Offset: 0x003B4521
		public void Serialize(Stream stream)
		{
			NotEnoughSpaceError.Serialize(stream, this);
		}

		// Token: 0x0600C506 RID: 50438 RVA: 0x003B632C File Offset: 0x003B452C
		public static void Serialize(Stream stream, NotEnoughSpaceError instance)
		{
			if (instance.HasAvailableSpace)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, instance.AvailableSpace);
			}
			if (instance.HasExpectedOrgBytes)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, instance.ExpectedOrgBytes);
			}
			if (instance.HasFilesDir)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.FilesDir));
			}
		}

		// Token: 0x0600C507 RID: 50439 RVA: 0x003B6398 File Offset: 0x003B4598
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAvailableSpace)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.AvailableSpace);
			}
			if (this.HasExpectedOrgBytes)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.ExpectedOrgBytes);
			}
			if (this.HasFilesDir)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.FilesDir);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num;
		}

		// Token: 0x04009D73 RID: 40307
		public bool HasAvailableSpace;

		// Token: 0x04009D74 RID: 40308
		private ulong _AvailableSpace;

		// Token: 0x04009D75 RID: 40309
		public bool HasExpectedOrgBytes;

		// Token: 0x04009D76 RID: 40310
		private ulong _ExpectedOrgBytes;

		// Token: 0x04009D77 RID: 40311
		public bool HasFilesDir;

		// Token: 0x04009D78 RID: 40312
		private string _FilesDir;
	}
}
