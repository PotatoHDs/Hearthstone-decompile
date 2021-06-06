using System;
using System.IO;
using System.Text;

namespace bnet.protocol.channel.v1
{
	// Token: 0x020004CF RID: 1231
	public class SendMessageNotification : IProtoBuf
	{
		// Token: 0x1700103D RID: 4157
		// (get) Token: 0x0600566A RID: 22122 RVA: 0x001091D5 File Offset: 0x001073D5
		// (set) Token: 0x0600566B RID: 22123 RVA: 0x001091DD File Offset: 0x001073DD
		public EntityId AgentId
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

		// Token: 0x0600566C RID: 22124 RVA: 0x001091F0 File Offset: 0x001073F0
		public void SetAgentId(EntityId val)
		{
			this.AgentId = val;
		}

		// Token: 0x1700103E RID: 4158
		// (get) Token: 0x0600566D RID: 22125 RVA: 0x001091F9 File Offset: 0x001073F9
		// (set) Token: 0x0600566E RID: 22126 RVA: 0x00109201 File Offset: 0x00107401
		public Message Message { get; set; }

		// Token: 0x0600566F RID: 22127 RVA: 0x0010920A File Offset: 0x0010740A
		public void SetMessage(Message val)
		{
			this.Message = val;
		}

		// Token: 0x1700103F RID: 4159
		// (get) Token: 0x06005670 RID: 22128 RVA: 0x00109213 File Offset: 0x00107413
		// (set) Token: 0x06005671 RID: 22129 RVA: 0x0010921B File Offset: 0x0010741B
		public ulong RequiredPrivileges
		{
			get
			{
				return this._RequiredPrivileges;
			}
			set
			{
				this._RequiredPrivileges = value;
				this.HasRequiredPrivileges = true;
			}
		}

		// Token: 0x06005672 RID: 22130 RVA: 0x0010922B File Offset: 0x0010742B
		public void SetRequiredPrivileges(ulong val)
		{
			this.RequiredPrivileges = val;
		}

		// Token: 0x17001040 RID: 4160
		// (get) Token: 0x06005673 RID: 22131 RVA: 0x00109234 File Offset: 0x00107434
		// (set) Token: 0x06005674 RID: 22132 RVA: 0x0010923C File Offset: 0x0010743C
		public string BattleTag
		{
			get
			{
				return this._BattleTag;
			}
			set
			{
				this._BattleTag = value;
				this.HasBattleTag = (value != null);
			}
		}

		// Token: 0x06005675 RID: 22133 RVA: 0x0010924F File Offset: 0x0010744F
		public void SetBattleTag(string val)
		{
			this.BattleTag = val;
		}

		// Token: 0x17001041 RID: 4161
		// (get) Token: 0x06005676 RID: 22134 RVA: 0x00109258 File Offset: 0x00107458
		// (set) Token: 0x06005677 RID: 22135 RVA: 0x00109260 File Offset: 0x00107460
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

		// Token: 0x06005678 RID: 22136 RVA: 0x00109273 File Offset: 0x00107473
		public void SetChannelId(ChannelId val)
		{
			this.ChannelId = val;
		}

		// Token: 0x17001042 RID: 4162
		// (get) Token: 0x06005679 RID: 22137 RVA: 0x0010927C File Offset: 0x0010747C
		// (set) Token: 0x0600567A RID: 22138 RVA: 0x00109284 File Offset: 0x00107484
		public SubscriberId SubscriberId
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

		// Token: 0x0600567B RID: 22139 RVA: 0x00109297 File Offset: 0x00107497
		public void SetSubscriberId(SubscriberId val)
		{
			this.SubscriberId = val;
		}

		// Token: 0x0600567C RID: 22140 RVA: 0x001092A0 File Offset: 0x001074A0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAgentId)
			{
				num ^= this.AgentId.GetHashCode();
			}
			num ^= this.Message.GetHashCode();
			if (this.HasRequiredPrivileges)
			{
				num ^= this.RequiredPrivileges.GetHashCode();
			}
			if (this.HasBattleTag)
			{
				num ^= this.BattleTag.GetHashCode();
			}
			if (this.HasChannelId)
			{
				num ^= this.ChannelId.GetHashCode();
			}
			if (this.HasSubscriberId)
			{
				num ^= this.SubscriberId.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600567D RID: 22141 RVA: 0x0010933C File Offset: 0x0010753C
		public override bool Equals(object obj)
		{
			SendMessageNotification sendMessageNotification = obj as SendMessageNotification;
			return sendMessageNotification != null && this.HasAgentId == sendMessageNotification.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(sendMessageNotification.AgentId)) && this.Message.Equals(sendMessageNotification.Message) && this.HasRequiredPrivileges == sendMessageNotification.HasRequiredPrivileges && (!this.HasRequiredPrivileges || this.RequiredPrivileges.Equals(sendMessageNotification.RequiredPrivileges)) && this.HasBattleTag == sendMessageNotification.HasBattleTag && (!this.HasBattleTag || this.BattleTag.Equals(sendMessageNotification.BattleTag)) && this.HasChannelId == sendMessageNotification.HasChannelId && (!this.HasChannelId || this.ChannelId.Equals(sendMessageNotification.ChannelId)) && this.HasSubscriberId == sendMessageNotification.HasSubscriberId && (!this.HasSubscriberId || this.SubscriberId.Equals(sendMessageNotification.SubscriberId));
		}

