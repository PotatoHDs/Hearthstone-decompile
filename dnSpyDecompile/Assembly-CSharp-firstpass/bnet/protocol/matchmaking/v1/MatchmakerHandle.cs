using System;
using System.IO;

namespace bnet.protocol.matchmaking.v1
{
	// Token: 0x020003DC RID: 988
	public class MatchmakerHandle : IProtoBuf
	{
		// Token: 0x17000BF6 RID: 3062
		// (get) Token: 0x060040EF RID: 16623 RVA: 0x000CEB73 File Offset: 0x000CCD73
		// (set) Token: 0x060040F0 RID: 16624 RVA: 0x000CEB7B File Offset: 0x000CCD7B
		public HostProxyPair Addr
		{
			get
			{
				return this._Addr;
			}
			set
			{
				this._Addr = value;
				this.HasAddr = (value != null);
			}
		}

		// Token: 0x060040F1 RID: 16625 RVA: 0x000CEB8E File Offset: 0x000CCD8E
		public void SetAddr(HostProxyPair val)
		{
			this.Addr = val;
		}

		// Token: 0x17000BF7 RID: 3063
		// (get) Token: 0x060040F2 RID: 16626 RVA: 0x000CEB97 File Offset: 0x000CCD97
		// (set) Token: 0x060040F3 RID: 16627 RVA: 0x000CEB9F File Offset: 0x000CCD9F
		public uint Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				this._Id = value;
				this.HasId = true;
			}
		}

		// Token: 0x060040F4 RID: 16628 RVA: 0x000CEBAF File Offset: 0x000CCDAF
		public void SetId(uint val)
		{
			this.Id = val;
		}

		// Token: 0x060040F5 RID: 16629 RVA: 0x000CEBB8 File Offset: 0x000CCDB8
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAddr)
			{
				num ^= this.Addr.GetHashCode();
			}
			if (this.HasId)
			{
				num ^= this.Id.GetHashCode();
			}
			return num;
		}

		// Token: 0x060040F6 RID: 16630 RVA: 0x000CEC04 File Offset: 0x000CCE04
		public override bool Equals(object obj)
		{
			MatchmakerHandle matchmakerHandle = obj as MatchmakerHandle;
			return matchmakerHandle != null && this.HasAddr == matchmakerHandle.HasAddr && (!this.HasAddr || this.Addr.Equals(matchmakerHandle.Addr)) && this.HasId == matchmakerHandle.HasId && (!this.HasId || this.Id.Equals(matchmakerHandle.Id));
		}

		// Token: 0x17000BF8 RID: 3064
		// (get) Token: 0x060040F7 RID: 16631 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060040F8 RID: 16632 RVA: 0x000CEC77 File Offset: 0x000CCE77
		public static MatchmakerHandle ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<MatchmakerHandle>(bs, 0, -1);
		}

		// Token: 0x060040F9 RID: 16633 RVA: 0x000CEC81 File Offset: 0x000CCE81
		public void Deserialize(Stream stream)
		{
			MatchmakerHandle.Deserialize(stream, this);
		}

		// Token: 0x060040FA RID: 16634 RVA: 0x000CEC8B File Offset: 0x000CCE8B
		public static MatchmakerHandle Deserialize(Stream stream, MatchmakerHandle instance)
		{
			return MatchmakerHandle.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060040FB RID: 16635 RVA: 0x000CEC98 File Offset: 0x000CCE98
		public static MatchmakerHandle DeserializeLengthDelimited(Stream stream)
		{
			MatchmakerHandle matchmakerHandle = new MatchmakerHandle();
			MatchmakerHandle.DeserializeLengthDelimited(stream, matchmakerHandle);
			return matchmakerHandle;
		}

		// Token: 0x060040FC RID: 16636 RVA: 0x000CECB4 File Offset: 0x000CCEB4
		public static MatchmakerHandle DeserializeLengthDelimited(Stream stream, MatchmakerHandle instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return MatchmakerHandle.Deserialize(stream, instance, num);
		}

		// Token: 0x060040FD RID: 16637 RVA: 0x000CECDC File Offset: 0x000CCEDC
		public static MatchmakerHandle Deserialize(Stream stream, MatchmakerHandle instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
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
					if (num != 21)
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
						instance.Id = binaryReader.ReadUInt32();
					}
				}
				else if (instance.Addr == null)
				{
					instance.Addr = HostProxyPair.DeserializeLengthDelimited(stream);
				}
				else
				{
					HostProxyPair.DeserializeLengthDelimited(stream, instance.Addr);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060040FE RID: 16638 RVA: 0x000CED95 File Offset: 0x000CCF95
		public void Serialize(Stream stream)
		{
			MatchmakerHandle.Serialize(stream, this);
		}

		// Token: 0x060040FF RID: 16639 RVA: 0x000CEDA0 File Offset: 0x000CCFA0
		public static void Serialize(Stream stream, MatchmakerHandle instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasAddr)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Addr.GetSerializedSize());
				HostProxyPair.Serialize(stream, instance.Addr);
			}
			if (instance.HasId)
			{
				stream.WriteByte(21);
				binaryWriter.Write(instance.Id);
			}
		}

		// Token: 0x06004100 RID: 16640 RVA: 0x000CEE00 File Offset: 0x000CD000
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAddr)
			{
				num += 1U;
				uint serializedSize = this.Addr.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasId)
			{
				num += 1U;
				num += 4U;
			}
			return num;
		}

		// Token: 0x04001685 RID: 5765
		public bool HasAddr;

		// Token: 0x04001686 RID: 5766
		private HostProxyPair _Addr;

		// Token: 0x04001687 RID: 5767
		public bool HasId;

		// Token: 0x04001688 RID: 5768
		private uint _Id;
	}
}
