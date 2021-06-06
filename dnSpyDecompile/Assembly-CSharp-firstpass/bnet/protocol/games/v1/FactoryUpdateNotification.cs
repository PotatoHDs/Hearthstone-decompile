using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.games.v1
{
	// Token: 0x02000392 RID: 914
	public class FactoryUpdateNotification : IProtoBuf
	{
		// Token: 0x17000AAC RID: 2732
		// (get) Token: 0x06003A89 RID: 14985 RVA: 0x000BDB03 File Offset: 0x000BBD03
		// (set) Token: 0x06003A8A RID: 14986 RVA: 0x000BDB0B File Offset: 0x000BBD0B
		public FactoryUpdateNotification.Types.Operation Op { get; set; }

		// Token: 0x06003A8B RID: 14987 RVA: 0x000BDB14 File Offset: 0x000BBD14
		public void SetOp(FactoryUpdateNotification.Types.Operation val)
		{
			this.Op = val;
		}

		// Token: 0x17000AAD RID: 2733
		// (get) Token: 0x06003A8C RID: 14988 RVA: 0x000BDB1D File Offset: 0x000BBD1D
		// (set) Token: 0x06003A8D RID: 14989 RVA: 0x000BDB25 File Offset: 0x000BBD25
		public GameFactoryDescription Description { get; set; }

		// Token: 0x06003A8E RID: 14990 RVA: 0x000BDB2E File Offset: 0x000BBD2E
		public void SetDescription(GameFactoryDescription val)
		{
			this.Description = val;
		}

		// Token: 0x17000AAE RID: 2734
		// (get) Token: 0x06003A8F RID: 14991 RVA: 0x000BDB37 File Offset: 0x000BBD37
		// (set) Token: 0x06003A90 RID: 14992 RVA: 0x000BDB3F File Offset: 0x000BBD3F
		public uint Program
		{
			get
			{
				return this._Program;
			}
			set
			{
				this._Program = value;
				this.HasProgram = true;
			}
		}

		// Token: 0x06003A91 RID: 14993 RVA: 0x000BDB4F File Offset: 0x000BBD4F
		public void SetProgram(uint val)
		{
			this.Program = val;
		}

		// Token: 0x17000AAF RID: 2735
		// (get) Token: 0x06003A92 RID: 14994 RVA: 0x000BDB58 File Offset: 0x000BBD58
		// (set) Token: 0x06003A93 RID: 14995 RVA: 0x000BDB60 File Offset: 0x000BBD60
		public List<HostRoute> Hosts
		{
			get
			{
				return this._Hosts;
			}
			set
			{
				this._Hosts = value;
			}
		}

		// Token: 0x17000AB0 RID: 2736
		// (get) Token: 0x06003A94 RID: 14996 RVA: 0x000BDB58 File Offset: 0x000BBD58
		public List<HostRoute> HostsList
		{
			get
			{
				return this._Hosts;
			}
		}

		// Token: 0x17000AB1 RID: 2737
		// (get) Token: 0x06003A95 RID: 14997 RVA: 0x000BDB69 File Offset: 0x000BBD69
		public int HostsCount
		{
			get
			{
				return this._Hosts.Count;
			}
		}

		// Token: 0x06003A96 RID: 14998 RVA: 0x000BDB76 File Offset: 0x000BBD76
		public void AddHosts(HostRoute val)
		{
			this._Hosts.Add(val);
		}

		// Token: 0x06003A97 RID: 14999 RVA: 0x000BDB84 File Offset: 0x000BBD84
		public void ClearHosts()
		{
			this._Hosts.Clear();
		}

		// Token: 0x06003A98 RID: 15000 RVA: 0x000BDB91 File Offset: 0x000BBD91
		public void SetHosts(List<HostRoute> val)
		{
			this.Hosts = val;
		}

		// Token: 0x06003A99 RID: 15001 RVA: 0x000BDB9C File Offset: 0x000BBD9C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Op.GetHashCode();
			num ^= this.Description.GetHashCode();
			if (this.HasProgram)
			{
				num ^= this.Program.GetHashCode();
			}
			foreach (HostRoute hostRoute in this.Hosts)
			{
				num ^= hostRoute.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003A9A RID: 15002 RVA: 0x000BDC40 File Offset: 0x000BBE40
		public override bool Equals(object obj)
		{
			FactoryUpdateNotification factoryUpdateNotification = obj as FactoryUpdateNotification;
			if (factoryUpdateNotification == null)
			{
				return false;
			}
			if (!this.Op.Equals(factoryUpdateNotification.Op))
			{
				return false;
			}
			if (!this.Description.Equals(factoryUpdateNotification.Description))
			{
				return false;
			}
			if (this.HasProgram != factoryUpdateNotification.HasProgram || (this.HasProgram && !this.Program.Equals(factoryUpdateNotification.Program)))
			{
				return false;
			}
			if (this.Hosts.Count != factoryUpdateNotification.Hosts.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Hosts.Count; i++)
			{
				if (!this.Hosts[i].Equals(factoryUpdateNotification.Hosts[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000AB2 RID: 2738
		// (get) Token: 0x06003A9B RID: 15003 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003A9C RID: 15004 RVA: 0x000BDD11 File Offset: 0x000BBF11
		public static FactoryUpdateNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<FactoryUpdateNotification>(bs, 0, -1);
		}

		// Token: 0x06003A9D RID: 15005 RVA: 0x000BDD1B File Offset: 0x000BBF1B
		public void Deserialize(Stream stream)
		{
			FactoryUpdateNotification.Deserialize(stream, this);
		}

		// Token: 0x06003A9E RID: 15006 RVA: 0x000BDD25 File Offset: 0x000BBF25
		public static FactoryUpdateNotification Deserialize(Stream stream, FactoryUpdateNotification instance)
		{
			return FactoryUpdateNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003A9F RID: 15007 RVA: 0x000BDD30 File Offset: 0x000BBF30
		public static FactoryUpdateNotification DeserializeLengthDelimited(Stream stream)
		{
			FactoryUpdateNotification factoryUpdateNotification = new FactoryUpdateNotification();
			FactoryUpdateNotification.DeserializeLengthDelimited(stream, factoryUpdateNotification);
			return factoryUpdateNotification;
		}

		// Token: 0x06003AA0 RID: 15008 RVA: 0x000BDD4C File Offset: 0x000BBF4C
		public static FactoryUpdateNotification DeserializeLengthDelimited(Stream stream, FactoryUpdateNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return FactoryUpdateNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x06003AA1 RID: 15009 RVA: 0x000BDD74 File Offset: 0x000BBF74
		public static FactoryUpdateNotification Deserialize(Stream stream, FactoryUpdateNotification instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			if (instance.Hosts == null)
			{
				instance.Hosts = new List<HostRoute>();
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
				else
				{
					if (num <= 18)
					{
						if (num == 8)
						{
							instance.Op = (FactoryUpdateNotification.Types.Operation)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 18)
						{
							if (instance.Description == null)
							{
								instance.Description = GameFactoryDescription.DeserializeLengthDelimited(stream);
								continue;
							}
							GameFactoryDescription.DeserializeLengthDelimited(stream, instance.Description);
							continue;
						}
					}
					else
					{
						if (num == 29)
						{
							instance.Program = binaryReader.ReadUInt32();
							continue;
						}
						if (num == 34)
						{
							instance.Hosts.Add(HostRoute.DeserializeLengthDelimited(stream));
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

		// Token: 0x06003AA2 RID: 15010 RVA: 0x000BDE7E File Offset: 0x000BC07E
		public void Serialize(Stream stream)
		{
			FactoryUpdateNotification.Serialize(stream, this);
		}

		// Token: 0x06003AA3 RID: 15011 RVA: 0x000BDE88 File Offset: 0x000BC088
		public static void Serialize(Stream stream, FactoryUpdateNotification instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Op));
			if (instance.Description == null)
			{
				throw new ArgumentNullException("Description", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteUInt32(stream, instance.Description.GetSerializedSize());
			GameFactoryDescription.Serialize(stream, instance.Description);
			if (instance.HasProgram)
			{
				stream.WriteByte(29);
				binaryWriter.Write(instance.Program);
			}
			if (instance.Hosts.Count > 0)
			{
				foreach (HostRoute hostRoute in instance.Hosts)
				{
					stream.WriteByte(34);
					ProtocolParser.WriteUInt32(stream, hostRoute.GetSerializedSize());
					HostRoute.Serialize(stream, hostRoute);
				}
			}
		}

		// Token: 0x06003AA4 RID: 15012 RVA: 0x000BDF74 File Offset: 0x000BC174
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Op));
			uint serializedSize = this.Description.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (this.HasProgram)
			{
				num += 1U;
				num += 4U;
			}
			if (this.Hosts.Count > 0)
			{
				foreach (HostRoute hostRoute in this.Hosts)
				{
					num += 1U;
					uint serializedSize2 = hostRoute.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			num += 2U;
			return num;
		}

		// Token: 0x04001541 RID: 5441
		public bool HasProgram;

		// Token: 0x04001542 RID: 5442
		private uint _Program;

		// Token: 0x04001543 RID: 5443
		private List<HostRoute> _Hosts = new List<HostRoute>();

		// Token: 0x020006FD RID: 1789
		public static class Types
		{
			// Token: 0x02000714 RID: 1812
			public enum Operation
			{
				// Token: 0x04002303 RID: 8963
				ADD = 1,
				// Token: 0x04002304 RID: 8964
				REMOVE,
				// Token: 0x04002305 RID: 8965
				CHANGE
			}
		}
	}
}
