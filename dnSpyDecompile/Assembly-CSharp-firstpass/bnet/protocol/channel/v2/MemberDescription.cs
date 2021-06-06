using System;
using System.IO;
using System.Text;
using bnet.protocol.account.v1;

namespace bnet.protocol.channel.v2
{
	// Token: 0x02000482 RID: 1154
	public class MemberDescription : IProtoBuf
	{
		// Token: 0x17000F10 RID: 3856
		// (get) Token: 0x06005015 RID: 20501 RVA: 0x000F8895 File Offset: 0x000F6A95
		// (set) Token: 0x06005016 RID: 20502 RVA: 0x000F889D File Offset: 0x000F6A9D
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

		// Token: 0x06005017 RID: 20503 RVA: 0x000F88B0 File Offset: 0x000F6AB0
		public void SetId(GameAccountHandle val)
		{
			this.Id = val;
		}

		// Token: 0x17000F11 RID: 3857
		// (get) Token: 0x06005018 RID: 20504 RVA: 0x000F88B9 File Offset: 0x000F6AB9
		// (set) Token: 0x06005019 RID: 20505 RVA: 0x000F88C1 File Offset: 0x000F6AC1
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

		// Token: 0x0600501A RID: 20506 RVA: 0x000F88D4 File Offset: 0x000F6AD4
		public void SetBattleTag(string val)
		{
			this.BattleTag = val;
		}

		// Token: 0x0600501B RID: 20507 RVA: 0x000F88E0 File Offset: 0x000F6AE0
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
			return num;
		}

		// Token: 0x0600501C RID: 20508 RVA: 0x000F8928 File Offset: 0x000F6B28
		public override bool Equals(object obj)
		{
			MemberDescription memberDescription = obj as MemberDescription;
			return memberDescription != null && this.HasId == memberDescription.HasId && (!this.HasId || this.Id.Equals(memberDescription.Id)) && this.HasBattleTag == memberDescription.HasBattleTag && (!this.HasBattleTag || this.BattleTag.Equals(memberDescription.BattleTag));
		}

		// Token: 0x17000F12 RID: 3858
		// (get) Token: 0x0600501D RID: 20509 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600501E RID: 20510 RVA: 0x000F8998 File Offset: 0x000F6B98
		public static MemberDescription ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<MemberDescription>(bs, 0, -1);
		}

		// Token: 0x0600501F RID: 20511 RVA: 0x000F89A2 File Offset: 0x000F6BA2
		public void Deserialize(Stream stream)
		{
			MemberDescription.Deserialize(stream, this);
		}

		// Token: 0x06005020 RID: 20512 RVA: 0x000F89AC File Offset: 0x000F6BAC
		public static MemberDescription Deserialize(Stream stream, MemberDescription instance)
		{
			return MemberDescription.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005021 RID: 20513 RVA: 0x000F89B8 File Offset: 0x000F6BB8
		public static MemberDescription DeserializeLengthDelimited(Stream stream)
		{
			MemberDescription memberDescription = new MemberDescription();
			MemberDescription.DeserializeLengthDelimited(stream, memberDescription);
			return memberDescription;
		}

		// Token: 0x06005022 RID: 20514 RVA: 0x000F89D4 File Offset: 0x000F6BD4
		public static MemberDescription DeserializeLengthDelimited(Stream stream, MemberDescription instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return MemberDescription.Deserialize(stream, instance, num);
		}

		// Token: 0x06005023 RID: 20515 RVA: 0x000F89FC File Offset: 0x000F6BFC
		public static MemberDescription Deserialize(Stream stream, MemberDescription instance, long limit)
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
				else if (num != 10)
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
						instance.BattleTag = ProtocolParser.ReadString(stream);
					}
				}
				else if (instance.Id == null)
				{
					instance.Id = GameAccountHandle.DeserializeLengthDelimited(stream);
				}
				else
				{
					GameAccountHandle.DeserializeLengthDelimited(stream, instance.Id);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06005024 RID: 20516 RVA: 0x000F8AAE File Offset: 0x000F6CAE
		public void Serialize(Stream stream)
		{
			MemberDescription.Serialize(stream, this);
		}

		// Token: 0x06005025 RID: 20517 RVA: 0x000F8AB8 File Offset: 0x000F6CB8
		public static void Serialize(Stream stream, MemberDescription instance)
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
		}

		// Token: 0x06005026 RID: 20518 RVA: 0x000F8B18 File Offset: 0x000F6D18
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
			return num;
		}

		// Token: 0x040019D6 RID: 6614
		public bool HasId;

		// Token: 0x040019D7 RID: 6615
		private GameAccountHandle _Id;

		// Token: 0x040019D8 RID: 6616
		public bool HasBattleTag;

		// Token: 0x040019D9 RID: 6617
		private string _BattleTag;
	}
}
