using System;
using System.IO;

namespace PegasusShared
{
	// Token: 0x02000134 RID: 308
	public class ProfileNoticeAdventureProgress : IProtoBuf
	{
		// Token: 0x170003CB RID: 971
		// (get) Token: 0x0600143C RID: 5180 RVA: 0x00045C5A File Offset: 0x00043E5A
		// (set) Token: 0x0600143D RID: 5181 RVA: 0x00045C62 File Offset: 0x00043E62
		public int WingId { get; set; }

		// Token: 0x0600143E RID: 5182 RVA: 0x00045C6C File Offset: 0x00043E6C
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.WingId.GetHashCode();
		}

		// Token: 0x0600143F RID: 5183 RVA: 0x00045C94 File Offset: 0x00043E94
		public override bool Equals(object obj)
		{
			ProfileNoticeAdventureProgress profileNoticeAdventureProgress = obj as ProfileNoticeAdventureProgress;
			return profileNoticeAdventureProgress != null && this.WingId.Equals(profileNoticeAdventureProgress.WingId);
		}

		// Token: 0x06001440 RID: 5184 RVA: 0x00045CC6 File Offset: 0x00043EC6
		public void Deserialize(Stream stream)
		{
			ProfileNoticeAdventureProgress.Deserialize(stream, this);
		}

		// Token: 0x06001441 RID: 5185 RVA: 0x00045CD0 File Offset: 0x00043ED0
		public static ProfileNoticeAdventureProgress Deserialize(Stream stream, ProfileNoticeAdventureProgress instance)
		{
			return ProfileNoticeAdventureProgress.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001442 RID: 5186 RVA: 0x00045CDC File Offset: 0x00043EDC
		public static ProfileNoticeAdventureProgress DeserializeLengthDelimited(Stream stream)
		{
			ProfileNoticeAdventureProgress profileNoticeAdventureProgress = new ProfileNoticeAdventureProgress();
			ProfileNoticeAdventureProgress.DeserializeLengthDelimited(stream, profileNoticeAdventureProgress);
			return profileNoticeAdventureProgress;
		}

		// Token: 0x06001443 RID: 5187 RVA: 0x00045CF8 File Offset: 0x00043EF8
		public static ProfileNoticeAdventureProgress DeserializeLengthDelimited(Stream stream, ProfileNoticeAdventureProgress instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ProfileNoticeAdventureProgress.Deserialize(stream, instance, num);
		}

		// Token: 0x06001444 RID: 5188 RVA: 0x00045D20 File Offset: 0x00043F20
		public static ProfileNoticeAdventureProgress Deserialize(Stream stream, ProfileNoticeAdventureProgress instance, long limit)
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
					instance.WingId = (int)ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x06001445 RID: 5189 RVA: 0x00045DA0 File Offset: 0x00043FA0
		public void Serialize(Stream stream)
		{
			ProfileNoticeAdventureProgress.Serialize(stream, this);
		}

		// Token: 0x06001446 RID: 5190 RVA: 0x00045DA9 File Offset: 0x00043FA9
		public static void Serialize(Stream stream, ProfileNoticeAdventureProgress instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.WingId));
		}

		// Token: 0x06001447 RID: 5191 RVA: 0x00045DBF File Offset: 0x00043FBF
		public uint GetSerializedSize()
		{
			return 0U + ProtocolParser.SizeOfUInt64((ulong)((long)this.WingId)) + 1U;
		}

		// Token: 0x02000628 RID: 1576
		public enum NoticeID
		{
			// Token: 0x040020AA RID: 8362
			ID = 14
		}
	}
}
