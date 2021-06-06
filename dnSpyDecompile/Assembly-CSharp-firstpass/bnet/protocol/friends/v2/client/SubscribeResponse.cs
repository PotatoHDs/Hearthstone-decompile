using System;
using System.IO;

namespace bnet.protocol.friends.v2.client
{
	// Token: 0x020003FA RID: 1018
	public class SubscribeResponse : IProtoBuf
	{
		// Token: 0x17000C80 RID: 3200
		// (get) Token: 0x06004397 RID: 17303 RVA: 0x000D5D26 File Offset: 0x000D3F26
		// (set) Token: 0x06004398 RID: 17304 RVA: 0x000D5D2E File Offset: 0x000D3F2E
		public FriendsState State
		{
			get
			{
				return this._State;
			}
			set
			{
				this._State = value;
				this.HasState = (value != null);
			}
		}

		// Token: 0x06004399 RID: 17305 RVA: 0x000D5D41 File Offset: 0x000D3F41
		public void SetState(FriendsState val)
		{
			this.State = val;
		}

		// Token: 0x0600439A RID: 17306 RVA: 0x000D5D4C File Offset: 0x000D3F4C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasState)
			{
				num ^= this.State.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600439B RID: 17307 RVA: 0x000D5D7C File Offset: 0x000D3F7C
		public override bool Equals(object obj)
		{
			SubscribeResponse subscribeResponse = obj as SubscribeResponse;
			return subscribeResponse != null && this.HasState == subscribeResponse.HasState && (!this.HasState || this.State.Equals(subscribeResponse.State));
		}

		// Token: 0x17000C81 RID: 3201
		// (get) Token: 0x0600439C RID: 17308 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600439D RID: 17309 RVA: 0x000D5DC1 File Offset: 0x000D3FC1
		public static SubscribeResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SubscribeResponse>(bs, 0, -1);
		}

		// Token: 0x0600439E RID: 17310 RVA: 0x000D5DCB File Offset: 0x000D3FCB
		public void Deserialize(Stream stream)
		{
			SubscribeResponse.Deserialize(stream, this);
		}

		// Token: 0x0600439F RID: 17311 RVA: 0x000D5DD5 File Offset: 0x000D3FD5
		public static SubscribeResponse Deserialize(Stream stream, SubscribeResponse instance)
		{
			return SubscribeResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060043A0 RID: 17312 RVA: 0x000D5DE0 File Offset: 0x000D3FE0
		public static SubscribeResponse DeserializeLengthDelimited(Stream stream)
		{
			SubscribeResponse subscribeResponse = new SubscribeResponse();
			SubscribeResponse.DeserializeLengthDelimited(stream, subscribeResponse);
			return subscribeResponse;
		}

		// Token: 0x060043A1 RID: 17313 RVA: 0x000D5DFC File Offset: 0x000D3FFC
		public static SubscribeResponse DeserializeLengthDelimited(Stream stream, SubscribeResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SubscribeResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x060043A2 RID: 17314 RVA: 0x000D5E24 File Offset: 0x000D4024
		public static SubscribeResponse Deserialize(Stream stream, SubscribeResponse instance, long limit)
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
					if (instance.State == null)
					{
						instance.State = FriendsState.DeserializeLengthDelimited(stream);
					}
					else
					{
						FriendsState.DeserializeLengthDelimited(stream, instance.State);
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

		// Token: 0x060043A3 RID: 17315 RVA: 0x000D5EBE File Offset: 0x000D40BE
		public void Serialize(Stream stream)
		{
			SubscribeResponse.Serialize(stream, this);
		}

		// Token: 0x060043A4 RID: 17316 RVA: 0x000D5EC7 File Offset: 0x000D40C7
		public static void Serialize(Stream stream, SubscribeResponse instance)
		{
			if (instance.HasState)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.State.GetSerializedSize());
				FriendsState.Serialize(stream, instance.State);
			}
		}

		// Token: 0x060043A5 RID: 17317 RVA: 0x000D5EF8 File Offset: 0x000D40F8
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasState)
			{
				num += 1U;
				uint serializedSize = this.State.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}

		// Token: 0x04001708 RID: 5896
		public bool HasState;

		// Token: 0x04001709 RID: 5897
		private FriendsState _State;
	}
}
