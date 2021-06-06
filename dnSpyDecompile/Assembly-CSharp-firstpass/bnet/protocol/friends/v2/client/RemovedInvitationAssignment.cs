using System;
using System.IO;
using bnet.protocol.Types;

namespace bnet.protocol.friends.v2.client
{
	// Token: 0x02000412 RID: 1042
	public class RemovedInvitationAssignment : IProtoBuf
	{
		// Token: 0x17000CD2 RID: 3282
		// (get) Token: 0x06004573 RID: 17779 RVA: 0x000DA31F File Offset: 0x000D851F
		// (set) Token: 0x06004574 RID: 17780 RVA: 0x000DA327 File Offset: 0x000D8527
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

		// Token: 0x06004575 RID: 17781 RVA: 0x000DA337 File Offset: 0x000D8537
		public void SetInvitationId(ulong val)
		{
			this.InvitationId = val;
		}

		// Token: 0x17000CD3 RID: 3283
		// (get) Token: 0x06004576 RID: 17782 RVA: 0x000DA340 File Offset: 0x000D8540
		// (set) Token: 0x06004577 RID: 17783 RVA: 0x000DA348 File Offset: 0x000D8548
		public InvitationRemovedReason Reason
		{
			get
			{
				return this._Reason;
			}
			set
			{
				this._Reason = value;
				this.HasReason = true;
			}
		}

		// Token: 0x06004578 RID: 17784 RVA: 0x000DA358 File Offset: 0x000D8558
		public void SetReason(InvitationRemovedReason val)
		{
			this.Reason = val;
		}

		// Token: 0x06004579 RID: 17785 RVA: 0x000DA364 File Offset: 0x000D8564
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasInvitationId)
			{
				num ^= this.InvitationId.GetHashCode();
			}
			if (this.HasReason)
			{
				num ^= this.Reason.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600457A RID: 17786 RVA: 0x000DA3B8 File Offset: 0x000D85B8
		public override bool Equals(object obj)
		{
			RemovedInvitationAssignment removedInvitationAssignment = obj as RemovedInvitationAssignment;
			return removedInvitationAssignment != null && this.HasInvitationId == removedInvitationAssignment.HasInvitationId && (!this.HasInvitationId || this.InvitationId.Equals(removedInvitationAssignment.InvitationId)) && this.HasReason == removedInvitationAssignment.HasReason && (!this.HasReason || this.Reason.Equals(removedInvitationAssignment.Reason));
		}

		// Token: 0x17000CD4 RID: 3284
		// (get) Token: 0x0600457B RID: 17787 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600457C RID: 17788 RVA: 0x000DA439 File Offset: 0x000D8639
		public static RemovedInvitationAssignment ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<RemovedInvitationAssignment>(bs, 0, -1);
		}

		// Token: 0x0600457D RID: 17789 RVA: 0x000DA443 File Offset: 0x000D8643
		public void Deserialize(Stream stream)
		{
			RemovedInvitationAssignment.Deserialize(stream, this);
		}

		// Token: 0x0600457E RID: 17790 RVA: 0x000DA44D File Offset: 0x000D864D
		public static RemovedInvitationAssignment Deserialize(Stream stream, RemovedInvitationAssignment instance)
		{
			return RemovedInvitationAssignment.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600457F RID: 17791 RVA: 0x000DA458 File Offset: 0x000D8658
		public static RemovedInvitationAssignment DeserializeLengthDelimited(Stream stream)
		{
			RemovedInvitationAssignment removedInvitationAssignment = new RemovedInvitationAssignment();
			RemovedInvitationAssignment.DeserializeLengthDelimited(stream, removedInvitationAssignment);
			return removedInvitationAssignment;
		}

		// Token: 0x06004580 RID: 17792 RVA: 0x000DA474 File Offset: 0x000D8674
		public static RemovedInvitationAssignment DeserializeLengthDelimited(Stream stream, RemovedInvitationAssignment instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return RemovedInvitationAssignment.Deserialize(stream, instance, num);
		}

		// Token: 0x06004581 RID: 17793 RVA: 0x000DA49C File Offset: 0x000D869C
		public static RemovedInvitationAssignment Deserialize(Stream stream, RemovedInvitationAssignment instance, long limit)
		{
			instance.Reason = InvitationRemovedReason.INVITATION_REMOVED_REASON_ACCEPTED;
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
				else if (num != 8)
				{
					if (num != 16)
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
						instance.Reason = (InvitationRemovedReason)ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.InvitationId = ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06004582 RID: 17794 RVA: 0x000DA53B File Offset: 0x000D873B
		public void Serialize(Stream stream)
		{
			RemovedInvitationAssignment.Serialize(stream, this);
		}

		// Token: 0x06004583 RID: 17795 RVA: 0x000DA544 File Offset: 0x000D8744
		public static void Serialize(Stream stream, RemovedInvitationAssignment instance)
		{
			if (instance.HasInvitationId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, instance.InvitationId);
			}
			if (instance.HasReason)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Reason));
			}
		}

		// Token: 0x06004584 RID: 17796 RVA: 0x000DA580 File Offset: 0x000D8780
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasInvitationId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.InvitationId);
			}
			if (this.HasReason)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Reason));
			}
			return num;
		}

		// Token: 0x04001763 RID: 5987
		public bool HasInvitationId;

		// Token: 0x04001764 RID: 5988
		private ulong _InvitationId;

		// Token: 0x04001765 RID: 5989
		public bool HasReason;

		// Token: 0x04001766 RID: 5990
		private InvitationRemovedReason _Reason;
	}
}
