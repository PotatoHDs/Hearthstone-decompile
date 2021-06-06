using System;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.whisper.v1
{
	// Token: 0x020002E4 RID: 740
	public class GetWhisperMessagesRequest : IProtoBuf
	{
		// Token: 0x170007C2 RID: 1986
		// (get) Token: 0x06002BB3 RID: 11187 RVA: 0x00096D6E File Offset: 0x00094F6E
		// (set) Token: 0x06002BB4 RID: 11188 RVA: 0x00096D76 File Offset: 0x00094F76
		public AccountId AgentId
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

		// Token: 0x06002BB5 RID: 11189 RVA: 0x00096D89 File Offset: 0x00094F89
		public void SetAgentId(AccountId val)
		{
			this.AgentId = val;
		}

		// Token: 0x170007C3 RID: 1987
		// (get) Token: 0x06002BB6 RID: 11190 RVA: 0x00096D92 File Offset: 0x00094F92
		// (set) Token: 0x06002BB7 RID: 11191 RVA: 0x00096D9A File Offset: 0x00094F9A
		public AccountId TargetId
		{
			get
			{
				return this._TargetId;
			}
			set
			{
				this._TargetId = value;
				this.HasTargetId = (value != null);
			}
		}

		// Token: 0x06002BB8 RID: 11192 RVA: 0x00096DAD File Offset: 0x00094FAD
		public void SetTargetId(AccountId val)
		{
			this.TargetId = val;
		}

		// Token: 0x170007C4 RID: 1988
		// (get) Token: 0x06002BB9 RID: 11193 RVA: 0x00096DB6 File Offset: 0x00094FB6
		// (set) Token: 0x06002BBA RID: 11194 RVA: 0x00096DBE File Offset: 0x00094FBE
		public GetEventOptions Options
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

		// Token: 0x06002BBB RID: 11195 RVA: 0x00096DD1 File Offset: 0x00094FD1
		public void SetOptions(GetEventOptions val)
		{
			this.Options = val;
		}

		// Token: 0x06002BBC RID: 11196 RVA: 0x00096DDC File Offset: 0x00094FDC
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAgentId)
			{
				num ^= this.AgentId.GetHashCode();
			}
			if (this.HasTargetId)
			{
				num ^= this.TargetId.GetHashCode();
			}
			if (this.HasOptions)
			{
				num ^= this.Options.GetHashCode();
			}
			return num;
		}

		// Token: 0x06002BBD RID: 11197 RVA: 0x00096E38 File Offset: 0x00095038
		public override bool Equals(object obj)
		{
			GetWhisperMessagesRequest getWhisperMessagesRequest = obj as GetWhisperMessagesRequest;
			return getWhisperMessagesRequest != null && this.HasAgentId == getWhisperMessagesRequest.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(getWhisperMessagesRequest.AgentId)) && this.HasTargetId == getWhisperMessagesRequest.HasTargetId && (!this.HasTargetId || this.TargetId.Equals(getWhisperMessagesRequest.TargetId)) && this.HasOptions == getWhisperMessagesRequest.HasOptions && (!this.HasOptions || this.Options.Equals(getWhisperMessagesRequest.Options));
		}

		// Token: 0x170007C5 RID: 1989
		// (get) Token: 0x06002BBE RID: 11198 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002BBF RID: 11199 RVA: 0x00096ED3 File Offset: 0x000950D3
		public static GetWhisperMessagesRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetWhisperMessagesRequest>(bs, 0, -1);
		}

		// Token: 0x06002BC0 RID: 11200 RVA: 0x00096EDD File Offset: 0x000950DD
		public void Deserialize(Stream stream)
		{
			GetWhisperMessagesRequest.Deserialize(stream, this);
		}

		// Token: 0x06002BC1 RID: 11201 RVA: 0x00096EE7 File Offset: 0x000950E7
		public static GetWhisperMessagesRequest Deserialize(Stream stream, GetWhisperMessagesRequest instance)
		{
			return GetWhisperMessagesRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06002BC2 RID: 11202 RVA: 0x00096EF4 File Offset: 0x000950F4
		public static GetWhisperMessagesRequest DeserializeLengthDelimited(Stream stream)
		{
			GetWhisperMessagesRequest getWhisperMessagesRequest = new GetWhisperMessagesRequest();
			GetWhisperMessagesRequest.DeserializeLengthDelimited(stream, getWhisperMessagesRequest);
			return getWhisperMessagesRequest;
		}

		// Token: 0x06002BC3 RID: 11203 RVA: 0x00096F10 File Offset: 0x00095110
		public static GetWhisperMessagesRequest DeserializeLengthDelimited(Stream stream, GetWhisperMessagesRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetWhisperMessagesRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06002BC4 RID: 11204 RVA: 0x00096F38 File Offset: 0x00095138
		public static GetWhisperMessagesRequest Deserialize(Stream stream, GetWhisperMessagesRequest instance, long limit)
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
						if (num != 26)
						{
							Key key = ProtocolParser.ReadKey((byte)num, stream);
							if (key.Field == 0U)
							{
								throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
							}
							ProtocolParser.SkipKey(stream, key);
						}
						else if (instance.Options == null)
						{
							instance.Options = GetEventOptions.DeserializeLengthDelimited(stream);
						}
						else
						{
							GetEventOptions.DeserializeLengthDelimited(stream, instance.Options);
						}
					}
					else if (instance.TargetId == null)
					{
						instance.TargetId = AccountId.DeserializeLengthDelimited(stream);
					}
					else
					{
						AccountId.DeserializeLengthDelimited(stream, instance.TargetId);
					}
				}
				else if (instance.AgentId == null)
				{
					instance.AgentId = AccountId.DeserializeLengthDelimited(stream);
				}
				else
				{
					AccountId.DeserializeLengthDelimited(stream, instance.AgentId);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06002BC5 RID: 11205 RVA: 0x0009703A File Offset: 0x0009523A
		public void Serialize(Stream stream)
		{
			GetWhisperMessagesRequest.Serialize(stream, this);
		}

		// Token: 0x06002BC6 RID: 11206 RVA: 0x00097044 File Offset: 0x00095244
		public static void Serialize(Stream stream, GetWhisperMessagesRequest instance)
		{
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				AccountId.Serialize(stream, instance.AgentId);
			}
			if (instance.HasTargetId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.TargetId.GetSerializedSize());
				AccountId.Serialize(stream, instance.TargetId);
			}
			if (instance.HasOptions)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.Options.GetSerializedSize());
				GetEventOptions.Serialize(stream, instance.Options);
			}
		}

		// Token: 0x06002BC7 RID: 11207 RVA: 0x000970D8 File Offset: 0x000952D8
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAgentId)
			{
				num += 1U;
				uint serializedSize = this.AgentId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasTargetId)
			{
				num += 1U;
				uint serializedSize2 = this.TargetId.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasOptions)
			{
				num += 1U;
				uint serializedSize3 = this.Options.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			return num;
		}

		// Token: 0x04001237 RID: 4663
		public bool HasAgentId;

		// Token: 0x04001238 RID: 4664
		private AccountId _AgentId;

		// Token: 0x04001239 RID: 4665
		public bool HasTargetId;

		// Token: 0x0400123A RID: 4666
		private AccountId _TargetId;

		// Token: 0x0400123B RID: 4667
		public bool HasOptions;

		// Token: 0x0400123C RID: 4668
		private GetEventOptions _Options;
	}
}
