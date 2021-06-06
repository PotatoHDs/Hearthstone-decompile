using System;
using System.Collections.Generic;
using System.IO;

namespace PegasusGame
{
	// Token: 0x020001B9 RID: 441
	public class EntityChoices : IProtoBuf
	{
		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x06001BFC RID: 7164 RVA: 0x00062B8D File Offset: 0x00060D8D
		// (set) Token: 0x06001BFD RID: 7165 RVA: 0x00062B95 File Offset: 0x00060D95
		public int Id { get; set; }

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x06001BFE RID: 7166 RVA: 0x00062B9E File Offset: 0x00060D9E
		// (set) Token: 0x06001BFF RID: 7167 RVA: 0x00062BA6 File Offset: 0x00060DA6
		public int ChoiceType { get; set; }

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x06001C00 RID: 7168 RVA: 0x00062BAF File Offset: 0x00060DAF
		// (set) Token: 0x06001C01 RID: 7169 RVA: 0x00062BB7 File Offset: 0x00060DB7
		public int CountMin { get; set; }

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x06001C02 RID: 7170 RVA: 0x00062BC0 File Offset: 0x00060DC0
		// (set) Token: 0x06001C03 RID: 7171 RVA: 0x00062BC8 File Offset: 0x00060DC8
		public int CountMax { get; set; }

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x06001C04 RID: 7172 RVA: 0x00062BD1 File Offset: 0x00060DD1
		// (set) Token: 0x06001C05 RID: 7173 RVA: 0x00062BD9 File Offset: 0x00060DD9
		public List<int> Entities
		{
			get
			{
				return this._Entities;
			}
			set
			{
				this._Entities = value;
			}
		}

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x06001C06 RID: 7174 RVA: 0x00062BE2 File Offset: 0x00060DE2
		// (set) Token: 0x06001C07 RID: 7175 RVA: 0x00062BEA File Offset: 0x00060DEA
		public int Source
		{
			get
			{
				return this._Source;
			}
			set
			{
				this._Source = value;
				this.HasSource = true;
			}
		}

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x06001C08 RID: 7176 RVA: 0x00062BFA File Offset: 0x00060DFA
		// (set) Token: 0x06001C09 RID: 7177 RVA: 0x00062C02 File Offset: 0x00060E02
		public int PlayerId { get; set; }

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x06001C0A RID: 7178 RVA: 0x00062C0B File Offset: 0x00060E0B
		// (set) Token: 0x06001C0B RID: 7179 RVA: 0x00062C13 File Offset: 0x00060E13
		public bool HideChosen { get; set; }

		// Token: 0x06001C0C RID: 7180 RVA: 0x00062C1C File Offset: 0x00060E1C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Id.GetHashCode();
			num ^= this.ChoiceType.GetHashCode();
			num ^= this.CountMin.GetHashCode();
			num ^= this.CountMax.GetHashCode();
			foreach (int num2 in this.Entities)
			{
				num ^= num2.GetHashCode();
			}
			if (this.HasSource)
			{
				num ^= this.Source.GetHashCode();
			}
			num ^= this.PlayerId.GetHashCode();
			num ^= this.HideChosen.GetHashCode();
			return num;
		}

		// Token: 0x06001C0D RID: 7181 RVA: 0x00062D00 File Offset: 0x00060F00
		public override bool Equals(object obj)
		{
			EntityChoices entityChoices = obj as EntityChoices;
			if (entityChoices == null)
			{
				return false;
			}
			if (!this.Id.Equals(entityChoices.Id))
			{
				return false;
			}
			if (!this.ChoiceType.Equals(entityChoices.ChoiceType))
			{
				return false;
			}
			if (!this.CountMin.Equals(entityChoices.CountMin))
			{
				return false;
			}
			if (!this.CountMax.Equals(entityChoices.CountMax))
			{
				return false;
			}
			if (this.Entities.Count != entityChoices.Entities.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Entities.Count; i++)
			{
				if (!this.Entities[i].Equals(entityChoices.Entities[i]))
				{
					return false;
				}
			}
			return this.HasSource == entityChoices.HasSource && (!this.HasSource || this.Source.Equals(entityChoices.Source)) && this.PlayerId.Equals(entityChoices.PlayerId) && this.HideChosen.Equals(entityChoices.HideChosen);
		}

		// Token: 0x06001C0E RID: 7182 RVA: 0x00062E2C File Offset: 0x0006102C
		public void Deserialize(Stream stream)
		{
			EntityChoices.Deserialize(stream, this);
		}

