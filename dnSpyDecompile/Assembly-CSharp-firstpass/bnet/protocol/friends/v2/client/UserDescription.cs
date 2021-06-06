using System;
using System.IO;
using System.Text;

namespace bnet.protocol.friends.v2.client
{
	// Token: 0x0200040F RID: 1039
	public class UserDescription : IProtoBuf
	{
		// Token: 0x17000CB8 RID: 3256
		// (get) Token: 0x0600450B RID: 17675 RVA: 0x000D8E74 File Offset: 0x000D7074
		// (set) Token: 0x0600450C RID: 17676 RVA: 0x000D8E7C File Offset: 0x000D707C
		public ulong AccountId
		{
			get
			{
				return this._AccountId;
			}
			set
			{
				this._AccountId = value;
				this.HasAccountId = true;
			}
		}

		// Token: 0x0600450D RID: 17677 RVA: 0x000D8E8C File Offset: 0x000D708C
		public void SetAccountId(ulong val)
		{
			this.AccountId = val;
		}

		// Token: 0x17000CB9 RID: 3257
		// (get) Token: 0x0600450E RID: 17678 RVA: 0x000D8E95 File Offset: 0x000D7095
		// (set) Token: 0x0600450F RID: 17679 RVA: 0x000D8E9D File Offset: 0x000D709D
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

		// Token: 0x06004510 RID: 17680 RVA: 0x000D8EB0 File Offset: 0x000D70B0
		public void SetBattleTag(string val)
		{
			this.BattleTag = val;
		}

		// Token: 0x17000CBA RID: 3258
		// (get) Token: 0x06004511 RID: 17681 RVA: 0x000D8EB9 File Offset: 0x000D70B9
		// (set) Token: 0x06004512 RID: 17682 RVA: 0x000D8EC1 File Offset: 0x000D70C1
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

		// Token: 0x06004513 RID: 17683 RVA: 0x000D8ED4 File Offset: 0x000D70D4
		public void SetFullName(string val)
		{
			this.FullName = val;
		}

		// Token: 0x06004514 RID: 17684 RVA: 0x000D8EE0 File Offset: 0x000D70E0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAccountId)
			{
				num ^= this.AccountId.GetHashCode();
			}
			if (this.HasBattleTag)
			{
				num ^= this.BattleTag.GetHashCode();
			}
			if (this.HasFullName)
			{
				num ^= this.FullName.GetHashCode();
			}
			return num;
		}

		// Token: 0x06004515 RID: 17685 RVA: 0x000D8F40 File Offset: 0x000D7140
		public override bool Equals(object obj)
		{
			UserDescription userDescription = obj as UserDescription;
			return userDescription != null && this.HasAccountId == userDescription.HasAccountId && (!this.HasAccountId || this.AccountId.Equals(userDescription.AccountId)) && this.HasBattleTag == userDescription.HasBattleTag && (!this.HasBattleTag || this.BattleTag.Equals(userDescription.BattleTag)) && this.HasFullName == userDescription.HasFullName && (!this.HasFullName || this.FullName.Equals(userDescription.FullName));
		}

		// Token: 0x17000CBB RID: 3259
		// (get) Token: 0x06004516 RID: 17686 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004517 RID: 17687 RVA: 0x000D8FDE File Offset: 0x000D71DE
		public static UserDescription ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<UserDescription>(bs, 0, -1);
		}

		// Token: 0x06004518 RID: 17688 RVA: 0x000D8FE8 File Offset: 0x000D71E8
		public void Deserialize(Stream stream)
		{
			UserDescription.Deserialize(stream, this);
		}

		// Token: 0x06004519 RID: 17689 RVA: 0x000D8FF2 File Offset: 0x000D71F2
		public static UserDescription Deserialize(Stream stream, UserDescription instance)
		{
			return UserDescription.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600451A RID: 17690 RVA: 0x000D9000 File Offset: 0x000D7200
		public static UserDescription DeserializeLengthDelimited(Stream stream)
		{
			UserDescription userDescription = new UserDescription();
			UserDescription.DeserializeLengthDelimited(stream, userDescription);
			return userDescription;
		}

		// Token: 0x0600451B RID: 17691 RVA: 0x000D901C File Offset: 0x000D721C
		public static UserDescription DeserializeLengthDelimited(Stream stream, UserDescription instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return UserDescription.Deserialize(stream, instance, num);
		}

		// Token: 0x0600451C RID: 17692 RVA: 0x000D9044 File Offset: 0x000D7244
		public static UserDescription Deserialize(Stream stream, UserDescription instance, long limit)
		{
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
							instance.FullName = ProtocolParser.ReadString(stream);
						}
					}
					else
					{
						instance.BattleTag = ProtocolParser.ReadString(stream);
					}
				}
				else
				{
					instance.AccountId = ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600451D RID: 17693 RVA: 0x000D90F1 File Offset: 0x000D72F1
		public void Serialize(Stream stream)
		{
			UserDescription.Serialize(stream, this);
		}

		// Token: 0x0600451E RID: 17694 RVA: 0x000D90FC File Offset: 0x000D72FC
		public static void Serialize(Stream stream, UserDescription instance)
		{
			if (instance.HasAccountId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, instance.AccountId);
			}
			if (instance.HasBattleTag)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.BattleTag));
			}
			if (instance.HasFullName)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.FullName));
			}
		}

		// Token: 0x0600451F RID: 17695 RVA: 0x000D9170 File Offset: 0x000D7370
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAccountId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.AccountId);
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
			return num;
		}

		// Token: 0x0400173F RID: 5951
		public bool HasAccountId;

		// Token: 0x04001740 RID: 5952
		private ulong _AccountId;

		// Token: 0x04001741 RID: 5953
		public bool HasBattleTag;

		// Token: 0x04001742 RID: 5954
		private string _BattleTag;

		// Token: 0x04001743 RID: 5955
		public bool HasFullName;

		// Token: 0x04001744 RID: 5956
		private string _FullName;
	}
}
