using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIA;

/*
 *  Programmer  : Stephen Glasspell J381116@tafe.wa.edu.au
 *  Date        : Sept/ Oct 2018
 *  Purpose     : Car Rental Database Application.
 *                This is an application for use in the offices of a Vehicle Hire company
 *                to be used to handle the administration of the business,
 *                including bookings, customer data, vehicle data and employee data.
 *                Secondary tools such as financial reporting and calendar views
 *                can be added to make all business tasks easier for the business.
 *                
 *  Version     : Draft.              
 */

namespace StephenGlasspell_CarRental
{
    public class Scanner
    {
        DeviceManager deviceManager = new DeviceManager();
        DeviceInfo scannerInfo = null;
        

        public Scanner()
        {
           
        }

        public bool initialiseScanner()
        {
            for (int i = 0; i < deviceManager.DeviceInfos.Count; i++)
            {
                try
                {
                    if (deviceManager.DeviceInfos[i].Type != WiaDeviceType.ScannerDeviceType)
                    {
                        continue;
                    }
                    else
                    {
                        scannerInfo = deviceManager.DeviceInfos[i];
                        break;
                    }
                }
                catch (Exception e)
                {
                    DataDelegate.errorMessages.Add(e.Message.ToString());
                }
            }

          

            if (scannerInfo == null)
            {
               

                if (StephenGlasspell_CarRental.CommonTasks.getInstance().Topmost == true)
                {
                    System.Windows.MessageBox.Show( CommonTasks.getInstance(), "No Scanner Found", "Error");
                }
                else
                {
                    System.Windows.MessageBox.Show(new MainWindow(), "No Scanner Found", "Error");
                }
                
                
                return false;
            }
            else
            {
                return true;
            }
        }


        public void scanImage()
        {
            if(scannerInfo != null)
            {
                var device = scannerInfo.Connect();
                var scannerItem = device.Items[1];
                var imageFile = (ImageFile)scannerItem.Transfer(FormatID.wiaFormatJPEG);
                var path = Directory.GetCurrentDirectory() + "\\ScannedImages\\scan.jpg";
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                try
                {
                    imageFile.SaveFile(path);
                }catch(Exception e)
                {
                    System.Windows.MessageBox.Show(e.Message.ToString(), "Error");
                }

                
            }
        }
    }
}
