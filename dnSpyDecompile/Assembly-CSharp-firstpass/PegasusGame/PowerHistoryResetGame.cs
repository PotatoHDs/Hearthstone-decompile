using System;
using System.IO;

namespace PegasusGame
{
	// Token: 0x020001BE RID: 446
	public class PowerHistoryResetGame : IProtoBuf
	{
		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x06001C51 RID: 7249 RVA: 0x00063D5A File Offset: 0x00061F5A
		// (set) Token: 0x06001C52 RID: 7250 RVA: 0x00063D62 File Offset: 0x00061F62
		public PowerHistoryCreateGame CreateGame { get; set; }

		// Token: 0x06001C53 RID: 7251 RVA: 0x00063D6B File Offset: 0x00061F6B
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.CreateGame.GetHashCode();
		}

		// Token: 0x06001C54 RID: 7252 RVA: 0x00063D84 File Offset: 0x00061F84
		public override bool Equals(object obj)
		{
			PowerHistoryResetGame powerHistoryResetGame = obj as PowerHistoryResetGame;
			return powerHistoryResetGame != null && this.CreateGame.Equals(powerHistoryResetGame.CreateGame);
		}

		// Token: 0x06001C55 RID: 7253 RVA: 0x00063DB3 File Offset: 0x00061FB3
		public void Deserialize(Stream stream)
		{
			PowerHistoryResetGame.Deserialize(stream, this);
		}

		// Token: 0x06001C56 RID: 7254 RVA: 0x00063DBD File Offset: 0x00061FBD
		public static PowerHistoryResetGame Deserialize(Stream stream, PowerHistoryResetGame instance)
		{
			return PowerHistoryResetGame.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001C57 RID: 7255 RVA: 0x00063DC8 File Offset: 0x00061FC8
		public static PowerHistoryResetGame DeserializeLengthDelimited(Stream stream)
		{
			PowerHistoryResetGame powerHistoryResetGame = new PowerHistoryResetGame();
			PowerHistoryResetGame.DeserializeLengthDelimited(stream, powerHistoryResetGame);
			return powerHistoryResetGame;
		}

		// Token: 0x06001C58 RID: 7256 RVA: 0x00063DE4 File Offset: 0x00061FE4
		public static PowerHistoryResetGame DeserializeLengthDelimited(Stream stream, PowerHistoryResetGame instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return PowerHistoryResetGame.Deserialize(stream, instance, num);
		}

		// Token: 0x06001C59 RID: 7257 RVA: 0x00063E0C File Offset: 0x0006200C
		public static PowerHistoryResetGame Deserialize(Stream stream, PowerHistoryResetGame instance, long limit)
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
				else if (num == 10)
				{
					if (instance.CreateGame == null)
					{
						instance.CreateGame = PowerHistoryCreateGame.DeserializeLengthDelimited(stream);
					}
					else
					{
						PowerHistoryCreateGame.DeserializeLengthDelimited(stream, instance.CreateGame);
					}
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

		// Token: 0x06001C5A RID: 7258 RVA: 0x00063EA6 File Offset: 0x000620A6
		public void Serialize(Stream stream)
		{
			PowerHistoryResetGame.Serialize(stream, this);
		}

		// Token: 0x06001C5B RID: 7259 RVA: 0x00063EAF File Offset: 0x000620AF
		public static void Serialize(Stream stream, PowerHistoryResetGame instance)
		{
			if (instance.CreateGame == null)
			{
				throw new ArgumentNullException("CreateGame", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.CreateGame.GetSerializedSize());
			PowerHistoryCreateGame.Serialize(stream, instance.CreateGame);
		}

		// Token: 0x06001C5C RID: 7260 RVA: 0x00063EF0 File Offset: 0x000620F0
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint serializedSize = this.CreateGame.GetSerializedSize();
			return num + (serializedSize + ProtocolParser.SizeOfUInt32(serializedSize)) + 1U;
		}
	}
}
