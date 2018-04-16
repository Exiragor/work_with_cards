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
        private string code;
        private int _value;
        private bool status_realized;
        private string created_at;
        private string realized_at;
        private Int32 created_at_unix;
        private Int32 realized_at_unix;

        public int Id { get; set; }

        public string Code
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

        public string Created_at
        {
            get { return created_at; }
            set { created_at = value; }
        }

        public string Realized_at
        {
            get { return realized_at; }
            set { realized_at = value; }
        }

        public Int32 Created_at_unix
        {
            get { return created_at_unix; }
            set { created_at_unix = value; }
        }

        public Int32 Realized_at_unix
        {
            get { return realized_at_unix; }
            set { realized_at_unix = value; }
        }

        public static int SetCards(string textbox, int value)
        {
            DatabaseContext db = Database.GetContext();
            int count = 0;

            Timestamp time = new Timestamp();
            time.SetTime();
            time.SetUnix();

            var cards = validateTextBox(textbox);

            foreach (var card in cards)
            {
                if (db.Cards
                        .Where(c => c.Code == card.Code)
                        .Where(c => c.Value == value)
                        .FirstOrDefault() != null)
                    continue;

                card.Created_at = time.StrValue;
                card.Created_at_unix = time.Unix;
                card.Value = value;

                db.Cards.Add(card);
                count++;
            }

            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return count;
        }


        public static int RealizeCards(string textbox, int value)
        {
            DatabaseContext db = Database.GetContext();
            int count = 0;

            var cards = validateTextBox(textbox);

            foreach (var card in cards)
            {
                var result = db.Cards
                        .Where(c => c.Code == card.Code)
                        .Where(c => c.Value == value)
                        .FirstOrDefault();

                if (result == null) continue;

                result.Status_realized = true;
                count++;
            }

            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
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

        public static Card[] getCards(bool realized, Int32 intervalFrom, Int32 intervalTo)
        {
            DatabaseContext db = Database.GetContext();


            return db.Cards
                .Where(c => c.Status_realized == realized)
                //.Where(c => c.Date_create >= intervalFrom)
                //.Where(c => c.Date_create <= intervalTo)
                .OrderBy(c => c.Value)
                .ToArray();
        }

        private static List<Card> validateTextBox(string textbox)
        {
            string[] sep = { "\n" };
            string[] rows = textbox.Split(sep, StringSplitOptions.RemoveEmptyEntries);
            var regex = @"\b\d{4}\b";
            List<Card> cards = new List<Card>();

            foreach (var row in rows)
            {
                var match = Regex.Match(row, regex, RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    Card card = new Card();
                    card.Code = row;
                    cards.Add(card);
                }
            }

            return cards;
        }
    }
}
