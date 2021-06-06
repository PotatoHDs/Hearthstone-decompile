using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.game_utilities.v1
{
	// Token: 0x02000362 RID: 866
	public class GetAllValuesForAttributeResponse : IProtoBuf
	{
		// Token: 0x170009F7 RID: 2551
		// (get) Token: 0x060036BE RID: 14014 RVA: 0x000B488B File Offset: 0x000B2A8B
		// (set) Token: 0x060036BF RID: 14015 RVA: 0x000B4893 File Offset: 0x000B2A93
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

		// Token: 0x170009F8 RID: 2552
		// (get) Token: 0x060036C0 RID: 14016 RVA: 0x000B488B File Offset: 0x000B2A8B
		public List<Variant> AttributeValueList
		{
			get
			{
				return this._AttributeValue;
			}
		}

		// Token: 0x170009F9 RID: 2553
		// (get) Token: 0x060036C1 RID: 14017 RVA: 0x000B489C File Offset: 0x000B2A9C
		public int AttributeValueCount
		{
			get
			{
				return this._AttributeValue.Count;
			}
		}

		// Token: 0x060036C2 RID: 14018 RVA: 0x000B48A9 File Offset: 0x000B2AA9
		public void AddAttributeValue(Variant val)
		{
			this._AttributeValue.Add(val);
		}

		// Token: 0x060036C3 RID: 14019 RVA: 0x000B48B7 File Offset: 0x000B2AB7
		public void ClearAttributeValue()
		{
			this._AttributeValue.Clear();
		}

		// Token: 0x060036C4 RID: 14020 RVA: 0x000B48C4 File Offset: 0x000B2AC4
		public void SetAttributeValue(List<Variant> val)
		{
			this.AttributeValue = val;
		}

		// Token: 0x060036C5 RID: 14021 RVA: 0x000B48D0 File Offset: 0x000B2AD0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (Variant variant in this.AttributeValue)
			{
				num ^= variant.GetHashCode();
			}
			return num;
		}

		// Token: 0x060036C6 RID: 14022 RVA: 0x000B4934 File Offset: 0x000B2B34
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

		// Token: 0x170009FA RID: 2554
		// (get) Token: 0x060036C7 RID: 14023 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060036C8 RID: 14024 RVA: 0x000B499F File Offset: 0x000B2B9F
		public static GetAllValuesForAttributeResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetAllValuesForAttributeResponse>(bs, 0, -1);
		}

		// Token: 0x060036C9 RID: 14025 RVA: 0x000B49A9 File Offset: 0x000B2BA9
		public void Deserialize(Stream stream)
		{
			GetAllValuesForAttributeResponse.Deserialize(stream, this);
		}

		// Token: 0x060036CA RID: 14026 RVA: 0x000B49B3 File Offset: 0x000B2BB3
		public static GetAllValuesForAttributeResponse Deserialize(Stream stream, GetAllValuesForAttributeResponse instance)
		{
			return GetAllValuesForAttributeResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060036CB RID: 14027 RVA: 0x000B49C0 File Offset: 0x000B2BC0
		public static GetAllValuesForAttributeResponse DeserializeLengthDelimited(Stream stream)
		{
			GetAllValuesForAttributeResponse getAllValuesForAttributeResponse = new GetAllValuesForAttributeResponse();
			GetAllValuesForAttributeResponse.DeserializeLengthDelimited(stream, getAllValuesForAttributeResponse);
			return getAllValuesForAttributeResponse;
		}

		// Token: 0x060036CC RID: 14028 RVA: 0x000B49DC File Offset: 0x000B2BDC
		public static GetAllValuesForAttributeResponse DeserializeLengthDelimited(Stream stream, GetAllValuesForAttributeResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetAllValuesForAttributeResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x060036CD RID: 14029 RVA: 0x000B4A04 File Offset: 0x000B2C04
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

		// Token: 0x060036CE RID: 14030 RVA: 0x000B4A9C File Offset: 0x000B2C9C
		public void Serialize(Stream stream)
		{
			GetAllValuesForAttributeResponse.Serialize(stream, this);
		}

		// Token: 0x060036CF RID: 14031 RVA: 0x000B4AA8 File Offset: 0x000B2CA8
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

		// Token: 0x060036D0 RID: 14032 RVA: 0x000B4B20 File Offset: 0x000B2D20
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

		// Token: 0x040014A2 RID: 5282
		private List<Variant> _AttributeValue = new List<Variant>();
	}
}
