﻿using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Localization.Sources;
using Microsoft.AspNetCore.Mvc.Rendering;
using Zero.Extensions;

namespace DPS.Park.Core.Shared
{
    public static class ParkHelper
    {
        #region Page Layout

        public static string PageLayout(int status, ILocalizationSource lang)
        {
            var salaryComponentTypeScope = (ParkEnums.PageLayout)status;
            return lang.GetString(salaryComponentTypeScope.GetStringValue());
        }

        public static List<SelectListItem> ListPageLayout(int currentLayout, ILocalizationSource lang)
        {
            return (from status in (ParkEnums.PageLayout[])Enum.GetValues(typeof(ParkEnums.PageLayout))
                select new SelectListItem(lang.GetString(status.GetStringValue()), ((int)status).ToString(), currentLayout == (int)status)).ToList();
        }

        #endregion

        #region Widget Content Type

        public static string WidgetContentType(int status, ILocalizationSource lang)
        {
            var salaryComponentTypeScope = (ParkEnums.WidgetContentType)status;
            return lang.GetString(salaryComponentTypeScope.GetStringValue());
        }

        public static List<SelectListItem> ListWidgetContentType(int currentLayout, ILocalizationSource lang)
        {
            return (from status in (ParkEnums.WidgetContentType[])Enum.GetValues(typeof(ParkEnums.WidgetContentType))
                select new SelectListItem(lang.GetString(status.GetStringValue()), ((int)status).ToString(), currentLayout == (int)status)).ToList();
        }

        #endregion

        #region Park

        public static List<SelectListItem> ListHistoryType(int currentHistoryType, ILocalizationSource lang)
        {
            return (from historyType in (ParkEnums.HistoryType[]) Enum.GetValues(typeof(ParkEnums.HistoryType))
                select new SelectListItem(lang.GetString(historyType.GetStringValue()),
                    ((int) historyType).ToString(), currentHistoryType == (int) historyType)).ToList();
        }
        
        public static List<SelectListItem> ListOrderStatus(int currentOrderStatus, ILocalizationSource lang)
        {
            return (from orderStatus in (ParkEnums.OrderStatus[]) Enum.GetValues(typeof(ParkEnums.OrderStatus))
                select new SelectListItem(lang.GetString(orderStatus.GetStringValue()),
                    ((int) orderStatus).ToString(), currentOrderStatus == (int) orderStatus)).ToList();
        }
        
        public static List<SelectListItem> ListFareType(int currentFareType, ILocalizationSource lang)
        {
            return (from fareType in (ParkEnums.FareType[]) Enum.GetValues(typeof(ParkEnums.FareType))
                select new SelectListItem(lang.GetString(fareType.GetStringValue()),
                    ((int) fareType).ToString(), currentFareType == (int) fareType)).ToList();
        }

        #endregion
    }

    public class SocialMediaHelper
    {
        public static readonly int SOCIALMEDIASERVICES = 35;
        public static readonly int URLSIZE = 1024;
        public static readonly int ARGLENGTH = 1024;

        public string title;
        public string url;
        public string image;
        public string desc;
        public string appid;
        public string redirecturl;
        public string via;
        public string hash_tags;
        public string provider;
        public string language;
        public string user_id;
        public string category;
        public string phone_number;
        public string email_address;
        public string cc_email_address;
        public string bcc_email_address;

        public string[] urls = new string[SOCIALMEDIASERVICES];

        public string[] servicesSortedByPopularity =
        {
            "google.bookmarks",
            "facebook",
            "reddit",
            "whatsapp",
            "twitter",
            "linkedin",
            "tumblr",
            "pinterest",
            "blogger",
            "livejournal",
            "evernote",
            "add.this",
            "getpocket",
            "hacker.news",
            "buffer",
            "flipboard",
            "instapaper",
            "surfingbird.ru",
            "flattr",
            "diaspora",
            "qzone",
            "vk",
            "weibo",
            "ok.ru",
            "douban",
            "xing",
            "renren",
            "threema",
            "sms",
            "line.me",
            "skype",
            "telegram.me",
            "email",
            "gmail",
            "yahoo",
        };

        public SocialMediaHelper()
        {
        }

        public SocialMediaHelper(string argTitle, string argUrl)
        {
            title = argTitle;
            url = argUrl;
            buildUrls();
        }
        
        public SocialMediaHelper(
            string argTitle,
            string argUrl,
            string argImage,
            string argDesc,
            string argAppid,
            string argRedirecturl,
            string argVia,
            string argHash_tags,
            string argProvider,
            string argLanguage,
            string argUser_id,
            string argCategory,
            string argPhone_number,
            string argEmail_address,
            string argCc_email_address,
            string argBcc_email_address
        )
        {
            title = argTitle;
            url = argUrl;
            image = argImage;
            desc = argDesc;
            appid = argAppid;
            redirecturl = argRedirecturl;
            via = argVia;
            hash_tags = argHash_tags;
            provider = argProvider;
            language = argLanguage;
            user_id = argUser_id;
            category = argCategory;
            phone_number = argPhone_number;
            email_address = argEmail_address;
            cc_email_address = argCc_email_address;
            bcc_email_address = argBcc_email_address;

            buildUrls();
        }

