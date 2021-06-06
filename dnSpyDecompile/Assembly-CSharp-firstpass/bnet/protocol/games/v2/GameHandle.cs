using System;
using System.IO;

namespace bnet.protocol.games.v2
{
	// Token: 0x0200036B RID: 875
	public class GameHandle : IProtoBuf
	{
		// Token: 0x17000A17 RID: 2583
		// (get) Token: 0x06003770 RID: 14192 RVA: 0x000B62F4 File Offset: 0x000B44F4
		// (set) Token: 0x06003771 RID: 14193 RVA: 0x000B62FC File Offset: 0x000B44FC
		public FactoryId FactoryId
		{
			get
			{
				return this._FactoryId;
			}
			set
			{
				this._FactoryId = value;
				this.HasFactoryId = (value != null);
			}
		}

		// Token: 0x06003772 RID: 14194 RVA: 0x000B630F File Offset: 0x000B450F
		public void SetFactoryId(FactoryId val)
		{
			this.FactoryId = val;
		}

		// Token: 0x17000A18 RID: 2584
		// (get) Token: 0x06003773 RID: 14195 RVA: 0x000B6318 File Offset: 0x000B4518
		// (set) Token: 0x06003774 RID: 14196 RVA: 0x000B6320 File Offset: 0x000B4520
		public GameId GameId
		{
			get
			{
				return this._GameId;
			}
			set
			{
				this._GameId = value;
				this.HasGameId = (value != null);
			}
		}

		// Token: 0x06003775 RID: 14197 RVA: 0x000B6333 File Offset: 0x000B4533
		public void SetGameId(GameId val)
		{
			this.GameId = val;
		}

		// Token: 0x06003776 RID: 14198 RVA: 0x000B633C File Offset: 0x000B453C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasFactoryId)
			{
				num ^= this.FactoryId.GetHashCode();
			}
			if (this.HasGameId)
			{
				num ^= this.GameId.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003777 RID: 14199 RVA: 0x000B6384 File Offset: 0x000B4584
		public override bool Equals(object obj)
		{
			GameHandle gameHandle = obj as GameHandle;
			return gameHandle != null && this.HasFactoryId == gameHandle.HasFactoryId && (!this.HasFactoryId || this.FactoryId.Equals(gameHandle.FactoryId)) && this.HasGameId == gameHandle.HasGameId && (!this.HasGameId || this.GameId.Equals(gameHandle.GameId));
		}

		// Token: 0x17000A19 RID: 2585
		// (get) Token: 0x06003778 RID: 14200 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003779 RID: 14201 RVA: 0x000B63F4 File Offset: 0x000B45F4
		public static GameHandle ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameHandle>(bs, 0, -1);
		}

		// Token: 0x0600377A RID: 14202 RVA: 0x000B63FE File Offset: 0x000B45FE
		public void Deserialize(Stream stream)
		{
			GameHandle.Deserialize(stream, this);
		}

		// Token: 0x0600377B RID: 14203 RVA: 0x000B6408 File Offset: 0x000B4608
		public static GameHandle Deserialize(Stream stream, GameHandle instance)
		{
			return GameHandle.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600377C RID: 14204 RVA: 0x000B6414 File Offset: 0x000B4614
		public static GameHandle DeserializeLengthDelimited(Stream stream)
		{
			GameHandle gameHandle = new GameHandle();
			GameHandle.DeserializeLengthDelimited(stream, gameHandle);
			return gameHandle;
		}

		// Token: 0x0600377D RID: 14205 RVA: 0x000B6430 File Offset: 0x000B4630
		public static GameHandle DeserializeLengthDelimited(Stream stream, GameHandle instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GameHandle.Deserialize(stream, instance, num);
		}

		// Token: 0x0600377E RID: 14206 RVA: 0x000B6458 File Offset: 0x000B4658
		public static GameHandle Deserialize(Stream stream, GameHandle instance, long limit)
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
					else if (instance.GameId == null)
					{
						instance.GameId = GameId.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameId.DeserializeLengthDelimited(stream, instance.GameId);
					}
				}
				else if (instance.FactoryId == null)
				{
					instance.FactoryId = FactoryId.DeserializeLengthDelimited(stream);
				}
				else
				{
					FactoryId.DeserializeLengthDelimited(stream, instance.FactoryId);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600377F RID: 14207 RVA: 0x000B652A File Offset: 0x000B472A
		public void Serialize(Stream stream)
		{
			GameHandle.Serialize(stream, this);
		}

		// Token: 0x06003780 RID: 14208 RVA: 0x000B6534 File Offset: 0x000B4734
		public static void Serialize(Stream stream, GameHandle instance)
		{
			if (instance.HasFactoryId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.FactoryId.GetSerializedSize());
				FactoryId.Serialize(stream, instance.FactoryId);
			}
			if (instance.HasGameId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.GameId.GetSerializedSize());
				GameId.Serialize(stream, instance.GameId);
			}
		}

		// Token: 0x06003781 RID: 14209 RVA: 0x000B659C File Offset: 0x000B479C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasFactoryId)
			{
				num += 1U;
				uint serializedSize = this.FactoryId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasGameId)
			{
				num += 1U;
				uint serializedSize2 = this.GameId.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num;
		}

		// Token: 0x040014BB RID: 5307
		public bool HasFactoryId;

		// Token: 0x040014BC RID: 5308
		private FactoryId _FactoryId;

		// Token: 0x040014BD RID: 5309
		public bool HasGameId;

		// Token: 0x040014BE RID: 5310
		private GameId _GameId;
	}
}
