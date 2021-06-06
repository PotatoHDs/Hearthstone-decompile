using System;
using System.Collections.Generic;
using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	// Token: 0x020000A1 RID: 161
	public class GetDeckContentsResponse : IProtoBuf
	{
		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000AE3 RID: 2787 RVA: 0x00028C57 File Offset: 0x00026E57
		// (set) Token: 0x06000AE4 RID: 2788 RVA: 0x00028C5F File Offset: 0x00026E5F
		public long DeprecatedDeckId
		{
			get
			{
				return this._DeprecatedDeckId;
			}
			set
			{
				this._DeprecatedDeckId = value;
				this.HasDeprecatedDeckId = true;
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000AE5 RID: 2789 RVA: 0x00028C6F File Offset: 0x00026E6F
		// (set) Token: 0x06000AE6 RID: 2790 RVA: 0x00028C77 File Offset: 0x00026E77
		public List<DeckCardData> DeprecatedCards
		{
			get
			{
				return this._DeprecatedCards;
			}
			set
			{
				this._DeprecatedCards = value;
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06000AE7 RID: 2791 RVA: 0x00028C80 File Offset: 0x00026E80
		// (set) Token: 0x06000AE8 RID: 2792 RVA: 0x00028C88 File Offset: 0x00026E88
		public List<DeckContents> Decks
		{
			get
			{
				return this._Decks;
			}
			set
			{
				this._Decks = value;
			}
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x00028C94 File Offset: 0x00026E94
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasDeprecatedDeckId)
			{
				num ^= this.DeprecatedDeckId.GetHashCode();
			}
			foreach (DeckCardData deckCardData in this.DeprecatedCards)
			{
				num ^= deckCardData.GetHashCode();
			}
			foreach (DeckContents deckContents in this.Decks)
			{
				num ^= deckContents.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x00028D58 File Offset: 0x00026F58
		public override bool Equals(object obj)
		{
			GetDeckContentsResponse getDeckContentsResponse = obj as GetDeckContentsResponse;
			if (getDeckContentsResponse == null)
			{
				return false;
			}
			if (this.HasDeprecatedDeckId != getDeckContentsResponse.HasDeprecatedDeckId || (this.HasDeprecatedDeckId && !this.DeprecatedDeckId.Equals(getDeckContentsResponse.DeprecatedDeckId)))
			{
				return false;
			}
			if (this.DeprecatedCards.Count != getDeckContentsResponse.DeprecatedCards.Count)
			{
				return false;
			}
			for (int i = 0; i < this.DeprecatedCards.Count; i++)
			{
				if (!this.DeprecatedCards[i].Equals(getDeckContentsResponse.DeprecatedCards[i]))
				{
					return false;
				}
			}
			if (this.Decks.Count != getDeckContentsResponse.Decks.Count)
			{
				return false;
			}
			for (int j = 0; j < this.Decks.Count; j++)
			{
				if (!this.Decks[j].Equals(getDeckContentsResponse.Decks[j]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x00028E42 File Offset: 0x00027042
		public void Deserialize(Stream stream)
		{
			GetDeckContentsResponse.Deserialize(stream, this);
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x00028E4C File Offset: 0x0002704C
		public static GetDeckContentsResponse Deserialize(Stream stream, GetDeckContentsResponse instance)
		{
			return GetDeckContentsResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x00028E58 File Offset: 0x00027058
		public static GetDeckContentsResponse DeserializeLengthDelimited(Stream stream)
		{
			GetDeckContentsResponse getDeckContentsResponse = new GetDeckContentsResponse();
			GetDeckContentsResponse.DeserializeLengthDelimited(stream, getDeckContentsResponse);
			return getDeckContentsResponse;
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x00028E74 File Offset: 0x00027074
		public static GetDeckContentsResponse DeserializeLengthDelimited(Stream stream, GetDeckContentsResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetDeckContentsResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x00028E9C File Offset: 0x0002709C
		public static GetDeckContentsResponse Deserialize(Stream stream, GetDeckContentsResponse instance, long limit)
		{
			if (instance.DeprecatedCards == null)
			{
				instance.DeprecatedCards = new List<DeckCardData>();
			}
			if (instance.Decks == null)
			{
				instance.Decks = new List<DeckContents>();
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
				else if (num != 8)
				{
					if (num != 18)
					{
						if (num != 26)
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
							instance.Decks.Add(DeckContents.DeserializeLengthDelimited(stream));
						}
					}
					else
					{
						instance.DeprecatedCards.Add(DeckCardData.DeserializeLengthDelimited(stream));
					}
				}
				else
				{
					instance.DeprecatedDeckId = (long)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06000AF0 RID: 2800 RVA: 0x00028F79 File Offset: 0x00027179
		public void Serialize(Stream stream)
		{
			GetDeckContentsResponse.Serialize(stream, this);
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x00028F84 File Offset: 0x00027184
		public static void Serialize(Stream stream, GetDeckContentsResponse instance)
		{
			if (instance.HasDeprecatedDeckId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.DeprecatedDeckId);
			}
			if (instance.DeprecatedCards.Count > 0)
			{
				foreach (DeckCardData deckCardData in instance.DeprecatedCards)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, deckCardData.GetSerializedSize());
					DeckCardData.Serialize(stream, deckCardData);
				}
			}
			if (instance.Decks.Count > 0)
			{
				foreach (DeckContents deckContents in instance.Decks)
				{
					stream.WriteByte(26);
					ProtocolParser.WriteUInt32(stream, deckContents.GetSerializedSize());
					DeckContents.Serialize(stream, deckContents);
				}
			}
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x00029078 File Offset: 0x00027278
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasDeprecatedDeckId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.DeprecatedDeckId);
			}
			if (this.DeprecatedCards.Count > 0)
			{
				foreach (DeckCardData deckCardData in this.DeprecatedCards)
				{
					num += 1U;
					uint serializedSize = deckCardData.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (this.Decks.Count > 0)
			{
				foreach (DeckContents deckContents in this.Decks)
				{
					num += 1U;
					uint serializedSize2 = deckContents.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			return num;
		}

		// Token: 0x040003BD RID: 957
		public bool HasDeprecatedDeckId;

		// Token: 0x040003BE RID: 958
		private long _DeprecatedDeckId;

		// Token: 0x040003BF RID: 959
		private List<DeckCardData> _DeprecatedCards = new List<DeckCardData>();

		// Token: 0x040003C0 RID: 960
		private List<DeckContents> _Decks = new List<DeckContents>();

		// Token: 0x020005AB RID: 1451
		public enum PacketID
		{
			// Token: 0x04001F5C RID: 8028
			ID = 215
		}
	}
}
