using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace AutoEcac.Services
{
    public class CertificateDialogHandler
    {
        

        public async void ChooseCertificate(string CertificateName, string windowTitle)
        {
            //await Task.Delay(2000);

            //if (AutoIt.AutoItX.WinExists("Siscomex Importação Web - Google Chrome", "") == 1)
            //{
            //    AutoIt.AutoItX.WinActivate("Siscomex Importação Web - Google Chrome", "");                
            //    AutoIt.AutoItX.Send(CertificateName ,0);
            //    AutoIt.AutoItX.Send("{ENTER}", 0);

            //    //AutoIt.AutoItX.ControlClick("Siscomex Importação Web - Google Chrome", "", "Button2");
            //    //return true;
            //}

            AutoIt.AutoItX.WinActive(windowTitle, "");
            string CurrentCertificate = "";
            ViewCertificateInfo(windowTitle);
            CurrentCertificate = GetCertificateIssuedTo();
            
            while (true)
            {
                if (!CurrentCertificate.Equals(CertificateName))
                {
                    SelectNextCertificate(windowTitle);
                    ViewCertificateInfo(windowTitle);
                    string newCert = GetCertificateIssuedTo();
                    if (newCert.Equals(CurrentCertificate))
                        throw new SystemException("Certificate was not found!");
                    else
                    {
                        CurrentCertificate = newCert;
                        continue;
                    }
                }

                else
                {
                    PressBtnOK(windowTitle);
                    break;
                }
            }
        }

        public void ConfirmSecurtyException()
        {
            Thread.Sleep(2);
            AutoIt.AutoItX.WinWaitActive("Security Alert", "", 0);

            PressBtnYes();

        }


        public void CancelCertificateDialog(string windowTitle)
        {
            Thread.Sleep(2);
            AutoIt.AutoItX.WinWaitActive(windowTitle, "", 0);

            PressBtnCancel();

        }

        /// <summary>
        /// Open certificate details window
        /// </summary>
        /// <param name="windowTitle">Title of certificate dialog</param>

        private void ViewCertificateInfo(string windowTitle)
        {
            if (windowTitle.Equals("Siscomex Importação Web - Google Chrome"))
            {
               AutoIt.AutoItX.Send("{TAB}", 0);
                Thread.Sleep(1000);
            }

            AutoIt.AutoItX.Send("{SPACE}", 0);
            Thread.Sleep(1000);
            AutoIt.AutoItX.WinWaitActive("Certificate", "", 0);
        }

        /// 
        /// Extract the certificate name from a "Certificate" window text
        /// 
        /// The certificate name
        private string GetCertificateIssuedTo()
        {
            string CertificateWndText;
            string sPattern;
            string[] SplittedCertificateWndText;

            CertificateWndText = AutoIt.AutoItX.WinGetText("Chrome Legacy Window", "");
            sPattern = "General\n\n(.+?)\n";
            SplittedCertificateWndText = Regex.Split(CertificateWndText, sPattern);

            return SplittedCertificateWndText[1]; ;
        }

        /// 
        /// Determines whether the certificate name is founded 
        /// in the "Certificate" window text
        /// 
        /// 
        private bool IsFoundedCertificate(string CertificateName)
        {
            return GetCertificateIssuedTo() == CertificateName;
        }

        /// 
        /// Return to the "Choose a digital certificate" window
        /// and press the button "OK" when the certificate is founded
        private void PressBtnOK(string windowTitle)
        {
            if (!windowTitle.Equals("Choose a digital certificate"))
            {
                AutoIt.AutoItX.WinWaitActive("Certificate Details", "", 0);
            }

            Thread.Sleep(1000);
            AutoIt.AutoItX.Send("{ESC}", 0);

            Thread.Sleep(1000);
            AutoIt.AutoItX.Send("{TAB}", 0);
            Thread.Sleep(1000);
            AutoIt.AutoItX.Send("{ENTER}", 0);

        }

        private void PressBtnCancel()
        {
            //Thread.Sleep(1000);
            //AutoIt.Send("{ESC}", 0);
            for (int i = 0; i < 3; i++)
            {
                AutoIt.AutoItX.Send("{TAB}", 0);
                Thread.Sleep(1000);
            }
            AutoIt.AutoItX.Send("{SPACE}", 0);
            Thread.Sleep(1000);
        }
        private void PressBtnYes()
        {
            //Thread.Sleep(1000);
            //AutoIt.Send("{ESC}", 0);          
            for (int i = 0; i < 2; i++)
            {
                AutoIt.AutoItX.Send("{TAB}", 0);
                Thread.Sleep(1000);
            }
            AutoIt.AutoItX.Send("{SPACE}", 0);
            Thread.Sleep(1000);
        }

        /// 
        /// Return to the "Choose a digital certificate" window and 
        /// select next certificate name from the name list
        /// 
        private void SelectNextCertificate(string windowTitle)
        {
            if (!windowTitle.Equals("Choose a digital certificate"))
            {
                AutoIt.AutoItX.WinWaitActive("Certificate Details", "", 0);
            }

            Thread.Sleep(1000);
            AutoIt.AutoItX.Send("{ESC}", 0);


            Thread.Sleep(1000);
            AutoIt.AutoItX.Send("{TAB}", 0);
            Thread.Sleep(1000);
            AutoIt.AutoItX.Send("{DOWN}", 0);
        }
    }
}
