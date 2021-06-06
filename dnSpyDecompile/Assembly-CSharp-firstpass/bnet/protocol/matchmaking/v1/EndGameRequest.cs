using System;
using System.IO;

namespace bnet.protocol.matchmaking.v1
{
	// Token: 0x020003BC RID: 956
	public class EndGameRequest : IProtoBuf
	{
		// Token: 0x17000B79 RID: 2937
		// (get) Token: 0x06003E4E RID: 15950 RVA: 0x000C81B3 File Offset: 0x000C63B3
		// (set) Token: 0x06003E4F RID: 15951 RVA: 0x000C81BB File Offset: 0x000C63BB
		public RemoveGameOptions Options
		{
			get
			{
				return this._Options;
			}
			set
			{
				this._Options = value;
				this.HasOptions = (value != null);
			}
		}

		// Token: 0x06003E50 RID: 15952 RVA: 0x000C81CE File Offset: 0x000C63CE
		public void SetOptions(RemoveGameOptions val)
		{
			this.Options = val;
		}

		// Token: 0x06003E51 RID: 15953 RVA: 0x000C81D8 File Offset: 0x000C63D8
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasOptions)
			{
				num ^= this.Options.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003E52 RID: 15954 RVA: 0x000C8208 File Offset: 0x000C6408
		public override bool Equals(object obj)
		{
			EndGameRequest endGameRequest = obj as EndGameRequest;
			return endGameRequest != null && this.HasOptions == endGameRequest.HasOptions && (!this.HasOptions || this.Options.Equals(endGameRequest.Options));
		}

		// Token: 0x17000B7A RID: 2938
		// (get) Token: 0x06003E53 RID: 15955 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003E54 RID: 15956 RVA: 0x000C824D File Offset: 0x000C644D
		public static EndGameRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<EndGameRequest>(bs, 0, -1);
		}

		// Token: 0x06003E55 RID: 15957 RVA: 0x000C8257 File Offset: 0x000C6457
		public void Deserialize(Stream stream)
		{
			EndGameRequest.Deserialize(stream, this);
		}

		// Token: 0x06003E56 RID: 15958 RVA: 0x000C8261 File Offset: 0x000C6461
		public static EndGameRequest Deserialize(Stream stream, EndGameRequest instance)
		{
			return EndGameRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003E57 RID: 15959 RVA: 0x000C826C File Offset: 0x000C646C
		public static EndGameRequest DeserializeLengthDelimited(Stream stream)
		{
			EndGameRequest endGameRequest = new EndGameRequest();
			EndGameRequest.DeserializeLengthDelimited(stream, endGameRequest);
			return endGameRequest;
		}

		// Token: 0x06003E58 RID: 15960 RVA: 0x000C8288 File Offset: 0x000C6488
		public static EndGameRequest DeserializeLengthDelimited(Stream stream, EndGameRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return EndGameRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06003E59 RID: 15961 RVA: 0x000C82B0 File Offset: 0x000C64B0
		public static EndGameRequest Deserialize(Stream stream, EndGameRequest instance, long limit)
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
					if (instance.Options == null)
					{
						instance.Options = RemoveGameOptions.DeserializeLengthDelimited(stream);
					}
					else
					{
						RemoveGameOptions.DeserializeLengthDelimited(stream, instance.Options);
					}
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

		// Token: 0x06003E5A RID: 15962 RVA: 0x000C834A File Offset: 0x000C654A
		public void Serialize(Stream stream)
		{
			EndGameRequest.Serialize(stream, this);
		}

		// Token: 0x06003E5B RID: 15963 RVA: 0x000C8353 File Offset: 0x000C6553
		public static void Serialize(Stream stream, EndGameRequest instance)
		{
			if (instance.HasOptions)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Options.GetSerializedSize());
				RemoveGameOptions.Serialize(stream, instance.Options);
			}
		}

		// Token: 0x06003E5C RID: 15964 RVA: 0x000C8384 File Offset: 0x000C6584
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasOptions)
			{
				num += 1U;
				uint serializedSize = this.Options.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}

		// Token: 0x04001602 RID: 5634
		public bool HasOptions;

		// Token: 0x04001603 RID: 5635
		private RemoveGameOptions _Options;
	}
}
