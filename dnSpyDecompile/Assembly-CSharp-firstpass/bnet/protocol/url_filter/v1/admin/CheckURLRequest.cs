using System;
using System.IO;
using System.Text;

namespace bnet.protocol.url_filter.v1.admin
{
	// Token: 0x020002D0 RID: 720
	public class CheckURLRequest : IProtoBuf
	{
		// Token: 0x17000789 RID: 1929
		// (get) Token: 0x06002A42 RID: 10818 RVA: 0x000936F7 File Offset: 0x000918F7
		// (set) Token: 0x06002A43 RID: 10819 RVA: 0x000936FF File Offset: 0x000918FF
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

		// Token: 0x06002A44 RID: 10820 RVA: 0x00093712 File Offset: 0x00091912
		public void SetUrl(string val)
		{
			this.Url = val;
		}

		// Token: 0x06002A45 RID: 10821 RVA: 0x0009371C File Offset: 0x0009191C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasUrl)
			{
				num ^= this.Url.GetHashCode();
			}
			return num;
		}

		// Token: 0x06002A46 RID: 10822 RVA: 0x0009374C File Offset: 0x0009194C
		public override bool Equals(object obj)
		{
			CheckURLRequest checkURLRequest = obj as CheckURLRequest;
			return checkURLRequest != null && this.HasUrl == checkURLRequest.HasUrl && (!this.HasUrl || this.Url.Equals(checkURLRequest.Url));
		}

		// Token: 0x1700078A RID: 1930
		// (get) Token: 0x06002A47 RID: 10823 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002A48 RID: 10824 RVA: 0x00093791 File Offset: 0x00091991
		public static CheckURLRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<CheckURLRequest>(bs, 0, -1);
		}

		// Token: 0x06002A49 RID: 10825 RVA: 0x0009379B File Offset: 0x0009199B
		public void Deserialize(Stream stream)
		{
			CheckURLRequest.Deserialize(stream, this);
		}

		// Token: 0x06002A4A RID: 10826 RVA: 0x000937A5 File Offset: 0x000919A5
		public static CheckURLRequest Deserialize(Stream stream, CheckURLRequest instance)
		{
			return CheckURLRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06002A4B RID: 10827 RVA: 0x000937B0 File Offset: 0x000919B0
		public static CheckURLRequest DeserializeLengthDelimited(Stream stream)
		{
			CheckURLRequest checkURLRequest = new CheckURLRequest();
			CheckURLRequest.DeserializeLengthDelimited(stream, checkURLRequest);
			return checkURLRequest;
		}

		// Token: 0x06002A4C RID: 10828 RVA: 0x000937CC File Offset: 0x000919CC
		public static CheckURLRequest DeserializeLengthDelimited(Stream stream, CheckURLRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return CheckURLRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06002A4D RID: 10829 RVA: 0x000937F4 File Offset: 0x000919F4
		public static CheckURLRequest Deserialize(Stream stream, CheckURLRequest instance, long limit)
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

		// Token: 0x06002A4E RID: 10830 RVA: 0x00093874 File Offset: 0x00091A74
		public void Serialize(Stream stream)
		{
			CheckURLRequest.Serialize(stream, this);
		}

		// Token: 0x06002A4F RID: 10831 RVA: 0x0009387D File Offset: 0x00091A7D
		public static void Serialize(Stream stream, CheckURLRequest instance)
		{
			if (instance.HasUrl)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Url));
			}
		}

		// Token: 0x06002A50 RID: 10832 RVA: 0x000938A8 File Offset: 0x00091AA8
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

		// Token: 0x040011F2 RID: 4594
		public bool HasUrl;

		// Token: 0x040011F3 RID: 4595
		private string _Url;
	}
}
