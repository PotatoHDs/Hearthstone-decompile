using System;
using System.IO;
using bnet.protocol.account.v1;
using bnet.protocol.Types;

namespace bnet.protocol.channel.v2
{
	// Token: 0x0200046C RID: 1132
	public class TypingIndicatorNotification : IProtoBuf
	{
		// Token: 0x17000E86 RID: 3718
		// (get) Token: 0x06004DBF RID: 19903 RVA: 0x000F1657 File Offset: 0x000EF857
		// (set) Token: 0x06004DC0 RID: 19904 RVA: 0x000F165F File Offset: 0x000EF85F
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

		// Token: 0x06004DC1 RID: 19905 RVA: 0x000F1672 File Offset: 0x000EF872
		public void SetAgentId(GameAccountHandle val)
		{
			this.AgentId = val;
		}

		// Token: 0x17000E87 RID: 3719
		// (get) Token: 0x06004DC2 RID: 19906 RVA: 0x000F167B File Offset: 0x000EF87B
		// (set) Token: 0x06004DC3 RID: 19907 RVA: 0x000F1683 File Offset: 0x000EF883
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

		// Token: 0x06004DC4 RID: 19908 RVA: 0x000F1696 File Offset: 0x000EF896
		public void SetSubscriberId(GameAccountHandle val)
		{
			this.SubscriberId = val;
		}

		// Token: 0x17000E88 RID: 3720
		// (get) Token: 0x06004DC5 RID: 19909 RVA: 0x000F169F File Offset: 0x000EF89F
		// (set) Token: 0x06004DC6 RID: 19910 RVA: 0x000F16A7 File Offset: 0x000EF8A7
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

		// Token: 0x06004DC7 RID: 19911 RVA: 0x000F16BA File Offset: 0x000EF8BA
		public void SetChannelId(ChannelId val)
		{
			this.ChannelId = val;
		}

		// Token: 0x17000E89 RID: 3721
		// (get) Token: 0x06004DC8 RID: 19912 RVA: 0x000F16C3 File Offset: 0x000EF8C3
		// (set) Token: 0x06004DC9 RID: 19913 RVA: 0x000F16CB File Offset: 0x000EF8CB
		public GameAccountHandle AuthorId
		{
			get
			{
				return this._AuthorId;
			}
			set
			{
				this._AuthorId = value;
				this.HasAuthorId = (value != null);
			}
		}

		// Token: 0x06004DCA RID: 19914 RVA: 0x000F16DE File Offset: 0x000EF8DE
		public void SetAuthorId(GameAccountHandle val)
		{
			this.AuthorId = val;
		}

		// Token: 0x17000E8A RID: 3722
		// (get) Token: 0x06004DCB RID: 19915 RVA: 0x000F16E7 File Offset: 0x000EF8E7
		// (set) Token: 0x06004DCC RID: 19916 RVA: 0x000F16EF File Offset: 0x000EF8EF
		public ulong Epoch
		{
			get
			{
				return this._Epoch;
			}
			set
			{
				this._Epoch = value;
				this.HasEpoch = true;
			}
		}

		// Token: 0x06004DCD RID: 19917 RVA: 0x000F16FF File Offset: 0x000EF8FF
		public void SetEpoch(ulong val)
		{
			this.Epoch = val;
		}

		// Token: 0x17000E8B RID: 3723
		// (get) Token: 0x06004DCE RID: 19918 RVA: 0x000F1708 File Offset: 0x000EF908
		// (set) Token: 0x06004DCF RID: 19919 RVA: 0x000F1710 File Offset: 0x000EF910
		public TypingIndicator Action
		{
			get
			{
				return this._Action;
			}
			set
			{
				this._Action = value;
				this.HasAction = true;
			}
		}

		// Token: 0x06004DD0 RID: 19920 RVA: 0x000F1720 File Offset: 0x000EF920
		public void SetAction(TypingIndicator val)
		{
			this.Action = val;
		}

		// Token: 0x06004DD1 RID: 19921 RVA: 0x000F172C File Offset: 0x000EF92C
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
			if (this.HasAuthorId)
			{
				num ^= this.AuthorId.GetHashCode();
			}
			if (this.HasEpoch)
			{
				num ^= this.Epoch.GetHashCode();
			}
			if (this.HasAction)
			{
				num ^= this.Action.GetHashCode();
			}
			return num;
		}

