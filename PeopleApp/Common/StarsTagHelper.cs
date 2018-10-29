using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleApp.Common
{
    public class StarsTagHelper : TagHelper
    {
        public int Level { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;
            //output.Attributes.Add("role", "alert");
            //output.Attributes.Add("class", "alert alert-" + Type.ToString().ToLower());
            //output.Content.SetContent(Message);

            //var content = new StringBuilder();

            for (int i = 0; i < 5; i++)
            {
                if (i<Level)
                {

                    output.Content.AppendHtml("<i class='fa fa-star' style='color:#ffd800'></i>");
                }
                else
                {
                    output.Content.AppendHtml("<i class='fa fa-star-o'></i>");
                }
            }

            //output.Content.SetContent(content);
        }
    }
}
