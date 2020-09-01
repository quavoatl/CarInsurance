using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using CarInsurance.DatabaseDemo.DbModels;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CarInsurance.DatabaseDemo
{
    public class Class1
    {
        public static string path1 = Path.Combine(Environment.CurrentDirectory, "CoverTheftLimit.json");
        public static string path2 = Path.Combine(Environment.CurrentDirectory, "CoverTheftQuestion.json");

        public static void Main(string[] args = null)
        {
            //    var broker1 = RetrieveFromDb(1);
            //    Console.WriteLine($"{broker1.Name}, id = {broker1.Id} = adresa din detalii : {broker1.BrokerDetails.Address}");
            //

            //CreateJsonCoverLimit();
            //ReadFromJson();
            //AddCoverToDb(1);
            var broker = RetrieveFromDb(1);
        }

        public static void AddCoverToDb(int id)
        {
            using (var db = new DemoDbContext())
            {
                var idValue = db.Broker.Find(id).Id;

                db.Add(new CoverTheft()
                {
                    CoverId = idValue,
                    Limits = File.ReadAllText(path1),
                    Questions = File.ReadAllText(path2)
                });

                db.SaveChanges();
            }
        }

        public static void CreateJsonCoverLimit()
        {
            CoverTheft coverTheft = new CoverTheft();
            Limit limit1 = new Limit("Limit1", new List<int>() { 100, 500, 1000, 5000, 10000 });
            Limit limit2 = new Limit("Limit2", new List<int>() { 500, 1000, 10000, 50000 });
            Question q1 = new Question("Q1", new List<string>() { "Are you mad?", "Do you park your car outside?", "Do you own a garage?" });
            coverTheft.LimitsList = new List<Limit>() { limit1, limit2 };
            coverTheft.QuestionsList = new List<Question>() { q1 };
            var path1 = Path.Combine(Environment.CurrentDirectory, "CoverTheftLimit.json");
            var path2 = Path.Combine(Environment.CurrentDirectory, "CoverTheftQuestion.json");

            using (var sWriter = File.CreateText(path1))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(sWriter, coverTheft.LimitsList);
            }

            using (var sWriter = File.CreateText(path2))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(sWriter, coverTheft.QuestionsList);
            }
        }

        public static void ReadFromJson()
        {
            List<Limit> limits = null;
            List<Question> questions = null;

            var path1 = Path.Combine(Environment.CurrentDirectory, "CoverTheftLimit.json");
            var path2 = Path.Combine(Environment.CurrentDirectory, "CoverTheftQuestion.json");
            using (var sReader = File.OpenText(path1))
            {
                JsonSerializer serializer = new JsonSerializer();
                limits = (List<Limit>)serializer.Deserialize(sReader, typeof(List<Limit>));
            }

            using (var sReader = File.OpenText(path2))
            {
                JsonSerializer serializer = new JsonSerializer();
                questions = (List<Question>)serializer.Deserialize(sReader, typeof(List<Question>));
            }
        }

        public static Broker RetrieveFromDb(int id)
        {
            using (var db = new DemoDbContext())
            {
                Broker broker = null;
                broker = db.Broker.Include(broker => broker.BrokerDetails)
                    .Where(broker => broker.Id == id).FirstOrDefault();
                broker.PolicyTemplate.ListOfCovers.Add(db.CoverTheft.Where(coverId => coverId.CoverId == broker.Id).FirstOrDefault());
                return broker;
            }
        }

        public static Broker RetrieveCoverFromDb(int id)
        {
            using (var db = new DemoDbContext())
            {
                return db.Broker.Include(broker => broker.BrokerDetails)
                    .Where(broker => broker.Id == id).FirstOrDefault();
            }
        }


        public static void AddToDb()
        {
            using (var db = new DemoDbContext())
            {
                db.Add(new Broker()
                {
                    Name = "Cacat",
                    CreationDate = DateTime.Now,
                    Username = "asd",
                    Password = "asd",

                    BrokerDetails = new BrokerDetails()
                    {
                        Address = "asd",
                        PhoneNo = "123",
                        Postcode = "123"
                    }
                });

                db.SaveChanges();
            }
        }
    }
}