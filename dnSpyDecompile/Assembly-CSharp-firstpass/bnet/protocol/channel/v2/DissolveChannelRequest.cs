using System;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.channel.v2
{
	// Token: 0x0200044B RID: 1099
	public class DissolveChannelRequest : IProtoBuf
	{
		// Token: 0x17000DFD RID: 3581
		// (get) Token: 0x06004AE2 RID: 19170 RVA: 0x000E965A File Offset: 0x000E785A
		// (set) Token: 0x06004AE3 RID: 19171 RVA: 0x000E9662 File Offset: 0x000E7862
		public GameAccountHandle AgentId
		{
			get
			{
				return this._AgentId;
			}
			set
			{
				this._AgentId = value;
				this.HasAgentId = (value != null);
			}
		}

		// Token: 0x06004AE4 RID: 19172 RVA: 0x000E9675 File Offset: 0x000E7875
		public void SetAgentId(GameAccountHandle val)
		{
			this.AgentId = val;
		}

		// Token: 0x17000DFE RID: 3582
		// (get) Token: 0x06004AE5 RID: 19173 RVA: 0x000E967E File Offset: 0x000E787E
		// (set) Token: 0x06004AE6 RID: 19174 RVA: 0x000E9686 File Offset: 0x000E7886
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

		// Token: 0x06004AE7 RID: 19175 RVA: 0x000E9699 File Offset: 0x000E7899
		public void SetChannelId(ChannelId val)
		{
			this.ChannelId = val;
		}

		// Token: 0x06004AE8 RID: 19176 RVA: 0x000E96A4 File Offset: 0x000E78A4
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAgentId)
			{
				num ^= this.AgentId.GetHashCode();
			}
			if (this.HasChannelId)
			{
				num ^= this.ChannelId.GetHashCode();
			}
			return num;
		}

		// Token: 0x06004AE9 RID: 19177 RVA: 0x000E96EC File Offset: 0x000E78EC
		public override bool Equals(object obj)
		{
			DissolveChannelRequest dissolveChannelRequest = obj as DissolveChannelRequest;
			return dissolveChannelRequest != null && this.HasAgentId == dissolveChannelRequest.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(dissolveChannelRequest.AgentId)) && this.HasChannelId == dissolveChannelRequest.HasChannelId && (!this.HasChannelId || this.ChannelId.Equals(dissolveChannelRequest.ChannelId));
		}

		// Token: 0x17000DFF RID: 3583
		// (get) Token: 0x06004AEA RID: 19178 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004AEB RID: 19179 RVA: 0x000E975C File Offset: 0x000E795C
		public static DissolveChannelRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<DissolveChannelRequest>(bs, 0, -1);
		}

		// Token: 0x06004AEC RID: 19180 RVA: 0x000E9766 File Offset: 0x000E7966
		public void Deserialize(Stream stream)
		{
			DissolveChannelRequest.Deserialize(stream, this);
		}

		// Token: 0x06004AED RID: 19181 RVA: 0x000E9770 File Offset: 0x000E7970
		public static DissolveChannelRequest Deserialize(Stream stream, DissolveChannelRequest instance)
		{
			return DissolveChannelRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004AEE RID: 19182 RVA: 0x000E977C File Offset: 0x000E797C
		public static DissolveChannelRequest DeserializeLengthDelimited(Stream stream)
		{
			DissolveChannelRequest dissolveChannelRequest = new DissolveChannelRequest();
			DissolveChannelRequest.DeserializeLengthDelimited(stream, dissolveChannelRequest);
			return dissolveChannelRequest;
		}

		// Token: 0x06004AEF RID: 19183 RVA: 0x000E9798 File Offset: 0x000E7998
		public static DissolveChannelRequest DeserializeLengthDelimited(Stream stream, DissolveChannelRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return DissolveChannelRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06004AF0 RID: 19184 RVA: 0x000E97C0 File Offset: 0x000E79C0
		public static DissolveChannelRequest Deserialize(Stream stream, DissolveChannelRequest instance, long limit)
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
				else if (instance.AgentId == null)
				{
					instance.AgentId = GameAccountHandle.DeserializeLengthDelimited(stream);
				}
				else
				{
					GameAccountHandle.DeserializeLengthDelimited(stream, instance.AgentId);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06004AF1 RID: 19185 RVA: 0x000E9892 File Offset: 0x000E7A92
		public void Serialize(Stream stream)
		{
			DissolveChannelRequest.Serialize(stream, this);
		}

		// Token: 0x06004AF2 RID: 19186 RVA: 0x000E989C File Offset: 0x000E7A9C
		public static void Serialize(Stream stream, DissolveChannelRequest instance)
		{
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.AgentId);
			}
			if (instance.HasChannelId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.ChannelId.GetSerializedSize());
				ChannelId.Serialize(stream, instance.ChannelId);
			}
		}

		// Token: 0x06004AF3 RID: 19187 RVA: 0x000E9904 File Offset: 0x000E7B04
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAgentId)
			{
				num += 1U;
				uint serializedSize = this.AgentId.GetSerializedSize();
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

		// Token: 0x0400188C RID: 6284
		public bool HasAgentId;

		// Token: 0x0400188D RID: 6285
		private GameAccountHandle _AgentId;

		// Token: 0x0400188E RID: 6286
		public bool HasChannelId;

		// Token: 0x0400188F RID: 6287
		private ChannelId _ChannelId;
	}
}
