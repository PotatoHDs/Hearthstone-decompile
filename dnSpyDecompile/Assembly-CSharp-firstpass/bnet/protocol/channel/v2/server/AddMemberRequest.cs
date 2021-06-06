using System;
using System.IO;
using bnet.protocol.channel.v1;

namespace bnet.protocol.channel.v2.server
{
	// Token: 0x0200049E RID: 1182
	public class AddMemberRequest : IProtoBuf
	{
		// Token: 0x17000F85 RID: 3973
		// (get) Token: 0x06005262 RID: 21090 RVA: 0x000FED6A File Offset: 0x000FCF6A
		// (set) Token: 0x06005263 RID: 21091 RVA: 0x000FED72 File Offset: 0x000FCF72
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

		// Token: 0x06005264 RID: 21092 RVA: 0x000FED85 File Offset: 0x000FCF85
		public void SetChannelId(ChannelId val)
		{
			this.ChannelId = val;
		}

		// Token: 0x17000F86 RID: 3974
		// (get) Token: 0x06005265 RID: 21093 RVA: 0x000FED8E File Offset: 0x000FCF8E
		// (set) Token: 0x06005266 RID: 21094 RVA: 0x000FED96 File Offset: 0x000FCF96
		public CreateMemberOptions Member
		{
			get
			{
				return this._Member;
			}
			set
			{
				this._Member = value;
				this.HasMember = (value != null);
			}
		}

		// Token: 0x06005267 RID: 21095 RVA: 0x000FEDA9 File Offset: 0x000FCFA9
		public void SetMember(CreateMemberOptions val)
		{
			this.Member = val;
		}

		// Token: 0x06005268 RID: 21096 RVA: 0x000FEDB4 File Offset: 0x000FCFB4
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasChannelId)
			{
				num ^= this.ChannelId.GetHashCode();
			}
			if (this.HasMember)
			{
				num ^= this.Member.GetHashCode();
			}
			return num;
		}

		// Token: 0x06005269 RID: 21097 RVA: 0x000FEDFC File Offset: 0x000FCFFC
		public override bool Equals(object obj)
		{
			AddMemberRequest addMemberRequest = obj as AddMemberRequest;
			return addMemberRequest != null && this.HasChannelId == addMemberRequest.HasChannelId && (!this.HasChannelId || this.ChannelId.Equals(addMemberRequest.ChannelId)) && this.HasMember == addMemberRequest.HasMember && (!this.HasMember || this.Member.Equals(addMemberRequest.Member));
		}

		// Token: 0x17000F87 RID: 3975
		// (get) Token: 0x0600526A RID: 21098 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600526B RID: 21099 RVA: 0x000FEE6C File Offset: 0x000FD06C
		public static AddMemberRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<AddMemberRequest>(bs, 0, -1);
		}

		// Token: 0x0600526C RID: 21100 RVA: 0x000FEE76 File Offset: 0x000FD076
		public void Deserialize(Stream stream)
		{
			AddMemberRequest.Deserialize(stream, this);
		}

		// Token: 0x0600526D RID: 21101 RVA: 0x000FEE80 File Offset: 0x000FD080
		public static AddMemberRequest Deserialize(Stream stream, AddMemberRequest instance)
		{
			return AddMemberRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600526E RID: 21102 RVA: 0x000FEE8C File Offset: 0x000FD08C
		public static AddMemberRequest DeserializeLengthDelimited(Stream stream)
		{
			AddMemberRequest addMemberRequest = new AddMemberRequest();
			AddMemberRequest.DeserializeLengthDelimited(stream, addMemberRequest);
			return addMemberRequest;
		}

		// Token: 0x0600526F RID: 21103 RVA: 0x000FEEA8 File Offset: 0x000FD0A8
		public static AddMemberRequest DeserializeLengthDelimited(Stream stream, AddMemberRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AddMemberRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06005270 RID: 21104 RVA: 0x000FEED0 File Offset: 0x000FD0D0
		public static AddMemberRequest Deserialize(Stream stream, AddMemberRequest instance, long limit)
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
						Key key = ProtocolParser.ReadKey((byte)num, stream);
						if (key.Field == 0U)
						{
							throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
						}
						ProtocolParser.SkipKey(stream, key);
					}
					else if (instance.Member == null)
					{
						instance.Member = CreateMemberOptions.DeserializeLengthDelimited(stream);
					}
					else
					{
						CreateMemberOptions.DeserializeLengthDelimited(stream, instance.Member);
					}
				}
				else if (instance.ChannelId == null)
				{
					instance.ChannelId = ChannelId.DeserializeLengthDelimited(stream);
				}
				else
				{
					ChannelId.DeserializeLengthDelimited(stream, instance.ChannelId);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06005271 RID: 21105 RVA: 0x000FEFA2 File Offset: 0x000FD1A2
		public void Serialize(Stream stream)
		{
			AddMemberRequest.Serialize(stream, this);
		}

		// Token: 0x06005272 RID: 21106 RVA: 0x000FEFAC File Offset: 0x000FD1AC
		public static void Serialize(Stream stream, AddMemberRequest instance)
		{
			if (instance.HasChannelId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.ChannelId.GetSerializedSize());
				ChannelId.Serialize(stream, instance.ChannelId);
			}
			if (instance.HasMember)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.Member.GetSerializedSize());
				CreateMemberOptions.Serialize(stream, instance.Member);
			}
		}

		// Token: 0x06005273 RID: 21107 RVA: 0x000FF014 File Offset: 0x000FD214
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasChannelId)
			{
				num += 1U;
				uint serializedSize = this.ChannelId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasMember)
			{
				num += 1U;
				uint serializedSize2 = this.Member.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num;
		}

		// Token: 0x04001A62 RID: 6754
		public bool HasChannelId;

		// Token: 0x04001A63 RID: 6755
		private ChannelId _ChannelId;

		// Token: 0x04001A64 RID: 6756
		public bool HasMember;

		// Token: 0x04001A65 RID: 6757
		private CreateMemberOptions _Member;
	}
}
