using System;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.matchmaking.v1
{
	// Token: 0x020003E1 RID: 993
	public class RemovePlayerOptions : IProtoBuf
	{
		// Token: 0x17000C17 RID: 3095
		// (get) Token: 0x0600417A RID: 16762 RVA: 0x000D0547 File Offset: 0x000CE747
		// (set) Token: 0x0600417B RID: 16763 RVA: 0x000D054F File Offset: 0x000CE74F
		public GameHandle GameHandle
		{
			get
			{
				return this._GameHandle;
			}
			set
			{
				this._GameHandle = value;
				this.HasGameHandle = (value != null);
			}
		}

		// Token: 0x0600417C RID: 16764 RVA: 0x000D0562 File Offset: 0x000CE762
		public void SetGameHandle(GameHandle val)
		{
			this.GameHandle = val;
		}

		// Token: 0x17000C18 RID: 3096
		// (get) Token: 0x0600417D RID: 16765 RVA: 0x000D056B File Offset: 0x000CE76B
		// (set) Token: 0x0600417E RID: 16766 RVA: 0x000D0573 File Offset: 0x000CE773
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

		// Token: 0x0600417F RID: 16767 RVA: 0x000D0586 File Offset: 0x000CE786
		public void SetGameAccount(GameAccountHandle val)
		{
			this.GameAccount = val;
		}

		// Token: 0x06004180 RID: 16768 RVA: 0x000D0590 File Offset: 0x000CE790
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasGameHandle)
			{
				num ^= this.GameHandle.GetHashCode();
			}
			if (this.HasGameAccount)
			{
				num ^= this.GameAccount.GetHashCode();
			}
			return num;
		}

		// Token: 0x06004181 RID: 16769 RVA: 0x000D05D8 File Offset: 0x000CE7D8
		public override bool Equals(object obj)
		{
			RemovePlayerOptions removePlayerOptions = obj as RemovePlayerOptions;
			return removePlayerOptions != null && this.HasGameHandle == removePlayerOptions.HasGameHandle && (!this.HasGameHandle || this.GameHandle.Equals(removePlayerOptions.GameHandle)) && this.HasGameAccount == removePlayerOptions.HasGameAccount && (!this.HasGameAccount || this.GameAccount.Equals(removePlayerOptions.GameAccount));
		}

		// Token: 0x17000C19 RID: 3097
		// (get) Token: 0x06004182 RID: 16770 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004183 RID: 16771 RVA: 0x000D0648 File Offset: 0x000CE848
		public static RemovePlayerOptions ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<RemovePlayerOptions>(bs, 0, -1);
		}

		// Token: 0x06004184 RID: 16772 RVA: 0x000D0652 File Offset: 0x000CE852
		public void Deserialize(Stream stream)
		{
			RemovePlayerOptions.Deserialize(stream, this);
		}

		// Token: 0x06004185 RID: 16773 RVA: 0x000D065C File Offset: 0x000CE85C
		public static RemovePlayerOptions Deserialize(Stream stream, RemovePlayerOptions instance)
		{
			return RemovePlayerOptions.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004186 RID: 16774 RVA: 0x000D0668 File Offset: 0x000CE868
		public static RemovePlayerOptions DeserializeLengthDelimited(Stream stream)
		{
			RemovePlayerOptions removePlayerOptions = new RemovePlayerOptions();
			RemovePlayerOptions.DeserializeLengthDelimited(stream, removePlayerOptions);
			return removePlayerOptions;
		}

		// Token: 0x06004187 RID: 16775 RVA: 0x000D0684 File Offset: 0x000CE884
		public static RemovePlayerOptions DeserializeLengthDelimited(Stream stream, RemovePlayerOptions instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return RemovePlayerOptions.Deserialize(stream, instance, num);
		}

		// Token: 0x06004188 RID: 16776 RVA: 0x000D06AC File Offset: 0x000CE8AC
		public static RemovePlayerOptions Deserialize(Stream stream, RemovePlayerOptions instance, long limit)
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
					else if (instance.GameAccount == null)
					{
						instance.GameAccount = GameAccountHandle.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameAccountHandle.DeserializeLengthDelimited(stream, instance.GameAccount);
					}
				}
				else if (instance.GameHandle == null)
				{
					instance.GameHandle = GameHandle.DeserializeLengthDelimited(stream);
				}
				else
				{
					GameHandle.DeserializeLengthDelimited(stream, instance.GameHandle);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06004189 RID: 16777 RVA: 0x000D077E File Offset: 0x000CE97E
		public void Serialize(Stream stream)
		{
			RemovePlayerOptions.Serialize(stream, this);
		}

		// Token: 0x0600418A RID: 16778 RVA: 0x000D0788 File Offset: 0x000CE988
		public static void Serialize(Stream stream, RemovePlayerOptions instance)
		{
			if (instance.HasGameHandle)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.GameHandle.GetSerializedSize());
				GameHandle.Serialize(stream, instance.GameHandle);
			}
			if (instance.HasGameAccount)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.GameAccount.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.GameAccount);
			}
		}

		// Token: 0x0600418B RID: 16779 RVA: 0x000D07F0 File Offset: 0x000CE9F0
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasGameHandle)
			{
				num += 1U;
				uint serializedSize = this.GameHandle.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasGameAccount)
			{
				num += 1U;
				uint serializedSize2 = this.GameAccount.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num;
		}

		// Token: 0x040016A4 RID: 5796
		public bool HasGameHandle;

		// Token: 0x040016A5 RID: 5797
		private GameHandle _GameHandle;

		// Token: 0x040016A6 RID: 5798
		public bool HasGameAccount;

		// Token: 0x040016A7 RID: 5799
		private GameAccountHandle _GameAccount;
	}
}
