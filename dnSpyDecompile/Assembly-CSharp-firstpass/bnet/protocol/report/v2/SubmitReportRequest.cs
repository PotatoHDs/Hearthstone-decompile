using System;
using System.IO;
using System.Text;
using bnet.protocol.account.v1;

namespace bnet.protocol.report.v2
{
	// Token: 0x0200031F RID: 799
	public class SubmitReportRequest : IProtoBuf
	{
		// Token: 0x170008C1 RID: 2241
		// (get) Token: 0x060030DE RID: 12510 RVA: 0x000A4870 File Offset: 0x000A2A70
		// (set) Token: 0x060030DF RID: 12511 RVA: 0x000A4878 File Offset: 0x000A2A78
		public AccountId AgentId
		{
			get
			{
				return this._AgentId;
			}
			set
			{
				this._AgentId = value;
				this.HasAgentId = (value != null);
			}
		}

		// Token: 0x060030E0 RID: 12512 RVA: 0x000A488B File Offset: 0x000A2A8B
		public void SetAgentId(AccountId val)
		{
			this.AgentId = val;
		}

		// Token: 0x170008C2 RID: 2242
		// (get) Token: 0x060030E1 RID: 12513 RVA: 0x000A4894 File Offset: 0x000A2A94
		// (set) Token: 0x060030E2 RID: 12514 RVA: 0x000A489C File Offset: 0x000A2A9C
		public string UserDescription
		{
			get
			{
				return this._UserDescription;
			}
			set
			{
				this._UserDescription = value;
				this.HasUserDescription = (value != null);
			}
		}

		// Token: 0x060030E3 RID: 12515 RVA: 0x000A48AF File Offset: 0x000A2AAF
		public void SetUserDescription(string val)
		{
			this.UserDescription = val;
		}

		// Token: 0x170008C3 RID: 2243
		// (get) Token: 0x060030E4 RID: 12516 RVA: 0x000A48B8 File Offset: 0x000A2AB8
		// (set) Token: 0x060030E5 RID: 12517 RVA: 0x000A48C0 File Offset: 0x000A2AC0
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

		// Token: 0x060030E6 RID: 12518 RVA: 0x000A48D0 File Offset: 0x000A2AD0
		public void SetProgram(uint val)
		{
			this.Program = val;
		}

		// Token: 0x170008C4 RID: 2244
		// (get) Token: 0x060030E7 RID: 12519 RVA: 0x000A48D9 File Offset: 0x000A2AD9
		// (set) Token: 0x060030E8 RID: 12520 RVA: 0x000A48E1 File Offset: 0x000A2AE1
		public UserOptions UserOptions
		{
			get
			{
				return this._UserOptions;
			}
			set
			{
				this._UserOptions = value;
				this.HasUserOptions = (value != null);
			}
		}

		// Token: 0x060030E9 RID: 12521 RVA: 0x000A48F4 File Offset: 0x000A2AF4
		public void SetUserOptions(UserOptions val)
		{
			this.UserOptions = val;
		}

		// Token: 0x170008C5 RID: 2245
		// (get) Token: 0x060030EA RID: 12522 RVA: 0x000A48FD File Offset: 0x000A2AFD
		// (set) Token: 0x060030EB RID: 12523 RVA: 0x000A4905 File Offset: 0x000A2B05
		public ClubOptions ClubOptions
		{
			get
			{
				return this._ClubOptions;
			}
			set
			{
				this._ClubOptions = value;
				this.HasClubOptions = (value != null);
			}
		}

		// Token: 0x060030EC RID: 12524 RVA: 0x000A4918 File Offset: 0x000A2B18
		public void SetClubOptions(ClubOptions val)
		{
			this.ClubOptions = val;
		}

