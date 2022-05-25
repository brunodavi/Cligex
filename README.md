# Cligex

command to test regex from terminal

```
Usage:
        cligex -t, --text | -s, --search | [ -r, --replace ] | [ -h, --help ]

Capture matches/replace by regex

-t|--text
        Text to search

-s|--search
        Search in regex

-r|--replace
        Replace search founding in text

-h|--help
        Show this message
```

### Examples

```bash
# Matches
$ cligex -t Test123 -s \d
1
2
3

# Replace
$ cligex -t Test123 -s \d+ -r 321
Test321

```
