using System;
using System.IO;
using System.Text;

namespace bnet.protocol.channel.v1
{
	// Token: 0x020004BA RID: 1210
	public class InvitationSuggestion : IProtoBuf
	{
		// Token: 0x17000FE1 RID: 4065
		// (get) Token: 0x0600548C RID: 21644 RVA: 0x0010402D File Offset: 0x0010222D
		// (set) Token: 0x0600548D RID: 21645 RVA: 0x00104035 File Offset: 0x00102235
		public EntityId ChannelId
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

		// Token: 0x0600548E RID: 21646 RVA: 0x00104048 File Offset: 0x00102248
		public void SetChannelId(EntityId val)
		{
			this.ChannelId = val;
		}

		// Token: 0x17000FE2 RID: 4066
		// (get) Token: 0x0600548F RID: 21647 RVA: 0x00104051 File Offset: 0x00102251
		// (set) Token: 0x06005490 RID: 21648 RVA: 0x00104059 File Offset: 0x00102259
		public EntityId SuggesterId { get; set; }

		// Token: 0x06005491 RID: 21649 RVA: 0x00104062 File Offset: 0x00102262
		public void SetSuggesterId(EntityId val)
		{
			this.SuggesterId = val;
		}

		// Token: 0x17000FE3 RID: 4067
		// (get) Token: 0x06005492 RID: 21650 RVA: 0x0010406B File Offset: 0x0010226B
		// (set) Token: 0x06005493 RID: 21651 RVA: 0x00104073 File Offset: 0x00102273
		public EntityId SuggesteeId { get; set; }

		// Token: 0x06005494 RID: 21652 RVA: 0x0010407C File Offset: 0x0010227C
		public void SetSuggesteeId(EntityId val)
		{
			this.SuggesteeId = val;
		}

		// Token: 0x17000FE4 RID: 4068
		// (get) Token: 0x06005495 RID: 21653 RVA: 0x00104085 File Offset: 0x00102285
		// (set) Token: 0x06005496 RID: 21654 RVA: 0x0010408D File Offset: 0x0010228D
		public string SuggesterName
		{
			get
			{
				return this._SuggesterName;
			}
			set
			{
				this._SuggesterName = value;
				this.HasSuggesterName = (value != null);
			}
		}

		// Token: 0x06005497 RID: 21655 RVA: 0x001040A0 File Offset: 0x001022A0
		public void SetSuggesterName(string val)
		{
			this.SuggesterName = val;
		}

		// Token: 0x17000FE5 RID: 4069
		// (get) Token: 0x06005498 RID: 21656 RVA: 0x001040A9 File Offset: 0x001022A9
		// (set) Token: 0x06005499 RID: 21657 RVA: 0x001040B1 File Offset: 0x001022B1
		public string SuggesteeName
		{
			get
			{
				return this._SuggesteeName;
			}
			set
			{
				this._SuggesteeName = value;
				this.HasSuggesteeName = (value != null);
			}
		}

		// Token: 0x0600549A RID: 21658 RVA: 0x001040C4 File Offset: 0x001022C4
		public void SetSuggesteeName(string val)
		{
			this.SuggesteeName = val;
		}

		// Token: 0x0600549B RID: 21659 RVA: 0x001040D0 File Offset: 0x001022D0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasChannelId)
			{
				num ^= this.ChannelId.GetHashCode();
			}
			num ^= this.SuggesterId.GetHashCode();
			num ^= this.SuggesteeId.GetHashCode();
			if (this.HasSuggesterName)
			{
				num ^= this.SuggesterName.GetHashCode();
			}
			if (this.HasSuggesteeName)
			{
				num ^= this.SuggesteeName.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600549C RID: 21660 RVA: 0x00104148 File Offset: 0x00102348
		public override bool Equals(object obj)
		{
			InvitationSuggestion invitationSuggestion = obj as InvitationSuggestion;
			return invitationSuggestion != null && this.HasChannelId == invitationSuggestion.HasChannelId && (!this.HasChannelId || this.ChannelId.Equals(invitationSuggestion.ChannelId)) && this.SuggesterId.Equals(invitationSuggestion.SuggesterId) && this.SuggesteeId.Equals(invitationSuggestion.SuggesteeId) && this.HasSuggesterName == invitationSuggestion.HasSuggesterName && (!this.HasSuggesterName || this.SuggesterName.Equals(invitationSuggestion.SuggesterName)) && this.HasSuggesteeName == invitationSuggestion.HasSuggesteeName && (!this.HasSuggesteeName || this.SuggesteeName.Equals(invitationSuggestion.SuggesteeName));
		}

		// Token: 0x17000FE6 RID: 4070
		// (get) Token: 0x0600549D RID: 21661 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600549E RID: 21662 RVA: 0x0010420D File Offset: 0x0010240D
		public static InvitationSuggestion ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<InvitationSuggestion>(bs, 0, -1);
		}

