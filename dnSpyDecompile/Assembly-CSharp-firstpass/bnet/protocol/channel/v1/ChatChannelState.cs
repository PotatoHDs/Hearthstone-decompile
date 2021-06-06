using System;
using System.IO;
using System.Text;

namespace bnet.protocol.channel.v1
{
	// Token: 0x020004DF RID: 1247
	public class ChatChannelState : IProtoBuf
	{
		// Token: 0x17001094 RID: 4244
		// (get) Token: 0x06005801 RID: 22529 RVA: 0x0010DC1F File Offset: 0x0010BE1F
		// (set) Token: 0x06005802 RID: 22530 RVA: 0x0010DC27 File Offset: 0x0010BE27
		public string Identity
		{
			get
			{
				return this._Identity;
			}
			set
			{
				this._Identity = value;
				this.HasIdentity = (value != null);
			}
		}

		// Token: 0x06005803 RID: 22531 RVA: 0x0010DC3A File Offset: 0x0010BE3A
		public void SetIdentity(string val)
		{
			this.Identity = val;
		}

		// Token: 0x17001095 RID: 4245
		// (get) Token: 0x06005804 RID: 22532 RVA: 0x0010DC43 File Offset: 0x0010BE43
		// (set) Token: 0x06005805 RID: 22533 RVA: 0x0010DC4B File Offset: 0x0010BE4B
		public uint Locale
		{
			get
			{
				return this._Locale;
			}
			set
			{
				this._Locale = value;
				this.HasLocale = true;
			}
		}

		// Token: 0x06005806 RID: 22534 RVA: 0x0010DC5B File Offset: 0x0010BE5B
		public void SetLocale(uint val)
		{
			this.Locale = val;
		}

		// Token: 0x17001096 RID: 4246
		// (get) Token: 0x06005807 RID: 22535 RVA: 0x0010DC64 File Offset: 0x0010BE64
		// (set) Token: 0x06005808 RID: 22536 RVA: 0x0010DC6C File Offset: 0x0010BE6C
		public bool Public
		{
			get
			{
				return this._Public;
			}
			set
			{
				this._Public = value;
				this.HasPublic = true;
			}
		}

		// Token: 0x06005809 RID: 22537 RVA: 0x0010DC7C File Offset: 0x0010BE7C
		public void SetPublic(bool val)
		{
			this.Public = val;
		}

		// Token: 0x17001097 RID: 4247
		// (get) Token: 0x0600580A RID: 22538 RVA: 0x0010DC85 File Offset: 0x0010BE85
		// (set) Token: 0x0600580B RID: 22539 RVA: 0x0010DC8D File Offset: 0x0010BE8D
		public uint BucketIndex
		{
			get
			{
				return this._BucketIndex;
			}
			set
			{
				this._BucketIndex = value;
				this.HasBucketIndex = true;
			}
		}

		// Token: 0x0600580C RID: 22540 RVA: 0x0010DC9D File Offset: 0x0010BE9D
		public void SetBucketIndex(uint val)
		{
			this.BucketIndex = val;
		}

		// Token: 0x0600580D RID: 22541 RVA: 0x0010DCA8 File Offset: 0x0010BEA8
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasIdentity)
			{
				num ^= this.Identity.GetHashCode();
			}
			if (this.HasLocale)
			{
				num ^= this.Locale.GetHashCode();
			}
			if (this.HasPublic)
			{
				num ^= this.Public.GetHashCode();
			}
			if (this.HasBucketIndex)
			{
				num ^= this.BucketIndex.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600580E RID: 22542 RVA: 0x0010DD24 File Offset: 0x0010BF24
		public override bool Equals(object obj)
		{
			ChatChannelState chatChannelState = obj as ChatChannelState;
			return chatChannelState != null && this.HasIdentity == chatChannelState.HasIdentity && (!this.HasIdentity || this.Identity.Equals(chatChannelState.Identity)) && this.HasLocale == chatChannelState.HasLocale && (!this.HasLocale || this.Locale.Equals(chatChannelState.Locale)) && this.HasPublic == chatChannelState.HasPublic && (!this.HasPublic || this.Public.Equals(chatChannelState.Public)) && this.HasBucketIndex == chatChannelState.HasBucketIndex && (!this.HasBucketIndex || this.BucketIndex.Equals(chatChannelState.BucketIndex));
		}

		// Token: 0x17001098 RID: 4248
		// (get) Token: 0x0600580F RID: 22543 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005810 RID: 22544 RVA: 0x0010DDF3 File Offset: 0x0010BFF3
		public static ChatChannelState ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ChatChannelState>(bs, 0, -1);
		}

