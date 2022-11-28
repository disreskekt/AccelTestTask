namespace AccelTestTask.Helpers;

public static class LinkHelper
{
    public static string RetrieveToken(this string link)
    {
        int lastIndexOfSlash = link.LastIndexOf('/');
        return link.Substring(lastIndexOfSlash + 1);
    }
}