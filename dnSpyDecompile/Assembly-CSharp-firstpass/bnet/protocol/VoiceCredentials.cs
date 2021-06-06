using System;
using System.IO;
using System.Text;
using bnet.protocol.Types;

namespace bnet.protocol
{
	// Token: 0x020002B8 RID: 696
	public class VoiceCredentials : IProtoBuf
	{
		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x060028F1 RID: 10481 RVA: 0x000904AF File Offset: 0x0008E6AF
		// (set) Token: 0x060028F2 RID: 10482 RVA: 0x000904B7 File Offset: 0x0008E6B7
		public string VoiceId
		{
			get
			{
				return this._VoiceId;
			}
			set
			{
				this._VoiceId = value;
				this.HasVoiceId = (value != null);
			}
		}

		// Token: 0x060028F3 RID: 10483 RVA: 0x000904CA File Offset: 0x0008E6CA
		public void SetVoiceId(string val)
		{
			this.VoiceId = val;
		}

		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x060028F4 RID: 10484 RVA: 0x000904D3 File Offset: 0x0008E6D3
		// (set) Token: 0x060028F5 RID: 10485 RVA: 0x000904DB File Offset: 0x0008E6DB
		public string Token
		{
			get
			{
				return this._Token;
			}
			set
			{
				this._Token = value;
				this.HasToken = (value != null);
			}
		}

		// Token: 0x060028F6 RID: 10486 RVA: 0x000904EE File Offset: 0x0008E6EE
		public void SetToken(string val)
		{
			this.Token = val;
		}

		// Token: 0x17000752 RID: 1874
		// (get) Token: 0x060028F7 RID: 10487 RVA: 0x000904F7 File Offset: 0x0008E6F7
		// (set) Token: 0x060028F8 RID: 10488 RVA: 0x000904FF File Offset: 0x0008E6FF
		public string Url
		{
			get
			{
				return this._Url;
			}
			set
			{
				this._Url = value;
				this.HasUrl = (value != null);
			}
		}

		// Token: 0x060028F9 RID: 10489 RVA: 0x00090512 File Offset: 0x0008E712
		public void SetUrl(string val)
		{
			this.Url = val;
		}

		// Token: 0x17000753 RID: 1875
		// (get) Token: 0x060028FA RID: 10490 RVA: 0x0009051B File Offset: 0x0008E71B
		// (set) Token: 0x060028FB RID: 10491 RVA: 0x00090523 File Offset: 0x0008E723
		public VoiceJoinType JoinType
		{
			get
			{
				return this._JoinType;
			}
			set
			{
				this._JoinType = value;
				this.HasJoinType = true;
			}
		}

		// Token: 0x060028FC RID: 10492 RVA: 0x00090533 File Offset: 0x0008E733
		public void SetJoinType(VoiceJoinType val)
		{
			this.JoinType = val;
		}

		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x060028FD RID: 10493 RVA: 0x0009053C File Offset: 0x0008E73C
		// (set) Token: 0x060028FE RID: 10494 RVA: 0x00090544 File Offset: 0x0008E744
		public VoiceMuteReason MuteReason
		{
			get
			{
				return this._MuteReason;
			}
			set
			{
				this._MuteReason = value;
				this.HasMuteReason = true;
			}
		}

		// Token: 0x060028FF RID: 10495 RVA: 0x00090554 File Offset: 0x0008E754
		public void SetMuteReason(VoiceMuteReason val)
		{
			this.MuteReason = val;
		}

