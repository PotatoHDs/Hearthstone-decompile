using System;
using System.IO;

namespace bnet.protocol.account.v1
{
	// Token: 0x02000531 RID: 1329
	public class GameTimeInfo : IProtoBuf
	{
		// Token: 0x1700121B RID: 4635
		// (get) Token: 0x06005F9C RID: 24476 RVA: 0x00121C33 File Offset: 0x0011FE33
		// (set) Token: 0x06005F9D RID: 24477 RVA: 0x00121C3B File Offset: 0x0011FE3B
		public bool IsUnlimitedPlayTime
		{
			get
			{
				return this._IsUnlimitedPlayTime;
			}
			set
			{
				this._IsUnlimitedPlayTime = value;
				this.HasIsUnlimitedPlayTime = true;
			}
		}

		// Token: 0x06005F9E RID: 24478 RVA: 0x00121C4B File Offset: 0x0011FE4B
		public void SetIsUnlimitedPlayTime(bool val)
		{
			this.IsUnlimitedPlayTime = val;
		}

		// Token: 0x1700121C RID: 4636
		// (get) Token: 0x06005F9F RID: 24479 RVA: 0x00121C54 File Offset: 0x0011FE54
		// (set) Token: 0x06005FA0 RID: 24480 RVA: 0x00121C5C File Offset: 0x0011FE5C
		public ulong PlayTimeExpires
		{
			get
			{
				return this._PlayTimeExpires;
			}
			set
			{
				this._PlayTimeExpires = value;
				this.HasPlayTimeExpires = true;
			}
		}

		// Token: 0x06005FA1 RID: 24481 RVA: 0x00121C6C File Offset: 0x0011FE6C
		public void SetPlayTimeExpires(ulong val)
		{
			this.PlayTimeExpires = val;
		}

		// Token: 0x1700121D RID: 4637
		// (get) Token: 0x06005FA2 RID: 24482 RVA: 0x00121C75 File Offset: 0x0011FE75
		// (set) Token: 0x06005FA3 RID: 24483 RVA: 0x00121C7D File Offset: 0x0011FE7D
		public bool IsSubscription
		{
			get
			{
				return this._IsSubscription;
			}
			set
			{
				this._IsSubscription = value;
				this.HasIsSubscription = true;
			}
		}

		// Token: 0x06005FA4 RID: 24484 RVA: 0x00121C8D File Offset: 0x0011FE8D
		public void SetIsSubscription(bool val)
		{
			this.IsSubscription = val;
		}

		// Token: 0x1700121E RID: 4638
		// (get) Token: 0x06005FA5 RID: 24485 RVA: 0x00121C96 File Offset: 0x0011FE96
		// (set) Token: 0x06005FA6 RID: 24486 RVA: 0x00121C9E File Offset: 0x0011FE9E
		public bool IsRecurringSubscription
		{
			get
			{
				return this._IsRecurringSubscription;
			}
			set
			{
				this._IsRecurringSubscription = value;
				this.HasIsRecurringSubscription = true;
			}
		}

		// Token: 0x06005FA7 RID: 24487 RVA: 0x00121CAE File Offset: 0x0011FEAE
		public void SetIsRecurringSubscription(bool val)
		{
			this.IsRecurringSubscription = val;
		}

