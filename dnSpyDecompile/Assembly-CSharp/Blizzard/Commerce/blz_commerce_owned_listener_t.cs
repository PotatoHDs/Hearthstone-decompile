using System;
using System.Runtime.InteropServices;

namespace Blizzard.Commerce
{
	// Token: 0x02001232 RID: 4658
	public class blz_commerce_owned_listener_t : IDisposable
	{
		// Token: 0x0600D0EF RID: 53487 RVA: 0x003E0A00 File Offset: 0x003DEC00
		internal blz_commerce_owned_listener_t(IntPtr cPtr, bool cMemoryOwn)
		{
			this.swigCMemOwn = cMemoryOwn;
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600D0F0 RID: 53488 RVA: 0x003E0A1C File Offset: 0x003DEC1C
		internal static HandleRef getCPtr(blz_commerce_owned_listener_t obj)
		{
			if (obj != null)
			{
				return obj.swigCPtr;
			}
			return new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600D0F1 RID: 53489 RVA: 0x003E0A34 File Offset: 0x003DEC34
		~blz_commerce_owned_listener_t()
		{
			this.Dispose(false);
		}

		// Token: 0x0600D0F2 RID: 53490 RVA: 0x003E0A64 File Offset: 0x003DEC64
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600D0F3 RID: 53491 RVA: 0x003E0A74 File Offset: 0x003DEC74
		protected virtual void Dispose(bool disposing)
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						battlenet_commercePINVOKE.delete_blz_commerce_owned_listener_t(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
			}
		}

		// Token: 0x170010A6 RID: 4262
		// (get) Token: 0x0600D0F5 RID: 53493 RVA: 0x003E0AFA File Offset: 0x003DECFA
		// (set) Token: 0x0600D0F4 RID: 53492 RVA: 0x003E0AEC File Offset: 0x003DECEC
		public IntPtr owner
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_owned_listener_t_owner_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_owned_listener_t_owner_set(this.swigCPtr, value);
			}
		}

		// Token: 0x170010A7 RID: 4263
		// (get) Token: 0x0600D0F7 RID: 53495 RVA: 0x00090064 File Offset: 0x0008E264
		// (set) Token: 0x0600D0F6 RID: 53494 RVA: 0x00003BE8 File Offset: 0x00001DE8
		public blz_commerce_listener listener
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x0600D0F8 RID: 53496 RVA: 0x003E0B07 File Offset: 0x003DED07
		public blz_commerce_owned_listener_t() : this(battlenet_commercePINVOKE.new_blz_commerce_owned_listener_t(), true)
		{
		}

		// Token: 0x0400A297 RID: 41623
		private HandleRef swigCPtr;

		// Token: 0x0400A298 RID: 41624
		protected bool swigCMemOwn;
	}
}
