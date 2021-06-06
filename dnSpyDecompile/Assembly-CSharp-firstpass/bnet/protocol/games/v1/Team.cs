using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.games.v1
{
	// Token: 0x020003A6 RID: 934
	public class Team : IProtoBuf
	{
		// Token: 0x17000B19 RID: 2841
		// (get) Token: 0x06003C88 RID: 15496 RVA: 0x000C3544 File Offset: 0x000C1744
		// (set) Token: 0x06003C89 RID: 15497 RVA: 0x000C354C File Offset: 0x000C174C
		public uint MaxPlayers { get; set; }

		// Token: 0x06003C8A RID: 15498 RVA: 0x000C3555 File Offset: 0x000C1755
		public void SetMaxPlayers(uint val)
		{
			this.MaxPlayers = val;
		}

		// Token: 0x17000B1A RID: 2842
		// (get) Token: 0x06003C8B RID: 15499 RVA: 0x000C355E File Offset: 0x000C175E
		// (set) Token: 0x06003C8C RID: 15500 RVA: 0x000C3566 File Offset: 0x000C1766
		public List<Attribute> Attribute
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

		// Token: 0x17000B1B RID: 2843
		// (get) Token: 0x06003C8D RID: 15501 RVA: 0x000C355E File Offset: 0x000C175E
		public List<Attribute> AttributeList
		{
			get
			{
				return this._Attribute;
			}
		}

		// Token: 0x17000B1C RID: 2844
		// (get) Token: 0x06003C8E RID: 15502 RVA: 0x000C356F File Offset: 0x000C176F
		public int AttributeCount
		{
			get
			{
				return this._Attribute.Count;
			}
		}

		// Token: 0x06003C8F RID: 15503 RVA: 0x000C357C File Offset: 0x000C177C
		public void AddAttribute(Attribute val)
		{
			this._Attribute.Add(val);
		}

		// Token: 0x06003C90 RID: 15504 RVA: 0x000C358A File Offset: 0x000C178A
		public void ClearAttribute()
		{
			this._Attribute.Clear();
		}

		// Token: 0x06003C91 RID: 15505 RVA: 0x000C3597 File Offset: 0x000C1797
		public void SetAttribute(List<Attribute> val)
		{
			this.Attribute = val;
		}

		// Token: 0x06003C92 RID: 15506 RVA: 0x000C35A0 File Offset: 0x000C17A0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.MaxPlayers.GetHashCode();
			foreach (Attribute attribute in this.Attribute)
			{
				num ^= attribute.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003C93 RID: 15507 RVA: 0x000C3614 File Offset: 0x000C1814
		public override bool Equals(object obj)
		{
			Team team = obj as Team;
			if (team == null)
			{
				return false;
			}
			if (!this.MaxPlayers.Equals(team.MaxPlayers))
			{
				return false;
			}
			if (this.Attribute.Count != team.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Attribute.Count; i++)
			{
				if (!this.Attribute[i].Equals(team.Attribute[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000B1D RID: 2845
		// (get) Token: 0x06003C94 RID: 15508 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003C95 RID: 15509 RVA: 0x000C3697 File Offset: 0x000C1897
		public static Team ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<Team>(bs, 0, -1);
		}

		// Token: 0x06003C96 RID: 15510 RVA: 0x000C36A1 File Offset: 0x000C18A1
		public void Deserialize(Stream stream)
		{
			Team.Deserialize(stream, this);
		}

		// Token: 0x06003C97 RID: 15511 RVA: 0x000C36AB File Offset: 0x000C18AB
		public static Team Deserialize(Stream stream, Team instance)
		{
			return Team.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003C98 RID: 15512 RVA: 0x000C36B8 File Offset: 0x000C18B8
		public static Team DeserializeLengthDelimited(Stream stream)
		{
			Team team = new Team();
			Team.DeserializeLengthDelimited(stream, team);
			return team;
		}

		// Token: 0x06003C99 RID: 15513 RVA: 0x000C36D4 File Offset: 0x000C18D4
		public static Team DeserializeLengthDelimited(Stream stream, Team instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return Team.Deserialize(stream, instance, num);
		}

		// Token: 0x06003C9A RID: 15514 RVA: 0x000C36FC File Offset: 0x000C18FC
		public static Team Deserialize(Stream stream, Team instance, long limit)
		{
			if (instance.Attribute == null)
			{
				instance.Attribute = new List<Attribute>();
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
				else if (num != 8)
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
						instance.Attribute.Add(bnet.protocol.Attribute.DeserializeLengthDelimited(stream));
					}
				}
				else
				{
					instance.MaxPlayers = ProtocolParser.ReadUInt32(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06003C9B RID: 15515 RVA: 0x000C37AB File Offset: 0x000C19AB
		public void Serialize(Stream stream)
		{
			Team.Serialize(stream, this);
		}

		// Token: 0x06003C9C RID: 15516 RVA: 0x000C37B4 File Offset: 0x000C19B4
		public static void Serialize(Stream stream, Team instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt32(stream, instance.MaxPlayers);
			if (instance.Attribute.Count > 0)
			{
				foreach (Attribute attribute in instance.Attribute)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, attribute.GetSerializedSize());
					bnet.protocol.Attribute.Serialize(stream, attribute);
				}
			}
		}

		// Token: 0x06003C9D RID: 15517 RVA: 0x000C383C File Offset: 0x000C1A3C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt32(this.MaxPlayers);
			if (this.Attribute.Count > 0)
			{
				foreach (Attribute attribute in this.Attribute)
				{
					num += 1U;
					uint serializedSize = attribute.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			num += 1U;
			return num;
		}

		// Token: 0x040015C1 RID: 5569
		private List<Attribute> _Attribute = new List<Attribute>();
	}
}
