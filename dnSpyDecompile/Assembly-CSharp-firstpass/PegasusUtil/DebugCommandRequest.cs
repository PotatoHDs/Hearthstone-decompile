using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PegasusUtil
{
	// Token: 0x02000077 RID: 119
	public class DebugCommandRequest : IProtoBuf
	{
		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000781 RID: 1921 RVA: 0x0001BA3B File Offset: 0x00019C3B
		// (set) Token: 0x06000782 RID: 1922 RVA: 0x0001BA43 File Offset: 0x00019C43
		public string Command { get; set; }

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000783 RID: 1923 RVA: 0x0001BA4C File Offset: 0x00019C4C
		// (set) Token: 0x06000784 RID: 1924 RVA: 0x0001BA54 File Offset: 0x00019C54
		public List<string> Args
		{
			get
			{
				return this._Args;
			}
			set
			{
				this._Args = value;
			}
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x0001BA60 File Offset: 0x00019C60
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Command.GetHashCode();
			foreach (string text in this.Args)
			{
				num ^= text.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x0001BAD0 File Offset: 0x00019CD0
		public override bool Equals(object obj)
		{
			DebugCommandRequest debugCommandRequest = obj as DebugCommandRequest;
			if (debugCommandRequest == null)
			{
				return false;
			}
			if (!this.Command.Equals(debugCommandRequest.Command))
			{
				return false;
			}
			if (this.Args.Count != debugCommandRequest.Args.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Args.Count; i++)
			{
				if (!this.Args[i].Equals(debugCommandRequest.Args[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x0001BB50 File Offset: 0x00019D50
		public void Deserialize(Stream stream)
		{
			DebugCommandRequest.Deserialize(stream, this);
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x0001BB5A File Offset: 0x00019D5A
		public static DebugCommandRequest Deserialize(Stream stream, DebugCommandRequest instance)
		{
			return DebugCommandRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x0001BB68 File Offset: 0x00019D68
		public static DebugCommandRequest DeserializeLengthDelimited(Stream stream)
		{
			DebugCommandRequest debugCommandRequest = new DebugCommandRequest();
			DebugCommandRequest.DeserializeLengthDelimited(stream, debugCommandRequest);
			return debugCommandRequest;
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x0001BB84 File Offset: 0x00019D84
		public static DebugCommandRequest DeserializeLengthDelimited(Stream stream, DebugCommandRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return DebugCommandRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x0001BBAC File Offset: 0x00019DAC
		public static DebugCommandRequest Deserialize(Stream stream, DebugCommandRequest instance, long limit)
		{
			if (instance.Args == null)
			{
				instance.Args = new List<string>();
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
				else if (num != 10)
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
					else
					{
						instance.Args.Add(ProtocolParser.ReadString(stream));
					}
				}
				else
				{
					instance.Command = ProtocolParser.ReadString(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x0001BC5C File Offset: 0x00019E5C
		public void Serialize(Stream stream)
		{
			DebugCommandRequest.Serialize(stream, this);
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x0001BC68 File Offset: 0x00019E68
		public static void Serialize(Stream stream, DebugCommandRequest instance)
		{
			if (instance.Command == null)
			{
				throw new ArgumentNullException("Command", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Command));
			if (instance.Args.Count > 0)
			{
				foreach (string s in instance.Args)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(s));
				}
			}
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x0001BD14 File Offset: 0x00019F14
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Command);
			num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			if (this.Args.Count > 0)
			{
				foreach (string s in this.Args)
				{
					num += 1U;
					uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(s);
					num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
				}
			}
			num += 1U;
			return num;
		}

		// Token: 0x04000261 RID: 609
		private List<string> _Args = new List<string>();

		// Token: 0x0200058A RID: 1418
		public enum PacketID
		{
			// Token: 0x04001EFB RID: 7931
			ID = 323,
			// Token: 0x04001EFC RID: 7932
			System = 0
		}
	}
}
