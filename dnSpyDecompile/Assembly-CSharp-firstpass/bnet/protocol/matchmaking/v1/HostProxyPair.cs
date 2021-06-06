using System;
using System.IO;

namespace bnet.protocol.matchmaking.v1
{
	// Token: 0x020003D2 RID: 978
	public class HostProxyPair : IProtoBuf
	{
		// Token: 0x17000BCD RID: 3021
		// (get) Token: 0x0600401C RID: 16412 RVA: 0x000CC959 File Offset: 0x000CAB59
		// (set) Token: 0x0600401D RID: 16413 RVA: 0x000CC961 File Offset: 0x000CAB61
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

		// Token: 0x0600401E RID: 16414 RVA: 0x000CC974 File Offset: 0x000CAB74
		public void SetHost(ProcessId val)
		{
			this.Host = val;
		}

		// Token: 0x17000BCE RID: 3022
		// (get) Token: 0x0600401F RID: 16415 RVA: 0x000CC97D File Offset: 0x000CAB7D
		// (set) Token: 0x06004020 RID: 16416 RVA: 0x000CC985 File Offset: 0x000CAB85
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

		// Token: 0x06004021 RID: 16417 RVA: 0x000CC998 File Offset: 0x000CAB98
		public void SetProxy(ProcessId val)
		{
			this.Proxy = val;
		}

		// Token: 0x06004022 RID: 16418 RVA: 0x000CC9A4 File Offset: 0x000CABA4
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

		// Token: 0x06004023 RID: 16419 RVA: 0x000CC9EC File Offset: 0x000CABEC
		public override bool Equals(object obj)
		{
			HostProxyPair hostProxyPair = obj as HostProxyPair;
			return hostProxyPair != null && this.HasHost == hostProxyPair.HasHost && (!this.HasHost || this.Host.Equals(hostProxyPair.Host)) && this.HasProxy == hostProxyPair.HasProxy && (!this.HasProxy || this.Proxy.Equals(hostProxyPair.Proxy));
		}

		// Token: 0x17000BCF RID: 3023
		// (get) Token: 0x06004024 RID: 16420 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004025 RID: 16421 RVA: 0x000CCA5C File Offset: 0x000CAC5C
		public static HostProxyPair ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<HostProxyPair>(bs, 0, -1);
		}

		// Token: 0x06004026 RID: 16422 RVA: 0x000CCA66 File Offset: 0x000CAC66
		public void Deserialize(Stream stream)
		{
			HostProxyPair.Deserialize(stream, this);
		}

		// Token: 0x06004027 RID: 16423 RVA: 0x000CCA70 File Offset: 0x000CAC70
		public static HostProxyPair Deserialize(Stream stream, HostProxyPair instance)
		{
			return HostProxyPair.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004028 RID: 16424 RVA: 0x000CCA7C File Offset: 0x000CAC7C
		public static HostProxyPair DeserializeLengthDelimited(Stream stream)
		{
			HostProxyPair hostProxyPair = new HostProxyPair();
			HostProxyPair.DeserializeLengthDelimited(stream, hostProxyPair);
			return hostProxyPair;
		}

		// Token: 0x06004029 RID: 16425 RVA: 0x000CCA98 File Offset: 0x000CAC98
		public static HostProxyPair DeserializeLengthDelimited(Stream stream, HostProxyPair instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return HostProxyPair.Deserialize(stream, instance, num);
		}

		// Token: 0x0600402A RID: 16426 RVA: 0x000CCAC0 File Offset: 0x000CACC0
		public static HostProxyPair Deserialize(Stream stream, HostProxyPair instance, long limit)
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

		// Token: 0x0600402B RID: 16427 RVA: 0x000CCB92 File Offset: 0x000CAD92
		public void Serialize(Stream stream)
		{
			HostProxyPair.Serialize(stream, this);
		}

		// Token: 0x0600402C RID: 16428 RVA: 0x000CCB9C File Offset: 0x000CAD9C
		public static void Serialize(Stream stream, HostProxyPair instance)
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

		// Token: 0x0600402D RID: 16429 RVA: 0x000CCC04 File Offset: 0x000CAE04
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

		// Token: 0x04001665 RID: 5733
		public bool HasHost;

		// Token: 0x04001666 RID: 5734
		private ProcessId _Host;

		// Token: 0x04001667 RID: 5735
		public bool HasProxy;

		// Token: 0x04001668 RID: 5736
		private ProcessId _Proxy;
	}
}
