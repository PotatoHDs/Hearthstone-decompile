using System;
using System.IO;

namespace bnet.protocol.friends.v2.client
{
	// Token: 0x02000405 RID: 1029
	public class RevokeInvitationRequest : IProtoBuf
	{
		// Token: 0x17000C9D RID: 3229
		// (get) Token: 0x06004458 RID: 17496 RVA: 0x000D768E File Offset: 0x000D588E
		// (set) Token: 0x06004459 RID: 17497 RVA: 0x000D7696 File Offset: 0x000D5896
		public ulong InvitationId
		{
			get
			{
				return this._InvitationId;
			}
			set
			{
				this._InvitationId = value;
				this.HasInvitationId = true;
			}
		}

		// Token: 0x0600445A RID: 17498 RVA: 0x000D76A6 File Offset: 0x000D58A6
		public void SetInvitationId(ulong val)
		{
			this.InvitationId = val;
		}

		// Token: 0x0600445B RID: 17499 RVA: 0x000D76B0 File Offset: 0x000D58B0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasInvitationId)
			{
				num ^= this.InvitationId.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600445C RID: 17500 RVA: 0x000D76E4 File Offset: 0x000D58E4
		public override bool Equals(object obj)
		{
			RevokeInvitationRequest revokeInvitationRequest = obj as RevokeInvitationRequest;
			return revokeInvitationRequest != null && this.HasInvitationId == revokeInvitationRequest.HasInvitationId && (!this.HasInvitationId || this.InvitationId.Equals(revokeInvitationRequest.InvitationId));
		}

		// Token: 0x17000C9E RID: 3230
		// (get) Token: 0x0600445D RID: 17501 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600445E RID: 17502 RVA: 0x000D772C File Offset: 0x000D592C
		public static RevokeInvitationRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<RevokeInvitationRequest>(bs, 0, -1);
		}

		// Token: 0x0600445F RID: 17503 RVA: 0x000D7736 File Offset: 0x000D5936
		public void Deserialize(Stream stream)
		{
			RevokeInvitationRequest.Deserialize(stream, this);
		}

		// Token: 0x06004460 RID: 17504 RVA: 0x000D7740 File Offset: 0x000D5940
		public static RevokeInvitationRequest Deserialize(Stream stream, RevokeInvitationRequest instance)
		{
			return RevokeInvitationRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004461 RID: 17505 RVA: 0x000D774C File Offset: 0x000D594C
		public static RevokeInvitationRequest DeserializeLengthDelimited(Stream stream)
		{
			RevokeInvitationRequest revokeInvitationRequest = new RevokeInvitationRequest();
			RevokeInvitationRequest.DeserializeLengthDelimited(stream, revokeInvitationRequest);
			return revokeInvitationRequest;
		}

		// Token: 0x06004462 RID: 17506 RVA: 0x000D7768 File Offset: 0x000D5968
		public static RevokeInvitationRequest DeserializeLengthDelimited(Stream stream, RevokeInvitationRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return RevokeInvitationRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06004463 RID: 17507 RVA: 0x000D7790 File Offset: 0x000D5990
		public static RevokeInvitationRequest Deserialize(Stream stream, RevokeInvitationRequest instance, long limit)
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
				else if (num == 16)
				{
					instance.InvitationId = ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x06004464 RID: 17508 RVA: 0x000D7810 File Offset: 0x000D5A10
		public void Serialize(Stream stream)
		{
			RevokeInvitationRequest.Serialize(stream, this);
		}

		// Token: 0x06004465 RID: 17509 RVA: 0x000D7819 File Offset: 0x000D5A19
		public static void Serialize(Stream stream, RevokeInvitationRequest instance)
		{
			if (instance.HasInvitationId)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, instance.InvitationId);
			}
		}

		// Token: 0x06004466 RID: 17510 RVA: 0x000D7838 File Offset: 0x000D5A38
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasInvitationId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.InvitationId);
			}
			return num;
		}

		// Token: 0x04001722 RID: 5922
		public bool HasInvitationId;

		// Token: 0x04001723 RID: 5923
		private ulong _InvitationId;
	}
}
