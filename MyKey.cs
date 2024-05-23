using MassTransit.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Klasy
{
    public class MyKey : SymmetricKey
    {
        public byte[] Key { get; set; }
        public byte[] IV { get; set; }
    }
}