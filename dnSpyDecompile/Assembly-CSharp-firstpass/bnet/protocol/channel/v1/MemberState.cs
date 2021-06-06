using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.channel.v1
{
	// Token: 0x020004D8 RID: 1240
	public class MemberState : IProtoBuf
	{
		// Token: 0x1700107A RID: 4218
		// (get) Token: 0x06005771 RID: 22385 RVA: 0x0010C48C File Offset: 0x0010A68C
		// (set) Token: 0x06005772 RID: 22386 RVA: 0x0010C494 File Offset: 0x0010A694
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

		// Token: 0x1700107B RID: 4219
		// (get) Token: 0x06005773 RID: 22387 RVA: 0x0010C48C File Offset: 0x0010A68C
		public List<Attribute> AttributeList
		{
			get
			{
				return this._Attribute;
			}
		}

		// Token: 0x1700107C RID: 4220
		// (get) Token: 0x06005774 RID: 22388 RVA: 0x0010C49D File Offset: 0x0010A69D
		public int AttributeCount
		{
			get
			{
				return this._Attribute.Count;
			}
		}

		// Token: 0x06005775 RID: 22389 RVA: 0x0010C4AA File Offset: 0x0010A6AA
		public void AddAttribute(Attribute val)
		{
			this._Attribute.Add(val);
		}

		// Token: 0x06005776 RID: 22390 RVA: 0x0010C4B8 File Offset: 0x0010A6B8
		public void ClearAttribute()
		{
			this._Attribute.Clear();
		}

		// Token: 0x06005777 RID: 22391 RVA: 0x0010C4C5 File Offset: 0x0010A6C5
		public void SetAttribute(List<Attribute> val)
		{
			this.Attribute = val;
		}

		// Token: 0x1700107D RID: 4221
		// (get) Token: 0x06005778 RID: 22392 RVA: 0x0010C4CE File Offset: 0x0010A6CE
		// (set) Token: 0x06005779 RID: 22393 RVA: 0x0010C4D6 File Offset: 0x0010A6D6
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

		// Token: 0x1700107E RID: 4222
		// (get) Token: 0x0600577A RID: 22394 RVA: 0x0010C4CE File Offset: 0x0010A6CE
		public List<uint> RoleList
		{
			get
			{
				return this._Role;
			}
		}

		// Token: 0x1700107F RID: 4223
		// (get) Token: 0x0600577B RID: 22395 RVA: 0x0010C4DF File Offset: 0x0010A6DF
		public int RoleCount
		{
			get
			{
				return this._Role.Count;
			}
		}

		// Token: 0x0600577C RID: 22396 RVA: 0x0010C4EC File Offset: 0x0010A6EC
		public void AddRole(uint val)
		{
			this._Role.Add(val);
		}

		// Token: 0x0600577D RID: 22397 RVA: 0x0010C4FA File Offset: 0x0010A6FA
		public void ClearRole()
		{
			this._Role.Clear();
		}

		// Token: 0x0600577E RID: 22398 RVA: 0x0010C507 File Offset: 0x0010A707
		public void SetRole(List<uint> val)
		{
			this.Role = val;
		}

		// Token: 0x17001080 RID: 4224
		// (get) Token: 0x0600577F RID: 22399 RVA: 0x0010C510 File Offset: 0x0010A710
		// (set) Token: 0x06005780 RID: 22400 RVA: 0x0010C518 File Offset: 0x0010A718
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

		// Token: 0x06005781 RID: 22401 RVA: 0x0010C528 File Offset: 0x0010A728
		public void SetPrivileges(ulong val)
		{
			this.Privileges = val;
		}

		// Token: 0x17001081 RID: 4225
		// (get) Token: 0x06005782 RID: 22402 RVA: 0x0010C531 File Offset: 0x0010A731
		// (set) Token: 0x06005783 RID: 22403 RVA: 0x0010C539 File Offset: 0x0010A739
		public MemberAccountInfo Info
		{
			get
			{
				return this._Info;
			}
			set
			{
				this._Info = value;
				this.HasInfo = (value != null);
			}
		}

		// Token: 0x06005784 RID: 22404 RVA: 0x0010C54C File Offset: 0x0010A74C
		public void SetInfo(MemberAccountInfo val)
		{
			this.Info = val;
		}

		// Token: 0x06005785 RID: 22405 RVA: 0x0010C558 File Offset: 0x0010A758
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
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
			if (this.HasInfo)
			{
				num ^= this.Info.GetHashCode();
			}
			return num;
		}

		// Token: 0x06005786 RID: 22406 RVA: 0x0010C630 File Offset: 0x0010A830
		public override bool Equals(object obj)
		{
			MemberState memberState = obj as MemberState;
			if (memberState == null)
			{
				return false;
			}
			if (this.Attribute.Count != memberState.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Attribute.Count; i++)
			{
				if (!this.Attribute[i].Equals(memberState.Attribute[i]))
				{
					return false;
				}
			}
			if (this.Role.Count != memberState.Role.Count)
			{
				return false;
			}
			for (int j = 0; j < this.Role.Count; j++)
			{
				if (!this.Role[j].Equals(memberState.Role[j]))
				{
					return false;
				}
			}
			return this.HasPrivileges == memberState.HasPrivileges && (!this.HasPrivileges || this.Privileges.Equals(memberState.Privileges)) && this.HasInfo == memberState.HasInfo && (!this.HasInfo || this.Info.Equals(memberState.Info));
		}

		// Token: 0x17001082 RID: 4226
		// (get) Token: 0x06005787 RID: 22407 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005788 RID: 22408 RVA: 0x0010C749 File Offset: 0x0010A949
		public static MemberState ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<MemberState>(bs, 0, -1);
		}

		// Token: 0x06005789 RID: 22409 RVA: 0x0010C753 File Offset: 0x0010A953
		public void Deserialize(Stream stream)
		{
			MemberState.Deserialize(stream, this);
		}

		// Token: 0x0600578A RID: 22410 RVA: 0x0010C75D File Offset: 0x0010A95D
		public static MemberState Deserialize(Stream stream, MemberState instance)
		{
			return MemberState.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600578B RID: 22411 RVA: 0x0010C768 File Offset: 0x0010A968
		public static MemberState DeserializeLengthDelimited(Stream stream)
		{
			MemberState memberState = new MemberState();
			MemberState.DeserializeLengthDelimited(stream, memberState);
			return memberState;
		}

		// Token: 0x0600578C RID: 22412 RVA: 0x0010C784 File Offset: 0x0010A984
		public static MemberState DeserializeLengthDelimited(Stream stream, MemberState instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return MemberState.Deserialize(stream, instance, num);
		}

		// Token: 0x0600578D RID: 22413 RVA: 0x0010C7AC File Offset: 0x0010A9AC
		public static MemberState Deserialize(Stream stream, MemberState instance, long limit)
		{
			if (instance.Attribute == null)
			{
				instance.Attribute = new List<Attribute>();
			}
			if (instance.Role == null)
			{
				instance.Role = new List<uint>();
			}
			instance.Privileges = 0UL;
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
						if (num == 10)
						{
							instance.Attribute.Add(bnet.protocol.Attribute.DeserializeLengthDelimited(stream));
							continue;
						}
						if (num == 18)
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
						if (num == 24)
						{
							instance.Privileges = ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 34)
						{
							if (instance.Info == null)
							{
								instance.Info = MemberAccountInfo.DeserializeLengthDelimited(stream);
								continue;
							}
							MemberAccountInfo.DeserializeLengthDelimited(stream, instance.Info);
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

		// Token: 0x0600578E RID: 22414 RVA: 0x0010C906 File Offset: 0x0010AB06
		public void Serialize(Stream stream)
		{
			MemberState.Serialize(stream, this);
		}

		// Token: 0x0600578F RID: 22415 RVA: 0x0010C910 File Offset: 0x0010AB10
		public static void Serialize(Stream stream, MemberState instance)
		{
			if (instance.Attribute.Count > 0)
			{
				foreach (Attribute attribute in instance.Attribute)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, attribute.GetSerializedSize());
					bnet.protocol.Attribute.Serialize(stream, attribute);
				}
			}
			if (instance.Role.Count > 0)
			{
				stream.WriteByte(18);
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
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, instance.Privileges);
			}
			if (instance.HasInfo)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.Info.GetSerializedSize());
				MemberAccountInfo.Serialize(stream, instance.Info);
			}
		}

		// Token: 0x06005790 RID: 22416 RVA: 0x0010CA7C File Offset: 0x0010AC7C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.Attribute.Count > 0)
			{
				foreach (Attribute attribute in this.Attribute)
				{
					num += 1U;
					uint serializedSize = attribute.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
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
			if (this.HasInfo)
			{
				num += 1U;
				uint serializedSize2 = this.Info.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num;
		}

		// Token: 0x04001B7D RID: 7037
		private List<Attribute> _Attribute = new List<Attribute>();

		// Token: 0x04001B7E RID: 7038
		private List<uint> _Role = new List<uint>();

		// Token: 0x04001B7F RID: 7039
		public bool HasPrivileges;

		// Token: 0x04001B80 RID: 7040
		private ulong _Privileges;

		// Token: 0x04001B81 RID: 7041
		public bool HasInfo;

		// Token: 0x04001B82 RID: 7042
		private MemberAccountInfo _Info;
	}
}
