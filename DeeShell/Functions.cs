/* DeeShell - A shell replacement for Windows
 * Pravin Paratey (February 19, 2007)
 * 
 * Article: http://www.dustyant.com/articles/deeshell
 * 
 * Released under Creative Commons Attribution 2.5 Licence
 * http://creativecommons.org/licenses/by/2.5/
 */
using System;
using System.Text;
using System.Diagnostics;
using System.Collections;
using System.Windows.Forms;

namespace DeeShell
{
	struct WindowData
	{
		public IntPtr hwnd;
		public string title;
	}
	class Functions
	{
		#region Private variables
		private static WinAPI.RECT m_rcOldDesktopRect;
		private static IntPtr m_hTaskBar;
		#endregion

		/// <summary>
		/// Resizes the Desktop area to our shells' requirements
		/// </summary>
		public static void MakeNewDesktopArea()
		{
			// Save current Working Area size
			m_rcOldDesktopRect.left = SystemInformation.WorkingArea.Left;
			m_rcOldDesktopRect.top = SystemInformation.WorkingArea.Top;
			m_rcOldDesktopRect.right = SystemInformation.WorkingArea.Right;
			m_rcOldDesktopRect.bottom = SystemInformation.WorkingArea.Bottom;

			// Make a new Workspace
			WinAPI.RECT rc;
			rc.left = SystemInformation.VirtualScreen.Left;
			rc.top = SystemInformation.VirtualScreen.Top + 24; // We reserve the 24 pixels on top for our taskbar
			rc.right = SystemInformation.VirtualScreen.Right;
			rc.bottom = SystemInformation.VirtualScreen.Bottom;
			WinAPI.SystemParametersInfo((int)WinAPI.SPI.SPI_SETWORKAREA, 0, ref rc, 0);
		}

		/// <summary>
		/// Restores the Desktop area
		/// </summary>
		public static void RestoreDesktopArea()
		{
			WinAPI.SystemParametersInfo((int)WinAPI.SPI.SPI_SETWORKAREA, 0, ref m_rcOldDesktopRect, 0);
		}

		/// <summary>
		/// Hides the Windows Taskbar
		/// </summary>
		public static void HideTaskBar()
		{
			// Get the Handle to the Windows Taskbar
			m_hTaskBar = WinAPI.FindWindow("Shell_TrayWnd", null);
			// Hide the Taskbar
			if (m_hTaskBar != IntPtr.Zero)
			{
				WinAPI.ShowWindow(m_hTaskBar, (int)WinAPI.WindowShowStyle.Hide);
			}
		}

		/// <summary>
		/// Show the Windows Taskbar
		/// </summary>
		public static void ShowTaskBar()
		{
			if (m_hTaskBar != IntPtr.Zero)
			{
				WinAPI.ShowWindow(m_hTaskBar, (int)WinAPI.WindowShowStyle.Show);
			}
		}

		/// <summary>
		/// Gets a list of Active Tasks
		/// </summary>
		public static ArrayList GetActiveTasks()
		{
			ArrayList ar = new ArrayList();
			IntPtr child = IntPtr.Zero;

			Process[] process = Process.GetProcesses();
			foreach (Process p in process)
			{
				WindowData w;
				if (p.MainWindowHandle != IntPtr.Zero && p.MainWindowTitle.Length > 0)
				{
					w.hwnd = p.MainWindowHandle;
					w.title = p.MainWindowTitle;
					ar.Add(w);
				}
			}
			return ar;
		}
	}
}
