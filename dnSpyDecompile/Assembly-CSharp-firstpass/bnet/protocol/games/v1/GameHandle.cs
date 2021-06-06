using System;
using System.IO;

namespace bnet.protocol.games.v1
{
	// Token: 0x020003A3 RID: 931
	public class GameHandle : IProtoBuf
	{
		// Token: 0x17000B0D RID: 2829
		// (get) Token: 0x06003C48 RID: 15432 RVA: 0x000C2BE2 File Offset: 0x000C0DE2
		// (set) Token: 0x06003C49 RID: 15433 RVA: 0x000C2BEA File Offset: 0x000C0DEA
		public ulong FactoryId { get; set; }

		// Token: 0x06003C4A RID: 15434 RVA: 0x000C2BF3 File Offset: 0x000C0DF3
		public void SetFactoryId(ulong val)
		{
			this.FactoryId = val;
		}

		// Token: 0x17000B0E RID: 2830
		// (get) Token: 0x06003C4B RID: 15435 RVA: 0x000C2BFC File Offset: 0x000C0DFC
		// (set) Token: 0x06003C4C RID: 15436 RVA: 0x000C2C04 File Offset: 0x000C0E04
		public EntityId GameId { get; set; }

		// Token: 0x06003C4D RID: 15437 RVA: 0x000C2C0D File Offset: 0x000C0E0D
		public void SetGameId(EntityId val)
		{
			this.GameId = val;
		}

		// Token: 0x06003C4E RID: 15438 RVA: 0x000C2C18 File Offset: 0x000C0E18
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.FactoryId.GetHashCode() ^ this.GameId.GetHashCode();
		}

		// Token: 0x06003C4F RID: 15439 RVA: 0x000C2C4C File Offset: 0x000C0E4C
		public override bool Equals(object obj)
		{
			GameHandle gameHandle = obj as GameHandle;
			return gameHandle != null && this.FactoryId.Equals(gameHandle.FactoryId) && this.GameId.Equals(gameHandle.GameId);
		}

		// Token: 0x17000B0F RID: 2831
		// (get) Token: 0x06003C50 RID: 15440 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003C51 RID: 15441 RVA: 0x000C2C93 File Offset: 0x000C0E93
		public static GameHandle ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameHandle>(bs, 0, -1);
		}

		// Token: 0x06003C52 RID: 15442 RVA: 0x000C2C9D File Offset: 0x000C0E9D
		public void Deserialize(Stream stream)
		{
			GameHandle.Deserialize(stream, this);
		}

		// Token: 0x06003C53 RID: 15443 RVA: 0x000C2CA7 File Offset: 0x000C0EA7
		public static GameHandle Deserialize(Stream stream, GameHandle instance)
		{
			return GameHandle.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003C54 RID: 15444 RVA: 0x000C2CB4 File Offset: 0x000C0EB4
		public static GameHandle DeserializeLengthDelimited(Stream stream)
		{
			GameHandle gameHandle = new GameHandle();
			GameHandle.DeserializeLengthDelimited(stream, gameHandle);
			return gameHandle;
		}

		// Token: 0x06003C55 RID: 15445 RVA: 0x000C2CD0 File Offset: 0x000C0ED0
		public static GameHandle DeserializeLengthDelimited(Stream stream, GameHandle instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GameHandle.Deserialize(stream, instance, num);
		}

		// Token: 0x06003C56 RID: 15446 RVA: 0x000C2CF8 File Offset: 0x000C0EF8
		public static GameHandle Deserialize(Stream stream, GameHandle instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
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
				else if (num != 9)
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
						instance.GameId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.GameId);
					}
				}
				else
				{
					instance.FactoryId = binaryReader.ReadUInt64();
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06003C57 RID: 15447 RVA: 0x000C2DB1 File Offset: 0x000C0FB1
		public void Serialize(Stream stream)
		{
			GameHandle.Serialize(stream, this);
		}

		// Token: 0x06003C58 RID: 15448 RVA: 0x000C2DBC File Offset: 0x000C0FBC
		public static void Serialize(Stream stream, GameHandle instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			stream.WriteByte(9);
			binaryWriter.Write(instance.FactoryId);
			if (instance.GameId == null)
			{
				throw new ArgumentNullException("GameId", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteUInt32(stream, instance.GameId.GetSerializedSize());
			EntityId.Serialize(stream, instance.GameId);
		}

		// Token: 0x06003C59 RID: 15449 RVA: 0x000C2E20 File Offset: 0x000C1020
		public uint GetSerializedSize()
		{
			uint num = 0U + 8U;
			uint serializedSize = this.GameId.GetSerializedSize();
			return num + (serializedSize + ProtocolParser.SizeOfUInt32(serializedSize)) + 2U;
		}
	}
}
