using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;
using Telegram.Bot;
using static System.Net.Mime.MediaTypeNames;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using Image = System.Drawing.Image;
using System.Drawing.Imaging;
using Encoder = System.Drawing.Imaging.Encoder;
using System.IO;
using File = System.IO.File;
using System.ComponentModel.DataAnnotations;

namespace EPOS_Tools.EPOS_REPORT
{
    internal class EposClient
    {

        

        CookieContainer clientCookie = new CookieContainer();

        private const string officeUrl = "https://office.permkrai.ru/";
        private const string reportURL = "https://epos-rep.permkrai.ru/";
        private const string authUrl = "https://auth.permkrai.ru/";
        private const string executeURL = "https://epos-rep.permkrai.ru/api/olap/execute";
        string __eposUrl__ = "https://school.permkrai.ru/";
        string __rsaa_form_url__ = "";

        string login = "akimenkoed";
        string passwd = "yS433DGZ";

        public EposClient()
        {
            Logger.Log("Starting EPOS_Client...");
            //self.__session__.headers['user-agent'] = 'EposPythonLibrary/1.0 (https://github.com/nkrapivin/epos.py; alienoom@yandex.ru)';
            //self.__session__.headers['accept'] = 'application/json, text/plain, text/html, */*';
            
            setHeaders();

            string csrf_token = HTTP_Worker.Get(reportURL, clientCookie);

            //ЭТО ТИПО КНОПКА НА КОТОРУЮ МЫ ОТПРАВЛЯЕМ ФОРМУ АВТОРИЗАЦИИ
            MatchCollection postCollection = Regex.Matches(csrf_token, "action=\"(.*)\" ");

            string newURL = postCollection[0].Groups[1].Value.ToString().Replace("&amp;", "&");

            var postData = "&username=" + Uri.EscapeDataString(login);
            postData += "&password=" + Uri.EscapeDataString(passwd);
            var data = Encoding.ASCII.GetBytes(postData);

            //Отпраивли форму, и получаем редирект
            string request2 = HTTP_Worker.Post(newURL, clientCookie, data);
            Logger.Success("Auth EPOS_Client success...");
        }

        void setHeaders()
        {
        }

