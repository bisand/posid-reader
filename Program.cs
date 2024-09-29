using System;
using System.Runtime.InteropServices;
using posid_reader;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

static PosInfoRec ReadPosInfoRecFromFile(string filePath)
{
    using var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite | FileShare.Delete);
    byte[] fileBytes = new byte[fs.Length];
    fs.Read(fileBytes, 0, fileBytes.Length);

    GCHandle handle = GCHandle.Alloc(fileBytes, GCHandleType.Pinned);
    try
    {
        IntPtr ptr = handle.AddrOfPinnedObject();
        return (PosInfoRec)Marshal.PtrToStructure(ptr, typeof(PosInfoRec))!;
    }
    finally
    {
        handle.Free();
    }
}

static PosInfoRecTest ReadPosInfoRecFromFileTest(string filePath)
{
    // Read form a file stream
    using var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite | FileShare.Delete);
    using var br = new BinaryReader(fs);
    var posInfo = new PosInfoRecTest
    {
        PosNr = br.ReadInt32(),
        TransNo = br.ReadUInt32(),
        IpAdr = new string(br.ReadChars(51)).TrimEnd('\0'),
        DbName = new string(br.ReadChars(31)).TrimEnd('\0'),
        GUIID = new string(br.ReadChars(41)).TrimEnd('\0'),
        Buff1 = new string(br.ReadChars(125)).TrimEnd('\0')
    };
    return posInfo;
}

string filePath = "VBDSTAT.RAM";
PosInfoRec posInfo = ReadPosInfoRecFromFile(filePath);
Console.WriteLine($"PosNr: {posInfo.PosNr}, TransNo: {posInfo.TransNo}, IpAdr: {posInfo.IpAdr}, DbName: {posInfo.DbName}, GUIID: {posInfo.GUIID}, Buff1: {posInfo.Buff1}");

PosInfoRecTest posInfoTest = ReadPosInfoRecFromFileTest(filePath);
Console.WriteLine($"PosNr: {posInfoTest.PosNr}, TransNo: {posInfoTest.TransNo}, IpAdr: {posInfoTest.IpAdr}, DbName: {posInfoTest.DbName}, GUIID: {posInfoTest.GUIID}, Buff1: {posInfoTest.Buff1}");

var _messageCount = 123040.234523;
var elapsed = TimeSpan.FromSeconds(123456.23423);
var totalElapsed = $"{(int)elapsed.TotalHours:D2}:{elapsed.Minutes:D2}:{elapsed.Seconds:D2}";
var cultureInfo = new System.Globalization.CultureInfo("nb-NO");
Console.WriteLine($"Processed {_messageCount.ToString("N0", cultureInfo)} messages in {totalElapsed}. Messages per second: {(_messageCount / elapsed.TotalSeconds).ToString("N2", cultureInfo)}");

Console.WriteLine($"Date: {DateTime.Now:yyyy-MM-dd HH:mm:ss} - Utc: {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}");
