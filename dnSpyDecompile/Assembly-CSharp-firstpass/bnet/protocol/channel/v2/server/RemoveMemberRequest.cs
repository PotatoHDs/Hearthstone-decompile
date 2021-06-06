using System;
using System.IO;
using bnet.protocol.account.v1;
using bnet.protocol.channel.v1;
using bnet.protocol.channel.v2.Types;

namespace bnet.protocol.channel.v2.server
{
	// Token: 0x0200049F RID: 1183
	public class RemoveMemberRequest : IProtoBuf
	{
		// Token: 0x17000F88 RID: 3976
		// (get) Token: 0x06005275 RID: 21109 RVA: 0x000FF06A File Offset: 0x000FD26A
		// (set) Token: 0x06005276 RID: 21110 RVA: 0x000FF072 File Offset: 0x000FD272
		public ChannelId ChannelId
		{
			get
			{
				return this._ChannelId;
			}
			set
			{
				this._ChannelId = value;
				this.HasChannelId = (value != null);
			}
		}

		// Token: 0x06005277 RID: 21111 RVA: 0x000FF085 File Offset: 0x000FD285
		public void SetChannelId(ChannelId val)
		{
			this.ChannelId = val;
		}

		// Token: 0x17000F89 RID: 3977
		// (get) Token: 0x06005278 RID: 21112 RVA: 0x000FF08E File Offset: 0x000FD28E
		// (set) Token: 0x06005279 RID: 21113 RVA: 0x000FF096 File Offset: 0x000FD296
		public ChannelRemovedReason Reason
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

		// Token: 0x0600527A RID: 21114 RVA: 0x000FF0A6 File Offset: 0x000FD2A6
		public void SetReason(ChannelRemovedReason val)
		{
			this.Reason = val;
		}

		// Token: 0x17000F8A RID: 3978
		// (get) Token: 0x0600527B RID: 21115 RVA: 0x000FF0AF File Offset: 0x000FD2AF
		// (set) Token: 0x0600527C RID: 21116 RVA: 0x000FF0B7 File Offset: 0x000FD2B7
		public GameAccountHandle MemberId
		{
			get
			{
				return this._MemberId;
			}
			set
			{
				this._MemberId = value;
				this.HasMemberId = (value != null);
			}
		}

		// Token: 0x0600527D RID: 21117 RVA: 0x000FF0CA File Offset: 0x000FD2CA
		public void SetMemberId(GameAccountHandle val)
		{
			this.MemberId = val;
		}

		// Token: 0x0600527E RID: 21118 RVA: 0x000FF0D4 File Offset: 0x000FD2D4
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasChannelId)
			{
				num ^= this.ChannelId.GetHashCode();
			}
			if (this.HasReason)
			{
				num ^= this.Reason.GetHashCode();
			}
			if (this.HasMemberId)
			{
				num ^= this.MemberId.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600527F RID: 21119 RVA: 0x000FF13C File Offset: 0x000FD33C
		public override bool Equals(object obj)
		{
			RemoveMemberRequest removeMemberRequest = obj as RemoveMemberRequest;
			return removeMemberRequest != null && this.HasChannelId == removeMemberRequest.HasChannelId && (!this.HasChannelId || this.ChannelId.Equals(removeMemberRequest.ChannelId)) && this.HasReason == removeMemberRequest.HasReason && (!this.HasReason || this.Reason.Equals(removeMemberRequest.Reason)) && this.HasMemberId == removeMemberRequest.HasMemberId && (!this.HasMemberId || this.MemberId.Equals(removeMemberRequest.MemberId));
		}

		// Token: 0x17000F8B RID: 3979
		// (get) Token: 0x06005280 RID: 21120 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005281 RID: 21121 RVA: 0x000FF1E5 File Offset: 0x000FD3E5
		public static RemoveMemberRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<RemoveMemberRequest>(bs, 0, -1);
		}

		// Token: 0x06005282 RID: 21122 RVA: 0x000FF1EF File Offset: 0x000FD3EF
		public void Deserialize(Stream stream)
		{
			RemoveMemberRequest.Deserialize(stream, this);
		}

		// Token: 0x06005283 RID: 21123 RVA: 0x000FF1F9 File Offset: 0x000FD3F9
		public static RemoveMemberRequest Deserialize(Stream stream, RemoveMemberRequest instance)
		{
			return RemoveMemberRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005284 RID: 21124 RVA: 0x000FF204 File Offset: 0x000FD404
		public static RemoveMemberRequest DeserializeLengthDelimited(Stream stream)
		{
			RemoveMemberRequest removeMemberRequest = new RemoveMemberRequest();
			RemoveMemberRequest.DeserializeLengthDelimited(stream, removeMemberRequest);
			return removeMemberRequest;
		}

		// Token: 0x06005285 RID: 21125 RVA: 0x000FF220 File Offset: 0x000FD420
		public static RemoveMemberRequest DeserializeLengthDelimited(Stream stream, RemoveMemberRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return RemoveMemberRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06005286 RID: 21126 RVA: 0x000FF248 File Offset: 0x000FD448
		public static RemoveMemberRequest Deserialize(Stream stream, RemoveMemberRequest instance, long limit)
		{
			instance.Reason = ChannelRemovedReason.CHANNEL_REMOVED_REASON_MEMBER_LEFT;
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
				else if (num != 10)
				{
					if (num != 16)
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
						else if (instance.MemberId == null)
						{
							instance.MemberId = GameAccountHandle.DeserializeLengthDelimited(stream);
						}
						else
						{
							GameAccountHandle.DeserializeLengthDelimited(stream, instance.MemberId);
						}
					}
					else
					{
						instance.Reason = (ChannelRemovedReason)ProtocolParser.ReadUInt64(stream);
					}
				}
				else if (instance.ChannelId == null)
				{
					instance.ChannelId = ChannelId.DeserializeLengthDelimited(stream);
				}
				else
				{
					ChannelId.DeserializeLengthDelimited(stream, instance.ChannelId);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06005287 RID: 21127 RVA: 0x000FF338 File Offset: 0x000FD538
		public void Serialize(Stream stream)
		{
			RemoveMemberRequest.Serialize(stream, this);
		}

		// Token: 0x06005288 RID: 21128 RVA: 0x000FF344 File Offset: 0x000FD544
		public static void Serialize(Stream stream, RemoveMemberRequest instance)
		{
			if (instance.HasChannelId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.ChannelId.GetSerializedSize());
				ChannelId.Serialize(stream, instance.ChannelId);
			}
			if (instance.HasReason)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Reason));
			}
			if (instance.HasMemberId)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.MemberId.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.MemberId);
			}
		}

		// Token: 0x06005289 RID: 21129 RVA: 0x000FF3C8 File Offset: 0x000FD5C8
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasChannelId)
			{
				num += 1U;
				uint serializedSize = this.ChannelId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasReason)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Reason));
			}
			if (this.HasMemberId)
			{
				num += 1U;
				uint serializedSize2 = this.MemberId.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num;
		}

		// Token: 0x04001A66 RID: 6758
		public bool HasChannelId;

		// Token: 0x04001A67 RID: 6759
		private ChannelId _ChannelId;

		// Token: 0x04001A68 RID: 6760
		public bool HasReason;

		// Token: 0x04001A69 RID: 6761
		private ChannelRemovedReason _Reason;

		// Token: 0x04001A6A RID: 6762
		public bool HasMemberId;

		// Token: 0x04001A6B RID: 6763
		private GameAccountHandle _MemberId;
	}
}
