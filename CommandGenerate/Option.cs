namespace Cligex.CommandLineGenerator
{
public class Option
    {
        public readonly char ShortOption;
        public readonly string LongOption;
        public readonly string Description;

        public readonly bool Required;

        public string Value { get; internal set; } = "";


        public Option(char shortOption, string longOption, string description, bool required = false)
        {
            ShortOption = shortOption;
            LongOption = longOption;

            Description = description;

            Required = required;
        }

        public override string ToString()
        {
            return $"-{ShortOption}|--{LongOption}\n\t{Description}";
        }
    }
}