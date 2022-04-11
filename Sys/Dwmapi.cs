﻿using System;
using System.Runtime.InteropServices;
using Nex.Sys.Data;

namespace Nex.Sys
{
    public static class Dwmapi
    {
        /// <summary>
        /// Extends the window frame into the client area.
        /// </summary>
        [DllImport("dwmapi.dll", SetLastError = true)]
        public static extern void DwmExtendFrameIntoClientArea(IntPtr hWnd, ref Margins pMargins);
    }
}
