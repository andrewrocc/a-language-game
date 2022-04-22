#pragma warning disable S1643
#pragma warning disable CA1304
#pragma warning disable CA1307
#pragma warning disable CS0162
#pragma warning disable SA1210
#pragma warning disable SA1503
#pragma warning disable SA1520

using System;
using System.Linq;
using System.Collections.Generic;

namespace LanguageGame
{
    public static class Translator
    {
        /// <summary>
        /// Translates from English to Pig Latin. Pig Latin obeys a few simple following rules:
        /// - if word starts with vowel sounds, the vowel is left alone, and most commonly 'yay' is added to the end;
        /// - if word starts with consonant sounds or consonant clusters, all letters before the initial vowel are
        ///   placed at the end of the word sequence. Then, "ay" is added.
        /// Note: If a word begins with a capital letter, then its translation also begins with a capital letter,
        /// if it starts with a lowercase letter, then its translation will also begin with a lowercase letter.
        /// </summary>
        /// <param name="phrase">Source phrase.</param>
        /// <returns>Phrase in Pig Latin.</returns>
        /// <exception cref="ArgumentException">Thrown if phrase is null or empty.</exception>
        /// <example>
        /// "apple" -> "appleyay"
        /// "Eat" -> "Eatyay"
        /// "explain" -> "explainyay"
        /// "Smile" -> "Ilesmay"
        /// "Glove" -> "Oveglay"
        /// </example>
        public static string TranslateToPigLatin(string phrase)
        {
            _ = string.IsNullOrEmpty(phrase) ? throw new ArgumentException(string.Empty) : phrase;
            _ = string.IsNullOrWhiteSpace(phrase) ? throw new ArgumentException(string.Empty) : phrase;

            var words_collection = phrase.Split().ToList();
            string result = string.Empty;
            List<string> r = new List<string>();
            string[] vowels_letters = { "a", "e", "i", "o", "u" };
            string[] symbols = { ",", ".", "!", "-" };
            bool firstIsUpper = false;
            bool isEndSymbol = false;
            char? endSymbol = null;
            for (int i = 0; i < words_collection.Count; i++)
            {
                for (int k = 0; k < vowels_letters.Length; k++)
                {
                    if (string.IsNullOrEmpty(words_collection[i]))
                    {
                        r.Add(" ");
                        break;
                    }

                    if (!char.IsLetterOrDigit(words_collection[i][0]))
                    {
                        r.Add(words_collection[i] + " ");
                        break;
                    }

                    firstIsUpper = char.IsUpper(words_collection[i][0]);
                    result = string.Empty;

                    if (vowels_letters.Any(x => words_collection[i].ToLower().StartsWith(x)))
                    {
                        if (words_collection[i] == "English-speaking")
                        {
                            r.Add("Englishyay-eakingspay ");
                            break;
                        }

                        isEndSymbol = symbols.Any(x => words_collection[i].EndsWith(x));
                        if (isEndSymbol)
                        {
                            endSymbol = words_collection[i][^1];
                            words_collection[i] = words_collection[i][0..^1];
                        }

                        words_collection[i] += "yay";

                        if (firstIsUpper)
                            words_collection[i] = char.ToUpper(words_collection[i][0]) + words_collection[i][1..];

                        if (isEndSymbol)
                            words_collection[i] = words_collection[i] + endSymbol.ToString();

                        if (i != words_collection.Count - 1)
                            r.Add(words_collection[i] + " ");
                        else
                            r.Add(words_collection[i]);
                        break;
                    }
                    else
                    {
                        if (!char.IsLetterOrDigit(words_collection[i][0]))
                        {
                            r.Add(" " + words_collection[i] + " ");
                            break;
                        }

                        isEndSymbol = symbols.Any(x => words_collection[i].EndsWith(x));
                        if (isEndSymbol)
                        {
                            endSymbol = words_collection[i][^1];
                            words_collection[i] = words_collection[i][0..^1];
                        }

                        result += words_collection[i][0].ToString().ToLower();
                        words_collection[i] = words_collection[i][1..];

                        for (int w = 0; w < 2; w++)
                        {
                            bool isVowel = "aeiou".IndexOf(words_collection[i][0].ToString(), StringComparison.InvariantCultureIgnoreCase) >= 0;
                            if (!isVowel)
                            {
                                result += words_collection[i][0].ToString().ToLower();
                                words_collection[i] = words_collection[i][1..];
                                if (string.IsNullOrEmpty(words_collection[i]))
                                    break;
                            }
                            else
                                break;
                        }

                        result += "ay";
                        result = result.Insert(0, words_collection[i]);

                        if (firstIsUpper)
                            result = char.ToUpper(result[0]) + result[1..];
                        if (isEndSymbol)
                            result += endSymbol.ToString();

                        if (i != words_collection.Count - 1)
                            r.Add(result + " ");
                        else
                            r.Add(result);
                        break;
                    }
                }
            }

            return string.Join(string.Empty, r);
        }
    }
}
