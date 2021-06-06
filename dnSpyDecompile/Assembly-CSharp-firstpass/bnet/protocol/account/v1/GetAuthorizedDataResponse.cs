using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.account.v1
{
	// Token: 0x0200051B RID: 1307
	public class GetAuthorizedDataResponse : IProtoBuf
	{
		// Token: 0x1700118D RID: 4493
		// (get) Token: 0x06005D28 RID: 23848 RVA: 0x0011AC4F File Offset: 0x00118E4F
		// (set) Token: 0x06005D29 RID: 23849 RVA: 0x0011AC57 File Offset: 0x00118E57
		public List<AuthorizedData> Data
		{
			get
			{
				return this._Data;
			}
			set
			{
				this._Data = value;
			}
		}

		// Token: 0x1700118E RID: 4494
		// (get) Token: 0x06005D2A RID: 23850 RVA: 0x0011AC4F File Offset: 0x00118E4F
		public List<AuthorizedData> DataList
		{
			get
			{
				return this._Data;
			}
		}

		// Token: 0x1700118F RID: 4495
		// (get) Token: 0x06005D2B RID: 23851 RVA: 0x0011AC60 File Offset: 0x00118E60
		public int DataCount
		{
			get
			{
				return this._Data.Count;
			}
		}

		// Token: 0x06005D2C RID: 23852 RVA: 0x0011AC6D File Offset: 0x00118E6D
		public void AddData(AuthorizedData val)
		{
			this._Data.Add(val);
		}

		// Token: 0x06005D2D RID: 23853 RVA: 0x0011AC7B File Offset: 0x00118E7B
		public void ClearData()
		{
			this._Data.Clear();
		}

		// Token: 0x06005D2E RID: 23854 RVA: 0x0011AC88 File Offset: 0x00118E88
		public void SetData(List<AuthorizedData> val)
		{
			this.Data = val;
		}

		// Token: 0x06005D2F RID: 23855 RVA: 0x0011AC94 File Offset: 0x00118E94
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (AuthorizedData authorizedData in this.Data)
			{
				num ^= authorizedData.GetHashCode();
			}
			return num;
		}

		// Token: 0x06005D30 RID: 23856 RVA: 0x0011ACF8 File Offset: 0x00118EF8
		public override bool Equals(object obj)
		{
			GetAuthorizedDataResponse getAuthorizedDataResponse = obj as GetAuthorizedDataResponse;
			if (getAuthorizedDataResponse == null)
			{
				return false;
			}
			if (this.Data.Count != getAuthorizedDataResponse.Data.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Data.Count; i++)
			{
				if (!this.Data[i].Equals(getAuthorizedDataResponse.Data[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17001190 RID: 4496
		// (get) Token: 0x06005D31 RID: 23857 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005D32 RID: 23858 RVA: 0x0011AD63 File Offset: 0x00118F63
		public static GetAuthorizedDataResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetAuthorizedDataResponse>(bs, 0, -1);
		}

		// Token: 0x06005D33 RID: 23859 RVA: 0x0011AD6D File Offset: 0x00118F6D
		public void Deserialize(Stream stream)
		{
			GetAuthorizedDataResponse.Deserialize(stream, this);
		}

		// Token: 0x06005D34 RID: 23860 RVA: 0x0011AD77 File Offset: 0x00118F77
		public static GetAuthorizedDataResponse Deserialize(Stream stream, GetAuthorizedDataResponse instance)
		{
			return GetAuthorizedDataResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005D35 RID: 23861 RVA: 0x0011AD84 File Offset: 0x00118F84
		public static GetAuthorizedDataResponse DeserializeLengthDelimited(Stream stream)
		{
			GetAuthorizedDataResponse getAuthorizedDataResponse = new GetAuthorizedDataResponse();
			GetAuthorizedDataResponse.DeserializeLengthDelimited(stream, getAuthorizedDataResponse);
			return getAuthorizedDataResponse;
		}

		// Token: 0x06005D36 RID: 23862 RVA: 0x0011ADA0 File Offset: 0x00118FA0
		public static GetAuthorizedDataResponse DeserializeLengthDelimited(Stream stream, GetAuthorizedDataResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetAuthorizedDataResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06005D37 RID: 23863 RVA: 0x0011ADC8 File Offset: 0x00118FC8
		public static GetAuthorizedDataResponse Deserialize(Stream stream, GetAuthorizedDataResponse instance, long limit)
		{
			if (instance.Data == null)
			{
				instance.Data = new List<AuthorizedData>();
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
				else if (num == 10)
				{
					instance.Data.Add(AuthorizedData.DeserializeLengthDelimited(stream));
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

		// Token: 0x06005D38 RID: 23864 RVA: 0x0011AE60 File Offset: 0x00119060
		public void Serialize(Stream stream)
		{
			GetAuthorizedDataResponse.Serialize(stream, this);
		}

		// Token: 0x06005D39 RID: 23865 RVA: 0x0011AE6C File Offset: 0x0011906C
		public static void Serialize(Stream stream, GetAuthorizedDataResponse instance)
		{
			if (instance.Data.Count > 0)
			{
				foreach (AuthorizedData authorizedData in instance.Data)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, authorizedData.GetSerializedSize());
					AuthorizedData.Serialize(stream, authorizedData);
				}
			}
		}

		// Token: 0x06005D3A RID: 23866 RVA: 0x0011AEE4 File Offset: 0x001190E4
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.Data.Count > 0)
			{
				foreach (AuthorizedData authorizedData in this.Data)
				{
					num += 1U;
					uint serializedSize = authorizedData.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num;
		}

		// Token: 0x04001CBB RID: 7355
		private List<AuthorizedData> _Data = new List<AuthorizedData>();
	}
}
