using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Media;
using WebEye.Controls.Wpf;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media.Imaging;
using System.Threading;

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
 *  
 *  Document    : This is the beginning of a Camera class
 *                It is functional, and works on my laptop
 *                but has not been tested on other computers or with webcams etc.
 *                The purpose is to provide the Car Rental company an extra level of security.
 *                While the car rental company will take photo ID in the form of a driving license
 *                and passport, some car rental companies take a snapshot of the customer prior to vehicle collection
 *                as added security. 
 *                This camera class is to let users of this application handle the snapshot aspect of the vehicle collection
 *                process as part of the software and database, as an integrated system.
 *                
 */


namespace StephenGlasspell_CarRental
{
    public class Camera
    {
        // Method to perform all the steps to take a picture.
        public void takePicture()
        {
            
            WebCameraControl control = new WebCameraControl();

            WebCameraId camera = null;

            if (control.GetVideoCaptureDevices().Count() > 0)
            {
                //Select the first camera we find. This can be more sophisticated.
                camera = (WebCameraId)control.GetVideoCaptureDevices().First();

                control.RenderSize = new System.Windows.Size(1920, 1080);

                control.StartCapture(camera);

                // This seems risky.
                // TODO change this to a more secure method of waiting.
                while (control.GetCurrentImage()==null) { }

                // Take 50 images (about 2 seconds.) The camera should have focussed and exposed correctly in that time.
                Image[] img = new Image[50];
                BitmapImage image = new BitmapImage();
                for (int i = 0; i < img.Length; i++) 
                {
                    img[i] = control.GetCurrentImage();
                    
                    MemoryStream ms = new MemoryStream();
                    img[i].Save(ms, ImageFormat.Bmp);
                    ms.Position = 0;
                    image = new BitmapImage();
                    image.BeginInit();
                    ms.Seek(0, SeekOrigin.Begin);
                    image.StreamSource = ms;
                    image.EndInit();
                }
                // Store the image in DataDelegate.
                DataDelegate.customerImage = image;
                // Record the time taken.
                DataDelegate.timeCustomerImageTaken = DateTime.Now;
                control.StopCapture();

                // TODO Fix the code below to allow saving of the file to disk and/or to database.




                /*
               String path = System.IO.Path.Combine(Environment.CurrentDirectory, @"..\..\Assets\audio\PhotographComplete.wav");
                Audio.play(path);

                string filename = @"/GLASS/CustomerPhoto";
                string ext = @".png";
                var filePath = @Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), filename + ext);

                SaveFileDialog saveFile = new SaveFileDialog();
                saveFile.Filter = "PNG file |*.png";
                saveFile.FileName = filename + DateTime.Now.Date.Year + "-" + DateTime.Now.Date.Month + "-" + DateTime.Now.Date.Day + "-" + ext;
                saveFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

                if (saveFile.ShowDialog() == DialogResult.OK)
                {
                    img.Last().Save(filePath);
                }
                */
                
            }
            
        }

    }

    
}
