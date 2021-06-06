using System;
using System.IO;
using System.Text;

namespace bnet.protocol.sns.v1
{
	// Token: 0x02000301 RID: 769
	public class GetGoogleSettingsResponse : IProtoBuf
	{
		// Token: 0x17000840 RID: 2112
		// (get) Token: 0x06002E39 RID: 11833 RVA: 0x0009D968 File Offset: 0x0009BB68
		// (set) Token: 0x06002E3A RID: 11834 RVA: 0x0009D970 File Offset: 0x0009BB70
		public string ClientId
		{
			get
			{
				return this._ClientId;
			}
			set
			{
				this._ClientId = value;
				this.HasClientId = (value != null);
			}
		}

		// Token: 0x06002E3B RID: 11835 RVA: 0x0009D983 File Offset: 0x0009BB83
		public void SetClientId(string val)
		{
			this.ClientId = val;
		}

		// Token: 0x06002E3C RID: 11836 RVA: 0x0009D98C File Offset: 0x0009BB8C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasClientId)
			{
				num ^= this.ClientId.GetHashCode();
			}
			return num;
		}

		// Token: 0x06002E3D RID: 11837 RVA: 0x0009D9BC File Offset: 0x0009BBBC
		public override bool Equals(object obj)
		{
			GetGoogleSettingsResponse getGoogleSettingsResponse = obj as GetGoogleSettingsResponse;
			return getGoogleSettingsResponse != null && this.HasClientId == getGoogleSettingsResponse.HasClientId && (!this.HasClientId || this.ClientId.Equals(getGoogleSettingsResponse.ClientId));
		}

		// Token: 0x17000841 RID: 2113
		// (get) Token: 0x06002E3E RID: 11838 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002E3F RID: 11839 RVA: 0x0009DA01 File Offset: 0x0009BC01
		public static GetGoogleSettingsResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetGoogleSettingsResponse>(bs, 0, -1);
		}

		// Token: 0x06002E40 RID: 11840 RVA: 0x0009DA0B File Offset: 0x0009BC0B
		public void Deserialize(Stream stream)
		{
			GetGoogleSettingsResponse.Deserialize(stream, this);
		}

		// Token: 0x06002E41 RID: 11841 RVA: 0x0009DA15 File Offset: 0x0009BC15
		public static GetGoogleSettingsResponse Deserialize(Stream stream, GetGoogleSettingsResponse instance)
		{
			return GetGoogleSettingsResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06002E42 RID: 11842 RVA: 0x0009DA20 File Offset: 0x0009BC20
		public static GetGoogleSettingsResponse DeserializeLengthDelimited(Stream stream)
		{
			GetGoogleSettingsResponse getGoogleSettingsResponse = new GetGoogleSettingsResponse();
			GetGoogleSettingsResponse.DeserializeLengthDelimited(stream, getGoogleSettingsResponse);
			return getGoogleSettingsResponse;
		}

		// Token: 0x06002E43 RID: 11843 RVA: 0x0009DA3C File Offset: 0x0009BC3C
		public static GetGoogleSettingsResponse DeserializeLengthDelimited(Stream stream, GetGoogleSettingsResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetGoogleSettingsResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06002E44 RID: 11844 RVA: 0x0009DA64 File Offset: 0x0009BC64
		public static GetGoogleSettingsResponse Deserialize(Stream stream, GetGoogleSettingsResponse instance, long limit)
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
				else if (num == 10)
				{
					instance.ClientId = ProtocolParser.ReadString(stream);
				}
				else
				{
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

		// Token: 0x06002E45 RID: 11845 RVA: 0x0009DAE4 File Offset: 0x0009BCE4
		public void Serialize(Stream stream)
		{
			GetGoogleSettingsResponse.Serialize(stream, this);
		}

		// Token: 0x06002E46 RID: 11846 RVA: 0x0009DAED File Offset: 0x0009BCED
		public static void Serialize(Stream stream, GetGoogleSettingsResponse instance)
		{
			if (instance.HasClientId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ClientId));
			}
		}

		// Token: 0x06002E47 RID: 11847 RVA: 0x0009DB18 File Offset: 0x0009BD18
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasClientId)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.ClientId);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num;
		}

		// Token: 0x040012BB RID: 4795
		public bool HasClientId;

		// Token: 0x040012BC RID: 4796
		private string _ClientId;
	}
}
