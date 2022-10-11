using DBusSharp.Exceptions;

namespace DBusSharp;

public class DBusObjectPath
{
    public string ObjectPath { get; }
   public DBusObjectPath(string objectPath)
   {
       // Validation
       if (!objectPath.StartsWith('/'))
       {
           throw new ObjectPathParsingException("Object path must start with a '/' character");
       }

       if (objectPath.EndsWith('/') && objectPath.Length != 1)
       {
           throw new ObjectPathParsingException("Object path cannot end with a slash unless it's the root '/'");
       }

       if (objectPath.EndsWith('/'))
       {
           ObjectPath = objectPath;
           return;
       }
       
       
       string[] pathParts = objectPath.Split('/');
       // Note(Jeremy): Skip the first pathPart because if it starts with / the first
       // path part will be an empty string ""
       foreach (string pathPart in pathParts.Skip(1))
       {
           if (pathPart.Length == 0)
           {
               throw new ObjectPathParsingException("Path parts may not be an empty string");
           }
          // Each element must only contain the ASCII characters "[A-Z][a-z][0-9]_"  
          foreach (char character in pathPart)
          {
              // within 65 - 90 or 97 - 122 or 48 - 57 or 95
              if ((character >= 65 && character <= 90) || (character >= 97 && character <= 122) ||
                  (character >= 48 && character <= 57) || character == 95)
              {
                // nothing  
              }
              else
              {
                  throw new ObjectPathParsingException($"Character {character} is not a valid object path character must be one of [A-Z][a-z][0-9]_");
              }
          }
       }
       
       ObjectPath = objectPath;
   } 
}