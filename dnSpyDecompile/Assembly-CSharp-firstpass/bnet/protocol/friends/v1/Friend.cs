using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.friends.v1
{
	// Token: 0x0200042E RID: 1070
	public class Friend : IProtoBuf
	{
		// Token: 0x17000D46 RID: 3398
		// (get) Token: 0x060047CB RID: 18379 RVA: 0x000E06B8 File Offset: 0x000DE8B8
		// (set) Token: 0x060047CC RID: 18380 RVA: 0x000E06C0 File Offset: 0x000DE8C0
		public EntityId AccountId { get; set; }

		// Token: 0x060047CD RID: 18381 RVA: 0x000E06C9 File Offset: 0x000DE8C9
		public void SetAccountId(EntityId val)
		{
			this.AccountId = val;
		}

		// Token: 0x17000D47 RID: 3399
		// (get) Token: 0x060047CE RID: 18382 RVA: 0x000E06D2 File Offset: 0x000DE8D2
		// (set) Token: 0x060047CF RID: 18383 RVA: 0x000E06DA File Offset: 0x000DE8DA
		public List<Attribute> Attribute
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

		// Token: 0x17000D48 RID: 3400
		// (get) Token: 0x060047D0 RID: 18384 RVA: 0x000E06D2 File Offset: 0x000DE8D2
		public List<Attribute> AttributeList
		{
			get
			{
				return this._Attribute;
			}
		}

		// Token: 0x17000D49 RID: 3401
		// (get) Token: 0x060047D1 RID: 18385 RVA: 0x000E06E3 File Offset: 0x000DE8E3
		public int AttributeCount
		{
			get
			{
				return this._Attribute.Count;
			}
		}

		// Token: 0x060047D2 RID: 18386 RVA: 0x000E06F0 File Offset: 0x000DE8F0
		public void AddAttribute(Attribute val)
		{
			this._Attribute.Add(val);
		}

		// Token: 0x060047D3 RID: 18387 RVA: 0x000E06FE File Offset: 0x000DE8FE
		public void ClearAttribute()
		{
			this._Attribute.Clear();
		}

		// Token: 0x060047D4 RID: 18388 RVA: 0x000E070B File Offset: 0x000DE90B
		public void SetAttribute(List<Attribute> val)
		{
			this.Attribute = val;
		}

		// Token: 0x17000D4A RID: 3402
		// (get) Token: 0x060047D5 RID: 18389 RVA: 0x000E0714 File Offset: 0x000DE914
		// (set) Token: 0x060047D6 RID: 18390 RVA: 0x000E071C File Offset: 0x000DE91C
		public List<uint> Role
		{
			get
			{
				return this._Role;
			}
			set
			{
				this._Role = value;
			}
		}

		// Token: 0x17000D4B RID: 3403
		// (get) Token: 0x060047D7 RID: 18391 RVA: 0x000E0714 File Offset: 0x000DE914
		public List<uint> RoleList
		{
			get
			{
				return this._Role;
			}
		}

		// Token: 0x17000D4C RID: 3404
		// (get) Token: 0x060047D8 RID: 18392 RVA: 0x000E0725 File Offset: 0x000DE925
		public int RoleCount
		{
			get
			{
				return this._Role.Count;
			}
		}

		// Token: 0x060047D9 RID: 18393 RVA: 0x000E0732 File Offset: 0x000DE932
		public void AddRole(uint val)
		{
			this._Role.Add(val);
		}

		// Token: 0x060047DA RID: 18394 RVA: 0x000E0740 File Offset: 0x000DE940
		public void ClearRole()
		{
			this._Role.Clear();
		}

		// Token: 0x060047DB RID: 18395 RVA: 0x000E074D File Offset: 0x000DE94D
		public void SetRole(List<uint> val)
		{
			this.Role = val;
		}

		// Token: 0x17000D4D RID: 3405
		// (get) Token: 0x060047DC RID: 18396 RVA: 0x000E0756 File Offset: 0x000DE956
		// (set) Token: 0x060047DD RID: 18397 RVA: 0x000E075E File Offset: 0x000DE95E
		public ulong Privileges
		{
			get
			{
				return this._Privileges;
			}
			set
			{
				this._Privileges = value;
				this.HasPrivileges = true;
			}
		}

		// Token: 0x060047DE RID: 18398 RVA: 0x000E076E File Offset: 0x000DE96E
		public void SetPrivileges(ulong val)
		{
			this.Privileges = val;
		}

		// Token: 0x17000D4E RID: 3406
		// (get) Token: 0x060047DF RID: 18399 RVA: 0x000E0777 File Offset: 0x000DE977
		// (set) Token: 0x060047E0 RID: 18400 RVA: 0x000E077F File Offset: 0x000DE97F
		public ulong AttributesEpoch
		{
			get
			{
				return this._AttributesEpoch;
			}
			set
			{
				this._AttributesEpoch = value;
				this.HasAttributesEpoch = true;
			}
		}

		// Token: 0x060047E1 RID: 18401 RVA: 0x000E078F File Offset: 0x000DE98F
		public void SetAttributesEpoch(ulong val)
		{
			this.AttributesEpoch = val;
		}

		// Token: 0x17000D4F RID: 3407
		// (get) Token: 0x060047E2 RID: 18402 RVA: 0x000E0798 File Offset: 0x000DE998
		// (set) Token: 0x060047E3 RID: 18403 RVA: 0x000E07A0 File Offset: 0x000DE9A0
		public ulong CreationTime
		{
			get
			{
				return this._CreationTime;
			}
			set
			{
				this._CreationTime = value;
				this.HasCreationTime = true;
			}
		}

		// Token: 0x060047E4 RID: 18404 RVA: 0x000E07B0 File Offset: 0x000DE9B0
		public void SetCreationTime(ulong val)
		{
			this.CreationTime = val;
		}

		// Token: 0x060047E5 RID: 18405 RVA: 0x000E07BC File Offset: 0x000DE9BC
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.AccountId.GetHashCode();
			foreach (Attribute attribute in this.Attribute)
			{
				num ^= attribute.GetHashCode();
			}
			foreach (uint num2 in this.Role)
			{
				num ^= num2.GetHashCode();
			}
			if (this.HasPrivileges)
			{
				num ^= this.Privileges.GetHashCode();
			}
			if (this.HasAttributesEpoch)
			{
				num ^= this.AttributesEpoch.GetHashCode();
			}
			if (this.HasCreationTime)
			{
				num ^= this.CreationTime.GetHashCode();
			}
			return num;
		}

		// Token: 0x060047E6 RID: 18406 RVA: 0x000E08C0 File Offset: 0x000DEAC0
		public override bool Equals(object obj)
		{
			Friend friend = obj as Friend;
			if (friend == null)
			{
				return false;
			}
			if (!this.AccountId.Equals(friend.AccountId))
			{
				return false;
			}
			if (this.Attribute.Count != friend.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Attribute.Count; i++)
			{
				if (!this.Attribute[i].Equals(friend.Attribute[i]))
				{
					return false;
				}
			}
			if (this.Role.Count != friend.Role.Count)
			{
				return false;
			}
			for (int j = 0; j < this.Role.Count; j++)
			{
				if (!this.Role[j].Equals(friend.Role[j]))
				{
					return false;
				}
			}
			return this.HasPrivileges == friend.HasPrivileges && (!this.HasPrivileges || this.Privileges.Equals(friend.Privileges)) && this.HasAttributesEpoch == friend.HasAttributesEpoch && (!this.HasAttributesEpoch || this.AttributesEpoch.Equals(friend.AttributesEpoch)) && this.HasCreationTime == friend.HasCreationTime && (!this.HasCreationTime || this.CreationTime.Equals(friend.CreationTime));
		}

		// Token: 0x17000D50 RID: 3408
		// (get) Token: 0x060047E7 RID: 18407 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060047E8 RID: 18408 RVA: 0x000E0A21 File Offset: 0x000DEC21
		public static Friend ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<Friend>(bs, 0, -1);
		}

		// Token: 0x060047E9 RID: 18409 RVA: 0x000E0A2B File Offset: 0x000DEC2B
		public void Deserialize(Stream stream)
		{
			Friend.Deserialize(stream, this);
		}

		// Token: 0x060047EA RID: 18410 RVA: 0x000E0A35 File Offset: 0x000DEC35
		public static Friend Deserialize(Stream stream, Friend instance)
		{
			return Friend.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060047EB RID: 18411 RVA: 0x000E0A40 File Offset: 0x000DEC40
		public static Friend DeserializeLengthDelimited(Stream stream)
		{
			Friend friend = new Friend();
			Friend.DeserializeLengthDelimited(stream, friend);
			return friend;
		}

		// Token: 0x060047EC RID: 18412 RVA: 0x000E0A5C File Offset: 0x000DEC5C
		public static Friend DeserializeLengthDelimited(Stream stream, Friend instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return Friend.Deserialize(stream, instance, num);
		}

		// Token: 0x060047ED RID: 18413 RVA: 0x000E0A84 File Offset: 0x000DEC84
		public static Friend Deserialize(Stream stream, Friend instance, long limit)
		{
			if (instance.Attribute == null)
			{
				instance.Attribute = new List<Attribute>();
			}
			if (instance.Role == null)
			{
				instance.Role = new List<uint>();
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
					if (num <= 26)
					{
						if (num != 10)
						{
							if (num == 18)
							{
								instance.Attribute.Add(bnet.protocol.Attribute.DeserializeLengthDelimited(stream));
								continue;
							}
							if (num == 26)
							{
								long num2 = (long)((ulong)ProtocolParser.ReadUInt32(stream));
								num2 += stream.Position;
								while (stream.Position < num2)
								{
									instance.Role.Add(ProtocolParser.ReadUInt32(stream));
								}
								if (stream.Position != num2)
								{
									throw new ProtocolBufferException("Read too many bytes in packed data");
								}
								continue;
							}
						}
						else
						{
							if (instance.AccountId == null)
							{
								instance.AccountId = EntityId.DeserializeLengthDelimited(stream);
								continue;
							}
							EntityId.DeserializeLengthDelimited(stream, instance.AccountId);
							continue;
						}
					}
					else
					{
						if (num == 32)
						{
							instance.Privileges = ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 40)
						{
							instance.AttributesEpoch = ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 48)
						{
							instance.CreationTime = ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x060047EE RID: 18414 RVA: 0x000E0C0E File Offset: 0x000DEE0E
		public void Serialize(Stream stream)
		{
			Friend.Serialize(stream, this);
		}

		// Token: 0x060047EF RID: 18415 RVA: 0x000E0C18 File Offset: 0x000DEE18
		public static void Serialize(Stream stream, Friend instance)
		{
			if (instance.AccountId == null)
			{
				throw new ArgumentNullException("AccountId", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.AccountId.GetSerializedSize());
			EntityId.Serialize(stream, instance.AccountId);
			if (instance.Attribute.Count > 0)
			{
				foreach (Attribute attribute in instance.Attribute)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, attribute.GetSerializedSize());
					bnet.protocol.Attribute.Serialize(stream, attribute);
				}
			}
			if (instance.Role.Count > 0)
			{
				stream.WriteByte(26);
				uint num = 0U;
				foreach (uint val in instance.Role)
				{
					num += ProtocolParser.SizeOfUInt32(val);
				}
				ProtocolParser.WriteUInt32(stream, num);
				foreach (uint val2 in instance.Role)
				{
					ProtocolParser.WriteUInt32(stream, val2);
				}
			}
			if (instance.HasPrivileges)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, instance.Privileges);
			}
			if (instance.HasAttributesEpoch)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, instance.AttributesEpoch);
			}
			if (instance.HasCreationTime)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt64(stream, instance.CreationTime);
			}
		}

		// Token: 0x060047F0 RID: 18416 RVA: 0x000E0DCC File Offset: 0x000DEFCC
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint serializedSize = this.AccountId.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (this.Attribute.Count > 0)
			{
				foreach (Attribute attribute in this.Attribute)
				{
					num += 1U;
					uint serializedSize2 = attribute.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			if (this.Role.Count > 0)
			{
				num += 1U;
				uint num2 = num;
				foreach (uint val in this.Role)
				{
					num += ProtocolParser.SizeOfUInt32(val);
				}
				num += ProtocolParser.SizeOfUInt32(num - num2);
			}
			if (this.HasPrivileges)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.Privileges);
			}
			if (this.HasAttributesEpoch)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.AttributesEpoch);
			}
			if (this.HasCreationTime)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.CreationTime);
			}
			num += 1U;
			return num;
		}

		// Token: 0x040017E0 RID: 6112
		private List<Attribute> _Attribute = new List<Attribute>();

		// Token: 0x040017E1 RID: 6113
		private List<uint> _Role = new List<uint>();

		// Token: 0x040017E2 RID: 6114
		public bool HasPrivileges;

		// Token: 0x040017E3 RID: 6115
		private ulong _Privileges;

		// Token: 0x040017E4 RID: 6116
		public bool HasAttributesEpoch;

		// Token: 0x040017E5 RID: 6117
		private ulong _AttributesEpoch;

		// Token: 0x040017E6 RID: 6118
		public bool HasCreationTime;

		// Token: 0x040017E7 RID: 6119
		private ulong _CreationTime;
	}
}
