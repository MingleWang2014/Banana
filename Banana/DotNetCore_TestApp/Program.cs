﻿/***********************************
 * Coder：EminemJK
 * Date：2018-12-04
 **********************************/

using System;
using Banana.Uow;
using Dapper.Contrib.Extensions;
using Banana.Uow.Models;
using System.Collections.Generic;
using Banana.Uow.Extension;
using Dapper;
using System.Linq;
using System.Threading.Tasks;
using Banana.Utility.Common;

namespace DotNetCore_TestApp
{
    class Program
    {
        static string strConn = @"Data Source=.;Initial Catalog = AdminLTE.Net.DB;User ID=sa;Password =mimashi123";
        static void Main(string[] args)
        {
            TestPostgres();
            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }

        static async void TestAsync()
        {
            var repoUserInfo = new Repository<UserInfo>();

            var page = await repoUserInfo.QueryAsync(1); 
            var page1 = await repoUserInfo.QueryListAsync(1, 10, "sex=@sex", new { sex = 1 }, order: "createTime", asc: false);
            var page2 = await repoUserInfo.QueryListAsync(2, 10, "sex=@sex", new { sex = 1 });
            var info = await repoUserInfo.QueryListAsync("UserName=@userName and Password =@psw", new { userName = "admin", psw = "25d55ad283aa400af464c76d713c07ad" });

            var count = await repoUserInfo.QueryCountAsync();
            Show("async");
            Show(DateTime.Now.ToString());
            Show(page);
            Show(DateTime.Now.ToString());
            Show(page1.data);
            Show(DateTime.Now.ToString());
            Show(page2.data);
            Show(DateTime.Now.ToString());
            Show(info);
            Show(DateTime.Now.ToString());
            Show("Count："+count); 

            var deleteAsync = await repoUserInfo.DeleteAsync("HeaderImg is Null", null);
        }

        #region Show
        static void Show(IEnumerable<UserInfo> infos)
        {
            foreach (var info in infos)
            {
                Show(info);
            }
        }

        static void Show(UserInfo info)
        {
            Show(info.ToString());
        }

        static void Show(string info)
        {
            Console.WriteLine(info);
        } 
        #endregion


        static List<UserInfo> TestData()
        { 
            List<UserInfo> data = new List<UserInfo>();

            data.Add(new UserInfo() { Name = "Monkey D. Luffy", Phone = "15878451111", Password = "12345678", Sex = 1, UserName = "Luffy", CreateTime = DateTime.Now, Enable = 1 });
            data.Add(new UserInfo() { Name = "索隆", Phone = "13355526663", Password = "12345678", Sex = 1, UserName = "Zoro", CreateTime = DateTime.Now, Enable =1 });
            data.Add(new UserInfo() { Name = "娜美", Phone = "15878451111", Password = "12345678", Sex = 0, UserName = "Nami", CreateTime = DateTime.Now, Enable = 1 });
            data.Add(new UserInfo() { Name = "山治", Phone = "17755602229", Password = "12345678", Sex = 1, UserName = "Sanji", CreateTime = DateTime.Now, Enable = 1 });
            data.Add(new UserInfo() { Name = "乌索普", Phone = "14799995555", Password = "12345678", Sex = 1, UserName = "Usopp", CreateTime = DateTime.Now, Enable = 1 });

            data.Add(new UserInfo() { Name = "乔巴", Phone = "18966660000", Password = "12345678", Sex = 1, UserName = "Chopper", CreateTime = DateTime.Now, Enable = 1 });
            data.Add(new UserInfo() { Name = "罗宾", Phone = "13122227878", Password = "12345678", Sex = 0, UserName = "Robin", CreateTime = DateTime.Now, Enable = 1 });
            data.Add(new UserInfo() { Name = "弗兰奇", Phone = "15962354412", Password = "12345678", Sex = 1, UserName = "Franky", CreateTime = DateTime.Now, Enable = 1 });
            data.Add(new UserInfo() { Name = "布鲁克", Phone = "14322221111", Password = "12345678", Sex = 1, UserName = "Brook", CreateTime = DateTime.Now, Enable = 1 });
            data.Add(new UserInfo() { Name = "甚平", Phone = "15655479960", Password = "12345678", Sex = 1, UserName = "Jinbe", CreateTime = DateTime.Now, Enable = 1 });
            return data;
        }

