using System;
using System.IO;

namespace bnet.protocol.matchmaking.v1
{
	// Token: 0x020003E2 RID: 994
	public class RemoveGameOptions : IProtoBuf
	{
		// Token: 0x17000C1A RID: 3098
		// (get) Token: 0x0600418D RID: 16781 RVA: 0x000D0846 File Offset: 0x000CEA46
		// (set) Token: 0x0600418E RID: 16782 RVA: 0x000D084E File Offset: 0x000CEA4E
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

		// Token: 0x0600418F RID: 16783 RVA: 0x000D0861 File Offset: 0x000CEA61
		public void SetGameHandle(GameHandle val)
		{
			this.GameHandle = val;
		}

		// Token: 0x06004190 RID: 16784 RVA: 0x000D086C File Offset: 0x000CEA6C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasGameHandle)
			{
				num ^= this.GameHandle.GetHashCode();
			}
			return num;
		}

		// Token: 0x06004191 RID: 16785 RVA: 0x000D089C File Offset: 0x000CEA9C
		public override bool Equals(object obj)
		{
			RemoveGameOptions removeGameOptions = obj as RemoveGameOptions;
			return removeGameOptions != null && this.HasGameHandle == removeGameOptions.HasGameHandle && (!this.HasGameHandle || this.GameHandle.Equals(removeGameOptions.GameHandle));
		}

		// Token: 0x17000C1B RID: 3099
		// (get) Token: 0x06004192 RID: 16786 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004193 RID: 16787 RVA: 0x000D08E1 File Offset: 0x000CEAE1
		public static RemoveGameOptions ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<RemoveGameOptions>(bs, 0, -1);
		}

		// Token: 0x06004194 RID: 16788 RVA: 0x000D08EB File Offset: 0x000CEAEB
		public void Deserialize(Stream stream)
		{
			RemoveGameOptions.Deserialize(stream, this);
		}

		// Token: 0x06004195 RID: 16789 RVA: 0x000D08F5 File Offset: 0x000CEAF5
		public static RemoveGameOptions Deserialize(Stream stream, RemoveGameOptions instance)
		{
			return RemoveGameOptions.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004196 RID: 16790 RVA: 0x000D0900 File Offset: 0x000CEB00
		public static RemoveGameOptions DeserializeLengthDelimited(Stream stream)
		{
			RemoveGameOptions removeGameOptions = new RemoveGameOptions();
			RemoveGameOptions.DeserializeLengthDelimited(stream, removeGameOptions);
			return removeGameOptions;
		}

		// Token: 0x06004197 RID: 16791 RVA: 0x000D091C File Offset: 0x000CEB1C
		public static RemoveGameOptions DeserializeLengthDelimited(Stream stream, RemoveGameOptions instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return RemoveGameOptions.Deserialize(stream, instance, num);
		}

		// Token: 0x06004198 RID: 16792 RVA: 0x000D0944 File Offset: 0x000CEB44
		public static RemoveGameOptions Deserialize(Stream stream, RemoveGameOptions instance, long limit)
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
				else if (num == 10)
				{
					if (instance.GameHandle == null)
					{
						instance.GameHandle = GameHandle.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameHandle.DeserializeLengthDelimited(stream, instance.GameHandle);
					}
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

		// Token: 0x06004199 RID: 16793 RVA: 0x000D09DE File Offset: 0x000CEBDE
		public void Serialize(Stream stream)
		{
			RemoveGameOptions.Serialize(stream, this);
		}

		// Token: 0x0600419A RID: 16794 RVA: 0x000D09E7 File Offset: 0x000CEBE7
		public static void Serialize(Stream stream, RemoveGameOptions instance)
		{
			if (instance.HasGameHandle)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.GameHandle.GetSerializedSize());
				GameHandle.Serialize(stream, instance.GameHandle);
			}
		}

		// Token: 0x0600419B RID: 16795 RVA: 0x000D0A18 File Offset: 0x000CEC18
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasGameHandle)
			{
				num += 1U;
				uint serializedSize = this.GameHandle.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}

		// Token: 0x040016A8 RID: 5800
		public bool HasGameHandle;

		// Token: 0x040016A9 RID: 5801
		private GameHandle _GameHandle;
	}
}
