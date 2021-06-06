using System;
using System.IO;

namespace PegasusGame
{
	// Token: 0x020001BA RID: 442
	public class EntitiesChosen : IProtoBuf
	{
		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x06001C17 RID: 7191 RVA: 0x0006327F File Offset: 0x0006147F
		// (set) Token: 0x06001C18 RID: 7192 RVA: 0x00063287 File Offset: 0x00061487
		public ChooseEntities ChooseEntities { get; set; }

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x06001C19 RID: 7193 RVA: 0x00063290 File Offset: 0x00061490
		// (set) Token: 0x06001C1A RID: 7194 RVA: 0x00063298 File Offset: 0x00061498
		public int PlayerId { get; set; }

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x06001C1B RID: 7195 RVA: 0x000632A1 File Offset: 0x000614A1
		// (set) Token: 0x06001C1C RID: 7196 RVA: 0x000632A9 File Offset: 0x000614A9
		public int ChoiceType
		{
			get
			{
				return this._ChoiceType;
			}
			set
			{
				this._ChoiceType = value;
				this.HasChoiceType = true;
			}
		}

		// Token: 0x06001C1D RID: 7197 RVA: 0x000632BC File Offset: 0x000614BC
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.ChooseEntities.GetHashCode();
			num ^= this.PlayerId.GetHashCode();
			if (this.HasChoiceType)
			{
				num ^= this.ChoiceType.GetHashCode();
			}
			return num;
		}

		// Token: 0x06001C1E RID: 7198 RVA: 0x00063310 File Offset: 0x00061510
		public override bool Equals(object obj)
		{
			EntitiesChosen entitiesChosen = obj as EntitiesChosen;
			return entitiesChosen != null && this.ChooseEntities.Equals(entitiesChosen.ChooseEntities) && this.PlayerId.Equals(entitiesChosen.PlayerId) && this.HasChoiceType == entitiesChosen.HasChoiceType && (!this.HasChoiceType || this.ChoiceType.Equals(entitiesChosen.ChoiceType));
		}

		// Token: 0x06001C1F RID: 7199 RVA: 0x00063385 File Offset: 0x00061585
		public void Deserialize(Stream stream)
		{
			EntitiesChosen.Deserialize(stream, this);
		}

		// Token: 0x06001C20 RID: 7200 RVA: 0x0006338F File Offset: 0x0006158F
		public static EntitiesChosen Deserialize(Stream stream, EntitiesChosen instance)
		{
			return EntitiesChosen.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001C21 RID: 7201 RVA: 0x0006339C File Offset: 0x0006159C
		public static EntitiesChosen DeserializeLengthDelimited(Stream stream)
		{
			EntitiesChosen entitiesChosen = new EntitiesChosen();
			EntitiesChosen.DeserializeLengthDelimited(stream, entitiesChosen);
			return entitiesChosen;
		}

		// Token: 0x06001C22 RID: 7202 RVA: 0x000633B8 File Offset: 0x000615B8
		public static EntitiesChosen DeserializeLengthDelimited(Stream stream, EntitiesChosen instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return EntitiesChosen.Deserialize(stream, instance, num);
		}

		// Token: 0x06001C23 RID: 7203 RVA: 0x000633E0 File Offset: 0x000615E0
		public static EntitiesChosen Deserialize(Stream stream, EntitiesChosen instance, long limit)
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
				else if (num != 26)
				{
					if (num != 32)
					{
						if (num != 40)
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
							instance.ChoiceType = (int)ProtocolParser.ReadUInt64(stream);
						}
					}
					else
					{
						instance.PlayerId = (int)ProtocolParser.ReadUInt64(stream);
					}
				}
				else if (instance.ChooseEntities == null)
				{
					instance.ChooseEntities = ChooseEntities.DeserializeLengthDelimited(stream);
				}
				else
				{
					ChooseEntities.DeserializeLengthDelimited(stream, instance.ChooseEntities);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06001C24 RID: 7204 RVA: 0x000634B0 File Offset: 0x000616B0
		public void Serialize(Stream stream)
		{
			EntitiesChosen.Serialize(stream, this);
		}

		// Token: 0x06001C25 RID: 7205 RVA: 0x000634BC File Offset: 0x000616BC
		public static void Serialize(Stream stream, EntitiesChosen instance)
		{
			if (instance.ChooseEntities == null)
			{
				throw new ArgumentNullException("ChooseEntities", "Required by proto specification.");
			}
			stream.WriteByte(26);
			ProtocolParser.WriteUInt32(stream, instance.ChooseEntities.GetSerializedSize());
			ChooseEntities.Serialize(stream, instance.ChooseEntities);
			stream.WriteByte(32);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.PlayerId));
			if (instance.HasChoiceType)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.ChoiceType));
			}
		}

		// Token: 0x06001C26 RID: 7206 RVA: 0x00063538 File Offset: 0x00061738
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint serializedSize = this.ChooseEntities.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.PlayerId));
			if (this.HasChoiceType)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.ChoiceType));
			}
			return num + 2U;
		}

		// Token: 0x04000A4F RID: 2639
		public bool HasChoiceType;

		// Token: 0x04000A50 RID: 2640
		private int _ChoiceType;

		// Token: 0x02000651 RID: 1617
		public enum PacketID
		{
			// Token: 0x04002116 RID: 8470
			ID = 13
		}
	}
}
