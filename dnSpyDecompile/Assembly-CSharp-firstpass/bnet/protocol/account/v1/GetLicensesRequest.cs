using System;
using System.IO;

namespace bnet.protocol.account.v1
{
	// Token: 0x02000512 RID: 1298
	public class GetLicensesRequest : IProtoBuf
	{
		// Token: 0x1700116F RID: 4463
		// (get) Token: 0x06005C78 RID: 23672 RVA: 0x001191D2 File Offset: 0x001173D2
		// (set) Token: 0x06005C79 RID: 23673 RVA: 0x001191DA File Offset: 0x001173DA
		public EntityId TargetId
		{
			get
			{
				return this._TargetId;
			}
			set
			{
				this._TargetId = value;
				this.HasTargetId = (value != null);
			}
		}

		// Token: 0x06005C7A RID: 23674 RVA: 0x001191ED File Offset: 0x001173ED
		public void SetTargetId(EntityId val)
		{
			this.TargetId = val;
		}

		// Token: 0x17001170 RID: 4464
		// (get) Token: 0x06005C7B RID: 23675 RVA: 0x001191F6 File Offset: 0x001173F6
		// (set) Token: 0x06005C7C RID: 23676 RVA: 0x001191FE File Offset: 0x001173FE
		public bool FetchAccountLicenses
		{
			get
			{
				return this._FetchAccountLicenses;
			}
			set
			{
				this._FetchAccountLicenses = value;
				this.HasFetchAccountLicenses = true;
			}
		}

		// Token: 0x06005C7D RID: 23677 RVA: 0x0011920E File Offset: 0x0011740E
		public void SetFetchAccountLicenses(bool val)
		{
			this.FetchAccountLicenses = val;
		}

		// Token: 0x17001171 RID: 4465
		// (get) Token: 0x06005C7E RID: 23678 RVA: 0x00119217 File Offset: 0x00117417
		// (set) Token: 0x06005C7F RID: 23679 RVA: 0x0011921F File Offset: 0x0011741F
		public bool FetchGameAccountLicenses
		{
			get
			{
				return this._FetchGameAccountLicenses;
			}
			set
			{
				this._FetchGameAccountLicenses = value;
				this.HasFetchGameAccountLicenses = true;
			}
		}

		// Token: 0x06005C80 RID: 23680 RVA: 0x0011922F File Offset: 0x0011742F
		public void SetFetchGameAccountLicenses(bool val)
		{
			this.FetchGameAccountLicenses = val;
		}

		// Token: 0x17001172 RID: 4466
		// (get) Token: 0x06005C81 RID: 23681 RVA: 0x00119238 File Offset: 0x00117438
		// (set) Token: 0x06005C82 RID: 23682 RVA: 0x00119240 File Offset: 0x00117440
		public bool FetchDynamicAccountLicenses
		{
			get
			{
				return this._FetchDynamicAccountLicenses;
			}
			set
			{
				this._FetchDynamicAccountLicenses = value;
				this.HasFetchDynamicAccountLicenses = true;
			}
		}

		// Token: 0x06005C83 RID: 23683 RVA: 0x00119250 File Offset: 0x00117450
		public void SetFetchDynamicAccountLicenses(bool val)
		{
			this.FetchDynamicAccountLicenses = val;
		}

		// Token: 0x17001173 RID: 4467
		// (get) Token: 0x06005C84 RID: 23684 RVA: 0x00119259 File Offset: 0x00117459
		// (set) Token: 0x06005C85 RID: 23685 RVA: 0x00119261 File Offset: 0x00117461
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

		// Token: 0x06005C86 RID: 23686 RVA: 0x00119271 File Offset: 0x00117471
		public void SetProgram(uint val)
		{
			this.Program = val;
		}

		// Token: 0x17001174 RID: 4468
		// (get) Token: 0x06005C87 RID: 23687 RVA: 0x0011927A File Offset: 0x0011747A
		// (set) Token: 0x06005C88 RID: 23688 RVA: 0x00119282 File Offset: 0x00117482
		public bool ExcludeUnknownProgram
		{
			get
			{
				return this._ExcludeUnknownProgram;
			}
			set
			{
				this._ExcludeUnknownProgram = value;
				this.HasExcludeUnknownProgram = true;
			}
		}

