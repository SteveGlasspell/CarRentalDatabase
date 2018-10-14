using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.IO;

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
 *  Document    : AUDIO
 *                Handles application sounds and audio notifications.
 *                Used during application startup for the annoyance of others and / or to give 
 *                the application the Glass PRestige Car Rental branding it needs.
 *                Used during camera use to notify the user.
 */

namespace StephenGlasspell_CarRental
{
    public static class Audio
    {
        // Method to play sound when taking a Customer Snapshot.
        public static void playPhotograph()
        {
            play(@"..\..\Assets\audio\WaitForPhotograph.wav");
        }
       
        // Generic method to play named audio files.
        public static void play(String filename)
        {
            if (!File.Exists(filename))
            {
                // Only display an error if in Debug Mode. Otherwise, just stay silent.
                if (DataDelegate.debugMode)
                {
                    System.Windows.MessageBox.Show(filename, "Couldn't find Filename");
                }
                return;
            }

            // We only play WAV audio files. Do not accept any other formats.
            if (!filename.EndsWith(".wav"))
            {
                return;
            }

            SoundPlayer sound = new SoundPlayer();
            sound.SoundLocation = filename;
            sound.PlaySync();
        }

    }
}
