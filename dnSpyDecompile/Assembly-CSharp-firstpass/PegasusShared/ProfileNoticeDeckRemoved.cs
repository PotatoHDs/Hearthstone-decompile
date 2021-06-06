using System;
using System.IO;

namespace PegasusShared
{
	// Token: 0x0200013D RID: 317
	public class ProfileNoticeDeckRemoved : IProtoBuf
	{
		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x060014CD RID: 5325 RVA: 0x0004744A File Offset: 0x0004564A
		// (set) Token: 0x060014CE RID: 5326 RVA: 0x00047452 File Offset: 0x00045652
		public long DeckId
		{
			get
			{
				return this._DeckId;
			}
			set
			{
				this._DeckId = value;
				this.HasDeckId = true;
			}
		}

		// Token: 0x060014CF RID: 5327 RVA: 0x00047464 File Offset: 0x00045664
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasDeckId)
			{
				num ^= this.DeckId.GetHashCode();
			}
			return num;
		}

		// Token: 0x060014D0 RID: 5328 RVA: 0x00047498 File Offset: 0x00045698
		public override bool Equals(object obj)
		{
			ProfileNoticeDeckRemoved profileNoticeDeckRemoved = obj as ProfileNoticeDeckRemoved;
			return profileNoticeDeckRemoved != null && this.HasDeckId == profileNoticeDeckRemoved.HasDeckId && (!this.HasDeckId || this.DeckId.Equals(profileNoticeDeckRemoved.DeckId));
		}

		// Token: 0x060014D1 RID: 5329 RVA: 0x000474E0 File Offset: 0x000456E0
		public void Deserialize(Stream stream)
		{
			ProfileNoticeDeckRemoved.Deserialize(stream, this);
		}

		// Token: 0x060014D2 RID: 5330 RVA: 0x000474EA File Offset: 0x000456EA
		public static ProfileNoticeDeckRemoved Deserialize(Stream stream, ProfileNoticeDeckRemoved instance)
		{
			return ProfileNoticeDeckRemoved.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060014D3 RID: 5331 RVA: 0x000474F8 File Offset: 0x000456F8
		public static ProfileNoticeDeckRemoved DeserializeLengthDelimited(Stream stream)
		{
			ProfileNoticeDeckRemoved profileNoticeDeckRemoved = new ProfileNoticeDeckRemoved();
			ProfileNoticeDeckRemoved.DeserializeLengthDelimited(stream, profileNoticeDeckRemoved);
			return profileNoticeDeckRemoved;
		}

		// Token: 0x060014D4 RID: 5332 RVA: 0x00047514 File Offset: 0x00045714
		public static ProfileNoticeDeckRemoved DeserializeLengthDelimited(Stream stream, ProfileNoticeDeckRemoved instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ProfileNoticeDeckRemoved.Deserialize(stream, instance, num);
		}

		// Token: 0x060014D5 RID: 5333 RVA: 0x0004753C File Offset: 0x0004573C
		public static ProfileNoticeDeckRemoved Deserialize(Stream stream, ProfileNoticeDeckRemoved instance, long limit)
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
					instance.DeckId = (long)ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x060014D6 RID: 5334 RVA: 0x000475BB File Offset: 0x000457BB
		public void Serialize(Stream stream)
		{
			ProfileNoticeDeckRemoved.Serialize(stream, this);
		}

		// Token: 0x060014D7 RID: 5335 RVA: 0x000475C4 File Offset: 0x000457C4
		public static void Serialize(Stream stream, ProfileNoticeDeckRemoved instance)
		{
			if (instance.HasDeckId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.DeckId);
			}
		}

		// Token: 0x060014D8 RID: 5336 RVA: 0x000475E4 File Offset: 0x000457E4
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasDeckId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.DeckId);
			}
			return num;
		}

		// Token: 0x04000656 RID: 1622
		public bool HasDeckId;

		// Token: 0x04000657 RID: 1623
		private long _DeckId;

		// Token: 0x02000633 RID: 1587
		public enum NoticeID
		{
			// Token: 0x040020C7 RID: 8391
			ID = 25
		}
	}
}
