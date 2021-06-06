using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using bnet.protocol.friends.v2.client.Types;
using bnet.protocol.v2;

namespace bnet.protocol.friends.v2.client
{
	// Token: 0x02000411 RID: 1041
	public class SentInvitation : IProtoBuf
	{
		// Token: 0x17000CC8 RID: 3272
		// (get) Token: 0x0600454D RID: 17741 RVA: 0x000D9B73 File Offset: 0x000D7D73
		// (set) Token: 0x0600454E RID: 17742 RVA: 0x000D9B7B File Offset: 0x000D7D7B
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

		// Token: 0x0600454F RID: 17743 RVA: 0x000D9B8B File Offset: 0x000D7D8B
		public void SetId(ulong val)
		{
			this.Id = val;
		}

		// Token: 0x17000CC9 RID: 3273
		// (get) Token: 0x06004550 RID: 17744 RVA: 0x000D9B94 File Offset: 0x000D7D94
		// (set) Token: 0x06004551 RID: 17745 RVA: 0x000D9B9C File Offset: 0x000D7D9C
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

		// Token: 0x06004552 RID: 17746 RVA: 0x000D9BAF File Offset: 0x000D7DAF
		public void SetTargetName(string val)
		{
			this.TargetName = val;
		}

		// Token: 0x17000CCA RID: 3274
		// (get) Token: 0x06004553 RID: 17747 RVA: 0x000D9BB8 File Offset: 0x000D7DB8
		// (set) Token: 0x06004554 RID: 17748 RVA: 0x000D9BC0 File Offset: 0x000D7DC0
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

		// Token: 0x06004555 RID: 17749 RVA: 0x000D9BD0 File Offset: 0x000D7DD0
		public void SetLevel(FriendLevel val)
		{
			this.Level = val;
		}

		// Token: 0x17000CCB RID: 3275
		// (get) Token: 0x06004556 RID: 17750 RVA: 0x000D9BD9 File Offset: 0x000D7DD9
		// (set) Token: 0x06004557 RID: 17751 RVA: 0x000D9BE1 File Offset: 0x000D7DE1
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

		// Token: 0x06004558 RID: 17752 RVA: 0x000D9BF1 File Offset: 0x000D7DF1
		public void SetProgram(uint val)
		{
			this.Program = val;
		}

		// Token: 0x17000CCC RID: 3276
		// (get) Token: 0x06004559 RID: 17753 RVA: 0x000D9BFA File Offset: 0x000D7DFA
		// (set) Token: 0x0600455A RID: 17754 RVA: 0x000D9C02 File Offset: 0x000D7E02
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

		// Token: 0x17000CCD RID: 3277
		// (get) Token: 0x0600455B RID: 17755 RVA: 0x000D9BFA File Offset: 0x000D7DFA
		public List<bnet.protocol.v2.Attribute> AttributeList
		{
			get
			{
				return this._Attribute;
			}
		}

		// Token: 0x17000CCE RID: 3278
		// (get) Token: 0x0600455C RID: 17756 RVA: 0x000D9C0B File Offset: 0x000D7E0B
		public int AttributeCount
		{
			get
			{
				return this._Attribute.Count;
			}
		}

		// Token: 0x0600455D RID: 17757 RVA: 0x000D9C18 File Offset: 0x000D7E18
		public void AddAttribute(bnet.protocol.v2.Attribute val)
		{
			this._Attribute.Add(val);
		}

		// Token: 0x0600455E RID: 17758 RVA: 0x000D9C26 File Offset: 0x000D7E26
		public void ClearAttribute()
		{
			this._Attribute.Clear();
		}

		// Token: 0x0600455F RID: 17759 RVA: 0x000D9C33 File Offset: 0x000D7E33
		public void SetAttribute(List<bnet.protocol.v2.Attribute> val)
		{
			this.Attribute = val;
		}

		// Token: 0x17000CCF RID: 3279
		// (get) Token: 0x06004560 RID: 17760 RVA: 0x000D9C3C File Offset: 0x000D7E3C
		// (set) Token: 0x06004561 RID: 17761 RVA: 0x000D9C44 File Offset: 0x000D7E44
		public ulong CreationTimeUs
		{
			get
			{
				return this._CreationTimeUs;
			}
			set
			{
				this._CreationTimeUs = value;
				this.HasCreationTimeUs = true;
			}
		}

		// Token: 0x06004562 RID: 17762 RVA: 0x000D9C54 File Offset: 0x000D7E54
		public void SetCreationTimeUs(ulong val)
		{
			this.CreationTimeUs = val;
		}

		// Token: 0x17000CD0 RID: 3280
		// (get) Token: 0x06004563 RID: 17763 RVA: 0x000D9C5D File Offset: 0x000D7E5D
		// (set) Token: 0x06004564 RID: 17764 RVA: 0x000D9C65 File Offset: 0x000D7E65
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

		// Token: 0x06004565 RID: 17765 RVA: 0x000D9C75 File Offset: 0x000D7E75
		public void SetModifiedTimeUs(ulong val)
		{
			this.ModifiedTimeUs = val;
		}

		// Token: 0x06004566 RID: 17766 RVA: 0x000D9C80 File Offset: 0x000D7E80
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
			if (this.HasLevel)
			{
				num ^= this.Level.GetHashCode();
			}
			if (this.HasProgram)
			{
				num ^= this.Program.GetHashCode();
			}
			foreach (bnet.protocol.v2.Attribute attribute in this.Attribute)
			{
				num ^= attribute.GetHashCode();
			}
			if (this.HasCreationTimeUs)
			{
				num ^= this.CreationTimeUs.GetHashCode();
			}
			if (this.HasModifiedTimeUs)
			{
				num ^= this.ModifiedTimeUs.GetHashCode();
			}
			return num;
		}

