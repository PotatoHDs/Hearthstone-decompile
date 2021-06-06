using System;
using System.IO;
using System.Text;

namespace bnet.protocol
{
	// Token: 0x0200029D RID: 669
	public class Variant : IProtoBuf
	{
		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x06002613 RID: 9747 RVA: 0x0008825A File Offset: 0x0008645A
		// (set) Token: 0x06002614 RID: 9748 RVA: 0x00088262 File Offset: 0x00086462
		public bool BoolValue
		{
			get
			{
				return this._BoolValue;
			}
			set
			{
				this._BoolValue = value;
				this.HasBoolValue = true;
			}
		}

		// Token: 0x06002615 RID: 9749 RVA: 0x00088272 File Offset: 0x00086472
		public void SetBoolValue(bool val)
		{
			this.BoolValue = val;
		}

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x06002616 RID: 9750 RVA: 0x0008827B File Offset: 0x0008647B
		// (set) Token: 0x06002617 RID: 9751 RVA: 0x00088283 File Offset: 0x00086483
		public long IntValue
		{
			get
			{
				return this._IntValue;
			}
			set
			{
				this._IntValue = value;
				this.HasIntValue = true;
			}
		}

		// Token: 0x06002618 RID: 9752 RVA: 0x00088293 File Offset: 0x00086493
		public void SetIntValue(long val)
		{
			this.IntValue = val;
		}

		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x06002619 RID: 9753 RVA: 0x0008829C File Offset: 0x0008649C
		// (set) Token: 0x0600261A RID: 9754 RVA: 0x000882A4 File Offset: 0x000864A4
		public double FloatValue
		{
			get
			{
				return this._FloatValue;
			}
			set
			{
				this._FloatValue = value;
				this.HasFloatValue = true;
			}
		}

		// Token: 0x0600261B RID: 9755 RVA: 0x000882B4 File Offset: 0x000864B4
		public void SetFloatValue(double val)
		{
			this.FloatValue = val;
		}

		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x0600261C RID: 9756 RVA: 0x000882BD File Offset: 0x000864BD
		// (set) Token: 0x0600261D RID: 9757 RVA: 0x000882C5 File Offset: 0x000864C5
		public string StringValue
		{
			get
			{
				return this._StringValue;
			}
			set
			{
				this._StringValue = value;
				this.HasStringValue = (value != null);
			}
		}

		// Token: 0x0600261E RID: 9758 RVA: 0x000882D8 File Offset: 0x000864D8
		public void SetStringValue(string val)
		{
			this.StringValue = val;
		}

		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x0600261F RID: 9759 RVA: 0x000882E1 File Offset: 0x000864E1
		// (set) Token: 0x06002620 RID: 9760 RVA: 0x000882E9 File Offset: 0x000864E9
		public byte[] BlobValue
		{
			get
			{
				return this._BlobValue;
			}
			set
			{
				this._BlobValue = value;
				this.HasBlobValue = (value != null);
			}
		}

		// Token: 0x06002621 RID: 9761 RVA: 0x000882FC File Offset: 0x000864FC
		public void SetBlobValue(byte[] val)
		{
			this.BlobValue = val;
		}

		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x06002622 RID: 9762 RVA: 0x00088305 File Offset: 0x00086505
		// (set) Token: 0x06002623 RID: 9763 RVA: 0x0008830D File Offset: 0x0008650D
		public byte[] MessageValue
		{
			get
			{
				return this._MessageValue;
			}
			set
			{
				this._MessageValue = value;
				this.HasMessageValue = (value != null);
			}
		}

		// Token: 0x06002624 RID: 9764 RVA: 0x00088320 File Offset: 0x00086520
		public void SetMessageValue(byte[] val)
		{
			this.MessageValue = val;
		}

		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x06002625 RID: 9765 RVA: 0x00088329 File Offset: 0x00086529
		// (set) Token: 0x06002626 RID: 9766 RVA: 0x00088331 File Offset: 0x00086531
		public string FourccValue
		{
			get
			{
				return this._FourccValue;
			}
			set
			{
				this._FourccValue = value;
				this.HasFourccValue = (value != null);
			}
		}

		// Token: 0x06002627 RID: 9767 RVA: 0x00088344 File Offset: 0x00086544
		public void SetFourccValue(string val)
		{
			this.FourccValue = val;
		}

		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x06002628 RID: 9768 RVA: 0x0008834D File Offset: 0x0008654D
		// (set) Token: 0x06002629 RID: 9769 RVA: 0x00088355 File Offset: 0x00086555
		public ulong UintValue
		{
			get
			{
				return this._UintValue;
			}
			set
			{
				this._UintValue = value;
				this.HasUintValue = true;
			}
		}

