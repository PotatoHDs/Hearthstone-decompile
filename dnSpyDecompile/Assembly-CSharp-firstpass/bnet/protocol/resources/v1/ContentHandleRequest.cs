using System;
using System.IO;

namespace bnet.protocol.resources.v1
{
	// Token: 0x0200031E RID: 798
	public class ContentHandleRequest : IProtoBuf
	{
		// Token: 0x170008BD RID: 2237
		// (get) Token: 0x060030C8 RID: 12488 RVA: 0x000A4596 File Offset: 0x000A2796
		// (set) Token: 0x060030C9 RID: 12489 RVA: 0x000A459E File Offset: 0x000A279E
		public uint Program { get; set; }

		// Token: 0x060030CA RID: 12490 RVA: 0x000A45A7 File Offset: 0x000A27A7
		public void SetProgram(uint val)
		{
			this.Program = val;
		}

		// Token: 0x170008BE RID: 2238
		// (get) Token: 0x060030CB RID: 12491 RVA: 0x000A45B0 File Offset: 0x000A27B0
		// (set) Token: 0x060030CC RID: 12492 RVA: 0x000A45B8 File Offset: 0x000A27B8
		public uint Stream { get; set; }

		// Token: 0x060030CD RID: 12493 RVA: 0x000A45C1 File Offset: 0x000A27C1
		public void SetStream(uint val)
		{
			this.Stream = val;
		}

		// Token: 0x170008BF RID: 2239
		// (get) Token: 0x060030CE RID: 12494 RVA: 0x000A45CA File Offset: 0x000A27CA
		// (set) Token: 0x060030CF RID: 12495 RVA: 0x000A45D2 File Offset: 0x000A27D2
		public uint Version
		{
			get
			{
				return this._Version;
			}
			set
			{
				this._Version = value;
				this.HasVersion = true;
			}
		}

		// Token: 0x060030D0 RID: 12496 RVA: 0x000A45E2 File Offset: 0x000A27E2
		public void SetVersion(uint val)
		{
			this.Version = val;
		}

		// Token: 0x060030D1 RID: 12497 RVA: 0x000A45EC File Offset: 0x000A27EC
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Program.GetHashCode();
			num ^= this.Stream.GetHashCode();
			if (this.HasVersion)
			{
				num ^= this.Version.GetHashCode();
			}
			return num;
		}

		// Token: 0x060030D2 RID: 12498 RVA: 0x000A4644 File Offset: 0x000A2844
		public override bool Equals(object obj)
		{
			ContentHandleRequest contentHandleRequest = obj as ContentHandleRequest;
			return contentHandleRequest != null && this.Program.Equals(contentHandleRequest.Program) && this.Stream.Equals(contentHandleRequest.Stream) && this.HasVersion == contentHandleRequest.HasVersion && (!this.HasVersion || this.Version.Equals(contentHandleRequest.Version));
		}

		// Token: 0x170008C0 RID: 2240
		// (get) Token: 0x060030D3 RID: 12499 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060030D4 RID: 12500 RVA: 0x000A46BC File Offset: 0x000A28BC
		public static ContentHandleRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ContentHandleRequest>(bs, 0, -1);
		}

		// Token: 0x060030D5 RID: 12501 RVA: 0x000A46C6 File Offset: 0x000A28C6
		public void Deserialize(Stream stream)
		{
			ContentHandleRequest.Deserialize(stream, this);
		}

		// Token: 0x060030D6 RID: 12502 RVA: 0x000A46D0 File Offset: 0x000A28D0
		public static ContentHandleRequest Deserialize(Stream stream, ContentHandleRequest instance)
		{
			return ContentHandleRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060030D7 RID: 12503 RVA: 0x000A46DC File Offset: 0x000A28DC
		public static ContentHandleRequest DeserializeLengthDelimited(Stream stream)
		{
			ContentHandleRequest contentHandleRequest = new ContentHandleRequest();
			ContentHandleRequest.DeserializeLengthDelimited(stream, contentHandleRequest);
			return contentHandleRequest;
		}

		// Token: 0x060030D8 RID: 12504 RVA: 0x000A46F8 File Offset: 0x000A28F8
		public static ContentHandleRequest DeserializeLengthDelimited(Stream stream, ContentHandleRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ContentHandleRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x060030D9 RID: 12505 RVA: 0x000A4720 File Offset: 0x000A2920
		public static ContentHandleRequest Deserialize(Stream stream, ContentHandleRequest instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			instance.Version = 1701729619U;
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
				else if (num != 13)
				{
					if (num != 21)
					{
						if (num != 29)
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
							instance.Version = binaryReader.ReadUInt32();
						}
					}
					else
					{
						instance.Stream = binaryReader.ReadUInt32();
					}
				}
				else
				{
					instance.Program = binaryReader.ReadUInt32();
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060030DA RID: 12506 RVA: 0x000A47E0 File Offset: 0x000A29E0
		public void Serialize(Stream stream)
		{
			ContentHandleRequest.Serialize(stream, this);
		}

		// Token: 0x060030DB RID: 12507 RVA: 0x000A47EC File Offset: 0x000A29EC
		public static void Serialize(Stream stream, ContentHandleRequest instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			stream.WriteByte(13);
			binaryWriter.Write(instance.Program);
			stream.WriteByte(21);
			binaryWriter.Write(instance.Stream);
			if (instance.HasVersion)
			{
				stream.WriteByte(29);
				binaryWriter.Write(instance.Version);
			}
		}

		// Token: 0x060030DC RID: 12508 RVA: 0x000A4844 File Offset: 0x000A2A44
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += 4U;
			num += 4U;
			if (this.HasVersion)
			{
				num += 1U;
				num += 4U;
			}
			return num + 2U;
		}

		// Token: 0x04001364 RID: 4964
		public bool HasVersion;

		// Token: 0x04001365 RID: 4965
		private uint _Version;
	}
}
