using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static rawinput.user32;
using static rawinput.consts;
using static rawinput.structs;

namespace rawinput
{
    public class RawInput
    {
        public event Action<Keys, bool> OnKeyEvent;
        public event Action<int, int> OnMouseMove;
        public event Action<MouseButtons, bool> OnMouseButton;

        private IntPtr _targetHwnd;

        public void Initialize(IntPtr hwnd)
        {
            _targetHwnd = hwnd;

            var devices = new RAWINPUTDEVICE[]
            {
            new RAWINPUTDEVICE
            {
                usUsagePage = RawInputConstants.HID_USAGE_PAGE_GENERIC,
                usUsage = RawInputConstants.HID_USAGE_GENERIC_MOUSE,
                dwFlags = RawInputConstants.RIM_INPUT,
                hwndTarget = _targetHwnd
            },
            new RAWINPUTDEVICE
            {
                usUsagePage = RawInputConstants.HID_USAGE_PAGE_GENERIC,
                usUsage = RawInputConstants.HID_USAGE_GENERIC_KEYBOARD,
                dwFlags = RawInputConstants.RIM_INPUT,
                hwndTarget = _targetHwnd
            }
            };

            if (!RegisterRawInputDevices(devices, (uint)devices.Length, (uint)Marshal.SizeOf(typeof(RAWINPUTDEVICE))))
            {
                throw new Exception("Failed to register raw input devices!");
            }
        }

        public void ProcessInputMessage(IntPtr lParam)
        {
            uint dwSize = 0;
            GetRawInputData(lParam, RawInputConstants.RIM_INPUT, IntPtr.Zero, ref dwSize, (uint)Marshal.SizeOf(typeof(RAWINPUTHEADER)));

            IntPtr buffer = Marshal.AllocHGlobal((int)dwSize);

            try
            {
                if (GetRawInputData(lParam, RawInputConstants.RIM_INPUT, buffer, ref dwSize, (uint)Marshal.SizeOf(typeof(RAWINPUTHEADER))) != dwSize)
                    return;

                RAWINPUT raw = Marshal.PtrToStructure<RAWINPUT>(buffer);

                switch ((RawInputDeviceType)raw.header.dwType)
                {
                    case RawInputDeviceType.RIM_TYPEKEYBOARD:
                        HandleKeyboard(raw.keyboard);
                        break;
                    case RawInputDeviceType.RIM_TYPEMOUSE:
                        HandleMouse(raw.mouse);
                        break;
                    case RawInputDeviceType.RIM_TYPEHID:

                        break;
                }
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }
        }

        private void HandleKeyboard(RAWKEYBOARD kb)
        {
            bool isDown = kb.Message == 0x0100 || kb.Message == 0x0104;
            Keys key = (Keys)kb.VKey;
            OnKeyEvent.Invoke(key, isDown);
        }

        private void HandleMouse(RAWMOUSE mouse)
        {
            int dx = mouse.lLastX;
            int dy = mouse.lLastY;

            if (dx != 0 || dy != 0)
                OnMouseMove.Invoke(dx, dy);

            if ((mouse.usButtonFlags & MouseEventFlags.RI_MOUSE_LEFT_BUTTON_DOWN) != 0)
                OnMouseButton.Invoke(MouseButtons.Left, true);

            if ((mouse.usButtonFlags & MouseEventFlags.RI_MOUSE_LEFT_BUTTON_UP) != 0)
                OnMouseButton.Invoke(MouseButtons.Left, false);

            if ((mouse.usButtonFlags & MouseEventFlags.RI_MOUSE_RIGHT_BUTTON_DOWN) != 0)
                OnMouseButton.Invoke(MouseButtons.Right, true);

            if ((mouse.usButtonFlags & MouseEventFlags.RI_MOUSE_RIGHT_BUTTON_UP) != 0)
                OnMouseButton.Invoke(MouseButtons.Right, false);

            if ((mouse.usButtonFlags & MouseEventFlags.RI_MOUSE_MIDDLE_BUTTON_DOWN) != 0)
                OnMouseButton.Invoke(MouseButtons.Middle, true);

            if ((mouse.usButtonFlags & MouseEventFlags.RI_MOUSE_MIDDLE_BUTTON_UP) != 0)
                OnMouseButton.Invoke(MouseButtons.Middle, false);
        }
    }
}
