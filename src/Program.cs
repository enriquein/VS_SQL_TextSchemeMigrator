using System.Windows.Forms;

namespace VS_SQL_TextSchemeMigrator
{
    class Program
    {
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ConversionSelect());
        }
    }
}