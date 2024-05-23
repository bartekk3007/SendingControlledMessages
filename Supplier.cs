using MassTransit.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Klasy
{
    public class Supplier : ISymmetricKeyProvider
    {
        private string k;
        public Supplier(string _k)
        { k = _k; }
        public bool TryGetKey(string keyId, out SymmetricKey key)
        {
            var sk = new MyKey();
            sk.IV = Encoding.ASCII.GetBytes(keyId.Substring(0, 16));
            sk.Key = Encoding.ASCII.GetBytes(k); key = sk;
            Console.WriteLine($"\nSzyfrowanie z różnym wektorem inicjalizujacym: {BitConverter.ToString(sk.IV)}");
            return true;
        }
    }
}