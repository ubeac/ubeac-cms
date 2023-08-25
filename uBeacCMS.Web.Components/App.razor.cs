using System.Text.RegularExpressions;

namespace uBeacCMS.Web.Components;

public partial class App : ComponentBase
{
    [Inject]
    protected ViewContext? Context { get; set; }

    [Parameter]
    public string? PageRoute { get; set; }

    public RenderFragment RenderPage() => builder =>
    {
        if (Context?.Site != null)
        {
            var markup = Context.Skin?.Markup;
            var paneTags = CustomTagExtractor.ExtractCustomTags(markup, "pane");

            var paneCounter = 0;
            var startIndex = 0;
            foreach (var pane in paneTags)
            {
                builder.AddMarkupContent(paneCounter, markup[startIndex..pane.StartIndex]);
                paneCounter++;
                builder.OpenComponent<Pane>(paneCounter);
                var attrCounter = 0;
                builder.AddAttribute(attrCounter, "modules", Context?.Modules?.Where(x => x.Pane.ToLower() == pane.Attributes["name"]).ToList());
                attrCounter++;
                builder.AddAttribute(attrCounter, "context", Context);
                attrCounter++;
                foreach (var attribute in pane.Attributes)
                {
                    builder.AddAttribute(attrCounter, attribute.Key, attribute.Value);
                    attrCounter++;
                }
                builder.CloseComponent();
                paneCounter++;
                startIndex = pane.EndIndex + 1;
            }

            builder.AddMarkupContent(paneCounter, markup.Substring(startIndex));

        }
    };
}

public class CustomTagDetails
{
    public int StartIndex { get; set; }
    public int EndIndex { get; set; }
    public Dictionary<string, string> Attributes { get; set; } = new Dictionary<string, string>();
}
public class CustomTagExtractor
{
    public static List<CustomTagDetails> ExtractCustomTags(string html, List<string> tagNames)
    {
        List<CustomTagDetails> customTags = new List<CustomTagDetails>();
        string pattern = "";
        foreach (var tagName in tagNames)
        {
            pattern += $"<{tagName}([^>]*)\\s*(\\/|>((.|\\n)*?)<\\/{tagName}>)|";
        }

        pattern = pattern.Substring(0, pattern.Length - 1);

        MatchCollection matches = Regex.Matches(html, pattern, RegexOptions.Singleline);

        foreach (Match match in matches)
        {
            int startIndex = match.Index;
            int endIndex = startIndex + match.Length - 1;
            string tagAttributes = match.Groups[1].Value;
            string tagContent = match.Groups[match.Groups.Count - 2].Value;
            Dictionary<string, string> attributes = ExtractAttributes(tagAttributes);

            customTags.Add(new CustomTagDetails
            {
                StartIndex = startIndex,
                EndIndex = endIndex,
                Attributes = attributes
            });
        }

        return customTags;
    }
    
    public static List<CustomTagDetails> ExtractCustomTags(string html, string tagName)
    {
        List<CustomTagDetails> customTags = new List<CustomTagDetails>();
        string pattern = $"<{tagName}([^>]*)\\s*(\\/|>((.|\\n)*?)<\\/{tagName}>)";

        MatchCollection matches = Regex.Matches(html, pattern, RegexOptions.Singleline);

        foreach (Match match in matches)
        {
            int startIndex = match.Index;
            int endIndex = startIndex + match.Length - 1;
            string tagAttributes = match.Groups[1].Value;
            string tagContent = match.Groups[match.Groups.Count - 2].Value;
            Dictionary<string, string> attributes = ExtractAttributes(tagAttributes);

            customTags.Add(new CustomTagDetails
            {
                StartIndex = startIndex,
                EndIndex = endIndex,
                Attributes = attributes
            });
        }

        return customTags;
    }

    private static Dictionary<string, string> ExtractAttributes(string tagAttributes)
    {
        Dictionary<string, string> attributes = new Dictionary<string, string>();
        string pattern = "\\b(\\w+)\\s*=\\s*\"([^\"]*)\"";

        MatchCollection matches = Regex.Matches(tagAttributes, pattern);

        foreach (Match match in matches)
        {
            string attributeName = match.Groups[1].Value;
            string attributeValue = match.Groups[2].Value;
            attributes[attributeName] = attributeValue;
        }

        return attributes;
    }
}
