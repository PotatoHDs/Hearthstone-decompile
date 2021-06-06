using System;
using System.IO;

namespace bnet.protocol.channel.v2.membership
{
	// Token: 0x020004A4 RID: 1188
	public class SubscribeResponse : IProtoBuf
	{
		// Token: 0x17000F97 RID: 3991
		// (get) Token: 0x060052D4 RID: 21204 RVA: 0x000FFF3F File Offset: 0x000FE13F
		// (set) Token: 0x060052D5 RID: 21205 RVA: 0x000FFF47 File Offset: 0x000FE147
		public ChannelMembershipState State
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

		// Token: 0x060052D6 RID: 21206 RVA: 0x000FFF5A File Offset: 0x000FE15A
		public void SetState(ChannelMembershipState val)
		{
			this.State = val;
		}

		// Token: 0x060052D7 RID: 21207 RVA: 0x000FFF64 File Offset: 0x000FE164
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasState)
			{
				num ^= this.State.GetHashCode();
			}
			return num;
		}

		// Token: 0x060052D8 RID: 21208 RVA: 0x000FFF94 File Offset: 0x000FE194
		public override bool Equals(object obj)
		{
			SubscribeResponse subscribeResponse = obj as SubscribeResponse;
			return subscribeResponse != null && this.HasState == subscribeResponse.HasState && (!this.HasState || this.State.Equals(subscribeResponse.State));
		}

		// Token: 0x17000F98 RID: 3992
		// (get) Token: 0x060052D9 RID: 21209 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060052DA RID: 21210 RVA: 0x000FFFD9 File Offset: 0x000FE1D9
		public static SubscribeResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SubscribeResponse>(bs, 0, -1);
		}

		// Token: 0x060052DB RID: 21211 RVA: 0x000FFFE3 File Offset: 0x000FE1E3
		public void Deserialize(Stream stream)
		{
			SubscribeResponse.Deserialize(stream, this);
		}

		// Token: 0x060052DC RID: 21212 RVA: 0x000FFFED File Offset: 0x000FE1ED
		public static SubscribeResponse Deserialize(Stream stream, SubscribeResponse instance)
		{
			return SubscribeResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060052DD RID: 21213 RVA: 0x000FFFF8 File Offset: 0x000FE1F8
		public static SubscribeResponse DeserializeLengthDelimited(Stream stream)
		{
			SubscribeResponse subscribeResponse = new SubscribeResponse();
			SubscribeResponse.DeserializeLengthDelimited(stream, subscribeResponse);
			return subscribeResponse;
		}

		// Token: 0x060052DE RID: 21214 RVA: 0x00100014 File Offset: 0x000FE214
		public static SubscribeResponse DeserializeLengthDelimited(Stream stream, SubscribeResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SubscribeResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x060052DF RID: 21215 RVA: 0x0010003C File Offset: 0x000FE23C
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
						instance.State = ChannelMembershipState.DeserializeLengthDelimited(stream);
					}
					else
					{
						ChannelMembershipState.DeserializeLengthDelimited(stream, instance.State);
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

		// Token: 0x060052E0 RID: 21216 RVA: 0x001000D6 File Offset: 0x000FE2D6
		public void Serialize(Stream stream)
		{
			SubscribeResponse.Serialize(stream, this);
		}

		// Token: 0x060052E1 RID: 21217 RVA: 0x001000DF File Offset: 0x000FE2DF
		public static void Serialize(Stream stream, SubscribeResponse instance)
		{
			if (instance.HasState)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.State.GetSerializedSize());
				ChannelMembershipState.Serialize(stream, instance.State);
			}
		}

		// Token: 0x060052E2 RID: 21218 RVA: 0x00100110 File Offset: 0x000FE310
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

		// Token: 0x04001A7A RID: 6778
		public bool HasState;

		// Token: 0x04001A7B RID: 6779
		private ChannelMembershipState _State;
	}
}
