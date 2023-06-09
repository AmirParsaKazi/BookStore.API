﻿using BookStore.Common.Dtos.Province;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Common.Dtos.City;

public class CitiesList {

    public CitiesList(string Id, string Name)
    {
        this.Id = Id;
        this.Name = Name;
    }

    public string Id { get; set; }
    public string Name { get; set; }
    public ProvincesList Province { get; set; }
}
