using System;
using System.Collections.Generic;
using System.IO;

namespace PegasusGame
{
	// Token: 0x020001B7 RID: 439
	public class AllOptions : IProtoBuf
	{
		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x06001BE0 RID: 7136 RVA: 0x000626C1 File Offset: 0x000608C1
		// (set) Token: 0x06001BE1 RID: 7137 RVA: 0x000626C9 File Offset: 0x000608C9
		public int Id { get; set; }

		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x06001BE2 RID: 7138 RVA: 0x000626D2 File Offset: 0x000608D2
		// (set) Token: 0x06001BE3 RID: 7139 RVA: 0x000626DA File Offset: 0x000608DA
		public List<Option> Options
		{
			get
			{
				return this._Options;
			}
			set
			{
				this._Options = value;
			}
		}

		// Token: 0x06001BE4 RID: 7140 RVA: 0x000626E4 File Offset: 0x000608E4
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Id.GetHashCode();
			foreach (Option option in this.Options)
			{
				num ^= option.GetHashCode();
			}
			return num;
		}

		// Token: 0x06001BE5 RID: 7141 RVA: 0x00062758 File Offset: 0x00060958
		public override bool Equals(object obj)
		{
			AllOptions allOptions = obj as AllOptions;
			if (allOptions == null)
			{
				return false;
			}
			if (!this.Id.Equals(allOptions.Id))
			{
				return false;
			}
			if (this.Options.Count != allOptions.Options.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Options.Count; i++)
			{
				if (!this.Options[i].Equals(allOptions.Options[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001BE6 RID: 7142 RVA: 0x000627DB File Offset: 0x000609DB
		public void Deserialize(Stream stream)
		{
			AllOptions.Deserialize(stream, this);
		}

		// Token: 0x06001BE7 RID: 7143 RVA: 0x000627E5 File Offset: 0x000609E5
		public static AllOptions Deserialize(Stream stream, AllOptions instance)
		{
			return AllOptions.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001BE8 RID: 7144 RVA: 0x000627F0 File Offset: 0x000609F0
		public static AllOptions DeserializeLengthDelimited(Stream stream)
		{
			AllOptions allOptions = new AllOptions();
			AllOptions.DeserializeLengthDelimited(stream, allOptions);
			return allOptions;
		}

		// Token: 0x06001BE9 RID: 7145 RVA: 0x0006280C File Offset: 0x00060A0C
		public static AllOptions DeserializeLengthDelimited(Stream stream, AllOptions instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AllOptions.Deserialize(stream, instance, num);
		}

		// Token: 0x06001BEA RID: 7146 RVA: 0x00062834 File Offset: 0x00060A34
		public static AllOptions Deserialize(Stream stream, AllOptions instance, long limit)
		{
			if (instance.Options == null)
			{
				instance.Options = new List<Option>();
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
						instance.Options.Add(Option.DeserializeLengthDelimited(stream));
					}
				}
				else
				{
					instance.Id = (int)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06001BEB RID: 7147 RVA: 0x000628E4 File Offset: 0x00060AE4
		public void Serialize(Stream stream)
		{
			AllOptions.Serialize(stream, this);
		}

		// Token: 0x06001BEC RID: 7148 RVA: 0x000628F0 File Offset: 0x00060AF0
		public static void Serialize(Stream stream, AllOptions instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Id));
			if (instance.Options.Count > 0)
			{
				foreach (Option option in instance.Options)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, option.GetSerializedSize());
					Option.Serialize(stream, option);
				}
			}
		}

		// Token: 0x06001BED RID: 7149 RVA: 0x0006297C File Offset: 0x00060B7C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Id));
			if (this.Options.Count > 0)
			{
				foreach (Option option in this.Options)
				{
					num += 1U;
					uint serializedSize = option.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			num += 1U;
			return num;
		}

		// Token: 0x04000A42 RID: 2626
		private List<Option> _Options = new List<Option>();

		// Token: 0x0200064E RID: 1614
		public enum PacketID
		{
			// Token: 0x04002110 RID: 8464
			ID = 14
		}
	}
}
