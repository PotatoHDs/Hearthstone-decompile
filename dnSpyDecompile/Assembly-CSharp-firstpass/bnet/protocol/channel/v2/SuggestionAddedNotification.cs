using System;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.channel.v2
{
	// Token: 0x0200046F RID: 1135
	public class SuggestionAddedNotification : IProtoBuf
	{
		// Token: 0x17000E98 RID: 3736
		// (get) Token: 0x06004E13 RID: 19987 RVA: 0x000F272C File Offset: 0x000F092C
		// (set) Token: 0x06004E14 RID: 19988 RVA: 0x000F2734 File Offset: 0x000F0934
		public GameAccountHandle AgentId
		{
			get
			{
				return this._AgentId;
			}
			set
			{
				this._AgentId = value;
				this.HasAgentId = (value != null);
			}
		}

		// Token: 0x06004E15 RID: 19989 RVA: 0x000F2747 File Offset: 0x000F0947
		public void SetAgentId(GameAccountHandle val)
		{
			this.AgentId = val;
		}

		// Token: 0x17000E99 RID: 3737
		// (get) Token: 0x06004E16 RID: 19990 RVA: 0x000F2750 File Offset: 0x000F0950
		// (set) Token: 0x06004E17 RID: 19991 RVA: 0x000F2758 File Offset: 0x000F0958
		public GameAccountHandle SubscriberId
		{
			get
			{
				return this._SubscriberId;
			}
			set
			{
				this._SubscriberId = value;
				this.HasSubscriberId = (value != null);
			}
		}

		// Token: 0x06004E18 RID: 19992 RVA: 0x000F276B File Offset: 0x000F096B
		public void SetSubscriberId(GameAccountHandle val)
		{
			this.SubscriberId = val;
		}

		// Token: 0x17000E9A RID: 3738
		// (get) Token: 0x06004E19 RID: 19993 RVA: 0x000F2774 File Offset: 0x000F0974
		// (set) Token: 0x06004E1A RID: 19994 RVA: 0x000F277C File Offset: 0x000F097C
		public Suggestion Suggestion
		{
			get
			{
				return this._Suggestion;
			}
			set
			{
				this._Suggestion = value;
				this.HasSuggestion = (value != null);
			}
		}

		// Token: 0x06004E1B RID: 19995 RVA: 0x000F278F File Offset: 0x000F098F
		public void SetSuggestion(Suggestion val)
		{
			this.Suggestion = val;
		}

		// Token: 0x06004E1C RID: 19996 RVA: 0x000F2798 File Offset: 0x000F0998
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAgentId)
			{
				num ^= this.AgentId.GetHashCode();
			}
			if (this.HasSubscriberId)
			{
				num ^= this.SubscriberId.GetHashCode();
			}
			if (this.HasSuggestion)
			{
				num ^= this.Suggestion.GetHashCode();
			}
			return num;
		}

		// Token: 0x06004E1D RID: 19997 RVA: 0x000F27F4 File Offset: 0x000F09F4
		public override bool Equals(object obj)
		{
			SuggestionAddedNotification suggestionAddedNotification = obj as SuggestionAddedNotification;
			return suggestionAddedNotification != null && this.HasAgentId == suggestionAddedNotification.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(suggestionAddedNotification.AgentId)) && this.HasSubscriberId == suggestionAddedNotification.HasSubscriberId && (!this.HasSubscriberId || this.SubscriberId.Equals(suggestionAddedNotification.SubscriberId)) && this.HasSuggestion == suggestionAddedNotification.HasSuggestion && (!this.HasSuggestion || this.Suggestion.Equals(suggestionAddedNotification.Suggestion));
		}

		// Token: 0x17000E9B RID: 3739
		// (get) Token: 0x06004E1E RID: 19998 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004E1F RID: 19999 RVA: 0x000F288F File Offset: 0x000F0A8F
		public static SuggestionAddedNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SuggestionAddedNotification>(bs, 0, -1);
		}

		// Token: 0x06004E20 RID: 20000 RVA: 0x000F2899 File Offset: 0x000F0A99
		public void Deserialize(Stream stream)
		{
			SuggestionAddedNotification.Deserialize(stream, this);
		}

		// Token: 0x06004E21 RID: 20001 RVA: 0x000F28A3 File Offset: 0x000F0AA3
		public static SuggestionAddedNotification Deserialize(Stream stream, SuggestionAddedNotification instance)
		{
			return SuggestionAddedNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004E22 RID: 20002 RVA: 0x000F28B0 File Offset: 0x000F0AB0
		public static SuggestionAddedNotification DeserializeLengthDelimited(Stream stream)
		{
			SuggestionAddedNotification suggestionAddedNotification = new SuggestionAddedNotification();
			SuggestionAddedNotification.DeserializeLengthDelimited(stream, suggestionAddedNotification);
			return suggestionAddedNotification;
		}

		// Token: 0x06004E23 RID: 20003 RVA: 0x000F28CC File Offset: 0x000F0ACC
		public static SuggestionAddedNotification DeserializeLengthDelimited(Stream stream, SuggestionAddedNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SuggestionAddedNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x06004E24 RID: 20004 RVA: 0x000F28F4 File Offset: 0x000F0AF4
		public static SuggestionAddedNotification Deserialize(Stream stream, SuggestionAddedNotification instance, long limit)
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
				else if (num != 10)
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
						else if (instance.Suggestion == null)
						{
							instance.Suggestion = Suggestion.DeserializeLengthDelimited(stream);
						}
						else
						{
							Suggestion.DeserializeLengthDelimited(stream, instance.Suggestion);
						}
					}
					else if (instance.SubscriberId == null)
					{
						instance.SubscriberId = GameAccountHandle.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameAccountHandle.DeserializeLengthDelimited(stream, instance.SubscriberId);
					}
				}
				else if (instance.AgentId == null)
				{
					instance.AgentId = GameAccountHandle.DeserializeLengthDelimited(stream);
				}
				else
				{
					GameAccountHandle.DeserializeLengthDelimited(stream, instance.AgentId);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06004E25 RID: 20005 RVA: 0x000F29F6 File Offset: 0x000F0BF6
		public void Serialize(Stream stream)
		{
			SuggestionAddedNotification.Serialize(stream, this);
		}

		// Token: 0x06004E26 RID: 20006 RVA: 0x000F2A00 File Offset: 0x000F0C00
		public static void Serialize(Stream stream, SuggestionAddedNotification instance)
		{
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.AgentId);
			}
			if (instance.HasSubscriberId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.SubscriberId.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.SubscriberId);
			}
			if (instance.HasSuggestion)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.Suggestion.GetSerializedSize());
				Suggestion.Serialize(stream, instance.Suggestion);
			}
		}

		// Token: 0x06004E27 RID: 20007 RVA: 0x000F2A94 File Offset: 0x000F0C94
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAgentId)
			{
				num += 1U;
				uint serializedSize = this.AgentId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasSubscriberId)
			{
				num += 1U;
				uint serializedSize2 = this.SubscriberId.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasSuggestion)
			{
				num += 1U;
				uint serializedSize3 = this.Suggestion.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			return num;
		}

		// Token: 0x04001966 RID: 6502
		public bool HasAgentId;

		// Token: 0x04001967 RID: 6503
		private GameAccountHandle _AgentId;

		// Token: 0x04001968 RID: 6504
		public bool HasSubscriberId;

		// Token: 0x04001969 RID: 6505
		private GameAccountHandle _SubscriberId;

		// Token: 0x0400196A RID: 6506
		public bool HasSuggestion;

		// Token: 0x0400196B RID: 6507
		private Suggestion _Suggestion;
	}
}
