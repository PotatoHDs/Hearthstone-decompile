using System;
using System.IO;

namespace bnet.protocol.friends.v2.client
{
	// Token: 0x02000408 RID: 1032
	public class IgnoreInvitationRequest : IProtoBuf
	{
		// Token: 0x17000CA3 RID: 3235
		// (get) Token: 0x06004488 RID: 17544 RVA: 0x000D7C15 File Offset: 0x000D5E15
		// (set) Token: 0x06004489 RID: 17545 RVA: 0x000D7C1D File Offset: 0x000D5E1D
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

		// Token: 0x0600448A RID: 17546 RVA: 0x000D7C2D File Offset: 0x000D5E2D
		public void SetInvitationId(ulong val)
		{
			this.InvitationId = val;
		}

		// Token: 0x0600448B RID: 17547 RVA: 0x000D7C38 File Offset: 0x000D5E38
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasInvitationId)
			{
				num ^= this.InvitationId.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600448C RID: 17548 RVA: 0x000D7C6C File Offset: 0x000D5E6C
		public override bool Equals(object obj)
		{
			IgnoreInvitationRequest ignoreInvitationRequest = obj as IgnoreInvitationRequest;
			return ignoreInvitationRequest != null && this.HasInvitationId == ignoreInvitationRequest.HasInvitationId && (!this.HasInvitationId || this.InvitationId.Equals(ignoreInvitationRequest.InvitationId));
		}

		// Token: 0x17000CA4 RID: 3236
		// (get) Token: 0x0600448D RID: 17549 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600448E RID: 17550 RVA: 0x000D7CB4 File Offset: 0x000D5EB4
		public static IgnoreInvitationRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<IgnoreInvitationRequest>(bs, 0, -1);
		}

		// Token: 0x0600448F RID: 17551 RVA: 0x000D7CBE File Offset: 0x000D5EBE
		public void Deserialize(Stream stream)
		{
			IgnoreInvitationRequest.Deserialize(stream, this);
		}

		// Token: 0x06004490 RID: 17552 RVA: 0x000D7CC8 File Offset: 0x000D5EC8
		public static IgnoreInvitationRequest Deserialize(Stream stream, IgnoreInvitationRequest instance)
		{
			return IgnoreInvitationRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004491 RID: 17553 RVA: 0x000D7CD4 File Offset: 0x000D5ED4
		public static IgnoreInvitationRequest DeserializeLengthDelimited(Stream stream)
		{
			IgnoreInvitationRequest ignoreInvitationRequest = new IgnoreInvitationRequest();
			IgnoreInvitationRequest.DeserializeLengthDelimited(stream, ignoreInvitationRequest);
			return ignoreInvitationRequest;
		}

		// Token: 0x06004492 RID: 17554 RVA: 0x000D7CF0 File Offset: 0x000D5EF0
		public static IgnoreInvitationRequest DeserializeLengthDelimited(Stream stream, IgnoreInvitationRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return IgnoreInvitationRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06004493 RID: 17555 RVA: 0x000D7D18 File Offset: 0x000D5F18
		public static IgnoreInvitationRequest Deserialize(Stream stream, IgnoreInvitationRequest instance, long limit)
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
				else if (num == 24)
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

		// Token: 0x06004494 RID: 17556 RVA: 0x000D7D98 File Offset: 0x000D5F98
		public void Serialize(Stream stream)
		{
			IgnoreInvitationRequest.Serialize(stream, this);
		}

		// Token: 0x06004495 RID: 17557 RVA: 0x000D7DA1 File Offset: 0x000D5FA1
		public static void Serialize(Stream stream, IgnoreInvitationRequest instance)
		{
			if (instance.HasInvitationId)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, instance.InvitationId);
			}
		}

		// Token: 0x06004496 RID: 17558 RVA: 0x000D7DC0 File Offset: 0x000D5FC0
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

		// Token: 0x04001728 RID: 5928
		public bool HasInvitationId;

		// Token: 0x04001729 RID: 5929
		private ulong _InvitationId;
	}
}
