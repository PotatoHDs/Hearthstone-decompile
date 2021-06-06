using System;
using System.IO;
using System.Text;

namespace bnet.protocol.friends.v2.client
{
	// Token: 0x0200040B RID: 1035
	public class SendInvitationTarget : IProtoBuf
	{
		// Token: 0x17000CAA RID: 3242
		// (get) Token: 0x060044BB RID: 17595 RVA: 0x000D8289 File Offset: 0x000D6489
		// (set) Token: 0x060044BC RID: 17596 RVA: 0x000D8291 File Offset: 0x000D6491
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				this._Name = value;
				this.HasName = (value != null);
			}
		}

		// Token: 0x060044BD RID: 17597 RVA: 0x000D82A4 File Offset: 0x000D64A4
		public void SetName(string val)
		{
			this.Name = val;
		}

		// Token: 0x17000CAB RID: 3243
		// (get) Token: 0x060044BE RID: 17598 RVA: 0x000D82AD File Offset: 0x000D64AD
		// (set) Token: 0x060044BF RID: 17599 RVA: 0x000D82B5 File Offset: 0x000D64B5
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

		// Token: 0x060044C0 RID: 17600 RVA: 0x000D82C5 File Offset: 0x000D64C5
		public void SetAccountId(ulong val)
		{
			this.AccountId = val;
		}

		// Token: 0x17000CAC RID: 3244
		// (get) Token: 0x060044C1 RID: 17601 RVA: 0x000D82CE File Offset: 0x000D64CE
		// (set) Token: 0x060044C2 RID: 17602 RVA: 0x000D82D6 File Offset: 0x000D64D6
		public string Email
		{
			get
			{
				return this._Email;
			}
			set
			{
				this._Email = value;
				this.HasEmail = (value != null);
			}
		}

		// Token: 0x060044C3 RID: 17603 RVA: 0x000D82E9 File Offset: 0x000D64E9
		public void SetEmail(string val)
		{
			this.Email = val;
		}

		// Token: 0x17000CAD RID: 3245
		// (get) Token: 0x060044C4 RID: 17604 RVA: 0x000D82F2 File Offset: 0x000D64F2
		// (set) Token: 0x060044C5 RID: 17605 RVA: 0x000D82FA File Offset: 0x000D64FA
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

		// Token: 0x060044C6 RID: 17606 RVA: 0x000D830D File Offset: 0x000D650D
		public void SetBattleTag(string val)
		{
			this.BattleTag = val;
		}

		// Token: 0x060044C7 RID: 17607 RVA: 0x000D8318 File Offset: 0x000D6518
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasName)
			{
				num ^= this.Name.GetHashCode();
			}
			if (this.HasAccountId)
			{
				num ^= this.AccountId.GetHashCode();
			}
			if (this.HasEmail)
			{
				num ^= this.Email.GetHashCode();
			}
			if (this.HasBattleTag)
			{
				num ^= this.BattleTag.GetHashCode();
			}
			return num;
		}

		// Token: 0x060044C8 RID: 17608 RVA: 0x000D8390 File Offset: 0x000D6590
		public override bool Equals(object obj)
		{
			SendInvitationTarget sendInvitationTarget = obj as SendInvitationTarget;
			return sendInvitationTarget != null && this.HasName == sendInvitationTarget.HasName && (!this.HasName || this.Name.Equals(sendInvitationTarget.Name)) && this.HasAccountId == sendInvitationTarget.HasAccountId && (!this.HasAccountId || this.AccountId.Equals(sendInvitationTarget.AccountId)) && this.HasEmail == sendInvitationTarget.HasEmail && (!this.HasEmail || this.Email.Equals(sendInvitationTarget.Email)) && this.HasBattleTag == sendInvitationTarget.HasBattleTag && (!this.HasBattleTag || this.BattleTag.Equals(sendInvitationTarget.BattleTag));
		}

		// Token: 0x17000CAE RID: 3246
		// (get) Token: 0x060044C9 RID: 17609 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060044CA RID: 17610 RVA: 0x000D8459 File Offset: 0x000D6659
		public static SendInvitationTarget ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SendInvitationTarget>(bs, 0, -1);
		}

		// Token: 0x060044CB RID: 17611 RVA: 0x000D8463 File Offset: 0x000D6663
		public void Deserialize(Stream stream)
		{
			SendInvitationTarget.Deserialize(stream, this);
		}

		// Token: 0x060044CC RID: 17612 RVA: 0x000D846D File Offset: 0x000D666D
		public static SendInvitationTarget Deserialize(Stream stream, SendInvitationTarget instance)
		{
			return SendInvitationTarget.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060044CD RID: 17613 RVA: 0x000D8478 File Offset: 0x000D6678
		public static SendInvitationTarget DeserializeLengthDelimited(Stream stream)
		{
			SendInvitationTarget sendInvitationTarget = new SendInvitationTarget();
			SendInvitationTarget.DeserializeLengthDelimited(stream, sendInvitationTarget);
			return sendInvitationTarget;
		}

		// Token: 0x060044CE RID: 17614 RVA: 0x000D8494 File Offset: 0x000D6694
		public static SendInvitationTarget DeserializeLengthDelimited(Stream stream, SendInvitationTarget instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SendInvitationTarget.Deserialize(stream, instance, num);
		}

		// Token: 0x060044CF RID: 17615 RVA: 0x000D84BC File Offset: 0x000D66BC
		public static SendInvitationTarget Deserialize(Stream stream, SendInvitationTarget instance, long limit)
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
				else
				{
					if (num <= 80)
					{
						if (num == 10)
						{
							instance.Name = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 80)
						{
							instance.AccountId = ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					else
					{
						if (num == 90)
						{
							instance.Email = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 98)
						{
							instance.BattleTag = ProtocolParser.ReadString(stream);
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

		// Token: 0x060044D0 RID: 17616 RVA: 0x000D858D File Offset: 0x000D678D
		public void Serialize(Stream stream)
		{
			SendInvitationTarget.Serialize(stream, this);
		}

		// Token: 0x060044D1 RID: 17617 RVA: 0x000D8598 File Offset: 0x000D6798
		public static void Serialize(Stream stream, SendInvitationTarget instance)
		{
			if (instance.HasName)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Name));
			}
			if (instance.HasAccountId)
			{
				stream.WriteByte(80);
				ProtocolParser.WriteUInt64(stream, instance.AccountId);
			}
			if (instance.HasEmail)
			{
				stream.WriteByte(90);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Email));
			}
			if (instance.HasBattleTag)
			{
				stream.WriteByte(98);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.BattleTag));
			}
		}

		// Token: 0x060044D2 RID: 17618 RVA: 0x000D8634 File Offset: 0x000D6834
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasName)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Name);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasAccountId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.AccountId);
			}
			if (this.HasEmail)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.Email);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (this.HasBattleTag)
			{
				num += 1U;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(this.BattleTag);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			return num;
		}

		// Token: 0x04001730 RID: 5936
		public bool HasName;

		// Token: 0x04001731 RID: 5937
		private string _Name;

		// Token: 0x04001732 RID: 5938
		public bool HasAccountId;

		// Token: 0x04001733 RID: 5939
		private ulong _AccountId;

		// Token: 0x04001734 RID: 5940
		public bool HasEmail;

		// Token: 0x04001735 RID: 5941
		private string _Email;

		// Token: 0x04001736 RID: 5942
		public bool HasBattleTag;

		// Token: 0x04001737 RID: 5943
		private string _BattleTag;
	}
}
