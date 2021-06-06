using System;
using System.IO;

namespace PegasusShared
{
	// Token: 0x0200013E RID: 318
	public class ProfileNoticeDeckGranted : IProtoBuf
	{
		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x060014DA RID: 5338 RVA: 0x0004760E File Offset: 0x0004580E
		// (set) Token: 0x060014DB RID: 5339 RVA: 0x00047616 File Offset: 0x00045816
		public int DeckDbiId
		{
			get
			{
				return this._DeckDbiId;
			}
			set
			{
				this._DeckDbiId = value;
				this.HasDeckDbiId = true;
			}
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x060014DC RID: 5340 RVA: 0x00047626 File Offset: 0x00045826
		// (set) Token: 0x060014DD RID: 5341 RVA: 0x0004762E File Offset: 0x0004582E
		public int ClassId
		{
			get
			{
				return this._ClassId;
			}
			set
			{
				this._ClassId = value;
				this.HasClassId = true;
			}
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x060014DE RID: 5342 RVA: 0x0004763E File Offset: 0x0004583E
		// (set) Token: 0x060014DF RID: 5343 RVA: 0x00047646 File Offset: 0x00045846
		public long PlayerDeckId
		{
			get
			{
				return this._PlayerDeckId;
			}
			set
			{
				this._PlayerDeckId = value;
				this.HasPlayerDeckId = true;
			}
		}

		// Token: 0x060014E0 RID: 5344 RVA: 0x00047658 File Offset: 0x00045858
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasDeckDbiId)
			{
				num ^= this.DeckDbiId.GetHashCode();
			}
			if (this.HasClassId)
			{
				num ^= this.ClassId.GetHashCode();
			}
			if (this.HasPlayerDeckId)
			{
				num ^= this.PlayerDeckId.GetHashCode();
			}
			return num;
		}

		// Token: 0x060014E1 RID: 5345 RVA: 0x000476C0 File Offset: 0x000458C0
		public override bool Equals(object obj)
		{
			ProfileNoticeDeckGranted profileNoticeDeckGranted = obj as ProfileNoticeDeckGranted;
			return profileNoticeDeckGranted != null && this.HasDeckDbiId == profileNoticeDeckGranted.HasDeckDbiId && (!this.HasDeckDbiId || this.DeckDbiId.Equals(profileNoticeDeckGranted.DeckDbiId)) && this.HasClassId == profileNoticeDeckGranted.HasClassId && (!this.HasClassId || this.ClassId.Equals(profileNoticeDeckGranted.ClassId)) && this.HasPlayerDeckId == profileNoticeDeckGranted.HasPlayerDeckId && (!this.HasPlayerDeckId || this.PlayerDeckId.Equals(profileNoticeDeckGranted.PlayerDeckId));
		}

		// Token: 0x060014E2 RID: 5346 RVA: 0x00047764 File Offset: 0x00045964
		public void Deserialize(Stream stream)
		{
			ProfileNoticeDeckGranted.Deserialize(stream, this);
		}

		// Token: 0x060014E3 RID: 5347 RVA: 0x0004776E File Offset: 0x0004596E
		public static ProfileNoticeDeckGranted Deserialize(Stream stream, ProfileNoticeDeckGranted instance)
		{
			return ProfileNoticeDeckGranted.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060014E4 RID: 5348 RVA: 0x0004777C File Offset: 0x0004597C
		public static ProfileNoticeDeckGranted DeserializeLengthDelimited(Stream stream)
		{
			ProfileNoticeDeckGranted profileNoticeDeckGranted = new ProfileNoticeDeckGranted();
			ProfileNoticeDeckGranted.DeserializeLengthDelimited(stream, profileNoticeDeckGranted);
			return profileNoticeDeckGranted;
		}

		// Token: 0x060014E5 RID: 5349 RVA: 0x00047798 File Offset: 0x00045998
		public static ProfileNoticeDeckGranted DeserializeLengthDelimited(Stream stream, ProfileNoticeDeckGranted instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ProfileNoticeDeckGranted.Deserialize(stream, instance, num);
		}

		// Token: 0x060014E6 RID: 5350 RVA: 0x000477C0 File Offset: 0x000459C0
		public static ProfileNoticeDeckGranted Deserialize(Stream stream, ProfileNoticeDeckGranted instance, long limit)
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
							instance.PlayerDeckId = (long)ProtocolParser.ReadUInt64(stream);
						}
					}
					else
					{
						instance.ClassId = (int)ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.DeckDbiId = (int)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060014E7 RID: 5351 RVA: 0x0004786F File Offset: 0x00045A6F
		public void Serialize(Stream stream)
		{
			ProfileNoticeDeckGranted.Serialize(stream, this);
		}

		// Token: 0x060014E8 RID: 5352 RVA: 0x00047878 File Offset: 0x00045A78
		public static void Serialize(Stream stream, ProfileNoticeDeckGranted instance)
		{
			if (instance.HasDeckDbiId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.DeckDbiId));
			}
			if (instance.HasClassId)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.ClassId));
			}
			if (instance.HasPlayerDeckId)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.PlayerDeckId);
			}
		}

		// Token: 0x060014E9 RID: 5353 RVA: 0x000478DC File Offset: 0x00045ADC
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasDeckDbiId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.DeckDbiId));
			}
			if (this.HasClassId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.ClassId));
			}
			if (this.HasPlayerDeckId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.PlayerDeckId);
			}
			return num;
		}

		// Token: 0x04000658 RID: 1624
		public bool HasDeckDbiId;

		// Token: 0x04000659 RID: 1625
		private int _DeckDbiId;

		// Token: 0x0400065A RID: 1626
		public bool HasClassId;

		// Token: 0x0400065B RID: 1627
		private int _ClassId;

		// Token: 0x0400065C RID: 1628
		public bool HasPlayerDeckId;

		// Token: 0x0400065D RID: 1629
		private long _PlayerDeckId;

		// Token: 0x02000634 RID: 1588
		public enum NoticeID
		{
			// Token: 0x040020C9 RID: 8393
			ID = 26
		}
	}
}
