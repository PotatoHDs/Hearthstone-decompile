using System;
using System.IO;

namespace bnet.protocol.friends.v2.client
{
	// Token: 0x020003FE RID: 1022
	public class GetFriendsRequest : IProtoBuf
	{
		// Token: 0x17000C86 RID: 3206
		// (get) Token: 0x060043D1 RID: 17361 RVA: 0x000D62FF File Offset: 0x000D44FF
		// (set) Token: 0x060043D2 RID: 17362 RVA: 0x000D6307 File Offset: 0x000D4507
		public GetFriendsOptions Options
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

		// Token: 0x060043D3 RID: 17363 RVA: 0x000D631A File Offset: 0x000D451A
		public void SetOptions(GetFriendsOptions val)
		{
			this.Options = val;
		}

		// Token: 0x17000C87 RID: 3207
		// (get) Token: 0x060043D4 RID: 17364 RVA: 0x000D6323 File Offset: 0x000D4523
		// (set) Token: 0x060043D5 RID: 17365 RVA: 0x000D632B File Offset: 0x000D452B
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

		// Token: 0x060043D6 RID: 17366 RVA: 0x000D633B File Offset: 0x000D453B
		public void SetContinuation(ulong val)
		{
			this.Continuation = val;
		}

		// Token: 0x060043D7 RID: 17367 RVA: 0x000D6344 File Offset: 0x000D4544
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasOptions)
			{
				num ^= this.Options.GetHashCode();
			}
			if (this.HasContinuation)
			{
				num ^= this.Continuation.GetHashCode();
			}
			return num;
		}

		// Token: 0x060043D8 RID: 17368 RVA: 0x000D6390 File Offset: 0x000D4590
		public override bool Equals(object obj)
		{
			GetFriendsRequest getFriendsRequest = obj as GetFriendsRequest;
			return getFriendsRequest != null && this.HasOptions == getFriendsRequest.HasOptions && (!this.HasOptions || this.Options.Equals(getFriendsRequest.Options)) && this.HasContinuation == getFriendsRequest.HasContinuation && (!this.HasContinuation || this.Continuation.Equals(getFriendsRequest.Continuation));
		}

		// Token: 0x17000C88 RID: 3208
		// (get) Token: 0x060043D9 RID: 17369 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060043DA RID: 17370 RVA: 0x000D6403 File Offset: 0x000D4603
		public static GetFriendsRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetFriendsRequest>(bs, 0, -1);
		}

		// Token: 0x060043DB RID: 17371 RVA: 0x000D640D File Offset: 0x000D460D
		public void Deserialize(Stream stream)
		{
			GetFriendsRequest.Deserialize(stream, this);
		}

		// Token: 0x060043DC RID: 17372 RVA: 0x000D6417 File Offset: 0x000D4617
		public static GetFriendsRequest Deserialize(Stream stream, GetFriendsRequest instance)
		{
			return GetFriendsRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060043DD RID: 17373 RVA: 0x000D6424 File Offset: 0x000D4624
		public static GetFriendsRequest DeserializeLengthDelimited(Stream stream)
		{
			GetFriendsRequest getFriendsRequest = new GetFriendsRequest();
			GetFriendsRequest.DeserializeLengthDelimited(stream, getFriendsRequest);
			return getFriendsRequest;
		}

		// Token: 0x060043DE RID: 17374 RVA: 0x000D6440 File Offset: 0x000D4640
		public static GetFriendsRequest DeserializeLengthDelimited(Stream stream, GetFriendsRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetFriendsRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x060043DF RID: 17375 RVA: 0x000D6468 File Offset: 0x000D4668
		public static GetFriendsRequest Deserialize(Stream stream, GetFriendsRequest instance, long limit)
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
				else if (num != 18)
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
				else if (instance.Options == null)
				{
					instance.Options = GetFriendsOptions.DeserializeLengthDelimited(stream);
				}
				else
				{
					GetFriendsOptions.DeserializeLengthDelimited(stream, instance.Options);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060043E0 RID: 17376 RVA: 0x000D651A File Offset: 0x000D471A
		public void Serialize(Stream stream)
		{
			GetFriendsRequest.Serialize(stream, this);
		}

		// Token: 0x060043E1 RID: 17377 RVA: 0x000D6524 File Offset: 0x000D4724
		public static void Serialize(Stream stream, GetFriendsRequest instance)
		{
			if (instance.HasOptions)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.Options.GetSerializedSize());
				GetFriendsOptions.Serialize(stream, instance.Options);
			}
			if (instance.HasContinuation)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, instance.Continuation);
			}
		}

		// Token: 0x060043E2 RID: 17378 RVA: 0x000D657C File Offset: 0x000D477C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasOptions)
			{
				num += 1U;
				uint serializedSize = this.Options.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasContinuation)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.Continuation);
			}
			return num;
		}

		// Token: 0x0400170C RID: 5900
		public bool HasOptions;

		// Token: 0x0400170D RID: 5901
		private GetFriendsOptions _Options;

		// Token: 0x0400170E RID: 5902
		public bool HasContinuation;

		// Token: 0x0400170F RID: 5903
		private ulong _Continuation;
	}
}
