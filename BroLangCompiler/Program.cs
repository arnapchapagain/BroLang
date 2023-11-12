namespace BroLangCompiler;

class Program
{
    enum TokenType
    {
        Yeet,
        IntegerLiteral,
        SemiColon
    }

    enum Keyword
    {
        Return = TokenType.Yeet
    }
    
    enum Punctuator
    {
        SemiColon = TokenType.SemiColon
    }

    struct Token
    {
        public TokenType Type;
        public string? Value;

        public Token(TokenType type, string? value)
        {
            Type = type;
            Value = value;
        }
    }

    static Dictionary<string, TokenType> _keywords = new Dictionary<string, TokenType>()
    {
        {"yeet", TokenType.Yeet}
    };

    static Dictionary<string, TokenType> _operators = new Dictionary<string, TokenType>();
    
    static Dictionary<string, TokenType> _punctuators = new Dictionary<string, TokenType>()
    {
        {";", TokenType.SemiColon}
    };

    static List<Token> Tokenize(string str)
    {
        List<Token> tokens = new List<Token>();
        List<char> buffer = new List<char>();
        
        for (int i = 0; i < str.Length; i++)
        {
            char c = str[i];

            if (Char.IsLetter(c))
            {
                buffer.Add(c);
                i++;
                while (Char.IsLetterOrDigit(str[i]))
                {
                    buffer.Add(str[i]);
                    i++;
                }
                i--;

                string? bufferString = buffer.Count > 0 ? string.Concat(buffer) : "";
                if (_keywords.TryGetValue(bufferString, out TokenType keywordType))
                {
                    tokens.Add(new Token(_keywords[bufferString], bufferString));
                    buffer.Clear();
                    continue;
                }
                else
                {
                    Console.WriteLine("Error: Invalid alphabetical token.");
                    Environment.Exit(1);
                }
            }
            
            else if (Char.IsDigit(c))
            {
                buffer.Add(c);
                i++;
                while (Char.IsDigit(str[i]))
                {
                    buffer.Add(str[i]);
                    i++;
                }
                i--;
                tokens.Add(new Token(TokenType.IntegerLiteral, string.Concat(buffer)));
                buffer.Clear();
            }
            
            else if (Char.IsPunctuation(c))
            {
                if (_punctuators.TryGetValue(c.ToString(), out TokenType punctuatorType))
                {
                    tokens.Add(new Token(_punctuators[c.ToString()], c.ToString()));
                    continue;
                }
            }

            else if (Char.IsWhiteSpace(c))
            {
                continue;
            }

            else
            {
                Console.WriteLine("Error: Invalid token.");
                Environment.Exit(1);
            }
            
        }
                
        return tokens;
    }
    
    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("No input file specified.");
            return;
        }

        if (!File.Exists(args[0]))
        {
            Console.WriteLine($"File {args[0]} does not exist.");
            return;
        }

        var content = File.ReadAllText(args[0]);
        List<Token> tokens = Tokenize(content);
        foreach (var token in tokens)
        {
            Console.WriteLine($"Type: {token.Type}, Value: {token.Value}");
        }

    }
}