        public void report(string repType, string dateTo)
        {
            Logger.Log("[EPOS_Client] Report " + repType + " loading...");
            var reportStatJson = "{\"name\":\"general-statistics\",\"dimensions\":{\"rows\":[{\"dimension\":\"District\",\"selection\":{\"values\":[{\"name\":\"70\",\"caption\":\"Пермский городской округ\"}]}},{\"dimension\":\"OrgranizationType\",\"selection\":{\"values\":[{\"name\":\"3\",\"caption\":\"ВУЗ\"},{\"name\":\"5\",\"caption\":\"ДОУ\"},{\"name\":\"2\",\"caption\":\"ПОО\"},{\"name\":\"6\",\"caption\":\"Прочее\"},{\"name\":\"4\",\"caption\":\"УДО\"},{\"name\":\"1\",\"caption\":\"Школа\"}]}},{\"dimension\":\"School\",\"selection\":{\"values\":[{\"name\":\"1692\",\"caption\":\"МАОУ «СОШ № 24» г.Перми\"}]}},{\"dimension\":\"FromDate\",\"selection\":{\"values\":[{\"name\":\"2022-09-01\",\"caption\":\"2022-09-01\"}]}},{\"dimension\":\"ToDate\",\"selection\":{\"values\":[{\"name\":\"" + dateTo + "\",\"caption\":\"" + dateTo + "\"}]}}]}}";
            var reportThemesJson = "{\"name\":\"REP_LESSON_COMPLETE\",\"dimensions\":{\"rows\":[{\"dimension\":\"District\",\"selection\":{\"values\":[{\"name\":\"70\",\"caption\":\"Пермский городской округ\"}]}},{\"dimension\":\"OrgranizationType\",\"selection\":{\"values\":[{\"name\":\"3\",\"caption\":\"ВУЗ\"},{\"name\":\"5\",\"caption\":\"ДОУ\"},{\"name\":\"2\",\"caption\":\"ПОО\"},{\"name\":\"6\",\"caption\":\"Прочее\"},{\"name\":\"4\",\"caption\":\"УДО\"},{\"name\":\"1\",\"caption\":\"Школа\"}]}},{\"dimension\":\"School\",\"selection\":{\"values\":[{\"name\":\"1692\",\"caption\":\"МАОУ «СОШ № 24» г.Перми\"}]}},{\"dimension\":\"Teacher\",\"selection\":{\"values\":[{\"name\":\"15589632\",\"caption\":\"Акименко Елена Дмитриевна\"},{\"name\":\"15589624\",\"caption\":\"Андреева Наталья Александровна\"},{\"name\":\"15589613\",\"caption\":\"Баженова Елена Ивановна\"},{\"name\":\"15589621\",\"caption\":\"Баранова Марина Сергеевна\"},{\"name\":\"16748317\",\"caption\":\"Белякова Валерия Эдуардовна\"},{\"name\":\"16737744\",\"caption\":\"Белякова Валерия Эдуардовна\"},{\"name\":\"15589661\",\"caption\":\"Беспалко Наталья Михайловна\"},{\"name\":\"15589612\",\"caption\":\"Биряльцева Елена Анатольевна\"},{\"name\":\"16934511\",\"caption\":\"Борисова Светлана Александровна\"},{\"name\":\"15589874\",\"caption\":\"Васева Татьяна Сергеевна\"},{\"name\":\"15589973\",\"caption\":\"Васенина Надежда Степановна\"},{\"name\":\"15589627\",\"caption\":\"Василинюк Елена Ивановна\"},{\"name\":\"15589610\",\"caption\":\"Голубович Лидия Ивановна\"},{\"name\":\"15589711\",\"caption\":\"Голубцова Алёна Сергеевна\"},{\"name\":\"15589713\",\"caption\":\"Джусупбекова Жанна Романовна\"},{\"name\":\"15589631\",\"caption\":\"Дружинина Елена Анатольевна\"},{\"name\":\"15589617\",\"caption\":\"Дурницина Зоя Антоновна\"},{\"name\":\"16038612\",\"caption\":\"Дурова Светлана Александровна\"},{\"name\":\"15589875\",\"caption\":\"Зубкова Ольга Викторовна\"},{\"name\":\"15589781\",\"caption\":\"Каган Татьяна Александровна\"},{\"name\":\"15589662\",\"caption\":\"Карпова Елена Ильинична\"},{\"name\":\"15589623\",\"caption\":\"Киселева Елена Николаевна\"},{\"name\":\"15589685\",\"caption\":\"Клепцина Елена Николаевна\"},{\"name\":\"15589622\",\"caption\":\"Котельникова Ирина Николаевна\"},{\"name\":\"15589609\",\"caption\":\"Куроптева Ольга Константиновна\"},{\"name\":\"15589619\",\"caption\":\"Лазукова Вера Васильевна\"},{\"name\":\"15589782\",\"caption\":\"Лекомцева Галина Борисовна\"},{\"name\":\"15589611\",\"caption\":\"Ломаева Светлана Евгеньевна\"},{\"name\":\"15589628\",\"caption\":\"Лукьянченко Ирина Михайловна\"},{\"name\":\"15589803\",\"caption\":\"Лядова Фарида Апышевна\"},{\"name\":\"16379632\",\"caption\":\"Магомедова Оксана Мусаибовна\"},{\"name\":\"15589804\",\"caption\":\"Манакова Инесса Анатольевна\"},{\"name\":\"16711125\",\"caption\":\"Мартын Екатерина Викторовна\"},{\"name\":\"16711307\",\"caption\":\"Мартын Екатерина Викторовна\"},{\"name\":\"15589618\",\"caption\":\"Мушакова Лариса Васифовна\"},{\"name\":\"15589614\",\"caption\":\"Напольских Елена Васильевна\"},{\"name\":\"15589876\",\"caption\":\"Ожгибесова Алена Игоревна\"},{\"name\":\"15589872\",\"caption\":\"Олейник Александра Наиловна\"},{\"name\":\"15589615\",\"caption\":\"Пьянкова Елена Ивановна\"},{\"name\":\"15964735\",\"caption\":\"Радионов Дмитрий Алексеевич\"},{\"name\":\"15589910\",\"caption\":\"Разепина Ольга Александровна\"},{\"name\":\"16723154\",\"caption\":\"Рычагова Елена Борисовна\"},{\"name\":\"15964532\",\"caption\":\"Сарапулова Тамара Геннадьевна\"},{\"name\":\"16050780\",\"caption\":\"Сарапулова Тамара Геннадьевна\"},{\"name\":\"15589620\",\"caption\":\"Соломатова Зинаида Борисовна\"},{\"name\":\"15992370\",\"caption\":\"Сухова Анастасия Александровна\"},{\"name\":\"16350445\",\"caption\":\"Ткаченко Ирина Николаевна\"},{\"name\":\"16316300\",\"caption\":\"Федосеев Данил Александрович\"},{\"name\":\"15589630\",\"caption\":\"Челухиди Татьяна Николаевна\"},{\"name\":\"15589616\",\"caption\":\"Черанева Наталья Владимировна\"},{\"name\":\"15589629\",\"caption\":\"Шавшукова Лилия Загировна\"},{\"name\":\"15589625\",\"caption\":\"Шилова Елена Павловна\"},{\"name\":\"16723153\",\"caption\":\"Шилова Ксения Алексеевна\"},{\"name\":\"15589943\",\"caption\":\"Шубина Вера Алексеевна\"},{\"name\":\"15964733\",\"caption\":\"Щёкина Ольга Владимировна\"}]}},{\"dimension\":\"EduLevel\",\"selection\":{\"values\":[{\"name\":\"4\",\"caption\":\"ДО\"},{\"name\":\"1\",\"caption\":\"НОО\"},{\"name\":\"2\",\"caption\":\"ООО\"},{\"name\":\"3\",\"caption\":\"СОО\"},{\"name\":\"5\",\"caption\":\"СПО\"}]}},{\"dimension\":\"Class\",\"selection\":{\"values\":[{\"name\":\"404077\",\"caption\":\"1\\\"А\\\"\"},{\"name\":\"405390\",\"caption\":\"1\\\"Б\\\"\"},{\"name\":\"405391\",\"caption\":\"1\\\"В\\\"\"},{\"name\":\"405394\",\"caption\":\"1\\\"Г\\\"\"},{\"name\":\"405396\",\"caption\":\"1\\\"Д\\\"\"},{\"name\":\"405395\",\"caption\":\"1\\\"Е\\\"\"},{\"name\":\"394614\",\"caption\":\"2\\\"А\\\"\"},{\"name\":\"394348\",\"caption\":\"2\\\"Б\\\"\"},{\"name\":\"394613\",\"caption\":\"2\\\"В\\\"\"},{\"name\":\"394345\",\"caption\":\"2\\\"Г\\\"\"},{\"name\":\"394616\",\"caption\":\"2\\\"Д\\\"\"},{\"name\":\"394595\",\"caption\":\"2\\\"Е\\\"\"},{\"name\":\"394624\",\"caption\":\"3\\\"А\\\"\"},{\"name\":\"394569\",\"caption\":\"3\\\"Б\\\"\"},{\"name\":\"394452\",\"caption\":\"3\\\"В\\\"\"},{\"name\":\"394459\",\"caption\":\"3\\\"Г\\\"\"},{\"name\":\"394414\",\"caption\":\"3\\\"Д\\\"\"},{\"name\":\"394502\",\"caption\":\"3\\\"Е\\\"\"},{\"name\":\"417514\",\"caption\":\"4\\\"А\\\"\"},{\"name\":\"417512\",\"caption\":\"4\\\"Б\\\"\"},{\"name\":\"417517\",\"caption\":\"4\\\"В\\\"\"},{\"name\":\"417518\",\"caption\":\"4\\\"Г\\\"\"},{\"name\":\"417819\",\"caption\":\"4\\\"Д\\\"\"},{\"name\":\"417522\",\"caption\":\"4\\\"Е\\\"\"},{\"name\":\"394532\",\"caption\":\"5\\\"А\\\"\"},{\"name\":\"394553\",\"caption\":\"5\\\"Б\\\"\"},{\"name\":\"394655\",\"caption\":\"5\\\"В\\\"\"},{\"name\":\"417503\",\"caption\":\"5\\\"Г\\\"\"},{\"name\":\"417507\",\"caption\":\"5\\\"Д\\\"\"},{\"name\":\"424691\",\"caption\":\"5\\\"Е\\\"\"},{\"name\":\"394471\",\"caption\":\"6\\\"А\\\"\"},{\"name\":\"394474\",\"caption\":\"6\\\"Б\\\"\"},{\"name\":\"394509\",\"caption\":\"6\\\"В\\\"\"},{\"name\":\"424688\",\"caption\":\"6\\\"Г\\\"\"},{\"name\":\"424689\",\"caption\":\"6\\\"Д\\\"\"},{\"name\":\"394589\",\"caption\":\"7\\\"А\\\"\"},{\"name\":\"394365\",\"caption\":\"7\\\"Б\\\"\"},{\"name\":\"394661\",\"caption\":\"7\\\"В\\\"\"},{\"name\":\"394430\",\"caption\":\"8\\\"А\\\"\"},{\"name\":\"394504\",\"caption\":\"8\\\"Б\\\"\"},{\"name\":\"394638\",\"caption\":\"8\\\"В\\\"\"},{\"name\":\"424687\",\"caption\":\"8\\\"Г\\\"\"},{\"name\":\"417117\",\"caption\":\"9\\\"А\\\"\"},{\"name\":\"417118\",\"caption\":\"9\\\"Б\\\"\"},{\"name\":\"417119\",\"caption\":\"9\\\"В\\\"\"},{\"name\":\"417110\",\"caption\":\"10\\\"А\\\"\"},{\"name\":\"424686\",\"caption\":\"11\\\"А\\\"\"}]}},{\"dimension\":\"FromDate\",\"selection\":{\"values\":[{\"name\":\"2022-09-01\",\"caption\":\"2022-09-01\"}]}},{\"dimension\":\"ToDate\",\"selection\":{\"values\":[{\"name\":\"" + dateTo + "\",\"caption\":\"" + dateTo + "\"}]}}]}}";
            var reportHomeworkJson = "{\"name\":\"REP_HOMEWORK_COMPLETE\",\"dimensions\":{\"rows\":[{\"dimension\":\"District\",\"selection\":{\"values\":[{\"name\":\"70\",\"caption\":\"Пермский городской округ\"}]}},{\"dimension\":\"OrgranizationType\",\"selection\":{\"values\":[{\"name\":\"1\",\"caption\":\"Школа\"}]}},{\"dimension\":\"School\",\"selection\":{\"values\":[{\"name\":\"1692\",\"caption\":\"МАОУ «СОШ № 24» г.Перми\"}]}},{\"dimension\":\"Teacher\",\"selection\":{\"values\":[{\"name\":\"15589632\",\"caption\":\"Акименко Елена Дмитриевна\"},{\"name\":\"15589624\",\"caption\":\"Андреева Наталья Александровна\"},{\"name\":\"15589613\",\"caption\":\"Баженова Елена Ивановна\"},{\"name\":\"15589621\",\"caption\":\"Баранова Марина Сергеевна\"},{\"name\":\"16748317\",\"caption\":\"Белякова Валерия Эдуардовна\"},{\"name\":\"16737744\",\"caption\":\"Белякова Валерия Эдуардовна\"},{\"name\":\"15589661\",\"caption\":\"Беспалко Наталья Михайловна\"},{\"name\":\"15589612\",\"caption\":\"Биряльцева Елена Анатольевна\"},{\"name\":\"16934511\",\"caption\":\"Борисова Светлана Александровна\"},{\"name\":\"15589874\",\"caption\":\"Васева Татьяна Сергеевна\"},{\"name\":\"15589973\",\"caption\":\"Васенина Надежда Степановна\"},{\"name\":\"15589627\",\"caption\":\"Василинюк Елена Ивановна\"},{\"name\":\"15589610\",\"caption\":\"Голубович Лидия Ивановна\"},{\"name\":\"15589711\",\"caption\":\"Голубцова Алёна Сергеевна\"},{\"name\":\"15589713\",\"caption\":\"Джусупбекова Жанна Романовна\"},{\"name\":\"15589631\",\"caption\":\"Дружинина Елена Анатольевна\"},{\"name\":\"15589617\",\"caption\":\"Дурницина Зоя Антоновна\"},{\"name\":\"16038612\",\"caption\":\"Дурова Светлана Александровна\"},{\"name\":\"15589875\",\"caption\":\"Зубкова Ольга Викторовна\"},{\"name\":\"15589781\",\"caption\":\"Каган Татьяна Александровна\"},{\"name\":\"15589662\",\"caption\":\"Карпова Елена Ильинична\"},{\"name\":\"15589623\",\"caption\":\"Киселева Елена Николаевна\"},{\"name\":\"15589685\",\"caption\":\"Клепцина Елена Николаевна\"},{\"name\":\"15589622\",\"caption\":\"Котельникова Ирина Николаевна\"},{\"name\":\"15589609\",\"caption\":\"Куроптева Ольга Константиновна\"},{\"name\":\"15589619\",\"caption\":\"Лазукова Вера Васильевна\"},{\"name\":\"15589782\",\"caption\":\"Лекомцева Галина Борисовна\"},{\"name\":\"15589611\",\"caption\":\"Ломаева Светлана Евгеньевна\"},{\"name\":\"15589628\",\"caption\":\"Лукьянченко Ирина Михайловна\"},{\"name\":\"15589803\",\"caption\":\"Лядова Фарида Апышевна\"},{\"name\":\"16379632\",\"caption\":\"Магомедова Оксана Мусаибовна\"},{\"name\":\"15589804\",\"caption\":\"Манакова Инесса Анатольевна\"},{\"name\":\"16711125\",\"caption\":\"Мартын Екатерина Викторовна\"},{\"name\":\"16711307\",\"caption\":\"Мартын Екатерина Викторовна\"},{\"name\":\"15589618\",\"caption\":\"Мушакова Лариса Васифовна\"},{\"name\":\"15589614\",\"caption\":\"Напольских Елена Васильевна\"},{\"name\":\"15589876\",\"caption\":\"Ожгибесова Алена Игоревна\"},{\"name\":\"15589872\",\"caption\":\"Олейник Александра Наиловна\"},{\"name\":\"15589615\",\"caption\":\"Пьянкова Елена Ивановна\"},{\"name\":\"15964735\",\"caption\":\"Радионов Дмитрий Алексеевич\"},{\"name\":\"15589910\",\"caption\":\"Разепина Ольга Александровна\"},{\"name\":\"16723154\",\"caption\":\"Рычагова Елена Борисовна\"},{\"name\":\"15964532\",\"caption\":\"Сарапулова Тамара Геннадьевна\"},{\"name\":\"16050780\",\"caption\":\"Сарапулова Тамара Геннадьевна\"},{\"name\":\"15589620\",\"caption\":\"Соломатова Зинаида Борисовна\"},{\"name\":\"15992370\",\"caption\":\"Сухова Анастасия Александровна\"},{\"name\":\"16350445\",\"caption\":\"Ткаченко Ирина Николаевна\"},{\"name\":\"16316300\",\"caption\":\"Федосеев Данил Александрович\"},{\"name\":\"15589630\",\"caption\":\"Челухиди Татьяна Николаевна\"},{\"name\":\"15589616\",\"caption\":\"Черанева Наталья Владимировна\"},{\"name\":\"15589629\",\"caption\":\"Шавшукова Лилия Загировна\"},{\"name\":\"15589625\",\"caption\":\"Шилова Елена Павловна\"},{\"name\":\"16723153\",\"caption\":\"Шилова Ксения Алексеевна\"},{\"name\":\"15589943\",\"caption\":\"Шубина Вера Алексеевна\"},{\"name\":\"15964733\",\"caption\":\"Щёкина Ольга Владимировна\"}]}},{\"dimension\":\"EduLevel\",\"selection\":{\"values\":[{\"name\":\"1\",\"caption\":\"НОО\"},{\"name\":\"2\",\"caption\":\"ООО\"},{\"name\":\"3\",\"caption\":\"СОО\"}]}},{\"dimension\":\"Class\",\"selection\":{\"values\":[{\"name\":\"404077\",\"caption\":\"1\\\"А\\\"\"},{\"name\":\"405390\",\"caption\":\"1\\\"Б\\\"\"},{\"name\":\"405391\",\"caption\":\"1\\\"В\\\"\"},{\"name\":\"405394\",\"caption\":\"1\\\"Г\\\"\"},{\"name\":\"405396\",\"caption\":\"1\\\"Д\\\"\"},{\"name\":\"405395\",\"caption\":\"1\\\"Е\\\"\"},{\"name\":\"394614\",\"caption\":\"2\\\"А\\\"\"},{\"name\":\"394348\",\"caption\":\"2\\\"Б\\\"\"},{\"name\":\"394613\",\"caption\":\"2\\\"В\\\"\"},{\"name\":\"394345\",\"caption\":\"2\\\"Г\\\"\"},{\"name\":\"394616\",\"caption\":\"2\\\"Д\\\"\"},{\"name\":\"394595\",\"caption\":\"2\\\"Е\\\"\"},{\"name\":\"394624\",\"caption\":\"3\\\"А\\\"\"},{\"name\":\"394569\",\"caption\":\"3\\\"Б\\\"\"},{\"name\":\"394452\",\"caption\":\"3\\\"В\\\"\"},{\"name\":\"394459\",\"caption\":\"3\\\"Г\\\"\"},{\"name\":\"394414\",\"caption\":\"3\\\"Д\\\"\"},{\"name\":\"394502\",\"caption\":\"3\\\"Е\\\"\"},{\"name\":\"417514\",\"caption\":\"4\\\"А\\\"\"},{\"name\":\"417512\",\"caption\":\"4\\\"Б\\\"\"},{\"name\":\"417517\",\"caption\":\"4\\\"В\\\"\"},{\"name\":\"417518\",\"caption\":\"4\\\"Г\\\"\"},{\"name\":\"417819\",\"caption\":\"4\\\"Д\\\"\"},{\"name\":\"417522\",\"caption\":\"4\\\"Е\\\"\"},{\"name\":\"394532\",\"caption\":\"5\\\"А\\\"\"},{\"name\":\"394553\",\"caption\":\"5\\\"Б\\\"\"},{\"name\":\"394655\",\"caption\":\"5\\\"В\\\"\"},{\"name\":\"417503\",\"caption\":\"5\\\"Г\\\"\"},{\"name\":\"417507\",\"caption\":\"5\\\"Д\\\"\"},{\"name\":\"424691\",\"caption\":\"5\\\"Е\\\"\"},{\"name\":\"394471\",\"caption\":\"6\\\"А\\\"\"},{\"name\":\"394474\",\"caption\":\"6\\\"Б\\\"\"},{\"name\":\"394509\",\"caption\":\"6\\\"В\\\"\"},{\"name\":\"424688\",\"caption\":\"6\\\"Г\\\"\"},{\"name\":\"424689\",\"caption\":\"6\\\"Д\\\"\"},{\"name\":\"394589\",\"caption\":\"7\\\"А\\\"\"},{\"name\":\"394365\",\"caption\":\"7\\\"Б\\\"\"},{\"name\":\"394661\",\"caption\":\"7\\\"В\\\"\"},{\"name\":\"394430\",\"caption\":\"8\\\"А\\\"\"},{\"name\":\"394504\",\"caption\":\"8\\\"Б\\\"\"},{\"name\":\"394638\",\"caption\":\"8\\\"В\\\"\"},{\"name\":\"424687\",\"caption\":\"8\\\"Г\\\"\"},{\"name\":\"417117\",\"caption\":\"9\\\"А\\\"\"},{\"name\":\"417118\",\"caption\":\"9\\\"Б\\\"\"},{\"name\":\"417119\",\"caption\":\"9\\\"В\\\"\"},{\"name\":\"417110\",\"caption\":\"10\\\"А\\\"\"},{\"name\":\"424686\",\"caption\":\"11\\\"А\\\"\"}]}},{\"dimension\":\"FromDate\",\"selection\":{\"values\":[{\"name\":\"2022-09-01\",\"caption\":\"2022-09-01\"}]}},{\"dimension\":\"ToDate\",\"selection\":{\"values\":[{\"name\":\"" + dateTo + "\",\"caption\":\"" + dateTo + "\"}]}}]}}";
            var reportActivityJson = "{\"name\":\"REP_MON_ACTION_CLASS\",\"dimensions\":{\"rows\":[{\"dimension\":\"District\",\"selection\":{\"values\":[{\"name\":\"70\",\"caption\":\"Пермский городской округ\"}]}},{\"dimension\":\"OrgranizationType\",\"selection\":{\"values\":[{\"name\":\"3\",\"caption\":\"ВУЗ\"},{\"name\":\"5\",\"caption\":\"ДОУ\"},{\"name\":\"2\",\"caption\":\"ПОО\"},{\"name\":\"6\",\"caption\":\"Прочее\"},{\"name\":\"4\",\"caption\":\"УДО\"},{\"name\":\"1\",\"caption\":\"Школа\"}]}},{\"dimension\":\"School\",\"selection\":{\"values\":[{\"name\":\"1692\",\"caption\":\"МАОУ «СОШ № 24» г.Перми\"}]}},{\"dimension\":\"Class\",\"selection\":{\"values\":[{\"name\":\"404077\",\"caption\":\"1\\\"А\\\"\"},{\"name\":\"405390\",\"caption\":\"1\\\"Б\\\"\"},{\"name\":\"405391\",\"caption\":\"1\\\"В\\\"\"},{\"name\":\"405394\",\"caption\":\"1\\\"Г\\\"\"},{\"name\":\"405396\",\"caption\":\"1\\\"Д\\\"\"},{\"name\":\"405395\",\"caption\":\"1\\\"Е\\\"\"},{\"name\":\"394614\",\"caption\":\"2\\\"А\\\"\"},{\"name\":\"394348\",\"caption\":\"2\\\"Б\\\"\"},{\"name\":\"394613\",\"caption\":\"2\\\"В\\\"\"},{\"name\":\"394345\",\"caption\":\"2\\\"Г\\\"\"},{\"name\":\"394616\",\"caption\":\"2\\\"Д\\\"\"},{\"name\":\"394595\",\"caption\":\"2\\\"Е\\\"\"},{\"name\":\"394624\",\"caption\":\"3\\\"А\\\"\"},{\"name\":\"394569\",\"caption\":\"3\\\"Б\\\"\"},{\"name\":\"394452\",\"caption\":\"3\\\"В\\\"\"},{\"name\":\"394459\",\"caption\":\"3\\\"Г\\\"\"},{\"name\":\"394414\",\"caption\":\"3\\\"Д\\\"\"},{\"name\":\"394502\",\"caption\":\"3\\\"Е\\\"\"},{\"name\":\"417514\",\"caption\":\"4\\\"А\\\"\"},{\"name\":\"417512\",\"caption\":\"4\\\"Б\\\"\"},{\"name\":\"417517\",\"caption\":\"4\\\"В\\\"\"},{\"name\":\"417518\",\"caption\":\"4\\\"Г\\\"\"},{\"name\":\"417819\",\"caption\":\"4\\\"Д\\\"\"},{\"name\":\"417522\",\"caption\":\"4\\\"Е\\\"\"},{\"name\":\"394532\",\"caption\":\"5\\\"А\\\"\"},{\"name\":\"394553\",\"caption\":\"5\\\"Б\\\"\"},{\"name\":\"394655\",\"caption\":\"5\\\"В\\\"\"},{\"name\":\"417503\",\"caption\":\"5\\\"Г\\\"\"},{\"name\":\"417507\",\"caption\":\"5\\\"Д\\\"\"},{\"name\":\"424691\",\"caption\":\"5\\\"Е\\\"\"},{\"name\":\"394471\",\"caption\":\"6\\\"А\\\"\"},{\"name\":\"394474\",\"caption\":\"6\\\"Б\\\"\"},{\"name\":\"394509\",\"caption\":\"6\\\"В\\\"\"},{\"name\":\"424688\",\"caption\":\"6\\\"Г\\\"\"},{\"name\":\"424689\",\"caption\":\"6\\\"Д\\\"\"},{\"name\":\"394589\",\"caption\":\"7\\\"А\\\"\"},{\"name\":\"394365\",\"caption\":\"7\\\"Б\\\"\"},{\"name\":\"394661\",\"caption\":\"7\\\"В\\\"\"},{\"name\":\"394430\",\"caption\":\"8\\\"А\\\"\"},{\"name\":\"394504\",\"caption\":\"8\\\"Б\\\"\"},{\"name\":\"394638\",\"caption\":\"8\\\"В\\\"\"},{\"name\":\"424687\",\"caption\":\"8\\\"Г\\\"\"},{\"name\":\"417117\",\"caption\":\"9\\\"А\\\"\"},{\"name\":\"417118\",\"caption\":\"9\\\"Б\\\"\"},{\"name\":\"417119\",\"caption\":\"9\\\"В\\\"\"},{\"name\":\"417110\",\"caption\":\"10\\\"А\\\"\"},{\"name\":\"424686\",\"caption\":\"11\\\"А\\\"\"}]}},{\"dimension\":\"FromDate\",\"selection\":{\"values\":[{\"name\":\"2022-12-08\",\"caption\":\"2022-12-08\"}]}},{\"dimension\":\"ToDate\",\"selection\":{\"values\":[{\"name\":\"" + dateTo +"\",\"caption\":\"" + dateTo + "\"}]}}]}}";

            byte[] byteArray = null;

            switch (repType)
            {
                case "stat":
                    byteArray = Encoding.UTF8.GetBytes(reportStatJson);
                    break;
                case "themes":
                    byteArray = Encoding.UTF8.GetBytes(reportThemesJson);
                    break;
                case "homework":
                    byteArray = Encoding.UTF8.GetBytes(reportHomeworkJson);
                    break;
                case "activity":
                    byteArray = Encoding.UTF8.GetBytes(reportActivityJson);
                    break;
            }
            

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(executeURL);
            request.Method = "POST";
            request.CookieContainer = clientCookie;
            request.ContentType = "application/json";
            request.ContentLength = byteArray.Length;
            Stream stream = request.GetRequestStream();
            stream.Write(byteArray, 0, byteArray.Length);
            stream.Close();

            string request3;
            HttpWebResponse responce = (HttpWebResponse)request.GetResponse();

            var encoding = UTF8Encoding.UTF8;
            string responseText;
            using (var reader = new System.IO.StreamReader(responce.GetResponseStream(), encoding))
            {
                responseText = reader.ReadToEnd();
            }
            HomeWorkReport deserializedProduct = JsonConvert.DeserializeObject<HomeWorkReport>(responseText);

            deserializedProduct.cells.RemoveAt(deserializedProduct.cells.Count - 1);

            Logger.Error(deserializedProduct.caption);

            //first, create a dummy bitmap just to get a graphics object
            Image img = new Bitmap(1, 1);
            Graphics drawing = Graphics.FromImage(img);
            //measure the string to see how big the image needs to be

            //set the stringformat flags to rtl
            StringFormat sf = new StringFormat();
            //uncomment the next line for right to left languages
            //sf.FormatFlags = StringFormatFlags.DirectionRightToLeft;
            sf.Trimming = StringTrimming.Word;
            //free up the dummy image and old graphics object
            img.Dispose();
            drawing.Dispose();


            int wtr = 0;
            int maxStr = 0;

            List<List<string>> toWr = new List<List<string>>();



            switch (repType)
            {
                case "stat":
                    foreach (List<Cell> row in deserializedProduct.cells)
                    {
                        if (row[13].value != "100,00")
                        {
                            var msg = String.Format("{0} - {1} - {2}", row[3].value, row[7].value, row[14].value.Substring(0, 9));
                            maxStr = Math.Max(maxStr, msg.Length);
                            Logger.Waring(msg);

                            List<string> track = new List<string>();
                            track.Add(row[3].value);
                            track.Add(row[7].value);
                            track.Add(row[14].value.Substring(0, 9));
                            toWr.Add(track);
                        }
                    }
                    break;
                case "themes":
                    foreach (List<Cell> row in deserializedProduct.cells)
                    {
                        if (row[14].value != "100,00")
                        {
                            var msg = String.Format("{0} - {1} - {2}", row[4].value, row[8].value, row[15].value.Substring(0, 9));
                            maxStr = Math.Max(maxStr, msg.Length);
                            Logger.Waring(msg);

                            List<string> track = new List<string>();
                            track.Add(row[4].value);
                            track.Add(row[8].value);
                            track.Add(row[15].value.Substring(0, 9));
                            toWr.Add(track);
                        }
                    }
                    break;
                case "homework":
                    foreach (List<Cell> row in deserializedProduct.cells)
                    {
                        if (row[13].value != "100,00")
                        {
                            var msg = String.Format("{0} - {1} - {2}", row[3].value, row[7].value, row[14].value.Substring(0, 9));
                            maxStr = Math.Max(maxStr, msg.Length);
                            Logger.Waring(msg);

                            List<string> track = new List<string>();
                            track.Add(row[3].value);
                            track.Add(row[7].value);
                            track.Add(row[14].value.Substring(0, 10));
                            toWr.Add(track);
                        }
                    }
                    break;
                case "activity":
                    foreach (List<Cell> row in deserializedProduct.cells)
                    {
                        var msg = String.Format("{0} - {1} - {2} - {3} - {4} - {5} - {6} - {7}",
                            row[4].value, row[18].value, row[20].value, row[21].value, row[34].value, row[35].value, row[48].value, row[49].value
                            );
                        maxStr = Math.Max(maxStr, msg.Length);
                            
                        Logger.Waring(msg);

                        List<string> track = new List<string>();
                        track.Add(row[4].value);
                        track.Add(row[18].value);
                        track.Add(row[20].value);
                        track.Add(row[21].value);
                        track.Add(row[34].value);
                        track.Add(row[35].value);
                        track.Add(row[48].value);
                        track.Add(row[49].value);
                        toWr.Add(track);
                    }
                    maxStr -= 500;

                    break;
            }


            //create a new image of the right size
            img = new Bitmap(maxStr * 15, toWr.Count * 22);

            drawing = Graphics.FromImage(img);
            //Adjust for high quality
            drawing.CompositingQuality = CompositingQuality.HighQuality;
            drawing.InterpolationMode = InterpolationMode.HighQualityBilinear;
            drawing.PixelOffsetMode = PixelOffsetMode.HighQuality;
            drawing.SmoothingMode = SmoothingMode.HighQuality;
            drawing.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

            //paint the background
            drawing.Clear(Color.White);

            //create a brush for the text
            Brush textBrush = new SolidBrush(Color.Black);

            int maxName = 0;
            int maxObj = 0;

            switch (repType)
            {
                case "stat":
                    break;
                case "themes":
                case "homework":
                    foreach (List<string> str in toWr)
                    {
                        maxName = Math.Max(maxName, str[0].Length);
                        maxObj = Math.Max(maxName, str[1].Length);
                    }

                    foreach (List<string> str in toWr)
                    {
                        drawing.DrawString(str[0], new Font("Arial", 16), textBrush, new RectangleF(0, wtr * 22, 1920, 23), sf);
                        drawing.DrawString(str[1], new Font("Arial", 16), textBrush, new RectangleF(maxName * 15, wtr * 22, 1920, 23), sf);
                        drawing.DrawString(str[2], new Font("Arial", 16), textBrush, new RectangleF((maxName + maxObj) * 15, wtr++ * 22, 1920, 23), sf);
                    }
                    break;
                case "activity":

                    toWr[0][0] = "Класс";
                    toWr[0][1] = "ВСЕГО";
                    toWr[0][2] = "УЧЕНИК";
                    toWr[0][3] = "УЧЕНИК %";
                    toWr[0][4] = "РОДИТЕ";
                    toWr[0][5] = "РОДИТЕ %";
                    toWr[0][6] = "ИТОГ";
                    toWr[0][7] = "ИТОГ %";

                    foreach (List<string> str in toWr)
                    {
                        maxName = Math.Max(maxName, str[0].Length);
                        maxObj = Math.Max(maxName, str[1].Length);
                    }

                    foreach (List<string> str in toWr)
                    {
                        drawing.DrawString(str[0], new Font("Arial", 16), textBrush, new RectangleF(0, wtr * 22, 1920, 23), sf);
                        drawing.DrawString(str[1], new Font("Arial", 16), textBrush, new RectangleF(maxName * 15, wtr * 22, 1920, 23), sf);
                        drawing.DrawString(str[2], new Font("Arial", 16), textBrush, new RectangleF((maxName + 5) * 15, wtr * 22, 1920, 23), sf);
                        drawing.DrawString(str[3], new Font("Arial", 16), textBrush, new RectangleF((maxName + 11) * 15, wtr * 22, 1920, 23), sf);
                        drawing.DrawString(str[4], new Font("Arial", 16), textBrush, new RectangleF((maxName + 18) * 15, wtr * 22, 1920, 23), sf);
                        drawing.DrawString(str[5], new Font("Arial", 16), textBrush, new RectangleF((maxName + 24) * 15, wtr * 22, 1920, 23), sf);
                        drawing.DrawString(str[6], new Font("Arial", 16), textBrush, new RectangleF((maxName + 35) * 15, wtr * 22, 1920, 23), sf);
                        drawing.DrawString(str[7], new Font("Arial", 16), textBrush, new RectangleF((maxName + 40) * 15, wtr++ * 22, 1920, 23), sf);
                    }
                    break;
            }

            drawing.Save();

            textBrush.Dispose();
            drawing.Dispose();
            img.Save("tmpPhoto.jpg", ImageFormat.Jpeg);
            img.Dispose();


            //imageGenerator(full, 100);
            //var task = sendMsg("⚙️" + row[4].value + "\n" + row[8].value + "\n" + row[15].value);
            using (FileStream fs = File.OpenRead("tmpPhoto.jpg"))
            {
                var task = sendMsg(deserializedProduct.caption, fs, "646274400");
                task.Wait();
            }


            Logger.Success("[EPOS_Client] Report " + repType + " OK...");
        }

        void loginPassword()
        {
        
        }

        async Task sendMsg(string msg, Stream str, string usr)
        {
            //TEMP
            var botClient = new TelegramBotClient("");
            using CancellationTokenSource cts = new();
            Message sentMessage = await botClient.SendPhotoAsync(
                        chatId: "646274400",
                        str,
                        caption: msg,
                        cancellationToken: cts.Token
            );
        }

        void imageGenerator(string text, int Padding)
        {

            
        }

        private static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }

    }
}
