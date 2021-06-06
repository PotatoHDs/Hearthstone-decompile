using System;
using System.Runtime.InteropServices;

namespace Blizzard.Commerce
{
	// Token: 0x0200123C RID: 4668
	public class blz_commerce_vec2d_t : IDisposable
	{
		// Token: 0x0600D13F RID: 53567 RVA: 0x003E1380 File Offset: 0x003DF580
		internal blz_commerce_vec2d_t(IntPtr cPtr, bool cMemoryOwn)
		{
			this.swigCMemOwn = cMemoryOwn;
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600D140 RID: 53568 RVA: 0x003E139C File Offset: 0x003DF59C
		internal static HandleRef getCPtr(blz_commerce_vec2d_t obj)
		{
			if (obj != null)
			{
				return obj.swigCPtr;
			}
			return new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600D141 RID: 53569 RVA: 0x003E13B4 File Offset: 0x003DF5B4
		~blz_commerce_vec2d_t()
		{
			this.Dispose(false);
		}

		// Token: 0x0600D142 RID: 53570 RVA: 0x003E13E4 File Offset: 0x003DF5E4
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600D143 RID: 53571 RVA: 0x003E13F4 File Offset: 0x003DF5F4
		protected virtual void Dispose(bool disposing)
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						battlenet_commercePINVOKE.delete_blz_commerce_vec2d_t(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
			}
		}

		// Token: 0x170010BC RID: 4284
		// (get) Token: 0x0600D145 RID: 53573 RVA: 0x003E147A File Offset: 0x003DF67A
		// (set) Token: 0x0600D144 RID: 53572 RVA: 0x003E146C File Offset: 0x003DF66C
		public int x
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_vec2d_t_x_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_vec2d_t_x_set(this.swigCPtr, value);
			}
		}

		// Token: 0x170010BD RID: 4285
		// (get) Token: 0x0600D147 RID: 53575 RVA: 0x003E1495 File Offset: 0x003DF695
		// (set) Token: 0x0600D146 RID: 53574 RVA: 0x003E1487 File Offset: 0x003DF687
		public int y
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_vec2d_t_y_get(this.swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_vec2d_t_y_set(this.swigCPtr, value);
			}
		}

		// Token: 0x0600D148 RID: 53576 RVA: 0x003E14A2 File Offset: 0x003DF6A2
		public blz_commerce_vec2d_t() : this(battlenet_commercePINVOKE.new_blz_commerce_vec2d_t(), true)
		{
		}

		// Token: 0x0400A2B6 RID: 41654
		private HandleRef swigCPtr;

		// Token: 0x0400A2B7 RID: 41655
		protected bool swigCMemOwn;
	}
}
