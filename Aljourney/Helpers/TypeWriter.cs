using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aljourney.Helpers
{
    class TypeWriter
    {
        private string inputString;
        private string outputString = "";
        private int charCount;

        public TypeWriter(string inputString)
        {
            this.inputString = inputString;
        }

        public void Update() 
        {
            charCount++;
        }

        public string typeLine() 
        {
            for(int i = 0; i < charCount; i++)
            {
                outputString += inputString[i];
            }
            return outputString;
        }


    }
}