		// Token: 0x06001C0F RID: 7183 RVA: 0x00062E36 File Offset: 0x00061036
		public static EntityChoices Deserialize(Stream stream, EntityChoices instance)
		{
			return EntityChoices.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001C10 RID: 7184 RVA: 0x00062E44 File Offset: 0x00061044
		public static EntityChoices DeserializeLengthDelimited(Stream stream)
		{
			EntityChoices entityChoices = new EntityChoices();
			EntityChoices.DeserializeLengthDelimited(stream, entityChoices);
			return entityChoices;
		}

		// Token: 0x06001C11 RID: 7185 RVA: 0x00062E60 File Offset: 0x00061060
		public static EntityChoices DeserializeLengthDelimited(Stream stream, EntityChoices instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return EntityChoices.Deserialize(stream, instance, num);
		}

		// Token: 0x06001C12 RID: 7186 RVA: 0x00062E88 File Offset: 0x00061088
		public static EntityChoices Deserialize(Stream stream, EntityChoices instance, long limit)
		{
			if (instance.Entities == null)
			{
				instance.Entities = new List<int>();
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
					if (num <= 40)
					{
						if (num <= 16)
						{
							if (num == 8)
							{
								instance.Id = (int)ProtocolParser.ReadUInt64(stream);
								continue;
							}
							if (num == 16)
							{
								instance.ChoiceType = (int)ProtocolParser.ReadUInt64(stream);
								continue;
							}
						}
						else
						{
							if (num == 32)
							{
								instance.CountMin = (int)ProtocolParser.ReadUInt64(stream);
								continue;
							}
							if (num == 40)
							{
								instance.CountMax = (int)ProtocolParser.ReadUInt64(stream);
								continue;
							}
						}
					}
					else if (num <= 56)
					{
						if (num != 50)
						{
							if (num == 56)
							{
								instance.Source = (int)ProtocolParser.ReadUInt64(stream);
								continue;
							}
						}
						else
						{
							long num2 = (long)((ulong)ProtocolParser.ReadUInt32(stream));
							num2 += stream.Position;
							while (stream.Position < num2)
							{
								instance.Entities.Add((int)ProtocolParser.ReadUInt64(stream));
							}
							if (stream.Position != num2)
							{
								throw new ProtocolBufferException("Read too many bytes in packed data");
							}
							continue;
						}
					}
					else
					{
						if (num == 64)
						{
							instance.PlayerId = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 72)
						{
							instance.HideChosen = ProtocolParser.ReadBool(stream);
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

		// Token: 0x06001C13 RID: 7187 RVA: 0x00063026 File Offset: 0x00061226
		public void Serialize(Stream stream)
		{
			EntityChoices.Serialize(stream, this);
		}

		// Token: 0x06001C14 RID: 7188 RVA: 0x00063030 File Offset: 0x00061230
		public static void Serialize(Stream stream, EntityChoices instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Id));
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.ChoiceType));
			stream.WriteByte(32);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.CountMin));
			stream.WriteByte(40);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.CountMax));
			if (instance.Entities.Count > 0)
			{
				stream.WriteByte(50);
				uint num = 0U;
				foreach (int num2 in instance.Entities)
				{
					num += ProtocolParser.SizeOfUInt64((ulong)((long)num2));
				}
				ProtocolParser.WriteUInt32(stream, num);
				foreach (int num3 in instance.Entities)
				{
					ProtocolParser.WriteUInt64(stream, (ulong)((long)num3));
				}
			}
			if (instance.HasSource)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Source));
			}
			stream.WriteByte(64);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.PlayerId));
			stream.WriteByte(72);
			ProtocolParser.WriteBool(stream, instance.HideChosen);
		}

		// Token: 0x06001C15 RID: 7189 RVA: 0x00063184 File Offset: 0x00061384
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Id));
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.ChoiceType));
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.CountMin));
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.CountMax));
			if (this.Entities.Count > 0)
			{
				num += 1U;
				uint num2 = num;
				foreach (int num3 in this.Entities)
				{
					num += ProtocolParser.SizeOfUInt64((ulong)((long)num3));
				}
				num += ProtocolParser.SizeOfUInt32(num - num2);
			}
			if (this.HasSource)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Source));
			}
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.PlayerId));
			num += 1U;
			num += 6U;
			return num;
		}

		// Token: 0x04000A48 RID: 2632
		private List<int> _Entities = new List<int>();

		// Token: 0x04000A49 RID: 2633
		public bool HasSource;

		// Token: 0x04000A4A RID: 2634
		private int _Source;

		// Token: 0x02000650 RID: 1616
		public enum PacketID
		{
			// Token: 0x04002114 RID: 8468
			ID = 17
		}
	}
}
