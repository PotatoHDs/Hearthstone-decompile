using System;
using System.IO;

namespace PegasusGame
{
	// Token: 0x020001CF RID: 463
	public class ServerResult : IProtoBuf
	{
		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x06001D8C RID: 7564 RVA: 0x00068113 File Offset: 0x00066313
		// (set) Token: 0x06001D8D RID: 7565 RVA: 0x0006811B File Offset: 0x0006631B
		public int ResultCode { get; set; }

		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x06001D8E RID: 7566 RVA: 0x00068124 File Offset: 0x00066324
		// (set) Token: 0x06001D8F RID: 7567 RVA: 0x0006812C File Offset: 0x0006632C
		public float RetryDelaySeconds
		{
			get
			{
				return this._RetryDelaySeconds;
			}
			set
			{
				this._RetryDelaySeconds = value;
				this.HasRetryDelaySeconds = true;
			}
		}

		// Token: 0x06001D90 RID: 7568 RVA: 0x0006813C File Offset: 0x0006633C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.ResultCode.GetHashCode();
			if (this.HasRetryDelaySeconds)
			{
				num ^= this.RetryDelaySeconds.GetHashCode();
			}
			return num;
		}

		// Token: 0x06001D91 RID: 7569 RVA: 0x00068180 File Offset: 0x00066380
		public override bool Equals(object obj)
		{
			ServerResult serverResult = obj as ServerResult;
			return serverResult != null && this.ResultCode.Equals(serverResult.ResultCode) && this.HasRetryDelaySeconds == serverResult.HasRetryDelaySeconds && (!this.HasRetryDelaySeconds || this.RetryDelaySeconds.Equals(serverResult.RetryDelaySeconds));
		}

		// Token: 0x06001D92 RID: 7570 RVA: 0x000681E0 File Offset: 0x000663E0
		public void Deserialize(Stream stream)
		{
			ServerResult.Deserialize(stream, this);
		}

		// Token: 0x06001D93 RID: 7571 RVA: 0x000681EA File Offset: 0x000663EA
		public static ServerResult Deserialize(Stream stream, ServerResult instance)
		{
			return ServerResult.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001D94 RID: 7572 RVA: 0x000681F8 File Offset: 0x000663F8
		public static ServerResult DeserializeLengthDelimited(Stream stream)
		{
			ServerResult serverResult = new ServerResult();
			ServerResult.DeserializeLengthDelimited(stream, serverResult);
			return serverResult;
		}

		// Token: 0x06001D95 RID: 7573 RVA: 0x00068214 File Offset: 0x00066414
		public static ServerResult DeserializeLengthDelimited(Stream stream, ServerResult instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ServerResult.Deserialize(stream, instance, num);
		}

		// Token: 0x06001D96 RID: 7574 RVA: 0x0006823C File Offset: 0x0006643C
		public static ServerResult Deserialize(Stream stream, ServerResult instance, long limit)
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
				else if (num != 8)
				{
					if (num != 21)
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
						instance.RetryDelaySeconds = binaryReader.ReadSingle();
					}
				}
				else
				{
					instance.ResultCode = (int)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06001D97 RID: 7575 RVA: 0x000682DB File Offset: 0x000664DB
		public void Serialize(Stream stream)
		{
			ServerResult.Serialize(stream, this);
		}

		// Token: 0x06001D98 RID: 7576 RVA: 0x000682E4 File Offset: 0x000664E4
		public static void Serialize(Stream stream, ServerResult instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.ResultCode));
			if (instance.HasRetryDelaySeconds)
			{
				stream.WriteByte(21);
				binaryWriter.Write(instance.RetryDelaySeconds);
			}
		}

		// Token: 0x06001D99 RID: 7577 RVA: 0x00068328 File Offset: 0x00066528
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.ResultCode));
			if (this.HasRetryDelaySeconds)
			{
				num += 1U;
				num += 4U;
			}
			return num + 1U;
		}

		// Token: 0x04000AB7 RID: 2743
		public bool HasRetryDelaySeconds;

		// Token: 0x04000AB8 RID: 2744
		private float _RetryDelaySeconds;

		// Token: 0x02000659 RID: 1625
		public enum Code
		{
			// Token: 0x04002149 RID: 8521
			RESULT_OK,
			// Token: 0x0400214A RID: 8522
			RESULT_RETRY,
			// Token: 0x0400214B RID: 8523
			RESULT_NOT_EXISTS
		}

		// Token: 0x0200065A RID: 1626
		public enum Constants
		{
			// Token: 0x0400214D RID: 8525
			DEFAULT_RETRY_SECONDS = 2
		}

		// Token: 0x0200065B RID: 1627
		public enum PacketID
		{
			// Token: 0x0400214F RID: 8527
			ID = 23
		}
	}
}
