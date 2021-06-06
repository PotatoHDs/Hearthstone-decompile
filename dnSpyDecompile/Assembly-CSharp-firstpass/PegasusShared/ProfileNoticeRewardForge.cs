using System;
using System.IO;

namespace PegasusShared
{
	// Token: 0x0200012E RID: 302
	public class ProfileNoticeRewardForge : IProtoBuf
	{
		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x060013E6 RID: 5094 RVA: 0x0004504D File Offset: 0x0004324D
		// (set) Token: 0x060013E7 RID: 5095 RVA: 0x00045055 File Offset: 0x00043255
		public int Quantity { get; set; }

		// Token: 0x060013E8 RID: 5096 RVA: 0x00045060 File Offset: 0x00043260
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.Quantity.GetHashCode();
		}

		// Token: 0x060013E9 RID: 5097 RVA: 0x00045088 File Offset: 0x00043288
		public override bool Equals(object obj)
		{
			ProfileNoticeRewardForge profileNoticeRewardForge = obj as ProfileNoticeRewardForge;
			return profileNoticeRewardForge != null && this.Quantity.Equals(profileNoticeRewardForge.Quantity);
		}

		// Token: 0x060013EA RID: 5098 RVA: 0x000450BA File Offset: 0x000432BA
		public void Deserialize(Stream stream)
		{
			ProfileNoticeRewardForge.Deserialize(stream, this);
		}

		// Token: 0x060013EB RID: 5099 RVA: 0x000450C4 File Offset: 0x000432C4
		public static ProfileNoticeRewardForge Deserialize(Stream stream, ProfileNoticeRewardForge instance)
		{
			return ProfileNoticeRewardForge.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060013EC RID: 5100 RVA: 0x000450D0 File Offset: 0x000432D0
		public static ProfileNoticeRewardForge DeserializeLengthDelimited(Stream stream)
		{
			ProfileNoticeRewardForge profileNoticeRewardForge = new ProfileNoticeRewardForge();
			ProfileNoticeRewardForge.DeserializeLengthDelimited(stream, profileNoticeRewardForge);
			return profileNoticeRewardForge;
		}

		// Token: 0x060013ED RID: 5101 RVA: 0x000450EC File Offset: 0x000432EC
		public static ProfileNoticeRewardForge DeserializeLengthDelimited(Stream stream, ProfileNoticeRewardForge instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ProfileNoticeRewardForge.Deserialize(stream, instance, num);
		}

		// Token: 0x060013EE RID: 5102 RVA: 0x00045114 File Offset: 0x00043314
		public static ProfileNoticeRewardForge Deserialize(Stream stream, ProfileNoticeRewardForge instance, long limit)
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
					instance.Quantity = (int)ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x060013EF RID: 5103 RVA: 0x00045194 File Offset: 0x00043394
		public void Serialize(Stream stream)
		{
			ProfileNoticeRewardForge.Serialize(stream, this);
		}

		// Token: 0x060013F0 RID: 5104 RVA: 0x0004519D File Offset: 0x0004339D
		public static void Serialize(Stream stream, ProfileNoticeRewardForge instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Quantity));
		}

		// Token: 0x060013F1 RID: 5105 RVA: 0x000451B3 File Offset: 0x000433B3
		public uint GetSerializedSize()
		{
			return 0U + ProtocolParser.SizeOfUInt64((ulong)((long)this.Quantity)) + 1U;
		}

		// Token: 0x02000622 RID: 1570
		public enum NoticeID
		{
			// Token: 0x0400209E RID: 8350
			ID = 8
		}
	}
}
