using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011AF RID: 4527
	public class ContentConnectFailedToConnect : IProtoBuf
	{
		// Token: 0x17000ECE RID: 3790
		// (get) Token: 0x0600C873 RID: 51315 RVA: 0x003C2A48 File Offset: 0x003C0C48
		// (set) Token: 0x0600C874 RID: 51316 RVA: 0x003C2A50 File Offset: 0x003C0C50
		public DeviceInfo DeviceInfo
		{
			get
			{
				return this._DeviceInfo;
			}
			set
			{
				this._DeviceInfo = value;
				this.HasDeviceInfo = (value != null);
			}
		}

		// Token: 0x17000ECF RID: 3791
		// (get) Token: 0x0600C875 RID: 51317 RVA: 0x003C2A63 File Offset: 0x003C0C63
		// (set) Token: 0x0600C876 RID: 51318 RVA: 0x003C2A6B File Offset: 0x003C0C6B
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

		// Token: 0x17000ED0 RID: 3792
		// (get) Token: 0x0600C877 RID: 51319 RVA: 0x003C2A7E File Offset: 0x003C0C7E
		// (set) Token: 0x0600C878 RID: 51320 RVA: 0x003C2A86 File Offset: 0x003C0C86
		public int HttpErrorcode
		{
			get
			{
				return this._HttpErrorcode;
			}
			set
			{
				this._HttpErrorcode = value;
				this.HasHttpErrorcode = true;
			}
		}

		// Token: 0x17000ED1 RID: 3793
		// (get) Token: 0x0600C879 RID: 51321 RVA: 0x003C2A96 File Offset: 0x003C0C96
		// (set) Token: 0x0600C87A RID: 51322 RVA: 0x003C2A9E File Offset: 0x003C0C9E
		public int ServerErrorcode
		{
			get
			{
				return this._ServerErrorcode;
			}
			set
			{
				this._ServerErrorcode = value;
				this.HasServerErrorcode = true;
			}
		}

		// Token: 0x0600C87B RID: 51323 RVA: 0x003C2AB0 File Offset: 0x003C0CB0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasDeviceInfo)
			{
				num ^= this.DeviceInfo.GetHashCode();
			}
			if (this.HasUrl)
			{
				num ^= this.Url.GetHashCode();
			}
			if (this.HasHttpErrorcode)
			{
				num ^= this.HttpErrorcode.GetHashCode();
			}
			if (this.HasServerErrorcode)
			{
				num ^= this.ServerErrorcode.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600C87C RID: 51324 RVA: 0x003C2B28 File Offset: 0x003C0D28
		public override bool Equals(object obj)
		{
			ContentConnectFailedToConnect contentConnectFailedToConnect = obj as ContentConnectFailedToConnect;
			return contentConnectFailedToConnect != null && this.HasDeviceInfo == contentConnectFailedToConnect.HasDeviceInfo && (!this.HasDeviceInfo || this.DeviceInfo.Equals(contentConnectFailedToConnect.DeviceInfo)) && this.HasUrl == contentConnectFailedToConnect.HasUrl && (!this.HasUrl || this.Url.Equals(contentConnectFailedToConnect.Url)) && this.HasHttpErrorcode == contentConnectFailedToConnect.HasHttpErrorcode && (!this.HasHttpErrorcode || this.HttpErrorcode.Equals(contentConnectFailedToConnect.HttpErrorcode)) && this.HasServerErrorcode == contentConnectFailedToConnect.HasServerErrorcode && (!this.HasServerErrorcode || this.ServerErrorcode.Equals(contentConnectFailedToConnect.ServerErrorcode));
		}

		// Token: 0x0600C87D RID: 51325 RVA: 0x003C2BF4 File Offset: 0x003C0DF4
		public void Deserialize(Stream stream)
		{
			ContentConnectFailedToConnect.Deserialize(stream, this);
		}

		// Token: 0x0600C87E RID: 51326 RVA: 0x003C2BFE File Offset: 0x003C0DFE
		public static ContentConnectFailedToConnect Deserialize(Stream stream, ContentConnectFailedToConnect instance)
		{
			return ContentConnectFailedToConnect.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600C87F RID: 51327 RVA: 0x003C2C0C File Offset: 0x003C0E0C
		public static ContentConnectFailedToConnect DeserializeLengthDelimited(Stream stream)
		{
			ContentConnectFailedToConnect contentConnectFailedToConnect = new ContentConnectFailedToConnect();
			ContentConnectFailedToConnect.DeserializeLengthDelimited(stream, contentConnectFailedToConnect);
			return contentConnectFailedToConnect;
		}

		// Token: 0x0600C880 RID: 51328 RVA: 0x003C2C28 File Offset: 0x003C0E28
		public static ContentConnectFailedToConnect DeserializeLengthDelimited(Stream stream, ContentConnectFailedToConnect instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ContentConnectFailedToConnect.Deserialize(stream, instance, num);
		}

		// Token: 0x0600C881 RID: 51329 RVA: 0x003C2C50 File Offset: 0x003C0E50
		public static ContentConnectFailedToConnect Deserialize(Stream stream, ContentConnectFailedToConnect instance, long limit)
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
				else
				{
					if (num <= 18)
					{
						if (num != 10)
						{
							if (num == 18)
							{
								instance.Url = ProtocolParser.ReadString(stream);
								continue;
							}
						}
						else
						{
							if (instance.DeviceInfo == null)
							{
								instance.DeviceInfo = DeviceInfo.DeserializeLengthDelimited(stream);
								continue;
							}
							DeviceInfo.DeserializeLengthDelimited(stream, instance.DeviceInfo);
							continue;
						}
					}
					else
					{
						if (num == 24)
						{
							instance.HttpErrorcode = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 32)
						{
							instance.ServerErrorcode = (int)ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x0600C882 RID: 51330 RVA: 0x003C2D3D File Offset: 0x003C0F3D
		public void Serialize(Stream stream)
		{
			ContentConnectFailedToConnect.Serialize(stream, this);
		}

		// Token: 0x0600C883 RID: 51331 RVA: 0x003C2D48 File Offset: 0x003C0F48
		public static void Serialize(Stream stream, ContentConnectFailedToConnect instance)
		{
			if (instance.HasDeviceInfo)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.DeviceInfo.GetSerializedSize());
				DeviceInfo.Serialize(stream, instance.DeviceInfo);
			}
			if (instance.HasUrl)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Url));
			}
			if (instance.HasHttpErrorcode)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.HttpErrorcode));
			}
			if (instance.HasServerErrorcode)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.ServerErrorcode));
			}
		}

		// Token: 0x0600C884 RID: 51332 RVA: 0x003C2DE4 File Offset: 0x003C0FE4
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasDeviceInfo)
			{
				num += 1U;
				uint serializedSize = this.DeviceInfo.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasUrl)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Url);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasHttpErrorcode)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.HttpErrorcode));
			}
			if (this.HasServerErrorcode)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.ServerErrorcode));
			}
			return num;
		}

		// Token: 0x04009EE7 RID: 40679
		public bool HasDeviceInfo;

		// Token: 0x04009EE8 RID: 40680
		private DeviceInfo _DeviceInfo;

		// Token: 0x04009EE9 RID: 40681
		public bool HasUrl;

		// Token: 0x04009EEA RID: 40682
		private string _Url;

		// Token: 0x04009EEB RID: 40683
		public bool HasHttpErrorcode;

		// Token: 0x04009EEC RID: 40684
		private int _HttpErrorcode;

		// Token: 0x04009EED RID: 40685
		public bool HasServerErrorcode;

		// Token: 0x04009EEE RID: 40686
		private int _ServerErrorcode;
	}
}
