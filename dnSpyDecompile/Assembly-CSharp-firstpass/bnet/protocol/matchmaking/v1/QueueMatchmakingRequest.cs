using System;
using System.IO;
using bnet.protocol.channel.v2;

namespace bnet.protocol.matchmaking.v1
{
	// Token: 0x020003E3 RID: 995
	public class QueueMatchmakingRequest : IProtoBuf
	{
		// Token: 0x17000C1C RID: 3100
		// (get) Token: 0x0600419D RID: 16797 RVA: 0x000D0A4B File Offset: 0x000CEC4B
		// (set) Token: 0x0600419E RID: 16798 RVA: 0x000D0A53 File Offset: 0x000CEC53
		public GameMatchmakingOptions Options
		{
			get
			{
				return this._Options;
			}
			set
			{
				this._Options = value;
				this.HasOptions = (value != null);
			}
		}

		// Token: 0x0600419F RID: 16799 RVA: 0x000D0A66 File Offset: 0x000CEC66
		public void SetOptions(GameMatchmakingOptions val)
		{
			this.Options = val;
		}

		// Token: 0x17000C1D RID: 3101
		// (get) Token: 0x060041A0 RID: 16800 RVA: 0x000D0A6F File Offset: 0x000CEC6F
		// (set) Token: 0x060041A1 RID: 16801 RVA: 0x000D0A77 File Offset: 0x000CEC77
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

		// Token: 0x060041A2 RID: 16802 RVA: 0x000D0A8A File Offset: 0x000CEC8A
		public void SetChannelId(ChannelId val)
		{
			this.ChannelId = val;
		}

		// Token: 0x060041A3 RID: 16803 RVA: 0x000D0A94 File Offset: 0x000CEC94
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasOptions)
			{
				num ^= this.Options.GetHashCode();
			}
			if (this.HasChannelId)
			{
				num ^= this.ChannelId.GetHashCode();
			}
			return num;
		}

		// Token: 0x060041A4 RID: 16804 RVA: 0x000D0ADC File Offset: 0x000CECDC
		public override bool Equals(object obj)
		{
			QueueMatchmakingRequest queueMatchmakingRequest = obj as QueueMatchmakingRequest;
			return queueMatchmakingRequest != null && this.HasOptions == queueMatchmakingRequest.HasOptions && (!this.HasOptions || this.Options.Equals(queueMatchmakingRequest.Options)) && this.HasChannelId == queueMatchmakingRequest.HasChannelId && (!this.HasChannelId || this.ChannelId.Equals(queueMatchmakingRequest.ChannelId));
		}

		// Token: 0x17000C1E RID: 3102
		// (get) Token: 0x060041A5 RID: 16805 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060041A6 RID: 16806 RVA: 0x000D0B4C File Offset: 0x000CED4C
		public static QueueMatchmakingRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<QueueMatchmakingRequest>(bs, 0, -1);
		}

		// Token: 0x060041A7 RID: 16807 RVA: 0x000D0B56 File Offset: 0x000CED56
		public void Deserialize(Stream stream)
		{
			QueueMatchmakingRequest.Deserialize(stream, this);
		}

		// Token: 0x060041A8 RID: 16808 RVA: 0x000D0B60 File Offset: 0x000CED60
		public static QueueMatchmakingRequest Deserialize(Stream stream, QueueMatchmakingRequest instance)
		{
			return QueueMatchmakingRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060041A9 RID: 16809 RVA: 0x000D0B6C File Offset: 0x000CED6C
		public static QueueMatchmakingRequest DeserializeLengthDelimited(Stream stream)
		{
			QueueMatchmakingRequest queueMatchmakingRequest = new QueueMatchmakingRequest();
			QueueMatchmakingRequest.DeserializeLengthDelimited(stream, queueMatchmakingRequest);
			return queueMatchmakingRequest;
		}

		// Token: 0x060041AA RID: 16810 RVA: 0x000D0B88 File Offset: 0x000CED88
		public static QueueMatchmakingRequest DeserializeLengthDelimited(Stream stream, QueueMatchmakingRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return QueueMatchmakingRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x060041AB RID: 16811 RVA: 0x000D0BB0 File Offset: 0x000CEDB0
		public static QueueMatchmakingRequest Deserialize(Stream stream, QueueMatchmakingRequest instance, long limit)
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
					else if (instance.ChannelId == null)
					{
						instance.ChannelId = ChannelId.DeserializeLengthDelimited(stream);
					}
					else
					{
						ChannelId.DeserializeLengthDelimited(stream, instance.ChannelId);
					}
				}
				else if (instance.Options == null)
				{
					instance.Options = GameMatchmakingOptions.DeserializeLengthDelimited(stream);
				}
				else
				{
					GameMatchmakingOptions.DeserializeLengthDelimited(stream, instance.Options);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060041AC RID: 16812 RVA: 0x000D0C82 File Offset: 0x000CEE82
		public void Serialize(Stream stream)
		{
			QueueMatchmakingRequest.Serialize(stream, this);
		}

		// Token: 0x060041AD RID: 16813 RVA: 0x000D0C8C File Offset: 0x000CEE8C
		public static void Serialize(Stream stream, QueueMatchmakingRequest instance)
		{
			if (instance.HasOptions)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Options.GetSerializedSize());
				GameMatchmakingOptions.Serialize(stream, instance.Options);
			}
			if (instance.HasChannelId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.ChannelId.GetSerializedSize());
				ChannelId.Serialize(stream, instance.ChannelId);
			}
		}

		// Token: 0x060041AE RID: 16814 RVA: 0x000D0CF4 File Offset: 0x000CEEF4
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasOptions)
			{
				num += 1U;
				uint serializedSize = this.Options.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasChannelId)
			{
				num += 1U;
				uint serializedSize2 = this.ChannelId.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num;
		}

		// Token: 0x040016AA RID: 5802
		public bool HasOptions;

		// Token: 0x040016AB RID: 5803
		private GameMatchmakingOptions _Options;

		// Token: 0x040016AC RID: 5804
		public bool HasChannelId;

		// Token: 0x040016AD RID: 5805
		private ChannelId _ChannelId;
	}
}
