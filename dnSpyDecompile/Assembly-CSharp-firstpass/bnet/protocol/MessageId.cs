using System;
using System.IO;

namespace bnet.protocol
{
	// Token: 0x020002AB RID: 683
	public class MessageId : IProtoBuf
	{
		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x06002769 RID: 10089 RVA: 0x0008BAE0 File Offset: 0x00089CE0
		// (set) Token: 0x0600276A RID: 10090 RVA: 0x0008BAE8 File Offset: 0x00089CE8
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

		// Token: 0x0600276B RID: 10091 RVA: 0x0008BAF8 File Offset: 0x00089CF8
		public void SetEpoch(ulong val)
		{
			this.Epoch = val;
		}

		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x0600276C RID: 10092 RVA: 0x0008BB01 File Offset: 0x00089D01
		// (set) Token: 0x0600276D RID: 10093 RVA: 0x0008BB09 File Offset: 0x00089D09
		public ulong Position
		{
			get
			{
				return this._Position;
			}
			set
			{
				this._Position = value;
				this.HasPosition = true;
			}
		}

		// Token: 0x0600276E RID: 10094 RVA: 0x0008BB19 File Offset: 0x00089D19
		public void SetPosition(ulong val)
		{
			this.Position = val;
		}

		// Token: 0x0600276F RID: 10095 RVA: 0x0008BB24 File Offset: 0x00089D24
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasEpoch)
			{
				num ^= this.Epoch.GetHashCode();
			}
			if (this.HasPosition)
			{
				num ^= this.Position.GetHashCode();
			}
			return num;
		}

		// Token: 0x06002770 RID: 10096 RVA: 0x0008BB70 File Offset: 0x00089D70
		public override bool Equals(object obj)
		{
			MessageId messageId = obj as MessageId;
			return messageId != null && this.HasEpoch == messageId.HasEpoch && (!this.HasEpoch || this.Epoch.Equals(messageId.Epoch)) && this.HasPosition == messageId.HasPosition && (!this.HasPosition || this.Position.Equals(messageId.Position));
		}

		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x06002771 RID: 10097 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002772 RID: 10098 RVA: 0x0008BBE6 File Offset: 0x00089DE6
		public static MessageId ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<MessageId>(bs, 0, -1);
		}

		// Token: 0x06002773 RID: 10099 RVA: 0x0008BBF0 File Offset: 0x00089DF0
		public void Deserialize(Stream stream)
		{
			MessageId.Deserialize(stream, this);
		}

		// Token: 0x06002774 RID: 10100 RVA: 0x0008BBFA File Offset: 0x00089DFA
		public static MessageId Deserialize(Stream stream, MessageId instance)
		{
			return MessageId.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06002775 RID: 10101 RVA: 0x0008BC08 File Offset: 0x00089E08
		public static MessageId DeserializeLengthDelimited(Stream stream)
		{
			MessageId messageId = new MessageId();
			MessageId.DeserializeLengthDelimited(stream, messageId);
			return messageId;
		}

		// Token: 0x06002776 RID: 10102 RVA: 0x0008BC24 File Offset: 0x00089E24
		public static MessageId DeserializeLengthDelimited(Stream stream, MessageId instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return MessageId.Deserialize(stream, instance, num);
		}

		// Token: 0x06002777 RID: 10103 RVA: 0x0008BC4C File Offset: 0x00089E4C
		public static MessageId Deserialize(Stream stream, MessageId instance, long limit)
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
						instance.Position = ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.Epoch = ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06002778 RID: 10104 RVA: 0x0008BCE3 File Offset: 0x00089EE3
		public void Serialize(Stream stream)
		{
			MessageId.Serialize(stream, this);
		}

		// Token: 0x06002779 RID: 10105 RVA: 0x0008BCEC File Offset: 0x00089EEC
		public static void Serialize(Stream stream, MessageId instance)
		{
			if (instance.HasEpoch)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, instance.Epoch);
			}
			if (instance.HasPosition)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, instance.Position);
			}
		}

		// Token: 0x0600277A RID: 10106 RVA: 0x0008BD28 File Offset: 0x00089F28
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasEpoch)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.Epoch);
			}
			if (this.HasPosition)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.Position);
			}
			return num;
		}

		// Token: 0x04001135 RID: 4405
		public bool HasEpoch;

		// Token: 0x04001136 RID: 4406
		private ulong _Epoch;

		// Token: 0x04001137 RID: 4407
		public bool HasPosition;

		// Token: 0x04001138 RID: 4408
		private ulong _Position;
	}
}
