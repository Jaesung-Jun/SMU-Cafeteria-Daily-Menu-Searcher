using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using HtmlAgilityPack;
using MySql.Data.MySqlClient;
using System.Runtime.InteropServices;

namespace KakaoFriendForSemyung
{

    public partial class Form1 : Form
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();
        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0; // 숨기기
        const int SW_SHOW = 1; // 보이기


        public Form1()
        {
            var handle = GetConsoleWindow();
            ShowWindow(handle, SW_HIDE);
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Database_list.BeginUpdate();
            Database_list.View = View.Details;
            Database_list.Columns.Add("", 30, HorizontalAlignment.Left);
            Database_list.Columns.Add("Date", 130, HorizontalAlignment.Left);
            Database_list.Columns.Add("BLD", 80, HorizontalAlignment.Left);
            Database_list.Columns.Add("Menu", 1000, HorizontalAlignment.Left);
            Database_list.EndUpdate();
        }
        private void InsertIntoDB_Click(object sender, EventArgs e)
        {
            string Url = "http://setopia.semyung.ac.kr/main/program/menu/foodProcDate.jsp?cafe_cd=";
            string cafe_id = CafeID.Text;
            string table_name = "foodinfo_" + cafe_id.Replace("GH", "");
            List<string>[] Crawld_List = new List<string>[3];
            Crawling_Semyung cr = new Crawling_Semyung();
            //try
            //{
            Crawld_List = cr.Crawling_Main(Url, cafe_id);
            if (Crawld_List[0].Count == 0 || Crawld_List[1].Count == 0 || Crawld_List[2].Count == 0)
            {
                Console.WriteLine("학식 정보를 불러오는 도중 에러가 발생했습니다.");
            }
            else
            {
                for (int i = 0; i < 3; i++)
                {
                    foreach (string str in Crawld_List[i])
                    {
                        Console.WriteLine("Insert Into " + table_name + " : " + str);
                    }
                }
                DatabaseConnect dbconnect = new DatabaseConnect("localhost", "kakao_data", "root", "wjswptjd3241");
                Console.WriteLine("DB Connected : " + dbconnect.isdbconnected);
                for (int i = 0; i < Crawld_List[0].Count; i++)
                {
                    dbconnect.Database_Insert(table_name, Crawld_List[0][i], Crawld_List[1][i], Crawld_List[2][i]);
                }
            }
            //}
            /*
            catch
            {
                MessageBox.Show("DB Insert 실패.");
                Console.WriteLine("DB Insert Failed.");
            }*/
        }

        private void Load_DB_Click(object sender, EventArgs e)
        {
            string cafe_id = CafeID.Text;
            string table_name = "foodinfo_" + cafe_id.Replace("GH", "");
            DatabaseConnect dbconnect = new DatabaseConnect("localhost", "kakao_data", "root", "wjswptjd3241");
            Console.WriteLine("DB Connected : " + dbconnect.isdbconnected);
            try
            {
                List<ListViewItem> lvList = dbconnect.Database_Select(table_name);
                Database_list.Items.Clear();
                for (int i = 0; i < lvList.Count; i++)
                {
                    Database_list.Items.Add(lvList[i]);
                }
            }
            catch
            {
                Console.WriteLine("DB Load Error!");
                MessageBox.Show("데이터베이스 로딩중 에러가 발생했습니다.");
            }
        }

        private void Search_Button_Click(object sender, EventArgs e)
        {
            ListView lv = this.Database_list;
            Console.WriteLine(Search_text.Text);
            List<ListViewItem> search_text = FormFunctions.FindItem(Database_list, Search_text.Text, 0);

            if (Search_Button.Text == "검색")
            {
                try {
                    if(search_text != null) {
                        Database_list.Items.Clear();
                        foreach (ListViewItem Items in search_text)
                        {
                            Database_list.Items.Add(Items);
                        }
                        Search_text.Clear();
                    }
                    else if(search_text == null)
                    {
                        MessageBox.Show("키워드를 찾을 수 없습니다.");
                        Search_text.Clear();
                    }
                }
                catch(ArgumentException)
                {
                    MessageBox.Show("Argument Exception");
                }
            }
        }

