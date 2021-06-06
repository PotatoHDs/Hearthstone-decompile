using System;
using System.IO;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011AD RID: 4525
	public class CollectionLeftRightClick : IProtoBuf
	{
		// Token: 0x17000EC7 RID: 3783
		// (get) Token: 0x0600C84F RID: 51279 RVA: 0x003C227B File Offset: 0x003C047B
		// (set) Token: 0x0600C850 RID: 51280 RVA: 0x003C2283 File Offset: 0x003C0483
		public CollectionLeftRightClick.Target Target_
		{
			get
			{
				return this._Target_;
			}
			set
			{
				this._Target_ = value;
				this.HasTarget_ = true;
			}
		}

		// Token: 0x0600C851 RID: 51281 RVA: 0x003C2294 File Offset: 0x003C0494
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasTarget_)
			{
				num ^= this.Target_.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600C852 RID: 51282 RVA: 0x003C22D0 File Offset: 0x003C04D0
		public override bool Equals(object obj)
		{
			CollectionLeftRightClick collectionLeftRightClick = obj as CollectionLeftRightClick;
			return collectionLeftRightClick != null && this.HasTarget_ == collectionLeftRightClick.HasTarget_ && (!this.HasTarget_ || this.Target_.Equals(collectionLeftRightClick.Target_));
		}

		// Token: 0x0600C853 RID: 51283 RVA: 0x003C2323 File Offset: 0x003C0523
		public void Deserialize(Stream stream)
		{
			CollectionLeftRightClick.Deserialize(stream, this);
		}

		// Token: 0x0600C854 RID: 51284 RVA: 0x003C232D File Offset: 0x003C052D
		public static CollectionLeftRightClick Deserialize(Stream stream, CollectionLeftRightClick instance)
		{
			return CollectionLeftRightClick.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600C855 RID: 51285 RVA: 0x003C2338 File Offset: 0x003C0538
		public static CollectionLeftRightClick DeserializeLengthDelimited(Stream stream)
		{
			CollectionLeftRightClick collectionLeftRightClick = new CollectionLeftRightClick();
			CollectionLeftRightClick.DeserializeLengthDelimited(stream, collectionLeftRightClick);
			return collectionLeftRightClick;
		}

		// Token: 0x0600C856 RID: 51286 RVA: 0x003C2354 File Offset: 0x003C0554
		public static CollectionLeftRightClick DeserializeLengthDelimited(Stream stream, CollectionLeftRightClick instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return CollectionLeftRightClick.Deserialize(stream, instance, num);
		}

		// Token: 0x0600C857 RID: 51287 RVA: 0x003C237C File Offset: 0x003C057C
		public static CollectionLeftRightClick Deserialize(Stream stream, CollectionLeftRightClick instance, long limit)
		{
			instance.Target_ = CollectionLeftRightClick.Target.CARD;
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
				else if (num == 8)
				{
					instance.Target_ = (CollectionLeftRightClick.Target)ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x0600C858 RID: 51288 RVA: 0x003C2403 File Offset: 0x003C0603
		public void Serialize(Stream stream)
		{
			CollectionLeftRightClick.Serialize(stream, this);
		}

		// Token: 0x0600C859 RID: 51289 RVA: 0x003C240C File Offset: 0x003C060C
		public static void Serialize(Stream stream, CollectionLeftRightClick instance)
		{
			if (instance.HasTarget_)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Target_));
			}
		}

		// Token: 0x0600C85A RID: 51290 RVA: 0x003C242C File Offset: 0x003C062C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasTarget_)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Target_));
			}
			return num;
		}

		// Token: 0x04009ED9 RID: 40665
		public bool HasTarget_;

		// Token: 0x04009EDA RID: 40666
		private CollectionLeftRightClick.Target _Target_;

		// Token: 0x0200293D RID: 10557
		public enum Target
		{
			// Token: 0x0400FC43 RID: 64579
			CARD = 1,
			// Token: 0x0400FC44 RID: 64580
			HERO_SKIN,
			// Token: 0x0400FC45 RID: 64581
			CARD_BACK
		}
	}
}
