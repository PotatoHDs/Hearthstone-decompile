using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace bnet.protocol.account.v1
{
	// Token: 0x0200051A RID: 1306
	public class GetAuthorizedDataRequest : IProtoBuf
	{
		// Token: 0x17001187 RID: 4487
		// (get) Token: 0x06005D0E RID: 23822 RVA: 0x0011A79B File Offset: 0x0011899B
		// (set) Token: 0x06005D0F RID: 23823 RVA: 0x0011A7A3 File Offset: 0x001189A3
		public EntityId EntityId
		{
			get
			{
				return this._EntityId;
			}
			set
			{
				this._EntityId = value;
				this.HasEntityId = (value != null);
			}
		}

		// Token: 0x06005D10 RID: 23824 RVA: 0x0011A7B6 File Offset: 0x001189B6
		public void SetEntityId(EntityId val)
		{
			this.EntityId = val;
		}

		// Token: 0x17001188 RID: 4488
		// (get) Token: 0x06005D11 RID: 23825 RVA: 0x0011A7BF File Offset: 0x001189BF
		// (set) Token: 0x06005D12 RID: 23826 RVA: 0x0011A7C7 File Offset: 0x001189C7
		public List<string> Tag
		{
			get
			{
				return this._Tag;
			}
			set
			{
				this._Tag = value;
			}
		}

		// Token: 0x17001189 RID: 4489
		// (get) Token: 0x06005D13 RID: 23827 RVA: 0x0011A7BF File Offset: 0x001189BF
		public List<string> TagList
		{
			get
			{
				return this._Tag;
			}
		}

		// Token: 0x1700118A RID: 4490
		// (get) Token: 0x06005D14 RID: 23828 RVA: 0x0011A7D0 File Offset: 0x001189D0
		public int TagCount
		{
			get
			{
				return this._Tag.Count;
			}
		}

		// Token: 0x06005D15 RID: 23829 RVA: 0x0011A7DD File Offset: 0x001189DD
		public void AddTag(string val)
		{
			this._Tag.Add(val);
		}

		// Token: 0x06005D16 RID: 23830 RVA: 0x0011A7EB File Offset: 0x001189EB
		public void ClearTag()
		{
			this._Tag.Clear();
		}

		// Token: 0x06005D17 RID: 23831 RVA: 0x0011A7F8 File Offset: 0x001189F8
		public void SetTag(List<string> val)
		{
			this.Tag = val;
		}

		// Token: 0x1700118B RID: 4491
		// (get) Token: 0x06005D18 RID: 23832 RVA: 0x0011A801 File Offset: 0x00118A01
		// (set) Token: 0x06005D19 RID: 23833 RVA: 0x0011A809 File Offset: 0x00118A09
		public bool PrivilegedNetwork
		{
			get
			{
				return this._PrivilegedNetwork;
			}
			set
			{
				this._PrivilegedNetwork = value;
				this.HasPrivilegedNetwork = true;
			}
		}

		// Token: 0x06005D1A RID: 23834 RVA: 0x0011A819 File Offset: 0x00118A19
		public void SetPrivilegedNetwork(bool val)
		{
			this.PrivilegedNetwork = val;
		}

		// Token: 0x06005D1B RID: 23835 RVA: 0x0011A824 File Offset: 0x00118A24
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasEntityId)
			{
				num ^= this.EntityId.GetHashCode();
			}
			foreach (string text in this.Tag)
			{
				num ^= text.GetHashCode();
			}
			if (this.HasPrivilegedNetwork)
			{
				num ^= this.PrivilegedNetwork.GetHashCode();
			}
			return num;
		}

		// Token: 0x06005D1C RID: 23836 RVA: 0x0011A8B8 File Offset: 0x00118AB8
		public override bool Equals(object obj)
		{
			GetAuthorizedDataRequest getAuthorizedDataRequest = obj as GetAuthorizedDataRequest;
			if (getAuthorizedDataRequest == null)
			{
				return false;
			}
			if (this.HasEntityId != getAuthorizedDataRequest.HasEntityId || (this.HasEntityId && !this.EntityId.Equals(getAuthorizedDataRequest.EntityId)))
			{
				return false;
			}
			if (this.Tag.Count != getAuthorizedDataRequest.Tag.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Tag.Count; i++)
			{
				if (!this.Tag[i].Equals(getAuthorizedDataRequest.Tag[i]))
				{
					return false;
				}
			}
			return this.HasPrivilegedNetwork == getAuthorizedDataRequest.HasPrivilegedNetwork && (!this.HasPrivilegedNetwork || this.PrivilegedNetwork.Equals(getAuthorizedDataRequest.PrivilegedNetwork));
		}

		// Token: 0x1700118C RID: 4492
		// (get) Token: 0x06005D1D RID: 23837 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005D1E RID: 23838 RVA: 0x0011A97C File Offset: 0x00118B7C
		public static GetAuthorizedDataRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetAuthorizedDataRequest>(bs, 0, -1);
		}

		// Token: 0x06005D1F RID: 23839 RVA: 0x0011A986 File Offset: 0x00118B86
		public void Deserialize(Stream stream)
		{
			GetAuthorizedDataRequest.Deserialize(stream, this);
		}

		// Token: 0x06005D20 RID: 23840 RVA: 0x0011A990 File Offset: 0x00118B90
		public static GetAuthorizedDataRequest Deserialize(Stream stream, GetAuthorizedDataRequest instance)
		{
			return GetAuthorizedDataRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005D21 RID: 23841 RVA: 0x0011A99C File Offset: 0x00118B9C
		public static GetAuthorizedDataRequest DeserializeLengthDelimited(Stream stream)
		{
			GetAuthorizedDataRequest getAuthorizedDataRequest = new GetAuthorizedDataRequest();
			GetAuthorizedDataRequest.DeserializeLengthDelimited(stream, getAuthorizedDataRequest);
			return getAuthorizedDataRequest;
		}

		// Token: 0x06005D22 RID: 23842 RVA: 0x0011A9B8 File Offset: 0x00118BB8
		public static GetAuthorizedDataRequest DeserializeLengthDelimited(Stream stream, GetAuthorizedDataRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetAuthorizedDataRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06005D23 RID: 23843 RVA: 0x0011A9E0 File Offset: 0x00118BE0
		public static GetAuthorizedDataRequest Deserialize(Stream stream, GetAuthorizedDataRequest instance, long limit)
		{
			if (instance.Tag == null)
			{
				instance.Tag = new List<string>();
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
							instance.PrivilegedNetwork = ProtocolParser.ReadBool(stream);
						}
					}
					else
					{
						instance.Tag.Add(ProtocolParser.ReadString(stream));
					}
				}
				else if (instance.EntityId == null)
				{
					instance.EntityId = EntityId.DeserializeLengthDelimited(stream);
				}
				else
				{
					EntityId.DeserializeLengthDelimited(stream, instance.EntityId);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06005D24 RID: 23844 RVA: 0x0011AAC6 File Offset: 0x00118CC6
		public void Serialize(Stream stream)
		{
			GetAuthorizedDataRequest.Serialize(stream, this);
		}

		// Token: 0x06005D25 RID: 23845 RVA: 0x0011AAD0 File Offset: 0x00118CD0
		public static void Serialize(Stream stream, GetAuthorizedDataRequest instance)
		{
			if (instance.HasEntityId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.EntityId.GetSerializedSize());
				EntityId.Serialize(stream, instance.EntityId);
			}
			if (instance.Tag.Count > 0)
			{
				foreach (string s in instance.Tag)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(s));
				}
			}
			if (instance.HasPrivilegedNetwork)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteBool(stream, instance.PrivilegedNetwork);
			}
		}

		// Token: 0x06005D26 RID: 23846 RVA: 0x0011AB8C File Offset: 0x00118D8C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasEntityId)
			{
				num += 1U;
				uint serializedSize = this.EntityId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.Tag.Count > 0)
			{
				foreach (string s in this.Tag)
				{
					num += 1U;
					uint byteCount = (uint)Encoding.UTF8.GetByteCount(s);
					num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
				}
			}
			if (this.HasPrivilegedNetwork)
			{
				num += 1U;
				num += 1U;
			}
			return num;
		}

		// Token: 0x04001CB6 RID: 7350
		public bool HasEntityId;

		// Token: 0x04001CB7 RID: 7351
		private EntityId _EntityId;

		// Token: 0x04001CB8 RID: 7352
		private List<string> _Tag = new List<string>();

		// Token: 0x04001CB9 RID: 7353
		public bool HasPrivilegedNetwork;

		// Token: 0x04001CBA RID: 7354
		private bool _PrivilegedNetwork;
	}
}
