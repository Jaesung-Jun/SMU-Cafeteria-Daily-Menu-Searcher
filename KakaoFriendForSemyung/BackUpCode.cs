using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KakaoFriendForSemyung
{
    class BackUpCode
    {
        public List<string>[] Crawling_GH002()
        {
            string url = "http://setopia.semyung.ac.kr/main/program/menu/foodProcDate.jsp?cafe_cd=" + cafe_id;

            List<string>[] returnValue = new List<string>[3];
            List<string> isBld = new List<string>();        //Brunch, Lunch, Dinner
            List<string> foodInfo = new List<string>();        //何のごはん？
            List<string> whatDate = new List<string>();

            bool only_lunch = false;
            int howmany_Day = 0;

            WebClient wc = new WebClient();
            wc.Encoding = Encoding.UTF8;
            string html = wc.DownloadString(url);
            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);
            HtmlNodeCollection thCollection = doc.DocumentNode.SelectNodes("//th");
            HtmlNodeCollection tdCollection = doc.DocumentNode.SelectNodes("//td");
            //값 받아오기
            returnValue[0] = new List<string>();
            returnValue[1] = new List<string>();
            returnValue[2] = new List<string>();

            //날짜 예외처리
            for (int i = 0; i < tdCollection.Count; i++)
            {
                if (tdCollection[i].InnerHtml.Contains("(월)") || tdCollection[i].InnerHtml.Contains("(화)") ||
                        tdCollection[i].InnerHtml.Contains("(수)") || tdCollection[i].InnerHtml.Contains("(목)") ||
                        tdCollection[i].InnerHtml.Contains("(금)") || tdCollection[i].InnerHtml.Contains("(토)") ||
                        tdCollection[i].InnerHtml.Contains("(일)"))
                {
                    if (only_lunch == false)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            whatDate.Add(tdCollection[i].InnerText);
                        }
                    }
                    else if (only_lunch == true)
                    {
                        howmany_Day = howmany_Day + 1;
                        whatDate.Add(tdCollection[i].InnerText);
                    }
                }
                if (tdCollection[i].InnerHtml.Contains("<br>"))
                {
                    if (tdCollection[i].InnerHtml.Contains("오늘의메뉴") == true && cafe_id == "GH001")
                    {
                        System.Text.RegularExpressions.Match match = System.Text.RegularExpressions.Regex.Match(tdCollection[i].InnerHtml, @"오늘의메뉴\(.*\)\([0-9]\,[0-9]{1,}원\)");
                        foodInfo.Add(match.Value);
                    }
                    else if (tdCollection[i].InnerHtml.Contains("오늘의메뉴") == false && cafe_id == "GH001")
                    {
                        foodInfo.Add("오늘의 메뉴 없음.");
                    }
                    else
                    {
                        foodInfo.Add(tdCollection[i].InnerHtml);
                    }
                }
            }

            for (int i = 0; i < thCollection.Count; i++)
            {
                if (only_lunch == true)
                {                                                 //주시 필요
                    if (i == 0)
                    {
                        i = thCollection.Count - howmany_Day;
                    }
                    isBld.Add("종일");
                }
                else
                {
                    isBld.Add(thCollection[i].InnerText);
                }
            }
            if (whatDate.Count != foodInfo.Count || whatDate.Count != isbld.Count)
            {
                Console.WriteLine(whatDate.Count.ToString() + " " + isbld.Count.ToString() + " " + food_info.Count.ToString());
                returnValue[0].Clear();
                returnValue[1].Clear();
                returnValue[2].Clear();
                return returnValue;
            }
            returnValue[0] = whatDate;
            returnValue[1] = isBld;
            returnValue[2] = foodInfo;
            return returnValue;
        }
    }
}
