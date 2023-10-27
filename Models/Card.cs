using System;
using System.Collections.Generic;

namespace CardsApi.Models
{
    public partial class Card
    {
        public Card()
        {
            Clients = new HashSet<Client>();
        }

        public short Id { get; set; }
        public string? CardName { get; set; }
        public short? Currency { get; set; }

        public virtual Currency? CurrencyNavigation { get; set; }
        public virtual ICollection<Client> Clients { get; set; }
    }
}
