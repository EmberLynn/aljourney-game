using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aljourney.Helpers
{
    //reads Dialog files from Dialogs folder
    class DialogReader
    {
        private List<string> introDialog = new List<string>();
        private string line;
        private string finalLine;
        System.IO.StreamReader introFile;

        public DialogReader() {
            introFile = new System.IO.StreamReader
                   (@"C:\Users\ember\source\repos\Aljourney\Aljourney\Dialogs\IntroDialog.txt");
        }

        public List<string> getDialog() {

            while ((line = introFile.ReadLine()) != null)
            {
                if (line.Equals("_B"))
                {
                    introDialog.Add(finalLine);
                    line = introFile.ReadLine();
                    finalLine = "";
                }
                finalLine += line + "\n";
            }

            return introDialog;
        }
    }
}
