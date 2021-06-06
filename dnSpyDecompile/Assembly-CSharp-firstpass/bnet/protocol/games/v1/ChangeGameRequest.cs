using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.games.v1
{
	// Token: 0x02000382 RID: 898
	public class ChangeGameRequest : IProtoBuf
	{
		// Token: 0x17000A69 RID: 2665
		// (get) Token: 0x06003934 RID: 14644 RVA: 0x000BA5E2 File Offset: 0x000B87E2
		// (set) Token: 0x06003935 RID: 14645 RVA: 0x000BA5EA File Offset: 0x000B87EA
		public GameHandle GameHandle { get; set; }

		// Token: 0x06003936 RID: 14646 RVA: 0x000BA5F3 File Offset: 0x000B87F3
		public void SetGameHandle(GameHandle val)
		{
			this.GameHandle = val;
		}

		// Token: 0x17000A6A RID: 2666
		// (get) Token: 0x06003937 RID: 14647 RVA: 0x000BA5FC File Offset: 0x000B87FC
		// (set) Token: 0x06003938 RID: 14648 RVA: 0x000BA604 File Offset: 0x000B8804
		public bool Open
		{
			get
			{
				return this._Open;
			}
			set
			{
				this._Open = value;
				this.HasOpen = true;
			}
		}

		// Token: 0x06003939 RID: 14649 RVA: 0x000BA614 File Offset: 0x000B8814
		public void SetOpen(bool val)
		{
			this.Open = val;
		}

		// Token: 0x17000A6B RID: 2667
		// (get) Token: 0x0600393A RID: 14650 RVA: 0x000BA61D File Offset: 0x000B881D
		// (set) Token: 0x0600393B RID: 14651 RVA: 0x000BA625 File Offset: 0x000B8825
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

		// Token: 0x17000A6C RID: 2668
		// (get) Token: 0x0600393C RID: 14652 RVA: 0x000BA61D File Offset: 0x000B881D
		public List<Attribute> AttributeList
		{
			get
			{
				return this._Attribute;
			}
		}

		// Token: 0x17000A6D RID: 2669
		// (get) Token: 0x0600393D RID: 14653 RVA: 0x000BA62E File Offset: 0x000B882E
		public int AttributeCount
		{
			get
			{
				return this._Attribute.Count;
			}
		}

		// Token: 0x0600393E RID: 14654 RVA: 0x000BA63B File Offset: 0x000B883B
		public void AddAttribute(Attribute val)
		{
			this._Attribute.Add(val);
		}

		// Token: 0x0600393F RID: 14655 RVA: 0x000BA649 File Offset: 0x000B8849
		public void ClearAttribute()
		{
			this._Attribute.Clear();
		}

		// Token: 0x06003940 RID: 14656 RVA: 0x000BA656 File Offset: 0x000B8856
		public void SetAttribute(List<Attribute> val)
		{
			this.Attribute = val;
		}

		// Token: 0x17000A6E RID: 2670
		// (get) Token: 0x06003941 RID: 14657 RVA: 0x000BA65F File Offset: 0x000B885F
		// (set) Token: 0x06003942 RID: 14658 RVA: 0x000BA667 File Offset: 0x000B8867
		public bool Replace
		{
			get
			{
				return this._Replace;
			}
			set
			{
				this._Replace = value;
				this.HasReplace = true;
			}
		}

		// Token: 0x06003943 RID: 14659 RVA: 0x000BA677 File Offset: 0x000B8877
		public void SetReplace(bool val)
		{
			this.Replace = val;
		}

		// Token: 0x06003944 RID: 14660 RVA: 0x000BA680 File Offset: 0x000B8880
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.GameHandle.GetHashCode();
			if (this.HasOpen)
			{
				num ^= this.Open.GetHashCode();
			}
			foreach (Attribute attribute in this.Attribute)
			{
				num ^= attribute.GetHashCode();
			}
			if (this.HasReplace)
			{
				num ^= this.Replace.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003945 RID: 14661 RVA: 0x000BA724 File Offset: 0x000B8924
		public override bool Equals(object obj)
		{
			ChangeGameRequest changeGameRequest = obj as ChangeGameRequest;
			if (changeGameRequest == null)
			{
				return false;
			}
			if (!this.GameHandle.Equals(changeGameRequest.GameHandle))
			{
				return false;
			}
			if (this.HasOpen != changeGameRequest.HasOpen || (this.HasOpen && !this.Open.Equals(changeGameRequest.Open)))
			{
				return false;
			}
			if (this.Attribute.Count != changeGameRequest.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Attribute.Count; i++)
			{
				if (!this.Attribute[i].Equals(changeGameRequest.Attribute[i]))
				{
					return false;
				}
			}
			return this.HasReplace == changeGameRequest.HasReplace && (!this.HasReplace || this.Replace.Equals(changeGameRequest.Replace));
		}

		// Token: 0x17000A6F RID: 2671
		// (get) Token: 0x06003946 RID: 14662 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003947 RID: 14663 RVA: 0x000BA800 File Offset: 0x000B8A00
		public static ChangeGameRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ChangeGameRequest>(bs, 0, -1);
		}

		// Token: 0x06003948 RID: 14664 RVA: 0x000BA80A File Offset: 0x000B8A0A
		public void Deserialize(Stream stream)
		{
			ChangeGameRequest.Deserialize(stream, this);
		}

		// Token: 0x06003949 RID: 14665 RVA: 0x000BA814 File Offset: 0x000B8A14
		public static ChangeGameRequest Deserialize(Stream stream, ChangeGameRequest instance)
		{
			return ChangeGameRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600394A RID: 14666 RVA: 0x000BA820 File Offset: 0x000B8A20
		public static ChangeGameRequest DeserializeLengthDelimited(Stream stream)
		{
			ChangeGameRequest changeGameRequest = new ChangeGameRequest();
			ChangeGameRequest.DeserializeLengthDelimited(stream, changeGameRequest);
			return changeGameRequest;
		}

		// Token: 0x0600394B RID: 14667 RVA: 0x000BA83C File Offset: 0x000B8A3C
		public static ChangeGameRequest DeserializeLengthDelimited(Stream stream, ChangeGameRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ChangeGameRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x0600394C RID: 14668 RVA: 0x000BA864 File Offset: 0x000B8A64
		public static ChangeGameRequest Deserialize(Stream stream, ChangeGameRequest instance, long limit)
		{
			if (instance.Attribute == null)
			{
				instance.Attribute = new List<Attribute>();
			}
			instance.Replace = false;
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
						if (num != 10)
						{
							if (num == 16)
							{
								instance.Open = ProtocolParser.ReadBool(stream);
								continue;
							}
						}
						else
						{
							if (instance.GameHandle == null)
							{
								instance.GameHandle = GameHandle.DeserializeLengthDelimited(stream);
								continue;
							}
							GameHandle.DeserializeLengthDelimited(stream, instance.GameHandle);
							continue;
						}
					}
					else
					{
						if (num == 26)
						{
							instance.Attribute.Add(bnet.protocol.Attribute.DeserializeLengthDelimited(stream));
							continue;
						}
						if (num == 32)
						{
							instance.Replace = ProtocolParser.ReadBool(stream);
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

		// Token: 0x0600394D RID: 14669 RVA: 0x000BA96E File Offset: 0x000B8B6E
		public void Serialize(Stream stream)
		{
			ChangeGameRequest.Serialize(stream, this);
		}

		// Token: 0x0600394E RID: 14670 RVA: 0x000BA978 File Offset: 0x000B8B78
		public static void Serialize(Stream stream, ChangeGameRequest instance)
		{
			if (instance.GameHandle == null)
			{
				throw new ArgumentNullException("GameHandle", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.GameHandle.GetSerializedSize());
			GameHandle.Serialize(stream, instance.GameHandle);
			if (instance.HasOpen)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteBool(stream, instance.Open);
			}
			if (instance.Attribute.Count > 0)
			{
				foreach (Attribute attribute in instance.Attribute)
				{
					stream.WriteByte(26);
					ProtocolParser.WriteUInt32(stream, attribute.GetSerializedSize());
					bnet.protocol.Attribute.Serialize(stream, attribute);
				}
			}
			if (instance.HasReplace)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteBool(stream, instance.Replace);
			}
		}

		// Token: 0x0600394F RID: 14671 RVA: 0x000BAA64 File Offset: 0x000B8C64
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint serializedSize = this.GameHandle.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (this.HasOpen)
			{
				num += 1U;
				num += 1U;
			}
			if (this.Attribute.Count > 0)
			{
				foreach (Attribute attribute in this.Attribute)
				{
					num += 1U;
					uint serializedSize2 = attribute.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			if (this.HasReplace)
			{
				num += 1U;
				num += 1U;
			}
			num += 1U;
			return num;
		}

		// Token: 0x04001510 RID: 5392
		public bool HasOpen;

		// Token: 0x04001511 RID: 5393
		private bool _Open;

		// Token: 0x04001512 RID: 5394
		private List<Attribute> _Attribute = new List<Attribute>();

		// Token: 0x04001513 RID: 5395
		public bool HasReplace;

		// Token: 0x04001514 RID: 5396
		private bool _Replace;
	}
}