		// Token: 0x17001043 RID: 4163
		// (get) Token: 0x0600567E RID: 22142 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600567F RID: 22143 RVA: 0x00109445 File Offset: 0x00107645
		public static SendMessageNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SendMessageNotification>(bs, 0, -1);
		}

		// Token: 0x06005680 RID: 22144 RVA: 0x0010944F File Offset: 0x0010764F
		public void Deserialize(Stream stream)
		{
			SendMessageNotification.Deserialize(stream, this);
		}

		// Token: 0x06005681 RID: 22145 RVA: 0x00109459 File Offset: 0x00107659
		public static SendMessageNotification Deserialize(Stream stream, SendMessageNotification instance)
		{
			return SendMessageNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005682 RID: 22146 RVA: 0x00109464 File Offset: 0x00107664
		public static SendMessageNotification DeserializeLengthDelimited(Stream stream)
		{
			SendMessageNotification sendMessageNotification = new SendMessageNotification();
			SendMessageNotification.DeserializeLengthDelimited(stream, sendMessageNotification);
			return sendMessageNotification;
		}

		// Token: 0x06005683 RID: 22147 RVA: 0x00109480 File Offset: 0x00107680
		public static SendMessageNotification DeserializeLengthDelimited(Stream stream, SendMessageNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SendMessageNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x06005684 RID: 22148 RVA: 0x001094A8 File Offset: 0x001076A8
		public static SendMessageNotification Deserialize(Stream stream, SendMessageNotification instance, long limit)
		{
			instance.RequiredPrivileges = 0UL;
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
					if (num <= 24)
					{
						if (num != 10)
						{
							if (num != 18)
							{
								if (num == 24)
								{
									instance.RequiredPrivileges = ProtocolParser.ReadUInt64(stream);
									continue;
								}
							}
							else
							{
								if (instance.Message == null)
								{
									instance.Message = Message.DeserializeLengthDelimited(stream);
									continue;
								}
								Message.DeserializeLengthDelimited(stream, instance.Message);
								continue;
							}
						}
						else
						{
							if (instance.AgentId == null)
							{
								instance.AgentId = EntityId.DeserializeLengthDelimited(stream);
								continue;
							}
							EntityId.DeserializeLengthDelimited(stream, instance.AgentId);
							continue;
						}
					}
					else
					{
						if (num == 34)
						{
							instance.BattleTag = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num != 42)
						{
							if (num == 50)
							{
								if (instance.SubscriberId == null)
								{
									instance.SubscriberId = SubscriberId.DeserializeLengthDelimited(stream);
									continue;
								}
								SubscriberId.DeserializeLengthDelimited(stream, instance.SubscriberId);
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

		// Token: 0x06005685 RID: 22149 RVA: 0x00109627 File Offset: 0x00107827
		public void Serialize(Stream stream)
		{
			SendMessageNotification.Serialize(stream, this);
		}

		// Token: 0x06005686 RID: 22150 RVA: 0x00109630 File Offset: 0x00107830
		public static void Serialize(Stream stream, SendMessageNotification instance)
		{
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				EntityId.Serialize(stream, instance.AgentId);
			}
			if (instance.Message == null)
			{
				throw new ArgumentNullException("Message", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteUInt32(stream, instance.Message.GetSerializedSize());
			Message.Serialize(stream, instance.Message);
			if (instance.HasRequiredPrivileges)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, instance.RequiredPrivileges);
			}
			if (instance.HasBattleTag)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.BattleTag));
			}
			if (instance.HasChannelId)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteUInt32(stream, instance.ChannelId.GetSerializedSize());
				ChannelId.Serialize(stream, instance.ChannelId);
			}
			if (instance.HasSubscriberId)
			{
				stream.WriteByte(50);
				ProtocolParser.WriteUInt32(stream, instance.SubscriberId.GetSerializedSize());
				SubscriberId.Serialize(stream, instance.SubscriberId);
			}
		}

		// Token: 0x06005687 RID: 22151 RVA: 0x00109744 File Offset: 0x00107944
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAgentId)
			{
				num += 1U;
				uint serializedSize = this.AgentId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			uint serializedSize2 = this.Message.GetSerializedSize();
			num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			if (this.HasRequiredPrivileges)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.RequiredPrivileges);
			}
			if (this.HasBattleTag)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.BattleTag);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasChannelId)
			{
				num += 1U;
				uint serializedSize3 = this.ChannelId.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			if (this.HasSubscriberId)
			{
				num += 1U;
				uint serializedSize4 = this.SubscriberId.GetSerializedSize();
				num += serializedSize4 + ProtocolParser.SizeOfUInt32(serializedSize4);
			}
			return num + 1U;
		}

		// Token: 0x04001B33 RID: 6963
		public bool HasAgentId;

		// Token: 0x04001B34 RID: 6964
		private EntityId _AgentId;

		// Token: 0x04001B36 RID: 6966
		public bool HasRequiredPrivileges;

		// Token: 0x04001B37 RID: 6967
		private ulong _RequiredPrivileges;

		// Token: 0x04001B38 RID: 6968
		public bool HasBattleTag;

		// Token: 0x04001B39 RID: 6969
		private string _BattleTag;

		// Token: 0x04001B3A RID: 6970
		public bool HasChannelId;

		// Token: 0x04001B3B RID: 6971
		private ChannelId _ChannelId;

		// Token: 0x04001B3C RID: 6972
		public bool HasSubscriberId;

		// Token: 0x04001B3D RID: 6973
		private SubscriberId _SubscriberId;
	}
}
