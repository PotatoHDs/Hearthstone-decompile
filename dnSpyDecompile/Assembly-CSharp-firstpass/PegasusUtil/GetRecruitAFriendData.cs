using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x020000DD RID: 221
	public class GetRecruitAFriendData : IProtoBuf
	{
		// Token: 0x06000F0B RID: 3851 RVA: 0x000157A7 File Offset: 0x000139A7
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode();
		}

		// Token: 0x06000F0C RID: 3852 RVA: 0x00036BC9 File Offset: 0x00034DC9
		public override bool Equals(object obj)
		{
			return obj is GetRecruitAFriendData;
		}

		// Token: 0x06000F0D RID: 3853 RVA: 0x00036BD6 File Offset: 0x00034DD6
		public void Deserialize(Stream stream)
		{
			GetRecruitAFriendData.Deserialize(stream, this);
		}

		// Token: 0x06000F0E RID: 3854 RVA: 0x00036BE0 File Offset: 0x00034DE0
		public static GetRecruitAFriendData Deserialize(Stream stream, GetRecruitAFriendData instance)
		{
			return GetRecruitAFriendData.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000F0F RID: 3855 RVA: 0x00036BEC File Offset: 0x00034DEC
		public static GetRecruitAFriendData DeserializeLengthDelimited(Stream stream)
		{
			GetRecruitAFriendData getRecruitAFriendData = new GetRecruitAFriendData();
			GetRecruitAFriendData.DeserializeLengthDelimited(stream, getRecruitAFriendData);
			return getRecruitAFriendData;
		}

		// Token: 0x06000F10 RID: 3856 RVA: 0x00036C08 File Offset: 0x00034E08
		public static GetRecruitAFriendData DeserializeLengthDelimited(Stream stream, GetRecruitAFriendData instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetRecruitAFriendData.Deserialize(stream, instance, num);
		}

		// Token: 0x06000F11 RID: 3857 RVA: 0x00036C30 File Offset: 0x00034E30
		public static GetRecruitAFriendData Deserialize(Stream stream, GetRecruitAFriendData instance, long limit)
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

		// Token: 0x06000F12 RID: 3858 RVA: 0x00036C9D File Offset: 0x00034E9D
		public void Serialize(Stream stream)
		{
			GetRecruitAFriendData.Serialize(stream, this);
		}

		// Token: 0x06000F13 RID: 3859 RVA: 0x00003FD0 File Offset: 0x000021D0
		public static void Serialize(Stream stream, GetRecruitAFriendData instance)
		{
		}

		// Token: 0x06000F14 RID: 3860 RVA: 0x00003D71 File Offset: 0x00001F71
		public uint GetSerializedSize()
		{
			return 0U;
		}

		// Token: 0x020005E2 RID: 1506
		public enum PacketID
		{
			// Token: 0x04001FEF RID: 8175
			ID = 337,
			// Token: 0x04001FF0 RID: 8176
			System = 2
		}
	}
}
