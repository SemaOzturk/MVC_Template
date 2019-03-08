using MVC_Template.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Template.Add_Classes
{
    public class Sepet
    {
        private List<Product> urunler = new List<Product>();

        public List<Product> Urunler
        {
            get {return urunler;}
            set {urunler = value;}
        }
    }
}