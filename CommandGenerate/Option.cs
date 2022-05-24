namespace Cligex.CommandLineGenerator
{
public class Option
    {
        public readonly char ShortOption;
        public readonly string LongOption;
        public readonly string Description;

        public readonly bool Required;

        private string? _value;


        public string Value
        {
            get => (_value != null) ? _value : "";
            set
            {
                if (Value != "")
                    throw new ArgumentException();
                else if (value == null)
                    throw new ArgumentNullException();

                _value = value;
            }
        }

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