		// Token: 0x0600262A RID: 9770 RVA: 0x00088365 File Offset: 0x00086565
		public void SetUintValue(ulong val)
		{
			this.UintValue = val;
		}

		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x0600262B RID: 9771 RVA: 0x0008836E File Offset: 0x0008656E
		// (set) Token: 0x0600262C RID: 9772 RVA: 0x00088376 File Offset: 0x00086576
		public EntityId EntityIdValue
		{
			get
			{
				return this._EntityIdValue;
			}
			set
			{
				this._EntityIdValue = value;
				this.HasEntityIdValue = (value != null);
			}
		}

		// Token: 0x0600262D RID: 9773 RVA: 0x00088389 File Offset: 0x00086589
		public void SetEntityIdValue(EntityId val)
		{
			this.EntityIdValue = val;
		}

		// Token: 0x0600262E RID: 9774 RVA: 0x00088394 File Offset: 0x00086594
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasBoolValue)
			{
				num ^= this.BoolValue.GetHashCode();
			}
			if (this.HasIntValue)
			{
				num ^= this.IntValue.GetHashCode();
			}
			if (this.HasFloatValue)
			{
				num ^= this.FloatValue.GetHashCode();
			}
			if (this.HasStringValue)
			{
				num ^= this.StringValue.GetHashCode();
			}
			if (this.HasBlobValue)
			{
				num ^= this.BlobValue.GetHashCode();
			}
			if (this.HasMessageValue)
			{
				num ^= this.MessageValue.GetHashCode();
			}
			if (this.HasFourccValue)
			{
				num ^= this.FourccValue.GetHashCode();
			}
			if (this.HasUintValue)
			{
				num ^= this.UintValue.GetHashCode();
			}
			if (this.HasEntityIdValue)
			{
				num ^= this.EntityIdValue.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600262F RID: 9775 RVA: 0x00088484 File Offset: 0x00086684
		public override bool Equals(object obj)
		{
			Variant variant = obj as Variant;
			return variant != null && this.HasBoolValue == variant.HasBoolValue && (!this.HasBoolValue || this.BoolValue.Equals(variant.BoolValue)) && this.HasIntValue == variant.HasIntValue && (!this.HasIntValue || this.IntValue.Equals(variant.IntValue)) && this.HasFloatValue == variant.HasFloatValue && (!this.HasFloatValue || this.FloatValue.Equals(variant.FloatValue)) && this.HasStringValue == variant.HasStringValue && (!this.HasStringValue || this.StringValue.Equals(variant.StringValue)) && this.HasBlobValue == variant.HasBlobValue && (!this.HasBlobValue || this.BlobValue.Equals(variant.BlobValue)) && this.HasMessageValue == variant.HasMessageValue && (!this.HasMessageValue || this.MessageValue.Equals(variant.MessageValue)) && this.HasFourccValue == variant.HasFourccValue && (!this.HasFourccValue || this.FourccValue.Equals(variant.FourccValue)) && this.HasUintValue == variant.HasUintValue && (!this.HasUintValue || this.UintValue.Equals(variant.UintValue)) && this.HasEntityIdValue == variant.HasEntityIdValue && (!this.HasEntityIdValue || this.EntityIdValue.Equals(variant.EntityIdValue));
		}

		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x06002630 RID: 9776 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002631 RID: 9777 RVA: 0x0008862E File Offset: 0x0008682E
		public static Variant ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<Variant>(bs, 0, -1);
		}

		// Token: 0x06002632 RID: 9778 RVA: 0x00088638 File Offset: 0x00086838
		public void Deserialize(Stream stream)
		{
			Variant.Deserialize(stream, this);
		}

