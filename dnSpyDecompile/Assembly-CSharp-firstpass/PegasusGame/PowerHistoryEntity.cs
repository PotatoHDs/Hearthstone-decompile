using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PegasusGame
{
	// Token: 0x020001BF RID: 447
	public class PowerHistoryEntity : IProtoBuf
	{
		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x06001C5E RID: 7262 RVA: 0x00063F15 File Offset: 0x00062115
		// (set) Token: 0x06001C5F RID: 7263 RVA: 0x00063F1D File Offset: 0x0006211D
		public int Entity { get; set; }

		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x06001C60 RID: 7264 RVA: 0x00063F26 File Offset: 0x00062126
		// (set) Token: 0x06001C61 RID: 7265 RVA: 0x00063F2E File Offset: 0x0006212E
		public string Name { get; set; }

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x06001C62 RID: 7266 RVA: 0x00063F37 File Offset: 0x00062137
		// (set) Token: 0x06001C63 RID: 7267 RVA: 0x00063F3F File Offset: 0x0006213F
		public List<Tag> Tags
		{
			get
			{
				return this._Tags;
			}
			set
			{
				this._Tags = value;
			}
		}

		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x06001C64 RID: 7268 RVA: 0x00063F48 File Offset: 0x00062148
		// (set) Token: 0x06001C65 RID: 7269 RVA: 0x00063F50 File Offset: 0x00062150
		public List<Tag> DefTags
		{
			get
			{
				return this._DefTags;
			}
			set
			{
				this._DefTags = value;
			}
		}

		// Token: 0x06001C66 RID: 7270 RVA: 0x00063F5C File Offset: 0x0006215C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Entity.GetHashCode();
			num ^= this.Name.GetHashCode();
			foreach (Tag tag in this.Tags)
			{
				num ^= tag.GetHashCode();
			}
			foreach (Tag tag2 in this.DefTags)
			{
				num ^= tag2.GetHashCode();
			}
			return num;
		}

		// Token: 0x06001C67 RID: 7271 RVA: 0x00064024 File Offset: 0x00062224
		public override bool Equals(object obj)
		{
			PowerHistoryEntity powerHistoryEntity = obj as PowerHistoryEntity;
			if (powerHistoryEntity == null)
			{
				return false;
			}
			if (!this.Entity.Equals(powerHistoryEntity.Entity))
			{
				return false;
			}
			if (!this.Name.Equals(powerHistoryEntity.Name))
			{
				return false;
			}
			if (this.Tags.Count != powerHistoryEntity.Tags.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Tags.Count; i++)
			{
				if (!this.Tags[i].Equals(powerHistoryEntity.Tags[i]))
				{
					return false;
				}
			}
			if (this.DefTags.Count != powerHistoryEntity.DefTags.Count)
			{
				return false;
			}
			for (int j = 0; j < this.DefTags.Count; j++)
			{
				if (!this.DefTags[j].Equals(powerHistoryEntity.DefTags[j]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001C68 RID: 7272 RVA: 0x0006410D File Offset: 0x0006230D
		public void Deserialize(Stream stream)
		{
			PowerHistoryEntity.Deserialize(stream, this);
		}

		// Token: 0x06001C69 RID: 7273 RVA: 0x00064117 File Offset: 0x00062317
		public static PowerHistoryEntity Deserialize(Stream stream, PowerHistoryEntity instance)
		{
			return PowerHistoryEntity.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001C6A RID: 7274 RVA: 0x00064124 File Offset: 0x00062324
		public static PowerHistoryEntity DeserializeLengthDelimited(Stream stream)
		{
			PowerHistoryEntity powerHistoryEntity = new PowerHistoryEntity();
			PowerHistoryEntity.DeserializeLengthDelimited(stream, powerHistoryEntity);
			return powerHistoryEntity;
		}

		// Token: 0x06001C6B RID: 7275 RVA: 0x00064140 File Offset: 0x00062340
		public static PowerHistoryEntity DeserializeLengthDelimited(Stream stream, PowerHistoryEntity instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return PowerHistoryEntity.Deserialize(stream, instance, num);
		}

		// Token: 0x06001C6C RID: 7276 RVA: 0x00064168 File Offset: 0x00062368
		public static PowerHistoryEntity Deserialize(Stream stream, PowerHistoryEntity instance, long limit)
		{
			if (instance.Tags == null)
			{
				instance.Tags = new List<Tag>();
			}
			if (instance.DefTags == null)
			{
				instance.DefTags = new List<Tag>();
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
				else
				{
					if (num <= 18)
					{
						if (num == 8)
						{
							instance.Entity = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 18)
						{
							instance.Name = ProtocolParser.ReadString(stream);
							continue;
						}
					}
					else
					{
						if (num == 26)
						{
							instance.Tags.Add(Tag.DeserializeLengthDelimited(stream));
							continue;
						}
						if (num == 34)
						{
							instance.DefTags.Add(Tag.DeserializeLengthDelimited(stream));
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

		// Token: 0x06001C6D RID: 7277 RVA: 0x00064269 File Offset: 0x00062469
		public void Serialize(Stream stream)
		{
			PowerHistoryEntity.Serialize(stream, this);
		}

		// Token: 0x06001C6E RID: 7278 RVA: 0x00064274 File Offset: 0x00062474
		public static void Serialize(Stream stream, PowerHistoryEntity instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Entity));
			if (instance.Name == null)
			{
				throw new ArgumentNullException("Name", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Name));
			if (instance.Tags.Count > 0)
			{
				foreach (Tag tag in instance.Tags)
				{
					stream.WriteByte(26);
					ProtocolParser.WriteUInt32(stream, tag.GetSerializedSize());
					Tag.Serialize(stream, tag);
				}
			}
			if (instance.DefTags.Count > 0)
			{
				foreach (Tag tag2 in instance.DefTags)
				{
					stream.WriteByte(34);
					ProtocolParser.WriteUInt32(stream, tag2.GetSerializedSize());
					Tag.Serialize(stream, tag2);
				}
			}
		}

		// Token: 0x06001C6F RID: 7279 RVA: 0x00064398 File Offset: 0x00062598
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Entity));
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Name);
			num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			if (this.Tags.Count > 0)
			{
				foreach (Tag tag in this.Tags)
				{
					num += 1U;
					uint serializedSize = tag.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (this.DefTags.Count > 0)
			{
				foreach (Tag tag2 in this.DefTags)
				{
					num += 1U;
					uint serializedSize2 = tag2.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			num += 2U;
			return num;
		}

		// Token: 0x04000A59 RID: 2649
		private List<Tag> _Tags = new List<Tag>();

		// Token: 0x04000A5A RID: 2650
		private List<Tag> _DefTags = new List<Tag>();
	}
}
