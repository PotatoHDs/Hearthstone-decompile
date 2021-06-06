using System;
using System.IO;
using System.Text;

namespace bnet.protocol.connection.v1
{
	// Token: 0x02000442 RID: 1090
	public class EchoRequest : IProtoBuf
	{
		// Token: 0x17000DCF RID: 3535
		// (get) Token: 0x06004A02 RID: 18946 RVA: 0x000E7133 File Offset: 0x000E5333
		// (set) Token: 0x06004A03 RID: 18947 RVA: 0x000E713B File Offset: 0x000E533B
		public ulong Time
		{
			get
			{
				return this._Time;
			}
			set
			{
				this._Time = value;
				this.HasTime = true;
			}
		}

		// Token: 0x06004A04 RID: 18948 RVA: 0x000E714B File Offset: 0x000E534B
		public void SetTime(ulong val)
		{
			this.Time = val;
		}

		// Token: 0x17000DD0 RID: 3536
		// (get) Token: 0x06004A05 RID: 18949 RVA: 0x000E7154 File Offset: 0x000E5354
		// (set) Token: 0x06004A06 RID: 18950 RVA: 0x000E715C File Offset: 0x000E535C
		public bool NetworkOnly
		{
			get
			{
				return this._NetworkOnly;
			}
			set
			{
				this._NetworkOnly = value;
				this.HasNetworkOnly = true;
			}
		}

		// Token: 0x06004A07 RID: 18951 RVA: 0x000E716C File Offset: 0x000E536C
		public void SetNetworkOnly(bool val)
		{
			this.NetworkOnly = val;
		}

		// Token: 0x17000DD1 RID: 3537
		// (get) Token: 0x06004A08 RID: 18952 RVA: 0x000E7175 File Offset: 0x000E5375
		// (set) Token: 0x06004A09 RID: 18953 RVA: 0x000E717D File Offset: 0x000E537D
		public byte[] Payload
		{
			get
			{
				return this._Payload;
			}
			set
			{
				this._Payload = value;
				this.HasPayload = (value != null);
			}
		}

		// Token: 0x06004A0A RID: 18954 RVA: 0x000E7190 File Offset: 0x000E5390
		public void SetPayload(byte[] val)
		{
			this.Payload = val;
		}

		// Token: 0x17000DD2 RID: 3538
		// (get) Token: 0x06004A0B RID: 18955 RVA: 0x000E7199 File Offset: 0x000E5399
		// (set) Token: 0x06004A0C RID: 18956 RVA: 0x000E71A1 File Offset: 0x000E53A1
		public ProcessId Forward
		{
			get
			{
				return this._Forward;
			}
			set
			{
				this._Forward = value;
				this.HasForward = (value != null);
			}
		}

		// Token: 0x06004A0D RID: 18957 RVA: 0x000E71B4 File Offset: 0x000E53B4
		public void SetForward(ProcessId val)
		{
			this.Forward = val;
		}

		// Token: 0x17000DD3 RID: 3539
		// (get) Token: 0x06004A0E RID: 18958 RVA: 0x000E71BD File Offset: 0x000E53BD
		// (set) Token: 0x06004A0F RID: 18959 RVA: 0x000E71C5 File Offset: 0x000E53C5
		public string ForwardClientId
		{
			get
			{
				return this._ForwardClientId;
			}
			set
			{
				this._ForwardClientId = value;
				this.HasForwardClientId = (value != null);
			}
		}

		// Token: 0x06004A10 RID: 18960 RVA: 0x000E71D8 File Offset: 0x000E53D8
		public void SetForwardClientId(string val)
		{
			this.ForwardClientId = val;
		}

