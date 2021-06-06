using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using bnet.protocol.friends.v2.client.Types;
using bnet.protocol.v2;

namespace bnet.protocol.friends.v2.client
{
	// Token: 0x02000415 RID: 1045
	public class Friend : IProtoBuf
	{
		// Token: 0x17000CDB RID: 3291
		// (get) Token: 0x060045AA RID: 17834 RVA: 0x000DAAB3 File Offset: 0x000D8CB3
		// (set) Token: 0x060045AB RID: 17835 RVA: 0x000DAABB File Offset: 0x000D8CBB
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

		// Token: 0x060045AC RID: 17836 RVA: 0x000DAACB File Offset: 0x000D8CCB
		public void SetId(ulong val)
		{
			this.Id = val;
		}

		// Token: 0x17000CDC RID: 3292
		// (get) Token: 0x060045AD RID: 17837 RVA: 0x000DAAD4 File Offset: 0x000D8CD4
		// (set) Token: 0x060045AE RID: 17838 RVA: 0x000DAADC File Offset: 0x000D8CDC
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

		// Token: 0x060045AF RID: 17839 RVA: 0x000DAAEC File Offset: 0x000D8CEC
		public void SetLevel(FriendLevel val)
		{
			this.Level = val;
		}

		// Token: 0x17000CDD RID: 3293
		// (get) Token: 0x060045B0 RID: 17840 RVA: 0x000DAAF5 File Offset: 0x000D8CF5
		// (set) Token: 0x060045B1 RID: 17841 RVA: 0x000DAAFD File Offset: 0x000D8CFD
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

		// Token: 0x060045B2 RID: 17842 RVA: 0x000DAB10 File Offset: 0x000D8D10
		public void SetBattleTag(string val)
		{
			this.BattleTag = val;
		}

		// Token: 0x17000CDE RID: 3294
		// (get) Token: 0x060045B3 RID: 17843 RVA: 0x000DAB19 File Offset: 0x000D8D19
		// (set) Token: 0x060045B4 RID: 17844 RVA: 0x000DAB21 File Offset: 0x000D8D21
		public string FullName
		{
			get
			{
				return this._FullName;
			}
			set
			{
				this._FullName = value;
				this.HasFullName = (value != null);
			}
		}

		// Token: 0x060045B5 RID: 17845 RVA: 0x000DAB34 File Offset: 0x000D8D34
		public void SetFullName(string val)
		{
			this.FullName = val;
		}

		// Token: 0x17000CDF RID: 3295
		// (get) Token: 0x060045B6 RID: 17846 RVA: 0x000DAB3D File Offset: 0x000D8D3D
		// (set) Token: 0x060045B7 RID: 17847 RVA: 0x000DAB45 File Offset: 0x000D8D45
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

		// Token: 0x17000CE0 RID: 3296
		// (get) Token: 0x060045B8 RID: 17848 RVA: 0x000DAB3D File Offset: 0x000D8D3D
		public List<bnet.protocol.v2.Attribute> AttributeList
		{
			get
			{
				return this._Attribute;
			}
		}

		// Token: 0x17000CE1 RID: 3297
		// (get) Token: 0x060045B9 RID: 17849 RVA: 0x000DAB4E File Offset: 0x000D8D4E
		public int AttributeCount
		{
			get
			{
				return this._Attribute.Count;
			}
		}

		// Token: 0x060045BA RID: 17850 RVA: 0x000DAB5B File Offset: 0x000D8D5B
		public void AddAttribute(bnet.protocol.v2.Attribute val)
		{
			this._Attribute.Add(val);
		}

		// Token: 0x060045BB RID: 17851 RVA: 0x000DAB69 File Offset: 0x000D8D69
		public void ClearAttribute()
		{
			this._Attribute.Clear();
		}

		// Token: 0x060045BC RID: 17852 RVA: 0x000DAB76 File Offset: 0x000D8D76
		public void SetAttribute(List<bnet.protocol.v2.Attribute> val)
		{
			this.Attribute = val;
		}

		// Token: 0x17000CE2 RID: 3298
		// (get) Token: 0x060045BD RID: 17853 RVA: 0x000DAB7F File Offset: 0x000D8D7F
		// (set) Token: 0x060045BE RID: 17854 RVA: 0x000DAB87 File Offset: 0x000D8D87
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

		// Token: 0x060045BF RID: 17855 RVA: 0x000DAB97 File Offset: 0x000D8D97
		public void SetCreationTimeUs(ulong val)
		{
			this.CreationTimeUs = val;
		}

		// Token: 0x17000CE3 RID: 3299
		// (get) Token: 0x060045C0 RID: 17856 RVA: 0x000DABA0 File Offset: 0x000D8DA0
		// (set) Token: 0x060045C1 RID: 17857 RVA: 0x000DABA8 File Offset: 0x000D8DA8
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

		// Token: 0x060045C2 RID: 17858 RVA: 0x000DABB8 File Offset: 0x000D8DB8
		public void SetModifiedTimeUs(ulong val)
		{
			this.ModifiedTimeUs = val;
		}

