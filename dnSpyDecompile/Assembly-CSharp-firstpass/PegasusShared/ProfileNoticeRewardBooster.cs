using System;
using System.IO;

namespace PegasusShared
{
	// Token: 0x02000128 RID: 296
	public class ProfileNoticeRewardBooster : IProtoBuf
	{
		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06001388 RID: 5000 RVA: 0x0004412E File Offset: 0x0004232E
		// (set) Token: 0x06001389 RID: 5001 RVA: 0x00044136 File Offset: 0x00042336
		public int BoosterType { get; set; }

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x0600138A RID: 5002 RVA: 0x0004413F File Offset: 0x0004233F
		// (set) Token: 0x0600138B RID: 5003 RVA: 0x00044147 File Offset: 0x00042347
		public int BoosterCount { get; set; }

		// Token: 0x0600138C RID: 5004 RVA: 0x00044150 File Offset: 0x00042350
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.BoosterType.GetHashCode() ^ this.BoosterCount.GetHashCode();
		}

		// Token: 0x0600138D RID: 5005 RVA: 0x00044188 File Offset: 0x00042388
		public override bool Equals(object obj)
		{
			ProfileNoticeRewardBooster profileNoticeRewardBooster = obj as ProfileNoticeRewardBooster;
			return profileNoticeRewardBooster != null && this.BoosterType.Equals(profileNoticeRewardBooster.BoosterType) && this.BoosterCount.Equals(profileNoticeRewardBooster.BoosterCount);
		}

		// Token: 0x0600138E RID: 5006 RVA: 0x000441D2 File Offset: 0x000423D2
		public void Deserialize(Stream stream)
		{
			ProfileNoticeRewardBooster.Deserialize(stream, this);
		}

		// Token: 0x0600138F RID: 5007 RVA: 0x000441DC File Offset: 0x000423DC
		public static ProfileNoticeRewardBooster Deserialize(Stream stream, ProfileNoticeRewardBooster instance)
		{
			return ProfileNoticeRewardBooster.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001390 RID: 5008 RVA: 0x000441E8 File Offset: 0x000423E8
		public static ProfileNoticeRewardBooster DeserializeLengthDelimited(Stream stream)
		{
			ProfileNoticeRewardBooster profileNoticeRewardBooster = new ProfileNoticeRewardBooster();
			ProfileNoticeRewardBooster.DeserializeLengthDelimited(stream, profileNoticeRewardBooster);
			return profileNoticeRewardBooster;
		}

		// Token: 0x06001391 RID: 5009 RVA: 0x00044204 File Offset: 0x00042404
		public static ProfileNoticeRewardBooster DeserializeLengthDelimited(Stream stream, ProfileNoticeRewardBooster instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ProfileNoticeRewardBooster.Deserialize(stream, instance, num);
		}

		// Token: 0x06001392 RID: 5010 RVA: 0x0004422C File Offset: 0x0004242C
		public static ProfileNoticeRewardBooster Deserialize(Stream stream, ProfileNoticeRewardBooster instance, long limit)
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
						instance.BoosterCount = (int)ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.BoosterType = (int)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06001393 RID: 5011 RVA: 0x000442C5 File Offset: 0x000424C5
		public void Serialize(Stream stream)
		{
			ProfileNoticeRewardBooster.Serialize(stream, this);
		}

		// Token: 0x06001394 RID: 5012 RVA: 0x000442CE File Offset: 0x000424CE
		public static void Serialize(Stream stream, ProfileNoticeRewardBooster instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.BoosterType));
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.BoosterCount));
		}

		// Token: 0x06001395 RID: 5013 RVA: 0x000442F9 File Offset: 0x000424F9
		public uint GetSerializedSize()
		{
			return 0U + ProtocolParser.SizeOfUInt64((ulong)((long)this.BoosterType)) + ProtocolParser.SizeOfUInt64((ulong)((long)this.BoosterCount)) + 2U;
		}

		// Token: 0x0200061A RID: 1562
		public enum NoticeID
		{
			// Token: 0x04002087 RID: 8327
			ID = 2
		}
	}
}
