using System;
using System.IO;

namespace bnet.protocol.friends.v2.client
{
	// Token: 0x02000409 RID: 1033
	public class RemoveFriendRequest : IProtoBuf
	{
		// Token: 0x17000CA5 RID: 3237
		// (get) Token: 0x06004498 RID: 17560 RVA: 0x000D7DEA File Offset: 0x000D5FEA
		// (set) Token: 0x06004499 RID: 17561 RVA: 0x000D7DF2 File Offset: 0x000D5FF2
		public ulong TargetAccountId
		{
			get
			{
				return this._TargetAccountId;
			}
			set
			{
				this._TargetAccountId = value;
				this.HasTargetAccountId = true;
			}
		}

		// Token: 0x0600449A RID: 17562 RVA: 0x000D7E02 File Offset: 0x000D6002
		public void SetTargetAccountId(ulong val)
		{
			this.TargetAccountId = val;
		}

		// Token: 0x0600449B RID: 17563 RVA: 0x000D7E0C File Offset: 0x000D600C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasTargetAccountId)
			{
				num ^= this.TargetAccountId.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600449C RID: 17564 RVA: 0x000D7E40 File Offset: 0x000D6040
		public override bool Equals(object obj)
		{
			RemoveFriendRequest removeFriendRequest = obj as RemoveFriendRequest;
			return removeFriendRequest != null && this.HasTargetAccountId == removeFriendRequest.HasTargetAccountId && (!this.HasTargetAccountId || this.TargetAccountId.Equals(removeFriendRequest.TargetAccountId));
		}

		// Token: 0x17000CA6 RID: 3238
		// (get) Token: 0x0600449D RID: 17565 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600449E RID: 17566 RVA: 0x000D7E88 File Offset: 0x000D6088
		public static RemoveFriendRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<RemoveFriendRequest>(bs, 0, -1);
		}

		// Token: 0x0600449F RID: 17567 RVA: 0x000D7E92 File Offset: 0x000D6092
		public void Deserialize(Stream stream)
		{
			RemoveFriendRequest.Deserialize(stream, this);
		}

		// Token: 0x060044A0 RID: 17568 RVA: 0x000D7E9C File Offset: 0x000D609C
		public static RemoveFriendRequest Deserialize(Stream stream, RemoveFriendRequest instance)
		{
			return RemoveFriendRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060044A1 RID: 17569 RVA: 0x000D7EA8 File Offset: 0x000D60A8
		public static RemoveFriendRequest DeserializeLengthDelimited(Stream stream)
		{
			RemoveFriendRequest removeFriendRequest = new RemoveFriendRequest();
			RemoveFriendRequest.DeserializeLengthDelimited(stream, removeFriendRequest);
			return removeFriendRequest;
		}

		// Token: 0x060044A2 RID: 17570 RVA: 0x000D7EC4 File Offset: 0x000D60C4
		public static RemoveFriendRequest DeserializeLengthDelimited(Stream stream, RemoveFriendRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return RemoveFriendRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x060044A3 RID: 17571 RVA: 0x000D7EEC File Offset: 0x000D60EC
		public static RemoveFriendRequest Deserialize(Stream stream, RemoveFriendRequest instance, long limit)
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
				else if (num == 16)
				{
					instance.TargetAccountId = ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x060044A4 RID: 17572 RVA: 0x000D7F6C File Offset: 0x000D616C
		public void Serialize(Stream stream)
		{
			RemoveFriendRequest.Serialize(stream, this);
		}

		// Token: 0x060044A5 RID: 17573 RVA: 0x000D7F75 File Offset: 0x000D6175
		public static void Serialize(Stream stream, RemoveFriendRequest instance)
		{
			if (instance.HasTargetAccountId)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, instance.TargetAccountId);
			}
		}

		// Token: 0x060044A6 RID: 17574 RVA: 0x000D7F94 File Offset: 0x000D6194
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasTargetAccountId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.TargetAccountId);
			}
			return num;
		}

		// Token: 0x0400172A RID: 5930
		public bool HasTargetAccountId;

		// Token: 0x0400172B RID: 5931
		private ulong _TargetAccountId;
	}
}
