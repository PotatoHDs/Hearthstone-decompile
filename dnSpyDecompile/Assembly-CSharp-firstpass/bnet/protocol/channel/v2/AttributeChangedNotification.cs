using System;
using System.Collections.Generic;
using System.IO;
using bnet.protocol.account.v1;
using bnet.protocol.v2;

namespace bnet.protocol.channel.v2
{
	// Token: 0x02000469 RID: 1129
	public class AttributeChangedNotification : IProtoBuf
	{
		// Token: 0x17000E75 RID: 3701
		// (get) Token: 0x06004D70 RID: 19824 RVA: 0x000F06CF File Offset: 0x000EE8CF
		// (set) Token: 0x06004D71 RID: 19825 RVA: 0x000F06D7 File Offset: 0x000EE8D7
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

		// Token: 0x06004D72 RID: 19826 RVA: 0x000F06EA File Offset: 0x000EE8EA
		public void SetAgentId(GameAccountHandle val)
		{
			this.AgentId = val;
		}

		// Token: 0x17000E76 RID: 3702
		// (get) Token: 0x06004D73 RID: 19827 RVA: 0x000F06F3 File Offset: 0x000EE8F3
		// (set) Token: 0x06004D74 RID: 19828 RVA: 0x000F06FB File Offset: 0x000EE8FB
		public GameAccountHandle SubscriberId
		{
			get
			{
				return this._SubscriberId;
			}
			set
			{
				this._SubscriberId = value;
				this.HasSubscriberId = (value != null);
			}
		}

		// Token: 0x06004D75 RID: 19829 RVA: 0x000F070E File Offset: 0x000EE90E
		public void SetSubscriberId(GameAccountHandle val)
		{
			this.SubscriberId = val;
		}

		// Token: 0x17000E77 RID: 3703
		// (get) Token: 0x06004D76 RID: 19830 RVA: 0x000F0717 File Offset: 0x000EE917
		// (set) Token: 0x06004D77 RID: 19831 RVA: 0x000F071F File Offset: 0x000EE91F
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

		// Token: 0x06004D78 RID: 19832 RVA: 0x000F0732 File Offset: 0x000EE932
		public void SetChannelId(ChannelId val)
		{
			this.ChannelId = val;
		}

		// Token: 0x17000E78 RID: 3704
		// (get) Token: 0x06004D79 RID: 19833 RVA: 0x000F073B File Offset: 0x000EE93B
		// (set) Token: 0x06004D7A RID: 19834 RVA: 0x000F0743 File Offset: 0x000EE943
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

		// Token: 0x17000E79 RID: 3705
		// (get) Token: 0x06004D7B RID: 19835 RVA: 0x000F073B File Offset: 0x000EE93B
		public List<bnet.protocol.v2.Attribute> AttributeList
		{
			get
			{
				return this._Attribute;
			}
		}

		// Token: 0x17000E7A RID: 3706
		// (get) Token: 0x06004D7C RID: 19836 RVA: 0x000F074C File Offset: 0x000EE94C
		public int AttributeCount
		{
			get
			{
				return this._Attribute.Count;
			}
		}

		// Token: 0x06004D7D RID: 19837 RVA: 0x000F0759 File Offset: 0x000EE959
		public void AddAttribute(bnet.protocol.v2.Attribute val)
		{
			this._Attribute.Add(val);
		}

		// Token: 0x06004D7E RID: 19838 RVA: 0x000F0767 File Offset: 0x000EE967
		public void ClearAttribute()
		{
			this._Attribute.Clear();
		}

		// Token: 0x06004D7F RID: 19839 RVA: 0x000F0774 File Offset: 0x000EE974
		public void SetAttribute(List<bnet.protocol.v2.Attribute> val)
		{
			this.Attribute = val;
		}

