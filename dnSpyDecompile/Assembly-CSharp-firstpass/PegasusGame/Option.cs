using System;
using System.Collections.Generic;
using System.IO;

namespace PegasusGame
{
	// Token: 0x02000195 RID: 405
	public class Option : IProtoBuf
	{
		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x0600192E RID: 6446 RVA: 0x0005882F File Offset: 0x00056A2F
		// (set) Token: 0x0600192F RID: 6447 RVA: 0x00058837 File Offset: 0x00056A37
		public Option.Type Type_ { get; set; }

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x06001930 RID: 6448 RVA: 0x00058840 File Offset: 0x00056A40
		// (set) Token: 0x06001931 RID: 6449 RVA: 0x00058848 File Offset: 0x00056A48
		public SubOption MainOption
		{
			get
			{
				return this._MainOption;
			}
			set
			{
				this._MainOption = value;
				this.HasMainOption = (value != null);
			}
		}

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x06001932 RID: 6450 RVA: 0x0005885B File Offset: 0x00056A5B
		// (set) Token: 0x06001933 RID: 6451 RVA: 0x00058863 File Offset: 0x00056A63
		public List<SubOption> SubOptions
		{
			get
			{
				return this._SubOptions;
			}
			set
			{
				this._SubOptions = value;
			}
		}

		// Token: 0x06001934 RID: 6452 RVA: 0x0005886C File Offset: 0x00056A6C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Type_.GetHashCode();
			if (this.HasMainOption)
			{
				num ^= this.MainOption.GetHashCode();
			}
			foreach (SubOption subOption in this.SubOptions)
			{
				num ^= subOption.GetHashCode();
			}
			return num;
		}

		// Token: 0x06001935 RID: 6453 RVA: 0x000588FC File Offset: 0x00056AFC
		public override bool Equals(object obj)
		{
			Option option = obj as Option;
			if (option == null)
			{
				return false;
			}
			if (!this.Type_.Equals(option.Type_))
			{
				return false;
			}
			if (this.HasMainOption != option.HasMainOption || (this.HasMainOption && !this.MainOption.Equals(option.MainOption)))
			{
				return false;
			}
			if (this.SubOptions.Count != option.SubOptions.Count)
			{
				return false;
			}
			for (int i = 0; i < this.SubOptions.Count; i++)
			{
				if (!this.SubOptions[i].Equals(option.SubOptions[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001936 RID: 6454 RVA: 0x000589B5 File Offset: 0x00056BB5
		public void Deserialize(Stream stream)
		{
			Option.Deserialize(stream, this);
		}

		// Token: 0x06001937 RID: 6455 RVA: 0x000589BF File Offset: 0x00056BBF
		public static Option Deserialize(Stream stream, Option instance)
		{
			return Option.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001938 RID: 6456 RVA: 0x000589CC File Offset: 0x00056BCC
		public static Option DeserializeLengthDelimited(Stream stream)
		{
			Option option = new Option();
			Option.DeserializeLengthDelimited(stream, option);
			return option;
		}

		// Token: 0x06001939 RID: 6457 RVA: 0x000589E8 File Offset: 0x00056BE8
		public static Option DeserializeLengthDelimited(Stream stream, Option instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return Option.Deserialize(stream, instance, num);
		}

		// Token: 0x0600193A RID: 6458 RVA: 0x00058A10 File Offset: 0x00056C10
		public static Option Deserialize(Stream stream, Option instance, long limit)
		{
			if (instance.SubOptions == null)
			{
				instance.SubOptions = new List<SubOption>();
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
						if (num != 26)
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
							instance.SubOptions.Add(SubOption.DeserializeLengthDelimited(stream));
						}
					}
					else if (instance.MainOption == null)
					{
						instance.MainOption = SubOption.DeserializeLengthDelimited(stream);
					}
					else
					{
						SubOption.DeserializeLengthDelimited(stream, instance.MainOption);
					}
				}
				else
				{
					instance.Type_ = (Option.Type)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600193B RID: 6459 RVA: 0x00058AF6 File Offset: 0x00056CF6
		public void Serialize(Stream stream)
		{
			Option.Serialize(stream, this);
		}

		// Token: 0x0600193C RID: 6460 RVA: 0x00058B00 File Offset: 0x00056D00
		public static void Serialize(Stream stream, Option instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Type_));
			if (instance.HasMainOption)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.MainOption.GetSerializedSize());
				SubOption.Serialize(stream, instance.MainOption);
			}
			if (instance.SubOptions.Count > 0)
			{
				foreach (SubOption subOption in instance.SubOptions)
				{
					stream.WriteByte(26);
					ProtocolParser.WriteUInt32(stream, subOption.GetSerializedSize());
					SubOption.Serialize(stream, subOption);
				}
			}
		}

		// Token: 0x0600193D RID: 6461 RVA: 0x00058BB8 File Offset: 0x00056DB8
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Type_));
			if (this.HasMainOption)
			{
				num += 1U;
				uint serializedSize = this.MainOption.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.SubOptions.Count > 0)
			{
				foreach (SubOption subOption in this.SubOptions)
				{
					num += 1U;
					uint serializedSize2 = subOption.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			num += 1U;
			return num;
		}

		// Token: 0x04000968 RID: 2408
		public bool HasMainOption;

		// Token: 0x04000969 RID: 2409
		private SubOption _MainOption;

		// Token: 0x0400096A RID: 2410
		private List<SubOption> _SubOptions = new List<SubOption>();

		// Token: 0x0200063A RID: 1594
		public enum Type
		{
			// Token: 0x040020E6 RID: 8422
			PASS = 1,
			// Token: 0x040020E7 RID: 8423
			END_TURN,
			// Token: 0x040020E8 RID: 8424
			POWER
		}
	}
}