		// Token: 0x06005C89 RID: 23689 RVA: 0x00119292 File Offset: 0x00117492
		public void SetExcludeUnknownProgram(bool val)
		{
			this.ExcludeUnknownProgram = val;
		}

		// Token: 0x06005C8A RID: 23690 RVA: 0x0011929C File Offset: 0x0011749C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasTargetId)
			{
				num ^= this.TargetId.GetHashCode();
			}
			if (this.HasFetchAccountLicenses)
			{
				num ^= this.FetchAccountLicenses.GetHashCode();
			}
			if (this.HasFetchGameAccountLicenses)
			{
				num ^= this.FetchGameAccountLicenses.GetHashCode();
			}
			if (this.HasFetchDynamicAccountLicenses)
			{
				num ^= this.FetchDynamicAccountLicenses.GetHashCode();
			}
			if (this.HasProgram)
			{
				num ^= this.Program.GetHashCode();
			}
			if (this.HasExcludeUnknownProgram)
			{
				num ^= this.ExcludeUnknownProgram.GetHashCode();
			}
			return num;
		}

		// Token: 0x06005C8B RID: 23691 RVA: 0x0011934C File Offset: 0x0011754C
		public override bool Equals(object obj)
		{
			GetLicensesRequest getLicensesRequest = obj as GetLicensesRequest;
			return getLicensesRequest != null && this.HasTargetId == getLicensesRequest.HasTargetId && (!this.HasTargetId || this.TargetId.Equals(getLicensesRequest.TargetId)) && this.HasFetchAccountLicenses == getLicensesRequest.HasFetchAccountLicenses && (!this.HasFetchAccountLicenses || this.FetchAccountLicenses.Equals(getLicensesRequest.FetchAccountLicenses)) && this.HasFetchGameAccountLicenses == getLicensesRequest.HasFetchGameAccountLicenses && (!this.HasFetchGameAccountLicenses || this.FetchGameAccountLicenses.Equals(getLicensesRequest.FetchGameAccountLicenses)) && this.HasFetchDynamicAccountLicenses == getLicensesRequest.HasFetchDynamicAccountLicenses && (!this.HasFetchDynamicAccountLicenses || this.FetchDynamicAccountLicenses.Equals(getLicensesRequest.FetchDynamicAccountLicenses)) && this.HasProgram == getLicensesRequest.HasProgram && (!this.HasProgram || this.Program.Equals(getLicensesRequest.Program)) && this.HasExcludeUnknownProgram == getLicensesRequest.HasExcludeUnknownProgram && (!this.HasExcludeUnknownProgram || this.ExcludeUnknownProgram.Equals(getLicensesRequest.ExcludeUnknownProgram));
		}

