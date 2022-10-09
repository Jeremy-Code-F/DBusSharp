namespace DBusSharp;

public enum FieldCodeEnum{
   Invalid = 0,
   Path = 1,
   Interface = 2,
   Member = 3,
   ErrorName = 4,
   ReplySerial = 5,
   Destination = 6,
   Sender = 7,
   Signature = 8,
   UnixFds = 9
}
public struct MessageHeaderField
{
   public FieldCodeEnum FieldCode { get; }
   public object FieldValue { get; }

   public MessageHeaderField(FieldCodeEnum fieldCode, object fieldValue)
   {
      this.FieldCode = fieldCode;
      this.FieldValue = fieldValue;
   }
}

public class MessageHeader
{
   //Endianness flag; ASCII 'l' for little-endian or ASCII 'B' for big-endian. Both header and body are in this endianness.
   // public Byte Endianness { get; set; }
   public MessageEndianess Endianness { get; set; }
   public MessageType MessageType { get; set; }
   public MessageFlag Flags { get; set; } // Bitwise OR of flags
   public Byte ProtocolVersion { get; set; }
   public UInt32 BodyLength { get; set; }
   
   // The serial of this message, used as a cookie by the sender to identify the reply corresponding to this request. This must not be zero. 
   public UInt32 Serial { get; set; }

   public List<MessageHeaderField> HeaderFields { get; set; } = new List<MessageHeaderField>();
   // Array of Struct of (Byte, Variant)
   // 	An array of zero or more header fields where the byte is the field code, and the variant is the field value. The message type determines which fields are required. 

   public MessageHeader ParseHeaderFromBuffer(Byte[] buffer)
   {
      MessageHeader header = new MessageHeader();
      if (0 == MessageEndianess.LittleEndian.CompareTo(buffer[0]))
      {
         header.Endianness = MessageEndianess.LittleEndian;
      }
      else
      {
         header.Endianness = MessageEndianess.BigEndian;
      }

      return header;
   }

   public MessageHeader()
   {
   }

   public MessageHeader(MessageType messageType)
   {
      this.Endianness = MessageEndianess.LittleEndian;
      this.MessageType = messageType;
      this.Flags = 0;
      this.ProtocolVersion = 1;
      this.BodyLength = 0;
      this.Serial = MessageGenerator.GetNextSerial();
   }

}

public enum MessageEndianess
{
   LittleEndian = 108,
   BigEndian = 66 
}

public enum MessageType
{
   Invalid = 0,
   MethodCall = 1,
   MethodReturn = 2,
   Error = 3,
   Signal = 4
}

public enum MessageFlag
{
   // This message does not expect method return replies or error replies, even if it is of a type that can have
   // a reply; the reply should be omitted. 
   NoReplyExpected = 0x1,
   
   // The bus must not launch an owner for the destination name in response to this message. 
   NoAutoStart = 0x2,
   
   //This flag may be set on a method call message to inform the receiving side that the caller is prepared to wait
   //for interactive authorization, which might take a considerable time to complete. For instance, if this flag is set,
   //it would be appropriate to query the user for passwords or confirmation via Polkit or a similar framework. 
   AllowInteractiveAuthorization = 0x4
}

public class MessageBody
{
    
}

public class DBusMessage
{
   private MessageHeader _header;
   private MessageBody _body;

   public DBusMessage(MessageHeader header, MessageBody body)
   {
      this._header = header;
      this._body = body;
   }

   public uint GetSerial()
   {
      return _header.Serial;
   }

   public DBusMessage (byte[] data)
   {
      _header = new MessageHeader();
      _body = new MessageBody();
      using (MemoryStream m = new MemoryStream())
      {
         using (BinaryReader reader = new BinaryReader(m))
         {
            _header.Endianness = (MessageEndianess)reader.ReadByte();
            _header.MessageType = (MessageType)reader.ReadByte();
            _header.Flags = (MessageFlag)reader.ReadByte();
            _header.ProtocolVersion = reader.ReadByte();
            _header.BodyLength = reader.ReadUInt32();
            _header.Serial = reader.ReadUInt32();
         }
      }
   }

   public byte[] Serialize()
   {
      using (MemoryStream stream = new MemoryStream())
      {
         using (BinaryWriter writer = new BinaryWriter(stream))
         {
            writer.Write((byte)_header.Endianness);
            writer.Write((byte)_header.MessageType);
            writer.Write((byte)_header.Flags);
            writer.Write(_header.ProtocolVersion);
            writer.Write(_header.BodyLength);
            writer.Write(_header.Serial);
            foreach (var headerField in _header.HeaderFields)
            {
               writer.Write((byte)headerField.FieldCode);
               writer.Write((string)headerField.FieldValue);
            }
         }

         return stream.ToArray();
      }
      
   }
}