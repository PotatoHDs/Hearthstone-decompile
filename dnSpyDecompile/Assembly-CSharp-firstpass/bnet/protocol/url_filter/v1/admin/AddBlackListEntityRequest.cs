using System;
using System.IO;
using System.Text;

namespace bnet.protocol.url_filter.v1.admin
{
	// Token: 0x020002D4 RID: 724
	public class AddBlackListEntityRequest : IProtoBuf
	{
		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x06002A82 RID: 10882 RVA: 0x00093EA4 File Offset: 0x000920A4
		// (set) Token: 0x06002A83 RID: 10883 RVA: 0x00093EAC File Offset: 0x000920AC
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

		// Token: 0x06002A84 RID: 10884 RVA: 0x00093EBF File Offset: 0x000920BF
		public void SetUrl(string val)
		{
			this.Url = val;
		}

		// Token: 0x06002A85 RID: 10885 RVA: 0x00093EC8 File Offset: 0x000920C8
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasUrl)
			{
				num ^= this.Url.GetHashCode();
			}
			return num;
		}

		// Token: 0x06002A86 RID: 10886 RVA: 0x00093EF8 File Offset: 0x000920F8
		public override bool Equals(object obj)
		{
			AddBlackListEntityRequest addBlackListEntityRequest = obj as AddBlackListEntityRequest;
			return addBlackListEntityRequest != null && this.HasUrl == addBlackListEntityRequest.HasUrl && (!this.HasUrl || this.Url.Equals(addBlackListEntityRequest.Url));
		}

		// Token: 0x17000792 RID: 1938
		// (get) Token: 0x06002A87 RID: 10887 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002A88 RID: 10888 RVA: 0x00093F3D File Offset: 0x0009213D
		public static AddBlackListEntityRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<AddBlackListEntityRequest>(bs, 0, -1);
		}

		// Token: 0x06002A89 RID: 10889 RVA: 0x00093F47 File Offset: 0x00092147
		public void Deserialize(Stream stream)
		{
			AddBlackListEntityRequest.Deserialize(stream, this);
		}

		// Token: 0x06002A8A RID: 10890 RVA: 0x00093F51 File Offset: 0x00092151
		public static AddBlackListEntityRequest Deserialize(Stream stream, AddBlackListEntityRequest instance)
		{
			return AddBlackListEntityRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06002A8B RID: 10891 RVA: 0x00093F5C File Offset: 0x0009215C
		public static AddBlackListEntityRequest DeserializeLengthDelimited(Stream stream)
		{
			AddBlackListEntityRequest addBlackListEntityRequest = new AddBlackListEntityRequest();
			AddBlackListEntityRequest.DeserializeLengthDelimited(stream, addBlackListEntityRequest);
			return addBlackListEntityRequest;
		}

		// Token: 0x06002A8C RID: 10892 RVA: 0x00093F78 File Offset: 0x00092178
		public static AddBlackListEntityRequest DeserializeLengthDelimited(Stream stream, AddBlackListEntityRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AddBlackListEntityRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06002A8D RID: 10893 RVA: 0x00093FA0 File Offset: 0x000921A0
		public static AddBlackListEntityRequest Deserialize(Stream stream, AddBlackListEntityRequest instance, long limit)
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
					instance.Url = ProtocolParser.ReadString(stream);
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

		// Token: 0x06002A8E RID: 10894 RVA: 0x00094020 File Offset: 0x00092220
		public void Serialize(Stream stream)
		{
			AddBlackListEntityRequest.Serialize(stream, this);
		}

		// Token: 0x06002A8F RID: 10895 RVA: 0x00094029 File Offset: 0x00092229
		public static void Serialize(Stream stream, AddBlackListEntityRequest instance)
		{
			if (instance.HasUrl)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Url));
			}
		}

		// Token: 0x06002A90 RID: 10896 RVA: 0x00094054 File Offset: 0x00092254
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasUrl)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Url);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num;
		}

		// Token: 0x040011FA RID: 4602
		public bool HasUrl;

		// Token: 0x040011FB RID: 4603
		private string _Url;
	}
}