		// Token: 0x17001175 RID: 4469
		// (get) Token: 0x06005C8C RID: 23692 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005C8D RID: 23693 RVA: 0x00119477 File Offset: 0x00117677
		public static GetLicensesRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetLicensesRequest>(bs, 0, -1);
		}

		// Token: 0x06005C8E RID: 23694 RVA: 0x00119481 File Offset: 0x00117681
		public void Deserialize(Stream stream)
		{
			GetLicensesRequest.Deserialize(stream, this);
		}

		// Token: 0x06005C8F RID: 23695 RVA: 0x0011948B File Offset: 0x0011768B
		public static GetLicensesRequest Deserialize(Stream stream, GetLicensesRequest instance)
		{
			return GetLicensesRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005C90 RID: 23696 RVA: 0x00119498 File Offset: 0x00117698
		public static GetLicensesRequest DeserializeLengthDelimited(Stream stream)
		{
			GetLicensesRequest getLicensesRequest = new GetLicensesRequest();
			GetLicensesRequest.DeserializeLengthDelimited(stream, getLicensesRequest);
			return getLicensesRequest;
		}

		// Token: 0x06005C91 RID: 23697 RVA: 0x001194B4 File Offset: 0x001176B4
		public static GetLicensesRequest DeserializeLengthDelimited(Stream stream, GetLicensesRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetLicensesRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06005C92 RID: 23698 RVA: 0x001194DC File Offset: 0x001176DC
		public static GetLicensesRequest Deserialize(Stream stream, GetLicensesRequest instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			instance.ExcludeUnknownProgram = false;
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
						if (num != 10)
						{
							if (num == 16)
							{
								instance.FetchAccountLicenses = ProtocolParser.ReadBool(stream);
								continue;
							}
							if (num == 24)
							{
								instance.FetchGameAccountLicenses = ProtocolParser.ReadBool(stream);
								continue;
							}
						}
						else
						{
							if (instance.TargetId == null)
							{
								instance.TargetId = EntityId.DeserializeLengthDelimited(stream);
								continue;
							}
							EntityId.DeserializeLengthDelimited(stream, instance.TargetId);
							continue;
						}
					}
					else
					{
						if (num == 32)
						{
							instance.FetchDynamicAccountLicenses = ProtocolParser.ReadBool(stream);
							continue;
						}
						if (num == 45)
						{
							instance.Program = binaryReader.ReadUInt32();
							continue;
						}
						if (num == 48)
						{
							instance.ExcludeUnknownProgram = ProtocolParser.ReadBool(stream);
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

		// Token: 0x06005C93 RID: 23699 RVA: 0x0011960D File Offset: 0x0011780D
		public void Serialize(Stream stream)
		{
			GetLicensesRequest.Serialize(stream, this);
		}

		// Token: 0x06005C94 RID: 23700 RVA: 0x00119618 File Offset: 0x00117818
		public static void Serialize(Stream stream, GetLicensesRequest instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasTargetId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.TargetId.GetSerializedSize());
				EntityId.Serialize(stream, instance.TargetId);
			}
			if (instance.HasFetchAccountLicenses)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteBool(stream, instance.FetchAccountLicenses);
			}
			if (instance.HasFetchGameAccountLicenses)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteBool(stream, instance.FetchGameAccountLicenses);
			}
			if (instance.HasFetchDynamicAccountLicenses)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteBool(stream, instance.FetchDynamicAccountLicenses);
			}
			if (instance.HasProgram)
			{
				stream.WriteByte(45);
				binaryWriter.Write(instance.Program);
			}
			if (instance.HasExcludeUnknownProgram)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteBool(stream, instance.ExcludeUnknownProgram);
			}
		}

		// Token: 0x06005C95 RID: 23701 RVA: 0x001196E8 File Offset: 0x001178E8
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasTargetId)
			{
				num += 1U;
				uint serializedSize = this.TargetId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasFetchAccountLicenses)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasFetchGameAccountLicenses)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasFetchDynamicAccountLicenses)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasProgram)
			{
				num += 1U;
				num += 4U;
			}
			if (this.HasExcludeUnknownProgram)
			{
				num += 1U;
				num += 1U;
			}
			return num;
		}

		// Token: 0x04001C9B RID: 7323
		public bool HasTargetId;

		// Token: 0x04001C9C RID: 7324
		private EntityId _TargetId;

		// Token: 0x04001C9D RID: 7325
		public bool HasFetchAccountLicenses;

		// Token: 0x04001C9E RID: 7326
		private bool _FetchAccountLicenses;

		// Token: 0x04001C9F RID: 7327
		public bool HasFetchGameAccountLicenses;

		// Token: 0x04001CA0 RID: 7328
		private bool _FetchGameAccountLicenses;

		// Token: 0x04001CA1 RID: 7329
		public bool HasFetchDynamicAccountLicenses;

		// Token: 0x04001CA2 RID: 7330
		private bool _FetchDynamicAccountLicenses;

		// Token: 0x04001CA3 RID: 7331
		public bool HasProgram;

		// Token: 0x04001CA4 RID: 7332
		private uint _Program;

		// Token: 0x04001CA5 RID: 7333
		public bool HasExcludeUnknownProgram;

		// Token: 0x04001CA6 RID: 7334
		private bool _ExcludeUnknownProgram;
	}
}
