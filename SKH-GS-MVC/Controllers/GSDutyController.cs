using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SKH_GS_MVC.Models;
using System.IO;
using Microsoft.Office.Interop.Excel;


namespace SKH_GS_MVC.Controllers
{
    
    public class GSDutyController : Controller
    {
        private GSdutyEntities db = new GSdutyEntities();
        // GET: GSDuty

        public ActionResult Index(FormCollection search)
        {
            int selected_year = new int();
            int selected_month = new int();
            string selected_division = "";
            string selected_division_id = "";
            ViewBag.skh_division = new SelectList(db.divisions, "id", "skh_division"); // 科別清單


            if (search.Count ==0) // 預設
            {
                DateTime today = DateTime.Today;
                selected_year = today.Year;
                selected_month = today.Month;
                selected_division = "一般外科";
                selected_division_id = "0117";
            }
            else
            {
                selected_year = Convert.ToInt16(search["year"]);
                selected_month = Convert.ToInt16(search["monthList"]);
                selected_division_id = search["skh_division"];
                selected_division = db.divisions.Where(c => c.id == selected_division_id).First().skh_division.Trim();
            }
            // 年選項
            string[] years = new string[] { "2016", "2017","2018" };
            int selectYearItem = 1;
            for(int yearItemCount =1; yearItemCount <= years.Count(); yearItemCount++)
            {
                if (years[yearItemCount - 1] == selected_year.ToString()) { selectYearItem = yearItemCount; }
            }
            SelectList yearList = new SelectList(years, selectYearItem);
            ViewBag.yearList = yearList;
            // 月選項
            string[] months = new string[] { "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12" };
            int selectMonthItem = 1;
            for(int monthItemCount=1; monthItemCount<= months.Count();monthItemCount++ )
            {
                if (months[monthItemCount - 1] == selected_month.ToString("00")) { selectMonthItem = monthItemCount; }
            }
            ViewBag.monthList = new SelectList
                (
                items: months,
                selectedValue: selectMonthItem
                );


            ViewBag.dutyTitle = selected_division + selected_year + "年" + selected_month + "月" + "值班表";
            DateTime begin_date = new DateTime(selected_year, selected_month, 1);
            DateTime end_date = new DateTime(selected_year, selected_month + 1, 1).AddDays(-1);
            // 找出目標月的所有班
            var dutyList =  db.duty_table
                .Where(c => (c.duty_type_id).Substring(0, 4) == selected_division_id)
                .Where(c => c.date >= begin_date && c.date <= end_date).ToList();


            List<DateTime> dateList = new List<DateTime>();
            Dictionary<string, string> titleList = new Dictionary<string, string>();
            List<DutyItemModel> dutyListModel = new List<DutyItemModel>();
    


            foreach (var dutyItem in dutyList)
            {
                // 檢查是否已有日期項目, 若無則新增
                if (dutyListModel.Where(c => c.dutyDate == dutyItem.date).Count() == 0)
                { 
                    DutyItemModel newdutyListItem = new DutyItemModel();
                    newdutyListItem.dutyDate = System.Convert.ToDateTime(dutyItem.date);
                    dutyListModel.Add(newdutyListItem);
                }
                //檢查是否已有Title項目, 若無則新增
                var duty_name = db.duty_type.Where(c => c.duty_type_id == dutyItem.duty_type_id).First().duty_name.Trim();
                if (titleList.Where(c => c.Key == dutyItem.duty_type_id).Count() == 0) { titleList.Add(dutyItem.duty_type_id, duty_name); }

            }
            // 依日期排序
            dutyListModel = dutyListModel.OrderBy(c => c.dutyDate).ToList();
            // 算天數及標題數
            int dateCount = dutyListModel.Count();
            int titleCount = titleList.Count();
            // 排入新整合班表
            for (int combinedItem = 0; combinedItem < dateCount; combinedItem++)
            {
                var dutyDate_work = dutyListModel.ElementAt(combinedItem).dutyDate;
                dutyListModel.ElementAt(combinedItem).weekDay = db.duty_table
                     .Where(c => c.date == dutyDate_work).First().weekDay;
                for (int tCount = 0; tCount < titleCount; tCount++)
                {
                    switch (tCount)
                    {

                        case 0:
                            var dutyItem1 = dutyList.Where(c => c.date == dutyDate_work && c.duty_type_id == titleList.ElementAt(0).Key);
                            dutyListModel.ElementAt(combinedItem).duty1Man = dutyItem1.First().duty_man.Trim();
                            break;
                        case 1:
                            var dutyItem2 = dutyList.Where(c => c.date == dutyDate_work && c.duty_type_id == titleList.ElementAt(1).Key);
                            dutyListModel.ElementAt(combinedItem).duty2Man = dutyItem2.First().duty_man.Trim();
                            break;
                        case 2:
                            var dutyItem3 = dutyList.Where(c => c.date == dutyDate_work && c.duty_type_id == titleList.ElementAt(2).Key);
                            dutyListModel.ElementAt(combinedItem).duty3Man = dutyItem3.First().duty_man.Trim();
                            break;
                        case 3:
                            var dutyItem4 = dutyList.Where(c => c.date == dutyDate_work && c.duty_type_id == titleList.ElementAt(3).Key);
                            dutyListModel.ElementAt(combinedItem).duty4Man = dutyItem4.First().duty_man.Trim();
                            break;
                        case 4:
                            var dutyItem5 = dutyList.Where(c => c.date == dutyDate_work && c.duty_type_id == titleList.ElementAt(4).Key);
                            dutyListModel.ElementAt(combinedItem).duty5Man = dutyItem5.First().duty_man.Trim();
                            break;
                    }
                }
            }
            ViewBag.titleList = titleList.Values;
            ViewBag.dutyToday = dutyListModel.Where(c => c.dutyDate == DateTime.Today).ToList();
            ViewBag.dutyNote = "班表説明:";
            return View(dutyListModel);
        }

