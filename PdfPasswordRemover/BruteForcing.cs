using System;
using System.Collections;
using System.Text;

namespace PdfPasswordRemover
{
    public class Bruteforcing : IBruteforcing, IEnumerable
    {
        private ulong _len;
        private StringBuilder _sb = new StringBuilder();

        public Bruteforcing(string charset, int min, int max)
        {
            Charset = charset;
            Max = max;
            Min = min;
        }

        public string Charset { get; set; }

        public int Max { get; set; }

        public int Min { get; set; }

        public IEnumerator GetEnumerator()
        {
            _len = (ulong) Charset.Length;
            for (double x = Min; x <= Max; x++)
            {
                var total = (ulong) Math.Pow(Charset.Length, x);
                ulong counter = 0;
                while (counter < total)
                {
                    var a = Factoradic(counter, x - 1);
                    yield return a;
                    counter++;
                }
            }
        }

        private string Factoradic(ulong l, double power)
        {
            _sb.Length = 0;
            while (power >= 0)
            {
                _sb = _sb.Append(Charset[(int) (l % _len)]);
                l /= _len;
                power--;
            }
            return _sb.ToString();
        }
    }
}