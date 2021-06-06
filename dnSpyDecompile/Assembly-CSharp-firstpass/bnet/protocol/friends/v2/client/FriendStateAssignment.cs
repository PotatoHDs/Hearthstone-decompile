using System;
using System.Collections.Generic;
using System.IO;
using bnet.protocol.friends.v2.client.Types;
using bnet.protocol.v2;

namespace bnet.protocol.friends.v2.client
{
	// Token: 0x02000416 RID: 1046
	public class FriendStateAssignment : IProtoBuf
	{
		// Token: 0x17000CE5 RID: 3301
		// (get) Token: 0x060045D0 RID: 17872 RVA: 0x000DB26F File Offset: 0x000D946F
		// (set) Token: 0x060045D1 RID: 17873 RVA: 0x000DB277 File Offset: 0x000D9477
		public ulong Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				this._Id = value;
				this.HasId = true;
			}
		}

		// Token: 0x060045D2 RID: 17874 RVA: 0x000DB287 File Offset: 0x000D9487
		public void SetId(ulong val)
		{
			this.Id = val;
		}

		// Token: 0x17000CE6 RID: 3302
		// (get) Token: 0x060045D3 RID: 17875 RVA: 0x000DB290 File Offset: 0x000D9490
		// (set) Token: 0x060045D4 RID: 17876 RVA: 0x000DB298 File Offset: 0x000D9498
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

		// Token: 0x060045D5 RID: 17877 RVA: 0x000DB2A8 File Offset: 0x000D94A8
		public void SetLevel(FriendLevel val)
		{
			this.Level = val;
		}

		// Token: 0x17000CE7 RID: 3303
		// (get) Token: 0x060045D6 RID: 17878 RVA: 0x000DB2B1 File Offset: 0x000D94B1
		// (set) Token: 0x060045D7 RID: 17879 RVA: 0x000DB2B9 File Offset: 0x000D94B9
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

		// Token: 0x17000CE8 RID: 3304
		// (get) Token: 0x060045D8 RID: 17880 RVA: 0x000DB2B1 File Offset: 0x000D94B1
		public List<bnet.protocol.v2.Attribute> AttributeList
		{
			get
			{
				return this._Attribute;
			}
		}

		// Token: 0x17000CE9 RID: 3305
		// (get) Token: 0x060045D9 RID: 17881 RVA: 0x000DB2C2 File Offset: 0x000D94C2
		public int AttributeCount
		{
			get
			{
				return this._Attribute.Count;
			}
		}

		// Token: 0x060045DA RID: 17882 RVA: 0x000DB2CF File Offset: 0x000D94CF
		public void AddAttribute(bnet.protocol.v2.Attribute val)
		{
			this._Attribute.Add(val);
		}

		// Token: 0x060045DB RID: 17883 RVA: 0x000DB2DD File Offset: 0x000D94DD
		public void ClearAttribute()
		{
			this._Attribute.Clear();
		}

		// Token: 0x060045DC RID: 17884 RVA: 0x000DB2EA File Offset: 0x000D94EA
		public void SetAttribute(List<bnet.protocol.v2.Attribute> val)
		{
			this.Attribute = val;
		}

		// Token: 0x17000CEA RID: 3306
		// (get) Token: 0x060045DD RID: 17885 RVA: 0x000DB2F3 File Offset: 0x000D94F3
		// (set) Token: 0x060045DE RID: 17886 RVA: 0x000DB2FB File Offset: 0x000D94FB
		public ulong ModifiedTimeUs
		{
			get
			{
				return this._ModifiedTimeUs;
			}
			set
			{
				this._ModifiedTimeUs = value;
				this.HasModifiedTimeUs = true;
			}
		}

		// Token: 0x060045DF RID: 17887 RVA: 0x000DB30B File Offset: 0x000D950B
		public void SetModifiedTimeUs(ulong val)
		{
			this.ModifiedTimeUs = val;
		}

		// Token: 0x060045E0 RID: 17888 RVA: 0x000DB314 File Offset: 0x000D9514
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasId)
			{
				num ^= this.Id.GetHashCode();
			}
			if (this.HasLevel)
			{
				num ^= this.Level.GetHashCode();
			}
			foreach (bnet.protocol.v2.Attribute attribute in this.Attribute)
			{
				num ^= attribute.GetHashCode();
			}
			if (this.HasModifiedTimeUs)
			{
				num ^= this.ModifiedTimeUs.GetHashCode();
			}
			return num;
		}

		// Token: 0x060045E1 RID: 17889 RVA: 0x000DB3CC File Offset: 0x000D95CC
		public override bool Equals(object obj)
		{
			FriendStateAssignment friendStateAssignment = obj as FriendStateAssignment;
			if (friendStateAssignment == null)
			{
				return false;
			}
			if (this.HasId != friendStateAssignment.HasId || (this.HasId && !this.Id.Equals(friendStateAssignment.Id)))
			{
				return false;
			}
			if (this.HasLevel != friendStateAssignment.HasLevel || (this.HasLevel && !this.Level.Equals(friendStateAssignment.Level)))
			{
				return false;
			}
			if (this.Attribute.Count != friendStateAssignment.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Attribute.Count; i++)
			{
				if (!this.Attribute[i].Equals(friendStateAssignment.Attribute[i]))
				{
					return false;
				}
			}
			return this.HasModifiedTimeUs == friendStateAssignment.HasModifiedTimeUs && (!this.HasModifiedTimeUs || this.ModifiedTimeUs.Equals(friendStateAssignment.ModifiedTimeUs));
		}

		// Token: 0x17000CEB RID: 3307
		// (get) Token: 0x060045E2 RID: 17890 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060045E3 RID: 17891 RVA: 0x000DB4CC File Offset: 0x000D96CC
		public static FriendStateAssignment ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<FriendStateAssignment>(bs, 0, -1);
		}

		// Token: 0x060045E4 RID: 17892 RVA: 0x000DB4D6 File Offset: 0x000D96D6
		public void Deserialize(Stream stream)
		{
			FriendStateAssignment.Deserialize(stream, this);
		}

		// Token: 0x060045E5 RID: 17893 RVA: 0x000DB4E0 File Offset: 0x000D96E0
		public static FriendStateAssignment Deserialize(Stream stream, FriendStateAssignment instance)
		{
			return FriendStateAssignment.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060045E6 RID: 17894 RVA: 0x000DB4EC File Offset: 0x000D96EC
		public static FriendStateAssignment DeserializeLengthDelimited(Stream stream)
		{
			FriendStateAssignment friendStateAssignment = new FriendStateAssignment();
			FriendStateAssignment.DeserializeLengthDelimited(stream, friendStateAssignment);
			return friendStateAssignment;
		}

		// Token: 0x060045E7 RID: 17895 RVA: 0x000DB508 File Offset: 0x000D9708
		public static FriendStateAssignment DeserializeLengthDelimited(Stream stream, FriendStateAssignment instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return FriendStateAssignment.Deserialize(stream, instance, num);
		}

		// Token: 0x060045E8 RID: 17896 RVA: 0x000DB530 File Offset: 0x000D9730
		public static FriendStateAssignment Deserialize(Stream stream, FriendStateAssignment instance, long limit)
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
				else
				{
					if (num <= 16)
					{
						if (num == 8)
						{
							instance.Id = ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 16)
						{
							instance.Level = (FriendLevel)ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					else
					{
						if (num == 42)
						{
							instance.Attribute.Add(bnet.protocol.v2.Attribute.DeserializeLengthDelimited(stream));
							continue;
						}
						if (num == 56)
						{
							instance.ModifiedTimeUs = ProtocolParser.ReadUInt64(stream);
							continue;
						}
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

		// Token: 0x060045E9 RID: 17897 RVA: 0x000DB620 File Offset: 0x000D9820
		public void Serialize(Stream stream)
		{
			FriendStateAssignment.Serialize(stream, this);
		}

		// Token: 0x060045EA RID: 17898 RVA: 0x000DB62C File Offset: 0x000D982C
		public static void Serialize(Stream stream, FriendStateAssignment instance)
		{
			if (instance.HasId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, instance.Id);
			}
			if (instance.HasLevel)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Level));
			}
			if (instance.Attribute.Count > 0)
			{
				foreach (bnet.protocol.v2.Attribute attribute in instance.Attribute)
				{
					stream.WriteByte(42);
					ProtocolParser.WriteUInt32(stream, attribute.GetSerializedSize());
					bnet.protocol.v2.Attribute.Serialize(stream, attribute);
				}
			}
			if (instance.HasModifiedTimeUs)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteUInt64(stream, instance.ModifiedTimeUs);
			}
		}

		// Token: 0x060045EB RID: 17899 RVA: 0x000DB6F8 File Offset: 0x000D98F8
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.Id);
			}
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
			if (this.HasModifiedTimeUs)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.ModifiedTimeUs);
			}
			return num;
		}

		// Token: 0x04001777 RID: 6007
		public bool HasId;

		// Token: 0x04001778 RID: 6008
		private ulong _Id;

		// Token: 0x04001779 RID: 6009
		public bool HasLevel;

		// Token: 0x0400177A RID: 6010
		private FriendLevel _Level;

		// Token: 0x0400177B RID: 6011
		private List<bnet.protocol.v2.Attribute> _Attribute = new List<bnet.protocol.v2.Attribute>();

		// Token: 0x0400177C RID: 6012
		public bool HasModifiedTimeUs;

		// Token: 0x0400177D RID: 6013
		private ulong _ModifiedTimeUs;
	}
}
