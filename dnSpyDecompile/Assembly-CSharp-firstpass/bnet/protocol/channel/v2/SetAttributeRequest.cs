using System;
using System.Collections.Generic;
using System.IO;
using bnet.protocol.account.v1;
using bnet.protocol.v2;

namespace bnet.protocol.channel.v2
{
	// Token: 0x02000454 RID: 1108
	public class SetAttributeRequest : IProtoBuf
	{
		// Token: 0x17000E1D RID: 3613
		// (get) Token: 0x06004B9A RID: 19354 RVA: 0x000EB3A6 File Offset: 0x000E95A6
		// (set) Token: 0x06004B9B RID: 19355 RVA: 0x000EB3AE File Offset: 0x000E95AE
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

		// Token: 0x06004B9C RID: 19356 RVA: 0x000EB3C1 File Offset: 0x000E95C1
		public void SetAgentId(GameAccountHandle val)
		{
			this.AgentId = val;
		}

		// Token: 0x17000E1E RID: 3614
		// (get) Token: 0x06004B9D RID: 19357 RVA: 0x000EB3CA File Offset: 0x000E95CA
		// (set) Token: 0x06004B9E RID: 19358 RVA: 0x000EB3D2 File Offset: 0x000E95D2
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

		// Token: 0x06004B9F RID: 19359 RVA: 0x000EB3E5 File Offset: 0x000E95E5
		public void SetChannelId(ChannelId val)
		{
			this.ChannelId = val;
		}

		// Token: 0x17000E1F RID: 3615
		// (get) Token: 0x06004BA0 RID: 19360 RVA: 0x000EB3EE File Offset: 0x000E95EE
		// (set) Token: 0x06004BA1 RID: 19361 RVA: 0x000EB3F6 File Offset: 0x000E95F6
		public List<bnet.protocol.v2.Attribute> Attribute
		{
			get
			{
				return this._Attribute;
			}
			set
			{
				this._Attribute = value;
			}
		}

		// Token: 0x17000E20 RID: 3616
		// (get) Token: 0x06004BA2 RID: 19362 RVA: 0x000EB3EE File Offset: 0x000E95EE
		public List<bnet.protocol.v2.Attribute> AttributeList
		{
			get
			{
				return this._Attribute;
			}
		}

		// Token: 0x17000E21 RID: 3617
		// (get) Token: 0x06004BA3 RID: 19363 RVA: 0x000EB3FF File Offset: 0x000E95FF
		public int AttributeCount
		{
			get
			{
				return this._Attribute.Count;
			}
		}

		// Token: 0x06004BA4 RID: 19364 RVA: 0x000EB40C File Offset: 0x000E960C
		public void AddAttribute(bnet.protocol.v2.Attribute val)
		{
			this._Attribute.Add(val);
		}

		// Token: 0x06004BA5 RID: 19365 RVA: 0x000EB41A File Offset: 0x000E961A
		public void ClearAttribute()
		{
			this._Attribute.Clear();
		}

		// Token: 0x06004BA6 RID: 19366 RVA: 0x000EB427 File Offset: 0x000E9627
		public void SetAttribute(List<bnet.protocol.v2.Attribute> val)
		{
			this.Attribute = val;
		}

		// Token: 0x06004BA7 RID: 19367 RVA: 0x000EB430 File Offset: 0x000E9630
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
			foreach (bnet.protocol.v2.Attribute attribute in this.Attribute)
			{
				num ^= attribute.GetHashCode();
			}
			return num;
		}