		// Token: 0x060030ED RID: 12525 RVA: 0x000A4924 File Offset: 0x000A2B24
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAgentId)
			{
				num ^= this.AgentId.GetHashCode();
			}
			if (this.HasUserDescription)
			{
				num ^= this.UserDescription.GetHashCode();
			}
			if (this.HasProgram)
			{
				num ^= this.Program.GetHashCode();
			}
			if (this.HasUserOptions)
			{
				num ^= this.UserOptions.GetHashCode();
			}
			if (this.HasClubOptions)
			{
				num ^= this.ClubOptions.GetHashCode();
			}
			return num;
		}

		// Token: 0x060030EE RID: 12526 RVA: 0x000A49B0 File Offset: 0x000A2BB0
		public override bool Equals(object obj)
		{
			SubmitReportRequest submitReportRequest = obj as SubmitReportRequest;
			return submitReportRequest != null && this.HasAgentId == submitReportRequest.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(submitReportRequest.AgentId)) && this.HasUserDescription == submitReportRequest.HasUserDescription && (!this.HasUserDescription || this.UserDescription.Equals(submitReportRequest.UserDescription)) && this.HasProgram == submitReportRequest.HasProgram && (!this.HasProgram || this.Program.Equals(submitReportRequest.Program)) && this.HasUserOptions == submitReportRequest.HasUserOptions && (!this.HasUserOptions || this.UserOptions.Equals(submitReportRequest.UserOptions)) && this.HasClubOptions == submitReportRequest.HasClubOptions && (!this.HasClubOptions || this.ClubOptions.Equals(submitReportRequest.ClubOptions));
		}

		// Token: 0x170008C6 RID: 2246
		// (get) Token: 0x060030EF RID: 12527 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060030F0 RID: 12528 RVA: 0x000A4AA4 File Offset: 0x000A2CA4
		public static SubmitReportRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SubmitReportRequest>(bs, 0, -1);
		}

		// Token: 0x060030F1 RID: 12529 RVA: 0x000A4AAE File Offset: 0x000A2CAE
		public void Deserialize(Stream stream)
		{
			SubmitReportRequest.Deserialize(stream, this);
		}

		// Token: 0x060030F2 RID: 12530 RVA: 0x000A4AB8 File Offset: 0x000A2CB8
		public static SubmitReportRequest Deserialize(Stream stream, SubmitReportRequest instance)
		{
			return SubmitReportRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060030F3 RID: 12531 RVA: 0x000A4AC4 File Offset: 0x000A2CC4
		public static SubmitReportRequest DeserializeLengthDelimited(Stream stream)
		{
			SubmitReportRequest submitReportRequest = new SubmitReportRequest();
			SubmitReportRequest.DeserializeLengthDelimited(stream, submitReportRequest);
			return submitReportRequest;
		}

		// Token: 0x060030F4 RID: 12532 RVA: 0x000A4AE0 File Offset: 0x000A2CE0
		public static SubmitReportRequest DeserializeLengthDelimited(Stream stream, SubmitReportRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SubmitReportRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x060030F5 RID: 12533 RVA: 0x000A4B08 File Offset: 0x000A2D08
		public static SubmitReportRequest Deserialize(Stream stream, SubmitReportRequest instance, long limit)
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
					if (num <= 18)
					{
						if (num != 10)
						{
							if (num == 18)
							{
								instance.UserDescription = ProtocolParser.ReadString(stream);
								continue;
							}
						}
						else
						{
							if (instance.AgentId == null)
							{
								instance.AgentId = AccountId.DeserializeLengthDelimited(stream);
								continue;
							}
							AccountId.DeserializeLengthDelimited(stream, instance.AgentId);
							continue;
						}
					}
					else
					{
						if (num == 24)
						{
							instance.Program = ProtocolParser.ReadUInt32(stream);
							continue;
						}
						if (num != 82)
						{
							if (num == 90)
							{
								if (instance.ClubOptions == null)
								{
									instance.ClubOptions = ClubOptions.DeserializeLengthDelimited(stream);
									continue;
								}
								ClubOptions.DeserializeLengthDelimited(stream, instance.ClubOptions);
								continue;
							}
						}
						else
						{
							if (instance.UserOptions == null)
							{
								instance.UserOptions = UserOptions.DeserializeLengthDelimited(stream);
								continue;
							}
							UserOptions.DeserializeLengthDelimited(stream, instance.UserOptions);
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

		// Token: 0x060030F6 RID: 12534 RVA: 0x000A4C46 File Offset: 0x000A2E46
		public void Serialize(Stream stream)
		{
			SubmitReportRequest.Serialize(stream, this);
		}

		// Token: 0x060030F7 RID: 12535 RVA: 0x000A4C50 File Offset: 0x000A2E50
		public static void Serialize(Stream stream, SubmitReportRequest instance)
		{
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				AccountId.Serialize(stream, instance.AgentId);
			}
			if (instance.HasUserDescription)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.UserDescription));
			}
			if (instance.HasProgram)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt32(stream, instance.Program);
			}
			if (instance.HasUserOptions)
			{
				stream.WriteByte(82);
				ProtocolParser.WriteUInt32(stream, instance.UserOptions.GetSerializedSize());
				UserOptions.Serialize(stream, instance.UserOptions);
			}
			if (instance.HasClubOptions)
			{
				stream.WriteByte(90);
				ProtocolParser.WriteUInt32(stream, instance.ClubOptions.GetSerializedSize());
				ClubOptions.Serialize(stream, instance.ClubOptions);
			}
		}

		// Token: 0x060030F8 RID: 12536 RVA: 0x000A4D28 File Offset: 0x000A2F28
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAgentId)
			{
				num += 1U;
				uint serializedSize = this.AgentId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasUserDescription)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.UserDescription);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasProgram)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.Program);
			}
			if (this.HasUserOptions)
			{
				num += 1U;
				uint serializedSize2 = this.UserOptions.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasClubOptions)
			{
				num += 1U;
				uint serializedSize3 = this.ClubOptions.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			return num;
		}

		// Token: 0x04001366 RID: 4966
		public bool HasAgentId;

		// Token: 0x04001367 RID: 4967
		private AccountId _AgentId;

		// Token: 0x04001368 RID: 4968
		public bool HasUserDescription;

		// Token: 0x04001369 RID: 4969
		private string _UserDescription;

		// Token: 0x0400136A RID: 4970
		public bool HasProgram;

		// Token: 0x0400136B RID: 4971
		private uint _Program;

		// Token: 0x0400136C RID: 4972
		public bool HasUserOptions;

		// Token: 0x0400136D RID: 4973
		private UserOptions _UserOptions;

		// Token: 0x0400136E RID: 4974
		public bool HasClubOptions;

		// Token: 0x0400136F RID: 4975
		private ClubOptions _ClubOptions;
	}
}
