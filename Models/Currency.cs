using System;
using System.Collections.Generic;

namespace CardsApi.Models
{
    public partial class Currency
    {
        public Currency()
        {
            Cards = new HashSet<Card>();
        }

        public string? CurrencyName { get; set; }
        public short Id { get; set; }

        public virtual ICollection<Card> Cards { get; set; }
    }
}
