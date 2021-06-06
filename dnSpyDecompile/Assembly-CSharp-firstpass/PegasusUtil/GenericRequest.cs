using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x020000C9 RID: 201
	public class GenericRequest : IProtoBuf
	{
		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06000DC7 RID: 3527 RVA: 0x00032EA8 File Offset: 0x000310A8
		// (set) Token: 0x06000DC8 RID: 3528 RVA: 0x00032EB0 File Offset: 0x000310B0
		public int RequestId { get; set; }

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06000DC9 RID: 3529 RVA: 0x00032EB9 File Offset: 0x000310B9
		// (set) Token: 0x06000DCA RID: 3530 RVA: 0x00032EC1 File Offset: 0x000310C1
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

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000DCB RID: 3531 RVA: 0x00032ED4 File Offset: 0x000310D4
		// (set) Token: 0x06000DCC RID: 3532 RVA: 0x00032EDC File Offset: 0x000310DC
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

		// Token: 0x06000DCD RID: 3533 RVA: 0x00032EEC File Offset: 0x000310EC
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.RequestId.GetHashCode();
			if (this.HasGenericData)
			{
				num ^= this.GenericData.GetHashCode();
			}
			if (this.HasRequestSubId)
			{
				num ^= this.RequestSubId.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000DCE RID: 3534 RVA: 0x00032F48 File Offset: 0x00031148
		public override bool Equals(object obj)
		{
			GenericRequest genericRequest = obj as GenericRequest;
			return genericRequest != null && this.RequestId.Equals(genericRequest.RequestId) && this.HasGenericData == genericRequest.HasGenericData && (!this.HasGenericData || this.GenericData.Equals(genericRequest.GenericData)) && this.HasRequestSubId == genericRequest.HasRequestSubId && (!this.HasRequestSubId || this.RequestSubId.Equals(genericRequest.RequestSubId));
		}

		// Token: 0x06000DCF RID: 3535 RVA: 0x00032FD3 File Offset: 0x000311D3
		public void Deserialize(Stream stream)
		{
			GenericRequest.Deserialize(stream, this);
		}

		// Token: 0x06000DD0 RID: 3536 RVA: 0x00032FDD File Offset: 0x000311DD
		public static GenericRequest Deserialize(Stream stream, GenericRequest instance)
		{
			return GenericRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000DD1 RID: 3537 RVA: 0x00032FE8 File Offset: 0x000311E8
		public static GenericRequest DeserializeLengthDelimited(Stream stream)
		{
			GenericRequest genericRequest = new GenericRequest();
			GenericRequest.DeserializeLengthDelimited(stream, genericRequest);
			return genericRequest;
		}

		// Token: 0x06000DD2 RID: 3538 RVA: 0x00033004 File Offset: 0x00031204
		public static GenericRequest DeserializeLengthDelimited(Stream stream, GenericRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GenericRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06000DD3 RID: 3539 RVA: 0x0003302C File Offset: 0x0003122C
		public static GenericRequest Deserialize(Stream stream, GenericRequest instance, long limit)
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
						if (num != 24)
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
							instance.RequestSubId = (int)ProtocolParser.ReadUInt64(stream);
						}
					}
					else if (instance.GenericData == null)
					{
						instance.GenericData = GenericData.DeserializeLengthDelimited(stream);
					}
					else
					{
						GenericData.DeserializeLengthDelimited(stream, instance.GenericData);
					}
				}
				else
				{
					instance.RequestId = (int)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06000DD4 RID: 3540 RVA: 0x000330FB File Offset: 0x000312FB
		public void Serialize(Stream stream)
		{
			GenericRequest.Serialize(stream, this);
		}

		// Token: 0x06000DD5 RID: 3541 RVA: 0x00033104 File Offset: 0x00031304
		public static void Serialize(Stream stream, GenericRequest instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.RequestId));
			if (instance.HasGenericData)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.GenericData.GetSerializedSize());
				GenericData.Serialize(stream, instance.GenericData);
			}
			if (instance.HasRequestSubId)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.RequestSubId));
			}
		}

		// Token: 0x06000DD6 RID: 3542 RVA: 0x00033170 File Offset: 0x00031370
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.RequestId));
			if (this.HasGenericData)
			{
				num += 1U;
				uint serializedSize = this.GenericData.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasRequestSubId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.RequestSubId));
			}
			return num + 1U;
		}

		// Token: 0x040004AC RID: 1196
		public bool HasGenericData;

		// Token: 0x040004AD RID: 1197
		private GenericData _GenericData;

		// Token: 0x040004AE RID: 1198
		public bool HasRequestSubId;

		// Token: 0x040004AF RID: 1199
		private int _RequestSubId;
	}
}
