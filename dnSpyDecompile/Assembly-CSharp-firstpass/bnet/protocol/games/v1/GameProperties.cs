using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.games.v1
{
	// Token: 0x02000370 RID: 880
	public class GameProperties : IProtoBuf
	{
		// Token: 0x17000A24 RID: 2596
		// (get) Token: 0x060037BF RID: 14271 RVA: 0x000B6EB9 File Offset: 0x000B50B9
		// (set) Token: 0x060037C0 RID: 14272 RVA: 0x000B6EC1 File Offset: 0x000B50C1
		public List<Attribute> CreationAttributes
		{
			get
			{
				return this._CreationAttributes;
			}
			set
			{
				this._CreationAttributes = value;
			}
		}

		// Token: 0x17000A25 RID: 2597
		// (get) Token: 0x060037C1 RID: 14273 RVA: 0x000B6EB9 File Offset: 0x000B50B9
		public List<Attribute> CreationAttributesList
		{
			get
			{
				return this._CreationAttributes;
			}
		}

		// Token: 0x17000A26 RID: 2598
		// (get) Token: 0x060037C2 RID: 14274 RVA: 0x000B6ECA File Offset: 0x000B50CA
		public int CreationAttributesCount
		{
			get
			{
				return this._CreationAttributes.Count;
			}
		}

		// Token: 0x060037C3 RID: 14275 RVA: 0x000B6ED7 File Offset: 0x000B50D7
		public void AddCreationAttributes(Attribute val)
		{
			this._CreationAttributes.Add(val);
		}

		// Token: 0x060037C4 RID: 14276 RVA: 0x000B6EE5 File Offset: 0x000B50E5
		public void ClearCreationAttributes()
		{
			this._CreationAttributes.Clear();
		}

		// Token: 0x060037C5 RID: 14277 RVA: 0x000B6EF2 File Offset: 0x000B50F2
		public void SetCreationAttributes(List<Attribute> val)
		{
			this.CreationAttributes = val;
		}

		// Token: 0x17000A27 RID: 2599
		// (get) Token: 0x060037C6 RID: 14278 RVA: 0x000B6EFB File Offset: 0x000B50FB
		// (set) Token: 0x060037C7 RID: 14279 RVA: 0x000B6F03 File Offset: 0x000B5103
		public AttributeFilter Filter
		{
			get
			{
				return this._Filter;
			}
			set
			{
				this._Filter = value;
				this.HasFilter = (value != null);
			}
		}

		// Token: 0x060037C8 RID: 14280 RVA: 0x000B6F16 File Offset: 0x000B5116
		public void SetFilter(AttributeFilter val)
		{
			this.Filter = val;
		}

		// Token: 0x17000A28 RID: 2600
		// (get) Token: 0x060037C9 RID: 14281 RVA: 0x000B6F1F File Offset: 0x000B511F
		// (set) Token: 0x060037CA RID: 14282 RVA: 0x000B6F27 File Offset: 0x000B5127
		public bool Create
		{
			get
			{
				return this._Create;
			}
			set
			{
				this._Create = value;
				this.HasCreate = true;
			}
		}

		// Token: 0x060037CB RID: 14283 RVA: 0x000B6F37 File Offset: 0x000B5137
		public void SetCreate(bool val)
		{
			this.Create = val;
		}

		// Token: 0x17000A29 RID: 2601
		// (get) Token: 0x060037CC RID: 14284 RVA: 0x000B6F40 File Offset: 0x000B5140
		// (set) Token: 0x060037CD RID: 14285 RVA: 0x000B6F48 File Offset: 0x000B5148
		public bool Open
		{
			get
			{
				return this._Open;
			}
			set
			{
				this._Open = value;
				this.HasOpen = true;
			}
		}

		// Token: 0x060037CE RID: 14286 RVA: 0x000B6F58 File Offset: 0x000B5158
		public void SetOpen(bool val)
		{
			this.Open = val;
		}

		// Token: 0x17000A2A RID: 2602
		// (get) Token: 0x060037CF RID: 14287 RVA: 0x000B6F61 File Offset: 0x000B5161
		// (set) Token: 0x060037D0 RID: 14288 RVA: 0x000B6F69 File Offset: 0x000B5169
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

		// Token: 0x060037D1 RID: 14289 RVA: 0x000B6F79 File Offset: 0x000B5179
		public void SetProgram(uint val)
		{
			this.Program = val;
		}

		// Token: 0x060037D2 RID: 14290 RVA: 0x000B6F84 File Offset: 0x000B5184
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (Attribute attribute in this.CreationAttributes)
			{
				num ^= attribute.GetHashCode();
			}
			if (this.HasFilter)
			{
				num ^= this.Filter.GetHashCode();
			}
			if (this.HasCreate)
			{
				num ^= this.Create.GetHashCode();
			}
			if (this.HasOpen)
			{
				num ^= this.Open.GetHashCode();
			}
			if (this.HasProgram)
			{
				num ^= this.Program.GetHashCode();
			}
			return num;
		}

		// Token: 0x060037D3 RID: 14291 RVA: 0x000B7048 File Offset: 0x000B5248
		public override bool Equals(object obj)
		{
			GameProperties gameProperties = obj as GameProperties;
			if (gameProperties == null)
			{
				return false;
			}
			if (this.CreationAttributes.Count != gameProperties.CreationAttributes.Count)
			{
				return false;
			}
			for (int i = 0; i < this.CreationAttributes.Count; i++)
			{
				if (!this.CreationAttributes[i].Equals(gameProperties.CreationAttributes[i]))
				{
					return false;
				}
			}
			return this.HasFilter == gameProperties.HasFilter && (!this.HasFilter || this.Filter.Equals(gameProperties.Filter)) && this.HasCreate == gameProperties.HasCreate && (!this.HasCreate || this.Create.Equals(gameProperties.Create)) && this.HasOpen == gameProperties.HasOpen && (!this.HasOpen || this.Open.Equals(gameProperties.Open)) && this.HasProgram == gameProperties.HasProgram && (!this.HasProgram || this.Program.Equals(gameProperties.Program));
		}

		// Token: 0x17000A2B RID: 2603
		// (get) Token: 0x060037D4 RID: 14292 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060037D5 RID: 14293 RVA: 0x000B7168 File Offset: 0x000B5368
		public static GameProperties ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameProperties>(bs, 0, -1);
		}

		// Token: 0x060037D6 RID: 14294 RVA: 0x000B7172 File Offset: 0x000B5372
		public void Deserialize(Stream stream)
		{
			GameProperties.Deserialize(stream, this);
		}

		// Token: 0x060037D7 RID: 14295 RVA: 0x000B717C File Offset: 0x000B537C
		public static GameProperties Deserialize(Stream stream, GameProperties instance)
		{
			return GameProperties.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060037D8 RID: 14296 RVA: 0x000B7188 File Offset: 0x000B5388
		public static GameProperties DeserializeLengthDelimited(Stream stream)
		{
			GameProperties gameProperties = new GameProperties();
			GameProperties.DeserializeLengthDelimited(stream, gameProperties);
			return gameProperties;
		}

		// Token: 0x060037D9 RID: 14297 RVA: 0x000B71A4 File Offset: 0x000B53A4
		public static GameProperties DeserializeLengthDelimited(Stream stream, GameProperties instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GameProperties.Deserialize(stream, instance, num);
		}

		// Token: 0x060037DA RID: 14298 RVA: 0x000B71CC File Offset: 0x000B53CC
		public static GameProperties Deserialize(Stream stream, GameProperties instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			if (instance.CreationAttributes == null)
			{
				instance.CreationAttributes = new List<Attribute>();
			}
			instance.Create = false;
			instance.Open = true;
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
						if (num == 10)
						{
							instance.CreationAttributes.Add(Attribute.DeserializeLengthDelimited(stream));
							continue;
						}
						if (num == 18)
						{
							if (instance.Filter == null)
							{
								instance.Filter = AttributeFilter.DeserializeLengthDelimited(stream);
								continue;
							}
							AttributeFilter.DeserializeLengthDelimited(stream, instance.Filter);
							continue;
						}
					}
					else
					{
						if (num == 24)
						{
							instance.Create = ProtocolParser.ReadBool(stream);
							continue;
						}
						if (num == 32)
						{
							instance.Open = ProtocolParser.ReadBool(stream);
							continue;
						}
						if (num == 45)
						{
							instance.Program = binaryReader.ReadUInt32();
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

		// Token: 0x060037DB RID: 14299 RVA: 0x000B7300 File Offset: 0x000B5500
		public void Serialize(Stream stream)
		{
			GameProperties.Serialize(stream, this);
		}

		// Token: 0x060037DC RID: 14300 RVA: 0x000B730C File Offset: 0x000B550C
		public static void Serialize(Stream stream, GameProperties instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.CreationAttributes.Count > 0)
			{
				foreach (Attribute attribute in instance.CreationAttributes)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, attribute.GetSerializedSize());
					Attribute.Serialize(stream, attribute);
				}
			}
			if (instance.HasFilter)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.Filter.GetSerializedSize());
				AttributeFilter.Serialize(stream, instance.Filter);
			}
			if (instance.HasCreate)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteBool(stream, instance.Create);
			}
			if (instance.HasOpen)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteBool(stream, instance.Open);
			}
			if (instance.HasProgram)
			{
				stream.WriteByte(45);
				binaryWriter.Write(instance.Program);
			}
		}

		// Token: 0x060037DD RID: 14301 RVA: 0x000B740C File Offset: 0x000B560C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.CreationAttributes.Count > 0)
			{
				foreach (Attribute attribute in this.CreationAttributes)
				{
					num += 1U;
					uint serializedSize = attribute.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (this.HasFilter)
			{
				num += 1U;
				uint serializedSize2 = this.Filter.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasCreate)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasOpen)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasProgram)
			{
				num += 1U;
				num += 4U;
			}
			return num;
		}

		// Token: 0x040014D5 RID: 5333
		private List<Attribute> _CreationAttributes = new List<Attribute>();

		// Token: 0x040014D6 RID: 5334
		public bool HasFilter;

		// Token: 0x040014D7 RID: 5335
		private AttributeFilter _Filter;

		// Token: 0x040014D8 RID: 5336
		public bool HasCreate;

		// Token: 0x040014D9 RID: 5337
		private bool _Create;

		// Token: 0x040014DA RID: 5338
		public bool HasOpen;

		// Token: 0x040014DB RID: 5339
		private bool _Open;

		// Token: 0x040014DC RID: 5340
		public bool HasProgram;

		// Token: 0x040014DD RID: 5341
		private uint _Program;
	}
}
