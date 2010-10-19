/* DeeShell - A shell replacement for Windows
 * Pravin Paratey (February 19, 2007)
 * 
 * Article: http://www.dustyant.com/articles/deeshell
 * 
 * Released under Creative Commons Attribution 2.5 Licence
 * http://creativecommons.org/licenses/by/2.5/
 */
using System;
using System.Data;
using System.Text;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.ComponentModel;

namespace DeeShell
{
	public partial class Taskbar : Form
	{
		public Taskbar()
		{
			InitializeComponent();
			WinAPI.SetWindowPos(this.Handle, IntPtr.Zero, 0, 0, SystemInformation.VirtualScreen.Width, 24, 0x0040);
			ArrayList ar = Functions.GetActiveTasks();
			for (int i = 0; i < ar.Count; i++)
			{
				WindowData w = (WindowData)ar[i];
				cboTaskList.Items.Add(w.title);
			}
		}

		private void OnExitClick(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void cboTaskList_SelectedIndexChanged(object sender, EventArgs e)
		{
			string windowName = cboTaskList.Text;
			IntPtr handle = WinAPI.FindWindow(null, windowName);
			WinAPI.SetForegroundWindow(handle);
		}
	}
}