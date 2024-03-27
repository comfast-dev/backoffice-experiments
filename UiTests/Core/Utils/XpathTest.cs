using NUnit.Framework;
using UiTests.Lib;

namespace UiTests.Lib;

[TestFixture]
[TestOf(typeof(Xpath))]
public class XpathTest {

    [Test]
    public void EscapeTextTest() {
        Assert.AreEqual("concat('It', \"'\", 's \"hard\" text to match')", 
            Xpath.EscapeText("It's \"hard\" text to match"));
        
        Assert.AreEqual("'Some text'", 
            Xpath.EscapeText("Some text"));

        Assert.AreEqual("\"I'am\"", 
            Xpath.EscapeText("I'am"));
    }
}