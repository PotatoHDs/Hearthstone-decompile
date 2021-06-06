using System;
using System.Runtime.InteropServices;

namespace Blizzard.Commerce
{
	// Token: 0x02001252 RID: 4690
	public class blz_commerce_window_close_t : IDisposable
	{
		// Token: 0x0600D204 RID: 53764 RVA: 0x003E2BD0 File Offset: 0x003E0DD0
		internal blz_commerce_window_close_t(IntPtr cPtr, bool cMemoryOwn)
		{
			this.swigCMemOwn = cMemoryOwn;
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600D205 RID: 53765 RVA: 0x003E2BEC File Offset: 0x003E0DEC
		internal static HandleRef getCPtr(blz_commerce_window_close_t obj)
		{
			if (obj != null)
			{
				return obj.swigCPtr;
			}
			return new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600D206 RID: 53766 RVA: 0x003E2C04 File Offset: 0x003E0E04
		~blz_commerce_window_close_t()
		{
			this.Dispose(false);
		}

		// Token: 0x0600D207 RID: 53767 RVA: 0x003E2C34 File Offset: 0x003E0E34
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600D208 RID: 53768 RVA: 0x003E2C44 File Offset: 0x003E0E44
		protected virtual void Dispose(bool disposing)
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						battlenet_commercePINVOKE.delete_blz_commerce_window_close_t(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
			}
		}

		// Token: 0x170010E9 RID: 4329
		// (get) Token: 0x0600D20A RID: 53770 RVA: 0x003E2CCA File Offset: 0x003E0ECA
		// (set) Token: 0x0600D209 RID: 53769 RVA: 0x003E2CBC File Offset: 0x003E0EBC
		public uint browser_id
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_window_close_t_browser_id_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_window_close_t_browser_id_set(this.swigCPtr, value);
			}
		}

		// Token: 0x0600D20B RID: 53771 RVA: 0x003E2CD7 File Offset: 0x003E0ED7
		public blz_commerce_window_close_t() : this(battlenet_commercePINVOKE.new_blz_commerce_window_close_t(), true)
		{
		}

		// Token: 0x0400A304 RID: 41732
		private HandleRef swigCPtr;

		// Token: 0x0400A305 RID: 41733
		protected bool swigCMemOwn;
	}
}
