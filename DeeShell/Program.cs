/* DeeShell - A shell replacement for Windows
 * Pravin Paratey (February 19, 2007)
 * 
 * Article: http://www.dustyant.com/articles/deeshell
 * 
 * Released under Creative Commons Attribution 2.5 Licence
 * http://creativecommons.org/licenses/by/2.5/
 */
using System;
using System.Drawing;
using System.Threading;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DeeShell
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			// Make new Working Area
			Functions.HideTaskBar();
			Functions.MakeNewDesktopArea();
			
			Application.Run(new Taskbar());

			// Restore Working Area Size
			Functions.RestoreDesktopArea();
			Functions.ShowTaskBar();
		}
	}
}