using System;
using System.IO;

namespace bnet.protocol.account.v1
{
	// Token: 0x02000536 RID: 1334
	public class GameSessionUpdateInfo : IProtoBuf
	{
		// Token: 0x17001236 RID: 4662
		// (get) Token: 0x0600601F RID: 24607 RVA: 0x001231EA File Offset: 0x001213EA
		// (set) Token: 0x06006020 RID: 24608 RVA: 0x001231F2 File Offset: 0x001213F2
		public CAIS Cais
		{
			get
			{
				return this._Cais;
			}
			set
			{
				this._Cais = value;
				this.HasCais = (value != null);
			}
		}

		// Token: 0x06006021 RID: 24609 RVA: 0x00123205 File Offset: 0x00121405
		public void SetCais(CAIS val)
		{
			this.Cais = val;
		}

		// Token: 0x06006022 RID: 24610 RVA: 0x00123210 File Offset: 0x00121410
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasCais)
			{
				num ^= this.Cais.GetHashCode();
			}
			return num;
		}

		// Token: 0x06006023 RID: 24611 RVA: 0x00123240 File Offset: 0x00121440
		public override bool Equals(object obj)
		{
			GameSessionUpdateInfo gameSessionUpdateInfo = obj as GameSessionUpdateInfo;
			return gameSessionUpdateInfo != null && this.HasCais == gameSessionUpdateInfo.HasCais && (!this.HasCais || this.Cais.Equals(gameSessionUpdateInfo.Cais));
		}

		// Token: 0x17001237 RID: 4663
		// (get) Token: 0x06006024 RID: 24612 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06006025 RID: 24613 RVA: 0x00123285 File Offset: 0x00121485
		public static GameSessionUpdateInfo ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameSessionUpdateInfo>(bs, 0, -1);
		}

		// Token: 0x06006026 RID: 24614 RVA: 0x0012328F File Offset: 0x0012148F
		public void Deserialize(Stream stream)
		{
			GameSessionUpdateInfo.Deserialize(stream, this);
		}

		// Token: 0x06006027 RID: 24615 RVA: 0x00123299 File Offset: 0x00121499
		public static GameSessionUpdateInfo Deserialize(Stream stream, GameSessionUpdateInfo instance)
		{
			return GameSessionUpdateInfo.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06006028 RID: 24616 RVA: 0x001232A4 File Offset: 0x001214A4
		public static GameSessionUpdateInfo DeserializeLengthDelimited(Stream stream)
		{
			GameSessionUpdateInfo gameSessionUpdateInfo = new GameSessionUpdateInfo();
			GameSessionUpdateInfo.DeserializeLengthDelimited(stream, gameSessionUpdateInfo);
			return gameSessionUpdateInfo;
		}

		// Token: 0x06006029 RID: 24617 RVA: 0x001232C0 File Offset: 0x001214C0
		public static GameSessionUpdateInfo DeserializeLengthDelimited(Stream stream, GameSessionUpdateInfo instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GameSessionUpdateInfo.Deserialize(stream, instance, num);
		}

		// Token: 0x0600602A RID: 24618 RVA: 0x001232E8 File Offset: 0x001214E8
		public static GameSessionUpdateInfo Deserialize(Stream stream, GameSessionUpdateInfo instance, long limit)
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
				else if (num == 66)
				{
					if (instance.Cais == null)
					{
						instance.Cais = CAIS.DeserializeLengthDelimited(stream);
					}
					else
					{
						CAIS.DeserializeLengthDelimited(stream, instance.Cais);
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

		// Token: 0x0600602B RID: 24619 RVA: 0x00123382 File Offset: 0x00121582
		public void Serialize(Stream stream)
		{
			GameSessionUpdateInfo.Serialize(stream, this);
		}

		// Token: 0x0600602C RID: 24620 RVA: 0x0012338B File Offset: 0x0012158B
		public static void Serialize(Stream stream, GameSessionUpdateInfo instance)
		{
			if (instance.HasCais)
			{
				stream.WriteByte(66);
				ProtocolParser.WriteUInt32(stream, instance.Cais.GetSerializedSize());
				CAIS.Serialize(stream, instance.Cais);
			}
		}

		// Token: 0x0600602D RID: 24621 RVA: 0x001233BC File Offset: 0x001215BC
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasCais)
			{
				num += 1U;
				uint serializedSize = this.Cais.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}

		// Token: 0x04001DA5 RID: 7589
		public bool HasCais;

		// Token: 0x04001DA6 RID: 7590
		private CAIS _Cais;
	}
}
