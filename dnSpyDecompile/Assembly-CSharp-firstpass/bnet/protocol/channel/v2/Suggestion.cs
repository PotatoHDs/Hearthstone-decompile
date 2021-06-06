using System;
using System.IO;

namespace bnet.protocol.channel.v2
{
	// Token: 0x02000485 RID: 1157
	public class Suggestion : IProtoBuf
	{
		// Token: 0x17000F1F RID: 3871
		// (get) Token: 0x06005060 RID: 20576 RVA: 0x000F966D File Offset: 0x000F786D
		// (set) Token: 0x06005061 RID: 20577 RVA: 0x000F9675 File Offset: 0x000F7875
		public ChannelId ChannelId
		{
			get
			{
				return this._ChannelId;
			}
			set
			{
				this._ChannelId = value;
				this.HasChannelId = (value != null);
			}
		}

		// Token: 0x06005062 RID: 20578 RVA: 0x000F9688 File Offset: 0x000F7888
		public void SetChannelId(ChannelId val)
		{
			this.ChannelId = val;
		}

		// Token: 0x17000F20 RID: 3872
		// (get) Token: 0x06005063 RID: 20579 RVA: 0x000F9691 File Offset: 0x000F7891
		// (set) Token: 0x06005064 RID: 20580 RVA: 0x000F9699 File Offset: 0x000F7899
		public MemberDescription Suggester
		{
			get
			{
				return this._Suggester;
			}
			set
			{
				this._Suggester = value;
				this.HasSuggester = (value != null);
			}
		}

		// Token: 0x06005065 RID: 20581 RVA: 0x000F96AC File Offset: 0x000F78AC
		public void SetSuggester(MemberDescription val)
		{
			this.Suggester = val;
		}

		// Token: 0x17000F21 RID: 3873
		// (get) Token: 0x06005066 RID: 20582 RVA: 0x000F96B5 File Offset: 0x000F78B5
		// (set) Token: 0x06005067 RID: 20583 RVA: 0x000F96BD File Offset: 0x000F78BD
		public MemberDescription Suggestee
		{
			get
			{
				return this._Suggestee;
			}
			set
			{
				this._Suggestee = value;
				this.HasSuggestee = (value != null);
			}
		}

		// Token: 0x06005068 RID: 20584 RVA: 0x000F96D0 File Offset: 0x000F78D0
		public void SetSuggestee(MemberDescription val)
		{
			this.Suggestee = val;
		}

		// Token: 0x17000F22 RID: 3874
		// (get) Token: 0x06005069 RID: 20585 RVA: 0x000F96D9 File Offset: 0x000F78D9
		// (set) Token: 0x0600506A RID: 20586 RVA: 0x000F96E1 File Offset: 0x000F78E1
		public ulong CreationTime
		{
			get
			{
				return this._CreationTime;
			}
			set
			{
				this._CreationTime = value;
				this.HasCreationTime = true;
			}
		}

		// Token: 0x0600506B RID: 20587 RVA: 0x000F96F1 File Offset: 0x000F78F1
		public void SetCreationTime(ulong val)
		{
			this.CreationTime = val;
		}

		// Token: 0x0600506C RID: 20588 RVA: 0x000F96FC File Offset: 0x000F78FC
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasChannelId)
			{
				num ^= this.ChannelId.GetHashCode();
			}
			if (this.HasSuggester)
			{
				num ^= this.Suggester.GetHashCode();
			}
			if (this.HasSuggestee)
			{
				num ^= this.Suggestee.GetHashCode();
			}
			if (this.HasCreationTime)
			{
				num ^= this.CreationTime.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600506D RID: 20589 RVA: 0x000F9774 File Offset: 0x000F7974
		public override bool Equals(object obj)
		{
			Suggestion suggestion = obj as Suggestion;
			return suggestion != null && this.HasChannelId == suggestion.HasChannelId && (!this.HasChannelId || this.ChannelId.Equals(suggestion.ChannelId)) && this.HasSuggester == suggestion.HasSuggester && (!this.HasSuggester || this.Suggester.Equals(suggestion.Suggester)) && this.HasSuggestee == suggestion.HasSuggestee && (!this.HasSuggestee || this.Suggestee.Equals(suggestion.Suggestee)) && this.HasCreationTime == suggestion.HasCreationTime && (!this.HasCreationTime || this.CreationTime.Equals(suggestion.CreationTime));
		}

		// Token: 0x17000F23 RID: 3875
		// (get) Token: 0x0600506E RID: 20590 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600506F RID: 20591 RVA: 0x000F983D File Offset: 0x000F7A3D
		public static Suggestion ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<Suggestion>(bs, 0, -1);
		}

