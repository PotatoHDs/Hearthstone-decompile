using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x0200008C RID: 140
	public class DraftError : IProtoBuf
	{
		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000950 RID: 2384 RVA: 0x000223AA File Offset: 0x000205AA
		// (set) Token: 0x06000951 RID: 2385 RVA: 0x000223B2 File Offset: 0x000205B2
		public DraftError.ErrorCode ErrorCode_ { get; set; }

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000952 RID: 2386 RVA: 0x000223BB File Offset: 0x000205BB
		// (set) Token: 0x06000953 RID: 2387 RVA: 0x000223C3 File Offset: 0x000205C3
		public int NumTicketsOwned
		{
			get
			{
				return this._NumTicketsOwned;
			}
			set
			{
				this._NumTicketsOwned = value;
				this.HasNumTicketsOwned = true;
			}
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x000223D4 File Offset: 0x000205D4
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.ErrorCode_.GetHashCode();
			if (this.HasNumTicketsOwned)
			{
				num ^= this.NumTicketsOwned.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000955 RID: 2389 RVA: 0x00022420 File Offset: 0x00020620
		public override bool Equals(object obj)
		{
			DraftError draftError = obj as DraftError;
			return draftError != null && this.ErrorCode_.Equals(draftError.ErrorCode_) && this.HasNumTicketsOwned == draftError.HasNumTicketsOwned && (!this.HasNumTicketsOwned || this.NumTicketsOwned.Equals(draftError.NumTicketsOwned));
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x0002248B File Offset: 0x0002068B
		public void Deserialize(Stream stream)
		{
			DraftError.Deserialize(stream, this);
		}

		// Token: 0x06000957 RID: 2391 RVA: 0x00022495 File Offset: 0x00020695
		public static DraftError Deserialize(Stream stream, DraftError instance)
		{
			return DraftError.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000958 RID: 2392 RVA: 0x000224A0 File Offset: 0x000206A0
		public static DraftError DeserializeLengthDelimited(Stream stream)
		{
			DraftError draftError = new DraftError();
			DraftError.DeserializeLengthDelimited(stream, draftError);
			return draftError;
		}

		// Token: 0x06000959 RID: 2393 RVA: 0x000224BC File Offset: 0x000206BC
		public static DraftError DeserializeLengthDelimited(Stream stream, DraftError instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return DraftError.Deserialize(stream, instance, num);
		}

		// Token: 0x0600095A RID: 2394 RVA: 0x000224E4 File Offset: 0x000206E4
		public static DraftError Deserialize(Stream stream, DraftError instance, long limit)
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
						instance.NumTicketsOwned = (int)ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.ErrorCode_ = (DraftError.ErrorCode)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600095B RID: 2395 RVA: 0x0002257D File Offset: 0x0002077D
		public void Serialize(Stream stream)
		{
			DraftError.Serialize(stream, this);
		}

		// Token: 0x0600095C RID: 2396 RVA: 0x00022586 File Offset: 0x00020786
		public static void Serialize(Stream stream, DraftError instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.ErrorCode_));
			if (instance.HasNumTicketsOwned)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.NumTicketsOwned));
			}
		}

		// Token: 0x0600095D RID: 2397 RVA: 0x000225BC File Offset: 0x000207BC
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.ErrorCode_));
			if (this.HasNumTicketsOwned)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.NumTicketsOwned));
			}
			return num + 1U;
		}

		// Token: 0x0400033C RID: 828
		public bool HasNumTicketsOwned;

		// Token: 0x0400033D RID: 829
		private int _NumTicketsOwned;

		// Token: 0x0200059F RID: 1439
		public enum PacketID
		{
			// Token: 0x04001F3A RID: 7994
			ID = 251
		}

		// Token: 0x020005A0 RID: 1440
		public enum ErrorCode
		{
			// Token: 0x04001F3C RID: 7996
			DE_UNKNOWN,
			// Token: 0x04001F3D RID: 7997
			DE_NO_LICENSE,
			// Token: 0x04001F3E RID: 7998
			DE_RETIRE_FIRST,
			// Token: 0x04001F3F RID: 7999
			DE_NOT_IN_DRAFT,
			// Token: 0x04001F40 RID: 8000
			DE_BAD_DECK,
			// Token: 0x04001F41 RID: 8001
			DE_BAD_SLOT,
			// Token: 0x04001F42 RID: 8002
			DE_BAD_INDEX,
			// Token: 0x04001F43 RID: 8003
			DE_NOT_IN_DRAFT_BUT_COULD_BE,
			// Token: 0x04001F44 RID: 8004
			DE_FEATURE_DISABLED,
			// Token: 0x04001F45 RID: 8005
			DE_SEASON_INCREMENTED
		}
	}
}
