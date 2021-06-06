using System;
using System.IO;
using System.Text;

namespace bnet.protocol.voice.v2.server
{
	// Token: 0x020002C3 RID: 707
	public class CreateChannelUriRequest : IProtoBuf
	{
		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x06002946 RID: 10566 RVA: 0x0009118C File Offset: 0x0008F38C
		// (set) Token: 0x06002947 RID: 10567 RVA: 0x00091194 File Offset: 0x0008F394
		public string ChannelName
		{
			get
			{
				return this._ChannelName;
			}
			set
			{
				this._ChannelName = value;
				this.HasChannelName = (value != null);
			}
		}

		// Token: 0x06002948 RID: 10568 RVA: 0x000911A7 File Offset: 0x0008F3A7
		public void SetChannelName(string val)
		{
			this.ChannelName = val;
		}

		// Token: 0x06002949 RID: 10569 RVA: 0x000911B0 File Offset: 0x0008F3B0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasChannelName)
			{
				num ^= this.ChannelName.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600294A RID: 10570 RVA: 0x000911E0 File Offset: 0x0008F3E0
		public override bool Equals(object obj)
		{
			CreateChannelUriRequest createChannelUriRequest = obj as CreateChannelUriRequest;
			return createChannelUriRequest != null && this.HasChannelName == createChannelUriRequest.HasChannelName && (!this.HasChannelName || this.ChannelName.Equals(createChannelUriRequest.ChannelName));
		}

		// Token: 0x17000760 RID: 1888
		// (get) Token: 0x0600294B RID: 10571 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600294C RID: 10572 RVA: 0x00091225 File Offset: 0x0008F425
		public static CreateChannelUriRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<CreateChannelUriRequest>(bs, 0, -1);
		}

		// Token: 0x0600294D RID: 10573 RVA: 0x0009122F File Offset: 0x0008F42F
		public void Deserialize(Stream stream)
		{
			CreateChannelUriRequest.Deserialize(stream, this);
		}

		// Token: 0x0600294E RID: 10574 RVA: 0x00091239 File Offset: 0x0008F439
		public static CreateChannelUriRequest Deserialize(Stream stream, CreateChannelUriRequest instance)
		{
			return CreateChannelUriRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600294F RID: 10575 RVA: 0x00091244 File Offset: 0x0008F444
		public static CreateChannelUriRequest DeserializeLengthDelimited(Stream stream)
		{
			CreateChannelUriRequest createChannelUriRequest = new CreateChannelUriRequest();
			CreateChannelUriRequest.DeserializeLengthDelimited(stream, createChannelUriRequest);
			return createChannelUriRequest;
		}

		// Token: 0x06002950 RID: 10576 RVA: 0x00091260 File Offset: 0x0008F460
		public static CreateChannelUriRequest DeserializeLengthDelimited(Stream stream, CreateChannelUriRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return CreateChannelUriRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06002951 RID: 10577 RVA: 0x00091288 File Offset: 0x0008F488
		public static CreateChannelUriRequest Deserialize(Stream stream, CreateChannelUriRequest instance, long limit)
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
				else if (num == 10)
				{
					instance.ChannelName = ProtocolParser.ReadString(stream);
				}
				else
				{
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

		// Token: 0x06002952 RID: 10578 RVA: 0x00091308 File Offset: 0x0008F508
		public void Serialize(Stream stream)
		{
			CreateChannelUriRequest.Serialize(stream, this);
		}

		// Token: 0x06002953 RID: 10579 RVA: 0x00091311 File Offset: 0x0008F511
		public static void Serialize(Stream stream, CreateChannelUriRequest instance)
		{
			if (instance.HasChannelName)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ChannelName));
			}
		}

		// Token: 0x06002954 RID: 10580 RVA: 0x0009133C File Offset: 0x0008F53C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasChannelName)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.ChannelName);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num;
		}

		// Token: 0x040011C2 RID: 4546
		public bool HasChannelName;

		// Token: 0x040011C3 RID: 4547
		private string _ChannelName;
	}
}
