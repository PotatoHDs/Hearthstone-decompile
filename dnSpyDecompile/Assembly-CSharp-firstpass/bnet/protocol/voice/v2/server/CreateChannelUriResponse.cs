using System;
using System.IO;
using System.Text;

namespace bnet.protocol.voice.v2.server
{
	// Token: 0x020002C4 RID: 708
	public class CreateChannelUriResponse : IProtoBuf
	{
		// Token: 0x17000761 RID: 1889
		// (get) Token: 0x06002956 RID: 10582 RVA: 0x00091374 File Offset: 0x0008F574
		// (set) Token: 0x06002957 RID: 10583 RVA: 0x0009137C File Offset: 0x0008F57C
		public string ChannelUri
		{
			get
			{
				return this._ChannelUri;
			}
			set
			{
				this._ChannelUri = value;
				this.HasChannelUri = (value != null);
			}
		}

		// Token: 0x06002958 RID: 10584 RVA: 0x0009138F File Offset: 0x0008F58F
		public void SetChannelUri(string val)
		{
			this.ChannelUri = val;
		}

		// Token: 0x06002959 RID: 10585 RVA: 0x00091398 File Offset: 0x0008F598
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasChannelUri)
			{
				num ^= this.ChannelUri.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600295A RID: 10586 RVA: 0x000913C8 File Offset: 0x0008F5C8
		public override bool Equals(object obj)
		{
			CreateChannelUriResponse createChannelUriResponse = obj as CreateChannelUriResponse;
			return createChannelUriResponse != null && this.HasChannelUri == createChannelUriResponse.HasChannelUri && (!this.HasChannelUri || this.ChannelUri.Equals(createChannelUriResponse.ChannelUri));
		}

		// Token: 0x17000762 RID: 1890
		// (get) Token: 0x0600295B RID: 10587 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600295C RID: 10588 RVA: 0x0009140D File Offset: 0x0008F60D
		public static CreateChannelUriResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<CreateChannelUriResponse>(bs, 0, -1);
		}

		// Token: 0x0600295D RID: 10589 RVA: 0x00091417 File Offset: 0x0008F617
		public void Deserialize(Stream stream)
		{
			CreateChannelUriResponse.Deserialize(stream, this);
		}

		// Token: 0x0600295E RID: 10590 RVA: 0x00091421 File Offset: 0x0008F621
		public static CreateChannelUriResponse Deserialize(Stream stream, CreateChannelUriResponse instance)
		{
			return CreateChannelUriResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600295F RID: 10591 RVA: 0x0009142C File Offset: 0x0008F62C
		public static CreateChannelUriResponse DeserializeLengthDelimited(Stream stream)
		{
			CreateChannelUriResponse createChannelUriResponse = new CreateChannelUriResponse();
			CreateChannelUriResponse.DeserializeLengthDelimited(stream, createChannelUriResponse);
			return createChannelUriResponse;
		}

		// Token: 0x06002960 RID: 10592 RVA: 0x00091448 File Offset: 0x0008F648
		public static CreateChannelUriResponse DeserializeLengthDelimited(Stream stream, CreateChannelUriResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return CreateChannelUriResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06002961 RID: 10593 RVA: 0x00091470 File Offset: 0x0008F670
		public static CreateChannelUriResponse Deserialize(Stream stream, CreateChannelUriResponse instance, long limit)
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
					instance.ChannelUri = ProtocolParser.ReadString(stream);
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

		// Token: 0x06002962 RID: 10594 RVA: 0x000914F0 File Offset: 0x0008F6F0
		public void Serialize(Stream stream)
		{
			CreateChannelUriResponse.Serialize(stream, this);
		}

		// Token: 0x06002963 RID: 10595 RVA: 0x000914F9 File Offset: 0x0008F6F9
		public static void Serialize(Stream stream, CreateChannelUriResponse instance)
		{
			if (instance.HasChannelUri)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ChannelUri));
			}
		}

		// Token: 0x06002964 RID: 10596 RVA: 0x00091524 File Offset: 0x0008F724
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasChannelUri)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.ChannelUri);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num;
		}

		// Token: 0x040011C4 RID: 4548
		public bool HasChannelUri;

		// Token: 0x040011C5 RID: 4549
		private string _ChannelUri;
	}
}
