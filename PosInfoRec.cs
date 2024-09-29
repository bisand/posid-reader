using System.Runtime.InteropServices;

namespace posid_reader;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct PosInfoRec
{
    public int PosNr;
    public uint TransNo;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 51)]
    public string IpAdr;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
    public string DbName;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
    public string GUIID;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 125)]
    public string Buff1;
}

public struct PosInfoRecTest
{
    public int PosNr;
    public uint TransNo;
    public string IpAdr;
    public string DbName;
    public string GUIID;
    public string Buff1;
}
