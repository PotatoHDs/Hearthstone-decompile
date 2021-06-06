using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.NGDP
{
	// Token: 0x02001188 RID: 4488
	public class VersionError : IProtoBuf
	{
		// Token: 0x17000E2B RID: 3627
		// (get) Token: 0x0600C580 RID: 50560 RVA: 0x003B7ADC File Offset: 0x003B5CDC
		// (set) Token: 0x0600C581 RID: 50561 RVA: 0x003B7AE4 File Offset: 0x003B5CE4
		public uint ErrorCode
		{
			get
			{
				return this._ErrorCode;
			}
			set
			{
				this._ErrorCode = value;
				this.HasErrorCode = true;
			}
		}

		// Token: 0x17000E2C RID: 3628
		// (get) Token: 0x0600C582 RID: 50562 RVA: 0x003B7AF4 File Offset: 0x003B5CF4
		// (set) Token: 0x0600C583 RID: 50563 RVA: 0x003B7AFC File Offset: 0x003B5CFC
		public uint AgentState
		{
			get
			{
				return this._AgentState;
			}
			set
			{
				this._AgentState = value;
				this.HasAgentState = true;
			}
		}

		// Token: 0x17000E2D RID: 3629
		// (get) Token: 0x0600C584 RID: 50564 RVA: 0x003B7B0C File Offset: 0x003B5D0C
		// (set) Token: 0x0600C585 RID: 50565 RVA: 0x003B7B14 File Offset: 0x003B5D14
		public string Languages
		{
			get
			{
				return this._Languages;
			}
			set
			{
				this._Languages = value;
				this.HasLanguages = (value != null);
			}
		}

		// Token: 0x17000E2E RID: 3630
		// (get) Token: 0x0600C586 RID: 50566 RVA: 0x003B7B27 File Offset: 0x003B5D27
		// (set) Token: 0x0600C587 RID: 50567 RVA: 0x003B7B2F File Offset: 0x003B5D2F
		public string Region
		{
			get
			{
				return this._Region;
			}
			set
			{
				this._Region = value;
				this.HasRegion = (value != null);
			}
		}

		// Token: 0x17000E2F RID: 3631
		// (get) Token: 0x0600C588 RID: 50568 RVA: 0x003B7B42 File Offset: 0x003B5D42
		// (set) Token: 0x0600C589 RID: 50569 RVA: 0x003B7B4A File Offset: 0x003B5D4A
		public string Branch
		{
			get
			{
				return this._Branch;
			}
			set
			{
				this._Branch = value;
				this.HasBranch = (value != null);
			}
		}

		// Token: 0x17000E30 RID: 3632
		// (get) Token: 0x0600C58A RID: 50570 RVA: 0x003B7B5D File Offset: 0x003B5D5D
		// (set) Token: 0x0600C58B RID: 50571 RVA: 0x003B7B65 File Offset: 0x003B5D65
		public string AdditionalTags
		{
			get
			{
				return this._AdditionalTags;
			}
			set
			{
				this._AdditionalTags = value;
				this.HasAdditionalTags = (value != null);
			}
		}

		// Token: 0x0600C58C RID: 50572 RVA: 0x003B7B78 File Offset: 0x003B5D78
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasErrorCode)
			{
				num ^= this.ErrorCode.GetHashCode();
			}
			if (this.HasAgentState)
			{
				num ^= this.AgentState.GetHashCode();
			}
			if (this.HasLanguages)
			{
				num ^= this.Languages.GetHashCode();
			}
			if (this.HasRegion)
			{
				num ^= this.Region.GetHashCode();
			}
			if (this.HasBranch)
			{
				num ^= this.Branch.GetHashCode();
			}
			if (this.HasAdditionalTags)
			{
				num ^= this.AdditionalTags.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600C58D RID: 50573 RVA: 0x003B7C1C File Offset: 0x003B5E1C
		public override bool Equals(object obj)
		{
			VersionError versionError = obj as VersionError;
			return versionError != null && this.HasErrorCode == versionError.HasErrorCode && (!this.HasErrorCode || this.ErrorCode.Equals(versionError.ErrorCode)) && this.HasAgentState == versionError.HasAgentState && (!this.HasAgentState || this.AgentState.Equals(versionError.AgentState)) && this.HasLanguages == versionError.HasLanguages && (!this.HasLanguages || this.Languages.Equals(versionError.Languages)) && this.HasRegion == versionError.HasRegion && (!this.HasRegion || this.Region.Equals(versionError.Region)) && this.HasBranch == versionError.HasBranch && (!this.HasBranch || this.Branch.Equals(versionError.Branch)) && this.HasAdditionalTags == versionError.HasAdditionalTags && (!this.HasAdditionalTags || this.AdditionalTags.Equals(versionError.AdditionalTags));
		}

		// Token: 0x0600C58E RID: 50574 RVA: 0x003B7D3E File Offset: 0x003B5F3E
		public void Deserialize(Stream stream)
		{
			VersionError.Deserialize(stream, this);
		}

		// Token: 0x0600C58F RID: 50575 RVA: 0x003B7D48 File Offset: 0x003B5F48
		public static VersionError Deserialize(Stream stream, VersionError instance)
		{
			return VersionError.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600C590 RID: 50576 RVA: 0x003B7D54 File Offset: 0x003B5F54
		public static VersionError DeserializeLengthDelimited(Stream stream)
		{
			VersionError versionError = new VersionError();
			VersionError.DeserializeLengthDelimited(stream, versionError);
			return versionError;
		}

		// Token: 0x0600C591 RID: 50577 RVA: 0x003B7D70 File Offset: 0x003B5F70
		public static VersionError DeserializeLengthDelimited(Stream stream, VersionError instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return VersionError.Deserialize(stream, instance, num);
		}

		// Token: 0x0600C592 RID: 50578 RVA: 0x003B7D98 File Offset: 0x003B5F98
		public static VersionError Deserialize(Stream stream, VersionError instance, long limit)
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
					if (num <= 42)
					{
						if (num == 8)
						{
							instance.ErrorCode = ProtocolParser.ReadUInt32(stream);
							continue;
						}
						if (num == 32)
						{
							instance.AgentState = ProtocolParser.ReadUInt32(stream);
							continue;
						}
						if (num == 42)
						{
							instance.Languages = ProtocolParser.ReadString(stream);
							continue;
						}
					}
					else
					{
						if (num == 50)
						{
							instance.Region = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 58)
						{
							instance.Branch = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 66)
						{
							instance.AdditionalTags = ProtocolParser.ReadString(stream);
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

		// Token: 0x0600C593 RID: 50579 RVA: 0x003B7E94 File Offset: 0x003B6094
		public void Serialize(Stream stream)
		{
			VersionError.Serialize(stream, this);
		}

		// Token: 0x0600C594 RID: 50580 RVA: 0x003B7EA0 File Offset: 0x003B60A0
		public static void Serialize(Stream stream, VersionError instance)
		{
			if (instance.HasErrorCode)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt32(stream, instance.ErrorCode);
			}
			if (instance.HasAgentState)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt32(stream, instance.AgentState);
			}
			if (instance.HasLanguages)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Languages));
			}
			if (instance.HasRegion)
			{
				stream.WriteByte(50);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Region));
			}
			if (instance.HasBranch)
			{
				stream.WriteByte(58);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Branch));
			}
			if (instance.HasAdditionalTags)
			{
				stream.WriteByte(66);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.AdditionalTags));
			}
		}

		// Token: 0x0600C595 RID: 50581 RVA: 0x003B7F7C File Offset: 0x003B617C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasErrorCode)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.ErrorCode);
			}
			if (this.HasAgentState)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.AgentState);
			}
			if (this.HasLanguages)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Languages);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasRegion)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.Region);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (this.HasBranch)
			{
				num += 1U;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(this.Branch);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			if (this.HasAdditionalTags)
			{
				num += 1U;
				uint byteCount4 = (uint)Encoding.UTF8.GetByteCount(this.AdditionalTags);
				num += ProtocolParser.SizeOfUInt32(byteCount4) + byteCount4;
			}
			return num;
		}

		// Token: 0x04009DA3 RID: 40355
		public bool HasErrorCode;

		// Token: 0x04009DA4 RID: 40356
		private uint _ErrorCode;

		// Token: 0x04009DA5 RID: 40357
		public bool HasAgentState;

		// Token: 0x04009DA6 RID: 40358
		private uint _AgentState;

		// Token: 0x04009DA7 RID: 40359
		public bool HasLanguages;

		// Token: 0x04009DA8 RID: 40360
		private string _Languages;

		// Token: 0x04009DA9 RID: 40361
		public bool HasRegion;

		// Token: 0x04009DAA RID: 40362
		private string _Region;

		// Token: 0x04009DAB RID: 40363
		public bool HasBranch;

		// Token: 0x04009DAC RID: 40364
		private string _Branch;

		// Token: 0x04009DAD RID: 40365
		public bool HasAdditionalTags;

		// Token: 0x04009DAE RID: 40366
		private string _AdditionalTags;
	}
}
