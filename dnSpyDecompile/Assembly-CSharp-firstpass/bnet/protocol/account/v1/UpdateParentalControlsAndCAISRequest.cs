using System;
using System.IO;
using System.Text;

namespace bnet.protocol.account.v1
{
	// Token: 0x0200051C RID: 1308
	public class UpdateParentalControlsAndCAISRequest : IProtoBuf
	{
		// Token: 0x17001191 RID: 4497
		// (get) Token: 0x06005D3C RID: 23868 RVA: 0x0011AF6B File Offset: 0x0011916B
		// (set) Token: 0x06005D3D RID: 23869 RVA: 0x0011AF73 File Offset: 0x00119173
		public AccountId Account
		{
			get
			{
				return this._Account;
			}
			set
			{
				this._Account = value;
				this.HasAccount = (value != null);
			}
		}

		// Token: 0x06005D3E RID: 23870 RVA: 0x0011AF86 File Offset: 0x00119186
		public void SetAccount(AccountId val)
		{
			this.Account = val;
		}

		// Token: 0x17001192 RID: 4498
		// (get) Token: 0x06005D3F RID: 23871 RVA: 0x0011AF8F File Offset: 0x0011918F
		// (set) Token: 0x06005D40 RID: 23872 RVA: 0x0011AF97 File Offset: 0x00119197
		public ParentalControlInfo ParentalControlInfo
		{
			get
			{
				return this._ParentalControlInfo;
			}
			set
			{
				this._ParentalControlInfo = value;
				this.HasParentalControlInfo = (value != null);
			}
		}

		// Token: 0x06005D41 RID: 23873 RVA: 0x0011AFAA File Offset: 0x001191AA
		public void SetParentalControlInfo(ParentalControlInfo val)
		{
			this.ParentalControlInfo = val;
		}

		// Token: 0x17001193 RID: 4499
		// (get) Token: 0x06005D42 RID: 23874 RVA: 0x0011AFB3 File Offset: 0x001191B3
		// (set) Token: 0x06005D43 RID: 23875 RVA: 0x0011AFBB File Offset: 0x001191BB
		public string CaisId
		{
			get
			{
				return this._CaisId;
			}
			set
			{
				this._CaisId = value;
				this.HasCaisId = (value != null);
			}
		}

		// Token: 0x06005D44 RID: 23876 RVA: 0x0011AFCE File Offset: 0x001191CE
		public void SetCaisId(string val)
		{
			this.CaisId = val;
		}

		// Token: 0x17001194 RID: 4500
		// (get) Token: 0x06005D45 RID: 23877 RVA: 0x0011AFD7 File Offset: 0x001191D7
		// (set) Token: 0x06005D46 RID: 23878 RVA: 0x0011AFDF File Offset: 0x001191DF
		public ulong SessionStartTime
		{
			get
			{
				return this._SessionStartTime;
			}
			set
			{
				this._SessionStartTime = value;
				this.HasSessionStartTime = true;
			}
		}

		// Token: 0x06005D47 RID: 23879 RVA: 0x0011AFEF File Offset: 0x001191EF
		public void SetSessionStartTime(ulong val)
		{
			this.SessionStartTime = val;
		}

		// Token: 0x17001195 RID: 4501
		// (get) Token: 0x06005D48 RID: 23880 RVA: 0x0011AFF8 File Offset: 0x001191F8
		// (set) Token: 0x06005D49 RID: 23881 RVA: 0x0011B000 File Offset: 0x00119200
		public ulong StartTime
		{
			get
			{
				return this._StartTime;
			}
			set
			{
				this._StartTime = value;
				this.HasStartTime = true;
			}
		}

		// Token: 0x06005D4A RID: 23882 RVA: 0x0011B010 File Offset: 0x00119210
		public void SetStartTime(ulong val)
		{
			this.StartTime = val;
		}

		// Token: 0x17001196 RID: 4502
		// (get) Token: 0x06005D4B RID: 23883 RVA: 0x0011B019 File Offset: 0x00119219
		// (set) Token: 0x06005D4C RID: 23884 RVA: 0x0011B021 File Offset: 0x00119221
		public ulong EndTime
		{
			get
			{
				return this._EndTime;
			}
			set
			{
				this._EndTime = value;
				this.HasEndTime = true;
			}
		}

