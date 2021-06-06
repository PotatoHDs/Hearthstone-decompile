using System;
using System.IO;

namespace PegasusShared
{
	// Token: 0x0200013C RID: 316
	public class ProfileNoticeFreeDeckChoice : IProtoBuf
	{
		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x060014C0 RID: 5312 RVA: 0x00047287 File Offset: 0x00045487
		// (set) Token: 0x060014C1 RID: 5313 RVA: 0x0004728F File Offset: 0x0004548F
		public long PlayerId
		{
			get
			{
				return this._PlayerId;
			}
			set
			{
				this._PlayerId = value;
				this.HasPlayerId = true;
			}
		}

		// Token: 0x060014C2 RID: 5314 RVA: 0x000472A0 File Offset: 0x000454A0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasPlayerId)
			{
				num ^= this.PlayerId.GetHashCode();
			}
			return num;
		}

		// Token: 0x060014C3 RID: 5315 RVA: 0x000472D4 File Offset: 0x000454D4
		public override bool Equals(object obj)
		{
			ProfileNoticeFreeDeckChoice profileNoticeFreeDeckChoice = obj as ProfileNoticeFreeDeckChoice;
			return profileNoticeFreeDeckChoice != null && this.HasPlayerId == profileNoticeFreeDeckChoice.HasPlayerId && (!this.HasPlayerId || this.PlayerId.Equals(profileNoticeFreeDeckChoice.PlayerId));
		}

		// Token: 0x060014C4 RID: 5316 RVA: 0x0004731C File Offset: 0x0004551C
		public void Deserialize(Stream stream)
		{
			ProfileNoticeFreeDeckChoice.Deserialize(stream, this);
		}

		// Token: 0x060014C5 RID: 5317 RVA: 0x00047326 File Offset: 0x00045526
		public static ProfileNoticeFreeDeckChoice Deserialize(Stream stream, ProfileNoticeFreeDeckChoice instance)
		{
			return ProfileNoticeFreeDeckChoice.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060014C6 RID: 5318 RVA: 0x00047334 File Offset: 0x00045534
		public static ProfileNoticeFreeDeckChoice DeserializeLengthDelimited(Stream stream)
		{
			ProfileNoticeFreeDeckChoice profileNoticeFreeDeckChoice = new ProfileNoticeFreeDeckChoice();
			ProfileNoticeFreeDeckChoice.DeserializeLengthDelimited(stream, profileNoticeFreeDeckChoice);
			return profileNoticeFreeDeckChoice;
		}

		// Token: 0x060014C7 RID: 5319 RVA: 0x00047350 File Offset: 0x00045550
		public static ProfileNoticeFreeDeckChoice DeserializeLengthDelimited(Stream stream, ProfileNoticeFreeDeckChoice instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ProfileNoticeFreeDeckChoice.Deserialize(stream, instance, num);
		}

		// Token: 0x060014C8 RID: 5320 RVA: 0x00047378 File Offset: 0x00045578
		public static ProfileNoticeFreeDeckChoice Deserialize(Stream stream, ProfileNoticeFreeDeckChoice instance, long limit)
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
					instance.PlayerId = (long)ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x060014C9 RID: 5321 RVA: 0x000473F7 File Offset: 0x000455F7
		public void Serialize(Stream stream)
		{
			ProfileNoticeFreeDeckChoice.Serialize(stream, this);
		}

		// Token: 0x060014CA RID: 5322 RVA: 0x00047400 File Offset: 0x00045600
		public static void Serialize(Stream stream, ProfileNoticeFreeDeckChoice instance)
		{
			if (instance.HasPlayerId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.PlayerId);
			}
		}

		// Token: 0x060014CB RID: 5323 RVA: 0x00047420 File Offset: 0x00045620
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasPlayerId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.PlayerId);
			}
			return num;
		}

		// Token: 0x04000654 RID: 1620
		public bool HasPlayerId;

		// Token: 0x04000655 RID: 1621
		private long _PlayerId;

		// Token: 0x02000632 RID: 1586
		public enum NoticeID
		{
			// Token: 0x040020C5 RID: 8389
			ID = 24
		}
	}
}
