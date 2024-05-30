using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ClassItemLibrary
{
    public class clsItem 
    {   
        
   
        public string Name { get; set; }
        public float Price { get; set; }

        public bool MarkForDelete = false;

        public clsItem(string Name, float Price)
        {
            this.Name = Name;
            this.Price = Price;
        }

 
        public void addItemToList()
        {
           
            List<clsItem> myItems = LoadDataFromFileToObjList();
            myItems.Add(this);
            LoadDataFromObjListToFile(myItems);

        }

        public void updateItem()
        {
            List<clsItem> myItems = LoadDataFromFileToObjList();

            for(int i = 0; i < myItems.Count; i++)
            {
                
                if (myItems[i].Name == Name)
                {
                    myItems[i] = this;
                    break;
                }
            }

            LoadDataFromObjListToFile(myItems);
        }

        public void deleteItem()
        {
            List<clsItem> myItems = LoadDataFromFileToObjList();

            for (int i = 0; i < myItems.Count; i++)
            {

                if (myItems[i].Name == Name)
                {
                    myItems[i].MarkForDelete = true;
                    break;
                }
            }

            LoadDataFromObjListToFile(myItems);

        }

        public static clsItem findItem(string itemName)
        {
            List<clsItem> myItems = LoadDataFromFileToObjList();

            for (int i = 0; i < myItems.Count; i++)
            {
                if (myItems[i].Name == itemName)
                {
                    return myItems[i];
                    
                }
            }

            return null;
        }

        public static bool isItemExist(string itemName)
        {
            if(findItem(itemName) != null)
            {
                return true;
            }
            return false;
        }

        //_________________________________________________________________________
        // Files

        public static clsItem ConvertLineToItemObject(string line, string separator = "#//#")
        {
            string[] itemFields = line.Split(new string[] { separator }, StringSplitOptions.None);
       
            return new clsItem(itemFields[0], Convert.ToSingle (itemFields[1]));
        }

        public static List<clsItem> LoadDataFromFileToObjList()
        {
            List<clsItem> itemRecordList = new List<clsItem>();
            using (StreamReader reader = new StreamReader("Items.txt"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    // Check if the line is not empty or just whitespace
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        itemRecordList.Add(ConvertLineToItemObject(line));
                    }
                }
            }
            return itemRecordList;
        }

        public static string ConvertItemObjectToLine(clsItem item, string separator = "#//#")
        {
            return $"{item.Name}{separator}{item.Price}";
        }

        public static void LoadDataFromObjListToFile(List<clsItem> itemRecordList)
        {
            using (StreamWriter writer = new StreamWriter("Items.txt"))
            {
                foreach (clsItem item in itemRecordList)
                {
                    if (!item.MarkForDelete)
                    {
                        writer.WriteLine(ConvertItemObjectToLine(item));
                    }
                  
                }
            }
        }

        //_____________________________________________________________________________


    }




}
