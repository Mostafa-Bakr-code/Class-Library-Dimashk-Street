using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassItemLibrary
{
    public class clsOrder
    {
        public  List<clsItem> orderItems = new List<clsItem>();
        public string Date { get; set; }
        public string Time { get; set; }
        public float Total {  get; set; }

        public int orderNumber {  get; set; }

        public static int getOrderNumber()
        {
            List<clsOrder> orderList = LoadDataFromFileToOrderList();

            if (orderList.Count == 0)
            {
                
                return 1;
            }

            clsOrder order = orderList[orderList.Count - 1];

            return order.orderNumber + 1;

           
        }

        public static float getTotal(List<clsItem> items)
        {
            float total = 0;

            foreach(clsItem item in items)
            {
                total += item.Price;
            }

            return total;
        }

        public clsOrder(string date, string time, float total,int OrderNumber ,List<clsItem> itemsList)
        {

            Date = date;
            Time = time;
            Total = total;
            orderNumber = OrderNumber;
            orderItems = itemsList;

        }

        public void addOrder()
        {
            List<clsOrder> orders = LoadDataFromFileToOrderList();
            orders.Add(this);
            LoadDataFromObrderListToFile(orders);
        }

        public static float calcTotalinRange(DateTime dateFrom, DateTime dateTo)
        {
            float total = 0;

            List<clsOrder> orders = LoadDataFromFileToOrderList();

            foreach (clsOrder item in orders)
            {
                string dateString = item.Date;

                DateTime date;
                bool isValidDate = DateTime.TryParseExact(dateString, "yyyy-MM-dd",
                                    System.Globalization.CultureInfo.InvariantCulture,
                                    System.Globalization.DateTimeStyles.None, out date);

                if (!isValidDate)
                {
                    continue; 
                }

                if (date > dateTo)
                {
                    break; 
                }

                if (date >= dateFrom)
                {
                    total += item.Total;
                }
            }

            return total;
        }


        //_________________________________________________________________________
        // Files

        public static String[] ConvertLineToItemsArr(string line, string separator = "-")
        {
            // line is burger/50-hotdog/60

            string[] ArrItems = line.Split(new string[] { separator }, StringSplitOptions.None);

            return ArrItems;

            // return [burger/50, hotdog/60]
        }

        public static List<clsItem> ConvertArrItemsToclsItems(string[] ArrItems, string separator = "/")
        {
            List<clsItem> ItemsList = new List<clsItem>();

            for(int i = 0; i < ArrItems.Length; i++)
            {
                string[] orderFields = ArrItems[i].Split(new string[] { separator }, StringSplitOptions.None);

                // now i have a array string carrying [burger, 50]
                
                clsItem item = new clsItem(orderFields[0], Convert.ToSingle (orderFields[1]));
                ItemsList.Add(item);
            }

            return ItemsList;
        }

        public static clsOrder ConvertLineToOrderObject(string line, string separator = "#//#")
        {

            string[] orderFields = line.Split(new string[] { separator }, StringSplitOptions.None);



            return new clsOrder(orderFields[0], orderFields[1] ,Convert.ToSingle(orderFields[2]),
                int.Parse(orderFields[3]), ConvertArrItemsToclsItems(ConvertLineToItemsArr(orderFields[4])) );
        }

        public static List<clsOrder> LoadDataFromFileToOrderList()


        {
            List<clsOrder> itemRecordList = new List<clsOrder>();
            using (StreamReader reader = new StreamReader("OrderItems.txt"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        itemRecordList.Add(ConvertLineToOrderObject(line));
                    }
                }
            }
            return itemRecordList;
        }

        //_______________________________________________________________________

        public static string ConvertOrderObjectToLine(clsOrder order, string separator = "#//#")
        {

            string items = string.Join("-", order.orderItems.Select(item => $"{item.Name}/{item.Price}"));
            return $"{order.Date}{separator}{order.Time}{separator}{order.Total}{separator}{order.orderNumber}{separator}{items}";
        }

        public static void LoadDataFromObrderListToFile(List<clsOrder> OrderRecordList)
        {
            using (StreamWriter writer = new StreamWriter("OrderItems.txt"))
            {
                foreach (clsOrder order in OrderRecordList)
                {
                        writer.WriteLine(ConvertOrderObjectToLine(order));
                }
            }
        }

        //_____________________________________________________________________________



    }
}
