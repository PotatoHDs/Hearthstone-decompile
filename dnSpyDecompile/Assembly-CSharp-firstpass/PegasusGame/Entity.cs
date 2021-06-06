using System;
using System.Collections.Generic;
using System.IO;

namespace PegasusGame
{
	// Token: 0x02000192 RID: 402
	public class Entity : IProtoBuf
	{
		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x060018FB RID: 6395 RVA: 0x00057D90 File Offset: 0x00055F90
		// (set) Token: 0x060018FC RID: 6396 RVA: 0x00057D98 File Offset: 0x00055F98
		public int Id { get; set; }

		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x060018FD RID: 6397 RVA: 0x00057DA1 File Offset: 0x00055FA1
		// (set) Token: 0x060018FE RID: 6398 RVA: 0x00057DA9 File Offset: 0x00055FA9
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

		// Token: 0x060018FF RID: 6399 RVA: 0x00057DB4 File Offset: 0x00055FB4
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Id.GetHashCode();
			foreach (Tag tag in this.Tags)
			{
				num ^= tag.GetHashCode();
			}
			return num;
		}

		// Token: 0x06001900 RID: 6400 RVA: 0x00057E28 File Offset: 0x00056028
		public override bool Equals(object obj)
		{
			Entity entity = obj as Entity;
			if (entity == null)
			{
				return false;
			}
			if (!this.Id.Equals(entity.Id))
			{
				return false;
			}
			if (this.Tags.Count != entity.Tags.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Tags.Count; i++)
			{
				if (!this.Tags[i].Equals(entity.Tags[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001901 RID: 6401 RVA: 0x00057EAB File Offset: 0x000560AB
		public void Deserialize(Stream stream)
		{
			Entity.Deserialize(stream, this);
		}

		// Token: 0x06001902 RID: 6402 RVA: 0x00057EB5 File Offset: 0x000560B5
		public static Entity Deserialize(Stream stream, Entity instance)
		{
			return Entity.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001903 RID: 6403 RVA: 0x00057EC0 File Offset: 0x000560C0
		public static Entity DeserializeLengthDelimited(Stream stream)
		{
			Entity entity = new Entity();
			Entity.DeserializeLengthDelimited(stream, entity);
			return entity;
		}

		// Token: 0x06001904 RID: 6404 RVA: 0x00057EDC File Offset: 0x000560DC
		public static Entity DeserializeLengthDelimited(Stream stream, Entity instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return Entity.Deserialize(stream, instance, num);
		}

		// Token: 0x06001905 RID: 6405 RVA: 0x00057F04 File Offset: 0x00056104
		public static Entity Deserialize(Stream stream, Entity instance, long limit)
		{
			if (instance.Tags == null)
			{
				instance.Tags = new List<Tag>();
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
						instance.Tags.Add(Tag.DeserializeLengthDelimited(stream));
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

		// Token: 0x06001906 RID: 6406 RVA: 0x00057FB4 File Offset: 0x000561B4
		public void Serialize(Stream stream)
		{
			Entity.Serialize(stream, this);
		}

		// Token: 0x06001907 RID: 6407 RVA: 0x00057FC0 File Offset: 0x000561C0
		public static void Serialize(Stream stream, Entity instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Id));
			if (instance.Tags.Count > 0)
			{
				foreach (Tag tag in instance.Tags)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, tag.GetSerializedSize());
					Tag.Serialize(stream, tag);
				}
			}
		}

		// Token: 0x06001908 RID: 6408 RVA: 0x0005804C File Offset: 0x0005624C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Id));
			if (this.Tags.Count > 0)
			{
				foreach (Tag tag in this.Tags)
				{
					num += 1U;
					uint serializedSize = tag.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			num += 1U;
			return num;
		}

		// Token: 0x0400095D RID: 2397
		private List<Tag> _Tags = new List<Tag>();
	}
}
