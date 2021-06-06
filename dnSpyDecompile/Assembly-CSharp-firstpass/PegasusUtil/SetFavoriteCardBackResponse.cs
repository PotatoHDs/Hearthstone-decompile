using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x020000B6 RID: 182
	public class SetFavoriteCardBackResponse : IProtoBuf
	{
		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000C98 RID: 3224 RVA: 0x0002F543 File Offset: 0x0002D743
		// (set) Token: 0x06000C99 RID: 3225 RVA: 0x0002F54B File Offset: 0x0002D74B
		public bool Success { get; set; }

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000C9A RID: 3226 RVA: 0x0002F554 File Offset: 0x0002D754
		// (set) Token: 0x06000C9B RID: 3227 RVA: 0x0002F55C File Offset: 0x0002D75C
		public int CardBack { get; set; }

		// Token: 0x06000C9C RID: 3228 RVA: 0x0002F568 File Offset: 0x0002D768
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.Success.GetHashCode() ^ this.CardBack.GetHashCode();
		}

		// Token: 0x06000C9D RID: 3229 RVA: 0x0002F5A0 File Offset: 0x0002D7A0
		public override bool Equals(object obj)
		{
			SetFavoriteCardBackResponse setFavoriteCardBackResponse = obj as SetFavoriteCardBackResponse;
			return setFavoriteCardBackResponse != null && this.Success.Equals(setFavoriteCardBackResponse.Success) && this.CardBack.Equals(setFavoriteCardBackResponse.CardBack);
		}

		// Token: 0x06000C9E RID: 3230 RVA: 0x0002F5EA File Offset: 0x0002D7EA
		public void Deserialize(Stream stream)
		{
			SetFavoriteCardBackResponse.Deserialize(stream, this);
		}

		// Token: 0x06000C9F RID: 3231 RVA: 0x0002F5F4 File Offset: 0x0002D7F4
		public static SetFavoriteCardBackResponse Deserialize(Stream stream, SetFavoriteCardBackResponse instance)
		{
			return SetFavoriteCardBackResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000CA0 RID: 3232 RVA: 0x0002F600 File Offset: 0x0002D800
		public static SetFavoriteCardBackResponse DeserializeLengthDelimited(Stream stream)
		{
			SetFavoriteCardBackResponse setFavoriteCardBackResponse = new SetFavoriteCardBackResponse();
			SetFavoriteCardBackResponse.DeserializeLengthDelimited(stream, setFavoriteCardBackResponse);
			return setFavoriteCardBackResponse;
		}

		// Token: 0x06000CA1 RID: 3233 RVA: 0x0002F61C File Offset: 0x0002D81C
		public static SetFavoriteCardBackResponse DeserializeLengthDelimited(Stream stream, SetFavoriteCardBackResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SetFavoriteCardBackResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06000CA2 RID: 3234 RVA: 0x0002F644 File Offset: 0x0002D844
		public static SetFavoriteCardBackResponse Deserialize(Stream stream, SetFavoriteCardBackResponse instance, long limit)
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
				else if (num != 8)
				{
					if (num != 16)
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
						instance.CardBack = (int)ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.Success = ProtocolParser.ReadBool(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06000CA3 RID: 3235 RVA: 0x0002F6DC File Offset: 0x0002D8DC
		public void Serialize(Stream stream)
		{
			SetFavoriteCardBackResponse.Serialize(stream, this);
		}

		// Token: 0x06000CA4 RID: 3236 RVA: 0x0002F6E5 File Offset: 0x0002D8E5
		public static void Serialize(Stream stream, SetFavoriteCardBackResponse instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteBool(stream, instance.Success);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.CardBack));
		}

		// Token: 0x06000CA5 RID: 3237 RVA: 0x0002F70F File Offset: 0x0002D90F
		public uint GetSerializedSize()
		{
			return 0U + 1U + ProtocolParser.SizeOfUInt64((ulong)((long)this.CardBack)) + 2U;
		}

		// Token: 0x020005C1 RID: 1473
		public enum PacketID
		{
			// Token: 0x04001F91 RID: 8081
			ID = 292
		}
	}
}
