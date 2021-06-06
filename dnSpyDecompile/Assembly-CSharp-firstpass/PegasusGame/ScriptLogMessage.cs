using System;
using System.IO;
using System.Text;

namespace PegasusGame
{
	// Token: 0x020001D5 RID: 469
	public class ScriptLogMessage : IProtoBuf
	{
		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x06001DF2 RID: 7666 RVA: 0x0006948C File Offset: 0x0006768C
		// (set) Token: 0x06001DF3 RID: 7667 RVA: 0x00069494 File Offset: 0x00067694
		public int Severity
		{
			get
			{
				return this._Severity;
			}
			set
			{
				this._Severity = value;
				this.HasSeverity = true;
			}
		}

		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x06001DF4 RID: 7668 RVA: 0x000694A4 File Offset: 0x000676A4
		// (set) Token: 0x06001DF5 RID: 7669 RVA: 0x000694AC File Offset: 0x000676AC
		public string Event
		{
			get
			{
				return this._Event;
			}
			set
			{
				this._Event = value;
				this.HasEvent = (value != null);
			}
		}

		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x06001DF6 RID: 7670 RVA: 0x000694BF File Offset: 0x000676BF
		// (set) Token: 0x06001DF7 RID: 7671 RVA: 0x000694C7 File Offset: 0x000676C7
		public string Message
		{
			get
			{
				return this._Message;
			}
			set
			{
				this._Message = value;
				this.HasMessage = (value != null);
			}
		}

		// Token: 0x06001DF8 RID: 7672 RVA: 0x000694DC File Offset: 0x000676DC
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasSeverity)
			{
				num ^= this.Severity.GetHashCode();
			}
			if (this.HasEvent)
			{
				num ^= this.Event.GetHashCode();
			}
			if (this.HasMessage)
			{
				num ^= this.Message.GetHashCode();
			}
			return num;
		}

		// Token: 0x06001DF9 RID: 7673 RVA: 0x0006953C File Offset: 0x0006773C
		public override bool Equals(object obj)
		{
			ScriptLogMessage scriptLogMessage = obj as ScriptLogMessage;
			return scriptLogMessage != null && this.HasSeverity == scriptLogMessage.HasSeverity && (!this.HasSeverity || this.Severity.Equals(scriptLogMessage.Severity)) && this.HasEvent == scriptLogMessage.HasEvent && (!this.HasEvent || this.Event.Equals(scriptLogMessage.Event)) && this.HasMessage == scriptLogMessage.HasMessage && (!this.HasMessage || this.Message.Equals(scriptLogMessage.Message));
		}

		// Token: 0x06001DFA RID: 7674 RVA: 0x000695DA File Offset: 0x000677DA
		public void Deserialize(Stream stream)
		{
			ScriptLogMessage.Deserialize(stream, this);
		}

		// Token: 0x06001DFB RID: 7675 RVA: 0x000695E4 File Offset: 0x000677E4
		public static ScriptLogMessage Deserialize(Stream stream, ScriptLogMessage instance)
		{
			return ScriptLogMessage.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001DFC RID: 7676 RVA: 0x000695F0 File Offset: 0x000677F0
		public static ScriptLogMessage DeserializeLengthDelimited(Stream stream)
		{
			ScriptLogMessage scriptLogMessage = new ScriptLogMessage();
			ScriptLogMessage.DeserializeLengthDelimited(stream, scriptLogMessage);
			return scriptLogMessage;
		}

		// Token: 0x06001DFD RID: 7677 RVA: 0x0006960C File Offset: 0x0006780C
		public static ScriptLogMessage DeserializeLengthDelimited(Stream stream, ScriptLogMessage instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ScriptLogMessage.Deserialize(stream, instance, num);
		}

		// Token: 0x06001DFE RID: 7678 RVA: 0x00069634 File Offset: 0x00067834
		public static ScriptLogMessage Deserialize(Stream stream, ScriptLogMessage instance, long limit)
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
				else if (num != 8)
				{
					if (num != 18)
					{
						if (num != 26)
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
							instance.Message = ProtocolParser.ReadString(stream);
						}
					}
					else
					{
						instance.Event = ProtocolParser.ReadString(stream);
					}
				}
				else
				{
					instance.Severity = (int)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06001DFF RID: 7679 RVA: 0x000696E2 File Offset: 0x000678E2
		public void Serialize(Stream stream)
		{
			ScriptLogMessage.Serialize(stream, this);
		}

		// Token: 0x06001E00 RID: 7680 RVA: 0x000696EC File Offset: 0x000678EC
		public static void Serialize(Stream stream, ScriptLogMessage instance)
		{
			if (instance.HasSeverity)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Severity));
			}
			if (instance.HasEvent)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Event));
			}
			if (instance.HasMessage)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Message));
			}
		}

		// Token: 0x06001E01 RID: 7681 RVA: 0x00069764 File Offset: 0x00067964
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasSeverity)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Severity));
			}
			if (this.HasEvent)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Event);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasMessage)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.Message);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			return num;
		}

		// Token: 0x04000AD4 RID: 2772
		public bool HasSeverity;

		// Token: 0x04000AD5 RID: 2773
		private int _Severity;

		// Token: 0x04000AD6 RID: 2774
		public bool HasEvent;

		// Token: 0x04000AD7 RID: 2775
		private string _Event;

		// Token: 0x04000AD8 RID: 2776
		public bool HasMessage;

		// Token: 0x04000AD9 RID: 2777
		private string _Message;

		// Token: 0x02000660 RID: 1632
		public enum PacketID
		{
			// Token: 0x0400215A RID: 8538
			ID = 50
		}
	}
}