        static void TestSQLServer()
        {
            ConnectionBuilder.ConfigRegist(strConn, Banana.Uow.Models.DBType.SqlServer);
            var repoUserInfo = new Repository<UserInfo>(); 

            var model = repoUserInfo.Query(47);
            model.Phone = "12345678";
            repoUserInfo.Update(model); 
            model = repoUserInfo.Query(47);

            var page1 = repoUserInfo.QueryList(1, 10, "sex=@sex", new { sex = 1 }, order: "createTime", asc: false);
            var page2 = repoUserInfo.QueryList(2, 10, "sex=@sex", new { sex = 1 });
            var page3 = repoUserInfo.QueryList(3, 10, "sex=@sex", new { sex = 1 });
            var page4 = repoUserInfo.QueryList(2, 5);
            var page5 = repoUserInfo.QueryList(3, 5);

            var userModel = repoUserInfo.QueryList("UserName=@userName and Password =@psw", new { userName = "admin", psw = "25d55ad283aa400af464c76d713c07ad" }).FirstOrDefault();
            //var repo = new Repository<Category>();
            //var list = repo.QueryList("where ParentNamePath like @ParentNamePath", new { ParentNamePath = "%,电气设备,%" });
        }


        static void TestPostgres()
        {
            ConnectionBuilder.ConfigRegist("PORT=5432;DATABASE=postgres;HOST=localhost;PASSWORD=mimashi123;USER ID=postgres", Banana.Uow.Models.DBType.Postgres);
            var repoUserInfo = new Repository<UserModel>();
            //区分大小写，包括字段
            //repoUserInfo.Execute( @"CREATE TABLE t_user( 
            //                            id         SERIAL      PRIMARY KEY,
            //                            username    CHAR(50)    NOT NULL,
            //                            password    CHAR(50)    NOT NULL,
            //                            name        CHAR(50), 
            //                            phone       CHAR(11),
            //                            sex int,enable int,
            //                            createtime date); ", null);
            //var models = ModelConvertUtil<UserInfo, UserModel>.ModelCopy(TestData());
            //foreach (var data in models)
            //{
            //    repoUserInfo.Insert(data);
            //}
            var list = repoUserInfo.QueryList();

            var page1 = repoUserInfo.QueryList(1, 5);
            var page2 = repoUserInfo.QueryList(2, 5);
            var page3 = repoUserInfo.QueryList(3, 5);

            var model = repoUserInfo.Query(2);
            //bool b = repoUserInfo.Delete(model);
            //list = repoUserInfo.QueryList();

            model = repoUserInfo.Query(3);
            model.phone = "1234567";
            bool ub = repoUserInfo.Update(model);
            list = repoUserInfo.QueryList(order:"order by id");
        }


        static void TestUow()
        {
            //ConnectionBuilder.ConfigRegist(strConn, Banana.Uow.Models.DBType.SqlServer);
            //using (UnitOfWork uow = new UnitOfWork())
            //{
            //    var studentRepo = uow.Repository<Student>();
            //    var model = new Student("啊啊", 1, 1);
            //    var sid = studentRepo.Insert(model);

            //    var classRepo = uow.Repository<MClass>();
            //    var cid = classRepo.Insert(new MClass("五年级"));
            //    if (sid > 0 && cid < 0)
            //    {
            //        uow.Commit();
            //    }
            //    else
            //    {
            //        uow.Rollback();
            //    }
            //}
        }
    }
}
