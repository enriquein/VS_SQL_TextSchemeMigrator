using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;

namespace VS_SQL_TextSchemeMigrator
{
    /// <summary>
    /// Provides the mechanism to copy Text Editor color/font schemes from one application to another.
    /// This class was based on VSColorsToSQL by Tomas Restrepo (tomas@winterdom.com)
    /// </summary>
    public class ThemeImporter
    {
        private string _vsRegKeyFormat = "Software\\Microsoft\\VisualStudio\\{0}\\FontAndColors\\{A27B4E24-A735-4D1D-B8E7-9716E1E3D8E0}";
        private string _sqlRegKeyFormat = "Software\\Microsoft\\Microsoft SQL Server\\{0}\\Tools\\Shell\\FontAndColors\\{A27B4E24-A735-4D1D-B8E7-9716E1E3D8E0}";

        private IDictionary<string, string> _versionStrings;
        private IDictionary<string, string> _map;
        private IList<string> _ignorePrefixes;

        public ThemeImporter()
        {
            _ignorePrefixes = new string[0];

            _versionStrings = new Dictionary<string, string>(4);
            _versionStrings.Add("VS2005", "80");
            _versionStrings.Add("VS2008", "90");
            _versionStrings.Add("SQL2005", "90");
            _versionStrings.Add("SQL2008", "100");
        }

        /// <summary>
        /// Copies settings from a Visual Studio installation to Sql Server Management Studio.
        /// </summary>
        /// <param name="vsVersion">Represents the version of Visual Studio from which we will read the settings.</param>
        /// <param name="sqlVersion">Represents the version of SSMS which will receive the settings.</param>
        public void CopyVSToSql(VisualStudioVersion vsVersion, SqlStudioVersion sqlVersion)
        {
            string vsRegKeyPath = string.Format(_vsRegKeyFormat, _versionStrings[Enum.GetName(typeof(VisualStudioVersion), vsVersion)]);
            string sqlRegKeyPath = string.Format(_sqlRegKeyFormat, _versionStrings[Enum.GetName(typeof(SqlStudioVersion), sqlVersion)]);
            CreateMappingsFromVisualStudioToSql();
            Copy(vsRegKeyPath, sqlRegKeyPath);
        }

        /// <summary>
        /// Copies settings between different versions of Sql Server Management Studio.
        /// </summary>
        /// <param name="sqlVersionSource">Represents the version of SSMS from which we will read the settings.</param>
        /// <param name="sqlVersionDestination">Represents the version of SSMS which will receive the settings.</param>
        public void CopySqlToSql(SqlStudioVersion sqlVersionSource, SqlStudioVersion sqlVersionDestination)
        {
            string sqlRegKeyPathSource = string.Format(_vsRegKeyFormat, _versionStrings[Enum.GetName(typeof(VisualStudioVersion), sqlVersionSource)]);
            string sqlRegKeyPathDestination = string.Format(_sqlRegKeyFormat, _versionStrings[Enum.GetName(typeof(SqlStudioVersion), sqlVersionDestination)]);
            CreateMappingsFromSqlToSql();
            Copy(sqlRegKeyPathSource, sqlRegKeyPathDestination);
        }