		// Token: 0x06004D80 RID: 19840 RVA: 0x000F0780 File Offset: 0x000EE980
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAgentId)
			{
				num ^= this.AgentId.GetHashCode();
			}
			if (this.HasSubscriberId)
			{
				num ^= this.SubscriberId.GetHashCode();
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

		// Token: 0x06004D81 RID: 19841 RVA: 0x000F0824 File Offset: 0x000EEA24
		public override bool Equals(object obj)
		{
			AttributeChangedNotification attributeChangedNotification = obj as AttributeChangedNotification;
			if (attributeChangedNotification == null)
			{
				return false;
			}
			if (this.HasAgentId != attributeChangedNotification.HasAgentId || (this.HasAgentId && !this.AgentId.Equals(attributeChangedNotification.AgentId)))
			{
				return false;
			}
			if (this.HasSubscriberId != attributeChangedNotification.HasSubscriberId || (this.HasSubscriberId && !this.SubscriberId.Equals(attributeChangedNotification.SubscriberId)))
			{
				return false;
			}
			if (this.HasChannelId != attributeChangedNotification.HasChannelId || (this.HasChannelId && !this.ChannelId.Equals(attributeChangedNotification.ChannelId)))
			{
				return false;
			}
			if (this.Attribute.Count != attributeChangedNotification.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Attribute.Count; i++)
			{
				if (!this.Attribute[i].Equals(attributeChangedNotification.Attribute[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000E7B RID: 3707
		// (get) Token: 0x06004D82 RID: 19842 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004D83 RID: 19843 RVA: 0x000F0910 File Offset: 0x000EEB10
		public static AttributeChangedNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<AttributeChangedNotification>(bs, 0, -1);
		}

		// Token: 0x06004D84 RID: 19844 RVA: 0x000F091A File Offset: 0x000EEB1A
		public void Deserialize(Stream stream)
		{
			AttributeChangedNotification.Deserialize(stream, this);
		}

		// Token: 0x06004D85 RID: 19845 RVA: 0x000F0924 File Offset: 0x000EEB24
		public static AttributeChangedNotification Deserialize(Stream stream, AttributeChangedNotification instance)
		{
			return AttributeChangedNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004D86 RID: 19846 RVA: 0x000F0930 File Offset: 0x000EEB30
		public static AttributeChangedNotification DeserializeLengthDelimited(Stream stream)
		{
			AttributeChangedNotification attributeChangedNotification = new AttributeChangedNotification();
			AttributeChangedNotification.DeserializeLengthDelimited(stream, attributeChangedNotification);
			return attributeChangedNotification;
		}

		// Token: 0x06004D87 RID: 19847 RVA: 0x000F094C File Offset: 0x000EEB4C
		public static AttributeChangedNotification DeserializeLengthDelimited(Stream stream, AttributeChangedNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AttributeChangedNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x06004D88 RID: 19848 RVA: 0x000F0974 File Offset: 0x000EEB74
		public static AttributeChangedNotification Deserialize(Stream stream, AttributeChangedNotification instance, long limit)
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
				else
				{
					if (num <= 18)
					{
						if (num != 10)
						{
							if (num == 18)
							{
								if (instance.SubscriberId == null)
								{
									instance.SubscriberId = GameAccountHandle.DeserializeLengthDelimited(stream);
									continue;
								}
								GameAccountHandle.DeserializeLengthDelimited(stream, instance.SubscriberId);
								continue;
							}
						}
						else
						{
							if (instance.AgentId == null)
							{
								instance.AgentId = GameAccountHandle.DeserializeLengthDelimited(stream);
								continue;
							}
							GameAccountHandle.DeserializeLengthDelimited(stream, instance.AgentId);
							continue;
						}
					}
					else if (num != 26)
					{
						if (num == 34)
						{
							instance.Attribute.Add(bnet.protocol.v2.Attribute.DeserializeLengthDelimited(stream));
							continue;
						}
					}
					else
					{
						if (instance.ChannelId == null)
						{
							instance.ChannelId = ChannelId.DeserializeLengthDelimited(stream);
							continue;
						}
						ChannelId.DeserializeLengthDelimited(stream, instance.ChannelId);
						continue;
					}
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

		// Token: 0x06004D89 RID: 19849 RVA: 0x000F0AB7 File Offset: 0x000EECB7
		public void Serialize(Stream stream)
		{
			AttributeChangedNotification.Serialize(stream, this);
		}

		// Token: 0x06004D8A RID: 19850 RVA: 0x000F0AC0 File Offset: 0x000EECC0
		public static void Serialize(Stream stream, AttributeChangedNotification instance)
		{
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.AgentId);
			}
			if (instance.HasSubscriberId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.SubscriberId.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.SubscriberId);
			}
			if (instance.HasChannelId)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.ChannelId.GetSerializedSize());
				ChannelId.Serialize(stream, instance.ChannelId);
			}
			if (instance.Attribute.Count > 0)
			{
				foreach (bnet.protocol.v2.Attribute attribute in instance.Attribute)
				{
					stream.WriteByte(34);
					ProtocolParser.WriteUInt32(stream, attribute.GetSerializedSize());
					bnet.protocol.v2.Attribute.Serialize(stream, attribute);
				}
			}
		}

		// Token: 0x06004D8B RID: 19851 RVA: 0x000F0BBC File Offset: 0x000EEDBC
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAgentId)
			{
				num += 1U;
				uint serializedSize = this.AgentId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasSubscriberId)
			{
				num += 1U;
				uint serializedSize2 = this.SubscriberId.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasChannelId)
			{
				num += 1U;
				uint serializedSize3 = this.ChannelId.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			if (this.Attribute.Count > 0)
			{
				foreach (bnet.protocol.v2.Attribute attribute in this.Attribute)
				{
					num += 1U;
					uint serializedSize4 = attribute.GetSerializedSize();
					num += serializedSize4 + ProtocolParser.SizeOfUInt32(serializedSize4);
				}
			}
			return num;
		}

		// Token: 0x04001931 RID: 6449
		public bool HasAgentId;

		// Token: 0x04001932 RID: 6450
		private GameAccountHandle _AgentId;

		// Token: 0x04001933 RID: 6451
		public bool HasSubscriberId;

		// Token: 0x04001934 RID: 6452
		private GameAccountHandle _SubscriberId;

		// Token: 0x04001935 RID: 6453
		public bool HasChannelId;

		// Token: 0x04001936 RID: 6454
		private ChannelId _ChannelId;

		// Token: 0x04001937 RID: 6455
		private List<bnet.protocol.v2.Attribute> _Attribute = new List<bnet.protocol.v2.Attribute>();
	}
}
