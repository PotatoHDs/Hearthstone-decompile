using System;
using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	// Token: 0x020000B1 RID: 177
	public class RewardProgress : IProtoBuf
	{
		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000C45 RID: 3141 RVA: 0x0002E5A7 File Offset: 0x0002C7A7
		// (set) Token: 0x06000C46 RID: 3142 RVA: 0x0002E5AF File Offset: 0x0002C7AF
		public Date SeasonEnd { get; set; }

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000C47 RID: 3143 RVA: 0x0002E5B8 File Offset: 0x0002C7B8
		// (set) Token: 0x06000C48 RID: 3144 RVA: 0x0002E5C0 File Offset: 0x0002C7C0
		public int SeasonNumber { get; set; }

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000C49 RID: 3145 RVA: 0x0002E5C9 File Offset: 0x0002C7C9
		// (set) Token: 0x06000C4A RID: 3146 RVA: 0x0002E5D1 File Offset: 0x0002C7D1
		public Date NextQuestCancel { get; set; }

		// Token: 0x06000C4B RID: 3147 RVA: 0x0002E5DC File Offset: 0x0002C7DC
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.SeasonEnd.GetHashCode() ^ this.SeasonNumber.GetHashCode() ^ this.NextQuestCancel.GetHashCode();
		}

		// Token: 0x06000C4C RID: 3148 RVA: 0x0002E61C File Offset: 0x0002C81C
		public override bool Equals(object obj)
		{
			RewardProgress rewardProgress = obj as RewardProgress;
			return rewardProgress != null && this.SeasonEnd.Equals(rewardProgress.SeasonEnd) && this.SeasonNumber.Equals(rewardProgress.SeasonNumber) && this.NextQuestCancel.Equals(rewardProgress.NextQuestCancel);
		}

		// Token: 0x06000C4D RID: 3149 RVA: 0x0002E678 File Offset: 0x0002C878
		public void Deserialize(Stream stream)
		{
			RewardProgress.Deserialize(stream, this);
		}

		// Token: 0x06000C4E RID: 3150 RVA: 0x0002E682 File Offset: 0x0002C882
		public static RewardProgress Deserialize(Stream stream, RewardProgress instance)
		{
			return RewardProgress.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000C4F RID: 3151 RVA: 0x0002E690 File Offset: 0x0002C890
		public static RewardProgress DeserializeLengthDelimited(Stream stream)
		{
			RewardProgress rewardProgress = new RewardProgress();
			RewardProgress.DeserializeLengthDelimited(stream, rewardProgress);
			return rewardProgress;
		}

		// Token: 0x06000C50 RID: 3152 RVA: 0x0002E6AC File Offset: 0x0002C8AC
		public static RewardProgress DeserializeLengthDelimited(Stream stream, RewardProgress instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return RewardProgress.Deserialize(stream, instance, num);
		}

		// Token: 0x06000C51 RID: 3153 RVA: 0x0002E6D4 File Offset: 0x0002C8D4
		public static RewardProgress Deserialize(Stream stream, RewardProgress instance, long limit)
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
					if (num != 40)
					{
						if (num != 90)
						{
							Key key = ProtocolParser.ReadKey((byte)num, stream);
							if (key.Field == 0U)
							{
								throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
							}
							ProtocolParser.SkipKey(stream, key);
						}
						else if (instance.NextQuestCancel == null)
						{
							instance.NextQuestCancel = Date.DeserializeLengthDelimited(stream);
						}
						else
						{
							Date.DeserializeLengthDelimited(stream, instance.NextQuestCancel);
						}
					}
					else
					{
						instance.SeasonNumber = (int)ProtocolParser.ReadUInt64(stream);
					}
				}
				else if (instance.SeasonEnd == null)
				{
					instance.SeasonEnd = Date.DeserializeLengthDelimited(stream);
				}
				else
				{
					Date.DeserializeLengthDelimited(stream, instance.SeasonEnd);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06000C52 RID: 3154 RVA: 0x0002E7BD File Offset: 0x0002C9BD
		public void Serialize(Stream stream)
		{
			RewardProgress.Serialize(stream, this);
		}

		// Token: 0x06000C53 RID: 3155 RVA: 0x0002E7C8 File Offset: 0x0002C9C8
		public static void Serialize(Stream stream, RewardProgress instance)
		{
			if (instance.SeasonEnd == null)
			{
				throw new ArgumentNullException("SeasonEnd", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.SeasonEnd.GetSerializedSize());
			Date.Serialize(stream, instance.SeasonEnd);
			stream.WriteByte(40);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.SeasonNumber));
			if (instance.NextQuestCancel == null)
			{
				throw new ArgumentNullException("NextQuestCancel", "Required by proto specification.");
			}
			stream.WriteByte(90);
			ProtocolParser.WriteUInt32(stream, instance.NextQuestCancel.GetSerializedSize());
			Date.Serialize(stream, instance.NextQuestCancel);
		}

		// Token: 0x06000C54 RID: 3156 RVA: 0x0002E864 File Offset: 0x0002CA64
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint serializedSize = this.SeasonEnd.GetSerializedSize();
			uint num2 = num + (serializedSize + ProtocolParser.SizeOfUInt32(serializedSize)) + ProtocolParser.SizeOfUInt64((ulong)((long)this.SeasonNumber));
			uint serializedSize2 = this.NextQuestCancel.GetSerializedSize();
			return num2 + (serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2)) + 3U;
		}

		// Token: 0x020005BA RID: 1466
		public enum PacketID
		{
			// Token: 0x04001F7C RID: 8060
			ID = 271
		}
	}
}