		// Token: 0x06005811 RID: 22545 RVA: 0x0010DDFD File Offset: 0x0010BFFD
		public void Deserialize(Stream stream)
		{
			ChatChannelState.Deserialize(stream, this);
		}

		// Token: 0x06005812 RID: 22546 RVA: 0x0010DE07 File Offset: 0x0010C007
		public static ChatChannelState Deserialize(Stream stream, ChatChannelState instance)
		{
			return ChatChannelState.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005813 RID: 22547 RVA: 0x0010DE14 File Offset: 0x0010C014
		public static ChatChannelState DeserializeLengthDelimited(Stream stream)
		{
			ChatChannelState chatChannelState = new ChatChannelState();
			ChatChannelState.DeserializeLengthDelimited(stream, chatChannelState);
			return chatChannelState;
		}

		// Token: 0x06005814 RID: 22548 RVA: 0x0010DE30 File Offset: 0x0010C030
		public static ChatChannelState DeserializeLengthDelimited(Stream stream, ChatChannelState instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ChatChannelState.Deserialize(stream, instance, num);
		}

		// Token: 0x06005815 RID: 22549 RVA: 0x0010DE58 File Offset: 0x0010C058
		public static ChatChannelState Deserialize(Stream stream, ChatChannelState instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			instance.Public = false;
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
					if (num <= 29)
					{
						if (num == 10)
						{
							instance.Identity = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 29)
						{
							instance.Locale = binaryReader.ReadUInt32();
							continue;
						}
					}
					else
					{
						if (num == 32)
						{
							instance.Public = ProtocolParser.ReadBool(stream);
							continue;
						}
						if (num == 40)
						{
							instance.BucketIndex = ProtocolParser.ReadUInt32(stream);
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

		// Token: 0x06005816 RID: 22550 RVA: 0x0010DF37 File Offset: 0x0010C137
		public void Serialize(Stream stream)
		{
			ChatChannelState.Serialize(stream, this);
		}

		// Token: 0x06005817 RID: 22551 RVA: 0x0010DF40 File Offset: 0x0010C140
		public static void Serialize(Stream stream, ChatChannelState instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasIdentity)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Identity));
			}
			if (instance.HasLocale)
			{
				stream.WriteByte(29);
				binaryWriter.Write(instance.Locale);
			}
			if (instance.HasPublic)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteBool(stream, instance.Public);
			}
			if (instance.HasBucketIndex)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt32(stream, instance.BucketIndex);
			}
		}

		// Token: 0x06005818 RID: 22552 RVA: 0x0010DFD0 File Offset: 0x0010C1D0
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasIdentity)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Identity);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasLocale)
			{
				num += 1U;
				num += 4U;
			}
			if (this.HasPublic)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasBucketIndex)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.BucketIndex);
			}
			return num;
		}

		// Token: 0x04001B97 RID: 7063
		public bool HasIdentity;

		// Token: 0x04001B98 RID: 7064
		private string _Identity;

		// Token: 0x04001B99 RID: 7065
		public bool HasLocale;

		// Token: 0x04001B9A RID: 7066
		private uint _Locale;

		// Token: 0x04001B9B RID: 7067
		public bool HasPublic;

		// Token: 0x04001B9C RID: 7068
		private bool _Public;

		// Token: 0x04001B9D RID: 7069
		public bool HasBucketIndex;

		// Token: 0x04001B9E RID: 7070
		private uint _BucketIndex;
	}
}
