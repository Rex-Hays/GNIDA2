using System;

namespace LIDE
{
	public partial class Window : Gtk.Window
	{
		public Window () : 
				base(Gtk.WindowType.Toplevel)
		{
            this.Build();
		}
	}
}

