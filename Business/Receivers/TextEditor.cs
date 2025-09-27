namespace Command1.Business.Receivers;

public class TextEditor
{
    public string Content { get; private set; } = string.Empty;

    public void Write(string text)
    {
        Content += text;
    }

    public string DeleteLastNCharacters(int count)
    {
        if(count <= 0 || count > Content.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(count), "Invalid number of characters to delete.");
        }
        Content = Content.Remove(Content.Length - count, count); //Content = Content.Substring(0, Content.Length - count);
        return Content;
    }

}
