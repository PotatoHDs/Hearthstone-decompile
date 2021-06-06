using System;
using System.IO;

namespace bnet.protocol.channel.v1
{
	// Token: 0x020004B2 RID: 1202
	public class RevokeInvitationRequest : IProtoBuf
	{
		// Token: 0x17000FC3 RID: 4035
		// (get) Token: 0x060053E4 RID: 21476 RVA: 0x00102741 File Offset: 0x00100941
		// (set) Token: 0x060053E5 RID: 21477 RVA: 0x00102749 File Offset: 0x00100949
		public ulong InvitationId { get; set; }

		// Token: 0x060053E6 RID: 21478 RVA: 0x00102752 File Offset: 0x00100952
		public void SetInvitationId(ulong val)
		{
			this.InvitationId = val;
		}

		// Token: 0x17000FC4 RID: 4036
		// (get) Token: 0x060053E7 RID: 21479 RVA: 0x0010275B File Offset: 0x0010095B
		// (set) Token: 0x060053E8 RID: 21480 RVA: 0x00102763 File Offset: 0x00100963
		public EntityId ChannelId { get; set; }

		// Token: 0x060053E9 RID: 21481 RVA: 0x0010276C File Offset: 0x0010096C
		public void SetChannelId(EntityId val)
		{
			this.ChannelId = val;
		}

		// Token: 0x060053EA RID: 21482 RVA: 0x00102778 File Offset: 0x00100978
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.InvitationId.GetHashCode() ^ this.ChannelId.GetHashCode();
		}

		// Token: 0x060053EB RID: 21483 RVA: 0x001027AC File Offset: 0x001009AC
		public override bool Equals(object obj)
		{
			RevokeInvitationRequest revokeInvitationRequest = obj as RevokeInvitationRequest;
			return revokeInvitationRequest != null && this.InvitationId.Equals(revokeInvitationRequest.InvitationId) && this.ChannelId.Equals(revokeInvitationRequest.ChannelId);
		}

		// Token: 0x17000FC5 RID: 4037
		// (get) Token: 0x060053EC RID: 21484 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060053ED RID: 21485 RVA: 0x001027F3 File Offset: 0x001009F3
		public static RevokeInvitationRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<RevokeInvitationRequest>(bs, 0, -1);
		}

		// Token: 0x060053EE RID: 21486 RVA: 0x001027FD File Offset: 0x001009FD
		public void Deserialize(Stream stream)
		{
			RevokeInvitationRequest.Deserialize(stream, this);
		}

		// Token: 0x060053EF RID: 21487 RVA: 0x00102807 File Offset: 0x00100A07
		public static RevokeInvitationRequest Deserialize(Stream stream, RevokeInvitationRequest instance)
		{
			return RevokeInvitationRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060053F0 RID: 21488 RVA: 0x00102814 File Offset: 0x00100A14
		public static RevokeInvitationRequest DeserializeLengthDelimited(Stream stream)
		{
			RevokeInvitationRequest revokeInvitationRequest = new RevokeInvitationRequest();
			RevokeInvitationRequest.DeserializeLengthDelimited(stream, revokeInvitationRequest);
			return revokeInvitationRequest;
		}

		// Token: 0x060053F1 RID: 21489 RVA: 0x00102830 File Offset: 0x00100A30
		public static RevokeInvitationRequest DeserializeLengthDelimited(Stream stream, RevokeInvitationRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return RevokeInvitationRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x060053F2 RID: 21490 RVA: 0x00102858 File Offset: 0x00100A58
		public static RevokeInvitationRequest Deserialize(Stream stream, RevokeInvitationRequest instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
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
				else if (num != 25)
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
					else if (instance.ChannelId == null)
					{
						instance.ChannelId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.ChannelId);
					}
				}
				else
				{
					instance.InvitationId = binaryReader.ReadUInt64();
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060053F3 RID: 21491 RVA: 0x00102911 File Offset: 0x00100B11
		public void Serialize(Stream stream)
		{
			RevokeInvitationRequest.Serialize(stream, this);
		}

		// Token: 0x060053F4 RID: 21492 RVA: 0x0010291C File Offset: 0x00100B1C
		public static void Serialize(Stream stream, RevokeInvitationRequest instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			stream.WriteByte(25);
			binaryWriter.Write(instance.InvitationId);
			if (instance.ChannelId == null)
			{
				throw new ArgumentNullException("ChannelId", "Required by proto specification.");
			}
			stream.WriteByte(34);
			ProtocolParser.WriteUInt32(stream, instance.ChannelId.GetSerializedSize());
			EntityId.Serialize(stream, instance.ChannelId);
		}

		// Token: 0x060053F5 RID: 21493 RVA: 0x00102980 File Offset: 0x00100B80
		public uint GetSerializedSize()
		{
			uint num = 0U + 8U;
			uint serializedSize = this.ChannelId.GetSerializedSize();
			return num + (serializedSize + ProtocolParser.SizeOfUInt32(serializedSize)) + 2U;
		}
	}
}