        public void buildUrls()
        {
            string text = title;

            if (desc != "")
            {
                text += "%20%3A%20";
                text += desc;
            }

            string addthis = "http://www.addthis.com/bookmark.php?url=" + url;
            string blogger = "https://www.blogger.com/blog-this.g?u=" + url + "&n=" + title + "&t=" + desc;
            string buffer = "https://buffer.com/add?text=" + text + "&url=" + url;
            string diaspora = "https://share.diasporafoundation.org/?title=" + title + "&url=" + url;
            string douban = "http://www.douban.com/recommend/?url=" + url + "&title=" + title;
            string email = "mailto:" + email_address + "?subject=" + title + "&body=" + desc;
            string evernote = "https://www.evernote.com/clip.action?url=" + url + "&title=" + text;
            string getpocket = "https://getpocket.com/edit?url=" + url;
            string facebook = "http://www.facebook.com/sharer.php?u=" + url;
            string flattr = "https://flattr.com/submit/auto?user_id=" + user_id + "&url=" + url + "&title=" + title + "&description=" + text + "&language=" + language + "&tags=" + hash_tags +
                            "&hidden=HIDDEN&category=" + category;
            string flipboard = "https://share.flipboard.com/bookmarklet/popout?v=2&title=" + text + "&url=" + url;
            string gmail = "https://mail.google.com/mail/?view=cm&to=" + email_address + "&su=" + title + "&body=" + url + "&bcc=" + bcc_email_address + "&cc=" + cc_email_address;
            string googlebookmarks = "https://www.google.com/bookmarks/mark?op=edit&bkmk=" + url + "&title=" + title + "&annotation=" + text + "&labels=" + hash_tags;
            string instapaper = "http://www.instapaper.com/edit?url=" + url + "&title=" + title + "&description=" + desc;
            string lineme = "https://lineit.line.me/share/ui?url=" + url + "&text=" + text;
            string linkedin = "https://www.linkedin.com/sharing/share-offsite/?url=" + url;
            string livejournal = "http://www.livejournal.com/update.bml?subject=" + text + "&event=" + url;
            string hackernews = "https://news.ycombinator.com/submitlink?u=" + url + "&t=" + title;
            string okru = "https://connect.ok.ru/dk?st.cmd=WidgetSharePreview&st.shareUrl=" + url;
            string pinterest = "http://pinterest.com/pin/create/button/?url=" + url;
            string qzone = "http://sns.qzone.qq.com/cgi-bin/qzshare/cgi_qzshare_onekey?url=" + url;
            string reddit = "https://reddit.com/submit?url=" + url + "&title=" + title;
            string renren = "http://widget.renren.com/dialog/share?resourceUrl=" + url + "&srcUrl=" + url + "&title=" + text + "&description=" + desc;
            string skype = "https://web.skype.com/share?url=" + url + "&text=" + text;
            string sms = "sms:" + phone_number + "?body=" + text;
            string surfingbird = "http://surfingbird.ru/share?url=" + url + "&description=" + desc + "&screenshot=" + image + "&title=" + title;
            string telegramme = "https://t.me/share/url?url=" + url + "&text=" + text + "&to=" + phone_number;
            string threema = "threema://compose?text=" + text + "&id=" + user_id;
            string tumblr = "https://www.tumblr.com/widgets/share/tool?canonicalUrl=" + url + "&title=" + title + "&caption=" + desc + "&tags=" + hash_tags;
            string twitter = "https://twitter.com/intent/tweet?url=" + url + "&text=" + text + "&via=" + via + "&hashtags=" + hash_tags;
            string vk = "http://vk.com/share.php?url=" + url + "&title=" + title + "&comment=" + desc;
            string weibo = "http://service.weibo.com/share/share.php?url=" + url + "&appkey=&title=" + title + "&pic=&ralateUid=";
            string whatsapp = "https://api.whatsapp.com/send?text=" + text + "%20" + url;
            string xing = "https://www.xing.com/spi/shares/new?url=" + url;
            string yahoo = "http://compose.mail.yahoo.com/?to=" + email_address + "&subject=" + title + "&body=" + text;

            int i = 0;

            urls[i++] = googlebookmarks;
            urls[i++] = facebook;
            urls[i++] = reddit;
            urls[i++] = whatsapp;
            urls[i++] = twitter;
            urls[i++] = linkedin;
            urls[i++] = tumblr;
            urls[i++] = pinterest;
            urls[i++] = blogger;
            urls[i++] = livejournal;
            urls[i++] = evernote;
            urls[i++] = addthis;
            urls[i++] = getpocket;
            urls[i++] = hackernews;
            urls[i++] = buffer;
            urls[i++] = flipboard;
            urls[i++] = instapaper;
            urls[i++] = surfingbird;
            urls[i++] = flattr;
            urls[i++] = diaspora;
            urls[i++] = qzone;
            urls[i++] = vk;
            urls[i++] = weibo;
            urls[i++] = okru;
            urls[i++] = douban;
            urls[i++] = xing;
            urls[i++] = renren;
            urls[i++] = threema;
            urls[i++] = sms;
            urls[i++] = lineme;
            urls[i++] = skype;
            urls[i++] = telegramme;
            urls[i++] = email;
            urls[i++] = gmail;
            urls[i++] = yahoo;
        }
    }
}