		// Token: 0x06004BA8 RID: 19368 RVA: 0x000EB4C0 File Offset: 0x000E96C0
		public override bool Equals(object obj)
		{
			SetAttributeRequest setAttributeRequest = obj as SetAttributeRequest;
			if (setAttributeRequest == null)
			{
				return false;
			}
			if (this.HasAgentId != setAttributeRequest.HasAgentId || (this.HasAgentId && !this.AgentId.Equals(setAttributeRequest.AgentId)))
			{
				return false;
			}
			if (this.HasChannelId != setAttributeRequest.HasChannelId || (this.HasChannelId && !this.ChannelId.Equals(setAttributeRequest.ChannelId)))
			{
				return false;
			}
			if (this.Attribute.Count != setAttributeRequest.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Attribute.Count; i++)
			{
				if (!this.Attribute[i].Equals(setAttributeRequest.Attribute[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000E22 RID: 3618
		// (get) Token: 0x06004BA9 RID: 19369 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004BAA RID: 19370 RVA: 0x000EB581 File Offset: 0x000E9781
		public static SetAttributeRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SetAttributeRequest>(bs, 0, -1);
		}

		// Token: 0x06004BAB RID: 19371 RVA: 0x000EB58B File Offset: 0x000E978B
		public void Deserialize(Stream stream)
		{
			SetAttributeRequest.Deserialize(stream, this);
		}

		// Token: 0x06004BAC RID: 19372 RVA: 0x000EB595 File Offset: 0x000E9795
		public static SetAttributeRequest Deserialize(Stream stream, SetAttributeRequest instance)
		{
			return SetAttributeRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004BAD RID: 19373 RVA: 0x000EB5A0 File Offset: 0x000E97A0
		public static SetAttributeRequest DeserializeLengthDelimited(Stream stream)
		{
			SetAttributeRequest setAttributeRequest = new SetAttributeRequest();
			SetAttributeRequest.DeserializeLengthDelimited(stream, setAttributeRequest);
			return setAttributeRequest;
		}

		// Token: 0x06004BAE RID: 19374 RVA: 0x000EB5BC File Offset: 0x000E97BC
		public static SetAttributeRequest DeserializeLengthDelimited(Stream stream, SetAttributeRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SetAttributeRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06004BAF RID: 19375 RVA: 0x000EB5E4 File Offset: 0x000E97E4
		public static SetAttributeRequest Deserialize(Stream stream, SetAttributeRequest instance, long limit)
		{
			if (instance.Attribute == null)
			{
				instance.Attribute = new List<bnet.protocol.v2.Attribute>();
			}
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
						else
						{
							instance.Attribute.Add(bnet.protocol.v2.Attribute.DeserializeLengthDelimited(stream));
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

		// Token: 0x06004BB0 RID: 19376 RVA: 0x000EB6E4 File Offset: 0x000E98E4
		public void Serialize(Stream stream)
		{
			SetAttributeRequest.Serialize(stream, this);
		}

		// Token: 0x06004BB1 RID: 19377 RVA: 0x000EB6F0 File Offset: 0x000E98F0
		public static void Serialize(Stream stream, SetAttributeRequest instance)
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
			if (instance.Attribute.Count > 0)
			{
				foreach (bnet.protocol.v2.Attribute attribute in instance.Attribute)
				{
					stream.WriteByte(26);
					ProtocolParser.WriteUInt32(stream, attribute.GetSerializedSize());
					bnet.protocol.v2.Attribute.Serialize(stream, attribute);
				}
			}
		}

		// Token: 0x06004BB2 RID: 19378 RVA: 0x000EB7C0 File Offset: 0x000E99C0
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
			if (this.Attribute.Count > 0)
			{
				foreach (bnet.protocol.v2.Attribute attribute in this.Attribute)
				{
					num += 1U;
					uint serializedSize3 = attribute.GetSerializedSize();
					num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
				}
			}
			return num;
		}

		// Token: 0x040018B5 RID: 6325
		public bool HasAgentId;

		// Token: 0x040018B6 RID: 6326
		private GameAccountHandle _AgentId;

		// Token: 0x040018B7 RID: 6327
		public bool HasChannelId;

		// Token: 0x040018B8 RID: 6328
		private ChannelId _ChannelId;

		// Token: 0x040018B9 RID: 6329
		private List<bnet.protocol.v2.Attribute> _Attribute = new List<bnet.protocol.v2.Attribute>();
	}
}
