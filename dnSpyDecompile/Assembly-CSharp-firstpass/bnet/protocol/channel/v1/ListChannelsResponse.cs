using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.channel.v1
{
	// Token: 0x020004C3 RID: 1219
	public class ListChannelsResponse : IProtoBuf
	{
		// Token: 0x17001005 RID: 4101
		// (get) Token: 0x06005552 RID: 21842 RVA: 0x00106052 File Offset: 0x00104252
		// (set) Token: 0x06005553 RID: 21843 RVA: 0x0010605A File Offset: 0x0010425A
		public List<ChannelDescription> Channel
		{
			get
			{
				return this._Channel;
			}
			set
			{
				this._Channel = value;
			}
		}

		// Token: 0x17001006 RID: 4102
		// (get) Token: 0x06005554 RID: 21844 RVA: 0x00106052 File Offset: 0x00104252
		public List<ChannelDescription> ChannelList
		{
			get
			{
				return this._Channel;
			}
		}

		// Token: 0x17001007 RID: 4103
		// (get) Token: 0x06005555 RID: 21845 RVA: 0x00106063 File Offset: 0x00104263
		public int ChannelCount
		{
			get
			{
				return this._Channel.Count;
			}
		}

		// Token: 0x06005556 RID: 21846 RVA: 0x00106070 File Offset: 0x00104270
		public void AddChannel(ChannelDescription val)
		{
			this._Channel.Add(val);
		}

		// Token: 0x06005557 RID: 21847 RVA: 0x0010607E File Offset: 0x0010427E
		public void ClearChannel()
		{
			this._Channel.Clear();
		}

		// Token: 0x06005558 RID: 21848 RVA: 0x0010608B File Offset: 0x0010428B
		public void SetChannel(List<ChannelDescription> val)
		{
			this.Channel = val;
		}

		// Token: 0x06005559 RID: 21849 RVA: 0x00106094 File Offset: 0x00104294
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (ChannelDescription channelDescription in this.Channel)
			{
				num ^= channelDescription.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600555A RID: 21850 RVA: 0x001060F8 File Offset: 0x001042F8
		public override bool Equals(object obj)
		{
			ListChannelsResponse listChannelsResponse = obj as ListChannelsResponse;
			if (listChannelsResponse == null)
			{
				return false;
			}
			if (this.Channel.Count != listChannelsResponse.Channel.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Channel.Count; i++)
			{
				if (!this.Channel[i].Equals(listChannelsResponse.Channel[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17001008 RID: 4104
		// (get) Token: 0x0600555B RID: 21851 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600555C RID: 21852 RVA: 0x00106163 File Offset: 0x00104363
		public static ListChannelsResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ListChannelsResponse>(bs, 0, -1);
		}

		// Token: 0x0600555D RID: 21853 RVA: 0x0010616D File Offset: 0x0010436D
		public void Deserialize(Stream stream)
		{
			ListChannelsResponse.Deserialize(stream, this);
		}

		// Token: 0x0600555E RID: 21854 RVA: 0x00106177 File Offset: 0x00104377
		public static ListChannelsResponse Deserialize(Stream stream, ListChannelsResponse instance)
		{
			return ListChannelsResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600555F RID: 21855 RVA: 0x00106184 File Offset: 0x00104384
		public static ListChannelsResponse DeserializeLengthDelimited(Stream stream)
		{
			ListChannelsResponse listChannelsResponse = new ListChannelsResponse();
			ListChannelsResponse.DeserializeLengthDelimited(stream, listChannelsResponse);
			return listChannelsResponse;
		}

		// Token: 0x06005560 RID: 21856 RVA: 0x001061A0 File Offset: 0x001043A0
		public static ListChannelsResponse DeserializeLengthDelimited(Stream stream, ListChannelsResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ListChannelsResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06005561 RID: 21857 RVA: 0x001061C8 File Offset: 0x001043C8
		public static ListChannelsResponse Deserialize(Stream stream, ListChannelsResponse instance, long limit)
		{
			if (instance.Channel == null)
			{
				instance.Channel = new List<ChannelDescription>();
			}
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
					instance.Channel.Add(ChannelDescription.DeserializeLengthDelimited(stream));
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

		// Token: 0x06005562 RID: 21858 RVA: 0x00106260 File Offset: 0x00104460
		public void Serialize(Stream stream)
		{
			ListChannelsResponse.Serialize(stream, this);
		}

		// Token: 0x06005563 RID: 21859 RVA: 0x0010626C File Offset: 0x0010446C
		public static void Serialize(Stream stream, ListChannelsResponse instance)
		{
			if (instance.Channel.Count > 0)
			{
				foreach (ChannelDescription channelDescription in instance.Channel)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, channelDescription.GetSerializedSize());
					ChannelDescription.Serialize(stream, channelDescription);
				}
			}
		}

		// Token: 0x06005564 RID: 21860 RVA: 0x001062E4 File Offset: 0x001044E4
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.Channel.Count > 0)
			{
				foreach (ChannelDescription channelDescription in this.Channel)
				{
					num += 1U;
					uint serializedSize = channelDescription.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num;
		}

		// Token: 0x04001AF7 RID: 6903
		private List<ChannelDescription> _Channel = new List<ChannelDescription>();
	}
}