        /// <summary>
        /// Populates the DataDictionary object that contains mapping information between registry keys.
        /// </summary>
        private void CreateMappingsFromSqlToSql()
        {
            _map = new Dictionary<string, string>();

            _map.Add("Plain Text Foreground", "Plain Text Foreground");
            _map.Add("Plain Text Background", "Plain Text Background");
            _map.Add("Plain Text FontFlags", "Plain Text FontFlags");
            _map.Add("Selected Text Foreground", "Selected Text Foreground");
            _map.Add("Selected Text Background", "Selected Text Background");
            _map.Add("Selected Text FontFlags", "Selected Text FontFlags");
            _map.Add("Inactive Selected Text Foreground", "Inactive Selected Text Foreground");
            _map.Add("Inactive Selected Text Background", "Inactive Selected Text Background");
            _map.Add("Inactive Selected Text FontFlags", "Inactive Selected Text FontFlags");
            _map.Add("Indicator Margin Foreground", "Indicator Margin Foreground");
            _map.Add("Indicator Margin Background", "Indicator Margin Background");
            _map.Add("Indicator Margin FontFlags", "Indicator Margin FontFlags");
            _map.Add("Line Numbers Foreground", "Line Numbers Foreground");
            _map.Add("Line Numbers Background", "Line Numbers Background");
            _map.Add("Line Numbers FontFlags", "Line Numbers FontFlags");
            _map.Add("Visible White Space Foreground", "Visible White Space Foreground");
            _map.Add("Visible White Space Background", "Visible White Space Background");
            _map.Add("Visible White Space FontFlags", "Visible White Space FontFlags");
            _map.Add("Bookmark Foreground", "Bookmark Foreground");
            _map.Add("Bookmark Background", "Bookmark Background");
            _map.Add("Bookmark FontFlags", "Bookmark FontFlags");
            _map.Add("Brace Matching (Rectangle) Foreground", "Brace Matching (Rectangle) Foreground");
            _map.Add("Brace Matching (Rectangle) Background", "Brace Matching (Rectangle) Background");
            _map.Add("Brace Matching (Rectangle) FontFlags", "Brace Matching (Rectangle) FontFlags");
            _map.Add("Code Snippet Field Foreground", "Code Snippet Field Foreground");
            _map.Add("Code Snippet Field Background", "Code Snippet Field Background");
            _map.Add("Code Snippet Field FontFlags", "Code Snippet Field FontFlags");
            _map.Add("Collapsible Text Foreground", "Collapsible Text Foreground");
            _map.Add("Collapsible Text Background", "Collapsible Text Background");
            _map.Add("Collapsible Text FontFlags", "Collapsible Text FontFlags");
            _map.Add("Comment Foreground", "Comment Foreground");
            _map.Add("Comment Background", "Comment Background");
            _map.Add("Comment FontFlags", "Comment FontFlags");
            _map.Add("Compiler Error Foreground", "Compiler Error Foreground");
            _map.Add("Compiler Error Background", "Compiler Error Background");
            _map.Add("Compiler Error FontFlags", "Compiler Error FontFlags");
            _map.Add("Current list location Foreground", "Current list location Foreground");
            _map.Add("Current list location Background", "Current list location Background");
            _map.Add("Current list location FontFlags", "Current list location FontFlags");
            _map.Add("Definition Window Background Foreground", "Definition Window Background Foreground");
            _map.Add("Definition Window Background Background", "Definition Window Background Background");
            _map.Add("Definition Window Background FontFlags", "Definition Window Background FontFlags");
            _map.Add("Definition Window Current Match Foreground", "Definition Window Current Match Foreground");
            _map.Add("Definition Window Current Match Background", "Definition Window Current Match Background");
            _map.Add("Definition Window Current Match FontFlags", "Definition Window Current Match FontFlags");
            _map.Add("Identifier Foreground", "Identifier Foreground");
            _map.Add("Identifier Background", "Identifier Background");
            _map.Add("Identifier FontFlags", "Identifier FontFlags");
            _map.Add("Keyword Foreground", "Keyword Foreground");
            _map.Add("Keyword Background", "Keyword Background");
            _map.Add("Keyword FontFlags", "Keyword FontFlags");
            _map.Add("MDX Function Foreground", "MDX Function Foreground");
            _map.Add("MDX Function Background", "MDX Function Background");
            _map.Add("MDX Function FontFlags", "MDX Function FontFlags");
            _map.Add("MDX Property Foreground", "MDX Property Foreground");
            _map.Add("MDX Property Background", "MDX Property Background");
            _map.Add("MDX Property FontFlags", "MDX Property FontFlags");
            _map.Add("Number Foreground", "Number Foreground");
            _map.Add("Number Background", "Number Background");
            _map.Add("Number FontFlags", "Number FontFlags");
            _map.Add("Other Error Foreground", "Other Error Foreground");
            _map.Add("Other Error Background", "Other Error Background");
            _map.Add("Other Error FontFlags", "Other Error FontFlags");
            _map.Add("Read-Only Region Foreground", "Read-Only Region Foreground");
            _map.Add("Read-Only Region Background", "Read-Only Region Background");
            _map.Add("Read-Only Region FontFlags", "Read-Only Region FontFlags");
            _map.Add("Smart Tag Foreground", "Smart Tag Foreground");
            _map.Add("Smart Tag Background", "Smart Tag Background");
            _map.Add("Smart Tag FontFlags", "Smart Tag FontFlags");
            _map.Add("SQL Operator Foreground", "SQL Operator Foreground");
            _map.Add("SQL Operator Background", "SQL Operator Background");
            _map.Add("SQL Operator FontFlags", "SQL Operator FontFlags");
            _map.Add("SQL Stored Procedure Foreground", "SQL Stored Procedure Foreground");
            _map.Add("SQL Stored Procedure Background", "SQL Stored Procedure Background");
            _map.Add("SQL Stored Procedure FontFlags", "SQL Stored Procedure FontFlags");
            _map.Add("SQL String Foreground", "SQL String Foreground");
            _map.Add("SQL String Background", "SQL String Background");
            _map.Add("SQL String FontFlags", "SQL String FontFlags");
            _map.Add("SQL System Function Foreground", "SQL System Function Foreground");
            _map.Add("SQL System Function Background", "SQL System Function Background");
            _map.Add("SQL System Function FontFlags", "SQL System Function FontFlags");
            _map.Add("SQL System Table Foreground", "SQL System Table Foreground");
            _map.Add("SQL System Table Background", "SQL System Table Background");
            _map.Add("SQL System Table FontFlags", "SQL System Table FontFlags");
            _map.Add("SQLCMD Command Foreground", "SQLCMD Command Foreground");
            _map.Add("SQLCMD Command Background", "SQLCMD Command Background");
            _map.Add("SQLCMD Command FontFlags", "SQLCMD Command FontFlags");
            _map.Add("String Foreground", "String Foreground");
            _map.Add("String Background", "String Background");
            _map.Add("String FontFlags", "String FontFlags");
            _map.Add("Task List Shortcut Foreground", "Task List Shortcut Foreground");
            _map.Add("Task List Shortcut Background", "Task List Shortcut Background");
            _map.Add("Task List Shortcut FontFlags", "Task List Shortcut FontFlags");
            _map.Add("Template Parameter Foreground", "Template Parameter Foreground");
            _map.Add("Template Parameter Background", "Template Parameter Background");
            _map.Add("Template Parameter FontFlags", "Template Parameter FontFlags");
            _map.Add("Track Changes after save Foreground", "Track Changes after save Foreground");
            _map.Add("Track Changes after save Background", "Track Changes after save Background");
            _map.Add("Track Changes after save FontFlags", "Track Changes after save FontFlags");
            _map.Add("Track Changes before save Foreground", "Track Changes before save Foreground");
            _map.Add("Track Changes before save Background", "Track Changes before save Background");
            _map.Add("Track Changes before save FontFlags", "Track Changes before save FontFlags");
            _map.Add("Warning Foreground", "Warning Foreground");
            _map.Add("Warning Background", "Warning Background");
            _map.Add("Warning FontFlags", "Warning FontFlags");
            _map.Add("XML Attribute Name Foreground", "XML Attribute Name Foreground");
            _map.Add("XML Attribute Name Background", "XML Attribute Name Background");
            _map.Add("XML Attribute Name FontFlags", "XML Attribute Name FontFlags");
            _map.Add("XML Element Name Foreground", "XML Element Name Foreground");
            _map.Add("XML Element Name Background", "XML Element Name Background");
            _map.Add("XML Element Name FontFlags", "XML Element Name FontFlags");
            _map.Add("XML Tag Delimiter Foreground", "XML Tag Delimiter Foreground");
            _map.Add("XML Tag Delimiter Background", "XML Tag Delimiter Background");
            _map.Add("XML Tag Delimiter FontFlags", "XML Tag Delimiter FontFlags");
        }

