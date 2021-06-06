using System;
using System.IO;

namespace bnet.protocol.games.v1
{
	// Token: 0x0200039E RID: 926
	public class HostRoute : IProtoBuf
	{
		// Token: 0x17000AE1 RID: 2785
		// (get) Token: 0x06003B9A RID: 15258 RVA: 0x000C09DD File Offset: 0x000BEBDD
		// (set) Token: 0x06003B9B RID: 15259 RVA: 0x000C09E5 File Offset: 0x000BEBE5
		public ProcessId Host
		{
			get
			{
				return this._Host;
			}
			set
			{
				this._Host = value;
				this.HasHost = (value != null);
			}
		}

		// Token: 0x06003B9C RID: 15260 RVA: 0x000C09F8 File Offset: 0x000BEBF8
		public void SetHost(ProcessId val)
		{
			this.Host = val;
		}

		// Token: 0x17000AE2 RID: 2786
		// (get) Token: 0x06003B9D RID: 15261 RVA: 0x000C0A01 File Offset: 0x000BEC01
		// (set) Token: 0x06003B9E RID: 15262 RVA: 0x000C0A09 File Offset: 0x000BEC09
		public ProcessId Proxy
		{
			get
			{
				return this._Proxy;
			}
			set
			{
				this._Proxy = value;
				this.HasProxy = (value != null);
			}
		}

		// Token: 0x06003B9F RID: 15263 RVA: 0x000C0A1C File Offset: 0x000BEC1C
		public void SetProxy(ProcessId val)
		{
			this.Proxy = val;
		}

		// Token: 0x06003BA0 RID: 15264 RVA: 0x000C0A28 File Offset: 0x000BEC28
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasHost)
			{
				num ^= this.Host.GetHashCode();
			}
			if (this.HasProxy)
			{
				num ^= this.Proxy.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003BA1 RID: 15265 RVA: 0x000C0A70 File Offset: 0x000BEC70
		public override bool Equals(object obj)
		{
			HostRoute hostRoute = obj as HostRoute;
			return hostRoute != null && this.HasHost == hostRoute.HasHost && (!this.HasHost || this.Host.Equals(hostRoute.Host)) && this.HasProxy == hostRoute.HasProxy && (!this.HasProxy || this.Proxy.Equals(hostRoute.Proxy));
		}

		// Token: 0x17000AE3 RID: 2787
		// (get) Token: 0x06003BA2 RID: 15266 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003BA3 RID: 15267 RVA: 0x000C0AE0 File Offset: 0x000BECE0
		public static HostRoute ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<HostRoute>(bs, 0, -1);
		}

		// Token: 0x06003BA4 RID: 15268 RVA: 0x000C0AEA File Offset: 0x000BECEA
		public void Deserialize(Stream stream)
		{
			HostRoute.Deserialize(stream, this);
		}

		// Token: 0x06003BA5 RID: 15269 RVA: 0x000C0AF4 File Offset: 0x000BECF4
		public static HostRoute Deserialize(Stream stream, HostRoute instance)
		{
			return HostRoute.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003BA6 RID: 15270 RVA: 0x000C0B00 File Offset: 0x000BED00
		public static HostRoute DeserializeLengthDelimited(Stream stream)
		{
			HostRoute hostRoute = new HostRoute();
			HostRoute.DeserializeLengthDelimited(stream, hostRoute);
			return hostRoute;
		}

		// Token: 0x06003BA7 RID: 15271 RVA: 0x000C0B1C File Offset: 0x000BED1C
		public static HostRoute DeserializeLengthDelimited(Stream stream, HostRoute instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return HostRoute.Deserialize(stream, instance, num);
		}

		// Token: 0x06003BA8 RID: 15272 RVA: 0x000C0B44 File Offset: 0x000BED44
		public static HostRoute Deserialize(Stream stream, HostRoute instance, long limit)
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
						Key key = ProtocolParser.ReadKey((byte)num, stream);
						if (key.Field == 0U)
						{
							throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
						}
						ProtocolParser.SkipKey(stream, key);
					}
					else if (instance.Proxy == null)
					{
						instance.Proxy = ProcessId.DeserializeLengthDelimited(stream);
					}
					else
					{
						ProcessId.DeserializeLengthDelimited(stream, instance.Proxy);
					}
				}
				else if (instance.Host == null)
				{
					instance.Host = ProcessId.DeserializeLengthDelimited(stream);
				}
				else
				{
					ProcessId.DeserializeLengthDelimited(stream, instance.Host);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06003BA9 RID: 15273 RVA: 0x000C0C16 File Offset: 0x000BEE16
		public void Serialize(Stream stream)
		{
			HostRoute.Serialize(stream, this);
		}

		// Token: 0x06003BAA RID: 15274 RVA: 0x000C0C20 File Offset: 0x000BEE20
		public static void Serialize(Stream stream, HostRoute instance)
		{
			if (instance.HasHost)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Host.GetSerializedSize());
				ProcessId.Serialize(stream, instance.Host);
			}
			if (instance.HasProxy)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.Proxy.GetSerializedSize());
				ProcessId.Serialize(stream, instance.Proxy);
			}
		}

		// Token: 0x06003BAB RID: 15275 RVA: 0x000C0C88 File Offset: 0x000BEE88
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasHost)
			{
				num += 1U;
				uint serializedSize = this.Host.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasProxy)
			{
				num += 1U;
				uint serializedSize2 = this.Proxy.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num;
		}

		// Token: 0x04001580 RID: 5504
		public bool HasHost;

		// Token: 0x04001581 RID: 5505
		private ProcessId _Host;

		// Token: 0x04001582 RID: 5506
		public bool HasProxy;

		// Token: 0x04001583 RID: 5507
		private ProcessId _Proxy;
	}
}
