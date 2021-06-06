using System;
using System.IO;

namespace PegasusShared
{
	// Token: 0x02000132 RID: 306
	public class ProfileNoticeBonusStars : IProtoBuf
	{
		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06001422 RID: 5154 RVA: 0x00045991 File Offset: 0x00043B91
		// (set) Token: 0x06001423 RID: 5155 RVA: 0x00045999 File Offset: 0x00043B99
		public int StarLevel { get; set; }

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06001424 RID: 5156 RVA: 0x000459A2 File Offset: 0x00043BA2
		// (set) Token: 0x06001425 RID: 5157 RVA: 0x000459AA File Offset: 0x00043BAA
		public int Stars { get; set; }

		// Token: 0x06001426 RID: 5158 RVA: 0x000459B4 File Offset: 0x00043BB4
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.StarLevel.GetHashCode() ^ this.Stars.GetHashCode();
		}

		// Token: 0x06001427 RID: 5159 RVA: 0x000459EC File Offset: 0x00043BEC
		public override bool Equals(object obj)
		{
			ProfileNoticeBonusStars profileNoticeBonusStars = obj as ProfileNoticeBonusStars;
			return profileNoticeBonusStars != null && this.StarLevel.Equals(profileNoticeBonusStars.StarLevel) && this.Stars.Equals(profileNoticeBonusStars.Stars);
		}

		// Token: 0x06001428 RID: 5160 RVA: 0x00045A36 File Offset: 0x00043C36
		public void Deserialize(Stream stream)
		{
			ProfileNoticeBonusStars.Deserialize(stream, this);
		}

		// Token: 0x06001429 RID: 5161 RVA: 0x00045A40 File Offset: 0x00043C40
		public static ProfileNoticeBonusStars Deserialize(Stream stream, ProfileNoticeBonusStars instance)
		{
			return ProfileNoticeBonusStars.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600142A RID: 5162 RVA: 0x00045A4C File Offset: 0x00043C4C
		public static ProfileNoticeBonusStars DeserializeLengthDelimited(Stream stream)
		{
			ProfileNoticeBonusStars profileNoticeBonusStars = new ProfileNoticeBonusStars();
			ProfileNoticeBonusStars.DeserializeLengthDelimited(stream, profileNoticeBonusStars);
			return profileNoticeBonusStars;
		}

		// Token: 0x0600142B RID: 5163 RVA: 0x00045A68 File Offset: 0x00043C68
		public static ProfileNoticeBonusStars DeserializeLengthDelimited(Stream stream, ProfileNoticeBonusStars instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ProfileNoticeBonusStars.Deserialize(stream, instance, num);
		}

		// Token: 0x0600142C RID: 5164 RVA: 0x00045A90 File Offset: 0x00043C90
		public static ProfileNoticeBonusStars Deserialize(Stream stream, ProfileNoticeBonusStars instance, long limit)
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
						instance.Stars = (int)ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.StarLevel = (int)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600142D RID: 5165 RVA: 0x00045B29 File Offset: 0x00043D29
		public void Serialize(Stream stream)
		{
			ProfileNoticeBonusStars.Serialize(stream, this);
		}

		// Token: 0x0600142E RID: 5166 RVA: 0x00045B32 File Offset: 0x00043D32
		public static void Serialize(Stream stream, ProfileNoticeBonusStars instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.StarLevel));
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Stars));
		}

		// Token: 0x0600142F RID: 5167 RVA: 0x00045B5D File Offset: 0x00043D5D
		public uint GetSerializedSize()
		{
			return 0U + ProtocolParser.SizeOfUInt64((ulong)((long)this.StarLevel)) + ProtocolParser.SizeOfUInt64((ulong)((long)this.Stars)) + 2U;
		}

		// Token: 0x02000626 RID: 1574
		public enum NoticeID
		{
			// Token: 0x040020A6 RID: 8358
			ID = 12
		}
	}
}
