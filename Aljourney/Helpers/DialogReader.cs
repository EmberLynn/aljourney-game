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

        public DialogReader() {}

        public List<string> getDialog(string fileToRead) {

            introFile = new System.IO.StreamReader
                   (fileToRead);

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
