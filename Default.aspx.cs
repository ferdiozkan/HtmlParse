using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using System.Data;
using System.Xml;
using System.IO;
using System.Net;
using System.Text;
using HtmlAgilityPack;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            rss();
        }
    }
    protected void ddListBasliklar_SelectedIndexChanged(object sender, EventArgs e)
    {
        yaziyiGetir(ddListBasliklar.SelectedValue);
    }

    private void yaziyiGetir(string url)
    {
        HtmlWeb hw = new HtmlWeb();
        HtmlDocument doc = hw.Load(url);

        string path = "//div[@class='post']";

        HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes(path);

        string html = "";
        foreach (HtmlNode n in nodes)
        {
            html += n.InnerHtml;
        }

        literalIcerik.Text = html;
    }

    private void rss()
    {
        WebClient wc = new WebClient();

        //Xml Kaynagını  Byte dizisi olarak indirecegim.
        byte[] Html = wc.DownloadData("http://ferdiozkan.com/feed/");
        MemoryStream ms = new MemoryStream(Html);
        XmlReader xreader = XmlReader.Create(ms);
        XDocument doc = XDocument.Load(xreader);
        var sonuc = doc.Descendants("item");

        //Proje Root ıcerısınde yer alan Xml Kaynagı ornek olarak bakabılırsınız.Kaynak ıcerısınde her bır gonderim "item" elementı ıcerısınde gecmektedır.
        foreach (var item in sonuc)
        {
            ListItem li = new ListItem(item.Element("title").Value, item.Element("link").Value);
            ddListBasliklar.Items.Add(li);
        }
    }
}