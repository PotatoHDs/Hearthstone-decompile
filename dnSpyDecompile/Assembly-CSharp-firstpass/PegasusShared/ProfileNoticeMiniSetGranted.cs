using System;
using System.IO;

namespace PegasusShared
{
	// Token: 0x0200013F RID: 319
	public class ProfileNoticeMiniSetGranted : IProtoBuf
	{
		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x060014EB RID: 5355 RVA: 0x0004793C File Offset: 0x00045B3C
		// (set) Token: 0x060014EC RID: 5356 RVA: 0x00047944 File Offset: 0x00045B44
		public int MiniSetId
		{
			get
			{
				return this._MiniSetId;
			}
			set
			{
				this._MiniSetId = value;
				this.HasMiniSetId = true;
			}
		}

		// Token: 0x060014ED RID: 5357 RVA: 0x00047954 File Offset: 0x00045B54
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasMiniSetId)
			{
				num ^= this.MiniSetId.GetHashCode();
			}
			return num;
		}

		// Token: 0x060014EE RID: 5358 RVA: 0x00047988 File Offset: 0x00045B88
		public override bool Equals(object obj)
		{
			ProfileNoticeMiniSetGranted profileNoticeMiniSetGranted = obj as ProfileNoticeMiniSetGranted;
			return profileNoticeMiniSetGranted != null && this.HasMiniSetId == profileNoticeMiniSetGranted.HasMiniSetId && (!this.HasMiniSetId || this.MiniSetId.Equals(profileNoticeMiniSetGranted.MiniSetId));
		}

		// Token: 0x060014EF RID: 5359 RVA: 0x000479D0 File Offset: 0x00045BD0
		public void Deserialize(Stream stream)
		{
			ProfileNoticeMiniSetGranted.Deserialize(stream, this);
		}

		// Token: 0x060014F0 RID: 5360 RVA: 0x000479DA File Offset: 0x00045BDA
		public static ProfileNoticeMiniSetGranted Deserialize(Stream stream, ProfileNoticeMiniSetGranted instance)
		{
			return ProfileNoticeMiniSetGranted.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060014F1 RID: 5361 RVA: 0x000479E8 File Offset: 0x00045BE8
		public static ProfileNoticeMiniSetGranted DeserializeLengthDelimited(Stream stream)
		{
			ProfileNoticeMiniSetGranted profileNoticeMiniSetGranted = new ProfileNoticeMiniSetGranted();
			ProfileNoticeMiniSetGranted.DeserializeLengthDelimited(stream, profileNoticeMiniSetGranted);
			return profileNoticeMiniSetGranted;
		}

		// Token: 0x060014F2 RID: 5362 RVA: 0x00047A04 File Offset: 0x00045C04
		public static ProfileNoticeMiniSetGranted DeserializeLengthDelimited(Stream stream, ProfileNoticeMiniSetGranted instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ProfileNoticeMiniSetGranted.Deserialize(stream, instance, num);
		}

		// Token: 0x060014F3 RID: 5363 RVA: 0x00047A2C File Offset: 0x00045C2C
		public static ProfileNoticeMiniSetGranted Deserialize(Stream stream, ProfileNoticeMiniSetGranted instance, long limit)
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
					instance.MiniSetId = (int)ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x060014F4 RID: 5364 RVA: 0x00047AAC File Offset: 0x00045CAC
		public void Serialize(Stream stream)
		{
			ProfileNoticeMiniSetGranted.Serialize(stream, this);
		}

		// Token: 0x060014F5 RID: 5365 RVA: 0x00047AB5 File Offset: 0x00045CB5
		public static void Serialize(Stream stream, ProfileNoticeMiniSetGranted instance)
		{
			if (instance.HasMiniSetId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.MiniSetId));
			}
		}

		// Token: 0x060014F6 RID: 5366 RVA: 0x00047AD4 File Offset: 0x00045CD4
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasMiniSetId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.MiniSetId));
			}
			return num;
		}

		// Token: 0x0400065E RID: 1630
		public bool HasMiniSetId;

		// Token: 0x0400065F RID: 1631
		private int _MiniSetId;

		// Token: 0x02000635 RID: 1589
		public enum NoticeID
		{
			// Token: 0x040020CB RID: 8395
			ID = 27
		}
	}
}
