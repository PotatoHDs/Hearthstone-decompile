using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.games.v1
{
	// Token: 0x0200039F RID: 927
	public class Player : IProtoBuf
	{
		// Token: 0x17000AE4 RID: 2788
		// (get) Token: 0x06003BAD RID: 15277 RVA: 0x000C0CDE File Offset: 0x000BEEDE
		// (set) Token: 0x06003BAE RID: 15278 RVA: 0x000C0CE6 File Offset: 0x000BEEE6
		public Identity Identity
		{
			get
			{
				return this._Identity;
			}
			set
			{
				this._Identity = value;
				this.HasIdentity = (value != null);
			}
		}

		// Token: 0x06003BAF RID: 15279 RVA: 0x000C0CF9 File Offset: 0x000BEEF9
		public void SetIdentity(Identity val)
		{
			this.Identity = val;
		}

		// Token: 0x17000AE5 RID: 2789
		// (get) Token: 0x06003BB0 RID: 15280 RVA: 0x000C0D02 File Offset: 0x000BEF02
		// (set) Token: 0x06003BB1 RID: 15281 RVA: 0x000C0D0A File Offset: 0x000BEF0A
		public List<Attribute> Attribute
		{
			get
			{
				return this._Attribute;
			}
			set
			{
				this._Attribute = value;
			}
		}

		// Token: 0x17000AE6 RID: 2790
		// (get) Token: 0x06003BB2 RID: 15282 RVA: 0x000C0D02 File Offset: 0x000BEF02
		public List<Attribute> AttributeList
		{
			get
			{
				return this._Attribute;
			}
		}

		// Token: 0x17000AE7 RID: 2791
		// (get) Token: 0x06003BB3 RID: 15283 RVA: 0x000C0D13 File Offset: 0x000BEF13
		public int AttributeCount
		{
			get
			{
				return this._Attribute.Count;
			}
		}

		// Token: 0x06003BB4 RID: 15284 RVA: 0x000C0D20 File Offset: 0x000BEF20
		public void AddAttribute(Attribute val)
		{
			this._Attribute.Add(val);
		}

		// Token: 0x06003BB5 RID: 15285 RVA: 0x000C0D2E File Offset: 0x000BEF2E
		public void ClearAttribute()
		{
			this._Attribute.Clear();
		}

		// Token: 0x06003BB6 RID: 15286 RVA: 0x000C0D3B File Offset: 0x000BEF3B
		public void SetAttribute(List<Attribute> val)
		{
			this.Attribute = val;
		}

		// Token: 0x17000AE8 RID: 2792
		// (get) Token: 0x06003BB7 RID: 15287 RVA: 0x000C0D44 File Offset: 0x000BEF44
		// (set) Token: 0x06003BB8 RID: 15288 RVA: 0x000C0D4C File Offset: 0x000BEF4C
		public double Rating
		{
			get
			{
				return this._Rating;
			}
			set
			{
				this._Rating = value;
				this.HasRating = true;
			}
		}

		// Token: 0x06003BB9 RID: 15289 RVA: 0x000C0D5C File Offset: 0x000BEF5C
		public void SetRating(double val)
		{
			this.Rating = val;
		}

		// Token: 0x17000AE9 RID: 2793
		// (get) Token: 0x06003BBA RID: 15290 RVA: 0x000C0D65 File Offset: 0x000BEF65
		// (set) Token: 0x06003BBB RID: 15291 RVA: 0x000C0D6D File Offset: 0x000BEF6D
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

		// Token: 0x06003BBC RID: 15292 RVA: 0x000C0D80 File Offset: 0x000BEF80
		public void SetHost(HostRoute val)
		{
			this.Host = val;
		}

		// Token: 0x06003BBD RID: 15293 RVA: 0x000C0D8C File Offset: 0x000BEF8C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasIdentity)
			{
				num ^= this.Identity.GetHashCode();
			}
			foreach (Attribute attribute in this.Attribute)
			{
				num ^= attribute.GetHashCode();
			}
			if (this.HasRating)
			{
				num ^= this.Rating.GetHashCode();
			}
			if (this.HasHost)
			{
				num ^= this.Host.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003BBE RID: 15294 RVA: 0x000C0E34 File Offset: 0x000BF034
		public override bool Equals(object obj)
		{
			Player player = obj as Player;
			if (player == null)
			{
				return false;
			}
			if (this.HasIdentity != player.HasIdentity || (this.HasIdentity && !this.Identity.Equals(player.Identity)))
			{
				return false;
			}
			if (this.Attribute.Count != player.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Attribute.Count; i++)
			{
				if (!this.Attribute[i].Equals(player.Attribute[i]))
				{
					return false;
				}
			}
			return this.HasRating == player.HasRating && (!this.HasRating || this.Rating.Equals(player.Rating)) && this.HasHost == player.HasHost && (!this.HasHost || this.Host.Equals(player.Host));
		}

		// Token: 0x17000AEA RID: 2794
		// (get) Token: 0x06003BBF RID: 15295 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003BC0 RID: 15296 RVA: 0x000C0F23 File Offset: 0x000BF123
		public static Player ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<Player>(bs, 0, -1);
		}

		// Token: 0x06003BC1 RID: 15297 RVA: 0x000C0F2D File Offset: 0x000BF12D
		public void Deserialize(Stream stream)
		{
			Player.Deserialize(stream, this);
		}

		// Token: 0x06003BC2 RID: 15298 RVA: 0x000C0F37 File Offset: 0x000BF137
		public static Player Deserialize(Stream stream, Player instance)
		{
			return Player.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003BC3 RID: 15299 RVA: 0x000C0F44 File Offset: 0x000BF144
		public static Player DeserializeLengthDelimited(Stream stream)
		{
			Player player = new Player();
			Player.DeserializeLengthDelimited(stream, player);
			return player;
		}

		// Token: 0x06003BC4 RID: 15300 RVA: 0x000C0F60 File Offset: 0x000BF160
		public static Player DeserializeLengthDelimited(Stream stream, Player instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return Player.Deserialize(stream, instance, num);
		}

		// Token: 0x06003BC5 RID: 15301 RVA: 0x000C0F88 File Offset: 0x000BF188
		public static Player Deserialize(Stream stream, Player instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			if (instance.Attribute == null)
			{
				instance.Attribute = new List<Attribute>();
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
						if (num != 10)
						{
							if (num == 18)
							{
								instance.Attribute.Add(bnet.protocol.Attribute.DeserializeLengthDelimited(stream));
								continue;
							}
						}
						else
						{
							if (instance.Identity == null)
							{
								instance.Identity = Identity.DeserializeLengthDelimited(stream);
								continue;
							}
							Identity.DeserializeLengthDelimited(stream, instance.Identity);
							continue;
						}
					}
					else
					{
						if (num == 25)
						{
							instance.Rating = binaryReader.ReadDouble();
							continue;
						}
						if (num == 34)
						{
							if (instance.Host == null)
							{
								instance.Host = HostRoute.DeserializeLengthDelimited(stream);
								continue;
							}
							HostRoute.DeserializeLengthDelimited(stream, instance.Host);
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

		// Token: 0x06003BC6 RID: 15302 RVA: 0x000C10B2 File Offset: 0x000BF2B2
		public void Serialize(Stream stream)
		{
			Player.Serialize(stream, this);
		}

		// Token: 0x06003BC7 RID: 15303 RVA: 0x000C10BC File Offset: 0x000BF2BC
		public static void Serialize(Stream stream, Player instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasIdentity)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Identity.GetSerializedSize());
				Identity.Serialize(stream, instance.Identity);
			}
			if (instance.Attribute.Count > 0)
			{
				foreach (Attribute attribute in instance.Attribute)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, attribute.GetSerializedSize());
					bnet.protocol.Attribute.Serialize(stream, attribute);
				}
			}
			if (instance.HasRating)
			{
				stream.WriteByte(25);
				binaryWriter.Write(instance.Rating);
			}
			if (instance.HasHost)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.Host.GetSerializedSize());
				HostRoute.Serialize(stream, instance.Host);
			}
		}

		// Token: 0x06003BC8 RID: 15304 RVA: 0x000C11B0 File Offset: 0x000BF3B0
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasIdentity)
			{
				num += 1U;
				uint serializedSize = this.Identity.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.Attribute.Count > 0)
			{
				foreach (Attribute attribute in this.Attribute)
				{
					num += 1U;
					uint serializedSize2 = attribute.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			if (this.HasRating)
			{
				num += 1U;
				num += 8U;
			}
			if (this.HasHost)
			{
				num += 1U;
				uint serializedSize3 = this.Host.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			return num;
		}

		// Token: 0x04001584 RID: 5508
		public bool HasIdentity;

		// Token: 0x04001585 RID: 5509
		private Identity _Identity;

		// Token: 0x04001586 RID: 5510
		private List<Attribute> _Attribute = new List<Attribute>();

		// Token: 0x04001587 RID: 5511
		public bool HasRating;

		// Token: 0x04001588 RID: 5512
		private double _Rating;

		// Token: 0x04001589 RID: 5513
		public bool HasHost;

		// Token: 0x0400158A RID: 5514
		private HostRoute _Host;
	}
}
