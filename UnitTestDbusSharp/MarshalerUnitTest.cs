using DBusSharp;

namespace UnitTestDbusSharp;

public class MarshalerUnitTest
{
    
    [Theory]
    [InlineData(Byte.MaxValue, MessageEndianess.LittleEndian, "FF")]
    [InlineData(Byte.MinValue, MessageEndianess.LittleEndian, "00")]
    [InlineData(Byte.MaxValue, MessageEndianess.BigEndian, "FF")]
    [InlineData(Byte.MinValue, MessageEndianess.BigEndian, "00")]
    public void TestMarshalByte(Byte value, MessageEndianess endianess, string expected)
    {
        DBusMarshaler marshaler = new DBusMarshaler();
        WireFormatObject wireFormatObject = marshaler.MarshalObject(value, endianess);
        Assert.Equal(expected, wireFormatObject.GetHexRep());
        Assert.Equal('y', wireFormatObject.typeCode);
    }
    
    [Theory]
    [InlineData(false, MessageEndianess.LittleEndian, "00")]
    [InlineData(true, MessageEndianess.LittleEndian, "01")]
    [InlineData(false, MessageEndianess.BigEndian, "00")]
    [InlineData(true, MessageEndianess.BigEndian, "01")]
    public void TestMarshalBoolean(Boolean value, MessageEndianess endianess, string expected)
    {
        DBusMarshaler marshaler = new DBusMarshaler();
        // bool value = true;
        WireFormatObject wireFormatObject = marshaler.MarshalObject(value, endianess);
        Assert.Equal(expected, wireFormatObject.GetHexRep());
        Assert.Equal('b', wireFormatObject.typeCode);
    }
   
    
    
    [Theory]
    [InlineData(Int16.MaxValue, MessageEndianess.LittleEndian, "FF-7F")]
    [InlineData(Int16.MinValue, MessageEndianess.LittleEndian, "00-80")]
    [InlineData(Int16.MaxValue, MessageEndianess.BigEndian, "7F-FF")]
    [InlineData(Int16.MinValue, MessageEndianess.BigEndian, "80-00")]
    public void TestMarshalInt16(Int16 value, MessageEndianess endianess, string expected)
    {
        DBusMarshaler marshaler = new DBusMarshaler();
        WireFormatObject wireFormatObject = marshaler.MarshalObject(value, endianess);
        Assert.Equal(expected, wireFormatObject.GetHexRep());
        Assert.Equal('n', wireFormatObject.typeCode);
    }
    
    
    [Theory]
    [InlineData(UInt16.MaxValue - 1, MessageEndianess.LittleEndian, "FE-FF")]
    [InlineData(UInt16.MinValue + 1, MessageEndianess.LittleEndian, "01-00")]
    [InlineData(UInt16.MaxValue - 1, MessageEndianess.BigEndian, "FF-FE")]
    [InlineData(UInt16.MinValue + 1, MessageEndianess.BigEndian, "00-01")]
    public void TestMarshalUInt16(UInt16 value, MessageEndianess endianess, string expected)
    {
        DBusMarshaler marshaler = new DBusMarshaler();
        WireFormatObject wireFormatObject = marshaler.MarshalObject(value, endianess);
        Assert.Equal(expected, wireFormatObject.GetHexRep());
        Assert.Equal('q', wireFormatObject.typeCode);
    }
    
    [Theory]
    [InlineData(Int32.MaxValue - 1, MessageEndianess.LittleEndian, "FE-FF-FF-7F")]
    [InlineData(Int32.MinValue + 1, MessageEndianess.LittleEndian, "01-00-00-80")]
    [InlineData(Int32.MaxValue - 1, MessageEndianess.BigEndian, "7F-FF-FF-FE")]
    [InlineData(Int32.MinValue + 1, MessageEndianess.BigEndian, "80-00-00-01")]
    public void TestMarshalInt32(Int32 value, MessageEndianess endianess, string expected)
    {
        DBusMarshaler marshaler = new DBusMarshaler();
        WireFormatObject wireFormatObject = marshaler.MarshalObject(value, endianess);
        Assert.Equal(expected, wireFormatObject.GetHexRep());
        Assert.Equal('i', wireFormatObject.typeCode);
    }
    
    [Theory]
    [InlineData(UInt32.MaxValue - 1, MessageEndianess.LittleEndian, "FE-FF-FF-FF")]
    [InlineData(UInt32.MinValue + 1, MessageEndianess.LittleEndian, "01-00-00-00")]
    [InlineData(UInt32.MaxValue - 1, MessageEndianess.BigEndian, "FF-FF-FF-FE")]
    [InlineData(UInt32.MinValue + 1, MessageEndianess.BigEndian, "00-00-00-01")]
    public void TestMarshalUInt32(UInt32 value, MessageEndianess endianess, string expected)
    {
        DBusMarshaler marshaler = new DBusMarshaler();
        WireFormatObject wireFormatObject = marshaler.MarshalObject(value, endianess);
        Assert.Equal(expected, wireFormatObject.GetHexRep());
        Assert.Equal('u', wireFormatObject.typeCode);
    }
    
