using System;
using System.Runtime.InteropServices;

namespace Blizzard.Commerce
{
	// Token: 0x0200124F RID: 4687
	public class blz_commerce_character_input_t : IDisposable
	{
		// Token: 0x0600D1E4 RID: 53732 RVA: 0x003E27D4 File Offset: 0x003E09D4
		internal blz_commerce_character_input_t(IntPtr cPtr, bool cMemoryOwn)
		{
			this.swigCMemOwn = cMemoryOwn;
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600D1E5 RID: 53733 RVA: 0x003E27F0 File Offset: 0x003E09F0
		internal static HandleRef getCPtr(blz_commerce_character_input_t obj)
		{
			if (obj != null)
			{
				return obj.swigCPtr;
			}
			return new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600D1E6 RID: 53734 RVA: 0x003E2808 File Offset: 0x003E0A08
		~blz_commerce_character_input_t()
		{
			this.Dispose(false);
		}

		// Token: 0x0600D1E7 RID: 53735 RVA: 0x003E2838 File Offset: 0x003E0A38
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600D1E8 RID: 53736 RVA: 0x003E2848 File Offset: 0x003E0A48
		protected virtual void Dispose(bool disposing)
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						battlenet_commercePINVOKE.delete_blz_commerce_character_input_t(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
			}
		}

		// Token: 0x170010E2 RID: 4322
		// (get) Token: 0x0600D1EA RID: 53738 RVA: 0x003E28CE File Offset: 0x003E0ACE
		// (set) Token: 0x0600D1E9 RID: 53737 RVA: 0x003E28C0 File Offset: 0x003E0AC0
		public int character
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_character_input_t_character_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_character_input_t_character_set(this.swigCPtr, value);
			}
		}

		// Token: 0x170010E3 RID: 4323
		// (get) Token: 0x0600D1EC RID: 53740 RVA: 0x003E28E9 File Offset: 0x003E0AE9
		// (set) Token: 0x0600D1EB RID: 53739 RVA: 0x003E28DB File Offset: 0x003E0ADB
		public uint modifiers
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_character_input_t_modifiers_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_character_input_t_modifiers_set(this.swigCPtr, value);
			}
		}

		// Token: 0x0600D1ED RID: 53741 RVA: 0x003E28F6 File Offset: 0x003E0AF6
		public blz_commerce_character_input_t() : this(battlenet_commercePINVOKE.new_blz_commerce_character_input_t(), true)
		{
		}

		// Token: 0x0400A2FE RID: 41726
		private HandleRef swigCPtr;

		// Token: 0x0400A2FF RID: 41727
		protected bool swigCMemOwn;
	}
}
