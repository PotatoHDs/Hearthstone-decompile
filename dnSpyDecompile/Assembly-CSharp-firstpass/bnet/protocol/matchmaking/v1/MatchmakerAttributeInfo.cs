using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using bnet.protocol.v2;

namespace bnet.protocol.matchmaking.v1
{
	// Token: 0x020003D3 RID: 979
	public class MatchmakerAttributeInfo : IProtoBuf
	{
		// Token: 0x17000BD0 RID: 3024
		// (get) Token: 0x0600402F RID: 16431 RVA: 0x000CCC5A File Offset: 0x000CAE5A
		// (set) Token: 0x06004030 RID: 16432 RVA: 0x000CCC62 File Offset: 0x000CAE62
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				this._Name = value;
				this.HasName = (value != null);
			}
		}

		// Token: 0x06004031 RID: 16433 RVA: 0x000CCC75 File Offset: 0x000CAE75
		public void SetName(string val)
		{
			this.Name = val;
		}

		// Token: 0x17000BD1 RID: 3025
		// (get) Token: 0x06004032 RID: 16434 RVA: 0x000CCC7E File Offset: 0x000CAE7E
		// (set) Token: 0x06004033 RID: 16435 RVA: 0x000CCC86 File Offset: 0x000CAE86
		public uint Program
		{
			get
			{
				return this._Program;
			}
			set
			{
				this._Program = value;
				this.HasProgram = true;
			}
		}

		// Token: 0x06004034 RID: 16436 RVA: 0x000CCC96 File Offset: 0x000CAE96
		public void SetProgram(uint val)
		{
			this.Program = val;
		}

		// Token: 0x17000BD2 RID: 3026
		// (get) Token: 0x06004035 RID: 16437 RVA: 0x000CCC9F File Offset: 0x000CAE9F
		// (set) Token: 0x06004036 RID: 16438 RVA: 0x000CCCA7 File Offset: 0x000CAEA7
		public List<bnet.protocol.v2.Attribute> Attribute
		{
			get
			{
				return this._Attribute;
			}
			set
			{
				this._Attribute = value;
			}
		}

		// Token: 0x17000BD3 RID: 3027
		// (get) Token: 0x06004037 RID: 16439 RVA: 0x000CCC9F File Offset: 0x000CAE9F
		public List<bnet.protocol.v2.Attribute> AttributeList
		{
			get
			{
				return this._Attribute;
			}
		}

		// Token: 0x17000BD4 RID: 3028
		// (get) Token: 0x06004038 RID: 16440 RVA: 0x000CCCB0 File Offset: 0x000CAEB0
		public int AttributeCount
		{
			get
			{
				return this._Attribute.Count;
			}
		}

		// Token: 0x06004039 RID: 16441 RVA: 0x000CCCBD File Offset: 0x000CAEBD
		public void AddAttribute(bnet.protocol.v2.Attribute val)
		{
			this._Attribute.Add(val);
		}

		// Token: 0x0600403A RID: 16442 RVA: 0x000CCCCB File Offset: 0x000CAECB
		public void ClearAttribute()
		{
			this._Attribute.Clear();
		}

		// Token: 0x0600403B RID: 16443 RVA: 0x000CCCD8 File Offset: 0x000CAED8
		public void SetAttribute(List<bnet.protocol.v2.Attribute> val)
		{
			this.Attribute = val;
		}

		// Token: 0x17000BD5 RID: 3029
		// (get) Token: 0x0600403C RID: 16444 RVA: 0x000CCCE1 File Offset: 0x000CAEE1
		// (set) Token: 0x0600403D RID: 16445 RVA: 0x000CCCE9 File Offset: 0x000CAEE9
		public bool IsPrivate
		{
			get
			{
				return this._IsPrivate;
			}
			set
			{
				this._IsPrivate = value;
				this.HasIsPrivate = true;
			}
		}

		// Token: 0x0600403E RID: 16446 RVA: 0x000CCCF9 File Offset: 0x000CAEF9
		public void SetIsPrivate(bool val)
		{
			this.IsPrivate = val;
		}

		// Token: 0x0600403F RID: 16447 RVA: 0x000CCD04 File Offset: 0x000CAF04
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasName)
			{
				num ^= this.Name.GetHashCode();
			}
			if (this.HasProgram)
			{
				num ^= this.Program.GetHashCode();
			}
			foreach (bnet.protocol.v2.Attribute attribute in this.Attribute)
			{
				num ^= attribute.GetHashCode();
			}
			if (this.HasIsPrivate)
			{
				num ^= this.IsPrivate.GetHashCode();
			}
			return num;
		}

		// Token: 0x06004040 RID: 16448 RVA: 0x000CCDB0 File Offset: 0x000CAFB0
		public override bool Equals(object obj)
		{
			MatchmakerAttributeInfo matchmakerAttributeInfo = obj as MatchmakerAttributeInfo;
			if (matchmakerAttributeInfo == null)
			{
				return false;
			}
			if (this.HasName != matchmakerAttributeInfo.HasName || (this.HasName && !this.Name.Equals(matchmakerAttributeInfo.Name)))
			{
				return false;
			}
			if (this.HasProgram != matchmakerAttributeInfo.HasProgram || (this.HasProgram && !this.Program.Equals(matchmakerAttributeInfo.Program)))
			{
				return false;
			}
			if (this.Attribute.Count != matchmakerAttributeInfo.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Attribute.Count; i++)
			{
				if (!this.Attribute[i].Equals(matchmakerAttributeInfo.Attribute[i]))
				{
					return false;
				}
			}
			return this.HasIsPrivate == matchmakerAttributeInfo.HasIsPrivate && (!this.HasIsPrivate || this.IsPrivate.Equals(matchmakerAttributeInfo.IsPrivate));
		}

		// Token: 0x17000BD6 RID: 3030
		// (get) Token: 0x06004041 RID: 16449 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004042 RID: 16450 RVA: 0x000CCEA2 File Offset: 0x000CB0A2
		public static MatchmakerAttributeInfo ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<MatchmakerAttributeInfo>(bs, 0, -1);
		}

		// Token: 0x06004043 RID: 16451 RVA: 0x000CCEAC File Offset: 0x000CB0AC
		public void Deserialize(Stream stream)
		{
			MatchmakerAttributeInfo.Deserialize(stream, this);
		}

		// Token: 0x06004044 RID: 16452 RVA: 0x000CCEB6 File Offset: 0x000CB0B6
		public static MatchmakerAttributeInfo Deserialize(Stream stream, MatchmakerAttributeInfo instance)
		{
			return MatchmakerAttributeInfo.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004045 RID: 16453 RVA: 0x000CCEC4 File Offset: 0x000CB0C4
		public static MatchmakerAttributeInfo DeserializeLengthDelimited(Stream stream)
		{
			MatchmakerAttributeInfo matchmakerAttributeInfo = new MatchmakerAttributeInfo();
			MatchmakerAttributeInfo.DeserializeLengthDelimited(stream, matchmakerAttributeInfo);
			return matchmakerAttributeInfo;
		}

		// Token: 0x06004046 RID: 16454 RVA: 0x000CCEE0 File Offset: 0x000CB0E0
		public static MatchmakerAttributeInfo DeserializeLengthDelimited(Stream stream, MatchmakerAttributeInfo instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return MatchmakerAttributeInfo.Deserialize(stream, instance, num);
		}

		// Token: 0x06004047 RID: 16455 RVA: 0x000CCF08 File Offset: 0x000CB108
		public static MatchmakerAttributeInfo Deserialize(Stream stream, MatchmakerAttributeInfo instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			if (instance.Attribute == null)
			{
				instance.Attribute = new List<bnet.protocol.v2.Attribute>();
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
					if (num <= 21)
					{
						if (num == 10)
						{
							instance.Name = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 21)
						{
							instance.Program = binaryReader.ReadUInt32();
							continue;
						}
					}
					else
					{
						if (num == 26)
						{
							instance.Attribute.Add(bnet.protocol.v2.Attribute.DeserializeLengthDelimited(stream));
							continue;
						}
						if (num == 32)
						{
							instance.IsPrivate = ProtocolParser.ReadBool(stream);
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

		// Token: 0x06004048 RID: 16456 RVA: 0x000CCFF8 File Offset: 0x000CB1F8
		public void Serialize(Stream stream)
		{
			MatchmakerAttributeInfo.Serialize(stream, this);
		}

		// Token: 0x06004049 RID: 16457 RVA: 0x000CD004 File Offset: 0x000CB204
		public static void Serialize(Stream stream, MatchmakerAttributeInfo instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasName)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Name));
			}
			if (instance.HasProgram)
			{
				stream.WriteByte(21);
				binaryWriter.Write(instance.Program);
			}
			if (instance.Attribute.Count > 0)
			{
				foreach (bnet.protocol.v2.Attribute attribute in instance.Attribute)
				{
					stream.WriteByte(26);
					ProtocolParser.WriteUInt32(stream, attribute.GetSerializedSize());
					bnet.protocol.v2.Attribute.Serialize(stream, attribute);
				}
			}
			if (instance.HasIsPrivate)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteBool(stream, instance.IsPrivate);
			}
		}

		// Token: 0x0600404A RID: 16458 RVA: 0x000CD0E0 File Offset: 0x000CB2E0
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasName)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Name);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasProgram)
			{
				num += 1U;
				num += 4U;
			}
			if (this.Attribute.Count > 0)
			{
				foreach (bnet.protocol.v2.Attribute attribute in this.Attribute)
				{
					num += 1U;
					uint serializedSize = attribute.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (this.HasIsPrivate)
			{
				num += 1U;
				num += 1U;
			}
			return num;
		}

		// Token: 0x04001669 RID: 5737
		public bool HasName;

		// Token: 0x0400166A RID: 5738
		private string _Name;

		// Token: 0x0400166B RID: 5739
		public bool HasProgram;

		// Token: 0x0400166C RID: 5740
		private uint _Program;

		// Token: 0x0400166D RID: 5741
		private List<bnet.protocol.v2.Attribute> _Attribute = new List<bnet.protocol.v2.Attribute>();

		// Token: 0x0400166E RID: 5742
		public bool HasIsPrivate;

		// Token: 0x0400166F RID: 5743
		private bool _IsPrivate;
	}
}
