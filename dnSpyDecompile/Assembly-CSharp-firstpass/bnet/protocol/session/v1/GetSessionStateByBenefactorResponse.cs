using System;
using System.Collections.Generic;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.session.v1
{
	// Token: 0x0200030C RID: 780
	public class GetSessionStateByBenefactorResponse : IProtoBuf
	{
		// Token: 0x17000873 RID: 2163
		// (get) Token: 0x06002F3E RID: 12094 RVA: 0x000A05E3 File Offset: 0x0009E7E3
		// (set) Token: 0x06002F3F RID: 12095 RVA: 0x000A05EB File Offset: 0x0009E7EB
		public GameAccountHandle BenefactorHandle
		{
			get
			{
				return this._BenefactorHandle;
			}
			set
			{
				this._BenefactorHandle = value;
				this.HasBenefactorHandle = (value != null);
			}
		}

		// Token: 0x06002F40 RID: 12096 RVA: 0x000A05FE File Offset: 0x0009E7FE
		public void SetBenefactorHandle(GameAccountHandle val)
		{
			this.BenefactorHandle = val;
		}

		// Token: 0x17000874 RID: 2164
		// (get) Token: 0x06002F41 RID: 12097 RVA: 0x000A0607 File Offset: 0x0009E807
		// (set) Token: 0x06002F42 RID: 12098 RVA: 0x000A060F File Offset: 0x0009E80F
		public List<SessionState> Session
		{
			get
			{
				return this._Session;
			}
			set
			{
				this._Session = value;
			}
		}

		// Token: 0x17000875 RID: 2165
		// (get) Token: 0x06002F43 RID: 12099 RVA: 0x000A0607 File Offset: 0x0009E807
		public List<SessionState> SessionList
		{
			get
			{
				return this._Session;
			}
		}

		// Token: 0x17000876 RID: 2166
		// (get) Token: 0x06002F44 RID: 12100 RVA: 0x000A0618 File Offset: 0x0009E818
		public int SessionCount
		{
			get
			{
				return this._Session.Count;
			}
		}

		// Token: 0x06002F45 RID: 12101 RVA: 0x000A0625 File Offset: 0x0009E825
		public void AddSession(SessionState val)
		{
			this._Session.Add(val);
		}

		// Token: 0x06002F46 RID: 12102 RVA: 0x000A0633 File Offset: 0x0009E833
		public void ClearSession()
		{
			this._Session.Clear();
		}

		// Token: 0x06002F47 RID: 12103 RVA: 0x000A0640 File Offset: 0x0009E840
		public void SetSession(List<SessionState> val)
		{
			this.Session = val;
		}

		// Token: 0x17000877 RID: 2167
		// (get) Token: 0x06002F48 RID: 12104 RVA: 0x000A0649 File Offset: 0x0009E849
		// (set) Token: 0x06002F49 RID: 12105 RVA: 0x000A0651 File Offset: 0x0009E851
		public uint MinutesRemaining
		{
			get
			{
				return this._MinutesRemaining;
			}
			set
			{
				this._MinutesRemaining = value;
				this.HasMinutesRemaining = true;
			}
		}

		// Token: 0x06002F4A RID: 12106 RVA: 0x000A0661 File Offset: 0x0009E861
		public void SetMinutesRemaining(uint val)
		{
			this.MinutesRemaining = val;
		}

		// Token: 0x06002F4B RID: 12107 RVA: 0x000A066C File Offset: 0x0009E86C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasBenefactorHandle)
			{
				num ^= this.BenefactorHandle.GetHashCode();
			}
			foreach (SessionState sessionState in this.Session)
			{
				num ^= sessionState.GetHashCode();
			}
			if (this.HasMinutesRemaining)
			{
				num ^= this.MinutesRemaining.GetHashCode();
			}
			return num;
		}

		// Token: 0x06002F4C RID: 12108 RVA: 0x000A0700 File Offset: 0x0009E900
		public override bool Equals(object obj)
		{
			GetSessionStateByBenefactorResponse getSessionStateByBenefactorResponse = obj as GetSessionStateByBenefactorResponse;
			if (getSessionStateByBenefactorResponse == null)
			{
				return false;
			}
			if (this.HasBenefactorHandle != getSessionStateByBenefactorResponse.HasBenefactorHandle || (this.HasBenefactorHandle && !this.BenefactorHandle.Equals(getSessionStateByBenefactorResponse.BenefactorHandle)))
			{
				return false;
			}
			if (this.Session.Count != getSessionStateByBenefactorResponse.Session.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Session.Count; i++)
			{
				if (!this.Session[i].Equals(getSessionStateByBenefactorResponse.Session[i]))
				{
					return false;
				}
			}
			return this.HasMinutesRemaining == getSessionStateByBenefactorResponse.HasMinutesRemaining && (!this.HasMinutesRemaining || this.MinutesRemaining.Equals(getSessionStateByBenefactorResponse.MinutesRemaining));
		}

		// Token: 0x17000878 RID: 2168
		// (get) Token: 0x06002F4D RID: 12109 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002F4E RID: 12110 RVA: 0x000A07C4 File Offset: 0x0009E9C4
		public static GetSessionStateByBenefactorResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetSessionStateByBenefactorResponse>(bs, 0, -1);
		}

		// Token: 0x06002F4F RID: 12111 RVA: 0x000A07CE File Offset: 0x0009E9CE
		public void Deserialize(Stream stream)
		{
			GetSessionStateByBenefactorResponse.Deserialize(stream, this);
		}

		// Token: 0x06002F50 RID: 12112 RVA: 0x000A07D8 File Offset: 0x0009E9D8
		public static GetSessionStateByBenefactorResponse Deserialize(Stream stream, GetSessionStateByBenefactorResponse instance)
		{
			return GetSessionStateByBenefactorResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06002F51 RID: 12113 RVA: 0x000A07E4 File Offset: 0x0009E9E4
		public static GetSessionStateByBenefactorResponse DeserializeLengthDelimited(Stream stream)
		{
			GetSessionStateByBenefactorResponse getSessionStateByBenefactorResponse = new GetSessionStateByBenefactorResponse();
			GetSessionStateByBenefactorResponse.DeserializeLengthDelimited(stream, getSessionStateByBenefactorResponse);
			return getSessionStateByBenefactorResponse;
		}

		// Token: 0x06002F52 RID: 12114 RVA: 0x000A0800 File Offset: 0x0009EA00
		public static GetSessionStateByBenefactorResponse DeserializeLengthDelimited(Stream stream, GetSessionStateByBenefactorResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetSessionStateByBenefactorResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06002F53 RID: 12115 RVA: 0x000A0828 File Offset: 0x0009EA28
		public static GetSessionStateByBenefactorResponse Deserialize(Stream stream, GetSessionStateByBenefactorResponse instance, long limit)
		{
			if (instance.Session == null)
			{
				instance.Session = new List<SessionState>();
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
				else if (num != 10)
				{
					if (num != 18)
					{
						if (num != 24)
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
							instance.MinutesRemaining = ProtocolParser.ReadUInt32(stream);
						}
					}
					else
					{
						instance.Session.Add(SessionState.DeserializeLengthDelimited(stream));
					}
				}
				else if (instance.BenefactorHandle == null)
				{
					instance.BenefactorHandle = GameAccountHandle.DeserializeLengthDelimited(stream);
				}
				else
				{
					GameAccountHandle.DeserializeLengthDelimited(stream, instance.BenefactorHandle);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06002F54 RID: 12116 RVA: 0x000A090E File Offset: 0x0009EB0E
		public void Serialize(Stream stream)
		{
			GetSessionStateByBenefactorResponse.Serialize(stream, this);
		}

		// Token: 0x06002F55 RID: 12117 RVA: 0x000A0918 File Offset: 0x0009EB18
		public static void Serialize(Stream stream, GetSessionStateByBenefactorResponse instance)
		{
			if (instance.HasBenefactorHandle)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.BenefactorHandle.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.BenefactorHandle);
			}
			if (instance.Session.Count > 0)
			{
				foreach (SessionState sessionState in instance.Session)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, sessionState.GetSerializedSize());
					SessionState.Serialize(stream, sessionState);
				}
			}
			if (instance.HasMinutesRemaining)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt32(stream, instance.MinutesRemaining);
			}
		}

		// Token: 0x06002F56 RID: 12118 RVA: 0x000A09D8 File Offset: 0x0009EBD8
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasBenefactorHandle)
			{
				num += 1U;
				uint serializedSize = this.BenefactorHandle.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.Session.Count > 0)
			{
				foreach (SessionState sessionState in this.Session)
				{
					num += 1U;
					uint serializedSize2 = sessionState.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			if (this.HasMinutesRemaining)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.MinutesRemaining);
			}
			return num;
		}

		// Token: 0x04001306 RID: 4870
		public bool HasBenefactorHandle;

		// Token: 0x04001307 RID: 4871
		private GameAccountHandle _BenefactorHandle;

		// Token: 0x04001308 RID: 4872
		private List<SessionState> _Session = new List<SessionState>();

		// Token: 0x04001309 RID: 4873
		public bool HasMinutesRemaining;

		// Token: 0x0400130A RID: 4874
		private uint _MinutesRemaining;
	}
}
