using System;
using System.IO;

namespace bnet.protocol.friends.v2.client
{
	// Token: 0x02000402 RID: 1026
	public class ViewFriendsRequest : IProtoBuf
	{
		// Token: 0x17000C92 RID: 3218
		// (get) Token: 0x0600441B RID: 17435 RVA: 0x000D6D34 File Offset: 0x000D4F34
		// (set) Token: 0x0600441C RID: 17436 RVA: 0x000D6D3C File Offset: 0x000D4F3C
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

		// Token: 0x0600441D RID: 17437 RVA: 0x000D6D4C File Offset: 0x000D4F4C
		public void SetTargetAccountId(ulong val)
		{
			this.TargetAccountId = val;
		}

		// Token: 0x17000C93 RID: 3219
		// (get) Token: 0x0600441E RID: 17438 RVA: 0x000D6D55 File Offset: 0x000D4F55
		// (set) Token: 0x0600441F RID: 17439 RVA: 0x000D6D5D File Offset: 0x000D4F5D
		public ulong Continuation
		{
			get
			{
				return this._Continuation;
			}
			set
			{
				this._Continuation = value;
				this.HasContinuation = true;
			}
		}

		// Token: 0x06004420 RID: 17440 RVA: 0x000D6D6D File Offset: 0x000D4F6D
		public void SetContinuation(ulong val)
		{
			this.Continuation = val;
		}

		// Token: 0x06004421 RID: 17441 RVA: 0x000D6D78 File Offset: 0x000D4F78
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasTargetAccountId)
			{
				num ^= this.TargetAccountId.GetHashCode();
			}
			if (this.HasContinuation)
			{
				num ^= this.Continuation.GetHashCode();
			}
			return num;
		}

		// Token: 0x06004422 RID: 17442 RVA: 0x000D6DC4 File Offset: 0x000D4FC4
		public override bool Equals(object obj)
		{
			ViewFriendsRequest viewFriendsRequest = obj as ViewFriendsRequest;
			return viewFriendsRequest != null && this.HasTargetAccountId == viewFriendsRequest.HasTargetAccountId && (!this.HasTargetAccountId || this.TargetAccountId.Equals(viewFriendsRequest.TargetAccountId)) && this.HasContinuation == viewFriendsRequest.HasContinuation && (!this.HasContinuation || this.Continuation.Equals(viewFriendsRequest.Continuation));
		}

		// Token: 0x17000C94 RID: 3220
		// (get) Token: 0x06004423 RID: 17443 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004424 RID: 17444 RVA: 0x000D6E3A File Offset: 0x000D503A
		public static ViewFriendsRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ViewFriendsRequest>(bs, 0, -1);
		}

		// Token: 0x06004425 RID: 17445 RVA: 0x000D6E44 File Offset: 0x000D5044
		public void Deserialize(Stream stream)
		{
			ViewFriendsRequest.Deserialize(stream, this);
		}

		// Token: 0x06004426 RID: 17446 RVA: 0x000D6E4E File Offset: 0x000D504E
		public static ViewFriendsRequest Deserialize(Stream stream, ViewFriendsRequest instance)
		{
			return ViewFriendsRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004427 RID: 17447 RVA: 0x000D6E5C File Offset: 0x000D505C
		public static ViewFriendsRequest DeserializeLengthDelimited(Stream stream)
		{
			ViewFriendsRequest viewFriendsRequest = new ViewFriendsRequest();
			ViewFriendsRequest.DeserializeLengthDelimited(stream, viewFriendsRequest);
			return viewFriendsRequest;
		}

		// Token: 0x06004428 RID: 17448 RVA: 0x000D6E78 File Offset: 0x000D5078
		public static ViewFriendsRequest DeserializeLengthDelimited(Stream stream, ViewFriendsRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ViewFriendsRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06004429 RID: 17449 RVA: 0x000D6EA0 File Offset: 0x000D50A0
		public static ViewFriendsRequest Deserialize(Stream stream, ViewFriendsRequest instance, long limit)
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
				else if (num != 16)
				{
					if (num != 24)
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
						instance.Continuation = ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.TargetAccountId = ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600442A RID: 17450 RVA: 0x000D6F38 File Offset: 0x000D5138
		public void Serialize(Stream stream)
		{
			ViewFriendsRequest.Serialize(stream, this);
		}

		// Token: 0x0600442B RID: 17451 RVA: 0x000D6F41 File Offset: 0x000D5141
		public static void Serialize(Stream stream, ViewFriendsRequest instance)
		{
			if (instance.HasTargetAccountId)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, instance.TargetAccountId);
			}
			if (instance.HasContinuation)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, instance.Continuation);
			}
		}

		// Token: 0x0600442C RID: 17452 RVA: 0x000D6F7C File Offset: 0x000D517C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasTargetAccountId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.TargetAccountId);
			}
			if (this.HasContinuation)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.Continuation);
			}
			return num;
		}

		// Token: 0x04001717 RID: 5911
		public bool HasTargetAccountId;

		// Token: 0x04001718 RID: 5912
		private ulong _TargetAccountId;

		// Token: 0x04001719 RID: 5913
		public bool HasContinuation;

		// Token: 0x0400171A RID: 5914
		private ulong _Continuation;
	}
}
