using System;
using System.IO;

namespace PegasusShared
{
	// Token: 0x02000135 RID: 309
	public class ProfileNoticeLevelUp : IProtoBuf
	{
		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06001449 RID: 5193 RVA: 0x00045DD1 File Offset: 0x00043FD1
		// (set) Token: 0x0600144A RID: 5194 RVA: 0x00045DD9 File Offset: 0x00043FD9
		public int HeroClass { get; set; }

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x0600144B RID: 5195 RVA: 0x00045DE2 File Offset: 0x00043FE2
		// (set) Token: 0x0600144C RID: 5196 RVA: 0x00045DEA File Offset: 0x00043FEA
		public int NewLevel { get; set; }

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x0600144D RID: 5197 RVA: 0x00045DF3 File Offset: 0x00043FF3
		// (set) Token: 0x0600144E RID: 5198 RVA: 0x00045DFB File Offset: 0x00043FFB
		public int TotalLevel
		{
			get
			{
				return this._TotalLevel;
			}
			set
			{
				this._TotalLevel = value;
				this.HasTotalLevel = true;
			}
		}

		// Token: 0x0600144F RID: 5199 RVA: 0x00045E0C File Offset: 0x0004400C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.HeroClass.GetHashCode();
			num ^= this.NewLevel.GetHashCode();
			if (this.HasTotalLevel)
			{
				num ^= this.TotalLevel.GetHashCode();
			}
			return num;
		}

		// Token: 0x06001450 RID: 5200 RVA: 0x00045E64 File Offset: 0x00044064
		public override bool Equals(object obj)
		{
			ProfileNoticeLevelUp profileNoticeLevelUp = obj as ProfileNoticeLevelUp;
			return profileNoticeLevelUp != null && this.HeroClass.Equals(profileNoticeLevelUp.HeroClass) && this.NewLevel.Equals(profileNoticeLevelUp.NewLevel) && this.HasTotalLevel == profileNoticeLevelUp.HasTotalLevel && (!this.HasTotalLevel || this.TotalLevel.Equals(profileNoticeLevelUp.TotalLevel));
		}

		// Token: 0x06001451 RID: 5201 RVA: 0x00045EDC File Offset: 0x000440DC
		public void Deserialize(Stream stream)
		{
			ProfileNoticeLevelUp.Deserialize(stream, this);
		}

		// Token: 0x06001452 RID: 5202 RVA: 0x00045EE6 File Offset: 0x000440E6
		public static ProfileNoticeLevelUp Deserialize(Stream stream, ProfileNoticeLevelUp instance)
		{
			return ProfileNoticeLevelUp.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001453 RID: 5203 RVA: 0x00045EF4 File Offset: 0x000440F4
		public static ProfileNoticeLevelUp DeserializeLengthDelimited(Stream stream)
		{
			ProfileNoticeLevelUp profileNoticeLevelUp = new ProfileNoticeLevelUp();
			ProfileNoticeLevelUp.DeserializeLengthDelimited(stream, profileNoticeLevelUp);
			return profileNoticeLevelUp;
		}

		// Token: 0x06001454 RID: 5204 RVA: 0x00045F10 File Offset: 0x00044110
		public static ProfileNoticeLevelUp DeserializeLengthDelimited(Stream stream, ProfileNoticeLevelUp instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ProfileNoticeLevelUp.Deserialize(stream, instance, num);
		}

		// Token: 0x06001455 RID: 5205 RVA: 0x00045F38 File Offset: 0x00044138
		public static ProfileNoticeLevelUp Deserialize(Stream stream, ProfileNoticeLevelUp instance, long limit)
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
							instance.TotalLevel = (int)ProtocolParser.ReadUInt64(stream);
						}
					}
					else
					{
						instance.NewLevel = (int)ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.HeroClass = (int)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06001456 RID: 5206 RVA: 0x00045FE8 File Offset: 0x000441E8
		public void Serialize(Stream stream)
		{
			ProfileNoticeLevelUp.Serialize(stream, this);
		}

		// Token: 0x06001457 RID: 5207 RVA: 0x00045FF4 File Offset: 0x000441F4
		public static void Serialize(Stream stream, ProfileNoticeLevelUp instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.HeroClass));
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.NewLevel));
			if (instance.HasTotalLevel)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.TotalLevel));
			}
		}

		// Token: 0x06001458 RID: 5208 RVA: 0x00046048 File Offset: 0x00044248
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.HeroClass));
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.NewLevel));
			if (this.HasTotalLevel)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.TotalLevel));
			}
			return num + 2U;
		}

		// Token: 0x04000638 RID: 1592
		public bool HasTotalLevel;

		// Token: 0x04000639 RID: 1593
		private int _TotalLevel;

		// Token: 0x02000629 RID: 1577
		public enum NoticeID
		{
			// Token: 0x040020AC RID: 8364
			ID = 15
		}
	}
}
