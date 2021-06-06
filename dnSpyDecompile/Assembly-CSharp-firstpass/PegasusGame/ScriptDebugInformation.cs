using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PegasusGame
{
	// Token: 0x020001B5 RID: 437
	public class ScriptDebugInformation : IProtoBuf
	{
		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x06001BB8 RID: 7096 RVA: 0x00061EBF File Offset: 0x000600BF
		// (set) Token: 0x06001BB9 RID: 7097 RVA: 0x00061EC7 File Offset: 0x000600C7
		public int EntityID { get; set; }

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x06001BBA RID: 7098 RVA: 0x00061ED0 File Offset: 0x000600D0
		// (set) Token: 0x06001BBB RID: 7099 RVA: 0x00061ED8 File Offset: 0x000600D8
		public string EntityName { get; set; }

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x06001BBC RID: 7100 RVA: 0x00061EE1 File Offset: 0x000600E1
		// (set) Token: 0x06001BBD RID: 7101 RVA: 0x00061EE9 File Offset: 0x000600E9
		public string PowerGUID { get; set; }

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x06001BBE RID: 7102 RVA: 0x00061EF2 File Offset: 0x000600F2
		// (set) Token: 0x06001BBF RID: 7103 RVA: 0x00061EFA File Offset: 0x000600FA
		public List<ScriptDebugCall> Calls
		{
			get
			{
				return this._Calls;
			}
			set
			{
				this._Calls = value;
			}
		}

		// Token: 0x06001BC0 RID: 7104 RVA: 0x00061F04 File Offset: 0x00060104
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.EntityID.GetHashCode();
			num ^= this.EntityName.GetHashCode();
			num ^= this.PowerGUID.GetHashCode();
			foreach (ScriptDebugCall scriptDebugCall in this.Calls)
			{
				num ^= scriptDebugCall.GetHashCode();
			}
			return num;
		}

		// Token: 0x06001BC1 RID: 7105 RVA: 0x00061F94 File Offset: 0x00060194
		public override bool Equals(object obj)
		{
			ScriptDebugInformation scriptDebugInformation = obj as ScriptDebugInformation;
			if (scriptDebugInformation == null)
			{
				return false;
			}
			if (!this.EntityID.Equals(scriptDebugInformation.EntityID))
			{
				return false;
			}
			if (!this.EntityName.Equals(scriptDebugInformation.EntityName))
			{
				return false;
			}
			if (!this.PowerGUID.Equals(scriptDebugInformation.PowerGUID))
			{
				return false;
			}
			if (this.Calls.Count != scriptDebugInformation.Calls.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Calls.Count; i++)
			{
				if (!this.Calls[i].Equals(scriptDebugInformation.Calls[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001BC2 RID: 7106 RVA: 0x00062041 File Offset: 0x00060241
		public void Deserialize(Stream stream)
		{
			ScriptDebugInformation.Deserialize(stream, this);
		}

		// Token: 0x06001BC3 RID: 7107 RVA: 0x0006204B File Offset: 0x0006024B
		public static ScriptDebugInformation Deserialize(Stream stream, ScriptDebugInformation instance)
		{
			return ScriptDebugInformation.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001BC4 RID: 7108 RVA: 0x00062058 File Offset: 0x00060258
		public static ScriptDebugInformation DeserializeLengthDelimited(Stream stream)
		{
			ScriptDebugInformation scriptDebugInformation = new ScriptDebugInformation();
			ScriptDebugInformation.DeserializeLengthDelimited(stream, scriptDebugInformation);
			return scriptDebugInformation;
		}

		// Token: 0x06001BC5 RID: 7109 RVA: 0x00062074 File Offset: 0x00060274
		public static ScriptDebugInformation DeserializeLengthDelimited(Stream stream, ScriptDebugInformation instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ScriptDebugInformation.Deserialize(stream, instance, num);
		}

		// Token: 0x06001BC6 RID: 7110 RVA: 0x0006209C File Offset: 0x0006029C
		public static ScriptDebugInformation Deserialize(Stream stream, ScriptDebugInformation instance, long limit)
		{
			if (instance.Calls == null)
			{
				instance.Calls = new List<ScriptDebugCall>();
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
				else
				{
					if (num <= 18)
					{
						if (num == 8)
						{
							instance.EntityID = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 18)
						{
							instance.EntityName = ProtocolParser.ReadString(stream);
							continue;
						}
					}
					else
					{
						if (num == 26)
						{
							instance.PowerGUID = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 34)
						{
							instance.Calls.Add(ScriptDebugCall.DeserializeLengthDelimited(stream));
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

		// Token: 0x06001BC7 RID: 7111 RVA: 0x00062185 File Offset: 0x00060385
		public void Serialize(Stream stream)
		{
			ScriptDebugInformation.Serialize(stream, this);
		}

		// Token: 0x06001BC8 RID: 7112 RVA: 0x00062190 File Offset: 0x00060390
		public static void Serialize(Stream stream, ScriptDebugInformation instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.EntityID));
			if (instance.EntityName == null)
			{
				throw new ArgumentNullException("EntityName", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.EntityName));
			if (instance.PowerGUID == null)
			{
				throw new ArgumentNullException("PowerGUID", "Required by proto specification.");
			}
			stream.WriteByte(26);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.PowerGUID));
			if (instance.Calls.Count > 0)
			{
				foreach (ScriptDebugCall scriptDebugCall in instance.Calls)
				{
					stream.WriteByte(34);
					ProtocolParser.WriteUInt32(stream, scriptDebugCall.GetSerializedSize());
					ScriptDebugCall.Serialize(stream, scriptDebugCall);
				}
			}
		}

		// Token: 0x06001BC9 RID: 7113 RVA: 0x00062288 File Offset: 0x00060488
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.EntityID));
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.EntityName);
			num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.PowerGUID);
			num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			if (this.Calls.Count > 0)
			{
				foreach (ScriptDebugCall scriptDebugCall in this.Calls)
				{
					num += 1U;
					uint serializedSize = scriptDebugCall.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			num += 3U;
			return num;
		}

		// Token: 0x04000A3B RID: 2619
		private List<ScriptDebugCall> _Calls = new List<ScriptDebugCall>();

		// Token: 0x0200064C RID: 1612
		public enum PacketID
		{
			// Token: 0x0400210C RID: 8460
			ID = 7
		}
	}
}
