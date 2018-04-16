using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;

namespace TestTask
{
    public class Card
    {
        private int code;
        private int _value;
        private bool status_realized;
        private string date_create;
        private string date_realized;

        public int Id { get; set; }

        public int Code
        {
            get { return code; }
            set { code = value; }
        }

        public int Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public bool Status_realized
        {
            get { return status_realized; }
            set { status_realized = value; }
        }

        public string Date_create
        {
            get { return date_create; }
            set { date_create = value; }
        }

        public string Date_realized
        {
            get { return date_realized; }
            set { date_realized = value; }
        }

        public static int setCards(string textbox, int value)
        {
            DatabaseContext db = Database.GetContext();

            string[] sep = { "\n" };
            int count = 0;
            string[] rows = textbox.Split(sep, StringSplitOptions.RemoveEmptyEntries);
            var regex = @"\b\d{4}\b";

            DateTime localDate = DateTime.Now;
            Card card = new Card();
            card.Value = value;
            card.Date_create = localDate.ToString();

            foreach (var row in rows)
            {
                var match = Regex.Match(row, regex, RegexOptions.IgnoreCase);

                if (match.Success)
                {
                    card.Code = Int32.Parse(row);
                    if (db.Cards
                        .Where(c => c.Code == card.Code)
                        .Where(c => c.Value == value)
                        .FirstOrDefault() != null)
                        continue;

                    db.Cards.Add(card);
                    try
                    {
                        db.SaveChanges();
                        count++;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }

            return count;
            
        }

        public static int realizeCards(string textbox, int value)
        {
            DatabaseContext db = Database.GetContext();

            string[] sep = { "\n" };
            int count = 0;
            string[] rows = textbox.Split(sep, StringSplitOptions.RemoveEmptyEntries);
            var regex = @"\b\d{4}\b";

            Card card = new Card();

            foreach (var row in rows)
            {
                var match = Regex.Match(row, regex, RegexOptions.IgnoreCase);

                if (match.Success)
                {
                    card.Code = Int32.Parse(row);
                    var result = db.Cards
                        .Where(c => c.Code == card.Code)
                        .Where(c => c.Value == value)
                        .FirstOrDefault();

                    if (result == null) continue;

                    result.Status_realized = true;
                    try
                    {
                        db.SaveChanges();
                        count++;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }

            return count;

        }

        public static Card[] getCards(bool realized)
        {
            DatabaseContext db = Database.GetContext();

            return db.Cards
                .Where(c => c.Status_realized == realized)
                .OrderBy(c => c.Value)
                .ToArray();
        }
    }
}
