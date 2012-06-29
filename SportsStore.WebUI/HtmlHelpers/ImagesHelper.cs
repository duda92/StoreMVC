using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.Drawing;
using System.IO;

namespace Store.WebUI.HtmlHelpers
{
    public static class HtmlHelperExtensions
    {
        private static byte[] no_image
        {
            get 
            {
                Image im = System.Drawing.Image.FromFile(@"D:\Practice\SvnLocalDir\SportsStore\SportsStore.WebUI\Content\no_image.png");
                MemoryStream ms = new MemoryStream();
                im.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();                 
            }
        }

        public static HtmlString Image(this HtmlHelper helper, byte[] imageBytes, int widthPx, int heightPx)
        {
            if (imageBytes == null)
                imageBytes = no_image;
            
            TagBuilder builder = new TagBuilder("img");
            string imageBase64 = Convert.ToBase64String(imageBytes);
            string imageSrc = string.Format("data:image/gif;base64,{0}", imageBase64);
            builder.Attributes["src"] = imageSrc;
            builder.Attributes["width"] = widthPx + "px";
            builder.Attributes["width"] = heightPx+ "px";
            string temp = builder.ToString();
            var ret = new MvcHtmlString(builder.ToString(TagRenderMode.SelfClosing));
            return ret;
        }
    }
}
