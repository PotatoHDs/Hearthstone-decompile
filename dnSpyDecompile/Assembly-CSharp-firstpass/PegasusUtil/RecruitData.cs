using System;
using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	// Token: 0x020000DE RID: 222
	public class RecruitData : IProtoBuf
	{
		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000F16 RID: 3862 RVA: 0x00036CA6 File Offset: 0x00034EA6
		// (set) Token: 0x06000F17 RID: 3863 RVA: 0x00036CAE File Offset: 0x00034EAE
		public BnetId GameAccountId { get; set; }

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000F18 RID: 3864 RVA: 0x00036CB7 File Offset: 0x00034EB7
		// (set) Token: 0x06000F19 RID: 3865 RVA: 0x00036CBF File Offset: 0x00034EBF
		public uint Progress { get; set; }

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000F1A RID: 3866 RVA: 0x00036CC8 File Offset: 0x00034EC8
		// (set) Token: 0x06000F1B RID: 3867 RVA: 0x00036CD0 File Offset: 0x00034ED0
		public RecruitAFriendState RecruitState { get; set; }

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000F1C RID: 3868 RVA: 0x00036CD9 File Offset: 0x00034ED9
		// (set) Token: 0x06000F1D RID: 3869 RVA: 0x00036CE1 File Offset: 0x00034EE1
		public Date GraduationDate
		{
			get
			{
				return this._GraduationDate;
			}
			set
			{
				this._GraduationDate = value;
				this.HasGraduationDate = (value != null);
			}
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000F1E RID: 3870 RVA: 0x00036CF4 File Offset: 0x00034EF4
		// (set) Token: 0x06000F1F RID: 3871 RVA: 0x00036CFC File Offset: 0x00034EFC
		public uint RecruiterReward
		{
			get
			{
				return this._RecruiterReward;
			}
			set
			{
				this._RecruiterReward = value;
				this.HasRecruiterReward = true;
			}
		}

		// Token: 0x06000F20 RID: 3872 RVA: 0x00036D0C File Offset: 0x00034F0C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.GameAccountId.GetHashCode();
			num ^= this.Progress.GetHashCode();
			num ^= this.RecruitState.GetHashCode();
			if (this.HasGraduationDate)
			{
				num ^= this.GraduationDate.GetHashCode();
			}
			if (this.HasRecruiterReward)
			{
				num ^= this.RecruiterReward.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000F21 RID: 3873 RVA: 0x00036D8C File Offset: 0x00034F8C
		public override bool Equals(object obj)
		{
			RecruitData recruitData = obj as RecruitData;
			return recruitData != null && this.GameAccountId.Equals(recruitData.GameAccountId) && this.Progress.Equals(recruitData.Progress) && this.RecruitState.Equals(recruitData.RecruitState) && this.HasGraduationDate == recruitData.HasGraduationDate && (!this.HasGraduationDate || this.GraduationDate.Equals(recruitData.GraduationDate)) && this.HasRecruiterReward == recruitData.HasRecruiterReward && (!this.HasRecruiterReward || this.RecruiterReward.Equals(recruitData.RecruiterReward));
		}

		// Token: 0x06000F22 RID: 3874 RVA: 0x00036E4F File Offset: 0x0003504F
		public void Deserialize(Stream stream)
		{
			RecruitData.Deserialize(stream, this);
		}

		// Token: 0x06000F23 RID: 3875 RVA: 0x00036E59 File Offset: 0x00035059
		public static RecruitData Deserialize(Stream stream, RecruitData instance)
		{
			return RecruitData.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000F24 RID: 3876 RVA: 0x00036E64 File Offset: 0x00035064
		public static RecruitData DeserializeLengthDelimited(Stream stream)
		{
			RecruitData recruitData = new RecruitData();
			RecruitData.DeserializeLengthDelimited(stream, recruitData);
			return recruitData;
		}

		// Token: 0x06000F25 RID: 3877 RVA: 0x00036E80 File Offset: 0x00035080
		public static RecruitData DeserializeLengthDelimited(Stream stream, RecruitData instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return RecruitData.Deserialize(stream, instance, num);
		}

		// Token: 0x06000F26 RID: 3878 RVA: 0x00036EA8 File Offset: 0x000350A8
		public static RecruitData Deserialize(Stream stream, RecruitData instance, long limit)
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
					if (num <= 16)
					{
						if (num != 10)
						{
							if (num == 16)
							{
								instance.Progress = ProtocolParser.ReadUInt32(stream);
								continue;
							}
						}
						else
						{
							if (instance.GameAccountId == null)
							{
								instance.GameAccountId = BnetId.DeserializeLengthDelimited(stream);
								continue;
							}
							BnetId.DeserializeLengthDelimited(stream, instance.GameAccountId);
							continue;
						}
					}
					else
					{
						if (num == 24)
						{
							instance.RecruitState = (RecruitAFriendState)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num != 34)
						{
							if (num == 40)
							{
								instance.RecruiterReward = ProtocolParser.ReadUInt32(stream);
								continue;
							}
						}
						else
						{
							if (instance.GraduationDate == null)
							{
								instance.GraduationDate = Date.DeserializeLengthDelimited(stream);
								continue;
							}
							Date.DeserializeLengthDelimited(stream, instance.GraduationDate);
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

		// Token: 0x06000F27 RID: 3879 RVA: 0x00036FCD File Offset: 0x000351CD
		public void Serialize(Stream stream)
		{
			RecruitData.Serialize(stream, this);
		}

		// Token: 0x06000F28 RID: 3880 RVA: 0x00036FD8 File Offset: 0x000351D8
		public static void Serialize(Stream stream, RecruitData instance)
		{
			if (instance.GameAccountId == null)
			{
				throw new ArgumentNullException("GameAccountId", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.GameAccountId.GetSerializedSize());
			BnetId.Serialize(stream, instance.GameAccountId);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt32(stream, instance.Progress);
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.RecruitState));
			if (instance.HasGraduationDate)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.GraduationDate.GetSerializedSize());
				Date.Serialize(stream, instance.GraduationDate);
			}
			if (instance.HasRecruiterReward)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt32(stream, instance.RecruiterReward);
			}
		}

		// Token: 0x06000F29 RID: 3881 RVA: 0x00037094 File Offset: 0x00035294
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint serializedSize = this.GameAccountId.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			num += ProtocolParser.SizeOfUInt32(this.Progress);
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.RecruitState));
			if (this.HasGraduationDate)
			{
				num += 1U;
				uint serializedSize2 = this.GraduationDate.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasRecruiterReward)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.RecruiterReward);
			}
			return num + 3U;
		}

		// Token: 0x040004F5 RID: 1269
		public bool HasGraduationDate;

		// Token: 0x040004F6 RID: 1270
		private Date _GraduationDate;

		// Token: 0x040004F7 RID: 1271
		public bool HasRecruiterReward;

		// Token: 0x040004F8 RID: 1272
		private uint _RecruiterReward;
	}
}
