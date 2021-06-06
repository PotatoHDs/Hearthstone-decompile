using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.games.v1
{
	// Token: 0x02000384 RID: 900
	public class GetFactoryInfoResponse : IProtoBuf
	{
		// Token: 0x17000A72 RID: 2674
		// (get) Token: 0x06003961 RID: 14689 RVA: 0x000BACB2 File Offset: 0x000B8EB2
		// (set) Token: 0x06003962 RID: 14690 RVA: 0x000BACBA File Offset: 0x000B8EBA
		public List<Attribute> Attribute
		{
			get
			{
				return this._Attribute;
			}
			set
			{
				this._Attribute = value;
			}
		}

		// Token: 0x17000A73 RID: 2675
		// (get) Token: 0x06003963 RID: 14691 RVA: 0x000BACB2 File Offset: 0x000B8EB2
		public List<Attribute> AttributeList
		{
			get
			{
				return this._Attribute;
			}
		}

		// Token: 0x17000A74 RID: 2676
		// (get) Token: 0x06003964 RID: 14692 RVA: 0x000BACC3 File Offset: 0x000B8EC3
		public int AttributeCount
		{
			get
			{
				return this._Attribute.Count;
			}
		}

		// Token: 0x06003965 RID: 14693 RVA: 0x000BACD0 File Offset: 0x000B8ED0
		public void AddAttribute(Attribute val)
		{
			this._Attribute.Add(val);
		}

		// Token: 0x06003966 RID: 14694 RVA: 0x000BACDE File Offset: 0x000B8EDE
		public void ClearAttribute()
		{
			this._Attribute.Clear();
		}

		// Token: 0x06003967 RID: 14695 RVA: 0x000BACEB File Offset: 0x000B8EEB
		public void SetAttribute(List<Attribute> val)
		{
			this.Attribute = val;
		}

		// Token: 0x17000A75 RID: 2677
		// (get) Token: 0x06003968 RID: 14696 RVA: 0x000BACF4 File Offset: 0x000B8EF4
		// (set) Token: 0x06003969 RID: 14697 RVA: 0x000BACFC File Offset: 0x000B8EFC
		public List<GameStatsBucket> StatsBucket
		{
			get
			{
				return this._StatsBucket;
			}
			set
			{
				this._StatsBucket = value;
			}
		}

		// Token: 0x17000A76 RID: 2678
		// (get) Token: 0x0600396A RID: 14698 RVA: 0x000BACF4 File Offset: 0x000B8EF4
		public List<GameStatsBucket> StatsBucketList
		{
			get
			{
				return this._StatsBucket;
			}
		}

		// Token: 0x17000A77 RID: 2679
		// (get) Token: 0x0600396B RID: 14699 RVA: 0x000BAD05 File Offset: 0x000B8F05
		public int StatsBucketCount
		{
			get
			{
				return this._StatsBucket.Count;
			}
		}

		// Token: 0x0600396C RID: 14700 RVA: 0x000BAD12 File Offset: 0x000B8F12
		public void AddStatsBucket(GameStatsBucket val)
		{
			this._StatsBucket.Add(val);
		}

		// Token: 0x0600396D RID: 14701 RVA: 0x000BAD20 File Offset: 0x000B8F20
		public void ClearStatsBucket()
		{
			this._StatsBucket.Clear();
		}

		// Token: 0x0600396E RID: 14702 RVA: 0x000BAD2D File Offset: 0x000B8F2D
		public void SetStatsBucket(List<GameStatsBucket> val)
		{
			this.StatsBucket = val;
		}

		// Token: 0x0600396F RID: 14703 RVA: 0x000BAD38 File Offset: 0x000B8F38
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (Attribute attribute in this.Attribute)
			{
				num ^= attribute.GetHashCode();
			}
			foreach (GameStatsBucket gameStatsBucket in this.StatsBucket)
			{
				num ^= gameStatsBucket.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003970 RID: 14704 RVA: 0x000BADE0 File Offset: 0x000B8FE0
		public override bool Equals(object obj)
		{
			GetFactoryInfoResponse getFactoryInfoResponse = obj as GetFactoryInfoResponse;
			if (getFactoryInfoResponse == null)
			{
				return false;
			}
			if (this.Attribute.Count != getFactoryInfoResponse.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Attribute.Count; i++)
			{
				if (!this.Attribute[i].Equals(getFactoryInfoResponse.Attribute[i]))
				{
					return false;
				}
			}
			if (this.StatsBucket.Count != getFactoryInfoResponse.StatsBucket.Count)
			{
				return false;
			}
			for (int j = 0; j < this.StatsBucket.Count; j++)
			{
				if (!this.StatsBucket[j].Equals(getFactoryInfoResponse.StatsBucket[j]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000A78 RID: 2680
		// (get) Token: 0x06003971 RID: 14705 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003972 RID: 14706 RVA: 0x000BAE9C File Offset: 0x000B909C
		public static GetFactoryInfoResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetFactoryInfoResponse>(bs, 0, -1);
		}

		// Token: 0x06003973 RID: 14707 RVA: 0x000BAEA6 File Offset: 0x000B90A6
		public void Deserialize(Stream stream)
		{
			GetFactoryInfoResponse.Deserialize(stream, this);
		}

		// Token: 0x06003974 RID: 14708 RVA: 0x000BAEB0 File Offset: 0x000B90B0
		public static GetFactoryInfoResponse Deserialize(Stream stream, GetFactoryInfoResponse instance)
		{
			return GetFactoryInfoResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003975 RID: 14709 RVA: 0x000BAEBC File Offset: 0x000B90BC
		public static GetFactoryInfoResponse DeserializeLengthDelimited(Stream stream)
		{
			GetFactoryInfoResponse getFactoryInfoResponse = new GetFactoryInfoResponse();
			GetFactoryInfoResponse.DeserializeLengthDelimited(stream, getFactoryInfoResponse);
			return getFactoryInfoResponse;
		}

		// Token: 0x06003976 RID: 14710 RVA: 0x000BAED8 File Offset: 0x000B90D8
		public static GetFactoryInfoResponse DeserializeLengthDelimited(Stream stream, GetFactoryInfoResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetFactoryInfoResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06003977 RID: 14711 RVA: 0x000BAF00 File Offset: 0x000B9100
		public static GetFactoryInfoResponse Deserialize(Stream stream, GetFactoryInfoResponse instance, long limit)
		{
			if (instance.Attribute == null)
			{
				instance.Attribute = new List<Attribute>();
			}
			if (instance.StatsBucket == null)
			{
				instance.StatsBucket = new List<GameStatsBucket>();
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
						instance.StatsBucket.Add(GameStatsBucket.DeserializeLengthDelimited(stream));
					}
				}
				else
				{
					instance.Attribute.Add(bnet.protocol.Attribute.DeserializeLengthDelimited(stream));
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06003978 RID: 14712 RVA: 0x000BAFC8 File Offset: 0x000B91C8
		public void Serialize(Stream stream)
		{
			GetFactoryInfoResponse.Serialize(stream, this);
		}

		// Token: 0x06003979 RID: 14713 RVA: 0x000BAFD4 File Offset: 0x000B91D4
		public static void Serialize(Stream stream, GetFactoryInfoResponse instance)
		{
			if (instance.Attribute.Count > 0)
			{
				foreach (Attribute attribute in instance.Attribute)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, attribute.GetSerializedSize());
					bnet.protocol.Attribute.Serialize(stream, attribute);
				}
			}
			if (instance.StatsBucket.Count > 0)
			{
				foreach (GameStatsBucket gameStatsBucket in instance.StatsBucket)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, gameStatsBucket.GetSerializedSize());
					GameStatsBucket.Serialize(stream, gameStatsBucket);
				}
			}
		}

		// Token: 0x0600397A RID: 14714 RVA: 0x000BB0B0 File Offset: 0x000B92B0
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.Attribute.Count > 0)
			{
				foreach (Attribute attribute in this.Attribute)
				{
					num += 1U;
					uint serializedSize = attribute.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (this.StatsBucket.Count > 0)
			{
				foreach (GameStatsBucket gameStatsBucket in this.StatsBucket)
				{
					num += 1U;
					uint serializedSize2 = gameStatsBucket.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			return num;
		}

		// Token: 0x04001516 RID: 5398
		private List<Attribute> _Attribute = new List<Attribute>();

		// Token: 0x04001517 RID: 5399
		private List<GameStatsBucket> _StatsBucket = new List<GameStatsBucket>();
	}
}