		// Token: 0x06004DD2 RID: 19922 RVA: 0x000F17D8 File Offset: 0x000EF9D8
		public override bool Equals(object obj)
		{
			TypingIndicatorNotification typingIndicatorNotification = obj as TypingIndicatorNotification;
			return typingIndicatorNotification != null && this.HasAgentId == typingIndicatorNotification.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(typingIndicatorNotification.AgentId)) && this.HasSubscriberId == typingIndicatorNotification.HasSubscriberId && (!this.HasSubscriberId || this.SubscriberId.Equals(typingIndicatorNotification.SubscriberId)) && this.HasChannelId == typingIndicatorNotification.HasChannelId && (!this.HasChannelId || this.ChannelId.Equals(typingIndicatorNotification.ChannelId)) && this.HasAuthorId == typingIndicatorNotification.HasAuthorId && (!this.HasAuthorId || this.AuthorId.Equals(typingIndicatorNotification.AuthorId)) && this.HasEpoch == typingIndicatorNotification.HasEpoch && (!this.HasEpoch || this.Epoch.Equals(typingIndicatorNotification.Epoch)) && this.HasAction == typingIndicatorNotification.HasAction && (!this.HasAction || this.Action.Equals(typingIndicatorNotification.Action));
		}

		// Token: 0x17000E8C RID: 3724
		// (get) Token: 0x06004DD3 RID: 19923 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004DD4 RID: 19924 RVA: 0x000F1905 File Offset: 0x000EFB05
		public static TypingIndicatorNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<TypingIndicatorNotification>(bs, 0, -1);
		}

		// Token: 0x06004DD5 RID: 19925 RVA: 0x000F190F File Offset: 0x000EFB0F
		public void Deserialize(Stream stream)
		{
			TypingIndicatorNotification.Deserialize(stream, this);
		}

		// Token: 0x06004DD6 RID: 19926 RVA: 0x000F1919 File Offset: 0x000EFB19
		public static TypingIndicatorNotification Deserialize(Stream stream, TypingIndicatorNotification instance)
		{
			return TypingIndicatorNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004DD7 RID: 19927 RVA: 0x000F1924 File Offset: 0x000EFB24
		public static TypingIndicatorNotification DeserializeLengthDelimited(Stream stream)
		{
			TypingIndicatorNotification typingIndicatorNotification = new TypingIndicatorNotification();
			TypingIndicatorNotification.DeserializeLengthDelimited(stream, typingIndicatorNotification);
			return typingIndicatorNotification;
		}

		// Token: 0x06004DD8 RID: 19928 RVA: 0x000F1940 File Offset: 0x000EFB40
		public static TypingIndicatorNotification DeserializeLengthDelimited(Stream stream, TypingIndicatorNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return TypingIndicatorNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x06004DD9 RID: 19929 RVA: 0x000F1968 File Offset: 0x000EFB68
		public static TypingIndicatorNotification Deserialize(Stream stream, TypingIndicatorNotification instance, long limit)
		{
			instance.Action = TypingIndicator.TYPING_START;
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
					if (num <= 26)
					{
						if (num != 10)
						{
							if (num != 18)
							{
								if (num == 26)
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
							else
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
					else if (num != 34)
					{
						if (num == 40)
						{
							instance.Epoch = ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 48)
						{
							instance.Action = (TypingIndicator)ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					else
					{
						if (instance.AuthorId == null)
						{
							instance.AuthorId = GameAccountHandle.DeserializeLengthDelimited(stream);
							continue;
						}
						GameAccountHandle.DeserializeLengthDelimited(stream, instance.AuthorId);
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

		// Token: 0x06004DDA RID: 19930 RVA: 0x000F1AEA File Offset: 0x000EFCEA
		public void Serialize(Stream stream)
		{
			TypingIndicatorNotification.Serialize(stream, this);
		}

		// Token: 0x06004DDB RID: 19931 RVA: 0x000F1AF4 File Offset: 0x000EFCF4
		public static void Serialize(Stream stream, TypingIndicatorNotification instance)
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
			if (instance.HasAuthorId)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.AuthorId.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.AuthorId);
			}
			if (instance.HasEpoch)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, instance.Epoch);
			}
			if (instance.HasAction)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Action));
			}
		}

		// Token: 0x06004DDC RID: 19932 RVA: 0x000F1BF0 File Offset: 0x000EFDF0
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
			if (this.HasAuthorId)
			{
				num += 1U;
				uint serializedSize4 = this.AuthorId.GetSerializedSize();
				num += serializedSize4 + ProtocolParser.SizeOfUInt32(serializedSize4);
			}
			if (this.HasEpoch)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.Epoch);
			}
			if (this.HasAction)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Action));
			}
			return num;
		}

		// Token: 0x04001948 RID: 6472
		public bool HasAgentId;

		// Token: 0x04001949 RID: 6473
		private GameAccountHandle _AgentId;

		// Token: 0x0400194A RID: 6474
		public bool HasSubscriberId;

		// Token: 0x0400194B RID: 6475
		private GameAccountHandle _SubscriberId;

		// Token: 0x0400194C RID: 6476
		public bool HasChannelId;

		// Token: 0x0400194D RID: 6477
		private ChannelId _ChannelId;

		// Token: 0x0400194E RID: 6478
		public bool HasAuthorId;

		// Token: 0x0400194F RID: 6479
		private GameAccountHandle _AuthorId;

		// Token: 0x04001950 RID: 6480
		public bool HasEpoch;

		// Token: 0x04001951 RID: 6481
		private ulong _Epoch;

		// Token: 0x04001952 RID: 6482
		public bool HasAction;

		// Token: 0x04001953 RID: 6483
		private TypingIndicator _Action;
	}
}
