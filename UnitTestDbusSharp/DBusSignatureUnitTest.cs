using System.Text;
using DBusSharp;
using DBusSharp.Exceptions;

namespace UnitTestDbusSharp;

public class DBusSignatureUnitTest
{
    [Fact]
    public void TestOverMaxLengthThrows()
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < 300; i++)
        {
            sb.Append('b');
        }
        Assert.Throws<SignatureParsingException>(() => new DBusSignature(sb.ToString()));
    }
    
    [Fact]
    public void TestStructTypeCodeThrows()
    {
        Assert.Throws<SignatureParsingException>(() => new DBusSignature("r"));
    }
    
    [Fact]
    public void TestDictTypeCodeThrows()
    {
        Assert.Throws<SignatureParsingException>(() => new DBusSignature("e"));
    }
    
    [Fact]
    public void TestStructTooManyParenthesisThrows()
    {
        Assert.Throws<SignatureParsingException>(() => new DBusSignature("(ii)))"));
    }
    [Fact]
    public void TestStructTooManyBracketsThrows()
    {
        Assert.Throws<SignatureParsingException>(() => new DBusSignature("(ii)))"));
    }
    [Fact]
    public void TestInvalidSignatureChar()
    {
        Assert.Throws<SignatureParsingException>(() => new DBusSignature("p"));
    }
}