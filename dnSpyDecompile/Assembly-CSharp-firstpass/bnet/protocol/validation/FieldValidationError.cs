using System;
using System.IO;
using System.Text;

namespace bnet.protocol.validation
{
	// Token: 0x0200031D RID: 797
	public class FieldValidationError : IProtoBuf
	{
		// Token: 0x170008B7 RID: 2231
		// (get) Token: 0x060030AC RID: 12460 RVA: 0x000A407F File Offset: 0x000A227F
		// (set) Token: 0x060030AD RID: 12461 RVA: 0x000A4087 File Offset: 0x000A2287
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

		// Token: 0x060030AE RID: 12462 RVA: 0x000A409A File Offset: 0x000A229A
		public void SetName(string val)
		{
			this.Name = val;
		}

		// Token: 0x170008B8 RID: 2232
		// (get) Token: 0x060030AF RID: 12463 RVA: 0x000A40A3 File Offset: 0x000A22A3
		// (set) Token: 0x060030B0 RID: 12464 RVA: 0x000A40AB File Offset: 0x000A22AB
		public bool IsRepeatedField
		{
			get
			{
				return this._IsRepeatedField;
			}
			set
			{
				this._IsRepeatedField = value;
				this.HasIsRepeatedField = true;
			}
		}

		// Token: 0x060030B1 RID: 12465 RVA: 0x000A40BB File Offset: 0x000A22BB
		public void SetIsRepeatedField(bool val)
		{
			this.IsRepeatedField = val;
		}

		// Token: 0x170008B9 RID: 2233
		// (get) Token: 0x060030B2 RID: 12466 RVA: 0x000A40C4 File Offset: 0x000A22C4
		// (set) Token: 0x060030B3 RID: 12467 RVA: 0x000A40CC File Offset: 0x000A22CC
		public int Index
		{
			get
			{
				return this._Index;
			}
			set
			{
				this._Index = value;
				this.HasIndex = true;
			}
		}

		// Token: 0x060030B4 RID: 12468 RVA: 0x000A40DC File Offset: 0x000A22DC
		public void SetIndex(int val)
		{
			this.Index = val;
		}

		// Token: 0x170008BA RID: 2234
		// (get) Token: 0x060030B5 RID: 12469 RVA: 0x000A40E5 File Offset: 0x000A22E5
		// (set) Token: 0x060030B6 RID: 12470 RVA: 0x000A40ED File Offset: 0x000A22ED
		public string Reason
		{
			get
			{
				return this._Reason;
			}
			set
			{
				this._Reason = value;
				this.HasReason = (value != null);
			}
		}

		// Token: 0x060030B7 RID: 12471 RVA: 0x000A4100 File Offset: 0x000A2300
		public void SetReason(string val)
		{
			this.Reason = val;
		}

		// Token: 0x170008BB RID: 2235
		// (get) Token: 0x060030B8 RID: 12472 RVA: 0x000A4109 File Offset: 0x000A2309
		// (set) Token: 0x060030B9 RID: 12473 RVA: 0x000A4111 File Offset: 0x000A2311
		public MessageValidationError Child
		{
			get
			{
				return this._Child;
			}
			set
			{
				this._Child = value;
				this.HasChild = (value != null);
			}
		}

		// Token: 0x060030BA RID: 12474 RVA: 0x000A4124 File Offset: 0x000A2324
		public void SetChild(MessageValidationError val)
		{
			this.Child = val;
		}

