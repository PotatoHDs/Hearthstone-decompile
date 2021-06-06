using System;
using System.Collections.Generic;
using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	// Token: 0x02000118 RID: 280
	public class UtilLogRelay : IProtoBuf
	{
		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06001286 RID: 4742 RVA: 0x000410CC File Offset: 0x0003F2CC
		// (set) Token: 0x06001287 RID: 4743 RVA: 0x000410D4 File Offset: 0x0003F2D4
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

		// Token: 0x06001288 RID: 4744 RVA: 0x000410E0 File Offset: 0x0003F2E0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (LogRelayMessage logRelayMessage in this.Messages)
			{
				num ^= logRelayMessage.GetHashCode();
			}
			return num;
		}

		// Token: 0x06001289 RID: 4745 RVA: 0x00041144 File Offset: 0x0003F344
		public override bool Equals(object obj)
		{
			UtilLogRelay utilLogRelay = obj as UtilLogRelay;
			if (utilLogRelay == null)
			{
				return false;
			}
			if (this.Messages.Count != utilLogRelay.Messages.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Messages.Count; i++)
			{
				if (!this.Messages[i].Equals(utilLogRelay.Messages[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600128A RID: 4746 RVA: 0x000411AF File Offset: 0x0003F3AF
		public void Deserialize(Stream stream)
		{
			UtilLogRelay.Deserialize(stream, this);
		}

		// Token: 0x0600128B RID: 4747 RVA: 0x000411B9 File Offset: 0x0003F3B9
		public static UtilLogRelay Deserialize(Stream stream, UtilLogRelay instance)
		{
			return UtilLogRelay.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600128C RID: 4748 RVA: 0x000411C4 File Offset: 0x0003F3C4
		public static UtilLogRelay DeserializeLengthDelimited(Stream stream)
		{
			UtilLogRelay utilLogRelay = new UtilLogRelay();
			UtilLogRelay.DeserializeLengthDelimited(stream, utilLogRelay);
			return utilLogRelay;
		}

		// Token: 0x0600128D RID: 4749 RVA: 0x000411E0 File Offset: 0x0003F3E0
		public static UtilLogRelay DeserializeLengthDelimited(Stream stream, UtilLogRelay instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return UtilLogRelay.Deserialize(stream, instance, num);
		}

		// Token: 0x0600128E RID: 4750 RVA: 0x00041208 File Offset: 0x0003F408
		public static UtilLogRelay Deserialize(Stream stream, UtilLogRelay instance, long limit)
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

		// Token: 0x0600128F RID: 4751 RVA: 0x000412A0 File Offset: 0x0003F4A0
		public void Serialize(Stream stream)
		{
			UtilLogRelay.Serialize(stream, this);
		}

		// Token: 0x06001290 RID: 4752 RVA: 0x000412AC File Offset: 0x0003F4AC
		public static void Serialize(Stream stream, UtilLogRelay instance)
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

		// Token: 0x06001291 RID: 4753 RVA: 0x00041324 File Offset: 0x0003F524
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

		// Token: 0x040005B6 RID: 1462
		private List<LogRelayMessage> _Messages = new List<LogRelayMessage>();

		// Token: 0x02000616 RID: 1558
		public enum PacketID
		{
			// Token: 0x0400207A RID: 8314
			ID = 390
		}
	}
}
