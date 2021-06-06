using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x020000CE RID: 206
	public class FreeDeckChoice : IProtoBuf
	{
		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06000E10 RID: 3600 RVA: 0x00033A45 File Offset: 0x00031C45
		// (set) Token: 0x06000E11 RID: 3601 RVA: 0x00033A4D File Offset: 0x00031C4D
		public int ClassId { get; set; }

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06000E12 RID: 3602 RVA: 0x00033A56 File Offset: 0x00031C56
		// (set) Token: 0x06000E13 RID: 3603 RVA: 0x00033A5E File Offset: 0x00031C5E
		public long NoticeId { get; set; }

		// Token: 0x06000E14 RID: 3604 RVA: 0x00033A68 File Offset: 0x00031C68
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.ClassId.GetHashCode() ^ this.NoticeId.GetHashCode();
		}

		// Token: 0x06000E15 RID: 3605 RVA: 0x00033AA0 File Offset: 0x00031CA0
		public override bool Equals(object obj)
		{
			FreeDeckChoice freeDeckChoice = obj as FreeDeckChoice;
			return freeDeckChoice != null && this.ClassId.Equals(freeDeckChoice.ClassId) && this.NoticeId.Equals(freeDeckChoice.NoticeId);
		}

		// Token: 0x06000E16 RID: 3606 RVA: 0x00033AEA File Offset: 0x00031CEA
		public void Deserialize(Stream stream)
		{
			FreeDeckChoice.Deserialize(stream, this);
		}

		// Token: 0x06000E17 RID: 3607 RVA: 0x00033AF4 File Offset: 0x00031CF4
		public static FreeDeckChoice Deserialize(Stream stream, FreeDeckChoice instance)
		{
			return FreeDeckChoice.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000E18 RID: 3608 RVA: 0x00033B00 File Offset: 0x00031D00
		public static FreeDeckChoice DeserializeLengthDelimited(Stream stream)
		{
			FreeDeckChoice freeDeckChoice = new FreeDeckChoice();
			FreeDeckChoice.DeserializeLengthDelimited(stream, freeDeckChoice);
			return freeDeckChoice;
		}

		// Token: 0x06000E19 RID: 3609 RVA: 0x00033B1C File Offset: 0x00031D1C
		public static FreeDeckChoice DeserializeLengthDelimited(Stream stream, FreeDeckChoice instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return FreeDeckChoice.Deserialize(stream, instance, num);
		}

		// Token: 0x06000E1A RID: 3610 RVA: 0x00033B44 File Offset: 0x00031D44
		public static FreeDeckChoice Deserialize(Stream stream, FreeDeckChoice instance, long limit)
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
						instance.NoticeId = (long)ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.ClassId = (int)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06000E1B RID: 3611 RVA: 0x00033BDC File Offset: 0x00031DDC
		public void Serialize(Stream stream)
		{
			FreeDeckChoice.Serialize(stream, this);
		}

		// Token: 0x06000E1C RID: 3612 RVA: 0x00033BE5 File Offset: 0x00031DE5
		public static void Serialize(Stream stream, FreeDeckChoice instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.ClassId));
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.NoticeId);
		}

		// Token: 0x06000E1D RID: 3613 RVA: 0x00033C0F File Offset: 0x00031E0F
		public uint GetSerializedSize()
		{
			return 0U + ProtocolParser.SizeOfUInt64((ulong)((long)this.ClassId)) + ProtocolParser.SizeOfUInt64((ulong)this.NoticeId) + 2U;
		}

		// Token: 0x020005DD RID: 1501
		public enum PacketID
		{
			// Token: 0x04001FE1 RID: 8161
			ID = 333,
			// Token: 0x04001FE2 RID: 8162
			System = 0
		}
	}
}
