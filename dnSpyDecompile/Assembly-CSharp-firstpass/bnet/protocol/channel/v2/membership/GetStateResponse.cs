using System;
using System.IO;

namespace bnet.protocol.channel.v2.membership
{
	// Token: 0x020004A7 RID: 1191
	public class GetStateResponse : IProtoBuf
	{
		// Token: 0x17000F9D RID: 3997
		// (get) Token: 0x06005304 RID: 21252 RVA: 0x0010054B File Offset: 0x000FE74B
		// (set) Token: 0x06005305 RID: 21253 RVA: 0x00100553 File Offset: 0x000FE753
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

		// Token: 0x06005306 RID: 21254 RVA: 0x00100566 File Offset: 0x000FE766
		public void SetState(ChannelMembershipState val)
		{
			this.State = val;
		}

		// Token: 0x06005307 RID: 21255 RVA: 0x00100570 File Offset: 0x000FE770
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasState)
			{
				num ^= this.State.GetHashCode();
			}
			return num;
		}

		// Token: 0x06005308 RID: 21256 RVA: 0x001005A0 File Offset: 0x000FE7A0
		public override bool Equals(object obj)
		{
			GetStateResponse getStateResponse = obj as GetStateResponse;
			return getStateResponse != null && this.HasState == getStateResponse.HasState && (!this.HasState || this.State.Equals(getStateResponse.State));
		}

		// Token: 0x17000F9E RID: 3998
		// (get) Token: 0x06005309 RID: 21257 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600530A RID: 21258 RVA: 0x001005E5 File Offset: 0x000FE7E5
		public static GetStateResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetStateResponse>(bs, 0, -1);
		}

		// Token: 0x0600530B RID: 21259 RVA: 0x001005EF File Offset: 0x000FE7EF
		public void Deserialize(Stream stream)
		{
			GetStateResponse.Deserialize(stream, this);
		}

		// Token: 0x0600530C RID: 21260 RVA: 0x001005F9 File Offset: 0x000FE7F9
		public static GetStateResponse Deserialize(Stream stream, GetStateResponse instance)
		{
			return GetStateResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600530D RID: 21261 RVA: 0x00100604 File Offset: 0x000FE804
		public static GetStateResponse DeserializeLengthDelimited(Stream stream)
		{
			GetStateResponse getStateResponse = new GetStateResponse();
			GetStateResponse.DeserializeLengthDelimited(stream, getStateResponse);
			return getStateResponse;
		}

		// Token: 0x0600530E RID: 21262 RVA: 0x00100620 File Offset: 0x000FE820
		public static GetStateResponse DeserializeLengthDelimited(Stream stream, GetStateResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetStateResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x0600530F RID: 21263 RVA: 0x00100648 File Offset: 0x000FE848
		public static GetStateResponse Deserialize(Stream stream, GetStateResponse instance, long limit)
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

		// Token: 0x06005310 RID: 21264 RVA: 0x001006E2 File Offset: 0x000FE8E2
		public void Serialize(Stream stream)
		{
			GetStateResponse.Serialize(stream, this);
		}

		// Token: 0x06005311 RID: 21265 RVA: 0x001006EB File Offset: 0x000FE8EB
		public static void Serialize(Stream stream, GetStateResponse instance)
		{
			if (instance.HasState)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.State.GetSerializedSize());
				ChannelMembershipState.Serialize(stream, instance.State);
			}
		}

		// Token: 0x06005312 RID: 21266 RVA: 0x0010071C File Offset: 0x000FE91C
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

		// Token: 0x04001A80 RID: 6784
		public bool HasState;

		// Token: 0x04001A81 RID: 6785
		private ChannelMembershipState _State;
	}
}
