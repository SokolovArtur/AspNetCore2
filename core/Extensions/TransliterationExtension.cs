using System.Collections.Generic;
using System.Text;

namespace Tochka.Extensions
{
    /// <summary>
    /// Cyrillic-latin transliteration (support only slavik languages) by GOST 7.79-2000 (ISO 9).
    /// </summary>
    public static class TransliterationExtension
    {
        /// <summary>
        /// Transliterate cyrillic string to latin.
        /// </summary>
        /// <param name="cyrillicSource">Source string.</param>
        /// <returns>Transliterated string.</returns>
        public static string CyrillicToLatin(string cyrillicSource)
        {
            return new CyrillicToLatinConverter(cyrillicSource)
                .Convert();
        }

        /// <summary>
        /// Transliterate latin string to cyrillic.
        /// </summary>
        /// <param name="latinSource">Source string.</param>
        /// <returns>Cyrillic string.</returns>
        public static string LatinToCyrillic(string latinSource)
        {
            return new LatinToCyrillicConverter(latinSource)
                .Convert();
        }
    }

    internal struct CyrillicToLatinConverter
    {
        private readonly Dictionary<char, string> _ruleSet;
        private readonly string _src;

        private StringBuilder _sb;

        /// <summary>
        /// Create an instance of algorithm.
        /// </summary>
        public CyrillicToLatinConverter(string source)
        {
            _ruleSet = new Dictionary<char, string>
            {
                {'а', @"a"},
                {'А', @"A"},
                {'б', @"b"},
                {'Б', @"B"},
                {'в', @"v"},
                {'В', @"V"},
                {'г', @"g"},
                {'Г', @"G"},
                {'д', @"d"},
                {'Д', @"D"},
                {'е', @"e"},
                {'Е', @"E"},
                {'ё', @"yo"},
                {'Ё', @"Yo"},
                {'ж', @"zh"},
                {'Ж', @"Zh"},
                {'з', @"z"},
                {'З', @"Z"},
                {'и', @"i"},
                {'И', @"I"},
                {'й', @"j"},
                {'Й', @"J"},
                {'i', @"i"},
                {'I', @"I"},
                {'к', @"k"},
                {'К', @"K"},
                {'л', @"l"},
                {'Л', @"L"},
                {'м', @"m"},
                {'М', @"M"},
                {'н', @"n"},
                {'Н', @"N"},
                {'о', @"o"},
                {'О', @"O"},
                {'п', @"p"},
                {'П', @"P"},
                {'р', @"r"},
                {'Р', @"R"},
                {'с', @"s"},
                {'С', @"S"},
                {'т', @"t"},
                {'Т', @"T"},
                {'у', @"u"},
                {'У', @"U"},
                {'ф', @"f"},
                {'Ф', @"F"},
                {'х', @"x"},
                {'Х', @"X"},
                {'ц', @"cz"},
                {'Ц', @"Cz"},
                {'ч', @"ch"},
                {'Ч', @"Ch"},
                {'ш', @"sh"},
                {'Ш', @"Sh"},
                {'щ', @"shh"},
                {'Щ', @"Shh"},
                {'ъ', @"``"},
                {'Ъ', @"``"},
                {'ы', @"y`"},
                {'Ы', @"Y`"},
                {'ь', @"`"},
                {'Ь', @"`"},
                {'э', @"e`"},
                {'Э', @"E`"},
                {'ю', @"yu"},
                {'Ю', @"Yu"},
                {'я', @"ya"},
                {'Я', @"Ya"},
                {'’', @"'"},
                {'ѣ', @"ye"},
                {'Ѣ', @"Ye"},
                {'ѳ', @"fh"},
                {'Ѳ', @"Fh"},
                {'ѵ', @"yh"},
                {'Ѵ', @"Yh"},
            };
            _src = source;

            _sb = null;
        }

        /// <summary>
        /// Should be invoked only once.
        /// </summary>
        public string Convert()
        {
            if (string.IsNullOrEmpty(_src))
            {
                return _src;
            }

            _sb = new StringBuilder();

            for (var srcIndex = 0; srcIndex < _src.Length; srcIndex++)
            {
                string substitute;
                if (_ruleSet.TryGetValue(_src[srcIndex], out substitute))
                {
                    var nextChar = (_src.Length > (srcIndex + 1)) ? _src[srcIndex + 1] : ' ';
                    substitute = CheckSpecificRules(substitute, nextChar);
                    _sb.Append(substitute);
                }
                else
                {
                    _sb.Append(_src[srcIndex]);
                }
            }

            return _sb.ToString();
        }

        private string CheckSpecificRules(string substitue, char nextSourceChar)
        {
            if ((substitue.Length != 2) || (substitue[1] != 'z'))
            {
                return substitue;
            }

            switch (nextSourceChar)
            {
                case 'Е':
                case 'Ё':
                case 'И':
                case 'Й':
                case 'I':
                case 'Ы':
                case 'Э':
                case 'Ю':
                case 'Я':
                case 'е':
                case 'ё':
                case 'и':
                case 'й':
                case 'i':
                case 'ы':
                case 'э':
                case 'ю':
                case 'я':
                case 'ѣ':
                case 'Ѣ':
                case 'ѵ':
                case 'Ѵ':
                    return substitue.Substring(0, 1);
                default:
                    return substitue;
            }
        }
    }

