using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;

namespace VS_SQL_TextSchemeMigrator
{
    /// <summary>
    /// This class was based on VSColorsToSQL by Tomas Restrepo (tomas@winterdom.com)
    /// </summary>
    public class ThemeImporter
    {
        private const string SRC_KEY = @"Software\Microsoft\VisualStudio\9.0\FontAndColors\{A27B4E24-A735-4D1D-B8E7-9716E1E3D8E0}";
        private const string DST_KEY = @"Software\Microsoft\Microsoft SQL Server\90\Tools\Shell\FontAndColors\{A27B4E24-A735-4D1D-B8E7-9716E1E3D8E0}";

        private IDictionary<string, string> _map;
        private IList<string> _ignorePrefixes;

        public ThemeImporter()
        {
            _map = new Dictionary<string, string>();

            #region values we map from one to the other
            _map.Add("Plain Text Foreground", "Plain Text Foreground");
            _map.Add("Plain Text Background", "Plain Text Background");
            _map.Add("Plain Text FontFlags", "Plain Text FontFlags");

            _map.Add("FontName", "FontName");
            _map.Add("FontPointSize", "FontPointSize");
            _map.Add("FontCharSet", "FontCharSet");

            _map.Add("Comment Foreground", "Comment Foreground");
            _map.Add("Comment Background", "Comment Background");
            _map.Add("Comment FontFlags", "Comment FontFlags");

            _map.Add("Line Numbers Foreground", "Line Numbers Foreground");
            _map.Add("Line Numbers Background", "Line Numbers Background");
            _map.Add("Line Numbers FontFlags", "Line Numbers FontFlags");

            _map.Add("Brace Matching (Highlight) Foreground", "Brace Matching (Highlight) Foreground");
            _map.Add("Brace Matching (Highlight) Background", "Brace Matching (Highlight) Background");
            _map.Add("Brace Matching (Highlight) FontFlags", "Brace Matching (Highlight) FontFlags");

            _map.Add("Brace Matching (Rectangle) Foreground", "Brace Matching (Rectangle) Foreground");
            _map.Add("Brace Matching (Rectangle) Background", "Brace Matching (Rectangle) Background");
            _map.Add("Brace Matching (Rectangle) FontFlags", "Brace Matching (Rectangle) FontFlags");

            _map.Add("String Background", "SQL String Background");
            _map.Add("String FontFlags", "SQL String FontFlags");
            _map.Add("String Foreground", "SQL String Foreground");

            _map.Add("Operator Foreground", "SQL Operator Foreground");
            _map.Add("Operator Background", "SQL Operator Background");
            _map.Add("Operator FontFlags", "SQL Operator FontFlags");

            _map.Add("User Types Foreground", "SQL Stored Procedure Foreground");
            _map.Add("User Types Background", "SQL Stored Procedure Background");
            _map.Add("User Types FontFlags", "SQL Stored Procedure FontFlags");

            _map.Add("User Types (Interfaces) Foreground", "SQL System Function Foreground");
            _map.Add("User Types (Interfaces) Background", "SQL System Function Background");
            _map.Add("User Types (Interfaces) FontFlags", "SQL System Function FontFlags");

            _map.Add("User Types (Value types) Foreground", "SQL System Table Foreground");
            _map.Add("User Types (Value types) Background", "SQL System Table Background");
            _map.Add("User Types (Value types) FontFlags", "SQL System Table FontFlags");

            _map.Add("XML Attribute Foreground", "XmlAttribute Foreground");
            _map.Add("XML Attribute Background", "XmlAttribute Background");
            _map.Add("XML Attribute FontFlags", "XmlAttribute FontFlags");

            _map.Add("XML Attribute Quotes Foreground", "XmlAttributeQuotes Foreground");
            _map.Add("XML Attribute Quotes Background", "XmlAttributeQuotes Background");
            _map.Add("XML Attribute Quotes FontFlags", "XmlAttributeQuotes FontFlags");

            _map.Add("XML CData Section Foreground", "XmlCData Foreground");
            _map.Add("XML CData Section Background", "XmlCData Background");
            _map.Add("XML CData Section FontFlags", "XmlCData FontFlags");

            _map.Add("XML Comment Foreground", "XmlComment Foreground");
            _map.Add("XML Comment Background", "XmlComment Background");
            _map.Add("XML Comment FontFlags", "XmlComment FontFlags");

            _map.Add("XML Delimiter Foreground", "XmlDelimiter Foreground");
            _map.Add("XML Delimiter Background", "XmlDelimiter Background");
            _map.Add("XML Delimiter FontFlags", "XmlDelimiter FontFlags");

            _map.Add("XML Keyword Foreground", "XmlKeyword Foreground");
            _map.Add("XML Keyword Background", "XmlKeyword Background");
            _map.Add("XML Keyword FontFlags", "XmlKeyword FontFlags");

            _map.Add("XML Name Foreground", "XmlName Foreground");
            _map.Add("XML Name Background", "XmlName Background");
            _map.Add("XML Name FontFlags", "XmlName FontFlags");

            _map.Add("XML Processing Instruction Foreground", "XmlProcessingInstruction Foreground");
            _map.Add("XML Processing Instruction Background", "XmlProcessingInstruction Background");
            _map.Add("XML Processing Instruction FontFlags", "XmlProcessingInstruction FontFlags");

            _map.Add("XML Text Foreground", "XmlText Foreground");
            _map.Add("XML Text Background", "XmlText Background");
            _map.Add("XML Text FontFlags", "XmlText FontFlags");

            _map.Add("XSLT Keyword Foreground", "XsltKeyword Foreground");
            _map.Add("XSLT Keyword Background", "XsltKeyword Background");
            _map.Add("XSLT Keyword FontFlags", "XsltKeyword FontFlags");

            #endregion

            _ignorePrefixes = new string[] {
            "CSS ", "Disassembly ", "HTML ", "Refactoring ", "XML Doc "
         };

        }

        public void Copy()
        {
            RegistryKey src = Registry.CurrentUser.OpenSubKey(SRC_KEY);
            RegistryKey dst = Registry.CurrentUser.OpenSubKey(DST_KEY, true);


            string[] values = src.GetValueNames();
            float counter = 1;
            float totalValues = values.Length;
            foreach (string valname in values)
            {
                Console.SetCursorPosition(0, 3);
                Console.Write("{0} Done.", (counter / totalValues).ToString("#0%"));
                CopyValue(valname, src, dst);
                ++counter;
            }
        }

        private void CopyValue(string valname, RegistryKey src, RegistryKey dst)
        {
            if (!Ignore(valname))
            {
                string rv = valname;
                if (_map.ContainsKey(valname))
                    rv = _map[valname];

                dst.SetValue(rv, src.GetValue(valname));
                // hack: we also copy the Identifier ones over to plain text
                //if ( valname.StartsWith("Identifier") && !valname.Contains("Background") )
                //{
                //   dst.SetValue(
                //      valname.Replace("Identifier", "Plain Text"), 
                //      src.GetValue(valname)
                //      );
                //}
            }
        }

        private bool Ignore(string valname)
        {
            foreach (string prefix in _ignorePrefixes)
            {
                if (valname.StartsWith(prefix))
                    return true;
            }
            return false;
        }
    }
}
