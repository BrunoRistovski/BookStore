﻿using Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IBooksInBookStoresService
    {
        void Confirm(AddBookToBookStore dto);
    }
}
