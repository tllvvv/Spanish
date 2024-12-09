using System.Text;
using System.Threading.Tasks.Dataflow;

namespace Spanish;

public class Translator
{
    private readonly string[] _oneToTwenty =
       [string.Empty, "un", "dos", "tres", "cuatro", "cinco", "seis", "siete",
        "ocho", "nueve", "diez", "once", "doce", "trece", "catorce", "quince",
        "dieciséis", "diecisiete", "dieciocho", "diecinueve", "veinte"];

    private readonly string[] _twentyToNinety =
        ["veinti", "treinta", "cuarenta", "cincuenta", "sesenta", "setenta",
         "ochenta", "noventa"];

    private readonly string[] _hundredsAndMore =
    ["cien", "ciento", "doscient", "trescient", "cuatrocient", "quinient",
     "seiscient", "setecient", "ochocient", "novecient", "mil", "millón"];

    public string TranslateNumber(int inputNumber, char inputSex)
    {
        int maxValue = 999999999;
        int minValue = 0;
        char validSexFem = 'f';
        char validSexMas = 'm';

        if (!(minValue <= inputNumber && inputNumber <= maxValue) || !(inputSex == validSexMas || inputSex == validSexFem))
        {
            return "Value is invalid";
        }

        StringBuilder writingNumber = new();

        if (inputNumber == 0)
        {
            return "cero";
        }

        int rank = 1000000;
        int current = inputNumber;

        while (rank > 0)
        {
            int currentDigit = current / rank;

            if (currentDigit == 0)
            {
                rank /= 1000;
                continue;
            }
            else
            {
                int hundred = currentDigit / 100;
                int tens = currentDigit % 100;
                int unit = (currentDigit % 100) % 10;

                TranslateHundred(writingNumber, hundred, rank, tens, inputSex);

                if (tens <= 20)
                {
                    TranslateTens(writingNumber, tens, rank, hundred, inputSex);
                }
                else if (tens > 20 && tens < 30)
                {
                    TranslateTwenties(writingNumber, unit, rank, inputSex);
                }
                else
                {
                    TranslateBigNum(writingNumber, tens, unit, rank, inputSex);
                }

                TranslateMillionAndHundred(rank, writingNumber, inputNumber);
            }

            current %= rank;
            rank /= 1000;
        }

        CurrentSpace(writingNumber);
        return writingNumber.ToString();
    }

    private static void CurrentSpace(StringBuilder sb)
    {
        sb.Replace("  ", " ");
        sb.Replace("  ", " ");

        if (sb[0] == ' ')
        {
            sb.Remove(0, 1);
        }

        if (sb[^1] == ' ')
        {
            sb.Remove(sb.Length - 1, 1);
        }
    }

    private void TranslateMillionAndHundred(int rank, StringBuilder sb, int num)
    {
        if (rank == 1000000)
        {
            sb.Append(_hundredsAndMore[11] + (num / rank != 1 ? "es" : string.Empty) + " ");
        }
        else if (rank == 1000)
        {
            sb.Append(_hundredsAndMore[10] + " ");
        }
    }

    private void TranslateTens(StringBuilder sb, int tens, int rank, int hundred, char sex)
    {
        string firstTer = tens == 1 && rank == 1000 && hundred == 0
            ? string.Empty
            : _oneToTwenty[tens];

        string secondTer = tens == 1 && sex == 'f' && (rank == 1 || (rank == 1000 && hundred != 0))
            ? "a"
            : sex == 'm' && rank == 1 && tens == 1
            ? "o"
            : string.Empty;

        sb.Append(firstTer + secondTer + " ");
    }

    private void TranslateTwenties(StringBuilder sb, int unit, int rank, char sex)
    {
        var firstTer = rank == 1 && sex == 'm' && unit == 1
            ? "o"
            : string.Empty;

        var secondTer = rank < 1000000 && unit == 1 && sex == 'f'
            ? "a"
            : string.Empty;

        sb.Append("veinti" + _oneToTwenty[unit] + firstTer + secondTer + " ");
    }

    private void TranslateBigNum(StringBuilder sb, int tens, int unit, int rank, char sex)
    {
        var firstTer = unit == 0
            ? string.Empty
            : " y ";

        var secondTer = rank == 1 && sex == 'm' && unit == 1
            ? "o"
            : sex == 'f' && rank < 1000000 && unit == 1
            ? "a"
            : string.Empty;

        sb.Append(_twentyToNinety[(tens / 10) - 2] + firstTer + _oneToTwenty[unit] + secondTer + " ");
    }

    private void TranslateHundred(StringBuilder sb, int hundred, int rank, int tens, char sex)
    {
        var firstTer = rank == 1000000 || sex == 'm' ? "os" : "as";
        var secondTer = hundred == 1
            ? (tens == 0
            ? _hundredsAndMore[0]
            : _hundredsAndMore[1])
            : hundred > 1
            ? (_hundredsAndMore[hundred] + firstTer)
            : string.Empty;

        sb.Append(secondTer + " ");
    }
}
