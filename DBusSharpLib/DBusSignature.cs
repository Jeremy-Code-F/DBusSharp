using DBusSharp.Exceptions;

namespace DBusSharp;

public class DBusSignature
{
   public string SignatureStr;
   public DBusSignature(string signature)
   {
       int openParenthesisCount = 0;
       int closeParenthesisCount = 0;
       int openBracketCount = 0;
       int closeBracketCount = 0;
       
       if (signature.Length > 255)
       {
           throw new SignatureParsingException("Signatures must be < 255 characters in length");
       }
       if (signature.Contains('r'))
       {
           throw new SignatureParsingException("Type code for structs are not allowed use parenthesis instead ()");
       }
       if (signature.Contains('e'))
       {
           throw new SignatureParsingException("Type code for dicts are not allowed use parenthesis instead ()");
       }

       foreach (char c in signature)
       {
           if (c == '{')
           {
               openBracketCount += 1;
           }else if (c == '}')
           {
               closeBracketCount += 1;
           }else if (c == '(')
           {
               openParenthesisCount += 1;
           }else if (c == ')')
           {
               closeParenthesisCount += 1;
           }
       }

       if (openBracketCount != closeBracketCount)
       {
           throw new SignatureParsingException(
               "Must have matching close brackets for opening brackets for dictionary types");
       }

       if (openParenthesisCount != closeParenthesisCount)
       {
           throw new SignatureParsingException(
               "Must have matching closing parenthesis for opening parenthesis for struct types");
       }

       this.SignatureStr = signature;
   } 
}