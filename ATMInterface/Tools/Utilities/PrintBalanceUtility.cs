using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace ATMInterface.Tools.Utilities
{
    internal class PrintBalanceUtility
    {
        private static FlowDocument BuildBalanceDoc(string cardNumber, string balance)
        { 

            FlowDocument doc = new FlowDocument();

            Section section = new Section();

            Paragraph greeting = new Paragraph();
            Bold bld = new Bold();
            bld.Inlines.Add(new Run("Balance"));
            Underline underline = new Underline();
            underline.Inlines.Add(bld);
            greeting.Inlines.Add(underline);
            section.Blocks.Add(greeting);

            Paragraph cardNumPar = new Paragraph(new Run($"Card number: {cardNumber}"));
            section.Blocks.Add(cardNumPar);

            Paragraph balancePar = new Paragraph(new Run($"Your Balance: {balance} $"));
            section.Blocks.Add(balancePar);

            Paragraph currentTime = new Paragraph(new Run($"Time of print: {DateTime.Now}"));
            section.Blocks.Add(currentTime);
 
            doc.Blocks.Add(section);

            doc.Name = "Balance";

            return doc;

        }

        private static bool PrintDocument(FlowDocument doc, bool hidePrintDialog = false)
        {
            PrintDialog printDialog = new PrintDialog();

            if (!hidePrintDialog)
            {
                bool? isPrinted = printDialog.ShowDialog();
                if (isPrinted != true) return false;
            }

            try
            {
                IDocumentPaginatorSource idpSource = doc;

                printDialog.PrintDocument(idpSource.DocumentPaginator, "Balance");
                return true;
            }
            catch 
            {
                MessageBox.Show("Print error!", "ATM", MessageBoxButton.OK, MessageBoxImage.Error);

                return false;
            }
        }

        public static void PrintBalance(Tuple<string, string> printInfo)
        {
            FlowDocument doc = BuildBalanceDoc(printInfo.Item1, printInfo.Item2);
            if(!PrintDocument(doc)) MessageBox.Show("Document not printed!", "ATM", MessageBoxButton.OK, MessageBoxImage.Information);
        }

    }
}
