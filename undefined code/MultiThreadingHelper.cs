using System;
using System.Windows.Forms;

namespace Gds.Helper.WinForms
{
    /// <summary>
    /// Provide method for multi threading helper.
    /// </summary>
    public static class MultiThreadingHelper
    {
            /// <summary>
            /// Check need invoke for execute delegate.
            /// </summary>
            /// <param name="control">Control</param>
            /// <param name="action">Delegate</param>
            public static void InvokeEx(this Control control, Action action)
            {
                if (control.InvokeRequired)
                    control.Invoke(action);
                else
                    action();
            }
        /// <summary>
        /// Check need invoke for execute delegate.
        /// </summary>
        /// <typeparam name="T">Type of parameter delegate.</typeparam>
        /// <param name="control">Control.</param>
        /// <param name="action">Delegate.</param>
        /// <param name="obj">Parameter for delegate.</param>
        public static void InvokeEx<T>(this Control control, Action<T> action, T obj)
            {
                if (control.InvokeRequired)
                    control.Invoke(action, obj);
                else
                    action(obj);
            }
    }
}
