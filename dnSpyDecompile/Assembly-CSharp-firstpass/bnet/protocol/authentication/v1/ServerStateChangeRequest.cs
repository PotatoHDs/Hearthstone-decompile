using System;
using System.IO;

namespace bnet.protocol.authentication.v1
{
	// Token: 0x020004F3 RID: 1267
	public class ServerStateChangeRequest : IProtoBuf
	{
		// Token: 0x17001103 RID: 4355
		// (get) Token: 0x06005A02 RID: 23042 RVA: 0x001132FB File Offset: 0x001114FB
		// (set) Token: 0x06005A03 RID: 23043 RVA: 0x00113303 File Offset: 0x00111503
		public uint State { get; set; }

		// Token: 0x06005A04 RID: 23044 RVA: 0x0011330C File Offset: 0x0011150C
		public void SetState(uint val)
		{
			this.State = val;
		}

		// Token: 0x17001104 RID: 4356
		// (get) Token: 0x06005A05 RID: 23045 RVA: 0x00113315 File Offset: 0x00111515
		// (set) Token: 0x06005A06 RID: 23046 RVA: 0x0011331D File Offset: 0x0011151D
		public ulong EventTime { get; set; }

		// Token: 0x06005A07 RID: 23047 RVA: 0x00113326 File Offset: 0x00111526
		public void SetEventTime(ulong val)
		{
			this.EventTime = val;
		}

		// Token: 0x06005A08 RID: 23048 RVA: 0x00113330 File Offset: 0x00111530
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.State.GetHashCode() ^ this.EventTime.GetHashCode();
		}

		// Token: 0x06005A09 RID: 23049 RVA: 0x00113368 File Offset: 0x00111568
		public override bool Equals(object obj)
		{
			ServerStateChangeRequest serverStateChangeRequest = obj as ServerStateChangeRequest;
			return serverStateChangeRequest != null && this.State.Equals(serverStateChangeRequest.State) && this.EventTime.Equals(serverStateChangeRequest.EventTime);
		}

		// Token: 0x17001105 RID: 4357
		// (get) Token: 0x06005A0A RID: 23050 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005A0B RID: 23051 RVA: 0x001133B2 File Offset: 0x001115B2
		public static ServerStateChangeRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ServerStateChangeRequest>(bs, 0, -1);
		}

		// Token: 0x06005A0C RID: 23052 RVA: 0x001133BC File Offset: 0x001115BC
		public void Deserialize(Stream stream)
		{
			ServerStateChangeRequest.Deserialize(stream, this);
		}

		// Token: 0x06005A0D RID: 23053 RVA: 0x001133C6 File Offset: 0x001115C6
		public static ServerStateChangeRequest Deserialize(Stream stream, ServerStateChangeRequest instance)
		{
			return ServerStateChangeRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005A0E RID: 23054 RVA: 0x001133D4 File Offset: 0x001115D4
		public static ServerStateChangeRequest DeserializeLengthDelimited(Stream stream)
		{
			ServerStateChangeRequest serverStateChangeRequest = new ServerStateChangeRequest();
			ServerStateChangeRequest.DeserializeLengthDelimited(stream, serverStateChangeRequest);
			return serverStateChangeRequest;
		}

		// Token: 0x06005A0F RID: 23055 RVA: 0x001133F0 File Offset: 0x001115F0
		public static ServerStateChangeRequest DeserializeLengthDelimited(Stream stream, ServerStateChangeRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ServerStateChangeRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06005A10 RID: 23056 RVA: 0x00113418 File Offset: 0x00111618
		public static ServerStateChangeRequest Deserialize(Stream stream, ServerStateChangeRequest instance, long limit)
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
						instance.EventTime = ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.State = ProtocolParser.ReadUInt32(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06005A11 RID: 23057 RVA: 0x001134AF File Offset: 0x001116AF
		public void Serialize(Stream stream)
		{
			ServerStateChangeRequest.Serialize(stream, this);
		}

		// Token: 0x06005A12 RID: 23058 RVA: 0x001134B8 File Offset: 0x001116B8
		public static void Serialize(Stream stream, ServerStateChangeRequest instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt32(stream, instance.State);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, instance.EventTime);
		}

		// Token: 0x06005A13 RID: 23059 RVA: 0x001134E1 File Offset: 0x001116E1
		public uint GetSerializedSize()
		{
			return 0U + ProtocolParser.SizeOfUInt32(this.State) + ProtocolParser.SizeOfUInt64(this.EventTime) + 2U;
		}
	}
}
