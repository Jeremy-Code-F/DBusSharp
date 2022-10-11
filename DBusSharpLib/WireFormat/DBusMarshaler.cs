using System.Text;

namespace DBusSharp;

public class WireFormatObject
{
   public List<Byte> dataBytes { get; set; } = new List<byte>();
   public char typeCode { get; set; }

   public string GetHexRep()
   {
      return BitConverter.ToString(this.dataBytes.ToArray());
   }
}

public class DBusMarshaler
{
   private void FixEndianess(byte[] data, MessageEndianess endianess)
   {
       if (BitConverter.IsLittleEndian && endianess == MessageEndianess.BigEndian)
       {
          Array.Reverse(data);
       }
       else if (!BitConverter.IsLittleEndian && endianess == MessageEndianess.LittleEndian)
       {
          Array.Reverse(data);
       }
   }
   public WireFormatObject  MarshalObject(object objToMarshal, MessageEndianess endianess)
   {
       WireFormatObject wireFormatObject = new WireFormatObject();
       if (objToMarshal is byte objByte)
       {
          wireFormatObject.dataBytes.Add(objByte);
          wireFormatObject.typeCode = 'y';
       }

       if (objToMarshal is Boolean booleanObj)
       {
          byte[] bytes = BitConverter.GetBytes(booleanObj);
          foreach (byte intByte in bytes)
          {
             wireFormatObject.dataBytes.Add(intByte);
          }

          wireFormatObject.typeCode = 'b';
       }
       
       if (objToMarshal is Int16 int16Obj)
       {
          byte[] bytes = BitConverter.GetBytes(int16Obj);
          FixEndianess(bytes, endianess);
          foreach (byte intByte in bytes)
          {
             wireFormatObject.dataBytes.Add(intByte);
          }
          
          wireFormatObject.typeCode = 'n';
       }
       
       if (objToMarshal is UInt16 uInt16Obj)
       {
          byte[] bytes = BitConverter.GetBytes(uInt16Obj);
          FixEndianess(bytes, endianess);
          foreach (byte uIntByte in bytes)
          {
             wireFormatObject.dataBytes.Add(uIntByte);
          }
          
          wireFormatObject.typeCode = 'q';
       }
       
       if (objToMarshal is Int32 int32Obj)
       {
          byte[] bytes = BitConverter.GetBytes(int32Obj);
          FixEndianess(bytes, endianess);
          foreach (byte uIntByte in bytes)
          {
             wireFormatObject.dataBytes.Add(uIntByte);
          }
          
          wireFormatObject.typeCode = 'i';
       }

       if (objToMarshal is UInt32 uInt32)
       {
          byte[] bytes = BitConverter.GetBytes(uInt32);
          FixEndianess(bytes, endianess);
          foreach (byte intByte in bytes)
          {
             wireFormatObject.dataBytes.Add(intByte);
          }

          wireFormatObject.typeCode = 'u';
       }
       
       if (objToMarshal is Int64 int64Obj)
       {
          byte[] bytes = BitConverter.GetBytes(int64Obj);
          FixEndianess(bytes, endianess);
          foreach (byte uIntByte in bytes)
          {
             wireFormatObject.dataBytes.Add(uIntByte);
          }
          
          wireFormatObject.typeCode = 'x';
       }
       
       if (objToMarshal is UInt64 uInt64Obj)
       {
          byte[] bytes = BitConverter.GetBytes(uInt64Obj);
          FixEndianess(bytes, endianess);
          foreach (byte uIntByte in bytes)
          {
             wireFormatObject.dataBytes.Add(uIntByte);
          }
          
          wireFormatObject.typeCode = 't';
       }
       
       if (objToMarshal is Double doubleObj)
       {
          byte[] bytes = BitConverter.GetBytes(doubleObj);
          FixEndianess(bytes, endianess);
          foreach (byte uIntByte in bytes)
          {
             wireFormatObject.dataBytes.Add(uIntByte);
          }
          
          wireFormatObject.typeCode = 'd';
       }
       
       if (objToMarshal is UnixFd fdObject)
       {
          byte[] bytes = BitConverter.GetBytes(fdObject.FileHandle);
          FixEndianess(bytes, endianess);
          foreach (byte uIntByte in bytes)
          {
             wireFormatObject.dataBytes.Add(uIntByte);
          }
          
          wireFormatObject.typeCode = 'h';
       }

       if (objToMarshal is String strObj)
       {
          byte[] bytes = Encoding.UTF8.GetBytes(strObj);
          int bytesLen = bytes.Length;
          Array.Resize(ref bytes, bytes.Length + 1);
          bytes[bytesLen] = (byte)'\0';
          FixEndianess(bytes, endianess);
          
          foreach (byte strByte in bytes)
          {
             wireFormatObject.dataBytes.Add(strByte);
          }
          
          wireFormatObject.typeCode = 's';
       }

       if (objToMarshal is DBusObjectPath objPath)
       {
          byte[] bytes = Encoding.UTF8.GetBytes(objPath.ObjectPath);
          int bytesLen = bytes.Length;
          Array.Resize(ref bytes, bytes.Length + 1);
          bytes[bytesLen] = (byte)'\0';
          FixEndianess(bytes, endianess);
          
          foreach (byte strByte in bytes)
          {
             wireFormatObject.dataBytes.Add(strByte);
          }
          
          wireFormatObject.typeCode = 'o';
          
       }
       
       return wireFormatObject;
   }
   
}