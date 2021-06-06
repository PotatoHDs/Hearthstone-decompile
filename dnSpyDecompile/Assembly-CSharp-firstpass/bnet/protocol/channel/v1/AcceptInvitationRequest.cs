using System;
using System.IO;

namespace bnet.protocol.channel.v1
{
	// Token: 0x020004AD RID: 1197
	public class AcceptInvitationRequest : IProtoBuf
	{
		// Token: 0x17000FB4 RID: 4020
		// (get) Token: 0x06005385 RID: 21381 RVA: 0x00101B0E File Offset: 0x000FFD0E
		// (set) Token: 0x06005386 RID: 21382 RVA: 0x00101B16 File Offset: 0x000FFD16
		public ulong InvitationId { get; set; }

		// Token: 0x06005387 RID: 21383 RVA: 0x00101B1F File Offset: 0x000FFD1F
		public void SetInvitationId(ulong val)
		{
			this.InvitationId = val;
		}

		// Token: 0x17000FB5 RID: 4021
		// (get) Token: 0x06005388 RID: 21384 RVA: 0x00101B28 File Offset: 0x000FFD28
		// (set) Token: 0x06005389 RID: 21385 RVA: 0x00101B30 File Offset: 0x000FFD30
		public ulong ObjectId { get; set; }

		// Token: 0x0600538A RID: 21386 RVA: 0x00101B39 File Offset: 0x000FFD39
		public void SetObjectId(ulong val)
		{
			this.ObjectId = val;
		}

		// Token: 0x0600538B RID: 21387 RVA: 0x00101B44 File Offset: 0x000FFD44
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.InvitationId.GetHashCode() ^ this.ObjectId.GetHashCode();
		}

		// Token: 0x0600538C RID: 21388 RVA: 0x00101B7C File Offset: 0x000FFD7C
		public override bool Equals(object obj)
		{
			AcceptInvitationRequest acceptInvitationRequest = obj as AcceptInvitationRequest;
			return acceptInvitationRequest != null && this.InvitationId.Equals(acceptInvitationRequest.InvitationId) && this.ObjectId.Equals(acceptInvitationRequest.ObjectId);
		}

		// Token: 0x17000FB6 RID: 4022
		// (get) Token: 0x0600538D RID: 21389 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600538E RID: 21390 RVA: 0x00101BC6 File Offset: 0x000FFDC6
		public static AcceptInvitationRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<AcceptInvitationRequest>(bs, 0, -1);
		}

		// Token: 0x0600538F RID: 21391 RVA: 0x00101BD0 File Offset: 0x000FFDD0
		public void Deserialize(Stream stream)
		{
			AcceptInvitationRequest.Deserialize(stream, this);
		}

		// Token: 0x06005390 RID: 21392 RVA: 0x00101BDA File Offset: 0x000FFDDA
		public static AcceptInvitationRequest Deserialize(Stream stream, AcceptInvitationRequest instance)
		{
			return AcceptInvitationRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005391 RID: 21393 RVA: 0x00101BE8 File Offset: 0x000FFDE8
		public static AcceptInvitationRequest DeserializeLengthDelimited(Stream stream)
		{
			AcceptInvitationRequest acceptInvitationRequest = new AcceptInvitationRequest();
			AcceptInvitationRequest.DeserializeLengthDelimited(stream, acceptInvitationRequest);
			return acceptInvitationRequest;
		}

		// Token: 0x06005392 RID: 21394 RVA: 0x00101C04 File Offset: 0x000FFE04
		public static AcceptInvitationRequest DeserializeLengthDelimited(Stream stream, AcceptInvitationRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AcceptInvitationRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06005393 RID: 21395 RVA: 0x00101C2C File Offset: 0x000FFE2C
		public static AcceptInvitationRequest Deserialize(Stream stream, AcceptInvitationRequest instance, long limit)
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
					if (num != 32)
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
						instance.ObjectId = ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x06005394 RID: 21396 RVA: 0x00101CCB File Offset: 0x000FFECB
		public void Serialize(Stream stream)
		{
			AcceptInvitationRequest.Serialize(stream, this);
		}

		// Token: 0x06005395 RID: 21397 RVA: 0x00101CD4 File Offset: 0x000FFED4
		public static void Serialize(Stream stream, AcceptInvitationRequest instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			stream.WriteByte(25);
			binaryWriter.Write(instance.InvitationId);
			stream.WriteByte(32);
			ProtocolParser.WriteUInt64(stream, instance.ObjectId);
		}

		// Token: 0x06005396 RID: 21398 RVA: 0x00101D03 File Offset: 0x000FFF03
		public uint GetSerializedSize()
		{
			return 0U + 8U + ProtocolParser.SizeOfUInt64(this.ObjectId) + 2U;
		}
	}
}
