using System;
using System.Collections.Generic;
using System.IO;

namespace PegasusGame
{
	// Token: 0x020001AB RID: 427
	public class ChooseEntities : IProtoBuf
	{
		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x06001AFE RID: 6910 RVA: 0x0005F84B File Offset: 0x0005DA4B
		// (set) Token: 0x06001AFF RID: 6911 RVA: 0x0005F853 File Offset: 0x0005DA53
		public int Id { get; set; }

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x06001B00 RID: 6912 RVA: 0x0005F85C File Offset: 0x0005DA5C
		// (set) Token: 0x06001B01 RID: 6913 RVA: 0x0005F864 File Offset: 0x0005DA64
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

		// Token: 0x06001B02 RID: 6914 RVA: 0x0005F870 File Offset: 0x0005DA70
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Id.GetHashCode();
			foreach (int num2 in this.Entities)
			{
				num ^= num2.GetHashCode();
			}
			return num;
		}

		// Token: 0x06001B03 RID: 6915 RVA: 0x0005F8E4 File Offset: 0x0005DAE4
		public override bool Equals(object obj)
		{
			ChooseEntities chooseEntities = obj as ChooseEntities;
			if (chooseEntities == null)
			{
				return false;
			}
			if (!this.Id.Equals(chooseEntities.Id))
			{
				return false;
			}
			if (this.Entities.Count != chooseEntities.Entities.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Entities.Count; i++)
			{
				if (!this.Entities[i].Equals(chooseEntities.Entities[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001B04 RID: 6916 RVA: 0x0005F96A File Offset: 0x0005DB6A
		public void Deserialize(Stream stream)
		{
			ChooseEntities.Deserialize(stream, this);
		}

		// Token: 0x06001B05 RID: 6917 RVA: 0x0005F974 File Offset: 0x0005DB74
		public static ChooseEntities Deserialize(Stream stream, ChooseEntities instance)
		{
			return ChooseEntities.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001B06 RID: 6918 RVA: 0x0005F980 File Offset: 0x0005DB80
		public static ChooseEntities DeserializeLengthDelimited(Stream stream)
		{
			ChooseEntities chooseEntities = new ChooseEntities();
			ChooseEntities.DeserializeLengthDelimited(stream, chooseEntities);
			return chooseEntities;
		}

		// Token: 0x06001B07 RID: 6919 RVA: 0x0005F99C File Offset: 0x0005DB9C
		public static ChooseEntities DeserializeLengthDelimited(Stream stream, ChooseEntities instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ChooseEntities.Deserialize(stream, instance, num);
		}

		// Token: 0x06001B08 RID: 6920 RVA: 0x0005F9C4 File Offset: 0x0005DBC4
		public static ChooseEntities Deserialize(Stream stream, ChooseEntities instance, long limit)
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

		// Token: 0x06001B09 RID: 6921 RVA: 0x0005FAAC File Offset: 0x0005DCAC
		public void Serialize(Stream stream)
		{
			ChooseEntities.Serialize(stream, this);
		}

		// Token: 0x06001B0A RID: 6922 RVA: 0x0005FAB8 File Offset: 0x0005DCB8
		public static void Serialize(Stream stream, ChooseEntities instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Id));
			if (instance.Entities.Count > 0)
			{
				stream.WriteByte(18);
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
		}

		// Token: 0x06001B0B RID: 6923 RVA: 0x0005FB88 File Offset: 0x0005DD88
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Id));
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
			num += 1U;
			return num;
		}

		// Token: 0x040009FF RID: 2559
		private List<int> _Entities = new List<int>();

		// Token: 0x02000643 RID: 1603
		public enum PacketID
		{
			// Token: 0x040020FA RID: 8442
			ID = 3
		}
	}
}
