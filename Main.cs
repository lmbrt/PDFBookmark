namespace PDFBookmarkReader
{
	using System;
	using System.Text.RegularExpressions;
	using System.Collections.Generic;
	using iTextSharp.text.pdf;

	class MainClass
	{
		private static Regex _whitespaceRegex = new Regex(@"\s{2,}", RegexOptions.Compiled);
		
		public static void Main (string[] args)
		{			
			if (args.Length < 1)
			{
				Console.WriteLine ("Usage: pdfBookmarkReader.exe pdffile.pdf");	
				return;
			}
			
			string file = args[0];
			
			if (! System.IO.File.Exists (file))
			{
				Console.WriteLine ("File {0} is missing", file);	
				return;
			}
				
			
			PdfReader pr = new PdfReader(file);
			IList<Dictionary<string, object>> bookmarks = SimpleBookmark.GetBookmark(pr);
			Console.WriteLine ("<PDF>\n\t<PageCount>{0}</PageCount>\n\t<Bookmarks Total=\"{1}\">\t", pr.NumberOfPages, bookmarks.Count);		

		    if (bookmarks != null)
            {
                foreach (Dictionary<string, object> bookmark in bookmarks)
                {	
                    string title = (string)bookmark["Title"];

                    title = _whitespaceRegex.Replace(title, " ");
                    string[] pageData = ((string)(bookmark["Page"])).Split(' ');
                    string page = pageData[0];
                    page = page.Trim();

                    Console.WriteLine ("\t\t<Bookmark>\n\t\t\t<Title>{0}</Title>\n\t\t\t<Page>{1}</Page>\n\t\t</Bookmark>", title, page);	
                }
            }
			
			Console.WriteLine("\t</Bookmarks>\n</PDF>");
		}
	}
}