		// Token: 0x06004A11 RID: 18961 RVA: 0x000E71E4 File Offset: 0x000E53E4
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasTime)
			{
				num ^= this.Time.GetHashCode();
			}
			if (this.HasNetworkOnly)
			{
				num ^= this.NetworkOnly.GetHashCode();
			}
			if (this.HasPayload)
			{
				num ^= this.Payload.GetHashCode();
			}
			if (this.HasForward)
			{
				num ^= this.Forward.GetHashCode();
			}
			if (this.HasForwardClientId)
			{
				num ^= this.ForwardClientId.GetHashCode();
			}
			return num;
		}

		// Token: 0x06004A12 RID: 18962 RVA: 0x000E7274 File Offset: 0x000E5474
		public override bool Equals(object obj)
		{
			EchoRequest echoRequest = obj as EchoRequest;
			return echoRequest != null && this.HasTime == echoRequest.HasTime && (!this.HasTime || this.Time.Equals(echoRequest.Time)) && this.HasNetworkOnly == echoRequest.HasNetworkOnly && (!this.HasNetworkOnly || this.NetworkOnly.Equals(echoRequest.NetworkOnly)) && this.HasPayload == echoRequest.HasPayload && (!this.HasPayload || this.Payload.Equals(echoRequest.Payload)) && this.HasForward == echoRequest.HasForward && (!this.HasForward || this.Forward.Equals(echoRequest.Forward)) && this.HasForwardClientId == echoRequest.HasForwardClientId && (!this.HasForwardClientId || this.ForwardClientId.Equals(echoRequest.ForwardClientId));
		}

		// Token: 0x17000DD4 RID: 3540
		// (get) Token: 0x06004A13 RID: 18963 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004A14 RID: 18964 RVA: 0x000E736B File Offset: 0x000E556B
		public static EchoRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<EchoRequest>(bs, 0, -1);
		}

		// Token: 0x06004A15 RID: 18965 RVA: 0x000E7375 File Offset: 0x000E5575
		public void Deserialize(Stream stream)
		{
			EchoRequest.Deserialize(stream, this);
		}

		// Token: 0x06004A16 RID: 18966 RVA: 0x000E737F File Offset: 0x000E557F
		public static EchoRequest Deserialize(Stream stream, EchoRequest instance)
		{
			return EchoRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004A17 RID: 18967 RVA: 0x000E738C File Offset: 0x000E558C
		public static EchoRequest DeserializeLengthDelimited(Stream stream)
		{
			EchoRequest echoRequest = new EchoRequest();
			EchoRequest.DeserializeLengthDelimited(stream, echoRequest);
			return echoRequest;
		}

		// Token: 0x06004A18 RID: 18968 RVA: 0x000E73A8 File Offset: 0x000E55A8
		public static EchoRequest DeserializeLengthDelimited(Stream stream, EchoRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return EchoRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06004A19 RID: 18969 RVA: 0x000E73D0 File Offset: 0x000E55D0
		public static EchoRequest Deserialize(Stream stream, EchoRequest instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			instance.NetworkOnly = false;
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
					if (num <= 16)
					{
						if (num == 9)
						{
							instance.Time = binaryReader.ReadUInt64();
							continue;
						}
						if (num == 16)
						{
							instance.NetworkOnly = ProtocolParser.ReadBool(stream);
							continue;
						}
					}
					else
					{
						if (num == 26)
						{
							instance.Payload = ProtocolParser.ReadBytes(stream);
							continue;
						}
						if (num != 34)
						{
							if (num == 42)
							{
								instance.ForwardClientId = ProtocolParser.ReadString(stream);
								continue;
							}
						}
						else
						{
							if (instance.Forward == null)
							{
								instance.Forward = ProcessId.DeserializeLengthDelimited(stream);
								continue;
							}
							ProcessId.DeserializeLengthDelimited(stream, instance.Forward);
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

		// Token: 0x06004A1A RID: 18970 RVA: 0x000E74DF File Offset: 0x000E56DF
		public void Serialize(Stream stream)
		{
			EchoRequest.Serialize(stream, this);
		}

		// Token: 0x06004A1B RID: 18971 RVA: 0x000E74E8 File Offset: 0x000E56E8
		public static void Serialize(Stream stream, EchoRequest instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasTime)
			{
				stream.WriteByte(9);
				binaryWriter.Write(instance.Time);
			}
			if (instance.HasNetworkOnly)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteBool(stream, instance.NetworkOnly);
			}
			if (instance.HasPayload)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, instance.Payload);
			}
			if (instance.HasForward)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.Forward.GetSerializedSize());
				ProcessId.Serialize(stream, instance.Forward);
			}
			if (instance.HasForwardClientId)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ForwardClientId));
			}
		}

		// Token: 0x06004A1C RID: 18972 RVA: 0x000E75A4 File Offset: 0x000E57A4
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasTime)
			{
				num += 1U;
				num += 8U;
			}
			if (this.HasNetworkOnly)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasPayload)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.Payload.Length) + (uint)this.Payload.Length;
			}
			if (this.HasForward)
			{
				num += 1U;
				uint serializedSize = this.Forward.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasForwardClientId)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.ForwardClientId);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num;
		}

		// Token: 0x0400184E RID: 6222
		public bool HasTime;

		// Token: 0x0400184F RID: 6223
		private ulong _Time;

		// Token: 0x04001850 RID: 6224
		public bool HasNetworkOnly;

		// Token: 0x04001851 RID: 6225
		private bool _NetworkOnly;

		// Token: 0x04001852 RID: 6226
		public bool HasPayload;

		// Token: 0x04001853 RID: 6227
		private byte[] _Payload;

		// Token: 0x04001854 RID: 6228
		public bool HasForward;

		// Token: 0x04001855 RID: 6229
		private ProcessId _Forward;

		// Token: 0x04001856 RID: 6230
		public bool HasForwardClientId;

		// Token: 0x04001857 RID: 6231
		private string _ForwardClientId;
	}
}
