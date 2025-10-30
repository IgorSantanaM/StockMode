using Mjml.Net;
using RazorLight;
using StockMode.Application.Common.Interfaces;
using StockMode.Application.Features.Sales.Dtos;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StockMode.Infra.Services.Email
{
    public class RazorLightMjmlMailRenderer : IHtmlMailRenderer
    {
        private readonly RazorLightEngine _razor;
        private readonly IMjmlRenderer _mjml;
        private readonly IMailTemplateProvider _templates;
        public RazorLightMjmlMailRenderer(IMjmlRenderer mjml, IMailTemplateProvider templates)
        {
            _razor = new RazorLightEngineBuilder()
                .UseMemoryCachingProvider()
                .EnableDebugMode()
                .Build();

            _mjml = mjml;
            _templates = templates;
        }

        #region cssRules
        private readonly string[] cssAtRules = [
        "bottom-center", "bottom-left", "bottom-left-corner", "bottom-right", "bottom-right-corner", "charset", "counter-style",
        "document", "font-face", "font-feature-values", "import", "left-bottom", "left-middle", "left-top", "keyframes", "media",
        "namespace", "page", "right-bottom", "right-middle", "right-top", "supports", "top-center", "top-left", "top-left-corner",
        "top-right", "top-right-corner"
        ];

        private string EscapeCssRulesInRazorTemplate(string mjmlOutput) =>
        cssAtRules.Aggregate(mjmlOutput,
        (current, rule) => Regex.Replace(current, $@"(?<!@)@{rule}\b", $"@@{rule}"));

        private string EscapeCssFontWeightsInRazorTemplate(string mjmlOutput) =>
        mjmlOutput.Replace(":wght@", ":wght@@");
        #endregion

        public async Task<string> RenderAsync<TModel>(string templateName, TModel model)
        {
            var htmlTemplate = CompileMjmltemplateAsync(templateName);

            var findHtml = await _razor.CompileRenderStringAsync(templateName, htmlTemplate, model);

            return findHtml;
        }

        private string CompileMjmltemplateAsync(string templateName)
        {

             var mjmlSource = _templates.GetEmailTemplate(templateName);

            var (htmlOutput, errors) = _mjml.Render(mjmlSource);

            if (errors.Any())
            {
                var allErrors = string.Join("\n", errors.Select(e => $"Line {e.Position}: {e.Error}"));
                throw new Exception($"Error while compiling MJML:\n{allErrors}");
            }

            htmlOutput = EscapeCssRulesInRazorTemplate(htmlOutput);
            htmlOutput = EscapeCssFontWeightsInRazorTemplate(htmlOutput);

            return htmlOutput;
        }

    }
}
