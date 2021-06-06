using System;
using System.Collections.Generic;
using System.IO;

namespace PegasusShared
{
	// Token: 0x0200015F RID: 351
	public class PVPDRSeasonSpec : IProtoBuf
	{
		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x060017F1 RID: 6129 RVA: 0x00053CBB File Offset: 0x00051EBB
		// (set) Token: 0x060017F2 RID: 6130 RVA: 0x00053CC3 File Offset: 0x00051EC3
		public GameContentSeasonSpec GameContentSeason { get; set; }

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x060017F3 RID: 6131 RVA: 0x00053CCC File Offset: 0x00051ECC
		// (set) Token: 0x060017F4 RID: 6132 RVA: 0x00053CD4 File Offset: 0x00051ED4
		public List<LocalizedString> Strings
		{
			get
			{
				return this._Strings;
			}
			set
			{
				this._Strings = value;
			}
		}

		// Token: 0x060017F5 RID: 6133 RVA: 0x00053CE0 File Offset: 0x00051EE0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.GameContentSeason.GetHashCode();
			foreach (LocalizedString localizedString in this.Strings)
			{
				num ^= localizedString.GetHashCode();
			}
			return num;
		}

		// Token: 0x060017F6 RID: 6134 RVA: 0x00053D50 File Offset: 0x00051F50
		public override bool Equals(object obj)
		{
			PVPDRSeasonSpec pvpdrseasonSpec = obj as PVPDRSeasonSpec;
			if (pvpdrseasonSpec == null)
			{
				return false;
			}
			if (!this.GameContentSeason.Equals(pvpdrseasonSpec.GameContentSeason))
			{
				return false;
			}
			if (this.Strings.Count != pvpdrseasonSpec.Strings.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Strings.Count; i++)
			{
				if (!this.Strings[i].Equals(pvpdrseasonSpec.Strings[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060017F7 RID: 6135 RVA: 0x00053DD0 File Offset: 0x00051FD0
		public void Deserialize(Stream stream)
		{
			PVPDRSeasonSpec.Deserialize(stream, this);
		}

		// Token: 0x060017F8 RID: 6136 RVA: 0x00053DDA File Offset: 0x00051FDA
		public static PVPDRSeasonSpec Deserialize(Stream stream, PVPDRSeasonSpec instance)
		{
			return PVPDRSeasonSpec.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060017F9 RID: 6137 RVA: 0x00053DE8 File Offset: 0x00051FE8
		public static PVPDRSeasonSpec DeserializeLengthDelimited(Stream stream)
		{
			PVPDRSeasonSpec pvpdrseasonSpec = new PVPDRSeasonSpec();
			PVPDRSeasonSpec.DeserializeLengthDelimited(stream, pvpdrseasonSpec);
			return pvpdrseasonSpec;
		}

		// Token: 0x060017FA RID: 6138 RVA: 0x00053E04 File Offset: 0x00052004
		public static PVPDRSeasonSpec DeserializeLengthDelimited(Stream stream, PVPDRSeasonSpec instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return PVPDRSeasonSpec.Deserialize(stream, instance, num);
		}

		// Token: 0x060017FB RID: 6139 RVA: 0x00053E2C File Offset: 0x0005202C
		public static PVPDRSeasonSpec Deserialize(Stream stream, PVPDRSeasonSpec instance, long limit)
		{
			if (instance.Strings == null)
			{
				instance.Strings = new List<LocalizedString>();
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
					if (instance.GameContentSeason == null)
					{
						instance.GameContentSeason = GameContentSeasonSpec.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameContentSeasonSpec.DeserializeLengthDelimited(stream, instance.GameContentSeason);
					}
				}
				else
				{
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					uint field = key.Field;
					if (field == 0U)
					{
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					}
					if (field != 100U)
					{
						ProtocolParser.SkipKey(stream, key);
					}
					else if (key.WireType == Wire.LengthDelimited)
					{
						instance.Strings.Add(LocalizedString.DeserializeLengthDelimited(stream));
					}
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060017FC RID: 6140 RVA: 0x00053F08 File Offset: 0x00052108
		public void Serialize(Stream stream)
		{
			PVPDRSeasonSpec.Serialize(stream, this);
		}

		// Token: 0x060017FD RID: 6141 RVA: 0x00053F14 File Offset: 0x00052114
		public static void Serialize(Stream stream, PVPDRSeasonSpec instance)
		{
			if (instance.GameContentSeason == null)
			{
				throw new ArgumentNullException("GameContentSeason", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.GameContentSeason.GetSerializedSize());
			GameContentSeasonSpec.Serialize(stream, instance.GameContentSeason);
			if (instance.Strings.Count > 0)
			{
				foreach (LocalizedString localizedString in instance.Strings)
				{
					stream.WriteByte(162);
					stream.WriteByte(6);
					ProtocolParser.WriteUInt32(stream, localizedString.GetSerializedSize());
					LocalizedString.Serialize(stream, localizedString);
				}
			}
		}

		// Token: 0x060017FE RID: 6142 RVA: 0x00053FD0 File Offset: 0x000521D0
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint serializedSize = this.GameContentSeason.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (this.Strings.Count > 0)
			{
				foreach (LocalizedString localizedString in this.Strings)
				{
					num += 2U;
					uint serializedSize2 = localizedString.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			num += 1U;
			return num;
		}

		// Token: 0x040007A4 RID: 1956
		private List<LocalizedString> _Strings = new List<LocalizedString>();
	}
}
