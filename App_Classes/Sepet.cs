using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bijuteri.App_Classes
{
    public class Sepet
    {
        public static Sepet AktifSepet
        {
            get
            {
                HttpContext ctx = HttpContext.Current;  //sisteme giris yapmis kullanicinin hesabini tutar
                if (ctx.Session["AktifSepet"] == null)
                {
                    ctx.Session["AktifSepet"] =new Sepet();
                }

                return (Sepet)ctx.Session["AktifSepet"];
            }
        }

        //Sepete eklenmis urunlerin listesini tutacak field
        private List<SepetItem> urunler = new List<SepetItem>() ;

        //property
        public List<SepetItem> Urunler
        {
            get 
            { 
                return urunler;
            }
            set { urunler = value; }
        }

        //Sepete urun ekleme
        public void SepeteEkle(SepetItem si)
        {
            if (HttpContext.Current.Session["AktifSepet"] != null) //sepette urun var
            {
                Sepet s = (Sepet)HttpContext.Current.Session["AktifSepet"];
                //any() : icinde yazan kosula uyan durumda true dondurur
                if (s.Urunler.Any(x => x.Urun.Id == si.Urun.Id))
                {
                    s.Urunler.FirstOrDefault(x => x.Urun.Id == si.Urun.Id).Adet++; //ayni urunden alindiginda adetini artiracak
                }
                else
                {
                    s.Urunler.Add(si);
                }
            }
            else //sepet yok
            {
                Sepet s = new Sepet();
                s.Urunler.Add(si);
                HttpContext.Current.Session["AktifSepet"] = s;
            }

            
        }

        //sepetteki butun urunlerin toplam fiyati
        public decimal ToplamTutar
        {
            get
            {
                return Urunler.Sum(x => x.Tutar);
            }
        }

    }


    public class SepetItem
    {
        public Urun Urun { get; set; }

        public int Adet { get; set; }

        public double Indirim { get; set; }  //yuzde olarak tutuluyor

        public decimal Tutar
        {
            get
            {
                return Urun.SatisFiyat * Adet * (decimal)(1 - Indirim);

            }
        }
    }
}