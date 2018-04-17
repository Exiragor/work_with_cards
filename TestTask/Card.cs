using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace TestTask
{
    public class Card
    {
        private string code; //уникальный код карты
        private int _value; //номинал
        private bool status_realized; //реализованная ли карта
        private string created_at; //Дата создания
        private string realized_at; //Дата реализации
        private Int32 created_at_unix; //Дата создания в unix
        private Int32 realized_at_unix; //Дата реализация в unix
        private static Count count; //Класс для работы с подсчетами

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

        // Сохраняем карты из текстбокса по номиналу
        public static Count SetCards(string textbox, int value)
        {
            DatabaseContext db = Database.GetContext();
            count = new Count(0, 0);

            Timestamp time = new Timestamp();
            time.SetTime();
            time.SetUnix();

            List<Card> cards = validateTextBox(textbox);

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
                count.SuccessTick();
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

        // Реализуем карты из текстбокса по номиналу
        public static Count RealizeCards(string textbox, int value)
        {
            DatabaseContext db = Database.GetContext();
            Timestamp time = new Timestamp();
            time.SetTime();
            time.SetUnix();
            count = new Count(0, 0);

            var cards = validateTextBox(textbox);

            foreach (var card in cards)
            {
                var result = db.Cards
                        .Where(c => c.Code == card.Code)
                        .Where(c => c.Value == value)
                        .FirstOrDefault();

                if (result == null) continue;

                result.Status_realized = true;
                result.Realized_at = time.StrValue;
                result.Realized_at_unix = time.Unix;

                count.SuccessTick();
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

        // Получаем реализованные или нереализованные карты
        public static Card[] GetCards(bool realized)
        {
            DatabaseContext db = Database.GetContext();

            return db.Cards
                .Where(c => c.Status_realized == realized)
                .OrderBy(c => c.Value)
                .ToArray();
        }

        // Получаем реализованные или нереализованные карты по дате 
        public static Card[] GetCards(bool realized, string date)
        {
            DatabaseContext db = Database.GetContext();

            return db.Cards
                .Where(c => c.Status_realized == realized)
                .Where(c => c.Realized_at == date)
                .OrderBy(c => c.Value)
                .ToArray();
        }

        // Получаем реализованные или нереализованные карты на интервале
        public static Card[] GetCards(bool realized, Int32 intervalFrom, Int32 intervalTo)
        {
            DatabaseContext db = Database.GetContext();

            return db.Cards
                .Where(c => c.Status_realized == realized)
                .Where(c => c.Created_at_unix >= intervalFrom)
                .Where(c => c.Created_at_unix <= intervalTo)
                .OrderBy(c => c.Value)
                .ToArray();
        }

        // Получаем первую карту
        public static Card GetFirst()
        {
            DatabaseContext db = Database.GetContext();

            return db.Cards.FirstOrDefault();
        }

        // Получаем количество реализованных карт, разбитых по номиналу
        public static int[] GetCountCards(Card[] cards)
        {
            int k1 = 0, k3 = 0, k5 = 0;

            foreach (var card in cards)
            {
                if (card.Value == 1000)
                {
                    k1++;
                }
                else if (card.Value == 3000)
                {
                    k3++;
                }
                else
                {
                    k5++;
                }
            }

            return new int[3] { k1, k3, k5 };
        }

        // Отбираем уникальные коды из текстбокса
        private static List<Card> validateTextBox(string textbox)
        {
            string[] sep = { "\n" };
            string[] rows = textbox.Split(sep, StringSplitOptions.RemoveEmptyEntries);
            var regex = @"\b\d{4}\b";
            List<Card> cards = new List<Card>();

            foreach (var row in rows)
            {
                count.CommonTick();

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
