using System;
using System.IO;

namespace bnet.protocol.channel.v1
{
	// Token: 0x020004BF RID: 1215
	public class JoinChannelResponse : IProtoBuf
	{
		// Token: 0x17000FF7 RID: 4087
		// (get) Token: 0x06005500 RID: 21760 RVA: 0x001053D0 File Offset: 0x001035D0
		// (set) Token: 0x06005501 RID: 21761 RVA: 0x001053D8 File Offset: 0x001035D8
		public ulong ObjectId
		{
			get
			{
				return this._ObjectId;
			}
			set
			{
				this._ObjectId = value;
				this.HasObjectId = true;
			}
		}

		// Token: 0x06005502 RID: 21762 RVA: 0x001053E8 File Offset: 0x001035E8
		public void SetObjectId(ulong val)
		{
			this.ObjectId = val;
		}

		// Token: 0x17000FF8 RID: 4088
		// (get) Token: 0x06005503 RID: 21763 RVA: 0x001053F1 File Offset: 0x001035F1
		// (set) Token: 0x06005504 RID: 21764 RVA: 0x001053F9 File Offset: 0x001035F9
		public bool AlreadyMember
		{
			get
			{
				return this._AlreadyMember;
			}
			set
			{
				this._AlreadyMember = value;
				this.HasAlreadyMember = true;
			}
		}

		// Token: 0x06005505 RID: 21765 RVA: 0x00105409 File Offset: 0x00103609
		public void SetAlreadyMember(bool val)
		{
			this.AlreadyMember = val;
		}

		// Token: 0x17000FF9 RID: 4089
		// (get) Token: 0x06005506 RID: 21766 RVA: 0x00105412 File Offset: 0x00103612
		// (set) Token: 0x06005507 RID: 21767 RVA: 0x0010541A File Offset: 0x0010361A
		public EntityId MemberId
		{
			get
			{
				return this._MemberId;
			}
			set
			{
				this._MemberId = value;
				this.HasMemberId = (value != null);
			}
		}

		// Token: 0x06005508 RID: 21768 RVA: 0x0010542D File Offset: 0x0010362D
		public void SetMemberId(EntityId val)
		{
			this.MemberId = val;
		}

		// Token: 0x06005509 RID: 21769 RVA: 0x00105438 File Offset: 0x00103638
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasObjectId)
			{
				num ^= this.ObjectId.GetHashCode();
			}
			if (this.HasAlreadyMember)
			{
				num ^= this.AlreadyMember.GetHashCode();
			}
			if (this.HasMemberId)
			{
				num ^= this.MemberId.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600550A RID: 21770 RVA: 0x0010549C File Offset: 0x0010369C
		public override bool Equals(object obj)
		{
			JoinChannelResponse joinChannelResponse = obj as JoinChannelResponse;
			return joinChannelResponse != null && this.HasObjectId == joinChannelResponse.HasObjectId && (!this.HasObjectId || this.ObjectId.Equals(joinChannelResponse.ObjectId)) && this.HasAlreadyMember == joinChannelResponse.HasAlreadyMember && (!this.HasAlreadyMember || this.AlreadyMember.Equals(joinChannelResponse.AlreadyMember)) && this.HasMemberId == joinChannelResponse.HasMemberId && (!this.HasMemberId || this.MemberId.Equals(joinChannelResponse.MemberId));
		}

		// Token: 0x17000FFA RID: 4090
		// (get) Token: 0x0600550B RID: 21771 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600550C RID: 21772 RVA: 0x0010553D File Offset: 0x0010373D
		public static JoinChannelResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<JoinChannelResponse>(bs, 0, -1);
		}

		// Token: 0x0600550D RID: 21773 RVA: 0x00105547 File Offset: 0x00103747
		public void Deserialize(Stream stream)
		{
			JoinChannelResponse.Deserialize(stream, this);
		}

		// Token: 0x0600550E RID: 21774 RVA: 0x00105551 File Offset: 0x00103751
		public static JoinChannelResponse Deserialize(Stream stream, JoinChannelResponse instance)
		{
			return JoinChannelResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600550F RID: 21775 RVA: 0x0010555C File Offset: 0x0010375C
		public static JoinChannelResponse DeserializeLengthDelimited(Stream stream)
		{
			JoinChannelResponse joinChannelResponse = new JoinChannelResponse();
			JoinChannelResponse.DeserializeLengthDelimited(stream, joinChannelResponse);
			return joinChannelResponse;
		}

		// Token: 0x06005510 RID: 21776 RVA: 0x00105578 File Offset: 0x00103778
		public static JoinChannelResponse DeserializeLengthDelimited(Stream stream, JoinChannelResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return JoinChannelResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06005511 RID: 21777 RVA: 0x001055A0 File Offset: 0x001037A0
		public static JoinChannelResponse Deserialize(Stream stream, JoinChannelResponse instance, long limit)
		{
			instance.AlreadyMember = false;
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
					if (num != 32)
					{
						if (num != 42)
						{
							Key key = ProtocolParser.ReadKey((byte)num, stream);
							if (key.Field == 0U)
							{
								throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
							}
							ProtocolParser.SkipKey(stream, key);
						}
						else if (instance.MemberId == null)
						{
							instance.MemberId = EntityId.DeserializeLengthDelimited(stream);
						}
						else
						{
							EntityId.DeserializeLengthDelimited(stream, instance.MemberId);
						}
					}
					else
					{
						instance.AlreadyMember = ProtocolParser.ReadBool(stream);
					}
				}
				else
				{
					instance.ObjectId = ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06005512 RID: 21778 RVA: 0x00105671 File Offset: 0x00103871
		public void Serialize(Stream stream)
		{
			JoinChannelResponse.Serialize(stream, this);
		}

		// Token: 0x06005513 RID: 21779 RVA: 0x0010567C File Offset: 0x0010387C
		public static void Serialize(Stream stream, JoinChannelResponse instance)
		{
			if (instance.HasObjectId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, instance.ObjectId);
			}
			if (instance.HasAlreadyMember)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteBool(stream, instance.AlreadyMember);
			}
			if (instance.HasMemberId)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteUInt32(stream, instance.MemberId.GetSerializedSize());
				EntityId.Serialize(stream, instance.MemberId);
			}
		}

		// Token: 0x06005514 RID: 21780 RVA: 0x001056F0 File Offset: 0x001038F0
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasObjectId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.ObjectId);
			}
			if (this.HasAlreadyMember)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasMemberId)
			{
				num += 1U;
				uint serializedSize = this.MemberId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}

		// Token: 0x04001AE6 RID: 6886
		public bool HasObjectId;

		// Token: 0x04001AE7 RID: 6887
		private ulong _ObjectId;

		// Token: 0x04001AE8 RID: 6888
		public bool HasAlreadyMember;

		// Token: 0x04001AE9 RID: 6889
		private bool _AlreadyMember;

		// Token: 0x04001AEA RID: 6890
		public bool HasMemberId;

		// Token: 0x04001AEB RID: 6891
		private EntityId _MemberId;
	}
}