		// Token: 0x0600549F RID: 21663 RVA: 0x00104217 File Offset: 0x00102417
		public void Deserialize(Stream stream)
		{
			InvitationSuggestion.Deserialize(stream, this);
		}

		// Token: 0x060054A0 RID: 21664 RVA: 0x00104221 File Offset: 0x00102421
		public static InvitationSuggestion Deserialize(Stream stream, InvitationSuggestion instance)
		{
			return InvitationSuggestion.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060054A1 RID: 21665 RVA: 0x0010422C File Offset: 0x0010242C
		public static InvitationSuggestion DeserializeLengthDelimited(Stream stream)
		{
			InvitationSuggestion invitationSuggestion = new InvitationSuggestion();
			InvitationSuggestion.DeserializeLengthDelimited(stream, invitationSuggestion);
			return invitationSuggestion;
		}

		// Token: 0x060054A2 RID: 21666 RVA: 0x00104248 File Offset: 0x00102448
		public static InvitationSuggestion DeserializeLengthDelimited(Stream stream, InvitationSuggestion instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return InvitationSuggestion.Deserialize(stream, instance, num);
		}

		// Token: 0x060054A3 RID: 21667 RVA: 0x00104270 File Offset: 0x00102470
		public static InvitationSuggestion Deserialize(Stream stream, InvitationSuggestion instance, long limit)
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
					if (num <= 18)
					{
						if (num != 10)
						{
							if (num == 18)
							{
								if (instance.SuggesterId == null)
								{
									instance.SuggesterId = EntityId.DeserializeLengthDelimited(stream);
									continue;
								}
								EntityId.DeserializeLengthDelimited(stream, instance.SuggesterId);
								continue;
							}
						}
						else
						{
							if (instance.ChannelId == null)
							{
								instance.ChannelId = EntityId.DeserializeLengthDelimited(stream);
								continue;
							}
							EntityId.DeserializeLengthDelimited(stream, instance.ChannelId);
							continue;
						}
					}
					else if (num != 26)
					{
						if (num == 34)
						{
							instance.SuggesterName = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 42)
						{
							instance.SuggesteeName = ProtocolParser.ReadString(stream);
							continue;
						}
					}
					else
					{
						if (instance.SuggesteeId == null)
						{
							instance.SuggesteeId = EntityId.DeserializeLengthDelimited(stream);
							continue;
						}
						EntityId.DeserializeLengthDelimited(stream, instance.SuggesteeId);
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

		// Token: 0x060054A4 RID: 21668 RVA: 0x001043B7 File Offset: 0x001025B7
		public void Serialize(Stream stream)
		{
			InvitationSuggestion.Serialize(stream, this);
		}

		// Token: 0x060054A5 RID: 21669 RVA: 0x001043C0 File Offset: 0x001025C0
		public static void Serialize(Stream stream, InvitationSuggestion instance)
		{
			if (instance.HasChannelId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.ChannelId.GetSerializedSize());
				EntityId.Serialize(stream, instance.ChannelId);
			}
			if (instance.SuggesterId == null)
			{
				throw new ArgumentNullException("SuggesterId", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteUInt32(stream, instance.SuggesterId.GetSerializedSize());
			EntityId.Serialize(stream, instance.SuggesterId);
			if (instance.SuggesteeId == null)
			{
				throw new ArgumentNullException("SuggesteeId", "Required by proto specification.");
			}
			stream.WriteByte(26);
			ProtocolParser.WriteUInt32(stream, instance.SuggesteeId.GetSerializedSize());
			EntityId.Serialize(stream, instance.SuggesteeId);
			if (instance.HasSuggesterName)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.SuggesterName));
			}
			if (instance.HasSuggesteeName)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.SuggesteeName));
			}
		}

		// Token: 0x060054A6 RID: 21670 RVA: 0x001044C0 File Offset: 0x001026C0
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasChannelId)
			{
				num += 1U;
				uint serializedSize = this.ChannelId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			uint serializedSize2 = this.SuggesterId.GetSerializedSize();
			num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			uint serializedSize3 = this.SuggesteeId.GetSerializedSize();
			num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			if (this.HasSuggesterName)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.SuggesterName);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasSuggesteeName)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.SuggesteeName);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			return num + 2U;
		}

		// Token: 0x04001AC9 RID: 6857
		public bool HasChannelId;

		// Token: 0x04001ACA RID: 6858
		private EntityId _ChannelId;

		// Token: 0x04001ACD RID: 6861
		public bool HasSuggesterName;

		// Token: 0x04001ACE RID: 6862
		private string _SuggesterName;

		// Token: 0x04001ACF RID: 6863
		public bool HasSuggesteeName;

		// Token: 0x04001AD0 RID: 6864
		private string _SuggesteeName;
	}
}
