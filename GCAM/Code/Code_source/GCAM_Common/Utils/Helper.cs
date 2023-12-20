
namespace GCAM_Common
{
    internal static class Helper
    {
        internal static string BytesToHex(byte[] bytes) 
        {
            return BitConverter.ToString(bytes).Replace('-', ' ');
        }
    }
}
