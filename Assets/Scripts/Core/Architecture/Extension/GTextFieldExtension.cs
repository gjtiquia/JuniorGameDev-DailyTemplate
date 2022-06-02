// Author: Eric Chan, Sunny Tsui, Andy Chow
// Date: 30/09/2021

using System;
using FairyGUI;
using UnityEngine;

namespace GTextFieldExtensionMethods
{
    /// <summary>
    /// Since fairygui does not provide break line function for GTextField,
    /// this extension aims to break lines when a leading character(s) is at the end of a line or a following character(s) is at the start of a line.
    /// Checking for English and Chinese is supported.
    /// 
    /// Usage:
    /// You need to call FormatContent in Setup_AfterAdd method of fairygui's GTextField.
    /// i.e
    /// if (str != null)
    /// {
    ///     this.text = str;
    ///     FormatContent();
    /// }
    /// 
    /// public void FormatContent()
    /// {
    ///     this.FormatContent(_textField);    
    /// }
    /// </summary>
    public static class GTextFieldExtension
    {
        private readonly static string followingChars = ")]｝〕〉》」』】〙〗〟’”｠»ヽヾーァィゥェォッャュョヮヵヶぁぃぅぇぉっゃゅょゎゕゖㇰㇱㇲㇳㇴㇵㇶㇷㇸㇹㇺㇻㇼㇽㇾㇿ々〻‐゠–〜?!‼⁇⁈⁉・、%,.:;。！？］）：；＝}¢°\"†‡℃〆％，．…⋯";
        private readonly static string leadingChars = "([｛〈〔《「『【〘〖〝‘“｟«$—‥〳〴〵\\［（{£¥\"々〇＄￥￦#";
        private readonly static string englishAlphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private static TextField _textField;

        public static void FormatContent(this GTextField gTextField, TextField _t)
        {
            _textField = _t;
            int tryCount = 0;
            string outputText = gTextField.text;
            bool hasChange = true;
            bool changedOnce = false;

            // Recursively check until no new line break is added
            while (hasChange)
            {
                // for safety measure
                if (tryCount >= 100)
                {
                    Debug.LogWarning($"Exceed Line breaker limit! Parsed text: {_textField.parsedText}");
                    return;
                }
                tryCount++;

                hasChange = false;
                for (int i = 0; i < _textField.lines.Count; i++)
                {
                    var line = _textField.lines[i];
                    if (line.charCount == 0) continue; //handle case with empty line

                    var firstChar = _textField.parsedText[line.charIndex].ToString();
                    var lastChar = _textField.parsedText[line.charIndex + line.charCount - 1].ToString();

                    // !!! Need to check leading character first
                    // Check if specific leadingChar is at the end of the line, add line break if necessary
                    if (leadingChars.Contains(lastChar))
                    {
                        CheckLeadingCharsToStart(ref outputText, ref hasChange, line);
                        if (hasChange)
                        {
                            changedOnce = true;
                            break;
                        }
                    }

                    if (_textField.lines.Count > 1 && englishAlphabet.Contains(lastChar) && i != _textField.lines.Count - 1)
                    {
                        CheckEnglishAlphabetToStart(ref outputText, ref hasChange, line);
                        if (hasChange)
                        {
                            changedOnce = true;
                            break;
                        }
                    }

                    if (line.charIndex - 1 < 0) continue;

                    // Check if specific followingChar is at the start of the line, add line break if necessary
                    if (followingChars.Contains(firstChar))
                    {
                        string previousCharacter = _textField.parsedText[line.charIndex - 1].ToString();
                        CheckFollowingCharsToStart(ref outputText, ref hasChange, line, GetLineBreaker(previousCharacter));
                        if (hasChange)
                        {
                            changedOnce = true;
                            break;
                        }
                    }
                }

                //if the text never change, we don't assign the textfield again.
                if (!changedOnce) return;
                // Update text display
                //gTextField.text = outputText;
                _textField.text = outputText;
            }
        }

        private static Func<int, string> GetLineBreaker(string previousCharacter)
        {
            Func<int, string> targetLineBreakCheck = null;
            // check is english
            if (englishAlphabet.Contains(previousCharacter))
            {
                targetLineBreakCheck = EnglishLineBreakCheck;
            }
            else
            {
                targetLineBreakCheck = ChineseLineBreakCheck;
            }
            return targetLineBreakCheck;
        }

        private static void CheckLeadingCharsToStart(ref string outputText, ref bool hasChange, TextField.LineInfo line)
        {
            for (int x = line.charIndex + line.charCount - 2; x > 0; x--)
            {
                if (leadingChars.Contains(_textField.parsedText[x].ToString())) continue;
                outputText = _textField.parsedText.Insert(x + 1, "\n");
                hasChange = true;
                break;
            }
        }

        private static void CheckEnglishAlphabetToStart(ref string outputText, ref bool hasChange, TextField.LineInfo line)
        {
            for (int x = line.charIndex + line.charCount - 2; x > 0; x--)
            {
                var character = _textField.parsedText[x].ToString();
                if (englishAlphabet.Contains(character)) continue;
                if (leadingChars.Contains(character)) continue;
                outputText = _textField.parsedText.Insert(x + 1, "\n");
                hasChange = true;
                break;
            }
        }

        private static void CheckFollowingCharsToStart(ref string outputText, ref bool hasChange, TextField.LineInfo line, Func<int, string> lineBreakCheck)
        {
            // loop from previous character to the start of the string 
            for (int x = line.charIndex - 1; x > 0; x--)
            {
                var character = _textField.parsedText[x].ToString();
                // if it is a leading character, ignore it　
                if (leadingChars.Contains(character)) continue;

                var output = lineBreakCheck(x);
                if (!String.IsNullOrEmpty(output))
                {
                    outputText = output;
                    hasChange = true;
                    break;
                }
            }
        }

        private static string EnglishLineBreakCheck(int x)
        {
            // if it is not english alphabet, add a line break
            if (englishAlphabet.Contains(_textField.parsedText[x].ToString())) return "";

            var outputText = _textField.parsedText.Insert(x + 1, "\n");
            return outputText;
        }

        private static string ChineseLineBreakCheck(int x)
        {
            // if it is a leading character, ignore it
            if (leadingChars.Contains(_textField.parsedText[x].ToString())) return "";

            var outputText = _textField.parsedText.Insert(x, "\n");
            return outputText;
        }
    }
}