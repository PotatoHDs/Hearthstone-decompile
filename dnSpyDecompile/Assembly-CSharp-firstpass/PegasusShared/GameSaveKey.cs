using System;
using System.IO;
using System.Text;

namespace PegasusShared
{
	// Token: 0x02000166 RID: 358
	public class GameSaveKey : IProtoBuf
	{
		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x06001892 RID: 6290 RVA: 0x00056567 File Offset: 0x00054767
		// (set) Token: 0x06001893 RID: 6291 RVA: 0x0005656F File Offset: 0x0005476F
		public long Id { get; set; }

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x06001894 RID: 6292 RVA: 0x00056578 File Offset: 0x00054778
		// (set) Token: 0x06001895 RID: 6293 RVA: 0x00056580 File Offset: 0x00054780
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				this._Name = value;
				this.HasName = (value != null);
			}
		}

		// Token: 0x06001896 RID: 6294 RVA: 0x00056594 File Offset: 0x00054794
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Id.GetHashCode();
			if (this.HasName)
			{
				num ^= this.Name.GetHashCode();
			}
			return num;
		}

		// Token: 0x06001897 RID: 6295 RVA: 0x000565D8 File Offset: 0x000547D8
		public override bool Equals(object obj)
		{
			GameSaveKey gameSaveKey = obj as GameSaveKey;
			return gameSaveKey != null && this.Id.Equals(gameSaveKey.Id) && this.HasName == gameSaveKey.HasName && (!this.HasName || this.Name.Equals(gameSaveKey.Name));
		}

		// Token: 0x06001898 RID: 6296 RVA: 0x00056635 File Offset: 0x00054835
		public void Deserialize(Stream stream)
		{
			GameSaveKey.Deserialize(stream, this);
		}

		// Token: 0x06001899 RID: 6297 RVA: 0x0005663F File Offset: 0x0005483F
		public static GameSaveKey Deserialize(Stream stream, GameSaveKey instance)
		{
			return GameSaveKey.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600189A RID: 6298 RVA: 0x0005664C File Offset: 0x0005484C
		public static GameSaveKey DeserializeLengthDelimited(Stream stream)
		{
			GameSaveKey gameSaveKey = new GameSaveKey();
			GameSaveKey.DeserializeLengthDelimited(stream, gameSaveKey);
			return gameSaveKey;
		}

		// Token: 0x0600189B RID: 6299 RVA: 0x00056668 File Offset: 0x00054868
		public static GameSaveKey DeserializeLengthDelimited(Stream stream, GameSaveKey instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GameSaveKey.Deserialize(stream, instance, num);
		}

		// Token: 0x0600189C RID: 6300 RVA: 0x00056690 File Offset: 0x00054890
		public static GameSaveKey Deserialize(Stream stream, GameSaveKey instance, long limit)
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
						instance.Name = ProtocolParser.ReadString(stream);
					}
				}
				else
				{
					instance.Id = (long)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600189D RID: 6301 RVA: 0x00056727 File Offset: 0x00054927
		public void Serialize(Stream stream)
		{
			GameSaveKey.Serialize(stream, this);
		}

		// Token: 0x0600189E RID: 6302 RVA: 0x00056730 File Offset: 0x00054930
		public static void Serialize(Stream stream, GameSaveKey instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Id);
			if (instance.HasName)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Name));
			}
		}

		// Token: 0x0600189F RID: 6303 RVA: 0x0005676C File Offset: 0x0005496C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)this.Id);
			if (this.HasName)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Name);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num + 1U;
		}

		// Token: 0x040007DE RID: 2014
		public bool HasName;

		// Token: 0x040007DF RID: 2015
		private string _Name;
	}
}
