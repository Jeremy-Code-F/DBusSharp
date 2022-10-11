using DBusSharp;
using DBusSharp.Exceptions;

namespace UnitTestDbusSharp;

public class DBusObjectPathUnitTest
{
    [Fact]
    public void TestStartsWithSlash()
    {
        // assert no exception thrown
        DBusObjectPath objectPath = new DBusObjectPath("/");
    }
    
    [Fact]
    public void TestPathNotStartingWithSlash()
    {
        Assert.Throws<ObjectPathParsingException>(() => new DBusObjectPath("somethingbefore/"));
    }
    
    [Fact]
    public void TestParsePathWithValidCharacters()
    {
        // assert no exception thrown
        _ = new DBusObjectPath("/somepath");
    }
    [Fact]
    public void TestParsePathWithValidCharactersMult()
    {
        // assert no exception thrown
        _ = new DBusObjectPath("/somepath/Test/Other/Boy");
    }
    [Fact]
    public void TestParsePathWithInvalidCharacters()
    {
        Assert.Throws<ObjectPathParsingException>(() => new DBusObjectPath("/{}"));
    }
    [Fact]
    public void TestPathPartEmpty()
    {
        Assert.Throws<ObjectPathParsingException>(() => new DBusObjectPath("/some/emptypart//"));
    }
    [Fact]
    public void TestPathTrailingSlashNonRoot()
    {
        Assert.Throws<ObjectPathParsingException>(() => new DBusObjectPath("/some/"));
    }
}