using System;
using System.IO;

namespace PegasusShared
{
	// Token: 0x02000139 RID: 313
	public class ProfileNoticeGenericRewardChest : IProtoBuf
	{
		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06001489 RID: 5257 RVA: 0x00046784 File Offset: 0x00044984
		// (set) Token: 0x0600148A RID: 5258 RVA: 0x0004678C File Offset: 0x0004498C
		public int RewardChestAssetId { get; set; }

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x0600148B RID: 5259 RVA: 0x00046795 File Offset: 0x00044995
		// (set) Token: 0x0600148C RID: 5260 RVA: 0x0004679D File Offset: 0x0004499D
		public RewardChest RewardChest { get; set; }

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x0600148D RID: 5261 RVA: 0x000467A6 File Offset: 0x000449A6
		// (set) Token: 0x0600148E RID: 5262 RVA: 0x000467AE File Offset: 0x000449AE
		public uint RewardChestByteSize
		{
			get
			{
				return this._RewardChestByteSize;
			}
			set
			{
				this._RewardChestByteSize = value;
				this.HasRewardChestByteSize = true;
			}
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x0600148F RID: 5263 RVA: 0x000467BE File Offset: 0x000449BE
		// (set) Token: 0x06001490 RID: 5264 RVA: 0x000467C6 File Offset: 0x000449C6
		public byte[] RewardChestHash
		{
			get
			{
				return this._RewardChestHash;
			}
			set
			{
				this._RewardChestHash = value;
				this.HasRewardChestHash = (value != null);
			}
		}

		// Token: 0x06001491 RID: 5265 RVA: 0x000467DC File Offset: 0x000449DC
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.RewardChestAssetId.GetHashCode();
			num ^= this.RewardChest.GetHashCode();
			if (this.HasRewardChestByteSize)
			{
				num ^= this.RewardChestByteSize.GetHashCode();
			}
			if (this.HasRewardChestHash)
			{
				num ^= this.RewardChestHash.GetHashCode();
			}
			return num;
		}

		// Token: 0x06001492 RID: 5266 RVA: 0x00046844 File Offset: 0x00044A44
		public override bool Equals(object obj)
		{
			ProfileNoticeGenericRewardChest profileNoticeGenericRewardChest = obj as ProfileNoticeGenericRewardChest;
			return profileNoticeGenericRewardChest != null && this.RewardChestAssetId.Equals(profileNoticeGenericRewardChest.RewardChestAssetId) && this.RewardChest.Equals(profileNoticeGenericRewardChest.RewardChest) && this.HasRewardChestByteSize == profileNoticeGenericRewardChest.HasRewardChestByteSize && (!this.HasRewardChestByteSize || this.RewardChestByteSize.Equals(profileNoticeGenericRewardChest.RewardChestByteSize)) && this.HasRewardChestHash == profileNoticeGenericRewardChest.HasRewardChestHash && (!this.HasRewardChestHash || this.RewardChestHash.Equals(profileNoticeGenericRewardChest.RewardChestHash));
		}

		// Token: 0x06001493 RID: 5267 RVA: 0x000468E4 File Offset: 0x00044AE4
		public void Deserialize(Stream stream)
		{
			ProfileNoticeGenericRewardChest.Deserialize(stream, this);
		}

		// Token: 0x06001494 RID: 5268 RVA: 0x000468EE File Offset: 0x00044AEE
		public static ProfileNoticeGenericRewardChest Deserialize(Stream stream, ProfileNoticeGenericRewardChest instance)
		{
			return ProfileNoticeGenericRewardChest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001495 RID: 5269 RVA: 0x000468FC File Offset: 0x00044AFC
		public static ProfileNoticeGenericRewardChest DeserializeLengthDelimited(Stream stream)
		{
			ProfileNoticeGenericRewardChest profileNoticeGenericRewardChest = new ProfileNoticeGenericRewardChest();
			ProfileNoticeGenericRewardChest.DeserializeLengthDelimited(stream, profileNoticeGenericRewardChest);
			return profileNoticeGenericRewardChest;
		}

		// Token: 0x06001496 RID: 5270 RVA: 0x00046918 File Offset: 0x00044B18
		public static ProfileNoticeGenericRewardChest DeserializeLengthDelimited(Stream stream, ProfileNoticeGenericRewardChest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ProfileNoticeGenericRewardChest.Deserialize(stream, instance, num);
		}

		// Token: 0x06001497 RID: 5271 RVA: 0x00046940 File Offset: 0x00044B40
		public static ProfileNoticeGenericRewardChest Deserialize(Stream stream, ProfileNoticeGenericRewardChest instance, long limit)
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
					if (num <= 18)
					{
						if (num == 8)
						{
							instance.RewardChestAssetId = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 18)
						{
							if (instance.RewardChest == null)
							{
								instance.RewardChest = RewardChest.DeserializeLengthDelimited(stream);
								continue;
							}
							RewardChest.DeserializeLengthDelimited(stream, instance.RewardChest);
							continue;
						}
					}
					else
					{
						if (num == 24)
						{
							instance.RewardChestByteSize = ProtocolParser.ReadUInt32(stream);
							continue;
						}
						if (num == 34)
						{
							instance.RewardChestHash = ProtocolParser.ReadBytes(stream);
							continue;
						}
					}
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

		// Token: 0x06001498 RID: 5272 RVA: 0x00046A2B File Offset: 0x00044C2B
		public void Serialize(Stream stream)
		{
			ProfileNoticeGenericRewardChest.Serialize(stream, this);
		}

		// Token: 0x06001499 RID: 5273 RVA: 0x00046A34 File Offset: 0x00044C34
		public static void Serialize(Stream stream, ProfileNoticeGenericRewardChest instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.RewardChestAssetId));
			if (instance.RewardChest == null)
			{
				throw new ArgumentNullException("RewardChest", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteUInt32(stream, instance.RewardChest.GetSerializedSize());
			RewardChest.Serialize(stream, instance.RewardChest);
			if (instance.HasRewardChestByteSize)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt32(stream, instance.RewardChestByteSize);
			}
			if (instance.HasRewardChestHash)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, instance.RewardChestHash);
			}
		}

		// Token: 0x0600149A RID: 5274 RVA: 0x00046ACC File Offset: 0x00044CCC
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.RewardChestAssetId));
			uint serializedSize = this.RewardChest.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (this.HasRewardChestByteSize)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.RewardChestByteSize);
			}
			if (this.HasRewardChestHash)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.RewardChestHash.Length) + (uint)this.RewardChestHash.Length;
			}
			return num + 2U;
		}

		// Token: 0x04000644 RID: 1604
		public bool HasRewardChestByteSize;

		// Token: 0x04000645 RID: 1605
		private uint _RewardChestByteSize;

		// Token: 0x04000646 RID: 1606
		public bool HasRewardChestHash;

		// Token: 0x04000647 RID: 1607
		private byte[] _RewardChestHash;

		// Token: 0x0200062D RID: 1581
		public enum NoticeID
		{
			// Token: 0x040020B4 RID: 8372
			ID = 20
		}
	}
}
