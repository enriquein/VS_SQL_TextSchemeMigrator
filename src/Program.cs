using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace VS_SQL_TextSchemeMigrator
{
    class Program
    {
        static void Main(string[] args)
        {
            //try
            //{
            //   Console.Clear();
            //   Console.WriteLine("Visual Studio 2008 to SQL Management Studio Text Settings Converter.");
            //   ColorImporter importer = new ColorImporter();
            //   importer.Copy();
            //   Console.WriteLine("");
            //   Console.WriteLine("Import completed successfully. Press Enter key to exit.");
            //   Console.Read();
            //} catch ( Exception ex ) {
            //   Console.WriteLine("");
            //   Console.WriteLine("An exception ocurred while executing. The error returned was {0}. Press Enter to exit.", ex.Message);
            //   Console.Read();
            //}
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ConversionSelect());
        }
    }
}