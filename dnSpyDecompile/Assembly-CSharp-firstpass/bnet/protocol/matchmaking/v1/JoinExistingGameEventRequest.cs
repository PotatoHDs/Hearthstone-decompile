using System;
using System.IO;

namespace bnet.protocol.matchmaking.v1
{
	// Token: 0x020003CE RID: 974
	public class JoinExistingGameEventRequest : IProtoBuf
	{
		// Token: 0x17000BC2 RID: 3010
		// (get) Token: 0x06003FD3 RID: 16339 RVA: 0x000CBF52 File Offset: 0x000CA152
		// (set) Token: 0x06003FD4 RID: 16340 RVA: 0x000CBF5A File Offset: 0x000CA15A
		public MatchmakingEventInfo EventInfo
		{
			get
			{
				return this._EventInfo;
			}
			set
			{
				this._EventInfo = value;
				this.HasEventInfo = (value != null);
			}
		}

		// Token: 0x06003FD5 RID: 16341 RVA: 0x000CBF6D File Offset: 0x000CA16D
		public void SetEventInfo(MatchmakingEventInfo val)
		{
			this.EventInfo = val;
		}

		// Token: 0x06003FD6 RID: 16342 RVA: 0x000CBF78 File Offset: 0x000CA178
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasEventInfo)
			{
				num ^= this.EventInfo.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003FD7 RID: 16343 RVA: 0x000CBFA8 File Offset: 0x000CA1A8
		public override bool Equals(object obj)
		{
			JoinExistingGameEventRequest joinExistingGameEventRequest = obj as JoinExistingGameEventRequest;
			return joinExistingGameEventRequest != null && this.HasEventInfo == joinExistingGameEventRequest.HasEventInfo && (!this.HasEventInfo || this.EventInfo.Equals(joinExistingGameEventRequest.EventInfo));
		}

		// Token: 0x17000BC3 RID: 3011
		// (get) Token: 0x06003FD8 RID: 16344 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003FD9 RID: 16345 RVA: 0x000CBFED File Offset: 0x000CA1ED
		public static JoinExistingGameEventRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<JoinExistingGameEventRequest>(bs, 0, -1);
		}

		// Token: 0x06003FDA RID: 16346 RVA: 0x000CBFF7 File Offset: 0x000CA1F7
		public void Deserialize(Stream stream)
		{
			JoinExistingGameEventRequest.Deserialize(stream, this);
		}

		// Token: 0x06003FDB RID: 16347 RVA: 0x000CC001 File Offset: 0x000CA201
		public static JoinExistingGameEventRequest Deserialize(Stream stream, JoinExistingGameEventRequest instance)
		{
			return JoinExistingGameEventRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003FDC RID: 16348 RVA: 0x000CC00C File Offset: 0x000CA20C
		public static JoinExistingGameEventRequest DeserializeLengthDelimited(Stream stream)
		{
			JoinExistingGameEventRequest joinExistingGameEventRequest = new JoinExistingGameEventRequest();
			JoinExistingGameEventRequest.DeserializeLengthDelimited(stream, joinExistingGameEventRequest);
			return joinExistingGameEventRequest;
		}

		// Token: 0x06003FDD RID: 16349 RVA: 0x000CC028 File Offset: 0x000CA228
		public static JoinExistingGameEventRequest DeserializeLengthDelimited(Stream stream, JoinExistingGameEventRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return JoinExistingGameEventRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06003FDE RID: 16350 RVA: 0x000CC050 File Offset: 0x000CA250
		public static JoinExistingGameEventRequest Deserialize(Stream stream, JoinExistingGameEventRequest instance, long limit)
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
					if (instance.EventInfo == null)
					{
						instance.EventInfo = MatchmakingEventInfo.DeserializeLengthDelimited(stream);
					}
					else
					{
						MatchmakingEventInfo.DeserializeLengthDelimited(stream, instance.EventInfo);
					}
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

		// Token: 0x06003FDF RID: 16351 RVA: 0x000CC0EA File Offset: 0x000CA2EA
		public void Serialize(Stream stream)
		{
			JoinExistingGameEventRequest.Serialize(stream, this);
		}

		// Token: 0x06003FE0 RID: 16352 RVA: 0x000CC0F3 File Offset: 0x000CA2F3
		public static void Serialize(Stream stream, JoinExistingGameEventRequest instance)
		{
			if (instance.HasEventInfo)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.EventInfo.GetSerializedSize());
				MatchmakingEventInfo.Serialize(stream, instance.EventInfo);
			}
		}

		// Token: 0x06003FE1 RID: 16353 RVA: 0x000CC124 File Offset: 0x000CA324
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasEventInfo)
			{
				num += 1U;
				uint serializedSize = this.EventInfo.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}

		// Token: 0x04001657 RID: 5719
		public bool HasEventInfo;

		// Token: 0x04001658 RID: 5720
		private MatchmakingEventInfo _EventInfo;
	}
}
