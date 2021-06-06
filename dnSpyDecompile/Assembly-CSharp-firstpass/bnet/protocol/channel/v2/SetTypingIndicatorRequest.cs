using System;
using System.IO;
using bnet.protocol.account.v1;
using bnet.protocol.Types;

namespace bnet.protocol.channel.v2
{
	// Token: 0x02000457 RID: 1111
	public class SetTypingIndicatorRequest : IProtoBuf
	{
		// Token: 0x17000E2B RID: 3627
		// (get) Token: 0x06004BE0 RID: 19424 RVA: 0x000EC041 File Offset: 0x000EA241
		// (set) Token: 0x06004BE1 RID: 19425 RVA: 0x000EC049 File Offset: 0x000EA249
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

		// Token: 0x06004BE2 RID: 19426 RVA: 0x000EC05C File Offset: 0x000EA25C
		public void SetAgentId(GameAccountHandle val)
		{
			this.AgentId = val;
		}

		// Token: 0x17000E2C RID: 3628
		// (get) Token: 0x06004BE3 RID: 19427 RVA: 0x000EC065 File Offset: 0x000EA265
		// (set) Token: 0x06004BE4 RID: 19428 RVA: 0x000EC06D File Offset: 0x000EA26D
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

		// Token: 0x06004BE5 RID: 19429 RVA: 0x000EC080 File Offset: 0x000EA280
		public void SetChannelId(ChannelId val)
		{
			this.ChannelId = val;
		}

		// Token: 0x17000E2D RID: 3629
		// (get) Token: 0x06004BE6 RID: 19430 RVA: 0x000EC089 File Offset: 0x000EA289
		// (set) Token: 0x06004BE7 RID: 19431 RVA: 0x000EC091 File Offset: 0x000EA291
		public TypingIndicator Action
		{
			get
			{
				return this._Action;
			}
			set
			{
				this._Action = value;
				this.HasAction = true;
			}
		}

		// Token: 0x06004BE8 RID: 19432 RVA: 0x000EC0A1 File Offset: 0x000EA2A1
		public void SetAction(TypingIndicator val)
		{
			this.Action = val;
		}

		// Token: 0x06004BE9 RID: 19433 RVA: 0x000EC0AC File Offset: 0x000EA2AC
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
			if (this.HasAction)
			{
				num ^= this.Action.GetHashCode();
			}
			return num;
		}

		// Token: 0x06004BEA RID: 19434 RVA: 0x000EC114 File Offset: 0x000EA314
		public override bool Equals(object obj)
		{
			SetTypingIndicatorRequest setTypingIndicatorRequest = obj as SetTypingIndicatorRequest;
			return setTypingIndicatorRequest != null && this.HasAgentId == setTypingIndicatorRequest.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(setTypingIndicatorRequest.AgentId)) && this.HasChannelId == setTypingIndicatorRequest.HasChannelId && (!this.HasChannelId || this.ChannelId.Equals(setTypingIndicatorRequest.ChannelId)) && this.HasAction == setTypingIndicatorRequest.HasAction && (!this.HasAction || this.Action.Equals(setTypingIndicatorRequest.Action));
		}

		// Token: 0x17000E2E RID: 3630
		// (get) Token: 0x06004BEB RID: 19435 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004BEC RID: 19436 RVA: 0x000EC1BD File Offset: 0x000EA3BD
		public static SetTypingIndicatorRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SetTypingIndicatorRequest>(bs, 0, -1);
		}

		// Token: 0x06004BED RID: 19437 RVA: 0x000EC1C7 File Offset: 0x000EA3C7
		public void Deserialize(Stream stream)
		{
			SetTypingIndicatorRequest.Deserialize(stream, this);
		}

		// Token: 0x06004BEE RID: 19438 RVA: 0x000EC1D1 File Offset: 0x000EA3D1
		public static SetTypingIndicatorRequest Deserialize(Stream stream, SetTypingIndicatorRequest instance)
		{
			return SetTypingIndicatorRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004BEF RID: 19439 RVA: 0x000EC1DC File Offset: 0x000EA3DC
		public static SetTypingIndicatorRequest DeserializeLengthDelimited(Stream stream)
		{
			SetTypingIndicatorRequest setTypingIndicatorRequest = new SetTypingIndicatorRequest();
			SetTypingIndicatorRequest.DeserializeLengthDelimited(stream, setTypingIndicatorRequest);
			return setTypingIndicatorRequest;
		}

		// Token: 0x06004BF0 RID: 19440 RVA: 0x000EC1F8 File Offset: 0x000EA3F8
		public static SetTypingIndicatorRequest DeserializeLengthDelimited(Stream stream, SetTypingIndicatorRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SetTypingIndicatorRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06004BF1 RID: 19441 RVA: 0x000EC220 File Offset: 0x000EA420
		public static SetTypingIndicatorRequest Deserialize(Stream stream, SetTypingIndicatorRequest instance, long limit)
		{
			instance.Action = TypingIndicator.TYPING_START;
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
						if (num != 24)
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
							instance.Action = (TypingIndicator)ProtocolParser.ReadUInt64(stream);
						}
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

		// Token: 0x06004BF2 RID: 19442 RVA: 0x000EC310 File Offset: 0x000EA510
		public void Serialize(Stream stream)
		{
			SetTypingIndicatorRequest.Serialize(stream, this);
		}

		// Token: 0x06004BF3 RID: 19443 RVA: 0x000EC31C File Offset: 0x000EA51C
		public static void Serialize(Stream stream, SetTypingIndicatorRequest instance)
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
			if (instance.HasAction)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Action));
			}
		}

		// Token: 0x06004BF4 RID: 19444 RVA: 0x000EC3A0 File Offset: 0x000EA5A0
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
			if (this.HasAction)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Action));
			}
			return num;
		}

		// Token: 0x040018C6 RID: 6342
		public bool HasAgentId;

		// Token: 0x040018C7 RID: 6343
		private GameAccountHandle _AgentId;

		// Token: 0x040018C8 RID: 6344
		public bool HasChannelId;

		// Token: 0x040018C9 RID: 6345
		private ChannelId _ChannelId;

		// Token: 0x040018CA RID: 6346
		public bool HasAction;

		// Token: 0x040018CB RID: 6347
		private TypingIndicator _Action;
	}
}
