using System;
using System.IO;
using bnet.protocol.account.v1;
using bnet.protocol.Types;

namespace bnet.protocol.channel.v2.membership
{
	// Token: 0x020004AB RID: 1195
	public class ReceivedInvitationRemovedNotification : IProtoBuf
	{
		// Token: 0x17000FAC RID: 4012
		// (get) Token: 0x06005359 RID: 21337 RVA: 0x001013D9 File Offset: 0x000FF5D9
		// (set) Token: 0x0600535A RID: 21338 RVA: 0x001013E1 File Offset: 0x000FF5E1
		public GameAccountHandle AgentId
		{
			get
			{
				return this._AgentId;
			}
			set
			{
				this._AgentId = value;
				this.HasAgentId = (value != null);
			}
		}

		// Token: 0x0600535B RID: 21339 RVA: 0x001013F4 File Offset: 0x000FF5F4
		public void SetAgentId(GameAccountHandle val)
		{
			this.AgentId = val;
		}

		// Token: 0x17000FAD RID: 4013
		// (get) Token: 0x0600535C RID: 21340 RVA: 0x001013FD File Offset: 0x000FF5FD
		// (set) Token: 0x0600535D RID: 21341 RVA: 0x00101405 File Offset: 0x000FF605
		public GameAccountHandle SubscriberId
		{
			get
			{
				return this._SubscriberId;
			}
			set
			{
				this._SubscriberId = value;
				this.HasSubscriberId = (value != null);
			}
		}

		// Token: 0x0600535E RID: 21342 RVA: 0x00101418 File Offset: 0x000FF618
		public void SetSubscriberId(GameAccountHandle val)
		{
			this.SubscriberId = val;
		}

		// Token: 0x17000FAE RID: 4014
		// (get) Token: 0x0600535F RID: 21343 RVA: 0x00101421 File Offset: 0x000FF621
		// (set) Token: 0x06005360 RID: 21344 RVA: 0x00101429 File Offset: 0x000FF629
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

		// Token: 0x06005361 RID: 21345 RVA: 0x00101439 File Offset: 0x000FF639
		public void SetInvitationId(ulong val)
		{
			this.InvitationId = val;
		}

		// Token: 0x17000FAF RID: 4015
		// (get) Token: 0x06005362 RID: 21346 RVA: 0x00101442 File Offset: 0x000FF642
		// (set) Token: 0x06005363 RID: 21347 RVA: 0x0010144A File Offset: 0x000FF64A
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

		// Token: 0x06005364 RID: 21348 RVA: 0x0010145A File Offset: 0x000FF65A
		public void SetReason(InvitationRemovedReason val)
		{
			this.Reason = val;
		}

		// Token: 0x06005365 RID: 21349 RVA: 0x00101464 File Offset: 0x000FF664
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAgentId)
			{
				num ^= this.AgentId.GetHashCode();
			}
			if (this.HasSubscriberId)
			{
				num ^= this.SubscriberId.GetHashCode();
			}
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

