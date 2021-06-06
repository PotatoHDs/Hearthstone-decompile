using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x020000C8 RID: 200
	public class GenericResponse : IProtoBuf
	{
		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06000DB4 RID: 3508 RVA: 0x00032AEC File Offset: 0x00030CEC
		// (set) Token: 0x06000DB5 RID: 3509 RVA: 0x00032AF4 File Offset: 0x00030CF4
		public GenericResponse.Result ResultCode { get; set; }

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06000DB6 RID: 3510 RVA: 0x00032AFD File Offset: 0x00030CFD
		// (set) Token: 0x06000DB7 RID: 3511 RVA: 0x00032B05 File Offset: 0x00030D05
		public int RequestId { get; set; }

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06000DB8 RID: 3512 RVA: 0x00032B0E File Offset: 0x00030D0E
		// (set) Token: 0x06000DB9 RID: 3513 RVA: 0x00032B16 File Offset: 0x00030D16
		public int RequestSubId
		{
			get
			{
				return this._RequestSubId;
			}
			set
			{
				this._RequestSubId = value;
				this.HasRequestSubId = true;
			}
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06000DBA RID: 3514 RVA: 0x00032B26 File Offset: 0x00030D26
		// (set) Token: 0x06000DBB RID: 3515 RVA: 0x00032B2E File Offset: 0x00030D2E
		public GenericData GenericData
		{
			get
			{
				return this._GenericData;
			}
			set
			{
				this._GenericData = value;
				this.HasGenericData = (value != null);
			}
		}

		// Token: 0x06000DBC RID: 3516 RVA: 0x00032B44 File Offset: 0x00030D44
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.ResultCode.GetHashCode();
			num ^= this.RequestId.GetHashCode();
			if (this.HasRequestSubId)
			{
				num ^= this.RequestSubId.GetHashCode();
			}
			if (this.HasGenericData)
			{
				num ^= this.GenericData.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000DBD RID: 3517 RVA: 0x00032BB8 File Offset: 0x00030DB8
		public override bool Equals(object obj)
		{
			GenericResponse genericResponse = obj as GenericResponse;
			return genericResponse != null && this.ResultCode.Equals(genericResponse.ResultCode) && this.RequestId.Equals(genericResponse.RequestId) && this.HasRequestSubId == genericResponse.HasRequestSubId && (!this.HasRequestSubId || this.RequestSubId.Equals(genericResponse.RequestSubId)) && this.HasGenericData == genericResponse.HasGenericData && (!this.HasGenericData || this.GenericData.Equals(genericResponse.GenericData));
		}

		// Token: 0x06000DBE RID: 3518 RVA: 0x00032C66 File Offset: 0x00030E66
		public void Deserialize(Stream stream)
		{
			GenericResponse.Deserialize(stream, this);
		}

		// Token: 0x06000DBF RID: 3519 RVA: 0x00032C70 File Offset: 0x00030E70
		public static GenericResponse Deserialize(Stream stream, GenericResponse instance)
		{
			return GenericResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000DC0 RID: 3520 RVA: 0x00032C7C File Offset: 0x00030E7C
		public static GenericResponse DeserializeLengthDelimited(Stream stream)
		{
			GenericResponse genericResponse = new GenericResponse();
			GenericResponse.DeserializeLengthDelimited(stream, genericResponse);
			return genericResponse;
		}

		// Token: 0x06000DC1 RID: 3521 RVA: 0x00032C98 File Offset: 0x00030E98
		public static GenericResponse DeserializeLengthDelimited(Stream stream, GenericResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GenericResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06000DC2 RID: 3522 RVA: 0x00032CC0 File Offset: 0x00030EC0
		public static GenericResponse Deserialize(Stream stream, GenericResponse instance, long limit)
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
						if (num == 8)
						{
							instance.ResultCode = (GenericResponse.Result)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 16)
						{
							instance.RequestId = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					else
					{
						if (num == 24)
						{
							instance.RequestSubId = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 34)
						{
							if (instance.GenericData == null)
							{
								instance.GenericData = GenericData.DeserializeLengthDelimited(stream);
								continue;
							}
							GenericData.DeserializeLengthDelimited(stream, instance.GenericData);
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

		// Token: 0x06000DC3 RID: 3523 RVA: 0x00032DAD File Offset: 0x00030FAD
		public void Serialize(Stream stream)
		{
			GenericResponse.Serialize(stream, this);
		}

		// Token: 0x06000DC4 RID: 3524 RVA: 0x00032DB8 File Offset: 0x00030FB8
		public static void Serialize(Stream stream, GenericResponse instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.ResultCode));
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.RequestId));
			if (instance.HasRequestSubId)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.RequestSubId));
			}
			if (instance.HasGenericData)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.GenericData.GetSerializedSize());
				GenericData.Serialize(stream, instance.GenericData);
			}
		}

		// Token: 0x06000DC5 RID: 3525 RVA: 0x00032E38 File Offset: 0x00031038
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.ResultCode));
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.RequestId));
			if (this.HasRequestSubId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.RequestSubId));
			}
			if (this.HasGenericData)
			{
				num += 1U;
				uint serializedSize = this.GenericData.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num + 2U;
		}

		// Token: 0x040004A7 RID: 1191
		public bool HasRequestSubId;

		// Token: 0x040004A8 RID: 1192
		private int _RequestSubId;

		// Token: 0x040004A9 RID: 1193
		public bool HasGenericData;

		// Token: 0x040004AA RID: 1194
		private GenericData _GenericData;

		// Token: 0x020005D6 RID: 1494
		public enum PacketID
		{
			// Token: 0x04001FC5 RID: 8133
			ID = 326
		}

		// Token: 0x020005D7 RID: 1495
		public enum Result
		{
			// Token: 0x04001FC7 RID: 8135
			RESULT_OK,
			// Token: 0x04001FC8 RID: 8136
			RESULT_REQUEST_IN_PROCESS,
			// Token: 0x04001FC9 RID: 8137
			RESULT_REQUEST_COMPLETE,
			// Token: 0x04001FCA RID: 8138
			RESULT_UNKNOWN_ERROR = 100,
			// Token: 0x04001FCB RID: 8139
			RESULT_INTERNAL_ERROR,
			// Token: 0x04001FCC RID: 8140
			RESULT_DB_ERROR,
			// Token: 0x04001FCD RID: 8141
			RESULT_INVALID_REQUEST,
			// Token: 0x04001FCE RID: 8142
			RESULT_LOGIN_LOAD,
			// Token: 0x04001FCF RID: 8143
			RESULT_DATA_MIGRATION_OR_PLAYER_ID_ERROR,
			// Token: 0x04001FD0 RID: 8144
			RESULT_INTERNAL_RPC_ERROR,
			// Token: 0x04001FD1 RID: 8145
			RESULT_DATA_MIGRATION_REQUIRED
		}
	}
}
