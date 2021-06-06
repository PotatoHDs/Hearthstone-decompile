using System;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.channel.v2
{
	// Token: 0x0200046B RID: 1131
	public class SendMessageNotification : IProtoBuf
	{
		// Token: 0x17000E81 RID: 3713
		// (get) Token: 0x06004DA6 RID: 19878 RVA: 0x000F1174 File Offset: 0x000EF374
		// (set) Token: 0x06004DA7 RID: 19879 RVA: 0x000F117C File Offset: 0x000EF37C
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

		// Token: 0x06004DA8 RID: 19880 RVA: 0x000F118F File Offset: 0x000EF38F
		public void SetAgentId(GameAccountHandle val)
		{
			this.AgentId = val;
		}

		// Token: 0x17000E82 RID: 3714
		// (get) Token: 0x06004DA9 RID: 19881 RVA: 0x000F1198 File Offset: 0x000EF398
		// (set) Token: 0x06004DAA RID: 19882 RVA: 0x000F11A0 File Offset: 0x000EF3A0
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

		// Token: 0x06004DAB RID: 19883 RVA: 0x000F11B3 File Offset: 0x000EF3B3
		public void SetSubscriberId(GameAccountHandle val)
		{
			this.SubscriberId = val;
		}

		// Token: 0x17000E83 RID: 3715
		// (get) Token: 0x06004DAC RID: 19884 RVA: 0x000F11BC File Offset: 0x000EF3BC
		// (set) Token: 0x06004DAD RID: 19885 RVA: 0x000F11C4 File Offset: 0x000EF3C4
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

		// Token: 0x06004DAE RID: 19886 RVA: 0x000F11D7 File Offset: 0x000EF3D7
		public void SetChannelId(ChannelId val)
		{
			this.ChannelId = val;
		}

		// Token: 0x17000E84 RID: 3716
		// (get) Token: 0x06004DAF RID: 19887 RVA: 0x000F11E0 File Offset: 0x000EF3E0
		// (set) Token: 0x06004DB0 RID: 19888 RVA: 0x000F11E8 File Offset: 0x000EF3E8
		public ChannelMessage Message
		{
			get
			{
				return this._Message;
			}
			set
			{
				this._Message = value;
				this.HasMessage = (value != null);
			}
		}

		// Token: 0x06004DB1 RID: 19889 RVA: 0x000F11FB File Offset: 0x000EF3FB
		public void SetMessage(ChannelMessage val)
		{
			this.Message = val;
		}

		// Token: 0x06004DB2 RID: 19890 RVA: 0x000F1204 File Offset: 0x000EF404
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
			if (this.HasChannelId)
			{
				num ^= this.ChannelId.GetHashCode();
			}
			if (this.HasMessage)
			{
				num ^= this.Message.GetHashCode();
			}
			return num;
		}

		// Token: 0x06004DB3 RID: 19891 RVA: 0x000F1278 File Offset: 0x000EF478
		public override bool Equals(object obj)
		{
			SendMessageNotification sendMessageNotification = obj as SendMessageNotification;
			return sendMessageNotification != null && this.HasAgentId == sendMessageNotification.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(sendMessageNotification.AgentId)) && this.HasSubscriberId == sendMessageNotification.HasSubscriberId && (!this.HasSubscriberId || this.SubscriberId.Equals(sendMessageNotification.SubscriberId)) && this.HasChannelId == sendMessageNotification.HasChannelId && (!this.HasChannelId || this.ChannelId.Equals(sendMessageNotification.ChannelId)) && this.HasMessage == sendMessageNotification.HasMessage && (!this.HasMessage || this.Message.Equals(sendMessageNotification.Message));
		}

		// Token: 0x17000E85 RID: 3717
		// (get) Token: 0x06004DB4 RID: 19892 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004DB5 RID: 19893 RVA: 0x000F133E File Offset: 0x000EF53E
		public static SendMessageNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SendMessageNotification>(bs, 0, -1);
		}

		// Token: 0x06004DB6 RID: 19894 RVA: 0x000F1348 File Offset: 0x000EF548
		public void Deserialize(Stream stream)
		{
			SendMessageNotification.Deserialize(stream, this);
		}

		// Token: 0x06004DB7 RID: 19895 RVA: 0x000F1352 File Offset: 0x000EF552
		public static SendMessageNotification Deserialize(Stream stream, SendMessageNotification instance)
		{
			return SendMessageNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004DB8 RID: 19896 RVA: 0x000F1360 File Offset: 0x000EF560
		public static SendMessageNotification DeserializeLengthDelimited(Stream stream)
		{
			SendMessageNotification sendMessageNotification = new SendMessageNotification();
			SendMessageNotification.DeserializeLengthDelimited(stream, sendMessageNotification);
			return sendMessageNotification;
		}

		// Token: 0x06004DB9 RID: 19897 RVA: 0x000F137C File Offset: 0x000EF57C
		public static SendMessageNotification DeserializeLengthDelimited(Stream stream, SendMessageNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SendMessageNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x06004DBA RID: 19898 RVA: 0x000F13A4 File Offset: 0x000EF5A4
		public static SendMessageNotification Deserialize(Stream stream, SendMessageNotification instance, long limit)
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
					else if (num != 26)
					{
						if (num == 34)
						{
							if (instance.Message == null)
							{
								instance.Message = ChannelMessage.DeserializeLengthDelimited(stream);
								continue;
							}
							ChannelMessage.DeserializeLengthDelimited(stream, instance.Message);
							continue;
						}
					}
					else
					{
						if (instance.ChannelId == null)
						{
							instance.ChannelId = ChannelId.DeserializeLengthDelimited(stream);
							continue;
						}
						ChannelId.DeserializeLengthDelimited(stream, instance.ChannelId);
						continue;
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

		// Token: 0x06004DBB RID: 19899 RVA: 0x000F14E9 File Offset: 0x000EF6E9
		public void Serialize(Stream stream)
		{
			SendMessageNotification.Serialize(stream, this);
		}

		// Token: 0x06004DBC RID: 19900 RVA: 0x000F14F4 File Offset: 0x000EF6F4
		public static void Serialize(Stream stream, SendMessageNotification instance)
		{
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
			if (instance.HasChannelId)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.ChannelId.GetSerializedSize());
				ChannelId.Serialize(stream, instance.ChannelId);
			}
			if (instance.HasMessage)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.Message.GetSerializedSize());
				ChannelMessage.Serialize(stream, instance.Message);
			}
		}

		// Token: 0x06004DBD RID: 19901 RVA: 0x000F15B8 File Offset: 0x000EF7B8
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
			if (this.HasChannelId)
			{
				num += 1U;
				uint serializedSize3 = this.ChannelId.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			if (this.HasMessage)
			{
				num += 1U;
				uint serializedSize4 = this.Message.GetSerializedSize();
				num += serializedSize4 + ProtocolParser.SizeOfUInt32(serializedSize4);
			}
			return num;
		}

		// Token: 0x04001940 RID: 6464
		public bool HasAgentId;

		// Token: 0x04001941 RID: 6465
		private GameAccountHandle _AgentId;

		// Token: 0x04001942 RID: 6466
		public bool HasSubscriberId;

		// Token: 0x04001943 RID: 6467
		private GameAccountHandle _SubscriberId;

		// Token: 0x04001944 RID: 6468
		public bool HasChannelId;

		// Token: 0x04001945 RID: 6469
		private ChannelId _ChannelId;

		// Token: 0x04001946 RID: 6470
		public bool HasMessage;

		// Token: 0x04001947 RID: 6471
		private ChannelMessage _Message;
	}
}
