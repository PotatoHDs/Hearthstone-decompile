using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.games.v1
{
	// Token: 0x0200038A RID: 906
	public class GameRequestServerEntry : IProtoBuf
	{
		// Token: 0x17000A8D RID: 2701
		// (get) Token: 0x060039E6 RID: 14822 RVA: 0x000BC1BC File Offset: 0x000BA3BC
		// (set) Token: 0x060039E7 RID: 14823 RVA: 0x000BC1C4 File Offset: 0x000BA3C4
		public HostRoute Host
		{
			get
			{
				return this._Host;
			}
			set
			{
				this._Host = value;
				this.HasHost = (value != null);
			}
		}

		// Token: 0x060039E8 RID: 14824 RVA: 0x000BC1D7 File Offset: 0x000BA3D7
		public void SetHost(HostRoute val)
		{
			this.Host = val;
		}

		// Token: 0x17000A8E RID: 2702
		// (get) Token: 0x060039E9 RID: 14825 RVA: 0x000BC1E0 File Offset: 0x000BA3E0
		// (set) Token: 0x060039EA RID: 14826 RVA: 0x000BC1E8 File Offset: 0x000BA3E8
		public List<GameRequestEntry> GameRequests
		{
			get
			{
				return this._GameRequests;
			}
			set
			{
				this._GameRequests = value;
			}
		}

		// Token: 0x17000A8F RID: 2703
		// (get) Token: 0x060039EB RID: 14827 RVA: 0x000BC1E0 File Offset: 0x000BA3E0
		public List<GameRequestEntry> GameRequestsList
		{
			get
			{
				return this._GameRequests;
			}
		}

		// Token: 0x17000A90 RID: 2704
		// (get) Token: 0x060039EC RID: 14828 RVA: 0x000BC1F1 File Offset: 0x000BA3F1
		public int GameRequestsCount
		{
			get
			{
				return this._GameRequests.Count;
			}
		}

		// Token: 0x060039ED RID: 14829 RVA: 0x000BC1FE File Offset: 0x000BA3FE
		public void AddGameRequests(GameRequestEntry val)
		{
			this._GameRequests.Add(val);
		}

		// Token: 0x060039EE RID: 14830 RVA: 0x000BC20C File Offset: 0x000BA40C
		public void ClearGameRequests()
		{
			this._GameRequests.Clear();
		}

		// Token: 0x060039EF RID: 14831 RVA: 0x000BC219 File Offset: 0x000BA419
		public void SetGameRequests(List<GameRequestEntry> val)
		{
			this.GameRequests = val;
		}

		// Token: 0x060039F0 RID: 14832 RVA: 0x000BC224 File Offset: 0x000BA424
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasHost)
			{
				num ^= this.Host.GetHashCode();
			}
			foreach (GameRequestEntry gameRequestEntry in this.GameRequests)
			{
				num ^= gameRequestEntry.GetHashCode();
			}
			return num;
		}

		// Token: 0x060039F1 RID: 14833 RVA: 0x000BC29C File Offset: 0x000BA49C
		public override bool Equals(object obj)
		{
			GameRequestServerEntry gameRequestServerEntry = obj as GameRequestServerEntry;
			if (gameRequestServerEntry == null)
			{
				return false;
			}
			if (this.HasHost != gameRequestServerEntry.HasHost || (this.HasHost && !this.Host.Equals(gameRequestServerEntry.Host)))
			{
				return false;
			}
			if (this.GameRequests.Count != gameRequestServerEntry.GameRequests.Count)
			{
				return false;
			}
			for (int i = 0; i < this.GameRequests.Count; i++)
			{
				if (!this.GameRequests[i].Equals(gameRequestServerEntry.GameRequests[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000A91 RID: 2705
		// (get) Token: 0x060039F2 RID: 14834 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060039F3 RID: 14835 RVA: 0x000BC332 File Offset: 0x000BA532
		public static GameRequestServerEntry ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameRequestServerEntry>(bs, 0, -1);
		}

		// Token: 0x060039F4 RID: 14836 RVA: 0x000BC33C File Offset: 0x000BA53C
		public void Deserialize(Stream stream)
		{
			GameRequestServerEntry.Deserialize(stream, this);
		}

		// Token: 0x060039F5 RID: 14837 RVA: 0x000BC346 File Offset: 0x000BA546
		public static GameRequestServerEntry Deserialize(Stream stream, GameRequestServerEntry instance)
		{
			return GameRequestServerEntry.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060039F6 RID: 14838 RVA: 0x000BC354 File Offset: 0x000BA554
		public static GameRequestServerEntry DeserializeLengthDelimited(Stream stream)
		{
			GameRequestServerEntry gameRequestServerEntry = new GameRequestServerEntry();
			GameRequestServerEntry.DeserializeLengthDelimited(stream, gameRequestServerEntry);
			return gameRequestServerEntry;
		}

		// Token: 0x060039F7 RID: 14839 RVA: 0x000BC370 File Offset: 0x000BA570
		public static GameRequestServerEntry DeserializeLengthDelimited(Stream stream, GameRequestServerEntry instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GameRequestServerEntry.Deserialize(stream, instance, num);
		}

		// Token: 0x060039F8 RID: 14840 RVA: 0x000BC398 File Offset: 0x000BA598
		public static GameRequestServerEntry Deserialize(Stream stream, GameRequestServerEntry instance, long limit)
		{
			if (instance.GameRequests == null)
			{
				instance.GameRequests = new List<GameRequestEntry>();
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
					else
					{
						instance.GameRequests.Add(GameRequestEntry.DeserializeLengthDelimited(stream));
					}
				}
				else if (instance.Host == null)
				{
					instance.Host = HostRoute.DeserializeLengthDelimited(stream);
				}
				else
				{
					HostRoute.DeserializeLengthDelimited(stream, instance.Host);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060039F9 RID: 14841 RVA: 0x000BC462 File Offset: 0x000BA662
		public void Serialize(Stream stream)
		{
			GameRequestServerEntry.Serialize(stream, this);
		}

		// Token: 0x060039FA RID: 14842 RVA: 0x000BC46C File Offset: 0x000BA66C
		public static void Serialize(Stream stream, GameRequestServerEntry instance)
		{
			if (instance.HasHost)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Host.GetSerializedSize());
				HostRoute.Serialize(stream, instance.Host);
			}
			if (instance.GameRequests.Count > 0)
			{
				foreach (GameRequestEntry gameRequestEntry in instance.GameRequests)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, gameRequestEntry.GetSerializedSize());
					GameRequestEntry.Serialize(stream, gameRequestEntry);
				}
			}
		}

		// Token: 0x060039FB RID: 14843 RVA: 0x000BC510 File Offset: 0x000BA710
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasHost)
			{
				num += 1U;
				uint serializedSize = this.Host.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.GameRequests.Count > 0)
			{
				foreach (GameRequestEntry gameRequestEntry in this.GameRequests)
				{
					num += 1U;
					uint serializedSize2 = gameRequestEntry.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			return num;
		}

		// Token: 0x0400152A RID: 5418
		public bool HasHost;

		// Token: 0x0400152B RID: 5419
		private HostRoute _Host;

		// Token: 0x0400152C RID: 5420
		private List<GameRequestEntry> _GameRequests = new List<GameRequestEntry>();
	}
}
