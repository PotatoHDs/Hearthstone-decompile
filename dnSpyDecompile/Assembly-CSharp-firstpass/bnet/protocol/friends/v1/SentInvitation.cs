using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace bnet.protocol.friends.v1
{
	// Token: 0x02000432 RID: 1074
	public class SentInvitation : IProtoBuf
	{
		// Token: 0x17000D6B RID: 3435
		// (get) Token: 0x06004858 RID: 18520 RVA: 0x000E247A File Offset: 0x000E067A
		// (set) Token: 0x06004859 RID: 18521 RVA: 0x000E2482 File Offset: 0x000E0682
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

		// Token: 0x0600485A RID: 18522 RVA: 0x000E2492 File Offset: 0x000E0692
		public void SetId(ulong val)
		{
			this.Id = val;
		}

		// Token: 0x17000D6C RID: 3436
		// (get) Token: 0x0600485B RID: 18523 RVA: 0x000E249B File Offset: 0x000E069B
		// (set) Token: 0x0600485C RID: 18524 RVA: 0x000E24A3 File Offset: 0x000E06A3
		public string TargetName
		{
			get
			{
				return this._TargetName;
			}
			set
			{
				this._TargetName = value;
				this.HasTargetName = (value != null);
			}
		}

		// Token: 0x0600485D RID: 18525 RVA: 0x000E24B6 File Offset: 0x000E06B6
		public void SetTargetName(string val)
		{
			this.TargetName = val;
		}

		// Token: 0x17000D6D RID: 3437
		// (get) Token: 0x0600485E RID: 18526 RVA: 0x000E24BF File Offset: 0x000E06BF
		// (set) Token: 0x0600485F RID: 18527 RVA: 0x000E24C7 File Offset: 0x000E06C7
		public uint Role
		{
			get
			{
				return this._Role;
			}
			set
			{
				this._Role = value;
				this.HasRole = true;
			}
		}

		// Token: 0x06004860 RID: 18528 RVA: 0x000E24D7 File Offset: 0x000E06D7
		public void SetRole(uint val)
		{
			this.Role = val;
		}

		// Token: 0x17000D6E RID: 3438
		// (get) Token: 0x06004861 RID: 18529 RVA: 0x000E24E0 File Offset: 0x000E06E0
		// (set) Token: 0x06004862 RID: 18530 RVA: 0x000E24E8 File Offset: 0x000E06E8
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

		// Token: 0x17000D6F RID: 3439
		// (get) Token: 0x06004863 RID: 18531 RVA: 0x000E24E0 File Offset: 0x000E06E0
		public List<Attribute> AttributeList
		{
			get
			{
				return this._Attribute;
			}
		}

		// Token: 0x17000D70 RID: 3440
		// (get) Token: 0x06004864 RID: 18532 RVA: 0x000E24F1 File Offset: 0x000E06F1
		public int AttributeCount
		{
			get
			{
				return this._Attribute.Count;
			}
		}

		// Token: 0x06004865 RID: 18533 RVA: 0x000E24FE File Offset: 0x000E06FE
		public void AddAttribute(Attribute val)
		{
			this._Attribute.Add(val);
		}

		// Token: 0x06004866 RID: 18534 RVA: 0x000E250C File Offset: 0x000E070C
		public void ClearAttribute()
		{
			this._Attribute.Clear();
		}

		// Token: 0x06004867 RID: 18535 RVA: 0x000E2519 File Offset: 0x000E0719
		public void SetAttribute(List<Attribute> val)
		{
			this.Attribute = val;
		}

		// Token: 0x17000D71 RID: 3441
		// (get) Token: 0x06004868 RID: 18536 RVA: 0x000E2522 File Offset: 0x000E0722
		// (set) Token: 0x06004869 RID: 18537 RVA: 0x000E252A File Offset: 0x000E072A
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

		// Token: 0x0600486A RID: 18538 RVA: 0x000E253A File Offset: 0x000E073A
		public void SetCreationTime(ulong val)
		{
			this.CreationTime = val;
		}

		// Token: 0x17000D72 RID: 3442
		// (get) Token: 0x0600486B RID: 18539 RVA: 0x000E2543 File Offset: 0x000E0743
		// (set) Token: 0x0600486C RID: 18540 RVA: 0x000E254B File Offset: 0x000E074B
		public uint Program
		{
			get
			{
				return this._Program;
			}
			set
			{
				this._Program = value;
				this.HasProgram = true;
			}
		}

		// Token: 0x0600486D RID: 18541 RVA: 0x000E255B File Offset: 0x000E075B
		public void SetProgram(uint val)
		{
			this.Program = val;
		}

		// Token: 0x0600486E RID: 18542 RVA: 0x000E2564 File Offset: 0x000E0764
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasId)
			{
				num ^= this.Id.GetHashCode();
			}
			if (this.HasTargetName)
			{
				num ^= this.TargetName.GetHashCode();
			}
			if (this.HasRole)
			{
				num ^= this.Role.GetHashCode();
			}
			foreach (Attribute attribute in this.Attribute)
			{
				num ^= attribute.GetHashCode();
			}
			if (this.HasCreationTime)
			{
				num ^= this.CreationTime.GetHashCode();
			}
			if (this.HasProgram)
			{
				num ^= this.Program.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600486F RID: 18543 RVA: 0x000E2644 File Offset: 0x000E0844
		public override bool Equals(object obj)
		{
			SentInvitation sentInvitation = obj as SentInvitation;
			if (sentInvitation == null)
			{
				return false;
			}
			if (this.HasId != sentInvitation.HasId || (this.HasId && !this.Id.Equals(sentInvitation.Id)))
			{
				return false;
			}
			if (this.HasTargetName != sentInvitation.HasTargetName || (this.HasTargetName && !this.TargetName.Equals(sentInvitation.TargetName)))
			{
				return false;
			}
			if (this.HasRole != sentInvitation.HasRole || (this.HasRole && !this.Role.Equals(sentInvitation.Role)))
			{
				return false;
			}
			if (this.Attribute.Count != sentInvitation.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Attribute.Count; i++)
			{
				if (!this.Attribute[i].Equals(sentInvitation.Attribute[i]))
				{
					return false;
				}
			}
			return this.HasCreationTime == sentInvitation.HasCreationTime && (!this.HasCreationTime || this.CreationTime.Equals(sentInvitation.CreationTime)) && this.HasProgram == sentInvitation.HasProgram && (!this.HasProgram || this.Program.Equals(sentInvitation.Program));
		}

		// Token: 0x17000D73 RID: 3443
		// (get) Token: 0x06004870 RID: 18544 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004871 RID: 18545 RVA: 0x000E2792 File Offset: 0x000E0992
		public static SentInvitation ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SentInvitation>(bs, 0, -1);
		}

		// Token: 0x06004872 RID: 18546 RVA: 0x000E279C File Offset: 0x000E099C
		public void Deserialize(Stream stream)
		{
			SentInvitation.Deserialize(stream, this);
		}

		// Token: 0x06004873 RID: 18547 RVA: 0x000E27A6 File Offset: 0x000E09A6
		public static SentInvitation Deserialize(Stream stream, SentInvitation instance)
		{
			return SentInvitation.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004874 RID: 18548 RVA: 0x000E27B4 File Offset: 0x000E09B4
		public static SentInvitation DeserializeLengthDelimited(Stream stream)
		{
			SentInvitation sentInvitation = new SentInvitation();
			SentInvitation.DeserializeLengthDelimited(stream, sentInvitation);
			return sentInvitation;
		}

		// Token: 0x06004875 RID: 18549 RVA: 0x000E27D0 File Offset: 0x000E09D0
		public static SentInvitation DeserializeLengthDelimited(Stream stream, SentInvitation instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SentInvitation.Deserialize(stream, instance, num);
		}

		// Token: 0x06004876 RID: 18550 RVA: 0x000E27F8 File Offset: 0x000E09F8
		public static SentInvitation Deserialize(Stream stream, SentInvitation instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			if (instance.Attribute == null)
			{
				instance.Attribute = new List<Attribute>();
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
					if (num <= 24)
					{
						if (num == 9)
						{
							instance.Id = binaryReader.ReadUInt64();
							continue;
						}
						if (num == 18)
						{
							instance.TargetName = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 24)
						{
							instance.Role = ProtocolParser.ReadUInt32(stream);
							continue;
						}
					}
					else
					{
						if (num == 34)
						{
							instance.Attribute.Add(bnet.protocol.Attribute.DeserializeLengthDelimited(stream));
							continue;
						}
						if (num == 40)
						{
							instance.CreationTime = ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 53)
						{
							instance.Program = binaryReader.ReadUInt32();
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

		// Token: 0x06004877 RID: 18551 RVA: 0x000E2917 File Offset: 0x000E0B17
		public void Serialize(Stream stream)
		{
			SentInvitation.Serialize(stream, this);
		}

		// Token: 0x06004878 RID: 18552 RVA: 0x000E2920 File Offset: 0x000E0B20
		public static void Serialize(Stream stream, SentInvitation instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasId)
			{
				stream.WriteByte(9);
				binaryWriter.Write(instance.Id);
			}
			if (instance.HasTargetName)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.TargetName));
			}
			if (instance.HasRole)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt32(stream, instance.Role);
			}
			if (instance.Attribute.Count > 0)
			{
				foreach (Attribute attribute in instance.Attribute)
				{
					stream.WriteByte(34);
					ProtocolParser.WriteUInt32(stream, attribute.GetSerializedSize());
					bnet.protocol.Attribute.Serialize(stream, attribute);
				}
			}
			if (instance.HasCreationTime)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, instance.CreationTime);
			}
			if (instance.HasProgram)
			{
				stream.WriteByte(53);
				binaryWriter.Write(instance.Program);
			}
		}

		// Token: 0x06004879 RID: 18553 RVA: 0x000E2A34 File Offset: 0x000E0C34
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasId)
			{
				num += 1U;
				num += 8U;
			}
			if (this.HasTargetName)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.TargetName);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasRole)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.Role);
			}
			if (this.Attribute.Count > 0)
			{
				foreach (Attribute attribute in this.Attribute)
				{
					num += 1U;
					uint serializedSize = attribute.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (this.HasCreationTime)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.CreationTime);
			}
			if (this.HasProgram)
			{
				num += 1U;
				num += 4U;
			}
			return num;
		}

		// Token: 0x04001804 RID: 6148
		public bool HasId;

		// Token: 0x04001805 RID: 6149
		private ulong _Id;

		// Token: 0x04001806 RID: 6150
		public bool HasTargetName;

		// Token: 0x04001807 RID: 6151
		private string _TargetName;

		// Token: 0x04001808 RID: 6152
		public bool HasRole;

		// Token: 0x04001809 RID: 6153
		private uint _Role;

		// Token: 0x0400180A RID: 6154
		private List<Attribute> _Attribute = new List<Attribute>();

		// Token: 0x0400180B RID: 6155
		public bool HasCreationTime;

		// Token: 0x0400180C RID: 6156
		private ulong _CreationTime;

		// Token: 0x0400180D RID: 6157
		public bool HasProgram;

		// Token: 0x0400180E RID: 6158
		private uint _Program;
	}
}
