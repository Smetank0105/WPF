#include <windows.h>

#define DLL_EXPORT __declspec(dllexport)

extern "C" {
    DLL_EXPORT DWORD GetIpAddress(HWND hwndControl)
    {
        if (!hwndControl || !IsWindow(hwndControl))
            return 0;

        DWORD ipAddress = 0;
        LRESULT result = SendMessage(hwndControl, 0x500 + 101, 0, (LPARAM)&ipAddress);
        return ipAddress;
    }

    DLL_EXPORT BOOL SetIpAddress(HWND hwndControl, DWORD ipAddress)
    {
        if (!hwndControl || !IsWindow(hwndControl))
            return FALSE;

        LRESULT result = SendMessage(hwndControl, 0x500 + 102, 0, (LPARAM)ipAddress);
        return result != 0;
    }
}
