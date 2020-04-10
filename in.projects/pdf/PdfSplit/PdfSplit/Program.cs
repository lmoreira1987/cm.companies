using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace PdfSplit
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            string tryAgain = String.Empty;

            do
            {
                MainProgram();
                tryAgain = TryAgain();

            } while (tryAgain.ToLower() == "y" || tryAgain.ToLower() == "yes");
        }

        private static string TryAgain()
        {
            string tryAgain;
            Console.WriteLine("Do you want to run it again?");
            Console.WriteLine("Y - Yes");
            Console.WriteLine("N - No");

            tryAgain = Console.ReadLine();

            return tryAgain;
        }

        private static void MainExceptionMessage(Exception e)
        {
            Console.WriteLine("\nFAIL\n");
            Console.WriteLine("Error: " + e.Message);
            Console.WriteLine("\n-----\n");
            Console.WriteLine("Something went wrong. Read the instructions and try again");
        }

        private static string GetOutputFolder()
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                return fbd.SelectedPath;
            }
            else
            {
                // TODO
                throw new Exception("Try again - loop");
            }
        }

        private static string GetInputFilename()
        {
            string fileName = String.Empty;
            OpenFileDialog fd = new OpenFileDialog();
            if (fd.ShowDialog() == DialogResult.OK)
                fileName = fd.FileName;
            return fileName;
        }

        private static void MainProgram()
        {
            try
            {
                Console.Clear();
                Program program = new Program();
                int interval = 1;

                InitialSetupMessage();

                string pdfInputName = PdfInputName(program);

                PdfReader reader = new PdfReader(pdfInputName);

                int numberOfPages = NumberOfPages(reader);

                Console.WriteLine("Number of Pages: " + numberOfPages);

                Console.Write("Output folder: ");
                string outputFolder = GetOutputFolder();
                Console.WriteLine(outputFolder);

                Console.WriteLine("\n--- New files --- ");

                for (int pageNumber = 1; pageNumber <= numberOfPages; pageNumber += interval)
                {
                    var name = program.ReadPdfFile(pdfInputName, pageNumber);
                    program.SplitAndSaveInterval(pdfInputName, pageNumber, interval, name, outputFolder);

                    Console.WriteLine(string.Format("{0} - {1}", pageNumber, name));
                }

                Console.WriteLine("\nSUCESS\n");
            }
            catch (Exception e)
            {
                MainExceptionMessage(e);
            }
        }

        private static int NumberOfPages(PdfReader reader)
        {
            int numberOfPages = reader.NumberOfPages;

            if (numberOfPages <= 0)
                throw new Exception("There are no pages to be splitted.");

            return numberOfPages;
        }

        private static string PdfInputName(Program obj)
        {
            Console.Write("\nPDF Input file: ");

            string pdfInputName = obj.ValidName(GetInputFilename());
            Console.Write(pdfInputName + "\n");
            pdfInputName = pdfInputName.Contains(".pdf") ? pdfInputName : string.Format("{0}.pdf", pdfInputName);

            return pdfInputName;
        }

        private string ValidName(string name)
        {
            var filenameSplitted = name.Split('\\');
            string filename = filenameSplitted[filenameSplitted.Length - 1].Replace(".pdf", "");

            if (string.IsNullOrEmpty(filename))
                ErrorMessage("Input filename is blank");
            else if (filename.Contains(" "))
                ErrorMessage("Input filename contains a blank space. Please remove this blank space and try again.");
            else if (!new Regex("^[a-zA-Z0-9 ]*$").IsMatch(filename))
                ErrorMessage("Filename format is not valid. It may NOT contains special characters.");

            return name;
        }

        private static void InitialSetupMessage()
        {
            Console.WriteLine("*** PDF Splitter ***");
            Console.WriteLine("Splits a single PDF file with many pages to one file per page\n");
        }

        private void SplitAndSaveInterval(string pdfFilePath, int startPage, int interval, string pdfFileName, string outputFolder)
        {
            using (PdfReader reader = new PdfReader(pdfFilePath))
            {
                Document document = new Document();
                PdfCopy copy = new PdfCopy(document, new FileStream(outputFolder + "\\" + pdfFileName + ".pdf", FileMode.Create));

                document.Open();

                for (int pagenumber = startPage; pagenumber < (startPage + interval); pagenumber++)
                {
                    if (reader.NumberOfPages >= pagenumber)
                    {
                        copy.AddPage(copy.GetImportedPage(reader, pagenumber));
                    }
                    else
                    {
                        break;
                    }
                }

                document.Close();
            }
        }

        public string ReadPdfFile(string fileName, int startPage)
        {
            StringBuilder text = new StringBuilder();
            string orderNumber = String.Empty;
            string nifNumber = String.Empty;
            string message = String.Empty;

            if (File.Exists(fileName))
            {
                PdfReader pdfReader = new PdfReader(fileName);

                ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
                string currentText = PdfTextExtractor.GetTextFromPage(pdfReader, startPage, strategy);

                currentText = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(currentText)));
                if (string.IsNullOrEmpty(currentText)) ErrorMessage(currentText);

                var splitValues = currentText.Split(new char[] { '\n' });
                for (int i = 0; i < splitValues.Length; i++)
                {
                    if (splitValues[i] == "Nº Ordem")
                    {
                        orderNumber = splitValues[i + 1].Replace(" ", "");
                        if (string.IsNullOrEmpty(orderNumber)) ErrorMessage("There is no ORDER NUMBER");
                    }

                    if (splitValues[i] == "Nº Contribuinte")
                    {
                        nifNumber = splitValues[i + 1].Replace(" ", "");
                        if (string.IsNullOrEmpty(nifNumber)) ErrorMessage("There is no NIF");
                    }
                }

                text.Append(currentText);
                pdfReader.Close();
            }
            else
            {
                ErrorMessage("File does not exists");
            }

            return nifNumber + '-' + orderNumber;
        }

        private static void ErrorMessage(string message)
        {
            if (!string.IsNullOrEmpty(message))
                throw new Exception(message);
        }
    }
}
