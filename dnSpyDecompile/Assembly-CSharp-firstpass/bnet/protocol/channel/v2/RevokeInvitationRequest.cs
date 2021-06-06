using System;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.channel.v2
{
	// Token: 0x02000461 RID: 1121
	public class RevokeInvitationRequest : IProtoBuf
	{
		// Token: 0x17000E51 RID: 3665
		// (get) Token: 0x06004CB6 RID: 19638 RVA: 0x000EE4F6 File Offset: 0x000EC6F6
		// (set) Token: 0x06004CB7 RID: 19639 RVA: 0x000EE4FE File Offset: 0x000EC6FE
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

		// Token: 0x06004CB8 RID: 19640 RVA: 0x000EE511 File Offset: 0x000EC711
		public void SetAgentId(GameAccountHandle val)
		{
			this.AgentId = val;
		}

		// Token: 0x17000E52 RID: 3666
		// (get) Token: 0x06004CB9 RID: 19641 RVA: 0x000EE51A File Offset: 0x000EC71A
		// (set) Token: 0x06004CBA RID: 19642 RVA: 0x000EE522 File Offset: 0x000EC722
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

		// Token: 0x06004CBB RID: 19643 RVA: 0x000EE535 File Offset: 0x000EC735
		public void SetChannelId(ChannelId val)
		{
			this.ChannelId = val;
		}

		// Token: 0x17000E53 RID: 3667
		// (get) Token: 0x06004CBC RID: 19644 RVA: 0x000EE53E File Offset: 0x000EC73E
		// (set) Token: 0x06004CBD RID: 19645 RVA: 0x000EE546 File Offset: 0x000EC746
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

		// Token: 0x06004CBE RID: 19646 RVA: 0x000EE556 File Offset: 0x000EC756
		public void SetInvitationId(ulong val)
		{
			this.InvitationId = val;
		}

		// Token: 0x06004CBF RID: 19647 RVA: 0x000EE560 File Offset: 0x000EC760
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAgentId)
			{
				num ^= this.AgentId.GetHashCode();
			}
			if (this.HasChannelId)
			{
				num ^= this.ChannelId.GetHashCode();
			}
			if (this.HasInvitationId)
			{
				num ^= this.InvitationId.GetHashCode();
			}
			return num;
		}

		// Token: 0x06004CC0 RID: 19648 RVA: 0x000EE5C0 File Offset: 0x000EC7C0
		public override bool Equals(object obj)
		{
			RevokeInvitationRequest revokeInvitationRequest = obj as RevokeInvitationRequest;
			return revokeInvitationRequest != null && this.HasAgentId == revokeInvitationRequest.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(revokeInvitationRequest.AgentId)) && this.HasChannelId == revokeInvitationRequest.HasChannelId && (!this.HasChannelId || this.ChannelId.Equals(revokeInvitationRequest.ChannelId)) && this.HasInvitationId == revokeInvitationRequest.HasInvitationId && (!this.HasInvitationId || this.InvitationId.Equals(revokeInvitationRequest.InvitationId));
		}

		// Token: 0x17000E54 RID: 3668
		// (get) Token: 0x06004CC1 RID: 19649 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004CC2 RID: 19650 RVA: 0x000EE65E File Offset: 0x000EC85E
		public static RevokeInvitationRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<RevokeInvitationRequest>(bs, 0, -1);
		}

		// Token: 0x06004CC3 RID: 19651 RVA: 0x000EE668 File Offset: 0x000EC868
		public void Deserialize(Stream stream)
		{
			RevokeInvitationRequest.Deserialize(stream, this);
		}

		// Token: 0x06004CC4 RID: 19652 RVA: 0x000EE672 File Offset: 0x000EC872
		public static RevokeInvitationRequest Deserialize(Stream stream, RevokeInvitationRequest instance)
		{
			return RevokeInvitationRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004CC5 RID: 19653 RVA: 0x000EE680 File Offset: 0x000EC880
		public static RevokeInvitationRequest DeserializeLengthDelimited(Stream stream)
		{
			RevokeInvitationRequest revokeInvitationRequest = new RevokeInvitationRequest();
			RevokeInvitationRequest.DeserializeLengthDelimited(stream, revokeInvitationRequest);
			return revokeInvitationRequest;
		}

		// Token: 0x06004CC6 RID: 19654 RVA: 0x000EE69C File Offset: 0x000EC89C
		public static RevokeInvitationRequest DeserializeLengthDelimited(Stream stream, RevokeInvitationRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return RevokeInvitationRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06004CC7 RID: 19655 RVA: 0x000EE6C4 File Offset: 0x000EC8C4
		public static RevokeInvitationRequest Deserialize(Stream stream, RevokeInvitationRequest instance, long limit)
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
				else if (num != 10)
				{
					if (num != 18)
					{
						if (num != 25)
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
							instance.InvitationId = binaryReader.ReadUInt64();
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
				else if (instance.AgentId == null)
				{
					instance.AgentId = GameAccountHandle.DeserializeLengthDelimited(stream);
				}
				else
				{
					GameAccountHandle.DeserializeLengthDelimited(stream, instance.AgentId);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06004CC8 RID: 19656 RVA: 0x000EE7B3 File Offset: 0x000EC9B3
		public void Serialize(Stream stream)
		{
			RevokeInvitationRequest.Serialize(stream, this);
		}

		// Token: 0x06004CC9 RID: 19657 RVA: 0x000EE7BC File Offset: 0x000EC9BC
		public static void Serialize(Stream stream, RevokeInvitationRequest instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.AgentId);
			}
			if (instance.HasChannelId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.ChannelId.GetSerializedSize());
				ChannelId.Serialize(stream, instance.ChannelId);
			}
			if (instance.HasInvitationId)
			{
				stream.WriteByte(25);
				binaryWriter.Write(instance.InvitationId);
			}
		}

		// Token: 0x06004CCA RID: 19658 RVA: 0x000EE848 File Offset: 0x000ECA48
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAgentId)
			{
				num += 1U;
				uint serializedSize = this.AgentId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasChannelId)
			{
				num += 1U;
				uint serializedSize2 = this.ChannelId.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasInvitationId)
			{
				num += 1U;
				num += 8U;
			}
			return num;
		}

		// Token: 0x040018FE RID: 6398
		public bool HasAgentId;

		// Token: 0x040018FF RID: 6399
		private GameAccountHandle _AgentId;

		// Token: 0x04001900 RID: 6400
		public bool HasChannelId;

		// Token: 0x04001901 RID: 6401
		private ChannelId _ChannelId;

		// Token: 0x04001902 RID: 6402
		public bool HasInvitationId;

		// Token: 0x04001903 RID: 6403
		private ulong _InvitationId;
	}
}