		// Token: 0x06002900 RID: 10496 RVA: 0x00090560 File Offset: 0x0008E760
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasVoiceId)
			{
				num ^= this.VoiceId.GetHashCode();
			}
			if (this.HasToken)
			{
				num ^= this.Token.GetHashCode();
			}
			if (this.HasUrl)
			{
				num ^= this.Url.GetHashCode();
			}
			if (this.HasJoinType)
			{
				num ^= this.JoinType.GetHashCode();
			}
			if (this.HasMuteReason)
			{
				num ^= this.MuteReason.GetHashCode();
			}
			return num;
		}

		// Token: 0x06002901 RID: 10497 RVA: 0x000905FC File Offset: 0x0008E7FC
		public override bool Equals(object obj)
		{
			VoiceCredentials voiceCredentials = obj as VoiceCredentials;
			return voiceCredentials != null && this.HasVoiceId == voiceCredentials.HasVoiceId && (!this.HasVoiceId || this.VoiceId.Equals(voiceCredentials.VoiceId)) && this.HasToken == voiceCredentials.HasToken && (!this.HasToken || this.Token.Equals(voiceCredentials.Token)) && this.HasUrl == voiceCredentials.HasUrl && (!this.HasUrl || this.Url.Equals(voiceCredentials.Url)) && this.HasJoinType == voiceCredentials.HasJoinType && (!this.HasJoinType || this.JoinType.Equals(voiceCredentials.JoinType)) && this.HasMuteReason == voiceCredentials.HasMuteReason && (!this.HasMuteReason || this.MuteReason.Equals(voiceCredentials.MuteReason));
		}

		// Token: 0x17000755 RID: 1877
		// (get) Token: 0x06002902 RID: 10498 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002903 RID: 10499 RVA: 0x00090709 File Offset: 0x0008E909
		public static VoiceCredentials ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<VoiceCredentials>(bs, 0, -1);
		}

		// Token: 0x06002904 RID: 10500 RVA: 0x00090713 File Offset: 0x0008E913
		public void Deserialize(Stream stream)
		{
			VoiceCredentials.Deserialize(stream, this);
		}

		// Token: 0x06002905 RID: 10501 RVA: 0x0009071D File Offset: 0x0008E91D
		public static VoiceCredentials Deserialize(Stream stream, VoiceCredentials instance)
		{
			return VoiceCredentials.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06002906 RID: 10502 RVA: 0x00090728 File Offset: 0x0008E928
		public static VoiceCredentials DeserializeLengthDelimited(Stream stream)
		{
			VoiceCredentials voiceCredentials = new VoiceCredentials();
			VoiceCredentials.DeserializeLengthDelimited(stream, voiceCredentials);
			return voiceCredentials;
		}

		// Token: 0x06002907 RID: 10503 RVA: 0x00090744 File Offset: 0x0008E944
		public static VoiceCredentials DeserializeLengthDelimited(Stream stream, VoiceCredentials instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return VoiceCredentials.Deserialize(stream, instance, num);
		}

		// Token: 0x06002908 RID: 10504 RVA: 0x0009076C File Offset: 0x0008E96C
		public static VoiceCredentials Deserialize(Stream stream, VoiceCredentials instance, long limit)
		{
			instance.JoinType = VoiceJoinType.VOICE_JOIN_NORMAL;
			instance.MuteReason = VoiceMuteReason.VOICE_MUTE_REASON_NONE;
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
						if (num == 10)
						{
							instance.VoiceId = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 18)
						{
							instance.Token = ProtocolParser.ReadString(stream);
							continue;
						}
					}
					else
					{
						if (num == 26)
						{
							instance.Url = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 32)
						{
							instance.JoinType = (VoiceJoinType)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 40)
						{
							instance.MuteReason = (VoiceMuteReason)ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x06002909 RID: 10505 RVA: 0x00090863 File Offset: 0x0008EA63
		public void Serialize(Stream stream)
		{
			VoiceCredentials.Serialize(stream, this);
		}

		// Token: 0x0600290A RID: 10506 RVA: 0x0009086C File Offset: 0x0008EA6C
		public static void Serialize(Stream stream, VoiceCredentials instance)
		{
			if (instance.HasVoiceId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.VoiceId));
			}
			if (instance.HasToken)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Token));
			}
			if (instance.HasUrl)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Url));
			}
			if (instance.HasJoinType)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.JoinType));
			}
			if (instance.HasMuteReason)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.MuteReason));
			}
		}

		// Token: 0x0600290B RID: 10507 RVA: 0x00090928 File Offset: 0x0008EB28
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasVoiceId)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.VoiceId);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasToken)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.Token);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (this.HasUrl)
			{
				num += 1U;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(this.Url);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			if (this.HasJoinType)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.JoinType));
			}
			if (this.HasMuteReason)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.MuteReason));
			}
			return num;
		}

		// Token: 0x0400118E RID: 4494
		public bool HasVoiceId;

		// Token: 0x0400118F RID: 4495
		private string _VoiceId;

		// Token: 0x04001190 RID: 4496
		public bool HasToken;

		// Token: 0x04001191 RID: 4497
		private string _Token;

		// Token: 0x04001192 RID: 4498
		public bool HasUrl;

		// Token: 0x04001193 RID: 4499
		private string _Url;

		// Token: 0x04001194 RID: 4500
		public bool HasJoinType;

		// Token: 0x04001195 RID: 4501
		private VoiceJoinType _JoinType;

		// Token: 0x04001196 RID: 4502
		public bool HasMuteReason;

		// Token: 0x04001197 RID: 4503
		private VoiceMuteReason _MuteReason;
	}
}
