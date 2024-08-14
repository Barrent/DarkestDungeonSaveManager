using WixSharp;

namespace WixSharpSetup.Extensions
{
    public static class WixExtensions
    {
        public static ExeFileShortcut Copy(this ExeFileShortcut source)
        {
            return new ExeFileShortcut(source.Name, source.Target, source.Arguments)
            {
                WorkingDirectory = source.WorkingDirectory
            };
        }
    }
}