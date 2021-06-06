using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace bnet.protocol.validation
{
	// Token: 0x0200031C RID: 796
	public class MessageValidationError : IProtoBuf
	{
		// Token: 0x170008B2 RID: 2226
		// (get) Token: 0x06003095 RID: 12437 RVA: 0x000A3C97 File Offset: 0x000A1E97
		// (set) Token: 0x06003096 RID: 12438 RVA: 0x000A3C9F File Offset: 0x000A1E9F
		public List<FieldValidationError> FieldError
		{
			get
			{
				return this._FieldError;
			}
			set
			{
				this._FieldError = value;
			}
		}

		// Token: 0x170008B3 RID: 2227
		// (get) Token: 0x06003097 RID: 12439 RVA: 0x000A3C97 File Offset: 0x000A1E97
		public List<FieldValidationError> FieldErrorList
		{
			get
			{
				return this._FieldError;
			}
		}

		// Token: 0x170008B4 RID: 2228
		// (get) Token: 0x06003098 RID: 12440 RVA: 0x000A3CA8 File Offset: 0x000A1EA8
		public int FieldErrorCount
		{
			get
			{
				return this._FieldError.Count;
			}
		}

		// Token: 0x06003099 RID: 12441 RVA: 0x000A3CB5 File Offset: 0x000A1EB5
		public void AddFieldError(FieldValidationError val)
		{
			this._FieldError.Add(val);
		}

		// Token: 0x0600309A RID: 12442 RVA: 0x000A3CC3 File Offset: 0x000A1EC3
		public void ClearFieldError()
		{
			this._FieldError.Clear();
		}

		// Token: 0x0600309B RID: 12443 RVA: 0x000A3CD0 File Offset: 0x000A1ED0
		public void SetFieldError(List<FieldValidationError> val)
		{
			this.FieldError = val;
		}

		// Token: 0x170008B5 RID: 2229
		// (get) Token: 0x0600309C RID: 12444 RVA: 0x000A3CD9 File Offset: 0x000A1ED9
		// (set) Token: 0x0600309D RID: 12445 RVA: 0x000A3CE1 File Offset: 0x000A1EE1
		public string CustomError
		{
			get
			{
				return this._CustomError;
			}
			set
			{
				this._CustomError = value;
				this.HasCustomError = (value != null);
			}
		}

		// Token: 0x0600309E RID: 12446 RVA: 0x000A3CF4 File Offset: 0x000A1EF4
		public void SetCustomError(string val)
		{
			this.CustomError = val;
		}

		// Token: 0x0600309F RID: 12447 RVA: 0x000A3D00 File Offset: 0x000A1F00
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (FieldValidationError fieldValidationError in this.FieldError)
			{
				num ^= fieldValidationError.GetHashCode();
			}
			if (this.HasCustomError)
			{
				num ^= this.CustomError.GetHashCode();
			}
			return num;
		}

		// Token: 0x060030A0 RID: 12448 RVA: 0x000A3D78 File Offset: 0x000A1F78
		public override bool Equals(object obj)
		{
			MessageValidationError messageValidationError = obj as MessageValidationError;
			if (messageValidationError == null)
			{
				return false;
			}
			if (this.FieldError.Count != messageValidationError.FieldError.Count)
			{
				return false;
			}
			for (int i = 0; i < this.FieldError.Count; i++)
			{
				if (!this.FieldError[i].Equals(messageValidationError.FieldError[i]))
				{
					return false;
				}
			}
			return this.HasCustomError == messageValidationError.HasCustomError && (!this.HasCustomError || this.CustomError.Equals(messageValidationError.CustomError));
		}

		// Token: 0x170008B6 RID: 2230
		// (get) Token: 0x060030A1 RID: 12449 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060030A2 RID: 12450 RVA: 0x000A3E0E File Offset: 0x000A200E
		public static MessageValidationError ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<MessageValidationError>(bs, 0, -1);
		}

		// Token: 0x060030A3 RID: 12451 RVA: 0x000A3E18 File Offset: 0x000A2018
		public void Deserialize(Stream stream)
		{
			MessageValidationError.Deserialize(stream, this);
		}

		// Token: 0x060030A4 RID: 12452 RVA: 0x000A3E22 File Offset: 0x000A2022
		public static MessageValidationError Deserialize(Stream stream, MessageValidationError instance)
		{
			return MessageValidationError.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060030A5 RID: 12453 RVA: 0x000A3E30 File Offset: 0x000A2030
		public static MessageValidationError DeserializeLengthDelimited(Stream stream)
		{
			MessageValidationError messageValidationError = new MessageValidationError();
			MessageValidationError.DeserializeLengthDelimited(stream, messageValidationError);
			return messageValidationError;
		}

		// Token: 0x060030A6 RID: 12454 RVA: 0x000A3E4C File Offset: 0x000A204C
		public static MessageValidationError DeserializeLengthDelimited(Stream stream, MessageValidationError instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return MessageValidationError.Deserialize(stream, instance, num);
		}

		// Token: 0x060030A7 RID: 12455 RVA: 0x000A3E74 File Offset: 0x000A2074
		public static MessageValidationError Deserialize(Stream stream, MessageValidationError instance, long limit)
		{
			if (instance.FieldError == null)
			{
				instance.FieldError = new List<FieldValidationError>();
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
				else if (num != 10)
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
						instance.CustomError = ProtocolParser.ReadString(stream);
					}
				}
				else
				{
					instance.FieldError.Add(FieldValidationError.DeserializeLengthDelimited(stream));
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060030A8 RID: 12456 RVA: 0x000A3F24 File Offset: 0x000A2124
		public void Serialize(Stream stream)
		{
			MessageValidationError.Serialize(stream, this);
		}

		// Token: 0x060030A9 RID: 12457 RVA: 0x000A3F30 File Offset: 0x000A2130
		public static void Serialize(Stream stream, MessageValidationError instance)
		{
			if (instance.FieldError.Count > 0)
			{
				foreach (FieldValidationError fieldValidationError in instance.FieldError)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, fieldValidationError.GetSerializedSize());
					FieldValidationError.Serialize(stream, fieldValidationError);
				}
			}
			if (instance.HasCustomError)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.CustomError));
			}
		}

		// Token: 0x060030AA RID: 12458 RVA: 0x000A3FCC File Offset: 0x000A21CC
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.FieldError.Count > 0)
			{
				foreach (FieldValidationError fieldValidationError in this.FieldError)
				{
					num += 1U;
					uint serializedSize = fieldValidationError.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (this.HasCustomError)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.CustomError);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num;
		}

		// Token: 0x04001355 RID: 4949
		private List<FieldValidationError> _FieldError = new List<FieldValidationError>();

		// Token: 0x04001356 RID: 4950
		public bool HasCustomError;

		// Token: 0x04001357 RID: 4951
		private string _CustomError;
	}
}
