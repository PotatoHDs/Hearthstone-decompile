using System;
using System.Collections.Generic;
using System.IO;
using bnet.protocol.friends.v2.client.Types;
using bnet.protocol.v2;

namespace bnet.protocol.friends.v2.client
{
	// Token: 0x0200040C RID: 1036
	public class SendInvitationOptions : IProtoBuf
	{
		// Token: 0x17000CAF RID: 3247
		// (get) Token: 0x060044D4 RID: 17620 RVA: 0x000D86D6 File Offset: 0x000D68D6
		// (set) Token: 0x060044D5 RID: 17621 RVA: 0x000D86DE File Offset: 0x000D68DE
		public FriendLevel Level
		{
			get
			{
				return this._Level;
			}
			set
			{
				this._Level = value;
				this.HasLevel = true;
			}
		}

		// Token: 0x060044D6 RID: 17622 RVA: 0x000D86EE File Offset: 0x000D68EE
		public void SetLevel(FriendLevel val)
		{
			this.Level = val;
		}

		// Token: 0x17000CB0 RID: 3248
		// (get) Token: 0x060044D7 RID: 17623 RVA: 0x000D86F7 File Offset: 0x000D68F7
		// (set) Token: 0x060044D8 RID: 17624 RVA: 0x000D86FF File Offset: 0x000D68FF
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

		// Token: 0x17000CB1 RID: 3249
		// (get) Token: 0x060044D9 RID: 17625 RVA: 0x000D86F7 File Offset: 0x000D68F7
		public List<bnet.protocol.v2.Attribute> AttributeList
		{
			get
			{
				return this._Attribute;
			}
		}

		// Token: 0x17000CB2 RID: 3250
		// (get) Token: 0x060044DA RID: 17626 RVA: 0x000D8708 File Offset: 0x000D6908
		public int AttributeCount
		{
			get
			{
				return this._Attribute.Count;
			}
		}

		// Token: 0x060044DB RID: 17627 RVA: 0x000D8715 File Offset: 0x000D6915
		public void AddAttribute(bnet.protocol.v2.Attribute val)
		{
			this._Attribute.Add(val);
		}

		// Token: 0x060044DC RID: 17628 RVA: 0x000D8723 File Offset: 0x000D6923
		public void ClearAttribute()
		{
			this._Attribute.Clear();
		}

		// Token: 0x060044DD RID: 17629 RVA: 0x000D8730 File Offset: 0x000D6930
		public void SetAttribute(List<bnet.protocol.v2.Attribute> val)
		{
			this.Attribute = val;
		}

		// Token: 0x060044DE RID: 17630 RVA: 0x000D873C File Offset: 0x000D693C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasLevel)
			{
				num ^= this.Level.GetHashCode();
			}
			foreach (bnet.protocol.v2.Attribute attribute in this.Attribute)
			{
				num ^= attribute.GetHashCode();
			}
			return num;
		}

		// Token: 0x060044DF RID: 17631 RVA: 0x000D87C0 File Offset: 0x000D69C0
		public override bool Equals(object obj)
		{
			SendInvitationOptions sendInvitationOptions = obj as SendInvitationOptions;
			if (sendInvitationOptions == null)
			{
				return false;
			}
			if (this.HasLevel != sendInvitationOptions.HasLevel || (this.HasLevel && !this.Level.Equals(sendInvitationOptions.Level)))
			{
				return false;
			}
			if (this.Attribute.Count != sendInvitationOptions.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Attribute.Count; i++)
			{
				if (!this.Attribute[i].Equals(sendInvitationOptions.Attribute[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000CB3 RID: 3251
		// (get) Token: 0x060044E0 RID: 17632 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060044E1 RID: 17633 RVA: 0x000D8864 File Offset: 0x000D6A64
		public static SendInvitationOptions ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SendInvitationOptions>(bs, 0, -1);
		}

		// Token: 0x060044E2 RID: 17634 RVA: 0x000D886E File Offset: 0x000D6A6E
		public void Deserialize(Stream stream)
		{
			SendInvitationOptions.Deserialize(stream, this);
		}

		// Token: 0x060044E3 RID: 17635 RVA: 0x000D8878 File Offset: 0x000D6A78
		public static SendInvitationOptions Deserialize(Stream stream, SendInvitationOptions instance)
		{
			return SendInvitationOptions.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060044E4 RID: 17636 RVA: 0x000D8884 File Offset: 0x000D6A84
		public static SendInvitationOptions DeserializeLengthDelimited(Stream stream)
		{
			SendInvitationOptions sendInvitationOptions = new SendInvitationOptions();
			SendInvitationOptions.DeserializeLengthDelimited(stream, sendInvitationOptions);
			return sendInvitationOptions;
		}

		// Token: 0x060044E5 RID: 17637 RVA: 0x000D88A0 File Offset: 0x000D6AA0
		public static SendInvitationOptions DeserializeLengthDelimited(Stream stream, SendInvitationOptions instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SendInvitationOptions.Deserialize(stream, instance, num);
		}

		// Token: 0x060044E6 RID: 17638 RVA: 0x000D88C8 File Offset: 0x000D6AC8
		public static SendInvitationOptions Deserialize(Stream stream, SendInvitationOptions instance, long limit)
		{
			instance.Level = FriendLevel.FRIEND_LEVEL_BATTLE_TAG;
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
				else if (num != 8)
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
					else
					{
						instance.Attribute.Add(bnet.protocol.v2.Attribute.DeserializeLengthDelimited(stream));
					}
				}
				else
				{
					instance.Level = (FriendLevel)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060044E7 RID: 17639 RVA: 0x000D897F File Offset: 0x000D6B7F
		public void Serialize(Stream stream)
		{
			SendInvitationOptions.Serialize(stream, this);
		}

		// Token: 0x060044E8 RID: 17640 RVA: 0x000D8988 File Offset: 0x000D6B88
		public static void Serialize(Stream stream, SendInvitationOptions instance)
		{
			if (instance.HasLevel)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Level));
			}
			if (instance.Attribute.Count > 0)
			{
				foreach (bnet.protocol.v2.Attribute attribute in instance.Attribute)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, attribute.GetSerializedSize());
					bnet.protocol.v2.Attribute.Serialize(stream, attribute);
				}
			}
		}

		// Token: 0x060044E9 RID: 17641 RVA: 0x000D8A1C File Offset: 0x000D6C1C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasLevel)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Level));
			}
			if (this.Attribute.Count > 0)
			{
				foreach (bnet.protocol.v2.Attribute attribute in this.Attribute)
				{
					num += 1U;
					uint serializedSize = attribute.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num;
		}

		// Token: 0x04001738 RID: 5944
		public bool HasLevel;

		// Token: 0x04001739 RID: 5945
		private FriendLevel _Level;

		// Token: 0x0400173A RID: 5946
		private List<bnet.protocol.v2.Attribute> _Attribute = new List<bnet.protocol.v2.Attribute>();
	}
}
