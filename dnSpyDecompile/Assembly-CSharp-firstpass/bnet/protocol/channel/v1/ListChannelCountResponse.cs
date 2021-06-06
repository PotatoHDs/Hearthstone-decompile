using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.channel.v1
{
	// Token: 0x020004B4 RID: 1204
	public class ListChannelCountResponse : IProtoBuf
	{
		// Token: 0x17000FC9 RID: 4041
		// (get) Token: 0x0600540A RID: 21514 RVA: 0x00102C09 File Offset: 0x00100E09
		// (set) Token: 0x0600540B RID: 21515 RVA: 0x00102C11 File Offset: 0x00100E11
		public List<ChannelCount> Channel
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

		// Token: 0x17000FCA RID: 4042
		// (get) Token: 0x0600540C RID: 21516 RVA: 0x00102C09 File Offset: 0x00100E09
		public List<ChannelCount> ChannelList
		{
			get
			{
				return this._Channel;
			}
		}

		// Token: 0x17000FCB RID: 4043
		// (get) Token: 0x0600540D RID: 21517 RVA: 0x00102C1A File Offset: 0x00100E1A
		public int ChannelCount
		{
			get
			{
				return this._Channel.Count;
			}
		}

		// Token: 0x0600540E RID: 21518 RVA: 0x00102C27 File Offset: 0x00100E27
		public void AddChannel(ChannelCount val)
		{
			this._Channel.Add(val);
		}

		// Token: 0x0600540F RID: 21519 RVA: 0x00102C35 File Offset: 0x00100E35
		public void ClearChannel()
		{
			this._Channel.Clear();
		}

		// Token: 0x06005410 RID: 21520 RVA: 0x00102C42 File Offset: 0x00100E42
		public void SetChannel(List<ChannelCount> val)
		{
			this.Channel = val;
		}

		// Token: 0x06005411 RID: 21521 RVA: 0x00102C4C File Offset: 0x00100E4C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (ChannelCount channelCount in this.Channel)
			{
				num ^= channelCount.GetHashCode();
			}
			return num;
		}

		// Token: 0x06005412 RID: 21522 RVA: 0x00102CB0 File Offset: 0x00100EB0
		public override bool Equals(object obj)
		{
			ListChannelCountResponse listChannelCountResponse = obj as ListChannelCountResponse;
			if (listChannelCountResponse == null)
			{
				return false;
			}
			if (this.Channel.Count != listChannelCountResponse.Channel.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Channel.Count; i++)
			{
				if (!this.Channel[i].Equals(listChannelCountResponse.Channel[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000FCC RID: 4044
		// (get) Token: 0x06005413 RID: 21523 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005414 RID: 21524 RVA: 0x00102D1B File Offset: 0x00100F1B
		public static ListChannelCountResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ListChannelCountResponse>(bs, 0, -1);
		}

		// Token: 0x06005415 RID: 21525 RVA: 0x00102D25 File Offset: 0x00100F25
		public void Deserialize(Stream stream)
		{
			ListChannelCountResponse.Deserialize(stream, this);
		}

		// Token: 0x06005416 RID: 21526 RVA: 0x00102D2F File Offset: 0x00100F2F
		public static ListChannelCountResponse Deserialize(Stream stream, ListChannelCountResponse instance)
		{
			return ListChannelCountResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005417 RID: 21527 RVA: 0x00102D3C File Offset: 0x00100F3C
		public static ListChannelCountResponse DeserializeLengthDelimited(Stream stream)
		{
			ListChannelCountResponse listChannelCountResponse = new ListChannelCountResponse();
			ListChannelCountResponse.DeserializeLengthDelimited(stream, listChannelCountResponse);
			return listChannelCountResponse;
		}

		// Token: 0x06005418 RID: 21528 RVA: 0x00102D58 File Offset: 0x00100F58
		public static ListChannelCountResponse DeserializeLengthDelimited(Stream stream, ListChannelCountResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ListChannelCountResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06005419 RID: 21529 RVA: 0x00102D80 File Offset: 0x00100F80
		public static ListChannelCountResponse Deserialize(Stream stream, ListChannelCountResponse instance, long limit)
		{
			if (instance.Channel == null)
			{
				instance.Channel = new List<ChannelCount>();
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
					instance.Channel.Add(bnet.protocol.channel.v1.ChannelCount.DeserializeLengthDelimited(stream));
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

		// Token: 0x0600541A RID: 21530 RVA: 0x00102E18 File Offset: 0x00101018
		public void Serialize(Stream stream)
		{
			ListChannelCountResponse.Serialize(stream, this);
		}

		// Token: 0x0600541B RID: 21531 RVA: 0x00102E24 File Offset: 0x00101024
		public static void Serialize(Stream stream, ListChannelCountResponse instance)
		{
			if (instance.Channel.Count > 0)
			{
				foreach (ChannelCount channelCount in instance.Channel)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, channelCount.GetSerializedSize());
					bnet.protocol.channel.v1.ChannelCount.Serialize(stream, channelCount);
				}
			}
		}

		// Token: 0x0600541C RID: 21532 RVA: 0x00102E9C File Offset: 0x0010109C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.Channel.Count > 0)
			{
				foreach (ChannelCount channelCount in this.Channel)
				{
					num += 1U;
					uint serializedSize = channelCount.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num;
		}

		// Token: 0x04001AB1 RID: 6833
		private List<ChannelCount> _Channel = new List<ChannelCount>();
	}
}
