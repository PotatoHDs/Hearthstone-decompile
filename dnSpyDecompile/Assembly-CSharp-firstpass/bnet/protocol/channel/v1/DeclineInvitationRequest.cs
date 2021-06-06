using System;
using System.IO;

namespace bnet.protocol.channel.v1
{
	// Token: 0x020004AF RID: 1199
	public class DeclineInvitationRequest : IProtoBuf
	{
		// Token: 0x17000FB9 RID: 4025
		// (get) Token: 0x060053A8 RID: 21416 RVA: 0x00101E9E File Offset: 0x0010009E
		// (set) Token: 0x060053A9 RID: 21417 RVA: 0x00101EA6 File Offset: 0x001000A6
		public ulong InvitationId { get; set; }

		// Token: 0x060053AA RID: 21418 RVA: 0x00101EAF File Offset: 0x001000AF
		public void SetInvitationId(ulong val)
		{
			this.InvitationId = val;
		}

		// Token: 0x060053AB RID: 21419 RVA: 0x00101EB8 File Offset: 0x001000B8
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.InvitationId.GetHashCode();
		}

		// Token: 0x060053AC RID: 21420 RVA: 0x00101EE0 File Offset: 0x001000E0
		public override bool Equals(object obj)
		{
			DeclineInvitationRequest declineInvitationRequest = obj as DeclineInvitationRequest;
			return declineInvitationRequest != null && this.InvitationId.Equals(declineInvitationRequest.InvitationId);
		}

		// Token: 0x17000FBA RID: 4026
		// (get) Token: 0x060053AD RID: 21421 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060053AE RID: 21422 RVA: 0x00101F12 File Offset: 0x00100112
		public static DeclineInvitationRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<DeclineInvitationRequest>(bs, 0, -1);
		}

		// Token: 0x060053AF RID: 21423 RVA: 0x00101F1C File Offset: 0x0010011C
		public void Deserialize(Stream stream)
		{
			DeclineInvitationRequest.Deserialize(stream, this);
		}

		// Token: 0x060053B0 RID: 21424 RVA: 0x00101F26 File Offset: 0x00100126
		public static DeclineInvitationRequest Deserialize(Stream stream, DeclineInvitationRequest instance)
		{
			return DeclineInvitationRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060053B1 RID: 21425 RVA: 0x00101F34 File Offset: 0x00100134
		public static DeclineInvitationRequest DeserializeLengthDelimited(Stream stream)
		{
			DeclineInvitationRequest declineInvitationRequest = new DeclineInvitationRequest();
			DeclineInvitationRequest.DeserializeLengthDelimited(stream, declineInvitationRequest);
			return declineInvitationRequest;
		}

		// Token: 0x060053B2 RID: 21426 RVA: 0x00101F50 File Offset: 0x00100150
		public static DeclineInvitationRequest DeserializeLengthDelimited(Stream stream, DeclineInvitationRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return DeclineInvitationRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x060053B3 RID: 21427 RVA: 0x00101F78 File Offset: 0x00100178
		public static DeclineInvitationRequest Deserialize(Stream stream, DeclineInvitationRequest instance, long limit)
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
				else if (num == 25)
				{
					instance.InvitationId = binaryReader.ReadUInt64();
				}
				else
				{
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

		// Token: 0x060053B4 RID: 21428 RVA: 0x00101FFF File Offset: 0x001001FF
		public void Serialize(Stream stream)
		{
			DeclineInvitationRequest.Serialize(stream, this);
		}

		// Token: 0x060053B5 RID: 21429 RVA: 0x00102008 File Offset: 0x00100208
		public static void Serialize(Stream stream, DeclineInvitationRequest instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			stream.WriteByte(25);
			binaryWriter.Write(instance.InvitationId);
		}

		// Token: 0x060053B6 RID: 21430 RVA: 0x000BACAB File Offset: 0x000B8EAB
		public uint GetSerializedSize()
		{
			return 0U + 8U + 1U;
		}
	}
}