		// Token: 0x06004567 RID: 17767 RVA: 0x000D9D80 File Offset: 0x000D7F80
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
			if (this.HasLevel != sentInvitation.HasLevel || (this.HasLevel && !this.Level.Equals(sentInvitation.Level)))
			{
				return false;
			}
			if (this.HasProgram != sentInvitation.HasProgram || (this.HasProgram && !this.Program.Equals(sentInvitation.Program)))
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
			return this.HasCreationTimeUs == sentInvitation.HasCreationTimeUs && (!this.HasCreationTimeUs || this.CreationTimeUs.Equals(sentInvitation.CreationTimeUs)) && this.HasModifiedTimeUs == sentInvitation.HasModifiedTimeUs && (!this.HasModifiedTimeUs || this.ModifiedTimeUs.Equals(sentInvitation.ModifiedTimeUs));
		}

		// Token: 0x17000CD1 RID: 3281
		// (get) Token: 0x06004568 RID: 17768 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004569 RID: 17769 RVA: 0x000D9F0D File Offset: 0x000D810D
		public static SentInvitation ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SentInvitation>(bs, 0, -1);
		}

		// Token: 0x0600456A RID: 17770 RVA: 0x000D9F17 File Offset: 0x000D8117
		public void Deserialize(Stream stream)
		{
			SentInvitation.Deserialize(stream, this);
		}

		// Token: 0x0600456B RID: 17771 RVA: 0x000D9F21 File Offset: 0x000D8121
		public static SentInvitation Deserialize(Stream stream, SentInvitation instance)
		{
			return SentInvitation.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600456C RID: 17772 RVA: 0x000D9F2C File Offset: 0x000D812C
		public static SentInvitation DeserializeLengthDelimited(Stream stream)
		{
			SentInvitation sentInvitation = new SentInvitation();
			SentInvitation.DeserializeLengthDelimited(stream, sentInvitation);
			return sentInvitation;
		}

		// Token: 0x0600456D RID: 17773 RVA: 0x000D9F48 File Offset: 0x000D8148
		public static SentInvitation DeserializeLengthDelimited(Stream stream, SentInvitation instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SentInvitation.Deserialize(stream, instance, num);
		}

		// Token: 0x0600456E RID: 17774 RVA: 0x000D9F70 File Offset: 0x000D8170
		public static SentInvitation Deserialize(Stream stream, SentInvitation instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
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
					if (num <= 24)
					{
						if (num == 8)
						{
							instance.Id = ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 18)
						{
							instance.TargetName = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 24)
						{
							instance.Level = (FriendLevel)ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					else if (num <= 42)
					{
						if (num == 37)
						{
							instance.Program = binaryReader.ReadUInt32();
							continue;
						}
						if (num == 42)
						{
							instance.Attribute.Add(bnet.protocol.v2.Attribute.DeserializeLengthDelimited(stream));
							continue;
						}
					}
					else
					{
						if (num == 48)
						{
							instance.CreationTimeUs = ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x0600456F RID: 17775 RVA: 0x000DA0BC File Offset: 0x000D82BC
		public void Serialize(Stream stream)
		{
			SentInvitation.Serialize(stream, this);
		}

		// Token: 0x06004570 RID: 17776 RVA: 0x000DA0C8 File Offset: 0x000D82C8
		public static void Serialize(Stream stream, SentInvitation instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, instance.Id);
			}
			if (instance.HasTargetName)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.TargetName));
			}
			if (instance.HasLevel)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Level));
			}
			if (instance.HasProgram)
			{
				stream.WriteByte(37);
				binaryWriter.Write(instance.Program);
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
			if (instance.HasCreationTimeUs)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt64(stream, instance.CreationTimeUs);
			}
			if (instance.HasModifiedTimeUs)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteUInt64(stream, instance.ModifiedTimeUs);
			}
		}

		// Token: 0x06004571 RID: 17777 RVA: 0x000DA1F8 File Offset: 0x000D83F8
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.Id);
			}
			if (this.HasTargetName)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.TargetName);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasLevel)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Level));
			}
			if (this.HasProgram)
			{
				num += 1U;
				num += 4U;
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
			if (this.HasCreationTimeUs)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.CreationTimeUs);
			}
			if (this.HasModifiedTimeUs)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.ModifiedTimeUs);
			}
			return num;
		}

		// Token: 0x04001756 RID: 5974
		public bool HasId;

		// Token: 0x04001757 RID: 5975
		private ulong _Id;

		// Token: 0x04001758 RID: 5976
		public bool HasTargetName;

		// Token: 0x04001759 RID: 5977
		private string _TargetName;

		// Token: 0x0400175A RID: 5978
		public bool HasLevel;

		// Token: 0x0400175B RID: 5979
		private FriendLevel _Level;

		// Token: 0x0400175C RID: 5980
		public bool HasProgram;

		// Token: 0x0400175D RID: 5981
		private uint _Program;

		// Token: 0x0400175E RID: 5982
		private List<bnet.protocol.v2.Attribute> _Attribute = new List<bnet.protocol.v2.Attribute>();

		// Token: 0x0400175F RID: 5983
		public bool HasCreationTimeUs;

		// Token: 0x04001760 RID: 5984
		private ulong _CreationTimeUs;

		// Token: 0x04001761 RID: 5985
		public bool HasModifiedTimeUs;

		// Token: 0x04001762 RID: 5986
		private ulong _ModifiedTimeUs;
	}
}
