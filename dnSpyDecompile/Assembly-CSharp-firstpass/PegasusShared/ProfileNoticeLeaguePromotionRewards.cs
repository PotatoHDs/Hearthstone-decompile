using System;
using System.IO;

namespace PegasusShared
{
	// Token: 0x0200013A RID: 314
	public class ProfileNoticeLeaguePromotionRewards : IProtoBuf
	{
		// Token: 0x170003DA RID: 986
		// (get) Token: 0x0600149C RID: 5276 RVA: 0x00046B45 File Offset: 0x00044D45
		// (set) Token: 0x0600149D RID: 5277 RVA: 0x00046B4D File Offset: 0x00044D4D
		public RewardChest RewardChest { get; set; }

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x0600149E RID: 5278 RVA: 0x00046B56 File Offset: 0x00044D56
		// (set) Token: 0x0600149F RID: 5279 RVA: 0x00046B5E File Offset: 0x00044D5E
		public int LeagueId { get; set; }

		// Token: 0x060014A0 RID: 5280 RVA: 0x00046B68 File Offset: 0x00044D68
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.RewardChest.GetHashCode() ^ this.LeagueId.GetHashCode();
		}

		// Token: 0x060014A1 RID: 5281 RVA: 0x00046B9C File Offset: 0x00044D9C
		public override bool Equals(object obj)
		{
			ProfileNoticeLeaguePromotionRewards profileNoticeLeaguePromotionRewards = obj as ProfileNoticeLeaguePromotionRewards;
			return profileNoticeLeaguePromotionRewards != null && this.RewardChest.Equals(profileNoticeLeaguePromotionRewards.RewardChest) && this.LeagueId.Equals(profileNoticeLeaguePromotionRewards.LeagueId);
		}

		// Token: 0x060014A2 RID: 5282 RVA: 0x00046BE3 File Offset: 0x00044DE3
		public void Deserialize(Stream stream)
		{
			ProfileNoticeLeaguePromotionRewards.Deserialize(stream, this);
		}

		// Token: 0x060014A3 RID: 5283 RVA: 0x00046BED File Offset: 0x00044DED
		public static ProfileNoticeLeaguePromotionRewards Deserialize(Stream stream, ProfileNoticeLeaguePromotionRewards instance)
		{
			return ProfileNoticeLeaguePromotionRewards.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060014A4 RID: 5284 RVA: 0x00046BF8 File Offset: 0x00044DF8
		public static ProfileNoticeLeaguePromotionRewards DeserializeLengthDelimited(Stream stream)
		{
			ProfileNoticeLeaguePromotionRewards profileNoticeLeaguePromotionRewards = new ProfileNoticeLeaguePromotionRewards();
			ProfileNoticeLeaguePromotionRewards.DeserializeLengthDelimited(stream, profileNoticeLeaguePromotionRewards);
			return profileNoticeLeaguePromotionRewards;
		}

		// Token: 0x060014A5 RID: 5285 RVA: 0x00046C14 File Offset: 0x00044E14
		public static ProfileNoticeLeaguePromotionRewards DeserializeLengthDelimited(Stream stream, ProfileNoticeLeaguePromotionRewards instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ProfileNoticeLeaguePromotionRewards.Deserialize(stream, instance, num);
		}

		// Token: 0x060014A6 RID: 5286 RVA: 0x00046C3C File Offset: 0x00044E3C
		public static ProfileNoticeLeaguePromotionRewards Deserialize(Stream stream, ProfileNoticeLeaguePromotionRewards instance, long limit)
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
				else if (num != 10)
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
						instance.LeagueId = (int)ProtocolParser.ReadUInt64(stream);
					}
				}
				else if (instance.RewardChest == null)
				{
					instance.RewardChest = RewardChest.DeserializeLengthDelimited(stream);
				}
				else
				{
					RewardChest.DeserializeLengthDelimited(stream, instance.RewardChest);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060014A7 RID: 5287 RVA: 0x00046CEF File Offset: 0x00044EEF
		public void Serialize(Stream stream)
		{
			ProfileNoticeLeaguePromotionRewards.Serialize(stream, this);
		}

		// Token: 0x060014A8 RID: 5288 RVA: 0x00046CF8 File Offset: 0x00044EF8
		public static void Serialize(Stream stream, ProfileNoticeLeaguePromotionRewards instance)
		{
			if (instance.RewardChest == null)
			{
				throw new ArgumentNullException("RewardChest", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.RewardChest.GetSerializedSize());
			RewardChest.Serialize(stream, instance.RewardChest);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.LeagueId));
		}

		// Token: 0x060014A9 RID: 5289 RVA: 0x00046D58 File Offset: 0x00044F58
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint serializedSize = this.RewardChest.GetSerializedSize();
			return num + (serializedSize + ProtocolParser.SizeOfUInt32(serializedSize)) + ProtocolParser.SizeOfUInt64((ulong)((long)this.LeagueId)) + 2U;
		}

		// Token: 0x0200062E RID: 1582
		public enum NoticeID
		{
			// Token: 0x040020B6 RID: 8374
			ID = 21
		}
	}
}
