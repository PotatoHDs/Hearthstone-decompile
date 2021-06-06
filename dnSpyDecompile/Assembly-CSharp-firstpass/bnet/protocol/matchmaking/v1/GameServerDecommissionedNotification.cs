using System;
using System.IO;

namespace bnet.protocol.matchmaking.v1
{
	// Token: 0x020003B9 RID: 953
	public class GameServerDecommissionedNotification : IProtoBuf
	{
		// Token: 0x17000B73 RID: 2931
		// (get) Token: 0x06003E1E RID: 15902 RVA: 0x000C7BCB File Offset: 0x000C5DCB
		// (set) Token: 0x06003E1F RID: 15903 RVA: 0x000C7BD3 File Offset: 0x000C5DD3
		public ulong GameServerGuid
		{
			get
			{
				return this._GameServerGuid;
			}
			set
			{
				this._GameServerGuid = value;
				this.HasGameServerGuid = true;
			}
		}

		// Token: 0x06003E20 RID: 15904 RVA: 0x000C7BE3 File Offset: 0x000C5DE3
		public void SetGameServerGuid(ulong val)
		{
			this.GameServerGuid = val;
		}

		// Token: 0x06003E21 RID: 15905 RVA: 0x000C7BEC File Offset: 0x000C5DEC
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasGameServerGuid)
			{
				num ^= this.GameServerGuid.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003E22 RID: 15906 RVA: 0x000C7C20 File Offset: 0x000C5E20
		public override bool Equals(object obj)
		{
			GameServerDecommissionedNotification gameServerDecommissionedNotification = obj as GameServerDecommissionedNotification;
			return gameServerDecommissionedNotification != null && this.HasGameServerGuid == gameServerDecommissionedNotification.HasGameServerGuid && (!this.HasGameServerGuid || this.GameServerGuid.Equals(gameServerDecommissionedNotification.GameServerGuid));
		}

		// Token: 0x17000B74 RID: 2932
		// (get) Token: 0x06003E23 RID: 15907 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003E24 RID: 15908 RVA: 0x000C7C68 File Offset: 0x000C5E68
		public static GameServerDecommissionedNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameServerDecommissionedNotification>(bs, 0, -1);
		}

		// Token: 0x06003E25 RID: 15909 RVA: 0x000C7C72 File Offset: 0x000C5E72
		public void Deserialize(Stream stream)
		{
			GameServerDecommissionedNotification.Deserialize(stream, this);
		}

		// Token: 0x06003E26 RID: 15910 RVA: 0x000C7C7C File Offset: 0x000C5E7C
		public static GameServerDecommissionedNotification Deserialize(Stream stream, GameServerDecommissionedNotification instance)
		{
			return GameServerDecommissionedNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003E27 RID: 15911 RVA: 0x000C7C88 File Offset: 0x000C5E88
		public static GameServerDecommissionedNotification DeserializeLengthDelimited(Stream stream)
		{
			GameServerDecommissionedNotification gameServerDecommissionedNotification = new GameServerDecommissionedNotification();
			GameServerDecommissionedNotification.DeserializeLengthDelimited(stream, gameServerDecommissionedNotification);
			return gameServerDecommissionedNotification;
		}

		// Token: 0x06003E28 RID: 15912 RVA: 0x000C7CA4 File Offset: 0x000C5EA4
		public static GameServerDecommissionedNotification DeserializeLengthDelimited(Stream stream, GameServerDecommissionedNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GameServerDecommissionedNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x06003E29 RID: 15913 RVA: 0x000C7CCC File Offset: 0x000C5ECC
		public static GameServerDecommissionedNotification Deserialize(Stream stream, GameServerDecommissionedNotification instance, long limit)
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
				else if (num == 9)
				{
					instance.GameServerGuid = binaryReader.ReadUInt64();
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

		// Token: 0x06003E2A RID: 15914 RVA: 0x000C7D53 File Offset: 0x000C5F53
		public void Serialize(Stream stream)
		{
			GameServerDecommissionedNotification.Serialize(stream, this);
		}

		// Token: 0x06003E2B RID: 15915 RVA: 0x000C7D5C File Offset: 0x000C5F5C
		public static void Serialize(Stream stream, GameServerDecommissionedNotification instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasGameServerGuid)
			{
				stream.WriteByte(9);
				binaryWriter.Write(instance.GameServerGuid);
			}
		}

		// Token: 0x06003E2C RID: 15916 RVA: 0x000C7D8C File Offset: 0x000C5F8C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasGameServerGuid)
			{
				num += 1U;
				num += 8U;
			}
			return num;
		}

		// Token: 0x040015FC RID: 5628
		public bool HasGameServerGuid;

		// Token: 0x040015FD RID: 5629
		private ulong _GameServerGuid;
	}
}
