using System;
using System.IO;

namespace bnet.protocol.games.v1
{
	// Token: 0x0200038C RID: 908
	public class GameResponseEntry : IProtoBuf
	{
		// Token: 0x17000A96 RID: 2710
		// (get) Token: 0x06003A11 RID: 14865 RVA: 0x000BC8D3 File Offset: 0x000BAAD3
		// (set) Token: 0x06003A12 RID: 14866 RVA: 0x000BC8DB File Offset: 0x000BAADB
		public ulong FactoryId
		{
			get
			{
				return this._FactoryId;
			}
			set
			{
				this._FactoryId = value;
				this.HasFactoryId = true;
			}
		}

		// Token: 0x06003A13 RID: 14867 RVA: 0x000BC8EB File Offset: 0x000BAAEB
		public void SetFactoryId(ulong val)
		{
			this.FactoryId = val;
		}

		// Token: 0x17000A97 RID: 2711
		// (get) Token: 0x06003A14 RID: 14868 RVA: 0x000BC8F4 File Offset: 0x000BAAF4
		// (set) Token: 0x06003A15 RID: 14869 RVA: 0x000BC8FC File Offset: 0x000BAAFC
		public float Popularity
		{
			get
			{
				return this._Popularity;
			}
			set
			{
				this._Popularity = value;
				this.HasPopularity = true;
			}
		}

		// Token: 0x06003A16 RID: 14870 RVA: 0x000BC90C File Offset: 0x000BAB0C
		public void SetPopularity(float val)
		{
			this.Popularity = val;
		}

		// Token: 0x06003A17 RID: 14871 RVA: 0x000BC918 File Offset: 0x000BAB18
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasFactoryId)
			{
				num ^= this.FactoryId.GetHashCode();
			}
			if (this.HasPopularity)
			{
				num ^= this.Popularity.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003A18 RID: 14872 RVA: 0x000BC964 File Offset: 0x000BAB64
		public override bool Equals(object obj)
		{
			GameResponseEntry gameResponseEntry = obj as GameResponseEntry;
			return gameResponseEntry != null && this.HasFactoryId == gameResponseEntry.HasFactoryId && (!this.HasFactoryId || this.FactoryId.Equals(gameResponseEntry.FactoryId)) && this.HasPopularity == gameResponseEntry.HasPopularity && (!this.HasPopularity || this.Popularity.Equals(gameResponseEntry.Popularity));
		}

		// Token: 0x17000A98 RID: 2712
		// (get) Token: 0x06003A19 RID: 14873 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003A1A RID: 14874 RVA: 0x000BC9DA File Offset: 0x000BABDA
		public static GameResponseEntry ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameResponseEntry>(bs, 0, -1);
		}

		// Token: 0x06003A1B RID: 14875 RVA: 0x000BC9E4 File Offset: 0x000BABE4
		public void Deserialize(Stream stream)
		{
			GameResponseEntry.Deserialize(stream, this);
		}

		// Token: 0x06003A1C RID: 14876 RVA: 0x000BC9EE File Offset: 0x000BABEE
		public static GameResponseEntry Deserialize(Stream stream, GameResponseEntry instance)
		{
			return GameResponseEntry.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003A1D RID: 14877 RVA: 0x000BC9FC File Offset: 0x000BABFC
		public static GameResponseEntry DeserializeLengthDelimited(Stream stream)
		{
			GameResponseEntry gameResponseEntry = new GameResponseEntry();
			GameResponseEntry.DeserializeLengthDelimited(stream, gameResponseEntry);
			return gameResponseEntry;
		}

		// Token: 0x06003A1E RID: 14878 RVA: 0x000BCA18 File Offset: 0x000BAC18
		public static GameResponseEntry DeserializeLengthDelimited(Stream stream, GameResponseEntry instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GameResponseEntry.Deserialize(stream, instance, num);
		}

		// Token: 0x06003A1F RID: 14879 RVA: 0x000BCA40 File Offset: 0x000BAC40
		public static GameResponseEntry Deserialize(Stream stream, GameResponseEntry instance, long limit)
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
					if (num != 21)
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
						instance.Popularity = binaryReader.ReadSingle();
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

		// Token: 0x06003A20 RID: 14880 RVA: 0x000BCADF File Offset: 0x000BACDF
		public void Serialize(Stream stream)
		{
			GameResponseEntry.Serialize(stream, this);
		}

		// Token: 0x06003A21 RID: 14881 RVA: 0x000BCAE8 File Offset: 0x000BACE8
		public static void Serialize(Stream stream, GameResponseEntry instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasFactoryId)
			{
				stream.WriteByte(9);
				binaryWriter.Write(instance.FactoryId);
			}
			if (instance.HasPopularity)
			{
				stream.WriteByte(21);
				binaryWriter.Write(instance.Popularity);
			}
		}

		// Token: 0x06003A22 RID: 14882 RVA: 0x000BCB34 File Offset: 0x000BAD34
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasFactoryId)
			{
				num += 1U;
				num += 8U;
			}
			if (this.HasPopularity)
			{
				num += 1U;
				num += 4U;
			}
			return num;
		}

		// Token: 0x0400152E RID: 5422
		public bool HasFactoryId;

		// Token: 0x0400152F RID: 5423
		private ulong _FactoryId;

		// Token: 0x04001530 RID: 5424
		public bool HasPopularity;

		// Token: 0x04001531 RID: 5425
		private float _Popularity;
	}
}