		// Token: 0x06005366 RID: 21350 RVA: 0x001014E4 File Offset: 0x000FF6E4
		public override bool Equals(object obj)
		{
			ReceivedInvitationRemovedNotification receivedInvitationRemovedNotification = obj as ReceivedInvitationRemovedNotification;
			return receivedInvitationRemovedNotification != null && this.HasAgentId == receivedInvitationRemovedNotification.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(receivedInvitationRemovedNotification.AgentId)) && this.HasSubscriberId == receivedInvitationRemovedNotification.HasSubscriberId && (!this.HasSubscriberId || this.SubscriberId.Equals(receivedInvitationRemovedNotification.SubscriberId)) && this.HasInvitationId == receivedInvitationRemovedNotification.HasInvitationId && (!this.HasInvitationId || this.InvitationId.Equals(receivedInvitationRemovedNotification.InvitationId)) && this.HasReason == receivedInvitationRemovedNotification.HasReason && (!this.HasReason || this.Reason.Equals(receivedInvitationRemovedNotification.Reason));
		}

		// Token: 0x17000FB0 RID: 4016
		// (get) Token: 0x06005367 RID: 21351 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005368 RID: 21352 RVA: 0x001015BB File Offset: 0x000FF7BB
		public static ReceivedInvitationRemovedNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ReceivedInvitationRemovedNotification>(bs, 0, -1);
		}

		// Token: 0x06005369 RID: 21353 RVA: 0x001015C5 File Offset: 0x000FF7C5
		public void Deserialize(Stream stream)
		{
			ReceivedInvitationRemovedNotification.Deserialize(stream, this);
		}

		// Token: 0x0600536A RID: 21354 RVA: 0x001015CF File Offset: 0x000FF7CF
		public static ReceivedInvitationRemovedNotification Deserialize(Stream stream, ReceivedInvitationRemovedNotification instance)
		{
			return ReceivedInvitationRemovedNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600536B RID: 21355 RVA: 0x001015DC File Offset: 0x000FF7DC
		public static ReceivedInvitationRemovedNotification DeserializeLengthDelimited(Stream stream)
		{
			ReceivedInvitationRemovedNotification receivedInvitationRemovedNotification = new ReceivedInvitationRemovedNotification();
			ReceivedInvitationRemovedNotification.DeserializeLengthDelimited(stream, receivedInvitationRemovedNotification);
			return receivedInvitationRemovedNotification;
		}

		// Token: 0x0600536C RID: 21356 RVA: 0x001015F8 File Offset: 0x000FF7F8
		public static ReceivedInvitationRemovedNotification DeserializeLengthDelimited(Stream stream, ReceivedInvitationRemovedNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ReceivedInvitationRemovedNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x0600536D RID: 21357 RVA: 0x00101620 File Offset: 0x000FF820
		public static ReceivedInvitationRemovedNotification Deserialize(Stream stream, ReceivedInvitationRemovedNotification instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
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
				else
				{
					if (num <= 18)
					{
						if (num != 10)
						{
							if (num == 18)
							{
								if (instance.SubscriberId == null)
								{
									instance.SubscriberId = GameAccountHandle.DeserializeLengthDelimited(stream);
									continue;
								}
								GameAccountHandle.DeserializeLengthDelimited(stream, instance.SubscriberId);
								continue;
							}
						}
						else
						{
							if (instance.AgentId == null)
							{
								instance.AgentId = GameAccountHandle.DeserializeLengthDelimited(stream);
								continue;
							}
							GameAccountHandle.DeserializeLengthDelimited(stream, instance.AgentId);
							continue;
						}
					}
					else
					{
						if (num == 25)
						{
							instance.InvitationId = binaryReader.ReadUInt64();
							continue;
						}
						if (num == 32)
						{
							instance.Reason = (InvitationRemovedReason)ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
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

		// Token: 0x0600536E RID: 21358 RVA: 0x0010173A File Offset: 0x000FF93A
		public void Serialize(Stream stream)
		{
			ReceivedInvitationRemovedNotification.Serialize(stream, this);
		}

		// Token: 0x0600536F RID: 21359 RVA: 0x00101744 File Offset: 0x000FF944
		public static void Serialize(Stream stream, ReceivedInvitationRemovedNotification instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.AgentId);
			}
			if (instance.HasSubscriberId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.SubscriberId.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.SubscriberId);
			}
			if (instance.HasInvitationId)
			{
				stream.WriteByte(25);
				binaryWriter.Write(instance.InvitationId);
			}
			if (instance.HasReason)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Reason));
			}
		}

		// Token: 0x06005370 RID: 21360 RVA: 0x001017EC File Offset: 0x000FF9EC
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAgentId)
			{
				num += 1U;
				uint serializedSize = this.AgentId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasSubscriberId)
			{
				num += 1U;
				uint serializedSize2 = this.SubscriberId.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasInvitationId)
			{
				num += 1U;
				num += 8U;
			}
			if (this.HasReason)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Reason));
			}
			return num;
		}

		// Token: 0x04001A96 RID: 6806
		public bool HasAgentId;

		// Token: 0x04001A97 RID: 6807
		private GameAccountHandle _AgentId;

		// Token: 0x04001A98 RID: 6808
		public bool HasSubscriberId;

		// Token: 0x04001A99 RID: 6809
		private GameAccountHandle _SubscriberId;

		// Token: 0x04001A9A RID: 6810
		public bool HasInvitationId;

		// Token: 0x04001A9B RID: 6811
		private ulong _InvitationId;

		// Token: 0x04001A9C RID: 6812
		public bool HasReason;

		// Token: 0x04001A9D RID: 6813
		private InvitationRemovedReason _Reason;
	}
}