		// Token: 0x06002633 RID: 9779 RVA: 0x00088642 File Offset: 0x00086842
		public static Variant Deserialize(Stream stream, Variant instance)
		{
			return Variant.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06002634 RID: 9780 RVA: 0x00088650 File Offset: 0x00086850
		public static Variant DeserializeLengthDelimited(Stream stream)
		{
			Variant variant = new Variant();
			Variant.DeserializeLengthDelimited(stream, variant);
			return variant;
		}

		// Token: 0x06002635 RID: 9781 RVA: 0x0008866C File Offset: 0x0008686C
		public static Variant DeserializeLengthDelimited(Stream stream, Variant instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return Variant.Deserialize(stream, instance, num);
		}

		// Token: 0x06002636 RID: 9782 RVA: 0x00088694 File Offset: 0x00086894
		public static Variant Deserialize(Stream stream, Variant instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
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
					if (num <= 42)
					{
						if (num <= 24)
						{
							if (num == 16)
							{
								instance.BoolValue = ProtocolParser.ReadBool(stream);
								continue;
							}
							if (num == 24)
							{
								instance.IntValue = (long)ProtocolParser.ReadUInt64(stream);
								continue;
							}
						}
						else
						{
							if (num == 33)
							{
								instance.FloatValue = binaryReader.ReadDouble();
								continue;
							}
							if (num == 42)
							{
								instance.StringValue = ProtocolParser.ReadString(stream);
								continue;
							}
						}
					}
					else if (num <= 58)
					{
						if (num == 50)
						{
							instance.BlobValue = ProtocolParser.ReadBytes(stream);
							continue;
						}
						if (num == 58)
						{
							instance.MessageValue = ProtocolParser.ReadBytes(stream);
							continue;
						}
					}
					else
					{
						if (num == 66)
						{
							instance.FourccValue = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 72)
						{
							instance.UintValue = ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 82)
						{
							if (instance.EntityIdValue == null)
							{
								instance.EntityIdValue = EntityId.DeserializeLengthDelimited(stream);
								continue;
							}
							EntityId.DeserializeLengthDelimited(stream, instance.EntityIdValue);
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

		// Token: 0x06002637 RID: 9783 RVA: 0x0008881A File Offset: 0x00086A1A
		public void Serialize(Stream stream)
		{
			Variant.Serialize(stream, this);
		}

		// Token: 0x06002638 RID: 9784 RVA: 0x00088824 File Offset: 0x00086A24
		public static void Serialize(Stream stream, Variant instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasBoolValue)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteBool(stream, instance.BoolValue);
			}
			if (instance.HasIntValue)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.IntValue);
			}
			if (instance.HasFloatValue)
			{
				stream.WriteByte(33);
				binaryWriter.Write(instance.FloatValue);
			}
			if (instance.HasStringValue)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.StringValue));
			}
			if (instance.HasBlobValue)
			{
				stream.WriteByte(50);
				ProtocolParser.WriteBytes(stream, instance.BlobValue);
			}
			if (instance.HasMessageValue)
			{
				stream.WriteByte(58);
				ProtocolParser.WriteBytes(stream, instance.MessageValue);
			}
			if (instance.HasFourccValue)
			{
				stream.WriteByte(66);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.FourccValue));
			}
			if (instance.HasUintValue)
			{
				stream.WriteByte(72);
				ProtocolParser.WriteUInt64(stream, instance.UintValue);
			}
			if (instance.HasEntityIdValue)
			{
				stream.WriteByte(82);
				ProtocolParser.WriteUInt32(stream, instance.EntityIdValue.GetSerializedSize());
				EntityId.Serialize(stream, instance.EntityIdValue);
			}
		}

		// Token: 0x06002639 RID: 9785 RVA: 0x0008895C File Offset: 0x00086B5C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasBoolValue)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasIntValue)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.IntValue);
			}
			if (this.HasFloatValue)
			{
				num += 1U;
				num += 8U;
			}
			if (this.HasStringValue)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.StringValue);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasBlobValue)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.BlobValue.Length) + (uint)this.BlobValue.Length;
			}
			if (this.HasMessageValue)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.MessageValue.Length) + (uint)this.MessageValue.Length;
			}
			if (this.HasFourccValue)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.FourccValue);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (this.HasUintValue)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.UintValue);
			}
			if (this.HasEntityIdValue)
			{
				num += 1U;
				uint serializedSize = this.EntityIdValue.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}

		// Token: 0x040010D9 RID: 4313
		public bool HasBoolValue;

		// Token: 0x040010DA RID: 4314
		private bool _BoolValue;

		// Token: 0x040010DB RID: 4315
		public bool HasIntValue;

		// Token: 0x040010DC RID: 4316
		private long _IntValue;

		// Token: 0x040010DD RID: 4317
		public bool HasFloatValue;

		// Token: 0x040010DE RID: 4318
		private double _FloatValue;

		// Token: 0x040010DF RID: 4319
		public bool HasStringValue;

		// Token: 0x040010E0 RID: 4320
		private string _StringValue;

		// Token: 0x040010E1 RID: 4321
		public bool HasBlobValue;

		// Token: 0x040010E2 RID: 4322
		private byte[] _BlobValue;

		// Token: 0x040010E3 RID: 4323
		public bool HasMessageValue;

		// Token: 0x040010E4 RID: 4324
		private byte[] _MessageValue;

		// Token: 0x040010E5 RID: 4325
		public bool HasFourccValue;

		// Token: 0x040010E6 RID: 4326
		private string _FourccValue;

		// Token: 0x040010E7 RID: 4327
		public bool HasUintValue;

		// Token: 0x040010E8 RID: 4328
		private ulong _UintValue;

		// Token: 0x040010E9 RID: 4329
		public bool HasEntityIdValue;

		// Token: 0x040010EA RID: 4330
		private EntityId _EntityIdValue;
	}
}
