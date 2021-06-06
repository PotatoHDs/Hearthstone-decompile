using System;
using System.IO;

namespace bnet.protocol.account.v1
{
	// Token: 0x02000523 RID: 1315
	public class GameAccountHandle : IProtoBuf
	{
		// Token: 0x170011B0 RID: 4528
		// (get) Token: 0x06005DDD RID: 24029 RVA: 0x0011C9D4 File Offset: 0x0011ABD4
		// (set) Token: 0x06005DDE RID: 24030 RVA: 0x0011C9DC File Offset: 0x0011ABDC
		public uint Id { get; set; }

		// Token: 0x06005DDF RID: 24031 RVA: 0x0011C9E5 File Offset: 0x0011ABE5
		public void SetId(uint val)
		{
			this.Id = val;
		}

		// Token: 0x170011B1 RID: 4529
		// (get) Token: 0x06005DE0 RID: 24032 RVA: 0x0011C9EE File Offset: 0x0011ABEE
		// (set) Token: 0x06005DE1 RID: 24033 RVA: 0x0011C9F6 File Offset: 0x0011ABF6
		public uint Program { get; set; }

		// Token: 0x06005DE2 RID: 24034 RVA: 0x0011C9FF File Offset: 0x0011ABFF
		public void SetProgram(uint val)
		{
			this.Program = val;
		}

		// Token: 0x170011B2 RID: 4530
		// (get) Token: 0x06005DE3 RID: 24035 RVA: 0x0011CA08 File Offset: 0x0011AC08
		// (set) Token: 0x06005DE4 RID: 24036 RVA: 0x0011CA10 File Offset: 0x0011AC10
		public uint Region { get; set; }

		// Token: 0x06005DE5 RID: 24037 RVA: 0x0011CA19 File Offset: 0x0011AC19
		public void SetRegion(uint val)
		{
			this.Region = val;
		}

		// Token: 0x06005DE6 RID: 24038 RVA: 0x0011CA24 File Offset: 0x0011AC24
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.Id.GetHashCode() ^ this.Program.GetHashCode() ^ this.Region.GetHashCode();
		}

		// Token: 0x06005DE7 RID: 24039 RVA: 0x0011CA6C File Offset: 0x0011AC6C
		public override bool Equals(object obj)
		{
			GameAccountHandle gameAccountHandle = obj as GameAccountHandle;
			return gameAccountHandle != null && this.Id.Equals(gameAccountHandle.Id) && this.Program.Equals(gameAccountHandle.Program) && this.Region.Equals(gameAccountHandle.Region);
		}

		// Token: 0x170011B3 RID: 4531
		// (get) Token: 0x06005DE8 RID: 24040 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005DE9 RID: 24041 RVA: 0x0011CACE File Offset: 0x0011ACCE
		public static GameAccountHandle ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameAccountHandle>(bs, 0, -1);
		}

		// Token: 0x06005DEA RID: 24042 RVA: 0x0011CAD8 File Offset: 0x0011ACD8
		public void Deserialize(Stream stream)
		{
			GameAccountHandle.Deserialize(stream, this);
		}

		// Token: 0x06005DEB RID: 24043 RVA: 0x0011CAE2 File Offset: 0x0011ACE2
		public static GameAccountHandle Deserialize(Stream stream, GameAccountHandle instance)
		{
			return GameAccountHandle.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005DEC RID: 24044 RVA: 0x0011CAF0 File Offset: 0x0011ACF0
		public static GameAccountHandle DeserializeLengthDelimited(Stream stream)
		{
			GameAccountHandle gameAccountHandle = new GameAccountHandle();
			GameAccountHandle.DeserializeLengthDelimited(stream, gameAccountHandle);
			return gameAccountHandle;
		}

		// Token: 0x06005DED RID: 24045 RVA: 0x0011CB0C File Offset: 0x0011AD0C
		public static GameAccountHandle DeserializeLengthDelimited(Stream stream, GameAccountHandle instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GameAccountHandle.Deserialize(stream, instance, num);
		}

		// Token: 0x06005DEE RID: 24046 RVA: 0x0011CB34 File Offset: 0x0011AD34
		public static GameAccountHandle Deserialize(Stream stream, GameAccountHandle instance, long limit)
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
				else if (num != 13)
				{
					if (num != 21)
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
							instance.Region = ProtocolParser.ReadUInt32(stream);
						}
					}
					else
					{
						instance.Program = binaryReader.ReadUInt32();
					}
				}
				else
				{
					instance.Id = binaryReader.ReadUInt32();
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06005DEF RID: 24047 RVA: 0x0011CBE9 File Offset: 0x0011ADE9
		public void Serialize(Stream stream)
		{
			GameAccountHandle.Serialize(stream, this);
		}

		// Token: 0x06005DF0 RID: 24048 RVA: 0x0011CBF4 File Offset: 0x0011ADF4
		public static void Serialize(Stream stream, GameAccountHandle instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			stream.WriteByte(13);
			binaryWriter.Write(instance.Id);
			stream.WriteByte(21);
			binaryWriter.Write(instance.Program);
			stream.WriteByte(24);
			ProtocolParser.WriteUInt32(stream, instance.Region);
		}

		// Token: 0x06005DF1 RID: 24049 RVA: 0x0011CC42 File Offset: 0x0011AE42
		public uint GetSerializedSize()
		{
			return 0U + 4U + 4U + ProtocolParser.SizeOfUInt32(this.Region) + 3U;
		}
	}
}
