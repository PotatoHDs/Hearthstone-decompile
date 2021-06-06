using System;
using System.IO;

namespace PegasusShared
{
	// Token: 0x02000136 RID: 310
	public class ProfileNoticeAccountLicense : IProtoBuf
	{
		// Token: 0x170003CF RID: 975
		// (get) Token: 0x0600145A RID: 5210 RVA: 0x00046095 File Offset: 0x00044295
		// (set) Token: 0x0600145B RID: 5211 RVA: 0x0004609D File Offset: 0x0004429D
		public long License { get; set; }

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x0600145C RID: 5212 RVA: 0x000460A6 File Offset: 0x000442A6
		// (set) Token: 0x0600145D RID: 5213 RVA: 0x000460AE File Offset: 0x000442AE
		public long CasId { get; set; }

		// Token: 0x0600145E RID: 5214 RVA: 0x000460B8 File Offset: 0x000442B8
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.License.GetHashCode() ^ this.CasId.GetHashCode();
		}

		// Token: 0x0600145F RID: 5215 RVA: 0x000460F0 File Offset: 0x000442F0
		public override bool Equals(object obj)
		{
			ProfileNoticeAccountLicense profileNoticeAccountLicense = obj as ProfileNoticeAccountLicense;
			return profileNoticeAccountLicense != null && this.License.Equals(profileNoticeAccountLicense.License) && this.CasId.Equals(profileNoticeAccountLicense.CasId);
		}

		// Token: 0x06001460 RID: 5216 RVA: 0x0004613A File Offset: 0x0004433A
		public void Deserialize(Stream stream)
		{
			ProfileNoticeAccountLicense.Deserialize(stream, this);
		}

		// Token: 0x06001461 RID: 5217 RVA: 0x00046144 File Offset: 0x00044344
		public static ProfileNoticeAccountLicense Deserialize(Stream stream, ProfileNoticeAccountLicense instance)
		{
			return ProfileNoticeAccountLicense.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001462 RID: 5218 RVA: 0x00046150 File Offset: 0x00044350
		public static ProfileNoticeAccountLicense DeserializeLengthDelimited(Stream stream)
		{
			ProfileNoticeAccountLicense profileNoticeAccountLicense = new ProfileNoticeAccountLicense();
			ProfileNoticeAccountLicense.DeserializeLengthDelimited(stream, profileNoticeAccountLicense);
			return profileNoticeAccountLicense;
		}

		// Token: 0x06001463 RID: 5219 RVA: 0x0004616C File Offset: 0x0004436C
		public static ProfileNoticeAccountLicense DeserializeLengthDelimited(Stream stream, ProfileNoticeAccountLicense instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ProfileNoticeAccountLicense.Deserialize(stream, instance, num);
		}

		// Token: 0x06001464 RID: 5220 RVA: 0x00046194 File Offset: 0x00044394
		public static ProfileNoticeAccountLicense Deserialize(Stream stream, ProfileNoticeAccountLicense instance, long limit)
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
						instance.CasId = (long)ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.License = (long)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06001465 RID: 5221 RVA: 0x0004622B File Offset: 0x0004442B
		public void Serialize(Stream stream)
		{
			ProfileNoticeAccountLicense.Serialize(stream, this);
		}

		// Token: 0x06001466 RID: 5222 RVA: 0x00046234 File Offset: 0x00044434
		public static void Serialize(Stream stream, ProfileNoticeAccountLicense instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.License);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.CasId);
		}

		// Token: 0x06001467 RID: 5223 RVA: 0x0004625D File Offset: 0x0004445D
		public uint GetSerializedSize()
		{
			return 0U + ProtocolParser.SizeOfUInt64((ulong)this.License) + ProtocolParser.SizeOfUInt64((ulong)this.CasId) + 2U;
		}

		// Token: 0x0200062A RID: 1578
		public enum NoticeID
		{
			// Token: 0x040020AE RID: 8366
			ID = 16
		}
	}
}
