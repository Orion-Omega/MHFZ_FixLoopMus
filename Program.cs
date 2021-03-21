using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;

namespace Reverse
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("##############################");
            Console.WriteLine("#   MOD LOOP FIX MUS FILES   #");
            Console.WriteLine("#      CREATED BY ORION      #");
            Console.WriteLine("#        Verison 1.0         #");
            Console.WriteLine("##############################");
            Console.WriteLine("");
            if (File.Exists("mhfo-hd.dll"))
            {

                OpenBrowser("https://gist.github.com/Andoryuuta/1294a43fd44f0872648c3ef2bb995ef0");
                Console.WriteLine("If the value is same to by another you need to enter a value manually !!!");
                Console.WriteLine("");
                Thread thread = new Thread(new ThreadStart(Start));
                thread.Start();

            }
            else { Console.WriteLine("Put the mhfo-hd.dll in same folder to this program !"); Console.ReadLine(); }
        }
        static void Start()
        {
            string ByteHex(int value) => (value & 0xFF).ToString("X2");

            // UNK0_Original Value
            Console.WriteLine("Insert a [Original Track] (unk0) : ");
            string unk0_original_str = Console.ReadLine();
            Int32 unk0_original = Int32.Parse(unk0_original_str);
            string unk0Hex_original = ByteHex(unk0_original) + ByteHex(unk0_original >> 8) + ByteHex(unk0_original >> 16) + ByteHex(unk0_original >> 24);

            // UNK1_Original Value
            Console.WriteLine("Insert a [Original Track] (unk1) : ");
            string unk1_original_str = Console.ReadLine();
            Int32 unk1_original = Int32.Parse(unk1_original_str);
            string unk1Hex_original = ByteHex(unk1_original) + ByteHex(unk1_original >> 8) + ByteHex(unk1_original >> 16) + ByteHex(unk1_original >> 24);

            // UNK0_Modded Value
            Console.WriteLine("Insert a [Modded Track] (unk0) : ");
            string unk0_modded_str = Console.ReadLine();
            Int32 unk0_modded = Int32.Parse(unk0_modded_str);
            string unk0Hex_modded = ByteHex(unk0_modded) + ByteHex(unk0_modded >> 8) + ByteHex(unk0_modded >> 16) + ByteHex(unk0_modded >> 24);

            // UNK1_Modded Value
            Console.WriteLine("Insert a [Modded Track] (unk1) : ");
            string unk1_modded_str = Console.ReadLine();
            Int32 unk1_modded = Int32.Parse(unk1_modded_str);
            string unk1Hex_modded = ByteHex(unk1_modded) + ByteHex(unk1_modded >> 8) + ByteHex(unk1_modded >> 16) + ByteHex(unk1_modded >> 24);
            Console.WriteLine("Processing ...");
            try
            {
                byte[] buffer = File.ReadAllBytes("mhfo-hd.dll");
                string hex = BitConverter.ToString(buffer).Replace("-", string.Empty);
                if (hex.Contains(unk0Hex_original) & hex.Contains(unk1Hex_original))
                {
                    hex = Regex.Replace(hex, unk0Hex_original, unk0Hex_modded);
                    hex = Regex.Replace(hex, unk1Hex_original, unk1Hex_modded);
                    File.WriteAllBytes("mhfo-hd.dll", StringToByteArray(hex));
                    Console.WriteLine("mhfo-hd.dll completed !");
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("unk0 or unk1 [Original] not found !");
                    Console.ReadLine();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error" + e);
                Console.ReadLine();
            }
        }
        public static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }
        public static void OpenBrowser(string url)
        {
            try
            {
                Process.Start(url);
            }
            catch
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    url = url.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", url);
                }
                else
                {
                    throw;
                }
            }
        }
    }
}