		// Token: 0x06005D4D RID: 23885 RVA: 0x0011B031 File Offset: 0x00119231
		public void SetEndTime(ulong val)
		{
			this.EndTime = val;
		}

		// Token: 0x06005D4E RID: 23886 RVA: 0x0011B03C File Offset: 0x0011923C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAccount)
			{
				num ^= this.Account.GetHashCode();
			}
			if (this.HasParentalControlInfo)
			{
				num ^= this.ParentalControlInfo.GetHashCode();
			}
			if (this.HasCaisId)
			{
				num ^= this.CaisId.GetHashCode();
			}
			if (this.HasSessionStartTime)
			{
				num ^= this.SessionStartTime.GetHashCode();
			}
			if (this.HasStartTime)
			{
				num ^= this.StartTime.GetHashCode();
			}
			if (this.HasEndTime)
			{
				num ^= this.EndTime.GetHashCode();
			}
			return num;
		}

		// Token: 0x06005D4F RID: 23887 RVA: 0x0011B0E4 File Offset: 0x001192E4
		public override bool Equals(object obj)
		{
			UpdateParentalControlsAndCAISRequest updateParentalControlsAndCAISRequest = obj as UpdateParentalControlsAndCAISRequest;
			return updateParentalControlsAndCAISRequest != null && this.HasAccount == updateParentalControlsAndCAISRequest.HasAccount && (!this.HasAccount || this.Account.Equals(updateParentalControlsAndCAISRequest.Account)) && this.HasParentalControlInfo == updateParentalControlsAndCAISRequest.HasParentalControlInfo && (!this.HasParentalControlInfo || this.ParentalControlInfo.Equals(updateParentalControlsAndCAISRequest.ParentalControlInfo)) && this.HasCaisId == updateParentalControlsAndCAISRequest.HasCaisId && (!this.HasCaisId || this.CaisId.Equals(updateParentalControlsAndCAISRequest.CaisId)) && this.HasSessionStartTime == updateParentalControlsAndCAISRequest.HasSessionStartTime && (!this.HasSessionStartTime || this.SessionStartTime.Equals(updateParentalControlsAndCAISRequest.SessionStartTime)) && this.HasStartTime == updateParentalControlsAndCAISRequest.HasStartTime && (!this.HasStartTime || this.StartTime.Equals(updateParentalControlsAndCAISRequest.StartTime)) && this.HasEndTime == updateParentalControlsAndCAISRequest.HasEndTime && (!this.HasEndTime || this.EndTime.Equals(updateParentalControlsAndCAISRequest.EndTime));
		}

