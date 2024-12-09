using System.Globalization;
using Humanizer;

namespace Spanish;

public class Program
{
    private static void Main()
    {
        int ansNumber = GetNumber();
        char ansSex = GetSex();
        var humanizerSex = ansSex == 'm' ? GrammaticalGender.Masculine : GrammaticalGender.Feminine;
        Translator translator = new();

        Console.WriteLine($"\nHumanizer: {ansNumber.ToWords(humanizerSex, new CultureInfo("es-ES_tradnl"))}");
        Console.WriteLine($"My Answer: {translator.TranslateNumber(ansNumber, ansSex)}");

        /*char[] sexx = { 'f', 'm' };

        foreach (char sex in sexx)
        {
            Translator translator = new Translator();
            var humanizerSex = sex == 'm' ? GrammaticalGender.Masculine : GrammaticalGender.Feminine;
            bool flag = false;
            for (int i = 0; i <= 999999999; i++)
            {
                int number = i;
                string a = number.ToWords(humanizerSex, new CultureInfo("es-ES_tradnl"));
                string b = translator.TranslateNumber(number, sex);
                if (a.Length != b.Length)
                {
                    Console.WriteLine($"{sex}, {number}");
                    Console.WriteLine($"{a}");
                    Console.WriteLine($"{b}");
                    flag = true;
                    break;
                }

                for (int j = 0; j < a.Length; j++)
                {
                    bool chechcur = a[j] == b[j] || Math.Abs((int)a[j] - (int)b[j]) == 132 || Math.Abs((int)a[j] - (int)b[j]) == 133;
                    if (!chechcur)
                    {
                        Console.WriteLine($"{sex}, {number}");
                        Console.WriteLine($"{a}.");
                        Console.WriteLine($"{b}.");
                        flag = true;
                        break;
                    }
                }

                if (flag)
                {
                    break;
                }
            }
        }*/
    }

    private static int GetNumber()
    {
        int number;
        bool inputCorrectnessFlag = false;
        const int minCurrectNumber = 0;
        const int maxCurrectNumber = 999999999;

        do
        {
            if (!inputCorrectnessFlag)
            {
                Console.WriteLine("Enter a number from 0 to 999.999.999:");
            }
            else
            {
                Console.WriteLine("Sorry, your number is incorrect :( try again:");
            }

            inputCorrectnessFlag = true;
        }
        while (!(int.TryParse(Console.ReadLine(), out number) && number >= minCurrectNumber && number <= maxCurrectNumber));

        return number;
    }

    private static char GetSex()
    {
        char sex;
        bool isinputCorrectness = false;
        const char currectFemaleSex = 'f';
        const char currectMaleSex = 'm';

        do
        {
            if (!isinputCorrectness)
            {
                Console.WriteLine("Select sex using f or m:");
            }
            else
            {
                Console.WriteLine("Sorry, your sex is incorrect :( try again:");
            }

            isinputCorrectness = true;
        }
        while (!(char.TryParse(Console.ReadLine(), out sex) && (sex == currectFemaleSex || sex == currectMaleSex)));

        return sex;
    }
}
