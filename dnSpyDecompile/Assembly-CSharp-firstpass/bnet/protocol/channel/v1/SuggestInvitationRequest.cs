using System;
using System.IO;

namespace bnet.protocol.channel.v1
{
	// Token: 0x020004B1 RID: 1201
	public class SuggestInvitationRequest : IProtoBuf
	{
		// Token: 0x17000FBF RID: 4031
		// (get) Token: 0x060053CE RID: 21454 RVA: 0x001023A4 File Offset: 0x001005A4
		// (set) Token: 0x060053CF RID: 21455 RVA: 0x001023AC File Offset: 0x001005AC
		public EntityId ChannelId { get; set; }

		// Token: 0x060053D0 RID: 21456 RVA: 0x001023B5 File Offset: 0x001005B5
		public void SetChannelId(EntityId val)
		{
			this.ChannelId = val;
		}

		// Token: 0x17000FC0 RID: 4032
		// (get) Token: 0x060053D1 RID: 21457 RVA: 0x001023BE File Offset: 0x001005BE
		// (set) Token: 0x060053D2 RID: 21458 RVA: 0x001023C6 File Offset: 0x001005C6
		public EntityId TargetId { get; set; }

		// Token: 0x060053D3 RID: 21459 RVA: 0x001023CF File Offset: 0x001005CF
		public void SetTargetId(EntityId val)
		{
			this.TargetId = val;
		}

		// Token: 0x17000FC1 RID: 4033
		// (get) Token: 0x060053D4 RID: 21460 RVA: 0x001023D8 File Offset: 0x001005D8
		// (set) Token: 0x060053D5 RID: 21461 RVA: 0x001023E0 File Offset: 0x001005E0
		public EntityId ApprovalId
		{
			get
			{
				return this._ApprovalId;
			}
			set
			{
				this._ApprovalId = value;
				this.HasApprovalId = (value != null);
			}
		}

		// Token: 0x060053D6 RID: 21462 RVA: 0x001023F3 File Offset: 0x001005F3
		public void SetApprovalId(EntityId val)
		{
			this.ApprovalId = val;
		}

		// Token: 0x060053D7 RID: 21463 RVA: 0x001023FC File Offset: 0x001005FC
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.ChannelId.GetHashCode();
			num ^= this.TargetId.GetHashCode();
			if (this.HasApprovalId)
			{
				num ^= this.ApprovalId.GetHashCode();
			}
			return num;
		}

		// Token: 0x060053D8 RID: 21464 RVA: 0x00102448 File Offset: 0x00100648
		public override bool Equals(object obj)
		{
			SuggestInvitationRequest suggestInvitationRequest = obj as SuggestInvitationRequest;
			return suggestInvitationRequest != null && this.ChannelId.Equals(suggestInvitationRequest.ChannelId) && this.TargetId.Equals(suggestInvitationRequest.TargetId) && this.HasApprovalId == suggestInvitationRequest.HasApprovalId && (!this.HasApprovalId || this.ApprovalId.Equals(suggestInvitationRequest.ApprovalId));
		}

		// Token: 0x17000FC2 RID: 4034
		// (get) Token: 0x060053D9 RID: 21465 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060053DA RID: 21466 RVA: 0x001024B7 File Offset: 0x001006B7
		public static SuggestInvitationRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SuggestInvitationRequest>(bs, 0, -1);
		}

		// Token: 0x060053DB RID: 21467 RVA: 0x001024C1 File Offset: 0x001006C1
		public void Deserialize(Stream stream)
		{
			SuggestInvitationRequest.Deserialize(stream, this);
		}

		// Token: 0x060053DC RID: 21468 RVA: 0x001024CB File Offset: 0x001006CB
		public static SuggestInvitationRequest Deserialize(Stream stream, SuggestInvitationRequest instance)
		{
			return SuggestInvitationRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060053DD RID: 21469 RVA: 0x001024D8 File Offset: 0x001006D8
		public static SuggestInvitationRequest DeserializeLengthDelimited(Stream stream)
		{
			SuggestInvitationRequest suggestInvitationRequest = new SuggestInvitationRequest();
			SuggestInvitationRequest.DeserializeLengthDelimited(stream, suggestInvitationRequest);
			return suggestInvitationRequest;
		}

		// Token: 0x060053DE RID: 21470 RVA: 0x001024F4 File Offset: 0x001006F4
		public static SuggestInvitationRequest DeserializeLengthDelimited(Stream stream, SuggestInvitationRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SuggestInvitationRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x060053DF RID: 21471 RVA: 0x0010251C File Offset: 0x0010071C
		public static SuggestInvitationRequest Deserialize(Stream stream, SuggestInvitationRequest instance, long limit)
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
				else if (num != 18)
				{
					if (num != 26)
					{
						if (num != 34)
						{
							Key key = ProtocolParser.ReadKey((byte)num, stream);
							if (key.Field == 0U)
							{
								throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
							}
							ProtocolParser.SkipKey(stream, key);
						}
						else if (instance.ApprovalId == null)
						{
							instance.ApprovalId = EntityId.DeserializeLengthDelimited(stream);
						}
						else
						{
							EntityId.DeserializeLengthDelimited(stream, instance.ApprovalId);
						}
					}
					else if (instance.TargetId == null)
					{
						instance.TargetId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.TargetId);
					}
				}
				else if (instance.ChannelId == null)
				{
					instance.ChannelId = EntityId.DeserializeLengthDelimited(stream);
				}
				else
				{
					EntityId.DeserializeLengthDelimited(stream, instance.ChannelId);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060053E0 RID: 21472 RVA: 0x0010261E File Offset: 0x0010081E
		public void Serialize(Stream stream)
		{
			SuggestInvitationRequest.Serialize(stream, this);
		}

		// Token: 0x060053E1 RID: 21473 RVA: 0x00102628 File Offset: 0x00100828
		public static void Serialize(Stream stream, SuggestInvitationRequest instance)
		{
			if (instance.ChannelId == null)
			{
				throw new ArgumentNullException("ChannelId", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteUInt32(stream, instance.ChannelId.GetSerializedSize());
			EntityId.Serialize(stream, instance.ChannelId);
			if (instance.TargetId == null)
			{
				throw new ArgumentNullException("TargetId", "Required by proto specification.");
			}
			stream.WriteByte(26);
			ProtocolParser.WriteUInt32(stream, instance.TargetId.GetSerializedSize());
			EntityId.Serialize(stream, instance.TargetId);
			if (instance.HasApprovalId)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.ApprovalId.GetSerializedSize());
				EntityId.Serialize(stream, instance.ApprovalId);
			}
		}

		// Token: 0x060053E2 RID: 21474 RVA: 0x001026DC File Offset: 0x001008DC
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint serializedSize = this.ChannelId.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			uint serializedSize2 = this.TargetId.GetSerializedSize();
			num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			if (this.HasApprovalId)
			{
				num += 1U;
				uint serializedSize3 = this.ApprovalId.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			return num + 2U;
		}

		// Token: 0x04001AAB RID: 6827
		public bool HasApprovalId;

		// Token: 0x04001AAC RID: 6828
		private EntityId _ApprovalId;
	}
}
