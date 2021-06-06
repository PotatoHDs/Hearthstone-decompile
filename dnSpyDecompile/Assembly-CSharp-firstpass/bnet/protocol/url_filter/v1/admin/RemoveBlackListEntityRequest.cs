using System;
using System.IO;
using System.Text;

namespace bnet.protocol.url_filter.v1.admin
{
	// Token: 0x020002D5 RID: 725
	public class RemoveBlackListEntityRequest : IProtoBuf
	{
		// Token: 0x17000793 RID: 1939
		// (get) Token: 0x06002A92 RID: 10898 RVA: 0x0009408C File Offset: 0x0009228C
		// (set) Token: 0x06002A93 RID: 10899 RVA: 0x00094094 File Offset: 0x00092294
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

		// Token: 0x06002A94 RID: 10900 RVA: 0x000940A7 File Offset: 0x000922A7
		public void SetUrl(string val)
		{
			this.Url = val;
		}

		// Token: 0x06002A95 RID: 10901 RVA: 0x000940B0 File Offset: 0x000922B0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasUrl)
			{
				num ^= this.Url.GetHashCode();
			}
			return num;
		}

		// Token: 0x06002A96 RID: 10902 RVA: 0x000940E0 File Offset: 0x000922E0
		public override bool Equals(object obj)
		{
			RemoveBlackListEntityRequest removeBlackListEntityRequest = obj as RemoveBlackListEntityRequest;
			return removeBlackListEntityRequest != null && this.HasUrl == removeBlackListEntityRequest.HasUrl && (!this.HasUrl || this.Url.Equals(removeBlackListEntityRequest.Url));
		}

		// Token: 0x17000794 RID: 1940
		// (get) Token: 0x06002A97 RID: 10903 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002A98 RID: 10904 RVA: 0x00094125 File Offset: 0x00092325
		public static RemoveBlackListEntityRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<RemoveBlackListEntityRequest>(bs, 0, -1);
		}

		// Token: 0x06002A99 RID: 10905 RVA: 0x0009412F File Offset: 0x0009232F
		public void Deserialize(Stream stream)
		{
			RemoveBlackListEntityRequest.Deserialize(stream, this);
		}

		// Token: 0x06002A9A RID: 10906 RVA: 0x00094139 File Offset: 0x00092339
		public static RemoveBlackListEntityRequest Deserialize(Stream stream, RemoveBlackListEntityRequest instance)
		{
			return RemoveBlackListEntityRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06002A9B RID: 10907 RVA: 0x00094144 File Offset: 0x00092344
		public static RemoveBlackListEntityRequest DeserializeLengthDelimited(Stream stream)
		{
			RemoveBlackListEntityRequest removeBlackListEntityRequest = new RemoveBlackListEntityRequest();
			RemoveBlackListEntityRequest.DeserializeLengthDelimited(stream, removeBlackListEntityRequest);
			return removeBlackListEntityRequest;
		}

		// Token: 0x06002A9C RID: 10908 RVA: 0x00094160 File Offset: 0x00092360
		public static RemoveBlackListEntityRequest DeserializeLengthDelimited(Stream stream, RemoveBlackListEntityRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return RemoveBlackListEntityRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06002A9D RID: 10909 RVA: 0x00094188 File Offset: 0x00092388
		public static RemoveBlackListEntityRequest Deserialize(Stream stream, RemoveBlackListEntityRequest instance, long limit)
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

		// Token: 0x06002A9E RID: 10910 RVA: 0x00094208 File Offset: 0x00092408
		public void Serialize(Stream stream)
		{
			RemoveBlackListEntityRequest.Serialize(stream, this);
		}

		// Token: 0x06002A9F RID: 10911 RVA: 0x00094211 File Offset: 0x00092411
		public static void Serialize(Stream stream, RemoveBlackListEntityRequest instance)
		{
			if (instance.HasUrl)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Url));
			}
		}

		// Token: 0x06002AA0 RID: 10912 RVA: 0x0009423C File Offset: 0x0009243C
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

		// Token: 0x040011FC RID: 4604
		public bool HasUrl;

		// Token: 0x040011FD RID: 4605
		private string _Url;
	}
}
