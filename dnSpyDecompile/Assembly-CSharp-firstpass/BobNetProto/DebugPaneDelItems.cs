using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BobNetProto
{
	// Token: 0x020001E2 RID: 482
	public class DebugPaneDelItems : IProtoBuf
	{
		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x06001E96 RID: 7830 RVA: 0x0006AF93 File Offset: 0x00069193
		// (set) Token: 0x06001E97 RID: 7831 RVA: 0x0006AF9B File Offset: 0x0006919B
		public List<DebugPaneDelItems.DebugPaneDelItem> Items
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

		// Token: 0x06001E98 RID: 7832 RVA: 0x0006AFA4 File Offset: 0x000691A4
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (DebugPaneDelItems.DebugPaneDelItem debugPaneDelItem in this.Items)
			{
				num ^= debugPaneDelItem.GetHashCode();
			}
			return num;
		}

		// Token: 0x06001E99 RID: 7833 RVA: 0x0006B008 File Offset: 0x00069208
		public override bool Equals(object obj)
		{
			DebugPaneDelItems debugPaneDelItems = obj as DebugPaneDelItems;
			if (debugPaneDelItems == null)
			{
				return false;
			}
			if (this.Items.Count != debugPaneDelItems.Items.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Items.Count; i++)
			{
				if (!this.Items[i].Equals(debugPaneDelItems.Items[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001E9A RID: 7834 RVA: 0x0006B073 File Offset: 0x00069273
		public void Deserialize(Stream stream)
		{
			DebugPaneDelItems.Deserialize(stream, this);
		}

		// Token: 0x06001E9B RID: 7835 RVA: 0x0006B07D File Offset: 0x0006927D
		public static DebugPaneDelItems Deserialize(Stream stream, DebugPaneDelItems instance)
		{
			return DebugPaneDelItems.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001E9C RID: 7836 RVA: 0x0006B088 File Offset: 0x00069288
		public static DebugPaneDelItems DeserializeLengthDelimited(Stream stream)
		{
			DebugPaneDelItems debugPaneDelItems = new DebugPaneDelItems();
			DebugPaneDelItems.DeserializeLengthDelimited(stream, debugPaneDelItems);
			return debugPaneDelItems;
		}

		// Token: 0x06001E9D RID: 7837 RVA: 0x0006B0A4 File Offset: 0x000692A4
		public static DebugPaneDelItems DeserializeLengthDelimited(Stream stream, DebugPaneDelItems instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return DebugPaneDelItems.Deserialize(stream, instance, num);
		}

		// Token: 0x06001E9E RID: 7838 RVA: 0x0006B0CC File Offset: 0x000692CC
		public static DebugPaneDelItems Deserialize(Stream stream, DebugPaneDelItems instance, long limit)
		{
			if (instance.Items == null)
			{
				instance.Items = new List<DebugPaneDelItems.DebugPaneDelItem>();
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
					instance.Items.Add(DebugPaneDelItems.DebugPaneDelItem.DeserializeLengthDelimited(stream));
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

		// Token: 0x06001E9F RID: 7839 RVA: 0x0006B164 File Offset: 0x00069364
		public void Serialize(Stream stream)
		{
			DebugPaneDelItems.Serialize(stream, this);
		}

		// Token: 0x06001EA0 RID: 7840 RVA: 0x0006B170 File Offset: 0x00069370
		public static void Serialize(Stream stream, DebugPaneDelItems instance)
		{
			if (instance.Items.Count > 0)
			{
				foreach (DebugPaneDelItems.DebugPaneDelItem debugPaneDelItem in instance.Items)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, debugPaneDelItem.GetSerializedSize());
					DebugPaneDelItems.DebugPaneDelItem.Serialize(stream, debugPaneDelItem);
				}
			}
		}

		// Token: 0x06001EA1 RID: 7841 RVA: 0x0006B1E8 File Offset: 0x000693E8
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.Items.Count > 0)
			{
				foreach (DebugPaneDelItems.DebugPaneDelItem debugPaneDelItem in this.Items)
				{
					num += 1U;
					uint serializedSize = debugPaneDelItem.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num;
		}

		// Token: 0x04000AF0 RID: 2800
		private List<DebugPaneDelItems.DebugPaneDelItem> _Items = new List<DebugPaneDelItems.DebugPaneDelItem>();

		// Token: 0x0200066F RID: 1647
		public enum PacketID
		{
			// Token: 0x04002179 RID: 8569
			ID = 143
		}

		// Token: 0x02000670 RID: 1648
		public class DebugPaneDelItem : IProtoBuf
		{
			// Token: 0x17001283 RID: 4739
			// (get) Token: 0x060061A6 RID: 24998 RVA: 0x00126EBC File Offset: 0x001250BC
			// (set) Token: 0x060061A7 RID: 24999 RVA: 0x00126EC4 File Offset: 0x001250C4
			public string Name { get; set; }

			// Token: 0x060061A8 RID: 25000 RVA: 0x00126ECD File Offset: 0x001250CD
			public override int GetHashCode()
			{
				return base.GetType().GetHashCode() ^ this.Name.GetHashCode();
			}

			// Token: 0x060061A9 RID: 25001 RVA: 0x00126EE8 File Offset: 0x001250E8
			public override bool Equals(object obj)
			{
				DebugPaneDelItems.DebugPaneDelItem debugPaneDelItem = obj as DebugPaneDelItems.DebugPaneDelItem;
				return debugPaneDelItem != null && this.Name.Equals(debugPaneDelItem.Name);
			}

			// Token: 0x060061AA RID: 25002 RVA: 0x00126F17 File Offset: 0x00125117
			public void Deserialize(Stream stream)
			{
				DebugPaneDelItems.DebugPaneDelItem.Deserialize(stream, this);
			}

			// Token: 0x060061AB RID: 25003 RVA: 0x00126F21 File Offset: 0x00125121
			public static DebugPaneDelItems.DebugPaneDelItem Deserialize(Stream stream, DebugPaneDelItems.DebugPaneDelItem instance)
			{
				return DebugPaneDelItems.DebugPaneDelItem.Deserialize(stream, instance, -1L);
			}

			// Token: 0x060061AC RID: 25004 RVA: 0x00126F2C File Offset: 0x0012512C
			public static DebugPaneDelItems.DebugPaneDelItem DeserializeLengthDelimited(Stream stream)
			{
				DebugPaneDelItems.DebugPaneDelItem debugPaneDelItem = new DebugPaneDelItems.DebugPaneDelItem();
				DebugPaneDelItems.DebugPaneDelItem.DeserializeLengthDelimited(stream, debugPaneDelItem);
				return debugPaneDelItem;
			}

			// Token: 0x060061AD RID: 25005 RVA: 0x00126F48 File Offset: 0x00125148
			public static DebugPaneDelItems.DebugPaneDelItem DeserializeLengthDelimited(Stream stream, DebugPaneDelItems.DebugPaneDelItem instance)
			{
				long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
				num += stream.Position;
				return DebugPaneDelItems.DebugPaneDelItem.Deserialize(stream, instance, num);
			}

			// Token: 0x060061AE RID: 25006 RVA: 0x00126F70 File Offset: 0x00125170
			public static DebugPaneDelItems.DebugPaneDelItem Deserialize(Stream stream, DebugPaneDelItems.DebugPaneDelItem instance, long limit)
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
					else if (num == 10)
					{
						instance.Name = ProtocolParser.ReadString(stream);
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

			// Token: 0x060061AF RID: 25007 RVA: 0x00126FF0 File Offset: 0x001251F0
			public void Serialize(Stream stream)
			{
				DebugPaneDelItems.DebugPaneDelItem.Serialize(stream, this);
			}

			// Token: 0x060061B0 RID: 25008 RVA: 0x00126FF9 File Offset: 0x001251F9
			public static void Serialize(Stream stream, DebugPaneDelItems.DebugPaneDelItem instance)
			{
				if (instance.Name == null)
				{
					throw new ArgumentNullException("Name", "Required by proto specification.");
				}
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Name));
			}

			// Token: 0x060061B1 RID: 25009 RVA: 0x00127034 File Offset: 0x00125234
			public uint GetSerializedSize()
			{
				uint num = 0U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Name);
				return num + (ProtocolParser.SizeOfUInt32(byteCount) + byteCount) + 1U;
			}
		}
	}
}
