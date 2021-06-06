using System;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.channel.v2
{
	// Token: 0x02000463 RID: 1123
	public class GetJoinVoiceTokenRequest : IProtoBuf
	{
		// Token: 0x17000E58 RID: 3672
		// (get) Token: 0x06004CDF RID: 19679 RVA: 0x000EEBAE File Offset: 0x000ECDAE
		// (set) Token: 0x06004CE0 RID: 19680 RVA: 0x000EEBB6 File Offset: 0x000ECDB6
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

		// Token: 0x06004CE1 RID: 19681 RVA: 0x000EEBC9 File Offset: 0x000ECDC9
		public void SetAgentId(GameAccountHandle val)
		{
			this.AgentId = val;
		}

		// Token: 0x17000E59 RID: 3673
		// (get) Token: 0x06004CE2 RID: 19682 RVA: 0x000EEBD2 File Offset: 0x000ECDD2
		// (set) Token: 0x06004CE3 RID: 19683 RVA: 0x000EEBDA File Offset: 0x000ECDDA
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

		// Token: 0x06004CE4 RID: 19684 RVA: 0x000EEBED File Offset: 0x000ECDED
		public void SetChannelId(ChannelId val)
		{
			this.ChannelId = val;
		}

		// Token: 0x06004CE5 RID: 19685 RVA: 0x000EEBF8 File Offset: 0x000ECDF8
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
			return num;
		}

		// Token: 0x06004CE6 RID: 19686 RVA: 0x000EEC40 File Offset: 0x000ECE40
		public override bool Equals(object obj)
		{
			GetJoinVoiceTokenRequest getJoinVoiceTokenRequest = obj as GetJoinVoiceTokenRequest;
			return getJoinVoiceTokenRequest != null && this.HasAgentId == getJoinVoiceTokenRequest.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(getJoinVoiceTokenRequest.AgentId)) && this.HasChannelId == getJoinVoiceTokenRequest.HasChannelId && (!this.HasChannelId || this.ChannelId.Equals(getJoinVoiceTokenRequest.ChannelId));
		}

		// Token: 0x17000E5A RID: 3674
		// (get) Token: 0x06004CE7 RID: 19687 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004CE8 RID: 19688 RVA: 0x000EECB0 File Offset: 0x000ECEB0
		public static GetJoinVoiceTokenRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetJoinVoiceTokenRequest>(bs, 0, -1);
		}

		// Token: 0x06004CE9 RID: 19689 RVA: 0x000EECBA File Offset: 0x000ECEBA
		public void Deserialize(Stream stream)
		{
			GetJoinVoiceTokenRequest.Deserialize(stream, this);
		}

		// Token: 0x06004CEA RID: 19690 RVA: 0x000EECC4 File Offset: 0x000ECEC4
		public static GetJoinVoiceTokenRequest Deserialize(Stream stream, GetJoinVoiceTokenRequest instance)
		{
			return GetJoinVoiceTokenRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004CEB RID: 19691 RVA: 0x000EECD0 File Offset: 0x000ECED0
		public static GetJoinVoiceTokenRequest DeserializeLengthDelimited(Stream stream)
		{
			GetJoinVoiceTokenRequest getJoinVoiceTokenRequest = new GetJoinVoiceTokenRequest();
			GetJoinVoiceTokenRequest.DeserializeLengthDelimited(stream, getJoinVoiceTokenRequest);
			return getJoinVoiceTokenRequest;
		}

		// Token: 0x06004CEC RID: 19692 RVA: 0x000EECEC File Offset: 0x000ECEEC
		public static GetJoinVoiceTokenRequest DeserializeLengthDelimited(Stream stream, GetJoinVoiceTokenRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetJoinVoiceTokenRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06004CED RID: 19693 RVA: 0x000EED14 File Offset: 0x000ECF14
		public static GetJoinVoiceTokenRequest Deserialize(Stream stream, GetJoinVoiceTokenRequest instance, long limit)
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
				else if (num != 10)
				{
					if (num != 18)
					{
						Key key = ProtocolParser.ReadKey((byte)num, stream);
						if (key.Field == 0U)
						{
							throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
						}
						ProtocolParser.SkipKey(stream, key);
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

		// Token: 0x06004CEE RID: 19694 RVA: 0x000EEDE6 File Offset: 0x000ECFE6
		public void Serialize(Stream stream)
		{
			GetJoinVoiceTokenRequest.Serialize(stream, this);
		}

		// Token: 0x06004CEF RID: 19695 RVA: 0x000EEDF0 File Offset: 0x000ECFF0
		public static void Serialize(Stream stream, GetJoinVoiceTokenRequest instance)
		{
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
		}

		// Token: 0x06004CF0 RID: 19696 RVA: 0x000EEE58 File Offset: 0x000ED058
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
			return num;
		}

		// Token: 0x04001908 RID: 6408
		public bool HasAgentId;

		// Token: 0x04001909 RID: 6409
		private GameAccountHandle _AgentId;

		// Token: 0x0400190A RID: 6410
		public bool HasChannelId;

		// Token: 0x0400190B RID: 6411
		private ChannelId _ChannelId;
	}
}
