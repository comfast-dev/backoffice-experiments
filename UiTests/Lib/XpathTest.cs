using NUnit.Framework;
using UiTests.Lib;

namespace UiTests.Lib;

[TestFixture]
[TestOf(typeof(Xpath))]
public class XpathTest {

    [Test]
    public void EscapeTextTest() {
        Assert.AreEqual("concat('\"That', \"'\", 's mine\", he said.')", 
            Xpath.EscapeText("\"That's mine\", he said."));
        
        Assert.AreEqual("'Some text'", 
            Xpath.EscapeText("Some text"));

        Assert.AreEqual("\"I'am\"", 
            Xpath.EscapeText("I'am"));
    }
}