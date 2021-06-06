using System;
using System.IO;

namespace bnet.protocol.presence.v1
{
	// Token: 0x02000340 RID: 832
	public class RichPresenceLocalizationKey : IProtoBuf
	{
		// Token: 0x17000951 RID: 2385
		// (get) Token: 0x0600339C RID: 13212 RVA: 0x000AC12F File Offset: 0x000AA32F
		// (set) Token: 0x0600339D RID: 13213 RVA: 0x000AC137 File Offset: 0x000AA337
		public uint Program { get; set; }

		// Token: 0x0600339E RID: 13214 RVA: 0x000AC140 File Offset: 0x000AA340
		public void SetProgram(uint val)
		{
			this.Program = val;
		}

		// Token: 0x17000952 RID: 2386
		// (get) Token: 0x0600339F RID: 13215 RVA: 0x000AC149 File Offset: 0x000AA349
		// (set) Token: 0x060033A0 RID: 13216 RVA: 0x000AC151 File Offset: 0x000AA351
		public uint Stream { get; set; }

		// Token: 0x060033A1 RID: 13217 RVA: 0x000AC15A File Offset: 0x000AA35A
		public void SetStream(uint val)
		{
			this.Stream = val;
		}

		// Token: 0x17000953 RID: 2387
		// (get) Token: 0x060033A2 RID: 13218 RVA: 0x000AC163 File Offset: 0x000AA363
		// (set) Token: 0x060033A3 RID: 13219 RVA: 0x000AC16B File Offset: 0x000AA36B
		public uint LocalizationId { get; set; }

		// Token: 0x060033A4 RID: 13220 RVA: 0x000AC174 File Offset: 0x000AA374
		public void SetLocalizationId(uint val)
		{
			this.LocalizationId = val;
		}

		// Token: 0x060033A5 RID: 13221 RVA: 0x000AC180 File Offset: 0x000AA380
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.Program.GetHashCode() ^ this.Stream.GetHashCode() ^ this.LocalizationId.GetHashCode();
		}

		// Token: 0x060033A6 RID: 13222 RVA: 0x000AC1C8 File Offset: 0x000AA3C8
		public override bool Equals(object obj)
		{
			RichPresenceLocalizationKey richPresenceLocalizationKey = obj as RichPresenceLocalizationKey;
			return richPresenceLocalizationKey != null && this.Program.Equals(richPresenceLocalizationKey.Program) && this.Stream.Equals(richPresenceLocalizationKey.Stream) && this.LocalizationId.Equals(richPresenceLocalizationKey.LocalizationId);
		}

		// Token: 0x17000954 RID: 2388
		// (get) Token: 0x060033A7 RID: 13223 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060033A8 RID: 13224 RVA: 0x000AC22A File Offset: 0x000AA42A
		public static RichPresenceLocalizationKey ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<RichPresenceLocalizationKey>(bs, 0, -1);
		}

		// Token: 0x060033A9 RID: 13225 RVA: 0x000AC234 File Offset: 0x000AA434
		public void Deserialize(Stream stream)
		{
			RichPresenceLocalizationKey.Deserialize(stream, this);
		}

		// Token: 0x060033AA RID: 13226 RVA: 0x000AC23E File Offset: 0x000AA43E
		public static RichPresenceLocalizationKey Deserialize(Stream stream, RichPresenceLocalizationKey instance)
		{
			return RichPresenceLocalizationKey.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060033AB RID: 13227 RVA: 0x000AC24C File Offset: 0x000AA44C
		public static RichPresenceLocalizationKey DeserializeLengthDelimited(Stream stream)
		{
			RichPresenceLocalizationKey richPresenceLocalizationKey = new RichPresenceLocalizationKey();
			RichPresenceLocalizationKey.DeserializeLengthDelimited(stream, richPresenceLocalizationKey);
			return richPresenceLocalizationKey;
		}

		// Token: 0x060033AC RID: 13228 RVA: 0x000AC268 File Offset: 0x000AA468
		public static RichPresenceLocalizationKey DeserializeLengthDelimited(Stream stream, RichPresenceLocalizationKey instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return RichPresenceLocalizationKey.Deserialize(stream, instance, num);
		}

		// Token: 0x060033AD RID: 13229 RVA: 0x000AC290 File Offset: 0x000AA490
		public static RichPresenceLocalizationKey Deserialize(Stream stream, RichPresenceLocalizationKey instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
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
						if (num != 24)
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
							instance.LocalizationId = ProtocolParser.ReadUInt32(stream);
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

		// Token: 0x060033AE RID: 13230 RVA: 0x000AC345 File Offset: 0x000AA545
		public void Serialize(Stream stream)
		{
			RichPresenceLocalizationKey.Serialize(stream, this);
		}

		// Token: 0x060033AF RID: 13231 RVA: 0x000AC350 File Offset: 0x000AA550
		public static void Serialize(Stream stream, RichPresenceLocalizationKey instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			stream.WriteByte(13);
			binaryWriter.Write(instance.Program);
			stream.WriteByte(21);
			binaryWriter.Write(instance.Stream);
			stream.WriteByte(24);
			ProtocolParser.WriteUInt32(stream, instance.LocalizationId);
		}

		// Token: 0x060033B0 RID: 13232 RVA: 0x000AC39E File Offset: 0x000AA59E
		public uint GetSerializedSize()
		{
			return 0U + 4U + 4U + ProtocolParser.SizeOfUInt32(this.LocalizationId) + 3U;
		}
	}
}
