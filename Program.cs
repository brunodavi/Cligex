using System;
using System.Text.RegularExpressions;


using Cligex.CommandLineGenerator;

namespace Cligex
{
	public class Program
	{
		static void Main(string[] args)
		{
			CommandGenerate command = new
			(
				"cligex",
				"Capture matches/replace by regex",

				new Option
				(
					't', "text",
					"Text to search",

					required: true
				),

				new Option
				(
					's', "search",
					"Search in regex",

					required: true
				),

				new Option
				(
					'r', "replace",
					"Replace search founding in text"
				),

				new Option
				(
					'h', "help",
					"Show this message"
				)
			);

			command.Run(args);

			if (command.ContainOptions("help"))
			{
				command.Help();
			}

			if (command.ContainOnlyOptions("text", "search"))
			{
				string text = command["text"].Value;
				string search = command["search"].Value;

				foreach (Match match in Regex.Matches(text, search))
				{
					Console.WriteLine(match.Value);
				}
			}
			else if (command.ContainOnlyOptions("text", "search", "replace"))
			{
				string text = command["text"].Value;
				string search = command["search"].Value;
				string replace = command["replace"].Value;

				Console.WriteLine
				(
					Regex.Replace(text, search, replace)
				);
			}
		}
	}
}