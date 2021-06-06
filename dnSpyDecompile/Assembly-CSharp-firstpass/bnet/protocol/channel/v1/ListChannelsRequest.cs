using System;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.channel.v1
{
	// Token: 0x020004C2 RID: 1218
	public class ListChannelsRequest : IProtoBuf
	{
		// Token: 0x17001002 RID: 4098
		// (get) Token: 0x0600553F RID: 21823 RVA: 0x00105D72 File Offset: 0x00103F72
		// (set) Token: 0x06005540 RID: 21824 RVA: 0x00105D7A File Offset: 0x00103F7A
		public Identity AgentIdentity
		{
			get
			{
				return this._AgentIdentity;
			}
			set
			{
				this._AgentIdentity = value;
				this.HasAgentIdentity = (value != null);
			}
		}

		// Token: 0x06005541 RID: 21825 RVA: 0x00105D8D File Offset: 0x00103F8D
		public void SetAgentIdentity(Identity val)
		{
			this.AgentIdentity = val;
		}

		// Token: 0x17001003 RID: 4099
		// (get) Token: 0x06005542 RID: 21826 RVA: 0x00105D96 File Offset: 0x00103F96
		// (set) Token: 0x06005543 RID: 21827 RVA: 0x00105D9E File Offset: 0x00103F9E
		public ListChannelsOptions Options { get; set; }

		// Token: 0x06005544 RID: 21828 RVA: 0x00105DA7 File Offset: 0x00103FA7
		public void SetOptions(ListChannelsOptions val)
		{
			this.Options = val;
		}

		// Token: 0x06005545 RID: 21829 RVA: 0x00105DB0 File Offset: 0x00103FB0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAgentIdentity)
			{
				num ^= this.AgentIdentity.GetHashCode();
			}
			return num ^ this.Options.GetHashCode();
		}

		// Token: 0x06005546 RID: 21830 RVA: 0x00105DF0 File Offset: 0x00103FF0
		public override bool Equals(object obj)
		{
			ListChannelsRequest listChannelsRequest = obj as ListChannelsRequest;
			return listChannelsRequest != null && this.HasAgentIdentity == listChannelsRequest.HasAgentIdentity && (!this.HasAgentIdentity || this.AgentIdentity.Equals(listChannelsRequest.AgentIdentity)) && this.Options.Equals(listChannelsRequest.Options);
		}

		// Token: 0x17001004 RID: 4100
		// (get) Token: 0x06005547 RID: 21831 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005548 RID: 21832 RVA: 0x00105E4A File Offset: 0x0010404A
		public static ListChannelsRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ListChannelsRequest>(bs, 0, -1);
		}

		// Token: 0x06005549 RID: 21833 RVA: 0x00105E54 File Offset: 0x00104054
		public void Deserialize(Stream stream)
		{
			ListChannelsRequest.Deserialize(stream, this);
		}

		// Token: 0x0600554A RID: 21834 RVA: 0x00105E5E File Offset: 0x0010405E
		public static ListChannelsRequest Deserialize(Stream stream, ListChannelsRequest instance)
		{
			return ListChannelsRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600554B RID: 21835 RVA: 0x00105E6C File Offset: 0x0010406C
		public static ListChannelsRequest DeserializeLengthDelimited(Stream stream)
		{
			ListChannelsRequest listChannelsRequest = new ListChannelsRequest();
			ListChannelsRequest.DeserializeLengthDelimited(stream, listChannelsRequest);
			return listChannelsRequest;
		}

		// Token: 0x0600554C RID: 21836 RVA: 0x00105E88 File Offset: 0x00104088
		public static ListChannelsRequest DeserializeLengthDelimited(Stream stream, ListChannelsRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ListChannelsRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x0600554D RID: 21837 RVA: 0x00105EB0 File Offset: 0x001040B0
		public static ListChannelsRequest Deserialize(Stream stream, ListChannelsRequest instance, long limit)
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
					else if (instance.Options == null)
					{
						instance.Options = ListChannelsOptions.DeserializeLengthDelimited(stream);
					}
					else
					{
						ListChannelsOptions.DeserializeLengthDelimited(stream, instance.Options);
					}
				}
				else if (instance.AgentIdentity == null)
				{
					instance.AgentIdentity = Identity.DeserializeLengthDelimited(stream);
				}
				else
				{
					Identity.DeserializeLengthDelimited(stream, instance.AgentIdentity);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600554E RID: 21838 RVA: 0x00105F82 File Offset: 0x00104182
		public void Serialize(Stream stream)
		{
			ListChannelsRequest.Serialize(stream, this);
		}

		// Token: 0x0600554F RID: 21839 RVA: 0x00105F8C File Offset: 0x0010418C
		public static void Serialize(Stream stream, ListChannelsRequest instance)
		{
			if (instance.HasAgentIdentity)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentIdentity.GetSerializedSize());
				Identity.Serialize(stream, instance.AgentIdentity);
			}
			if (instance.Options == null)
			{
				throw new ArgumentNullException("Options", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteUInt32(stream, instance.Options.GetSerializedSize());
			ListChannelsOptions.Serialize(stream, instance.Options);
		}

		// Token: 0x06005550 RID: 21840 RVA: 0x00106004 File Offset: 0x00104204
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAgentIdentity)
			{
				num += 1U;
				uint serializedSize = this.AgentIdentity.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			uint serializedSize2 = this.Options.GetSerializedSize();
			num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			return num + 1U;
		}

		// Token: 0x04001AF4 RID: 6900
		public bool HasAgentIdentity;

		// Token: 0x04001AF5 RID: 6901
		private Identity _AgentIdentity;
	}
}
