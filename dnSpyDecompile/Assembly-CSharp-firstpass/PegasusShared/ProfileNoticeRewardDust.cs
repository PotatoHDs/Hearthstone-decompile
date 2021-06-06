using System;
using System.IO;

namespace PegasusShared
{
	// Token: 0x0200012C RID: 300
	public class ProfileNoticeRewardDust : IProtoBuf
	{
		// Token: 0x170003BF RID: 959
		// (get) Token: 0x060013CC RID: 5068 RVA: 0x00044D5D File Offset: 0x00042F5D
		// (set) Token: 0x060013CD RID: 5069 RVA: 0x00044D65 File Offset: 0x00042F65
		public int Amount { get; set; }

		// Token: 0x060013CE RID: 5070 RVA: 0x00044D70 File Offset: 0x00042F70
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.Amount.GetHashCode();
		}

		// Token: 0x060013CF RID: 5071 RVA: 0x00044D98 File Offset: 0x00042F98
		public override bool Equals(object obj)
		{
			ProfileNoticeRewardDust profileNoticeRewardDust = obj as ProfileNoticeRewardDust;
			return profileNoticeRewardDust != null && this.Amount.Equals(profileNoticeRewardDust.Amount);
		}

		// Token: 0x060013D0 RID: 5072 RVA: 0x00044DCA File Offset: 0x00042FCA
		public void Deserialize(Stream stream)
		{
			ProfileNoticeRewardDust.Deserialize(stream, this);
		}

		// Token: 0x060013D1 RID: 5073 RVA: 0x00044DD4 File Offset: 0x00042FD4
		public static ProfileNoticeRewardDust Deserialize(Stream stream, ProfileNoticeRewardDust instance)
		{
			return ProfileNoticeRewardDust.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060013D2 RID: 5074 RVA: 0x00044DE0 File Offset: 0x00042FE0
		public static ProfileNoticeRewardDust DeserializeLengthDelimited(Stream stream)
		{
			ProfileNoticeRewardDust profileNoticeRewardDust = new ProfileNoticeRewardDust();
			ProfileNoticeRewardDust.DeserializeLengthDelimited(stream, profileNoticeRewardDust);
			return profileNoticeRewardDust;
		}

		// Token: 0x060013D3 RID: 5075 RVA: 0x00044DFC File Offset: 0x00042FFC
		public static ProfileNoticeRewardDust DeserializeLengthDelimited(Stream stream, ProfileNoticeRewardDust instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ProfileNoticeRewardDust.Deserialize(stream, instance, num);
		}

		// Token: 0x060013D4 RID: 5076 RVA: 0x00044E24 File Offset: 0x00043024
		public static ProfileNoticeRewardDust Deserialize(Stream stream, ProfileNoticeRewardDust instance, long limit)
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
					instance.Amount = (int)ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x060013D5 RID: 5077 RVA: 0x00044EA4 File Offset: 0x000430A4
		public void Serialize(Stream stream)
		{
			ProfileNoticeRewardDust.Serialize(stream, this);
		}

		// Token: 0x060013D6 RID: 5078 RVA: 0x00044EAD File Offset: 0x000430AD
		public static void Serialize(Stream stream, ProfileNoticeRewardDust instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Amount));
		}

		// Token: 0x060013D7 RID: 5079 RVA: 0x00044EC3 File Offset: 0x000430C3
		public uint GetSerializedSize()
		{
			return 0U + ProtocolParser.SizeOfUInt64((ulong)((long)this.Amount)) + 1U;
		}

		// Token: 0x02000620 RID: 1568
		public enum NoticeID
		{
			// Token: 0x0400209A RID: 8346
			ID = 6
		}
	}
}
