using System;
using System.IO;

namespace bnet.protocol.friends.v2.client
{
	// Token: 0x02000401 RID: 1025
	public class IsFriendResponse : IProtoBuf
	{
		// Token: 0x17000C90 RID: 3216
		// (get) Token: 0x0600440B RID: 17419 RVA: 0x000D6B6A File Offset: 0x000D4D6A
		// (set) Token: 0x0600440C RID: 17420 RVA: 0x000D6B72 File Offset: 0x000D4D72
		public bool Result
		{
			get
			{
				return this._Result;
			}
			set
			{
				this._Result = value;
				this.HasResult = true;
			}
		}

		// Token: 0x0600440D RID: 17421 RVA: 0x000D6B82 File Offset: 0x000D4D82
		public void SetResult(bool val)
		{
			this.Result = val;
		}

		// Token: 0x0600440E RID: 17422 RVA: 0x000D6B8C File Offset: 0x000D4D8C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasResult)
			{
				num ^= this.Result.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600440F RID: 17423 RVA: 0x000D6BC0 File Offset: 0x000D4DC0
		public override bool Equals(object obj)
		{
			IsFriendResponse isFriendResponse = obj as IsFriendResponse;
			return isFriendResponse != null && this.HasResult == isFriendResponse.HasResult && (!this.HasResult || this.Result.Equals(isFriendResponse.Result));
		}

		// Token: 0x17000C91 RID: 3217
		// (get) Token: 0x06004410 RID: 17424 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004411 RID: 17425 RVA: 0x000D6C08 File Offset: 0x000D4E08
		public static IsFriendResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<IsFriendResponse>(bs, 0, -1);
		}

		// Token: 0x06004412 RID: 17426 RVA: 0x000D6C12 File Offset: 0x000D4E12
		public void Deserialize(Stream stream)
		{
			IsFriendResponse.Deserialize(stream, this);
		}

		// Token: 0x06004413 RID: 17427 RVA: 0x000D6C1C File Offset: 0x000D4E1C
		public static IsFriendResponse Deserialize(Stream stream, IsFriendResponse instance)
		{
			return IsFriendResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004414 RID: 17428 RVA: 0x000D6C28 File Offset: 0x000D4E28
		public static IsFriendResponse DeserializeLengthDelimited(Stream stream)
		{
			IsFriendResponse isFriendResponse = new IsFriendResponse();
			IsFriendResponse.DeserializeLengthDelimited(stream, isFriendResponse);
			return isFriendResponse;
		}

		// Token: 0x06004415 RID: 17429 RVA: 0x000D6C44 File Offset: 0x000D4E44
		public static IsFriendResponse DeserializeLengthDelimited(Stream stream, IsFriendResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return IsFriendResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06004416 RID: 17430 RVA: 0x000D6C6C File Offset: 0x000D4E6C
		public static IsFriendResponse Deserialize(Stream stream, IsFriendResponse instance, long limit)
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
					instance.Result = ProtocolParser.ReadBool(stream);
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

		// Token: 0x06004417 RID: 17431 RVA: 0x000D6CEB File Offset: 0x000D4EEB
		public void Serialize(Stream stream)
		{
			IsFriendResponse.Serialize(stream, this);
		}

		// Token: 0x06004418 RID: 17432 RVA: 0x000D6CF4 File Offset: 0x000D4EF4
		public static void Serialize(Stream stream, IsFriendResponse instance)
		{
			if (instance.HasResult)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteBool(stream, instance.Result);
			}
		}

		// Token: 0x06004419 RID: 17433 RVA: 0x000D6D14 File Offset: 0x000D4F14
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasResult)
			{
				num += 1U;
				num += 1U;
			}
			return num;
		}

		// Token: 0x04001715 RID: 5909
		public bool HasResult;

		// Token: 0x04001716 RID: 5910
		private bool _Result;
	}
}
