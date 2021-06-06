using System;
using System.IO;
using System.Text;

namespace bnet.protocol.game_utilities.v2.client
{
	// Token: 0x02000354 RID: 852
	public class GetAllValuesForAttributeRequest : IProtoBuf
	{
		// Token: 0x170009BC RID: 2492
		// (get) Token: 0x0600358F RID: 13711 RVA: 0x000B165B File Offset: 0x000AF85B
		// (set) Token: 0x06003590 RID: 13712 RVA: 0x000B1663 File Offset: 0x000AF863
		public string AttributeKey
		{
			get
			{
				return this._AttributeKey;
			}
			set
			{
				this._AttributeKey = value;
				this.HasAttributeKey = (value != null);
			}
		}

		// Token: 0x06003591 RID: 13713 RVA: 0x000B1676 File Offset: 0x000AF876
		public void SetAttributeKey(string val)
		{
			this.AttributeKey = val;
		}

		// Token: 0x06003592 RID: 13714 RVA: 0x000B1680 File Offset: 0x000AF880
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAttributeKey)
			{
				num ^= this.AttributeKey.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003593 RID: 13715 RVA: 0x000B16B0 File Offset: 0x000AF8B0
		public override bool Equals(object obj)
		{
			GetAllValuesForAttributeRequest getAllValuesForAttributeRequest = obj as GetAllValuesForAttributeRequest;
			return getAllValuesForAttributeRequest != null && this.HasAttributeKey == getAllValuesForAttributeRequest.HasAttributeKey && (!this.HasAttributeKey || this.AttributeKey.Equals(getAllValuesForAttributeRequest.AttributeKey));
		}

		// Token: 0x170009BD RID: 2493
		// (get) Token: 0x06003594 RID: 13716 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003595 RID: 13717 RVA: 0x000B16F5 File Offset: 0x000AF8F5
		public static GetAllValuesForAttributeRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetAllValuesForAttributeRequest>(bs, 0, -1);
		}

		// Token: 0x06003596 RID: 13718 RVA: 0x000B16FF File Offset: 0x000AF8FF
		public void Deserialize(Stream stream)
		{
			GetAllValuesForAttributeRequest.Deserialize(stream, this);
		}

		// Token: 0x06003597 RID: 13719 RVA: 0x000B1709 File Offset: 0x000AF909
		public static GetAllValuesForAttributeRequest Deserialize(Stream stream, GetAllValuesForAttributeRequest instance)
		{
			return GetAllValuesForAttributeRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003598 RID: 13720 RVA: 0x000B1714 File Offset: 0x000AF914
		public static GetAllValuesForAttributeRequest DeserializeLengthDelimited(Stream stream)
		{
			GetAllValuesForAttributeRequest getAllValuesForAttributeRequest = new GetAllValuesForAttributeRequest();
			GetAllValuesForAttributeRequest.DeserializeLengthDelimited(stream, getAllValuesForAttributeRequest);
			return getAllValuesForAttributeRequest;
		}

		// Token: 0x06003599 RID: 13721 RVA: 0x000B1730 File Offset: 0x000AF930
		public static GetAllValuesForAttributeRequest DeserializeLengthDelimited(Stream stream, GetAllValuesForAttributeRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetAllValuesForAttributeRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x0600359A RID: 13722 RVA: 0x000B1758 File Offset: 0x000AF958
		public static GetAllValuesForAttributeRequest Deserialize(Stream stream, GetAllValuesForAttributeRequest instance, long limit)
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
				else if (num == 10)
				{
					instance.AttributeKey = ProtocolParser.ReadString(stream);
				}
				else
				{
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

		// Token: 0x0600359B RID: 13723 RVA: 0x000B17D8 File Offset: 0x000AF9D8
		public void Serialize(Stream stream)
		{
			GetAllValuesForAttributeRequest.Serialize(stream, this);
		}

		// Token: 0x0600359C RID: 13724 RVA: 0x000B17E1 File Offset: 0x000AF9E1
		public static void Serialize(Stream stream, GetAllValuesForAttributeRequest instance)
		{
			if (instance.HasAttributeKey)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.AttributeKey));
			}
		}

		// Token: 0x0600359D RID: 13725 RVA: 0x000B180C File Offset: 0x000AFA0C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAttributeKey)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.AttributeKey);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num;
		}

		// Token: 0x0400146F RID: 5231
		public bool HasAttributeKey;

		// Token: 0x04001470 RID: 5232
		private string _AttributeKey;
	}
}
