using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace bnet.protocol.games.v1
{
	// Token: 0x020003A2 RID: 930
	public class GameFactoryDescription : IProtoBuf
	{
		// Token: 0x17000B00 RID: 2816
		// (get) Token: 0x06003C1B RID: 15387 RVA: 0x000C22A4 File Offset: 0x000C04A4
		// (set) Token: 0x06003C1C RID: 15388 RVA: 0x000C22AC File Offset: 0x000C04AC
		public ulong Id { get; set; }

		// Token: 0x06003C1D RID: 15389 RVA: 0x000C22B5 File Offset: 0x000C04B5
		public void SetId(ulong val)
		{
			this.Id = val;
		}

		// Token: 0x17000B01 RID: 2817
		// (get) Token: 0x06003C1E RID: 15390 RVA: 0x000C22BE File Offset: 0x000C04BE
		// (set) Token: 0x06003C1F RID: 15391 RVA: 0x000C22C6 File Offset: 0x000C04C6
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

		// Token: 0x06003C20 RID: 15392 RVA: 0x000C22D9 File Offset: 0x000C04D9
		public void SetName(string val)
		{
			this.Name = val;
		}

		// Token: 0x17000B02 RID: 2818
		// (get) Token: 0x06003C21 RID: 15393 RVA: 0x000C22E2 File Offset: 0x000C04E2
		// (set) Token: 0x06003C22 RID: 15394 RVA: 0x000C22EA File Offset: 0x000C04EA
		public List<Attribute> Attribute
		{
			get
			{
				return this._Attribute;
			}
			set
			{
				this._Attribute = value;
			}
		}

		// Token: 0x17000B03 RID: 2819
		// (get) Token: 0x06003C23 RID: 15395 RVA: 0x000C22E2 File Offset: 0x000C04E2
		public List<Attribute> AttributeList
		{
			get
			{
				return this._Attribute;
			}
		}

		// Token: 0x17000B04 RID: 2820
		// (get) Token: 0x06003C24 RID: 15396 RVA: 0x000C22F3 File Offset: 0x000C04F3
		public int AttributeCount
		{
			get
			{
				return this._Attribute.Count;
			}
		}

		// Token: 0x06003C25 RID: 15397 RVA: 0x000C2300 File Offset: 0x000C0500
		public void AddAttribute(Attribute val)
		{
			this._Attribute.Add(val);
		}

		// Token: 0x06003C26 RID: 15398 RVA: 0x000C230E File Offset: 0x000C050E
		public void ClearAttribute()
		{
			this._Attribute.Clear();
		}

		// Token: 0x06003C27 RID: 15399 RVA: 0x000C231B File Offset: 0x000C051B
		public void SetAttribute(List<Attribute> val)
		{
			this.Attribute = val;
		}

		// Token: 0x17000B05 RID: 2821
		// (get) Token: 0x06003C28 RID: 15400 RVA: 0x000C2324 File Offset: 0x000C0524
		// (set) Token: 0x06003C29 RID: 15401 RVA: 0x000C232C File Offset: 0x000C052C
		public List<GameStatsBucket> StatsBucket
		{
			get
			{
				return this._StatsBucket;
			}
			set
			{
				this._StatsBucket = value;
			}
		}

		// Token: 0x17000B06 RID: 2822
		// (get) Token: 0x06003C2A RID: 15402 RVA: 0x000C2324 File Offset: 0x000C0524
		public List<GameStatsBucket> StatsBucketList
		{
			get
			{
				return this._StatsBucket;
			}
		}

		// Token: 0x17000B07 RID: 2823
		// (get) Token: 0x06003C2B RID: 15403 RVA: 0x000C2335 File Offset: 0x000C0535
		public int StatsBucketCount
		{
			get
			{
				return this._StatsBucket.Count;
			}
		}

		// Token: 0x06003C2C RID: 15404 RVA: 0x000C2342 File Offset: 0x000C0542
		public void AddStatsBucket(GameStatsBucket val)
		{
			this._StatsBucket.Add(val);
		}

		// Token: 0x06003C2D RID: 15405 RVA: 0x000C2350 File Offset: 0x000C0550
		public void ClearStatsBucket()
		{
			this._StatsBucket.Clear();
		}

		// Token: 0x06003C2E RID: 15406 RVA: 0x000C235D File Offset: 0x000C055D
		public void SetStatsBucket(List<GameStatsBucket> val)
		{
			this.StatsBucket = val;
		}

		// Token: 0x17000B08 RID: 2824
		// (get) Token: 0x06003C2F RID: 15407 RVA: 0x000C2366 File Offset: 0x000C0566
		// (set) Token: 0x06003C30 RID: 15408 RVA: 0x000C236E File Offset: 0x000C056E
		public ulong UnseededId
		{
			get
			{
				return this._UnseededId;
			}
			set
			{
				this._UnseededId = value;
				this.HasUnseededId = true;
			}
		}

		// Token: 0x06003C31 RID: 15409 RVA: 0x000C237E File Offset: 0x000C057E
		public void SetUnseededId(ulong val)
		{
			this.UnseededId = val;
		}

		// Token: 0x17000B09 RID: 2825
		// (get) Token: 0x06003C32 RID: 15410 RVA: 0x000C2387 File Offset: 0x000C0587
		// (set) Token: 0x06003C33 RID: 15411 RVA: 0x000C238F File Offset: 0x000C058F
		public bool AllowQueueing
		{
			get
			{
				return this._AllowQueueing;
			}
			set
			{
				this._AllowQueueing = value;
				this.HasAllowQueueing = true;
			}
		}

		// Token: 0x06003C34 RID: 15412 RVA: 0x000C239F File Offset: 0x000C059F
		public void SetAllowQueueing(bool val)
		{
			this.AllowQueueing = val;
		}

		// Token: 0x17000B0A RID: 2826
		// (get) Token: 0x06003C35 RID: 15413 RVA: 0x000C23A8 File Offset: 0x000C05A8
		// (set) Token: 0x06003C36 RID: 15414 RVA: 0x000C23B0 File Offset: 0x000C05B0
		public bool RequiresPlayerRating
		{
			get
			{
				return this._RequiresPlayerRating;
			}
			set
			{
				this._RequiresPlayerRating = value;
				this.HasRequiresPlayerRating = true;
			}
		}

		// Token: 0x06003C37 RID: 15415 RVA: 0x000C23C0 File Offset: 0x000C05C0
		public void SetRequiresPlayerRating(bool val)
		{
			this.RequiresPlayerRating = val;
		}

		// Token: 0x17000B0B RID: 2827
		// (get) Token: 0x06003C38 RID: 15416 RVA: 0x000C23C9 File Offset: 0x000C05C9
		// (set) Token: 0x06003C39 RID: 15417 RVA: 0x000C23D1 File Offset: 0x000C05D1
		public bool RequiresQueuePriority
		{
			get
			{
				return this._RequiresQueuePriority;
			}
			set
			{
				this._RequiresQueuePriority = value;
				this.HasRequiresQueuePriority = true;
			}
		}

		// Token: 0x06003C3A RID: 15418 RVA: 0x000C23E1 File Offset: 0x000C05E1
		public void SetRequiresQueuePriority(bool val)
		{
			this.RequiresQueuePriority = val;
		}

		// Token: 0x06003C3B RID: 15419 RVA: 0x000C23EC File Offset: 0x000C05EC
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Id.GetHashCode();
			if (this.HasName)
			{
				num ^= this.Name.GetHashCode();
			}
			foreach (Attribute attribute in this.Attribute)
			{
				num ^= attribute.GetHashCode();
			}
			foreach (GameStatsBucket gameStatsBucket in this.StatsBucket)
			{
				num ^= gameStatsBucket.GetHashCode();
			}
			if (this.HasUnseededId)
			{
				num ^= this.UnseededId.GetHashCode();
			}
			if (this.HasAllowQueueing)
			{
				num ^= this.AllowQueueing.GetHashCode();
			}
			if (this.HasRequiresPlayerRating)
			{
				num ^= this.RequiresPlayerRating.GetHashCode();
			}
			if (this.HasRequiresQueuePriority)
			{
				num ^= this.RequiresQueuePriority.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003C3C RID: 15420 RVA: 0x000C2524 File Offset: 0x000C0724
		public override bool Equals(object obj)
		{
			GameFactoryDescription gameFactoryDescription = obj as GameFactoryDescription;
			if (gameFactoryDescription == null)
			{
				return false;
			}
			if (!this.Id.Equals(gameFactoryDescription.Id))
			{
				return false;
			}
			if (this.HasName != gameFactoryDescription.HasName || (this.HasName && !this.Name.Equals(gameFactoryDescription.Name)))
			{
				return false;
			}
			if (this.Attribute.Count != gameFactoryDescription.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Attribute.Count; i++)
			{
				if (!this.Attribute[i].Equals(gameFactoryDescription.Attribute[i]))
				{
					return false;
				}
			}
			if (this.StatsBucket.Count != gameFactoryDescription.StatsBucket.Count)
			{
				return false;
			}
			for (int j = 0; j < this.StatsBucket.Count; j++)
			{
				if (!this.StatsBucket[j].Equals(gameFactoryDescription.StatsBucket[j]))
				{
					return false;
				}
			}
			return this.HasUnseededId == gameFactoryDescription.HasUnseededId && (!this.HasUnseededId || this.UnseededId.Equals(gameFactoryDescription.UnseededId)) && this.HasAllowQueueing == gameFactoryDescription.HasAllowQueueing && (!this.HasAllowQueueing || this.AllowQueueing.Equals(gameFactoryDescription.AllowQueueing)) && this.HasRequiresPlayerRating == gameFactoryDescription.HasRequiresPlayerRating && (!this.HasRequiresPlayerRating || this.RequiresPlayerRating.Equals(gameFactoryDescription.RequiresPlayerRating)) && this.HasRequiresQueuePriority == gameFactoryDescription.HasRequiresQueuePriority && (!this.HasRequiresQueuePriority || this.RequiresQueuePriority.Equals(gameFactoryDescription.RequiresQueuePriority));
		}

		// Token: 0x17000B0C RID: 2828
		// (get) Token: 0x06003C3D RID: 15421 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003C3E RID: 15422 RVA: 0x000C26DE File Offset: 0x000C08DE
		public static GameFactoryDescription ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameFactoryDescription>(bs, 0, -1);
		}

		// Token: 0x06003C3F RID: 15423 RVA: 0x000C26E8 File Offset: 0x000C08E8
		public void Deserialize(Stream stream)
		{
			GameFactoryDescription.Deserialize(stream, this);
		}

		// Token: 0x06003C40 RID: 15424 RVA: 0x000C26F2 File Offset: 0x000C08F2
		public static GameFactoryDescription Deserialize(Stream stream, GameFactoryDescription instance)
		{
			return GameFactoryDescription.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003C41 RID: 15425 RVA: 0x000C2700 File Offset: 0x000C0900
		public static GameFactoryDescription DeserializeLengthDelimited(Stream stream)
		{
			GameFactoryDescription gameFactoryDescription = new GameFactoryDescription();
			GameFactoryDescription.DeserializeLengthDelimited(stream, gameFactoryDescription);
			return gameFactoryDescription;
		}

		// Token: 0x06003C42 RID: 15426 RVA: 0x000C271C File Offset: 0x000C091C
		public static GameFactoryDescription DeserializeLengthDelimited(Stream stream, GameFactoryDescription instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GameFactoryDescription.Deserialize(stream, instance, num);
		}

		// Token: 0x06003C43 RID: 15427 RVA: 0x000C2744 File Offset: 0x000C0944
		public static GameFactoryDescription Deserialize(Stream stream, GameFactoryDescription instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			if (instance.Attribute == null)
			{
				instance.Attribute = new List<Attribute>();
			}
			if (instance.StatsBucket == null)
			{
				instance.StatsBucket = new List<GameStatsBucket>();
			}
			instance.UnseededId = 0UL;
			instance.AllowQueueing = true;
			instance.RequiresPlayerRating = false;
			instance.RequiresQueuePriority = false;
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
					if (num <= 34)
					{
						if (num <= 18)
						{
							if (num == 9)
							{
								instance.Id = binaryReader.ReadUInt64();
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
								instance.Attribute.Add(bnet.protocol.Attribute.DeserializeLengthDelimited(stream));
								continue;
							}
							if (num == 34)
							{
								instance.StatsBucket.Add(GameStatsBucket.DeserializeLengthDelimited(stream));
								continue;
							}
						}
					}
					else if (num <= 48)
					{
						if (num == 41)
						{
							instance.UnseededId = binaryReader.ReadUInt64();
							continue;
						}
						if (num == 48)
						{
							instance.AllowQueueing = ProtocolParser.ReadBool(stream);
							continue;
						}
					}
					else
					{
						if (num == 56)
						{
							instance.RequiresPlayerRating = ProtocolParser.ReadBool(stream);
							continue;
						}
						if (num == 64)
						{
							instance.RequiresQueuePriority = ProtocolParser.ReadBool(stream);
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

		// Token: 0x06003C44 RID: 15428 RVA: 0x000C28E4 File Offset: 0x000C0AE4
		public void Serialize(Stream stream)
		{
			GameFactoryDescription.Serialize(stream, this);
		}

		// Token: 0x06003C45 RID: 15429 RVA: 0x000C28F0 File Offset: 0x000C0AF0
		public static void Serialize(Stream stream, GameFactoryDescription instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			stream.WriteByte(9);
			binaryWriter.Write(instance.Id);
			if (instance.HasName)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Name));
			}
			if (instance.Attribute.Count > 0)
			{
				foreach (Attribute attribute in instance.Attribute)
				{
					stream.WriteByte(26);
					ProtocolParser.WriteUInt32(stream, attribute.GetSerializedSize());
					bnet.protocol.Attribute.Serialize(stream, attribute);
				}
			}
			if (instance.StatsBucket.Count > 0)
			{
				foreach (GameStatsBucket gameStatsBucket in instance.StatsBucket)
				{
					stream.WriteByte(34);
					ProtocolParser.WriteUInt32(stream, gameStatsBucket.GetSerializedSize());
					GameStatsBucket.Serialize(stream, gameStatsBucket);
				}
			}
			if (instance.HasUnseededId)
			{
				stream.WriteByte(41);
				binaryWriter.Write(instance.UnseededId);
			}
			if (instance.HasAllowQueueing)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteBool(stream, instance.AllowQueueing);
			}
			if (instance.HasRequiresPlayerRating)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteBool(stream, instance.RequiresPlayerRating);
			}
			if (instance.HasRequiresQueuePriority)
			{
				stream.WriteByte(64);
				ProtocolParser.WriteBool(stream, instance.RequiresQueuePriority);
			}
		}

		// Token: 0x06003C46 RID: 15430 RVA: 0x000C2A80 File Offset: 0x000C0C80
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += 8U;
			if (this.HasName)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Name);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.Attribute.Count > 0)
			{
				foreach (Attribute attribute in this.Attribute)
				{
					num += 1U;
					uint serializedSize = attribute.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (this.StatsBucket.Count > 0)
			{
				foreach (GameStatsBucket gameStatsBucket in this.StatsBucket)
				{
					num += 1U;
					uint serializedSize2 = gameStatsBucket.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			if (this.HasUnseededId)
			{
				num += 1U;
				num += 8U;
			}
			if (this.HasAllowQueueing)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasRequiresPlayerRating)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasRequiresQueuePriority)
			{
				num += 1U;
				num += 1U;
			}
			num += 1U;
			return num;
		}

		// Token: 0x040015AA RID: 5546
		public bool HasName;

		// Token: 0x040015AB RID: 5547
		private string _Name;

		// Token: 0x040015AC RID: 5548
		private List<Attribute> _Attribute = new List<Attribute>();

		// Token: 0x040015AD RID: 5549
		private List<GameStatsBucket> _StatsBucket = new List<GameStatsBucket>();

		// Token: 0x040015AE RID: 5550
		public bool HasUnseededId;

		// Token: 0x040015AF RID: 5551
		private ulong _UnseededId;

		// Token: 0x040015B0 RID: 5552
		public bool HasAllowQueueing;

		// Token: 0x040015B1 RID: 5553
		private bool _AllowQueueing;

		// Token: 0x040015B2 RID: 5554
		public bool HasRequiresPlayerRating;

		// Token: 0x040015B3 RID: 5555
		private bool _RequiresPlayerRating;

		// Token: 0x040015B4 RID: 5556
		public bool HasRequiresQueuePriority;

		// Token: 0x040015B5 RID: 5557
		private bool _RequiresQueuePriority;
	}
}
