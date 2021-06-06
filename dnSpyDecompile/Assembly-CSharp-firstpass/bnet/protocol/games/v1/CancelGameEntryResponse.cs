using System;
using System.IO;

namespace bnet.protocol.games.v1
{
	// Token: 0x020003A5 RID: 933
	public class CancelGameEntryResponse : IProtoBuf
	{
		// Token: 0x17000B17 RID: 2839
		// (get) Token: 0x06003C78 RID: 15480 RVA: 0x000C337B File Offset: 0x000C157B
		// (set) Token: 0x06003C79 RID: 15481 RVA: 0x000C3383 File Offset: 0x000C1583
		public bool EntireGameEntryCancelled
		{
			get
			{
				return this._EntireGameEntryCancelled;
			}
			set
			{
				this._EntireGameEntryCancelled = value;
				this.HasEntireGameEntryCancelled = true;
			}
		}

		// Token: 0x06003C7A RID: 15482 RVA: 0x000C3393 File Offset: 0x000C1593
		public void SetEntireGameEntryCancelled(bool val)
		{
			this.EntireGameEntryCancelled = val;
		}

		// Token: 0x06003C7B RID: 15483 RVA: 0x000C339C File Offset: 0x000C159C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasEntireGameEntryCancelled)
			{
				num ^= this.EntireGameEntryCancelled.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003C7C RID: 15484 RVA: 0x000C33D0 File Offset: 0x000C15D0
		public override bool Equals(object obj)
		{
			CancelGameEntryResponse cancelGameEntryResponse = obj as CancelGameEntryResponse;
			return cancelGameEntryResponse != null && this.HasEntireGameEntryCancelled == cancelGameEntryResponse.HasEntireGameEntryCancelled && (!this.HasEntireGameEntryCancelled || this.EntireGameEntryCancelled.Equals(cancelGameEntryResponse.EntireGameEntryCancelled));
		}

		// Token: 0x17000B18 RID: 2840
		// (get) Token: 0x06003C7D RID: 15485 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003C7E RID: 15486 RVA: 0x000C3418 File Offset: 0x000C1618
		public static CancelGameEntryResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<CancelGameEntryResponse>(bs, 0, -1);
		}

		// Token: 0x06003C7F RID: 15487 RVA: 0x000C3422 File Offset: 0x000C1622
		public void Deserialize(Stream stream)
		{
			CancelGameEntryResponse.Deserialize(stream, this);
		}

		// Token: 0x06003C80 RID: 15488 RVA: 0x000C342C File Offset: 0x000C162C
		public static CancelGameEntryResponse Deserialize(Stream stream, CancelGameEntryResponse instance)
		{
			return CancelGameEntryResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003C81 RID: 15489 RVA: 0x000C3438 File Offset: 0x000C1638
		public static CancelGameEntryResponse DeserializeLengthDelimited(Stream stream)
		{
			CancelGameEntryResponse cancelGameEntryResponse = new CancelGameEntryResponse();
			CancelGameEntryResponse.DeserializeLengthDelimited(stream, cancelGameEntryResponse);
			return cancelGameEntryResponse;
		}

		// Token: 0x06003C82 RID: 15490 RVA: 0x000C3454 File Offset: 0x000C1654
		public static CancelGameEntryResponse DeserializeLengthDelimited(Stream stream, CancelGameEntryResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return CancelGameEntryResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06003C83 RID: 15491 RVA: 0x000C347C File Offset: 0x000C167C
		public static CancelGameEntryResponse Deserialize(Stream stream, CancelGameEntryResponse instance, long limit)
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
					instance.EntireGameEntryCancelled = ProtocolParser.ReadBool(stream);
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

		// Token: 0x06003C84 RID: 15492 RVA: 0x000C34FB File Offset: 0x000C16FB
		public void Serialize(Stream stream)
		{
			CancelGameEntryResponse.Serialize(stream, this);
		}

		// Token: 0x06003C85 RID: 15493 RVA: 0x000C3504 File Offset: 0x000C1704
		public static void Serialize(Stream stream, CancelGameEntryResponse instance)
		{
			if (instance.HasEntireGameEntryCancelled)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteBool(stream, instance.EntireGameEntryCancelled);
			}
		}

		// Token: 0x06003C86 RID: 15494 RVA: 0x000C3524 File Offset: 0x000C1724
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasEntireGameEntryCancelled)
			{
				num += 1U;
				num += 1U;
			}
			return num;
		}

		// Token: 0x040015BE RID: 5566
		public bool HasEntireGameEntryCancelled;

		// Token: 0x040015BF RID: 5567
		private bool _EntireGameEntryCancelled;
	}
}