		// Token: 0x060045C3 RID: 17859 RVA: 0x000DABC4 File Offset: 0x000D8DC4
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
			if (this.HasBattleTag)
			{
				num ^= this.BattleTag.GetHashCode();
			}
			if (this.HasFullName)
			{
				num ^= this.FullName.GetHashCode();
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

		// Token: 0x060045C4 RID: 17860 RVA: 0x000DACC0 File Offset: 0x000D8EC0
		public override bool Equals(object obj)
		{
			Friend friend = obj as Friend;
			if (friend == null)
			{
				return false;
			}
			if (this.HasId != friend.HasId || (this.HasId && !this.Id.Equals(friend.Id)))
			{
				return false;
			}
			if (this.HasLevel != friend.HasLevel || (this.HasLevel && !this.Level.Equals(friend.Level)))
			{
				return false;
			}
			if (this.HasBattleTag != friend.HasBattleTag || (this.HasBattleTag && !this.BattleTag.Equals(friend.BattleTag)))
			{
				return false;
			}
			if (this.HasFullName != friend.HasFullName || (this.HasFullName && !this.FullName.Equals(friend.FullName)))
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
			return this.HasCreationTimeUs == friend.HasCreationTimeUs && (!this.HasCreationTimeUs || this.CreationTimeUs.Equals(friend.CreationTimeUs)) && this.HasModifiedTimeUs == friend.HasModifiedTimeUs && (!this.HasModifiedTimeUs || this.ModifiedTimeUs.Equals(friend.ModifiedTimeUs));
		}

		// Token: 0x17000CE4 RID: 3300
		// (get) Token: 0x060045C5 RID: 17861 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060045C6 RID: 17862 RVA: 0x000DAE44 File Offset: 0x000D9044
		public static Friend ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<Friend>(bs, 0, -1);
		}

		// Token: 0x060045C7 RID: 17863 RVA: 0x000DAE4E File Offset: 0x000D904E
		public void Deserialize(Stream stream)
		{
			Friend.Deserialize(stream, this);
		}

		// Token: 0x060045C8 RID: 17864 RVA: 0x000DAE58 File Offset: 0x000D9058
		public static Friend Deserialize(Stream stream, Friend instance)
		{
			return Friend.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060045C9 RID: 17865 RVA: 0x000DAE64 File Offset: 0x000D9064
		public static Friend DeserializeLengthDelimited(Stream stream)
		{
			Friend friend = new Friend();
			Friend.DeserializeLengthDelimited(stream, friend);
			return friend;
		}

		// Token: 0x060045CA RID: 17866 RVA: 0x000DAE80 File Offset: 0x000D9080
		public static Friend DeserializeLengthDelimited(Stream stream, Friend instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return Friend.Deserialize(stream, instance, num);
		}

		// Token: 0x060045CB RID: 17867 RVA: 0x000DAEA8 File Offset: 0x000D90A8
		public static Friend Deserialize(Stream stream, Friend instance, long limit)
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
					if (num <= 26)
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
						if (num == 26)
						{
							instance.BattleTag = ProtocolParser.ReadString(stream);
							continue;
						}
					}
					else if (num <= 42)
					{
						if (num == 34)
						{
							instance.FullName = ProtocolParser.ReadString(stream);
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

		// Token: 0x060045CC RID: 17868 RVA: 0x000DAFED File Offset: 0x000D91ED
		public void Serialize(Stream stream)
		{
			Friend.Serialize(stream, this);
		}

		// Token: 0x060045CD RID: 17869 RVA: 0x000DAFF8 File Offset: 0x000D91F8
		public static void Serialize(Stream stream, Friend instance)
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
			if (instance.HasBattleTag)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.BattleTag));
			}
			if (instance.HasFullName)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.FullName));
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

		// Token: 0x060045CE RID: 17870 RVA: 0x000DB12C File Offset: 0x000D932C
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
			if (this.HasBattleTag)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.BattleTag);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasFullName)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.FullName);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
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

		// Token: 0x0400176A RID: 5994
		public bool HasId;

		// Token: 0x0400176B RID: 5995
		private ulong _Id;

		// Token: 0x0400176C RID: 5996
		public bool HasLevel;

		// Token: 0x0400176D RID: 5997
		private FriendLevel _Level;

		// Token: 0x0400176E RID: 5998
		public bool HasBattleTag;

		// Token: 0x0400176F RID: 5999
		private string _BattleTag;

		// Token: 0x04001770 RID: 6000
		public bool HasFullName;

		// Token: 0x04001771 RID: 6001
		private string _FullName;

		// Token: 0x04001772 RID: 6002
		private List<bnet.protocol.v2.Attribute> _Attribute = new List<bnet.protocol.v2.Attribute>();

		// Token: 0x04001773 RID: 6003
		public bool HasCreationTimeUs;

		// Token: 0x04001774 RID: 6004
		private ulong _CreationTimeUs;

		// Token: 0x04001775 RID: 6005
		public bool HasModifiedTimeUs;

		// Token: 0x04001776 RID: 6006
		private ulong _ModifiedTimeUs;
	}
}
