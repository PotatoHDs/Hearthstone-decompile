using System;
using System.IO;

namespace PegasusShared
{
	// Token: 0x02000137 RID: 311
	public class ProfileNoticeTavernBrawlRewards : IProtoBuf
	{
		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06001469 RID: 5225 RVA: 0x0004627A File Offset: 0x0004447A
		// (set) Token: 0x0600146A RID: 5226 RVA: 0x00046282 File Offset: 0x00044482
		public RewardChest RewardChest { get; set; }

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x0600146B RID: 5227 RVA: 0x0004628B File Offset: 0x0004448B
		// (set) Token: 0x0600146C RID: 5228 RVA: 0x00046293 File Offset: 0x00044493
		public int NumWins { get; set; }

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x0600146D RID: 5229 RVA: 0x0004629C File Offset: 0x0004449C
		// (set) Token: 0x0600146E RID: 5230 RVA: 0x000462A4 File Offset: 0x000444A4
		public TavernBrawlMode BrawlMode
		{
			get
			{
				return this._BrawlMode;
			}
			set
			{
				this._BrawlMode = value;
				this.HasBrawlMode = true;
			}
		}

		// Token: 0x0600146F RID: 5231 RVA: 0x000462B4 File Offset: 0x000444B4
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.RewardChest.GetHashCode();
			num ^= this.NumWins.GetHashCode();
			if (this.HasBrawlMode)
			{
				num ^= this.BrawlMode.GetHashCode();
			}
			return num;
		}

		// Token: 0x06001470 RID: 5232 RVA: 0x0004630C File Offset: 0x0004450C
		public override bool Equals(object obj)
		{
			ProfileNoticeTavernBrawlRewards profileNoticeTavernBrawlRewards = obj as ProfileNoticeTavernBrawlRewards;
			return profileNoticeTavernBrawlRewards != null && this.RewardChest.Equals(profileNoticeTavernBrawlRewards.RewardChest) && this.NumWins.Equals(profileNoticeTavernBrawlRewards.NumWins) && this.HasBrawlMode == profileNoticeTavernBrawlRewards.HasBrawlMode && (!this.HasBrawlMode || this.BrawlMode.Equals(profileNoticeTavernBrawlRewards.BrawlMode));
		}

		// Token: 0x06001471 RID: 5233 RVA: 0x0004638C File Offset: 0x0004458C
		public void Deserialize(Stream stream)
		{
			ProfileNoticeTavernBrawlRewards.Deserialize(stream, this);
		}

		// Token: 0x06001472 RID: 5234 RVA: 0x00046396 File Offset: 0x00044596
		public static ProfileNoticeTavernBrawlRewards Deserialize(Stream stream, ProfileNoticeTavernBrawlRewards instance)
		{
			return ProfileNoticeTavernBrawlRewards.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001473 RID: 5235 RVA: 0x000463A4 File Offset: 0x000445A4
		public static ProfileNoticeTavernBrawlRewards DeserializeLengthDelimited(Stream stream)
		{
			ProfileNoticeTavernBrawlRewards profileNoticeTavernBrawlRewards = new ProfileNoticeTavernBrawlRewards();
			ProfileNoticeTavernBrawlRewards.DeserializeLengthDelimited(stream, profileNoticeTavernBrawlRewards);
			return profileNoticeTavernBrawlRewards;
		}

		// Token: 0x06001474 RID: 5236 RVA: 0x000463C0 File Offset: 0x000445C0
		public static ProfileNoticeTavernBrawlRewards DeserializeLengthDelimited(Stream stream, ProfileNoticeTavernBrawlRewards instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ProfileNoticeTavernBrawlRewards.Deserialize(stream, instance, num);
		}

		// Token: 0x06001475 RID: 5237 RVA: 0x000463E8 File Offset: 0x000445E8
		public static ProfileNoticeTavernBrawlRewards Deserialize(Stream stream, ProfileNoticeTavernBrawlRewards instance, long limit)
		{
			instance.BrawlMode = TavernBrawlMode.TB_MODE_NORMAL;
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
						if (num != 24)
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
							instance.BrawlMode = (TavernBrawlMode)ProtocolParser.ReadUInt64(stream);
						}
					}
					else
					{
						instance.NumWins = (int)ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x06001476 RID: 5238 RVA: 0x000464BF File Offset: 0x000446BF
		public void Serialize(Stream stream)
		{
			ProfileNoticeTavernBrawlRewards.Serialize(stream, this);
		}

		// Token: 0x06001477 RID: 5239 RVA: 0x000464C8 File Offset: 0x000446C8
		public static void Serialize(Stream stream, ProfileNoticeTavernBrawlRewards instance)
		{
			if (instance.RewardChest == null)
			{
				throw new ArgumentNullException("RewardChest", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.RewardChest.GetSerializedSize());
			RewardChest.Serialize(stream, instance.RewardChest);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.NumWins));
			if (instance.HasBrawlMode)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.BrawlMode));
			}
		}

		// Token: 0x06001478 RID: 5240 RVA: 0x00046544 File Offset: 0x00044744
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint serializedSize = this.RewardChest.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.NumWins));
			if (this.HasBrawlMode)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.BrawlMode));
			}
			return num + 2U;
		}

		// Token: 0x0400063E RID: 1598
		public bool HasBrawlMode;

		// Token: 0x0400063F RID: 1599
		private TavernBrawlMode _BrawlMode;

		// Token: 0x0200062B RID: 1579
		public enum NoticeID
		{
			// Token: 0x040020B0 RID: 8368
			ID = 17
		}
	}
}
