using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace bnet.protocol.user_manager.v1
{
	// Token: 0x020002F7 RID: 759
	public class RecentPlayer : IProtoBuf
	{
		// Token: 0x17000817 RID: 2071
		// (get) Token: 0x06002D5E RID: 11614 RVA: 0x0009B59B File Offset: 0x0009979B
		// (set) Token: 0x06002D5F RID: 11615 RVA: 0x0009B5A3 File Offset: 0x000997A3
		public EntityId EntityId { get; set; }

		// Token: 0x06002D60 RID: 11616 RVA: 0x0009B5AC File Offset: 0x000997AC
		public void SetEntityId(EntityId val)
		{
			this.EntityId = val;
		}

		// Token: 0x17000818 RID: 2072
		// (get) Token: 0x06002D61 RID: 11617 RVA: 0x0009B5B5 File Offset: 0x000997B5
		// (set) Token: 0x06002D62 RID: 11618 RVA: 0x0009B5BD File Offset: 0x000997BD
		public string Program
		{
			get
			{
				return this._Program;
			}
			set
			{
				this._Program = value;
				this.HasProgram = (value != null);
			}
		}

		// Token: 0x06002D63 RID: 11619 RVA: 0x0009B5D0 File Offset: 0x000997D0
		public void SetProgram(string val)
		{
			this.Program = val;
		}

		// Token: 0x17000819 RID: 2073
		// (get) Token: 0x06002D64 RID: 11620 RVA: 0x0009B5D9 File Offset: 0x000997D9
		// (set) Token: 0x06002D65 RID: 11621 RVA: 0x0009B5E1 File Offset: 0x000997E1
		public ulong TimestampPlayed
		{
			get
			{
				return this._TimestampPlayed;
			}
			set
			{
				this._TimestampPlayed = value;
				this.HasTimestampPlayed = true;
			}
		}

		// Token: 0x06002D66 RID: 11622 RVA: 0x0009B5F1 File Offset: 0x000997F1
		public void SetTimestampPlayed(ulong val)
		{
			this.TimestampPlayed = val;
		}

		// Token: 0x1700081A RID: 2074
		// (get) Token: 0x06002D67 RID: 11623 RVA: 0x0009B5FA File Offset: 0x000997FA
		// (set) Token: 0x06002D68 RID: 11624 RVA: 0x0009B602 File Offset: 0x00099802
		public List<Attribute> Attributes
		{
			get
			{
				return this._Attributes;
			}
			set
			{
				this._Attributes = value;
			}
		}

		// Token: 0x1700081B RID: 2075
		// (get) Token: 0x06002D69 RID: 11625 RVA: 0x0009B5FA File Offset: 0x000997FA
		public List<Attribute> AttributesList
		{
			get
			{
				return this._Attributes;
			}
		}

		// Token: 0x1700081C RID: 2076
		// (get) Token: 0x06002D6A RID: 11626 RVA: 0x0009B60B File Offset: 0x0009980B
		public int AttributesCount
		{
			get
			{
				return this._Attributes.Count;
			}
		}

		// Token: 0x06002D6B RID: 11627 RVA: 0x0009B618 File Offset: 0x00099818
		public void AddAttributes(Attribute val)
		{
			this._Attributes.Add(val);
		}

		// Token: 0x06002D6C RID: 11628 RVA: 0x0009B626 File Offset: 0x00099826
		public void ClearAttributes()
		{
			this._Attributes.Clear();
		}

		// Token: 0x06002D6D RID: 11629 RVA: 0x0009B633 File Offset: 0x00099833
		public void SetAttributes(List<Attribute> val)
		{
			this.Attributes = val;
		}

		// Token: 0x1700081D RID: 2077
		// (get) Token: 0x06002D6E RID: 11630 RVA: 0x0009B63C File Offset: 0x0009983C
		// (set) Token: 0x06002D6F RID: 11631 RVA: 0x0009B644 File Offset: 0x00099844
		public uint Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				this._Id = value;
				this.HasId = true;
			}
		}

		// Token: 0x06002D70 RID: 11632 RVA: 0x0009B654 File Offset: 0x00099854
		public void SetId(uint val)
		{
			this.Id = val;
		}

		// Token: 0x1700081E RID: 2078
		// (get) Token: 0x06002D71 RID: 11633 RVA: 0x0009B65D File Offset: 0x0009985D
		// (set) Token: 0x06002D72 RID: 11634 RVA: 0x0009B665 File Offset: 0x00099865
		public uint Counter
		{
			get
			{
				return this._Counter;
			}
			set
			{
				this._Counter = value;
				this.HasCounter = true;
			}
		}

		// Token: 0x06002D73 RID: 11635 RVA: 0x0009B675 File Offset: 0x00099875
		public void SetCounter(uint val)
		{
			this.Counter = val;
		}

		// Token: 0x06002D74 RID: 11636 RVA: 0x0009B680 File Offset: 0x00099880
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.EntityId.GetHashCode();
			if (this.HasProgram)
			{
				num ^= this.Program.GetHashCode();
			}
			if (this.HasTimestampPlayed)
			{
				num ^= this.TimestampPlayed.GetHashCode();
			}
			foreach (Attribute attribute in this.Attributes)
			{
				num ^= attribute.GetHashCode();
			}
			if (this.HasId)
			{
				num ^= this.Id.GetHashCode();
			}
			if (this.HasCounter)
			{
				num ^= this.Counter.GetHashCode();
			}
			return num;
		}

		// Token: 0x06002D75 RID: 11637 RVA: 0x0009B754 File Offset: 0x00099954
		public override bool Equals(object obj)
		{
			RecentPlayer recentPlayer = obj as RecentPlayer;
			if (recentPlayer == null)
			{
				return false;
			}
			if (!this.EntityId.Equals(recentPlayer.EntityId))
			{
				return false;
			}
			if (this.HasProgram != recentPlayer.HasProgram || (this.HasProgram && !this.Program.Equals(recentPlayer.Program)))
			{
				return false;
			}
			if (this.HasTimestampPlayed != recentPlayer.HasTimestampPlayed || (this.HasTimestampPlayed && !this.TimestampPlayed.Equals(recentPlayer.TimestampPlayed)))
			{
				return false;
			}
			if (this.Attributes.Count != recentPlayer.Attributes.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Attributes.Count; i++)
			{
				if (!this.Attributes[i].Equals(recentPlayer.Attributes[i]))
				{
					return false;
				}
			}
			return this.HasId == recentPlayer.HasId && (!this.HasId || this.Id.Equals(recentPlayer.Id)) && this.HasCounter == recentPlayer.HasCounter && (!this.HasCounter || this.Counter.Equals(recentPlayer.Counter));
		}

		// Token: 0x1700081F RID: 2079
		// (get) Token: 0x06002D76 RID: 11638 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002D77 RID: 11639 RVA: 0x0009B889 File Offset: 0x00099A89
		public static RecentPlayer ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<RecentPlayer>(bs, 0, -1);
		}

		// Token: 0x06002D78 RID: 11640 RVA: 0x0009B893 File Offset: 0x00099A93
		public void Deserialize(Stream stream)
		{
			RecentPlayer.Deserialize(stream, this);
		}

		// Token: 0x06002D79 RID: 11641 RVA: 0x0009B89D File Offset: 0x00099A9D
		public static RecentPlayer Deserialize(Stream stream, RecentPlayer instance)
		{
			return RecentPlayer.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06002D7A RID: 11642 RVA: 0x0009B8A8 File Offset: 0x00099AA8
		public static RecentPlayer DeserializeLengthDelimited(Stream stream)
		{
			RecentPlayer recentPlayer = new RecentPlayer();
			RecentPlayer.DeserializeLengthDelimited(stream, recentPlayer);
			return recentPlayer;
		}

		// Token: 0x06002D7B RID: 11643 RVA: 0x0009B8C4 File Offset: 0x00099AC4
		public static RecentPlayer DeserializeLengthDelimited(Stream stream, RecentPlayer instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return RecentPlayer.Deserialize(stream, instance, num);
		}

		// Token: 0x06002D7C RID: 11644 RVA: 0x0009B8EC File Offset: 0x00099AEC
		public static RecentPlayer Deserialize(Stream stream, RecentPlayer instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			if (instance.Attributes == null)
			{
				instance.Attributes = new List<Attribute>();
			}
			instance.Id = 0U;
			instance.Counter = 0U;
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
					if (num <= 25)
					{
						if (num != 10)
						{
							if (num == 18)
							{
								instance.Program = ProtocolParser.ReadString(stream);
								continue;
							}
							if (num == 25)
							{
								instance.TimestampPlayed = binaryReader.ReadUInt64();
								continue;
							}
						}
						else
						{
							if (instance.EntityId == null)
							{
								instance.EntityId = EntityId.DeserializeLengthDelimited(stream);
								continue;
							}
							EntityId.DeserializeLengthDelimited(stream, instance.EntityId);
							continue;
						}
					}
					else
					{
						if (num == 34)
						{
							instance.Attributes.Add(Attribute.DeserializeLengthDelimited(stream));
							continue;
						}
						if (num == 45)
						{
							instance.Id = binaryReader.ReadUInt32();
							continue;
						}
						if (num == 53)
						{
							instance.Counter = binaryReader.ReadUInt32();
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

		// Token: 0x06002D7D RID: 11645 RVA: 0x0009BA3C File Offset: 0x00099C3C
		public void Serialize(Stream stream)
		{
			RecentPlayer.Serialize(stream, this);
		}

		// Token: 0x06002D7E RID: 11646 RVA: 0x0009BA48 File Offset: 0x00099C48
		public static void Serialize(Stream stream, RecentPlayer instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.EntityId == null)
			{
				throw new ArgumentNullException("EntityId", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.EntityId.GetSerializedSize());
			EntityId.Serialize(stream, instance.EntityId);
			if (instance.HasProgram)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Program));
			}
			if (instance.HasTimestampPlayed)
			{
				stream.WriteByte(25);
				binaryWriter.Write(instance.TimestampPlayed);
			}
			if (instance.Attributes.Count > 0)
			{
				foreach (Attribute attribute in instance.Attributes)
				{
					stream.WriteByte(34);
					ProtocolParser.WriteUInt32(stream, attribute.GetSerializedSize());
					Attribute.Serialize(stream, attribute);
				}
			}
			if (instance.HasId)
			{
				stream.WriteByte(45);
				binaryWriter.Write(instance.Id);
			}
			if (instance.HasCounter)
			{
				stream.WriteByte(53);
				binaryWriter.Write(instance.Counter);
			}
		}

		// Token: 0x06002D7F RID: 11647 RVA: 0x0009BB7C File Offset: 0x00099D7C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint serializedSize = this.EntityId.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (this.HasProgram)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Program);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasTimestampPlayed)
			{
				num += 1U;
				num += 8U;
			}
			if (this.Attributes.Count > 0)
			{
				foreach (Attribute attribute in this.Attributes)
				{
					num += 1U;
					uint serializedSize2 = attribute.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			if (this.HasId)
			{
				num += 1U;
				num += 4U;
			}
			if (this.HasCounter)
			{
				num += 1U;
				num += 4U;
			}
			num += 1U;
			return num;
		}

		// Token: 0x0400128A RID: 4746
		public bool HasProgram;

		// Token: 0x0400128B RID: 4747
		private string _Program;

		// Token: 0x0400128C RID: 4748
		public bool HasTimestampPlayed;

		// Token: 0x0400128D RID: 4749
		private ulong _TimestampPlayed;

		// Token: 0x0400128E RID: 4750
		private List<Attribute> _Attributes = new List<Attribute>();

		// Token: 0x0400128F RID: 4751
		public bool HasId;

		// Token: 0x04001290 RID: 4752
		private uint _Id;

		// Token: 0x04001291 RID: 4753
		public bool HasCounter;

		// Token: 0x04001292 RID: 4754
		private uint _Counter;
	}
}
