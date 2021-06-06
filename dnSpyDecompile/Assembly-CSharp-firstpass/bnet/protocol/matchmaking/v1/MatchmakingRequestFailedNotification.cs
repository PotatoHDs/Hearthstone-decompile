using System;
using System.IO;

namespace bnet.protocol.matchmaking.v1
{
	// Token: 0x020003CF RID: 975
	public class MatchmakingRequestFailedNotification : IProtoBuf
	{
		// Token: 0x17000BC4 RID: 3012
		// (get) Token: 0x06003FE3 RID: 16355 RVA: 0x000CC157 File Offset: 0x000CA357
		// (set) Token: 0x06003FE4 RID: 16356 RVA: 0x000CC15F File Offset: 0x000CA35F
		public RequestId RequestId
		{
			get
			{
				return this._RequestId;
			}
			set
			{
				this._RequestId = value;
				this.HasRequestId = (value != null);
			}
		}

		// Token: 0x06003FE5 RID: 16357 RVA: 0x000CC172 File Offset: 0x000CA372
		public void SetRequestId(RequestId val)
		{
			this.RequestId = val;
		}

		// Token: 0x17000BC5 RID: 3013
		// (get) Token: 0x06003FE6 RID: 16358 RVA: 0x000CC17B File Offset: 0x000CA37B
		// (set) Token: 0x06003FE7 RID: 16359 RVA: 0x000CC183 File Offset: 0x000CA383
		public uint Result
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

		// Token: 0x06003FE8 RID: 16360 RVA: 0x000CC193 File Offset: 0x000CA393
		public void SetResult(uint val)
		{
			this.Result = val;
		}

		// Token: 0x06003FE9 RID: 16361 RVA: 0x000CC19C File Offset: 0x000CA39C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasRequestId)
			{
				num ^= this.RequestId.GetHashCode();
			}
			if (this.HasResult)
			{
				num ^= this.Result.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003FEA RID: 16362 RVA: 0x000CC1E8 File Offset: 0x000CA3E8
		public override bool Equals(object obj)
		{
			MatchmakingRequestFailedNotification matchmakingRequestFailedNotification = obj as MatchmakingRequestFailedNotification;
			return matchmakingRequestFailedNotification != null && this.HasRequestId == matchmakingRequestFailedNotification.HasRequestId && (!this.HasRequestId || this.RequestId.Equals(matchmakingRequestFailedNotification.RequestId)) && this.HasResult == matchmakingRequestFailedNotification.HasResult && (!this.HasResult || this.Result.Equals(matchmakingRequestFailedNotification.Result));
		}

		// Token: 0x17000BC6 RID: 3014
		// (get) Token: 0x06003FEB RID: 16363 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003FEC RID: 16364 RVA: 0x000CC25B File Offset: 0x000CA45B
		public static MatchmakingRequestFailedNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<MatchmakingRequestFailedNotification>(bs, 0, -1);
		}

		// Token: 0x06003FED RID: 16365 RVA: 0x000CC265 File Offset: 0x000CA465
		public void Deserialize(Stream stream)
		{
			MatchmakingRequestFailedNotification.Deserialize(stream, this);
		}

		// Token: 0x06003FEE RID: 16366 RVA: 0x000CC26F File Offset: 0x000CA46F
		public static MatchmakingRequestFailedNotification Deserialize(Stream stream, MatchmakingRequestFailedNotification instance)
		{
			return MatchmakingRequestFailedNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003FEF RID: 16367 RVA: 0x000CC27C File Offset: 0x000CA47C
		public static MatchmakingRequestFailedNotification DeserializeLengthDelimited(Stream stream)
		{
			MatchmakingRequestFailedNotification matchmakingRequestFailedNotification = new MatchmakingRequestFailedNotification();
			MatchmakingRequestFailedNotification.DeserializeLengthDelimited(stream, matchmakingRequestFailedNotification);
			return matchmakingRequestFailedNotification;
		}

		// Token: 0x06003FF0 RID: 16368 RVA: 0x000CC298 File Offset: 0x000CA498
		public static MatchmakingRequestFailedNotification DeserializeLengthDelimited(Stream stream, MatchmakingRequestFailedNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return MatchmakingRequestFailedNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x06003FF1 RID: 16369 RVA: 0x000CC2C0 File Offset: 0x000CA4C0
		public static MatchmakingRequestFailedNotification Deserialize(Stream stream, MatchmakingRequestFailedNotification instance, long limit)
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
				else if (num != 10)
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
						instance.Result = ProtocolParser.ReadUInt32(stream);
					}
				}
				else if (instance.RequestId == null)
				{
					instance.RequestId = RequestId.DeserializeLengthDelimited(stream);
				}
				else
				{
					RequestId.DeserializeLengthDelimited(stream, instance.RequestId);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06003FF2 RID: 16370 RVA: 0x000CC372 File Offset: 0x000CA572
		public void Serialize(Stream stream)
		{
			MatchmakingRequestFailedNotification.Serialize(stream, this);
		}

		// Token: 0x06003FF3 RID: 16371 RVA: 0x000CC37C File Offset: 0x000CA57C
		public static void Serialize(Stream stream, MatchmakingRequestFailedNotification instance)
		{
			if (instance.HasRequestId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.RequestId.GetSerializedSize());
				RequestId.Serialize(stream, instance.RequestId);
			}
			if (instance.HasResult)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.Result);
			}
		}

		// Token: 0x06003FF4 RID: 16372 RVA: 0x000CC3D4 File Offset: 0x000CA5D4
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasRequestId)
			{
				num += 1U;
				uint serializedSize = this.RequestId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasResult)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.Result);
			}
			return num;
		}

		// Token: 0x04001659 RID: 5721
		public bool HasRequestId;

		// Token: 0x0400165A RID: 5722
		private RequestId _RequestId;

		// Token: 0x0400165B RID: 5723
		public bool HasResult;

		// Token: 0x0400165C RID: 5724
		private uint _Result;
	}
}
