using System;
using System.Collections.Generic;
using System.IO;
using bnet.protocol.v2;

namespace bnet.protocol.game_utilities.v2.client
{
	// Token: 0x02000355 RID: 853
	public class GetAllValuesForAttributeResponse : IProtoBuf
	{
		// Token: 0x170009BE RID: 2494
		// (get) Token: 0x0600359F RID: 13727 RVA: 0x000B1844 File Offset: 0x000AFA44
		// (set) Token: 0x060035A0 RID: 13728 RVA: 0x000B184C File Offset: 0x000AFA4C
		public List<Variant> AttributeValue
		{
			get
			{
				return this._AttributeValue;
			}
			set
			{
				this._AttributeValue = value;
			}
		}

		// Token: 0x170009BF RID: 2495
		// (get) Token: 0x060035A1 RID: 13729 RVA: 0x000B1844 File Offset: 0x000AFA44
		public List<Variant> AttributeValueList
		{
			get
			{
				return this._AttributeValue;
			}
		}

		// Token: 0x170009C0 RID: 2496
		// (get) Token: 0x060035A2 RID: 13730 RVA: 0x000B1855 File Offset: 0x000AFA55
		public int AttributeValueCount
		{
			get
			{
				return this._AttributeValue.Count;
			}
		}

		// Token: 0x060035A3 RID: 13731 RVA: 0x000B1862 File Offset: 0x000AFA62
		public void AddAttributeValue(Variant val)
		{
			this._AttributeValue.Add(val);
		}

		// Token: 0x060035A4 RID: 13732 RVA: 0x000B1870 File Offset: 0x000AFA70
		public void ClearAttributeValue()
		{
			this._AttributeValue.Clear();
		}

		// Token: 0x060035A5 RID: 13733 RVA: 0x000B187D File Offset: 0x000AFA7D
		public void SetAttributeValue(List<Variant> val)
		{
			this.AttributeValue = val;
		}

		// Token: 0x060035A6 RID: 13734 RVA: 0x000B1888 File Offset: 0x000AFA88
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (Variant variant in this.AttributeValue)
			{
				num ^= variant.GetHashCode();
			}
			return num;
		}

		// Token: 0x060035A7 RID: 13735 RVA: 0x000B18EC File Offset: 0x000AFAEC
		public override bool Equals(object obj)
		{
			GetAllValuesForAttributeResponse getAllValuesForAttributeResponse = obj as GetAllValuesForAttributeResponse;
			if (getAllValuesForAttributeResponse == null)
			{
				return false;
			}
			if (this.AttributeValue.Count != getAllValuesForAttributeResponse.AttributeValue.Count)
			{
				return false;
			}
			for (int i = 0; i < this.AttributeValue.Count; i++)
			{
				if (!this.AttributeValue[i].Equals(getAllValuesForAttributeResponse.AttributeValue[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x170009C1 RID: 2497
		// (get) Token: 0x060035A8 RID: 13736 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060035A9 RID: 13737 RVA: 0x000B1957 File Offset: 0x000AFB57
		public static GetAllValuesForAttributeResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetAllValuesForAttributeResponse>(bs, 0, -1);
		}

		// Token: 0x060035AA RID: 13738 RVA: 0x000B1961 File Offset: 0x000AFB61
		public void Deserialize(Stream stream)
		{
			GetAllValuesForAttributeResponse.Deserialize(stream, this);
		}

		// Token: 0x060035AB RID: 13739 RVA: 0x000B196B File Offset: 0x000AFB6B
		public static GetAllValuesForAttributeResponse Deserialize(Stream stream, GetAllValuesForAttributeResponse instance)
		{
			return GetAllValuesForAttributeResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060035AC RID: 13740 RVA: 0x000B1978 File Offset: 0x000AFB78
		public static GetAllValuesForAttributeResponse DeserializeLengthDelimited(Stream stream)
		{
			GetAllValuesForAttributeResponse getAllValuesForAttributeResponse = new GetAllValuesForAttributeResponse();
			GetAllValuesForAttributeResponse.DeserializeLengthDelimited(stream, getAllValuesForAttributeResponse);
			return getAllValuesForAttributeResponse;
		}

		// Token: 0x060035AD RID: 13741 RVA: 0x000B1994 File Offset: 0x000AFB94
		public static GetAllValuesForAttributeResponse DeserializeLengthDelimited(Stream stream, GetAllValuesForAttributeResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetAllValuesForAttributeResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x060035AE RID: 13742 RVA: 0x000B19BC File Offset: 0x000AFBBC
		public static GetAllValuesForAttributeResponse Deserialize(Stream stream, GetAllValuesForAttributeResponse instance, long limit)
		{
			if (instance.AttributeValue == null)
			{
				instance.AttributeValue = new List<Variant>();
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
				else if (num == 10)
				{
					instance.AttributeValue.Add(Variant.DeserializeLengthDelimited(stream));
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

		// Token: 0x060035AF RID: 13743 RVA: 0x000B1A54 File Offset: 0x000AFC54
		public void Serialize(Stream stream)
		{
			GetAllValuesForAttributeResponse.Serialize(stream, this);
		}

		// Token: 0x060035B0 RID: 13744 RVA: 0x000B1A60 File Offset: 0x000AFC60
		public static void Serialize(Stream stream, GetAllValuesForAttributeResponse instance)
		{
			if (instance.AttributeValue.Count > 0)
			{
				foreach (Variant variant in instance.AttributeValue)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, variant.GetSerializedSize());
					Variant.Serialize(stream, variant);
				}
			}
		}

		// Token: 0x060035B1 RID: 13745 RVA: 0x000B1AD8 File Offset: 0x000AFCD8
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.AttributeValue.Count > 0)
			{
				foreach (Variant variant in this.AttributeValue)
				{
					num += 1U;
					uint serializedSize = variant.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num;
		}

		// Token: 0x04001471 RID: 5233
		private List<Variant> _AttributeValue = new List<Variant>();
	}
}
