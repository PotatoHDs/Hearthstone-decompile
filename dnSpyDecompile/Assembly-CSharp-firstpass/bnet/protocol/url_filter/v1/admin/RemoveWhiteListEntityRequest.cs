using System;
using System.IO;
using System.Text;

namespace bnet.protocol.url_filter.v1.admin
{
	// Token: 0x020002D3 RID: 723
	public class RemoveWhiteListEntityRequest : IProtoBuf
	{
		// Token: 0x1700078F RID: 1935
		// (get) Token: 0x06002A72 RID: 10866 RVA: 0x00093CBC File Offset: 0x00091EBC
		// (set) Token: 0x06002A73 RID: 10867 RVA: 0x00093CC4 File Offset: 0x00091EC4
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

		// Token: 0x06002A74 RID: 10868 RVA: 0x00093CD7 File Offset: 0x00091ED7
		public void SetUrl(string val)
		{
			this.Url = val;
		}

		// Token: 0x06002A75 RID: 10869 RVA: 0x00093CE0 File Offset: 0x00091EE0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasUrl)
			{
				num ^= this.Url.GetHashCode();
			}
			return num;
		}

		// Token: 0x06002A76 RID: 10870 RVA: 0x00093D10 File Offset: 0x00091F10
		public override bool Equals(object obj)
		{
			RemoveWhiteListEntityRequest removeWhiteListEntityRequest = obj as RemoveWhiteListEntityRequest;
			return removeWhiteListEntityRequest != null && this.HasUrl == removeWhiteListEntityRequest.HasUrl && (!this.HasUrl || this.Url.Equals(removeWhiteListEntityRequest.Url));
		}

		// Token: 0x17000790 RID: 1936
		// (get) Token: 0x06002A77 RID: 10871 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002A78 RID: 10872 RVA: 0x00093D55 File Offset: 0x00091F55
		public static RemoveWhiteListEntityRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<RemoveWhiteListEntityRequest>(bs, 0, -1);
		}

		// Token: 0x06002A79 RID: 10873 RVA: 0x00093D5F File Offset: 0x00091F5F
		public void Deserialize(Stream stream)
		{
			RemoveWhiteListEntityRequest.Deserialize(stream, this);
		}

		// Token: 0x06002A7A RID: 10874 RVA: 0x00093D69 File Offset: 0x00091F69
		public static RemoveWhiteListEntityRequest Deserialize(Stream stream, RemoveWhiteListEntityRequest instance)
		{
			return RemoveWhiteListEntityRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06002A7B RID: 10875 RVA: 0x00093D74 File Offset: 0x00091F74
		public static RemoveWhiteListEntityRequest DeserializeLengthDelimited(Stream stream)
		{
			RemoveWhiteListEntityRequest removeWhiteListEntityRequest = new RemoveWhiteListEntityRequest();
			RemoveWhiteListEntityRequest.DeserializeLengthDelimited(stream, removeWhiteListEntityRequest);
			return removeWhiteListEntityRequest;
		}

		// Token: 0x06002A7C RID: 10876 RVA: 0x00093D90 File Offset: 0x00091F90
		public static RemoveWhiteListEntityRequest DeserializeLengthDelimited(Stream stream, RemoveWhiteListEntityRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return RemoveWhiteListEntityRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06002A7D RID: 10877 RVA: 0x00093DB8 File Offset: 0x00091FB8
		public static RemoveWhiteListEntityRequest Deserialize(Stream stream, RemoveWhiteListEntityRequest instance, long limit)
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

		// Token: 0x06002A7E RID: 10878 RVA: 0x00093E38 File Offset: 0x00092038
		public void Serialize(Stream stream)
		{
			RemoveWhiteListEntityRequest.Serialize(stream, this);
		}

		// Token: 0x06002A7F RID: 10879 RVA: 0x00093E41 File Offset: 0x00092041
		public static void Serialize(Stream stream, RemoveWhiteListEntityRequest instance)
		{
			if (instance.HasUrl)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Url));
			}
		}

		// Token: 0x06002A80 RID: 10880 RVA: 0x00093E6C File Offset: 0x0009206C
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

		// Token: 0x040011F8 RID: 4600
		public bool HasUrl;

		// Token: 0x040011F9 RID: 4601
		private string _Url;
	}
}