    internal struct LatinToCyrillicConverter
    {
        private readonly ConvertRule[] _ruleSet;
        private readonly string _src;

        /// <summary>
        /// Create an instance of algorithm.
        /// </summary>
        public LatinToCyrillicConverter(string source)
        {
            _ruleSet = new[] // ru-RU
            {
                new ConvertRule("щ", @"shh"),
                new ConvertRule("Щ", @"Shh"),
                new ConvertRule("ё", @"yo"),
                new ConvertRule("Ё", @"Yo"),
                new ConvertRule("ж", @"zh"),
                new ConvertRule("Ж", @"Zh"),
                new ConvertRule("ц", @"cz"),
                new ConvertRule("Ц", @"Cz"),
                new ConvertRule("ч", @"ch"),
                new ConvertRule("Ч", @"Ch"),
                new ConvertRule("ш", @"sh"),
                new ConvertRule("Ш", @"Sh"),
                new ConvertRule("ъ", @"``"),
                new ConvertRule("Ъ", @"``"),
                new ConvertRule("ы", @"y`"),
                new ConvertRule("Ы", @"Y`"),
                new ConvertRule("э", @"e`"),
                new ConvertRule("Э", @"E`"),
                new ConvertRule("ю", @"yu"),
                new ConvertRule("Ю", @"Yu"),
                new ConvertRule("я", @"ya"),
                new ConvertRule("Я", @"Ya"),
                new ConvertRule("ѣ", @"ye"),
                new ConvertRule("Ѣ", @"Ye"),
                new ConvertRule("ѳ", @"fh"),
                new ConvertRule("Ѳ", @"Fh"),
                new ConvertRule("ѵ", @"yh"),
                new ConvertRule("Ѵ", @"Yh"),
                new ConvertRule("а", @"a"),
                new ConvertRule("А", @"A"),
                new ConvertRule("б", @"b"),
                new ConvertRule("Б", @"B"),
                new ConvertRule("в", @"v"),
                new ConvertRule("В", @"V"),
                new ConvertRule("г", @"g"),
                new ConvertRule("Г", @"G"),
                new ConvertRule("д", @"d"),
                new ConvertRule("Д", @"D"),
                new ConvertRule("е", @"e"),
                new ConvertRule("Е", @"E"),
                new ConvertRule("з", @"z"),
                new ConvertRule("З", @"Z"),
                new ConvertRule("и", @"i"),
                new ConvertRule("И", @"I"),
                new ConvertRule("й", @"j"),
                new ConvertRule("Й", @"J"),
                new ConvertRule("i", @"i"),
                new ConvertRule("I", @"I"),
                new ConvertRule("к", @"k"),
                new ConvertRule("К", @"K"),
                new ConvertRule("л", @"l"),
                new ConvertRule("Л", @"L"),
                new ConvertRule("м", @"m"),
                new ConvertRule("М", @"M"),
                new ConvertRule("н", @"n"),
                new ConvertRule("Н", @"N"),
                new ConvertRule("о", @"o"),
                new ConvertRule("О", @"O"),
                new ConvertRule("п", @"p"),
                new ConvertRule("П", @"P"),
                new ConvertRule("р", @"r"),
                new ConvertRule("Р", @"R"),
                new ConvertRule("с", @"s"),
                new ConvertRule("С", @"S"),
                new ConvertRule("т", @"t"),
                new ConvertRule("Т", @"T"),
                new ConvertRule("у", @"u"),
                new ConvertRule("У", @"U"),
                new ConvertRule("ф", @"f"),
                new ConvertRule("Ф", @"F"),
                new ConvertRule("х", @"x"),
                new ConvertRule("Х", @"X"),
                new ConvertRule("ц", @"c"),
                new ConvertRule("Ц", @"C"),
                new ConvertRule("ь", @"`"),
                new ConvertRule("Ь", @"`"),
                new ConvertRule("’", @"'"),
            };
            _src = source;
        }

        /// <summary>
        /// Detransliterate source. Should be invoked only once.
        /// </summary>
        /// <returns>Detransliterated cyrillic string.</returns>
        public string Convert()
        {
            if (string.IsNullOrEmpty(_src))
            {
                return _src;
            }

            var result = _src;
            for (var i = 0; i < _ruleSet.Length; i++)
            {
                result = result.Replace(_ruleSet[i].Latin, _ruleSet[i].Cyrillic);
            }
            return result;
        }

        private struct ConvertRule
        {
            public readonly string Latin;
            public readonly string Cyrillic;

            public ConvertRule(string cyrillic, string latin)
            {
                Cyrillic = cyrillic;
                Latin = latin;
            }
        }
    }
}
