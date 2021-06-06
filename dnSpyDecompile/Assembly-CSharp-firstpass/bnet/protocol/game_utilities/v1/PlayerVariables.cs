using System;
using System.Collections.Generic;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.game_utilities.v1
{
	// Token: 0x02000366 RID: 870
	public class PlayerVariables : IProtoBuf
	{
		// Token: 0x17000A03 RID: 2563
		// (get) Token: 0x06003706 RID: 14086 RVA: 0x000B5242 File Offset: 0x000B3442
		// (set) Token: 0x06003707 RID: 14087 RVA: 0x000B524A File Offset: 0x000B344A
		public Identity Identity { get; set; }

		// Token: 0x06003708 RID: 14088 RVA: 0x000B5253 File Offset: 0x000B3453
		public void SetIdentity(Identity val)
		{
			this.Identity = val;
		}

		// Token: 0x17000A04 RID: 2564
		// (get) Token: 0x06003709 RID: 14089 RVA: 0x000B525C File Offset: 0x000B345C
		// (set) Token: 0x0600370A RID: 14090 RVA: 0x000B5264 File Offset: 0x000B3464
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

		// Token: 0x0600370B RID: 14091 RVA: 0x000B5274 File Offset: 0x000B3474
		public void SetRating(double val)
		{
			this.Rating = val;
		}

		// Token: 0x17000A05 RID: 2565
		// (get) Token: 0x0600370C RID: 14092 RVA: 0x000B527D File Offset: 0x000B347D
		// (set) Token: 0x0600370D RID: 14093 RVA: 0x000B5285 File Offset: 0x000B3485
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

		// Token: 0x17000A06 RID: 2566
		// (get) Token: 0x0600370E RID: 14094 RVA: 0x000B527D File Offset: 0x000B347D
		public List<Attribute> AttributeList
		{
			get
			{
				return this._Attribute;
			}
		}

		// Token: 0x17000A07 RID: 2567
		// (get) Token: 0x0600370F RID: 14095 RVA: 0x000B528E File Offset: 0x000B348E
		public int AttributeCount
		{
			get
			{
				return this._Attribute.Count;
			}
		}

		// Token: 0x06003710 RID: 14096 RVA: 0x000B529B File Offset: 0x000B349B
		public void AddAttribute(Attribute val)
		{
			this._Attribute.Add(val);
		}

		// Token: 0x06003711 RID: 14097 RVA: 0x000B52A9 File Offset: 0x000B34A9
		public void ClearAttribute()
		{
			this._Attribute.Clear();
		}

		// Token: 0x06003712 RID: 14098 RVA: 0x000B52B6 File Offset: 0x000B34B6
		public void SetAttribute(List<Attribute> val)
		{
			this.Attribute = val;
		}

		// Token: 0x06003713 RID: 14099 RVA: 0x000B52C0 File Offset: 0x000B34C0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Identity.GetHashCode();
			if (this.HasRating)
			{
				num ^= this.Rating.GetHashCode();
			}
			foreach (Attribute attribute in this.Attribute)
			{
				num ^= attribute.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003714 RID: 14100 RVA: 0x000B534C File Offset: 0x000B354C
		public override bool Equals(object obj)
		{
			PlayerVariables playerVariables = obj as PlayerVariables;
			if (playerVariables == null)
			{
				return false;
			}
			if (!this.Identity.Equals(playerVariables.Identity))
			{
				return false;
			}
			if (this.HasRating != playerVariables.HasRating || (this.HasRating && !this.Rating.Equals(playerVariables.Rating)))
			{
				return false;
			}
			if (this.Attribute.Count != playerVariables.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Attribute.Count; i++)
			{
				if (!this.Attribute[i].Equals(playerVariables.Attribute[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000A08 RID: 2568
		// (get) Token: 0x06003715 RID: 14101 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003716 RID: 14102 RVA: 0x000B53FA File Offset: 0x000B35FA
		public static PlayerVariables ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<PlayerVariables>(bs, 0, -1);
		}

		// Token: 0x06003717 RID: 14103 RVA: 0x000B5404 File Offset: 0x000B3604
		public void Deserialize(Stream stream)
		{
			PlayerVariables.Deserialize(stream, this);
		}

		// Token: 0x06003718 RID: 14104 RVA: 0x000B540E File Offset: 0x000B360E
		public static PlayerVariables Deserialize(Stream stream, PlayerVariables instance)
		{
			return PlayerVariables.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003719 RID: 14105 RVA: 0x000B541C File Offset: 0x000B361C
		public static PlayerVariables DeserializeLengthDelimited(Stream stream)
		{
			PlayerVariables playerVariables = new PlayerVariables();
			PlayerVariables.DeserializeLengthDelimited(stream, playerVariables);
			return playerVariables;
		}

		// Token: 0x0600371A RID: 14106 RVA: 0x000B5438 File Offset: 0x000B3638
		public static PlayerVariables DeserializeLengthDelimited(Stream stream, PlayerVariables instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return PlayerVariables.Deserialize(stream, instance, num);
		}

		// Token: 0x0600371B RID: 14107 RVA: 0x000B5460 File Offset: 0x000B3660
		public static PlayerVariables Deserialize(Stream stream, PlayerVariables instance, long limit)
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
				else if (num != 10)
				{
					if (num != 17)
					{
						if (num != 26)
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
							instance.Attribute.Add(bnet.protocol.Attribute.DeserializeLengthDelimited(stream));
						}
					}
					else
					{
						instance.Rating = binaryReader.ReadDouble();
					}
				}
				else if (instance.Identity == null)
				{
					instance.Identity = Identity.DeserializeLengthDelimited(stream);
				}
				else
				{
					Identity.DeserializeLengthDelimited(stream, instance.Identity);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600371C RID: 14108 RVA: 0x000B554D File Offset: 0x000B374D
		public void Serialize(Stream stream)
		{
			PlayerVariables.Serialize(stream, this);
		}

		// Token: 0x0600371D RID: 14109 RVA: 0x000B5558 File Offset: 0x000B3758
		public static void Serialize(Stream stream, PlayerVariables instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.Identity == null)
			{
				throw new ArgumentNullException("Identity", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.Identity.GetSerializedSize());
			Identity.Serialize(stream, instance.Identity);
			if (instance.HasRating)
			{
				stream.WriteByte(17);
				binaryWriter.Write(instance.Rating);
			}
			if (instance.Attribute.Count > 0)
			{
				foreach (Attribute attribute in instance.Attribute)
				{
					stream.WriteByte(26);
					ProtocolParser.WriteUInt32(stream, attribute.GetSerializedSize());
					bnet.protocol.Attribute.Serialize(stream, attribute);
				}
			}
		}

		// Token: 0x0600371E RID: 14110 RVA: 0x000B5630 File Offset: 0x000B3830
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint serializedSize = this.Identity.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (this.HasRating)
			{
				num += 1U;
				num += 8U;
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
			num += 1U;
			return num;
		}

		// Token: 0x040014A9 RID: 5289
		public bool HasRating;

		// Token: 0x040014AA RID: 5290
		private double _Rating;

		// Token: 0x040014AB RID: 5291
		private List<Attribute> _Attribute = new List<Attribute>();
	}
}
