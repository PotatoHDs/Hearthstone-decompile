using System;
using System.IO;

namespace bnet.protocol.channel.v1
{
	// Token: 0x020004AC RID: 1196
	public class SendInvitationRequest : IProtoBuf
	{
		// Token: 0x17000FB1 RID: 4017
		// (get) Token: 0x06005372 RID: 21362 RVA: 0x0010186D File Offset: 0x000FFA6D
		// (set) Token: 0x06005373 RID: 21363 RVA: 0x00101875 File Offset: 0x000FFA75
		public EntityId TargetId { get; set; }

		// Token: 0x06005374 RID: 21364 RVA: 0x0010187E File Offset: 0x000FFA7E
		public void SetTargetId(EntityId val)
		{
			this.TargetId = val;
		}

		// Token: 0x17000FB2 RID: 4018
		// (get) Token: 0x06005375 RID: 21365 RVA: 0x00101887 File Offset: 0x000FFA87
		// (set) Token: 0x06005376 RID: 21366 RVA: 0x0010188F File Offset: 0x000FFA8F
		public InvitationParams Params { get; set; }

		// Token: 0x06005377 RID: 21367 RVA: 0x00101898 File Offset: 0x000FFA98
		public void SetParams(InvitationParams val)
		{
			this.Params = val;
		}

		// Token: 0x06005378 RID: 21368 RVA: 0x001018A1 File Offset: 0x000FFAA1
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.TargetId.GetHashCode() ^ this.Params.GetHashCode();
		}

		// Token: 0x06005379 RID: 21369 RVA: 0x001018C8 File Offset: 0x000FFAC8
		public override bool Equals(object obj)
		{
			SendInvitationRequest sendInvitationRequest = obj as SendInvitationRequest;
			return sendInvitationRequest != null && this.TargetId.Equals(sendInvitationRequest.TargetId) && this.Params.Equals(sendInvitationRequest.Params);
		}

		// Token: 0x17000FB3 RID: 4019
		// (get) Token: 0x0600537A RID: 21370 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600537B RID: 21371 RVA: 0x0010190C File Offset: 0x000FFB0C
		public static SendInvitationRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SendInvitationRequest>(bs, 0, -1);
		}

		// Token: 0x0600537C RID: 21372 RVA: 0x00101916 File Offset: 0x000FFB16
		public void Deserialize(Stream stream)
		{
			SendInvitationRequest.Deserialize(stream, this);
		}

		// Token: 0x0600537D RID: 21373 RVA: 0x00101920 File Offset: 0x000FFB20
		public static SendInvitationRequest Deserialize(Stream stream, SendInvitationRequest instance)
		{
			return SendInvitationRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600537E RID: 21374 RVA: 0x0010192C File Offset: 0x000FFB2C
		public static SendInvitationRequest DeserializeLengthDelimited(Stream stream)
		{
			SendInvitationRequest sendInvitationRequest = new SendInvitationRequest();
			SendInvitationRequest.DeserializeLengthDelimited(stream, sendInvitationRequest);
			return sendInvitationRequest;
		}

		// Token: 0x0600537F RID: 21375 RVA: 0x00101948 File Offset: 0x000FFB48
		public static SendInvitationRequest DeserializeLengthDelimited(Stream stream, SendInvitationRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SendInvitationRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06005380 RID: 21376 RVA: 0x00101970 File Offset: 0x000FFB70
		public static SendInvitationRequest Deserialize(Stream stream, SendInvitationRequest instance, long limit)
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
						Key key = ProtocolParser.ReadKey((byte)num, stream);
						if (key.Field == 0U)
						{
							throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
						}
						ProtocolParser.SkipKey(stream, key);
					}
					else if (instance.Params == null)
					{
						instance.Params = InvitationParams.DeserializeLengthDelimited(stream);
					}
					else
					{
						InvitationParams.DeserializeLengthDelimited(stream, instance.Params);
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
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06005381 RID: 21377 RVA: 0x00101A42 File Offset: 0x000FFC42
		public void Serialize(Stream stream)
		{
			SendInvitationRequest.Serialize(stream, this);
		}

		// Token: 0x06005382 RID: 21378 RVA: 0x00101A4C File Offset: 0x000FFC4C
		public static void Serialize(Stream stream, SendInvitationRequest instance)
		{
			if (instance.TargetId == null)
			{
				throw new ArgumentNullException("TargetId", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteUInt32(stream, instance.TargetId.GetSerializedSize());
			EntityId.Serialize(stream, instance.TargetId);
			if (instance.Params == null)
			{
				throw new ArgumentNullException("Params", "Required by proto specification.");
			}
			stream.WriteByte(26);
			ProtocolParser.WriteUInt32(stream, instance.Params.GetSerializedSize());
			InvitationParams.Serialize(stream, instance.Params);
		}

		// Token: 0x06005383 RID: 21379 RVA: 0x00101AD4 File Offset: 0x000FFCD4
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint serializedSize = this.TargetId.GetSerializedSize();
			uint num2 = num + (serializedSize + ProtocolParser.SizeOfUInt32(serializedSize));
			uint serializedSize2 = this.Params.GetSerializedSize();
			return num2 + (serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2)) + 2U;
		}
	}
}
