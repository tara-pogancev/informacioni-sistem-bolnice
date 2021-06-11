using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace SIMS.Service
{
    class FeedbackService
    {
        internal void Send(string message)
        {
            SaveFileDialog save = new SaveFileDialog
            {
                FileName = "ApplicationFeedback.txt",
                Filter = "Text File | *.txt"
            };

            if (save.ShowDialog() == DialogResult.OK)
            {

                StreamWriter writer = new StreamWriter(save.OpenFile());

                writer.WriteLine(message);
                writer.Dispose();
                writer.Close();

            }
        }
    }
}
