using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassItemLibrary
{
    public class clsLog
    {

        public string _dateIn { get; set; }
        public string _timeIn { get; set; }
        public string _dateOut { get; set; }
        public string _timeOut { get; set; }
        public string _userName { get; set; }

        public clsLog(string dateIn, string timeIn, string dateOut, string timeOut, string userName) {
            
            _dateIn = dateIn;
            _timeIn = timeIn;
            _dateOut = dateOut;
            _timeOut = timeOut;
            _userName = userName;

        }


        //_________________________________________________________________________
        // Files

        public static clsLog ConvertLineToLog(string line, string separator = "#//#")
        {
            string[] itemFields = line.Split(new string[] { separator }, StringSplitOptions.None);

            return new clsLog(itemFields[0], itemFields[1], itemFields[2], itemFields[3], itemFields[4]);
        }

        public static List<clsLog> LoadDataFromFileToLogList()
        {
            List<clsLog> logsRecordList = new List<clsLog>();
            using (StreamReader reader = new StreamReader("Logs.txt"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                  
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        logsRecordList.Add(ConvertLineToLog(line));
                    }
                }
            }
            return logsRecordList;
        }

        public static string ConvertLogToLine(clsLog Log, string separator = "#//#")
        {
            return $"{Log._dateIn}{separator}{Log._timeIn}{separator}{Log._dateOut}{separator}{Log._timeOut}{separator}{Log._userName}";
        }

        public static void LoadDataFromLogListToFile(List<clsLog> logsRecordList)
        {
            using (StreamWriter writer = new StreamWriter("Logs.txt", true))
            {
                foreach (clsLog log in logsRecordList)
                {
                        writer.WriteLine(ConvertLogToLine(log));
             
                }
            }
        }

        //_________________________________________________________________________


    }
}
