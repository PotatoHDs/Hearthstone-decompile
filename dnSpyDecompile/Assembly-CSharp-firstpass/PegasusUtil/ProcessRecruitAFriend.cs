using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x020000E1 RID: 225
	public class ProcessRecruitAFriend : IProtoBuf
	{
		// Token: 0x06000F47 RID: 3911 RVA: 0x000157A7 File Offset: 0x000139A7
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode();
		}

		// Token: 0x06000F48 RID: 3912 RVA: 0x00037743 File Offset: 0x00035943
		public override bool Equals(object obj)
		{
			return obj is ProcessRecruitAFriend;
		}

		// Token: 0x06000F49 RID: 3913 RVA: 0x00037750 File Offset: 0x00035950
		public void Deserialize(Stream stream)
		{
			ProcessRecruitAFriend.Deserialize(stream, this);
		}

		// Token: 0x06000F4A RID: 3914 RVA: 0x0003775A File Offset: 0x0003595A
		public static ProcessRecruitAFriend Deserialize(Stream stream, ProcessRecruitAFriend instance)
		{
			return ProcessRecruitAFriend.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000F4B RID: 3915 RVA: 0x00037768 File Offset: 0x00035968
		public static ProcessRecruitAFriend DeserializeLengthDelimited(Stream stream)
		{
			ProcessRecruitAFriend processRecruitAFriend = new ProcessRecruitAFriend();
			ProcessRecruitAFriend.DeserializeLengthDelimited(stream, processRecruitAFriend);
			return processRecruitAFriend;
		}

		// Token: 0x06000F4C RID: 3916 RVA: 0x00037784 File Offset: 0x00035984
		public static ProcessRecruitAFriend DeserializeLengthDelimited(Stream stream, ProcessRecruitAFriend instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ProcessRecruitAFriend.Deserialize(stream, instance, num);
		}

		// Token: 0x06000F4D RID: 3917 RVA: 0x000377AC File Offset: 0x000359AC
		public static ProcessRecruitAFriend Deserialize(Stream stream, ProcessRecruitAFriend instance, long limit)
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
				else
				{
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

		// Token: 0x06000F4E RID: 3918 RVA: 0x00037819 File Offset: 0x00035A19
		public void Serialize(Stream stream)
		{
			ProcessRecruitAFriend.Serialize(stream, this);
		}

		// Token: 0x06000F4F RID: 3919 RVA: 0x00003FD0 File Offset: 0x000021D0
		public static void Serialize(Stream stream, ProcessRecruitAFriend instance)
		{
		}

		// Token: 0x06000F50 RID: 3920 RVA: 0x00003D71 File Offset: 0x00001F71
		public uint GetSerializedSize()
		{
			return 0U;
		}

		// Token: 0x020005E5 RID: 1509
		public enum PacketID
		{
			// Token: 0x04001FF6 RID: 8182
			ID = 339,
			// Token: 0x04001FF7 RID: 8183
			System = 2
		}
	}
}
