﻿namespace AccountErp.Models.Address
{
    public class AddressModel
    {
        public int? Id { get; set; }
        public int? CountryId { get; set; }
        public string StreetNumber { get; set; }
        public string StreetName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Phone { get; set; }
    }
}
