using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.config
{
	// Token: 0x02000351 RID: 849
	public class ServiceAliases : IProtoBuf
	{
		// Token: 0x170009AD RID: 2477
		// (get) Token: 0x0600354C RID: 13644 RVA: 0x000B0B34 File Offset: 0x000AED34
		// (set) Token: 0x0600354D RID: 13645 RVA: 0x000B0B3C File Offset: 0x000AED3C
		public List<ProtocolAlias> ProtocolAlias
		{
			get
			{
				return this._ProtocolAlias;
			}
			set
			{
				this._ProtocolAlias = value;
			}
		}

		// Token: 0x170009AE RID: 2478
		// (get) Token: 0x0600354E RID: 13646 RVA: 0x000B0B34 File Offset: 0x000AED34
		public List<ProtocolAlias> ProtocolAliasList
		{
			get
			{
				return this._ProtocolAlias;
			}
		}

		// Token: 0x170009AF RID: 2479
		// (get) Token: 0x0600354F RID: 13647 RVA: 0x000B0B45 File Offset: 0x000AED45
		public int ProtocolAliasCount
		{
			get
			{
				return this._ProtocolAlias.Count;
			}
		}

		// Token: 0x06003550 RID: 13648 RVA: 0x000B0B52 File Offset: 0x000AED52
		public void AddProtocolAlias(ProtocolAlias val)
		{
			this._ProtocolAlias.Add(val);
		}

		// Token: 0x06003551 RID: 13649 RVA: 0x000B0B60 File Offset: 0x000AED60
		public void ClearProtocolAlias()
		{
			this._ProtocolAlias.Clear();
		}

		// Token: 0x06003552 RID: 13650 RVA: 0x000B0B6D File Offset: 0x000AED6D
		public void SetProtocolAlias(List<ProtocolAlias> val)
		{
			this.ProtocolAlias = val;
		}

		// Token: 0x06003553 RID: 13651 RVA: 0x000B0B78 File Offset: 0x000AED78
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (ProtocolAlias protocolAlias in this.ProtocolAlias)
			{
				num ^= protocolAlias.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003554 RID: 13652 RVA: 0x000B0BDC File Offset: 0x000AEDDC
		public override bool Equals(object obj)
		{
			ServiceAliases serviceAliases = obj as ServiceAliases;
			if (serviceAliases == null)
			{
				return false;
			}
			if (this.ProtocolAlias.Count != serviceAliases.ProtocolAlias.Count)
			{
				return false;
			}
			for (int i = 0; i < this.ProtocolAlias.Count; i++)
			{
				if (!this.ProtocolAlias[i].Equals(serviceAliases.ProtocolAlias[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x170009B0 RID: 2480
		// (get) Token: 0x06003555 RID: 13653 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003556 RID: 13654 RVA: 0x000B0C47 File Offset: 0x000AEE47
		public static ServiceAliases ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ServiceAliases>(bs, 0, -1);
		}

		// Token: 0x06003557 RID: 13655 RVA: 0x000B0C51 File Offset: 0x000AEE51
		public void Deserialize(Stream stream)
		{
			ServiceAliases.Deserialize(stream, this);
		}

		// Token: 0x06003558 RID: 13656 RVA: 0x000B0C5B File Offset: 0x000AEE5B
		public static ServiceAliases Deserialize(Stream stream, ServiceAliases instance)
		{
			return ServiceAliases.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003559 RID: 13657 RVA: 0x000B0C68 File Offset: 0x000AEE68
		public static ServiceAliases DeserializeLengthDelimited(Stream stream)
		{
			ServiceAliases serviceAliases = new ServiceAliases();
			ServiceAliases.DeserializeLengthDelimited(stream, serviceAliases);
			return serviceAliases;
		}

		// Token: 0x0600355A RID: 13658 RVA: 0x000B0C84 File Offset: 0x000AEE84
		public static ServiceAliases DeserializeLengthDelimited(Stream stream, ServiceAliases instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ServiceAliases.Deserialize(stream, instance, num);
		}

		// Token: 0x0600355B RID: 13659 RVA: 0x000B0CAC File Offset: 0x000AEEAC
		public static ServiceAliases Deserialize(Stream stream, ServiceAliases instance, long limit)
		{
			if (instance.ProtocolAlias == null)
			{
				instance.ProtocolAlias = new List<ProtocolAlias>();
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
					instance.ProtocolAlias.Add(bnet.protocol.config.ProtocolAlias.DeserializeLengthDelimited(stream));
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

		// Token: 0x0600355C RID: 13660 RVA: 0x000B0D44 File Offset: 0x000AEF44
		public void Serialize(Stream stream)
		{
			ServiceAliases.Serialize(stream, this);
		}

		// Token: 0x0600355D RID: 13661 RVA: 0x000B0D50 File Offset: 0x000AEF50
		public static void Serialize(Stream stream, ServiceAliases instance)
		{
			if (instance.ProtocolAlias.Count > 0)
			{
				foreach (ProtocolAlias protocolAlias in instance.ProtocolAlias)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, protocolAlias.GetSerializedSize());
					bnet.protocol.config.ProtocolAlias.Serialize(stream, protocolAlias);
				}
			}
		}

		// Token: 0x0600355E RID: 13662 RVA: 0x000B0DC8 File Offset: 0x000AEFC8
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.ProtocolAlias.Count > 0)
			{
				foreach (ProtocolAlias protocolAlias in this.ProtocolAlias)
				{
					num += 1U;
					uint serializedSize = protocolAlias.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num;
		}

		// Token: 0x0400146B RID: 5227
		private List<ProtocolAlias> _ProtocolAlias = new List<ProtocolAlias>();
	}
}
