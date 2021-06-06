using System;
using System.IO;

namespace PegasusShared
{
	// Token: 0x02000133 RID: 307
	public class ProfileNoticeRewardCard2x : IProtoBuf
	{
		// Token: 0x06001431 RID: 5169 RVA: 0x000157A7 File Offset: 0x000139A7
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode();
		}

		// Token: 0x06001432 RID: 5170 RVA: 0x00045B7C File Offset: 0x00043D7C
		public override bool Equals(object obj)
		{
			return obj is ProfileNoticeRewardCard2x;
		}

		// Token: 0x06001433 RID: 5171 RVA: 0x00045B89 File Offset: 0x00043D89
		public void Deserialize(Stream stream)
		{
			ProfileNoticeRewardCard2x.Deserialize(stream, this);
		}

		// Token: 0x06001434 RID: 5172 RVA: 0x00045B93 File Offset: 0x00043D93
		public static ProfileNoticeRewardCard2x Deserialize(Stream stream, ProfileNoticeRewardCard2x instance)
		{
			return ProfileNoticeRewardCard2x.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001435 RID: 5173 RVA: 0x00045BA0 File Offset: 0x00043DA0
		public static ProfileNoticeRewardCard2x DeserializeLengthDelimited(Stream stream)
		{
			ProfileNoticeRewardCard2x profileNoticeRewardCard2x = new ProfileNoticeRewardCard2x();
			ProfileNoticeRewardCard2x.DeserializeLengthDelimited(stream, profileNoticeRewardCard2x);
			return profileNoticeRewardCard2x;
		}

		// Token: 0x06001436 RID: 5174 RVA: 0x00045BBC File Offset: 0x00043DBC
		public static ProfileNoticeRewardCard2x DeserializeLengthDelimited(Stream stream, ProfileNoticeRewardCard2x instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ProfileNoticeRewardCard2x.Deserialize(stream, instance, num);
		}

		// Token: 0x06001437 RID: 5175 RVA: 0x00045BE4 File Offset: 0x00043DE4
		public static ProfileNoticeRewardCard2x Deserialize(Stream stream, ProfileNoticeRewardCard2x instance, long limit)
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

		// Token: 0x06001438 RID: 5176 RVA: 0x00045C51 File Offset: 0x00043E51
		public void Serialize(Stream stream)
		{
			ProfileNoticeRewardCard2x.Serialize(stream, this);
		}

		// Token: 0x06001439 RID: 5177 RVA: 0x00003FD0 File Offset: 0x000021D0
		public static void Serialize(Stream stream, ProfileNoticeRewardCard2x instance)
		{
		}

		// Token: 0x0600143A RID: 5178 RVA: 0x00003D71 File Offset: 0x00001F71
		public uint GetSerializedSize()
		{
			return 0U;
		}

		// Token: 0x02000627 RID: 1575
		public enum NoticeID
		{
			// Token: 0x040020A8 RID: 8360
			ID = 13
		}
	}
}