		// Token: 0x060030BB RID: 12475 RVA: 0x000A4130 File Offset: 0x000A2330
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasName)
			{
				num ^= this.Name.GetHashCode();
			}
			if (this.HasIsRepeatedField)
			{
				num ^= this.IsRepeatedField.GetHashCode();
			}
			if (this.HasIndex)
			{
				num ^= this.Index.GetHashCode();
			}
			if (this.HasReason)
			{
				num ^= this.Reason.GetHashCode();
			}
			if (this.HasChild)
			{
				num ^= this.Child.GetHashCode();
			}
			return num;
		}

		// Token: 0x060030BC RID: 12476 RVA: 0x000A41C0 File Offset: 0x000A23C0
		public override bool Equals(object obj)
		{
			FieldValidationError fieldValidationError = obj as FieldValidationError;
			return fieldValidationError != null && this.HasName == fieldValidationError.HasName && (!this.HasName || this.Name.Equals(fieldValidationError.Name)) && this.HasIsRepeatedField == fieldValidationError.HasIsRepeatedField && (!this.HasIsRepeatedField || this.IsRepeatedField.Equals(fieldValidationError.IsRepeatedField)) && this.HasIndex == fieldValidationError.HasIndex && (!this.HasIndex || this.Index.Equals(fieldValidationError.Index)) && this.HasReason == fieldValidationError.HasReason && (!this.HasReason || this.Reason.Equals(fieldValidationError.Reason)) && this.HasChild == fieldValidationError.HasChild && (!this.HasChild || this.Child.Equals(fieldValidationError.Child));
		}

		// Token: 0x170008BC RID: 2236
		// (get) Token: 0x060030BD RID: 12477 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060030BE RID: 12478 RVA: 0x000A42B7 File Offset: 0x000A24B7
		public static FieldValidationError ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<FieldValidationError>(bs, 0, -1);
		}

		// Token: 0x060030BF RID: 12479 RVA: 0x000A42C1 File Offset: 0x000A24C1
		public void Deserialize(Stream stream)
		{
			FieldValidationError.Deserialize(stream, this);
		}

		// Token: 0x060030C0 RID: 12480 RVA: 0x000A42CB File Offset: 0x000A24CB
		public static FieldValidationError Deserialize(Stream stream, FieldValidationError instance)
		{
			return FieldValidationError.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060030C1 RID: 12481 RVA: 0x000A42D8 File Offset: 0x000A24D8
		public static FieldValidationError DeserializeLengthDelimited(Stream stream)
		{
			FieldValidationError fieldValidationError = new FieldValidationError();
			FieldValidationError.DeserializeLengthDelimited(stream, fieldValidationError);
			return fieldValidationError;
		}

		// Token: 0x060030C2 RID: 12482 RVA: 0x000A42F4 File Offset: 0x000A24F4
		public static FieldValidationError DeserializeLengthDelimited(Stream stream, FieldValidationError instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return FieldValidationError.Deserialize(stream, instance, num);
		}

		// Token: 0x060030C3 RID: 12483 RVA: 0x000A431C File Offset: 0x000A251C
		public static FieldValidationError Deserialize(Stream stream, FieldValidationError instance, long limit)
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
				else
				{
					if (num <= 16)
					{
						if (num == 10)
						{
							instance.Name = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 16)
						{
							instance.IsRepeatedField = ProtocolParser.ReadBool(stream);
							continue;
						}
					}
					else
					{
						if (num == 24)
						{
							instance.Index = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 34)
						{
							instance.Reason = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 42)
						{
							if (instance.Child == null)
							{
								instance.Child = MessageValidationError.DeserializeLengthDelimited(stream);
								continue;
							}
							MessageValidationError.DeserializeLengthDelimited(stream, instance.Child);
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

		// Token: 0x060030C4 RID: 12484 RVA: 0x000A441E File Offset: 0x000A261E
		public void Serialize(Stream stream)
		{
			FieldValidationError.Serialize(stream, this);
		}

		// Token: 0x060030C5 RID: 12485 RVA: 0x000A4428 File Offset: 0x000A2628
		public static void Serialize(Stream stream, FieldValidationError instance)
		{
			if (instance.HasName)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Name));
			}
			if (instance.HasIsRepeatedField)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteBool(stream, instance.IsRepeatedField);
			}
			if (instance.HasIndex)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Index));
			}
			if (instance.HasReason)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Reason));
			}
			if (instance.HasChild)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteUInt32(stream, instance.Child.GetSerializedSize());
				MessageValidationError.Serialize(stream, instance.Child);
			}
		}

		// Token: 0x060030C6 RID: 12486 RVA: 0x000A44E8 File Offset: 0x000A26E8
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasName)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Name);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasIsRepeatedField)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasIndex)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Index));
			}
			if (this.HasReason)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.Reason);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (this.HasChild)
			{
				num += 1U;
				uint serializedSize = this.Child.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}

		// Token: 0x04001358 RID: 4952
		public bool HasName;

		// Token: 0x04001359 RID: 4953
		private string _Name;

		// Token: 0x0400135A RID: 4954
		public bool HasIsRepeatedField;

		// Token: 0x0400135B RID: 4955
		private bool _IsRepeatedField;

		// Token: 0x0400135C RID: 4956
		public bool HasIndex;

		// Token: 0x0400135D RID: 4957
		private int _Index;

		// Token: 0x0400135E RID: 4958
		public bool HasReason;

		// Token: 0x0400135F RID: 4959
		private string _Reason;

		// Token: 0x04001360 RID: 4960
		public bool HasChild;

		// Token: 0x04001361 RID: 4961
		private MessageValidationError _Child;
	}
}
