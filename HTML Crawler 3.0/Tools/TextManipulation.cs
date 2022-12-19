using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTML_Crawler_3._0.Tools
{
    class TextManipulation
    {
        public string[] Split(string input, char separator)
        {
            if (input == null)
                throw new NullReferenceException("");

            int sepCount = 0;

            foreach (char c in input)
            {
                if (c == separator)
                    sepCount++;
            }

            string[] splitInput = new string[sepCount + 1];
            if (sepCount == 0)
            {
                splitInput[0] = input;
                return splitInput;
            }

            string current = "";
            int postion = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] != separator && i != input.Length - 1)
                {
                    current += input[i];
                }
                else if (i == input.Length - 1 && input[i] != separator)
                {
                    current += input[i];
                    splitInput[postion] = current;
                }
                else
                {
                    if (current != "")
                    {
                        splitInput[postion] = current;
                        postion++;
                        current = "";
                    }
                }
            }
            return splitInput;
        }

        public string Trim(string input)
        {
            input = TrimStart(input);
            input = TrimEnd(input);

            return input;
        }
        public string TrimStart(string input)
        {
            int firstLetterCounter = 0;
            string newInput = "";
            foreach (char c in input)
            {
                if ((c >= '!' || c <= '~') && c != ' ' && c != '\r')
                {
                    firstLetterCounter = 1;
                }
                if (firstLetterCounter == 1)
                {
                    newInput += c;
                }
            }
            return newInput;
        }

        public string TrimEnd(string input)
        {
            int firstLetterCounter = 0;
            string newInput = "";
            for (int i = input.Length - 1; i >= 0; i--)
            {
                if ((input[i] >= '!' || input[i] <= '~') && input[i] != ' ' && input[i] != '\r')
                {
                    firstLetterCounter = 1;
                }
                if (firstLetterCounter == 1)
                {
                    newInput += input[i];
                }

            }

            newInput = Reverse(newInput);
            return newInput;
        }

        public string Reverse(string input)
        {
            char[] arr = new char[input.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = input[i];
            }
            for (int i = 0; i < arr.Length / 2; i++)
            {
                char temp = arr[i];
                arr[i] = arr[arr.Length - i - 1];
                arr[arr.Length - i - 1] = temp;

            }
            string reversed = "";
            foreach (char c in arr)
            {
                reversed += c;
            }
            return reversed;
        }

        public bool Contains(string input, char character)
        {
            foreach (char c in input)
            {
                if (character == c)
                {
                    return true;
                }
            }
            return false;
        }

        public string[] SetInputSeparataor(string input)
        {
            string[] inputArr = new string[3];
            int counter = 0;
            string wordToAdd = "";
            for (int i = 0; i <= input.Length - 1; i++)
            {
                if (i == input.Length - 1)
                {
                    wordToAdd += input[i];
                    inputArr[counter++] = wordToAdd;
                    break;
                }
                else if (input[i] == ' ' && input[i + 1] == '\"')
                {
                    inputArr[counter++] = wordToAdd;
                    wordToAdd = "";
                }
                else
                {
                    wordToAdd += input[i];

                }
            }
            return inputArr;
        }
        public bool HasSRC(string input)
        {
            string cT = "";
            for (int i = 0; i < 3; i++)
            {
                cT += input[i];
            }
            if (cT == "src")
                return true;
            return false;
        }


        //TO DO: ToLowerCase()
    }
}
