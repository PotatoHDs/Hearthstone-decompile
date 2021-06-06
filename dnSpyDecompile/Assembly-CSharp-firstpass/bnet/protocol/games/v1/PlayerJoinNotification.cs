using System;
using System.IO;
using bnet.protocol.games.v2;

namespace bnet.protocol.games.v1
{
	// Token: 0x02000393 RID: 915
	public class PlayerJoinNotification : IProtoBuf
	{
		// Token: 0x17000AB3 RID: 2739
		// (get) Token: 0x06003AA6 RID: 15014 RVA: 0x000BE033 File Offset: 0x000BC233
		// (set) Token: 0x06003AA7 RID: 15015 RVA: 0x000BE03B File Offset: 0x000BC23B
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

		// Token: 0x06003AA8 RID: 15016 RVA: 0x000BE04E File Offset: 0x000BC24E
		public void SetGameHandle(GameHandle val)
		{
			this.GameHandle = val;
		}

		// Token: 0x17000AB4 RID: 2740
		// (get) Token: 0x06003AA9 RID: 15017 RVA: 0x000BE057 File Offset: 0x000BC257
		// (set) Token: 0x06003AAA RID: 15018 RVA: 0x000BE05F File Offset: 0x000BC25F
		public Assignment Assignment
		{
			get
			{
				return this._Assignment;
			}
			set
			{
				this._Assignment = value;
				this.HasAssignment = (value != null);
			}
		}

		// Token: 0x06003AAB RID: 15019 RVA: 0x000BE072 File Offset: 0x000BC272
		public void SetAssignment(Assignment val)
		{
			this.Assignment = val;
		}

		// Token: 0x06003AAC RID: 15020 RVA: 0x000BE07C File Offset: 0x000BC27C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasGameHandle)
			{
				num ^= this.GameHandle.GetHashCode();
			}
			if (this.HasAssignment)
			{
				num ^= this.Assignment.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003AAD RID: 15021 RVA: 0x000BE0C4 File Offset: 0x000BC2C4
		public override bool Equals(object obj)
		{
			PlayerJoinNotification playerJoinNotification = obj as PlayerJoinNotification;
			return playerJoinNotification != null && this.HasGameHandle == playerJoinNotification.HasGameHandle && (!this.HasGameHandle || this.GameHandle.Equals(playerJoinNotification.GameHandle)) && this.HasAssignment == playerJoinNotification.HasAssignment && (!this.HasAssignment || this.Assignment.Equals(playerJoinNotification.Assignment));
		}

		// Token: 0x17000AB5 RID: 2741
		// (get) Token: 0x06003AAE RID: 15022 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003AAF RID: 15023 RVA: 0x000BE134 File Offset: 0x000BC334
		public static PlayerJoinNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<PlayerJoinNotification>(bs, 0, -1);
		}

		// Token: 0x06003AB0 RID: 15024 RVA: 0x000BE13E File Offset: 0x000BC33E
		public void Deserialize(Stream stream)
		{
			PlayerJoinNotification.Deserialize(stream, this);
		}

		// Token: 0x06003AB1 RID: 15025 RVA: 0x000BE148 File Offset: 0x000BC348
		public static PlayerJoinNotification Deserialize(Stream stream, PlayerJoinNotification instance)
		{
			return PlayerJoinNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003AB2 RID: 15026 RVA: 0x000BE154 File Offset: 0x000BC354
		public static PlayerJoinNotification DeserializeLengthDelimited(Stream stream)
		{
			PlayerJoinNotification playerJoinNotification = new PlayerJoinNotification();
			PlayerJoinNotification.DeserializeLengthDelimited(stream, playerJoinNotification);
			return playerJoinNotification;
		}

		// Token: 0x06003AB3 RID: 15027 RVA: 0x000BE170 File Offset: 0x000BC370
		public static PlayerJoinNotification DeserializeLengthDelimited(Stream stream, PlayerJoinNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return PlayerJoinNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x06003AB4 RID: 15028 RVA: 0x000BE198 File Offset: 0x000BC398
		public static PlayerJoinNotification Deserialize(Stream stream, PlayerJoinNotification instance, long limit)
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
					else if (instance.Assignment == null)
					{
						instance.Assignment = Assignment.DeserializeLengthDelimited(stream);
					}
					else
					{
						Assignment.DeserializeLengthDelimited(stream, instance.Assignment);
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

		// Token: 0x06003AB5 RID: 15029 RVA: 0x000BE26A File Offset: 0x000BC46A
		public void Serialize(Stream stream)
		{
			PlayerJoinNotification.Serialize(stream, this);
		}

		// Token: 0x06003AB6 RID: 15030 RVA: 0x000BE274 File Offset: 0x000BC474
		public static void Serialize(Stream stream, PlayerJoinNotification instance)
		{
			if (instance.HasGameHandle)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.GameHandle.GetSerializedSize());
				GameHandle.Serialize(stream, instance.GameHandle);
			}
			if (instance.HasAssignment)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.Assignment.GetSerializedSize());
				Assignment.Serialize(stream, instance.Assignment);
			}
		}

		// Token: 0x06003AB7 RID: 15031 RVA: 0x000BE2DC File Offset: 0x000BC4DC
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasGameHandle)
			{
				num += 1U;
				uint serializedSize = this.GameHandle.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasAssignment)
			{
				num += 1U;
				uint serializedSize2 = this.Assignment.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num;
		}

		// Token: 0x04001544 RID: 5444
		public bool HasGameHandle;

		// Token: 0x04001545 RID: 5445
		private GameHandle _GameHandle;

		// Token: 0x04001546 RID: 5446
		public bool HasAssignment;

		// Token: 0x04001547 RID: 5447
		private Assignment _Assignment;
	}
}
