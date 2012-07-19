using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using HtmlAgilityPack;
using System.Text.RegularExpressions;

/// <summary>
/// Summary description for SyndicationFeedsDataObject
/// </summary>
[DataObject(true)]
public class SyndicationFeedsDataObject
{
    public string FeedUrl { get; set; }
    public string Title { get; set; }
    public string MimeType { get; set; }

    [DataObjectMethod(DataObjectMethodType.Select, true)]
    public static IEnumerable<SyndicationFeedsDataObject> Select(string WebsiteUrl)
    {
        // parse the url
        WebsiteUrl = ParseUrl(WebsiteUrl);

        if (!IsValidUrl(WebsiteUrl))
        {
            return null;
        }

        // retrieve the website html
        HtmlDocument doc = RetrieveHtml(WebsiteUrl);

        // extract all tags
        HtmlNodeCollection feeds = SelectFeeds(doc);

        if (!IsValidFeedCollection(feeds))
        {
            return null;
        }

        // convert extracted tags to SyndicationFeedsDataObjects
        var query = ConvertNodesToSyndicationFeeds(feeds);

        // return the data
        return query;
    }

    private static string ParseUrl(string WebsiteUrl)
    {
        if (string.IsNullOrWhiteSpace(WebsiteUrl))
        {
            return string.Empty;
        }

        if (!Regex.IsMatch(WebsiteUrl, "^http://", RegexOptions.IgnoreCase))
        {
            WebsiteUrl = "http://" + WebsiteUrl;
        }

        try
        {
            Uri TestUrl = new Uri(WebsiteUrl);
        }
        catch (UriFormatException)
        {
            WebsiteUrl = string.Empty;
        }

        return WebsiteUrl;
    }

    private static bool IsValidUrl(string WebsiteUrl)
    {
        return !string.IsNullOrWhiteSpace(WebsiteUrl);
    }

    private static HtmlDocument RetrieveHtml(string WebsiteUrl)
    {
        HtmlWeb hw = new HtmlWeb();
        return hw.Load(WebsiteUrl);
    }

    private static HtmlNodeCollection SelectFeeds(HtmlDocument doc)
    {
        return doc.DocumentNode.SelectNodes("//link[(@type='application/rss+xml' or @type='application/atom+xml') and @rel='alternate']");
    }

    private static bool IsValidFeedCollection(HtmlNodeCollection feeds)
    {
        return feeds != null;
    }

    private static IEnumerable<SyndicationFeedsDataObject> ConvertNodesToSyndicationFeeds(HtmlNodeCollection feeds)
    {
        var query = from link in feeds
                    select new SyndicationFeedsDataObject
                    {
                        FeedUrl = link.Attributes["href"].Value,
                        Title = link.Attributes["title"].Value,
                        MimeType = link.Attributes["type"].Value
                    };
        return query;
    }
}