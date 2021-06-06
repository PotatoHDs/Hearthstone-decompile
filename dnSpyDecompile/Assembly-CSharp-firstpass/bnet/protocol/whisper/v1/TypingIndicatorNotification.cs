using System;
using System.IO;
using bnet.protocol.account.v1;
using bnet.protocol.Types;

namespace bnet.protocol.whisper.v1
{
	// Token: 0x020002D8 RID: 728
	public class TypingIndicatorNotification : IProtoBuf
	{
		// Token: 0x1700079C RID: 1948
		// (get) Token: 0x06002ACB RID: 10955 RVA: 0x0009493E File Offset: 0x00092B3E
		// (set) Token: 0x06002ACC RID: 10956 RVA: 0x00094946 File Offset: 0x00092B46
		public AccountId SubscriberId
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

		// Token: 0x06002ACD RID: 10957 RVA: 0x00094959 File Offset: 0x00092B59
		public void SetSubscriberId(AccountId val)
		{
			this.SubscriberId = val;
		}

		// Token: 0x1700079D RID: 1949
		// (get) Token: 0x06002ACE RID: 10958 RVA: 0x00094962 File Offset: 0x00092B62
		// (set) Token: 0x06002ACF RID: 10959 RVA: 0x0009496A File Offset: 0x00092B6A
		public AccountId SenderId
		{
			get
			{
				return this._SenderId;
			}
			set
			{
				this._SenderId = value;
				this.HasSenderId = (value != null);
			}
		}

		// Token: 0x06002AD0 RID: 10960 RVA: 0x0009497D File Offset: 0x00092B7D
		public void SetSenderId(AccountId val)
		{
			this.SenderId = val;
		}

		// Token: 0x1700079E RID: 1950
		// (get) Token: 0x06002AD1 RID: 10961 RVA: 0x00094986 File Offset: 0x00092B86
		// (set) Token: 0x06002AD2 RID: 10962 RVA: 0x0009498E File Offset: 0x00092B8E
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

		// Token: 0x06002AD3 RID: 10963 RVA: 0x0009499E File Offset: 0x00092B9E
		public void SetAction(TypingIndicator val)
		{
			this.Action = val;
		}

		// Token: 0x06002AD4 RID: 10964 RVA: 0x000949A8 File Offset: 0x00092BA8
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasSubscriberId)
			{
				num ^= this.SubscriberId.GetHashCode();
			}
			if (this.HasSenderId)
			{
				num ^= this.SenderId.GetHashCode();
			}
			if (this.HasAction)
			{
				num ^= this.Action.GetHashCode();
			}
			return num;
		}

		// Token: 0x06002AD5 RID: 10965 RVA: 0x00094A10 File Offset: 0x00092C10
		public override bool Equals(object obj)
		{
			TypingIndicatorNotification typingIndicatorNotification = obj as TypingIndicatorNotification;
			return typingIndicatorNotification != null && this.HasSubscriberId == typingIndicatorNotification.HasSubscriberId && (!this.HasSubscriberId || this.SubscriberId.Equals(typingIndicatorNotification.SubscriberId)) && this.HasSenderId == typingIndicatorNotification.HasSenderId && (!this.HasSenderId || this.SenderId.Equals(typingIndicatorNotification.SenderId)) && this.HasAction == typingIndicatorNotification.HasAction && (!this.HasAction || this.Action.Equals(typingIndicatorNotification.Action));
		}

		// Token: 0x1700079F RID: 1951
		// (get) Token: 0x06002AD6 RID: 10966 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002AD7 RID: 10967 RVA: 0x00094AB9 File Offset: 0x00092CB9
		public static TypingIndicatorNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<TypingIndicatorNotification>(bs, 0, -1);
		}

		// Token: 0x06002AD8 RID: 10968 RVA: 0x00094AC3 File Offset: 0x00092CC3
		public void Deserialize(Stream stream)
		{
			TypingIndicatorNotification.Deserialize(stream, this);
		}

		// Token: 0x06002AD9 RID: 10969 RVA: 0x00094ACD File Offset: 0x00092CCD
		public static TypingIndicatorNotification Deserialize(Stream stream, TypingIndicatorNotification instance)
		{
			return TypingIndicatorNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06002ADA RID: 10970 RVA: 0x00094AD8 File Offset: 0x00092CD8
		public static TypingIndicatorNotification DeserializeLengthDelimited(Stream stream)
		{
			TypingIndicatorNotification typingIndicatorNotification = new TypingIndicatorNotification();
			TypingIndicatorNotification.DeserializeLengthDelimited(stream, typingIndicatorNotification);
			return typingIndicatorNotification;
		}

		// Token: 0x06002ADB RID: 10971 RVA: 0x00094AF4 File Offset: 0x00092CF4
		public static TypingIndicatorNotification DeserializeLengthDelimited(Stream stream, TypingIndicatorNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return TypingIndicatorNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x06002ADC RID: 10972 RVA: 0x00094B1C File Offset: 0x00092D1C
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
				else if (num != 10)
				{
					if (num != 18)
					{
						if (num != 24)
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
							instance.Action = (TypingIndicator)ProtocolParser.ReadUInt64(stream);
						}
					}
					else if (instance.SenderId == null)
					{
						instance.SenderId = AccountId.DeserializeLengthDelimited(stream);
					}
					else
					{
						AccountId.DeserializeLengthDelimited(stream, instance.SenderId);
					}
				}
				else if (instance.SubscriberId == null)
				{
					instance.SubscriberId = AccountId.DeserializeLengthDelimited(stream);
				}
				else
				{
					AccountId.DeserializeLengthDelimited(stream, instance.SubscriberId);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06002ADD RID: 10973 RVA: 0x00094C0C File Offset: 0x00092E0C
		public void Serialize(Stream stream)
		{
			TypingIndicatorNotification.Serialize(stream, this);
		}

		// Token: 0x06002ADE RID: 10974 RVA: 0x00094C18 File Offset: 0x00092E18
		public static void Serialize(Stream stream, TypingIndicatorNotification instance)
		{
			if (instance.HasSubscriberId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.SubscriberId.GetSerializedSize());
				AccountId.Serialize(stream, instance.SubscriberId);
			}
			if (instance.HasSenderId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.SenderId.GetSerializedSize());
				AccountId.Serialize(stream, instance.SenderId);
			}
			if (instance.HasAction)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Action));
			}
		}

		// Token: 0x06002ADF RID: 10975 RVA: 0x00094C9C File Offset: 0x00092E9C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasSubscriberId)
			{
				num += 1U;
				uint serializedSize = this.SubscriberId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasSenderId)
			{
				num += 1U;
				uint serializedSize2 = this.SenderId.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasAction)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Action));
			}
			return num;
		}

		// Token: 0x04001208 RID: 4616
		public bool HasSubscriberId;

		// Token: 0x04001209 RID: 4617
		private AccountId _SubscriberId;

		// Token: 0x0400120A RID: 4618
		public bool HasSenderId;

		// Token: 0x0400120B RID: 4619
		private AccountId _SenderId;

		// Token: 0x0400120C RID: 4620
		public bool HasAction;

		// Token: 0x0400120D RID: 4621
		private TypingIndicator _Action;
	}
}
