using System;
using System.IO;

namespace PegasusShared
{
	// Token: 0x02000131 RID: 305
	public class ProfileNoticeCardBack : IProtoBuf
	{
		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06001415 RID: 5141 RVA: 0x0004581B File Offset: 0x00043A1B
		// (set) Token: 0x06001416 RID: 5142 RVA: 0x00045823 File Offset: 0x00043A23
		public int CardBack { get; set; }

		// Token: 0x06001417 RID: 5143 RVA: 0x0004582C File Offset: 0x00043A2C
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.CardBack.GetHashCode();
		}

		// Token: 0x06001418 RID: 5144 RVA: 0x00045854 File Offset: 0x00043A54
		public override bool Equals(object obj)
		{
			ProfileNoticeCardBack profileNoticeCardBack = obj as ProfileNoticeCardBack;
			return profileNoticeCardBack != null && this.CardBack.Equals(profileNoticeCardBack.CardBack);
		}

		// Token: 0x06001419 RID: 5145 RVA: 0x00045886 File Offset: 0x00043A86
		public void Deserialize(Stream stream)
		{
			ProfileNoticeCardBack.Deserialize(stream, this);
		}

		// Token: 0x0600141A RID: 5146 RVA: 0x00045890 File Offset: 0x00043A90
		public static ProfileNoticeCardBack Deserialize(Stream stream, ProfileNoticeCardBack instance)
		{
			return ProfileNoticeCardBack.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600141B RID: 5147 RVA: 0x0004589C File Offset: 0x00043A9C
		public static ProfileNoticeCardBack DeserializeLengthDelimited(Stream stream)
		{
			ProfileNoticeCardBack profileNoticeCardBack = new ProfileNoticeCardBack();
			ProfileNoticeCardBack.DeserializeLengthDelimited(stream, profileNoticeCardBack);
			return profileNoticeCardBack;
		}

		// Token: 0x0600141C RID: 5148 RVA: 0x000458B8 File Offset: 0x00043AB8
		public static ProfileNoticeCardBack DeserializeLengthDelimited(Stream stream, ProfileNoticeCardBack instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ProfileNoticeCardBack.Deserialize(stream, instance, num);
		}

		// Token: 0x0600141D RID: 5149 RVA: 0x000458E0 File Offset: 0x00043AE0
		public static ProfileNoticeCardBack Deserialize(Stream stream, ProfileNoticeCardBack instance, long limit)
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
					instance.CardBack = (int)ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x0600141E RID: 5150 RVA: 0x00045960 File Offset: 0x00043B60
		public void Serialize(Stream stream)
		{
			ProfileNoticeCardBack.Serialize(stream, this);
		}

		// Token: 0x0600141F RID: 5151 RVA: 0x00045969 File Offset: 0x00043B69
		public static void Serialize(Stream stream, ProfileNoticeCardBack instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.CardBack));
		}

		// Token: 0x06001420 RID: 5152 RVA: 0x0004597F File Offset: 0x00043B7F
		public uint GetSerializedSize()
		{
			return 0U + ProtocolParser.SizeOfUInt64((ulong)((long)this.CardBack)) + 1U;
		}

		// Token: 0x02000625 RID: 1573
		public enum NoticeID
		{
			// Token: 0x040020A4 RID: 8356
			ID = 11
		}
	}
}
