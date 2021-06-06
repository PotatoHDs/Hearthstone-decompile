using System;
using System.IO;

namespace bnet.protocol.games.v1
{
	// Token: 0x02000387 RID: 903
	public class SetGameSlotsRequest : IProtoBuf
	{
		// Token: 0x17000A80 RID: 2688
		// (get) Token: 0x060039A3 RID: 14755 RVA: 0x000BB727 File Offset: 0x000B9927
		// (set) Token: 0x060039A4 RID: 14756 RVA: 0x000BB72F File Offset: 0x000B992F
		public uint GameSlots
		{
			get
			{
				return this._GameSlots;
			}
			set
			{
				this._GameSlots = value;
				this.HasGameSlots = true;
			}
		}

		// Token: 0x060039A5 RID: 14757 RVA: 0x000BB73F File Offset: 0x000B993F
		public void SetGameSlots(uint val)
		{
			this.GameSlots = val;
		}

		// Token: 0x17000A81 RID: 2689
		// (get) Token: 0x060039A6 RID: 14758 RVA: 0x000BB748 File Offset: 0x000B9948
		// (set) Token: 0x060039A7 RID: 14759 RVA: 0x000BB750 File Offset: 0x000B9950
		public uint CreateGameRate
		{
			get
			{
				return this._CreateGameRate;
			}
			set
			{
				this._CreateGameRate = value;
				this.HasCreateGameRate = true;
			}
		}

		// Token: 0x060039A8 RID: 14760 RVA: 0x000BB760 File Offset: 0x000B9960
		public void SetCreateGameRate(uint val)
		{
			this.CreateGameRate = val;
		}

		// Token: 0x060039A9 RID: 14761 RVA: 0x000BB76C File Offset: 0x000B996C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasGameSlots)
			{
				num ^= this.GameSlots.GetHashCode();
			}
			if (this.HasCreateGameRate)
			{
				num ^= this.CreateGameRate.GetHashCode();
			}
			return num;
		}

		// Token: 0x060039AA RID: 14762 RVA: 0x000BB7B8 File Offset: 0x000B99B8
		public override bool Equals(object obj)
		{
			SetGameSlotsRequest setGameSlotsRequest = obj as SetGameSlotsRequest;
			return setGameSlotsRequest != null && this.HasGameSlots == setGameSlotsRequest.HasGameSlots && (!this.HasGameSlots || this.GameSlots.Equals(setGameSlotsRequest.GameSlots)) && this.HasCreateGameRate == setGameSlotsRequest.HasCreateGameRate && (!this.HasCreateGameRate || this.CreateGameRate.Equals(setGameSlotsRequest.CreateGameRate));
		}

		// Token: 0x17000A82 RID: 2690
		// (get) Token: 0x060039AB RID: 14763 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060039AC RID: 14764 RVA: 0x000BB82E File Offset: 0x000B9A2E
		public static SetGameSlotsRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SetGameSlotsRequest>(bs, 0, -1);
		}

		// Token: 0x060039AD RID: 14765 RVA: 0x000BB838 File Offset: 0x000B9A38
		public void Deserialize(Stream stream)
		{
			SetGameSlotsRequest.Deserialize(stream, this);
		}

		// Token: 0x060039AE RID: 14766 RVA: 0x000BB842 File Offset: 0x000B9A42
		public static SetGameSlotsRequest Deserialize(Stream stream, SetGameSlotsRequest instance)
		{
			return SetGameSlotsRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060039AF RID: 14767 RVA: 0x000BB850 File Offset: 0x000B9A50
		public static SetGameSlotsRequest DeserializeLengthDelimited(Stream stream)
		{
			SetGameSlotsRequest setGameSlotsRequest = new SetGameSlotsRequest();
			SetGameSlotsRequest.DeserializeLengthDelimited(stream, setGameSlotsRequest);
			return setGameSlotsRequest;
		}

		// Token: 0x060039B0 RID: 14768 RVA: 0x000BB86C File Offset: 0x000B9A6C
		public static SetGameSlotsRequest DeserializeLengthDelimited(Stream stream, SetGameSlotsRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SetGameSlotsRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x060039B1 RID: 14769 RVA: 0x000BB894 File Offset: 0x000B9A94
		public static SetGameSlotsRequest Deserialize(Stream stream, SetGameSlotsRequest instance, long limit)
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
				else if (num != 8)
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
						instance.CreateGameRate = ProtocolParser.ReadUInt32(stream);
					}
				}
				else
				{
					instance.GameSlots = ProtocolParser.ReadUInt32(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060039B2 RID: 14770 RVA: 0x000BB92B File Offset: 0x000B9B2B
		public void Serialize(Stream stream)
		{
			SetGameSlotsRequest.Serialize(stream, this);
		}

		// Token: 0x060039B3 RID: 14771 RVA: 0x000BB934 File Offset: 0x000B9B34
		public static void Serialize(Stream stream, SetGameSlotsRequest instance)
		{
			if (instance.HasGameSlots)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt32(stream, instance.GameSlots);
			}
			if (instance.HasCreateGameRate)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.CreateGameRate);
			}
		}

		// Token: 0x060039B4 RID: 14772 RVA: 0x000BB970 File Offset: 0x000B9B70
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasGameSlots)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.GameSlots);
			}
			if (this.HasCreateGameRate)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.CreateGameRate);
			}
			return num;
		}

		// Token: 0x0400151B RID: 5403
		public bool HasGameSlots;

		// Token: 0x0400151C RID: 5404
		private uint _GameSlots;

		// Token: 0x0400151D RID: 5405
		public bool HasCreateGameRate;

		// Token: 0x0400151E RID: 5406
		private uint _CreateGameRate;
	}
}
