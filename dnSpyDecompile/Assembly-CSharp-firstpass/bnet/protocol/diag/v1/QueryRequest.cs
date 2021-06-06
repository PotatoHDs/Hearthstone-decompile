using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace bnet.protocol.diag.v1
{
	// Token: 0x02000439 RID: 1081
	public class QueryRequest : IProtoBuf
	{
		// Token: 0x17000D99 RID: 3481
		// (get) Token: 0x0600491A RID: 18714 RVA: 0x000E4740 File Offset: 0x000E2940
		// (set) Token: 0x0600491B RID: 18715 RVA: 0x000E4748 File Offset: 0x000E2948
		public string Name { get; set; }

		// Token: 0x0600491C RID: 18716 RVA: 0x000E4751 File Offset: 0x000E2951
		public void SetName(string val)
		{
			this.Name = val;
		}

		// Token: 0x17000D9A RID: 3482
		// (get) Token: 0x0600491D RID: 18717 RVA: 0x000E475A File Offset: 0x000E295A
		// (set) Token: 0x0600491E RID: 18718 RVA: 0x000E4762 File Offset: 0x000E2962
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

		// Token: 0x17000D9B RID: 3483
		// (get) Token: 0x0600491F RID: 18719 RVA: 0x000E475A File Offset: 0x000E295A
		public List<string> ArgsList
		{
			get
			{
				return this._Args;
			}
		}

		// Token: 0x17000D9C RID: 3484
		// (get) Token: 0x06004920 RID: 18720 RVA: 0x000E476B File Offset: 0x000E296B
		public int ArgsCount
		{
			get
			{
				return this._Args.Count;
			}
		}

		// Token: 0x06004921 RID: 18721 RVA: 0x000E4778 File Offset: 0x000E2978
		public void AddArgs(string val)
		{
			this._Args.Add(val);
		}

		// Token: 0x06004922 RID: 18722 RVA: 0x000E4786 File Offset: 0x000E2986
		public void ClearArgs()
		{
			this._Args.Clear();
		}

		// Token: 0x06004923 RID: 18723 RVA: 0x000E4793 File Offset: 0x000E2993
		public void SetArgs(List<string> val)
		{
			this.Args = val;
		}

		// Token: 0x06004924 RID: 18724 RVA: 0x000E479C File Offset: 0x000E299C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Name.GetHashCode();
			foreach (string text in this.Args)
			{
				num ^= text.GetHashCode();
			}
			return num;
		}

		// Token: 0x06004925 RID: 18725 RVA: 0x000E480C File Offset: 0x000E2A0C
		public override bool Equals(object obj)
		{
			QueryRequest queryRequest = obj as QueryRequest;
			if (queryRequest == null)
			{
				return false;
			}
			if (!this.Name.Equals(queryRequest.Name))
			{
				return false;
			}
			if (this.Args.Count != queryRequest.Args.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Args.Count; i++)
			{
				if (!this.Args[i].Equals(queryRequest.Args[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000D9D RID: 3485
		// (get) Token: 0x06004926 RID: 18726 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004927 RID: 18727 RVA: 0x000E488C File Offset: 0x000E2A8C
		public static QueryRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<QueryRequest>(bs, 0, -1);
		}

		// Token: 0x06004928 RID: 18728 RVA: 0x000E4896 File Offset: 0x000E2A96
		public void Deserialize(Stream stream)
		{
			QueryRequest.Deserialize(stream, this);
		}

		// Token: 0x06004929 RID: 18729 RVA: 0x000E48A0 File Offset: 0x000E2AA0
		public static QueryRequest Deserialize(Stream stream, QueryRequest instance)
		{
			return QueryRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600492A RID: 18730 RVA: 0x000E48AC File Offset: 0x000E2AAC
		public static QueryRequest DeserializeLengthDelimited(Stream stream)
		{
			QueryRequest queryRequest = new QueryRequest();
			QueryRequest.DeserializeLengthDelimited(stream, queryRequest);
			return queryRequest;
		}

		// Token: 0x0600492B RID: 18731 RVA: 0x000E48C8 File Offset: 0x000E2AC8
		public static QueryRequest DeserializeLengthDelimited(Stream stream, QueryRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return QueryRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x0600492C RID: 18732 RVA: 0x000E48F0 File Offset: 0x000E2AF0
		public static QueryRequest Deserialize(Stream stream, QueryRequest instance, long limit)
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
					instance.Name = ProtocolParser.ReadString(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600492D RID: 18733 RVA: 0x000E49A0 File Offset: 0x000E2BA0
		public void Serialize(Stream stream)
		{
			QueryRequest.Serialize(stream, this);
		}

		// Token: 0x0600492E RID: 18734 RVA: 0x000E49AC File Offset: 0x000E2BAC
		public static void Serialize(Stream stream, QueryRequest instance)
		{
			if (instance.Name == null)
			{
				throw new ArgumentNullException("Name", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Name));
			if (instance.Args.Count > 0)
			{
				foreach (string s in instance.Args)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(s));
				}
			}
		}

		// Token: 0x0600492F RID: 18735 RVA: 0x000E4A58 File Offset: 0x000E2C58
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Name);
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

		// Token: 0x0400182C RID: 6188
		private List<string> _Args = new List<string>();
	}
}
