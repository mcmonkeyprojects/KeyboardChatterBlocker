using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace KeyboardChatterBlocker
{
    public static class FullScreenDetectHelper
    {
        public enum QUERY_USER_NOTIFICATION_STATE
        {
            QUNS_NOT_PRESENT = 1,
            QUNS_BUSY = 2,
            QUNS_RUNNING_D3D_FULL_SCREEN = 3,
            QUNS_PRESENTATION_MODE = 4,
            QUNS_ACCEPTS_NOTIFICATIONS = 5,
            QUNS_QUIET_TIME = 6
        }

        [DllImport("shell32.dll")]
        public static extern int SHQueryUserNotificationState(out QUERY_USER_NOTIFICATION_STATE pquns);

        public static bool IsFullscreen()
        {
            SHQueryUserNotificationState(out QUERY_USER_NOTIFICATION_STATE state);
            return state == QUERY_USER_NOTIFICATION_STATE.QUNS_RUNNING_D3D_FULL_SCREEN;
        }
    }
}
