using System;
using System.IO;

namespace bnet.protocol.matchmaking.v1
{
	// Token: 0x020003DA RID: 986
	public class GameMatchmakingEntry : IProtoBuf
	{
		// Token: 0x17000BEE RID: 3054
		// (get) Token: 0x060040C5 RID: 16581 RVA: 0x000CE47B File Offset: 0x000CC67B
		// (set) Token: 0x060040C6 RID: 16582 RVA: 0x000CE483 File Offset: 0x000CC683
		public GameMatchmakingOptions Options
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

		// Token: 0x060040C7 RID: 16583 RVA: 0x000CE496 File Offset: 0x000CC696
		public void SetOptions(GameMatchmakingOptions val)
		{
			this.Options = val;
		}

		// Token: 0x17000BEF RID: 3055
		// (get) Token: 0x060040C8 RID: 16584 RVA: 0x000CE49F File Offset: 0x000CC69F
		// (set) Token: 0x060040C9 RID: 16585 RVA: 0x000CE4A7 File Offset: 0x000CC6A7
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

		// Token: 0x060040CA RID: 16586 RVA: 0x000CE4BA File Offset: 0x000CC6BA
		public void SetRequestId(RequestId val)
		{
			this.RequestId = val;
		}

		// Token: 0x060040CB RID: 16587 RVA: 0x000CE4C4 File Offset: 0x000CC6C4
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasOptions)
			{
				num ^= this.Options.GetHashCode();
			}
			if (this.HasRequestId)
			{
				num ^= this.RequestId.GetHashCode();
			}
			return num;
		}

		// Token: 0x060040CC RID: 16588 RVA: 0x000CE50C File Offset: 0x000CC70C
		public override bool Equals(object obj)
		{
			GameMatchmakingEntry gameMatchmakingEntry = obj as GameMatchmakingEntry;
			return gameMatchmakingEntry != null && this.HasOptions == gameMatchmakingEntry.HasOptions && (!this.HasOptions || this.Options.Equals(gameMatchmakingEntry.Options)) && this.HasRequestId == gameMatchmakingEntry.HasRequestId && (!this.HasRequestId || this.RequestId.Equals(gameMatchmakingEntry.RequestId));
		}

		// Token: 0x17000BF0 RID: 3056
		// (get) Token: 0x060040CD RID: 16589 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060040CE RID: 16590 RVA: 0x000CE57C File Offset: 0x000CC77C
		public static GameMatchmakingEntry ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameMatchmakingEntry>(bs, 0, -1);
		}

		// Token: 0x060040CF RID: 16591 RVA: 0x000CE586 File Offset: 0x000CC786
		public void Deserialize(Stream stream)
		{
			GameMatchmakingEntry.Deserialize(stream, this);
		}

		// Token: 0x060040D0 RID: 16592 RVA: 0x000CE590 File Offset: 0x000CC790
		public static GameMatchmakingEntry Deserialize(Stream stream, GameMatchmakingEntry instance)
		{
			return GameMatchmakingEntry.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060040D1 RID: 16593 RVA: 0x000CE59C File Offset: 0x000CC79C
		public static GameMatchmakingEntry DeserializeLengthDelimited(Stream stream)
		{
			GameMatchmakingEntry gameMatchmakingEntry = new GameMatchmakingEntry();
			GameMatchmakingEntry.DeserializeLengthDelimited(stream, gameMatchmakingEntry);
			return gameMatchmakingEntry;
		}

		// Token: 0x060040D2 RID: 16594 RVA: 0x000CE5B8 File Offset: 0x000CC7B8
		public static GameMatchmakingEntry DeserializeLengthDelimited(Stream stream, GameMatchmakingEntry instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GameMatchmakingEntry.Deserialize(stream, instance, num);
		}

		// Token: 0x060040D3 RID: 16595 RVA: 0x000CE5E0 File Offset: 0x000CC7E0
		public static GameMatchmakingEntry Deserialize(Stream stream, GameMatchmakingEntry instance, long limit)
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
					if (num != 18)
					{
						Key key = ProtocolParser.ReadKey((byte)num, stream);
						if (key.Field == 0U)
						{
							throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
						}
						ProtocolParser.SkipKey(stream, key);
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
				else if (instance.Options == null)
				{
					instance.Options = GameMatchmakingOptions.DeserializeLengthDelimited(stream);
				}
				else
				{
					GameMatchmakingOptions.DeserializeLengthDelimited(stream, instance.Options);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060040D4 RID: 16596 RVA: 0x000CE6B2 File Offset: 0x000CC8B2
		public void Serialize(Stream stream)
		{
			GameMatchmakingEntry.Serialize(stream, this);
		}

		// Token: 0x060040D5 RID: 16597 RVA: 0x000CE6BC File Offset: 0x000CC8BC
		public static void Serialize(Stream stream, GameMatchmakingEntry instance)
		{
			if (instance.HasOptions)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Options.GetSerializedSize());
				GameMatchmakingOptions.Serialize(stream, instance.Options);
			}
			if (instance.HasRequestId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.RequestId.GetSerializedSize());
				RequestId.Serialize(stream, instance.RequestId);
			}
		}

		// Token: 0x060040D6 RID: 16598 RVA: 0x000CE724 File Offset: 0x000CC924
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasOptions)
			{
				num += 1U;
				uint serializedSize = this.Options.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasRequestId)
			{
				num += 1U;
				uint serializedSize2 = this.RequestId.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num;
		}

		// Token: 0x0400167E RID: 5758
		public bool HasOptions;

		// Token: 0x0400167F RID: 5759
		private GameMatchmakingOptions _Options;

		// Token: 0x04001680 RID: 5760
		public bool HasRequestId;

		// Token: 0x04001681 RID: 5761
		private RequestId _RequestId;
	}
}
