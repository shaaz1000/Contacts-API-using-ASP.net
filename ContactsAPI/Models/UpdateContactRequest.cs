﻿using System;
namespace ContactsAPI.Models
{
	public class UpdateContactRequest
	{
        public String Name { get; set; }
        public String Email { get; set; }
        public long Phone { get; set; }
        public String Address { get; set; }
    }
}

