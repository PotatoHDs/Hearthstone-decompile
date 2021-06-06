using System;
using System.IO;
using System.Text;

namespace bnet.protocol.challenge.v1
{
	// Token: 0x020004E2 RID: 1250
	public class ChallengeExternalRequest : IProtoBuf
	{
		// Token: 0x170010A1 RID: 4257
		// (get) Token: 0x06005846 RID: 22598 RVA: 0x0010E732 File Offset: 0x0010C932
		// (set) Token: 0x06005847 RID: 22599 RVA: 0x0010E73A File Offset: 0x0010C93A
		public string RequestToken
		{
			get
			{
				return this._RequestToken;
			}
			set
			{
				this._RequestToken = value;
				this.HasRequestToken = (value != null);
			}
		}

		// Token: 0x06005848 RID: 22600 RVA: 0x0010E74D File Offset: 0x0010C94D
		public void SetRequestToken(string val)
		{
			this.RequestToken = val;
		}

		// Token: 0x170010A2 RID: 4258
		// (get) Token: 0x06005849 RID: 22601 RVA: 0x0010E756 File Offset: 0x0010C956
		// (set) Token: 0x0600584A RID: 22602 RVA: 0x0010E75E File Offset: 0x0010C95E
		public string PayloadType
		{
			get
			{
				return this._PayloadType;
			}
			set
			{
				this._PayloadType = value;
				this.HasPayloadType = (value != null);
			}
		}

		// Token: 0x0600584B RID: 22603 RVA: 0x0010E771 File Offset: 0x0010C971
		public void SetPayloadType(string val)
		{
			this.PayloadType = val;
		}

		// Token: 0x170010A3 RID: 4259
		// (get) Token: 0x0600584C RID: 22604 RVA: 0x0010E77A File Offset: 0x0010C97A
		// (set) Token: 0x0600584D RID: 22605 RVA: 0x0010E782 File Offset: 0x0010C982
		public byte[] Payload
		{
			get
			{
				return this._Payload;
			}
			set
			{
				this._Payload = value;
				this.HasPayload = (value != null);
			}
		}

		// Token: 0x0600584E RID: 22606 RVA: 0x0010E795 File Offset: 0x0010C995
		public void SetPayload(byte[] val)
		{
			this.Payload = val;
		}

		// Token: 0x0600584F RID: 22607 RVA: 0x0010E7A0 File Offset: 0x0010C9A0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasRequestToken)
			{
				num ^= this.RequestToken.GetHashCode();
			}
			if (this.HasPayloadType)
			{
				num ^= this.PayloadType.GetHashCode();
			}
			if (this.HasPayload)
			{
				num ^= this.Payload.GetHashCode();
			}
			return num;
		}

		// Token: 0x06005850 RID: 22608 RVA: 0x0010E7FC File Offset: 0x0010C9FC
		public override bool Equals(object obj)
		{
			ChallengeExternalRequest challengeExternalRequest = obj as ChallengeExternalRequest;
			return challengeExternalRequest != null && this.HasRequestToken == challengeExternalRequest.HasRequestToken && (!this.HasRequestToken || this.RequestToken.Equals(challengeExternalRequest.RequestToken)) && this.HasPayloadType == challengeExternalRequest.HasPayloadType && (!this.HasPayloadType || this.PayloadType.Equals(challengeExternalRequest.PayloadType)) && this.HasPayload == challengeExternalRequest.HasPayload && (!this.HasPayload || this.Payload.Equals(challengeExternalRequest.Payload));
		}

		// Token: 0x170010A4 RID: 4260
		// (get) Token: 0x06005851 RID: 22609 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005852 RID: 22610 RVA: 0x0010E897 File Offset: 0x0010CA97
		public static ChallengeExternalRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ChallengeExternalRequest>(bs, 0, -1);
		}

		// Token: 0x06005853 RID: 22611 RVA: 0x0010E8A1 File Offset: 0x0010CAA1
		public void Deserialize(Stream stream)
		{
			ChallengeExternalRequest.Deserialize(stream, this);
		}

		// Token: 0x06005854 RID: 22612 RVA: 0x0010E8AB File Offset: 0x0010CAAB
		public static ChallengeExternalRequest Deserialize(Stream stream, ChallengeExternalRequest instance)
		{
			return ChallengeExternalRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005855 RID: 22613 RVA: 0x0010E8B8 File Offset: 0x0010CAB8
		public static ChallengeExternalRequest DeserializeLengthDelimited(Stream stream)
		{
			ChallengeExternalRequest challengeExternalRequest = new ChallengeExternalRequest();
			ChallengeExternalRequest.DeserializeLengthDelimited(stream, challengeExternalRequest);
			return challengeExternalRequest;
		}

		// Token: 0x06005856 RID: 22614 RVA: 0x0010E8D4 File Offset: 0x0010CAD4
		public static ChallengeExternalRequest DeserializeLengthDelimited(Stream stream, ChallengeExternalRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ChallengeExternalRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06005857 RID: 22615 RVA: 0x0010E8FC File Offset: 0x0010CAFC
		public static ChallengeExternalRequest Deserialize(Stream stream, ChallengeExternalRequest instance, long limit)
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
							instance.Payload = ProtocolParser.ReadBytes(stream);
						}
					}
					else
					{
						instance.PayloadType = ProtocolParser.ReadString(stream);
					}
				}
				else
				{
					instance.RequestToken = ProtocolParser.ReadString(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06005858 RID: 22616 RVA: 0x0010E9AA File Offset: 0x0010CBAA
		public void Serialize(Stream stream)
		{
			ChallengeExternalRequest.Serialize(stream, this);
		}

		// Token: 0x06005859 RID: 22617 RVA: 0x0010E9B4 File Offset: 0x0010CBB4
		public static void Serialize(Stream stream, ChallengeExternalRequest instance)
		{
			if (instance.HasRequestToken)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.RequestToken));
			}
			if (instance.HasPayloadType)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.PayloadType));
			}
			if (instance.HasPayload)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, instance.Payload);
			}
		}

		// Token: 0x0600585A RID: 22618 RVA: 0x0010EA2C File Offset: 0x0010CC2C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasRequestToken)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.RequestToken);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasPayloadType)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.PayloadType);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (this.HasPayload)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.Payload.Length) + (uint)this.Payload.Length;
			}
			return num;
		}

		// Token: 0x04001BAB RID: 7083
		public bool HasRequestToken;

		// Token: 0x04001BAC RID: 7084
		private string _RequestToken;

		// Token: 0x04001BAD RID: 7085
		public bool HasPayloadType;

		// Token: 0x04001BAE RID: 7086
		private string _PayloadType;

		// Token: 0x04001BAF RID: 7087
		public bool HasPayload;

		// Token: 0x04001BB0 RID: 7088
		private byte[] _Payload;
	}
}
