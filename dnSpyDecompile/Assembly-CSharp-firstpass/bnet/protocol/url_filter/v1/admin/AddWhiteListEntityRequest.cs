using System;
using System.IO;
using System.Text;

namespace bnet.protocol.url_filter.v1.admin
{
	// Token: 0x020002D2 RID: 722
	public class AddWhiteListEntityRequest : IProtoBuf
	{
		// Token: 0x1700078D RID: 1933
		// (get) Token: 0x06002A62 RID: 10850 RVA: 0x00093AD3 File Offset: 0x00091CD3
		// (set) Token: 0x06002A63 RID: 10851 RVA: 0x00093ADB File Offset: 0x00091CDB
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

		// Token: 0x06002A64 RID: 10852 RVA: 0x00093AEE File Offset: 0x00091CEE
		public void SetUrl(string val)
		{
			this.Url = val;
		}

		// Token: 0x06002A65 RID: 10853 RVA: 0x00093AF8 File Offset: 0x00091CF8
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasUrl)
			{
				num ^= this.Url.GetHashCode();
			}
			return num;
		}

		// Token: 0x06002A66 RID: 10854 RVA: 0x00093B28 File Offset: 0x00091D28
		public override bool Equals(object obj)
		{
			AddWhiteListEntityRequest addWhiteListEntityRequest = obj as AddWhiteListEntityRequest;
			return addWhiteListEntityRequest != null && this.HasUrl == addWhiteListEntityRequest.HasUrl && (!this.HasUrl || this.Url.Equals(addWhiteListEntityRequest.Url));
		}

		// Token: 0x1700078E RID: 1934
		// (get) Token: 0x06002A67 RID: 10855 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002A68 RID: 10856 RVA: 0x00093B6D File Offset: 0x00091D6D
		public static AddWhiteListEntityRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<AddWhiteListEntityRequest>(bs, 0, -1);
		}

		// Token: 0x06002A69 RID: 10857 RVA: 0x00093B77 File Offset: 0x00091D77
		public void Deserialize(Stream stream)
		{
			AddWhiteListEntityRequest.Deserialize(stream, this);
		}

		// Token: 0x06002A6A RID: 10858 RVA: 0x00093B81 File Offset: 0x00091D81
		public static AddWhiteListEntityRequest Deserialize(Stream stream, AddWhiteListEntityRequest instance)
		{
			return AddWhiteListEntityRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06002A6B RID: 10859 RVA: 0x00093B8C File Offset: 0x00091D8C
		public static AddWhiteListEntityRequest DeserializeLengthDelimited(Stream stream)
		{
			AddWhiteListEntityRequest addWhiteListEntityRequest = new AddWhiteListEntityRequest();
			AddWhiteListEntityRequest.DeserializeLengthDelimited(stream, addWhiteListEntityRequest);
			return addWhiteListEntityRequest;
		}

		// Token: 0x06002A6C RID: 10860 RVA: 0x00093BA8 File Offset: 0x00091DA8
		public static AddWhiteListEntityRequest DeserializeLengthDelimited(Stream stream, AddWhiteListEntityRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AddWhiteListEntityRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06002A6D RID: 10861 RVA: 0x00093BD0 File Offset: 0x00091DD0
		public static AddWhiteListEntityRequest Deserialize(Stream stream, AddWhiteListEntityRequest instance, long limit)
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

		// Token: 0x06002A6E RID: 10862 RVA: 0x00093C50 File Offset: 0x00091E50
		public void Serialize(Stream stream)
		{
			AddWhiteListEntityRequest.Serialize(stream, this);
		}

		// Token: 0x06002A6F RID: 10863 RVA: 0x00093C59 File Offset: 0x00091E59
		public static void Serialize(Stream stream, AddWhiteListEntityRequest instance)
		{
			if (instance.HasUrl)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Url));
			}
		}

		// Token: 0x06002A70 RID: 10864 RVA: 0x00093C84 File Offset: 0x00091E84
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

		// Token: 0x040011F6 RID: 4598
		public bool HasUrl;

		// Token: 0x040011F7 RID: 4599
		private string _Url;
	}
}