        public ActionResult FileUpload()
        {
            return View();
        }

        public ActionResult Upload()
        {
                 return View();
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            if (file!= null)
            {
                GSdutyEntities dutyDB = new GSdutyEntities();
               
                

                var fileName = Path.GetFileName(file.FileName);
                fileName = fileName.Substring(0, fileName.LastIndexOf(".")); // 去掉 .xlsx

                string divisionId = fileName.Substring(0, 4); // 科別代碼

                var path = Server.MapPath("~/Duty_Files");
                var dutyFiles = Directory.GetFiles(path).ToList();
                int dutyFilesCount = 1;
                foreach(var dutyFile in dutyFiles)
                {
                    if (dutyFile.Contains(fileName)) {dutyFilesCount += 1; }
                }
               var NewFileName = fileName+ dutyFilesCount.ToString("000") +".xlsx";              
                path = Path.Combine(path, NewFileName);
               file.SaveAs(path);


                Application excelApp = new Application();            
                Workbook workbook = excelApp.Workbooks.Open(path);
                Worksheet dutySheet = workbook.Worksheets[1];
                Range dutyRange = dutySheet.Range["A1:H60"];

                int anchorRow = dutyRange.Find("日期").Row;
                int anchorColumn = dutyRange.Find("日期").Column;
                Range anchor = dutySheet.Cells[anchorRow, anchorColumn];

                int nextRow = dutyRange.Find("日期", anchor).Row;
                int rowDistance = nextRow - anchorRow;
                int dutyYear = Convert.ToInt16(fileName.Substring(4, 4));
                int dutyMonth = Convert.ToInt16(fileName.Substring(8, 2));

                for(int rowCount =1; rowCount<=6; rowCount++)
                {
                    for (int columnCount =1; columnCount<=7; columnCount++)
                    {
                        // 檢查位置非空白
                        var dateCheck = dutySheet.Cells[(anchorRow + (rowCount - 1) * rowDistance), (anchorColumn + columnCount)].Value;
                        if (dateCheck != null)
                        {
                            for (int dutyCount = 1; dutyCount <= (rowDistance - 2); dutyCount++)
                            {
                                duty_table singleDuty = new duty_table();
                            
                                SingleDuty dutyListItem = new SingleDuty();                       
                                string dutyListType = dutySheet.Cells[anchorRow + (rowCount - 1) * rowDistance + dutyCount, 1].Value;
                                string dutyListMan = dutySheet.Cells[anchorRow + (rowCount - 1) * rowDistance + dutyCount, anchorColumn + columnCount].Value;

                                singleDuty.duty_man = dutyListMan;
                                singleDuty.duty_type_id = dutyDB.duty_type
                                    .Where(c=> c.duty_type_id.Substring(0,4) == divisionId)
                                    .Where(c=> c.duty_name==dutyListType)
                                    .First().duty_type_id;
                                singleDuty.date = new DateTime(dutyYear, dutyMonth,Convert.ToInt16(dateCheck));
                                singleDuty.weekDay = (System.Globalization.DateTimeFormatInfo.CurrentInfo.DayNames[(byte)(singleDuty.date.Value.DayOfWeek)]).Substring(2,1);
                                var currentDuty = dutyDB.duty_table.Where(c => (c.date == singleDuty.date) & (c.duty_type_id == singleDuty.duty_type_id));
                                
                                if(currentDuty.Count() !=0)
                                {
                                    var dutyItem = currentDuty.First();
                                    dutyItem.duty_man = singleDuty.duty_man;
                                }
                                else
                                {
                                    dutyDB.duty_table.Add(singleDuty);
                                }                          

                            }

                        }
                    }
                }
                dutyDB.SaveChanges();
 
            }

            return RedirectToAction("Index");
        }
    }
}