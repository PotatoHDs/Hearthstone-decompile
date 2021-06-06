using System;
using System.IO;

namespace bnet.protocol.channel.v1
{
	// Token: 0x020004E0 RID: 1248
	public class ChannelId : IProtoBuf
	{
		// Token: 0x17001099 RID: 4249
		// (get) Token: 0x0600581A RID: 22554 RVA: 0x0010E042 File Offset: 0x0010C242
		// (set) Token: 0x0600581B RID: 22555 RVA: 0x0010E04A File Offset: 0x0010C24A
		public uint Type
		{
			get
			{
				return this._Type;
			}
			set
			{
				this._Type = value;
				this.HasType = true;
			}
		}

		// Token: 0x0600581C RID: 22556 RVA: 0x0010E05A File Offset: 0x0010C25A
		public void SetType(uint val)
		{
			this.Type = val;
		}

		// Token: 0x1700109A RID: 4250
		// (get) Token: 0x0600581D RID: 22557 RVA: 0x0010E063 File Offset: 0x0010C263
		// (set) Token: 0x0600581E RID: 22558 RVA: 0x0010E06B File Offset: 0x0010C26B
		public ProcessId Host
		{
			get
			{
				return this._Host;
			}
			set
			{
				this._Host = value;
				this.HasHost = (value != null);
			}
		}

		// Token: 0x0600581F RID: 22559 RVA: 0x0010E07E File Offset: 0x0010C27E
		public void SetHost(ProcessId val)
		{
			this.Host = val;
		}

		// Token: 0x1700109B RID: 4251
		// (get) Token: 0x06005820 RID: 22560 RVA: 0x0010E087 File Offset: 0x0010C287
		// (set) Token: 0x06005821 RID: 22561 RVA: 0x0010E08F File Offset: 0x0010C28F
		public uint Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				this._Id = value;
				this.HasId = true;
			}
		}

		// Token: 0x06005822 RID: 22562 RVA: 0x0010E09F File Offset: 0x0010C29F
		public void SetId(uint val)
		{
			this.Id = val;
		}

		// Token: 0x06005823 RID: 22563 RVA: 0x0010E0A8 File Offset: 0x0010C2A8
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasType)
			{
				num ^= this.Type.GetHashCode();
			}
			if (this.HasHost)
			{
				num ^= this.Host.GetHashCode();
			}
			if (this.HasId)
			{
				num ^= this.Id.GetHashCode();
			}
			return num;
		}

		// Token: 0x06005824 RID: 22564 RVA: 0x0010E10C File Offset: 0x0010C30C
		public override bool Equals(object obj)
		{
			ChannelId channelId = obj as ChannelId;
			return channelId != null && this.HasType == channelId.HasType && (!this.HasType || this.Type.Equals(channelId.Type)) && this.HasHost == channelId.HasHost && (!this.HasHost || this.Host.Equals(channelId.Host)) && this.HasId == channelId.HasId && (!this.HasId || this.Id.Equals(channelId.Id));
		}

		// Token: 0x1700109C RID: 4252
		// (get) Token: 0x06005825 RID: 22565 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005826 RID: 22566 RVA: 0x0010E1AD File Offset: 0x0010C3AD
		public static ChannelId ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ChannelId>(bs, 0, -1);
		}

		// Token: 0x06005827 RID: 22567 RVA: 0x0010E1B7 File Offset: 0x0010C3B7
		public void Deserialize(Stream stream)
		{
			ChannelId.Deserialize(stream, this);
		}

		// Token: 0x06005828 RID: 22568 RVA: 0x0010E1C1 File Offset: 0x0010C3C1
		public static ChannelId Deserialize(Stream stream, ChannelId instance)
		{
			return ChannelId.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005829 RID: 22569 RVA: 0x0010E1CC File Offset: 0x0010C3CC
		public static ChannelId DeserializeLengthDelimited(Stream stream)
		{
			ChannelId channelId = new ChannelId();
			ChannelId.DeserializeLengthDelimited(stream, channelId);
			return channelId;
		}

		// Token: 0x0600582A RID: 22570 RVA: 0x0010E1E8 File Offset: 0x0010C3E8
		public static ChannelId DeserializeLengthDelimited(Stream stream, ChannelId instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ChannelId.Deserialize(stream, instance, num);
		}

		// Token: 0x0600582B RID: 22571 RVA: 0x0010E210 File Offset: 0x0010C410
		public static ChannelId Deserialize(Stream stream, ChannelId instance, long limit)
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
				else if (num != 8)
				{
					if (num != 18)
					{
						if (num != 29)
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
							instance.Id = binaryReader.ReadUInt32();
						}
					}
					else if (instance.Host == null)
					{
						instance.Host = ProcessId.DeserializeLengthDelimited(stream);
					}
					else
					{
						ProcessId.DeserializeLengthDelimited(stream, instance.Host);
					}
				}
				else
				{
					instance.Type = ProtocolParser.ReadUInt32(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600582C RID: 22572 RVA: 0x0010E2E4 File Offset: 0x0010C4E4
		public void Serialize(Stream stream)
		{
			ChannelId.Serialize(stream, this);
		}

		// Token: 0x0600582D RID: 22573 RVA: 0x0010E2F0 File Offset: 0x0010C4F0
		public static void Serialize(Stream stream, ChannelId instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasType)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt32(stream, instance.Type);
			}
			if (instance.HasHost)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.Host.GetSerializedSize());
				ProcessId.Serialize(stream, instance.Host);
			}
			if (instance.HasId)
			{
				stream.WriteByte(29);
				binaryWriter.Write(instance.Id);
			}
		}

		// Token: 0x0600582E RID: 22574 RVA: 0x0010E368 File Offset: 0x0010C568
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasType)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.Type);
			}
			if (this.HasHost)
			{
				num += 1U;
				uint serializedSize = this.Host.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasId)
			{
				num += 1U;
				num += 4U;
			}
			return num;
		}

		// Token: 0x04001B9F RID: 7071
		public bool HasType;

		// Token: 0x04001BA0 RID: 7072
		private uint _Type;

		// Token: 0x04001BA1 RID: 7073
		public bool HasHost;

		// Token: 0x04001BA2 RID: 7074
		private ProcessId _Host;

		// Token: 0x04001BA3 RID: 7075
		public bool HasId;

		// Token: 0x04001BA4 RID: 7076
		private uint _Id;
	}
}
