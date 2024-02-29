using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1.Packets
{
    public class serverMessage
    {
        public StringBuilder stringBuilder = new StringBuilder();

        public serverMessage(string message)
        {
            stringBuilder.Append(message);
        }
        public void Init(string stringB)
        {
            stringBuilder.Append(stringB);
        }
        public void AppendString(string stringB)
        {
            stringBuilder.Append((Char)13);
            stringBuilder.Append(stringB);
        }
        public void AppendInteger(int intB)
        {
            stringBuilder.Append((Char)13);
            stringBuilder.Append(intB);
        }
        public void Append(string stringB)
        {
            stringBuilder.Append(stringB);
        }
        public void Append(int intB)
        {
            stringBuilder.Append(intB);
        }
        public void AppendChar(int i)
        {
            stringBuilder.Append((Char)i);
        }
        public override string ToString()
        {
            return stringBuilder.ToString();
        }
    }
}
