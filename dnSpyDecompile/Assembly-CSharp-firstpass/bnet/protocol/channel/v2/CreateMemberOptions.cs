using System;
using System.Collections.Generic;
using System.IO;
using bnet.protocol.account.v1;
using bnet.protocol.v2;

namespace bnet.protocol.channel.v2
{
	// Token: 0x02000474 RID: 1140
	public class CreateMemberOptions : IProtoBuf
	{
		// Token: 0x17000EAC RID: 3756
		// (get) Token: 0x06004E7D RID: 20093 RVA: 0x000F3907 File Offset: 0x000F1B07
		// (set) Token: 0x06004E7E RID: 20094 RVA: 0x000F390F File Offset: 0x000F1B0F
		public GameAccountHandle MemberId
		{
			get
			{
				return this._MemberId;
			}
			set
			{
				this._MemberId = value;
				this.HasMemberId = (value != null);
			}
		}

		// Token: 0x06004E7F RID: 20095 RVA: 0x000F3922 File Offset: 0x000F1B22
		public void SetMemberId(GameAccountHandle val)
		{
			this.MemberId = val;
		}

		// Token: 0x17000EAD RID: 3757
		// (get) Token: 0x06004E80 RID: 20096 RVA: 0x000F392B File Offset: 0x000F1B2B
		// (set) Token: 0x06004E81 RID: 20097 RVA: 0x000F3933 File Offset: 0x000F1B33
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

		// Token: 0x17000EAE RID: 3758
		// (get) Token: 0x06004E82 RID: 20098 RVA: 0x000F392B File Offset: 0x000F1B2B
		public List<bnet.protocol.v2.Attribute> AttributeList
		{
			get
			{
				return this._Attribute;
			}
		}

		// Token: 0x17000EAF RID: 3759
		// (get) Token: 0x06004E83 RID: 20099 RVA: 0x000F393C File Offset: 0x000F1B3C
		public int AttributeCount
		{
			get
			{
				return this._Attribute.Count;
			}
		}

		// Token: 0x06004E84 RID: 20100 RVA: 0x000F3949 File Offset: 0x000F1B49
		public void AddAttribute(bnet.protocol.v2.Attribute val)
		{
			this._Attribute.Add(val);
		}

		// Token: 0x06004E85 RID: 20101 RVA: 0x000F3957 File Offset: 0x000F1B57
		public void ClearAttribute()
		{
			this._Attribute.Clear();
		}

		// Token: 0x06004E86 RID: 20102 RVA: 0x000F3964 File Offset: 0x000F1B64
		public void SetAttribute(List<bnet.protocol.v2.Attribute> val)
		{
			this.Attribute = val;
		}

		// Token: 0x06004E87 RID: 20103 RVA: 0x000F3970 File Offset: 0x000F1B70
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasMemberId)
			{
				num ^= this.MemberId.GetHashCode();
			}
			foreach (bnet.protocol.v2.Attribute attribute in this.Attribute)
			{
				num ^= attribute.GetHashCode();
			}
			return num;
		}

		// Token: 0x06004E88 RID: 20104 RVA: 0x000F39E8 File Offset: 0x000F1BE8
		public override bool Equals(object obj)
		{
			CreateMemberOptions createMemberOptions = obj as CreateMemberOptions;
			if (createMemberOptions == null)
			{
				return false;
			}
			if (this.HasMemberId != createMemberOptions.HasMemberId || (this.HasMemberId && !this.MemberId.Equals(createMemberOptions.MemberId)))
			{
				return false;
			}
			if (this.Attribute.Count != createMemberOptions.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Attribute.Count; i++)
			{
				if (!this.Attribute[i].Equals(createMemberOptions.Attribute[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000EB0 RID: 3760
		// (get) Token: 0x06004E89 RID: 20105 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004E8A RID: 20106 RVA: 0x000F3A7E File Offset: 0x000F1C7E
		public static CreateMemberOptions ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<CreateMemberOptions>(bs, 0, -1);
		}

		// Token: 0x06004E8B RID: 20107 RVA: 0x000F3A88 File Offset: 0x000F1C88
		public void Deserialize(Stream stream)
		{
			CreateMemberOptions.Deserialize(stream, this);
		}

		// Token: 0x06004E8C RID: 20108 RVA: 0x000F3A92 File Offset: 0x000F1C92
		public static CreateMemberOptions Deserialize(Stream stream, CreateMemberOptions instance)
		{
			return CreateMemberOptions.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004E8D RID: 20109 RVA: 0x000F3AA0 File Offset: 0x000F1CA0
		public static CreateMemberOptions DeserializeLengthDelimited(Stream stream)
		{
			CreateMemberOptions createMemberOptions = new CreateMemberOptions();
			CreateMemberOptions.DeserializeLengthDelimited(stream, createMemberOptions);
			return createMemberOptions;
		}

		// Token: 0x06004E8E RID: 20110 RVA: 0x000F3ABC File Offset: 0x000F1CBC
		public static CreateMemberOptions DeserializeLengthDelimited(Stream stream, CreateMemberOptions instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return CreateMemberOptions.Deserialize(stream, instance, num);
		}

		// Token: 0x06004E8F RID: 20111 RVA: 0x000F3AE4 File Offset: 0x000F1CE4
		public static CreateMemberOptions Deserialize(Stream stream, CreateMemberOptions instance, long limit)
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
				else if (instance.MemberId == null)
				{
					instance.MemberId = GameAccountHandle.DeserializeLengthDelimited(stream);
				}
				else
				{
					GameAccountHandle.DeserializeLengthDelimited(stream, instance.MemberId);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06004E90 RID: 20112 RVA: 0x000F3BAE File Offset: 0x000F1DAE
		public void Serialize(Stream stream)
		{
			CreateMemberOptions.Serialize(stream, this);
		}

		// Token: 0x06004E91 RID: 20113 RVA: 0x000F3BB8 File Offset: 0x000F1DB8
		public static void Serialize(Stream stream, CreateMemberOptions instance)
		{
			if (instance.HasMemberId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.MemberId.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.MemberId);
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

		// Token: 0x06004E92 RID: 20114 RVA: 0x000F3C5C File Offset: 0x000F1E5C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasMemberId)
			{
				num += 1U;
				uint serializedSize = this.MemberId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.Attribute.Count > 0)
			{
				foreach (bnet.protocol.v2.Attribute attribute in this.Attribute)
				{
					num += 1U;
					uint serializedSize2 = attribute.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			return num;
		}

		// Token: 0x0400197A RID: 6522
		public bool HasMemberId;

		// Token: 0x0400197B RID: 6523
		private GameAccountHandle _MemberId;

		// Token: 0x0400197C RID: 6524
		private List<bnet.protocol.v2.Attribute> _Attribute = new List<bnet.protocol.v2.Attribute>();
	}
}
