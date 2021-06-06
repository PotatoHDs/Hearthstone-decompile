using System;
using System.IO;

namespace bnet.protocol.friends.v2.client
{
	// Token: 0x02000400 RID: 1024
	public class IsFriendRequest : IProtoBuf
	{
		// Token: 0x17000C8E RID: 3214
		// (get) Token: 0x060043FB RID: 17403 RVA: 0x000D6997 File Offset: 0x000D4B97
		// (set) Token: 0x060043FC RID: 17404 RVA: 0x000D699F File Offset: 0x000D4B9F
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

		// Token: 0x060043FD RID: 17405 RVA: 0x000D69AF File Offset: 0x000D4BAF
		public void SetTargetAccountId(ulong val)
		{
			this.TargetAccountId = val;
		}

		// Token: 0x060043FE RID: 17406 RVA: 0x000D69B8 File Offset: 0x000D4BB8
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasTargetAccountId)
			{
				num ^= this.TargetAccountId.GetHashCode();
			}
			return num;
		}

		// Token: 0x060043FF RID: 17407 RVA: 0x000D69EC File Offset: 0x000D4BEC
		public override bool Equals(object obj)
		{
			IsFriendRequest isFriendRequest = obj as IsFriendRequest;
			return isFriendRequest != null && this.HasTargetAccountId == isFriendRequest.HasTargetAccountId && (!this.HasTargetAccountId || this.TargetAccountId.Equals(isFriendRequest.TargetAccountId));
		}

		// Token: 0x17000C8F RID: 3215
		// (get) Token: 0x06004400 RID: 17408 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004401 RID: 17409 RVA: 0x000D6A34 File Offset: 0x000D4C34
		public static IsFriendRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<IsFriendRequest>(bs, 0, -1);
		}

		// Token: 0x06004402 RID: 17410 RVA: 0x000D6A3E File Offset: 0x000D4C3E
		public void Deserialize(Stream stream)
		{
			IsFriendRequest.Deserialize(stream, this);
		}

		// Token: 0x06004403 RID: 17411 RVA: 0x000D6A48 File Offset: 0x000D4C48
		public static IsFriendRequest Deserialize(Stream stream, IsFriendRequest instance)
		{
			return IsFriendRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004404 RID: 17412 RVA: 0x000D6A54 File Offset: 0x000D4C54
		public static IsFriendRequest DeserializeLengthDelimited(Stream stream)
		{
			IsFriendRequest isFriendRequest = new IsFriendRequest();
			IsFriendRequest.DeserializeLengthDelimited(stream, isFriendRequest);
			return isFriendRequest;
		}

		// Token: 0x06004405 RID: 17413 RVA: 0x000D6A70 File Offset: 0x000D4C70
		public static IsFriendRequest DeserializeLengthDelimited(Stream stream, IsFriendRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return IsFriendRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06004406 RID: 17414 RVA: 0x000D6A98 File Offset: 0x000D4C98
		public static IsFriendRequest Deserialize(Stream stream, IsFriendRequest instance, long limit)
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

		// Token: 0x06004407 RID: 17415 RVA: 0x000D6B18 File Offset: 0x000D4D18
		public void Serialize(Stream stream)
		{
			IsFriendRequest.Serialize(stream, this);
		}

		// Token: 0x06004408 RID: 17416 RVA: 0x000D6B21 File Offset: 0x000D4D21
		public static void Serialize(Stream stream, IsFriendRequest instance)
		{
			if (instance.HasTargetAccountId)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, instance.TargetAccountId);
			}
		}

		// Token: 0x06004409 RID: 17417 RVA: 0x000D6B40 File Offset: 0x000D4D40
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

		// Token: 0x04001713 RID: 5907
		public bool HasTargetAccountId;

		// Token: 0x04001714 RID: 5908
		private ulong _TargetAccountId;
	}
}
