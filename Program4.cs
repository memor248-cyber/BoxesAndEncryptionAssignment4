using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== نظام موازنة محتويات الصناديق والتشفير المبتكر ===");

        // اختبار المسألة الأولى: موازنة الصناديق
        int[,] boxes = new int[3, 4];
        InitializeBoxes(boxes);
        Console.WriteLine("\nمحتويات الصناديق قبل الموازنة:");
        PrintBoxes(boxes);

        BalanceRowWeights(boxes);
        Console.WriteLine("\nمحتويات الصناديق بعد الموازنة:");
        PrintBoxes(boxes);

        // اختبار المسألة الثانية: التشفير وفك التشفير
        string text = "Hello World";
        int shiftKey = 3;

        string encrypted = EncryptText(text, shiftKey);
        Console.WriteLine($"\nالنص الأصلي: {text}");
        Console.WriteLine($"النص المشفر: {encrypted}");

        string decrypted = DecryptText(encrypted, shiftKey);
        Console.WriteLine($"النص بعد فك التشفير: {decrypted}");
    }

    #region المسألة الأولى: نظام موازنة محتويات الصناديق

    static void InitializeBoxes(int[,] boxes)
    {
        Random rnd = new Random();

        for (int i = 0; i < boxes.GetLength(0); i++)
        {
            for (int j = 0; j < boxes.GetLength(1); j++)
            {
                boxes[i, j] = rnd.Next(-1, 51);
            }
        }
    }

    static void BalanceRowWeights(int[,] boxes)
    {
        for (int i = 0; i < boxes.GetLength(0); i++)
        {
            for (int j = 1; j < boxes.GetLength(1); j++)
            {
                if (boxes[i, j] > 30 && boxes[i, j - 1] == 0)
                {
                    boxes[i, j - 1] = boxes[i, j];
                    boxes[i, j] = 0;
                }
            }
        }
    }

    static void PrintBoxes(int[,] boxes)
    {
        for (int i = 0; i < boxes.GetLength(0); i++)
        {
            for (int j = 0; j < boxes.GetLength(1); j++)
            {
                Console.Write(boxes[i, j].ToString().PadLeft(5));
            }

            Console.WriteLine();
        }
    }

    #endregion

    #region المسألة الثانية: نظام تشفير النصوص المبتكر

    static string EncryptText(string text, int shiftKey)
    {
        int totalLength = text.Length;

        string[] words = text.Split(' ');

        for (int i = 0; i < words.Length; i++)
        {
            if (words[i].Length % 2 == 0)
                words[i] = ReverseString(words[i]);
        }

        string step1Text = string.Join(" ", words);

        char[] result = step1Text.ToCharArray();

        for (int i = 0; i < result.Length; i++)
        {
            char c = result[i];

            if (IsEnglishLetter(c))
            {
                if (i % 2 == 0)
                    c = ShiftForward(c, shiftKey);
                else
                    c = ShiftBackward(c, shiftKey);
            }

            if (IsVowel(c))
                c = (char)(c - totalLength);

            result[i] = c;
        }

        return new string(result);
    }

    static string DecryptText(string encryptedText, int shiftKey)
    {
        int totalLength = encryptedText.Length;

        char[] result = encryptedText.ToCharArray();

        // عكس استبدال الأحرف الصوتية بإضافة طول النص الكلي للـ ASCII
        for (int i = 0; i < result.Length; i++)
        {
            int originalValue = result[i] + totalLength;

            if (originalValue <= char.MaxValue && IsVowel((char)originalValue))
            {
                result[i] = (char)originalValue;
            }
        }

        // عكس الإزاحة الأبجدية
        for (int i = 0; i < result.Length; i++)
        {
            char c = result[i];

            if (IsEnglishLetter(c))
            {
                if (i % 2 == 0)
                    c = ShiftBackward(c, shiftKey);
                else
                    c = ShiftForward(c, shiftKey);
            }

            result[i] = c;
        }

        // عكس ترتيب الكلمات الزوجية لاستعادة حالتها الأصلية
        string[] words = new string(result).Split(' ');

        for (int i = 0; i < words.Length; i++)
        {
            if (words[i].Length % 2 == 0)
                words[i] = ReverseString(words[i]);
        }

        return string.Join(" ", words);
    }

    // الطرق المساعدة (Helper Methods)

    static string ReverseString(string word)
    {
        char[] chars = word.ToCharArray();
        Array.Reverse(chars);
        return new string(chars);
    }

    static char ShiftForward(char c, int shiftKey)
    {
        shiftKey = ((shiftKey % 26) + 26) % 26;

        if (c >= 'a' && c <= 'z')
            return (char)('a' + (c - 'a' + shiftKey) % 26);

        if (c >= 'A' && c <= 'Z')
            return (char)('A' + (c - 'A' + shiftKey) % 26);

        return c;
    }

    static char ShiftBackward(char c, int shiftKey)
    {
        shiftKey = ((shiftKey % 26) + 26) % 26;

        if (c >= 'a' && c <= 'z')
            return (char)('a' + (c - 'a' - shiftKey + 26) % 26);

        if (c >= 'A' && c <= 'Z')
            return (char)('A' + (c - 'A' - shiftKey + 26) % 26);

        return c;
    }

    static bool IsVowel(char c)
    {
        return "aeiouAEIOU".IndexOf(c) >= 0;
    }

    static bool IsEnglishLetter(char c)
    {
        return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z');
    }

    #endregion
  }
