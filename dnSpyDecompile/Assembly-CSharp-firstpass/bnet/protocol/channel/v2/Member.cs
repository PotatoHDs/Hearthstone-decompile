using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using bnet.protocol.account.v1;
using bnet.protocol.v2;

namespace bnet.protocol.channel.v2
{
	// Token: 0x02000475 RID: 1141
	public class Member : IProtoBuf
	{
		// Token: 0x17000EB1 RID: 3761
		// (get) Token: 0x06004E94 RID: 20116 RVA: 0x000F3D03 File Offset: 0x000F1F03
		// (set) Token: 0x06004E95 RID: 20117 RVA: 0x000F3D0B File Offset: 0x000F1F0B
		public GameAccountHandle Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				this._Id = value;
				this.HasId = (value != null);
			}
		}

		// Token: 0x06004E96 RID: 20118 RVA: 0x000F3D1E File Offset: 0x000F1F1E
		public void SetId(GameAccountHandle val)
		{
			this.Id = val;
		}

		// Token: 0x17000EB2 RID: 3762
		// (get) Token: 0x06004E97 RID: 20119 RVA: 0x000F3D27 File Offset: 0x000F1F27
		// (set) Token: 0x06004E98 RID: 20120 RVA: 0x000F3D2F File Offset: 0x000F1F2F
		public string BattleTag
		{
			get
			{
				return this._BattleTag;
			}
			set
			{
				this._BattleTag = value;
				this.HasBattleTag = (value != null);
			}
		}

		// Token: 0x06004E99 RID: 20121 RVA: 0x000F3D42 File Offset: 0x000F1F42
		public void SetBattleTag(string val)
		{
			this.BattleTag = val;
		}

		// Token: 0x17000EB3 RID: 3763
		// (get) Token: 0x06004E9A RID: 20122 RVA: 0x000F3D4B File Offset: 0x000F1F4B
		// (set) Token: 0x06004E9B RID: 20123 RVA: 0x000F3D53 File Offset: 0x000F1F53
		public string VoiceId
		{
			get
			{
				return this._VoiceId;
			}
			set
			{
				this._VoiceId = value;
				this.HasVoiceId = (value != null);
			}
		}

		// Token: 0x06004E9C RID: 20124 RVA: 0x000F3D66 File Offset: 0x000F1F66
		public void SetVoiceId(string val)
		{
			this.VoiceId = val;
		}

		// Token: 0x17000EB4 RID: 3764
		// (get) Token: 0x06004E9D RID: 20125 RVA: 0x000F3D6F File Offset: 0x000F1F6F
		// (set) Token: 0x06004E9E RID: 20126 RVA: 0x000F3D77 File Offset: 0x000F1F77
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

		// Token: 0x17000EB5 RID: 3765
		// (get) Token: 0x06004E9F RID: 20127 RVA: 0x000F3D6F File Offset: 0x000F1F6F
		public List<uint> RoleList
		{
			get
			{
				return this._Role;
			}
		}

		// Token: 0x17000EB6 RID: 3766
		// (get) Token: 0x06004EA0 RID: 20128 RVA: 0x000F3D80 File Offset: 0x000F1F80
		public int RoleCount
		{
			get
			{
				return this._Role.Count;
			}
		}

		// Token: 0x06004EA1 RID: 20129 RVA: 0x000F3D8D File Offset: 0x000F1F8D
		public void AddRole(uint val)
		{
			this._Role.Add(val);
		}

		// Token: 0x06004EA2 RID: 20130 RVA: 0x000F3D9B File Offset: 0x000F1F9B
		public void ClearRole()
		{
			this._Role.Clear();
		}

		// Token: 0x06004EA3 RID: 20131 RVA: 0x000F3DA8 File Offset: 0x000F1FA8
		public void SetRole(List<uint> val)
		{
			this.Role = val;
		}

		// Token: 0x17000EB7 RID: 3767
		// (get) Token: 0x06004EA4 RID: 20132 RVA: 0x000F3DB1 File Offset: 0x000F1FB1
		// (set) Token: 0x06004EA5 RID: 20133 RVA: 0x000F3DB9 File Offset: 0x000F1FB9
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

		// Token: 0x17000EB8 RID: 3768
		// (get) Token: 0x06004EA6 RID: 20134 RVA: 0x000F3DB1 File Offset: 0x000F1FB1
		public List<bnet.protocol.v2.Attribute> AttributeList
		{
			get
			{
				return this._Attribute;
			}
		}

		// Token: 0x17000EB9 RID: 3769
		// (get) Token: 0x06004EA7 RID: 20135 RVA: 0x000F3DC2 File Offset: 0x000F1FC2
		public int AttributeCount
		{
			get
			{
				return this._Attribute.Count;
			}
		}

		// Token: 0x06004EA8 RID: 20136 RVA: 0x000F3DCF File Offset: 0x000F1FCF
		public void AddAttribute(bnet.protocol.v2.Attribute val)
		{
			this._Attribute.Add(val);
		}

		// Token: 0x06004EA9 RID: 20137 RVA: 0x000F3DDD File Offset: 0x000F1FDD
		public void ClearAttribute()
		{
			this._Attribute.Clear();
		}

		// Token: 0x06004EAA RID: 20138 RVA: 0x000F3DEA File Offset: 0x000F1FEA
		public void SetAttribute(List<bnet.protocol.v2.Attribute> val)
		{
			this.Attribute = val;
		}

		// Token: 0x06004EAB RID: 20139 RVA: 0x000F3DF4 File Offset: 0x000F1FF4
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasId)
			{
				num ^= this.Id.GetHashCode();
			}
			if (this.HasBattleTag)
			{
				num ^= this.BattleTag.GetHashCode();
			}
			if (this.HasVoiceId)
			{
				num ^= this.VoiceId.GetHashCode();
			}
			foreach (uint num2 in this.Role)
			{
				num ^= num2.GetHashCode();
			}
			foreach (bnet.protocol.v2.Attribute attribute in this.Attribute)
			{
				num ^= attribute.GetHashCode();
			}
			return num;
		}

		// Token: 0x06004EAC RID: 20140 RVA: 0x000F3EE0 File Offset: 0x000F20E0
		public override bool Equals(object obj)
		{
			Member member = obj as Member;
			if (member == null)
			{
				return false;
			}
			if (this.HasId != member.HasId || (this.HasId && !this.Id.Equals(member.Id)))
			{
				return false;
			}
			if (this.HasBattleTag != member.HasBattleTag || (this.HasBattleTag && !this.BattleTag.Equals(member.BattleTag)))
			{
				return false;
			}
			if (this.HasVoiceId != member.HasVoiceId || (this.HasVoiceId && !this.VoiceId.Equals(member.VoiceId)))
			{
				return false;
			}
			if (this.Role.Count != member.Role.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Role.Count; i++)
			{
				if (!this.Role[i].Equals(member.Role[i]))
				{
					return false;
				}
			}
			if (this.Attribute.Count != member.Attribute.Count)
			{
				return false;
			}
			for (int j = 0; j < this.Attribute.Count; j++)
			{
				if (!this.Attribute[j].Equals(member.Attribute[j]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000EBA RID: 3770
		// (get) Token: 0x06004EAD RID: 20141 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004EAE RID: 20142 RVA: 0x000F4020 File Offset: 0x000F2220
		public static Member ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<Member>(bs, 0, -1);
		}

		// Token: 0x06004EAF RID: 20143 RVA: 0x000F402A File Offset: 0x000F222A
		public void Deserialize(Stream stream)
		{
			Member.Deserialize(stream, this);
		}

		// Token: 0x06004EB0 RID: 20144 RVA: 0x000F4034 File Offset: 0x000F2234
		public static Member Deserialize(Stream stream, Member instance)
		{
			return Member.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004EB1 RID: 20145 RVA: 0x000F4040 File Offset: 0x000F2240
		public static Member DeserializeLengthDelimited(Stream stream)
		{
			Member member = new Member();
			Member.DeserializeLengthDelimited(stream, member);
			return member;
		}

		// Token: 0x06004EB2 RID: 20146 RVA: 0x000F405C File Offset: 0x000F225C
		public static Member DeserializeLengthDelimited(Stream stream, Member instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return Member.Deserialize(stream, instance, num);
		}

		// Token: 0x06004EB3 RID: 20147 RVA: 0x000F4084 File Offset: 0x000F2284
		public static Member Deserialize(Stream stream, Member instance, long limit)
		{
			if (instance.Role == null)
			{
				instance.Role = new List<uint>();
			}
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
								instance.BattleTag = ProtocolParser.ReadString(stream);
								continue;
							}
						}
						else
						{
							if (instance.Id == null)
							{
								instance.Id = GameAccountHandle.DeserializeLengthDelimited(stream);
								continue;
							}
							GameAccountHandle.DeserializeLengthDelimited(stream, instance.Id);
							continue;
						}
					}
					else
					{
						if (num == 26)
						{
							instance.VoiceId = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num != 34)
						{
							if (num == 42)
							{
								instance.Attribute.Add(bnet.protocol.v2.Attribute.DeserializeLengthDelimited(stream));
								continue;
							}
						}
						else
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

		// Token: 0x06004EB4 RID: 20148 RVA: 0x000F41EF File Offset: 0x000F23EF
		public void Serialize(Stream stream)
		{
			Member.Serialize(stream, this);
		}

		// Token: 0x06004EB5 RID: 20149 RVA: 0x000F41F8 File Offset: 0x000F23F8
		public static void Serialize(Stream stream, Member instance)
		{
			if (instance.HasId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Id.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.Id);
			}
			if (instance.HasBattleTag)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.BattleTag));
			}
			if (instance.HasVoiceId)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.VoiceId));
			}
			if (instance.Role.Count > 0)
			{
				stream.WriteByte(34);
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
			if (instance.Attribute.Count > 0)
			{
				foreach (bnet.protocol.v2.Attribute attribute in instance.Attribute)
				{
					stream.WriteByte(42);
					ProtocolParser.WriteUInt32(stream, attribute.GetSerializedSize());
					bnet.protocol.v2.Attribute.Serialize(stream, attribute);
				}
			}
		}

		// Token: 0x06004EB6 RID: 20150 RVA: 0x000F4390 File Offset: 0x000F2590
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasId)
			{
				num += 1U;
				uint serializedSize = this.Id.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasBattleTag)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.BattleTag);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasVoiceId)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.VoiceId);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
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

		// Token: 0x0400197D RID: 6525
		public bool HasId;

		// Token: 0x0400197E RID: 6526
		private GameAccountHandle _Id;

		// Token: 0x0400197F RID: 6527
		public bool HasBattleTag;

		// Token: 0x04001980 RID: 6528
		private string _BattleTag;

		// Token: 0x04001981 RID: 6529
		public bool HasVoiceId;

		// Token: 0x04001982 RID: 6530
		private string _VoiceId;

		// Token: 0x04001983 RID: 6531
		private List<uint> _Role = new List<uint>();

		// Token: 0x04001984 RID: 6532
		private List<bnet.protocol.v2.Attribute> _Attribute = new List<bnet.protocol.v2.Attribute>();
	}
}
