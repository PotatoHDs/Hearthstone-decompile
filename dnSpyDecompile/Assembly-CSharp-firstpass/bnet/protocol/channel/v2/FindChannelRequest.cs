using System;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.channel.v2
{
	// Token: 0x02000450 RID: 1104
	public class FindChannelRequest : IProtoBuf
	{
		// Token: 0x17000E12 RID: 3602
		// (get) Token: 0x06004B51 RID: 19281 RVA: 0x000EA8A3 File Offset: 0x000E8AA3
		// (set) Token: 0x06004B52 RID: 19282 RVA: 0x000EA8AB File Offset: 0x000E8AAB
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

		// Token: 0x06004B53 RID: 19283 RVA: 0x000EA8BE File Offset: 0x000E8ABE
		public void SetAgentId(GameAccountHandle val)
		{
			this.AgentId = val;
		}

		// Token: 0x17000E13 RID: 3603
		// (get) Token: 0x06004B54 RID: 19284 RVA: 0x000EA8C7 File Offset: 0x000E8AC7
		// (set) Token: 0x06004B55 RID: 19285 RVA: 0x000EA8CF File Offset: 0x000E8ACF
		public FindChannelOptions Options
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

		// Token: 0x06004B56 RID: 19286 RVA: 0x000EA8E2 File Offset: 0x000E8AE2
		public void SetOptions(FindChannelOptions val)
		{
			this.Options = val;
		}

		// Token: 0x06004B57 RID: 19287 RVA: 0x000EA8EC File Offset: 0x000E8AEC
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAgentId)
			{
				num ^= this.AgentId.GetHashCode();
			}
			if (this.HasOptions)
			{
				num ^= this.Options.GetHashCode();
			}
			return num;
		}

		// Token: 0x06004B58 RID: 19288 RVA: 0x000EA934 File Offset: 0x000E8B34
		public override bool Equals(object obj)
		{
			FindChannelRequest findChannelRequest = obj as FindChannelRequest;
			return findChannelRequest != null && this.HasAgentId == findChannelRequest.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(findChannelRequest.AgentId)) && this.HasOptions == findChannelRequest.HasOptions && (!this.HasOptions || this.Options.Equals(findChannelRequest.Options));
		}

		// Token: 0x17000E14 RID: 3604
		// (get) Token: 0x06004B59 RID: 19289 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004B5A RID: 19290 RVA: 0x000EA9A4 File Offset: 0x000E8BA4
		public static FindChannelRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<FindChannelRequest>(bs, 0, -1);
		}

		// Token: 0x06004B5B RID: 19291 RVA: 0x000EA9AE File Offset: 0x000E8BAE
		public void Deserialize(Stream stream)
		{
			FindChannelRequest.Deserialize(stream, this);
		}

		// Token: 0x06004B5C RID: 19292 RVA: 0x000EA9B8 File Offset: 0x000E8BB8
		public static FindChannelRequest Deserialize(Stream stream, FindChannelRequest instance)
		{
			return FindChannelRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004B5D RID: 19293 RVA: 0x000EA9C4 File Offset: 0x000E8BC4
		public static FindChannelRequest DeserializeLengthDelimited(Stream stream)
		{
			FindChannelRequest findChannelRequest = new FindChannelRequest();
			FindChannelRequest.DeserializeLengthDelimited(stream, findChannelRequest);
			return findChannelRequest;
		}

		// Token: 0x06004B5E RID: 19294 RVA: 0x000EA9E0 File Offset: 0x000E8BE0
		public static FindChannelRequest DeserializeLengthDelimited(Stream stream, FindChannelRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return FindChannelRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06004B5F RID: 19295 RVA: 0x000EAA08 File Offset: 0x000E8C08
		public static FindChannelRequest Deserialize(Stream stream, FindChannelRequest instance, long limit)
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
					else if (instance.Options == null)
					{
						instance.Options = FindChannelOptions.DeserializeLengthDelimited(stream);
					}
					else
					{
						FindChannelOptions.DeserializeLengthDelimited(stream, instance.Options);
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

		// Token: 0x06004B60 RID: 19296 RVA: 0x000EAADA File Offset: 0x000E8CDA
		public void Serialize(Stream stream)
		{
			FindChannelRequest.Serialize(stream, this);
		}

		// Token: 0x06004B61 RID: 19297 RVA: 0x000EAAE4 File Offset: 0x000E8CE4
		public static void Serialize(Stream stream, FindChannelRequest instance)
		{
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.AgentId);
			}
			if (instance.HasOptions)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.Options.GetSerializedSize());
				FindChannelOptions.Serialize(stream, instance.Options);
			}
		}

		// Token: 0x06004B62 RID: 19298 RVA: 0x000EAB4C File Offset: 0x000E8D4C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAgentId)
			{
				num += 1U;
				uint serializedSize = this.AgentId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasOptions)
			{
				num += 1U;
				uint serializedSize2 = this.Options.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num;
		}

		// Token: 0x040018A7 RID: 6311
		public bool HasAgentId;

		// Token: 0x040018A8 RID: 6312
		private GameAccountHandle _AgentId;

		// Token: 0x040018A9 RID: 6313
		public bool HasOptions;

		// Token: 0x040018AA RID: 6314
		private FindChannelOptions _Options;
	}
}
