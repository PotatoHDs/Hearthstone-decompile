using System;
using System.IO;

namespace PegasusShared
{
	// Token: 0x0200012D RID: 301
	public class ProfileNoticeRewardMount : IProtoBuf
	{
		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x060013D9 RID: 5081 RVA: 0x00044ED5 File Offset: 0x000430D5
		// (set) Token: 0x060013DA RID: 5082 RVA: 0x00044EDD File Offset: 0x000430DD
		public int MountId { get; set; }

		// Token: 0x060013DB RID: 5083 RVA: 0x00044EE8 File Offset: 0x000430E8
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.MountId.GetHashCode();
		}

		// Token: 0x060013DC RID: 5084 RVA: 0x00044F10 File Offset: 0x00043110
		public override bool Equals(object obj)
		{
			ProfileNoticeRewardMount profileNoticeRewardMount = obj as ProfileNoticeRewardMount;
			return profileNoticeRewardMount != null && this.MountId.Equals(profileNoticeRewardMount.MountId);
		}

		// Token: 0x060013DD RID: 5085 RVA: 0x00044F42 File Offset: 0x00043142
		public void Deserialize(Stream stream)
		{
			ProfileNoticeRewardMount.Deserialize(stream, this);
		}

		// Token: 0x060013DE RID: 5086 RVA: 0x00044F4C File Offset: 0x0004314C
		public static ProfileNoticeRewardMount Deserialize(Stream stream, ProfileNoticeRewardMount instance)
		{
			return ProfileNoticeRewardMount.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060013DF RID: 5087 RVA: 0x00044F58 File Offset: 0x00043158
		public static ProfileNoticeRewardMount DeserializeLengthDelimited(Stream stream)
		{
			ProfileNoticeRewardMount profileNoticeRewardMount = new ProfileNoticeRewardMount();
			ProfileNoticeRewardMount.DeserializeLengthDelimited(stream, profileNoticeRewardMount);
			return profileNoticeRewardMount;
		}

		// Token: 0x060013E0 RID: 5088 RVA: 0x00044F74 File Offset: 0x00043174
		public static ProfileNoticeRewardMount DeserializeLengthDelimited(Stream stream, ProfileNoticeRewardMount instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ProfileNoticeRewardMount.Deserialize(stream, instance, num);
		}

		// Token: 0x060013E1 RID: 5089 RVA: 0x00044F9C File Offset: 0x0004319C
		public static ProfileNoticeRewardMount Deserialize(Stream stream, ProfileNoticeRewardMount instance, long limit)
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
					instance.MountId = (int)ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x060013E2 RID: 5090 RVA: 0x0004501C File Offset: 0x0004321C
		public void Serialize(Stream stream)
		{
			ProfileNoticeRewardMount.Serialize(stream, this);
		}

		// Token: 0x060013E3 RID: 5091 RVA: 0x00045025 File Offset: 0x00043225
		public static void Serialize(Stream stream, ProfileNoticeRewardMount instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.MountId));
		}

		// Token: 0x060013E4 RID: 5092 RVA: 0x0004503B File Offset: 0x0004323B
		public uint GetSerializedSize()
		{
			return 0U + ProtocolParser.SizeOfUInt64((ulong)((long)this.MountId)) + 1U;
		}

		// Token: 0x02000621 RID: 1569
		public enum NoticeID
		{
			// Token: 0x0400209C RID: 8348
			ID = 7
		}
	}
}