        private void Developer_Mode_Click(object sender, EventArgs e)
        {
            var handle = GetConsoleWindow();
            if (Developer_Mode.Text == "개발자모드 끄기")
            {
                ShowWindow(handle, SW_HIDE);
                Developer_Mode.Text = "개발자모드";
            }
            else
            {
                ShowWindow(handle, SW_SHOW);
                Developer_Mode.Text = "개발자모드 끄기";
            }

        }
    }
    public class Crawling_Semyung
    {
        /***************************************************
                             크롤링 처리
        ***************************************************/
        public List<string>[] Crawling_Main(string Url, string CafeID)
        {
            string Url_CafeID = Url + CafeID;
            exceptionForCrawling_All exceptions = new exceptionForCrawling_All(Url_CafeID, CafeID);
            return exceptions.ExceptionAllProcessed;
        }

    }
    /***************************************************************************
        Crawling_Semyung Class에서 공통된 기능들을 구현해놓기 위한 상속 클래스
    ****************************************************************************/
    public class GetHtmlForCrawling : Crawling_Semyung
    {
        private static HtmlAgilityPack.HtmlDocument Get_Html(string url)
        {
            WebClient wc = new WebClient();
            wc.Encoding = Encoding.UTF8;
            string html = wc.DownloadString(url);
            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);
            return doc;
        }
        public static HtmlNodeCollection Get_Html_tdCollection(string url)
        {
            var doc = Get_Html(url);
            HtmlNodeCollection tdCollection = doc.DocumentNode.SelectNodes("//td");
            return tdCollection;
        }
        public static HtmlNodeCollection Get_Html_thCollection(string url)
        {
            var doc = Get_Html(url);
            HtmlNodeCollection thCollection = doc.DocumentNode.SelectNodes("//th");
            return thCollection;
        }

    }
    /***************************************************************************
     Crawling_Semyung Class에서 공통된 예외처리들을 구현해놓기 위한 상속 클래스
    ****************************************************************************/
    public class exceptionForCrawling_All : Crawling_Semyung
    {
        int HowManyDay;
        public List<string>[] ExceptionAllProcessed = new List<string>[3];
        public exceptionForCrawling_All(string url, string CafeId)
        {
            List<string> IsBld = new List<string>();        //Brunch, Lunch, Dinner
            List<string> FoodInfo = new List<string>();        //何のごはん？
            List<string> WhatDate = new List<string>();
            string[] HtmlTagTD;
            string[] HtmlTagTH;

            bool IsCountOK;

            HtmlNodeCollection TdCollection = GetHtmlForCrawling.Get_Html_tdCollection(url);
            HtmlNodeCollection ThCollection = GetHtmlForCrawling.Get_Html_thCollection(url);

            HtmlTagTD = Collection_To_Array(TdCollection);
            HtmlTagTH = Collection_To_Array(ThCollection);

  
            WhatDate = Date_Exception(HtmlTagTD, CafeId);
            IsBld = Bld_Exception(HtmlTagTH, CafeId);
            FoodInfo = Todays_Menu_Exception(HtmlTagTD, CafeId);

            IsCountOK = List_Count_Check(WhatDate, IsBld, FoodInfo);

            if(IsCountOK == true)
            {

                ExceptionAllProcessed[0] = WhatDate;
                ExceptionAllProcessed[1] = IsBld;
                ExceptionAllProcessed[2] = FoodInfo;
            }
            else if(IsCountOK == false)
            {
                ExceptionAllProcessed[0] = new List<string>();
                ExceptionAllProcessed[1] = new List<string>();
                ExceptionAllProcessed[2] = new List<string>();
            }
        }

        private string[] Collection_To_Array(HtmlNodeCollection Input)
        {
            List<string> Temp = new List<string>();
            string[] Return_Value;
            for(int i = 0; i < Input.Count; i++)
            {
                Temp.Add(Input[i].InnerHtml);
            }
            Return_Value = Temp.ToArray();
            return Return_Value;
        }

        private List<string> Date_Exception(string[] HtmlTagTD, string CafeId)
        {
            List<string> WhatDate = new List<string>();
            for(int i = 0; i < HtmlTagTD.Length; i++)
            {
                System.Text.RegularExpressions.Match match = System.Text.RegularExpressions.Regex.Match(HtmlTagTD[i], @"[0-9]{4}\.[0-9]{1,2}\.[0-9]{1,2}\. \([가-힣]\)");
                if (match.Value != "") {
                    HowManyDay = HowManyDay + 1;
                    WhatDate.Add(HtmlTagTD[i]);
                    if (CafeId != "GH001" && CafeId != "GH002") {
                        WhatDate.Add(HtmlTagTD[i]);
                        WhatDate.Add(HtmlTagTD[i]);
                    }
                }
            }
            return WhatDate;
        }

        private List<string> Bld_Exception(string[] HtmlTagTH, string CafeId)
        {
            List<string> IsBld = new List<string>();
            bool OnlyLunch = false;
            if (CafeId == "GH001" && CafeId == "GH002")
            {
                OnlyLunch = true;
            }
            for (int i = 0; i < HtmlTagTH.Length; i++)
            {
                if (OnlyLunch == true)
                {                                                 //주시 필요
                    if (i == 0)
                    {
                        i = HtmlTagTH.Length - HowManyDay;
                    }
                    IsBld.Add("종일");
                }
                if (CafeId == "GH002")
                {
                    IsBld.Add("점심");
                    IsBld.Add("점심");
                    IsBld.Add( "점심");
                    IsBld.Add("점심");
                    IsBld.Add("점심");
                }
                else
                {
                    IsBld.Add(HtmlTagTH[i]);
                }
            }
            return IsBld;
        }
        
        private List<string> Todays_Menu_Exception(string[] HtmlTagTD, string CafeID)
        {
            List<string> foodInfo = new List<string>();
            for (int i = 0; i < HtmlTagTD.Length; i++)
            {
                if (HtmlTagTD[i].Contains("<br>"))
                {
                    //GH001(65번가)
                    /////////////////////////////////////////////////
                    if(CafeID == "GH001")
                    {

                        if (HtmlTagTD[i].Contains("오늘의메뉴") == true)
                        {
                            System.Text.RegularExpressions.Match match = System.Text.RegularExpressions.Regex.Match(HtmlTagTD[i], @"오늘의메뉴\(.*\)\([0-9]\,[0-9]{1,}원\)");
                            foodInfo.Add(match.Value);
                        }
                        else if (HtmlTagTD[i].Contains("오늘의메뉴") == false)
                        {
                            foodInfo.Add("오늘의 메뉴 없음.");
                        }
                    }
                    //다른식당
                    ///////////////////////////////////////////////////
                    else if (HtmlTagTD[i].Contains("이용시간") == true){ }
                    else
                    {
                        foodInfo.Add(HtmlTagTD[i]);
                    }
                }
            }
            return foodInfo;
        }
        private bool List_Count_Check(List<string> WhatDate, List<string> IsBld, List<string> FoodInfo)
        {
            if(WhatDate.Count == IsBld.Count || IsBld.Count == FoodInfo.Count || FoodInfo.Count == WhatDate.Count)
            {
                return true;
            }
            else
            {
                Console.WriteLine("=== List Length Not Correct! ===");
                Console.WriteLine(WhatDate.Count);
                Console.WriteLine(IsBld.Count);
                Console.WriteLine(FoodInfo.Count);
                Console.WriteLine("================================");
                return false;
            }
        }
    }
    public class DatabaseConnect
    {
        public bool isdbconnected = false;
        string strConn;
        MySqlConnection conn;

        public DatabaseConnect(string Server, string Database, string Uid, string Pwd)                          //class호출할때 변수4개 적기 Python으로 치면 __init__같은거
        {
            strConn = "Server=" + Server + ";Database=" + Database + ";Uid=" + Uid + ";Pwd=" + Pwd + ";";
            try
            {
                conn = new MySqlConnection(strConn);
                conn.Open();
                Console.WriteLine("DB Connected Successfully");
                isdbconnected = true;
            }
            catch
            {
                MessageBox.Show("Error : DB Connection Failed");
                isdbconnected = false;
            }
        }

        public void Database_Insert(string tableName, string Date, string BLD, string Menu)
        {
            string command = "insert into "+ tableName + " values(\"" + Date + "\", \"" + BLD + "\", \"" + Menu + "\");";
            MySqlCommand cmd = new MySqlCommand(command, conn);
            cmd.ExecuteNonQuery();
        }
        public List<ListViewItem> Database_Select(string tableName)
        {
            int index = 0;
            string command = "select Date,BLD,Menu from " + tableName + ";";
            MySqlCommand cmd = new MySqlCommand(command, conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            List<ListViewItem> lvList = new List<ListViewItem>();
            ListViewItem lv = new ListViewItem();
            while (reader.Read())
            {
                index += 1;
                lv = new ListViewItem(index.ToString());
                lv.SubItems.Add(reader.GetString(0));
                lv.SubItems.Add(reader.GetString(1));
                lv.SubItems.Add(reader.GetString(2));
                lvList.Add(lv);
                Console.WriteLine("Load from " + tableName + " : " + reader.GetString(0));
                Console.WriteLine("Load from " + tableName + " : " + reader.GetString(1));  
                Console.WriteLine("Load from " + tableName + " : " + reader.GetString(2));
            }
            reader.Close();
            return lvList;
        }

    }
    public class FormFunctions
    {
        public static List<ListViewItem> FindItem(ListView listview, string keyword, int startIndex)
        {
            List<ListViewItem> FoundItems = new List<ListViewItem>();
            bool isContains = false;
            foreach (ListViewItem item in listview.Items)
            {
                for(int i = 0; i < item.SubItems.Count; i++)
                {
                    if (item.SubItems[i].Text.Contains(keyword))
                    {
                        Console.WriteLine("Found at Index " + item.Text);
                        FoundItems.Add(item);
                        isContains = true;
                    }   
                }
            }
            if (isContains == true)
            {
                return FoundItems;
            }
            else
            {
                Console.WriteLine("Not Found :(");
                FoundItems = null;
                return FoundItems;
            }
        }
    }
}
