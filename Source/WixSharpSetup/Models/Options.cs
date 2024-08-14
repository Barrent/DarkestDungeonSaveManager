using CommandLine;

namespace WixSharpSetup.Models
{
    public class Options
    {
        [Option('o', "Version", Required = false, HelpText = "Product version.")]
        public string Version { get; set; } = "0.9.0.0";
    }
}