using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.connection.v1
{
	// Token: 0x0200043D RID: 1085
	public class ConnectionMeteringContentHandles : IProtoBuf
	{
		// Token: 0x17000DAE RID: 3502
		// (get) Token: 0x06004979 RID: 18809 RVA: 0x000E5776 File Offset: 0x000E3976
		// (set) Token: 0x0600497A RID: 18810 RVA: 0x000E577E File Offset: 0x000E397E
		public List<ContentHandle> ContentHandle
		{
			get
			{
				return this._ContentHandle;
			}
			set
			{
				this._ContentHandle = value;
			}
		}

		// Token: 0x17000DAF RID: 3503
		// (get) Token: 0x0600497B RID: 18811 RVA: 0x000E5776 File Offset: 0x000E3976
		public List<ContentHandle> ContentHandleList
		{
			get
			{
				return this._ContentHandle;
			}
		}

		// Token: 0x17000DB0 RID: 3504
		// (get) Token: 0x0600497C RID: 18812 RVA: 0x000E5787 File Offset: 0x000E3987
		public int ContentHandleCount
		{
			get
			{
				return this._ContentHandle.Count;
			}
		}

		// Token: 0x0600497D RID: 18813 RVA: 0x000E5794 File Offset: 0x000E3994
		public void AddContentHandle(ContentHandle val)
		{
			this._ContentHandle.Add(val);
		}

		// Token: 0x0600497E RID: 18814 RVA: 0x000E57A2 File Offset: 0x000E39A2
		public void ClearContentHandle()
		{
			this._ContentHandle.Clear();
		}

		// Token: 0x0600497F RID: 18815 RVA: 0x000E57AF File Offset: 0x000E39AF
		public void SetContentHandle(List<ContentHandle> val)
		{
			this.ContentHandle = val;
		}

		// Token: 0x06004980 RID: 18816 RVA: 0x000E57B8 File Offset: 0x000E39B8
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (ContentHandle contentHandle in this.ContentHandle)
			{
				num ^= contentHandle.GetHashCode();
			}
			return num;
		}

		// Token: 0x06004981 RID: 18817 RVA: 0x000E581C File Offset: 0x000E3A1C
		public override bool Equals(object obj)
		{
			ConnectionMeteringContentHandles connectionMeteringContentHandles = obj as ConnectionMeteringContentHandles;
			if (connectionMeteringContentHandles == null)
			{
				return false;
			}
			if (this.ContentHandle.Count != connectionMeteringContentHandles.ContentHandle.Count)
			{
				return false;
			}
			for (int i = 0; i < this.ContentHandle.Count; i++)
			{
				if (!this.ContentHandle[i].Equals(connectionMeteringContentHandles.ContentHandle[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000DB1 RID: 3505
		// (get) Token: 0x06004982 RID: 18818 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004983 RID: 18819 RVA: 0x000E5887 File Offset: 0x000E3A87
		public static ConnectionMeteringContentHandles ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ConnectionMeteringContentHandles>(bs, 0, -1);
		}

		// Token: 0x06004984 RID: 18820 RVA: 0x000E5891 File Offset: 0x000E3A91
		public void Deserialize(Stream stream)
		{
			ConnectionMeteringContentHandles.Deserialize(stream, this);
		}

		// Token: 0x06004985 RID: 18821 RVA: 0x000E589B File Offset: 0x000E3A9B
		public static ConnectionMeteringContentHandles Deserialize(Stream stream, ConnectionMeteringContentHandles instance)
		{
			return ConnectionMeteringContentHandles.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004986 RID: 18822 RVA: 0x000E58A8 File Offset: 0x000E3AA8
		public static ConnectionMeteringContentHandles DeserializeLengthDelimited(Stream stream)
		{
			ConnectionMeteringContentHandles connectionMeteringContentHandles = new ConnectionMeteringContentHandles();
			ConnectionMeteringContentHandles.DeserializeLengthDelimited(stream, connectionMeteringContentHandles);
			return connectionMeteringContentHandles;
		}

		// Token: 0x06004987 RID: 18823 RVA: 0x000E58C4 File Offset: 0x000E3AC4
		public static ConnectionMeteringContentHandles DeserializeLengthDelimited(Stream stream, ConnectionMeteringContentHandles instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ConnectionMeteringContentHandles.Deserialize(stream, instance, num);
		}

		// Token: 0x06004988 RID: 18824 RVA: 0x000E58EC File Offset: 0x000E3AEC
		public static ConnectionMeteringContentHandles Deserialize(Stream stream, ConnectionMeteringContentHandles instance, long limit)
		{
			if (instance.ContentHandle == null)
			{
				instance.ContentHandle = new List<ContentHandle>();
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
					instance.ContentHandle.Add(bnet.protocol.ContentHandle.DeserializeLengthDelimited(stream));
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

		// Token: 0x06004989 RID: 18825 RVA: 0x000E5984 File Offset: 0x000E3B84
		public void Serialize(Stream stream)
		{
			ConnectionMeteringContentHandles.Serialize(stream, this);
		}

		// Token: 0x0600498A RID: 18826 RVA: 0x000E5990 File Offset: 0x000E3B90
		public static void Serialize(Stream stream, ConnectionMeteringContentHandles instance)
		{
			if (instance.ContentHandle.Count > 0)
			{
				foreach (ContentHandle contentHandle in instance.ContentHandle)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, contentHandle.GetSerializedSize());
					bnet.protocol.ContentHandle.Serialize(stream, contentHandle);
				}
			}
		}

		// Token: 0x0600498B RID: 18827 RVA: 0x000E5A08 File Offset: 0x000E3C08
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.ContentHandle.Count > 0)
			{
				foreach (ContentHandle contentHandle in this.ContentHandle)
				{
					num += 1U;
					uint serializedSize = contentHandle.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num;
		}

		// Token: 0x04001837 RID: 6199
		private List<ContentHandle> _ContentHandle = new List<ContentHandle>();
	}
}