    [Theory]
    [InlineData(Int64.MaxValue - 1, MessageEndianess.LittleEndian, "FE-FF-FF-FF-FF-FF-FF-7F")]
    [InlineData(Int64.MinValue + 1, MessageEndianess.LittleEndian, "01-00-00-00-00-00-00-80")]
    [InlineData(Int64.MaxValue - 1, MessageEndianess.BigEndian,    "7F-FF-FF-FF-FF-FF-FF-FE")]
    [InlineData(Int64.MinValue + 1, MessageEndianess.BigEndian,    "80-00-00-00-00-00-00-01")]
    public void TestMarshalInt64(Int64 value, MessageEndianess endianess, string expected)
    {
        DBusMarshaler marshaler = new DBusMarshaler();
        WireFormatObject wireFormatObject = marshaler.MarshalObject(value, endianess);
        Assert.Equal(expected, wireFormatObject.GetHexRep());
        Assert.Equal('x', wireFormatObject.typeCode);
    }
    
    [Theory]
    [InlineData(UInt64.MaxValue - 1, MessageEndianess.LittleEndian, "FE-FF-FF-FF-FF-FF-FF-FF")]
    [InlineData(UInt64.MinValue + 1, MessageEndianess.LittleEndian, "01-00-00-00-00-00-00-00")]
    [InlineData(UInt64.MaxValue - 1, MessageEndianess.BigEndian,    "FF-FF-FF-FF-FF-FF-FF-FE")]
    [InlineData(UInt64.MinValue + 1, MessageEndianess.BigEndian,    "00-00-00-00-00-00-00-01")]
    public void TestMarshalUInt64(UInt64 value, MessageEndianess endianess, string expected)
    {
        DBusMarshaler marshaler = new DBusMarshaler();
        WireFormatObject wireFormatObject = marshaler.MarshalObject(value, endianess);
        Assert.Equal(expected, wireFormatObject.GetHexRep());
        Assert.Equal('t', wireFormatObject.typeCode);
    }
    
    [Theory]
    [InlineData(Double.MaxValue - 1, MessageEndianess.LittleEndian, "FF-FF-FF-FF-FF-FF-EF-7F")]
    [InlineData(Double.MinValue + 1, MessageEndianess.LittleEndian, "FF-FF-FF-FF-FF-FF-EF-FF")]
    [InlineData(Double.MaxValue - 1, MessageEndianess.BigEndian,    "7F-EF-FF-FF-FF-FF-FF-FF")]
    [InlineData(Double.MinValue + 1, MessageEndianess.BigEndian,    "FF-EF-FF-FF-FF-FF-FF-FF")]
    public void TestMarshalDouble(Double value, MessageEndianess endianess, string expected)
    {
        DBusMarshaler marshaler = new DBusMarshaler();
        WireFormatObject wireFormatObject = marshaler.MarshalObject(value, endianess);
        Assert.Equal(expected, wireFormatObject.GetHexRep());
        Assert.Equal('d', wireFormatObject.typeCode);
    }
    
    [Theory]
    [InlineData(UInt32.MaxValue - 1, MessageEndianess.LittleEndian, "FE-FF-FF-FF")]
    [InlineData(UInt32.MinValue + 1, MessageEndianess.LittleEndian, "01-00-00-00")]
    [InlineData(UInt32.MaxValue - 1, MessageEndianess.BigEndian, "FF-FF-FF-FE")]
    [InlineData(UInt32.MinValue + 1, MessageEndianess.BigEndian, "00-00-00-01")]
    public void TestMarshalUnixFd(UInt32 value, MessageEndianess endianess, string expected)
    {
        DBusMarshaler marshaler = new DBusMarshaler();
        UnixFd unixFd = new UnixFd();
        unixFd.FileHandle = value;
        
        WireFormatObject wireFormatObject = marshaler.MarshalObject(unixFd, endianess);
        Assert.Equal(expected, wireFormatObject.GetHexRep());
        Assert.Equal('h', wireFormatObject.typeCode);
    }
    
    [Theory]
    [InlineData("a", MessageEndianess.LittleEndian, "61-00")]
    [InlineData("b", MessageEndianess.LittleEndian, "62-00")]
    [InlineData("a", MessageEndianess.BigEndian, "00-61")]
    [InlineData("b", MessageEndianess.BigEndian, "00-62")]
    [InlineData("ab", MessageEndianess.BigEndian, "00-62-61")]
    [InlineData("ab", MessageEndianess.LittleEndian, "61-62-00")]
    public void TestMarshalString(string value, MessageEndianess endianess, string expected)
    {
        DBusMarshaler marshaler = new DBusMarshaler();
        
        WireFormatObject wireFormatObject = marshaler.MarshalObject(value, endianess);
        Assert.Equal(expected, wireFormatObject.GetHexRep());
        Assert.Equal('s', wireFormatObject.typeCode);
        switch (endianess)
        {
            case MessageEndianess.BigEndian:
                Assert.StartsWith("00-", wireFormatObject.GetHexRep());
                break;
            case MessageEndianess.LittleEndian:
                Assert.EndsWith("-00", wireFormatObject.GetHexRep());
                break;
        }
    }

    [Theory]
    [InlineData("/abc", MessageEndianess.LittleEndian, "2F-61-62-63-00")]
    [InlineData("/abc", MessageEndianess.BigEndian, "00-63-62-61-2F")]
    public void TestMarshalObjectPath(string value, MessageEndianess endianess, string expected)
    {
        DBusObjectPath objectPath = new DBusObjectPath(value);
        DBusMarshaler marshaler = new DBusMarshaler();
        
        WireFormatObject wireFormatObject = marshaler.MarshalObject(objectPath, endianess);
        Assert.Equal(expected, wireFormatObject.GetHexRep());
        Assert.Equal('o', wireFormatObject.typeCode);
    }
}