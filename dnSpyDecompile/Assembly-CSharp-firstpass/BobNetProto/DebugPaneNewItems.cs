using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BobNetProto
{
	// Token: 0x020001E1 RID: 481
	public class DebugPaneNewItems : IProtoBuf
	{
		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x06001E89 RID: 7817 RVA: 0x0006ACB7 File Offset: 0x00068EB7
		// (set) Token: 0x06001E8A RID: 7818 RVA: 0x0006ACBF File Offset: 0x00068EBF
		public List<DebugPaneNewItems.DebugPaneNewItem> Items
		{
			get
			{
				return this._Items;
			}
			set
			{
				this._Items = value;
			}
		}

		// Token: 0x06001E8B RID: 7819 RVA: 0x0006ACC8 File Offset: 0x00068EC8
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (DebugPaneNewItems.DebugPaneNewItem debugPaneNewItem in this.Items)
			{
				num ^= debugPaneNewItem.GetHashCode();
			}
			return num;
		}

		// Token: 0x06001E8C RID: 7820 RVA: 0x0006AD2C File Offset: 0x00068F2C
		public override bool Equals(object obj)
		{
			DebugPaneNewItems debugPaneNewItems = obj as DebugPaneNewItems;
			if (debugPaneNewItems == null)
			{
				return false;
			}
			if (this.Items.Count != debugPaneNewItems.Items.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Items.Count; i++)
			{
				if (!this.Items[i].Equals(debugPaneNewItems.Items[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001E8D RID: 7821 RVA: 0x0006AD97 File Offset: 0x00068F97
		public void Deserialize(Stream stream)
		{
			DebugPaneNewItems.Deserialize(stream, this);
		}

		// Token: 0x06001E8E RID: 7822 RVA: 0x0006ADA1 File Offset: 0x00068FA1
		public static DebugPaneNewItems Deserialize(Stream stream, DebugPaneNewItems instance)
		{
			return DebugPaneNewItems.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001E8F RID: 7823 RVA: 0x0006ADAC File Offset: 0x00068FAC
		public static DebugPaneNewItems DeserializeLengthDelimited(Stream stream)
		{
			DebugPaneNewItems debugPaneNewItems = new DebugPaneNewItems();
			DebugPaneNewItems.DeserializeLengthDelimited(stream, debugPaneNewItems);
			return debugPaneNewItems;
		}

		// Token: 0x06001E90 RID: 7824 RVA: 0x0006ADC8 File Offset: 0x00068FC8
		public static DebugPaneNewItems DeserializeLengthDelimited(Stream stream, DebugPaneNewItems instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return DebugPaneNewItems.Deserialize(stream, instance, num);
		}

		// Token: 0x06001E91 RID: 7825 RVA: 0x0006ADF0 File Offset: 0x00068FF0
		public static DebugPaneNewItems Deserialize(Stream stream, DebugPaneNewItems instance, long limit)
		{
			if (instance.Items == null)
			{
				instance.Items = new List<DebugPaneNewItems.DebugPaneNewItem>();
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
					instance.Items.Add(DebugPaneNewItems.DebugPaneNewItem.DeserializeLengthDelimited(stream));
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

		// Token: 0x06001E92 RID: 7826 RVA: 0x0006AE88 File Offset: 0x00069088
		public void Serialize(Stream stream)
		{
			DebugPaneNewItems.Serialize(stream, this);
		}

		// Token: 0x06001E93 RID: 7827 RVA: 0x0006AE94 File Offset: 0x00069094
		public static void Serialize(Stream stream, DebugPaneNewItems instance)
		{
			if (instance.Items.Count > 0)
			{
				foreach (DebugPaneNewItems.DebugPaneNewItem debugPaneNewItem in instance.Items)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, debugPaneNewItem.GetSerializedSize());
					DebugPaneNewItems.DebugPaneNewItem.Serialize(stream, debugPaneNewItem);
				}
			}
		}

		// Token: 0x06001E94 RID: 7828 RVA: 0x0006AF0C File Offset: 0x0006910C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.Items.Count > 0)
			{
				foreach (DebugPaneNewItems.DebugPaneNewItem debugPaneNewItem in this.Items)
				{
					num += 1U;
					uint serializedSize = debugPaneNewItem.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num;
		}

		// Token: 0x04000AEF RID: 2799
		private List<DebugPaneNewItems.DebugPaneNewItem> _Items = new List<DebugPaneNewItems.DebugPaneNewItem>();

		// Token: 0x0200066D RID: 1645
		public enum PacketID
		{
			// Token: 0x04002175 RID: 8565
			ID = 142
		}

		// Token: 0x0200066E RID: 1646
		public class DebugPaneNewItem : IProtoBuf
		{
			// Token: 0x17001281 RID: 4737
			// (get) Token: 0x06006197 RID: 24983 RVA: 0x00126C6F File Offset: 0x00124E6F
			// (set) Token: 0x06006198 RID: 24984 RVA: 0x00126C77 File Offset: 0x00124E77
			public string Name { get; set; }

			// Token: 0x17001282 RID: 4738
			// (get) Token: 0x06006199 RID: 24985 RVA: 0x00126C80 File Offset: 0x00124E80
			// (set) Token: 0x0600619A RID: 24986 RVA: 0x00126C88 File Offset: 0x00124E88
			public string Value { get; set; }

			// Token: 0x0600619B RID: 24987 RVA: 0x00126C91 File Offset: 0x00124E91
			public override int GetHashCode()
			{
				return base.GetType().GetHashCode() ^ this.Name.GetHashCode() ^ this.Value.GetHashCode();
			}

			// Token: 0x0600619C RID: 24988 RVA: 0x00126CB8 File Offset: 0x00124EB8
			public override bool Equals(object obj)
			{
				DebugPaneNewItems.DebugPaneNewItem debugPaneNewItem = obj as DebugPaneNewItems.DebugPaneNewItem;
				return debugPaneNewItem != null && this.Name.Equals(debugPaneNewItem.Name) && this.Value.Equals(debugPaneNewItem.Value);
			}

			// Token: 0x0600619D RID: 24989 RVA: 0x00126CFC File Offset: 0x00124EFC
			public void Deserialize(Stream stream)
			{
				DebugPaneNewItems.DebugPaneNewItem.Deserialize(stream, this);
			}

			// Token: 0x0600619E RID: 24990 RVA: 0x00126D06 File Offset: 0x00124F06
			public static DebugPaneNewItems.DebugPaneNewItem Deserialize(Stream stream, DebugPaneNewItems.DebugPaneNewItem instance)
			{
				return DebugPaneNewItems.DebugPaneNewItem.Deserialize(stream, instance, -1L);
			}

			// Token: 0x0600619F RID: 24991 RVA: 0x00126D14 File Offset: 0x00124F14
			public static DebugPaneNewItems.DebugPaneNewItem DeserializeLengthDelimited(Stream stream)
			{
				DebugPaneNewItems.DebugPaneNewItem debugPaneNewItem = new DebugPaneNewItems.DebugPaneNewItem();
				DebugPaneNewItems.DebugPaneNewItem.DeserializeLengthDelimited(stream, debugPaneNewItem);
				return debugPaneNewItem;
			}

			// Token: 0x060061A0 RID: 24992 RVA: 0x00126D30 File Offset: 0x00124F30
			public static DebugPaneNewItems.DebugPaneNewItem DeserializeLengthDelimited(Stream stream, DebugPaneNewItems.DebugPaneNewItem instance)
			{
				long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
				num += stream.Position;
				return DebugPaneNewItems.DebugPaneNewItem.Deserialize(stream, instance, num);
			}

			// Token: 0x060061A1 RID: 24993 RVA: 0x00126D58 File Offset: 0x00124F58
			public static DebugPaneNewItems.DebugPaneNewItem Deserialize(Stream stream, DebugPaneNewItems.DebugPaneNewItem instance, long limit)
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
							instance.Value = ProtocolParser.ReadString(stream);
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

			// Token: 0x060061A2 RID: 24994 RVA: 0x00126DF0 File Offset: 0x00124FF0
			public void Serialize(Stream stream)
			{
				DebugPaneNewItems.DebugPaneNewItem.Serialize(stream, this);
			}

			// Token: 0x060061A3 RID: 24995 RVA: 0x00126DFC File Offset: 0x00124FFC
			public static void Serialize(Stream stream, DebugPaneNewItems.DebugPaneNewItem instance)
			{
				if (instance.Name == null)
				{
					throw new ArgumentNullException("Name", "Required by proto specification.");
				}
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Name));
				if (instance.Value == null)
				{
					throw new ArgumentNullException("Value", "Required by proto specification.");
				}
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Value));
			}

			// Token: 0x060061A4 RID: 24996 RVA: 0x00126E78 File Offset: 0x00125078
			public uint GetSerializedSize()
			{
				uint num = 0U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Name);
				uint num2 = num + (ProtocolParser.SizeOfUInt32(byteCount) + byteCount);
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.Value);
				return num2 + (ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2) + 2U;
			}
		}
	}
}
