using System;
using System.Collections.Generic;
using System.IO;
using PegasusShared;

namespace PegasusGame
{
	// Token: 0x020001D6 RID: 470
	public class GameLogRelay : IProtoBuf
	{
		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x06001E03 RID: 7683 RVA: 0x000697DF File Offset: 0x000679DF
		// (set) Token: 0x06001E04 RID: 7684 RVA: 0x000697E7 File Offset: 0x000679E7
		public List<LogRelayMessage> Messages
		{
			get
			{
				return this._Messages;
			}
			set
			{
				this._Messages = value;
			}
		}

		// Token: 0x06001E05 RID: 7685 RVA: 0x000697F0 File Offset: 0x000679F0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (LogRelayMessage logRelayMessage in this.Messages)
			{
				num ^= logRelayMessage.GetHashCode();
			}
			return num;
		}

		// Token: 0x06001E06 RID: 7686 RVA: 0x00069854 File Offset: 0x00067A54
		public override bool Equals(object obj)
		{
			GameLogRelay gameLogRelay = obj as GameLogRelay;
			if (gameLogRelay == null)
			{
				return false;
			}
			if (this.Messages.Count != gameLogRelay.Messages.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Messages.Count; i++)
			{
				if (!this.Messages[i].Equals(gameLogRelay.Messages[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001E07 RID: 7687 RVA: 0x000698BF File Offset: 0x00067ABF
		public void Deserialize(Stream stream)
		{
			GameLogRelay.Deserialize(stream, this);
		}

		// Token: 0x06001E08 RID: 7688 RVA: 0x000698C9 File Offset: 0x00067AC9
		public static GameLogRelay Deserialize(Stream stream, GameLogRelay instance)
		{
			return GameLogRelay.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001E09 RID: 7689 RVA: 0x000698D4 File Offset: 0x00067AD4
		public static GameLogRelay DeserializeLengthDelimited(Stream stream)
		{
			GameLogRelay gameLogRelay = new GameLogRelay();
			GameLogRelay.DeserializeLengthDelimited(stream, gameLogRelay);
			return gameLogRelay;
		}

		// Token: 0x06001E0A RID: 7690 RVA: 0x000698F0 File Offset: 0x00067AF0
		public static GameLogRelay DeserializeLengthDelimited(Stream stream, GameLogRelay instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GameLogRelay.Deserialize(stream, instance, num);
		}

		// Token: 0x06001E0B RID: 7691 RVA: 0x00069918 File Offset: 0x00067B18
		public static GameLogRelay Deserialize(Stream stream, GameLogRelay instance, long limit)
		{
			if (instance.Messages == null)
			{
				instance.Messages = new List<LogRelayMessage>();
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
					instance.Messages.Add(LogRelayMessage.DeserializeLengthDelimited(stream));
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

		// Token: 0x06001E0C RID: 7692 RVA: 0x000699B0 File Offset: 0x00067BB0
		public void Serialize(Stream stream)
		{
			GameLogRelay.Serialize(stream, this);
		}

		// Token: 0x06001E0D RID: 7693 RVA: 0x000699BC File Offset: 0x00067BBC
		public static void Serialize(Stream stream, GameLogRelay instance)
		{
			if (instance.Messages.Count > 0)
			{
				foreach (LogRelayMessage logRelayMessage in instance.Messages)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, logRelayMessage.GetSerializedSize());
					LogRelayMessage.Serialize(stream, logRelayMessage);
				}
			}
		}

		// Token: 0x06001E0E RID: 7694 RVA: 0x00069A34 File Offset: 0x00067C34
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.Messages.Count > 0)
			{
				foreach (LogRelayMessage logRelayMessage in this.Messages)
				{
					num += 1U;
					uint serializedSize = logRelayMessage.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num;
		}

		// Token: 0x04000ADA RID: 2778
		private List<LogRelayMessage> _Messages = new List<LogRelayMessage>();

		// Token: 0x02000661 RID: 1633
		public enum PacketID
		{
			// Token: 0x0400215C RID: 8540
			ID = 51
		}
	}
}
