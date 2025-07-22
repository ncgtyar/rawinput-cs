using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rawinput
{
    public static class consts
    {
        public static class RawInputConstants
        {
            public const uint RIM_INPUT = 0x00000000;
            public const uint RIM_INPUTSINK = 0x00000100;
            public const uint RIDI_PREPARSEDDATA = 0x20000005;
            public const uint RIDI_DEVICENAME = 0x20000007;
            public const uint RIDI_DEVICEINFO = 0x2000000b;
            public const ushort HID_USAGE_PAGE_GENERIC = 0x01;
            public const ushort HID_USAGE_GENERIC_MOUSE = 0x02;
            public const ushort HID_USAGE_GENERIC_KEYBOARD = 0x06;
        }

        public static class MouseEventFlags
        {
            public const ushort RI_MOUSE_LEFT_BUTTON_DOWN = 0x0001;
            public const ushort RI_MOUSE_LEFT_BUTTON_UP = 0x0002;
            public const ushort RI_MOUSE_RIGHT_BUTTON_DOWN = 0x0004;
            public const ushort RI_MOUSE_RIGHT_BUTTON_UP = 0x0008;
            public const ushort RI_MOUSE_MIDDLE_BUTTON_DOWN = 0x0010;
            public const ushort RI_MOUSE_MIDDLE_BUTTON_UP = 0x0020;
            public const ushort RI_MOUSE_BUTTON_4_DOWN = 0x0040;
            public const ushort RI_MOUSE_BUTTON_4_UP = 0x0080;
            public const ushort RI_MOUSE_BUTTON_5_DOWN = 0x0100;
            public const ushort RI_MOUSE_BUTTON_5_UP = 0x0200;
            public const ushort RI_MOUSE_WHEEL = 0x0400;
        }

        public enum RawInputDeviceType : uint
        {
            RIM_TYPEMOUSE = 0,
            RIM_TYPEKEYBOARD = 1,
            RIM_TYPEHID = 2
        }
    }
}
