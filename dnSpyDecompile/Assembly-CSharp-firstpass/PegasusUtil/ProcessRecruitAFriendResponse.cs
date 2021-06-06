using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x020000E2 RID: 226
	public class ProcessRecruitAFriendResponse : IProtoBuf
	{
		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06000F52 RID: 3922 RVA: 0x00037822 File Offset: 0x00035A22
		// (set) Token: 0x06000F53 RID: 3923 RVA: 0x0003782A File Offset: 0x00035A2A
		public RAFServiceStatus RafServiceStatus { get; set; }

		// Token: 0x06000F54 RID: 3924 RVA: 0x00037834 File Offset: 0x00035A34
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.RafServiceStatus.GetHashCode();
		}

		// Token: 0x06000F55 RID: 3925 RVA: 0x00037864 File Offset: 0x00035A64
		public override bool Equals(object obj)
		{
			ProcessRecruitAFriendResponse processRecruitAFriendResponse = obj as ProcessRecruitAFriendResponse;
			return processRecruitAFriendResponse != null && this.RafServiceStatus.Equals(processRecruitAFriendResponse.RafServiceStatus);
		}

		// Token: 0x06000F56 RID: 3926 RVA: 0x000378A1 File Offset: 0x00035AA1
		public void Deserialize(Stream stream)
		{
			ProcessRecruitAFriendResponse.Deserialize(stream, this);
		}

		// Token: 0x06000F57 RID: 3927 RVA: 0x000378AB File Offset: 0x00035AAB
		public static ProcessRecruitAFriendResponse Deserialize(Stream stream, ProcessRecruitAFriendResponse instance)
		{
			return ProcessRecruitAFriendResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000F58 RID: 3928 RVA: 0x000378B8 File Offset: 0x00035AB8
		public static ProcessRecruitAFriendResponse DeserializeLengthDelimited(Stream stream)
		{
			ProcessRecruitAFriendResponse processRecruitAFriendResponse = new ProcessRecruitAFriendResponse();
			ProcessRecruitAFriendResponse.DeserializeLengthDelimited(stream, processRecruitAFriendResponse);
			return processRecruitAFriendResponse;
		}

		// Token: 0x06000F59 RID: 3929 RVA: 0x000378D4 File Offset: 0x00035AD4
		public static ProcessRecruitAFriendResponse DeserializeLengthDelimited(Stream stream, ProcessRecruitAFriendResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ProcessRecruitAFriendResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06000F5A RID: 3930 RVA: 0x000378FC File Offset: 0x00035AFC
		public static ProcessRecruitAFriendResponse Deserialize(Stream stream, ProcessRecruitAFriendResponse instance, long limit)
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
				else if (num == 8)
				{
					instance.RafServiceStatus = (RAFServiceStatus)ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x06000F5B RID: 3931 RVA: 0x0003797C File Offset: 0x00035B7C
		public void Serialize(Stream stream)
		{
			ProcessRecruitAFriendResponse.Serialize(stream, this);
		}

		// Token: 0x06000F5C RID: 3932 RVA: 0x00037985 File Offset: 0x00035B85
		public static void Serialize(Stream stream, ProcessRecruitAFriendResponse instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.RafServiceStatus));
		}

		// Token: 0x06000F5D RID: 3933 RVA: 0x0003799B File Offset: 0x00035B9B
		public uint GetSerializedSize()
		{
			return 0U + ProtocolParser.SizeOfUInt64((ulong)((long)this.RafServiceStatus)) + 1U;
		}

		// Token: 0x020005E6 RID: 1510
		public enum PacketID
		{
			// Token: 0x04001FF9 RID: 8185
			ID = 342
		}
	}
}
