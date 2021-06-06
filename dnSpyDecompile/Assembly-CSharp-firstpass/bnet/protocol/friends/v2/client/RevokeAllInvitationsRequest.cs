using System;
using System.IO;

namespace bnet.protocol.friends.v2.client
{
	// Token: 0x02000406 RID: 1030
	public class RevokeAllInvitationsRequest : IProtoBuf
	{
		// Token: 0x06004468 RID: 17512 RVA: 0x000157A7 File Offset: 0x000139A7
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode();
		}

		// Token: 0x06004469 RID: 17513 RVA: 0x000D7862 File Offset: 0x000D5A62
		public override bool Equals(object obj)
		{
			return obj is RevokeAllInvitationsRequest;
		}

		// Token: 0x17000C9F RID: 3231
		// (get) Token: 0x0600446A RID: 17514 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600446B RID: 17515 RVA: 0x000D786F File Offset: 0x000D5A6F
		public static RevokeAllInvitationsRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<RevokeAllInvitationsRequest>(bs, 0, -1);
		}

		// Token: 0x0600446C RID: 17516 RVA: 0x000D7879 File Offset: 0x000D5A79
		public void Deserialize(Stream stream)
		{
			RevokeAllInvitationsRequest.Deserialize(stream, this);
		}

		// Token: 0x0600446D RID: 17517 RVA: 0x000D7883 File Offset: 0x000D5A83
		public static RevokeAllInvitationsRequest Deserialize(Stream stream, RevokeAllInvitationsRequest instance)
		{
			return RevokeAllInvitationsRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600446E RID: 17518 RVA: 0x000D7890 File Offset: 0x000D5A90
		public static RevokeAllInvitationsRequest DeserializeLengthDelimited(Stream stream)
		{
			RevokeAllInvitationsRequest revokeAllInvitationsRequest = new RevokeAllInvitationsRequest();
			RevokeAllInvitationsRequest.DeserializeLengthDelimited(stream, revokeAllInvitationsRequest);
			return revokeAllInvitationsRequest;
		}

		// Token: 0x0600446F RID: 17519 RVA: 0x000D78AC File Offset: 0x000D5AAC
		public static RevokeAllInvitationsRequest DeserializeLengthDelimited(Stream stream, RevokeAllInvitationsRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return RevokeAllInvitationsRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06004470 RID: 17520 RVA: 0x000D78D4 File Offset: 0x000D5AD4
		public static RevokeAllInvitationsRequest Deserialize(Stream stream, RevokeAllInvitationsRequest instance, long limit)
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

		// Token: 0x06004471 RID: 17521 RVA: 0x000D7941 File Offset: 0x000D5B41
		public void Serialize(Stream stream)
		{
			RevokeAllInvitationsRequest.Serialize(stream, this);
		}

		// Token: 0x06004472 RID: 17522 RVA: 0x00003FD0 File Offset: 0x000021D0
		public static void Serialize(Stream stream, RevokeAllInvitationsRequest instance)
		{
		}

		// Token: 0x06004473 RID: 17523 RVA: 0x00003D71 File Offset: 0x00001F71
		public uint GetSerializedSize()
		{
			return 0U;
		}
	}
}
