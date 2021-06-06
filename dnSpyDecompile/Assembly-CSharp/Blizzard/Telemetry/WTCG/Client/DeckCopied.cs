using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011B4 RID: 4532
	public class DeckCopied : IProtoBuf
	{
		// Token: 0x17000EE0 RID: 3808
		// (get) Token: 0x0600C8CE RID: 51406 RVA: 0x003C3D57 File Offset: 0x003C1F57
		// (set) Token: 0x0600C8CF RID: 51407 RVA: 0x003C3D5F File Offset: 0x003C1F5F
		public Player Player
		{
			get
			{
				return this._Player;
			}
			set
			{
				this._Player = value;
				this.HasPlayer = (value != null);
			}
		}

		// Token: 0x17000EE1 RID: 3809
		// (get) Token: 0x0600C8D0 RID: 51408 RVA: 0x003C3D72 File Offset: 0x003C1F72
		// (set) Token: 0x0600C8D1 RID: 51409 RVA: 0x003C3D7A File Offset: 0x003C1F7A
		public DeviceInfo DeviceInfo
		{
			get
			{
				return this._DeviceInfo;
			}
			set
			{
				this._DeviceInfo = value;
				this.HasDeviceInfo = (value != null);
			}
		}

		// Token: 0x17000EE2 RID: 3810
		// (get) Token: 0x0600C8D2 RID: 51410 RVA: 0x003C3D8D File Offset: 0x003C1F8D
		// (set) Token: 0x0600C8D3 RID: 51411 RVA: 0x003C3D95 File Offset: 0x003C1F95
		public long DeckId
		{
			get
			{
				return this._DeckId;
			}
			set
			{
				this._DeckId = value;
				this.HasDeckId = true;
			}
		}

		// Token: 0x17000EE3 RID: 3811
		// (get) Token: 0x0600C8D4 RID: 51412 RVA: 0x003C3DA5 File Offset: 0x003C1FA5
		// (set) Token: 0x0600C8D5 RID: 51413 RVA: 0x003C3DAD File Offset: 0x003C1FAD
		public string DeckHash
		{
			get
			{
				return this._DeckHash;
			}
			set
			{
				this._DeckHash = value;
				this.HasDeckHash = (value != null);
			}
		}

		// Token: 0x0600C8D6 RID: 51414 RVA: 0x003C3DC0 File Offset: 0x003C1FC0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasPlayer)
			{
				num ^= this.Player.GetHashCode();
			}
			if (this.HasDeviceInfo)
			{
				num ^= this.DeviceInfo.GetHashCode();
			}
			if (this.HasDeckId)
			{
				num ^= this.DeckId.GetHashCode();
			}
			if (this.HasDeckHash)
			{
				num ^= this.DeckHash.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600C8D7 RID: 51415 RVA: 0x003C3E38 File Offset: 0x003C2038
		public override bool Equals(object obj)
		{
			DeckCopied deckCopied = obj as DeckCopied;
			return deckCopied != null && this.HasPlayer == deckCopied.HasPlayer && (!this.HasPlayer || this.Player.Equals(deckCopied.Player)) && this.HasDeviceInfo == deckCopied.HasDeviceInfo && (!this.HasDeviceInfo || this.DeviceInfo.Equals(deckCopied.DeviceInfo)) && this.HasDeckId == deckCopied.HasDeckId && (!this.HasDeckId || this.DeckId.Equals(deckCopied.DeckId)) && this.HasDeckHash == deckCopied.HasDeckHash && (!this.HasDeckHash || this.DeckHash.Equals(deckCopied.DeckHash));
		}

		// Token: 0x0600C8D8 RID: 51416 RVA: 0x003C3F01 File Offset: 0x003C2101
		public void Deserialize(Stream stream)
		{
			DeckCopied.Deserialize(stream, this);
		}

		// Token: 0x0600C8D9 RID: 51417 RVA: 0x003C3F0B File Offset: 0x003C210B
		public static DeckCopied Deserialize(Stream stream, DeckCopied instance)
		{
			return DeckCopied.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600C8DA RID: 51418 RVA: 0x003C3F18 File Offset: 0x003C2118
		public static DeckCopied DeserializeLengthDelimited(Stream stream)
		{
			DeckCopied deckCopied = new DeckCopied();
			DeckCopied.DeserializeLengthDelimited(stream, deckCopied);
			return deckCopied;
		}

		// Token: 0x0600C8DB RID: 51419 RVA: 0x003C3F34 File Offset: 0x003C2134
		public static DeckCopied DeserializeLengthDelimited(Stream stream, DeckCopied instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return DeckCopied.Deserialize(stream, instance, num);
		}

		// Token: 0x0600C8DC RID: 51420 RVA: 0x003C3F5C File Offset: 0x003C215C
		public static DeckCopied Deserialize(Stream stream, DeckCopied instance, long limit)
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
					if (num <= 18)
					{
						if (num != 10)
						{
							if (num == 18)
							{
								if (instance.DeviceInfo == null)
								{
									instance.DeviceInfo = DeviceInfo.DeserializeLengthDelimited(stream);
									continue;
								}
								DeviceInfo.DeserializeLengthDelimited(stream, instance.DeviceInfo);
								continue;
							}
						}
						else
						{
							if (instance.Player == null)
							{
								instance.Player = Player.DeserializeLengthDelimited(stream);
								continue;
							}
							Player.DeserializeLengthDelimited(stream, instance.Player);
							continue;
						}
					}
					else
					{
						if (num == 24)
						{
							instance.DeckId = (long)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 34)
						{
							instance.DeckHash = ProtocolParser.ReadString(stream);
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

		// Token: 0x0600C8DD RID: 51421 RVA: 0x003C4067 File Offset: 0x003C2267
		public void Serialize(Stream stream)
		{
			DeckCopied.Serialize(stream, this);
		}

		// Token: 0x0600C8DE RID: 51422 RVA: 0x003C4070 File Offset: 0x003C2270
		public static void Serialize(Stream stream, DeckCopied instance)
		{
			if (instance.HasPlayer)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Player.GetSerializedSize());
				Player.Serialize(stream, instance.Player);
			}
			if (instance.HasDeviceInfo)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.DeviceInfo.GetSerializedSize());
				DeviceInfo.Serialize(stream, instance.DeviceInfo);
			}
			if (instance.HasDeckId)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.DeckId);
			}
			if (instance.HasDeckHash)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.DeckHash));
			}
		}

		// Token: 0x0600C8DF RID: 51423 RVA: 0x003C411C File Offset: 0x003C231C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasPlayer)
			{
				num += 1U;
				uint serializedSize = this.Player.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasDeviceInfo)
			{
				num += 1U;
				uint serializedSize2 = this.DeviceInfo.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasDeckId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.DeckId);
			}
			if (this.HasDeckHash)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.DeckHash);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num;
		}

		// Token: 0x04009F0B RID: 40715
		public bool HasPlayer;

		// Token: 0x04009F0C RID: 40716
		private Player _Player;

		// Token: 0x04009F0D RID: 40717
		public bool HasDeviceInfo;

		// Token: 0x04009F0E RID: 40718
		private DeviceInfo _DeviceInfo;

		// Token: 0x04009F0F RID: 40719
		public bool HasDeckId;

		// Token: 0x04009F10 RID: 40720
		private long _DeckId;

		// Token: 0x04009F11 RID: 40721
		public bool HasDeckHash;

		// Token: 0x04009F12 RID: 40722
		private string _DeckHash;
	}
}