        /// <summary>
        /// Populates the DataDictionary object that contains mapping information between registry keys.
        /// </summary>
        private void CreateMappingsFromVisualStudioToSql()
        {
            _map = new Dictionary<string, string>();

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

            _ignorePrefixes = new string[] { "CSS ", "Disassembly ", "HTML ", "Refactoring ", "XML Doc " };
        }

        /// <summary>
        /// Copies the values defined in the DataDictionary. 
        /// </summary>
        /// <param name="sourcePath">Registry path to the source registry key.</param>
        /// <param name="destinationPath">Registry path to the destination registry key.</param>
        private void Copy(string sourcePath, string destinationPath)
        {
            RegistryKey sourceKey = Registry.CurrentUser.OpenSubKey(sourcePath);
            RegistryKey destinationKey = Registry.CurrentUser.OpenSubKey(destinationPath, true);

            string[] values = sourceKey.GetValueNames();
            foreach (string valueName in values)
            {
                if (!IsIgnoredValue(valueName))
                {
                    string registryValue = valueName;
                    if (_map.ContainsKey(valueName))
                        registryValue = _map[valueName];

                    destinationKey.SetValue(registryValue, sourceKey.GetValue(valueName));
                }
            }
        }

        /// <summary>
        /// Checks if the value is part of the "ignore list".
        /// </summary>
        /// <param name="valueName">Value to check.</param>
        /// <returns>True if it should be ignored, false otherwise.</returns>
        private bool IsIgnoredValue(string valueName)
        {
            foreach (string prefix in _ignorePrefixes)
            {
                if (valueName.StartsWith(prefix))
                    return true;
            }
            return false;
        }
    }
}
