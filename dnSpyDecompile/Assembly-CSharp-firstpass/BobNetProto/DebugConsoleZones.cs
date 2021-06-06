using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BobNetProto
{
	// Token: 0x020001E3 RID: 483
	public class DebugConsoleZones : IProtoBuf
	{
		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x06001EA3 RID: 7843 RVA: 0x0006B26F File Offset: 0x0006946F
		// (set) Token: 0x06001EA4 RID: 7844 RVA: 0x0006B277 File Offset: 0x00069477
		public List<DebugConsoleZones.DebugConsoleZone> Zones
		{
			get
			{
				return this._Zones;
			}
			set
			{
				this._Zones = value;
			}
		}

		// Token: 0x06001EA5 RID: 7845 RVA: 0x0006B280 File Offset: 0x00069480
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (DebugConsoleZones.DebugConsoleZone debugConsoleZone in this.Zones)
			{
				num ^= debugConsoleZone.GetHashCode();
			}
			return num;
		}

		// Token: 0x06001EA6 RID: 7846 RVA: 0x0006B2E4 File Offset: 0x000694E4
		public override bool Equals(object obj)
		{
			DebugConsoleZones debugConsoleZones = obj as DebugConsoleZones;
			if (debugConsoleZones == null)
			{
				return false;
			}
			if (this.Zones.Count != debugConsoleZones.Zones.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Zones.Count; i++)
			{
				if (!this.Zones[i].Equals(debugConsoleZones.Zones[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001EA7 RID: 7847 RVA: 0x0006B34F File Offset: 0x0006954F
		public void Deserialize(Stream stream)
		{
			DebugConsoleZones.Deserialize(stream, this);
		}

		// Token: 0x06001EA8 RID: 7848 RVA: 0x0006B359 File Offset: 0x00069559
		public static DebugConsoleZones Deserialize(Stream stream, DebugConsoleZones instance)
		{
			return DebugConsoleZones.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001EA9 RID: 7849 RVA: 0x0006B364 File Offset: 0x00069564
		public static DebugConsoleZones DeserializeLengthDelimited(Stream stream)
		{
			DebugConsoleZones debugConsoleZones = new DebugConsoleZones();
			DebugConsoleZones.DeserializeLengthDelimited(stream, debugConsoleZones);
			return debugConsoleZones;
		}

		// Token: 0x06001EAA RID: 7850 RVA: 0x0006B380 File Offset: 0x00069580
		public static DebugConsoleZones DeserializeLengthDelimited(Stream stream, DebugConsoleZones instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return DebugConsoleZones.Deserialize(stream, instance, num);
		}

		// Token: 0x06001EAB RID: 7851 RVA: 0x0006B3A8 File Offset: 0x000695A8
		public static DebugConsoleZones Deserialize(Stream stream, DebugConsoleZones instance, long limit)
		{
			if (instance.Zones == null)
			{
				instance.Zones = new List<DebugConsoleZones.DebugConsoleZone>();
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
					instance.Zones.Add(DebugConsoleZones.DebugConsoleZone.DeserializeLengthDelimited(stream));
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

		// Token: 0x06001EAC RID: 7852 RVA: 0x0006B440 File Offset: 0x00069640
		public void Serialize(Stream stream)
		{
			DebugConsoleZones.Serialize(stream, this);
		}

		// Token: 0x06001EAD RID: 7853 RVA: 0x0006B44C File Offset: 0x0006964C
		public static void Serialize(Stream stream, DebugConsoleZones instance)
		{
			if (instance.Zones.Count > 0)
			{
				foreach (DebugConsoleZones.DebugConsoleZone debugConsoleZone in instance.Zones)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, debugConsoleZone.GetSerializedSize());
					DebugConsoleZones.DebugConsoleZone.Serialize(stream, debugConsoleZone);
				}
			}
		}

		// Token: 0x06001EAE RID: 7854 RVA: 0x0006B4C4 File Offset: 0x000696C4
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.Zones.Count > 0)
			{
				foreach (DebugConsoleZones.DebugConsoleZone debugConsoleZone in this.Zones)
				{
					num += 1U;
					uint serializedSize = debugConsoleZone.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num;
		}

		// Token: 0x04000AF1 RID: 2801
		private List<DebugConsoleZones.DebugConsoleZone> _Zones = new List<DebugConsoleZones.DebugConsoleZone>();

		// Token: 0x02000671 RID: 1649
		public enum PacketID
		{
			// Token: 0x0400217C RID: 8572
			ID = 148
		}

		// Token: 0x02000672 RID: 1650
		public class DebugConsoleZone : IProtoBuf
		{
			// Token: 0x17001284 RID: 4740
			// (get) Token: 0x060061B3 RID: 25011 RVA: 0x0012705E File Offset: 0x0012525E
			// (set) Token: 0x060061B4 RID: 25012 RVA: 0x00127066 File Offset: 0x00125266
			public string Name { get; set; }

			// Token: 0x17001285 RID: 4741
			// (get) Token: 0x060061B5 RID: 25013 RVA: 0x0012706F File Offset: 0x0012526F
			// (set) Token: 0x060061B6 RID: 25014 RVA: 0x00127077 File Offset: 0x00125277
			public uint Id { get; set; }

			// Token: 0x060061B7 RID: 25015 RVA: 0x00127080 File Offset: 0x00125280
			public override int GetHashCode()
			{
				return base.GetType().GetHashCode() ^ this.Name.GetHashCode() ^ this.Id.GetHashCode();
			}

			// Token: 0x060061B8 RID: 25016 RVA: 0x001270B4 File Offset: 0x001252B4
			public override bool Equals(object obj)
			{
				DebugConsoleZones.DebugConsoleZone debugConsoleZone = obj as DebugConsoleZones.DebugConsoleZone;
				return debugConsoleZone != null && this.Name.Equals(debugConsoleZone.Name) && this.Id.Equals(debugConsoleZone.Id);
			}

			// Token: 0x060061B9 RID: 25017 RVA: 0x001270FB File Offset: 0x001252FB
			public void Deserialize(Stream stream)
			{
				DebugConsoleZones.DebugConsoleZone.Deserialize(stream, this);
			}

			// Token: 0x060061BA RID: 25018 RVA: 0x00127105 File Offset: 0x00125305
			public static DebugConsoleZones.DebugConsoleZone Deserialize(Stream stream, DebugConsoleZones.DebugConsoleZone instance)
			{
				return DebugConsoleZones.DebugConsoleZone.Deserialize(stream, instance, -1L);
			}

			// Token: 0x060061BB RID: 25019 RVA: 0x00127110 File Offset: 0x00125310
			public static DebugConsoleZones.DebugConsoleZone DeserializeLengthDelimited(Stream stream)
			{
				DebugConsoleZones.DebugConsoleZone debugConsoleZone = new DebugConsoleZones.DebugConsoleZone();
				DebugConsoleZones.DebugConsoleZone.DeserializeLengthDelimited(stream, debugConsoleZone);
				return debugConsoleZone;
			}

			// Token: 0x060061BC RID: 25020 RVA: 0x0012712C File Offset: 0x0012532C
			public static DebugConsoleZones.DebugConsoleZone DeserializeLengthDelimited(Stream stream, DebugConsoleZones.DebugConsoleZone instance)
			{
				long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
				num += stream.Position;
				return DebugConsoleZones.DebugConsoleZone.Deserialize(stream, instance, num);
			}

			// Token: 0x060061BD RID: 25021 RVA: 0x00127154 File Offset: 0x00125354
			public static DebugConsoleZones.DebugConsoleZone Deserialize(Stream stream, DebugConsoleZones.DebugConsoleZone instance, long limit)
			{
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
						if (num != 16)
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
							instance.Id = ProtocolParser.ReadUInt32(stream);
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

			// Token: 0x060061BE RID: 25022 RVA: 0x001271EC File Offset: 0x001253EC
			public void Serialize(Stream stream)
			{
				DebugConsoleZones.DebugConsoleZone.Serialize(stream, this);
			}

			// Token: 0x060061BF RID: 25023 RVA: 0x001271F8 File Offset: 0x001253F8
			public static void Serialize(Stream stream, DebugConsoleZones.DebugConsoleZone instance)
			{
				if (instance.Name == null)
				{
					throw new ArgumentNullException("Name", "Required by proto specification.");
				}
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Name));
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.Id);
			}

			// Token: 0x060061C0 RID: 25024 RVA: 0x00127250 File Offset: 0x00125450
			public uint GetSerializedSize()
			{
				uint num = 0U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Name);
				return num + (ProtocolParser.SizeOfUInt32(byteCount) + byteCount) + ProtocolParser.SizeOfUInt32(this.Id) + 2U;
			}
		}
	}
}
