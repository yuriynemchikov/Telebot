public struct ModelMessage
{
    public string userId;
    public string userFirstName;
    public string userMessageText;
    public string userUpdateId;
    public string messageId;

    public int tag;

    public override string ToString()
    {
        return $"{userFirstName} {userMessageText}";
    }
}

