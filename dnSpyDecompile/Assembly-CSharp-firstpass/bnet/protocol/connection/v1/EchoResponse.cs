using System;
using System.IO;

namespace bnet.protocol.connection.v1
{
	// Token: 0x02000443 RID: 1091
	public class EchoResponse : IProtoBuf
	{
		// Token: 0x17000DD5 RID: 3541
		// (get) Token: 0x06004A1E RID: 18974 RVA: 0x000E7644 File Offset: 0x000E5844
		// (set) Token: 0x06004A1F RID: 18975 RVA: 0x000E764C File Offset: 0x000E584C
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

		// Token: 0x06004A20 RID: 18976 RVA: 0x000E765C File Offset: 0x000E585C
		public void SetTime(ulong val)
		{
			this.Time = val;
		}

		// Token: 0x17000DD6 RID: 3542
		// (get) Token: 0x06004A21 RID: 18977 RVA: 0x000E7665 File Offset: 0x000E5865
		// (set) Token: 0x06004A22 RID: 18978 RVA: 0x000E766D File Offset: 0x000E586D
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

		// Token: 0x06004A23 RID: 18979 RVA: 0x000E7680 File Offset: 0x000E5880
		public void SetPayload(byte[] val)
		{
			this.Payload = val;
		}

		// Token: 0x06004A24 RID: 18980 RVA: 0x000E768C File Offset: 0x000E588C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasTime)
			{
				num ^= this.Time.GetHashCode();
			}
			if (this.HasPayload)
			{
				num ^= this.Payload.GetHashCode();
			}
			return num;
		}

		// Token: 0x06004A25 RID: 18981 RVA: 0x000E76D8 File Offset: 0x000E58D8
		public override bool Equals(object obj)
		{
			EchoResponse echoResponse = obj as EchoResponse;
			return echoResponse != null && this.HasTime == echoResponse.HasTime && (!this.HasTime || this.Time.Equals(echoResponse.Time)) && this.HasPayload == echoResponse.HasPayload && (!this.HasPayload || this.Payload.Equals(echoResponse.Payload));
		}

		// Token: 0x17000DD7 RID: 3543
		// (get) Token: 0x06004A26 RID: 18982 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004A27 RID: 18983 RVA: 0x000E774B File Offset: 0x000E594B
		public static EchoResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<EchoResponse>(bs, 0, -1);
		}

		// Token: 0x06004A28 RID: 18984 RVA: 0x000E7755 File Offset: 0x000E5955
		public void Deserialize(Stream stream)
		{
			EchoResponse.Deserialize(stream, this);
		}

		// Token: 0x06004A29 RID: 18985 RVA: 0x000E775F File Offset: 0x000E595F
		public static EchoResponse Deserialize(Stream stream, EchoResponse instance)
		{
			return EchoResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004A2A RID: 18986 RVA: 0x000E776C File Offset: 0x000E596C
		public static EchoResponse DeserializeLengthDelimited(Stream stream)
		{
			EchoResponse echoResponse = new EchoResponse();
			EchoResponse.DeserializeLengthDelimited(stream, echoResponse);
			return echoResponse;
		}

		// Token: 0x06004A2B RID: 18987 RVA: 0x000E7788 File Offset: 0x000E5988
		public static EchoResponse DeserializeLengthDelimited(Stream stream, EchoResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return EchoResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06004A2C RID: 18988 RVA: 0x000E77B0 File Offset: 0x000E59B0
		public static EchoResponse Deserialize(Stream stream, EchoResponse instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
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
				else if (num != 9)
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
					else
					{
						instance.Payload = ProtocolParser.ReadBytes(stream);
					}
				}
				else
				{
					instance.Time = binaryReader.ReadUInt64();
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06004A2D RID: 18989 RVA: 0x000E784F File Offset: 0x000E5A4F
		public void Serialize(Stream stream)
		{
			EchoResponse.Serialize(stream, this);
		}

		// Token: 0x06004A2E RID: 18990 RVA: 0x000E7858 File Offset: 0x000E5A58
		public static void Serialize(Stream stream, EchoResponse instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasTime)
			{
				stream.WriteByte(9);
				binaryWriter.Write(instance.Time);
			}
			if (instance.HasPayload)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, instance.Payload);
			}
		}

		// Token: 0x06004A2F RID: 18991 RVA: 0x000E78A4 File Offset: 0x000E5AA4
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasTime)
			{
				num += 1U;
				num += 8U;
			}
			if (this.HasPayload)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.Payload.Length) + (uint)this.Payload.Length;
			}
			return num;
		}

		// Token: 0x04001858 RID: 6232
		public bool HasTime;

		// Token: 0x04001859 RID: 6233
		private ulong _Time;

		// Token: 0x0400185A RID: 6234
		public bool HasPayload;

		// Token: 0x0400185B RID: 6235
		private byte[] _Payload;
	}
}
