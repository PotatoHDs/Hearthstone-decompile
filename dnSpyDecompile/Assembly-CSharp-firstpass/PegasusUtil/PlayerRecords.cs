using System;
using System.Collections.Generic;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x020000B0 RID: 176
	public class PlayerRecords : IProtoBuf
	{
		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000C38 RID: 3128 RVA: 0x0002E2C9 File Offset: 0x0002C4C9
		// (set) Token: 0x06000C39 RID: 3129 RVA: 0x0002E2D1 File Offset: 0x0002C4D1
		public List<PlayerRecord> Records
		{
			get
			{
				return this._Records;
			}
			set
			{
				this._Records = value;
			}
		}

		// Token: 0x06000C3A RID: 3130 RVA: 0x0002E2DC File Offset: 0x0002C4DC
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (PlayerRecord playerRecord in this.Records)
			{
				num ^= playerRecord.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000C3B RID: 3131 RVA: 0x0002E340 File Offset: 0x0002C540
		public override bool Equals(object obj)
		{
			PlayerRecords playerRecords = obj as PlayerRecords;
			if (playerRecords == null)
			{
				return false;
			}
			if (this.Records.Count != playerRecords.Records.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Records.Count; i++)
			{
				if (!this.Records[i].Equals(playerRecords.Records[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000C3C RID: 3132 RVA: 0x0002E3AB File Offset: 0x0002C5AB
		public void Deserialize(Stream stream)
		{
			PlayerRecords.Deserialize(stream, this);
		}

		// Token: 0x06000C3D RID: 3133 RVA: 0x0002E3B5 File Offset: 0x0002C5B5
		public static PlayerRecords Deserialize(Stream stream, PlayerRecords instance)
		{
			return PlayerRecords.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000C3E RID: 3134 RVA: 0x0002E3C0 File Offset: 0x0002C5C0
		public static PlayerRecords DeserializeLengthDelimited(Stream stream)
		{
			PlayerRecords playerRecords = new PlayerRecords();
			PlayerRecords.DeserializeLengthDelimited(stream, playerRecords);
			return playerRecords;
		}

		// Token: 0x06000C3F RID: 3135 RVA: 0x0002E3DC File Offset: 0x0002C5DC
		public static PlayerRecords DeserializeLengthDelimited(Stream stream, PlayerRecords instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return PlayerRecords.Deserialize(stream, instance, num);
		}

		// Token: 0x06000C40 RID: 3136 RVA: 0x0002E404 File Offset: 0x0002C604
		public static PlayerRecords Deserialize(Stream stream, PlayerRecords instance, long limit)
		{
			if (instance.Records == null)
			{
				instance.Records = new List<PlayerRecord>();
			}
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
				else if (num == 10)
				{
					instance.Records.Add(PlayerRecord.DeserializeLengthDelimited(stream));
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

		// Token: 0x06000C41 RID: 3137 RVA: 0x0002E49C File Offset: 0x0002C69C
		public void Serialize(Stream stream)
		{
			PlayerRecords.Serialize(stream, this);
		}

		// Token: 0x06000C42 RID: 3138 RVA: 0x0002E4A8 File Offset: 0x0002C6A8
		public static void Serialize(Stream stream, PlayerRecords instance)
		{
			if (instance.Records.Count > 0)
			{
				foreach (PlayerRecord playerRecord in instance.Records)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, playerRecord.GetSerializedSize());
					PlayerRecord.Serialize(stream, playerRecord);
				}
			}
		}

		// Token: 0x06000C43 RID: 3139 RVA: 0x0002E520 File Offset: 0x0002C720
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.Records.Count > 0)
			{
				foreach (PlayerRecord playerRecord in this.Records)
				{
					num += 1U;
					uint serializedSize = playerRecord.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num;
		}

		// Token: 0x04000451 RID: 1105
		private List<PlayerRecord> _Records = new List<PlayerRecord>();

		// Token: 0x020005B9 RID: 1465
		public enum PacketID
		{
			// Token: 0x04001F7A RID: 8058
			ID = 270
		}
	}
}
