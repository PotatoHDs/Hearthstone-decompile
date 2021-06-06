using System;
using System.Collections.Generic;
using System.IO;
using PegasusShared;

namespace HSCachedDeckCompletion
{
	// Token: 0x02000017 RID: 23
	public class HSCachedDeckCompletionResponse : IProtoBuf
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00004707 File Offset: 0x00002907
		// (set) Token: 0x060000CF RID: 207 RVA: 0x0000470F File Offset: 0x0000290F
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

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x0000471F File Offset: 0x0000291F
		// (set) Token: 0x060000D1 RID: 209 RVA: 0x00004727 File Offset: 0x00002927
		public List<DeckCardData> PlayerDeckCard
		{
			get
			{
				return this._PlayerDeckCard;
			}
			set
			{
				this._PlayerDeckCard = value;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x00004730 File Offset: 0x00002930
		// (set) Token: 0x060000D3 RID: 211 RVA: 0x00004738 File Offset: 0x00002938
		public List<DeckCardData> IdealDeckCard
		{
			get
			{
				return this._IdealDeckCard;
			}
			set
			{
				this._IdealDeckCard = value;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x00004741 File Offset: 0x00002941
		// (set) Token: 0x060000D5 RID: 213 RVA: 0x00004749 File Offset: 0x00002949
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

		// Token: 0x060000D6 RID: 214 RVA: 0x0000475C File Offset: 0x0000295C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasPlayerId)
			{
				num ^= this.PlayerId.GetHashCode();
			}
			foreach (DeckCardData deckCardData in this.PlayerDeckCard)
			{
				num ^= deckCardData.GetHashCode();
			}
			foreach (DeckCardData deckCardData2 in this.IdealDeckCard)
			{
				num ^= deckCardData2.GetHashCode();
			}
			if (this.HasDeckId)
			{
				num ^= this.DeckId.GetHashCode();
			}
			return num;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00004838 File Offset: 0x00002A38
		public override bool Equals(object obj)
		{
			HSCachedDeckCompletionResponse hscachedDeckCompletionResponse = obj as HSCachedDeckCompletionResponse;
			if (hscachedDeckCompletionResponse == null)
			{
				return false;
			}
			if (this.HasPlayerId != hscachedDeckCompletionResponse.HasPlayerId || (this.HasPlayerId && !this.PlayerId.Equals(hscachedDeckCompletionResponse.PlayerId)))
			{
				return false;
			}
			if (this.PlayerDeckCard.Count != hscachedDeckCompletionResponse.PlayerDeckCard.Count)
			{
				return false;
			}
			for (int i = 0; i < this.PlayerDeckCard.Count; i++)
			{
				if (!this.PlayerDeckCard[i].Equals(hscachedDeckCompletionResponse.PlayerDeckCard[i]))
				{
					return false;
				}
			}
			if (this.IdealDeckCard.Count != hscachedDeckCompletionResponse.IdealDeckCard.Count)
			{
				return false;
			}
			for (int j = 0; j < this.IdealDeckCard.Count; j++)
			{
				if (!this.IdealDeckCard[j].Equals(hscachedDeckCompletionResponse.IdealDeckCard[j]))
				{
					return false;
				}
			}
			return this.HasDeckId == hscachedDeckCompletionResponse.HasDeckId && (!this.HasDeckId || this.DeckId.Equals(hscachedDeckCompletionResponse.DeckId));
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00004950 File Offset: 0x00002B50
		public void Deserialize(Stream stream)
		{
			HSCachedDeckCompletionResponse.Deserialize(stream, this);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x0000495A File Offset: 0x00002B5A
		public static HSCachedDeckCompletionResponse Deserialize(Stream stream, HSCachedDeckCompletionResponse instance)
		{
			return HSCachedDeckCompletionResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00004968 File Offset: 0x00002B68
		public static HSCachedDeckCompletionResponse DeserializeLengthDelimited(Stream stream)
		{
			HSCachedDeckCompletionResponse hscachedDeckCompletionResponse = new HSCachedDeckCompletionResponse();
			HSCachedDeckCompletionResponse.DeserializeLengthDelimited(stream, hscachedDeckCompletionResponse);
			return hscachedDeckCompletionResponse;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00004984 File Offset: 0x00002B84
		public static HSCachedDeckCompletionResponse DeserializeLengthDelimited(Stream stream, HSCachedDeckCompletionResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return HSCachedDeckCompletionResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x060000DC RID: 220 RVA: 0x000049AC File Offset: 0x00002BAC
		public static HSCachedDeckCompletionResponse Deserialize(Stream stream, HSCachedDeckCompletionResponse instance, long limit)
		{
			if (instance.PlayerDeckCard == null)
			{
				instance.PlayerDeckCard = new List<DeckCardData>();
			}
			if (instance.IdealDeckCard == null)
			{
				instance.IdealDeckCard = new List<DeckCardData>();
			}
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
					if (num <= 18)
					{
						if (num == 8)
						{
							instance.PlayerId = (long)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 18)
						{
							instance.PlayerDeckCard.Add(DeckCardData.DeserializeLengthDelimited(stream));
							continue;
						}
					}
					else
					{
						if (num == 26)
						{
							instance.IdealDeckCard.Add(DeckCardData.DeserializeLengthDelimited(stream));
							continue;
						}
						if (num == 32)
						{
							instance.DeckId = (long)ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x060000DD RID: 221 RVA: 0x00004AAC File Offset: 0x00002CAC
		public void Serialize(Stream stream)
		{
			HSCachedDeckCompletionResponse.Serialize(stream, this);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00004AB8 File Offset: 0x00002CB8
		public static void Serialize(Stream stream, HSCachedDeckCompletionResponse instance)
		{
			if (instance.HasPlayerId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.PlayerId);
			}
			if (instance.PlayerDeckCard.Count > 0)
			{
				foreach (DeckCardData deckCardData in instance.PlayerDeckCard)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, deckCardData.GetSerializedSize());
					DeckCardData.Serialize(stream, deckCardData);
				}
			}
			if (instance.IdealDeckCard.Count > 0)
			{
				foreach (DeckCardData deckCardData2 in instance.IdealDeckCard)
				{
					stream.WriteByte(26);
					ProtocolParser.WriteUInt32(stream, deckCardData2.GetSerializedSize());
					DeckCardData.Serialize(stream, deckCardData2);
				}
			}
			if (instance.HasDeckId)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.DeckId);
			}
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00004BC8 File Offset: 0x00002DC8
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasPlayerId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.PlayerId);
			}
			if (this.PlayerDeckCard.Count > 0)
			{
				foreach (DeckCardData deckCardData in this.PlayerDeckCard)
				{
					num += 1U;
					uint serializedSize = deckCardData.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (this.IdealDeckCard.Count > 0)
			{
				foreach (DeckCardData deckCardData2 in this.IdealDeckCard)
				{
					num += 1U;
					uint serializedSize2 = deckCardData2.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			if (this.HasDeckId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.DeckId);
			}
			return num;
		}

		// Token: 0x0400003B RID: 59
		public bool HasPlayerId;

		// Token: 0x0400003C RID: 60
		private long _PlayerId;

		// Token: 0x0400003D RID: 61
		private List<DeckCardData> _PlayerDeckCard = new List<DeckCardData>();

		// Token: 0x0400003E RID: 62
		private List<DeckCardData> _IdealDeckCard = new List<DeckCardData>();

		// Token: 0x0400003F RID: 63
		public bool HasDeckId;

		// Token: 0x04000040 RID: 64
		private long _DeckId;
	}
}
