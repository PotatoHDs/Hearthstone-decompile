using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x0200004D RID: 77
	public class ValidateAchieve : IProtoBuf
	{
		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060004F5 RID: 1269 RVA: 0x00014E6F File Offset: 0x0001306F
		// (set) Token: 0x060004F6 RID: 1270 RVA: 0x00014E77 File Offset: 0x00013077
		public int Achieve { get; set; }

		// Token: 0x060004F7 RID: 1271 RVA: 0x00014E80 File Offset: 0x00013080
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.Achieve.GetHashCode();
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x00014EA8 File Offset: 0x000130A8
		public override bool Equals(object obj)
		{
			ValidateAchieve validateAchieve = obj as ValidateAchieve;
			return validateAchieve != null && this.Achieve.Equals(validateAchieve.Achieve);
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x00014EDA File Offset: 0x000130DA
		public void Deserialize(Stream stream)
		{
			ValidateAchieve.Deserialize(stream, this);
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x00014EE4 File Offset: 0x000130E4
		public static ValidateAchieve Deserialize(Stream stream, ValidateAchieve instance)
		{
			return ValidateAchieve.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x00014EF0 File Offset: 0x000130F0
		public static ValidateAchieve DeserializeLengthDelimited(Stream stream)
		{
			ValidateAchieve validateAchieve = new ValidateAchieve();
			ValidateAchieve.DeserializeLengthDelimited(stream, validateAchieve);
			return validateAchieve;
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x00014F0C File Offset: 0x0001310C
		public static ValidateAchieve DeserializeLengthDelimited(Stream stream, ValidateAchieve instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ValidateAchieve.Deserialize(stream, instance, num);
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x00014F34 File Offset: 0x00013134
		public static ValidateAchieve Deserialize(Stream stream, ValidateAchieve instance, long limit)
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
				else if (num == 8)
				{
					instance.Achieve = (int)ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x060004FE RID: 1278 RVA: 0x00014FB4 File Offset: 0x000131B4
		public void Serialize(Stream stream)
		{
			ValidateAchieve.Serialize(stream, this);
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x00014FBD File Offset: 0x000131BD
		public static void Serialize(Stream stream, ValidateAchieve instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Achieve));
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x00014FD3 File Offset: 0x000131D3
		public uint GetSerializedSize()
		{
			return 0U + ProtocolParser.SizeOfUInt64((ulong)((long)this.Achieve)) + 1U;
		}

		// Token: 0x0200055D RID: 1373
		public enum PacketID
		{
			// Token: 0x04001E55 RID: 7765
			ID = 284,
			// Token: 0x04001E56 RID: 7766
			System = 0
		}
	}
}
