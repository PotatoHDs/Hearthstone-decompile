using System;
using System.IO;
using bnet.protocol.account.v1;
using bnet.protocol.channel.v2.Types;

namespace bnet.protocol.channel.v2
{
	// Token: 0x02000455 RID: 1109
	public class SetPrivacyLevelRequest : IProtoBuf
	{
		// Token: 0x17000E23 RID: 3619
		// (get) Token: 0x06004BB4 RID: 19380 RVA: 0x000EB88F File Offset: 0x000E9A8F
		// (set) Token: 0x06004BB5 RID: 19381 RVA: 0x000EB897 File Offset: 0x000E9A97
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

		// Token: 0x06004BB6 RID: 19382 RVA: 0x000EB8AA File Offset: 0x000E9AAA
		public void SetAgentId(GameAccountHandle val)
		{
			this.AgentId = val;
		}

		// Token: 0x17000E24 RID: 3620
		// (get) Token: 0x06004BB7 RID: 19383 RVA: 0x000EB8B3 File Offset: 0x000E9AB3
		// (set) Token: 0x06004BB8 RID: 19384 RVA: 0x000EB8BB File Offset: 0x000E9ABB
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

		// Token: 0x06004BB9 RID: 19385 RVA: 0x000EB8CE File Offset: 0x000E9ACE
		public void SetChannelId(ChannelId val)
		{
			this.ChannelId = val;
		}

		// Token: 0x17000E25 RID: 3621
		// (get) Token: 0x06004BBA RID: 19386 RVA: 0x000EB8D7 File Offset: 0x000E9AD7
		// (set) Token: 0x06004BBB RID: 19387 RVA: 0x000EB8DF File Offset: 0x000E9ADF
		public PrivacyLevel PrivacyLevel
		{
			get
			{
				return this._PrivacyLevel;
			}
			set
			{
				this._PrivacyLevel = value;
				this.HasPrivacyLevel = true;
			}
		}

		// Token: 0x06004BBC RID: 19388 RVA: 0x000EB8EF File Offset: 0x000E9AEF
		public void SetPrivacyLevel(PrivacyLevel val)
		{
			this.PrivacyLevel = val;
		}

		// Token: 0x06004BBD RID: 19389 RVA: 0x000EB8F8 File Offset: 0x000E9AF8
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
			if (this.HasPrivacyLevel)
			{
				num ^= this.PrivacyLevel.GetHashCode();
			}
			return num;
		}

		// Token: 0x06004BBE RID: 19390 RVA: 0x000EB960 File Offset: 0x000E9B60
		public override bool Equals(object obj)
		{
			SetPrivacyLevelRequest setPrivacyLevelRequest = obj as SetPrivacyLevelRequest;
			return setPrivacyLevelRequest != null && this.HasAgentId == setPrivacyLevelRequest.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(setPrivacyLevelRequest.AgentId)) && this.HasChannelId == setPrivacyLevelRequest.HasChannelId && (!this.HasChannelId || this.ChannelId.Equals(setPrivacyLevelRequest.ChannelId)) && this.HasPrivacyLevel == setPrivacyLevelRequest.HasPrivacyLevel && (!this.HasPrivacyLevel || this.PrivacyLevel.Equals(setPrivacyLevelRequest.PrivacyLevel));
		}

		// Token: 0x17000E26 RID: 3622
		// (get) Token: 0x06004BBF RID: 19391 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004BC0 RID: 19392 RVA: 0x000EBA09 File Offset: 0x000E9C09
		public static SetPrivacyLevelRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SetPrivacyLevelRequest>(bs, 0, -1);
		}

		// Token: 0x06004BC1 RID: 19393 RVA: 0x000EBA13 File Offset: 0x000E9C13
		public void Deserialize(Stream stream)
		{
			SetPrivacyLevelRequest.Deserialize(stream, this);
		}

		// Token: 0x06004BC2 RID: 19394 RVA: 0x000EBA1D File Offset: 0x000E9C1D
		public static SetPrivacyLevelRequest Deserialize(Stream stream, SetPrivacyLevelRequest instance)
		{
			return SetPrivacyLevelRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004BC3 RID: 19395 RVA: 0x000EBA28 File Offset: 0x000E9C28
		public static SetPrivacyLevelRequest DeserializeLengthDelimited(Stream stream)
		{
			SetPrivacyLevelRequest setPrivacyLevelRequest = new SetPrivacyLevelRequest();
			SetPrivacyLevelRequest.DeserializeLengthDelimited(stream, setPrivacyLevelRequest);
			return setPrivacyLevelRequest;
		}

		// Token: 0x06004BC4 RID: 19396 RVA: 0x000EBA44 File Offset: 0x000E9C44
		public static SetPrivacyLevelRequest DeserializeLengthDelimited(Stream stream, SetPrivacyLevelRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SetPrivacyLevelRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06004BC5 RID: 19397 RVA: 0x000EBA6C File Offset: 0x000E9C6C
		public static SetPrivacyLevelRequest Deserialize(Stream stream, SetPrivacyLevelRequest instance, long limit)
		{
			instance.PrivacyLevel = PrivacyLevel.PRIVACY_LEVEL_OPEN;
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
							instance.PrivacyLevel = (PrivacyLevel)ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x06004BC6 RID: 19398 RVA: 0x000EBB5C File Offset: 0x000E9D5C
		public void Serialize(Stream stream)
		{
			SetPrivacyLevelRequest.Serialize(stream, this);
		}

		// Token: 0x06004BC7 RID: 19399 RVA: 0x000EBB68 File Offset: 0x000E9D68
		public static void Serialize(Stream stream, SetPrivacyLevelRequest instance)
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
			if (instance.HasPrivacyLevel)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.PrivacyLevel));
			}
		}

		// Token: 0x06004BC8 RID: 19400 RVA: 0x000EBBEC File Offset: 0x000E9DEC
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
			if (this.HasPrivacyLevel)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.PrivacyLevel));
			}
			return num;
		}

		// Token: 0x040018BA RID: 6330
		public bool HasAgentId;

		// Token: 0x040018BB RID: 6331
		private GameAccountHandle _AgentId;

		// Token: 0x040018BC RID: 6332
		public bool HasChannelId;

		// Token: 0x040018BD RID: 6333
		private ChannelId _ChannelId;

		// Token: 0x040018BE RID: 6334
		public bool HasPrivacyLevel;

		// Token: 0x040018BF RID: 6335
		private PrivacyLevel _PrivacyLevel;
	}
}
