using System.Text.RegularExpressions;

namespace Cligex.CommandLineGenerator
{
	using DictOptions = Dictionary<string, Option>;

	public class CommandGenerate
	{
		private readonly Option[] _options;
		private readonly DictOptions _dict_options = new();

		public string CommandName;
		public string Description;

		public CommandGenerate(string commandName, string description, params Option[] options)
		{
			_options = options;

			CommandName = commandName;
			Description = description;
		}

		public Option this[string key]
		{
			get { return _dict_options[key]; }
		}

		public void Run(params string[] args)
		{
			string commandLine = String.Join(" ", args);

			foreach (Option option in _options)
			{
				string optionsLongName = String.Join
				(
					"|",

					_options
					.Where((x) => x.LongOption != option.LongOption)
					.Select((x) => x.LongOption)
				);

				string optionsShortName = String.Join
				(
					"",

					_options
					.Where((x) => x.ShortOption != option.ShortOption)
					.Select((x) => x.ShortOption)
				);

				string patternCommands = $"-{option.ShortOption}|--{option.LongOption}"; 
				string pattern = @$"(?:{patternCommands})\s+(.+?)(?=(?:\s+(?:-[{optionsShortName}]|--(?:{optionsLongName})+)|$))";
				
				if (Regex.IsMatch(commandLine, patternCommands))
				{
					Match match = Regex.Match
					(
						commandLine,
						pattern
					);

					if  (match.Success)
					{
						option.Value = match.Groups[1].Value;
					}

					_dict_options.Add(option.LongOption, option);
				}

			}
		}

		public bool ContainOptions(params string[] optionsName)
		{
			return optionsName.All((optionName) => _dict_options.ContainsKey(optionName));
		}

		public bool ContainOnlyOptions(params string[] optionsName)
		{
			return ContainOptions(optionsName) && _dict_options.Count == optionsName.Length;
		}

		public void Help()
		{
			var options = _options.Select
			(
				(x) =>
				{
					bool required = x.Required;
					string options = $"-{x.ShortOption}, --{x.LongOption}";
					return (required) ? options : $"[ {options} ]";
				}
			);

			var descriptions = _options.Select((x) => $"{x}");

			string usage_options = String.Join(" | ", options);
			string descriptions_options = String.Join("\n\n", descriptions);

			string usage = String.Join
			(
				'\n',

				"Usage:",
				$"\t{CommandName} {usage_options}",
				"",
				$"{Description}",
				"",
				$"{descriptions_options}"
			);

			Console.WriteLine(usage);
		}
	}
}