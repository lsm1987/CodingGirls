using UnityEngine;
using NUnit.Framework;

public class StringDefineTest
{
    [Test]
    public void SplitFirstWordSingleWordTest()
    {
        string text = "word";
        string word;
        string remain;
        StringDefine.SplitFirstWord(text, out word, out remain);

        Assert.AreEqual("word", word);
        Assert.AreEqual("", remain);
    }

    [Test]
    public void SplitFirstWordMultiWordTest()
    {
        string text = "word text1 text2 text3";
        string word;
        string remain;
        StringDefine.SplitFirstWord(text, out word, out remain);

        Assert.AreEqual("word", word);
        Assert.AreEqual("text1 text2 text3", remain);
    }

    [Test]
    public void ParsingColorTest()
    {
        string text = "(255,0,255)";
        Color color = StringDefine.ParseColor(text);

        Assert.AreEqual(new Color(1.0f, 0.0f, 1.0f), color);
    }

    [Test]
    public void ParsingVector3Test()
    {
        string text = "(0,-1.5,10.0)";
        Vector3 vec = StringDefine.ParseVector3(text);

        Assert.AreEqual(new Vector3(0.0f, -1.5f, 10.0f), vec);
    }

    [Test]
    public void ParsingVector2Test()
    {
        string text = "(0,-1.5)";
        Vector2 vec = StringDefine.ParseVector2(text);

        Assert.AreEqual(new Vector2(0.0f, -1.5f), vec);
    }
}