		// Token: 0x06005070 RID: 20592 RVA: 0x000F9847 File Offset: 0x000F7A47
		public void Deserialize(Stream stream)
		{
			Suggestion.Deserialize(stream, this);
		}

		// Token: 0x06005071 RID: 20593 RVA: 0x000F9851 File Offset: 0x000F7A51
		public static Suggestion Deserialize(Stream stream, Suggestion instance)
		{
			return Suggestion.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005072 RID: 20594 RVA: 0x000F985C File Offset: 0x000F7A5C
		public static Suggestion DeserializeLengthDelimited(Stream stream)
		{
			Suggestion suggestion = new Suggestion();
			Suggestion.DeserializeLengthDelimited(stream, suggestion);
			return suggestion;
		}

		// Token: 0x06005073 RID: 20595 RVA: 0x000F9878 File Offset: 0x000F7A78
		public static Suggestion DeserializeLengthDelimited(Stream stream, Suggestion instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return Suggestion.Deserialize(stream, instance, num);
		}

		// Token: 0x06005074 RID: 20596 RVA: 0x000F98A0 File Offset: 0x000F7AA0
		public static Suggestion Deserialize(Stream stream, Suggestion instance, long limit)
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
				else
				{
					if (num <= 26)
					{
						if (num != 18)
						{
							if (num == 26)
							{
								if (instance.Suggester == null)
								{
									instance.Suggester = MemberDescription.DeserializeLengthDelimited(stream);
									continue;
								}
								MemberDescription.DeserializeLengthDelimited(stream, instance.Suggester);
								continue;
							}
						}
						else
						{
							if (instance.ChannelId == null)
							{
								instance.ChannelId = ChannelId.DeserializeLengthDelimited(stream);
								continue;
							}
							ChannelId.DeserializeLengthDelimited(stream, instance.ChannelId);
							continue;
						}
					}
					else if (num != 34)
					{
						if (num == 56)
						{
							instance.CreationTime = ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					else
					{
						if (instance.Suggestee == null)
						{
							instance.Suggestee = MemberDescription.DeserializeLengthDelimited(stream);
							continue;
						}
						MemberDescription.DeserializeLengthDelimited(stream, instance.Suggestee);
						continue;
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

		// Token: 0x06005075 RID: 20597 RVA: 0x000F99CB File Offset: 0x000F7BCB
		public void Serialize(Stream stream)
		{
			Suggestion.Serialize(stream, this);
		}

		// Token: 0x06005076 RID: 20598 RVA: 0x000F99D4 File Offset: 0x000F7BD4
		public static void Serialize(Stream stream, Suggestion instance)
		{
			if (instance.HasChannelId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.ChannelId.GetSerializedSize());
				ChannelId.Serialize(stream, instance.ChannelId);
			}
			if (instance.HasSuggester)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.Suggester.GetSerializedSize());
				MemberDescription.Serialize(stream, instance.Suggester);
			}
			if (instance.HasSuggestee)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.Suggestee.GetSerializedSize());
				MemberDescription.Serialize(stream, instance.Suggestee);
			}
			if (instance.HasCreationTime)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteUInt64(stream, instance.CreationTime);
			}
		}

		// Token: 0x06005077 RID: 20599 RVA: 0x000F9A84 File Offset: 0x000F7C84
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasChannelId)
			{
				num += 1U;
				uint serializedSize = this.ChannelId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasSuggester)
			{
				num += 1U;
				uint serializedSize2 = this.Suggester.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasSuggestee)
			{
				num += 1U;
				uint serializedSize3 = this.Suggestee.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			if (this.HasCreationTime)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.CreationTime);
			}
			return num;
		}

		// Token: 0x040019EE RID: 6638
		public bool HasChannelId;

		// Token: 0x040019EF RID: 6639
		private ChannelId _ChannelId;

		// Token: 0x040019F0 RID: 6640
		public bool HasSuggester;

		// Token: 0x040019F1 RID: 6641
		private MemberDescription _Suggester;

		// Token: 0x040019F2 RID: 6642
		public bool HasSuggestee;

		// Token: 0x040019F3 RID: 6643
		private MemberDescription _Suggestee;

		// Token: 0x040019F4 RID: 6644
		public bool HasCreationTime;

		// Token: 0x040019F5 RID: 6645
		private ulong _CreationTime;
	}
}
