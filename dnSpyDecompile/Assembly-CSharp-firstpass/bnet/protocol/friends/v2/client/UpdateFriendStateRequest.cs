using System;
using System.IO;

namespace bnet.protocol.friends.v2.client
{
	// Token: 0x0200040A RID: 1034
	public class UpdateFriendStateRequest : IProtoBuf
	{
		// Token: 0x17000CA7 RID: 3239
		// (get) Token: 0x060044A8 RID: 17576 RVA: 0x000D7FBE File Offset: 0x000D61BE
		// (set) Token: 0x060044A9 RID: 17577 RVA: 0x000D7FC6 File Offset: 0x000D61C6
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

		// Token: 0x060044AA RID: 17578 RVA: 0x000D7FD6 File Offset: 0x000D61D6
		public void SetTargetAccountId(ulong val)
		{
			this.TargetAccountId = val;
		}

		// Token: 0x17000CA8 RID: 3240
		// (get) Token: 0x060044AB RID: 17579 RVA: 0x000D7FDF File Offset: 0x000D61DF
		// (set) Token: 0x060044AC RID: 17580 RVA: 0x000D7FE7 File Offset: 0x000D61E7
		public UpdateFriendStateOptions Options
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

		// Token: 0x060044AD RID: 17581 RVA: 0x000D7FFA File Offset: 0x000D61FA
		public void SetOptions(UpdateFriendStateOptions val)
		{
			this.Options = val;
		}

		// Token: 0x060044AE RID: 17582 RVA: 0x000D8004 File Offset: 0x000D6204
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasTargetAccountId)
			{
				num ^= this.TargetAccountId.GetHashCode();
			}
			if (this.HasOptions)
			{
				num ^= this.Options.GetHashCode();
			}
			return num;
		}

		// Token: 0x060044AF RID: 17583 RVA: 0x000D8050 File Offset: 0x000D6250
		public override bool Equals(object obj)
		{
			UpdateFriendStateRequest updateFriendStateRequest = obj as UpdateFriendStateRequest;
			return updateFriendStateRequest != null && this.HasTargetAccountId == updateFriendStateRequest.HasTargetAccountId && (!this.HasTargetAccountId || this.TargetAccountId.Equals(updateFriendStateRequest.TargetAccountId)) && this.HasOptions == updateFriendStateRequest.HasOptions && (!this.HasOptions || this.Options.Equals(updateFriendStateRequest.Options));
		}

		// Token: 0x17000CA9 RID: 3241
		// (get) Token: 0x060044B0 RID: 17584 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060044B1 RID: 17585 RVA: 0x000D80C3 File Offset: 0x000D62C3
		public static UpdateFriendStateRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<UpdateFriendStateRequest>(bs, 0, -1);
		}

		// Token: 0x060044B2 RID: 17586 RVA: 0x000D80CD File Offset: 0x000D62CD
		public void Deserialize(Stream stream)
		{
			UpdateFriendStateRequest.Deserialize(stream, this);
		}

		// Token: 0x060044B3 RID: 17587 RVA: 0x000D80D7 File Offset: 0x000D62D7
		public static UpdateFriendStateRequest Deserialize(Stream stream, UpdateFriendStateRequest instance)
		{
			return UpdateFriendStateRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060044B4 RID: 17588 RVA: 0x000D80E4 File Offset: 0x000D62E4
		public static UpdateFriendStateRequest DeserializeLengthDelimited(Stream stream)
		{
			UpdateFriendStateRequest updateFriendStateRequest = new UpdateFriendStateRequest();
			UpdateFriendStateRequest.DeserializeLengthDelimited(stream, updateFriendStateRequest);
			return updateFriendStateRequest;
		}

		// Token: 0x060044B5 RID: 17589 RVA: 0x000D8100 File Offset: 0x000D6300
		public static UpdateFriendStateRequest DeserializeLengthDelimited(Stream stream, UpdateFriendStateRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return UpdateFriendStateRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x060044B6 RID: 17590 RVA: 0x000D8128 File Offset: 0x000D6328
		public static UpdateFriendStateRequest Deserialize(Stream stream, UpdateFriendStateRequest instance, long limit)
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
					if (num != 26)
					{
						Key key = ProtocolParser.ReadKey((byte)num, stream);
						if (key.Field == 0U)
						{
							throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
						}
						ProtocolParser.SkipKey(stream, key);
					}
					else if (instance.Options == null)
					{
						instance.Options = UpdateFriendStateOptions.DeserializeLengthDelimited(stream);
					}
					else
					{
						UpdateFriendStateOptions.DeserializeLengthDelimited(stream, instance.Options);
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

		// Token: 0x060044B7 RID: 17591 RVA: 0x000D81DA File Offset: 0x000D63DA
		public void Serialize(Stream stream)
		{
			UpdateFriendStateRequest.Serialize(stream, this);
		}

		// Token: 0x060044B8 RID: 17592 RVA: 0x000D81E4 File Offset: 0x000D63E4
		public static void Serialize(Stream stream, UpdateFriendStateRequest instance)
		{
			if (instance.HasTargetAccountId)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, instance.TargetAccountId);
			}
			if (instance.HasOptions)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.Options.GetSerializedSize());
				UpdateFriendStateOptions.Serialize(stream, instance.Options);
			}
		}

		// Token: 0x060044B9 RID: 17593 RVA: 0x000D823C File Offset: 0x000D643C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasTargetAccountId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.TargetAccountId);
			}
			if (this.HasOptions)
			{
				num += 1U;
				uint serializedSize = this.Options.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}

		// Token: 0x0400172C RID: 5932
		public bool HasTargetAccountId;

		// Token: 0x0400172D RID: 5933
		private ulong _TargetAccountId;

		// Token: 0x0400172E RID: 5934
		public bool HasOptions;

		// Token: 0x0400172F RID: 5935
		private UpdateFriendStateOptions _Options;
	}
}
