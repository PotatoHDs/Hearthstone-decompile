using System;
using System.IO;

namespace PegasusShared
{
	// Token: 0x02000124 RID: 292
	public class BnetId : IProtoBuf
	{
		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06001336 RID: 4918 RVA: 0x00042FCD File Offset: 0x000411CD
		// (set) Token: 0x06001337 RID: 4919 RVA: 0x00042FD5 File Offset: 0x000411D5
		public ulong Hi { get; set; }

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06001338 RID: 4920 RVA: 0x00042FDE File Offset: 0x000411DE
		// (set) Token: 0x06001339 RID: 4921 RVA: 0x00042FE6 File Offset: 0x000411E6
		public ulong Lo { get; set; }

		// Token: 0x0600133A RID: 4922 RVA: 0x00042FF0 File Offset: 0x000411F0
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.Hi.GetHashCode() ^ this.Lo.GetHashCode();
		}

		// Token: 0x0600133B RID: 4923 RVA: 0x00043028 File Offset: 0x00041228
		public override bool Equals(object obj)
		{
			BnetId bnetId = obj as BnetId;
			return bnetId != null && this.Hi.Equals(bnetId.Hi) && this.Lo.Equals(bnetId.Lo);
		}

		// Token: 0x0600133C RID: 4924 RVA: 0x00043072 File Offset: 0x00041272
		public void Deserialize(Stream stream)
		{
			BnetId.Deserialize(stream, this);
		}

		// Token: 0x0600133D RID: 4925 RVA: 0x0004307C File Offset: 0x0004127C
		public static BnetId Deserialize(Stream stream, BnetId instance)
		{
			return BnetId.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600133E RID: 4926 RVA: 0x00043088 File Offset: 0x00041288
		public static BnetId DeserializeLengthDelimited(Stream stream)
		{
			BnetId bnetId = new BnetId();
			BnetId.DeserializeLengthDelimited(stream, bnetId);
			return bnetId;
		}

		// Token: 0x0600133F RID: 4927 RVA: 0x000430A4 File Offset: 0x000412A4
		public static BnetId DeserializeLengthDelimited(Stream stream, BnetId instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return BnetId.Deserialize(stream, instance, num);
		}

		// Token: 0x06001340 RID: 4928 RVA: 0x000430CC File Offset: 0x000412CC
		public static BnetId Deserialize(Stream stream, BnetId instance, long limit)
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
						Key key = ProtocolParser.ReadKey((byte)num, stream);
						if (key.Field == 0U)
						{
							throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
						}
						ProtocolParser.SkipKey(stream, key);
					}
					else
					{
						instance.Lo = ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.Hi = ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06001341 RID: 4929 RVA: 0x00043163 File Offset: 0x00041363
		public void Serialize(Stream stream)
		{
			BnetId.Serialize(stream, this);
		}

		// Token: 0x06001342 RID: 4930 RVA: 0x0004316C File Offset: 0x0004136C
		public static void Serialize(Stream stream, BnetId instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, instance.Hi);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, instance.Lo);
		}

		// Token: 0x06001343 RID: 4931 RVA: 0x00043195 File Offset: 0x00041395
		public uint GetSerializedSize()
		{
			return 0U + ProtocolParser.SizeOfUInt64(this.Hi) + ProtocolParser.SizeOfUInt64(this.Lo) + 2U;
		}
	}
}