		// Token: 0x17001197 RID: 4503
		// (get) Token: 0x06005D50 RID: 23888 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005D51 RID: 23889 RVA: 0x0011B209 File Offset: 0x00119409
		public static UpdateParentalControlsAndCAISRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<UpdateParentalControlsAndCAISRequest>(bs, 0, -1);
		}

		// Token: 0x06005D52 RID: 23890 RVA: 0x0011B213 File Offset: 0x00119413
		public void Deserialize(Stream stream)
		{
			UpdateParentalControlsAndCAISRequest.Deserialize(stream, this);
		}

		// Token: 0x06005D53 RID: 23891 RVA: 0x0011B21D File Offset: 0x0011941D
		public static UpdateParentalControlsAndCAISRequest Deserialize(Stream stream, UpdateParentalControlsAndCAISRequest instance)
		{
			return UpdateParentalControlsAndCAISRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005D54 RID: 23892 RVA: 0x0011B228 File Offset: 0x00119428
		public static UpdateParentalControlsAndCAISRequest DeserializeLengthDelimited(Stream stream)
		{
			UpdateParentalControlsAndCAISRequest updateParentalControlsAndCAISRequest = new UpdateParentalControlsAndCAISRequest();
			UpdateParentalControlsAndCAISRequest.DeserializeLengthDelimited(stream, updateParentalControlsAndCAISRequest);
			return updateParentalControlsAndCAISRequest;
		}

		// Token: 0x06005D55 RID: 23893 RVA: 0x0011B244 File Offset: 0x00119444
		public static UpdateParentalControlsAndCAISRequest DeserializeLengthDelimited(Stream stream, UpdateParentalControlsAndCAISRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return UpdateParentalControlsAndCAISRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06005D56 RID: 23894 RVA: 0x0011B26C File Offset: 0x0011946C
		public static UpdateParentalControlsAndCAISRequest Deserialize(Stream stream, UpdateParentalControlsAndCAISRequest instance, long limit)
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
					if (num <= 26)
					{
						if (num != 10)
						{
							if (num != 18)
							{
								if (num == 26)
								{
									instance.CaisId = ProtocolParser.ReadString(stream);
									continue;
								}
							}
							else
							{
								if (instance.ParentalControlInfo == null)
								{
									instance.ParentalControlInfo = ParentalControlInfo.DeserializeLengthDelimited(stream);
									continue;
								}
								ParentalControlInfo.DeserializeLengthDelimited(stream, instance.ParentalControlInfo);
								continue;
							}
						}
						else
						{
							if (instance.Account == null)
							{
								instance.Account = AccountId.DeserializeLengthDelimited(stream);
								continue;
							}
							AccountId.DeserializeLengthDelimited(stream, instance.Account);
							continue;
						}
					}
					else
					{
						if (num == 32)
						{
							instance.SessionStartTime = ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 40)
						{
							instance.StartTime = ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 48)
						{
							instance.EndTime = ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x06005D57 RID: 23895 RVA: 0x0011B3AF File Offset: 0x001195AF
		public void Serialize(Stream stream)
		{
			UpdateParentalControlsAndCAISRequest.Serialize(stream, this);
		}

		// Token: 0x06005D58 RID: 23896 RVA: 0x0011B3B8 File Offset: 0x001195B8
		public static void Serialize(Stream stream, UpdateParentalControlsAndCAISRequest instance)
		{
			if (instance.HasAccount)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Account.GetSerializedSize());
				AccountId.Serialize(stream, instance.Account);
			}
			if (instance.HasParentalControlInfo)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.ParentalControlInfo.GetSerializedSize());
				ParentalControlInfo.Serialize(stream, instance.ParentalControlInfo);
			}
			if (instance.HasCaisId)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.CaisId));
			}
			if (instance.HasSessionStartTime)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, instance.SessionStartTime);
			}
			if (instance.HasStartTime)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, instance.StartTime);
			}
			if (instance.HasEndTime)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt64(stream, instance.EndTime);
			}
		}

		// Token: 0x06005D59 RID: 23897 RVA: 0x0011B49C File Offset: 0x0011969C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAccount)
			{
				num += 1U;
				uint serializedSize = this.Account.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasParentalControlInfo)
			{
				num += 1U;
				uint serializedSize2 = this.ParentalControlInfo.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasCaisId)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.CaisId);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasSessionStartTime)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.SessionStartTime);
			}
			if (this.HasStartTime)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.StartTime);
			}
			if (this.HasEndTime)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.EndTime);
			}
			return num;
		}

		// Token: 0x04001CBC RID: 7356
		public bool HasAccount;

		// Token: 0x04001CBD RID: 7357
		private AccountId _Account;

		// Token: 0x04001CBE RID: 7358
		public bool HasParentalControlInfo;

		// Token: 0x04001CBF RID: 7359
		private ParentalControlInfo _ParentalControlInfo;

		// Token: 0x04001CC0 RID: 7360
		public bool HasCaisId;

		// Token: 0x04001CC1 RID: 7361
		private string _CaisId;

		// Token: 0x04001CC2 RID: 7362
		public bool HasSessionStartTime;

		// Token: 0x04001CC3 RID: 7363
		private ulong _SessionStartTime;

		// Token: 0x04001CC4 RID: 7364
		public bool HasStartTime;

		// Token: 0x04001CC5 RID: 7365
		private ulong _StartTime;

		// Token: 0x04001CC6 RID: 7366
		public bool HasEndTime;

		// Token: 0x04001CC7 RID: 7367
		private ulong _EndTime;
	}
}
