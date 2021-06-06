using System;
using System.IO;

namespace bnet.protocol.channel.v1
{
	// Token: 0x020004AE RID: 1198
	public class AcceptInvitationResponse : IProtoBuf
	{
		// Token: 0x17000FB7 RID: 4023
		// (get) Token: 0x06005398 RID: 21400 RVA: 0x00101D16 File Offset: 0x000FFF16
		// (set) Token: 0x06005399 RID: 21401 RVA: 0x00101D1E File Offset: 0x000FFF1E
		public ulong ObjectId { get; set; }

		// Token: 0x0600539A RID: 21402 RVA: 0x00101D27 File Offset: 0x000FFF27
		public void SetObjectId(ulong val)
		{
			this.ObjectId = val;
		}

		// Token: 0x0600539B RID: 21403 RVA: 0x00101D30 File Offset: 0x000FFF30
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.ObjectId.GetHashCode();
		}

		// Token: 0x0600539C RID: 21404 RVA: 0x00101D58 File Offset: 0x000FFF58
		public override bool Equals(object obj)
		{
			AcceptInvitationResponse acceptInvitationResponse = obj as AcceptInvitationResponse;
			return acceptInvitationResponse != null && this.ObjectId.Equals(acceptInvitationResponse.ObjectId);
		}

		// Token: 0x17000FB8 RID: 4024
		// (get) Token: 0x0600539D RID: 21405 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600539E RID: 21406 RVA: 0x00101D8A File Offset: 0x000FFF8A
		public static AcceptInvitationResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<AcceptInvitationResponse>(bs, 0, -1);
		}

		// Token: 0x0600539F RID: 21407 RVA: 0x00101D94 File Offset: 0x000FFF94
		public void Deserialize(Stream stream)
		{
			AcceptInvitationResponse.Deserialize(stream, this);
		}

		// Token: 0x060053A0 RID: 21408 RVA: 0x00101D9E File Offset: 0x000FFF9E
		public static AcceptInvitationResponse Deserialize(Stream stream, AcceptInvitationResponse instance)
		{
			return AcceptInvitationResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060053A1 RID: 21409 RVA: 0x00101DAC File Offset: 0x000FFFAC
		public static AcceptInvitationResponse DeserializeLengthDelimited(Stream stream)
		{
			AcceptInvitationResponse acceptInvitationResponse = new AcceptInvitationResponse();
			AcceptInvitationResponse.DeserializeLengthDelimited(stream, acceptInvitationResponse);
			return acceptInvitationResponse;
		}

		// Token: 0x060053A2 RID: 21410 RVA: 0x00101DC8 File Offset: 0x000FFFC8
		public static AcceptInvitationResponse DeserializeLengthDelimited(Stream stream, AcceptInvitationResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AcceptInvitationResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x060053A3 RID: 21411 RVA: 0x00101DF0 File Offset: 0x000FFFF0
		public static AcceptInvitationResponse Deserialize(Stream stream, AcceptInvitationResponse instance, long limit)
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
				else if (num == 8)
				{
					instance.ObjectId = ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x060053A4 RID: 21412 RVA: 0x00101E6F File Offset: 0x0010006F
		public void Serialize(Stream stream)
		{
			AcceptInvitationResponse.Serialize(stream, this);
		}

		// Token: 0x060053A5 RID: 21413 RVA: 0x00101E78 File Offset: 0x00100078
		public static void Serialize(Stream stream, AcceptInvitationResponse instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, instance.ObjectId);
		}

		// Token: 0x060053A6 RID: 21414 RVA: 0x00101E8D File Offset: 0x0010008D
		public uint GetSerializedSize()
		{
			return 0U + ProtocolParser.SizeOfUInt64(this.ObjectId) + 1U;
		}
	}
}
