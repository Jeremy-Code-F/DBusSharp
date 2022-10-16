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
   
   private byte[] WireMarshalSignature(string strToMarshal, MessageEndianess endianess, int byteAlignment=1)
   {
      List<byte> strBytes = new List<byte>();
          
      byte[] bytes = Encoding.UTF8.GetBytes(strToMarshal);
      byte bytesLen = (byte)bytes.Length;
      strBytes.Add(bytesLen);

      foreach (byte strByte in bytes)
      {
         strBytes.Add(strByte);
      }
      strBytes.Add((byte)'\0');

      int remainingAlignmentBytes = byteAlignment - (strBytes.Count % byteAlignment) ;
      if (remainingAlignmentBytes < byteAlignment)
      {
         for (int i = 0; i < remainingAlignmentBytes; i++)
         {
            strBytes.Add((byte)'\0'); 
         }
      }

      byte[] byteArr = strBytes.ToArray();
      FixEndianess(byteArr, endianess);
      return byteArr;
   }

   private byte[] WireMarshalString(string strToMarshal, MessageEndianess endianess, int byteAlignment=4)
   {
      List<byte> strBytes = new List<byte>();
          
      byte[] bytes = Encoding.UTF8.GetBytes(strToMarshal);
      UInt32 bytesLen = (UInt32)bytes.Length;
      byte[] strLenBytes = BitConverter.GetBytes(bytesLen);
      foreach (byte byteLenByte in strLenBytes)
      {
         strBytes.Add(byteLenByte);
      }

      foreach (byte strByte in bytes)
      {
         strBytes.Add(strByte);
      }
      strBytes.Add((byte)'\0');

      int remainingAlignmentBytes = byteAlignment - (strBytes.Count % byteAlignment) ;
      if (remainingAlignmentBytes < byteAlignment)
      {
         for (int i = 0; i < remainingAlignmentBytes; i++)
         {
            strBytes.Add((byte)'\0'); 
         }
      }

      byte[] byteArr = strBytes.ToArray();
      FixEndianess(byteArr, endianess);
      return byteArr;
   }
   
   public WireFormatObject  MarshalObject(object objToMarshal, MessageEndianess endianess)
   {
       WireFormatObject wireFormatObject = new WireFormatObject();
       if (objToMarshal is byte objByte)
       {
          wireFormatObject.dataBytes.Add(objByte);
          wireFormatObject.typeCode = 'y';
       }
         
       // BOOLEAN values are encoded in 32 bits (of which only the least significant bit is used). 
       if (objToMarshal is Boolean booleanObj)
       {
          Int32 boolInt = 0; 
          if (booleanObj)
          {
             boolInt = 1;
          }
          byte[] bytes = BitConverter.GetBytes(boolInt);
          FixEndianess(bytes, endianess);
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
          byte[] byteArr = this.WireMarshalString(strObj, endianess);
          foreach (byte strByte in byteArr)
          {
             wireFormatObject.dataBytes.Add(strByte);
          }
          
          wireFormatObject.typeCode = 's';
       }

       if (objToMarshal is DBusObjectPath objPath)
       {
          byte[] bytes = this.WireMarshalString(objPath.ObjectPath, endianess);
          
          foreach (byte strByte in bytes)
          {
             wireFormatObject.dataBytes.Add(strByte);
          }
          
          wireFormatObject.typeCode = 'o';
          
       }

       if (objToMarshal is DBusSignature signatureObj)
       {
          byte[] bytes = this.WireMarshalSignature(signatureObj.SignatureStr, endianess);
          foreach (byte strByte in bytes)
          {
             wireFormatObject.dataBytes.Add(strByte);
          }
          
          wireFormatObject.typeCode = 'g';
       }

       if (objToMarshal.GetType().IsArray)
       {
          
       }

       // if (objToMarshal is Int32[] int32ArrObj)
       // {
       //    List<byte> dataBytes = new List<byte>();
       //    UInt32 arrDataLen = (UInt32)int32ArrObj.Length;
       //    byte[] dataLenBytes = BitConverter.GetBytes(arrDataLen);
       //    foreach (byte dataLenByte in dataLenBytes)
       //    {
       //       dataBytes.Add(dataLenByte);
       //    }
       //    
       //    // alignment padding
       //      int remainingAlignmentBytes = 4- (wireFormatObject.dataBytes.Count % 4) ;
       //      if (remainingAlignmentBytes < 4)
       //      {
       //         for (int i = 0; i < remainingAlignmentBytes; i++)
       //         {
       //            dataBytes.Add((byte)'\0'); 
       //         }
       //      }
       //
       //      foreach (Int32 i32 in int32ArrObj)
       //      {
       //         WireFormatObject subObj = this.MarshalObject(i32, endianess);
       //         foreach (byte subObjByte in subObj.dataBytes)
       //         {
       //            dataBytes.Add(subObjByte);
       //         }
       //      }
       //
       //      byte[] dataBytesArr = dataBytes.ToArray();
       //      this.FixEndianess(dataBytesArr, endianess);
       //      
       //      foreach (byte dataByte in dataBytesArr)
       //      {
       //         wireFormatObject.dataBytes.Add(dataByte);
       //      }
       //    wireFormatObject.typeCode = 'a';
       // }
       
       return wireFormatObject;
   }
   
}