namespace RenameUnicodeFilesNet6App
{
    using System;
    using System.Globalization;
    using System.Windows.Forms;
    using System.Runtime.InteropServices;

    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        /// 
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        [STAThread]
        static void Main()
        {
            AllocConsole();
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("RenameUnicodeFiles start");
            var folderBrowserDialog = new FolderBrowserDialog();
            var folderBrowserDialogResult = folderBrowserDialog.ShowDialog();
            if (folderBrowserDialogResult == DialogResult.OK)
            {
                //var allFiles = Directory.GetFiles("C:\\test\\Unicode_files");
                var allFiles = Directory.GetFiles(folderBrowserDialog.SelectedPath);
                foreach (var filePath in allFiles)
                {
                    //Console.WriteLine(filePath);
                    var fileName = Path.GetFileName(filePath);
                    var folderPath = Path.GetDirectoryName(filePath);
                    var fileNameNormSymbols = new List<char> { };
                    foreach (var symbol in fileName)
                    {
                        //Console.WriteLine(" ");
                        //Console.WriteLine(symbol);
                        //Console.WriteLine(Char.GetUnicodeCategory(symbol));
                        ////Console.WriteLine(Char.IsSymbol(symbol));
                        ////Console.WriteLine(Char.IsWhiteSpace(symbol));
                        //Console.WriteLine(Char.IsAscii(symbol));
                        //Console.WriteLine(" ");
                        if (Char.GetUnicodeCategory(symbol) == UnicodeCategory.Format)
                        {
                            continue;
                        }
                        else if (Char.GetUnicodeCategory(symbol) == UnicodeCategory.SpaceSeparator && !Char.IsAscii(symbol))
                        {
                            continue;
                        }
                        else
                        {
                            fileNameNormSymbols.Add(symbol);
                        }
                    }
                    var fileNameNorm = new string(fileNameNormSymbols.ToArray());
                    var newFilePath = Path.Combine(folderPath, fileNameNorm);
                    if (File.Exists(newFilePath))
                    {
                        while (File.Exists(newFilePath))
                        {
                            newFilePath = newFilePath.Replace(".", "_.");
                        }
                    }
                    File.Move(filePath, newFilePath);
                    //var output = new string(fileName.Where(c => Char.GetUnicodeCategory(c) != UnicodeCategory.Format).ToArray());
                    //Console.WriteLine(output);
                    Console.WriteLine(newFilePath);
                }
                Console.WriteLine("All files renamed press enter to finish");
                Console.ReadLine();
            }
        }
    }
}