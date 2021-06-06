using System;
using System.IO;
using PegasusShared;

namespace PegasusGame
{
	// Token: 0x0200019F RID: 415
	public class Player : IProtoBuf
	{
		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x06001A14 RID: 6676 RVA: 0x0005C69B File Offset: 0x0005A89B
		// (set) Token: 0x06001A15 RID: 6677 RVA: 0x0005C6A3 File Offset: 0x0005A8A3
		public int Id { get; set; }

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x06001A16 RID: 6678 RVA: 0x0005C6AC File Offset: 0x0005A8AC
		// (set) Token: 0x06001A17 RID: 6679 RVA: 0x0005C6B4 File Offset: 0x0005A8B4
		public BnetId GameAccountId { get; set; }

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x06001A18 RID: 6680 RVA: 0x0005C6BD File Offset: 0x0005A8BD
		// (set) Token: 0x06001A19 RID: 6681 RVA: 0x0005C6C5 File Offset: 0x0005A8C5
		public int CardBack { get; set; }

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x06001A1A RID: 6682 RVA: 0x0005C6CE File Offset: 0x0005A8CE
		// (set) Token: 0x06001A1B RID: 6683 RVA: 0x0005C6D6 File Offset: 0x0005A8D6
		public Entity Entity { get; set; }

		// Token: 0x06001A1C RID: 6684 RVA: 0x0005C6E0 File Offset: 0x0005A8E0
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.Id.GetHashCode() ^ this.GameAccountId.GetHashCode() ^ this.CardBack.GetHashCode() ^ this.Entity.GetHashCode();
		}

		// Token: 0x06001A1D RID: 6685 RVA: 0x0005C730 File Offset: 0x0005A930
		public override bool Equals(object obj)
		{
			Player player = obj as Player;
			return player != null && this.Id.Equals(player.Id) && this.GameAccountId.Equals(player.GameAccountId) && this.CardBack.Equals(player.CardBack) && this.Entity.Equals(player.Entity);
		}

		// Token: 0x06001A1E RID: 6686 RVA: 0x0005C7A4 File Offset: 0x0005A9A4
		public void Deserialize(Stream stream)
		{
			Player.Deserialize(stream, this);
		}

		// Token: 0x06001A1F RID: 6687 RVA: 0x0005C7AE File Offset: 0x0005A9AE
		public static Player Deserialize(Stream stream, Player instance)
		{
			return Player.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001A20 RID: 6688 RVA: 0x0005C7BC File Offset: 0x0005A9BC
		public static Player DeserializeLengthDelimited(Stream stream)
		{
			Player player = new Player();
			Player.DeserializeLengthDelimited(stream, player);
			return player;
		}

		// Token: 0x06001A21 RID: 6689 RVA: 0x0005C7D8 File Offset: 0x0005A9D8
		public static Player DeserializeLengthDelimited(Stream stream, Player instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return Player.Deserialize(stream, instance, num);
		}

		// Token: 0x06001A22 RID: 6690 RVA: 0x0005C800 File Offset: 0x0005AA00
		public static Player Deserialize(Stream stream, Player instance, long limit)
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
				else
				{
					if (num <= 18)
					{
						if (num == 8)
						{
							instance.Id = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 18)
						{
							if (instance.GameAccountId == null)
							{
								instance.GameAccountId = BnetId.DeserializeLengthDelimited(stream);
								continue;
							}
							BnetId.DeserializeLengthDelimited(stream, instance.GameAccountId);
							continue;
						}
					}
					else
					{
						if (num == 24)
						{
							instance.CardBack = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 34)
						{
							if (instance.Entity == null)
							{
								instance.Entity = Entity.DeserializeLengthDelimited(stream);
								continue;
							}
							Entity.DeserializeLengthDelimited(stream, instance.Entity);
							continue;
						}
					}
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

		// Token: 0x06001A23 RID: 6691 RVA: 0x0005C90C File Offset: 0x0005AB0C
		public void Serialize(Stream stream)
		{
			Player.Serialize(stream, this);
		}

		// Token: 0x06001A24 RID: 6692 RVA: 0x0005C918 File Offset: 0x0005AB18
		public static void Serialize(Stream stream, Player instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Id));
			if (instance.GameAccountId == null)
			{
				throw new ArgumentNullException("GameAccountId", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteUInt32(stream, instance.GameAccountId.GetSerializedSize());
			BnetId.Serialize(stream, instance.GameAccountId);
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.CardBack));
			if (instance.Entity == null)
			{
				throw new ArgumentNullException("Entity", "Required by proto specification.");
			}
			stream.WriteByte(34);
			ProtocolParser.WriteUInt32(stream, instance.Entity.GetSerializedSize());
			Entity.Serialize(stream, instance.Entity);
		}

		// Token: 0x06001A25 RID: 6693 RVA: 0x0005C9C8 File Offset: 0x0005ABC8
		public uint GetSerializedSize()
		{
			uint num = 0U + ProtocolParser.SizeOfUInt64((ulong)((long)this.Id));
			uint serializedSize = this.GameAccountId.GetSerializedSize();
			uint num2 = num + (serializedSize + ProtocolParser.SizeOfUInt32(serializedSize)) + ProtocolParser.SizeOfUInt64((ulong)((long)this.CardBack));
			uint serializedSize2 = this.Entity.GetSerializedSize();
			return num2 + (serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2)) + 4U;
		}
	}
}