		// Token: 0x06005FA8 RID: 24488 RVA: 0x00121CB8 File Offset: 0x0011FEB8
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasIsUnlimitedPlayTime)
			{
				num ^= this.IsUnlimitedPlayTime.GetHashCode();
			}
			if (this.HasPlayTimeExpires)
			{
				num ^= this.PlayTimeExpires.GetHashCode();
			}
			if (this.HasIsSubscription)
			{
				num ^= this.IsSubscription.GetHashCode();
			}
			if (this.HasIsRecurringSubscription)
			{
				num ^= this.IsRecurringSubscription.GetHashCode();
			}
			return num;
		}

		// Token: 0x06005FA9 RID: 24489 RVA: 0x00121D38 File Offset: 0x0011FF38
		public override bool Equals(object obj)
		{
			GameTimeInfo gameTimeInfo = obj as GameTimeInfo;
			return gameTimeInfo != null && this.HasIsUnlimitedPlayTime == gameTimeInfo.HasIsUnlimitedPlayTime && (!this.HasIsUnlimitedPlayTime || this.IsUnlimitedPlayTime.Equals(gameTimeInfo.IsUnlimitedPlayTime)) && this.HasPlayTimeExpires == gameTimeInfo.HasPlayTimeExpires && (!this.HasPlayTimeExpires || this.PlayTimeExpires.Equals(gameTimeInfo.PlayTimeExpires)) && this.HasIsSubscription == gameTimeInfo.HasIsSubscription && (!this.HasIsSubscription || this.IsSubscription.Equals(gameTimeInfo.IsSubscription)) && this.HasIsRecurringSubscription == gameTimeInfo.HasIsRecurringSubscription && (!this.HasIsRecurringSubscription || this.IsRecurringSubscription.Equals(gameTimeInfo.IsRecurringSubscription));
		}

		// Token: 0x1700121F RID: 4639
		// (get) Token: 0x06005FAA RID: 24490 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005FAB RID: 24491 RVA: 0x00121E0A File Offset: 0x0012000A
		public static GameTimeInfo ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameTimeInfo>(bs, 0, -1);
		}

		// Token: 0x06005FAC RID: 24492 RVA: 0x00121E14 File Offset: 0x00120014
		public void Deserialize(Stream stream)
		{
			GameTimeInfo.Deserialize(stream, this);
		}

		// Token: 0x06005FAD RID: 24493 RVA: 0x00121E1E File Offset: 0x0012001E
		public static GameTimeInfo Deserialize(Stream stream, GameTimeInfo instance)
		{
			return GameTimeInfo.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005FAE RID: 24494 RVA: 0x00121E2C File Offset: 0x0012002C
		public static GameTimeInfo DeserializeLengthDelimited(Stream stream)
		{
			GameTimeInfo gameTimeInfo = new GameTimeInfo();
			GameTimeInfo.DeserializeLengthDelimited(stream, gameTimeInfo);
			return gameTimeInfo;
		}

		// Token: 0x06005FAF RID: 24495 RVA: 0x00121E48 File Offset: 0x00120048
		public static GameTimeInfo DeserializeLengthDelimited(Stream stream, GameTimeInfo instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GameTimeInfo.Deserialize(stream, instance, num);
		}

		// Token: 0x06005FB0 RID: 24496 RVA: 0x00121E70 File Offset: 0x00120070
		public static GameTimeInfo Deserialize(Stream stream, GameTimeInfo instance, long limit)
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
				else
				{
					if (num <= 40)
					{
						if (num == 24)
						{
							instance.IsUnlimitedPlayTime = ProtocolParser.ReadBool(stream);
							continue;
						}
						if (num == 40)
						{
							instance.PlayTimeExpires = ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					else
					{
						if (num == 48)
						{
							instance.IsSubscription = ProtocolParser.ReadBool(stream);
							continue;
						}
						if (num == 56)
						{
							instance.IsRecurringSubscription = ProtocolParser.ReadBool(stream);
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

		// Token: 0x06005FB1 RID: 24497 RVA: 0x00121F41 File Offset: 0x00120141
		public void Serialize(Stream stream)
		{
			GameTimeInfo.Serialize(stream, this);
		}

		// Token: 0x06005FB2 RID: 24498 RVA: 0x00121F4C File Offset: 0x0012014C
		public static void Serialize(Stream stream, GameTimeInfo instance)
		{
			if (instance.HasIsUnlimitedPlayTime)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteBool(stream, instance.IsUnlimitedPlayTime);
			}
			if (instance.HasPlayTimeExpires)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, instance.PlayTimeExpires);
			}
			if (instance.HasIsSubscription)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteBool(stream, instance.IsSubscription);
			}
			if (instance.HasIsRecurringSubscription)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteBool(stream, instance.IsRecurringSubscription);
			}
		}

		// Token: 0x06005FB3 RID: 24499 RVA: 0x00121FCC File Offset: 0x001201CC
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasIsUnlimitedPlayTime)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasPlayTimeExpires)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.PlayTimeExpires);
			}
			if (this.HasIsSubscription)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasIsRecurringSubscription)
			{
				num += 1U;
				num += 1U;
			}
			return num;
		}

		// Token: 0x04001D79 RID: 7545
		public bool HasIsUnlimitedPlayTime;

		// Token: 0x04001D7A RID: 7546
		private bool _IsUnlimitedPlayTime;

		// Token: 0x04001D7B RID: 7547
		public bool HasPlayTimeExpires;

		// Token: 0x04001D7C RID: 7548
		private ulong _PlayTimeExpires;

		// Token: 0x04001D7D RID: 7549
		public bool HasIsSubscription;

		// Token: 0x04001D7E RID: 7550
		private bool _IsSubscription;

		// Token: 0x04001D7F RID: 7551
		public bool HasIsRecurringSubscription;

		// Token: 0x04001D80 RID: 7552
		private bool _IsRecurringSubscription;
	}
}
