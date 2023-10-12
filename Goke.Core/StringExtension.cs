using Markdig;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Goke.Core.Extensions
{
	public static class StringExtension
	{
		public static bool IsNumeric(this string s)
		{
			return Decimal.TryParse(s, out var d);
		}

		public static bool IsPoint(this string s)
		{
			return s == ".";
		}

		public static bool IsNegation(this string s)
		{
			return s == "+/-";
		}
		public static bool IsOperator(this string s)
		{
			return s == "+" || s == "-" || s == "*" || s == "/" || s == "=";
		}



		public static string ToSentence(this string s, string separator = "", bool removeSeparator = true, string fill = " ")
		{
			if (string.IsNullOrWhiteSpace(s))
				return s;

			string r = string.Empty;
			//int c = 0;
			for (int i = 0; i < s.Length; i++)
			{
				var q = s[i];
				if ((char.IsUpper(q) || separator == q.ToString()) && i > 0 && char.IsLower(s[(i - 1)]))
				{
					r = string.Format("{0}{1}{2}", r, fill, q);
					//c++;
				}
				else
				{
					r = string.Format("{0}{1}", r, q);
				}
			}

			//
			for (int i = 0; i < r.Length; i++)
			{
				var q = r[i];
				if (char.IsLower(q))
				{
					if (i > 1)
					{
						var j = i - 1;
						r = string.Format("{0}{1}{2}", r.Substring(0, j), fill, r[j..]);
					}
					break;
				}
			}
			if (removeSeparator && separator != "")
			{
				r = r.Replace(separator, "");
			}

			return r;
		}

		public static string ToTitleCase(this string s, string separator = " ")
		{
			if (string.IsNullOrWhiteSpace(s))
				return s;

			string r = string.Empty;

			var words = s.Split(separator, StringSplitOptions.RemoveEmptyEntries);
			foreach (var w in words)
			{
				r += " " + w.Substring(0, 1).ToUpper() + w[1..];
			}

			return r.TrimStart();
		}

		public static string ToPlural(this string word)
		{
			var len = word.Length;
			var w = word.ToLower();
			string s;
			if (System.String.Compare(w, "person", System.StringComparison.Ordinal) == 0)
			{
				s = word.Substring(0, 1) + "eople";
				return s;
			}
			if (w.EndsWith("staff"))
			{
				return word + "s";
			}
			if (System.String.Compare(w, "staff", System.StringComparison.Ordinal) == 0)
			{
				return word + "s";
			}

			var l = w.Last();
			switch (l)
			{
				case 'f': s = word.Substring(0, (len - 1)) + "ves"; break;
				case 'h':
				case 'o':
				case 's':
				case 'x': s = word + "es"; break;
				case 'y': s = word.Substring(0, (len - 1)) + "ies"; break;
				default:
					s = word + "s"; break;
			}
			return s;
		}

		public static string ToCamelCase(this string s)
		{
			if (string.IsNullOrWhiteSpace(s))
				return s;
			// CGPAText, GodExcellent
			var j = 0;
			for (int i = 0; i < s.Count(); i++)
			{
				var q = s[i];
				if (char.IsLower(q))
				{
					j = i;
					if (i > 1)
						j = i - 1;

					break;
				}
			}

			var l = s.Substring(0, j);
			var r = s[j..];
			return l.ToLower() + r;

		}

		public static string ToDisplayName(this string propertyName)
		{
			var name = propertyName;
			if (propertyName.EndsWith("Id"))
			{
				name = propertyName.Remove(propertyName.Length - 2);
			};

			return ToSentence(name);
		}

		public static int IndexOfNth(this string str, string value, int nth = 0)
		{
			if (nth < 0)
				throw new ArgumentException("Can not find a negative index of substring in string. Must start with 0");

			int offset = str.IndexOf(value);
			for (int i = 0; i < nth; i++)
			{
				if (offset == -1) return -1;
				offset = str.IndexOf(value, offset + 1);
			}

			return offset;
		}

		public static string ToThumb(this string img)
		{
			if (img.IndexOf('/') < 1) return img;

			var first = img.Substring(0, img.LastIndexOf('/'));
			var second = img.Substring(img.LastIndexOf('/'));

			return $"{first}/thumbs{second}";
		}

		public static string Capitalize(this string str)
		{
			if (string.IsNullOrEmpty(str))
				return string.Empty;
			char[] a = str.ToCharArray();
			a[0] = char.ToUpper(a[0]);
			return new string(a);
		}

		public static string MdToHtml(this string str)
		{
			var mpl = new MarkdownPipelineBuilder()
				.UsePipeTables()
				.UseAdvancedExtensions()
				.Build();

			return Markdown.ToHtml(str, mpl);
		}

		public static bool Contains(this string source, string toCheck, StringComparison comp)
		{
			return source.IndexOf(toCheck, comp) >= 0;
		}

		private static readonly Regex RegexStripHtml = new Regex("<[^>]*>", RegexOptions.Compiled);

		public static string StripHtml(this string str)
		{
			return string.IsNullOrWhiteSpace(str) ? string.Empty : RegexStripHtml.Replace(str, string.Empty).Trim();
		}

		/// <summary>
		/// Should extract title (file name) from file path or Url
		/// </summary>
		/// <param name="str">c:\foo\test.png</param>
		/// <returns>test.png</returns>
		public static string ExtractTitle(this string str)
		{
			if (str.Contains("\\"))
			{
				return string.IsNullOrWhiteSpace(str) ? string.Empty : str.Substring(str.LastIndexOf("\\")).Replace("\\", "");
			}
			else if (str.Contains("/"))
			{
				return string.IsNullOrWhiteSpace(str) ? string.Empty : str.Substring(str.LastIndexOf("/")).Replace("/", "");
			}
			else
			{
				return str;
			}
		}

		/// <summary>
		/// Converts title to valid URL slug
		/// </summary>
		/// <returns>Slug</returns>
		public static string ToSlug(this string title)
		{
			var str = title.ToLowerInvariant();
			str = str.Trim('-', '_');

			if (string.IsNullOrEmpty(str))
				return string.Empty;

			var bytes = Encoding.GetEncoding("utf-8").GetBytes(str);
			str = Encoding.UTF8.GetString(bytes);

			str = Regex.Replace(str, @"\s", "-", RegexOptions.Compiled);

			str = Regex.Replace(str, @"([-_]){2,}", "$1", RegexOptions.Compiled);

			str = RemoveIllegalCharacters(str);

			return str;
		}

		public static string Hash(this string source, string salt)
		{
			var bytes = KeyDerivation.Pbkdf2(
					  password: source,
					  salt: Encoding.UTF8.GetBytes(salt),
					  prf: KeyDerivationPrf.HMACSHA512,
					  iterationCount: 10000,
					  numBytesRequested: 256 / 8);

			return Convert.ToBase64String(bytes);
		}

		public static string ReplaceIgnoreCase(this string str, string search, string replacement)
		{
			string result = Regex.Replace(
				str,
				Regex.Escape(search),
				replacement.Replace("$", "$$"),
				RegexOptions.IgnoreCase
			);
			return result;
		}

		#region Helper Methods

		static string RemoveIllegalCharacters(string text)
		{
			if (string.IsNullOrEmpty(text))
			{
				return text;
			}

			string[] chars = new string[] {
				":", "/", "?", "!", "#", "[", "]", "{", "}", "@", "*", ".", ",",
				"\"","&", "'", "~", "$"
			};

			foreach (var ch in chars)
			{
				text = text.Replace(ch, string.Empty);
			}

			text = text.Replace("–", "-");
			text = text.Replace(" ", "-");

			text = RemoveUnicodePunctuation(text);
			text = RemoveDiacritics(text);
			text = RemoveExtraHyphen(text);

			return System.Web.HttpUtility.HtmlEncode(text).Replace("%", string.Empty);
		}

		static string RemoveUnicodePunctuation(string text)
		{
			var normalized = text.Normalize(NormalizationForm.FormD);
			var sb = new StringBuilder();

			foreach (var c in
				normalized.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.InitialQuotePunctuation &&
									  CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.FinalQuotePunctuation))
			{
				sb.Append(c);
			}

			return sb.ToString();
		}

		static string RemoveDiacritics(string text)
		{
			var normalized = text.Normalize(NormalizationForm.FormD);
			var sb = new StringBuilder();

			foreach (var c in
				normalized.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark))
			{
				sb.Append(c);
			}

			return sb.ToString();
		}

		static string RemoveExtraHyphen(string text)
		{
			if (text.Contains("--"))
			{
				text = text.Replace("--", "-");
				return RemoveExtraHyphen(text);
			}

			return text;
		}

		public static string SanitizePath(this string str)
		{
			if (string.IsNullOrWhiteSpace(str))
				return string.Empty;

			str = str.Replace("%2E", ".").Replace("%2F", "/");

			if (str.Contains("..") || str.Contains("//"))
				throw new ApplicationException("Invalid directory path");

			return str;
		}

		public static string SanitizeFileName(this string str)
		{
			str = str.SanitizePath();

			//TODO: add filename specific validation here

			return str;
		}

		#endregion
	}
}

