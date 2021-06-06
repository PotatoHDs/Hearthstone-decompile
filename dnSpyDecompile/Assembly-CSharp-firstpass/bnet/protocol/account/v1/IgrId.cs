using System;
using System.IO;

namespace bnet.protocol.account.v1
{
	// Token: 0x0200053F RID: 1343
	public class IgrId : IProtoBuf
	{
		// Token: 0x17001262 RID: 4706
		// (get) Token: 0x060060F3 RID: 24819 RVA: 0x001256D7 File Offset: 0x001238D7
		// (set) Token: 0x060060F4 RID: 24820 RVA: 0x001256DF File Offset: 0x001238DF
		public GameAccountHandle GameAccount
		{
			get
			{
				return this._GameAccount;
			}
			set
			{
				this._GameAccount = value;
				this.HasGameAccount = (value != null);
			}
		}

		// Token: 0x060060F5 RID: 24821 RVA: 0x001256F2 File Offset: 0x001238F2
		public void SetGameAccount(GameAccountHandle val)
		{
			this.GameAccount = val;
		}

		// Token: 0x17001263 RID: 4707
		// (get) Token: 0x060060F6 RID: 24822 RVA: 0x001256FB File Offset: 0x001238FB
		// (set) Token: 0x060060F7 RID: 24823 RVA: 0x00125703 File Offset: 0x00123903
		public uint ExternalId
		{
			get
			{
				return this._ExternalId;
			}
			set
			{
				this._ExternalId = value;
				this.HasExternalId = true;
			}
		}

		// Token: 0x060060F8 RID: 24824 RVA: 0x00125713 File Offset: 0x00123913
		public void SetExternalId(uint val)
		{
			this.ExternalId = val;
		}

		// Token: 0x060060F9 RID: 24825 RVA: 0x0012571C File Offset: 0x0012391C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasGameAccount)
			{
				num ^= this.GameAccount.GetHashCode();
			}
			if (this.HasExternalId)
			{
				num ^= this.ExternalId.GetHashCode();
			}
			return num;
		}

		// Token: 0x060060FA RID: 24826 RVA: 0x00125768 File Offset: 0x00123968
		public override bool Equals(object obj)
		{
			IgrId igrId = obj as IgrId;
			return igrId != null && this.HasGameAccount == igrId.HasGameAccount && (!this.HasGameAccount || this.GameAccount.Equals(igrId.GameAccount)) && this.HasExternalId == igrId.HasExternalId && (!this.HasExternalId || this.ExternalId.Equals(igrId.ExternalId));
		}

		// Token: 0x17001264 RID: 4708
		// (get) Token: 0x060060FB RID: 24827 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060060FC RID: 24828 RVA: 0x001257DB File Offset: 0x001239DB
		public static IgrId ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<IgrId>(bs, 0, -1);
		}

		// Token: 0x060060FD RID: 24829 RVA: 0x001257E5 File Offset: 0x001239E5
		public void Deserialize(Stream stream)
		{
			IgrId.Deserialize(stream, this);
		}

		// Token: 0x060060FE RID: 24830 RVA: 0x001257EF File Offset: 0x001239EF
		public static IgrId Deserialize(Stream stream, IgrId instance)
		{
			return IgrId.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060060FF RID: 24831 RVA: 0x001257FC File Offset: 0x001239FC
		public static IgrId DeserializeLengthDelimited(Stream stream)
		{
			IgrId igrId = new IgrId();
			IgrId.DeserializeLengthDelimited(stream, igrId);
			return igrId;
		}

		// Token: 0x06006100 RID: 24832 RVA: 0x00125818 File Offset: 0x00123A18
		public static IgrId DeserializeLengthDelimited(Stream stream, IgrId instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return IgrId.Deserialize(stream, instance, num);
		}

		// Token: 0x06006101 RID: 24833 RVA: 0x00125840 File Offset: 0x00123A40
		public static IgrId Deserialize(Stream stream, IgrId instance, long limit)
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
						instance.ExternalId = binaryReader.ReadUInt32();
					}
				}
				else if (instance.GameAccount == null)
				{
					instance.GameAccount = GameAccountHandle.DeserializeLengthDelimited(stream);
				}
				else
				{
					GameAccountHandle.DeserializeLengthDelimited(stream, instance.GameAccount);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06006102 RID: 24834 RVA: 0x001258F9 File Offset: 0x00123AF9
		public void Serialize(Stream stream)
		{
			IgrId.Serialize(stream, this);
		}

		// Token: 0x06006103 RID: 24835 RVA: 0x00125904 File Offset: 0x00123B04
		public static void Serialize(Stream stream, IgrId instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasGameAccount)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.GameAccount.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.GameAccount);
			}
			if (instance.HasExternalId)
			{
				stream.WriteByte(21);
				binaryWriter.Write(instance.ExternalId);
			}
		}

		// Token: 0x06006104 RID: 24836 RVA: 0x00125964 File Offset: 0x00123B64
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasGameAccount)
			{
				num += 1U;
				uint serializedSize = this.GameAccount.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasExternalId)
			{
				num += 1U;
				num += 4U;
			}
			return num;
		}

		// Token: 0x04001DD2 RID: 7634
		public bool HasGameAccount;

		// Token: 0x04001DD3 RID: 7635
		private GameAccountHandle _GameAccount;

		// Token: 0x04001DD4 RID: 7636
		public bool HasExternalId;

		// Token: 0x04001DD5 RID: 7637
		private uint _ExternalId;
	}
}
