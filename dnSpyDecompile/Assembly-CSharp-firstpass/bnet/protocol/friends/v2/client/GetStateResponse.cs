using System;
using System.IO;

namespace bnet.protocol.friends.v2.client
{
	// Token: 0x020003FD RID: 1021
	public class GetStateResponse : IProtoBuf
	{
		// Token: 0x17000C84 RID: 3204
		// (get) Token: 0x060043C1 RID: 17345 RVA: 0x000D60FA File Offset: 0x000D42FA
		// (set) Token: 0x060043C2 RID: 17346 RVA: 0x000D6102 File Offset: 0x000D4302
		public FriendsState State
		{
			get
			{
				return this._State;
			}
			set
			{
				this._State = value;
				this.HasState = (value != null);
			}
		}

		// Token: 0x060043C3 RID: 17347 RVA: 0x000D6115 File Offset: 0x000D4315
		public void SetState(FriendsState val)
		{
			this.State = val;
		}

		// Token: 0x060043C4 RID: 17348 RVA: 0x000D6120 File Offset: 0x000D4320
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasState)
			{
				num ^= this.State.GetHashCode();
			}
			return num;
		}

		// Token: 0x060043C5 RID: 17349 RVA: 0x000D6150 File Offset: 0x000D4350
		public override bool Equals(object obj)
		{
			GetStateResponse getStateResponse = obj as GetStateResponse;
			return getStateResponse != null && this.HasState == getStateResponse.HasState && (!this.HasState || this.State.Equals(getStateResponse.State));
		}

		// Token: 0x17000C85 RID: 3205
		// (get) Token: 0x060043C6 RID: 17350 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060043C7 RID: 17351 RVA: 0x000D6195 File Offset: 0x000D4395
		public static GetStateResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetStateResponse>(bs, 0, -1);
		}

		// Token: 0x060043C8 RID: 17352 RVA: 0x000D619F File Offset: 0x000D439F
		public void Deserialize(Stream stream)
		{
			GetStateResponse.Deserialize(stream, this);
		}

		// Token: 0x060043C9 RID: 17353 RVA: 0x000D61A9 File Offset: 0x000D43A9
		public static GetStateResponse Deserialize(Stream stream, GetStateResponse instance)
		{
			return GetStateResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060043CA RID: 17354 RVA: 0x000D61B4 File Offset: 0x000D43B4
		public static GetStateResponse DeserializeLengthDelimited(Stream stream)
		{
			GetStateResponse getStateResponse = new GetStateResponse();
			GetStateResponse.DeserializeLengthDelimited(stream, getStateResponse);
			return getStateResponse;
		}

		// Token: 0x060043CB RID: 17355 RVA: 0x000D61D0 File Offset: 0x000D43D0
		public static GetStateResponse DeserializeLengthDelimited(Stream stream, GetStateResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetStateResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x060043CC RID: 17356 RVA: 0x000D61F8 File Offset: 0x000D43F8
		public static GetStateResponse Deserialize(Stream stream, GetStateResponse instance, long limit)
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
					if (instance.State == null)
					{
						instance.State = FriendsState.DeserializeLengthDelimited(stream);
					}
					else
					{
						FriendsState.DeserializeLengthDelimited(stream, instance.State);
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

		// Token: 0x060043CD RID: 17357 RVA: 0x000D6292 File Offset: 0x000D4492
		public void Serialize(Stream stream)
		{
			GetStateResponse.Serialize(stream, this);
		}

		// Token: 0x060043CE RID: 17358 RVA: 0x000D629B File Offset: 0x000D449B
		public static void Serialize(Stream stream, GetStateResponse instance)
		{
			if (instance.HasState)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.State.GetSerializedSize());
				FriendsState.Serialize(stream, instance.State);
			}
		}

		// Token: 0x060043CF RID: 17359 RVA: 0x000D62CC File Offset: 0x000D44CC
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasState)
			{
				num += 1U;
				uint serializedSize = this.State.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}

		// Token: 0x0400170A RID: 5898
		public bool HasState;

		// Token: 0x0400170B RID: 5899
		private FriendsState _State;
	}
}
